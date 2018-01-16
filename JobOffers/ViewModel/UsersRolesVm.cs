using JobOffers.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModel
{
    public class UsersRolesVm
    {
        //public string UserName { get; set; }
        //public List<IdentityUserRole> Roles { get; set; }
        //public string RoleName { get; set; }

        public IEnumerator<ApplicationUser> users { get; set; }
    }
}