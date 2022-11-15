using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class LeaveType
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
    }
}