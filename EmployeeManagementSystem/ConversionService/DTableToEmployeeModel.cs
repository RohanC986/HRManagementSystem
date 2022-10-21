using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EmployeeManagementSystem.ConversionService
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
                             DOB = dr["DOB"].ToString(),
                             DOJ = dr["DOJ"].ToString(),
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
                             Role = dr["Role"].ToString(),
                             Designation = dr["Designation"].ToString(),
                             Experienced = Convert.ToBoolean(dr["Experienced"]),
                             PreviousCompanyName = dr["PreviousCompanyName"].ToString(),
                             YearsOfExprience = Convert.ToInt32(dr["YearsOfExprience"])






                         }

                ).ToList();
            return employees;
        }
    }
}