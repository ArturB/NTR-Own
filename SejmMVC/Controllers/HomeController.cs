using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SejmMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posel()
        {
            return View();
        }

        public ActionResult Club()
        {
            return View();
        }

        public ActionResult Vote()
        {
            return View();
        }

    }
}