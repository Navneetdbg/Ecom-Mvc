using RealBazzar.services;
using RealBazzar.web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBazzar.web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult OrderTable(string UserId, string Email, string Status, string MobileNo, int? pageNo)
        {
            OrderViewModel model = new OrderViewModel();
            model.Email = Email;
            model.Status = Status;
            model.MobileNo = MobileNo;
            int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ConfigPageSize").Value);
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecord = OrderService.Instance.SearchOrderCount(UserId, Status, MobileNo,Email);
            model.Orders = OrderService.Instance.GetOrders(UserId, Email,Status, MobileNo, pageNo.Value, pageSize);
            model.Pager = new Pager(totalRecord, pageNo, pageSize);
            return PartialView(model);
        }

        public ActionResult Detail( int Id)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();
            model.Orders = OrderService.Instance.GetOrderById(Id);
            model.AvailableStatus = new List<string>() { "Pending", "Apprroved", "Dispached", "Dilivered","Cancelled" };
            return PartialView(model);
        }

        public JsonResult ChangeStatus(int Id, string status)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;


            jsonResult.Data = new { Success = OrderService.Instance.UpdateSratus(Id , status) };

            return jsonResult;
            
        }

    }
}