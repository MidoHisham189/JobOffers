using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using JobOffers.Models;
namespace JobOffers.ViewModel
{
    public class RolesAndUsersViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public IEnumerable<IdentityRole> roles { get; set; }

        public IEnumerable<ApplicationUser> users { get; set; }
    }
}