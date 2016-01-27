using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    //**** Model email ****//
    public class Email
    {
        [Required]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Téléphone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Mail { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}