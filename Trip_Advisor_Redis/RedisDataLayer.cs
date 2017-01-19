﻿using System;
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

        //public static System.Timers.Timer timer;            //TopVisitedCountries resenje No.1

        private static readonly string hashCountriesByRating = "topCountriesByRating";
        private static readonly string hashPlacesByRating = "topPlacesByRating";
        private static readonly string hashAllCountries = "allWorldCountries";

        private static readonly string hashCountriesByVisitors = "topCountriesByVisitors";
        private static readonly string hashPlacesByVisitors = "topPlacesByVisitors1";

        private static readonly string hashPlaceRCounter = "placeRating";
        private static readonly string hashPlaceGlobalVCounter = "placeVisitors";
        private static readonly string hashCountryGlobalVCounter = "countryVisitorCounter";     //TopVisitedCountries resenje No.2
                                                                                                //inkrementira se u isto vreme kao i 
                                                                                                //place visitor counter, samo ima 3x vecu vrednost(npr.)



        public static void InitializeCounters()
        {
            if (!CheckNextUrlGlobalCounterExists(hashPlaceRCounter))
            {
                var redisPlaceCounterSetup = redis.As<long>();
                //redisCounterSetup.SetEntry(hashPlaceRatingCounter, 0);
                redisPlaceCounterSetup.SetValue(hashPlaceRCounter, 0);
            }

            if (!CheckNextUrlGlobalCounterExists(hashPlaceGlobalVCounter))
            {
                var redisPlaceCounterSetup = redis.As<long>();
                redisPlaceCounterSetup.SetValue(hashPlaceGlobalVCounter, 0);
            }

            if (!CheckNextUrlGlobalCounterExists(hashCountryGlobalVCounter))
            {
                var redisPlaceCounterSetup = redis.As<long>();
                redisPlaceCounterSetup.SetValue(hashCountryGlobalVCounter, 0);
            }

            //timer = new System.Timers.Timer();
            //timer.Elapsed += Timer_Elapsed;
            //timer.Interval = 60000;
            //timer.Enabled = true;

        }

        //private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    timer.Stop();
        //    SaveTopVisitedCountries();
        //    timer.Start();
           
        //}

        public static bool CheckNextUrlGlobalCounterExists(string hash)
        {
            var test = redis.Get<object>(hash);
            return (test != null) ? true : false;
        }

        public static void SaveTopRatedPlaces()
        {
            redis.RemoveAllFromList(hashPlacesByRating);
            List<Place> topRatedPlaces = DataProviderGet.GetTopNRatedPlaces(10);
            redis.PushItemToList(hashPlacesByRating, JsonSerializer.SerializeToString<List<Place>>(topRatedPlaces));    
        }

        public static void SaveTopVisitedPlaces()
        {
            redis.RemoveAllFromList(hashPlacesByVisitors);
            List<Place> topVisitedPlaces = DataProviderGet.GetTopNVisitedPlaces(10);
            redis.PushItemToList(hashPlacesByVisitors, JsonSerializer.SerializeToString<List<Place>>(topVisitedPlaces));
        }

        public static void SaveTopRatedCountries()
        {
            redis.RemoveAllFromList(hashCountriesByRating);
            List<Country> topRatedCountries = DataProviderGet.GetTopNRatedCountries(10);
            redis.PushItemToList(hashCountriesByRating, JsonSerializer.SerializeToString<List<Country>>(topRatedCountries));

        }

        public static void SaveTopVisitedCountries()
        {
            redis.RemoveAllFromList(hashCountriesByVisitors);
            List<Country> topVisitedCountries = DataProviderGet.GetTopNVisitedCountries(10);
            redis.PushItemToList(hashCountriesByVisitors, JsonSerializer.SerializeToString<List<Country>>(topVisitedCountries));
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

        public static void RefreshPlaceRCache()
        {
            long placeRatingChangeCounter = redis.Incr(hashPlaceRCounter);
            //MessageBox.Show(nextCounterKey.ToString());
            if(placeRatingChangeCounter == 15)                          // kada 15 mesta promeni rejting kes se osvezava
            {
                var redisPlaceCounterSetup = redis.As<long>();
                redisPlaceCounterSetup.SetValue(hashPlaceRCounter, 0);

               // redis.Del()
                SaveTopRatedPlaces(); 
            }
        }

        public static void RefreshPlaceVCache()
        {
            long placeGlobalVisitorCounter = redis.Incr(hashPlaceGlobalVCounter);
    
            if (placeGlobalVisitorCounter == 30)                          
            {
                var redisPlaceCounterSetup = redis.As<long>();
                redisPlaceCounterSetup.SetValue(hashPlaceGlobalVCounter, 0);

       
                SaveTopVisitedPlaces();
            }

            long countryGlobalVisitorCounter = redis.Incr(hashCountryGlobalVCounter);

            if (countryGlobalVisitorCounter == 100)
            {
                var redisPlaceCounterSetup = redis.As<long>();
                redisPlaceCounterSetup.SetValue(hashCountryGlobalVCounter, 0);


                SaveTopVisitedCountries();
            }

        }

        public static void UpdateCountryRating(int countryId)
        {
            string hash = "CountryId: " + countryId;
            long countryRatingUpdaterCounter = redis.Incr(hash);
            if(countryRatingUpdaterCounter == 5)                       // broj mesta kojima je apdejtovan rejting 
            {                                                          // zemlja mora imati barem 5 turistickih mesta kako bi dobila privilegiju da bude dodata u nas sistem (tbh. broj promeniti po potrebi ili parametrizovati)
                DataProviderUpdate.UpdateCountryRating(countryId);     // apdejtujemo rejting zemlje u glavnoj bazi
                SaveTopRatedCountries();                                    // osvezavamo kes (osvezava i topVisited - korisnik mora prvo da poseti neku zemlju da bi je ocenio
                var redisPlaceCounterSetup = redis.As<long>();         
                redisPlaceCounterSetup.SetValue(hash, 0);   // resetovanje brojaca
            }
        }       //i osvezi redis kes
    }
}
