using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToLeaveRequestModel
    {
        //Returns List of Class LeaveRequest after Converting data from DataTable to List

        public List<LeaveRequest> DataTabletoLeaveModel(DataTable dt)
        {
            List<LeaveRequest> leaves = new List<LeaveRequest>();
            leaves = (from DataRow dr in dt.Rows
                      select new LeaveRequest
                      {
                          LeaveRequestId = Convert.ToInt32(dr["LeaveRequestId"]),
                          IsHalfday = Convert.ToBoolean(dr["IsHalfday"]),
                          LeaveType = Convert.ToString(dr["LeaveType"]),
                          Reason = Convert.ToString(dr["Reason"]),
                          LengthOfLeave = Convert.ToInt32(dr["LengthOfLeave"]),
                          StartDate = DateTime.Parse(dr["StartDate"].ToString()),
                          EndDate = DateTime.Parse(dr["EndDate"].ToString()),
                          Status = Convert.ToString(dr["Status"]),
                          Created = Convert.ToString(dr["Created"]),
                          LastModified = Convert.ToString(dr["LastModified"])

                      }

                ).ToList();
            return leaves;

        }
    }
}