﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abcMarket.Models;

namespace abcMarket.Areas.Admin.Controllers
{
    public class ShippingController : Controller
    {
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult GetDataList()
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var models = db.Shippings.OrderBy(m => m.mno).ToList();
                return Json(new { data = models }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                if (id == 0)
                {
                    Shippings new_model = new Shippings();
                    return View(new_model);
                }
                var models = db.Shippings.Where(m => m.rowid == id).FirstOrDefault();
                return View(models);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Edit(Shippings models)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (abcMarketEntities db = new abcMarketEntities())
                {
                    if (models.rowid > 0)
                    {
                        //Edit 
                        var Shippings = db.Shippings.Where(m => m.rowid == models.rowid).FirstOrDefault();
                        if (Shippings != null)
                        {
                            Shippings.mno = models.mno;
                            Shippings.mname = models.mname;
                            Shippings.remark = models.remark;
                        }
                    }
                    else
                    {
                        //Save
                        db.Shippings.Add(models);
                    }
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpGet]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Delete(int id)
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Shippings.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult DeleteData(int id)
        {
            bool status = false;
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Shippings.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    db.Shippings.Remove(model);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}