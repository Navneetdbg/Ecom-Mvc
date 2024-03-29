﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.Entities
{
  public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public  virtual Order Order { get; set; }

        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
