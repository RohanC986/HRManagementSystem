using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
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
                             Designation = dr["Designation"].ToString(),
                             ProjectName = dr["ProjectName"].ToString()
                         }

                ).ToList();
            return team;
        }
    }
}