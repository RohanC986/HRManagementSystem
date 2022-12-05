using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToAccountDetailsModel
    {
        //Returns List of Class AddAccountViewModel after Converting data from DataTable to List

        public List<AddAccountViewModel> DataTabletoAccountDetailsModel(DataTable dt)
        {
            List<AddAccountViewModel> details = new List<AddAccountViewModel>();
            details = (from DataRow dr in dt.Rows
                                select new AddAccountViewModel
                                {
                                    
                                    EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                                    UANNo = Convert.ToString(dr["UANNo"]),
                                    BankAcNo = Convert.ToString(dr["BankAcNo"]),
                                    IFSCCode = Convert.ToString(dr["IFSCCode"]),
                                    Created = DateTime.Parse(dr["Created"].ToString()),
                                    LastModified = DateTime.Parse(dr["LastModified"].ToString())
                                    //EmployeesOnLeave = Convert.ToInt32(dr["EmployeesOnLeave"]),
                                }

                ).ToList();
            return details;
        }
    }
}