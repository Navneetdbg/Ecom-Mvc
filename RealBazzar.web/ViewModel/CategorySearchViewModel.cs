using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class CategorySearchViewModel
    {
        public List<Category> categories { get; set; }

        public string Search { get; set; }

        public Pager pager { get; set; }
    }

    public class NewCategoriesViewModel
    {
        [Required]
        [MaxLength(50),MinLength(3)]
        public string Name { get; set; }
        [MaxLength(5000)]
        public string Description { get; set; }
        [Range(1, 5000000)]
        public decimal Price { get; set; }

        public decimal CostPrice { get; set; }

        public string ImageUrl { get; set; }

        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }

        public bool IsFeatured { get; set; }

        public bool BestSeller { get; set; }
        public bool dealofday { get; set; }
        public int CategoryId { get; set; }
    }


}