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

        [Required]
        public string Text { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public User Owner { get; set; }

    }
}