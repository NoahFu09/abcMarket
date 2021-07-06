﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abcMarket.Models;

namespace abcMarket.Areas.Admin.Controllers
{
    public class ProductsController : Controller
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
                var models = db.Products
                    .OrderBy(m => m.product_no)
                    .ToList();
                if (models.Count > 0)
                {
                    for (int i = 0; i < models.Count; i++)
                    {
                        models[i].bool_istop = (models[i].istop == 1);
                        models[i].bool_ishot = (models[i].ishot == 1);
                        models[i].bool_issale = (models[i].issale == 1);
                        //models[i].bool_isMB = (models[i].isMB == 1);
                        //models[i].bool_isVGA = (models[i].isVGA == 1);
                        //models[i].bool_isRAM = (models[i].isRAM == 1);
                        //models[i].bool_isSSD = (models[i].isSSD == 1);
                        //models[i].bool_isPOWER = (models[i].isPOWER == 1);
                        //models[i].bool_isCPU = (models[i].isCPU == 1);
                    }
                }
                return Json(new { data = models }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var models = db.Products.Where(m => m.rowid == id).FirstOrDefault();

                //Category DropDownList
                string str_rowid = "0";
                var categoryList = new List<SelectListItem>();
                List<Categorys> clists = db.Categorys.OrderBy(m => m.category_no).ToList();
                foreach (var item in clists)
                {
                    SelectListItem list = new SelectListItem();
                    list.Value = item.rowid.ToString();
                    list.Text = Shop.GetCategoryName(item.rowid);
                    categoryList.Add(list);
                    if (id == 0)
                    { if (str_rowid == "0") str_rowid = item.rowid.ToString(); }
                }

                if (models != null) str_rowid = models.categoryid.ToString();

                //預設選擇哪一筆
                if (str_rowid != "0")
                    categoryList.Where(m => m.Value == str_rowid).First().Selected = true;
                ViewBag.CategoryList = categoryList;

                if (id == 0)
                {
                    Products new_model = new Products();
                    new_model.remark = "";
                    //new_model.start_count = 5;
                    //new_model.browse_count = 0;
                    new_model.bool_istop = false;
                    new_model.bool_ishot = false;
                    //new_model.bool_isMB = false;
                    //new_model.bool_isVGA = false;
                    //new_model.bool_isRAM = false;
                    //new_model.bool_isSSD = false;
                    //new_model.bool_isPOWER = false;
                    //new_model.bool_isCPU = false;
                    new_model.bool_issale = true;
                    return View(new_model);
                }

                models.bool_istop = (models.istop == 1);
                models.bool_ishot = (models.ishot == 1);
                //models.bool_isMB = (models.isMB == 1);
                //models.bool_isVGA = (models.isVGA == 1);
                //models.bool_isRAM = (models.isRAM == 1);
                //models.bool_isSSD = (models.isSSD == 1);
                //models.bool_isPOWER = (models.isPOWER == 1);
                //models.bool_isCPU = (models.isCPU == 1);
                models.bool_issale = (models.issale == 1);
                return View(models);
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Edit(Products models)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (abcMarketEntities db = new abcMarketEntities())
                {
                    int int_cate_id = 0;
                    if (models.rowid > 0)
                    {
                        //Edit 
                        var products = db.Products.Where(m => m.rowid == models.rowid).FirstOrDefault();
                        if (products != null)
                        {
                            int_cate_id = models.categoryid.GetValueOrDefault();
                            products.product_no = models.product_no;
                            products.product_name = models.product_name;
                            products.product_spec = models.product_spec;
                            products.categoryid = int_cate_id;
                            products.category_name = Shop.GetCategoryName(int_cate_id);
                            products.istop = (models.bool_istop) ? 1 : 0;
                            products.ishot = (models.bool_ishot) ? 1 : 0;
                            products.issale = (models.bool_issale) ? 1 : 0;
                            //products.isMB = (models.bool_isMB) ? 1 : 0;
                            //products.isVGA = (models.bool_isVGA) ? 1 : 0;
                            //products.isRAM = (models.bool_isRAM) ? 1 : 0;
                            //products.isSSD = (models.bool_isSSD) ? 1 : 0;
                            //products.isPOWER = (models.bool_isPOWER) ? 1 : 0;
                            //products.isCPU = (models.bool_isCPU) ? 1 : 0;
                            products.price_cost = models.price_cost;
                            products.price_discont = models.price_discont;
                            products.price_sale = models.price_sale;
                            //products.start_count = models.start_count;
                            //products.browse_count = models.browse_count;
                            //products.vendor_no = UserAccount.UserNo;
                            products.remark = models.remark;
                        }
                    }
                    else
                    {
                        //Save
                        //models.vendor_no = UserAccount.UserNo;
                        int_cate_id = models.categoryid.GetValueOrDefault();
                        models.category_name = Shop.GetCategoryName(int_cate_id);
                        models.istop = (models.bool_istop) ? 1 : 0;
                        models.ishot = (models.bool_ishot) ? 1 : 0;
                        models.issale = (models.bool_issale) ? 1 : 0;
                        //models.isMB = (models.bool_isMB) ? 1 : 0;
                        //models.isVGA = (models.bool_isVGA) ? 1 : 0;
                        //models.isRAM = (models.bool_isRAM) ? 1 : 0;
                        //models.isSSD = (models.bool_isSSD) ? 1 : 0;
                        //models.isPOWER = (models.bool_isPOWER) ? 1 : 0;
                        //models.isCPU = (models.bool_isCPU) ? 1 : 0;
                        db.Products.Add(models);
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
                var model = db.Products.Where(m => m.rowid == id).FirstOrDefault();
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
                var model = db.Products.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    db.Products.Remove(model);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult ProductProperty(int id = 0)
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Products.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    Shop.ProductNo = model.product_no;
                    return RedirectToAction("Index", "ProductProperty", new { @Area = "Admin" });
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Upload(int id = 0)
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Products.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    Shop.ProductNo = model.product_no;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string str_folder = string.Format("~/Images/product/{0}", Shop.ProductNo);
                    string str_folder_path = Server.MapPath(str_folder);
                    if (!Directory.Exists(str_folder_path)) Directory.CreateDirectory(str_folder_path);
                    string str_file_name = Shop.ProductNo + ".jpg";
                    var path = Path.Combine(str_folder_path, str_file_name);
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("Index");
        }

        [LoginAuthorize(RoleNo = "Admin")]
        public ActionResult Remark(int id = 0)
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Products.Where(m => m.rowid == id).FirstOrDefault();
                if (model != null)
                {
                    Shop.ProductNo = model.product_no;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [LoginAuthorize(RoleNo = "Admin")]
        [ValidateInput(false)]
        public ActionResult Remark(Products products)
        {
            bool status = false;
            using (abcMarketEntities db = new abcMarketEntities())
            {
                var model = db.Products.Where(m => m.rowid == products.rowid).FirstOrDefault();
                if (model != null)
                {
                    model.remark = products.remark;
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string url; // url to return
            string message; // message to display (optional)

            // 設定圖片上傳路徑
            string path = Server.MapPath("~/Images/uploads");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = System.IO.Path.Combine(path, Shop.ProductNo);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string ImageName = upload.FileName;
            string str_file_name = System.IO.Path.Combine(path, ImageName);
            if (System.IO.File.Exists(str_file_name)) System.IO.File.Delete(str_file_name);
            upload.SaveAs(str_file_name);


            // 取得網址
            // http://localhost:9999/Images/uploads/00001/ImageName.jpg
            url = Request.Url.GetLeftPart(UriPartial.Authority) + "/Images/uploads/" + Shop.ProductNo + "/" + ImageName;

            // passing message success/failure
            message = "圖片儲存成功!!";

            // since it is an ajax request it requires this string
            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\", \"" + message + "\");</script></body></html>";
            return Content(output);
        }
    }
}