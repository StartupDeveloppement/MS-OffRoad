using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    //**** Modele de la connexion ****//
    public class LogIn
    {
        [Required]
        [Display(Name = "Pseudo")]
        public string NickName { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        public string PassWord { get; set; }
    }
}