using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trip_Advisor_Neo4j.DomainModel
{
    public class User
    {
       
        public string Description { get; set; }
        public long DateJoined { get; set; }
        public int UserStatusFLAG { get; set; }

        // 0 - registrovan 
        // 1 - redovan korisnik
        // 2 - muted
        // 3 - suspendovan
        // 9 - moderator
        // 10 - admin

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }

        public Status UserStatus { get; set; }
        public List<User> Followers { get; set; }
        public List<User> Following { get; set; }
        public List<InterestTag> Interests { get; set; }
        public List<Recommendation> Recommended { get; set; }
        public Place CurrentLocation { get; set; }
        public List<Place> PlansToVisit { get; set; }
        public List<Place> Visited { get; set; }

        public User()
        {
            this.Followers = new List<User>();
            this.Interests = new List<InterestTag>();
            this.Recommended = new List<Recommendation>();
            this.PlansToVisit = new List<Place>();
            this.Visited = new List<Place>();
            this.Following = new List<User>();
        }
         
    }
}
