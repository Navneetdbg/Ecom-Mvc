using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.Database
{
   public class RbContext : DbContext , IDisposable
    {
        public RbContext():base("RealBazzar")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Config> Configurations { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Rating>Ratings { get; set; }
    }
}
