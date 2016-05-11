using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Troubleshoot.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog m_Logger = log4net.LogManager.GetLogger(typeof(HomeController).FullName);

        public ActionResult Index()
        {
            m_Logger.Debug("Home page viewed.");
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            m_Logger.Warn("This is a warning message!");
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