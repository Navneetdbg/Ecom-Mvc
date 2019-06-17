using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class NewCategoriesViewModel1
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsFeatured { get; set; }

        public bool BestSeller { get; set; }
        public int CategoryId { get; set; }
    }
}