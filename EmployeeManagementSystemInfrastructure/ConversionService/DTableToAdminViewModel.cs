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
        public List<AdminViewModel> DataTabletoAdminEmployeeModel(DataTable dt)
        {
            List<AdminViewModel> employees = new List<AdminViewModel>();
            employees = (from DataRow dr in dt.Rows
                         select new AdminViewModel
                         {

                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             FirstName = dr["FirstName"].ToString(),
                             MiddleName = dr["MiddleName"].ToString(),
                             LastName = dr["LastName"].ToString(),
                             Email = dr["Email"].ToString(),
                             //DOB = Convert.ToDateTime(dr["DOB"]),
                             DOB = DateTime.Parse(dr["DOB"].ToString()).ToString("dd/MM/yyyy"),
                             DOJ = DateTime.Parse(dr["DOJ"].ToString()).ToString("dd/MM/yyyy"),
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
                             //RoleId = Convert.ToInt16(dr["RoleId"]),

                             RoleName = dr["RoleName"].ToString(),
                             DesignationName = dr["DesignationName"].ToString(),
                             Experienced = Convert.ToBoolean(dr["Experienced"]),
                             PreviousCompanyName = dr["PreviousCompanyName"].ToString(),
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
