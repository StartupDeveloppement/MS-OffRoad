using OffRoad.Context;
using OffRoad.Methodes;
using OffRoad.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OffRoad.Controllers
{
    public class CommentairesController : Controller
    {
        private ArticleMethode artM = new ArticleMethode();
        private EventMethode eventM = new EventMethode();
        private CommentaireMethodes comM = new CommentaireMethodes();
        private DBContext db = new DBContext();

        // GET: Commentaires
        public ActionResult Edit(int id, int idCom, string type)
        {
            User user = db.Users.FirstOrDefault(u => u.NickName == User.Identity.Name);
            if (user == null)
            {
                return View("Error");
            }
            else
            {
                if (type == null || id == 0 || idCom == 0)
                {
                    return View("Error");
                }
                if (type == "Evenements")
                {
                    EventComment mod = new EventComment();
                    var evenement = db.Events.Find(id);
                    if (evenement == null)
                    {
                        return View("Error");
                    }
                    mod = comM.GetCommentaireForEvent(idCom, db);
                    var roleProvider = new OffRoad.Provider.RoleProvider();
                    var role = roleProvider.GetRoleForUserId(user.Id);
                    if (role.Id == 1 || mod.User.Id == user.Id)
                    {
                        Commentaire com = new Commentaire();
                        com.CreateDate = mod.CreateDate;
                        com.Text = mod.Text;
                        com.IdUser = mod.User.Id;
                        com.IdCommentaire = mod.Id;
                        com.IdType = mod.Event.Id;
                        com.Type = "Evenements";
                        return View(com);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    ArticleComment mod = new ArticleComment();
                    var article = db.Articles.Find(id);
                    if (article == null)
                    {
                        return View("Error");
                    }
                    mod = comM.GetCommentaireForArticle(idCom, db);
                    Commentaire com = new Commentaire();
                    com.CreateDate = mod.CreateDate;
                    com.Text = mod.Text;
                    com.IdUser = mod.User.Id;
                    com.IdCommentaire = mod.Id;
                    com.IdType = mod.Article.Id;
                    com.Type = "Article";
                    return View(com);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCommentaire,Text,CreateDate,Type,IdUser,IdType")] Commentaire commentaire)
        {
            if (commentaire == null)
            {
                return View("Error");
            }
            else
            {
                if (commentaire.Type == "Article")
                {
                    ArticleComment art = db.ArticleComments.Find(commentaire.IdCommentaire);
                    art.Text = commentaire.Text;
                    art.User = db.Users.Find(commentaire.IdUser);
                    art.Article = db.Articles.Find(commentaire.IdType);
                    db.Entry(art).State = EntityState.Modified;
                    db.SaveChanges();                  
                    return RedirectToAction("Details", "Articles", new { id = art.Article.Id});
                }
                else
                {
                    EventComment eve = db.EventComment.Find(commentaire.IdCommentaire);
                    eve.Text = commentaire.Text;
                    eve.User = db.Users.Find(commentaire.IdUser);
                    eve.Event = db.Events.Find(commentaire.IdType);
                    db.Entry(eve).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "Events", new { id = eve.Event.Id });
                }
            }
        }

        public ActionResult Delete (int id, string type)
        {
            int idRetour = 0;
            if (type == "Article")
            {
                ArticleComment articleCom = db.ArticleComments.Find(id);
                idRetour = articleCom.Article.Id;
                db.ArticleComments.Remove(articleCom);
                db.SaveChanges();
                return RedirectToAction("Details", "Articles", new { id = idRetour });
            }
            else
            {
                EventComment eveCom = db.EventComment.Find(id);
                idRetour = eveCom.Event.Id;
                db.EventComment.Remove(eveCom);
                db.SaveChanges();
                return RedirectToAction("Details", "Events", new { id = idRetour });
            }

        }
    }
}