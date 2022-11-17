
using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemInfrastructure.ConversionService;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EmployeeManagementSystem.Controllers.AccountsController;
using EmployeeManagementSystemInfrastructure.TeamLeadBL;
using EmployeeManagementSystemInfrastructure.AdminBL;

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
        DTableToLeaveModel dtLeave = new DTableToLeaveModel();
        // GET: TeamLead
        public ViewResult GetAllTeamEmps(string emp)
        {
            try
            {
                    int empid = Convert.ToInt32(Session["EmpId"]);               
                    List<TeamEmpDetailsViewModel> employees = new List<TeamEmpDetailsViewModel>();
                    LeavesService leavesService = new LeavesService();
                    employees = leavesService.GetTeamEmps( emp,empid);
                    ViewData["teamEmps1"] = employees;
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
                int balanceLeaves=leavesummary.getleaves[0].BalanceLeaves;
                int leavesTaken= leavesummary.getleaves[0].LeavesTaken;
                int unpaidleaves = leavesummary.getleaves[0].UnPaidLeaves;

                if (leavesTaken < 15)
                {
                    if((leavesTaken + (leaveRequest.LengthOfLeave))<=15)
                    {
                        Dictionary<string, object> dict4 = new Dictionary<string, object>()
                        {
                            {"@EmployeeId",leaveRequest.EmployeeId },
                            {"@BalanceLeaves",(15-(leavesTaken+(leaveRequest.LengthOfLeave))) },
                            {"@LeavesTaken",(leavesTaken+(leaveRequest.LengthOfLeave)) }
                        };
                        dal.ExecuteNonQuery("uspUpdateLeavesBefore15",dict4);
                    }
                    else if((leavesTaken + (leaveRequest.LengthOfLeave))>15)
                    {
                        Dictionary<string, object> dict5 = new Dictionary<string, object>()
                        {
                            {"@EmployeeId",leaveRequest.EmployeeId },
                            {"@BalanceLeaves",0 },
                            {"@LeavesTaken",leavesTaken+leaveRequest.LengthOfLeave },
                            { "@UnPaidLeaves",(leavesTaken+leaveRequest.LengthOfLeave)-15}
                        };
                        dal.ExecuteNonQuery("uspUpdateLeavesAfter15",dict5);
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