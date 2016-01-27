using OffRoad.Context;
using OffRoad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OffRoad.Methodes
{
    public class AuthMethode
    {
        private  DBContext db = new DBContext();

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
        /// Test de login valide
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Retourne user si existe sinon null</returns>
        public User Authentifier(string nickname, string password)
        {
            User user = db.Users.FirstOrDefault(u => u.NickName == nickname);

            if (user == null || !user.Active )
            {
                return null;
            }
            else
            {
                PasswordHash.ValidatePassword(password, user.Password);
                return user;
            }
        }

        public User GetCurrentUser(string name)
        {
            var nickName = name;
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