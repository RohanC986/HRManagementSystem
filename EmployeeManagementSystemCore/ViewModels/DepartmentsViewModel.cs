﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class DepartmentsViewModel
    {
        public int ProjectHeadEmployeeId { get; set; }
        public string ProjectName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int TotalMembers { get; set; }
        public int EmployeesOnLeave { get; set; }
       
    }



}