using OffRoad.Context;
using OffRoad.Models;
using OffRoad.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OffRoad.Controllers
{
    public class AuthController : Controller
    {
        private DBContext db = new DBContext();
        private OffRoad.Provider.RoleProvider roleProvider = new OffRoad.Provider.RoleProvider();

        #region Actions
        // GET: Auth
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogIn viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = Authentifier(viewModel.NickName, viewModel.PassWord);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.NickName, false);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("LoginError", "Mail et/ou mot de passe incorrect(s)");
                return RedirectToAction("LogIn");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register user)
        {
            if (ModelState.IsValid)
            {
                // State = 0 Si tout est Ok , State = 1 si probleme détécté
                int state = 0;
                string err = "";

                //**** Tests de la validité des champs d'inscription ****//
                if (!user.Password.Equals(user.PasswordConfirmation))
                {
                    state = 1;
                    err += "- Les mots de passes ne sont pas égaux, veuillez saisir des mots de passes identiques<br/>";
                }

                if (!user.Email.Equals(user.EmailConfirmation))
                {
                    state = 1;
                    err += "- Les adresses mails ne sont pas égales, veuillez saisir des adresses mails identiques<br/>";

                }
                else
                {
                    if (!IsExistEmail(user.Email))
                    {
                        state = 1;
                        err += "- L'adresse mail est déjà utilisée<br/>";
                    }
                }

                if (!IsExistNickname(user.NickName))
                {
                    state = 1;
                    err += "- Le pseudo est déjà utilisé<br/>";
                }

                if (state != 0)
                {
                    ModelState.AddModelError("RegisterError", err);
                    return View();
                }

                //**** Création d'un utilisateur et initialisation d'un role ****//
                try
                {
                    User newuser = AjouterUtilisateur(user.LastName, user.FirstName, user.Password, user.Email, user.NickName);
                    UserRole userrole = new UserRole { IdUser = newuser, Roles = db.Roles.Find(1) };
                    // db.UserRole.Add(userrole);
                    //  db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("RegisterError", ex.Message + " " + ex.InnerException);
                    return View();
                }
            }

            return RedirectToAction("LogIn");
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Création de l'utilisateur dans la table USER
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="nickname"></param>
        /// <returns>Utilisateur crée</returns>
        public User AjouterUtilisateur(string lastName, string firstName, string password, string email, string nickname)
        {
            string pwHash;

            pwHash = PasswordHash.CreateHash(password);

            User utilisateur = new User { FirstName = firstName, LastName = lastName, Email = email, Password = pwHash, NickName = nickname };
            utilisateur.Birthday = DateTime.Now;
            utilisateur.Active = true;
            var userRoleRequete = from b in db.UserRole
                           where b.IdUser.Id.Equals(utilisateur.Id)
                           select b;
            UserRole userRoleVerify = userRoleRequete.FirstOrDefault();
            if (userRoleVerify == null)
            {
                UserRole userRole = new UserRole();
                userRole.IdUser = utilisateur;
                userRole.Roles = db.Roles.Find(3);
                db.UserRole.Add(userRole);
            }
            db.Users.Add(utilisateur);
            db.SaveChanges();
            return utilisateur;
        }

        /// <summary>
        /// Verification de l'existance du Mail 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Vrai si existant, sinon faux </returns>
        public bool IsExistEmail(string email)
        {

            User userToTest = db.Users.FirstOrDefault(u => u.Email == email);
            if (userToTest == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Verification de l'existance du Pseudo
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns>Vrai si existant, sinon faux</returns>
        public bool IsExistNickname(string nickname)
        {

            User userToTest = db.Users.FirstOrDefault(u => u.NickName == nickname);
            if (userToTest == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Test de login valide
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Retourne user si existe sinon null</returns>
        public User Authentifier(string nickname, string password)
        {
            User user = db.Users.FirstOrDefault(u => u.NickName == nickname);

            if (!user.Active || user == null)
            {
                return null;
            }
            else
            {
                PasswordHash.ValidatePassword(password, user.Password);
                return user;
            }

        }

        #endregion
    }
}