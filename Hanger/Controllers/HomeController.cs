using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hanger.Models;

namespace Hanger.Controllers
{
    public class HomeController : Controller
    {

        private HangerDatabase db = new HangerDatabase();
        public ActionResult Index()
        {
            ViewBag.Title="Hanger";
            return View();
        }

        public ActionResult New()
        {
            var ad = from s in db.Ad
                     select s;

            return View(ad.ToList());
        }
    }
}