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


        //[httppost]
        //public actionresult submitphoto()
        //{
        //    httppostedfilebase file = request.files[0];
        //    byte[] imagesize = new byte[file.contentlength];
        //    file.inputstream.read(imagesize, 0, (int)file.contentlength);


        //    using (hangerentities datacontext = new hangerentities())
        //    {
        //        photos p = new photos();
        //        p.adid = 1;
        //        p.name = file.filename;

        //        if (datacontext.photos != null && datacontext.photos.count() != 0)
        //        {
        //            p.id = (from ph in datacontext.photos
        //                    select ph.id).max() + 1;
        //        }
        //        else
        //            p.id = 0;


        //        p.type = file.contenttype;
        //        datacontext.photos.add(p);
        //        datacontext.savechanges();
        //    }

        //    return redirecttoaction("userwall", "wall");
        //}
    }
}