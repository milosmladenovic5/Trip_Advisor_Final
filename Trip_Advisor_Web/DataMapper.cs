﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Neo4j.DomainModel;
using Trip_Advisor_Web.Models;
using System.Web.Mvc;




namespace Trip_Advisor_Web
{
    public static class DataMapper
    {


        public static CityModel CreateCityModel(int cityId)
        {
 
            City city = DataProviderGet.GetNode<City>(cityId, "City");

            CityModel cityModel = new CityModel()
            {
                CityId = cityId,
                Name = city.Name
            };

            Country country = DataProviderGet.GetCitysCountry(cityId);
            CountryModel countryModel = new CountryModel()
            {
                Name = country.Name,
                CountryId = country.CountryId,
                NationalFlag = country.NationalFlag
            };

            cityModel.Country = countryModel;

            List<Place> citysPlaces = DataProviderGet.GetPlacesOfCity(cityId);
            foreach (Place p in citysPlaces)
            {
                PlaceModel place = new PlaceModel()
                {
                    Name = p.Name,
                    CityCenterDistance = p.CityCenterDistance,
                    PlaceId = p.PlaceId,
                    Pictures = p.Pictures,
                    Rating = p.Rating
                };
                cityModel.Places.Add(place);
            }

            return cityModel;

        }

        public static CountryModel CreateCountryModel(int countryId)
        {

            Country country = DataProviderGet.GetNode<Country>(countryId, "Country");

            CountryModel countryModel = new CountryModel();
            countryModel.CountryId = country.CountryId;
            countryModel.Name = country.Name;
            countryModel.NationalFlag = country.NationalFlag;
            countryModel.OverallRating = country.OverallRating;


            List<Place> ratedPlaces = DataProviderGet.GetTopNRatedPlacesByCountry(10, countryId);
            if (ratedPlaces != null)
            {
                foreach (Place p in ratedPlaces)
                {
                    PlaceModel place = new PlaceModel()
                    {
                        Name = p.Name,
                        PlaceId = p.PlaceId,
                        Pictures = p.Pictures,
                        CityCenterDistance = p.CityCenterDistance,
                        Description = p.Description,
                        Rating = p.Rating
                    };

                    countryModel.TopRatedPlaces.Add(place);
                }

            }

            List<Place> visitedPlaces = DataProviderGet.GetTopNVisitedPlacesByCountry(10, countryId);

            if (visitedPlaces != null)
            {
                foreach (Place p in ratedPlaces)
                {
                    PlaceModel place = new PlaceModel()
                    {
                        Name = p.Name,
                        PlaceId = p.PlaceId,
                        Pictures = p.Pictures,
                        CityCenterDistance = p.CityCenterDistance,
                        Description = p.Description,
                        Rating = p.Rating
                    };

                    countryModel.TopRatedPlaces.Add(place);
                }
            }


            return countryModel;
        }


        public static ListOfPlacesModel CreateListOfPlacesModel (List<Place> places)
        {
            ListOfPlacesModel list = new ListOfPlacesModel();

            foreach (Place p in places)
            {
                PlaceModel placeMd = new PlaceModel();
                placeMd.Name = p.Name;
                placeMd.PlaceId = p.PlaceId;
                placeMd.Rating = p.Rating;

                list.PlacesList.Add(placeMd);
            }

            return list;
        }


        public static PlaceModel CreatePlaceModel (int placeId)
        {
            Place place = DataProviderGet.GetNode<Place>(placeId, "Place");

            PlaceModel placeModel = new PlaceModel();
            placeModel.Name = place.Name;
            placeModel.Description = place.Description;
            placeModel.CityCenterDistance = place.CityCenterDistance;
            placeModel.PlaceId = place.PlaceId;
            placeModel.Rating = place.Rating;
            placeModel.Type = place.Type;
            
            if(place.Pictures!=null)
            { 
                    foreach(var pic in place.Pictures)
                    {
                        placeModel.Pictures.Add(pic);
                    }
            }

            City placeLocation = DataProviderGet.GetPlaceLocation(placeId);
            placeModel.PlaceLocation.Name = placeLocation.Name;
     

            List<Recommendation> recommendations = DataProviderGet.GetPlaceRecommendations(placeId);

            foreach(Recommendation r in recommendations)
            {
                RecommendationModel recommendationModel = new RecommendationModel();
                recommendationModel.Comment = r.Comment;
                recommendationModel.Rating = r.Rating;
                recommendationModel.RefferedBy = CreateUserModel(r.UserId);
                recommendationModel.RecommendationTime = r.RecommendationTime;
               

                placeModel.Recommendations.Add(recommendationModel);
            }

            List<InterestTag> tags = DataProviderGet.GetInterestTagsOfPlace(placeId);

            foreach (var tag in tags)
            {
                InterestTagModel tagMd = new InterestTagModel();
                tagMd = CreateInterestTagModel(tag.InterestTagId);

                placeModel.Tags.Add(tagMd);
            }

            return placeModel;
        }

