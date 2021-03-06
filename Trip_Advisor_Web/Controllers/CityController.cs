﻿using System;
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

        

    }
}