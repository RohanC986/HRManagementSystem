using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;


namespace EmployeeManagementSystemInfrastructure.EmployeeBL
{
    public class EmployeeService
    {
        DataAccessService dal = new DataAccessService();
        DTableToLeaveModel lm = new DTableToLeaveModel();
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();
        DTableToAdminViewModel dtadminvm= new DTableToAdminViewModel();

        //Saves the Leave Request into DataBase
        public object SaveLeaveRequest(LeaveRequest model, int Empid)
        {
            try
            {
                //Procced if the leave is for half day
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
                    object output = dal.ExecuteNonQuery("uspLeaveRequest", leavedict);                  //Saves the Leave Request into DataBase & returns 1
                    return output;
                }
                //Procced if Leave is not half Day
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
                    object output = dal.ExecuteNonQuery("uspLeaveRequest", leavedict);           //Saves the Leave Request into DataBase & returns 1
                    return output;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }



        //Gets the Leave Summary of an Employee
        public LeaveViewModel LeaveSummary(Leave obj, int Empid)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",Empid},
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetLeaveSummary", dict);          //Gets the Leave Summary of an Employee
            LeaveViewModel leaveViewModel = new LeaveViewModel();
            leaveViewModel.getleaves = lm.DataTabletoLeaveModel(EmpTable);                          //Pass the data into List of LeaveViewModel from DataTable
            return leaveViewModel;
        }

        //Gets User Details
        //public EmployeeViewModel GetUserDetails(Leave obj,int EmpId)
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>()
        //    {
        //        { "@EmployeeId",EmpId}
        //    };      
        //    DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);             //Gets User Details
        //    EmployeeViewModel employee = new EmployeeViewModel();       
        //    employee.employees = dTableToEmployeeModel.DataTabletoEmployeeModel(EmpTable);          //Pass the data into List of EmployeeViewModel from DataTable
        //    return employee;
        //}

        //Gets User Details
        public AdminViewModelList GetUserOwnDetails(int EmpId)   
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",EmpId}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);            //Gets User Details
            AdminViewModelList employee = new AdminViewModelList();
            employee.allEmployees = dtadminvm.DataTabletoAdminEmployeeModel(EmpTable);                  //Pass the data into List of AdminViewModelList from DataTable

            return employee;
        }


        //Gets Leave Request of the User
        public LeaveRequestViewModel GetLeaveRequest(int EmpId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",EmpId}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetLeaveRequest", dict);          //Gets Leave Request of the User
            LeaveRequestViewModel leaveRequest = new LeaveRequestViewModel();
            leaveRequest.leaveRequests = DTableToLeaveRequestModel.DataTabletoLeaveModel(EmpTable);     //Pass the data into List of LeaveRequestViewModel from DataTable
            return leaveRequest;
        }
    }
}
