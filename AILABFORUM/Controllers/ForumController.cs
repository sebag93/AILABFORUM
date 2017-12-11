using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AILABFORUM.Models;

namespace AILABFORUM.Controllers
{
    public class ForumController : Controller
    {
        //NewTopic
        [Authorize]
        [HttpGet]
        public ActionResult NewTopic()
        {
            return View();
        }

        //NewTopic POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewTopic(Topic topic)
        {
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                #region //temat juz istnieje
                var topicExist = IsTopicExist(topic.tytul);
                if (topicExist)
                {
                    ModelState.AddModelError("TopicExist", "Temat o podanej nazwie już istnieje. Niemożliwe jest powielanie nazw tematów");
                    return View(topic);
                }
                #endregion

                topic.data_dodania = DateTime.Now;
                topic.autor = User.Identity.Name;

                #region //zapis do bazy danych
                using (AILABFORUMEntities db = new AILABFORUMEntities())
                {
                    db.Topics.Add(topic);
                    db.SaveChanges();
                    message = "Temat utworzony pomyślnie.";
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
            return View(topic);
        }



        //Czy temat o danej nazwie istnieje
        [NonAction]
        public bool IsTopicExist(string TYTUL)
        {
            using (AILABFORUMEntities db = new AILABFORUMEntities())
            {
                var v = db.Topics.Where(x => x.tytul == TYTUL).FirstOrDefault();
                return v != null;
            }
        }
    }
}