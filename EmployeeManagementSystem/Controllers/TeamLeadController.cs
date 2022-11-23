
using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using EmployeeManagementSystemInfrastructure.TeamLeadBL;
using System;
using System.Collections.Generic;
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
        DTableToLeaveModel dtLeave = new DTableToLeaveModel();
        // GET: TeamLead
        public ViewResult GetAllTeamEmps(string emp)
        {
            try
            {
                int empid = Convert.ToInt32(Session["EmpId"]);
                List<TeamEmpDetailsViewModel> employees = new List<TeamEmpDetailsViewModel>();
                TeamLead leavesService = new TeamLead();
                employees = leavesService.GetTeamEmps(emp, empid);
                ViewData["teamEmps1"] = employees;
                return View(ViewData);
            }
            catch (Exception ex)
            {
                ViewBag.GetAllTeamEmps = "GetAllTeamEmps Error";
            }
            return View(ViewData);


        }


        public ActionResult GetTeamLeaveRequest()

        {

            try
            {
                int EmpId = Convert.ToInt32(Session["EmpId"]);
                TeamLead leavesService = new TeamLead();
                GetTeamLeaveRequestViewModel op = leavesService.TeamLeaveRequest(EmpId);
                ViewData["TeamLeaveRequest"] = op;
                return View(op);
            }
            catch (Exception ex)
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

                TeamLead teamLead = new TeamLead();
                var op = teamLead.LeaveAccept(leaveRequest);
                if (op != null)
                {
                    this.AddNotification("Leave Accepted", NotificationType.SUCCESS);

                }
                teamLead.LeaveAcceptResponse(leaveRequest);
                if (Convert.ToInt32(Session["role"]) == 2)
                {
                    return RedirectToAction("GetTeamLeaveRequest");

                }
                else if (Convert.ToInt32(Session["role"]) == 1)
                {
                    return RedirectToAction("GetTeamLeadLeaveRequest", "Admin");
                }
            }
            catch (Exception ex)
            {
                ViewBag.LeaveAccept = "Leave Accept Error ";
                return RedirectToAction("GetTeamLeaveRequest");
            }
            return RedirectToAction("GetTeamLeaveRequest");


        }

        public ActionResult LeaveReject(GetTeamLeaveRequestViewModel leaveRequest)
        {
            try
            {
                TeamLead teamLead = new TeamLead();
                var opp = teamLead.LeaveReject(leaveRequest);
                if (opp != null)
                {
                    this.AddNotification("Leave Rejected", NotificationType.WARNING);

                }
                if (Convert.ToInt32(Session["role"]) == 2)
                {
                    return RedirectToAction("GetTeamLeaveRequest");

                }
                else if (Convert.ToInt32(Session["role"]) == 1)
                {
                    return RedirectToAction("GetTeamLeadLeaveRequest", "Admin");
                }
            }
            catch (Exception ex)
            {
                ViewBag.LeaveReject = "Leave Reject Error";
            }
            return RedirectToAction("LeaveReject");


        }
        public ActionResult GetTeamSpecificUserDetails(TeamEmpDetailsViewModel emp)
        {
            try
            {
                TeamLead teamLead = new TeamLead();
                var EmpId = Convert.ToInt32(HttpContext.Session["EmpId"]);
                if (EmpId >= 0)
                {

                    AdminViewModel op  = teamLead.GetUserSpecificDetails(emp.EmployeeId);  

                    return View(op.allEmployees[0]);
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
        public ActionResult GetUserOwnDetails(TeamEmpDetailsViewModel emp)
        {
            try
            {
                TeamLead teamLead = new TeamLead();
                var EmpId = Convert.ToInt32(HttpContext.Session["EmpId"]);
                if (EmpId >= 0)
                {
                    AdminViewModel op = teamLead.GetUserSpecificDetails(EmpId);

                    return View(op);
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