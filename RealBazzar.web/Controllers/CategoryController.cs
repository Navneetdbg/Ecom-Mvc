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
    public class CategoryController : Controller
    {
      

        // GET: Category

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewCategory(string search , int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.Search = search;
            int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ConfigproPageSize").Value);

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
           
            var totalRecord = CategoriesServices.CategoryInstance.GetCategoriesCount(search);
               model.categories = CategoriesServices.CategoryInstance.GetCategories(search,pageNo.Value , pageSize);

            if (model.categories != null)
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
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoriesServices.CategoryInstance.SaveCategories(category);
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        public ActionResult Edit(int Id)
        {

            var v = CategoriesServices.CategoryInstance.GetCategory(Id);
            return PartialView(v);
        }

        [HttpPost]
        public ActionResult Edit(Category category)

        {
            if (ModelState.IsValid)
            {
                CategoriesServices.CategoryInstance.UpdateCategories(category);
                return RedirectToAction("Index");
            }
          
             else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        public ActionResult Delete(int Id)
        {
            var v = CategoriesServices.CategoryInstance.GetCategory(Id);
            return PartialView(v);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            try
            {
                CategoriesServices.CategoryInstance.Delete(category.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}