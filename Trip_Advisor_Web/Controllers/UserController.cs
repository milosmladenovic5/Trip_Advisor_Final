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

        [AllowAnonymous]
        public ActionResult ReturnUserPanelByUsername(string username)
        {
            User us = DataProviderGet.GetUser(username);

            return View("~/Views/Home/UserPanel.cshtml", DataMapper.CreateUserModel(us.UserId));
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

        [HttpPost]
        public JsonResult SendMessage(string receiver, string subject, string body)
        {

            int messageId = DataProviderCreate.CreateMessage(body, (string)Session["Username"], receiver, subject);

            if(messageId != 0)
            {
                DataRelationships.SendMessageToUser((string)Session["Username"], receiver, messageId);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAllReceivedMessagesOfUser(int userId)
        {
            List<Trip_Advisor_Neo4j.DomainModel.Message> messages = DataProviderGet.GetAllMessagesSentOrReceivedByUser(userId, "RECEIVED");

            List<MessageModel> messagesList = new List<MessageModel>();
            foreach(var mess in messages)
            {
                messagesList.Add(DataMapper.CreateMessageModel(mess));
            }

            return Json(messagesList, JsonRequestBehavior.AllowGet);
        }


        
    }
}