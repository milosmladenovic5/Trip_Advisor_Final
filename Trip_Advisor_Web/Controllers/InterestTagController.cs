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
    public class InterestTagController : Controller
    {
        // GET: InterestTag
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPlacesByTag(int interestTagId)
        {



            return null;
        }
    }
}