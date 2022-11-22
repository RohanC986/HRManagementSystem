using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class DTableToTeamEmpModel
    {
        public List<TeamEmpDetailsViewModel> DataTabletoTeamEmployeesModel(DataTable dt)
        {
            List<TeamEmpDetailsViewModel> team = new List<TeamEmpDetailsViewModel>();
            team = (from DataRow dr in dt.Rows
                         select new TeamEmpDetailsViewModel
                         {
                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             FirstName = dr["FirstName"].ToString(),
                             LastName = dr["LastName"].ToString(),
                             Email = dr["Email"].ToString(),
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
                             //RoleName = dr["RoleName"].ToString(),
                             DesignationName = dr["DesignationName"].ToString(),
                             Experienced = Convert.ToBoolean(dr["Experienced"]),
                             PreviousCompanyName = dr["PreviousCompanyName"].ToString(),
                             YearsOfExprience = Convert.ToInt32(dr["YearsOfExprience"]),
                             ProjectName = dr["ProjectName"].ToString()
                         }

                ).ToList();
            return team;
        }
    }
}