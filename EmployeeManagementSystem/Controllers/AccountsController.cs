
using EmployeeManagementSystem.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemInfrastructure.ConversionService;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.AccountsBL;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]")]

    [NoCache]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration

    public class AccountsController : Controller
    {
        //Creating objects of various required classes
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
                
                HttpContext.Session["EmpId"] = null;                       //Initialiazing Http Context Session with null value

                ViewData.Clear();                                          //Clearing all the ViewData if present

                LoginLogoutService loginLogout = new LoginLogoutService(); //Creating a object of LoginLogoutServie


                LoginViewModel User = loginLogout.Login(model);            //Get the User info as pass into the LoginViewModel object

                //Proceed if Username EmployeeId & Password EmployeeId is Present 
                if (User.EmployeeId != null && User.UsernameMessage== null && User.PasswordMessage==null)
                {
                    HttpContext.Session["role"] = User.RoleId;                  //Store the RoleId of User in Session
                    HttpContext.Session["EmpId"] = User.EmployeeId;             //Store the EmployeeId of User in Session

                    Dictionary<string, object> dict3 = new Dictionary<string, object>()
                    {

                            { "EmployeeId",model.EmployeeId }
                    };

                    dal.ExecuteNonQuery("uspResetAttempts",dict3);             //Reset the Attempts count of User
                    Session["role"] = model.RoleId;                            //Store the RoleId of User in Session             
                    Session["EmpId"] = model.EmployeeId;                       //Store the EmployeeId of User in Session
                    FormsAuthentication.SetAuthCookie(Convert.ToString(model.EmployeeId), false);  //Set the Authentication Cookie
                    this.AddNotification("Logged In Successfully", NotificationType.SUCCESS);       //Pass the Success Notification
                    
                    //Procced if RoleId is not Present
                    if (model.RoleId.ToString() == null)
                    { 
                        this.AddNotification("Invalid Username or Password", NotificationType.ERROR); //Pass the Error Notification
                        ViewBag.LoginError = "Invalid Username or Password";                          //Store Login Error Message in ViewBag

                        return View();
                    }

                    //Procced if isActive is not Present
                    else if (Convert.ToInt32(model.IsActive) == 0)
                    {
                        
                        ViewBag.UsernameError = "User Blocked";                                     //Store Login Error Message in ViewBag 

                        return View();

                    }
                    //Procced if RoleId & isActive is present
                    else
                    {
                        //Procced if User is an Employee
                        if (model.RoleId == 3)
                        {
                            return RedirectToAction("GetUserOwnDetails", "Employee");          //Redirect User to his Profile Page
                        }

                        //Procced if User is an Admin
                        else if (model.RoleId == 1)
                        {

                            return RedirectToAction("GetAllEmployeesDetails", "Admin",User);   //Redirect User to his all employees dashboard page
                        }

                        //Procced if User is a Team Lead
                        else if (model.RoleId == 2)
                        {
                            return RedirectToAction("GetUserOwnDetails", "TeamLead",User.EmployeeId);  //Redirect User to all employees in the team page
                        }

                    }
                }

                //Proceed if Username or Password  is not present in database
                else
                {
                    ViewBag.UsernameError = User.UsernameMessage;
                    ViewBag.PasswordError = User.PasswordMessage;
                    return View();
                }
                return View();

            }

            //Catch any Exception if occured
            catch (Exception e)
            {
                ViewBag.LoginError= "User Not Found";
                return View();

            }
            return View("Error");
            //return RedirectToAction("login");

            }

        [NoCache]


        //Logout
        public ActionResult Logout()
        {   

            //Clear all the Cache
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
            HttpContext.Session.Clear();
            this.AddNotification("logged Out ", NotificationType.WARNING);
            return RedirectToAction("Login");
        }
        public class NoCacheAttribute : ActionFilterAttribute
        {

            //Clear All the cache to prevent the back page error after logout
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