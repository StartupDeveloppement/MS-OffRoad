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
        public ActionResult Index()
        {
            return View();
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
                mailMethode.SendMail(email.Mail, "[Contact]"+email.LastName+" "+email.FirstName, email.Message);
                return View("Index");
            }
            catch
            {
                return View("Error");
            }
            
        }
    }
}