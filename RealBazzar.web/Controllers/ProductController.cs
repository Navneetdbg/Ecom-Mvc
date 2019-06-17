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
    public class ProductController : Controller
    {
        // GET: Product
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductList(string search , int? pageNo)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();
            int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ConfigPageSize").Value);

            pageNo = pageNo.HasValue ? pageNo.Value>0 ? pageNo.Value:1 : 1;

            var totalRecord = ProductServices.ProductInstance.GetProductCount(search);
            model.Products = ProductServices.ProductInstance.GetProductListing(search, pageNo.Value,pageSize);

            if (model.Products != null)
            {

                model.pager = new Pager(totalRecord, pageNo, pageSize);


                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            
            var v = CategoriesServices.CategoryInstance.GetCategoriesbyId();
            return PartialView(v);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(NewCategoriesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Product();
                newProduct.Name = model.Name;
                newProduct.Price = model.Price;
                newProduct.Description = model.Description;
                newProduct.IsFeatured = model.IsFeatured;
                newProduct.CostPrice = model.CostPrice;
                newProduct.ImageURL = model.ImageUrl;
                newProduct.ImageURL2 = model.ImageUrl2;
                newProduct.ImageURL3 = model.ImageUrl3;
                newProduct.category = CategoriesServices.CategoryInstance.GetCategory(model.CategoryId);
                newProduct.BestSeller = model.BestSeller;
                newProduct.dealofday = model.dealofday;
                ProductServices.ProductInstance.SaveProducts(newProduct);
                return RedirectToAction("ProductList");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {
            EditProductViewModel model = new EditProductViewModel();
            var v = ProductServices.ProductInstance.GetProduct(Id);
            model.Id = v.Id;
            model.Name = v.Name;
            model.Description = v.Description;
            model.Price = v.Price;
            model.CostPrice = v.CostPrice;
            model.ImageUrl = v.ImageURL;
            model.ImageUrl2 = v.ImageURL2;
            model.ImageUrl3 = v.ImageURL3;
            model.CategoryId = v.category != null ? v.category.Id : 0;
            model.Availablecategories = CategoriesServices.CategoryInstance.GetCategoriesbyId();
            model.BestSeller = v.BestSeller;
            model.IsFeatured = v.IsFeatured;
            model.dealofday = v.dealofday;
            return PartialView(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = ProductServices.ProductInstance.GetProductbyId(model.Id);
                existingProduct.Name = model.Name;
                existingProduct.IsFeatured = model.IsFeatured;
                existingProduct.Price = model.Price;
                existingProduct.Description = model.Description;
                existingProduct.CategoryId = model.CategoryId;
                existingProduct.BestSeller = model.BestSeller;
                existingProduct.CostPrice = model.CostPrice;
                existingProduct.ImageURL = model.ImageUrl;
                existingProduct.ImageURL2 = model.ImageUrl2;
                existingProduct.ImageURL3 = model.ImageUrl3;
                existingProduct.dealofday = model.dealofday;
                ProductServices.ProductInstance.UpdateProducts(existingProduct);
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ProductServices.ProductInstance.Delete(Id);
            return RedirectToAction("ProductList");
        }

        public ActionResult Details(int Id)
        {
            DetailsProductViewModel model = new DetailsProductViewModel();
            model.Products = ProductServices.ProductInstance.GetProduct(Id);
            
            return View(model);
        }

        public ActionResult Review(int ProductId)
        {
            RatingViewModel model = new RatingViewModel();
            model.ratings = ProductServices.ProductInstance.GetProductRatings(ProductId);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult PostRating(Rating rating)
        {
            
            ProductServices.ProductInstance.SaveRatings(rating);
            return RedirectToAction("Details", "Product", new { Id = rating.ProductId });
        }
    }
}