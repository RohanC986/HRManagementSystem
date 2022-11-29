using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToDesignationModel
    {
        public List<Designation> DataTabletoDesignationsModel(DataTable dt)
        {
            List<Designation> designationViews = new List<Designation>();
            designationViews = (from DataRow dr in dt.Rows
                                select new Designation
                                {
                                    DesignationId = Convert.ToInt32(dr["DesignationId"]),
                                    DesignationName = Convert.ToString(dr["DesignationName"]),
                                    Created = Convert.ToString(dr["Created"]),
                                    LastModified = Convert.ToString(dr["LastModified"])
                                }

                ).ToList();
            return designationViews;
        }
    }
}