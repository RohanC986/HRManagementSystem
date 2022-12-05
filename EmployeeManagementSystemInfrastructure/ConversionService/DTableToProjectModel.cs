using EmployeeManagementSystemCore;
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

        //Returns List of Class Project after Converting data from DataTable to List

        public List<Project> DataTableToProjectModel(DataTable dt)
        {
            List<Project> ProjectList = new List<Project>();
            ProjectList = (from DataRow dr in dt.Rows
                         select new Project
                         {
                             ProjectId = Convert.ToInt32(dr["ProjectId"]),
                             ProjectHeadEmployeeId = Convert.ToInt32(dr["ProjectHeadEmployeeId"]),
                             ProjectName = Convert.ToString(dr["ProjectName"]),
                             Created = Convert.ToString(dr["Created"]),
                             LastModified = Convert.ToString(dr["LastModified"])
                            
                            
                         }

                ).ToList();
            return ProjectList;
        }
    }
}