using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class ListOfCitiesModel
    {
        public List<CityModel> CitiesList { get; set; }

        public ListOfCitiesModel()
        {
            this.CitiesList = new List<CityModel>();
        }

       // public string Warning { get; set; }
    }
}