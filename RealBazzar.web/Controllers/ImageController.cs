using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBazzar.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ImageController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadImage()
        {
            string path = Server.MapPath("~/Content/images/");
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                file.SaveAs(path + file.FileName);
            }
            return Json(files.Count + " Files Uploaded!");
        }


    }
}