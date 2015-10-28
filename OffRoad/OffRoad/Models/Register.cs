using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    //**** Modele de l'inscription ****//
    public class Register
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Confirmation du mail")]
        public string EmailConfirmation { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirmation mot de passe")]
        public string PasswordConfirmation { get; set; }

        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Pseudo")]
        public string NickName { get; set; }
    }
}