using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToDepartmentsModel
    {
        //Returns List of Class DepartmentsViewModel after Converting data from DataTable to List

        public List<DepartmentsViewModel> DataTabletoDepartmentsModel(DataTable dt)
        {
            List<DepartmentsViewModel> departmentsViews = new List<DepartmentsViewModel>();
            departmentsViews = (from DataRow dr in dt.Rows
                         select new DepartmentsViewModel
                         {
                             ProjectId = Convert.ToInt32(dr["ProjectId"]),
                             ProjectHeadEmployeeId = Convert.ToInt32(dr["ProjectHeadEmployeeId"]),
                             ProjectName = Convert.ToString(dr["ProjectName"]),
                             FirstName = Convert.ToString(dr["FirstName"]),
                             LastName = Convert.ToString(dr["LastName"]),
                             TotalMembers = Convert.ToInt32(dr["TotalMembers"]),
                             DesignationId = Convert.ToInt32(dr["DesignationId"])
                             //EmployeesOnLeave = Convert.ToInt32(dr["EmployeesOnLeave"]),
                         }

                ).ToList();
            return departmentsViews;
        }
    }
}