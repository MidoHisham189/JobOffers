using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModel
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}