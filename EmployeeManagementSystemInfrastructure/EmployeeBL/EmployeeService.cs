using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace EmployeeManagementSystemInfrastructure.EmployeeBL
{
    public class EmployeeService
    {
        DataAccessService dal = new DataAccessService();
        private LeaveViewModel LeaveView;
        DTableToLeaveModel lm = new DTableToLeaveModel();
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();
        DTableToAdminViewModel dtadminvm= new DTableToAdminViewModel();
        public object SaveLeaveRequest(LeaveRequest model, int Empid)
        {
            try
            {
                if (model.IsHalfday == true)
                {


                    Dictionary<string, object> leavedict = new Dictionary<string, object>() {
                { "@EmployeeId",Empid},
                { "@IsHalfday",model.IsHalfday},
                { "@LeaveType",model.LeaveType},
                { "@Reason",model.Reason},
                { "@LengthOfLeave",0},
                { "@StartDate",model.StartDate},
                { "@EndDate",model.StartDate},
                { "@Status","Pending"}


                };
                    object output = dal.ExecuteNonQuery("uspLeaveRequest", leavedict);

                    return output;
                }
                else
                {
                    int days = (model.EndDate - model.StartDate).Days;


                    Dictionary<string, object> leavedict = new Dictionary<string, object>() {
                { "@EmployeeId",Empid},
                { "@IsHalfday",model.IsHalfday},
                { "@LeaveType",model.LeaveType},
                { "@Reason",model.Reason},
                { "@LengthOfLeave",days+1},
                { "@StartDate",model.StartDate},
                { "@EndDate",model.EndDate},
                { "@Status","Pending"}


                };
                    object output = dal.ExecuteNonQuery("uspLeaveRequest", leavedict);

                    return output;
                }
            }
            catch(Exception e)
            {
                return null;
            }
                
            
           
           
        }

        public LeaveViewModel LeaveSummary(Leave obj, int Empid)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",Empid},
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetLeaveSummary", dict);
            LeaveViewModel leaveViewModel = new LeaveViewModel();
            leaveViewModel.getleaves = lm.DataTabletoLeaveModel(EmpTable);
            return leaveViewModel;
        }


        public EmployeeViewModel GetUserDetails(Leave obj,int EmpId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",EmpId}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);
            EmployeeViewModel employee = new EmployeeViewModel();
            employee.employees = dTableToEmployeeModel.DataTabletoEmployeeModel(EmpTable);
            return employee;
        }


        public AdminViewModel GetUserOwnDetails(int EmpId)   
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",EmpId}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);
            AdminViewModel employee = new AdminViewModel();
            employee.allEmployees = dtadminvm.DataTabletoAdminEmployeeModel(EmpTable);
            
            return employee;
        }
        public LeaveRequestViewModel GetLeaveRequest(int EmpId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",EmpId}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetLeaveRequest", dict);
            LeaveRequestViewModel leaveRequest = new LeaveRequestViewModel();
            leaveRequest.leaveRequests = DTableToLeaveRequestModel.DataTabletoLeaveModel(EmpTable);
            return leaveRequest;
        }
    }
}
