using RealBazzar.Database;
using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBazzar.services
{
   public class ShopServices
    {
        #region Singleton
        public static ShopServices Instance
        {
            get
            {
                if (instance == null) instance = new ShopServices();
                return instance;
            }
        }

        private static ShopServices instance { get; set; }
        private ShopServices()
        {

        }

        #endregion

        public int Saveorder(Order order)
        {
            using (var db = new RbContext())
            {
               
                db.orders.Add(order);
               return db.SaveChanges();
            }
        }

        public void UpdateOrder(int Id, string Address, string Country, int Pincode, string Mobile)
        {
            using (var db = new RbContext())
            {
               var order= db.orders.Find(Id);
                order.Address = Address;
                order.Country = Country;
                order.Pincode = Pincode.ToString();
                order.Mobile = Mobile;
                db.SaveChanges() ;
            }
        }
    }
}
