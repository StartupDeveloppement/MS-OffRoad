using OffRoad.Context;
using OffRoad.Models;
using OffRoad.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffRoad.Methodes
{
    public class BackOfficeMethode
    {
        private DBContext db = new DBContext();
        private RoleProvider roleProvider = new RoleProvider();

        public UserRole DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            int role = roleProvider.GetRoleForUser(user).Id;
            var requete = from b in db.UserRole
                          where b.IdUser.Id.Equals(user.Id)
                          select b;
            UserRole userRole = requete.FirstOrDefault();
            return userRole;
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
    }
}