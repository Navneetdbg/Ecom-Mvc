using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class UserOrderItems
    {
        public List<Order> Orders { get; set; }
       
        
        public Pager Pager { get; set; }
    }

    public class UserToItems
    {

        public Order Order { get; set; }
        
    }
}