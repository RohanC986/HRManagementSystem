using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToDesignationModel
    {


        //Returns List of Class Designation after Converting data from DataTable to List
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