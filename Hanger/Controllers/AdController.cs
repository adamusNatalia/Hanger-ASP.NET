using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hanger.Models;
using System.Data.Entity.Infrastructure;

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
                    db.Ad.Add(A);
                    db.SaveChanges();
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


            return View(A);
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

        public ActionResult Tiles()
            {
                ViewBag.Title = "Hanger";
                return View();
            }

}

    }

    