using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class Login
    {
        public int LoginId { get; set; }

        [Required(ErrorMessage ="Employee is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage ="User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public bool IsActive { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
    }
}