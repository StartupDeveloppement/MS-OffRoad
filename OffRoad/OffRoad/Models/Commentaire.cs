using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OffRoad.Models
{
    public class Commentaire
    {
        [Required]
        public int IdCommentaire { get; set; }

        [Required]
        [AllowHtml]
        public string Text { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int IdUser { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int IdType { get; set; }


    }
}