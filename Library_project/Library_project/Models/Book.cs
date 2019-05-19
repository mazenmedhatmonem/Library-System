using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library_project.Models
{
    public class Book
    {
        [Key]
        public int ISBN { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Category { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Author { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string publisher { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        public DateTime PublishingDate { get; set; }

        [Required]
        public string Edition { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Pages { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int NoOfCopy { get; set; }

        [Required]
        public bool Available { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ShelfNumber { get; set; }

        public bool IsNewArrived { get; set; }
    }
}