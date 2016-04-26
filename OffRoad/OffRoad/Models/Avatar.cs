using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffRoad.Models
{
    public class Avatar
    {
        [Key]
        public int Id { get; set; }

        public byte[] Image { get; set; }
    }
}