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
    public class ConfigServices
    {
        #region Singleton
        public static ConfigServices Instance {
            get
            {
                if (instance == null) instance = new ConfigServices();
                return instance;
            }
        }

        private static ConfigServices instance { get; set; }
        private ConfigServices()
        {

        }

        #endregion
        public Config getConfigs(string Key)
        {
            
            using (RbContext rb = new RbContext())
            {
                return rb.Configurations.Find(Key);
            }
        }

        public List<Config>GetAll(string search , int pageNo, int pageSize)
        {
            using (RbContext db = new RbContext())
            {
                //int pageSize = int.Parse(ConfigServices.Instance.getConfigs("ConfigTable").Value);


                if (!string.IsNullOrEmpty(search))
                {
                    return db.Configurations.
                        Where(a => a.Value.ToLower().Contains(search.ToLower())).
                        OrderBy(x => x.Key).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();

                }
                else
                {
                    return db.Configurations.OrderBy(x => x.Key).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                }
                
            }
        }

        public int GetCount(string search)
        {
            using (var db = new RbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return db.Configurations.
                        Where(a => a.Key.ToLower().Contains(search.ToLower())).Count();


                }
                else
                {
                    return db.Configurations.Count();
                }


            }
        }
        public void SaveCategories(Config config)
        {
            using (var db = new RbContext())
            {
                db.Configurations.Add(config);
                db.SaveChanges();
            }
        }

        public void Update(Config config)
        {
            using (var db = new RbContext())
            {
                db.Entry(config).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }

}
