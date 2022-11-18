using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class TeamEmpDetailsViewModel 
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DesignationName { get; set; }
        public string ProjectName { get; set; }

        public List<TeamEmpDetailsViewModel> teamEmps { get; set; }


    }
}