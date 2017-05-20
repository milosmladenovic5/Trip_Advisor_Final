using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class ListOfCountriesModel
    {
        public List<CountryModel> CountriesList { get; set; }

        public ListOfCountriesModel()
        {
            this.CountriesList = new List<CountryModel>();
        }
    }
}