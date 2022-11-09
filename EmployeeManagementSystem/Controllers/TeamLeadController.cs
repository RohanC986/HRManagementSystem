using EmployeeManagementSystem.ConversionService;
using EmployeeManagementSystem.DataAccessLayer;
using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EmployeeManagementSystem.Controllers.AccountsController;

namespace EmployeeManagementSystem.Controllers
{
    [NoCache]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class TeamLeadController : Controller
    {
        
        DataAccessService dal = new DataAccessService();
        private LeaveViewModel LeaveView;
        DTableToTeamEmpModel tableToTeamEmpModel = new DTableToTeamEmpModel();
        DTableToTeamLeaveRequestModel DTableToTeamLeaveRequestModel = new DTableToTeamLeaveRequestModel();
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        // GET: TeamLead
        public ViewResult GetAllTeamEmps(/*TeamEmpDetailsViewModel obj*/)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@ProjectHeadEmployeeId",Session["EmpId"]},
                };


                DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspTeamEmps", dict);
                TeamEmpDetailsViewModel tempEmpDetialsView = new TeamEmpDetailsViewModel();
                tempEmpDetialsView.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable);
                ViewData["teamEmps"] = tempEmpDetialsView.teamEmps;
                //TeamEmps = tempEmpDetialsView;

                return View(ViewData);
            }
            catch(Exception ex)
            {
                ViewBag.GetAllTeamEmps = "GetAllTeamEmps Error";
            }
            return View(ViewData);


        }

        
        public ActionResult GetTeamLeaveRequest()

        {

            try
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
                    {
                        { "@ProjectHeadEmployeeId",Session["EmpId"]},
                    };


                    DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetTeamLeaveRequest", dict);
                    GetTeamLeaveRequestViewModel getTeamLeaveRequest = new GetTeamLeaveRequestViewModel();
                    getTeamLeaveRequest.getTeamLeaveRequestViewModels = DTableToTeamLeaveRequestModel.DataTabletoLeaveRequestViewModel(EmpTable);
                    ViewData["TeamLeaveRequest"] = getTeamLeaveRequest.getTeamLeaveRequestViewModels;
                return View(getTeamLeaveRequest);
            }
            catch(Exception ex)
            {
                ViewBag.GetTeamLeaveRequest = "Leave request Error";
            }
            //TeamEmps = tempEmpDetialsView;
            return RedirectToAction("GetTeamLeaveRequest");

        }

        public ActionResult LeaveAccept(GetTeamLeaveRequestViewModel leaveRequest)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@LeaveRequestId",leaveRequest.LeaveRequestId},
                };
                dal.ExecuteScalar("uspAcceptLeave", dict);
                this.AddNotification("Leave Accepted", NotificationType.SUCCESS);

                return RedirectToAction("GetTeamLeaveRequest");
            }
            catch(Exception ex)
            {
                ViewBag.LeaveAccept = "Leave Accept Error ";
            }
            return RedirectToAction("LeaveAccept");


        }

        public ActionResult LeaveReject(GetTeamLeaveRequestViewModel leaveRequest)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@LeaveRequestId",leaveRequest.LeaveRequestId},

                };
                dal.ExecuteScalar("uspRejectLeave", dict);
                this.AddNotification("Leave Rejected", NotificationType.WARNING);

                return RedirectToAction("GetTeamLeaveRequest");

            }
            catch(Exception ex)
            {
                ViewBag.LeaveReject = "Leave Reject Error";
            }

            return RedirectToAction("LeaveReject");


        }
        public ActionResult GetTeamSpecificUserDetails(Employee emp)
        {
            try
            {
                if (HttpContext.Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",HttpContext.Session["EmpId"]}
            };
                    DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);
                    EmployeeViewModel employee = new EmployeeViewModel();
                    employee.employees = dTableToEmployeeModel.DataTabletoEmployeeModel(EmpTable);
                    Employee employeeowndetail = new Employee();
                    employeeowndetail = employee.employees[0];

                    return View(employeeowndetail);
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");


                }

            }
            catch (Exception ex)
            {
                ViewBag.GetUserOwnDetails = "Could not get User Own Details";
                return View();
            }
            return View();

        }


    }
}