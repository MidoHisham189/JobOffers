using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOffers.ViewModel;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
namespace JobOffers.Controllers
{
    [Authorize(Roles="Publisher")]
    public class JobController : Controller
    {
        private ApplicationDbContext db;

        public JobController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Job
        public ActionResult Index()
        {
         var jobs =  db.jobs
                .Include(x => x.Category)
                .ToList();
            return View(jobs);
        }

        public ActionResult New(JobViewModel job)
        {
            var jobVm = new JobViewModel
            {
                job = new Job(),
                Categories = db.categories.ToList()
            };
            return View(jobVm);
        }

        public ActionResult Save(Job job)
        {
            if (job.JobId == 0)
            {
                job.UserId = User.Identity.GetUserId();
                db.jobs.Add(job);
            }
            else
            {
                var jobInDb = db.jobs.SingleOrDefault(j => j.JobId == job.JobId);
                jobInDb.JobTitle = job.JobTitle;
                jobInDb.JobContent = job.JobContent;
                jobInDb.CategoryId = job.CategoryId;
                jobInDb.UserId = User.Identity.GetUserId();
               
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var job = db.jobs.Find(id);

            var viewModel = new JobViewModel
            {
                job = job,
                Categories = db.categories.ToList()
            };

            if(job == null){
                return HttpNotFound();
            }
            return View("New", viewModel);
        }
        public ActionResult Details(int id)
        {
            var job = db.jobs.SingleOrDefault(j => j.JobId == id);

            return View(job);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var job = db.jobs.SingleOrDefault(j => j.JobId == id);

            if (job == null)
            {
                return Json("Job Not Found");
            }

            db.jobs.Remove(job);
            db.SaveChanges();

            return Json("Job Deleted Successfully");
           

           

        }
    }
}