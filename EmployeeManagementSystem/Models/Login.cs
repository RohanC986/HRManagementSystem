﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class Login
    {
        public int LoginId { get; set; }
        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LastLogin { get; set; }
        public bool IsActive { get; set; }
    }
}