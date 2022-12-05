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
using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystemInfrastructure.EmployeeBL;

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

       //Leave Request Page
        public ActionResult LeaveRequest()
        {
                return View();
        }


        //Saves the Leave Request of the User
        public ActionResult SaveLeaveRequest(LeaveRequest model)
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                //Procced if Session is not Empty
                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    object op = employeeService.SaveLeaveRequest(model,Empid);            //Saves the Leave Request of the User and returns 1
                    if (Convert.ToInt32(op) == 1)
                    {
                        this.AddNotification("Leave Requested Successfully", NotificationType.SUCCESS);  //Pass the Success notification
                        return RedirectToAction("GetLeaveRequest", "Employee");

                    }
                    else if(Convert.ToInt32(op) == 0)
                    {
                        ViewBag.Error = "Length does not match dates";
                        this.AddNotification("Leave Requested not filed", NotificationType.ERROR);          //Pass the ERROR notification
                        return RedirectToAction("LeaveRequest");
                    }
                    else if(op==null)
                    {
                        this.AddNotification("Leave Requested not filed", NotificationType.ERROR);       //Pass the ERROR notification

                        return RedirectToAction("GetUserDetails");
                    }

                    
                }
                //Procced if Session is Empty

                else
                {
                    return RedirectToAction("Login", "Accounts");

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

        //Gets the Leave Summary of a Employee
        public ActionResult LeaveSummary(Leave obj)
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                //Procced if Session is not Empty
                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    var Leave = employeeService.LeaveSummary(obj, Empid);           //Gets the Leave Summary of a Employee
                    //ViewData["getleaves"] = Leave.getleaves;
                    
                    return View(Leave);
                }
                //Procced if Session is  Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }

            }
            catch(Exception ex)
            {
                ViewBag.LeaveSummary = "Could not get Leave Summary";
                return View();
            };
        }

        //public ActionResult GetUserDetails(Leave obj)
        //{
        //    try
        //    {
        //        int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
        //        if (HttpContext.Session["EmpId"] != null)
        //        {
        //            EmployeeService employeeService = new EmployeeService();
        //            var details = employeeService.GetUserDetails(obj, Empid);
        //            ViewData["userdetails"] = details.employees;
        //            return View();
                    

        //        }
        //        else
        //        {
        //            return RedirectToAction("Login", "Accounts");

        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ViewBag.GetUserDetails = "Could not get User Details";
        //        return View();

        //    };
        //    return View();


        //}


        //Gets Details of a User
        public ActionResult GetUserOwnDetails()
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                //Procced if Session is not Empty

                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    AdminViewModelList details = employeeService.GetUserOwnDetails(Empid);       //Gets Details of a User

                    return View(details.allEmployees[0]);
                }
                //Procced if Session is Empty

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


        //Gets Leave Request of the User
        public ActionResult GetLeaveRequest(LeaveRequest obj)
        {
            try
            {
                int Empid = Convert.ToInt16(HttpContext.Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    EmployeeService employeeService = new EmployeeService();
                    var details = employeeService.GetLeaveRequest(Empid);               // Gets Leave Request of the User
                    //ViewData["getleaverequest"] = details.leaveRequests;       
                    return View(details);
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