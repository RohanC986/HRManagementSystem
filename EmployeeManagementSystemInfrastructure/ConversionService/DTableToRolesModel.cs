﻿using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
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