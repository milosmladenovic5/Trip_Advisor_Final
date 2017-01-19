using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class RecommendationModel
    {
        public int RecommendationId { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public DateTime RecommendationTime { get; set; }

        public UserModel RefferedBy { get; set; }
        public PlaceModel RatedPlace { get; set; }

        public RecommendationModel()
        {
            RefferedBy = new UserModel();
            RatedPlace = new PlaceModel();
        }
    }
}