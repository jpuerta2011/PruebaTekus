using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tekus.Web.Client.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            ViewBag.endpoint = ConfigurationManager.AppSettings["endpoint"];
            return View();
        }

        public ActionResult EditClient(long id)
        {
            ViewBag.endpoint = ConfigurationManager.AppSettings["endpoint"];
            return View(id);
        }

        public ActionResult AddClient()
        {
            ViewBag.endpoint = ConfigurationManager.AppSettings["endpoint"];
            return View();
        }
    }
}