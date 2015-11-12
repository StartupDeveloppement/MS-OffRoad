using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Corps")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Date de création")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        public string PicturePath { get; set; }

        [Required]
        [Display(Name = "Catégorie")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Auteur")]
        public User Author { get; set; }

    }
}