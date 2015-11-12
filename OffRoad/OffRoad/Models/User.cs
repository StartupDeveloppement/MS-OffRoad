using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    //**** Modele des utilisateurs ****//
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [Display(Name = "Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Pseudo")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string NickName { get; set; }

        [Display(Name = "Date de naissance")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Ville")]
        public string City { get; set; }

        [Display(Name = "Sexe")]
        public int Gender { get; set; }

        public Avatar Avatar { get; set; }
    }
}