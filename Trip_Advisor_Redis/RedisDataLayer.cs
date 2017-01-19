using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Neo4j.DomainModel;
using ServiceStack.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;
//quartz
//signaler


// kada god neki korisnik oceni neko mesto globalni brojac redisa (x) ce se inkrementirati
// kada brojac dostigne zadati broj x (npr. 50) memoriski kes redisa ce se osveziti 
// posto korisnik mora posetiti neko mesto da bi ga oceni isto vazi i za posecena mesta


// kada apdejtovati rating zemlje DataProviderUpdate.UpdateCountryRating(int countryId)?
// u redis kesu ce se cuvati parovi id - zemlje i drugi paramter brojac koji se inkrementira kad god se rejting nekog
// njenog mesta promeni brojac se inkrementira i kada dostigne zadatu vrednost x(npr. 5) poziva se gore pomenuta funkcija
// countryId ce prestavljati hask za pristup

// osvezavanje kesa top zemalalja se vrsi  po istom principu kao i za mesta

namespace Trip_Advisor_Redis
{
    public static class RedisDataLayer
    {

        public static readonly RedisClient redis = new RedisClient(Config.SingleHost);

        private static readonly string hashCountriesByRating = "topCountriesByRating";
        private static readonly string hashPlacesByRating = "topPlacesByRating12";
        private static readonly string hashAllCountries = "allWorldCountries";

        private static readonly string hashCountriesByVisitors = "topCountriesByVisitors";
        private static readonly string hashPlacesByVisitors = "topPlacesByVisitors1";

        private static readonly string hashPlaceCounter = "placeRating";
        //private static readonly string hashCountryCounter = "countryCounter";



        public static void InitializeCounters()
        {
            if (!CheckNextUrlGlobalCounterExists(hashPlaceCounter))
            {
                var redisPlaceCounterSetup = redis.As<long>();
                //redisCounterSetup.SetEntry(hashPlaceRatingCounter, 0);
                redisPlaceCounterSetup.SetValue(hashPlaceCounter, 0);
            }

            //if (!CheckNextUrlGlobalCounterExists(hashCountryCounter))
            //{
            //    var redisPlaceCounterSetup = redis.As<long>();
            //    redisPlaceCounterSetup.SetValue(hashCountryCounter, 0);
            //}
        }



        public static bool CheckNextUrlGlobalCounterExists(string hash)
        {
            var test = redis.Get<object>(hash);
            return (test != null) ? true : false;
        }

        public static void SaveTopPlaces()
        {
            //redis.DeleteById<List<Place>>(hashPlacesByRating);
            //redis.DeleteById<List<Place>>(hashPlacesByVisitors);

            redis.RemoveAllFromList(hashPlacesByRating);
            redis.RemoveAllFromList(hashPlacesByVisitors);

          

            List<Place> topRatedPlaces = DataProviderGet.GetTopNRatedPlaces(10);
            //List < string > l = new List<string>();
            //for (int i = 0; i < topRatedPlaces.Count; i++)
            //    l.Add(topRatedPlaces[i].Name);
            //redis.PushItemToList(hashPlacesByRating, JsonSerializer.SerializeToString<List<string>>(l));
            redis.PushItemToList(hashPlacesByRating, JsonSerializer.SerializeToString<List<Place>>(topRatedPlaces));
            

            List<Place> topVisitedPlaces = DataProviderGet.GetTopNVisitedPlaces(10);
            redis.PushItemToList(hashPlacesByVisitors, JsonSerializer.SerializeToString<List<Place>>(topVisitedPlaces));
            
            
        }

        public static void SaveTopCountries()
        {
            redis.RemoveAllFromList(hashCountriesByRating);
            redis.RemoveAllFromList(hashCountriesByVisitors);

            List<Country> topVisitedCountries = DataProviderGet.GetTopNVisitedCountries(10);
            redis.PushItemToList(hashCountriesByVisitors, JsonSerializer.SerializeToString<List<Country>>(topVisitedCountries));

            List<Country> topRatedCountries = DataProviderGet.GetTopNRatedCountries(10);
            redis.PushItemToList(hashCountriesByRating, JsonSerializer.SerializeToString<List<Country>>(topRatedCountries));

        }

        public static List<Country> GetTopCountriesByRating()
        {
            string stringOfCountries = redis.GetRangeFromList(hashCountriesByRating, 0, 0).First();
  
            return (List<Country>)JsonSerializer.DeserializeFromString(stringOfCountries, typeof(List<Country>));
        }

        public static List<Country> GetTopCountriesByVisitors()
        {
            string stringOfCountries = redis.GetRangeFromList(hashCountriesByVisitors, 0, 0).First();

            return (List<Country>)JsonSerializer.DeserializeFromString(stringOfCountries, typeof(List<Country>));
        }

        public static List<Place> GetTopPlacesByRating()
        {
            string stringOfPlaces = redis.GetRangeFromList(hashPlacesByRating, 0, 0).First();

            return (List<Place>)JsonSerializer.DeserializeFromString(stringOfPlaces, typeof(List<Place>));
        }

        public static List<Place> GetTopPlacesByVisitors()
        {
            string stringOfPlaces = redis.GetRangeFromList(hashPlacesByVisitors, 0, 0).First();

            return (List<Place>)JsonSerializer.DeserializeFromString(stringOfPlaces, typeof(List<Place>));
        }

        public static void SaveAllCountries()
        {
            List<Country> countriesByRating = DataProviderGet.GetTopNRatedCountries(60);
            redis.PushItemToList(hashAllCountries, JsonSerializer.SerializeToString<List<Country>>(countriesByRating));

        }          // NIJE ISKORISCENO I NAJVEROVATNIJE I NECE !

       //------------------------------------------------------------------------------------------------------------

        public static void RefreshPlaceCache()
        {
            long placeRatingChangeCounter = redis.Incr(hashPlaceCounter);
            //MessageBox.Show(nextCounterKey.ToString());
            if(placeRatingChangeCounter == 15)                          // kada 15 mesta promeni rejting kes se osvezava
            {
                var redisPlaceCounterSetup = redis.As<long>();
                redisPlaceCounterSetup.SetValue(hashPlaceCounter, 0);

               // redis.Del()
                SaveTopPlaces(); 
            }
        }
     
        public static void UpdateCountryRating(int countryId)
        {
            string hash = "CountryId: " + countryId;
            long countryRatingUpdaterCounter = redis.Incr(hash);
            if(countryRatingUpdaterCounter == 5)                       // broj mesta kojima je apdejtovan rejting 
            {                                                          // zemlja mora imati barem 5 turistickih mesta kako bi dobila privilegiju da bude dodata u nas sistem (tbh. broj promeniti po potrebi ili parametrizovati)
                DataProviderUpdate.UpdateCountryRating(countryId);     // apdejtujemo rejting zemlje u glavnoj bazi
                SaveTopCountries();                                    // osvezavamo kes (osvezava i topVisited - korisnik mora prvo da poseti neku zemlju da bi je ocenio
                var redisPlaceCounterSetup = redis.As<long>();         
                redisPlaceCounterSetup.SetValue(hashPlaceCounter, 0);   // resetovanje brojaca
            }
        }
    }
}
