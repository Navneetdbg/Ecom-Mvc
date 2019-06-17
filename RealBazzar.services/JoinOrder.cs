using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.services
{
  public class JoinOrder
    {
        public int Id { get; set; }

        public DateTime OrderTime { get; set; }

        public string OrderStatus { get; set; }

        public decimal TotalAmount { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        
    }
}
