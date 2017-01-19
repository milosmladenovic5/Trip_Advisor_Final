using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip_Advisor_Neo4j.DomainModel
{
    public class Country
    {
        public int CountryId { get; set; }

        public string NationalFlag { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; }
        public float OverallRating { get; set; }

        public Country()
        {
            Cities = new List<City>();
        }
    }
}
