using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    public class EventUser
    {
        [Key]
        public int Id { get; set; }

        public User Participant { get; set; }

        public Event Event { get; set; }
    }
}