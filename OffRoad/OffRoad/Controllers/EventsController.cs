using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OffRoad.Context;
using OffRoad.Models;
using OffRoad.Filtre;
using OffRoad.Methodes;

namespace OffRoad.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private DBContext db = new DBContext();
        private static DateTime? createDateSave = DateTime.MinValue;
        private EventMethode eventM = new EventMethode();
        private AuthMethode authM = new AuthMethode();
        private OffRoad.Provider.RoleProvider roleProvider = new OffRoad.Provider.RoleProvider();

        // GET: Events
        [AllowAnonymous]
        public ActionResult Index()
        {
            User user = db.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            if(user == null)
            {
                ViewBag.Role = 4;
            }
            else
            {                
                Roles role = roleProvider.GetRoleForUser(user);
                ViewBag.Role = role.Id;
            }
            return View(db.Events.OrderBy(b => b.BeginDate).ToList());
        }

        // GET: Events/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            if(user == null)
            {
                ViewBag.Role = 4;
            }
            else
            {
                ViewBag.UserId = user.Id;
                var roleProvider = new OffRoad.Provider.RoleProvider();
                var role = roleProvider.GetRoleForUserId(user.Id);
                ViewBag.Role = role.Id;
            }
            Event evenement = db.Events.Find(id);

            if (evenement == null)
            {
                return View("Error");
            }

            ViewBag.Comments = eventM.GetCommentairesForEvent(evenement.Id);
            return View(evenement);
        }

        [AuthorizeAdminFilter]
        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,BeginDate,EndDate,CreateDate,Place,Owner")] Event evenement)
        {
            User user = db.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            ModelState.Remove("Owner");
            if (evenement.BeginDate > evenement.EndDate)
            {
                ModelState.AddModelError("", "La date de début est supérieure à la date de fin de l'évenement");
                return View(evenement);
            }
            if (evenement.BeginDate < DateTime.Now)
            {
                ModelState.AddModelError("", "La date de début de l'evenement ne peut pas être antérieure à la date du jour");
                return View(evenement);
            }
            if (evenement.EndDate < DateTime.Now)
            {
                ModelState.AddModelError("", "La date de fin de l'evenement ne peut pas être antérieure à la date du jour");
                return View(evenement);
            }
            if (ModelState.IsValid)
            {
                evenement.Owner = user;
                evenement.CreateDate = DateTime.Now;
                db.Events.Add(evenement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evenement);
        }

        [AuthorizeAdminFilter]
        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event evenement = db.Events.Find(id);
            if (evenement == null)
            {
               return View("Error");
            }

            //Récupération de la date de création
            createDateSave = evenement.CreateDate;

            return View(evenement);
        }

        // POST: Events/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CreateDate,BeginDate,EndDate,Place")] Event evenement)
        {
            User user = db.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            evenement.Owner = user;
            evenement.EditDate = DateTime.Now;
            if (evenement.BeginDate > evenement.EndDate)
            {
                ModelState.AddModelError("", "La date de début est supérieure à la date de fin de l'évenement");
                return View(evenement);
            }
            if (ModelState.IsValid)
            {
                evenement.CreateDate = createDateSave;
    
                db.Entry(evenement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = evenement.Id});
            }
            return View(evenement);
        }

        [AuthorizeAdminFilter]
        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event evenement = db.Events.Find(id);
            if (evenement == null)
            {
                return View("Error");
            }
            else
            {
               return View(evenement);
            }            
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "Id,Text,Utilisateur,Date")] EventComment commentaires, int idEvent)
        {
            User user = authM.GetCurrentUser(HttpContext.User.Identity.Name);
            User userToSave = db.Users.Find(user.Id);
            if (user == null)
            {
              
                return HttpNotFound();
            }
            commentaires.User = userToSave;
            commentaires.CreateDate = DateTime.Now;
            if (idEvent != 0)
                commentaires.Event = db.Events.Find(idEvent);
            else
            {
               return View("Error");
            }
            db.EventComment.Add(commentaires);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = idEvent });
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event evenement = db.Events.Find(id);
            db.Events.Remove(evenement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
