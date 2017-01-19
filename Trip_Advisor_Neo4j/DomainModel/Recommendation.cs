using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip_Advisor_Neo4j.DomainModel
{
    public class Recommendation
    {
        public int RecommendationId { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime RecommendationTime { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public User RefferedBy { get; set; }
        public Place RatedPlace { get; set; }
    }
}
