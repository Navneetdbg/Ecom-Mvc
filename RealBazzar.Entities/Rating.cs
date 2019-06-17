using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.Entities
{
   public class Rating
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int? Rate { get; set; }

        public string Review { get; set; }

        public string Email { get; set; }

    }
}
