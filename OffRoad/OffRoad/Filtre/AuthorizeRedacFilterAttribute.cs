using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OffRoad.Models;
using OffRoad.Context;
using OffRoad.Provider;

namespace OffRoad.Filtre
{
    public class AuthorizeRedacFilterAttribute : AuthorizeAttribute
    {
        private DBContext db = new DBContext();
        private RoleProvider roleProvider = new RoleProvider();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            User user = GetCurrentUser();
            int role = roleProvider.GetRoleForUser(user).Id;
            if (role != 1 && role != 2)
            {
                filterContext.Result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
            }
            base.OnAuthorization(filterContext);
        }

        public User GetCurrentUser()
        {
            var nickName = HttpContext.Current.User.Identity.Name;

            var userRequete = from b in db.Users
                              where b.NickName.Equals(nickName)
                              select b;
            return userRequete.FirstOrDefault();
        }
    }
}