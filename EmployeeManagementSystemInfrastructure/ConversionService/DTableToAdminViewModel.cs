using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToAdminViewModel
    {
        //Returns List of Class AdminViewModel after Converting data from DataTable to List

        public List<AdminViewModel> DataTabletoAdminEmployeeModel(DataTable dt)
        {
            List<AdminViewModel> employees = new List<AdminViewModel>();
            employees = (from DataRow dr in dt.Rows
                         select new AdminViewModel
                         {

                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             EmployeeCode = Convert.ToString(dr["EmployeeCode"]),
                             FirstName = Convert.ToString(dr["FirstName"]),
                             MiddleName = Convert.ToString(dr["MiddleName"]),
                             LastName = Convert.ToString(dr["LastName"]),
                             Email = Convert.ToString(dr["Email"]),
                             //DOB = Convert.ToDateTime(dr["DOB"]),
                             DOB = DateTime.Parse(dr["DOB"].ToString()).ToString("dd/MM/yyyy"),
                             DOJ = DateTime.Parse(dr["DOJ"].ToString()).ToString("dd/MM/yyyy"),
                             BloodGroup = Convert.ToString(dr["BloodGroup"]),
                             Gender = Convert.ToString(dr["Gender"]),
                             PersonalContact = Convert.ToInt64(dr["PersonalContact"]),
                             EmergencyContact = Convert.ToInt64(dr["EmergencyContact"]),
                             AadharCardNo = Convert.ToInt64(dr["AadharCardNo"]),
                             PancardNo = Convert.ToString(dr["PancardNo"]),
                             PassportNo = Convert.ToString(dr["PassportNo"]),
                             Address = Convert.ToString(dr["Address"]),
                             City = Convert.ToString(dr["City"]),
                             State = Convert.ToString(dr["State"]),
                             Pincode = Convert.ToString(dr["Pincode"]),
                             RoleId = Convert.ToInt16(dr["RoleId"]),
                             DesignationId = Convert.ToInt16(dr["DesignationId"]), 

                             RoleName = Convert.ToString(dr["RoleName"]),
                             DesignationName = Convert.ToString(dr["DesignationName"]),
                             Experienced = Convert.ToBoolean(dr["Experienced"]),
                             PreviousCompanyName = Convert.ToString(dr["PreviousCompanyName"]),
                             YearsOfExprience = Convert.ToInt32(dr["YearsOfExprience"]),
                             IsActive = Convert.ToBoolean(dr["IsActive"]),
                             Created = DateTime.Parse(dr["Created"].ToString()),
                             LastModified = DateTime.Parse(dr["LastModified"].ToString())


                         }

                ).ToList();
            return employees;
        }
    }
}
