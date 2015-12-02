﻿using System;
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
        public Category Category { get; set; }

        [Display(Name = "Auteur")]
        public User Author { get; set; }

    }
}