        public static RecommendationModel CreateRecommendationModel (int recommendationId)
        {
            //Recommendation recomm = DataProviderGet.GetNode<Recommendation>(recommendationId, "Recommendation");

            //RecommendationModel recommendationModel = new RecommendationModel();
            //recommendationModel.Comment = recomm.Comment;
            //recommendationModel.Rating = recomm.Rating;
            //recommendationModel.RefferedBy = 

            return null;
        }

        public static InterestTagModel CreateInterestTagModel (int interestTagId)
        {
            InterestTag intTag = DataProviderGet.GetNode<InterestTag>(interestTagId, "InterestTag");

            InterestTagModel intTagModel = new InterestTagModel();
            intTagModel.FieldOfLife = intTag.FieldOfLife;
            intTagModel.InterestTagId = intTag.InterestTagId;
            intTagModel.Name = intTag.Name;
            intTagModel.Type = intTag.Type;

            return intTagModel;
        }

     
        public static UserModel CreateUserModel (int userId)
        {
            HttpContext context = HttpContext.Current;

            User user = DataProviderGet.GetNode<User>(userId, "User");

            UserModel userModel = new UserModel();
            userModel.Username = user.Username;
            userModel.Password = user.Password;
            userModel.ProfilePicture = user.ProfilePicture;
            userModel.Email = user.Email;
            userModel.UserId = user.UserId;

            Place currentLocation = DataProviderGet.GetCurrentLocation(userId);
            PlaceModel placeMdl = new PlaceModel() {
                Name = currentLocation.Name,
                PlaceId = currentLocation.PlaceId,
                Description = currentLocation.Description
            };

            for (int pic = 0; pic < 1; pic++)
            {
                placeMdl.Pictures.Add(currentLocation.Pictures[pic]);
            }

            userModel.CurrentLocation = placeMdl;

            List<Place> PlacesThatWantsToVisit = DataProviderGet.GetPlaces(user.UserId, "PLANSTOVISIT");
            foreach (var place in PlacesThatWantsToVisit)
            {
                PlaceModel placeMd = new PlaceModel();
                placeMd.CityCenterDistance = place.CityCenterDistance;
                placeMd.Description = place.Description;
                placeMd.Name = place.Name;
                placeMd.PlaceId = place.PlaceId;
                placeMd.PlaceLocation = null;
                placeMd.Tags = null;

                userModel.PlansToVisit.Add(placeMd);
            }

            List<Place> PlacestThatUserVisited = DataProviderGet.GetPlaces(user.UserId, "VISITED");
            foreach (var place in PlacestThatUserVisited)
            {
                PlaceModel placeMd = new PlaceModel();
                placeMd.CityCenterDistance = place.CityCenterDistance;
                placeMd.Description = place.Description;
                placeMd.Name = place.Name;
                placeMd.PlaceId = place.PlaceId;
                placeMd.PlaceLocation = null;
                placeMd.Tags = null;

                userModel.Visited.Add(placeMd);
            }

            userModel.FollowingHim = false;
            List<User> Followers = DataProviderGet.GetAllFollowers(user.UserId);
            foreach (var friend in Followers)
            {
                UserModel userMd = new UserModel();
                userMd.Username = friend.Username;
                userMd.ProfilePicture = friend.ProfilePicture;
                userMd.UserId = friend.UserId;
                userModel.Followers.Add(userMd);

                if (friend.UserId == (int)context.Session["Id"])
                {
                    userModel.FollowingHim = true;
                }
            }

            List<User> SamePlaceUsers = DataProviderGet.GetFollowingThatAreCurrentlyAtTheSameCity(userId);
            foreach(var p in SamePlaceUsers)
            {
                UserModel us = new UserModel()
                {
                    UserId = p.UserId,
                    Username = p.Username,
                    Email = p.Email,
                    ProfilePicture = p.ProfilePicture
                };

                userModel.UsersCurrentlyAtTheSamePlace.Add(us);
            }

            List<InterestTag> Interests = DataProviderGet.GetInterestsOfUser(user.UserId);
            foreach (var tag in Interests)
            {
                InterestTagModel tagMd = new InterestTagModel();
                tagMd.FieldOfLife = tag.FieldOfLife;
                tagMd.InterestTagId = tag.InterestTagId;
                tagMd.Name = tag.Name;
                tagMd.Type = tag.Type;

                userModel.Interests.Add(tagMd);
            }

            List<User> Following = DataProviderGet.GetAllFollowing(user.UserId);
            foreach (var friend in Following)
            {
                UserModel userMd = new UserModel();
                userMd.Username = friend.Username;
                userMd.ProfilePicture = friend.ProfilePicture;
                userMd.UserId = friend.UserId;
                                    
                userModel.Following.Add(userMd);
            }
            
            return userModel;
        }


    }
}