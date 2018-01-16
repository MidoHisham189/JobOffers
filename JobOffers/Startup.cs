using JobOffers.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobOffers.Startup))]
namespace JobOffers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>
            (new RoleStore<IdentityRole>(db));
            
            var UserManager = new UserManager<ApplicationUser>
                (new UserStore<ApplicationUser>(db));

            if(!roleManager.RoleExists("Admin")){

                var role = new IdentityRole();

                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();

                user.UserName = "Midooo";
                user.Email = "Midooo@gmail.com";

                string userPwd = "@Aa123456";

                var chkUser = UserManager.Create(user, userPwd);

                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");
                }
            }


        }
    }
}
