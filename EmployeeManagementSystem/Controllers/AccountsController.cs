
using EmployeeManagementSystem.Extensions;

using iTextSharp.text;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemInfrastructure.ConversionService;
using EmployeeManagementSystemCore.ViewModels;
using WebGrease.Activities;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemInfrastructure.AccountsBL;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]")]

    [NoCache]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration

    public class AccountsController : Controller
    {

        private readonly string constr;
        DataAccessService dal = new DataAccessService();
        DTableToEmployeeModel cs = new DTableToEmployeeModel();
        DTableToLoginViewModel dTable = new DTableToLoginViewModel();
        EncryptDecryptConversion encryptDecryptConversion = new EncryptDecryptConversion();

        LoginViewModel LoginViewModel = new LoginViewModel();


        public AccountsController()
        {
            constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }


        // GET: Accounts
        public ActionResult Login()
        {
            //HttpContext.Session.Clear();
            //HttpContext.Session.Abandon();
            return View();
        }

        [Route("[controller]/login")]
        [HttpPost]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [NoCache]

        public ActionResult Login(LoginViewModel model)
            {
            try
            {
                
               
                HttpContext.Session["EmpId"] = null;
                ViewData.Clear();

                LoginLogout loginLogout = new LoginLogout();
                LoginViewModel op = loginLogout.Login(model);
                if (op.EmployeeId != null && op.LoginMessage== null)
                {
                    Dictionary<string, object> dict3 = new Dictionary<string, object>()
                    {

                            { "EmployeeId",model.EmployeeId }
                    };
                    dal.ExecuteNonQuery("uspResetAttempts",dict3);
                    Session["role"] = model.RoleId;
                    Session["EmpId"] = model.EmployeeId;
                    this.AddNotification("Logged In Successfully", NotificationType.SUCCESS);
                    TempData["Login"] = model;
                    if (model.RoleId.ToString() == null)
                    {
                        this.AddNotification("Invalid Username or Password", NotificationType.ERROR);
                        ViewBag.LoginError = "Invalid Username or Password";

                        return View();
                    }
                    else if (Convert.ToInt32(model.IsActive) == 0)
                    {
                        this.AddNotification("User Blocked", NotificationType.ERROR);
                        ViewBag.LoginError = "User Blocked";

                        return View();

                    }
                    else
                    {

                        if (model.RoleId == 3)
                        {
                            return RedirectToAction("GetUserOwnDetails", "Employee");
                        }
                        else if (model.RoleId == 1)
                        {
                            return RedirectToAction("GetAllEmployeesDetails", "Admin",model);
                        }
                        else if (model.RoleId == 2)
                        {
                            return RedirectToAction("GetAllTeamEmps", "TeamLead");
                        }

                    }
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            
                return View();


            }
            catch (Exception e)
            {
                ViewBag.LoginError= "User Not Found";
                return View();

            }
            return View("Error");
            //return RedirectToAction("login");

            }

        [NoCache]

        public ActionResult Logout()
        {   
            FormsAuthentication.SignOut();
            Response.Cookies.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
            Response.Cache.SetNoStore();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            Response.AddHeader("Cache-Control", "no-cache, no-store,must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            Response.Cookies.Clear();
            //session.removeall();
            HttpContext.Session.Clear();
            this.AddNotification("logged Out ", NotificationType.WARNING);

            //session["empid"] = null;
            return RedirectToAction("Login");
        }

        public class NoCacheAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                if (filterContext == null) throw new ArgumentNullException("filterContext");

                var cache = GetCache(filterContext);

                cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                cache.SetValidUntilExpires(false);
                cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                cache.SetCacheability(HttpCacheability.NoCache);
                cache.SetNoStore();

                base.OnResultExecuting(filterContext);
            }

            protected virtual HttpCachePolicyBase GetCache(ResultExecutingContext filterContext)
            {
                return filterContext.HttpContext.Response.Cache;
            }
        }









    }
}