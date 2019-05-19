using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_project.Models
{
    public class Member
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), ForeignKey("User")]
        public string MemberID { get; set; }

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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Phone { get; set; }
        public string Photo { get; set; }
    }
}