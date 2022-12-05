using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System.Collections.Generic;
using System.Data;

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
        DTableToAdminViewModel DTableToAdminViewModel = new DTableToAdminViewModel();


        //Gets all the Employees under the Team Lead
        public TeamEmpDetailsViewModel GetTeamEmps(string searchEmp, int empid)

        {
            //Procced if searchEmp is not empty
            if (searchEmp != null)
            {
                Dictionary<string, object> dict1 = new Dictionary<string, object>()
                {
                    { "@ProjectHeadEmployeeId",empid},
                     { "@FirstName",searchEmp},

                };

                DataTable EmpTable1 = dal.ExecuteDataSet<DataTable>("uspTeamEmpsSearch", dict1);            //Gets the data of according to Search 
                TeamEmpDetailsViewModel tempEmpDetialsView1 = new TeamEmpDetailsViewModel();
                tempEmpDetialsView1.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable1); //Pass the Data to the list of type TeamEmpDetailsViewModel 

                return tempEmpDetialsView1;
            }
            //Procced if searchEmp is empty
            Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@ProjectHeadEmployeeId",empid},
                };

            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspTeamEmps", dict);                  //Gets all the Employees under the Team Lead
            TeamEmpDetailsViewModel tempEmpDetialsView = new TeamEmpDetailsViewModel();
            tempEmpDetialsView.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable);  //Pass the Data to the list of type TeamEmpDetailsViewModel 
            return tempEmpDetialsView;

        }


        //Gets all the Leave request of employee under the Team Lead
        public GetTeamLeaveRequestViewModel TeamLeaveRequest(int EmpId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
                    {
                        { "@ProjectHeadEmployeeId",EmpId},
                    };


            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetTeamLeaveRequest", dict);           //Gets all the Leave request of employee under the Team Lead
            GetTeamLeaveRequestViewModel getTeamLeaveRequest = new GetTeamLeaveRequestViewModel();
            getTeamLeaveRequest.getTeamLeaveRequestViewModels = DTableToTeamLeaveRequestModel.DataTabletoLeaveRequestViewModel(EmpTable);       //Pass the Data to the list of type TeamEmpDetailsViewModel
            return getTeamLeaveRequest;
        }

        //Accpets the Leave of a particular Employee
        public object LeaveAccept(GetTeamLeaveRequestViewModel leaveRequest)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@LeaveRequestId",leaveRequest.LeaveRequestId},
                };
            var check = dal.ExecuteNonQuery("uspAcceptLeave", dict);           //Accpets the Leave of a particular Employee and returns 1
            return check;
        }


        //Updates the Leave Summary of particular Employee after leave is accepted
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
            DataTable balLeaves = dal.ExecuteDataSet<DataTable>("uspgetLeaveSummary", dict3);   //Gets the Summary of the Employee
            LeaveViewModel leavesummary = new LeaveViewModel();
            leavesummary.getleaves = dtLeave.DataTabletoLeaveModel(balLeaves);
            int balanceLeaves = leavesummary.getleaves[0].BalanceLeaves;                        //Pass the balance leaves to variable
            int leavesTaken = leavesummary.getleaves[0].LeavesTaken;                            //Pass the leaves taken to variable
            int unpaidleaves = leavesummary.getleaves[0].UnPaidLeaves;                           //Pass the unpaid leaves to variable
            int halfdaycount = leavesummary.getleaves[0].HalfDay;                                 //Pass halfday to variable
            
            //Procced if Leave request if not half day
            if (leaveRequest.isHalfDay==false)
            {
                //Procced if Leaves taken are less than 15
                if (leavesTaken < 15)
                {
                    //Procced if previous leaves taken & current leave request length are less than 15
                    if ((leavesTaken + (leaveRequest.LengthOfLeave) + (halfdaycount * 0.5)) <= 15)
                    {
                        Dictionary<string, object> dict4 = new Dictionary<string, object>()
                    {
                            {"@EmployeeId",leaveRequest.EmployeeId },
                            {"@BalanceLeaves",(15-(leavesTaken+(leaveRequest.LengthOfLeave))) },
                            {"@LeavesTaken",(leavesTaken+(leaveRequest.LengthOfLeave)) }
                        };
                        dal.ExecuteNonQuery("uspUpdateLeavesBefore15", dict4);                      //Update the leave summary
                    }
                    //Procced if previous leaves taken & current leave request length is greater than 15
                    else if ((leavesTaken + (leaveRequest.LengthOfLeave) + (halfdaycount * 0.5)) > 15)
                    {
                        Dictionary<string, object> dict5 = new Dictionary<string, object>()
                    {
                            {"@EmployeeId",leaveRequest.EmployeeId },
                            {"@BalanceLeaves",0 },
                            {"@LeavesTaken",leavesTaken+leaveRequest.LengthOfLeave },
                            { "@UnPaidLeaves",(leavesTaken+leaveRequest.LengthOfLeave)-15}
                        };
                        dal.ExecuteNonQuery("uspUpdateLeavesAfter15", dict5);                       //Update the leave summary
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
                    dal.ExecuteNonQuery("uspUpdateUnPaidLeaves", dict6);                            //Update the leave summary
                }
            }
            else
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>()
                {
                    {"@EmployeeId",leaveRequest.EmployeeId },
                    {"@HalfDay",1 }
                };
                dal.ExecuteNonQuery("uspUpdateHalfDay", dictionary);                         //Update the leave summary
            }
        }

        //Rejects the leave request
        public object LeaveReject(GetTeamLeaveRequestViewModel leaveRequest)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@LeaveRequestId",leaveRequest.LeaveRequestId},

                };
            var check = dal.ExecuteScalar("uspRejectLeave", dict);                     //Rejects the leave request and returns 1
            return check;

        }

        //Gets Details of a particular Employee
        public AdminViewModel GetUserSpecificDetails(int EmpId)
        {
                Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",EmpId}
            };
                DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);         //Gets Details of a particular Employee
            AdminViewModelList employee = new AdminViewModelList();
                employee.allEmployees = DTableToAdminViewModel.DataTabletoAdminEmployeeModel(EmpTable);     //Pass the data from datatable to List of type AdminViewModelList
            AdminViewModel employeeowndetail = employee.allEmployees[0];
            return employeeowndetail;
        }
    }
}
