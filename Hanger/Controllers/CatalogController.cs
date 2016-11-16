using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hanger.Models;
using PagedList;
using PagedList.Mvc;

namespace Hanger.Controllers
{
    public class CatalogController : Controller
    {
        private HangerDatabase db = new HangerDatabase();


        public ActionResult Index1(int id, string sortOrder, string searchBy, string search, int? page, string size)
        {
            var ad = from s in db.Ad
                     where (s.SubcategoryId == id)
                     select s;
            if (ad == null)
            {
                return RedirectToAction("NoItems", "Catalog");
            }

            var SizeLst = new List<string>();

            var SizeQry2 = from d in db.Ad
                           orderby d.SizeId
                           select d.SizeId;

            var SizeQry = from d in db.Size
                          orderby d.Name
                          select d.Name;
            //var SizeQry= from 

            SizeLst.AddRange(SizeQry.Distinct());
            ViewBag.size = new SelectList(SizeLst);

            var IdSize= from d in db.Size
                        where (d.Name==size)
                        select d.Id;
            //Size sId = new Size();
            //sId = (from d in db.Size
            //       where (d.Name == size)
            //       select d).Max();
            //int idSize = (from d in db.Size
            //       where (d.Name == size)
            //       select d.Id).Max();
            //int formID = int.Parse(IdSize.First());

            if (!String.IsNullOrEmpty(size))
            {
                int idSize = (from d in db.Size
                       where (d.Name == size)
                       select d.Id).Max();
                //ad = ad.Where(x => x.SizeId == idSize);
                ad = ad.Where(x => x.SizeId == 4);
                int amount=ad.Count();
                int amount2 = ad.Count();
            }


            //if (searchBy == "Color")
            //{
            //    return View(db.Ad.Where(x => x.ColorId.Equals(Int32.Parse(search)) || search == null).ToList().ToPagedList(page ?? 1, 5));
            //}
            //else
            //    if (searchBy == "Size")
            //{
            //    return View(db.Ad.Where(x => x.SizeId.Equals(Int32.Parse(search)) || search == null).ToList().ToPagedList(page ?? 1, 5));
            //}


            ViewBag.ThemeSortParm = sortOrder == "Price" ? "price_desc" : "Price";
           // ViewBag.LpSortParm = String.IsNullOrEmpty(sortOrder) ? "lp_desc" : "";
          //  ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";



            switch (sortOrder)
            {
                case "price_desc":
                    ad = ad.OrderByDescending(a => a.Price);
                    break;
                case "Price":
                    ad = ad.OrderBy(a => a.Price);
                    break;
                //case "lp_desc":
                //    ad = ad.OrderByDescending(s => s.ID);
                //    break;

                //case "Status":
                //    ad = ad.OrderBy(s => s.Status);
                //    break;
                //case "status_desc":
                //    ad = ad.OrderByDescending(s => s.Status);
                //    break;
                default:
                    ad = ad.OrderBy(s => s.Id);
                    break;
            }

            
            return View(ad.ToList().ToPagedList(page ?? 1, 9));
        }

        public ActionResult Index(int id, string size, string brand, string condition, string color)
        {
            var ad = from s in db.Ad
                     where (s.SubcategoryId == id)
                     select s;
            if (ad == null)
            {
                return RedirectToAction("NoItems", "Catalog");
            }

            var SizeLst = new List<string>();

            var SizeQry = from d in db.Size
                          orderby d.Name
                          select d.Name;

            SizeLst.AddRange(SizeQry.Distinct());
            ViewBag.size = new SelectList(SizeLst);

            var IdSize = from d in db.Size
                         where (d.Name == size)
                         select d.Id;

            var BrandLst = new List<string>();

            var BrandQry = from d in db.Brand
                          orderby d.Name
                          select d.Name;

            BrandLst.AddRange(BrandQry.Distinct());
            ViewBag.brand = new SelectList(BrandLst);

            var IdBrand = from d in db.Brand
                         where (d.Name == brand)
                         select d.Id;
            var ConditionLst = new List<string>();

            var ConditionQry = from d in db.Condition
                           orderby d.Name
                           select d.Name;

            ConditionLst.AddRange(ConditionQry.Distinct());
            ViewBag.condition = new SelectList(ConditionLst);

            var IdCondition = from d in db.Condition
                          where (d.Name == condition)
                          select d.Id;

            var ColorLst = new List<string>();

            var ColorQry = from d in db.Color
                           orderby d.Name
                           select d.Name;

            ColorLst.AddRange(ColorQry.Distinct());
            ViewBag.color = new SelectList(ColorLst);

            var IdColor = from d in db.Brand
                          where (d.Name == color)
                          select d.Id;


            if (!String.IsNullOrEmpty(size))
            {
                int idSize = (from d in db.Size
                              where (d.Name == size)
                              select d.Id).Max();
                ad = ad.Where(x => x.SizeId == idSize);
            }
            if (!String.IsNullOrEmpty(condition))
            {
                int idCondition = (from d in db.Condition
                              where (d.Name == condition)
                              select d.Id).Max();
                ad = ad.Where(x => x.ConditionId == idCondition);
            }

            if (!String.IsNullOrEmpty(color))
            {
                int idColor = (from d in db.Color
                              where (d.Name == color)
                              select d.Id).Max();
                ad = ad.Where(x => x.ColorId == idColor);    
            }

            if (!String.IsNullOrEmpty(brand))
            {
                int idBrand = (from d in db.Brand
                               where (d.Name == brand)
                               select d.Id).Max();
                ad = ad.Where(x => x.BrandId == idBrand);

            }

            return View(ad.ToList());
        }

        public ActionResult NoItems()
        {
            
            return View();
        }

    }
}