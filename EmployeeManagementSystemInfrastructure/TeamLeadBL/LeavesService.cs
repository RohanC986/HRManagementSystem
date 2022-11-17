using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystemInfrastructure.TeamLeadBL
{
    public class LeavesService
    {

        DataAccessService dal = new DataAccessService();
        private LeaveViewModel LeaveView;
        DTableToTeamEmpModel tableToTeamEmpModel = new DTableToTeamEmpModel();
        DTableToTeamLeaveRequestModel DTableToTeamLeaveRequestModel = new DTableToTeamLeaveRequestModel();
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        DTableToLeaveModel dtLeave = new DTableToLeaveModel();

        public List<TeamEmpDetailsViewModel> GetTeamEmps(string emp, int empid)

        {

           if (emp != null)
            {
                Dictionary<string, object> dict1 = new Dictionary<string, object>()
                {
                    { "@ProjectHeadEmployeeId",empid},
                     { "@FirstName",emp},

                };

                DataTable EmpTable1 = dal.ExecuteDataSet<DataTable>("uspTeamEmpsSearch", dict1);
                TeamEmpDetailsViewModel tempEmpDetialsView1 = new TeamEmpDetailsViewModel();
                tempEmpDetialsView1.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable1);

                return tempEmpDetialsView1.teamEmps;
            }
            Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@ProjectHeadEmployeeId",empid},
                };  

            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspTeamEmps", dict);
            TeamEmpDetailsViewModel tempEmpDetialsView = new TeamEmpDetailsViewModel();
            tempEmpDetialsView.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable);
            return tempEmpDetialsView.teamEmps;

        }

        public GetTeamLeaveRequestViewModel TeamLeaveRequest(int EmpId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
                    {
                        { "@ProjectHeadEmployeeId",EmpId},
                    };


            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetTeamLeaveRequest", dict);
            GetTeamLeaveRequestViewModel getTeamLeaveRequest = new GetTeamLeaveRequestViewModel();
            getTeamLeaveRequest.getTeamLeaveRequestViewModels = DTableToTeamLeaveRequestModel.DataTabletoLeaveRequestViewModel(EmpTable);
            return getTeamLeaveRequest;
        }
    }
}
