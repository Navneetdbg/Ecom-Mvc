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
    public class OrderService
    {
        #region Singleton
        public static OrderService Instance
        {
            get
            {
                if (instance == null) instance = new OrderService();
                return instance;
            }
        }

        private static OrderService instance { get; set; }
        private OrderService()
        {

        }

        #endregion

        public List<Order> GetOrders(string search, string Email, string Status, string MobileNo, int pageNo, int PageSize)
        {
            using (var db = new RbContext())
            {
                var order = db.orders.OrderByDescending(x=>x.OrderTime).ToList();
                if (!string.IsNullOrEmpty(search))
                {
                    order = order.Where(x => x.UserId.ToLower().Contains(search.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(Status))
                {
                    order = order.Where(x => x.OrderStatus.ToLower().Contains(Status.ToLower())).ToList();

                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    order = order.Where(x => x.Mobile.ToLower().Contains(MobileNo.ToLower())).ToList();

                }
                if (!string.IsNullOrEmpty(Email))
                {
                    order = order.Where(x => x.Email.ToLower().Contains(Email.ToLower())).ToList();

                }
                return order.Skip((pageNo - 1) * PageSize).Take(PageSize).ToList();
            }
        }

        public int SearchOrderCount(string search, string Status, string MobileNo, string Email)
        {
            using (var db = new RbContext())
            {
                var order = db.orders.OrderBy(x => x.Id).ToList();

                if (!string.IsNullOrEmpty(search))
                {
                    order = order.Where(x => x.UserId.ToLower().Contains(search.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(Status))
                {
                    order = order.Where(x => x.OrderStatus.ToLower().Contains(Status.ToLower())).ToList();

                }
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    order = order.Where(x => x.Mobile.ToLower().Contains(MobileNo.ToLower())).ToList();

                }
                if (!string.IsNullOrEmpty(Email))
                {
                    order = order.Where(x => x.Email.ToLower().Contains(Email.ToLower())).ToList();

                }

                return order.Count;
            }
        }

        public bool UpdateSratus(int Id, string status)
        {
            using (var db = new RbContext())
            {
                var order = db.orders.Find(Id);
                order.OrderStatus = status;
                return db.SaveChanges() > 0;
            }
        }

        public Order GetOrderById(int Id)
        {
            using (var db = new RbContext())
            {

                return db.orders.Where(x => x.Id == Id).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
            }
        }


        public List<Order> GetOrdersUser(string UserId, int pageNo, int PageSize)
        {
            using (var db = new RbContext())
            {
                var order = db.orders.ToList();
                if (!string.IsNullOrEmpty(UserId))
                {
                    order = order.Where(x => x.Email.ToLower()==UserId.ToLower()).OrderByDescending(x=>x.OrderTime).ToList();
                }

                return order.Skip((pageNo - 1) * PageSize).Take(PageSize).ToList();
            }
        }

        public int SearchOrderCountUser(string UserId)
        {
            using (var db = new RbContext())
            {
                var order = db.orders.OrderBy(x => x.Id).ToList();

                if (!string.IsNullOrEmpty(UserId))
                {
                    order = order.Where(x => x.UserId.ToLower().Contains(UserId.ToLower())).ToList();
                }


                return order.Count;
            }
        }


      

        public List<OrderItem>OrderItem(int id)
        {
            using (var db = new RbContext())
            {
                return db.OrderItems.Where(x => x.Id == id).Include(x=>x.Product).ToList();
            }
        }

        public Order GetOrderByIdforUser(string UserId,int Id)
        {
            using (var db = new RbContext())
            {
                //if(db.orders.Where(x=>x.))

                return db.orders.Where(x => x.Email==UserId && x.Id == Id).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
            }
        }

        public bool UpdatecancleSratus(int Id, string Email,string status)
        {
            using (var db = new RbContext())
            {

                var order = db.orders.Where(x=>x.Email== Email && x.Id == Id).FirstOrDefault();
                order.OrderStatus = status;
                return db.SaveChanges() > 0;

            }
        }
    }
}
