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
using static EmployeeManagementSystem.Controllers.AccountsController;
using Chunk = iTextSharp.text.Chunk;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace EmployeeManagementSystem.Controllers
{
    [Route("controller")]
    [NoCache]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]

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
        DTableToTeamEmpModel tableToTeamEmpModel = new DTableToTeamEmpModel();

        DTableToEmployeeIdNameViewModel dtEmpIdName = new DTableToEmployeeIdNameViewModel();
        DTableToAccountDetailsModel dtAccountDetailsModel = new DTableToAccountDetailsModel();

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
        public ActionResult GetAllEmployeesDetails()
        {
            if (HttpContext.Session["Empid"] != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees", dict);
                AdminViewModel adminViewModel = new AdminViewModel();
                adminViewModel.allEmployees = cs.DataTabletoEmployeeModel(EmpTable);
                ViewData["allEmployees"] = adminViewModel.allEmployees;
                EmpAllOver = adminViewModel;
                return View(ViewData);
            }

            return RedirectToAction("Login", "Accounts");

        }



        public ActionResult AddNewEmp(Employee model)
        {
            if (HttpContext.Session["Empid"] != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();

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
            return RedirectToAction("AccountDetails", "Accounts");

        }

        public ActionResult SaveNewEmp(Employee model)
        {
            if (Session["EmpId"] != null)
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
                return RedirectToAction("AccountDetails", "Admin");
            }
            return RedirectToAction("Login", "Accounts");

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
            if (Session["EmpId"] != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles", dict);
                Role roleOptions = new Role();
                roleOptions.RolesList = dtRole.DataTableToRolesModel(dt);
                ViewData["roleOptions"] = roleOptions;
                DataTable dtDesignation = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation", dict);
                Designation designation = new Designation();
                designation.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(dtDesignation);
                ViewData["designationOptions"] = designation;
                return View(model);
            }

            return RedirectToAction("Login", "Accounts");

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

            if (Session["EmpId"] != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                DataTable Department = dal.ExecuteDataSet<DataTable>("uspGetAllTeams", dict);
                DepartmentListViewModel departmentsViewModel = new DepartmentListViewModel();
                departmentsViewModel.DepartmentsViews = dataTabletoDepartmentsModel.DataTabletoDepartmentsModel(Department);
                ViewData["TeamEmps"] = departmentsViewModel.DepartmentsViews;
                return View(ViewData);
            }

            return RedirectToAction("Login", "Accounts");


        }


        public ActionResult AddProject()
        {
            if (Session["EmpId"] != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();


                DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetEmpIdName", dict);
                EmployeeIdNameViewModel empIdName = new EmployeeIdNameViewModel();
                empIdName.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpIdname);
                ViewData["AllEmpIdName"] = empIdName;

                return View();
            }

            return RedirectToAction("Login", "Accounts");


        }

        public ActionResult SaveProject(Project model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@ProjectName",model.ProjectName },
                {"@ProjectHeadEmployeeId",model.ProjectHeadEmployeeId }
            };
            dal.ExecuteNonQuery("uspSaveProject", dict);

            return RedirectToAction("AddProjectMembers", "Admin");



        }

        public ActionResult AddProjectMembers()
        {
            if (Session["EmpId"] != null)
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


            return RedirectToAction("Login", "Accounts");


        }

        public ActionResult AddLogin()
        {
            if (Session["EmpId"] != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();

                DataTable EmpDt = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees", dict);

                Employee EmpDR = new Employee();

                EmpDR.EmployeeList = cs.DataTabletoEmployeeModel(EmpDt);

                ViewData["EmpCodeOption"] = EmpDR;

                return View();

            }

            return RedirectToAction("Login", "Accounts");

        }
        public ActionResult SaveLogin(Login model)
        {
            try
            {
                if (Session["EmpId"] != null)
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
            }
            catch (Exception ex)
            {
                ViewBag.SaveLogin = "Could Not Save Login";
                return View();

            }


            return RedirectToAction("Login", "Accounts");

        }

        public ActionResult SaveProjectMember(ProjectMembers model)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@EmployeeId",model.EmployeeId },
                {"@ProjectId",model.ProjectId }

            };
                    dal.ExecuteNonQuery("uspSaveProjectMember", dict);

                    return RedirectToAction("AddProjectMembers", "Admin");
                }
            }
            catch (Exception ex)
            {
                ViewBag.GetAllDesignation = "Could Not Save Project Member";
                return View();

            }


            return RedirectToAction("Login", "Accounts");


        }

        public ActionResult DisableEmp(Employee model)
        {
            try
            {
                if (Session["EmpId"] != null)
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


            }
            catch (Exception ex)
            {
                ViewBag.GetAllDesignation = "Could Not Disable Employee ";
                return View();

            }


            return RedirectToAction("Login", "Accounts");

        }


        public ActionResult AddDesignation()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.AddDesignation = "Could Not Add Designation";
                return View();

            }
            return View();
        }
        public ActionResult SaveDesignation(Designation designation)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@DesignationName",designation.DesignationName}
            };
                    object check = dal.ExecuteNonQuery("uspAddDesignation", dict);
                    return RedirectToAction("GetAllDesignation");
                }
            }
            catch (Exception ex)
            {
                ViewBag.SaveDesignation = "Could Not Save Designation";
                return View();

            }



            return RedirectToAction("Login", "Accounts");


        }



        public ActionResult GetAllDesignation()
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    DataTable datatable = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation", dict);
                    Designation designation1 = new Designation();
                    designation1.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(datatable);
                    ViewData["designation"] = designation1.DesignationsList;
                    return View(ViewData);
                }

            }

            catch (Exception ex)
            {
                ViewBag.GetAllDesignation = "Could Not Get AllDesignation";
                return View();

            }
            return RedirectToAction("Login", "Accounts");



        }

        public ActionResult EditDesignation(Designation model)
        {
            try
            {
                return View(model);

            }
            catch (Exception ex)
            {
                ViewBag.EditDesignation = "Could Not Edit Designation";
                return View();
            }
            return View(model);
        }

        public ActionResult UpdateDesignation(Designation model)
        {
            try
            {
                if (Session["EmpId"] != null)
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
            }
            catch (Exception ex)
            {
                ViewBag.UpdateDesignation = "Could Not Update Designation";
                return View();

            }


            return RedirectToAction("Login", "Accounts");

        }

        public ActionResult DeleteDesignation(Designation model)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@DesignationId",model.DesignationId},
            };
                    object check = dal.ExecuteNonQuery("uspDeleteDesignation", dict);
                    return RedirectToAction("GetAllDesignation");

                }
            }
            catch (Exception ex)
            {
                ViewBag.DeleteDesignation = "Could Not Delete Designation";
                return View();

            }



            return RedirectToAction("Login", "Accounts");

        }

        public ActionResult Report(Employee model)
        {
            try
            {
                if (Session["EmpId"] != null)
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

            }
            catch (Exception ex)
            {
                ViewBag.Report = "Report Not Found";
                return View();
            }

            return RedirectToAction("Login", "Accounts");


        }

        public ActionResult RoleView()
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles", dict);
                    Role role = new Role();
                    role.RolesList = dtRole.DataTableToRolesModel(dt);
                    return View(role);
                }

            }
            catch (Exception ex)
            {
                ViewBag.RoleView = "RoleView Not Found";
                return View();
            }


            return RedirectToAction("Login", "Accounts");

        }

        public ViewResult AddRole()
        {

            return View();
        }
        public ActionResult SaveRole(Role model)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@RoleName", model.RoleName}
            };
                    dal.ExecuteNonQuery("uspSaveRole", dict);

                    return RedirectToAction("RoleView", "Admin");
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }

            }
            catch (Exception ex)
            {
                ViewBag.Role = "Role Not Found";
                return View();
            }


            return RedirectToAction("Login", "Accounts");


        }
        public ActionResult DeleteRole(Role model)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@RoleName", model.RoleName}
            };
                    dal.ExecuteNonQuery("uspDeleteRole", dict);

                    return RedirectToAction("RoleView", "Admin");

                }
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }
            }
            catch (Exception ex)
            {
                ViewBag.DeleteRole = "Could Not Delete";
                return View();
            }

            return RedirectToAction("Login", "Accounts");


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


        public ActionResult GetAllTeamEmpsAdmin(Project emp)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@ProjectHeadEmployeeId",emp.ProjectHeadEmployeeId},
            };


                    DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspTeamEmps", dict);
                    TeamEmpDetailsViewModel tempEmpDetialsView = new TeamEmpDetailsViewModel();
                    tempEmpDetialsView.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable);
                    ViewData["teamEmps"] = tempEmpDetialsView.teamEmps;
                    //TeamEmps = tempEmpDetialsView;
                    return View(ViewData);

                }
                else
                {
                    return RedirectToAction("Login", "Admin");

                }

            }
            catch (Exception ex)
            {
                ViewBag.GetAllTeamEmpsAdmin = "Cannot not All Team Emps";
                return View();
            }


            return RedirectToAction("Login", "Admin");


        }

        public ActionResult AccountDetails(Project emp)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();

                    DataTable EmpDt = dal.ExecuteDataSet<DataTable>("uspEmpsWithoutAC", dict);
                    EmployeeIdNameViewModel empname = new EmployeeIdNameViewModel();
                    empname.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpDt);
                    ViewData["EmpName"] = empname;



                    return View();
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("SaveDetails", "Admin");

        }
        public ActionResult SaveDetails(AccountDetails ac)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {

                { "@EmployeeID",ac.EmployeeID},
                { "@UANNo",ac.UANNo},
                { "@BankAcNo",ac.BankAcNo},
                { "@IFSCCode",ac.IFSCCode},

            };

            dal.ExecuteNonQuery("uspAddAccountD", dict);
            return RedirectToAction("AddLogin", "Admin");
        }









    }
}