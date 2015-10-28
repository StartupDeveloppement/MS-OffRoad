using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using OffRoad.Models;
namespace OffRoad.Context
{
    public class DBContext : DbContext
    {
        public DBContext()
            :base("name=DefaultConnexion")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContext, Configuration>());
                
        }      

        public DbSet<User> Users {get;set;}
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    }
}