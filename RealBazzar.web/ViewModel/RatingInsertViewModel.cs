using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class RatingInsertViewModel
    {
        public int ProductId { get; set; }

        public string Review { get; set; }

        public string Email { get; set; }

        public int rating { get; set; }
    }
}