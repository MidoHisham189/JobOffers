using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOffers.Models;
using JobOffers.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
namespace JobOffers.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var jobs = db.categories
                .Include("jobs")
                .ToList();

            return View(jobs);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var job = db.jobs.SingleOrDefault(j => j.JobId == id);

            if (job == null)
            {
                return HttpNotFound();
            }
            Session["jobId"] = job.JobId;
            return View(job);

        }

        [HttpGet]
        [Authorize(Roles = "Searcher")]
        public ActionResult Apply()
        {
            if (Session["JobId"] == null)
            {
                return RedirectToAction ("Index");
            }
            return View();
        }




        [Authorize(Roles = "Searcher")]
        [HttpPost]
        public ActionResult Apply(string Message)
        {
          
            var JobId = (int) Session["JobId"];
            var UserId = User.Identity.GetUserId();
            var searchforjob = db.appliesForJobs.SingleOrDefault(aj => aj.JobId == JobId && aj.UserId == UserId);
            ApplyForJob apply = new ApplyForJob();
            if (searchforjob == null)
            {
                 apply = new ApplyForJob
                {
                    
                    UserId = User.Identity.GetUserId(),
                    JobId = JobId,
                    Message = Message,
                    ApplyDate = DateTime.Now
                };

                db.appliesForJobs.Add(apply);
                db.SaveChanges();

            }
            else
            {
                return Json(false);
            }


            return Json(true);
        }

        [HttpGet]
        [Authorize(Roles = "Searcher")]
        public ActionResult GetJobUserApplied()
        {
            var UserId = User.Identity.GetUserId();
            var jobuserApplied = db.appliesForJobs
                .Include(ja=> ja.job)
                .Include(ja=> ja.job.Category)
                .Where(ja => ja.UserId == UserId);

            return View(jobuserApplied);
        }

        [Authorize(Roles="Publisher")]
        public ActionResult GetUserAppliesTOjob()
        {
            string UserId = User.Identity.GetUserId();
            //var jobs = (from j in db.appliesForJobs
            //            join i in db.jobs
            //            on j.JobId equals i.JobId
            //            where i.User.Id == UserId
            //            select new ShowJobToPublisherViewModel()
            //            {
            //                UserName = j.User.UserName,
            //                JobTitle = i.JobTitle,
            //                Message = j.Message,
            //                DataApplied = j.ApplyDate
            //            }).ToList();

            //var jobs = (from j in db.appliesForJobs
            //            join i in db.jobs
            //            on j.JobId equals i.JobId
            //            where i.User.Id == UserId
            //            select new ShowJobToPublisherViewModel()
            //            {
            //                UserName = j.User.UserName,
            //                JobTitle = i.JobTitle,
            //                Message = j.Message,
            //                DataApplied = j.ApplyDate
            //            }).ToList();

               var jobs = (from j in db.appliesForJobs
                        join i in db.jobs
                        on j.JobId equals i.JobId
                        where i.User.Id == UserId
                        select j);

               var grouped = from j in jobs
                             group j by j.job.JobTitle
                                 into gr
                                 select new JobsViewModel()
                                 {
                                     JobTitle = gr.Key,
                                     Items = gr
                                 };
               return View(grouped.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}