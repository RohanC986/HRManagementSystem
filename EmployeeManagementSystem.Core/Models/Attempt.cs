using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class Attempt
    {
        public int AttemptsId { get; set; }
        public int EmployeeId { get; set; }
        public int Attempts { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
    }
}