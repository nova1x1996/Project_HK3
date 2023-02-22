using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using Project.Models;
using System.Configuration;
using System.Security.Claims;
using Project.Helper;
using AspNetCoreHero.ToastNotification.Notyf.Models;
using System.Security.Policy;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;


namespace Project.Controllers
{


    public class CustomerOrderController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly DatabaseContext db;

      
       
        private static string Mode = "sb-pjm9425080818@business.example.com";
        
        public CustomerOrderController(DatabaseContext _db, IConfiguration config,INotyfService notyf)
        {
            db = _db;
            _notyf = notyf;
       
        }
 
        


                 protected string  Page_Load(int ItemId,string price ,int customerId,string loaiSanPham)
                {
            
                    var APIContext = HelperPayPal.GetAPIContext();

            // Thiết lập chi tiết đơn hàng
            var itemList = new ItemList
            {
                items = new List<Item>
                                            {
                                                new Item
                                                {

                                                    
                                                    description = customerId.ToString(),
                                                    name = ItemId.ToString(),
                                                    currency = "USD",
                                                    price = price,
                                                    quantity = "1",
                                                    sku = loaiSanPham
                                                }
                                            }
                            };

                            // Tính tổng giá trị đơn hàng
                                            var total = new Amount
                            {
                                currency = "USD",
                                total = price
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
                return RedirectToAction("PaymentFailed");
            }
            else
            {

                var items = executedPayment.transactions[0].item_list.items.FirstOrDefault();
                var total_money = executedPayment.transactions[0].amount.total;
                var pay_type = executedPayment.payer.payment_method;
                var customerId = items.description;
                string aaa = items.sku;

                if (items.sku.Equals("package"))
                {
                    
                    var package = db.Packages.Find(int.Parse(items.name));


                    var Mo = new Customer_order();

                    Mo.total_money = decimal.Parse(total_money);
                    Mo.pay_type = pay_type;
                    Mo.customer_id = int.Parse(customerId);
                    Mo.package_id = package.id;
                    Mo.state = true;
                    Mo.date = DateTime.Now;
                    db.Customer_orders.Add(Mo);

                    //Điền thông tin gói cước vào Customer

                    var customer = db.Customers.Find(int.Parse(customerId));
                    customer.payment_monthly = package.price;
                    customer.package_id = package.id;
                    customer.services_sub_date = DateTime.Now;
                    //customer.date_left = DateTime.Now.AddMonths(package.duration.Value);
                    customer.date_left = DateTime.Now.AddSeconds(30);
                    db.SaveChanges();


                    // Xử lý khi thanh toán thành công
                    return RedirectToAction("PaymentSuccess");
                }
                else if (items.sku.Equals("movie"))
                {
                    var movie = db.Movies.Find(int.Parse(items.name));

                    var Mo = new Customer_order();
                    Mo.total_money = decimal.Parse(total_money);
                    Mo.pay_type = pay_type;
                    Mo.customer_id = int.Parse(customerId);
                    Mo.movie_id = int.Parse(items.name);
                    Mo.state = true;
                    Mo.date = DateTime.Now;
                    db.Customer_orders.Add(Mo);
                    db.SaveChanges();
                    return RedirectToAction("PaymentSuccess");

                }
                else if(items.sku.Equals("setupbox"))
                {
                    var setupbox = db.SetUpBoxes.Find(int.Parse(items.name));


                    var Mo = new Customer_order();
                    Mo.total_money = decimal.Parse(total_money);
                    Mo.pay_type = pay_type;
                    Mo.customer_id = int.Parse(customerId);
                    Mo.setUpBox_id = int.Parse(items.name);
                    Mo.state = true;
                    Mo.date = DateTime.Now;
                    db.Customer_orders.Add(Mo);
                    db.SaveChanges();
                    return RedirectToAction("PaymentSuccess");
                }
                return Content("PaymentFailed");

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

                return Redirect(Page_Load(package.id,package.price.ToString(), customer.id,"package"));

            }


            var Mo = new Customer_order();
            Mo.total_money = decimal.Parse(total_money);
            Mo.pay_type = pay_type;
            Mo.customer_id = customer.id;
            Mo.package_id = package_id;
            Mo.date = DateTime.Now;
            db.Customer_orders.Add(Mo);

            //Customer thanh toán Packed thành công
           
            db.SaveChanges();


            _notyf.Success("You have successfully placed an order, my staff will contact you soon.");

            return RedirectToAction("Index","Package");
        }



        //PaymentSuccess

        public IActionResult PaymentSuccess()
        {
          

            return View();
        }
        public IActionResult PaymentFailed()
        {


            return View();
        }





        //Movie

        [HttpGet()]
        public IActionResult MovieOrder(int id)
        {
           
             var model = db.Movies.Find(id);

             return View(model);
         
          
        }

        [HttpPost()]
        public IActionResult MovieOrder(string pay_type, string total_money, int movies_id)
        {
            //Lấy Id của Phiên Đăng nhập hiện tại
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Tìm Customer dựa trên ID của Phiên Đăng nhập hiện tại
            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();
            var movie = db.Movies.Find(movies_id);


            if (pay_type.Equals("paypal"))
            {

                return Redirect(Page_Load(movie.id, movie.price.ToString(), customer.id,"movie"));

            }


            var Mo = new Customer_order();
            Mo.total_money = decimal.Parse(total_money);
            Mo.pay_type = pay_type;
            Mo.customer_id = customer.id;
            Mo.movie_id = movies_id;
            Mo.state = false;
            Mo.date = DateTime.Now;
            db.Customer_orders.Add(Mo);
            db.SaveChanges();




            _notyf.Success("You have successfully placed an order, my staff will contact you soon.");
            return RedirectToAction("Index", "Movie");


          
        }

        [HttpGet()]
        public IActionResult SetUpBoxOrder(int id)
        {
            var model = db.SetUpBoxes.Find(id);

            return View(model);
        }

        [HttpPost()]
        public IActionResult SetUpBoxOrder(string pay_type, string total_money, int package_id)
        {
            //Lấy Id của Phiên Đăng nhập hiện tại
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Tìm Customer dựa trên ID của Phiên Đăng nhập hiện tại
            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();
            var setUpBox = db.SetUpBoxes.Find(package_id);


            if (pay_type.Equals("paypal"))
            {

                return Redirect(Page_Load(setUpBox.id, setUpBox.price.ToString(), customer.id, "setupbox"));

            }
            else
            {
                var Mo = new Customer_order();
                Mo.state = false;
                Mo.total_money = decimal.Parse(total_money);
                Mo.pay_type = pay_type;
                Mo.customer_id = customer.id;
                Mo.setUpBox_id = setUpBox.id;
                Mo.date = DateTime.Now;
                db.Customer_orders.Add(Mo);
                db.SaveChanges();




                _notyf.Success("You have successfully placed an order, my staff will contact you soon.");
                return RedirectToAction("Index", "SetUpBox");
            }
        }
    }
}
