using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

using TechStudioTest.Auth;
using TechStudioTest.DataAccess;
using TechStudioTest.Models;
using TechStudioTest.ViewModels;

namespace TechStudioTest.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.UserName, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.UserName, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new ViewModels.CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            UserName = loginView.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(60), false, userData);
                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        Session["TechStudioTestAuth"] = enTicket;
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Something wrong: UserName or Password Invalid!");
            return View(loginView);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationView registrationView)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                // Email Verification
                string userName = Membership.GetUserNameByEmail(registrationView.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already exists.");
                    return View(registrationView);
                }

                // Save User Data
                using (AuthContext authContext = new AuthContext())
                {
                    var user = new User()
                    {
                        UserName = registrationView.UserName,
                        FirstName = registrationView.FirstName,
                        LastName = registrationView.LastName,
                        Email = registrationView.Email,
                        Password = registrationView.Password,
                        IsActive = true
                    };

                    authContext.Users.Add(user);
                    authContext.SaveChanges();
                }

                messageRegistration = "Your account has been created successfully.";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Something wrong!";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registrationView);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account", null);
        }

        public ActionResult KeepAlive()
        {
            if (Session["TechStudioTestAuth"] == null)
            {
                return RedirectToAction("Login", "Account", null);
            }
            else
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(Session["TechStudioTestAuth"].ToString());
                var userModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                string userData = JsonConvert.SerializeObject(userModel);
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(1, userModel.UserName, DateTime.Now, DateTime.Now.AddMinutes(60), false, userData);
                string enTicket = FormsAuthentication.Encrypt(newTicket);
                Session["TechStudioTestAuth"] = enTicket;

                return Redirect(Request.RawUrl);
            }
        }
    }
}