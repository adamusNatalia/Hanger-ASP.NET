using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hanger.Models;
using System.Data.Entity.Infrastructure;
using System.Net.Mail;
using System.Net;

namespace Hanger.Controllers
{
    public class AdController : Controller
    {
        private HangerDatabase db = new HangerDatabase();
        // GET: Ad
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int Id)
        {
            Ad advertisement = db.Ad.Find(Id);
            return View(advertisement);
        }
        

        public ActionResult Product(int Id)
        {
            Ad advertisement = db.Ad.Find(Id);

            return View(advertisement);

        }
        [HttpPost]
        public ActionResult SendMail2(string email, string body, string subject, string to, int id )
        {
            var user = from p in db.User
                       where p.Profil_name == to
                       select p;

            
            MailMessage msg = new MailMessage();
          
            String emailTo = user.First().Mail;

            msg.To.Add(emailTo);
            //msg.To.Add("hanger.natalia@gmail.com");
            //msg.From = new MailAddress(email);
            msg.From = new MailAddress("hanger.natalia@gmail.com",email);
            msg.Subject = subject;
            msg.Body = body;
            msg.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.Credentials = new NetworkCredential("hanger.natalia@gmail.com", "hangertest");

            client.EnableSsl = true;
    
            client.Send(msg);
            return RedirectToAction("Product", "Ad", new { Id = id });
        }
        [HttpPost]
        public ActionResult SendMail()
        {
            // MailMessage msg = new MailMessage();
            // msg.To.Add("hanger.natalia@gmail.com");
            // msg.From = new MailAddress("hanger.natalia@gmail.com");
            MailMessage msg = new MailMessage("hanger.natalia@gmail.com", "hanger.natalia@gmail.com");
            msg.Subject = "Welcome To REBAR Mobile Showcase";
            msg.Body = "Hi," + Environment.NewLine + @"Welcome to REBAR Mobile Showcase. Please click on the below link : https://ciouishowcase.accenture.com/mobile/m"
                + Environment.NewLine + "Regards," + Environment.NewLine + "CIO Design Agency";
            msg.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            client.Credentials = new NetworkCredential("hanger.natalia@gmail.com", "hangertest");
            // client.Host = "email.abc.com";
            //client.Port = 587;
            //  client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            // client.UseDefaultCredentials = true;
            client.Send(msg);
            return RedirectToAction("Index", "Mail");
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Message(Message M, string email, string name, string title)
        //{

        //    try
        //    {
        //        if (ModelState.IsValid)

        //        {
        //            A.Date_start = DateTime.Now;
        //            A.UserId = (Session["LogedUserID"] as User).Id;
        //            db.Ad.Add(A);
        //            db.SaveChanges();
        //            ModelState.Clear();

        //        }
        //    }
        //    catch (RetryLimitExceededException /* dex */)
        //    {
        //        //Log the error (uncomment dex variable name and add a line here to write a log.)
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
        //    }
        //    if (ModelState.IsValid)
        //    {

        //        {
        //            M.Date = DateTime.Now;
        //            if (Session["LogedUserID"] != null)
        //            {
        //                M.UserId = (Session["LogedUserID"] as User).Id;
        //            }
        //            else
        //            {
        //                M.UserId = 4;
        //            }
        //            M.UserId2=
        //            db.SaveChanges();
        //            ModelState.Clear();
        //            U = null;
        //            ViewBag.Message = "Successfully Registration Done";
        //        }
        //    }
        //    return View(U);
        //}






        public ActionResult Photo()
        {
          
            return View();
        }

        public ActionResult New()
        {
            SizeDropDownList();
            BrandDropDownList();
            ColorDropDownList();
            ConditionDropDownList();
            SubcategoryDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Ad A)
        {
            try
            {
                if (ModelState.IsValid)

                {
                    A.Date_start = DateTime.Now;
                    A.UserId = (Session["LogedUserID"] as User).Id;
                    //A.Id = 23;
                    db.Ad.Add(A);
                    //db.SaveChanges();
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                    ModelState.Clear();
                    
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        
            SizeDropDownList(A.SizeId);
            BrandDropDownList(A.BrandId);
            ColorDropDownList(A.ColorId);
            ConditionDropDownList(A.ConditionId);
            SubcategoryDropDownList(A.SubcategoryId);


            //return View(A);
            return RedirectToAction("Photo", "Ad");
        }

        private void SizeDropDownList(object selectedSize = null)
        {
            var sizeQuery = from d in db.Size
                                   orderby d.Name
                                   select d;
            ViewBag.SizeId = new SelectList(sizeQuery, "Id", "Name", selectedSize);
        }
        private void ColorDropDownList(object selectedColor = null)
        {
            var sizeQuery = from d in db.Color
                            orderby d.Name
                            select d;
            ViewBag.COlorId = new SelectList(sizeQuery, "Id", "Name", selectedColor);
        }
        private void BrandDropDownList(object selectedBrand = null)
        {
            var sizeQuery = from d in db.Brand
                            orderby d.Name
                            select d;
            ViewBag.BrandId = new SelectList(sizeQuery, "Id", "Name", selectedBrand);
        }

        private void ConditionDropDownList(object selectedCondition = null)
        {
            var sizeQuery = from d in db.Condition
                            orderby d.Name
                            select d;
            ViewBag.ConditionId = new SelectList(sizeQuery, "Id", "Name", selectedCondition);
        }

        private void SubcategoryDropDownList(object selectedSubcategory = null)
        {
            var sizeQuery = from d in db.Subcategory
                            orderby d.Name
                            select d;
            ViewBag.SubcategoryId = new SelectList(sizeQuery, "Id", "Name", selectedSubcategory);
        }

        //private void CategoryDropDownList(object selectedSubcategory = null)
        //{
        //    var sizeQuery = from d in db.Category
        //                    orderby d.Name
        //                    select d;
        //    ViewBag.SelectedtegoryId = new SelectList(sizeQuery, "Id", "Name", selectedCategory);
        //}




        public ActionResult Tiles()
            {
                ViewBag.Title = "Hanger";
                return View();
            }

        public ActionResult Details2()
        {
            var ad = from s in db.Ad
                        select s;

            return View(ad.ToList());
        }
        public ActionResult FrontPhoto()
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] imageSize = new byte[file.ContentLength];
            file.InputStream.Read(imageSize, 0, (int)file.ContentLength);


            using (HangerDatabase db = new HangerDatabase())
            {
                Photos p = new Photos();
                p.Photo = imageSize;
                p.FIle_name = file.FileName;

                if (db.Photos != null && db.Photos.Count() != 0)
                {
                    p.Id = (from ph in db.Photos
                                 select ph.Id).Max() + 1;
                }
                else
                    p.Id = 0;

               // p.OwnerId = (Session["CurrentUserEmail"] as User).UserId;
                p.AdId= (from ad in db.Ad
                         select ad.Id).Max();
                p.Type = file.ContentType;
                db.Photos.Add(p);
                db.SaveChanges();
            }

            //return RedirectToAction("New", "Home");
            return RedirectToAction("Photo", "Ad");
        }


        public ActionResult MainPhoto()
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] imageSize = new byte[file.ContentLength];
            file.InputStream.Read(imageSize, 0, (int)file.ContentLength);


