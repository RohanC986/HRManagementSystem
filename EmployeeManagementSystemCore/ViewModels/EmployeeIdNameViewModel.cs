using System;
using EmployeeManagementSystemCore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class EmployeeIdNameViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public List<EmployeeIdNameViewModel> EmployeeIdNameList { get; set; }
    }
}