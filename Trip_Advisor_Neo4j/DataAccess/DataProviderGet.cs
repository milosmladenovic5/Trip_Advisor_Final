using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip_Advisor_Neo4j.DomainModel;
using Neo4jClient;
using System.ComponentModel;
using System.Windows.Forms;

namespace Trip_Advisor_Neo4j.DataAccess
{
    public class DataProviderGet
    {

     

        public static string GetMaxId(string entityType)
        {

            //var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where exists(n.Id) return max(n.Id)",
            //        new Dictionary<string, object>(), CypherResultMode.Set);

            var query = new CypherQuery("match (n:" + entityType + "Id) return max(n.Id)", null, CypherResultMode.Set);
           

            return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
                
        } 
        public static int GenerateId(string entityType)
        {
            try
            {
                int mId = Int32.Parse(GetMaxId(entityType));
                var createIdQuery = new CypherQuery("match (n:"+entityType+"Id {Id:"+mId+"}) set n.Id = "+(++mId)+"", null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(createIdQuery);

                return (mId);
            }
            catch
            {
                return -1;
            }
           
        }

        public static List<Place> GetPlacesWithTag(string tagName)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("tagName", tagName);

                var query = new CypherQuery("match (place), (place) - [:HASINTERESTTAG] -> (tag:InterestTag {Name: {tagName} }) return place", queryDict, CypherResultMode.Set);


                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public static List<Place> GetSimilarPlaces(int userId, int placeId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("placeId", placeId);  // VISAK - ALI NEKA GA ZA SADA!
                queryDict.Add("userId", userId);
                var query = new CypherQuery("match (place:Place {PlaceId: {placeId} }), (places) <- [:HASPLACE] - (city) - [:HASPLACE] -> (place) - [:HASINTERESTTAG] -> (tag) <- [:HASINTERESTTAG] - (places) return places", queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();

            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
        public static List<User> GetAllFollowers (int userId)
        {
            try
            {
                var query = new CypherQuery("match (users) - [r:FOLLOWS] -> (user:User {UserId:" + userId + "}) return users", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<User>(query).ToList();
            }
            catch
            {
                return null;
            }
           

        }
        public static List<User> GetAllFollowing (int userId)
        {
            try
            {
                var query = new CypherQuery("match (user:User {UserId:" + userId + "}) - [r:FOLLOWS] -> (users)  return users", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<User>(query).ToList();
            }
            catch
            {
                return null;
            }
            
        }
        public static List<City> GetAllCountryCities(int countryId)
        {
            try
            {
                var query = new CypherQuery("match (country:Country {CountryId:" + countryId + "}) - [r:HASCITY] -> (cities)  return cities", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<City>(query).ToList();
            }
            catch
            {
                return null;
            }


        }
        public static List<User> GetAllUsersThatVisitedPlace (int placeId)
        {
            try
            {
                var query = new CypherQuery("match (users) - [r:VISITED] -> (place:Place {PlaceId:" + placeId + "}) return users",
                                null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<User>(query).ToList();
            }
            catch
            {
                return null; 
            }
            
        }
        public static List<User> GetUsersThatHaveSameStatus (int userId)
        {
            try
            {
                var query = new CypherQuery("match (user:User {UserId:" + userId + "}) - [r:HASSTATUS] -> (status), (users) - [:HASSTATUS] -> (status) return users",
                                                                 null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<User>(query).ToList();
            }
            catch
            {
                return null;
            }

        }
        //dole imaju dict
        public static List<Place> GetPlaces(int userId, string relationship)        // relationship - VISITED, PLANSTOVISIT
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId", userId);
                //queryDict.Add("relationship", relationship);


                var query = new CypherQuery("match (n)-[r:" + relationship + "]->(a) where n.UserId = {userId} return a",
                                                                queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();
            }
            catch
            {
                return null;
            }
           
        }
        public static List<Recommendation> GetUserRecommendations(int userId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId", userId);

                var query = new CypherQuery("match (n)-[r:RECOMMENDS]->(a) where n.UserId = {userId} return r",
                                                                queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Recommendation>(query).ToList();
            }
            catch
            {
                return null;
            }
            

        }
        public static List<Recommendation> GetPlaceRecommendations(int placeId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("placeId", placeId);

                var query = new CypherQuery("start n=node(*) match (n)-[r:RECOMMENDS]->(a) where a.PlaceId = {placeId} return r",
                                                                queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Recommendation>(query).ToList();
            }
            catch
            {
                return null;
            }
           

        }
        public static List<Place> GetCountryPlaces(int countryId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("countryId", countryId);

                var query = new CypherQuery("match (n)<-[r:HASPLACE]-(a)<-[t:HASCITY]-(c) where c.CountryId = {countryId} return n",
                                                                queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();
            }
            catch
            {
                return null;
            }
           
        }
        public static float CalculatePlaceRating(int placeId)
        {
            List<Recommendation> rl = GetPlaceRecommendations(placeId);

            float rating = 0.0f;
            if (rl.Count > 0)
            {
                foreach (Recommendation r in rl)
                {
                    rating += (float)r.Rating;
                }

                return rating / rl.Count;
            }
            else
                return rating;
        }
        public static float CalculateCountryRating(int countryId)
        {
            List<Place> cp = GetCountryPlaces(countryId);

            float rating = 0.0f;
            if (cp.Count > 0)
            {
                foreach (Place p in cp)
                {
                    rating += p.Rating;
                }

                return rating / cp.Count;
            }
            else
                return rating;
        }

        //------------------------------------ZA REDIS-----------------------------------------------------------------

        public static List<Country> GetTopNRatedCountries(int n)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("n", n);

                var query = new CypherQuery("match (n:Country) return n order by n.OverallRating desc limit {n}",
                                                            queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Country>(query).ToList();
            }
            catch
            {
                return null;
            }
            
        }
        public static List<Country> GetTopNVisitedCountries(int n)
        {

            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("n", n);

                var query = new CypherQuery("match (n:Country)-[hs:HASCITY]->(c:City)-[hp:HASPLACE]->(p:Place)<-[v:VISITED]-() with n, count(v) as x return n order by x desc limit {n}",
                                                           queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Country>(query).ToList();
            }
            catch
            {
                return null;
            }
           
        }
        public static List<Place> GetTopNRatedPlaces(int n)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("n", n);

                var query = new CypherQuery("match (p:Place) return p order by p.Rating desc limit {n}",
                                                            queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();
            }   
            catch
            {
                return null;
            }
        }
        public static List<Place> GetTopNVisitedPlaces(int n)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("n", n);

                var query = new CypherQuery("match (p:Place)<-[v:VISITED]-() with p, count(v) as x return p order by x desc limit {n}",
                                                         queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();
            }
            catch
            {
                return null;
            }
         
        }
        public static List<Place> GetTopNRatedPlacesByCountry (int n, int countryId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("countryId", countryId);
                queryDict.Add("n", n);

                var query = new CypherQuery("match (n:Country) - [hs:HASCITY] -> (c:City) - [hp:HASPLACE] -> (p:Place) where n.CountryId = {countryId} return p order by p.Rating desc limit {n}", queryDict, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();

            }
            catch
            {
                return null;
            }
        }
        public static List<Place> GetTopNVisitedPlacesByCountry (int n, int countryId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("countryId", countryId);
                queryDict.Add("n", n);

                var query = new CypherQuery("match (n:Country {CountryId: {countryId} }) - [hs:HASCITY] -> (c:City) - [hp:HASPLACE] -> (p:Place) <- [v:VISITED] - () with p, count(v) as x return p order by x desc limit {n}", queryDict, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();

            }
            catch
            {
                return null;
            }


        }

        //-------------------------------------PRIBAVLJANJE POJEDINACNIH ENTITETA------------------------------------------------

        public static User GetUser(string username)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("username", username);

                var query = new CypherQuery("match (user:User {Username:{username} }) return user", queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<User>(query).First();
            }
            catch
            {
                return null;
            }
       
        }
        public static Place GetPlaceByName (string placeName)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("placeName", placeName);

                var query = new CypherQuery("match (place:Place {Name:{placeName}}) return place", queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).First();
            }
            catch
            {
                return null;
            }
        }
        public static Country GetCountryByName (string countryName)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("countryName", countryName);

                var query = new CypherQuery("match (country:Country {Name:{countryName}}) return country", queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Country>(query).First();
            }
            catch
            {
                return null;
            }
        }
        public static City GetCityByName (string cityName)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("cityName", cityName);

