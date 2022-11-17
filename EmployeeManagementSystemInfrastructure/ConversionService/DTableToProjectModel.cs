﻿using EmployeeManagementSystemCore;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToProjectModel
    {
        public List<Project> DataTableToProjectModel(DataTable dt)
        {
            List<Project> ProjectList = new List<Project>();
            ProjectList = (from DataRow dr in dt.Rows
                         select new Project
                         {
                             ProjectId = Convert.ToInt32(dr["ProjectId"]),
                             ProjectHeadEmployeeId = Convert.ToInt32(dr["ProjectHeadEmployeeId"]),
                             ProjectName = dr["ProjectName"].ToString(),
                             Created = dr["Created"].ToString(),
                             LastModified = dr["LastModified"].ToString()
                            
                            
                         }

                ).ToList();
            return ProjectList;
        }
    }
}