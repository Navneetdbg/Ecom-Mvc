using RealBazzar.Entities;
using RealBazzar.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class CheckoutViewModel
    {
        public List<Product> CheckoutProduct { get; set; }

        public List<int> CartproductIds { get; set; }

        public ApplicationUser Users { get; set; }

        public int Id { get; set; }

        public int Quantity { get; set; }
    }


    public class ShopViewModel
    {
        public int? SortBy { get; set; }

        public List<Product> products { get; set; }

        public int MaxPice { get; set; }

        public int? CategoryId { get; set; }
        public List<Category> featuredCategory { get; set; }

        public Pager pager { get; set; }
    }

    public class FilterViewModel
    {
        public int? SortBy { get; set; }
        public List<Product> products { get; set; }
        public int? CategoryId { get; set; }

        public Pager pager { get; set; }
    }


}