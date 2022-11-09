﻿using EmployeeManagementSystem.ConversionService;
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
using static EmployeeManagementSystem.Controllers.AccountsController;

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

        private EMSContext db = new EMSContext();
        DataAccessService dal = new DataAccessService();
        private LeaveViewModel LeaveView;
        DTableToLeaveModel lm = new DTableToLeaveModel();
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();



       
        public ActionResult LeaveRequest(LeaveRequest model)
        {
            try
            {
                if (HttpContext.Session["EmpId"] != null)
                {
                    Dictionary<string, object> leavedict = new Dictionary<string, object>() {
                { "@EmployeeId",HttpContext.Session["EmpId"]},
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
            }
            catch(Exception ex)
            {
                ViewBag.LeaveRequest = "Leave Request Not Successful";
                return View();
            };
            return RedirectToAction("Login","Accounts");   
               
        }
        public ActionResult LeaveSummary(Leave obj)
        {
            try
            {
                if (HttpContext.Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",HttpContext.Session["EmpId"]},
            };


                    DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetLeaveSummary", dict);
                    LeaveViewModel leaveViewModel = new LeaveViewModel();
                    leaveViewModel.getleaves = lm.DataTabletoLeaveModel(EmpTable);
                    ViewData["getleaves"] = leaveViewModel.getleaves;
                    LeaveView = leaveViewModel;
                    return View(ViewData);

                }
            }
            catch(Exception ex)
            {
                ViewBag.LeaveSummary = "Could not get Leave Summary";
                return View();
            };
            
            return RedirectToAction("Login", "Accounts");


        }

        public ActionResult GetUserDetails(Leave obj)
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
                    ViewData["userdetails"] = employee.employees;
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
                if (HttpContext.Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId",HttpContext.Session["EmpId"]}
            };
                    DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetLeaveRequest", dict);
                    LeaveRequestViewModel leaveRequest = new LeaveRequestViewModel();
                    leaveRequest.leaveRequests = DTableToLeaveRequestModel.DataTabletoLeaveModel(EmpTable);
                    ViewData["getleaverequest"] = leaveRequest.leaveRequests;
                    return View(ViewData);

                }
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }

            }
            catch(Exception ex)
            {
                ViewBag.GetLeaveRequest = "Could not get Leave Request";
                return View();
            }
            return View();





        }





        /* [ValidateAntiForgeryToken]*/
        //[Authorize(Roles = "Admin")]

    }
}