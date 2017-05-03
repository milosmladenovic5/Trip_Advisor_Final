using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip_Advisor_Neo4j.DomainModel;
using Trip_Advisor_Neo4j.DataAccess;
using Neo4jClient;
using Neo4jClient.Cypher;
using System.Windows.Forms;

namespace Trip_Advisor_Neo4j.DataAccess
{
    public static class DataProviderCreate
    {
        public static bool CreateIdNodes()
        {
            try
            {
                string[] id_nodes = new string[] { "UserId", "CountryId", "InterestTagId", "CityId", "PlaceId", "StatusId", "RecommendationId", "MessageId" };

                for (int i = 0; i < id_nodes.Length; i++)
                {
                    var query = new CypherQuery("CREATE (n:" + id_nodes[i] + "{Id:" + 0 + "})",
                       null, CypherResultMode.Set);

                    ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CreateCity(City city)
        {
            try
            {
                int generatedId = DataProviderGet.GenerateId("City");

                //var query = new CypherQuery("CREATE (n:City {CityId:" + generatedId + ", Name:'" + city.Name + "', CenterLatitude:"+city.CenterLatitude+", CenterLongitude:"+city.CenterLongitude+"})",
                //    null, CypherResultMode.Set);

                //((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                city.CityId = generatedId;
                DataLayer.Client.Cypher
                .Create("(city:City {newCity})")
                .WithParam("newCity", city)
                .ExecuteWithoutResults();

                return true;
            }
            catch 
            {
                return false;
            }
        }
        public static bool CreateCountry(Country country)
        {
            try
            {
                int generatedId = DataProviderGet.GenerateId("Country");

                //var query = new CypherQuery("CREATE (n:Country {CountryId:" + generatedId + ", Name:'" + country.Name + "' , OverallRating:"+country.OverallRating+ ", PromotionalVideoURL:'" + country.PromotionalVideoURL + "' , NationalFlag:'" + country.NationalFlag + "'})",
                //    null, CypherResultMode.Set);

                //((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                country.CountryId = generatedId;
                DataLayer.Client.Cypher
                .Create("(country:Country {newCountry})")
                .WithParam("newCountry", country)
                .ExecuteWithoutResults();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CreateInterestTag(InterestTag interestTag)
        {
            try
            {
                int generatedId = DataProviderGet.GenerateId("InterestTag");

                //var query = new CypherQuery("CREATE (n:InterestTag {InterestTagId:" + generatedId + ", Name:'" + interestTag.Name + "' , FieldOfLife:'" + interestTag.FieldOfLife + "', Type:'" + interestTag.Type +"'})",
                //    null, CypherResultMode.Set);

                //((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                interestTag.InterestTagId = generatedId;
                DataLayer.Client.Cypher
                .Create("(interestTag:InterestTag {newInterestTag})")
                .WithParam("newInterestTag", interestTag)
                .ExecuteWithoutResults();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool CreatePlace(Place place)
        {
            try
            {
                int generatedId = DataProviderGet.GenerateId("Place");
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", generatedId);
                queryDict.Add("name", place.Name);
                queryDict.Add("type", place.Type);
                queryDict.Add("ccd", place.CityCenterDistance);
                queryDict.Add("desc", place.Description);
                queryDict.Add("rating", place.Rating);
                queryDict.Add("lat", place.Latitude);
                queryDict.Add("long", place.Longitude);

                var query = new CypherQuery("CREATE (n:Place {PlaceId: {id}, Name: {name}, Type: {type} , CityCenterDistance: {ccd} , Description: {desc} , Rating: {rating}, Pictures:[], Longitude: {long}, Latitude: {lat} })",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                //place.PlaceId = generatedId;
                //DataLayer.Client.Cypher
                //.Create("(place:Place {newPlace})")
                //.WithParam("newPlace", place)
                //.ExecuteWithoutResults();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool CreateStatus(Status status)
        {
            try
            {
                int generatedId = DataProviderGet.GenerateId("Status");

                var query = new CypherQuery("CREATE (n:Status {StatusId:" + generatedId + ", StatusName:'" + status.StatusName + "', Description:'" + status.Description + "'})",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch 
            {
                return false;
            }
        }
        public static bool CreateUser(User user)
        {
            try
            {
                int generatedId = DataProviderGet.GenerateId("User");

                DateTime date = DateTime.Now;
                long n = long.Parse(date.ToString("yyyyMMddHHmmss"));

                //var query = new CypherQuery("CREATE (n:User {UserId:"+generatedId+", Username:'"+ user.Username+"', Password:'"+user.Password+"', Email:'"+user.Email+"', ProfilePicture:'"+user.ProfilePicture+ "', DateJoined:'" + n + "', UserStatusFLAG:" +user.UserStatusFLAG+ ", Description: 'No description set.'})",
                //                                               null,  CypherResultMode.Set);

                //((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                user.UserId = generatedId;
                user.DateJoined = n;
                DataLayer.Client.Cypher
                .Create("(user:User {newUser})")
                .WithParam("newUser", user)
                .ExecuteWithoutResults();

                return true;
            }
            catch(Exception e) 
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public static int CreateMessage(string text, string subject)
        {
            try
            {
                int generatedId = DataProviderGet.GenerateId("Message");

                DateTime date = DateTime.Now;
                long n = long.Parse(date.ToString("yyyyMMddHHmmss"));

                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("id", generatedId);
                queryDict.Add("text", text);
                queryDict.Add("sub", subject);
                queryDict.Add("n", n);

                var query = new CypherQuery("CREATE (m:Message {MessageId: {id}, Text: {text} , Subject: {sub}, SendingDate: {n} })", queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return generatedId;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return 0;
            }
        }


    }
}
