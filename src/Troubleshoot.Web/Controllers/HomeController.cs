using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Troubleshoot.Common;
using Troubleshoot.Web.Models;

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
            var model = new LeakModel();
            model.TotalBytes = Trouble.MemoryLeak();
            return View(model);
        }

        public ActionResult Locking()
        {
            var model = new LockingModel()
            {
                RequestCount = Trouble.GetRequestCount()
            };
            return View(model);
        }

        public ActionResult Catastrophic()
        {
            Trouble.CrashStackoverflow(7);
            return View();
        }

        public ActionResult Wrong()
        {
            Trouble.WrangCall();
            return View();
        }

        
    }
}