using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    //**** Modele des roles ****//
    public class Roles {
        [Key] 
        public int Id { get; set; }
        public string Label { get; set; }
    }
}