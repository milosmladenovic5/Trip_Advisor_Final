using System;
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
        public ActionResult ReturnUserPanel (int userId)
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
    }
}