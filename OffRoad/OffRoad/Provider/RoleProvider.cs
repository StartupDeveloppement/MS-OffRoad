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
            var requeteRoleForUser = from b in db.UserRole where b.IdUser.Equals(user.Id) select b;
            return requeteRoleForUser.First().Roles; 
        }
    }
}