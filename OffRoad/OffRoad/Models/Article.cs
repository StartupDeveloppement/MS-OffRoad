using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OffRoad.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Corps")]
        public string Text { get; set; }

        [Display(Name = "Date de création")]
        public Nullable<DateTime> CreateDate { get; set; }

        [Display(Name = "Date de modification")]
        public Nullable <DateTime>EditDate { get; set; }

        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string PicturePath { get; set; }

        [Display(Name = "Catégorie")]
        public virtual Category Category { get; set; }

        [Display(Name = "Auteur")]
        public virtual User Author { get; set; }

    }
}