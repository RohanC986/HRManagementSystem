using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class LoginViewModel
    {
        public  int? EmployeeId { get; set; }

        [Required(ErrorMessage = " UserName required")]
        [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "E-mail id is not valid")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,12}$", ErrorMessage = "Password is not valid")]

        public string Password { get; set; }
        public int RoleId { get; set; }
        public int IsActive { get; set; }   
        public string UsernameMessage { get; set; }
        public string PasswordMessage { get; set; }

        public List<LoginViewModel> loginViewModels { get; set; }   
        public Dictionary<string, object> EmployeeDict { get; set; }

    }
}