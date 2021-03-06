﻿using OffRoad.Context;
using OffRoad.Models;
using OffRoad.Provider;
using OffRoad.Methodes;
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
        private AuthMethode AM = new AuthMethode();
        private MailMethode MailM = new MailMethode();
        private const string register = "Inscription sur le site de l'association OffRoad";

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
                User user = AM.Authentifier(viewModel.NickName, viewModel.PassWord);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.NickName, false);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("LoginError", "Pseudo et/ou mot de passe incorrect(s)");
                return View("LogIn");
            }
            else
            {
               return View("Error");
            }
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
                    if (AM.IsExistEmail(user.Email))
                    {
                        state = 1;
                        err += "- L'adresse mail est déjà utilisée<br/>";
                    }
                }

                if (!AM.IsExistNickname(user.NickName))
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
                    User newuser = AM.AjouterUtilisateur(user.LastName, user.FirstName, user.Password, user.Email, user.NickName);
                    UserRole userrole = new UserRole { IdUser = newuser, Roles = db.Roles.Find(1) };
                    string message =  "Bonjour "+newuser.NickName+",<br/> Vous venez de vous inscrire au site web de l'association OffRoad. <br/> Vous pouvez vous connecter à notre application via le lien suivant : http://offroad.com. <br/> Pour vous connecter il vous suffit d'utiliser votre adresse mail '"+newuser.Email+"' et votre mot de passe <br/> Si vous avez des questions, n'hesitez pas à nous contacter par mail à l'adresse suivante : <br/> offraoddev@gmail.com <br/><br/> Cordialement <br/> Equipe OffRoad";
                    //MailM.SendMail(newuser.Email, register, message);
                    return RedirectToAction("Index", "Home");
                }
                catch 
                {
                   return View("Error");
                }
            }

            return RedirectToAction("LogIn");
        }

        [HttpGet]
        public ActionResult Password()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _Password(string mail)
        {
            AuthMethode AM = new AuthMethode();
            MailMethode MM = new MailMethode();

            if (AM.IsExistEmail(mail))
            {
                string password = AM.GeneratePassword(8);
                string pwHash = PasswordHash.CreateHash(password);

                User user = AM.GetUserByMail(mail);

                AM.UpdatePassword(user, pwHash);
                try
                {
                    MM.SendMail(mail, "[OffRoad] Nouveau mot de passe", "Bonjour " + user.NickName + ",<br/> Vous avez sollicité un nouveau mot de passe afin d'accéder à votre compte sur le site offroad.com <br/> Voici votre nouveau mot de passe : " + password + " <br/> N'hésitez pas à le changer directement sur notre site espace 'Mon Compte' <br/> <br/> Cordialement <br/> Equipe OffRoad");
                    ViewBag.Message = "Nouveau mot de passe envoyer par mail!";
                }
                catch
                {
                   return View("Error");
                }
            }
            else
            {
                ViewBag.Message = "Mail non valide";
            }

            return PartialView();
        }

        [HttpGet]
        public ActionResult _Password()
        {
            return View();
        }
        #endregion
    }
}