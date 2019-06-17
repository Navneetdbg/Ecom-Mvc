using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.Entities
{
   public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime OrderTime { get; set; }

        public string OrderStatus { get; set; }

        public decimal TotalAmount { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string Pincode { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
