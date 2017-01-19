﻿using Neo4jClient.Cypher;
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
        
        public static string GenerateId(string entityType)
        {
            try
            {
                int mId = Int32.Parse(GetMaxId(entityType));
               // Dictionary<string, object> queryDict = new Dictionary<string, object>();
               // queryDict.Add("Id", mId);
                var createIdQuery = new CypherQuery("match (n:"+entityType+"Id {Id:"+mId+"}) set n.Id = "+(++mId)+"", null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(createIdQuery);

                return (mId).ToString();
            }
            catch
            {
                return null;
            }
           
        }

        public static List<Place> GetSimilarPlacesIds(int userId, int placeId)
        {
            try
            {//ova funkcija je menjana
                var query = new CypherQuery("match (user:User {UserId:" + userId + "}) - [:PLANSTOVISIT] -> (place:Place {PlaceId:" + placeId + "}), (city) - [:HASPLACE] -> (place) - [:HASINTERESTTAG] -> (tag), (city) - [:HASPLACE] -> (places) - [:HASINTERESTTAG] -> (tag) return  places", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();

            }
            catch
            {
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
                var query = new CypherQuery("match (country:Country {CountryId:" + countryId + "}) - [r:HASCITY] -> (cities)  return users", null, CypherResultMode.Set);

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

                var query = new CypherQuery("start n=node(*) match (n)-[r:BELONGSTOCITY]->(a)-[t:BELONGSTOCOUNTRY]->(c) where c.CountryId = {countryId} return n",
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
            foreach(Recommendation r in rl)
            {
                rating += (float)r.Rating;
            }

            return rating / rl.Count;
        }

        public static float CalculateCountryRating(int countryId)
        {
            List<Place> cp = GetCountryPlaces(countryId);

            float rating = 0.0f;
            foreach (Place p in cp)
            {
                rating += p.Rating;
            }

            return rating / cp.Count;
        }

        //------------------------------------REDIS-----------------------------------------------------------------

        public static List<Country> GetTopNRatedCountries(int n)
        {
            try
            {
                var query = new CypherQuery("match (n:Country) return n order by n.OverallRating desc limit " + n + "",
                                                            null, CypherResultMode.Set);

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
                var query = new CypherQuery("match (n:Country)-[hs:HASCITY]->(c:City)-[hp:HASPLACE]->(p:Place)<-[v:VISITED]-() with n, count(v) as x return n order by x desc limit " + n + "",
                                                           null, CypherResultMode.Set);

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
                var query = new CypherQuery("match (p:Place) return p order by p.Rating desc limit " + n + "",
                                                            null, CypherResultMode.Set);

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
                var query = new CypherQuery("match (p:Place)<-[v:VISITED]-() with p, count(v) as x return p order by x desc limit " + n + "",
                                                         null, CypherResultMode.Set);

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

                var query = new CypherQuery("match (n:Country {CountryId:{countryId}}) - [hp:HASCITY] -> (c:City) - [hp:HASPLACE] -> (p:Place) return p order by p.Rating desc limit {n}", queryDict, CypherResultMode.Set);
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

                var query = new CypherQuery("match (n:Country {CountryId:{countryId}}) - [hp:HASCITY] -> (c:City) - [hp:HASPLACE] -> (p:Place) <- [v:VISITED] - () with n, count(v) as x return p order by x desc limit {n}", queryDict, CypherResultMode.Set);
                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<Place>(query).ToList();

            }
            catch
            {
                return null;
            }


        }

        //-------------------------------------PRIBAVLJANJE ENTITETA------------------------------------------------

        public static User GetUser(string username)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("username", username);

                var query = new CypherQuery("match (user:User {Username:{username}}) return user", queryDict, CypherResultMode.Set);

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
                var query = new CypherQuery("match (n:"+entity+") where n."+attributeName+" =~ '"+firstLetter+".*' return n."+attributeName+"", null, CypherResultMode.Set);

                return ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<string>(query).ToList();
            }
            catch (Exception ex)
            {
               
                return null;
            }

        }

        public static bool HasRelationshipWithaPlace(int userId, int placeId, string relName)
        {
            try
            {

                var query = new CypherQuery("match (user:User {UserId:"+userId+"}) - [r:"+relName+"] - > (place:Place {PlaceId:"+placeId+"}) return user", null, CypherResultMode.Set);


                User tester =  ((IRawGraphClient)DataLayer.Client).ExecuteGetCypherResults<User>(query).FirstOrDefault();

                if (tester == null)
                    return false;
                return true;
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

       

    }
    
}
