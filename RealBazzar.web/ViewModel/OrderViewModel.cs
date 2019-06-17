using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public string UserId { get; set; }

        public string Email { get; set; }
        public string MobileNo { get; set;}

        public string Status { get; set; }
        public Pager Pager { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public Order Orders { get; set; }
        public List<string> AvailableStatus { get;  set; }
    }
}