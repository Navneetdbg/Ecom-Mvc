using RealBazzar.Database;
using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.services
{
    public class ProductServices
    {
        #region Singleton
        public static ProductServices ProductInstance
        {
            get
            {
                if (Productinstance == null) Productinstance = new ProductServices();
                return Productinstance;
            }
        }

        private static ProductServices Productinstance { get; set; }
        private ProductServices()
        {

        }

        #endregion



        public List<Product> SearchProduct(string searchTerm, int? minimumPrice, int? Maximumprice, int? categoryId, int? sortBy, int pageNo, int PageSize)
        {
            using (var db = new RbContext())
            {
                var product = db.Products.OrderByDescending(x => x.Id).ToList();
                if (categoryId.HasValue)
                {
                    product = product.Where(x => x.category.Id == categoryId.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    product = product.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    product = product.Where(x => x.Price >= minimumPrice.Value).ToList();
                }

                if (Maximumprice.HasValue)
                {
                    product = product.Where(x => x.Price <= Maximumprice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy)
                    {
                        case 2:
                            product = product.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            product = product.Where(x => x.IsFeatured).ToList();
                            break;
                        case 4:
                            product = product.OrderByDescending(x => x.Price).ToList();
                            break;

                        case 5:
                            product = product.OrderBy(x => x.Price).ToList();
                            break;
                        case 6:
                            product = product.Where(x => x.BestSeller).ToList();
                            break;

                        default:
                            product = product.OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                }
                return product.Skip((pageNo - 1) * PageSize).Take(PageSize).ToList();
            }
        }

        public int SearchProductCount(string searchTerm, int? minimumPrice, int? Maximumprice, int? categoryId, int? sortBy)
        {
            using (var db = new RbContext())
            {
                var product = db.Products.OrderBy(x => x.Id).ToList();
                if (categoryId.HasValue)
                {
                    product = product.Where(x => x.category.Id == categoryId.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    product = product.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    product = product.Where(x => x.Price >= minimumPrice.Value).ToList();
                }

                if (Maximumprice.HasValue)
                {
                    product = product.Where(x => x.Price <= Maximumprice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy)
                    {
                        case 2:
                            product = product.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            product = product.Where(x => x.IsFeatured).ToList();
                            break;
                        case 4:
                            product = product.OrderByDescending(x => x.Price).ToList();
                            break;

                        case 5:
                            product = product.OrderBy(x => x.Price).ToList();
                            break;
                        case 6:
                            product = product.Where(x => x.BestSeller).ToList();
                            break;

                        default:
                            product = product.OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                }
                return product.Count;
            }
        }

        public int GetMaximumPrice()
        {
            using (var db = new RbContext())
            {
                return (int)(db.Products.Max(x => x.Price));

            }
        }

        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;
            using (var db = new RbContext())
            {

                return db.Products.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();
                //return db.Products.Include(x => x.category).ToList();


            }
        }

        public List<Product> GetProductListing(string search, int pageNo, int pageSize)
        {
            //int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ConfigPageSize").Value);
            using (var db = new RbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return db.Products.
                        Where(a => a.Name.ToLower().Contains(search.ToLower())).
                        OrderBy(x => x.Id).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).Include(x => x.category).
                        ToList();

                }
                else
                {
                    return db.Products.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();
                }
            }
        }

        public int GetProductCount(string search)
        {
            using (var db = new RbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return db.Products.
                        Where(a => a.Name.ToLower().Contains(search.ToLower())).Count();


                }
                else
                {
                    return db.Products.Count();
                }


            }
        }

        public List<Product> GetfeaturedProducts()
        {
            using (var db = new RbContext())
            {
                return db.Products.Include(x => x.category).Where(z => z.IsFeatured).ToList();

            }
        }
        public List<Product> BestSeller()
        {
            using (var db = new RbContext())
            {
                return db.Products.Include(x => x.category).Where(z => z.BestSeller).ToList();

            }
        }
        public Product GetProduct(int Id)
        {
            var db = new RbContext();

            return db.Products.Find(Id);


        }

        public List<Rating> GetProductRatings(int ProductId)
        {
            var db = new RbContext();

           return db.Ratings.Where(x => x.ProductId == ProductId).OrderByDescending(x=>x.Id).Take(2).ToList();


        }

        public void SaveRatings(Rating rating)
        {
            using (var db = new RbContext())
            {

                db.Ratings.Add(rating);
                db.SaveChanges();
            }
        }

        public Product GetProductbyId(int Id)
        {
            using (var db = new RbContext())
            {
                return db.Products.Find(Id);
            }
        }

        public List<Product> GetListProduct(List<int> Ids)
        {
            using (var db = new RbContext())
            {
                return db.Products.Where(z => Ids.Contains(z.Id)).ToList();

            }
        }

        public int GetCount(int Id)
        {
            using (var db = new RbContext())
            {
                return db.Products.Where(z => z.Id == Id).Count();
            }

        }
        public void SaveProducts(Product product)
        {
            using (var db = new RbContext())
            {
                db.Entry(product.category).State = EntityState.Unchanged;
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public void UpdateProducts(Product product)
        {
            using (var db = new RbContext())
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            using (var db = new RbContext())
            {
                var v = db.Products.Find(Id);
                db.Products.Remove(v);
                db.SaveChanges();
            }
        }

        public List<Product> GetLatestProducts(int noOfProduct)
        {

            using (var db = new RbContext())
            {
                return db.Products.OrderByDescending(x => x.Id).Take(noOfProduct).Include(x => x.category).ToList();

            }
        }

        public List<Product> GetProducts(int pageNo, int pageSize)
        {

            using (var db = new RbContext())
            {

                return db.Products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.category).ToList();
                //return db.Products.Include(x => x.category).ToList();


            }
        }
    }
}

