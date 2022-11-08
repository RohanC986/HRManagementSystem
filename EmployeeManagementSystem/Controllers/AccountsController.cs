using EmployeeManagementSystem.DataAccessLayer;
using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
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


                //HttpContext.Session["EmpId"] = null;
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
                HttpContext.Session["EmpId"] = output;
                Dictionary<string, object> DictRole = new Dictionary<string, object>() {
                { "@EmployeeId",output}
                };

                object role = dal.ExecuteScalar("getUserRole", DictRole);
                HttpContext.Session["role"] = role;
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
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                //Response.Cache.SetNoStore();

                return View();


            }
            catch (Exception e)
            {
                HttpContext.Session["LogimError"] = "User Not Found";
                //return RedirectToAction("login");
            }

            return RedirectToAction("login");

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