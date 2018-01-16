using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModel
{
    public class JobViewModel
    {
        public Job job { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}