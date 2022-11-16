using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class LoginViewModel
    {
        public  int? EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int IsActive { get; set; }   
        public string LoginMessage { get; set; }    
        public List<LoginViewModel> loginViewModels { get; set; }   
        public Dictionary<string, object> EmployeeDict { get; set; }

    }
}