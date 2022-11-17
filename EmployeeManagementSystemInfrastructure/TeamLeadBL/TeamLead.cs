using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.TeamLeadBL
{
    public class TeamLead
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


        public object LeaveAccept(GetTeamLeaveRequestViewModel leaveRequest)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@LeaveRequestId",leaveRequest.LeaveRequestId},
                };
            var op = dal.ExecuteNonQuery("uspAcceptLeave", dict);
            return op;
        }


        public void LeaveAcceptResponse(GetTeamLeaveRequestViewModel leaveRequest)
        {
            Dictionary<string, object> dict2 = new Dictionary<string, object>()
            {
                    {"@EmployeeId",leaveRequest.EmployeeId },
                    { "@LeavesTaken",leaveRequest.LengthOfLeave},

                };
            Dictionary<string, object> dict3 = new Dictionary<string, object>()
            {
                    {"@EmployeeId",leaveRequest.EmployeeId }
                };
            DataTable balLeaves = dal.ExecuteDataSet<DataTable>("uspgetLeaveSummary", dict3);
            LeaveViewModel leavesummary = new LeaveViewModel();
            leavesummary.getleaves = dtLeave.DataTabletoLeaveModel(balLeaves);
            int balanceLeaves = leavesummary.getleaves[0].BalanceLeaves;
            int leavesTaken = leavesummary.getleaves[0].LeavesTaken;
            int unpaidleaves = leavesummary.getleaves[0].UnPaidLeaves;

            if (leavesTaken < 15)
            {
                if ((leavesTaken + (leaveRequest.LengthOfLeave)) <= 15)
                {
                    Dictionary<string, object> dict4 = new Dictionary<string, object>()
                    {
                            {"@EmployeeId",leaveRequest.EmployeeId },
                            {"@BalanceLeaves",(15-(leavesTaken+(leaveRequest.LengthOfLeave))) },
                            {"@LeavesTaken",(leavesTaken+(leaveRequest.LengthOfLeave)) }
                        };
                    dal.ExecuteNonQuery("uspUpdateLeavesBefore15", dict4);
                }
                else if ((leavesTaken + (leaveRequest.LengthOfLeave)) > 15)
                {
                    Dictionary<string, object> dict5 = new Dictionary<string, object>()
                    {
                            {"@EmployeeId",leaveRequest.EmployeeId },
                        {"@BalanceLeaves",0 },
                            {"@LeavesTaken",leavesTaken+leaveRequest.LengthOfLeave },
                            { "@UnPaidLeaves",(leavesTaken+leaveRequest.LengthOfLeave)-15}
                        };
                    dal.ExecuteNonQuery("uspUpdateLeavesAfter15", dict5);
                }
            }
            else
            {
                Dictionary<string, object> dict6 = new Dictionary<string, object>()
                {
                            {"@EmployeeId",leaveRequest.EmployeeId },
                            {"@LeavesTaken",leavesTaken+leaveRequest.LengthOfLeave },
                            { "@UnPaidLeaves",unpaidleaves+leaveRequest.LengthOfLeave}
                        };
                dal.ExecuteNonQuery("uspUpdateUnPaidLeaves", dict6);
            }
        }
        public object LeaveReject(GetTeamLeaveRequestViewModel leaveRequest)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@LeaveRequestId",leaveRequest.LeaveRequestId},

                };
            var op = dal.ExecuteScalar("uspRejectLeave", dict);
            return op;

        }

        public Employee GetUserSpecificDetails(TeamEmpDetailsViewModel EmpId)
        {
                Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",EmpId.EmployeeId}
            };
                DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);
                EmployeeViewModel employee = new EmployeeViewModel();
                employee.employees = dTableToEmployeeModel.DataTabletoEmployeeModel(EmpTable);
                Employee employeeowndetail = new Employee();
                employeeowndetail = employee.employees[0];
                return employeeowndetail;
        }
    }
}
