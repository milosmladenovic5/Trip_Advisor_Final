using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip_Advisor_Neo4j.DomainModel
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public List<Place> Places { get; set; }
        public Country Country { get; set; }


        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }

        public City ()
        {
            Places = new List<Place>();
        }

    }
}
