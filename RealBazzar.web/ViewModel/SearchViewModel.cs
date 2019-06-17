using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class ProductSearchViewModel
    {
       

        public List<Product> Products { get; set; }

        public string SearchItem { get; set; }

        public Pager pager { get; set; }
    }

    public class NewProductViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public List<Category> AvailableCategory { get; set; }
    }
}