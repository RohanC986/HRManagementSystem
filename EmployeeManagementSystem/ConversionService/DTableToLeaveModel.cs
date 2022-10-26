using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
{
    public class DTableToLeaveModel
    {
        public List<Leave> DataTabletoLeaveModel(DataTable dt)
        {
            List<Leave> leaves = new List<Leave>();
            leaves = (from DataRow dr in dt.Rows
                         select new Leave
                         {
                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             LeavesAccrued = Convert.ToInt32(dr["LeavesAccrued"]),
                             PreviousBalance = Convert.ToInt32(dr["PreviousBalance"]),
                             SatSunWorking = Convert.ToInt32(dr["SatSunWorking"]),
                             BalanceLeaves = Convert.ToInt32(dr["BalanceLeaves"]),
                             TotalAvailable = Convert.ToInt32(dr["TotalAvailable"]),
                             LeavesTaken = Convert.ToInt32(dr["LeavesTaken"]),
                             CumilativeLeaves = Convert.ToInt32(dr["CumilativeLeaves"]),
                             BalanceEOM = Convert.ToInt32(dr["BalanceEOM"]),
                             LeaveWithoutPay = Convert.ToInt32(dr["LeaveWithoutPay"])

                         }

                ).ToList();
            return leaves;
        
        }
    }
}