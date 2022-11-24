using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class GetTeamLeaveRequestViewModel
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isHalfDay { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }


        public int LengthOfLeave { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        
    

        public List<GetTeamLeaveRequestViewModel> getTeamLeaveRequestViewModels { get; set; }



    }
}