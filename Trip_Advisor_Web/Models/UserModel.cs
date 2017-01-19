using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trip_Advisor_Neo4j.DomainModel;

namespace Trip_Advisor_Web.Models
{
    public class UserModel
    {
        
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string DateOfBirth { get; set; }
        public bool FollowingHim { get; set; }

        public StatusModel UserStatus { get; set; }
        public List<UserModel> Followers { get; set; }
        public List<UserModel> Following { get; set; }
        public List<InterestTagModel> Interests { get; set; }
        public List<RecommendationModel> Recommended { get; set; }
        public PlaceModel CurrentLocation { get; set; }
        public List<PlaceModel> PlansToVisit { get; set; }
        public List<PlaceModel> Visited { get; set; }

        public HttpPostedFileBase PictureFile { get; set; }

        public UserModel()
        {
            this.Followers = new List<UserModel>();
            this.Interests = new List<InterestTagModel>();
            this.Recommended = new List<RecommendationModel>();
            this.PlansToVisit = new List<PlaceModel>();
            this.Visited = new List<PlaceModel>();
            this.Following = new List<UserModel>();
        }
    }
}