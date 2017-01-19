using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trip_Advisor_Neo4j.DomainModel;

namespace Trip_Advisor_Web.Models
{
    public class CityModel
    {

        public int CityId { get; set; }

        public string Name { get; set; }
        public List<PlaceModel> Places { get; set; }
        public CountryModel Country { get; set; }

        public CityModel()
        {
            this.Places = new List<PlaceModel>();
        }

    }
}