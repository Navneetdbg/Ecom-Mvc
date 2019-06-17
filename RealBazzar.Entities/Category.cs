
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.Entities
{
    public class Category : BaseEntity
    {
        public string ImageURL { get; set; }
        public List<Product> products { get; set; }

        public bool IsFetured { get; set; }

        public string ImageURL2 { get; set; }

        public bool IndexPage { get; set; }
    }
}
