﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int ProjectHeadEmployeeId { get; set; }
        public string ProjectName { get; set; }
    }
}