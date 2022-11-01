using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
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
                             ProjectName = dr["ProjectName"].ToString()
                            
                            
                         }

                ).ToList();
            return ProjectList;
        }
    }
}