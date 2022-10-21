using System;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ViewModels
{
    public class AdminViewModel
    {
        public List<Employee> allEmployees { get; set; }
    }
}