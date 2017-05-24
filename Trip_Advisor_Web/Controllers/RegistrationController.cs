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
            if (ModelState.IsValid && DataProviderGet.CheckUsersDontExist(model.Username, model.Email))
            {
                User user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    UserStatus = null,
                    ProfilePicture = "/Content/Images/User-Default.jpg",
                    UserStatusFLAG = 1
                };
               
                
                DataProviderCreate.CreateUser(user);
                SendMail(user);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Registration", model);
            }

        }

        public void SendMail(User user)
        {
            try
            {
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                     new System.Net.Mail.MailAddress("tripAdvisorVault101Team@outlook.com", "Web Registration"),
                     new System.Net.Mail.MailAddress(user.Email));
                m.Subject = "Trip Advisor e-mail confirmation.";


                m.Body = string.Format("Dear {0}<BR/>Thank you for registering, click on the link below to complete the account creation: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", user.Username, Url.Action("ConfirmEmail", "Registration", new { Token = user.UserId, Email = user.Email }, Request.Url.Scheme));

                m.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");
                smtp.Credentials = new System.Net.NetworkCredential("tripAdvisorVault101Team@outlook.com", "Vault101UnbreakblePassword");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(m);
            }
            catch (Exception ex)
            {
                Console.Write(ex.InnerException);
            }
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmail(string Token, string Email)
        {
            int id = Int32.Parse(Token);
            User user = DataProviderGet.GetNode<User>(id, "User");
            if (user != null)
            {
                DataProviderUpdate.UpdateUserStatusFLAG(id, 2);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}