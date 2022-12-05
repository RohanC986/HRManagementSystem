using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.AdminBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
           
            return View();
        }
        public ActionResult GetAllEmployeesDetails(LoginViewModel model, string emp)
        {

            try

            {
                EmployeesService employees = new EmployeesService();
                int EmpId = Convert.ToInt32(Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    AdminViewModelList op = employees.GetAllEmployeesDetails(model, emp);
                    ViewData["allEmployees"] = op.allEmployees;
                    AdminViewModelList EmpAllOver = op;
                    ViewData["RoleId"] = model.RoleId;
                    return View(op);
                    //ViewData["RoleId"] = model.RoleId;
                    //return View(ViewData["allEmployees"]);
                }

                return RedirectToAction("Login", "Accounts");
            }
            catch (Exception e)
            {
                ViewBag.GetAllEmployeesDetailsError = "List of Users not found !";

            }


            return RedirectToAction("GetAllEmployeesDetails", "Admin");


        }
    }
}