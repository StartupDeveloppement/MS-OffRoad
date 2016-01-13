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

namespace OffRoad.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private DBContext db = new DBContext();
        private static DateTime? createDateSave = DateTime.MinValue;

        [AllowAnonymous]
        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        [AllowAnonymous]
        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event evenement = db.Events.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            ViewBag.Comments = GetCommentairesForEvent(evenement.Id);
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
        public ActionResult Create([Bind(Include = "Id,Title,Description,BeginDate,EndDate,Place,Owner")] Event evenement)
        {
            User user = db.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            ModelState.Remove("Owner");
            if (evenement.BeginDate > evenement.EndDate)
            {
                ModelState.AddModelError("", "La date de début est supérieure à la date de fin de l'évenement");
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
                return HttpNotFound();
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
            evenement.EditDate = DateTime.Now;
            if (evenement.BeginDate > evenement.EndDate)
            {
                ModelState.AddModelError("", "La date de début est supérieure à la date de fin de l'évenement");
                return View(evenement);
            }
            if (ModelState.IsValid)
            {
                evenement.CreateDate = createDateSave;
                evenement.Owner = user;
                db.Entry(evenement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
                return HttpNotFound();
            }
            return View(evenement);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "Id,Text,Utilisateur,Date")] EventComment commentaires, int idEvent)
        {
            User user = GetCurrentUser();
            if (user == null)
            {
                return HttpNotFound();
            }
            commentaires.User = user;
            commentaires.CreateDate = DateTime.Now;
            if (idEvent != 0)
                commentaires.Event = GetEventById(idEvent);
            else
            {
                return RedirectToAction("Error", "Shared");
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

        public User GetCurrentUser()
        {
            var nickName = HttpContext.User.Identity.Name;
            var userRequete = from b in db.Users
                              where b.NickName.Equals(nickName)
                              select b;
            return userRequete.FirstOrDefault();
        }

        public Event GetEventById(int idEvent)
        {
            var requeteEvent = from b in db.Events
                               where b.Id.Equals(idEvent)
                               select b;

            return requeteEvent.FirstOrDefault();
        }

        public List<EventComment> GetCommentairesForEvent(int idEvent)
        {
            var requeteCommentaires = from b in db.EventComment
                                      where b.Event.Id == idEvent
                                      select b;
            return requeteCommentaires.ToList<EventComment>();
        }
    }
}
