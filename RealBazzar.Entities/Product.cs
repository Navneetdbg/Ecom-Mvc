using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.Entities
{
   public class Product : BaseEntity
    {
        
        public virtual Category category { get; set; }

        [Range(1,5000000)]
         public decimal Price { get; set; }

        public bool IsFeatured { get; set; }

        public bool BestSeller { get; set; }

        public string ImageURL { get; set; }

        public string ImageURL2 { get; set; }
        public string ImageURL3 { get; set; }

        public decimal CostPrice { get; set; }

        public int Quantity { get; set; }
        public bool dealofday { get; set; }
        public int CategoryId { get; set; }
    }
}
