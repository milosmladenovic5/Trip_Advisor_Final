using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_Advisor_Neo4j.DomainModel;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Web.Models;
using Trip_Advisor_Redis;

namespace Trip_Advisor_Web.Controllers
{
    public class PlaceController : Controller
    {
        // GET: Place
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetPlaceById(int placeId)
        {

            //DataProviderUpdate.AddPictureOfPlace("~/Content/Images/DSC_7660.jpg", placeId);
            //  DataProviderUpdate.AddPictureOfPlace("~/Content/Images/sl.JPG", placeId);
            //DataProviderUpdate.AddPictureOfPlace("/Content/Images/Bubanj2.jpg", placeId);
            //DataProviderUpdate.AddPictureOfPlace("/Content/Images/Bubanj3.jpg", placeId);
            //DataProviderUpdate.AddPictureOfPlace("/Content/Images/Tri_pesnice_Bubanj.jpg", placeId);

            //Place place = DataProviderGet.GetNode<Place>(placeId, "Place");
            return View("Place", DataMapper.CreatePlaceModel(placeId));
        }

        public ActionResult GetPlace(int placeId)
        {
            return View("Place", DataMapper.CreatePlaceModel(placeId));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserCurrentlyAtPlace(int userId, int placeId)
        {
            if (!DataProviderGet.HasRelationshipWithaPlace(userId, placeId, "CURRENTLYAT"))
                DataRelationships.CurrentlyAtPlace(userId, placeId);
        
            List<Place> recommendedPlaces = DataProviderGet.GetSimilarPlacesIds(userId, placeId);
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(recommendedPlaces));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserPlansToVisitPlace(int userId, int placeId)
        {
            if (!DataProviderGet.HasRelationshipWithaPlace(userId, placeId, "PLANSTOVISIT"))
                DataRelationships.PlansToVisit(userId, placeId);

            List<Place> plansToVisit = DataProviderGet.GetPlaces(userId, "PLANSTOVISIT");
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(plansToVisit));

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserVisitedPlace(int userId, int placeId)
        {
            if (!DataProviderGet.HasRelationshipWithaPlace(userId, placeId, "VISITED"))
                DataRelationships.VisitedPlace(userId, placeId,DateTime.Now);

            RedisDataLayer.RefreshPlaceVCache();
            List<Place> plansToVisit = DataProviderGet.GetPlaces(userId, "VISITED");
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(plansToVisit));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserVisited (int userId)
        {
            List<Place> plansToVisit = DataProviderGet.GetPlaces(userId, "VISITED");
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(plansToVisit));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserPlansToVisit(int userId)
        {
            List<Place> plansToVisit = DataProviderGet.GetPlaces(userId, "PLANSTOVISIT");
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(plansToVisit));

        }

        [HttpPost]
        public ActionResult RecommendPlace(int userId, int placeId, string recommendationComment, int recommendationRating)
        {
            int rating = recommendationRating % 10;
            DataRelationships.Recommend(userId, placeId, recommendationComment, rating);
            DataProviderUpdate.UpdatePlaceRating(placeId);
            RedisDataLayer.RefreshPlaceRCache();
           // RedisDataLayer.UpdateCountryRating()
            return View("Place", DataMapper.CreatePlaceModel(placeId));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetTopVisitedPlaces()
        {
            List<Place> topVisitedPlaces = RedisDataLayer.GetTopPlacesByVisitors();
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(topVisitedPlaces));

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetTopRatedPlaces()
        {
            List<Place> topRatedPlaces = RedisDataLayer.GetTopPlacesByRating();
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(topRatedPlaces));

        }
    }
}