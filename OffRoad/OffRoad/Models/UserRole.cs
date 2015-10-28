using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    //**** Modele de la liaison utilisateur/Role ****//
    public class UserRole 
    {
        [Key] 
        public int Id { get; set; } 
        public User IdUser { get; set; } 
        public Roles Roles { get; set; } }
}