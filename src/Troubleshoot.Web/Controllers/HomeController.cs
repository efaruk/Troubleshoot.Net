using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Troubleshoot.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Leak()
        {
            return View();
        }

        public ActionResult Locking()
        {
            return View();
        }

        public ActionResult Catastrophic()
        {
            return View();
        }

        public ActionResult Wrong()
        {
            return View();
        }
    }
}