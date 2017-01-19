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
                string[] id_nodes = new string[] { "UserId", "CountryId", "InterestTagId", "CityId", "PlaceId", "StatusId", "RecommendationId" };

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
                int generatedId = Int32.Parse(DataProviderGet.GenerateId("City"));

                var query = new CypherQuery("CREATE (n:City {CityId:" + generatedId + ", Name:'" + city.Name + "'})",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

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
                int generatedId = Int32.Parse(DataProviderGet.GenerateId("Country"));

                var query = new CypherQuery("CREATE (n:Country {CountryId:" + generatedId + ", Name:'" + country.Name + "' , OverallRating:"+country.OverallRating+"})",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

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
                int generatedId = /*Int32.Parse(DataProviderGet.GenerateId("InterestTag")); */ 1;   // NEMAS OVAJ U BAZI A MRZI ME DA GLEDAM KAKO SE DODAJE

                var query = new CypherQuery("CREATE (n:InterestTag {InterestTagId:" + generatedId + ", Name:'" + interestTag.Name + "' , FieldOfLife:'" + interestTag.FieldOfLife + "', Type:'" + interestTag.Type +"'})",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

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
                int generatedId = Int32.Parse(DataProviderGet.GenerateId("Place"));

                var query = new CypherQuery("CREATE (n:Place {PlaceId:" + generatedId + ", Name:'" + place.Name + "', Type:'" + place.Type + "' , Description:'" + place.Description + "' , Rating:" + place.Rating + ", Pictures:[]})",
                    null, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch 
            {
                return false;
            }
        }
        public static bool CreateStatus(Status status)
        {
            try
            {
                int generatedId = Int32.Parse(DataProviderGet.GenerateId("Status"));

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
                int generatedId = Int32.Parse(DataProviderGet.GenerateId("User"));

                var query = new CypherQuery("CREATE (n:User {UserId:"+generatedId+", Username:'"+ user.Username+"', Password:'"+user.Password+"', Email:'"+user.Email+"', ProfilePicture:'"+user.ProfilePicture+"'})",
                                                               null,  CypherResultMode.Set);

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
