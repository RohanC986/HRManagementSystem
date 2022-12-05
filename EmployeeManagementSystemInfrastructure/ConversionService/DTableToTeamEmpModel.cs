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
        //Returns List of Class TeamEmpDetailsViewModel after Converting data from DataTable to List

        public List<TeamEmpDetailsViewModel> DataTabletoTeamEmployeesModel(DataTable dt)
        {
            List<TeamEmpDetailsViewModel> team = new List<TeamEmpDetailsViewModel>();
            team = (from DataRow dr in dt.Rows
                         select new TeamEmpDetailsViewModel
                         {
                             EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                             EmployeeCode= Convert.ToString(dr["EmployeeCode"]),
                             FirstName = Convert.ToString(dr["FirstName"]),
                             LastName = Convert.ToString(dr["LastName"]),
                             Email = Convert.ToString(dr["Email"]),
                             DOB = DateTime.Parse(dr["DOB"].ToString()),
                             DOJ = DateTime.Parse(dr["DOJ"].ToString()),
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
                             //RoleName = dr["RoleName"].ToString(),
                             DesignationName = Convert.ToString(dr["DesignationName"]),
                             Experienced = Convert.ToBoolean(dr["Experienced"]),
                             PreviousCompanyName = Convert.ToString(dr["PreviousCompanyName"]),
                             YearsOfExprience = Convert.ToInt32(dr["YearsOfExprience"]),
                             ProjectId= Convert.ToInt32(dr["ProjectId"]),
                             ProjectName = Convert.ToString(dr["ProjectName"])
                         }

                ).ToList();
            return team;
        }
    }
}