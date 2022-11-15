using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }

        public List<Role> RolesList { get; set; }
    }
}