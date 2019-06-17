using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class ProductWidgetListViewModel
    {
        public List<Product> Products { get; set; }


        public bool IsFeaturedProduct { get; set; }
    }
}