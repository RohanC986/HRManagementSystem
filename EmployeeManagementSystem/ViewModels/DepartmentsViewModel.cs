using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ViewModels
{
    public class DepartmentsViewModel
    {
        public string ProjectName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int TotalMembers { get; set; }
        public int EmployeesOnLeave { get; set; }
        

        
    }
}