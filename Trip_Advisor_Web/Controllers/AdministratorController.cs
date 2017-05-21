using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Neo4j.DomainModel;
using Trip_Advisor_Web.Models;

namespace Trip_Advisor_Web.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdministratorPanel()
        {
            return View("AdministratorPanel");
        }

        [HttpPost]
        public JsonResult UpdateUserStatusFLAG(int userId, int newFlag)
        {
            bool result = DataProviderUpdate.UpdateUserStatusFLAG(userId, newFlag);

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult PlaceAdministration()
        {
            List<Place> allPlaces = DataProviderGet.GetAllPlaces();
            return View("PlaceAdministration", DataMapper.CreateListOfPlacesAdminModel(allPlaces));

        }

        [HttpGet]
        public ActionResult AddNewPlaceRequest()
        {
            if ((int)Session["Status"] == 10)
            {
                return View("AddOrModifyPlace", DataMapper.CreatePlaceAdminModel(0));
            }
            else
                return View("Index");
        }

        [HttpGet]
        public ActionResult UpdatePlaceRequest(int placeId)
        {
            if ((int)Session["Status"] == 10)
            {
                return View("AddOrModifyPlace", DataMapper.CreatePlaceAdminModel(placeId));
            }
            else
                return View("Index");
        }

        [HttpPost]
        public ActionResult UpdatePlace(int placeId, string oldName, string pName = "", string desc = "", int selectCity = 0, IEnumerable<string> selectTags = null, float lat = 0.0f, float lon = 0.0f, int ccd = 0)
        {
            bool check = (pName == oldName || DataProviderGet.GetPlaceByName(pName) == null);
            if (!string.IsNullOrEmpty(pName) && selectCity != 0 && check)
            {
                Place update = new Place();
                update.Name = pName;
                update.PlaceId = placeId;
                update.Description = desc;
                update.Latitude = lat;
                update.Longitude = lon;
                update.CityCenterDistance = ccd;
                DataProviderUpdate.UpdatePlace(update);
                DataProviderDelete.DeletePlaceLocation(placeId);
                DataProviderDelete.DeletePlaceTags(placeId);
                DataRelationships.HasPlace(selectCity, placeId);
                if (selectTags != null)
                {
                    foreach (string tag in selectTags)
                        DataRelationships.HasInterestTag(placeId, tag);
                }

                //List<Place> allPlaces = DataProviderGet.GetAllPlaces();
                //return View("PlaceAdministration", DataMapper.CreateListOfPlacesAdminModel(allPlaces));
                return RedirectToAction("GetPlaceById", "Place", new { placeId = placeId });

            }
            else
            {
                PlaceAdminModel pam = DataMapper.CreatePlaceAdminModel(placeId);
                pam.Place.Name = oldName;
                pam.Place.Description = desc;
                pam.Place.Latitude = lat;
                pam.Place.Longitude = lon;
                pam.Place.CityCenterDistance = ccd;
                if (selectCity != 0) pam.SelectedID = selectCity;
                if (selectTags != null) pam.SelectedTags = selectTags.ToList();
                return View("AddOrModifyPlace", pam);
            }

        }

        [HttpPost]
        public ActionResult CreatePlace(string pName = "", string desc = "", int selectCity = 0, IEnumerable<string> selectTags = null, float lat = 0.0f, float lon = 0.0f, int ccd = 0)
        {
            bool check = (DataProviderGet.GetPlaceByName(pName) == null);
            if (!string.IsNullOrEmpty(pName) && selectCity != 0 && check)
            {
                Place p = new Place();
                p.Name = pName;
                p.Description = desc;
                p.Latitude = lat;
                p.Longitude = lon;
                p.CityCenterDistance = ccd;
                int placeId = DataProviderCreate.CreatePlace(p);

                DataRelationships.HasPlace(selectCity, placeId);
                if (selectTags != null)
                {
                    foreach (string tag in selectTags)
                        DataRelationships.HasInterestTag(placeId, tag);
                }

                //List<Place> allPlaces = DataProviderGet.GetAllPlaces();
                //return View("PlaceAdministration", DataMapper.CreateListOfPlacesAdminModel(allPlaces));
                return RedirectToAction("GetPlaceById", "Place", new { placeId = placeId });

            }
            else
            {
                PlaceAdminModel pam = DataMapper.CreatePlaceAdminModel(0);
                pam.Place.Name = "NOT UNIQUE OR EMPTY!";
                pam.Place.Description = desc;
                pam.Place.Latitude = lat;
                pam.Place.Longitude = lon;
                pam.Place.CityCenterDistance = ccd;
                if (selectCity != 0) pam.SelectedID = selectCity;
                if (selectTags != null) pam.SelectedTags = selectTags.ToList();
                return View("AddOrModifyPlace", pam);
            }

        }
    }
    
}