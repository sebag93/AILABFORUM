using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AILABFORUM.Models;
using System.Web.Security;
using System.Data.Entity;

namespace AILABFORUM.Controllers
{
    public class UserController : Controller
    {
        //Registration
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            bool Status = false;
            string message = "";
            //walidacja modelu
            if (ModelState.IsValid)
            {

                #region //email juz istnieje
                var emailExist = IsEmailExist(user.email);
                if (emailExist)
                {
                    ModelState.AddModelError("EmailExist", "Podany email został już zarejestrowany");
                    return View(user);
                }
                #endregion

                #region //login juz istnieje
                var loginExist = IsLoginExist(user.login);
                if (loginExist)
                {
                    ModelState.AddModelError("LoginExist", "Podana nazwa użytkownika została już zarejestrowana");
                    return View(user);
                }
                #endregion

                #region //zapis do bazy danych
                using (AiLabForumEntities db = new AiLabForumEntities())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    message = "Rejestracja zakończona pomyślnie. Możesz zalogować sie na swoje konto.";
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "Nieprawidłowe żądanie";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin dane, string ReturnUrl="")
        {
            string message = "";
            using (AiLabForumEntities db = new AiLabForumEntities())
            {
                var v = db.Users.Where(x => x.login == dane.login && x.haslo == dane.haslo).FirstOrDefault();
                if (v != null)
                {
                    int timeout = dane.zapamietaj ? 525600 : 20; //525600 minut to 1 rok
                    var ticket = new FormsAuthenticationTicket(dane.login, dane.zapamietaj, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
                    {
                        Expires = DateTime.Now.AddMinutes(timeout),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(cookie);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        //return RedirectToAction(ReturnUrl);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    message = "Wprowadzono niepoprawne dane";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        //Manage Account
        [Authorize]
        [HttpGet]
        public ActionResult ManageAcc()
        {
            return View();
        }

        //Manage Account POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageAcc(User user)
        {
            bool Status = false;
            string message = "";
            user.login = User.Identity.Name;
            using (AiLabForumEntities db = new AiLabForumEntities())
            {
                var v = db.Users.Where(x => x.login == user.login && x.haslo == user.haslo).FirstOrDefault();
                if (v != null)
                {
                    db.Users.SqlQuery("UPDATE Users SET haslo ='" + user.haslo + "' WHERE login='" + User.Identity.Name + "'");
                    db.SaveChanges();
                    message = "Hasło zmienione pomyślnie.";
                    Status = true;
                }
                else
                {
                    message = "Wprowadzono błędne hasło.";
                }
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        //Czy email istnieje juz w bazie
        [NonAction]
        public bool IsEmailExist(string EMAIL)
        {
            using (AiLabForumEntities db = new AiLabForumEntities())
            {
                var v = db.Users.Where(x => x.email == EMAIL).FirstOrDefault();
                return v != null;
            }
        }

        //Czy login istnieje juz w bazie
        [NonAction]
        public bool IsLoginExist(string LOGIN)
        {
            using (AiLabForumEntities db = new AiLabForumEntities())
            {
                var v = db.Users.Where(x => x.login == LOGIN).FirstOrDefault();
                return v != null;
            }
        }
    }
}