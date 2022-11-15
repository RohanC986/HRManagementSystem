using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
{
    public class DTableToRolesModel
    {
        public List<Role> DataTableToRolesModel(DataTable dt)
        {
            List<Role> departmentsViews = new List<Role>();
            departmentsViews = (from DataRow dr in dt.Rows
                         select new Role
                         {
                             RoleId = Convert.ToInt32(dr["RoleId"]),
                             RoleName = dr["RoleName"].ToString(),
                             Created = dr["Created"].ToString(),
                             LastModified = dr["LastModified"].ToString()

                             //EmployeesOnLeave = Convert.ToInt32(dr["EmployeesOnLeave"]),
                         }

                ).ToList();
            return departmentsViews;
        }
    }
}