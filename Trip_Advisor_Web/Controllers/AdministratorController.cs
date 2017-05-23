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
            if ((int)Session["Status"] == 10)
            {
                List<Place> allPlaces = DataProviderGet.GetAllPlaces();
                return View("PlaceAdministration", DataMapper.CreateListOfPlacesAdminModel(allPlaces));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CityAdministration()
        {
            if ((int)Session["Status"] == 10)
            {
                ViewBag.DeletionWarning = false;
                List<City> allCities = DataProviderGet.GetAllCities();
                return View("CityAdministration", DataMapper.CreateListOfCitiesAdminModel(allCities));
            }
            else
                return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult CountryAdministration()
        {

            if ((int)Session["Status"] == 10)
            {
                ViewBag.DeletionWarning = false;
                List<Country> allCountries = DataProviderGet.GetAllCountries();
                return View("CountryAdministration", DataMapper.CreateListOfCountriesModel(allCountries));
            }
            else
                return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult TagAdministration()
        {

            if ((int)Session["Status"] == 10)
            {

                ViewBag.Warning = false;
                List<InterestTag> allTags = DataProviderGet.GetAllTags2();
                return View("TagAdministration", DataMapper.CreateListOfTagsModel(allTags));
            }
            else
                return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult AddNewPlaceRequest()
        {
            if ((int)Session["Status"] == 10)
            {
                return View("AddOrModifyPlace", DataMapper.CreatePlaceAdminModel(0));
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddNewCityRequest()
        {
            if ((int)Session["Status"] == 10)
            {
                return View("AddOrModifyCity", DataMapper.CreateCityAdminModel(0));
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddNewCountryRequest()
        {
            if ((int)Session["Status"] == 10)
            {
                ViewBag.Update = false;
                return View("AddOrModifyCountry", new CountryModel());
            }
            else
                return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult UpdatePlaceRequest(int placeId)
        {
            if ((int)Session["Status"] == 10)
            {
                return View("AddOrModifyPlace", DataMapper.CreatePlaceAdminModel(placeId));
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateCityRequest(int cityId)
        {
            if ((int)Session["Status"] == 10)
            {
                return View("AddOrModifyCity", DataMapper.CreateCityAdminModel(cityId));
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateCountryRequest(int countryId)
        {
            if ((int)Session["Status"] == 10)
            {
                ViewBag.Update = true;
                return View("AddOrModifyCountry", DataMapper.CreateCountryAdminModel(countryId));
            }
            else
                return RedirectToAction("Index");
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


                //return RedirectToAction("GetPlaceById", "Place", new { placeId = placeId });
                return RedirectToAction("PlaceAdministration");

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


                //return RedirectToAction("GetPlaceById", "Place", new { placeId = placeId });
                return RedirectToAction("PlaceAdministration");

            }
            else
            {
                PlaceAdminModel pam = DataMapper.CreatePlaceAdminModel(0);
                pam.Place.Name = (pName == "" || check) ? "Invalid input!" : pName;
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
        public ActionResult UpdateCity(int cityId, string oldName, string cName = "", int selectCountry = 0, float lat = 0.0f, float lon = 0.0f)
        {
            bool check = (cName == oldName || DataProviderGet.GetCityByName(cName) == null);
            if (!string.IsNullOrEmpty(cName) && selectCountry != 0 && check)
            {
                City c = new City();
                c.Name = cName;
                c.CityId = cityId;
                c.CenterLongitude = lon;
                c.CenterLatitude = lat;
                DataProviderUpdate.UpdateCity(c);
                DataRelationships.HasCity(selectCountry, c.CityId);

                return RedirectToAction("CityAdministration");

            }
            else
            {
                CityAdminModel cam = DataMapper.CreateCityAdminModel(cityId);
                cam.City.Name = oldName;
                cam.City.CenterLatitude = lat;
                cam.City.CenterLongitude = lon;
                if (selectCountry != 0) cam.SelectedID = selectCountry;
                return View("AddOrModifyCity", cam);
            }

        }

        [HttpPost]
        public ActionResult CreateCity(string cName = "", int selectCountry = 0, float lat = 0.0f, float lon = 0.0f)
        {
            bool check = (DataProviderGet.GetCityByName(cName) == null);
            if (!string.IsNullOrEmpty(cName) && selectCountry != 0 && check)
            {
                City c = new City();
                c.Name = cName;
                c.CenterLongitude = lon;
                c.CenterLatitude = lat;
                int id = DataProviderCreate.CreateCity(c);
                DataRelationships.HasCity(selectCountry, id);

                return RedirectToAction("CityAdministration");

            }
            else
            {
                CityAdminModel cam = DataMapper.CreateCityAdminModel(0);
                cam.City.Name = (cName == "" || check) ? "Invalid input!" : cName;
                cam.City.CenterLatitude = lat;
                cam.City.CenterLongitude = lon;
                if (selectCountry != 0) cam.SelectedID = selectCountry;
                return View("AddOrModifyCity", cam);
            }

        }

        [HttpPost]
        public ActionResult UpdateCountry(int countryId, string oldName, string oldFlag, string cName = "", string url = "", HttpPostedFileBase file = null)
        {
            bool check = (cName == oldName || DataProviderGet.GetCountryByName(cName) == null);
            if (!string.IsNullOrEmpty(cName) && check)
            {
                Country c = new Country();
                c.Name = cName;
                c.CountryId = countryId;
                c.PromotionalVideoURL = url;
                c.NationalFlag = oldFlag;

                if (file != null && file.ContentLength > 0)
                {
    
                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Flags/"), fileName);


                    file.SaveAs(path);
                    string picturePath = "/Content/Images/Flags/" + fileName;
                    c.NationalFlag = picturePath;
                }

                DataProviderUpdate.UpdateCountry(c);
                return RedirectToAction("CountryAdministration");

            }
            else
            {
                CountryModel cm = DataMapper.CreateCountryAdminModel(countryId);
                cm.Name = oldName;
                cm.PromotionalVideoURL = url;

                ViewBag.Update = true;
                return View("AddOrModifyCountry", cm);
            }

        }

        [HttpPost]
        public ActionResult CreateCountry(string cName = "", string url = "", HttpPostedFileBase file = null)
        {
            bool check = (DataProviderGet.GetCountryByName(cName) == null);
            if (!string.IsNullOrEmpty(cName) && check)
            {
                Country c = new Country();
                c.Name = cName;
                c.PromotionalVideoURL = url;
                c.NationalFlag = "/Content/Images/Flags/f.jpg";
                if (file != null && file.ContentLength > 0)
                {

                    var fileName = System.IO.Path.GetFileName(file.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/Flags/"), fileName);


                    file.SaveAs(path);
                    string picturePath = "/Content/Images/Flags/" + fileName;
                    c.NationalFlag = picturePath;
                }

                DataProviderCreate.CreateCountry(c);


                return RedirectToAction("CountryAdministration");

            }
            else
            {
                CountryModel cm = new CountryModel();
                cm.Name = (cName == "" || check) ? "Invalid input!" : cName;
                cm.PromotionalVideoURL = url;

                ViewBag.Update = false;
                return View("AddOrModifyCountry", cm);
            }

        }

        [HttpPost]
        public JsonResult UpdateTag(int id, string newName)
        {
            if ((int)Session["Status"] == 10)
            {
                List<string> tags = DataProviderGet.GetAllTags();
                if(tags.Contains(newName))
                    return Json(false, JsonRequestBehavior.AllowGet);
                InterestTag it = new InterestTag();
                it.Name = newName;
                it.InterestTagId = id;
                bool result = DataProviderUpdate.UpdateTag(it);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateTag(string newTag)
        {
            if ((int)Session["Status"] == 10)
            {
                List<string> tags = DataProviderGet.GetAllTags();
                if (tags.Contains(newTag) || newTag == "")
                {
                    ViewBag.Warning = true;
                    List<InterestTag> allTags = DataProviderGet.GetAllTags2();
                    return View("TagAdministration", DataMapper.CreateListOfTagsModel(allTags));
                }
                InterestTag it = new InterestTag();
                it.Name = newTag;

                DataProviderCreate.CreateInterestTag(it);
                ViewBag.Warning = false;
                return RedirectToAction("TagAdministration");

            }
            else
                return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult DeleteTag(int id)
        {
            if ((int)Session["Status"] == 10)
            {
                bool result = DataProviderDelete.DeleteInterestTag(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeletePlace(int placeId)
        {
            if ((int)Session["Status"] == 10)
            {
                DataProviderDelete.DeletePlace(placeId);
                return RedirectToAction("PlaceAdministration");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteCity(int cityId)
        {
            if ((int)Session["Status"] == 10)
            {
                List<Place> places = DataProviderGet.GetPlacesOfCity(cityId);
                if (places == null || places.Count == 0)
                {
                    ViewBag.DeletionWarning = false;
                    DataProviderDelete.DeleteCity(cityId);
                }
                else
                    ViewBag.DeletionWarning = true;

                List<City> allCities = DataProviderGet.GetAllCities();
                return View("CityAdministration", DataMapper.CreateListOfCitiesAdminModel(allCities));
            }
            else
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteCountry(int countryId)
        {
            if ((int)Session["Status"] == 10)
            {
                List<City> cities = DataProviderGet.GetAllCountryCities(countryId);
                if (cities == null || cities.Count == 0)
                {
                    ViewBag.DeletionWarning = false;
                    DataProviderDelete.DeleteCountry(countryId);
                }
                else
                    ViewBag.DeletionWarning = true;

                List<Country> allCountries = DataProviderGet.GetAllCountries();
                return View("CountryAdministration", DataMapper.CreateListOfCountriesModel(allCountries));
            }
            else
                return RedirectToAction("Index");
        }

    }
    
}