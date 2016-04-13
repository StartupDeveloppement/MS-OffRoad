using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OffRoad.Models
{
    public class ArticleComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        public string Text { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Article Article { get; set; }


    }
}