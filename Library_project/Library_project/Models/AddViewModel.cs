using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_project.Models
{
    public class AddViewModel
    {
        
        public string FirstName { get; set; }

     
        public string LastName { get; set; }

     
        public DateTime Birthdate { get; set; }

       
        public DateTime HireDate { get; set; }

        
        public double Salary { get; set; }

        
        public string Address { get; set; }

        
        public string Phone { get; set; }
        public string Photo { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string UserType { get; set; }

        public string WorkerID { get; set; }
    }
}