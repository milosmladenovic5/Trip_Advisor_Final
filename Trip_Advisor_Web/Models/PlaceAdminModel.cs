using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class PlaceAdminModel
    {
        public bool Update { get; set; }
        public PlaceModel Place { get; set; }
        public List<CityModel> AllCities { get; set; }
        public List<string> AllTags { get; set; }

        public int SelectedID { get; set; }
        public List<string> SelectedTags { get; set; }

        public PlaceAdminModel()
        {
            this.Place = new PlaceModel();
            this.AllCities = new List<CityModel>();
            this.AllTags = new List<string>();
            this.SelectedTags = new List<string>();
        }
    }
}