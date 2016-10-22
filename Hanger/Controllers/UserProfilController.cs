using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hanger.Models;

namespace Hanger.Controllers
{
    public class UserProfilController : Controller
    {
        // GET: UserProfil
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SubmitPhoto()
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] imageSize = new byte[file.ContentLength];
            file.InputStream.Read(imageSize, 0, (int)file.ContentLength);


            using (HangerEntities DataContext = new HangerEntities())
            {
                Photos p = new Photos();
                p.AdId = 1;
                p.Name = file.FileName;

                if (DataContext.Photos != null && DataContext.Photos.Count() != 0)
                {
                    p.Id = (from ph in DataContext.Photos
                                 select ph.Id).Max() + 1;
                }
                else
                    p.Id = 0;

               
                p.Type = file.ContentType;
                DataContext.Photos.Add(p);
                DataContext.SaveChanges();
            }

            return RedirectToAction("UserWall", "Wall");
        }
    }
}