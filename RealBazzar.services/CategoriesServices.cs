using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealBazzar.Database;

namespace RealBazzar.services
{
    public class CategoriesServices
    {
        #region Sigleton
        public static CategoriesServices CategoryInstance
        {
            get
            {
                if (Categoriesinstance == null) Categoriesinstance = new CategoriesServices();
                return Categoriesinstance;
            }
        }

        private static CategoriesServices Categoriesinstance { get; set; }
        private CategoriesServices()
        {

        }


        #endregion
        public List<Category> GetCategories(string search,int pageNo , int pageSize)
        {
            //int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ConfigproPageSize").Value);
            using (var db = new RbContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    return db.Categories.Include(a => a.products)
                        .Where(a => a.Name.ToLower().Contains(search.ToLower())).
                        OrderBy(x => x.Id).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
                   
                }
                else


                {
                    return db.Categories.Include(a => a.products)
                       .OrderBy(x => x.Id).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
                }
            }
        }

        public List<Category> GetCategoriesbyId()
        {
            
            using (var db = new RbContext())
            {
                return db.Categories.ToList();
            }
        }

        public int GetCategoriesCount( string search)
        {
            using (var db = new RbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return db.Categories.
                        Where(a => a.Name.ToLower().Contains(search.ToLower())).Count();
                        

                }
                else
                {
                    return db.Categories.Count();
                }
                

            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var db = new RbContext())
            {
                return db.Categories.Where(x=>x.IsFetured == true).ToList();

            }
        }
        public Category GetCategory(int Id)
        {
            using (var db = new RbContext())
            {
                return db.Categories.Find(Id);

            }
        }
        public void SaveCategories(Category category)
        {
            using ( var db = new RbContext())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public void UpdateCategories(Category category)
        {
            using (var db = new RbContext())
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            using (var db = new RbContext())
            {
                var v = db.Categories.Where(x=>x.Id== Id).Include(x=>x.products).FirstOrDefault();
                db.Products.RemoveRange(v.products);
                db.Categories.Remove(v);
                db.SaveChanges();
            }
        }

        public Category GetIndexCategory()
        {
            using (var db = new RbContext())
            {
                return db.Categories.Where(x => x.ImageURL2 != null && x.IndexPage==true).FirstOrDefault();

            }
        }

        public Product GetdealofDay()
        {
            using (var db = new RbContext())
            {
                return db.Products.Where(x=> x.dealofday == true).FirstOrDefault();

            }
        }
    }
}
