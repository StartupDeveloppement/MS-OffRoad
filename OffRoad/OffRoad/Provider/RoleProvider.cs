using OffRoad.Context;
using OffRoad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffRoad.Provider
{
    public class RoleProvider
    {
        private DBContext db = new DBContext();
        public Roles GetRoleById(int idRole)
        {
            return db.Roles.Find(idRole);
        }

        public Roles GetRoleForUser(User user) 
        {
            var requeteRoleForUser = from b in db.UserRole 
                                     where b.IdUser.Id.Equals(user.Id) 
                                     select b.Roles;
            return requeteRoleForUser.First(); 
        }
        public int GetRoleForUserNickName(string nickName)
        {
            var requeteUser = from b in db.Users
                              where b.NickName.Equals(nickName)
                              select b;
            var requeteRoleForUser = from b in db.UserRole
                                     where b.IdUser.Id.Equals(requeteUser.First().Id)
                                     select b.Roles.Id;
            return requeteRoleForUser.First();
        }

        public Roles GetRoleForUserId(int userId)
        {
            var requeteRoleForUser = from b in db.UserRole
                                     where b.IdUser.Id.Equals(userId)
                                     select b.Roles;
            return requeteRoleForUser.First();
        }
    }
}