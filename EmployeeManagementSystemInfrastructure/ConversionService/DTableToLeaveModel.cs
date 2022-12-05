using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToLeaveModel
    {
        //Returns List of Class Leave after Converting data from DataTable to List
        public List<Leave> DataTabletoLeaveModel(DataTable dt)
        {
            List<Leave> leaves = new List<Leave>();
            leaves = (from DataRow dr in dt.Rows
                      select new Leave
                      {
                             LeaveId = Convert.ToInt32(dr["LeaveId"]), 
                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             LeavesAccrued = Convert.ToInt32(dr["AccruedLeaves"]),
                             LeavesTaken = Convert.ToInt32(dr["LeavesTaken"]),
                             BalanceLeaves = Convert.ToInt32(dr["BalanceLeaves"]),
                             UnPaidLeaves = Convert.ToInt32(dr["UnPaidLeaves"]),
                             Created = Convert.ToString(dr["Created"]),
                             LastModified = Convert.ToString(dr["LastModified"]),
                             HalfDay = Convert.ToInt32(dr["HalfDay"])


                         }

                ).ToList();
            return leaves;
        
        }
        //public List<Leave> LeaveSummary()
        //{
        //    connection();
        //    List<Leave> leaveList = new List<Leave>();


        //    SqlCommand com = new SqlCommand("uspgetLeaveSummary", con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@EmployeeId", HttpContext.Session["EmpId"]);
        //    SqlDataAdapter da = new SqlDataAdapter(com);
        //    DataTable dt = new DataTable();

        //    con.Open();
        //    da.Fill(dt);
        //    con.Close();
        //    //Bind EmpModel generic list using dataRow     
        //    foreach (DataRow dr in dt.Rows)
        //    {

        //        leaveList.Add(

        //            new Leave
        //            {

        //                LeavesAccrued = Convert.ToInt32(dr["LeavesAccrued"]),
        //                PreviousBalance = Convert.ToInt32(dr["PreviousBalance"]),
        //                SatSunWorking = Convert.ToInt32(dr["SatSunWorking"]),
        //                BalanceLeaves = Convert.ToInt32(dr["BalanceLeaves"]),
        //                TotalAvailable = Convert.ToInt32(dr["TotalAvailable"]),
        //                LeavesTaken = Convert.ToInt32(dr["LeavesTaken"]),
        //                CumilativeLeaves = Convert.ToInt32(dr["CumilativeLeaves"]),
        //                BalanceEOM = Convert.ToInt32(dr["BalanceEOM"]),
        //                LeaveWithoutPay = Convert.ToInt32(dr["LeaveWithoutPay"])

        //            }
        //            );
        //    }

        //    return leaveList;
        //}
    }
}