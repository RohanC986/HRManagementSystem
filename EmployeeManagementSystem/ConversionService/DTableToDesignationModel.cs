using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
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
                                    DesignationName = dr["DesignationName"].ToString(),
                                    Created = dr["Created"].ToString(),
                                    LastModified = dr["LastModified"].ToString()
                                }

                ).ToList();
            return designationViews;
        }
    }
}