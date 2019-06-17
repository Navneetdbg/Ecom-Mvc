using RealBazzar.services;
using RealBazzar.web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBazzar.web.Controllers
{
    public class WidgetController : Controller
    {
        // GET: Widget
        public ActionResult Product(bool isLatest)
        {
            ProductWidgetListViewModel model = new ProductWidgetListViewModel();
            model.IsFeaturedProduct = isLatest;
            if (isLatest)
            {
                model.Products = ProductServices.ProductInstance.GetLatestProducts(10);
            }
            else
            {
                model.Products = ProductServices.ProductInstance.GetProducts(1, 10);
            }
            return PartialView(model);
        }
    }
}