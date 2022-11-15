using System;
using EmployeeManagementSystemCore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class AdminViewModel
    {
        public List<Employee> allEmployees { get; set; }
    }
}