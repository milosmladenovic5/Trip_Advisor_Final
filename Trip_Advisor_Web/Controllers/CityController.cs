using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_Advisor_Web.Models;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Neo4j.DomainModel;

namespace Trip_Advisor_Web.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReturnCity(int cityId)
        {
            return View("City", DataMapper.CreateCityModel(cityId));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserCurrentlyAt(int userId, int cityId)
        {
            if (!DataProviderGet.HasRelationshipWithaPlace(userId, cityId, "CURRENTLYAT"))
            {
                DataProviderDelete.DeleteCurrentLocation(userId);
                DataRelationships.CurrentlyAt(userId, cityId);
            }

            // ovde mesta koja ga bi mogla da ga zanimaju u tom gradu
            // i prijatelje  koji su tu
            return View("City", DataMapper.CreateCityModel(cityId));
        }

    }
}