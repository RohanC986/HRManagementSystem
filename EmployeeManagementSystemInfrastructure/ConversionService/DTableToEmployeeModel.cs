using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToEmployeeModel
    {
        
        public List<Employee> DataTabletoEmployeeModel(DataTable dt)
        {
            List<Employee> employees = new List<Employee>();
            employees = (from DataRow dr in dt.Rows
                         select new Employee
                         {

                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             FirstName = dr["FirstName"].ToString(),
                             MiddleName = dr["MiddleName"].ToString(),
                             LastName = dr["LastName"].ToString(),
                             Email = dr["Email"].ToString(),
                             //DOB = Convert.ToDateTime(dr["DOB"]),
                             DOB = DateTime.Parse(dr["DOB"].ToString()),
                             DOJ = DateTime.Parse(dr["DOJ"].ToString()),
                             BloodGroup = dr["BloodGroup"].ToString(),
                             Gender = dr["Gender"].ToString(),
                             PersonalContact = Convert.ToInt64(dr["PersonalContact"]),
                             EmergencyContact = Convert.ToInt64(dr["EmergencyContact"]),
                             AadharCardNo = Convert.ToInt64(dr["AadharCardNo"]),
                             PancardNo = dr["PancardNo"].ToString(),
                             PassportNo = dr["PassportNo"].ToString(),
                             Address = dr["Address"].ToString(),
                             City = dr["City"].ToString(),
                             State = dr["State"].ToString(),
                             Pincode = dr["Pincode"].ToString(),
                             RoleId = Convert.ToInt16(dr["RoleId"]),
                             DesignationId = Convert.ToInt16(dr["DesignationId"].ToString()),
                             Experienced = Convert.ToBoolean(dr["Experienced"]),
                             PreviousCompanyName = dr["PreviousCompanyName"].ToString(),
                             YearsOfExprience = Convert.ToInt32(dr["YearsOfExprience"]),
                             IsActive  = Convert.ToBoolean(dr["IsActive"]),
                             Created = DateTime.Parse(dr["Created"].ToString()),
                             LastModified = DateTime.Parse(dr["LastModified"].ToString())


                         }

                ).ToList();
            return employees;
        }
    }
}