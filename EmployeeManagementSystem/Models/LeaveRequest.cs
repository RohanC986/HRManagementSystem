using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsHalfday { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }
        public int LengthOfLeave { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}