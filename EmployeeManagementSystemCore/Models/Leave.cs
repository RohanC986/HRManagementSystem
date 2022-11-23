using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class Leave
    {
        public int LeaveId { get; set; }

        public int EmployeeId { get; set; }
        public int LeavesAccrued { get; set; }
        public int LeavesTaken { get; set; }
        public int BalanceLeaves { get; set; }
        public int UnPaidLeaves { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public int HalfDay { get; set; }

    }
}