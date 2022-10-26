using EmployeeManagementSystem.DBContext;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using EmployeeManagementSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]")]
   

    public class AccountsController : Controller
    {
        private readonly string constr;
        DataAccessService dal = new DataAccessService();


        public AccountsController()
        {
             constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

       
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        [Route("[controller]/login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>() {
                { "@Username",model.Username},
                { "@Password",model.Password}
            };
            /* using (EMSContext context = new EMSContext())
             {
                 bool IsValidUser = context.Login.Any(user => user.Username.ToLower() ==
                      model.Username.ToLower() && user.Password == model.Password);
                 if (IsValidUser)
                 {
                     FormsAuthentication.SetAuthCookie(model.Username, false);
                     return RedirectToAction("About", "Home");
                 }
                 ModelState.AddModelError("", "invalid Username or Password");
                 return View();
             }*/
             object  output = dal.ExecuteScalar("checkCredentials",dict);
            Session["EmpId"] = output;
              Console.WriteLine(output);
            if(output==null)
            {
                ViewBag.Message = "Invalid credentials";
            }
            else
            {
                RedirectToAction("LeaveRequest","Employee");
            }
           
            return RedirectToAction("LeaveRequest", "Employee");


        }

        

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }






    }
}