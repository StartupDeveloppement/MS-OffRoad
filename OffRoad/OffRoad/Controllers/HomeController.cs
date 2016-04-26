using OffRoad.Context;
using OffRoad.Methodes;
using OffRoad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OffRoad.Controllers
{
    public class HomeController : Controller
    {
        private MailMethode mailMethode = new MailMethode();
        private DBContext db = new DBContext();
        private ArticleMethode artM = new ArticleMethode();
        private EventMethode eventM = new EventMethode();

        public ActionResult Index()
        {
            ViewBag.Categories = db.Categorys.ToList();
            List<Article> articleList = artM.GetRecentsArticles();
            ViewBag.LastEvents = eventM.GetRecentsEvents();
            return View(articleList);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(Email email )
        {
            try
            {
                //string subject = String.Format("[Contact] {0} {1} {2}", email.LastName, email.FirstName, email.Phone); 
                //mailMethode.SendMail(email.Mail, subject, email.Message);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }
            
        }
    }
}