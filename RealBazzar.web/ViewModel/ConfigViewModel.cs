using RealBazzar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBazzar.web.ViewModel
{
    public class ConfigViewModel
    {
        public string search { get; set; }
        public List<Config> configr { get; set; }

        public Pager pager { get; set; }
    }

    public class ConfigById
    {
        public Config ConfigbyId { get; set; }
    }
}