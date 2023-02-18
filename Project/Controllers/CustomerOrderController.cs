using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using Project.Models;
using System.Configuration;
using System.Security.Claims;
using Project.Helper;
using AspNetCoreHero.ToastNotification.Notyf.Models;
using System.Security.Policy;
using System.Diagnostics.Eventing.Reader;

namespace Project.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly DatabaseContext db;
       
        private static string Mode = "sb-pjm9425080818@business.example.com";
        
        public CustomerOrderController(DatabaseContext _db, IConfiguration config)
        {
            db = _db;
            
       
        }
 
        


                 protected string  Page_Load(Package package,Customer customer)
        {
          


            var APIContext = HelperPayPal.GetAPIContext();

                            // Thiết lập chi tiết đơn hàng
                            var itemList = new ItemList
                            {
                                items = new List<Item>
                                            {
                                                new Item
                                                {
                                                        
                                                    
                                                    description = customer.id.ToString(),
                                                    name = package.name,
                                                    currency = "USD",
                                                    price = (package.price).ToString(),
                                                    quantity = "1",
                                                    sku = "sku001"
                                                }
                                            }
                            };

                            // Tính tổng giá trị đơn hàng
                                            var total = new Amount
                            {
                                currency = "USD",
                                total = (package.price).ToString()
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
                                    return_url = "http://localhost:5296/CustomerOrder/btnPayment",
                                    cancel_url = "http://localhost:5296/Package/Index"
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
                return RedirectToAction("PaymentFailed");
            }
            else
            {

                var items = executedPayment.transactions[0].item_list.items.FirstOrDefault();
                var total_money = executedPayment.transactions[0].amount.total;
                var pay_type = executedPayment.payer.payment_method;
                var customerId = items.description;
                var package = db.Packages.Where(p => p.name.Equals(items.name)).SingleOrDefault();
                

                var Mo = new Customer_order();

                Mo.total_money = decimal.Parse(total_money);
                Mo.pay_type = pay_type;
                Mo.customer_id = int.Parse(customerId);
                Mo.package_id = package.id;
                Mo.date = DateTime.Now;
                db.Customer_orders.Add(Mo);

                //Điền thông tin gói cước vào Customer

                var customer = db.Customers.Find(int.Parse(customerId));
                customer.payment_monthly = package.price;
                customer.package_id = package.id;
                customer.services_sub_date = DateTime.Now;
                customer.date_left = DateTime.Now.AddMonths(package.duration.Value);
                db.SaveChanges();


                // Xử lý khi thanh toán thành công
                return RedirectToAction("PaymentSuccess");
            }
        }

        //Order
        [HttpGet()]
        public IActionResult PackageOrder(int id)
        {
            var model = db.Packages.Find(id);

            return View(model);
        }

        [HttpPost()]
        public IActionResult PackageOrder(string pay_type, string total_money, int package_id)
        {
            //Lấy Id của Phiên Đăng nhập hiện tại
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Tìm Customer dựa trên ID của Phiên Đăng nhập hiện tại
            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();
            var package = db.Packages.Find(package_id);


            if (pay_type.Equals("paypal")){

                return Redirect(Page_Load(package, customer));

            }


            var Mo = new Customer_order();
            Mo.total_money = decimal.Parse(total_money);
            Mo.pay_type = pay_type;
            Mo.customer_id = customer.id;
            Mo.package_id = package_id;
            db.Customer_orders.Add(Mo);

            //Customer thanh toán Packed thành công
            customer.payment_monthly = package.price;
            customer.package_id = package_id;
            customer.services_sub_date = DateTime.Now;
            customer.date_left = DateTime.Now.AddMonths(package.duration.Value);
            db.SaveChanges();


            TempData["thongBaoPackage"] = "You have successfully placed an order.";

            return RedirectToAction("Index","Package");
        }

        public IActionResult PaymentSuccess()
        {
          

            return View();
        }
        public IActionResult PaymentFailed()
        {


            return View();
        }




    }
}
