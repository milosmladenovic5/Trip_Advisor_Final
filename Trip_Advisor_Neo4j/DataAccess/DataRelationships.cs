using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trip_Advisor_Neo4j.DomainModel;

namespace Trip_Advisor_Neo4j.DataAccess
{
    public static class DataRelationships
    {
        public static bool Follow (int followerId, int followingId)
        {
            try
            {
                var query = new CypherQuery("MATCH (n:User),(m:User) WHERE n.UserId = " + followerId + " AND  m.UserId = " + followingId + " CREATE (n) - [r:FOLLOWS {Name: n.UserId + m.UserId}] -> (m)",
                    null, CypherResultMode.Set);


                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                return false;
            }

        }
        public static bool Recommend(int recommenderId, int placeId, string comment, int rating)
        {
            try
            {
                DateTime date = DateTime.Now;
                long n = long.Parse(date.ToString("yyyyMMddHHmmss"));

                var query = new CypherQuery("MATCH (user:User {UserId:" + recommenderId + "} ), (place:Place {PlaceId:" + placeId + "}) CREATE (user) - [r:RECOMMENDS {RecommendationId:"+DataProviderGet.GenerateId("Recommendation")+", UserId:"+recommenderId+", Comment:'" + comment + "' , Rating:" + rating + ", RecommendationTime:" + n + "}] -> (place)",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }

        }
        public static bool HasCity(int countryId, int cityId)
        {
            try
            {
                var query = new CypherQuery("MATCH (country:Country {CountryId:" + countryId + "}), (city:City {CityId:" + cityId + "}) CREATE (country) - [r:HASCITY] -> (city)", null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }
        public static bool HasPlace(int cityId, int placeId)
        {
            try
            {
                var query = new CypherQuery("MATCH (city:City {CityId:" + cityId + "}), (place:Place {PlaceId:" + placeId + "}) CREATE (city) - [r:HASPLACE] -> (place)",
                        null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }
        public static bool HasInterest( int userId, string interestTagName)             // odnosi se na korisnika, koga interesuje neka oblast
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (interest:InterestTag {Name:'" + interestTagName + "'}) CREATE (user) - [r:HASINTEREST] -> (interest)", null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }

        }
        public static bool HasInterestTag (int placeId, string interestTagName)         //odnosi se na mesto koje sadrzi tag
        {
            try
            {

                var query = new CypherQuery("MATCH (place:Place {PlaceId:" + placeId + "}), (interest:InterestTag {Name:'" + interestTagName + "'}) CREATE (place) - [r:HASINTERESTTAG] -> (interest)", null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }
        public static bool HasStatus (int userId, string statusName)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (status:Status {StatusName:'" + statusName + "'}) CREATE (user) - [r:HASSTATUS] -> (status)", null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }

        }
        public static bool VisitedPlace(int userId, int placeId, DateTime dateOfVisit)
        {
            try
            {
                long n = long.Parse(dateOfVisit.ToString("yyyyMMddHHmmss"));

                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (place:Place {PlaceId:" + placeId + "}) CREATE (user) - [r:VISITED {Date:'" + n + "'}] -> (place)",
                        null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }
        public static bool PlansToVisit (int userId, int placeId)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (place:Place {PlaceId:" + placeId + "}) CREATE (user) - [r:PLANSTOVISIT] -> (place)",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }

        }
        public static bool CurrentlyAt (int userId, int cityId)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (city:City {CityId:" + cityId + "}) CREATE (user) - [r:CURRENTLYAT] -> (city)",
                       null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }

        }


        public static bool SendMessage(int senderId, int receiverId, int messageId)
        {
            try
            {
                var query = new CypherQuery("MATCH (sender:User {UserId:" + senderId + "}), (receiver:User {UserId:" + receiverId + "}), (message:Message {MessageId:" + messageId + "}) CREATE (sender) - [r:SENT] -> (message) <- [h:RECEIVED] - (receiver)", 
                        null, CypherResultMode.Set);
         
                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Sending exception: " + ex.Message);
                return false;
            }
        }

        public static bool SendMessageToUser(string senderUsername, string receiverUsername, int messageId)
        {
            try
            {
                var query = new CypherQuery("MATCH (sender:User {Username:'" + senderUsername + "'}), (receiver:User {Username:'" + receiverUsername + "'}), (message:Message {MessageId:" + messageId + "}) CREATE (sender) - [r:SENT] -> (message) <- [h:RECEIVED] - (receiver)",
                        null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Sending exception: " + ex.Message);
                return false;
            }
        }




    }

}
