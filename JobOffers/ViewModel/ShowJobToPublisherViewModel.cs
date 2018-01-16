using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModel
{
    public class ShowJobToPublisherViewModel
    {
        public string UserName { get; set; }
        public string JobTitle {get; set;}
        public string Message { get; set; }
        public DateTime DataApplied { get; set; }
    }
}