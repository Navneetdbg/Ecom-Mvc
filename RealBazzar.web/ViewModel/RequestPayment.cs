using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class RequestPayment
    {
        public string email { get; set; }

        public string newAmount { get; set; }
    }
}