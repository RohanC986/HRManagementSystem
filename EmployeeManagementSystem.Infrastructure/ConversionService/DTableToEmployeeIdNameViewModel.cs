using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
{
    public class DTableToEmployeeIdNameViewModel
    {
        public List<EmployeeIdNameViewModel> DataTableToEmployeeIdNameViewModel(DataTable dt)
        {
            List<EmployeeIdNameViewModel> EmployeeIdNameList = new List<EmployeeIdNameViewModel>();
            EmployeeIdNameList = (from DataRow dr in dt.Rows
                         select new EmployeeIdNameViewModel
                         {
                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             FirstName = dr["FirstName"].ToString(),
                             LastName = dr["LastName"].ToString(),
                             
                         }

                ).ToList();
            return EmployeeIdNameList;
        }
    }
}