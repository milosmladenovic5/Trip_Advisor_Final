using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip_Advisor_Neo4j.DomainModel;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace Trip_Advisor_Neo4j
{
    public static class DataLayer
    {
        public static GraphClient Client { get; set; }

        public static void Connect()
        {
            Client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "newbase");
            try
            {
                Client.Connect();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }


    }
}
