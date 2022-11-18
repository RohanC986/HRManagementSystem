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
        [RegularExpression("^\\\\S+@\\\\S+\\\\.\\\\S+$", ErrorMessage = "Username should be in mail Id format")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage ="Password should be Atleast 1 caps,1 special char,1 number and min 8 in length")]
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public bool IsActive { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
    }
}