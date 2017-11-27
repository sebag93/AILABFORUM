using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AILABFORUM.Models;

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
                using (AILABFORUMEntities db = new AILABFORUMEntities())
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

        //Czy email istnieje juz w bazie
        [NonAction]
        public bool IsEmailExist(string EMAIL)
        {
            using (AILABFORUMEntities db = new AILABFORUMEntities())
            {
                var v = db.Users.Where(x => x.email == EMAIL).FirstOrDefault();
                return v != null;
            }
        }

        //Czy login istnieje juz w bazie
        [NonAction]
        public bool IsLoginExist(string LOGIN)
        {
            using (AILABFORUMEntities db = new AILABFORUMEntities())
            {
                var v = db.Users.Where(x => x.login == LOGIN).FirstOrDefault();
                return v != null;
            }
        }
    }
}