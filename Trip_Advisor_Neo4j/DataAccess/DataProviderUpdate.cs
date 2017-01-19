﻿using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trip_Advisor_Neo4j.DomainModel;

namespace Trip_Advisor_Neo4j.DataAccess
{
    public class DataProviderUpdate
    {
        public static bool UpdateUser(User user)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("UserId", user.UserId);
                queryDict.Add("Username", user.Username);
                queryDict.Add("Password", user.Password);
                queryDict.Add("Email", user.Email);
                queryDict.Add("ProfilePicture", user.ProfilePicture);

                var query = new CypherQuery("match (n:User {UserId: {UserId} }) set n.Username = {Username}, n.Password = {Password}, n.Email = {Email}, n.ProfilePicture = {ProfilePicture} ",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddPictureOfPlace (string picturePath, int placeId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("placeId", placeId);
                queryDict.Add("picturePath", picturePath);

               
                var query = new CypherQuery("match (n:Place {PlaceId: {placeId} }) set n.Pictures=n.Pictures+[{picturePath}]",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }

        }


        public static bool UpdatePlace(Place place)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("PlaceId", place.PlaceId);
                queryDict.Add("Name", place.Name);
                queryDict.Add("Rating", place.Rating);
                queryDict.Add("Type", place.Type);
                queryDict.Add("Description", place.Description);
                queryDict.Add("CityCenterDistance", place.CityCenterDistance);


                var query = new CypherQuery("match (n:Place {PlaceId: {PlaceId} }) set n.Name = {Name}, n.Type = {Type}, n.Description = {Description}, n.CityCenterDistance = {CityCenterDistance}, n.Rating = {Rating} ",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateCountry(Country country)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("CountryId", country.CountryId);
                queryDict.Add("Name", country.Name);
                queryDict.Add("OverallRating", country.OverallRating);
         


                var query = new CypherQuery("match (n:Country {CountryId: {CountryId} }) set n.Name = {Name}, n.OverallRating = {OverallRating}",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateCity(City city)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("CityId", city.CityId);
                queryDict.Add("Name", city.Name);
              


                var query = new CypherQuery("match (n:City {CityId: {CityId} }) set n.Name = {Name}",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateRecommendation(int userId, int placeId, Recommendation recommendation)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("UserId", userId);
                queryDict.Add("PlaceId", placeId);
                queryDict.Add("Comment", recommendation.Comment);
                queryDict.Add("Rating", recommendation.Rating);
                queryDict.Add("RecommendationTime", recommendation.RecommendationTime);         // i ovo da menjamo?



                var query = new CypherQuery("match (n:User {UserId: {UserId} }) - [r:RECOMMENDS] -> (m:Place {PlaceId: {PlaceId} }) set r.Comment = {Comment}, r.Rating = {Rating}, r.RecommendationTime = {RecommendationTime}",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdatePlaceRating(int placeId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("PlaceId", placeId);
                queryDict.Add("Rating", DataProviderGet.CalculatePlaceRating(placeId));     // ovo mozda moze optimalnije
                // funkcija apdejtuje rejting mesta kada se skupi "odredjen broj" novih preporuka


                var query = new CypherQuery("match (n:Place {PlaceId: {PlaceId} }) set n.Rating = {Rating} ",
                    queryDict, CypherResultMode.Set);

                ((IRawGraphClient)DataLayer.Client).ExecuteCypher(query);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateCountryRating(int countryId)
        {
            try
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();
                queryDict.Add("CountryId", countryId);
                queryDict.Add("OverallRating", DataProviderGet.CalculateCountryRating(countryId));  
                


                var query = new CypherQuery("match (n:Country {CountryId: {CountryId} }) set n.OverallRating = {OverallRating} ",
                    queryDict, CypherResultMode.Set);

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