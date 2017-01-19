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
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Registration()
        {
            RegistrationModel regModel = new RegistrationModel();
            return View(regModel);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    UserStatus = null,
                    ProfilePicture = null
                };
               
                
                DataProviderCreate.CreateUser(user);

                return RedirectToAction("Home", "Index");
            }
            else
            {
                return View("Register", model);
            }

        }

    }
}