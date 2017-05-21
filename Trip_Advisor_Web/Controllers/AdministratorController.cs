using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip_Advisor_Neo4j.DataAccess;

namespace Trip_Advisor_Web.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdministratorPanel()
        {
            return View("AdministratorPanel");
        }

        [HttpPost]
        public JsonResult UpdateUserStatusFLAG(int userId, int newFlag)
        {
            bool result = DataProviderUpdate.UpdateUserStatusFLAG(userId, newFlag);

            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}