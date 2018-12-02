using System;
using System.Collections.Generic;
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
            return View();
        }

        public ActionResult AddService()
        {
            return View();
        }

        public ActionResult EditService()
        {
            return View();
        }
    }
}