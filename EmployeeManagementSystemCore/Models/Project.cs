using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int ProjectHeadEmployeeId { get; set; }
        public string ProjectName { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }

        public List<Project> ProjectList { get; set; }
    }
}