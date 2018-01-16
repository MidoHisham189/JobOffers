using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobOffers.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db;
        public CategoryController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Category
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        [HttpGet]
        public ActionResult New(Category Cat)
        {
            return View(Cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category Cat)
        {
            if (!ModelState.IsValid)
            {
                return View("New",Cat);
            }
            if (Cat.CategoryId == 0)
            {
                db.categories.Add(Cat);
            }
            else{
                var CategoryInDb = db.categories.SingleOrDefault(c=> c.CategoryId == Cat.CategoryId);
                
                CategoryInDb.CategoryName = Cat.CategoryName;
                CategoryInDb.CategoryDescription = Cat.CategoryDescription;

            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? Id)
        {
            var Cat = db.categories.Find(Id);

            if (Cat == null)
            {
                return HttpNotFound();
            }

            return View("New", Cat);

        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            var Cat = db.categories.SingleOrDefault(c => c.CategoryId == Id);

            if (Cat == null)
            {
                return HttpNotFound();
            }

            return View(Cat);

        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {

            var Cat = db.categories.SingleOrDefault(c => c.CategoryId == Id);

            if (Cat == null)
            {

            }

            db.categories.Remove(Cat);
            db.SaveChanges();

            return Json("Category Deleted Successfully");
        }
    }
}