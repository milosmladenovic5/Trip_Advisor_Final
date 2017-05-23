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
        public ActionResult SearchPlacesByTags(string tags)
        {
            List<Place> foundPlaces = GetPlacesByTags(tags);
            //ViewBag.CurrentlyAt = true;
            return View("~/Views/Place/ListOfPlaces.cshtml", DataMapper.CreateListOfPlacesModel(foundPlaces));
        }

        public static List<Place> GetPlacesByTags(string tagNames)
        {
            List<PlaceModel> returnPlaces = new List<PlaceModel>();
            List<Place> fPlaces = new List<Place>();

            string primljeniTagovi = tagNames;

            char[] separatingChar = { '#', ' ' };
            string[] nizTagova = primljeniTagovi.Split(separatingChar, System.StringSplitOptions.RemoveEmptyEntries);

            List<Place> foundPlaces = DataProviderGet.GetPlacesWithTag(nizTagova[0]);

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

            //treba da konvertujemo listu pitanja u niz pitanja

            Place[] placesArray = foundPlaces.ToArray();

            int j = 0;
            for (int petlja = 0; petlja < placesArray.Length; petlja++)
            {
                int brojac = 0;
                foreach (var tag in nizTagova)
                {
                    if (tagsPlaces[j].Contains(tag))
                    {
                        brojac++;
                        if (brojac == nizTagova.Length)
                        {
                            fPlaces.Add(placesArray[j]);
                        }
                    }
                }
                j++;
            }

            return fPlaces;
        }

    }
}