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
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using Project.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace Project.Controllers
{


    public class CustomerOrderController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly DatabaseContext db;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;


        private static string Mode = "sb-pjm9425080818@business.example.com";

        public CustomerOrderController(DatabaseContext _db, IConfiguration config, INotyfService notyf, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            _notyf = notyf;
            userManager = _userManager;

        }
        public  string TraTenHienTai()
        {

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user =  userManager.FindByIdAsync(userId).Result;

            return user.FirstName;
        }

        public string TraEmailHienTai()
        {
           
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.FindByIdAsync(userId).Result;
            
            return user.Email;
        }
        public static void SendEmail(string email,string first_name,string product,string quantity, string price,string total,string SanPham )
        {


            var body = new StringBuilder();
       
            body.AppendLine("<table>");
            body.AppendLine($"<tr><th>{SanPham.ToUpper()}</th><th>Quantity</th><th>Unit Price</th><th>Total</th></tr>");
            body.AppendLine($"<tr><td>{product}</td><td>{quantity}</td><td>{price} $</td><td>{total} $</td></tr>");
            body.AppendLine("</table>");
  

            string CSS = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n\t<meta charset=\"utf-8\">\r\n\t<title>Hóa đơn của bạn</title>\r\n\t<style type=\"text/css\">\r\n\t\tbody {\r\n\t\t\tfont-family: Arial, sans-serif;\r\n\t\t\tfont-size: 14px;\r\n\t\t\tline-height: 1.5;\r\n\t\t\tcolor: #333333;\r\n\t\t}\r\n\t\ttable {\r\n\t\t\twidth: 100%;\r\n\t\t\tborder-collapse: collapse;\r\n\t\t}\r\n\t\ttd, th {\r\n\t\t\tpadding: 10px;\r\n\t\t\tborder: 1px solid #cccccc;\r\n\t\t}\r\n\t\tth {\r\n\t\t\tbackground-color: #f2f2f2;\r\n\t\t}\r\n\t\t.total-row {\r\n\t\t\tfont-weight: bold;\r\n\t\t}\r\n\t\t.grand-total {\r\n\t\t\tfont-size: 16px;\r\n\t\t\tfont-weight: bold;\r\n\t\t}\r\n\t h1{text-align:center;}</style>\r\n</head>";

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465);
                client.Authenticate("hoangdeptraibodoiqua4321@gmail.com", "gcbhrgquuqrfwohx");

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = CSS + $"<h1>Hi, {first_name}</h1><p>You have just successfully paid for your order on {DateTime.Now.ToString()}</p>" + body.ToString() + "<p>Thank you for using our service !</p>",
                  
                };

                var message = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody(),
                };
                message.From.Add(new MailboxAddress("R-DTH", "hoangdeptraibodoiqua4321@gmail.com"));
                message.To.Add(new MailboxAddress("User", email));
                message.Subject = "You have just made a payment !";
                client.Send(message);
                client.Disconnect(true);
            }
       
        }

        protected string Page_Load(int ItemId, string price, int customerId, string loaiSanPham)
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
            {  //Lay De Gui Mail
                var tenNguoiDung = TraTenHienTai();
                var emailhientai = TraEmailHienTai();
                //

                var items = executedPayment.transactions[0].item_list.items.FirstOrDefault();
                var total_money = executedPayment.transactions[0].amount.total;
                var pay_type = executedPayment.payer.payment_method;
                var customerId = items.description;
                string aaa = items.sku;

                if (items.sku.Equals("package"))
                {

                    var package = db.Packages.Find(int.Parse(items.name));
                    var tongtien = decimal.Parse(total_money);

                    var Mo = new Customer_order();
                    
                    if(tongtien/ package.price == 1)
                    {
                        Mo.monthPackage = 1;
                    }
                    else if(tongtien / package.price == 6)
                    {
                        Mo.monthPackage = 6;
                    }
                    else if(tongtien/package.price == 12)
                    {
                        Mo.monthPackage = 13;
                    }

                    Mo.total_money = tongtien;
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
                    customer.statePackage = true;
                    customer.date_left = DateTime.Now.AddMonths(Mo.monthPackage.Value);
                    //customer.date_left = DateTime.Now.AddSeconds(30);
                    db.SaveChanges();

                    SendEmail(emailhientai.ToString(), tenNguoiDung.ToString(), package.name, Mo.monthPackage.ToString() + " Month", package.price.ToString(), total_money, items.sku);
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

                  
                   
                    
                    SendEmail(emailhientai.ToString(), tenNguoiDung.ToString(),movie.name,"1",movie.price.ToString(),total_money,items.sku);
                    return RedirectToAction("PaymentSuccess");

                }
                else if (items.sku.Equals("setupbox"))
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


                    SendEmail(emailhientai.ToString(), tenNguoiDung.ToString(), setupbox.name, "1", setupbox.price.ToString(), total_money, items.sku);
                    return RedirectToAction("PaymentSuccess");
                }
                return RedirectToAction("PaymentFailed");

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
        public IActionResult PackageOrder(int id, int month)
        {
            var model = db.Packages.Find(id);
            ViewBag.Month = month;
            return View(model);
        }


        [HttpPost()]

        [Authorize(Roles = "customer")]
        public IActionResult PackageOrderKhac(string pay_type, string total_money, int package_id, int monthPackage)
        {
            //Lấy Id của Phiên Đăng nhập hiện tại
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Tìm Customer dựa trên ID của Phiên Đăng nhập hiện tại
            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();
            if (customer.package_id != null)
            {

                _notyf.Warning("Your current package is still valid, so you cannot order a new package. You can recharge or switch to a different package.");
                return RedirectToAction("Index", "Package");
            }

            var CusOrder = db.Customer_orders.Where(c => c.state == false && c.customer_id == customer.id && c.package_id != null).SingleOrDefault();
            if (CusOrder != null)
            {
                _notyf.Warning("You have an order to purchase a package that has not been paid yet. Please cancel it to order a different package.");
                return RedirectToAction("HistoryCustomerOrder", "UserAuthentication");
            }


            var package = db.Packages.Find(package_id);


            if (pay_type.Equals("paypal"))
            {

                return Redirect(Page_Load(package.id, total_money, customer.id, "package"));

            }


            var Mo = new Customer_order();
            Mo.total_money = decimal.Parse(total_money);
            Mo.pay_type = pay_type;
            Mo.customer_id = customer.id;
            Mo.package_id = package_id;
            Mo.monthPackage = monthPackage;
            Mo.date = DateTime.Now;
            db.Customer_orders.Add(Mo);

            //Customer thanh toán Packed thành công

            db.SaveChanges();


            _notyf.Success("You have successfully placed an order, my staff will contact you soon.");

            return RedirectToAction("Index", "Package");
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

        [Authorize(Roles = "customer")]

        [HttpGet()]
        public IActionResult MovieOrder(int id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).SingleOrDefault();

            var model = db.Movies.Find(id);

            var orderMovie = db.Customer_orders.Where(c => c.customer_id == customer.id && c.movie_id == id).SingleOrDefault();
            if(orderMovie != null)
            {
                _notyf.Error("You have booked this movie.");
                return RedirectToAction("Index","Movie");
            }

            return View(model);


        }

        [Authorize(Roles = "customer")]

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

                return Redirect(Page_Load(movie.id, movie.price.ToString(), customer.id, "movie"));

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
        [Authorize(Roles = "customer")]
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
