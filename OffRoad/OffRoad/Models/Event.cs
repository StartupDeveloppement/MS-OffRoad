using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Titre")]
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public Nullable<DateTime> CreateDate { get; set; }

        [Display(Name = "Date de début")]
        [Required]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Date de fin")]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "Lieu")]
        [Required]
        public string Place { get; set; }

        [Required]
        public virtual User Owner { get; set; }

    }
}