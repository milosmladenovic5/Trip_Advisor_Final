using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip_Advisor_Neo4j.DomainModel
{
    public class Place
    {
        public int PlaceId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public int CityCenterDistance { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public List<string> Pictures { get; set; }

        
        public List<InterestTag> Tags { get; set; }
        public List<Recommendation> Recommendations { get; set;}

       
        public City PlaceLocation { get; set; }

        public Place()
        {
            this.Tags = new List<InterestTag>();
            this.Recommendations = new List<Recommendation>();
        }

    }
}
