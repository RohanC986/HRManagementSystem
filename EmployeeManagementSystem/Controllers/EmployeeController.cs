using EmployeeManagementSystem.Models;
using System;
using EmployeeManagementSystem.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceProcess;
using EmployeeManagementSystem.DataAccessLayer;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private EMSContext db = new EMSContext();
        DataAccessService dal = new DataAccessService();



        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEmployee()
        {

            return RedirectToAction("Added Successfulyy");
        }
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
                { "@Status",model.Status}


            };
            object output = dal.ExecuteNonQuery("LeaveRequest", leavedict);
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



        /* [ValidateAntiForgeryToken]*/
        //[Authorize(Roles = "Admin")]

    }
}