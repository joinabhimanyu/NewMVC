using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using NewMVC.Models;

namespace NewMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();
        public CategoriesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult search_category(int ID)
        {
            var found_category = db.Categories.Find(ID);
            
            if (found_category != null)
            {
                return Json(found_category, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpNotFoundResult("Matching records not found");
            }
        }

        [HttpPost]
        public ActionResult search_category_by_name(string category_name)
        {
            //string CategoryName = Request.QueryString["StateName"].ToString().Trim();
            var found_category = db.Categories.Where(m => m.Category_Name == category_name);
            if (found_category != null)
            {
                return Json(found_category.First(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new HttpNotFoundResult("No matching records found");
            }
        }

        public PartialViewResult Create()
        {
            Category category = new Category();
            return PartialView("_CreateCategory", category);
        }

        public ActionResult CreateCategory()
        {
            Category category = new Category();
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public ActionResult UpdateCategory(int ID)
        {
            var found_category = db.Categories.Find(ID);
            if (found_category != null)
            {
                return View(found_category);
            }
            else
            {
                return new HttpNotFoundResult("No matching records found");
            }
        }

        [HttpPost]
        public ActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();

                //var result = db1.Categories.Where(m => m.Category_Name == "some")
                //    .Where(m => m.Description == "something")
                //    .OrderBy(m => m.Category_ID)
                //    .Select(m => m).ToList();

                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

    }
}
