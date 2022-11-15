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
        public List<GetTeamLeaveRequestViewModel> DataTabletoLeaveRequestViewModel(DataTable dt)
        {
            List<GetTeamLeaveRequestViewModel> getTeamLeaveRequestViewModels = new List<GetTeamLeaveRequestViewModel>();
            getTeamLeaveRequestViewModels = (from DataRow dr in dt.Rows
                                             select new GetTeamLeaveRequestViewModel
                                             {
                                                 LeaveRequestId = Convert.ToInt32(dr["LeaveRequestId"]),
                                                 EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                                                 FirstName = dr["FirstName"].ToString(),
                                                 LastName = dr["LastName"].ToString(),
                                                 isHalfDay = Convert.ToBoolean(dr["isHalfDay"]),
                                                 LeaveType = dr["LeaveType"].ToString(),
                                                 Reason = dr["Reason"].ToString(),
                                                 LengthOfLeave = Convert.ToInt32(dr["LengthOfLeave"]),
                                                 StartDate = dr["StartDate"].ToString(),
                                                 EndDate = dr["EndDate"].ToString(),
                                                 Status = dr["Status"].ToString(),

                                             }

                ).ToList();
            return getTeamLeaveRequestViewModels;
        }
    }
}