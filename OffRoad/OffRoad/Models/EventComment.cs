using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    public class EventComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Event Event { get; set; }


    }
}