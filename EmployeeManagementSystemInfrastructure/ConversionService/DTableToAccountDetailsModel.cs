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
        public List<AddAccountViewModel> DataTabletoAccountDetailsModel(DataTable dt)
        {
            List<AddAccountViewModel> details = new List<AddAccountViewModel>();
            details = (from DataRow dr in dt.Rows
                                select new AddAccountViewModel
                                {
                                    
                                    EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                                    UANNo = dr["UANNo"].ToString(),
                                    BankAcNo = Convert.ToInt32(dr["BankAcNo"]),
                                    IFSCCode = dr["IFSCCode"].ToString(),
                                    Created = dr["Created"].ToString(),
                                    LastModified = dr["LastModified"].ToString()
                                    //EmployeesOnLeave = Convert.ToInt32(dr["EmployeesOnLeave"]),
                                }

                ).ToList();
            return details;
        }
    }
}