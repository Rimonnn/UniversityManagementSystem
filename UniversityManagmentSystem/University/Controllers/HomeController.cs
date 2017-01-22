using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Our mission";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact Information";

            return View();
        }
    }
}