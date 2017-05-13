﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_Advisor_Neo4j.DataAccess;


namespace Trip_Advisor_Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ReturnUserPanel(int userId)
        {
            return View("~/Views/Home/UserPanel.cshtml", DataMapper.CreateUserModel(userId));
        }

        [HttpPost]
        public JsonResult Follow(int userId)
        {
            bool torf = DataRelationships.Follow((int)Session["Id"], userId);

            return Json("Succesfull follow!", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Unfollow(int userId)
        {
            bool torf = DataProviderDelete.Unfollow((int)Session["Id"], userId);

            return Json("No longer following the user!", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AddInterestTags(int userId, string[] interestTagNames)
        {
            if (interestTagNames.Length > 0)
                DataProviderDelete.DeleteInterestsOfUser(userId);

            for (int i = 0; i < interestTagNames.Length; i++)
            {
                DataRelationships.HasInterest(userId, interestTagNames[i]);
            }

            return Json("Added" + interestTagNames.Length + "new tags", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendMessage(int senderId, int receiverId, string messageText, string subject)
        {
            int messId = DataProviderCreate.CreateMessage(messageText, subject);
            bool success = DataRelationships.SendMessage(senderId, receiverId, messId);
           
            return Json(success);
        }

        [HttpPost]
        public JsonResult ReturnInterestTags()
        {
            List<Trip_Advisor_Web.JSON.UserTagPair> pairs = new List<Trip_Advisor_Web.JSON.UserTagPair>();

            List<string> tags = DataProviderGet.GetAllTags();
            List<string> userTags = DataProviderGet.GetInterestsOfUserToStringArray((int)Session["Id"]);

            for (int i = 0; i < tags.Count; i++)
            {
                bool hasIt = false;
                if (userTags.Contains(tags[i]))
                    hasIt = true;

                pairs.Add(new Trip_Advisor_Web.JSON.UserTagPair()
                {
                    tagName = tags[i],
                    userHasIt = hasIt
                }
                );

            }

            return Json(pairs, JsonRequestBehavior.AllowGet);
        }
    }
}