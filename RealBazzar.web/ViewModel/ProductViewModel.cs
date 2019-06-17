using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }

        public List<Category> Available { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal CostPrice { get; set; }
        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }

        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }

        public bool IsFeatured { get; set; }

        public bool dealofday { get; set; }
        public bool BestSeller { get; set; }
        public List<Category> Availablecategories { get; set; }
    }
    public class DetailsProductViewModel
    {
        public Product Products { get; set; }
    }

    public class RatingViewModel
    {
        

        public List<Rating> ratings { get; set; }
    }
}