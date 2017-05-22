using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class ListOfTagsModel
    {
        public List<InterestTagModel> List { get; set; }

        public ListOfTagsModel()
        {
            this.List = new List<InterestTagModel>();
        }
    }
}