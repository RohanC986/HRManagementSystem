using EmployeeManagementSystem.ConversionService;
using EmployeeManagementSystem.DataAccessLayer;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using Chunk = iTextSharp.text.Chunk;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace EmployeeManagementSystem.Controllers
{
    [Route("controller")]
    public class AdminController : Controller
    {
        private AdminViewModel EmpAllOver;
        DataAccessService dal = new DataAccessService();
        DTableToEmployeeIdNameViewModel dtEIN = new DTableToEmployeeIdNameViewModel();
        DTableToProjectModel dtP = new DTableToProjectModel();
        DTableToEmployeeModel cs = new DTableToEmployeeModel();
        DTableToDepartmentsModel dataTabletoDepartmentsModel = new DTableToDepartmentsModel();
        DTableToDesignationModel DTableToDesignationModel = new DTableToDesignationModel();
        DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();
        DTableToRolesModel dtRole = new DTableToRolesModel();

        DTableToEmployeeIdNameViewModel dtEmpIdName = new DTableToEmployeeIdNameViewModel();


        public List<Role> RolesList { get; private set; }

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
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees", dict);
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.allEmployees = cs.DataTabletoEmployeeModel(EmpTable);
            ViewData["allEmployees"] = adminViewModel.allEmployees;
            EmpAllOver = adminViewModel;
            return View(ViewData);
        }

       

        public ViewResult AddNewEmp(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
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
            Dictionary<string, object> dict1 = new Dictionary<string, object>();
            DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles"/*, dict*/, dict1);
            Role roleOptions = new Role();
            roleOptions.RolesList = dtRole.DataTableToRolesModel(dt);
            ViewData["roleOptions"] = roleOptions;

            DataTable dtDesignation = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation"/*, dict*/, dict1);
            Designation designation = new Designation();
            designation.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(dtDesignation);
            ViewData["designationOptions"] = designation;

            return View();
        }

        public ActionResult SaveNewEmp(Employee model)
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
            return View();
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
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles", dict);
            Role roleOptions = new Role();
            roleOptions.RolesList = dtRole.DataTableToRolesModel(dt);
            ViewData["roleOptions"] = roleOptions;


            DataTable dtDesignation = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation", dict);
            Designation designation = new Designation();
            designation.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(dtDesignation);
            ViewData["designationOptions"]=designation;
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
            EmployeeIdNameViewModel employeeIdNameViewModel = new EmployeeIdNameViewModel();
            employeeIdNameViewModel.EmployeeIdNameList = dtEmpIdName.DataTableToEmployeeIdNameViewModel(EmpIdname);
            ViewData["AllEmpIdName"] = employeeIdNameViewModel;

            return View();
        }

        public void SaveProject(Project model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@ProjectName",model.ProjectName },
                {"@ProjectHeadEmployeeId",model.ProjectHeadEmployeeId }
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
            projectsList.ProjectList = dtP.DataTableToProjectModel(ProjectsList);
            /*ViewData["AllEmpIdName"] = EmpIdname;*/
            ViewData["EmpIdNameList"] = empIdnameViewModel;
            ViewData["ProjectsList"] = projectsList;

            return View();
        }

        public ActionResult AddLogin()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpDt = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees", dict);

            Employee EmpDR = new Employee();

            EmpDR.EmployeeList= cs.DataTabletoEmployeeModel(EmpDt);

            ViewData["EmpCodeOption"]= EmpDR;

            return View();
        }
        public ActionResult SaveLogin(Login model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@EmployeeId",model.EmployeeId},
                { "@Username",model.Username},
                { "@Password",model.Password},
                { "@LastLogin",model.LastLogin},


            };
            object check = dal.ExecuteNonQuery("uspAddNewLogin", dict);
            Console.WriteLine(check);
            if (check == null)
            {
                ViewBag.Message = "Invalid credentials";
            }

            return RedirectToAction("GetAllEmployeesDetails");
            
        }

        public void SaveProjectMember(ProjectMembers model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@EmployeeId",model.EmployeeId },
                {"@ProjectId",model.ProjectId }

            };
            dal.ExecuteNonQuery("uspSaveProjectMember", dict);
        }

        public ActionResult DisableEmp(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@EmployeeId",model.EmployeeId},

            };
            object check = dal.ExecuteNonQuery("uspDisableEmp", dict);
            Console.WriteLine(check);
            if (check == null)
            {
                ViewBag.Message = "Invalid credentials";
            }

            return RedirectToAction("GetAllEmployeesDetails", "Admin");
        }


        public ActionResult AddDesignation()
        {

            return View();

        }
        public ActionResult SaveDesignation(Designation designation)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@DesignationName",designation.DesignationName}
            };
            object check = dal.ExecuteNonQuery("uspAddDesignation", dict);
            return RedirectToAction("GetAllDesignation");

        }



        public ActionResult GetAllDesignation()
        {

            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable datatable = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation", dict);
            Designation designation1 = new Designation();
            designation1.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(datatable);
            ViewData["designation"] = designation1.DesignationsList;
            return View(ViewData);

        }

        public ActionResult EditDesignation(Designation model)
        {
            return View(model);
        }

        public ActionResult UpdateDesignation(Designation model)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@DesignationId",model.DesignationId},
                { "@DesignationName",model.DesignationName}
            };
            object check = dal.ExecuteNonQuery("uspUpdateDesignation", dict);
            if (check != null)
            {
                ViewData["success"] = "success";

            }
            return RedirectToAction("GetAllDesignation");
        }

        public ActionResult DeleteDesignation(Designation model)
        {

            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@DesignationId",model.DesignationId},
            };
            object check = dal.ExecuteNonQuery("uspDeleteDesignation", dict);
            return RedirectToAction("GetAllDesignation");
        }

        public ActionResult Report(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@EmployeeId",model.EmployeeId},
            };
            DataTable datatable = dal.ExecuteDataSet<DataTable>("uspGetReport", dict);
            LeaveRequestViewModel leaveRequest = new LeaveRequestViewModel();
            leaveRequest.leaveRequests = DTableToLeaveRequestModel.DataTabletoLeaveModel(datatable);
            ViewData["leaveRequest"] = leaveRequest.leaveRequests;
            ExportToPdf(datatable, model.EmployeeId);
            return View();


        }

        public ViewResult RoleView()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles", dict);
            Role role = new Role();
            role.RolesList = dtRole.DataTableToRolesModel(dt);
            return View(role);
        }

        public ViewResult AddRole()
        {

            return View();
        }
        public ActionResult SaveRole(Role model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@RoleName", model.RoleName}
            };
            dal.ExecuteNonQuery("uspSaveRole", dict);

            return RedirectToAction("RoleView", "Admin");

        }
        public ActionResult DeleteRole(Role model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@RoleName", model.RoleName}
            };
            dal.ExecuteNonQuery("uspDeleteRole", dict);

            return RedirectToAction("RoleView", "Admin");

        }
        public void ExportToPdf(DataTable myDataTable, int model)
        {
            DataTable dt = myDataTable;
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            Font font13 = FontFactory.GetFont("ARIAL", 6);
            Font font18 = FontFactory.GetFont("ARIAL", 8);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                if (dt.Rows.Count > 0)
                {
                    PdfPTable PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = 200f;
                    PdfTable.LockedWidth = true;

                    PdfPCell PdfPCell = new PdfPCell(new Phrase(new Chunk("Employee Details", font18)));
                    PdfPCell.Border = Rectangle.NO_BORDER;
                    PdfTable.AddCell(PdfPCell);
                    DrawLine(writer, 25f, pdfDoc.Top - 30f, pdfDoc.PageSize.Width - 25f, pdfDoc.Top - 30f, new BaseColor(System.Drawing.Color.Red));
                    pdfDoc.Add(PdfTable);

                    PdfTable = new PdfPTable(dt.Columns.Count);
                    PdfTable.SpacingBefore = 20f;
                    for (int columns = 0; columns <= dt.Columns.Count - 1; columns++)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[columns].ColumnName, font18)));
                        PdfTable.AddCell(PdfPCell);
                    }

                    for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
                    {
                        for (int column = 0; column <= dt.Columns.Count - 1; column++)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font13)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }
                    pdfDoc.Add(PdfTable);
                }
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=Report" + model + DateTime.Now.Date.ToString() + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);
                Response.Flush();
                Response.End();
            }
            catch (DocumentException de)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(de.Message)
            catch (IOException ioEx)
            {
            }
            // System.Web.HttpContext.Current.Response.Write(ioEx.Message)
            catch (Exception ex)
            {
            }
        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }













    }
}