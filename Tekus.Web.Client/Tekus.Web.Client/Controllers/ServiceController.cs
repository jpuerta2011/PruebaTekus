using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tekus.Web.Client.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            ViewBag.endpoint = ConfigurationManager.AppSettings["endpoint"];
            return View();
        }

        public ActionResult AddService()
        {
            ViewBag.endpoint = ConfigurationManager.AppSettings["endpoint"];
            return View();
        }

        public ActionResult EditService(long id)
        {
            ViewBag.endpoint = ConfigurationManager.AppSettings["endpoint"];
            return View(id);
        }
    }
}