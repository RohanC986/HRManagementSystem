using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
{
    public class DTableToDepartmentsModel
    {
        public List<DepartmentsViewModel> DataTabletoDepartmentsModel(DataTable dt)
        {
            List<DepartmentsViewModel> departmentsViews = new List<DepartmentsViewModel>();
            departmentsViews = (from DataRow dr in dt.Rows
                         select new DepartmentsViewModel
                         {
                             ProjectHeadEmployeeId = Convert.ToInt32(dr["ProjectHeadEmployeeId"]),
                             ProjectName = dr["ProjectName"].ToString(),
                             FirstName = dr["FirstName"].ToString(),
                             LastName = dr["LastName"].ToString(),
                             TotalMembers = Convert.ToInt32(dr["TotalMembers"]),
                             //EmployeesOnLeave = Convert.ToInt32(dr["EmployeesOnLeave"]),
                         }

                ).ToList();
            return departmentsViews;
        }
    }
}