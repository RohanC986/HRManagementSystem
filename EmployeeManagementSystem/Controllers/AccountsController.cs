using EmployeeManagementSystem.ConversionService;
using EmployeeManagementSystem.DataAccessLayer;
using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
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

                Dictionary<string, object> dict1 = new Dictionary<string, object>() {
                { "@Username",model.Username}
                
                };
                object outputUser = dal.ExecuteScalar("uspGetUserEmployeeId", dict1);

                Dictionary<string, object> dict2 = new Dictionary<string, object>() {
                { "@Password",model.Password}

                };
                object outputPass = dal.ExecuteScalar("uspGetPassEmployeeId", dict2);


               


              

                if (Convert.ToInt32(outputUser)==Convert.ToInt32(outputPass))
                {
                    Dictionary<string, object> dict3 = new Dictionary<string, object>()
                    {
                        
                            { "EmployeeId",outputUser }
                    };
                    DataTable EmpTableUser = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict3);
                    AdminViewModel adminViewModelUser = new AdminViewModel();
                    adminViewModelUser.allEmployees = cs.DataTabletoEmployeeModel(EmpTableUser);
                    HttpContext.Session["role"] = adminViewModelUser.allEmployees[0].RoleId;
                    ViewData["role"] = adminViewModelUser.allEmployees[0].RoleId;
                    HttpContext.Session["EmpId"] = adminViewModelUser.allEmployees[0].EmployeeId;
                    HttpContext.Session["IsActive"] = adminViewModelUser.allEmployees[0].IsActive;
                    dal.ExecuteNonQuery("uspResetAttempts", dict3);
                    this.AddNotification("Logged In Successfully", NotificationType.SUCCESS);


                }
                else
                {
                    


                    if (outputUser != null && outputPass == null)
                    {
                        Dictionary<string, object> dict6 = new Dictionary<string, object>()
                        {
                            {"@EmployeeId",outputUser}

                        };
                        object attempts = dal.ExecuteScalar("uspGetLoginAttempts", dict6);
                        int attempts2 = Convert.ToInt32(attempts);
                        

                        if (attempts==null)
                        {
                            Dictionary<string, object> dict7 = new Dictionary<string, object>()
                                {
                                    {"@EmployeeId",outputUser},
                                    {"@Attempts",1 }

                                };
                            dal.ExecuteNonQuery("uspAddAttempts", dict7);
                            ViewBag.LoginError = "Invalid Password";
                            this.AddNotification("Invalid Password", NotificationType.ERROR);
                            return View();
                        }
                        else if (attempts2 < 4)
                        {

                            
                            
                            dal.ExecuteNonQuery("uspIncreaseAttempts", dict6);
                            ViewBag.LoginError = $"Invalid Password({5 - attempts2} remaining out of 5)";
                            this.AddNotification($"Invalid Password({5 - attempts2} remaining out of 5)", NotificationType.ERROR);
                            return View();
                            


                        }
                        else if (attempts2 == 4)
                        {


                           
                            
                                dal.ExecuteNonQuery("uspIncreaseAttempts", dict6);
                            ViewBag.LoginError = "Last Attempt Remaining";
                            this.AddNotification("Last Attempt Remaining", NotificationType.ERROR);
                            return View();
                        }
                        else if (attempts2 == 5)
                        {
                            
                            dal.ExecuteNonQuery("uspDisableEmployee", dict6);
                            ViewBag.LoginError = "User blocked due to exceeded limit of attempts with wrong Password";

                            this.AddNotification("User blocked due to exceeded limit of attempts with wrong Password", NotificationType.ERROR);
                            return View();                        
                        }

                    }
                    else if (outputUser == null && outputPass != null)
                    {
                        ViewBag.LoginError = "Invalid Username";
                        return View()
;                        /*Dictionary<string, object> dict6 = new Dictionary<string, object>()
                        {
                            {"@EmployeeId",outputPass}

                        };
                        object attempts = dal.ExecuteScalar("uspGetLoginAttempts", dict6);
                        int attempts2 = Convert.ToInt32(attempts);


                        if (attempts == null)
                        {
                            Dictionary<string, object> dict7 = new Dictionary<string, object>()
                                {
                                    {"@EmployeeId",outputPass},
                                    {"@Attempts",1 }

                                };
                            dal.ExecuteNonQuery("uspAddAttempts", dict7);
                            ViewBag.LoginError = "Invalid Username";

                            this.AddNotification("Invalid Username", NotificationType.ERROR); 
                            return View();
                        }
                        else if (attempts2 < 4)
                        {
                            
                            dal.ExecuteNonQuery("uspAddAttempts", dict6);
                            ViewBag.LoginError = $"Invalid Username({5 - attempts2} remaining out of 5)";

                            this.AddNotification($"Invalid Username({5 - attempts2} remaining out of 5)", NotificationType.ERROR);
                            return View();
                        }
                        else if (attempts2 == 4)
                        {
                           
                            dal.ExecuteNonQuery("uspAddAttempts", dict6);
                            ViewBag.LoginError = "Last Attempt Remaining";
                            this.AddNotification("Last Attempt Remaining", NotificationType.ERROR);
                            return View();

                        }
                        else if (attempts2 == 5)
                        {
                           
                            dal.ExecuteNonQuery("uspDisableEmployee", dict6);
                            ViewBag.LoginError = "Last Attempt Remaining";
                            this.AddNotification("User blocked due to exceeded limit of attempts with wrong Username", NotificationType.ERROR);
                            return View();
                        }*/
                    }
                    else if (outputPass == null && outputUser == null)
                    {
                        this.AddNotification("Invalid Credentials", NotificationType.ERROR);
                        ViewBag.LoginError = "Invalid Credentials";
                        return View();
                    }

                }
               


                

                if (ViewData["role"].ToString() == null)
                {
                    this.AddNotification("Invalid Username or Password", NotificationType.ERROR);
                    ViewBag.LoginError = "Invalid Username or Password";

                    return View();
                }
                else if (Convert.ToInt32(HttpContext.Session["IsActive"])==0)
                {
                    this.AddNotification("User Blocked", NotificationType.ERROR);
                    ViewBag.LoginError = "User Blocked";

                    return View();

                }
                else
                {

                    if (Convert.ToInt32(ViewData["role"]) == 3)
                    {
                        return RedirectToAction("GetUserOwnDetails", "Employee");
                    }
                    else if (Convert.ToInt32(ViewData["role"]) == 1)
                    {
                        return RedirectToAction("GetAllEmployeesDetails", "Admin");
                    }
                    else if (Convert.ToInt32(ViewData["role"]) == 2)
                    {
                        return RedirectToAction("GetAllTeamEmps", "TeamLead");
                    }



                }

                //if (output == null)
                //{
                //    ViewBag.Message = "User Not Found";
                //    return View();
                //}

              


              
               
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                //Response.Cache.SetNoStore();

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