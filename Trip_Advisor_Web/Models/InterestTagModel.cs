using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class InterestTagModel
    {
        public int InterestTagId { get; set; }
        public string FieldOfLife { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}