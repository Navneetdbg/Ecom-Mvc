using Microsoft.Owin.Security;
using RealBazzar.services;
using RealBazzar.web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RealBazzar.web.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.featuredcategoies = CategoriesServices.CategoryInstance.GetFeaturedCategories();
            model.category = CategoriesServices.CategoryInstance.GetIndexCategory();
            model.product = CategoriesServices.CategoryInstance.GetdealofDay();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult MyOrder(string UserId,int? pageNo,int?Ids)
        {
            UserId = User.Identity.Name;
            if (!string.IsNullOrEmpty(UserId))
            {
                
                UserOrderItems model = new UserOrderItems();
                
                int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ConfigPageSize").Value);
                pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
                var totalRecord = OrderService.Instance.SearchOrderCountUser(UserId);
                model.Orders = OrderService.Instance.GetOrdersUser(UserId, pageNo.Value, pageSize);
                //model.Order = OrderService.Instance.OrderItem(model.Id);
                model.Pager = new Pager(totalRecord, pageNo, pageSize);
                return View(model);
            }
           else
            {
                return RedirectToAction("Index", "Home");
            }
            
            
        }
        [Authorize]
        public ActionResult OrderDetails(string UserId,int Id)
        {
            UserId = User.Identity.Name;
            if (!string.IsNullOrEmpty(UserId))
            {
                UserToItems model = new UserToItems();
                model.Order = OrderService.Instance.GetOrderByIdforUser(UserId,Id);
                return View(model);
            }
            else
            {
                return View();
            }
        }
        //[HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public JsonResult Cancel(int Id,   string status = "Cancelled")
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                string Email = User.Identity.Name;

               
                jsonResult.Data = new { Success = OrderService.Instance.UpdatecancleSratus(Id, Email, status) };
                return jsonResult;
            }
            catch (Exception ex)
            {
                jsonResult.Data = new { Success = false, Message= "Your Exception is "+ex };
                return jsonResult;
            }
        }

    }
}