                var query = new CypherQuery("match (c:City {Name:{cityName}}) return c", queryDict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<City>(query).First();
            }
            catch
            {
                return null;
            }
        }
        public static T GetNode<T>(int nodeId, string nodeName)
        {
            try
            {
                var query = new CypherQuery("match (node:" + nodeName + " {" + nodeName + "Id:" + nodeId + "}) return node", null, CypherResultMode.Set);


                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<T>(query).FirstOrDefault();
            }
            catch
            {
                return default(T);
            }

       }
        /*-----------------------------------------------------------------------------------------------*/

        public static List<InterestTag> GetInterestsOfUser(int userId)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("userId", userId);

                var query = new CypherQuery("match (user:User {UserId:{userId}}) - [r:HASINTEREST] -> (tag) return tag", dict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<InterestTag>(query).ToList();
            }
            catch
            {
                return null;
            }
        }
        public static List<string> GetInterestsOfUserToStringArray(int userId)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("userId", userId);

                var query = new CypherQuery("match (user:User {UserId:{userId}}) - [r:HASINTEREST] -> (tag) return tag.Name", dict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<string>(query).ToList();
            }
            catch
            {
                return null;
            }
        }
        public static City GetPlaceLocation  (int placeId)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("placeId", placeId);

                var query = new CypherQuery("match (city:City) - [hp:HASPLACE] -> (place:Place {PlaceId:{placeId}}) return city", dict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<City>(query).First();
            }
            catch
            {
                return null;
            }
        }
        public static List<InterestTag> GetInterestTagsOfPlace(int placeId)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("placeId", placeId);

                var query = new CypherQuery("match (place:Place {PlaceId:{placeId}}) - [r:HASINTERESTTAG] -> (tag) return tag", dict, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<InterestTag>(query).ToList();
            }
            catch
            {
                return null;
            }
        }
        public static List<string> GetEntityByFirstLetter (string firstLetter, string entity, string attributeName)
        {
            try
            {
                //Dictionary<string, object> queryDict = new Dictionary<string, object>();
                //queryDict.Add("search", firstLetter);

                var query = new CypherQuery("match (n:"+entity+") where n."+attributeName+" =~ '"+firstLetter+".*' return n."+attributeName+"", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<string>(query).ToList();
            }
            catch 
            {    
                return null;
            }

        }

        public static bool HasRelationshipWithaPlace(int userId, int placeId, string relName)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("userId", userId);
                queryDict.Add("placeId", placeId);
                queryDict.Add("relationship", relName);
                //var query = new CypherQuery("match (user:User {UserId:{userId} }) - [r:"+relName+"] - > (place:Place {PlaceId:{placeId} }) return EXISTS(r)", queryDict, CypherResultMode.Set);

                var query = new CypherQuery("match (user:User {UserId:{userId} }), (place:Place {PlaceId:{placeId} }) return EXISTS( (user) - [:" + relName + "] - > (place) )", queryDict, CypherResultMode.Set);
                return  ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<bool>(query).FirstOrDefault();

                //if (tester == null)
                //    return false;
                //return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static Country GetCitysCountry (int cityId)
        {
            try
            {
                var query = new CypherQuery("match (country:Country) - [h:HASCITY] -> (city:City {CityId:" + cityId + "}) return country", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Country>(query).FirstOrDefault();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
        public static List<Place> GetPlacesOfCity(int cityId)
        {
            try
            {
                var query = new CypherQuery("match (city:City {CityId:"+cityId+"}) - [h:HASPLACE] -> (place) return place", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();

            }
            catch
            {
                return null;
            }
        }
        public static int GetCountryId (int placeId)
        {
            try
            {
                var query = new CypherQuery("match (country) - [h:HASCITY] -> (city) - [:HASPLACE] -> (place:Place {PlaceId:"+placeId+"}) return country.CountryId", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<int>(query).FirstOrDefault() ;
            }
            catch
            {
                return 0;
            }
        }
        public static List<User> GetAllFollowingThatAreCurrentlyAtTheSamePlace (int userId)
        {
            try
            {
                var query = new CypherQuery("match (user:User {UserId:" + userId + "}) - [f:FOLLOWS] -> (users), (user) - [:CURRENTLYAT] -> (place), (users) - [:CURRENTLYAT] -> (place) return users", null, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<User>(query).ToList();

            }
            catch
            {
                return null;
            }
        }
        public static List<User> GetFollowingThatAreCurrentlyAtTheSameCity (int userId)
        {
            try
            {                
                var query = new CypherQuery("match(user: User { UserId:"+userId+"}) - [f: FOLLOWS]-> (users), (user) - [:CURRENTLYAT]-> (place) < - [:HASPLACE] - (city), (city) - [:HASPLACE]-> (places), (users) - [:CURRENTLYAT]-> (places) return users", null, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<User>(query).ToList();

            }
            catch
            {
                return null;
            }

        }
        public static Place GetCurrentLocation (int userId)
        {
            try
            {
                var query = new CypherQuery("match (user: User { UserId:" + userId + "}) - [:CURRENTLYAT] -> (place) return place", null, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).FirstOrDefault();

            }
            catch
            {
                return null;
            }
        }    
        public static List<string> GetAllTags ()
        {
            try
            {
                var query = new CypherQuery("match (n:InterestTag) return n.Name", null, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<string>(query).ToList();

            }
            catch
            {
                return null;
            }
        }

        public static List<Recommendation> GetPlaceRecommendationsByTime(int placeId, bool ascending)
        {
            try
            {
                string orderKW = ascending ? string.Empty : "desc";
                var query = new CypherQuery("match (n)-[r:RECOMMENDS]->(a) where a.PlaceId = "+ placeId +" return r order by r.RecommendationTime "+orderKW+" ",
                                                                null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Recommendation>(query).ToList();
            }
            catch
            {
                return null;
            }
        }

        public static List<DomainModel.Message> GetAllMessagesSentOrReceivedByUser(int userId, string relName)//relName = "SENT" || "RECEIVED"
        {
            try
            {
                var query = new CypherQuery("match (n:User {UserId:" + userId + "}) - [r:" + relName + "] -> (m:Message) return m", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<DomainModel.Message>(query).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

// ------------------------------------------- ADMINISTRATOR ----------------------------------------------------------------------
        public static List<InterestTag> GetAllTags2()
        {
            try
            {
                var query = new CypherQuery("match (n:InterestTag) return n", null, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<InterestTag>(query).ToList();

            }
            catch
            {
                return null;
            }
        }
        public static List<Place> GetAllPlaces()
        {
            try
            {
                var query = new CypherQuery("match (n:Place) return n", null, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();

            }
            catch
            {
                return null;
            }
        }
        public static List<City> GetAllCities()
        {
            try
            {
                var query = new CypherQuery("match (n:City) return n", null, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<City>(query).ToList();

            }
            catch
            {
                return null;
            }
        }
        public static List<Country> GetAllCountries()
        {
            try
            {
                var query = new CypherQuery("match (n:Country) return n", null, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Country>(query).ToList();

            }
            catch
            {
                return null;
            }
        }

    }
    
}
