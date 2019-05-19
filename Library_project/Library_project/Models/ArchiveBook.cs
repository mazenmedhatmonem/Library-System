using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library_project.Models
{
    public class ArchiveBook
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("book")]
        public int ISBN { get; set; }

        public Book book { get; set; }

        [ForeignKey("User")]

        public string UserID { get; set; }

        public ApplicationUser User { get; set; }

        public bool isBorrowed { get; set; }

        public bool isRead { get; set; }

    }
}