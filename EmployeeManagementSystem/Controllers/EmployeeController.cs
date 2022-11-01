using EmployeeManagementSystem.ConversionService;
using EmployeeManagementSystem.DataAccessLayer;
using EmployeeManagementSystem.DBContext;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            con = new SqlConnection(constr);

        }

        private EMSContext db = new EMSContext();
        DataAccessService dal = new DataAccessService();
        private LeaveViewModel LeaveView;
        DTableToLeaveModel lm = new DTableToLeaveModel();
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();



       
        public ActionResult LeaveRequest(LeaveRequest model)
        {

            Dictionary<string, object> leavedict = new Dictionary<string, object>() {
                { "@EmployeeId",Session["EmpId"]},
                { "@IsHalfday",model.IsHalfday},
                { "@LeaveType",model.LeaveType},
                { "@Reason",model.Reason},
                { "@LengthOfLeave",model.LengthOfLeave},
                { "@StartDate",model.StartDate},
                { "@EndDate",model.EndDate},
                { "@Status","Pending"}


            };
            object output = dal.ExecuteNonQuery("uspLeaveRequest", leavedict);
            Console.WriteLine(output);
            if (output == null)
            {
                ViewBag.Message = "Invalid credentials";
            }
            else
            {
                ViewData["Leave"] = "Leave Requested Succefully";
            }
            return View();
        }
        public ViewResult LeaveSummary(Leave obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",Session["EmpId"]},
            };


            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetLeaveSummary", dict);
            LeaveViewModel leaveViewModel = new LeaveViewModel();
            leaveViewModel.getleaves = lm.DataTabletoLeaveModel(EmpTable);
            ViewData["getleaves"] = leaveViewModel.getleaves;
            LeaveView = leaveViewModel;
            return View(ViewData);

        }

        public ActionResult GetUserDetails(Leave obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",Session["EmpId"]}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);
            EmployeeViewModel employee = new EmployeeViewModel();
            employee.employees = dTableToEmployeeModel.DataTabletoEmployeeModel(EmpTable);
            ViewData["userdetails"] = employee.employees;
            return View(ViewData);
        }



        public ViewResult GetLeaveRequest(LeaveRequest obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",Session["EmpId"]}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetLeaveRequest", dict);
            LeaveRequestViewModel leaveRequest = new LeaveRequestViewModel();
            leaveRequest.leaveRequests = DTableToLeaveRequestModel.DataTabletoLeaveModel(EmpTable);
            ViewData["getleaverequest"] = leaveRequest.leaveRequests;
            return View(ViewData);
        }





        /* [ValidateAntiForgeryToken]*/
        //[Authorize(Roles = "Admin")]

    }
}