using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_Advisor_Neo4j.DomainModel;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Web.Models;
using Trip_Advisor_Redis;
using System.IO;

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
        public ActionResult GetSimilarPlaces(int userId, int placeId)
        {
   
            List<Place> recommendedPlaces = DataProviderGet.GetSimilarPlaces(userId, placeId);
            //ViewBag.CurrentlyAt = true;
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(recommendedPlaces));
        }

        [HttpGet]
        public ActionResult UserPlansToVisitPlace(int userId, int placeId)
        {
            if (!DataProviderGet.HasRelationshipWithaPlace(userId, placeId, "PLANSTOVISIT"))
                DataRelationships.PlansToVisit(userId, placeId);

            List<Place> plansToVisit = DataProviderGet.GetPlaces(userId, "PLANSTOVISIT");
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(plansToVisit));

        }

        [HttpGet]
        public ActionResult UserVisitedPlace(int userId, int placeId)
        {
            if (!DataProviderGet.HasRelationshipWithaPlace(userId, placeId, "VISITED"))
                DataRelationships.VisitedPlace(userId, placeId,DateTime.Now);

            RedisDataLayer.RefreshPlaceVCache();
            List<Place> plansToVisit = DataProviderGet.GetPlaces(userId, "VISITED");
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(plansToVisit));
        }

        [HttpGet]
        public ActionResult UserVisited (int userId)
        {
            List<Place> plansToVisit = DataProviderGet.GetPlaces(userId, "VISITED");
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(plansToVisit));
        }

        [HttpGet]
        public ActionResult UserPlansToVisit(int userId)
        {
            List<Place> plansToVisit = DataProviderGet.GetPlaces(userId, "PLANSTOVISIT");
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(plansToVisit));

        }

        [HttpPost]
        public ActionResult RecommendPlace(int placeId, string recommendationComment, int recommendationRating)
        {
            int rating = (recommendationRating==10) ? 10 : recommendationRating%10;
            string dateTest = DateTime.Now.ToString();
            DataRelationships.Recommend((int)Session["Id"], placeId, recommendationComment, rating);
            RedisDataLayer.UpdateRatings(placeId);
          
            return View("Place", DataMapper.CreatePlaceModel(placeId));
        }

        [HttpGet]
        public ActionResult GetTopVisitedPlaces()
        {
            List<Place> topVisitedPlaces = RedisDataLayer.GetTopPlacesByVisitors();
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(topVisitedPlaces));

        }

        [HttpGet]
        public ActionResult GetTopRatedPlaces()
        {
            List<Place> topRatedPlaces = RedisDataLayer.GetTopPlacesByRating();
            return View("ListOfPlaces", DataMapper.CreateListOfPlacesModel(topRatedPlaces));

        }

        [HttpPost]
        public JsonResult DeleteRecommendation(int recommendationId, int placeId)
        {
            bool result = DataProviderDelete.DeleteRecommendationById(recommendationId);
            if (result)
            {
                RedisDataLayer.UpdateRatings(placeId);
                //return View("Place", DataMapper.CreatePlaceModel(placeId));
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateDescription(int placeId, string descText)
        {
            DataProviderUpdate.UpdatePlaceDescription(placeId, descText);
            return View("Place", DataMapper.CreatePlaceModel(placeId));
        }

        [HttpPost]
        public ActionResult AddPicture(int placeId, HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                //Directory.CreateDirectory("~/Content/Images/Place" + placeId + "/");
                //var fileName = Path.GetFileName(file.FileName);
                //var path = Path.Combine(Server.MapPath("~/Content/Images/Place" + placeId + "/"), fileName);


                //file.SaveAs(path);
                //string picturePath = "/Content/Images/Place" + placeId + "/" + fileName;

               
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/Places/"), fileName);


                file.SaveAs(path);
                string picturePath = "/Content/Images/Places/"+ fileName;
                DataProviderUpdate.AddPictureOfPlace(picturePath, placeId);
            }

            return View("Place", DataMapper.CreatePlaceModel(placeId));
        }
    }
}