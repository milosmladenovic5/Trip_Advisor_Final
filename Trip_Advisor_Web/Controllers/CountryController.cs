using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Neo4j.DomainModel;
using Trip_Advisor_Redis;
using Trip_Advisor_Web.Models;

namespace Trip_Advisor_Web.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReturnCountry (int countryId)
        {
            return View("Country", DataMapper.CreateCountryModel(countryId));
        }

        public ActionResult ReturnTopVisitedCountries()
        {
            List<Country> cl = RedisDataLayer.GetTopCountriesByVisitors();
            return View("ListOfCountries", DataMapper.CreateListOfCountriesModel(cl));
        }

        public ActionResult ReturnTopRatedCountries()
        {
            List<Country> cl = RedisDataLayer.GetTopCountriesByRating();
            return View("ListOfCountries", DataMapper.CreateListOfCountriesModel(cl));
        }

    }
}