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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", cityId);

                var query = new CypherQuery("MATCH (city:City {CityId:{id} }) OPTIONAL MATCH(city) - [r] - () DELETE r, city",
                queryDict, CypherResultMode.Set);

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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", countryId);

                var query = new CypherQuery("MATCH (country:Country {CountryId:{id} }) OPTIONAL MATCH(country) - [r] - () DELETE r, country",
                queryDict, CypherResultMode.Set);

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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", interestTagId);

                var query = new CypherQuery("MATCH (interest:InterestTag {InterestTagId:{id} }) OPTIONAL MATCH(interest) - [r] - () DELETE r, interest",
                queryDict, CypherResultMode.Set);

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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", placeId);

                var query = new CypherQuery("MATCH (place:Place {PlaceId:{id} }) OPTIONAL MATCH(place) - [r] - () DELETE r, place",
                queryDict, CypherResultMode.Set);

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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", statusId);

                var query = new CypherQuery("MATCH (status:Status {StatusId:{id} }) OPTIONAL MATCH(status) - [r] - () DELETE r, status",
                queryDict, CypherResultMode.Set);

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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", userId);

                var query = new CypherQuery("MATCH (user:User {UserId:{userId} }) OPTIONAL MATCH(user) - [r] - () DELETE r, user",
                queryDict, CypherResultMode.Set);

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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId1", followerId);
                queryDict.Add("userId2", followingId);

                var query = new CypherQuery("MATCH (follower:User {UserId: {userId1} }) - [r:FOLLOWS] -> (following:User {UserId: {userId2} }) DELETE r",
                queryDict, CypherResultMode.Set);

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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId", recommenderId);
                queryDict.Add("placeId", placeId);
                var query = new CypherQuery("MATCH (user:User {UserId:{userId} }) - [r:RECOMMENDS] ->(place:Place {PlaceId:{placeId} }) DELETE r",
                    queryDict, CypherResultMode.Set);

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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId", userId);
                queryDict.Add("palceId", placeId);

                var query = new CypherQuery("MATCH (user:User {UserId: {userId} } ) - [r:PLANSTOVISIT] ->(place:Place {PlaceId: {placeId} }) DELETE r",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }
        }               // kada korisnik poseti mesto
        public static bool DeleteCurrentLocation (int userId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId", userId);

                var query = new CypherQuery("MATCH (user:User {UserId: {userId} }) - [r:CURRENTLYAT] -> ()  DELETE r", queryDict, CypherResultMode.Set);
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
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId", userId);

                var query = new CypherQuery("MATCH (user:User {UserId:{userId} }) - [r:HASINTEREST] -> ()  DELETE r", queryDict, CypherResultMode.Set);
                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteRecommendationById(int recommendationId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", recommendationId);

                var query = new CypherQuery("MATCH () - [r:RECOMMENDS] -> () WHERE r.RecommendationId = {id} DELETE r", queryDict, CypherResultMode.Set);
                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool DeleteRecommendation(int userId, int placeId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId", userId);
                queryDict.Add("placeId", placeId);

                var query = new CypherQuery("MATCH (user:User{UserId: {userId} }) - [r:RECOMMENDS] -> (place:Place{PlaceId:{placeId} })  DELETE r", queryDict, CypherResultMode.Set);
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