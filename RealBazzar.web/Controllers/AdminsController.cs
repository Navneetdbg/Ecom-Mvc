﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBazzar.web.Controllers
{
    public class AdminsController : Controller
    {
        // GET: Admin
        [Authorize(Roles ="Admin")]
        
        public ActionResult Index()
        {
            return View();
        }
    }
}