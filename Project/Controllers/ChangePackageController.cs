using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Utilities;
using PayPal.Api;
using Project.Helper;
using Project.Models;
using System.Security.Claims;

namespace Project.Controllers
{
    public class ChangePackageController : Controller
    {
        private DatabaseContext db;
        private readonly INotyfService _notyf;
        public ChangePackageController(DatabaseContext _db, INotyfService notyf)
        {
            db = _db;
            _notyf = notyf;
        }
        public IActionResult Index()    
        {
      
           var userId =  HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
         
                var customer = db.Customers.Where(c => c.user_id.Equals(userId)).Include(c=>c.package).SingleOrDefault();
            if(customer.package_id == null)
            {
                _notyf.Error("You have not yet subscribed to a package or your package has expired.");
                return RedirectToAction("DetailCustomer","UserAuthentication");
            }

            var packageCurrent = db.Packages.Find(customer.package_id);

            var package = db.Packages.Where(p=>p.price > packageCurrent.price).ToList();
           
           // var model = new SelectList()
            //var packageList = new SelectList(package,"id","name");
            ViewBag.packList = package;
            return View(customer);
        }

        [HttpPost]
        public IActionResult Cost(int SoThangConLai,int packageNew_id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = db.Customers.Where(c => c.user_id.Equals(userId)).Include(c => c.package).SingleOrDefault();
           
            var package = db.Packages.Find(packageNew_id);
            ViewBag.pack = package;
            ViewBag.tongTien =   (package.price - customer.package.price) * SoThangConLai;
            return View(customer);
        }


        protected string Page_Load2(int ItemId, string price, int customerId)
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
                    return_url = "http://localhost:5296/ChangePackage/btnPayment",
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
                var customer = db.Customers.Find(int.Parse(customerId));
                var newPackage = db.Packages.Find(int.Parse(items.name));

                  

                var model = new ChangePackage();
                

                model.customer_id = customer.id;
                model.packageOld = (int)customer.package_id;
                model.packageNew = int.Parse(items.name);
                model.price = (int)decimal.Parse(total_money);
                model.date = DateTime.Now;
                model.state = true;
                db.ChangePackages.Add(model);

                //Người dùng đc tính gói package mới
                customer.payment_monthly = newPackage.price;
                customer.package_id = int.Parse(items.name);
                db.SaveChanges();

                return RedirectToAction("PaymentSuccess","Recharge");

            }
        }
        public IActionResult PaymentUpdatePackage(int package_id, int customer_id, string tongtien, string pay_type)
        {
            var RechargeFalse = db.Recharges.Where(r => r.customer_id == customer_id && r.state == false).FirstOrDefault();
            var changePack = db.ChangePackages.Where(cp => cp.customer_id == customer_id && cp.state == false).FirstOrDefault();


            if (RechargeFalse != null)
            {
                _notyf.Error("You still have an unpaid Recharge order.");
                return RedirectToAction("Index");
            }else if (changePack != null)
            {
                _notyf.Error("You still have an unpaid Update Package order.");
                return RedirectToAction("Index");
            }

            if (pay_type.Equals("paypal"))
            {

                return Redirect(Page_Load2(package_id, tongtien, customer_id));
            }
          
            var customer = db.Customers.Find(customer_id);

            var model = new ChangePackage();
            model.customer_id = customer.id;

            model.packageOld = (int)customer.package_id;
            model.packageNew = package_id;
            model.price = int.Parse(tongtien);
            model.date = DateTime.Now;
            model.state = false;
            db.ChangePackages.Add(model);

         
            db.SaveChanges();
            _notyf.Success("You have successfully placed an order. We will contact you soon.");
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult History()
        {
        

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = db.Customers.Where(c=>c.user_id.Equals(userId)).FirstOrDefault();

            var CPList = db.ChangePackages.Where(cp => cp.customer_id == customer.id).ToList();
            var package = db.Packages.ToList();
            ViewBag.package = package;

            return View(CPList);
        }

        public IActionResult Delete(int id)
        {
            var model = db.ChangePackages.Find(id);
            db.ChangePackages.Remove(model);
            db.SaveChanges();
            _notyf.Success("Your Order Update Package has been delete !");
            return RedirectToAction("History","ChangePackage");

        }
    }
}
