using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class CityAdminModel
    {
        public CityModel City { get; set; }
        public ListOfCountriesModel AllCountries { get; set; }
        public int SelectedID { get; set; }
        public bool Update { get; set; }

        public CityAdminModel()
        {
            this.City = new CityModel();
            this.AllCountries = new ListOfCountriesModel();
            this.SelectedID = 0;
            this.Update = false;

        }
    }
}