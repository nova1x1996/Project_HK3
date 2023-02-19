﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;
using Project.Helper;
using Project.Models;
using System.Diagnostics.Eventing.Reader;

namespace Project.Controllers
{
    public class RechargeController : Controller
    {
        private DatabaseContext db;
        public RechargeController(DatabaseContext _db)
        {
            db = _db;
        }
        
        public IActionResult Index(string? id)
        {
          
            var model = db.Customers.Include(c=>c.package).SingleOrDefault(r=>r.user_id.Equals(id));
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string customerId,string? s)
        {
            var model = db.Customers.Include(c=>c.package).SingleOrDefault(c=>c.id==int.Parse(customerId));
            
            if(model != null)
            {
                return View(model);
            }
            
            return View();
        }

        //========================PayPal==============================

        protected string PayPal_Recharge(int month,int customer_id,int packageId)
        {

            var package = db.Packages.Find(packageId);

            int totalMoney = (int)package.price * month;


            var APIContext = HelperPayPal.GetAPIContext();

            // Thiết lập chi tiết đơn hàng
            var itemList = new ItemList
            {
                items = new List<Item>
                                            {
                                                new Item
                                                {


                                                    description = customer_id.ToString(),
                                                    name = package.id.ToString(),
                                                    currency = "USD",
                                                    price = package.price.ToString(),
                                                    quantity = month.ToString(),
                                     
                                                }
                                            }
            };

            // Tính tổng giá trị đơn hàng
            var total = new Amount
            {
                currency = "USD",
                total = totalMoney.ToString()
            };

            // Tạo chi tiết thanh toán
            var payment = new Payment
            {

                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                            {
                                new Transaction
                                {
                                    item_list = itemList,
                                    amount = total,
                                    description = "Description about the payment",
                                    invoice_number = DateTime.Now.ToString(),
                                    payment_options = new PaymentOptions
                                    {
                                        allowed_payment_method = "INSTANT_FUNDING_SOURCE"
                                    }
                                }
                            },
                redirect_urls = new RedirectUrls
                {
                    return_url = "http://localhost:5296/Recharge/btnPayment",
                    cancel_url = "http://localhost:5296/Home/Index"
                }
            };

            // Tạo thanh toán và lưu trữ thông tin trong session để sử dụng sau này
            var createdPayment = payment.Create(APIContext);
            HttpContext.Session.SetString("payment_id", createdPayment.id);
            string s = "";
            // Chuyển hướng đến trang thanh toán PayPal để người dùng thanh toán
            var links = createdPayment.links.GetEnumerator();
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    // Sử dụng JavaScript để chuyển hướng đến trang thanh toán
                    s = link.href;
                }
            }
            return s;
        }
        [HttpGet()]
        public IActionResult btnPayment(string PayerID)
        {
            // Lấy thông tin thanh toán từ session
            var APIContext = HelperPayPal.GetAPIContext();
            var paymentId = HttpContext.Session.GetString("payment_id");
            var payment = Payment.Get(APIContext, paymentId);

            // Xác nhận thanh toán
            var paymentExecution = new PaymentExecution { payer_id = PayerID };

            var executedPayment = payment.Execute(APIContext, paymentExecution);



            // Kiểm tra trạng thái thanh toán
            if (executedPayment.state.ToLower() != "approved")
            {
                // Xử lý khi thanh toán không thành công
                return RedirectToAction("PaymentFailed","CustomerOrder");
            }
            else
            {

                var items = executedPayment.transactions[0].item_list.items.FirstOrDefault();
                var total_money = executedPayment.transactions[0].amount.total;
                var pay_type = executedPayment.payer.payment_method;
                var customerId = items.description;
                var packageId = items.name;
                var month = items.quantity;

                var customer = db.Customers.Find(int.Parse(customerId));
                var package = db.Packages.Find(int.Parse(packageId));


                var reCharge = new Recharge();
                reCharge.customer_id = customer.id;
                reCharge.card_number = customer.card_number;
                reCharge.package_id = package.id;
                reCharge.pay_type = pay_type;
                reCharge.state = true;
                reCharge.month = int.Parse(month);
                reCharge.date = DateTime.Now;
                db.Add(reCharge);

                //Cộng thêm tháng vô ngày hết hạn
                var n = (DateTime)(customer.date_left);

                customer.date_left = n.AddMonths(int.Parse(month));
                db.SaveChanges();

                

            }
            return RedirectToAction("PaymentSuccess", "CustomerOrder");
        }
        //========================PayPAl===========================

        [HttpPost]
        public IActionResult RechargeOrder(string pay_type,int month, int packageId , int CustomerIdOrder,string card_number)
        {
            var customer = db.Customers.Find(CustomerIdOrder);




            if (pay_type.Equals("cod"))
            {
                var reCharge = new Recharge();
                reCharge.customer_id = customer.id;
                reCharge.card_number = card_number;
                reCharge.package_id = packageId;
                reCharge.pay_type = pay_type;
                reCharge.state = false;
                reCharge.month = month;
                reCharge.date = DateTime.Now;



                db.Add(reCharge);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else if(pay_type.Equals("paypal"))
            {
               return Redirect(PayPal_Recharge(month, CustomerIdOrder, packageId));
            }

            return View();
        }
       
    }
}