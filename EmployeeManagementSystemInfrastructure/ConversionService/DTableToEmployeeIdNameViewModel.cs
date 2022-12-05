using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToEmployeeIdNameViewModel
    {

        //Returns List of Class EmployeeIdNameViewModel after Converting data from DataTable to List

        public List<EmployeeIdNameViewModel> DataTableToEmployeeIdNameViewModel(DataTable dt)
        {
            List<EmployeeIdNameViewModel> EmployeeIdNameList = new List<EmployeeIdNameViewModel>();
            EmployeeIdNameList = (from DataRow dr in dt.Rows
                         select new EmployeeIdNameViewModel
                         {
                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             FirstName = Convert.ToString(dr["FirstName"]),
                             LastName = Convert.ToString(dr["LastName"]),
                             
                         }

                ).ToList();
            return EmployeeIdNameList;
        }
    }
}