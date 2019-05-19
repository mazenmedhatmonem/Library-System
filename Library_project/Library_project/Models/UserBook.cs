using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_project.Models
{
    public class UserBook
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), ForeignKey("book")]
        [Column(Order = 0)]
        public int ISBN { get; set; }

        public Book book { get; set; }

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), ForeignKey("User")]

        [Column(Order = 1)]
        public string UserID { get; set; }

        public ApplicationUser User { get; set; }

        public bool isBorrowed { get; set; }

        public bool isRead { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid Date in the format dd/mm/yyyy")]
        public DateTime Date { get; set; }

        public bool IsReturned { get; set; }

        public int Duration { get; set; }
    }
}