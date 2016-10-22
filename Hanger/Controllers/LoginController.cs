using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hanger.Models;
namespace Hanger.Controllers
{
    public class LoginController : Controller
    {
        private HangerEntities db = new HangerEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            Console.WriteLine("Login");
            System.Diagnostics.Debug.Write("Login");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {

            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Photo");
            ModelState.Remove("Description");
            ModelState.Remove("Mail");
            ModelState.Remove("Date_access");

            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
               
                {
                    Console.WriteLine("Zalogowano");
                    var v = db.User.Where(a => a.Profil_name.Equals(u.Profil_name) && a.Password.Equals(u.Password)).FirstOrDefault();

                    if (v != null)
                    {
                        Session["LogedUserID"] = v.Id.ToString();
                        Session["LogedUserFullname"] = v.Profil_name.ToString();
                        if (v.Profil_name.ToString() == "admin")
                        {
                            return RedirectToAction("AfterLoginAdmin");
                        }
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
            return View(u);
        }
        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}