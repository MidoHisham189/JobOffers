using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class Job
    {
        public int JobId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobContent { get; set; }



        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }


        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}