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
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ReturnAllUsersByFirstLetter(string firstLetter)
        {
           string[] UsersByFirstLetterArray = DataProviderGet.GetEntityByFirstLetter(firstLetter.ToString(), "User", "Username").ToArray();

            return Json(UsersByFirstLetterArray, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReturnAllPlacesByName(string firstLetter)
        {
            string[] PlacesByFirstLetter = DataProviderGet.GetEntityByFirstLetter(firstLetter, "Place", "Name").ToArray();

            return Json(PlacesByFirstLetter, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReturnAllCountriesByName(string firstLetter)
        {
            string[] CountriesByFirstLetter = DataProviderGet.GetEntityByFirstLetter(firstLetter.ToString(), "Country", "Name").ToArray();

            return Json(CountriesByFirstLetter, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReturnAllTagsByName(string firstLetter)
        {
            string[] tagsByFirstLetter = DataProviderGet.GetEntityByFirstLetter(firstLetter.ToString(), "InterestTag", "Name").ToArray();

            return Json(tagsByFirstLetter, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ReturnAllCitiesByName(string firstLetter)
        {
            string[] CitiesByFirstLetter = DataProviderGet.GetEntityByFirstLetter(firstLetter.ToString(), "City", "Name").ToArray();

            return Json(CitiesByFirstLetter, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ReturnEntity (string searchInput, string entityType)
        {
           if (entityType == "User")
            {
                //pozvacemo kontroler za korisnika
                User user = DataProviderGet.GetUser(searchInput);
                int userId = (user != null) ? user.UserId : (int)Session["Id"];
                return RedirectToAction("ReturnUserPanel", "User", new { userId = userId });


            }
           else if (entityType == "Place")
            {

                Place place = DataProviderGet.GetPlaceByName(searchInput);
                if (place != null)
                    return RedirectToAction("GetPlace", "Place", new { placeId = place.PlaceId });
                else
                    return RedirectToAction("ReturnUserPanel", "User", new { userId = (int)Session["Id"] });

            }
           else if (entityType == "Country")
            {
                Country country = DataProviderGet.GetCountryByName(searchInput);
                if (country != null)
                    return RedirectToAction("ReturnCountry", "Country", new { countryId = country.CountryId });
                else
                    return RedirectToAction("ReturnUserPanel", "User", new { userId = (int)Session["Id"] });
            }
            else if (entityType == "City")
            {
                City city = DataProviderGet.GetCityByName(searchInput);
                if (city != null)
                    return RedirectToAction("ReturnCity", "City", new { cityId = city.CityId });
                else
                    return RedirectToAction("ReturnUserPanel", "User", new { userId = (int)Session["Id"] });
            }
            else
            {
                return RedirectToAction("ReturnUserPanel", "User", new { userId = (int)Session["Id"] });
            }
            
        }

        [HttpPost]
        public ActionResult SearchPlacesByTags(int cityId, string tags)
        {
            List<Place> foundPlaces = GetPlacesByTags(cityId, tags);

            if (foundPlaces != null)
                return View("~/Views/Place/ListOfPlaces.cshtml", DataMapper.CreateListOfPlacesModel(foundPlaces));
            else
                return View("~/Views/Place/ListOfPlaces.cshtml", new ListOfPlacesModel());
        }

        public static List<Place> GetPlacesByTags(int cityId, string tagNames)
        {
            try
            {
                List<PlaceModel> returnPlaces = new List<PlaceModel>();
                List<Place> fPlaces = new List<Place>();

                string tagsN = tagNames;
                if (tagsN == "") return null;

                char[] separatingChar = { '#', ' ' };
                string[] tagArr = tagsN.Split(separatingChar, StringSplitOptions.RemoveEmptyEntries);

                List<Place> foundPlaces = DataProviderGet.GetPlacesWithTag(cityId, tagArr[0]);

                if (foundPlaces == null || foundPlaces.Count == 0) return null;

                string[] tagsPlaces = new string[foundPlaces.Count];

                for (int k = 0; k < tagsPlaces.Length; k++)
                {
                    tagsPlaces[k] = "";
                }


                int i = 0;
                foreach (var place in foundPlaces)
                {
                    List<InterestTag> tags = DataProviderGet.GetInterestTagsOfPlace(place.PlaceId);
                    foreach (var tag in tags)
                    {
                        tagsPlaces[i] = "" + tagsPlaces[i] + tag.Name + " ";

                    }
                    i++;
                }

                Place[] placesArray = foundPlaces.ToArray();

                int j = 0;
                for (int loop = 0; loop < placesArray.Length; loop++)
                {
                    int counter = 0;
                    foreach (var tag in tagArr)
                    {
                        if (tagsPlaces[j].Contains(tag))
                        {
                            counter++;
                            if (counter == tagArr.Length)
                            {
                                fPlaces.Add(placesArray[j]);
                            }
                        }
                    }
                    j++;
                }

                return fPlaces;
            }
            catch 
            {
                return null;
            }
        }

    }
}