using System;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeIdNameViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public List<EmployeeIdNameViewModel> EmployeeIdNameList { get; set; }
    }
}