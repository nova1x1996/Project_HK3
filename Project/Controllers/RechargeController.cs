using AspNetCoreHero.ToastNotification.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PayPal.Api;
using Project.Helper;
using Project.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

using System.Security.Claims;
using System.Text;

namespace Project.Controllers
{
    public class RechargeController : Controller
    {
        private DatabaseContext db;
        private INotyfService notyf;
        public RechargeController(DatabaseContext _db, INotyfService _notyf)
        {
            db = _db;
            notyf = _notyf;
        }

     

        public IActionResult Index(string? id)
        {
          
            var model = db.Customers.Include(c=>c.package).SingleOrDefault(r=>r.user_id.Equals(id));
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string customerId,string? s)
        {
            if(customerId == null)
            {
                notyf.Error("Customer ID is not entered !");
                return View();
            }
            var model = db.Customers.Include(c=>c.package).SingleOrDefault(c=>c.id==int.Parse(customerId));
            
            if(model == null)
            {
                notyf.Error("Customer ID does not exist !");
                return View();
            }

            return View(model);
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
            return RedirectToAction("PaymentSuccess");
        }
        //========================PayPAl===========================


        public IActionResult PaymentSuccess()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RechargeOrder(string pay_type,int month, int packageId , int CustomerIdOrder,string card_number)
        {

            var customer = db.Customers.Find(CustomerIdOrder);


            var changePack = db.ChangePackages.Where(cp => cp.customer_id == customer.id && cp.state == false).FirstOrDefault();
            var reCharge2 = db.Recharges.Where(r => r.customer_id == customer.id && r.state == false).FirstOrDefault();
            if(reCharge2 != null)
            {
                notyf.Error("You still have an unpaid Recharge order.");
                return RedirectToAction("Index", "Home");
            }else if (changePack != null) {
                notyf.Error("You still have an unpaid Update Package order.");
                return RedirectToAction("Index", "Home");
            }
        
        
            
      
        

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
                notyf.Success("You have successfully created a recharge request. We will contact you soon.");
                return RedirectToAction("Index", "Home");
            }
            else if(pay_type.Equals("paypal"))
            {
               return Redirect(PayPal_Recharge(month, CustomerIdOrder, packageId));
            }

            return View();
        }



        [HttpGet]
        public IActionResult History()
        {


            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).FirstOrDefault();

            var reChargeList = db.Recharges.Where(cp => cp.customer_id == customer.id).Include(cp=>cp.GetPackage).ToList();
          

            return View(reChargeList);
        }

        public IActionResult Delete(int id)
        {
            var model = db.Recharges.Find(id);
            db.Recharges.Remove(model);
            db.SaveChanges();
            notyf.Success("Your Order Recharge has been delete !");
            return RedirectToAction("History", "Recharge");

        }

    }
}
