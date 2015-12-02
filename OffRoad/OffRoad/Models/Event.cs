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
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Description { get; set; }

        [Display(Name = "Date de création")]
        public Nullable<DateTime> CreateDate { get; set; }

        [Display(Name = "Date de modification")]
        public Nullable<DateTime> EditDate { get; set; }

        [Display(Name = "Date de début")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> BeginDate { get; set; }

        [Display(Name = "Date de fin")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> EndDate { get; set; }

        [Display(Name = "Lieu")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Place { get; set; }

        public virtual User Owner { get; set; }

    }
}