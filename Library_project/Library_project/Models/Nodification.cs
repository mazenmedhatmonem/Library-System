using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_project.Models
{
    public class Nodification
    {
        [Key]
        public int Nodification_ID { get; set; }

        [Required]
        public string Message { get; set; }
        [Required]
        public string Role { get; set; }


    }
}