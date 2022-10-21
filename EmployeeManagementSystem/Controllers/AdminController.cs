using EmployeeManagementSystem.DBContext;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using EmployeeManagementSystem.DataAccessLayer;
using EmployeeManagementSystem.ConversionService;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System;

namespace EmployeeManagementSystem.Controllers
{
    [Route("controller")]
    public class AdminController : Controller
    {
        private  AdminViewModel EmpAllOver;
        DataAccessService dal = new DataAccessService();

        DTableToEmployeeModel cs = new DTableToEmployeeModel();


        public AdminController()
        {

        }
        // GET: Admin
        /*public ActionResult Index()
        {
            return View();
        }*/
       

        [Route("[controller]/getallemployees")]
        public ViewResult GetAllEmployees()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees", dict);
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.allEmployees  = cs.DataTabletoEmployeeModel(EmpTable);
            ViewData["allEmployees"]=adminViewModel.allEmployees;

            EmpAllOver = adminViewModel;




            return View(ViewData);
        }

        public ActionResult AddNewEmp(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {
                //{ "@EmployeeId",model.EmployeeId},
                { "@EmployeeCode",model.EmployeeCode},
                { "@FirstName",model.FirstName},
                { "@MiddleName",model.MiddleName},
                { "@LastName",model.LastName},
                { "@Email",model.Email},
                { "@DOB",model.DOB},
                { "@DOJ",model.DOJ},
                { "@BloodGroup",model.BloodGroup},
                { "@Gender",model.Gender},
                { "@PersonalContact",model.PersonalContact},
                { "@EmergencyContact",model.EmergencyContact},
                { "@AadharCardNo",model.AadharCardNo},
                { "@PancardNo",model.PancardNo},
                { "@Address",model.Address},
                { "@City",model.City},
                { "@State",model.State},
                { "@Pincode",model.Pincode},
                { "@Role",model.Role},
                { "@Designation",model.Designation},
                { "@Experienced",model.Experienced},
                { "@YearsOfExprience",model.YearsOfExprience},
            };
            object check = dal.ExecuteNonQuery("AddNewEmp", dict);
            Console.WriteLine(check);
            if (check == null)
            {
                ViewBag.Message = "Invalid credentials";
            }
            

            return View();
        }

    }
}