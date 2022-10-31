using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ViewModels
{
    public class GetTeamLeaveRequestViewModel
    {
        public int LeaveRequestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isHalfDay { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }


        public int LengthOfLeave { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }

        public List<GetTeamLeaveRequestViewModel> getTeamLeaveRequestViewModels { get; set; }



    }
}