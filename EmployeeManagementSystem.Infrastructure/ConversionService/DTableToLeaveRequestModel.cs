using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
{
    public class DTableToLeaveRequestModel
    {
        public List<LeaveRequest> DataTabletoLeaveModel(DataTable dt)
        {
            List<LeaveRequest> leaves = new List<LeaveRequest>();
            leaves = (from DataRow dr in dt.Rows
                      select new LeaveRequest
                      {
                          LeaveRequestId = Convert.ToInt32(dr["LeaveRequestId"]),
                          IsHalfday = Convert.ToBoolean(dr["IsHalfday"]),
                          LeaveType = dr["LeaveType"].ToString(),
                          Reason = dr["Reason"].ToString(),
                          LengthOfLeave = Convert.ToInt32(dr["LengthOfLeave"]),
                          StartDate = dr["StartDate"].ToString(),
                          EndDate = dr["EndDate"].ToString(),
                          Status = dr["Status"].ToString(),
                          Created = dr["Created"].ToString(),
                          LastModified = dr["LastModified"].ToString()

                      }

                ).ToList();
            return leaves;

        }
    }
}