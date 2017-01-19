using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_Advisor_Web.Models;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Neo4j.DomainModel;
using Trip_Advisor_Redis;
using System.IO;

namespace Trip_Advisor_Web.Controllers
{
    public class HomeController : Controller
    {
        public bool checks = false;
        public List<Place> topRatedPlaces;
        public List<Place> topVisitedPlaces;

        public List<Country> topRatedCountries;
        public List<Country> topVisitedCountries;

        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {

                if (!checks)
                {
                    Trip_Advisor_Neo4j.DataLayer.Connect();
                    RedisDataLayer.InitializeCounters();
                    RedisDataLayer.SaveTopRatedCountries();
                    RedisDataLayer.SaveTopRatedPlaces();
                    RedisDataLayer.SaveTopVisitedCountries();
                    RedisDataLayer.SaveTopVisitedPlaces();

                    //  this.topRatedCountries = redis.GetTopCountriesByRating();
                    //this.topRatedPlaces = redis.GetTopPlacesByRating();

                    //this.topVisitedCountries = redis.GetTopCountriesByVisitors();
                    //this.topVisitedPlaces = redis.GetTopPlacesByVisitors();

                    checks = true;
                }

                return View();
            }
            else
            {
                return View("UserPanel", DataMapper.CreateUserModel((int)Session["Id"]));
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            Trip_Advisor_Neo4j.DataLayer.Connect();

            User user = DataProviderGet.GetUser(model.Username);
            if (ModelState.IsValid && user != null && user.Password == model.Password)
            {

                Session["Id"] = user.UserId;
                Session["Username"] = user.Username;
                Session["Password"] = user.Password;
                Session["Picture"] = user.ProfilePicture;
                ViewBag.Change = false;


                //sada ovde treba da inicijalizujemo elemente korisnickog modela svim podacima iz baze

                //DataMapper.Context = 
                return View("UserPanel", DataMapper.CreateUserModel(user.UserId));

            }
            else
            {
                ViewBag.BadLogin = true;
                return View("Index", model);
            }
        }


        [HttpPost]
        public ActionResult ChangeData(UserModel user)
        {
            User userForChange = new Trip_Advisor_Neo4j.DomainModel.User();
            userForChange.UserId = (int)Session["Id"];
            userForChange.Username = user.Username;
            userForChange.Password = (string)Session["Password"];
            userForChange.Email = user.Email;
            userForChange.ProfilePicture = user.ProfilePicture;
            ViewBag.Change = false;

            string picturePath = String.Empty;
            if (user.PictureFile != null)
            {
                if (user.PictureFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(user.PictureFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    user.PictureFile.SaveAs(path);
                    picturePath = "/Content/Images/" + fileName;
                    userForChange.ProfilePicture = picturePath;
                }
            }


            DataProviderUpdate.UpdateUser(userForChange);


            return View("UserPanel", DataMapper.CreateUserModel((int)Session["Id"]));
        }


        [HttpPost]
        public ActionResult ChangeDataRequest(UserModel user)
        {
            //User userForChange = new Trip_Advisor_Neo4j.DomainModel.User();
            //userForChange.UserId = (int)Session["Id"];
            //userForChange.Username = (string)Session["Username"];
            //userForChange.Password = (string)Session["Password"];
            //userForChange.Email = (string)Session["Email"];
            ViewBag.Change = true;

            return View("UserPanel", DataMapper.CreateUserModel((int)Session["Id"]));
        }



        public ActionResult LogOut()
        {
            Session.Clear();
            return View("Index");
        }

    }

}