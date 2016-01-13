﻿using System;
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
    public class ArticlesController : Controller
    {
        private DBContext db = new DBContext();
        private static DateTime? createDateSave = DateTime.MinValue;

        #region Action
        [AllowAnonymous]
        // GET: Articles
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        [AllowAnonymous]
        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        [AuthorizeRedacFilter]
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categorys.ToList();
            return View();
        }

        // POST: Articles/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,Title,PicturePath")] Article article)
        {
            User user = db.Users.FirstOrDefault(u => u.NickName == HttpContext.User.Identity.Name );
            
            //Récupération de la catégory
            var categoryForm = Request.Form["Category"];
            int idCategory = Int32.Parse(categoryForm);
            var requete = from b in db.Categorys
                          where b.Id.Equals(idCategory)
                          select b;
            Category category = requete.FirstOrDefault();

            //Persistance des données non requises
            article.Author = user;
            article.CreateDate = DateTime.Now;
            article.Category = category;

            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(article);
        }

        [AuthorizeRedacFilter]
        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            //Récupération de la date de création
            createDateSave = article.CreateDate;

            return View(article);
        }

        // POST: Articles/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,CreateDate,Title,PicturePath")] Article article)
        {
            article.EditDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                article.CreateDate = createDateSave;
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        [AuthorizeRedacFilter]
        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

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
