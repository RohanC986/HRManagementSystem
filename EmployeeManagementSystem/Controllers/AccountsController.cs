using EmployeeManagementSystem.DataAccessLayer;
using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            try
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
                object output = dal.ExecuteScalar("uspcheckCredentials", dict);
                if (output == null)
                {
                    ViewBag.Loign = "User Not Found";
                    return RedirectToAction("login");
                }
                ViewBag.Loign = "Logged In Successfully";
                Session["EmpId"] = output;
                Dictionary<string, object> DictRole = new Dictionary<string, object>() {
                { "@EmployeeId",output}
                };

                object role = dal.ExecuteScalar("getUserRole", DictRole);
                Session["role"] = role;
                ViewData["role"] = role;

                Console.WriteLine(output);
                if (output == null)
                {
                    return RedirectToAction("login");
                }
                else
                {
                    if ((role).ToString() == "Employee")
                    {
                        return RedirectToAction("GetUserOwnDetails", "Employee");
                    }
                    else if ((role).ToString() == "Admin")
                    {
                        return RedirectToAction("GetAllEmployeesDetails", "Admin");
                    }
                    else if ((role).ToString() == "Team Lead")
                    {
                        return RedirectToAction("GetAllTeamEmps", "TeamLead");
                    }



                }

                return View();


            }
            catch (Exception e)
            {
                Session["LoginError"] = "User Not Found";
                //return RedirectToAction("login");
            }

            return RedirectToAction("login");

            }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }








    }
}