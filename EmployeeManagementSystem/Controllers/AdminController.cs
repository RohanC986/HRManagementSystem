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
        DTableToEmployeeIdNameViewModel dtEIN = new DTableToEmployeeIdNameViewModel();
        DTableToProjectModel dtP = new DTableToProjectModel();
        DTableToEmployeeModel cs = new DTableToEmployeeModel();
        DTableToDepartmentsModel dataTabletoDepartmentsModel = new DTableToDepartmentsModel();


        public AdminController()
        {

        }
        // GET: Admin
        /*public ActionResult Index()
        {
            return View();
        }*/
       

        [Route("[controller]/getallemployees")]
        public ViewResult GetAllEmployeesDetails()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees" ,dict );
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

                {"@PassportNo",model.PassportNo},

                { "@Address",model.Address},
                { "@City",model.City},
                { "@State",model.State},
                { "@Pincode",model.Pincode},
                { "@Role",model.Role},
                { "@Designation",model.Designation},
                { "@Experienced",model.Experienced},

                {"@PreviousCompanyName",model.PreviousCompanyName },

                { "@YearsOfExprience",model.YearsOfExprience},
            };
            object check = dal.ExecuteNonQuery("uspAddNewEmp", dict);
            Console.WriteLine(check);
            if (check == null)
            {
                ViewBag.Message = "Invalid credentials";
            }
            return RedirectToAction("GetAllEmployeesDetails");   
        }

        //public ActionResult UpdateEmpDetails(int EmployeeId)
        //{
        //    Employee model = new Employee(); 
        //    if (EmployeeId > null)
        //    {
        //        Dictionary<string, object> dict = new Dictionary<string, object>() {
        //        //{ "@EmployeeId",model.EmployeeId},
        //        { "@EmployeeCode",model.EmployeeCode},
        //        { "@FirstName",model.FirstName},
        //        { "@MiddleName",model.MiddleName},
        //        { "@LastName",model.LastName},
        //        { "@Email",model.Email},
        //        { "@DOB",model.DOB},
        //        { "@DOJ",model.DOJ},
        //        { "@BloodGroup",model.BloodGroup},
        //        { "@Gender",model.Gender},
        //        { "@PersonalContact",model.PersonalContact},
        //        { "@EmergencyContact",model.EmergencyContact},
        //        { "@AadharCardNo",model.AadharCardNo},
        //        { "@PancardNo",model.PancardNo},
        //        { "@Address",model.Address},
        //        { "@City",model.City},
        //        { "@State",model.State},
        //        { "@Pincode",model.Pincode},
        //        { "@Role",model.Role},
        //        { "@Designation",model.Designation},
        //        { "@Experienced",model.Experienced},
        //        { "@YearsOfExprience",model.YearsOfExprience},
        //    };
        //        object check = dal.ExecuteNonQuery("uspUpdateEmpDetails", dict);
        //    }
        //}


       public ActionResult EditEmp(Employee model)
        {
            return View(model);
        }

        public ActionResult UpdateEmpDetails(Employee model)
        {
            
            Dictionary<string, object> dict = new Dictionary<string, object>() {
                { "@EmployeeId",model.EmployeeId},
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
                { "@PassportNo",model.PassportNo},
                { "@PancardNo",model.PancardNo},
                { "@Address",model.Address},
                { "@City",model.City},
                { "@State",model.State},
                { "@Pincode",model.Pincode},
                { "@Role",model.Role},
                { "@Designation",model.Designation},
                { "@Experienced",model.Experienced},
                { "@YearsOfExprience",model.YearsOfExprience},
                { "@PreviousCompanyName",model.PreviousCompanyName}
            }; 
            object check = dal.ExecuteNonQuery("uspUpdateEmpDetails", dict);
            return RedirectToAction("GetAllEmployeesDetails");











        }

        public ActionResult Department()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable Department = dal.ExecuteDataSet<DataTable>("uspgetTeamEmps", dict);
            DepartmentListViewModel departmentsViewModel = new DepartmentListViewModel();
            departmentsViewModel.DepartmentsViews = dataTabletoDepartmentsModel.DataTabletoDepartmentsModel(Department);
            ViewData["TeamEmps"] = departmentsViewModel.DepartmentsViews;         
            return View(ViewData);
        }


        public ActionResult AddProject()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetEmpIdName", dict);
            ViewData["AllEmpIdName"] = EmpIdname;

            return View();
        }

        public void  SaveProject(Project model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@ProjectName",model.ProjectName },
                {"ProjectHeadEmployeeId",model.ProjectHeadEmployeeId }
            };
            dal.ExecuteNonQuery("uspSaveProject", dict);
            

        }

        public ActionResult AddProjectMembers()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetEmpIdName", dict);
            EmployeeIdNameViewModel empIdnameViewModel = new EmployeeIdNameViewModel();
            empIdnameViewModel.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpIdname);

            DataTable ProjectsList = dal.ExecuteDataSet<DataTable>("uspGetProjects", dict);
            Project projectsList = new Project();
            projectsList.ProjectList=dtP.DataTableToProjectModel(ProjectsList);
            /*ViewData["AllEmpIdName"] = EmpIdname;*/
            ViewData["EmpIdNameList"] = empIdnameViewModel;
            ViewData["ProjectsList"]=projectsList;

            return View();
        }

        public ActionResult AddLogin()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            {

            }

            DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetEmpIdName", dict);
            ViewData["AllEmpIdName"] = EmpIdname;

            return View();
        }





    }
}