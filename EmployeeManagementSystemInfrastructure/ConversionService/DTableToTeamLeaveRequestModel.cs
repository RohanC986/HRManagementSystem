using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToTeamLeaveRequestModel
    {

        //Returns List of Class TeamEmpDetailsViewModel after Converting data from DataTable to List

        public List<GetTeamLeaveRequestViewModel> DataTabletoLeaveRequestViewModel(DataTable dt)
        {
            List<GetTeamLeaveRequestViewModel> getTeamLeaveRequestViewModels = new List<GetTeamLeaveRequestViewModel>();
            getTeamLeaveRequestViewModels = (from DataRow dr in dt.Rows
                                             select new GetTeamLeaveRequestViewModel
                                             {
                                                 LeaveRequestId = Convert.ToInt32(dr["LeaveRequestId"]),
                                                 EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                                                 FirstName = Convert.ToString(dr["FirstName"]),
                                                 LastName = Convert.ToString(dr["LastName"]),
                                                 isHalfDay = Convert.ToBoolean(dr["isHalfDay"]),
                                                 LeaveType = Convert.ToString(dr["LeaveType"]),
                                                 Reason = Convert.ToString(dr["Reason"]),
                                                 LengthOfLeave = Convert.ToInt32(dr["LengthOfLeave"]),
                                                 StartDate = DateTime.Parse(dr["StartDate"].ToString()),
                                                 EndDate = DateTime.Parse(dr["EndDate"].ToString()),
                                                 Status = Convert.ToString(dr["Status"]),

                                             }

                ).ToList();
            return getTeamLeaveRequestViewModels;
        }
    }
}