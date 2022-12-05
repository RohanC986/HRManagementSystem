using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToRolesModel
    {
        //Returns List of Class Role after Converting data from DataTable to List

        public List<Role> DataTableToRolesModel(DataTable dt)
        {
            List<Role> departmentsViews = new List<Role>();
            departmentsViews = (from DataRow dr in dt.Rows
                         select new Role
                         {
                             RoleId = Convert.ToInt32(dr["RoleId"]),
                             RoleName = Convert.ToString(dr["RoleName"]),
                             Created = Convert.ToString(dr["Created"]),
                             LastModified = Convert.ToString(dr["LastModified"])

                             //EmployeesOnLeave = Convert.ToInt32(dr["EmployeesOnLeave"]),
                         }

                ).ToList();
            return departmentsViews;
        }
    }
}