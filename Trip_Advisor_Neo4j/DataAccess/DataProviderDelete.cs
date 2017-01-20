using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip_Advisor_Neo4j.DomainModel;

namespace Trip_Advisor_Neo4j.DataAccess
{
    public static class DataProviderDelete
    {
        public static bool DeleteCity(int cityId)
        {
            try
            {
                var query = new CypherQuery("MATCH (city:City {CityId:" + cityId + "}) OPTIONAL MATCH(city) - [r] - () DELETE r, city",
                null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteCountry(int countryId)
        {
            try
            {
                var query = new CypherQuery("MATCH (country:Country {CountryId:" + countryId + "}) OPTIONAL MATCH(country) - [r] - () DELETE r, country",
                null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteInterestTag(int interestTagId)
        {
            try
            {
                var query = new CypherQuery("MATCH (interest:InterestTag {InterestTagId:" + interestTagId + "}) OPTIONAL MATCH(interest) - [r] - () DELETE r, interest",
                null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeletePlace(int placeId)
        {
            try
            {
                var query = new CypherQuery("MATCH (place:Place {PlaceId:" + placeId + "}) OPTIONAL MATCH(place) - [r] - () DELETE r, place",
                null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteStatus(int statusId)
        {
            try
            {
                var query = new CypherQuery("MATCH (status:Status {StatusId:" + statusId + "}) OPTIONAL MATCH(status) - [r] - () DELETE r, status",
                null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteUser(int userId)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}) OPTIONAL MATCH(user) - [r] - () DELETE r, user",
                null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }



        public static bool Unfollow(int followerId, int followingId)
        {
            try
            {
                var query = new CypherQuery("MATCH (follower:User {UserId:" + followerId + "}) - [r:FOLLOWS] -> (following:User {UserId:" + followingId + "}) DELETE r",
                null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Unrecommend(int recommenderId, int placeId)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + recommenderId + "} ) - [r:RECOMMENDS] ->(place:Place {PlaceId:" + placeId + "}) DELETE r",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool RemoveFromPlansToVisitList(int userId, int placeId)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "} ) - [r:PLANSTOVISIT] ->(place:Place {PlaceId:" + placeId + "}) DELETE r",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }
        }               // kada korisnik poseti mesto

        public static bool DeleteCurrentPlace (int userId)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}) - [r:CURRENTLYAT] -> (place)  DELETE r", null, CypherResultMode.Set);
                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteInterestsOfUser(int userId)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}) - [r:HASINTEREST] -> ()  DELETE r", null, CypherResultMode.Set);
                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}