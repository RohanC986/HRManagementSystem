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

namespace EmployeeManagementSystemInfrastructure.EmployeeBL
{
    public class EmployeeService
    {
        DataAccessService dal = new DataAccessService();
        private LeaveViewModel LeaveView;
        DTableToLeaveModel lm = new DTableToLeaveModel();
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();
        public int SaveLeaveRequest(LeaveRequest model, int Empid)
        {

            Dictionary<string, object> leavedict = new Dictionary<string, object>() {
                { "@EmployeeId",Empid},
                { "@IsHalfday",model.IsHalfday},
                { "@LeaveType",model.LeaveType},
                { "@Reason",model.Reason},
                { "@LengthOfLeave",model.LengthOfLeave},
                { "@StartDate",model.StartDate},
                { "@EndDate",model.EndDate},
                { "@Status","Pending"}


            };
            object output = dal.ExecuteNonQuery("uspLeaveRequest", leavedict);
            if (model.LeaveType != null && model.StartDate != null && model.EndDate != null && model.LeaveType != null && model.Reason != null)
            {
                int op = 1;
                return op;
            }
            return 0;
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


        public Employee GetUserOwnDetails(int EmpId)   
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",EmpId}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);
            EmployeeViewModel employee = new EmployeeViewModel();
            employee.employees = dTableToEmployeeModel.DataTabletoEmployeeModel(EmpTable);
            Employee employeeowndetail = new Employee();
            employeeowndetail = employee.employees[0];
            return employeeowndetail;
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
