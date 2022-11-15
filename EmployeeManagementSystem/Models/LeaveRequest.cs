using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }

        [Required(ErrorMessage ="Employee is required")]
        public int EmployeeId { get; set; }

        public bool IsHalfday { get; set; }

        [Required(ErrorMessage = "Leave Type is required")]
        public string LeaveType { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Length of Leave is required")]
        public int LengthOfLeave { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
    }
}