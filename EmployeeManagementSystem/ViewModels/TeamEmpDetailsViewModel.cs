using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ViewModels
{
    public class TeamEmpDetailsViewModel 
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string ProjectName { get; set; }

        public List<TeamEmpDetailsViewModel> teamEmps { get; set; }


    }
}