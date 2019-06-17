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
    [Authorize(Roles ="Admin")]
    public class ConfigController : Controller
    {
        // GET: Config
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConfigTable(string search , int? pageNo)
        {
            ConfigViewModel model = new ConfigViewModel();
            model.search = search;
            int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ConfigTable").Value);
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecord = ConfigServices.Instance.GetCount(search);
            model.configr = ConfigServices.Instance.GetAll(search , pageNo.Value,pageSize);
            if (model.configr != null)
            {
                model.pager = new Pager(totalRecord, pageNo, pageSize);
                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Config config)
        {
            ConfigServices.Instance.SaveCategories(config);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Key)
        {
            ConfigById model = new ConfigById();  
            model.ConfigbyId = ConfigServices.Instance.getConfigs(Key);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(Config config)
        {
            ConfigServices.Instance.Update(config);
            return RedirectToAction("Index");
        }

    }
}