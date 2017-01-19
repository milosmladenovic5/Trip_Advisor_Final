﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip_Advisor_Web.Models
{
    public class PlaceModel
    {
        public int PlaceId { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public int CityCenterDistance { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }

        public List<string> Pictures { get; set; }
        public List<InterestTagModel> Tags { get; set; }
        public List<RecommendationModel> Recommendations { get; set; }
        public CityModel PlaceLocation { get; set; }

        public PlaceModel()
        {
            this.Tags = new List<InterestTagModel>();
            this.Recommendations = new List<RecommendationModel>();
            PlaceLocation = new CityModel();
            this.Pictures = new List<string>();
        }

    }
}