            using (HangerDatabase db = new HangerDatabase())
            {
                Photos p = new Photos();
                p.Photo = imageSize;
                p.FIle_name = file.FileName;

                if (db.Photos != null && db.Photos.Count() != 0)
                {
                    p.Id = (from ph in db.Photos
                            select ph.Id).Max() + 1;
                }
                else
                    p.Id = 0;

                // p.OwnerId = (Session["CurrentUserEmail"] as User).UserId;
                p.AdId = (from ad in db.Ad
                          select ad.Id).Max();
                p.Type = file.ContentType;
                p.Main_photo = true;
                p.PhotoSiteId = 1;
                db.Photos.Add(p);
                db.SaveChanges();
            }

            //return RedirectToAction("New", "Home");
            return RedirectToAction("Photo", "Ad");
        }
        public ActionResult ModelPhoto()
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] imageSize = new byte[file.ContentLength];
            file.InputStream.Read(imageSize, 0, (int)file.ContentLength);


            using (HangerDatabase db = new HangerDatabase())
            {
                Photos p = new Photos();
                p.Photo = imageSize;
                p.FIle_name = file.FileName;

                if (db.Photos != null && db.Photos.Count() != 0)
                {
                    p.Id = (from ph in db.Photos
                            select ph.Id).Max() + 1;
                }
                else
                    p.Id = 0;

                // p.OwnerId = (Session["CurrentUserEmail"] as User).UserId;
                p.AdId = (from ad in db.Ad
                          select ad.Id).Max();
                p.Type = file.ContentType;
                p.Main_photo = false;
                p.PhotoSiteId = 2;
                db.Photos.Add(p);
                db.SaveChanges();
            }
            //return RedirectToAction("New", "Home");
            return RedirectToAction("Photo", "Ad");
        }



        public ActionResult ZoomPhoto()
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] imageSize = new byte[file.ContentLength];
            file.InputStream.Read(imageSize, 0, (int)file.ContentLength);


            using (HangerDatabase db = new HangerDatabase())
            {
                Photos p = new Photos();
                p.Photo = imageSize;
                p.FIle_name = file.FileName;

                if (db.Photos != null && db.Photos.Count() != 0)
                {
                    p.Id = (from ph in db.Photos
                            select ph.Id).Max() + 1;
                }
                else
                    p.Id = 0;

                // p.OwnerId = (Session["CurrentUserEmail"] as User).UserId;
                p.AdId = (from ad in db.Ad
                          select ad.Id).Max();
                p.Type = file.ContentType;
                p.Main_photo = false;
                p.PhotoSiteId = 3;
                db.Photos.Add(p);
                db.SaveChanges();
            }

            //return RedirectToAction("New", "Home");
            return RedirectToAction("Photo", "Ad");
        }

        public ActionResult BackPhoto()
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] imageSize = new byte[file.ContentLength];
            file.InputStream.Read(imageSize, 0, (int)file.ContentLength);


            using (HangerDatabase db = new HangerDatabase())
            {
                Photos p = new Photos();
                p.Photo = imageSize;
                p.FIle_name = file.FileName;

                if (db.Photos != null && db.Photos.Count() != 0)
                {
                    p.Id = (from ph in db.Photos
                            select ph.Id).Max() + 1;
                }
                else
                    p.Id = 0;

                // p.OwnerId = (Session["CurrentUserEmail"] as User).UserId;
                p.AdId = (from ad in db.Ad
                          select ad.Id).Max();
                p.Type = file.ContentType;
                p.Main_photo = false;
                p.PhotoSiteId = 4;
                db.Photos.Add(p);
                db.SaveChanges();
            }

            //return RedirectToAction("New", "Home");
            return RedirectToAction("Photo", "Ad");
        }

    }

    }

    