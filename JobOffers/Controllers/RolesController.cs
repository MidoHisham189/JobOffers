using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOffers.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using JobOffers.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
namespace JobOffers.Controllers
{
    [Authorize(Roles="Admin")]
    public class RolesController : Controller
    {
        private ApplicationDbContext db;

        public RolesController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Roles
        public ActionResult Index()
        {
            var rolevm = new RoleViewModel
            {
                roles = db.Roles.ToList()
            };
            return View(rolevm);
        }

        [HttpGet]
        public ActionResult AddNewRole(RoleViewModel rolevm)
        {
            return View(rolevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(RoleViewModel role)
        {
            if (!ModelState.IsValid)
            {

                return View(role);
            }

            if(role.Id == null){
                var IRole = new IdentityRole
                {
                    Name = role.Name
                };

                db.Roles.Add(IRole);

            }
            else
            {
               IdentityRole roleInDb = db.Roles.SingleOrDefault(r => r.Id == role.Id);

               if (roleInDb == null)
               {

               }

               roleInDb.Name = role.Name;

            }

            db.SaveChanges();
            return RedirectToAction("Index");



        }

        public ActionResult Edit(string Id)
        {
            if (Id == null)
            {
                
            }

            var role = db.Roles.SingleOrDefault(r => r.Id == Id);

            var roleViewModel = new RoleViewModel{
                
                Id = role.Id,
                Name = role.Name
            };
            return View("AddNewRole", roleViewModel);
        }
        [HttpPost]
        public ActionResult Delete(string Id)
        {
            var roleToDelete = db.Roles.SingleOrDefault(x => x.Id == Id);

            if (roleToDelete == null)
            {
                
            }


            db.Roles.Remove(roleToDelete);
            db.SaveChanges();

            return Json("Role Deleted Successfully");
        }

        [HttpGet]
        public ActionResult AddRoleToUser()
        {
            var roleUserVm = new RolesAndUsersViewModel
            {
                roles = db.Roles.ToList(),
                users = db.Users.ToList()
            };
            return View(roleUserVm);
        }

        [HttpPost]
        public ActionResult AddRoleToUser(RolesAndUsersViewModel roletoUser)
        {
            var roleManager = new RoleManager<IdentityRole>
            (new RoleStore<IdentityRole>(db));

          
            var userManager = new UserManager<ApplicationUser>
            (new UserStore<ApplicationUser>(db));


            var user = db.Users.SingleOrDefault(u => u.Id == roletoUser.UserId);

            if (user == null)
            {
                return Json("Please Enter Valid User");
            }
            
            
            userManager.AddToRole(roletoUser.UserId, roletoUser.RoleId);

      
           
           
            return Json("Your Role has been Added");
        }


        public ActionResult UsersRoles()
        {

            //List<UsersRolesVm> UsersInRoles = new List<UsersRolesVm>();

            //foreach (var users in db.Users.Include(u => u.Roles))
            //{
            //    UsersInRoles.Add(

            //        new UsersRolesVm
            //        {

            //            UserName = users.UserName,
            //            Roles = users.Roles.ToList()
            //        }
            //        );

            //}


            
           // UsersRolesVm user = new UsersRolesVm();
           //List<ApplicationUser> UsersInRoles = new List<ApplicationUser>();

           // foreach (var role in db.Roles.ToList())
           // {
           // //  var users =  db.Users.Where(u => u.Roles.Select(y => y.RoleId).Contains(role.Name))

           //  db.Users.s

           // }

            var userWithRoles = (from user in db.Users
                                 from userRole in user.Roles
                                 join role in db.Roles
                                 on userRole.RoleId equals role.Id
                                 select new UserViewModel()
                                 {
                                     UserName = user.UserName,
                                     Email = user.Email,
                                     Role = role.Name
                                 }).ToList();

            return View(userWithRoles);
        }
    }
}