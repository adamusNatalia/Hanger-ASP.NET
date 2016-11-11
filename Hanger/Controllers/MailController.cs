using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hanger.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EmailRequest()
        {
            return View();
        }
        public ActionResult ProcessRequest()
        {
            return View();
        }
    }
}