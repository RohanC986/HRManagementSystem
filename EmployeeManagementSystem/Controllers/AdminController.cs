using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.AdminBL;
using EmployeeManagementSystemInfrastructure.ConversionService;
using EmployeeManagementSystemInfrastructure.TeamLeadBL;
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
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        DTableToEmployeeIdNameViewModel dtEmpIdName = new DTableToEmployeeIdNameViewModel();
        DTableToAccountDetailsModel dtAccountDetailsModel = new DTableToAccountDetailsModel();
        EncryptDecryptConversion encryptDecryptConversion = new EncryptDecryptConversion();
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
        public ActionResult GetAllEmployeesDetails(LoginViewModel model, string emp)
        {

            try

            {
                Employees employees = new Employees();
                int EmpId = Convert.ToInt32(Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    AdminViewModel op = employees.GetAllEmployeesDetails(model, emp);
                    ViewData["allEmployees"] = op.allEmployees;
                    EmpAllOver = op;
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



        public ActionResult AddNewEmp()
        {
            try
            {
                Employees employees = new Employees();
                int EmpId = Convert.ToInt32(Session["EmpId"]);
                if (HttpContext.Session["EmpId"] != null)
                {
                    var roles = employees.GetRoles();
                    ViewData["roleOptions"] = roles;
                    var designation = employees.GetDesignation();
                    ViewData["designationOptions"] = designation;

                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.AddNewEmpError = "List Not Found";
            }


            return RedirectToAction("GetAllEmployeesDetails", "Admin");


        }

        public ActionResult SaveNewEmp(Employee model)
        {
            try
            {
                if (Session["EmpId"] != null)
                {

                    Employees employees = new Employees();
                    var check = employees.SaveNewEmp(model);
                    if (check == null)
                    {
                        ViewBag.Message = "Invalid credentials";
                    }

                    this.AddNotification("Employee Added Successfully", NotificationType.SUCCESS);
                    return RedirectToAction("AccountDetails", "Admin");

                }
            }
            catch (Exception e)
            {
                ViewBag.SaveNewEmpError = "Data not saved";
                return RedirectToAction("AddNewEmp", "Admin");
            }

            return RedirectToAction("AccountDetails", "Admin");

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
            try
            {
                if (Session["EmpId"] != null)
                {
                    Employees employees = new Employees();
                    var roles = employees.GetRoles();
                    var designation = employees.GetDesignation();
                    ViewData["roleOptions"] = roles;
                    ViewData["designationOptions"] = designation;
                    return View(model);
                }
            }
            catch (Exception e)
            {
                ViewBag.EditEmpError = "Page not reloaded";
            }


            return RedirectToAction("GetAllEmployeesDetails", "Admin");

        }

        public ActionResult UpdateEmpDetails(Employee model)
        {
            try
            {
                Employees employees = new Employees();
                var check = employees.UpdateEmpDetails(model);
                this.AddNotification("Updated Successfully", NotificationType.SUCCESS);
                return RedirectToAction("GetAllEmployeesDetails");
            }
            catch (Exception e)
            {
                ViewBag.UpdateEmpDetailsError = "Data not Updated";
                return RedirectToAction("GetAllEmployeesDetails", "Admin");
            }
            return RedirectToAction("EditEmp");


        }

        public ActionResult Department(string emp)
        {
            try
            {
                ProjectService projectService = new ProjectService();
                if (Session["EmpId"] != null)
                {
                    if (emp != null)
                    {

                        DepartmentListViewModel department = projectService.Departments(emp);
                        ViewData["TeamEmps"] = department.DepartmentsViews;
                        return View(department.DepartmentsViews);
                    }
                    DepartmentListViewModel department1 = projectService.Departments(emp);
                    ViewData["TeamEmps"] = department1.DepartmentsViews;
                    return View(department1.DepartmentsViews);
                }
            }
            catch (Exception e)
            {
                ViewBag.DepartmentMessage = "List Not Loaded";
                return View();
            }


            return RedirectToAction("GetAllEmployeesDetails", "Admin");


        }


        public ActionResult AddProject()
        {
            try
            {

                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    EmployeeIdNameViewModel empIdName = projectService.AddProject();
                    ViewData["AllEmpIdName"] = empIdName;


                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.AddProjectError = "Page loading error";
            }


            return RedirectToAction("GetAllEmployeesDetails", "Admin");


        }

        public ActionResult SaveProject(Project model)
        {
            try
            {
                ProjectService projectService = new ProjectService();
                int op = projectService.SaveProject(model);
                if (op > 0)
                {
                    this.AddNotification("Project Saved Successfully", NotificationType.SUCCESS);

                }
                return RedirectToAction("Department", "Admin");
            }
            catch (Exception e)
            {
                ViewBag.SaveProjectError = "Data not saved";
            }
            return RedirectToAction("AddProject", "Admin");




        }

        public ActionResult AddProjectMembers()
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    EmployeeIdNameViewModel empIdnameViewModel = projectService.GetEmpId();
                    Project projectsList = projectService.AddProjectMembers();
                    ViewData["EmpIdNameList"] = empIdnameViewModel;
                    ViewData["ProjectsList"] = projectsList;
                    //this.AddNotification("Project Added Successfully", NotificationType.SUCCESS);
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.AddProjectMembersError = "Page Loading Error";
                return RedirectToAction("GetAllEmployeesDetails", "Admin");
            }



            return RedirectToAction("Login", "Accounts");


        }

        public ActionResult AddLogin()
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    //Dictionary<string, object> dict = new Dictionary<string, object>();

                    //DataTable EmpDt = dal.ExecuteDataSet<DataTable>("uspgetLoginEmployees", dict);

                    //Employee EmpDR = new Employee();

                    //EmpDR.EmployeeList = cs.DataTabletoEmployeeModel(EmpDt);
                    LoginService loginService = new LoginService();
                    Employee EmpDR = loginService.AddLogin();
                    ViewData["EmpCodeOption"] = EmpDR;

                    return View();

                }
            }
            catch (Exception e)
            {
                ViewBag.AddLoginError = "Page loading error";

            }
            return View();




        }
        public ActionResult SaveLogin(Login model)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    LoginService loginService = new LoginService();
                    int check = loginService.SaveLogin(model);
                    Console.WriteLine(check);
                    if (check == 0)
                    {
                        ViewBag.Message = "Invalid credentials";
                    }
                    this.AddNotification("Credentials Added Successfully", NotificationType.SUCCESS);


                    return RedirectToAction("GetAllEmployeesDetails", "Admin");
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
                    ProjectService projectService = new ProjectService();
                    int op = projectService.SaveProjectMember(model);
                    if (op > 0)
                    {
                        this.AddNotification("Member added Successfully", NotificationType.SUCCESS);
                    }
                    return RedirectToAction("Department", "Admin");
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
                    Employees employees = new Employees();
                    int check = employees.DisableEmp(model);
                    Console.WriteLine(check);
                    if (check > 0)
                    {
                        ViewBag.Message = "Invalid credentials";
                    }
                    this.AddNotification("Employee Disabled Successfully", NotificationType.SUCCESS);

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



        public ActionResult EnableEmp(Employee model)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    Employees employees = new Employees();
                    int check = employees.EnableEmp(model);
                    Console.WriteLine(check);
                    if (check > 0)
                    {
                        ViewBag.Message = "Invalid credentials";
                    }
                    this.AddNotification("Employee Enables Successfully", NotificationType.SUCCESS);

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
                    Employees employees = new Employees();
                    int check = employees.SaveDesignation(designation);
                    if (check > 0)
                    {
                        this.AddNotification("Designation Added Successfully", NotificationType.SUCCESS);
                    }
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
                    Employees employees = new Employees();
                    Designation designation = employees.GetAllDesignation();
                    ViewData["designation"] = designation.DesignationsList;
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
                    Employees employees = new Employees();
                    int check = employees.UpdateDesignation(model);
                    if (check > 0)
                    {
                        ViewData["success"] = "success";

                    }
                    this.AddNotification("Designation Updated Successfully", NotificationType.SUCCESS);

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
                    Employees employees = new Employees();
                    int check = employees.DeleteDesignation(model);
                    if (check > 0)
                    {
                        this.AddNotification("Designation Added Successfully", NotificationType.SUCCESS);
                    }


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
                    this.AddNotification("Report Dowmloaded Successfully", NotificationType.SUCCESS);
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
                    Employees employees = new Employees();
                    Role role = employees.GetRoles();
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
                    Employees employees = new Employees();
                    int check = employees.SaveRole(model);
                    if (check > 0)
                    {
                        this.AddNotification("Role Added Successfully", NotificationType.SUCCESS);
                    }

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
                    Employees employees = new Employees();
                    int check = employees.DeleteRole(model);
                    this.AddNotification("Role Deleted Successfully", NotificationType.SUCCESS);
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
                    Employees employees = new Employees();
                    TeamEmpDetailsViewModel tempEmpDetialsView = employees.GetAllTeamEmpsAdmin(emp);
                    ViewData["teamEmps"] = tempEmpDetialsView.teamEmps;

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
                    //Dictionary<string, object> dict = new Dictionary<string, object>();

                    //DataTable EmpDt = dal.ExecuteDataSet<DataTable>("uspEmpsWithoutAC", dict);
                    //EmployeeIdNameViewModel empname = new EmployeeIdNameViewModel();
                    //empname.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpDt);
                    Employees employee = new Employees();
                    EmployeeIdNameViewModel empname = employee.AccountDetails(emp); 
                    ViewData["EmpName"] = empname;



                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.AccountDetails = "Page Loading Error";

            }
            return RedirectToAction("SaveAccountDetails", "Admin");

        }
        public ActionResult SaveAccountDetails(AccountDetails ac)
        {
            try
            {
                Employees employee = new Employees();
                int check = employee.SaveAccountDetails(ac);
                if(check > 0)
                {
                    this.AddNotification("Details Added Successfully", NotificationType.SUCCESS);

                }
                return RedirectToAction("AddLogin", "Admin");
            }
            catch (Exception e)
            {
                ViewBag.SaveAccountDetails = "Dat not Saved !";
                return RedirectToAction("AccountDetails");
            }

        }



        public ActionResult GetSpecificUserDetails(Employee emp)
        {
            try
            {
                
                if (HttpContext.Session["EmpId"] != null)
                {
                    int EmpId = Convert.ToInt32(HttpContext.Session["EmpId"]);
                    Employees employees = new Employees();
                    AdminViewModel employeeowndetail = employees.GetSpecificUserDetails(emp.EmployeeId);

                    return View(employeeowndetail);
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");


                }

            }
            catch (Exception ex)
            {
                ViewBag.GetUserOwnDetails = "Could not get User Own Details";
                return View();
            }
            return View();

        }

        //public ActionResult GetTeamLeadLeaveRequest()
        //{
        //    try
        //    {
        //        int EmpId = Convert.ToInt32(Session["EmpId"]);
        //        TeamLead leavesService = new TeamLead();
        //        GetTeamLeaveRequestViewModel op = leavesService.TeamLeaveRequest(EmpId);
        //        ViewData["TeamLeadRequest"] = op;
        //        return View(op);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.GetTeamLeaveRequest = "Leave request Error";
        //    }
        //    //TeamEmps = tempEmpDetialsView;
        //    return RedirectToAction("");
        //}
        public ActionResult GetTeamLeadLeaveRequest()

        {

            try
            {
                int EmpId = Convert.ToInt32(Session["EmpId"]);
                Employees leavesService = new Employees();
                GetTeamLeaveRequestViewModel op = leavesService.TeamLeadRequest(EmpId);
                ViewData["TeamLeadRequest"] = op;
                return View(op);
            }
            catch (Exception ex)
            {
                ViewBag.GetTeamLeaveRequest = "Leave request Error";
            }
            //TeamEmps = tempEmpDetialsView;
            return RedirectToAction("GetTeamLeaveRequest");

        }

    }
}