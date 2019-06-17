using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using paytm;
using RealBazzar.Entities;
using RealBazzar.services;
using RealBazzar.web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBazzar.web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

       

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Shop
        public ActionResult Index(string searchTerm, int ? minimumPrice, int? Maximumprice , int? CategoryId , int? sortBy , int? pageNo)

        {
            ShopViewModel model = new ShopViewModel();
            model.featuredCategory = CategoriesServices.CategoryInstance.GetFeaturedCategories();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ShopPageSize").Value);

            model.MaxPice = ProductServices.ProductInstance.GetMaximumPrice();
            model.SortBy = sortBy;
            model.CategoryId = CategoryId;
            int TotalCount = ProductServices.ProductInstance.SearchProductCount(searchTerm, minimumPrice, Maximumprice, CategoryId, sortBy);
            model.products = ProductServices.ProductInstance.SearchProduct(searchTerm,minimumPrice,Maximumprice,CategoryId, sortBy,pageNo.Value,pageSize);
            model.pager = new Pager(TotalCount, pageNo, pageSize);
            return View(model);
        }

        public ActionResult Filter(string searchTerm, int? minimumPrice, int? Maximumprice, int? CategoryId, int? sortBy,int? pageNo)

        {
            FilterViewModel model = new FilterViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ShopPageSize").Value);
            model.SortBy = sortBy;
            model.CategoryId = CategoryId;
            int TotalCount = ProductServices.ProductInstance.SearchProductCount(searchTerm, minimumPrice, Maximumprice, CategoryId, sortBy);
            model.products = ProductServices.ProductInstance.SearchProduct(searchTerm, minimumPrice, Maximumprice, CategoryId, sortBy,pageNo.Value,pageSize);
            model.pager = new Pager(TotalCount, pageNo, pageSize);
            return PartialView(model);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var cartProductCookies = Request.Cookies["CartProduct"];
            if(cartProductCookies != null && !string.IsNullOrEmpty(cartProductCookies.Value))
            {
                model.CartproductIds = cartProductCookies.Value.Split('-').Select(x=> int.Parse(x)).ToList();
                
                model.CheckoutProduct = ProductServices.ProductInstance.GetListProduct(model.CartproductIds);
               
                
                
                model.Users = UserManager.FindById(User.Identity.GetUserId());
                return View(model);
            }
            else
            {
                return View();
            }
        }

        public JsonResult PlaceOrder(string ProductIds, string Name , string Address , string Country, string PinCode, string Mobile,string Email)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(ProductIds))
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    if (!string.IsNullOrEmpty(Address))
                    {
                        var Quan = ProductIds.Split('-').Select(x => int.Parse(x)).ToList();
                        var buyProduct = ProductServices.ProductInstance.GetListProduct(Quan.Distinct().ToList());
                        Order order = new Order();
                        order.UserId = User.Identity.GetUserId();
                        order.Email = Email;
                        order.OrderTime = DateTime.Now;
                        order.OrderStatus = "Pending";
                        order.TotalAmount = buyProduct.Sum(y => y.Price * Quan.Where(x => x == y.Id).Count());
                        order.Name = Name;
                        order.Pincode = PinCode;
                        order.Mobile = Mobile;
                        order.Address = Address;
                        order.Country = Country;
                        order.OrderItems = new List<OrderItem>();
                        order.OrderItems.AddRange(buyProduct.Select(x => new OrderItem() { ProductId = x.Id, Quantity = Quan.Where(a => a == x.Id).Count() }));
                        var effectedRows = ShopServices.Instance.Saveorder(order);

                        jsonResult.Data = new { Success = true, Rows = effectedRows };
                    }
                }
            }
            else
            {
                jsonResult.Data = new { Success = false };
            }
            return jsonResult;
        }

    
        [HttpPost]
        public ActionResult UpdateAddress(int id,string Address,string Country,int Pincode,string Mobile)
        {
           ShopServices.Instance.UpdateOrder(id, Address, Country, Pincode, Mobile) ;
                return RedirectToAction("Index");
           
        }


        public ActionResult WishList()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var cartProductCookies = Request.Cookies["WishList"];
            if (cartProductCookies != null && !string.IsNullOrEmpty(cartProductCookies.Value))
            {
                model.CartproductIds = cartProductCookies.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CheckoutProduct = ProductServices.ProductInstance.GetListProduct(model.CartproductIds);
                
            }
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Payment(ViewModel.RequestPayment data)
        //{
        //    String merchantKey = PaytmModelClass.PaytmKey.merchantKey ;
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("MID", PaytmModelClass.PaytmKey.merchantId);
        //    parameters.Add("CHANNEL_ID", "channel id value");
        //    parameters.Add("INDUSTRY_TYPE_ID", "industry value");
        //    parameters.Add("WEBSITE", "website value");
        //    parameters.Add("EMAIL", data.email);
        //    parameters.Add("MOBILE_NO", "mobile value");
        //    parameters.Add("CUST_ID", "cust id");
        //    parameters.Add("ORDER_ID", "23");
        //    parameters.Add("TXN_AMOUNT", data.newAmount);
        //    parameters.Add("CALLBACK_URL", "url"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

        //    string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
        //    string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x=>x.Key == "ORDER_ID");

        //    string outputHTML = "<html>";
        //    outputHTML += "<head>";
        //    outputHTML += "<title>Merchant Check Out Page</title>";
        //    outputHTML += "</head>";
        //    outputHTML += "<body>";
        //    outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
        //    outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
        //    outputHTML += "<table border='1'>";
        //    outputHTML += "<tbody>";
        //    foreach (string key in parameters.Keys)
        //    {
        //        outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
        //    }
        //    outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
        //    outputHTML += "</tbody>";
        //    outputHTML += "</table>";
        //    outputHTML += "<script type='text/javascript'>";
        //    outputHTML += "document.f1.submit();";
        //    outputHTML += "</script>";
        //    outputHTML += "</form>";
        //    outputHTML += "</body>";
        //    outputHTML += "</html>";
            
        //    ViewBag.html = outputHTML;

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult paymentReceived(ViewModel.PaytmRes data)
        //{
        //    return View(data);
        //}

    }
}