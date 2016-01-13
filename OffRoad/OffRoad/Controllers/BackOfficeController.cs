using OffRoad.Context;
using OffRoad.Filtre;
using OffRoad.Models;
using OffRoad.Provider;
using OffRoad.Methodes;
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

        BackOfficeMethode BOC = new BackOfficeMethode();
        AuthMethode AM = new AuthMethode();

        // GET: BackOffice
        public ActionResult Index()
        {
            User currentUser = AM.GetCurrentUser(HttpContext.User.Identity.Name);
            Roles roleUser = roleProvider.GetRoleForUser(currentUser);
            ViewBag.RoleId = roleUser.Id;
            return View(db.Users.ToList());
        }

        //GET: BackOffice/MonCompte
        public ActionResult MonCompte()
        {
            User currentUser = AM.GetCurrentUser(HttpContext.User.Identity.Name);
            ViewBag.CurrentUSer = currentUser;
            return View();
        }

        [AuthorizeAdminFilter]
        // GET: BackOffice/Edit/5
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

        // POST: BackOffice/Edit/5
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

        // GET: BackOffice/EditOwnAccount/5
        public ActionResult EditOwnAccount(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: BackOffice/EditOwnAccount/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOwnAccount([Bind(Include = "Id,NickName,FirstName,LastName,Email,Birthday,City,Gender")] User user)
        {
            User userToEdit = db.Users.Find(user.Id);
            if (userToEdit == null)
            {
                return HttpNotFound();
            }
            userToEdit.FirstName = user.FirstName;
            userToEdit.Email = user.Email;
            userToEdit.LastName = user.LastName;
            userToEdit.Birthday = user.Birthday;
            userToEdit.City = user.City;
            userToEdit.Gender = user.Gender;

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
        public ActionResult DesativeUser(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(BOC.DeleteUser(id));
            }
        }
        

        public ActionResult Desactive(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
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
            User userToModify = BOC.DesactiveUser(user);
            db.Entry(userToModify).State = EntityState.Modified;
            db.SaveChanges();

            User get_user = AM.GetCurrentUser(HttpContext.User.Identity.Name);
            if (idUser == get_user.Id)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                return Redirect("/Home/Index");
            }
            else
                return RedirectToAction("Index");
        }
    }
}