
using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using EmployeeManagementSystemInfrastructure.TeamLeadBL;
using System;
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
        
        //Gets all the Employees under the Team Lead
        public ActionResult GetAllTeamEmps(string searchEmp)
        {
            try
            {
                //Procced if Session is Active
                if (HttpContext.Session["EmpId"] != null)
                {
                    int empid = Convert.ToInt32(Session["EmpId"]);
                    TeamEmpDetailsViewModel employees = new TeamEmpDetailsViewModel();
                    TeamLead leavesService = new TeamLead();
                    employees = leavesService.GetTeamEmps(searchEmp, empid);            //Gets all the Employees under the Team Lead
                    //ViewData["teamEmps1"] = employees;
                    return View(employees);
                }
                //Procced if Session is not Active
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }

            }
            catch (Exception ex)
            {
                ViewBag.GetAllTeamEmps = "GetAllTeamEmps Error";
                return View();
            }
            //return View(ViewData);


        }

        //Gets the Leaves requests of Team Members
        public ActionResult GetTeamLeaveRequest()
        {
            try
            {
                //Procced if Session is Active
                if (HttpContext.Session["EmpId"] != null)
                {
                    int EmpId = Convert.ToInt32(Session["EmpId"]);
                    TeamLead leavesService = new TeamLead();
                    GetTeamLeaveRequestViewModel LeaveRequests = leavesService.TeamLeaveRequest(EmpId);        //Gets the Leaves requests of Team Members
                   // ViewData["TeamLeaveRequest"] = op;
                    return View(LeaveRequests);
                }
                //Procced if Session is not Active
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            catch (Exception ex)
            {
                ViewBag.GetTeamLeaveRequest = "Leave request Error";
            }
            //TeamEmps = tempEmpDetialsView;
            return RedirectToAction("GetTeamLeaveRequest");

        }
        //Accepts Leave Request of a Team Member or Team Lead 
        public ActionResult LeaveAccept(GetTeamLeaveRequestViewModel leaveRequest)
        {
            try
            {
                //Procced if Session is Active
                if (HttpContext.Session["EmpId"] != null)
                {
                    TeamLead teamLead = new TeamLead();
                    var check = teamLead.LeaveAccept(leaveRequest);             //Accepts Leave Request of a Team Member or Team Lead  & returns 1 
                    if (check != null)
                    {
                        this.AddNotification("Leave Accepted", NotificationType.SUCCESS);       //Pass success notification

                    }
                    teamLead.LeaveAcceptResponse(leaveRequest);
                    //Procced if user is an Team Lead
                    if (Convert.ToInt32(Session["role"]) == 2)
                    {
                        return RedirectToAction("GetTeamLeaveRequest");

                    }
                    //Procced if user is an Admin
                    else if (Convert.ToInt32(Session["role"]) == 1)
                    {
                        return RedirectToAction("GetTeamLeadLeaveRequest", "Admin");
                    }
                }
                //Procced if Session is not Active
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }

            }
            catch (Exception ex)
            {
                ViewBag.LeaveAccept = "Leave Accept Error ";
                return RedirectToAction("GetTeamLeaveRequest");
            }
            return RedirectToAction("GetTeamLeaveRequest");


        }


        //Accepts Leave Request of a Team Member or Team Lead 
        public ActionResult LeaveReject(GetTeamLeaveRequestViewModel leaveRequest)
        {
            try
            {
                //Procced if Session is Active

                if (HttpContext.Session["EmpId"] != null)
                {
                    TeamLead teamLead = new TeamLead();
                    var check = teamLead.LeaveReject(leaveRequest);                              //Accepts Leave Request of a Team Member or Team Lead and returns 1
                    if (check != null)
                    {
                        this.AddNotification("Leave Rejected", NotificationType.WARNING);       //Pass the success notification

                    }
                    //Procced if user is an Team Lead
                    if (Convert.ToInt32(Session["role"]) == 2)
                    {
                        return RedirectToAction("GetTeamLeaveRequest");

                    }
                    //Procced if user is an Admin
                    else if (Convert.ToInt32(Session["role"]) == 1)
                    {
                        return RedirectToAction("GetTeamLeadLeaveRequest", "Admin");
                    }
                }
                //Procced if Session is not Active
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }

            }
            catch (Exception ex)
            {
                ViewBag.LeaveReject = "Leave Reject Error";
            }
            return RedirectToAction("LeaveReject");


        }


        //Get User Details of a Specific Employee
        public ActionResult GetTeamSpecificUserDetails(int employeeId)
        {
            try
            {
                if (HttpContext.Session["EmpId"] != null)
                {
                    TeamLead teamLead = new TeamLead();
                    var EmpId = Convert.ToInt32(HttpContext.Session["EmpId"]);
                   
                    
                    /*emp.DOB = new DateTime(2002, 1, 1);*/
                    AdminViewModel Employee = teamLead.GetUserSpecificDetails(employeeId);                  //Get User Details of a Specific Employee
                    Employee.DOB = "lkasndlkasndklasnd";
                    return View("GetUserOwnDetails", Employee);
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


        //Get Details of User
        public ActionResult GetUserOwnDetails(TeamEmpDetailsViewModel emp)
        {
            try
            {
                if (HttpContext.Session["EmpId"] != null)
                {
                    TeamLead teamLead = new TeamLead();
                    var EmpId = Convert.ToInt32(HttpContext.Session["EmpId"]);
                    AdminViewModel UserDetails = teamLead.GetUserSpecificDetails(EmpId);      //Get Details of User
                    return View(UserDetails);
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
        }
    }
}