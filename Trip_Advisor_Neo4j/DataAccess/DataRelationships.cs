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
            catch
            {
                return false;
            }

        }

        public static void Recommend(int recommenderId, int placeId, string comment, int rating)
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + recommenderId + "} ), (place:Place {PlaceId:" + placeId + "}) CREATE (user) - [r:RECOMMENDS {RecommendationId:"+DataProviderGet.GenerateId("Recommendation")+", UserId:"+recommenderId+", Comment:'" + comment + "' , Rating:" + rating + ", RecommendationTime:'" + DateTime.Now.ToString() + "'}] -> (place)",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public static void HasCity(int countryId, int cityId)
        {
            var query = new CypherQuery("MATCH (country:Country {CountryId:" + countryId + "}), (city:City {CityId:" + cityId + "}) CREATE (country) - [r:HASCITY] -> (city)", null, CypherResultMode.Set);

            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
        }

        public static void HasPlace(int cityId, int placeId)
        {
            var query = new CypherQuery("MATCH (city:City {CityId:" + cityId + "}), (place:Place {PlaceId:" + placeId + "}) CREATE (city) - [r:HASPLACE] -> (place)",
                null, CypherResultMode.Set);

            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
        }
        
        public static void HasInterest( int userId, string interestTagName)             // odnosi se na korisnika, koga interesuje neka oblast
        {
            try
            {
                var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (interest:InterestTag {Name:'" + interestTagName + "'}) CREATE (user) - [r:HASINTEREST] -> (interest)", null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public static void HasInterestTag (int placeId, string interestTagName)         //odnosi se na mesto koje sadrzi tag
        {
            var query = new CypherQuery("MATCH (place:Place {PlaceId:" + placeId + "}), (interest:InterestTag {Name:'" + interestTagName + "'}) CREATE (place) - [r:HASINTERESTTAG] -> (interest)", null, CypherResultMode.Set);

            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
        }

        public static void HasStatus (int userId, string statusName)
        {
            var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (status:Status {StatusName:'" + statusName + "'}) CREATE (user) - [r:HASSTATUS] -> (status)", null, CypherResultMode.Set);

            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

        }

        public static void BelongsToCountry (int cityId, int countryId)
        {
            var query = new CypherQuery("MATCH (city:City {CityId:" + cityId + "}), (country:Country {CountryId:" + countryId + "}) CREATE (city) - [r:BELONGSTOCOUNTRY] -> (country)", null, CypherResultMode.Set);

            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
        }

        public static void BelongsToCity(int placeId, int cityId)
        {
            var query = new CypherQuery("MATCH (place:Place {PlaceId:" + placeId + "}), (city:City {CityId:" + cityId + "}) CREATE (place) - [r:BELONGSTOCITY] -> (city)", null, CypherResultMode.Set);

            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
        }


        public static void VisitedPlace(int userId, int placeId, DateTime dateOfVisit)
        {
            var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (place:Place {PlaceId:" + placeId + "}) CREATE (user) - [r:VISITED {Date:'"+dateOfVisit.ToShortDateString()+"'}] -> (place)",
                null, CypherResultMode.Set);
       
            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);
        }

        public static void PlansToVisit (int userId, int placeId)
        {
            var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (place:Place {PlaceId:" + placeId + "}) CREATE (user) - [r:PLANSTOVISIT] -> (place)",
                null, CypherResultMode.Set);

            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

        }

        public static void CurrentlyAtPlace (int userId, int placeId)
        {
            var query = new CypherQuery("MATCH (user:User {UserId:" + userId + "}), (place:Place {PlaceId:" + placeId + "}) CREATE (user) - [r:CURRENTLYAT] -> (place)",
               null, CypherResultMode.Set);

            ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

        }

      
    }

}
