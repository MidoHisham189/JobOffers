using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<IdentityRole> roles { get; set; }

    }
}