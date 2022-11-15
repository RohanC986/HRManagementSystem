using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class ProjectMembers
    {
        public int ProjectMembersId { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
    }
}