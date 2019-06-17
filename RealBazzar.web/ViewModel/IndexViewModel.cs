using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class IndexViewModel
    {
        public List<Category> featuredcategoies { get; set; }

        public Category category { get; set; }

        public Product product { get; set; }
      
    }
}