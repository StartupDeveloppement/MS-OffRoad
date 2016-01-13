using OffRoad.Context;
using OffRoad.Filtre;
using OffRoad.Models;
using OffRoad.Provider;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OffRoad.Controllers
{
    public class BackOfficeController : Controller
    {
        private DBContext db = new DBContext();
        private RoleProvider roleProvider = new RoleProvider();

        // GET: BackOffice
        public ActionResult Index()
        {
            User currentUser = GetCurrentUser();
            Roles roleUser = roleProvider.GetRoleForUser(currentUser);
            ViewBag.RoleId = roleUser.Id;
            return View(db.Users.ToList());

        }

        public ActionResult MonCompte()
        {
            User currentUser = GetCurrentUser();
            ViewBag.CurrentUSer = currentUser;
            return View();
        }

        [AuthorizeAdminFilter]
        // GET: Articles/Edit/5
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            Roles role = roleProvider.GetRoleForUserId(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Roles = db.Roles.ToList();
            ViewBag.UserRole = role.Id;
            return View(user);
        }

        // POST: Articles/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NickName,FirstName,LastName,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Articles/Edit/5
        public ActionResult EditOwnAccount(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Articles/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOwnAccount([Bind(Include = "Id,NickName,FirstName,LastName,Email")] User user)
        {
            User userToEdit = db.Users.Find(user.Id);
            if (userToEdit == null)
            {
                return HttpNotFound();
            }
            userToEdit.FirstName = user.FirstName;
            userToEdit.Email = user.Email;
            userToEdit.LastName = user.LastName;
            db.Entry(userToEdit).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MonCompte");
        }

        public ActionResult SaveRole()
        {
            var idStringUser = Request.Form["id"];
            int idUserToModify = int.Parse(idStringUser);
            User userToModify = db.Users.Find(idUserToModify);
            var newRoleString = Request.Form["Role"];
            int idNewRole = Int32.Parse(newRoleString);
            var requete = from b in db.UserRole
                          where b.IdUser.Id.Equals(idUserToModify)
                          select b;
            UserRole userRole = requete.FirstOrDefault();
            Roles newRole = db.Roles.Find(idNewRole);
            userRole.Roles = newRole;
            db.Entry(userRole).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [AuthorizeAdminFilter]
        public ActionResult Desactive(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            int role = roleProvider.GetRoleForUser(user).Id;
            var requete = from b in db.UserRole
                          where b.IdUser.Id.Equals(user.Id)
                          select b;
            UserRole userRole = requete.FirstOrDefault();
            return View(user);
        }

        public ActionResult DesactiveAccountUser()
        {
            var idUserString = Request.Form["id"];
            int idUser = Int32.Parse(idUserString);
            User user = db.Users.Find(idUser);
            var requeteRole = from b in db.UserRole
                              where b.IdUser.Id.Equals(idUser)
                              select b;
            UserRole role = requeteRole.FirstOrDefault();
            db.UserRole.Remove(role);
            User userToModify = DesactiveUser(user);
            db.Entry(userToModify).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public User DesactiveUser(User user)
        {
            user.Active = false;
            user.FirstName = "null";
            user.LastName = "null";
            user.Password = "null";
            user.Birthday = DateTime.Now;
            user.City = "null";
            user.Email = "null";
            user.Gender = 0;
            user.Avatar = null;
            return user;
        }

        public User GetCurrentUser()
        {
            var nickName = HttpContext.User.Identity.Name;
            var userRequete = from b in db.Users
                              where b.NickName.Equals(nickName)
                              select b;
            return userRequete.FirstOrDefault();
        }
        public User GetCurrentUserById(int idUser)
        {
            var userRequete = from b in db.Users
                              where b.Id.Equals(idUser)
                              select b;
            return userRequete.FirstOrDefault();
        }
    }
}