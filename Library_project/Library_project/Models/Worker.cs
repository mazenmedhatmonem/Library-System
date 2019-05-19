using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_project.Models
{
    public class Worker
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), ForeignKey("User")]
        public string WorkerID { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid Hiredate in the format dd/mm/yyyy")]
        public DateTime Birthdate { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid birthdate in the format dd/mm/yyyy")]
        public DateTime HireDate { get; set; }

        [Range(1000, double.MaxValue, ErrorMessage = "Please enter valid Double Number")]
        public double Salary { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Phone { get; set; }
        public string Photo { get; set; }

        
        public string UserType { get; set; }
    }
}