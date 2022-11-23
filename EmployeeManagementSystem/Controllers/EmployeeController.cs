using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemInfrastructure.ConversionService;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemCore.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using static EmployeeManagementSystem.Controllers.AccountsController;
using EmployeeManagementSystem.DBContext;
using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystemInfrastructure.EmployeeBL;
using Org.BouncyCastle.Crypto.Tls;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]")]
    [NoCache]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class EmployeeController : Controller
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            con = new SqlConnection(constr);

        }

        //private EMSContext db = new EMSContext();
        //DataAccessService dal = new DataAccessService();
        //private LeaveViewModel LeaveView;
        //DTableToLeaveModel lm = new DTableToLeaveModel();
        //DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        //DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();



       
        public ActionResult LeaveRequest()
        {
            
                return View();
            
               
        }

        


        public ActionResult SaveLeaveRequest(LeaveRequest model)
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    object op = employeeService.SaveLeaveRequest(model,Empid);
                    if (Convert.ToInt32(op) == 1)
                    {
                        this.AddNotification("Leave Requested Successfully", NotificationType.SUCCESS);
                        return RedirectToAction("GetLeaveRequest", "Employee");

                    }
                    else if(Convert.ToInt32(op) == 0)
                    {
                        ViewBag.Error = "Length does not match dates";
                        this.AddNotification("Leave Requested not filed", NotificationType.ERROR);
                        return RedirectToAction("LeaveRequest");
                    }
                    else if(op==null)
                    {
                        this.AddNotification("Leave Requested not filed", NotificationType.ERROR);

                        return RedirectToAction("GetUserDetails");
                    }

                    
                }
            }
            catch (Exception ex)
            {
                ViewBag.LeaveRequest = "Leave Request Not Successful";
                this.AddNotification("Leave Requested not filed", NotificationType.ERROR);
               
                return RedirectToAction("GetUserDetails");
            };
            return RedirectToAction("GetUserDetails", "Accounts");

        }


        public ActionResult LeaveSummary(Leave obj)
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    var Leave = employeeService.LeaveSummary(obj, Empid);
                    ViewData["getleaves"] = Leave.getleaves;
                    
                    return View(ViewData);

                }
            }
            catch(Exception ex)
            {
                ViewBag.LeaveSummary = "Could not get Leave Summary";
                return View();
            };
            
            return RedirectToAction("GetUserDetails", "Employee");


        }

        public ActionResult GetUserDetails(Leave obj)
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    var details = employeeService.GetUserDetails(obj, Empid);
                    ViewData["userdetails"] = details.employees;
                    return View();
                    

                }
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }
            }
            catch(Exception ex)
            {
                ViewBag.GetUserDetails = "Could not get User Details";
                return View();

            };
            return View();


        }

        public ActionResult GetUserOwnDetails()
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    AdminViewModelList details = employeeService.GetUserOwnDetails(Empid);

                    return View(details.allEmployees[0]);
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");


                }

            }
            catch(Exception ex)
            {
                ViewBag.GetUserOwnDetails = "Could not get User Own Details";
                return View();
            }
            return View();



        }



        public ActionResult GetLeaveRequest(LeaveRequest obj)
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    var details = employeeService.GetLeaveRequest(Empid);
                    ViewData["getleaverequest"] = details.leaveRequests;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }

            }
            catch(Exception ex)
            {
                ViewBag.GetLeaveRequest = "Could not get Leave Request";
                return RedirectToAction("GetUserOwnDetails");
            }
            return View();





        }





        /* [ValidateAntiForgeryToken]*/
        //[Authorize(Roles = "Admin")]

    }
}