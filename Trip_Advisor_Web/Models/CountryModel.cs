using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class CountryModel
    {

        public int CountryId { get; set; }

        public string Name { get; set; }
        public List<CityModel> Cities { get; set; }
        public float OverallRating { get; set; }
        public string NationalFlag { get; set; }
        public List<PlaceModel> TopRatedPlaces { get; set; }
        public List<PlaceModel> TheMostVisitedPlaces { get; set; }


        public CountryModel()
        {
            this.Cities = new List<CityModel>();
            this.TopRatedPlaces = new List<PlaceModel>();
            this.TheMostVisitedPlaces = new List<PlaceModel>();
        }
    }
}