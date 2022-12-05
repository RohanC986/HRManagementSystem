using EmployeeManagementSystem.Extensions;
using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.AdminBL;
using EmployeeManagementSystemInfrastructure.ConversionService;
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
        DataAccessService dal = new DataAccessService();
        DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();
        EncryptDecryptConversion encryptDecryptConversion = new EncryptDecryptConversion();
        AdminViewModelToEmployeeModel adminToEmployee = new AdminViewModelToEmployeeModel();
        public List<Role> RolesList { get; private set; }
        public AdminController()
        {
        }
        // GET: Admin

        [Route("[controller]/getallemployees")]

        //Admin Dashboard Page
        public ActionResult GetAllEmployeesDetails(LoginViewModel model, string searchEmp)
        {
            try
            {
                EmployeesService employees = new EmployeesService();                                       //Create a object of Employees Service
                int EmpId = Convert.ToInt32(Session["EmpId"]);                                             //Convert EmployeeId from Session to Int
                //Procced if EmployeeId is Present in Session
                if (HttpContext.Session["EmpId"] != null)
                {
                    AdminViewModelList EmployeeDetails = employees.GetAllEmployeesDetails(model, searchEmp); //Gets all the Employee Details and pass it into object of AdminViewModelList
                    ViewData["RoleId"] = model.RoleId;                                                       //Pass the RoleId to ViewData
                    return View(EmployeeDetails);
                }
                //Redirect to Login Page
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }

            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.GetAllEmployeesDetailsError = "List of Users not found !";
                return RedirectToAction("GetAllEmployeesDetails", "Admin");

            }
        }



        //Add New Employee View
        public ActionResult AddNewEmp()
        {
            try
            {
                EmployeesService employees = new EmployeesService();
                int EmpId = Convert.ToInt32(Session["EmpId"]);    
                if (HttpContext.Session["EmpId"] != null)
                {
                    var roles = employees.GetRoles();                                                  //Get all the roles and pass it to varialble
                    ViewData["roleOptions"] = roles;                                                   //Pass the roles into ViewData
                    var designation = employees.GetDesignation();                                       //Get all the Designation and pass it to varialble
                    ViewData["designationOptions"] = designation;                                       // Pass the Designation into ViewData
                    ViewData["EmployeeCode"] = employees.GetLastEmployeeCode();                         //Pass the Last Employee Code into ViewData

                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }
            }
            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.AddNewEmpError = "List Not Found";
                return RedirectToAction("GetAllEmployeesDetails", "Admin");
            }
        }



        //Edit Employee View
        public ActionResult EditEmp(AdminViewModel model)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {

                    Employee EmployeeModel = new Employee();
                    EmployeeModel = adminToEmployee.AdminToEmployee(model);     //Gets all the info of a particular employee                        
                    EmployeesService employees = new EmployeesService();
                    var roles = employees.GetRoles();                         //Gets all the roles
                    var designation = employees.GetDesignation();             //Gets all the Designation
                    ViewData["roleOptions"] = roles;
                    ViewData["designationOptions"] = designation;
                    return View(EmployeeModel);
                }
                //Procced if Session is Empty

                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.EditEmpError = "Page not reloaded";
            }


            return RedirectToAction("GetAllEmployeesDetails", "Admin");

        }


        //Updates the Employee Details of particular Employee
        public ActionResult UpdateEmpDetails(Employee model)
        {
            try
            {
                EmployeesService employees = new EmployeesService();
                var check = employees.UpdateEmpDetails(model);  
                if(check != null)
                {
                    this.AddNotification("Updated Successfully", NotificationType.SUCCESS);
                }
                return RedirectToAction("GetAllEmployeesDetails");
            }

            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.UpdateEmpDetailsError = "Data not Updated";
                return RedirectToAction("GetAllEmployeesDetails", "Admin");
            }
        }


        //All the teams and along with its Team Leader Page
        public ActionResult Department(string searchProjectName)
        {
            try
            {
                ProjectService projectService = new ProjectService();
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    DepartmentListViewModel department1 = projectService.Departments(searchProjectName);    //Gets all the Project Details
                    ViewData["TeamEmps"] = department1.DepartmentsViews;                                    //Pass the data into ViewData
                    return View(department1.DepartmentsViews);
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login","Accounts");
                }
               
                
            }
            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.DepartmentMessage = "List Not Loaded";
                return RedirectToAction("GetAllEmployeeDetails");
            }
        }

        //Add Project Page 
        public ActionResult AddProject()
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    EmployeeIdNameViewModelList empIdName = projectService.GetAllEmployees();         //Gets all the Employees 
                    ViewData["AllEmpIdName"] = empIdName;                                             //Pass the Data in ViewData
                    return View();
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.AddProjectError = "Page loading error";
            }
            return RedirectToAction("GetAllEmployeesDetails", "Admin");

        }

        //Saves the New Project into DataBase
        public ActionResult SaveProject(Project model)
        {
            try
            {
                ProjectService projectService = new ProjectService();
                int check = projectService.SaveProject(model);                                      
                if (check > 0)
                {
                    this.AddNotification("Project Saved Successfully", NotificationType.SUCCESS);               //pass success notification if succeeded

                }
                return RedirectToAction("Department", "Admin");
            }
            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.SaveProjectError = "Data not saved";
            }
            return RedirectToAction("AddProject", "Admin");




        }
        //Add Project Members Page
        public ActionResult AddProjectMembers(int projectId)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    EmployeeIdNameViewModelList empIdnameViewModel = projectService.GetEmpId();                         //Gets the EmployeeId of all Employees
                    Project projectsList = projectService.GetProject(projectId);                                        //Gets the ProjectId of all Projects
                    ViewData["EmpIdNameList"] = empIdnameViewModel;                                                     //Pass all the EmployeeId  into ViewData
                    ViewData["ProjectsList"] = projectsList;                                                            //Pass all the ProjectId into ViewData
                    ViewData["ProjectName"] = projectsList.ProjectList[0].ProjectName;                                  //Pass all the Project Name into ViewData
                    //this.AddNotification("Project Added Successfully", NotificationType.SUCCESS);
                    return View(new ProjectMembersViewModel { ProjectId = projectId });
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("GetAllTeamEmpsAdmin", ViewData["ProjectName"]);

                }
            }
            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.AddProjectMembersError = "Page Loading Error";
                return RedirectToAction("GetAllEmployeesDetails", "Admin");
            }

        }
        //Add Login Page
        public ActionResult AddLogin(AddNewEmployeeViewModel Employee)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    LoginService loginService = new LoginService();
                    Employee EmployeesWithOutLogin = loginService.GetEmployeesWithOutLogin();           //Gets all the employees without Login credentials
                    ViewData["EmpCodeOption"] = EmployeesWithOutLogin;                                  //Pass the data to ViewData
                    return View(Employee);
                }
                //Procced if Session is  Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            //Catch any Exception if occurred
            catch (Exception e)
            {
                ViewBag.AddLoginError = "Page loading error";
                return RedirectToAction("AddLogin");

            }

        }


        //Saves the details of new Employee
        public ActionResult SaveEmployee(AddNewEmployeeViewModel Employee)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    LoginService loginService = new LoginService();
                    int check = loginService.SaveEmployee(Employee);                //Saves the details of new Employee
                    if (check == 0)
                    {
                        ViewBag.Message = "Invalid credentials";
                    }
                    this.AddNotification("Credentials Added Successfully", NotificationType.SUCCESS);       //Pass success notification
                    return RedirectToAction("GetAllEmployeesDetails", "Admin");                             //Redirect to dashboard
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }
            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                ViewBag.SaveLogin = "Could Not Save Login";
                return RedirectToAction("AddNewEmp", "Admin", Employee);

            }
        }

        //Adds Project Members to Project
        public ActionResult SaveProjectMember(ProjectMembers model)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    int check = projectService.SaveProjectMember(model);                                 //Adds Project Members to Project
                    if (check > 0)
                    {
                        this.AddNotification("Member added Successfully", NotificationType.SUCCESS);     //Pass success notification
                    }
                    string projectName = Convert.ToString(ViewData["ProjectName"]);
                    return RedirectToAction("GetAllTeamEmpsAdmin", new { projectId = model.ProjectId });
                }
                //Procced if Session is  Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                ViewBag.GetAllDesignation = "Could Not Save Project Member";
                return View();

            }
        }

        //Disables the Login of a particular Employee
        public ActionResult DisableEmp(Employee model)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    EmployeesService employee = new EmployeesService();
                    int check = employee.DisableEmp(model);                    //Disables the Login of a particular Employee
                    if (check > 0)
                    {
                        this.AddNotification("Employee Disabled Successfully", NotificationType.SUCCESS);        //Pass success notification
                    }
                    return RedirectToAction("GetAllEmployeesDetails", "Admin");                                 //Redirect to DashBoard
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }


            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                ViewBag.GetAllDesignation = "Could Not Disable Employee ";
                return View();

            }
        }


        //Enables the Login of a particular Employee
        public ActionResult EnableEmp(Employee model)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    int check = employees.EnableEmp(model);                         //Enables the Login of a particular Employee
                    if (check > 0)
                    {
                        this.AddNotification("Employee Enables Successfully", NotificationType.SUCCESS);        //Pass success notification
                    }
                    return RedirectToAction("GetAllEmployeesDetails", "Admin");                      //Redirect to DashBoard
                }
                //Procced if Session is  Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }


            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                ViewBag.GetAllDesignation = "Could Not Disable Employee ";
                return View();

            }

        }

        //Add Designation Page
        public ActionResult AddDesignation()
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    return View();
                }
                //Procced if Session is  Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                return View();

            }
        }


        //Saves the Designation
        public ActionResult SaveDesignation(Designation designation)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    int check = employees.SaveDesignation(designation);             //Saves the Designation 
                    if (check > 0)
                    {
                        this.AddNotification("Designation Added Successfully", NotificationType.SUCCESS);       //Pass the Success Notification
                    }
                    return RedirectToAction("GetAllDesignation");                                               //Redirects to Add Desigantion Page

                }
                //Procced if Session is not Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }
            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                ViewBag.SaveDesignation = "Could Not Save Designation";
                return View();

            }
        }


        //Gets all Designations Page
        public ActionResult GetAllDesignation()
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    Designation designation = employees.GetAllDesignation();     //Gets all the Designations
                    ViewData["designation"] = designation.DesignationsList;      //Pass the Data in ViewData
                    return View(designation);
                }
                //Procced if Session is Empty

                else
                {
                    return RedirectToAction("Login", "Accounts");

                }

            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                ViewBag.GetAllDesignation = "Could Not Get AllDesignation";
                return View();

            }
        }

        //Edit Designation Page
        public ActionResult EditDesignation(Designation model)
        {
            try
            {
                //Procced if Session is not Empty

                if (Session["EmpId"] != null)
                {
                    return View(model);

                }
                //Procced if Session is  Empty

                else
                {
                    return RedirectToAction("Login", "Accounts");
                }

            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                ViewBag.EditDesignation = "Could Not Edit Designation";
                return View();
            }
        }


        //Edit the Name of a particular Designatiom
        public ActionResult UpdateDesignation(Designation model)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    int check = employees.UpdateDesignation(model);                         //Edit the Name of a particular Designatiom

                    if (check > 0)  
                    {
                        this.AddNotification("Designation Updated Successfully", NotificationType.SUCCESS);     //Pass the success notification
                        ViewData["success"] = "success";
                    }
                    return RedirectToAction("GetAllDesignation");
                }
                //Procced if Session is  Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }
            }
            //Catch any Exception if occurred

            catch (Exception ex)
            {
                ViewBag.UpdateDesignation = "Could Not Update Designation";
                return View();

            }

        }


        //Delete a Particular Designation
        public ActionResult DeleteDesignation(Designation model)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    int check = employees.DeleteDesignation(model);                         //Delete a Particular Designation

                    if (check > 0)
                    {
                        this.AddNotification("Designation Added Successfully", NotificationType.SUCCESS);           //Pass the success notification
                    }
                    return RedirectToAction("GetAllDesignation");
                }
                //Procced if Session is  Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            //Catch any Exception if occurred

            catch (Exception ex)
            {
                ViewBag.DeleteDesignation = "Could Not Delete Designation";
                return View();

            }
        }
        
        //Generates Pdf report of leave history of a particular employee
        public ActionResult Report(Employee model)
        {
            try
            {
                //Procced if Session is not Empty

                if (Session["EmpId"] != null)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@EmployeeId",model.EmployeeId},
            };
                    DataTable datatable = dal.ExecuteDataSet<DataSet>("uspGetReport", dict);                      //Gets the data of leave history of a particular employee
                    //Procced if there is any data in the Datatable
                    if (datatable.Rows.Count > 0)
                    {
                        LeaveRequestViewModel leaveRequest = new LeaveRequestViewModel();
                        leaveRequest.leaveRequests = DTableToLeaveRequestModel.DataTabletoLeaveModel(datatable);  //Pass the data from the datatable into list of type LeaveRequestViewModel 
                        ViewData["leaveRequest"] = leaveRequest.leaveRequests;                                    //Pass the data to ViewData
                        ExportToPdf(datatable, model.EmployeeId);
                        this.AddNotification("Report Dowmloaded Successfully", NotificationType.SUCCESS);         //return success notification
                    }
                    //Procced if the data is empty in the datatable
                    else
                    {
                        this.AddNotification("Report Empty", NotificationType.WARNING);                         //Pass the warning notification
                        return RedirectToAction("GetAllEmployeesDetails");
                    }
                    return View();
                }

            }
            //Catch any Exception if occurred

            catch (Exception ex)
            {
                ViewBag.Report = "Report Not Found";
                return View();
            }

            return RedirectToAction("Login", "Accounts");


        }
        //Page of Role for CRUD operation 
        public ActionResult RoleView()
        {
            try
            {
                //Procced if Session is not Empty

                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    Role role = employees.GetRoles();                                       //Gets the data of all roles
                    return View(role);
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }

            }
            catch (Exception ex)
            {
                ViewBag.RoleView = "RoleView Not Found";
                return View();
            }


            return RedirectToAction("Login", "Accounts");

        }


        //Add Role View
        public ViewResult AddRole()
        {

            return View();
        }

        //Saves new role into database
        public ActionResult SaveRole(Role model)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    int check = employees.SaveRole(model);                                     //Saves new role into database and returns 1
                    if (check > 0)
                    {
                        this.AddNotification("Role Added Successfully", NotificationType.SUCCESS);  //Pass the success notification
                    }

                    return RedirectToAction("RoleView", "Admin");
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }

            }
            //Catch any Exception if occurred
            catch (Exception ex)
            {
                ViewBag.Role = "Role Not Found";
                return View();
            }
        }


        //Deletes any particular Role
        public ActionResult DeleteRole(Role model)
        {
            try
            {
                //Procced if Session is not Empty

                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    int check = employees.DeleteRole(model);                     //Deletes any particular Role and return 1 if succeded
                    this.AddNotification("Role Deleted Successfully", NotificationType.SUCCESS);    //Pass the success notification
                    return RedirectToAction("RoleView", "Admin");

                }
                //Procced if Session is Empty

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


        //Converts DataTable to PDF
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


        //Generates boarder lines for the table
        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }



        //Gets all the Employees of a particular team
        public ActionResult GetAllTeamEmpsAdmin(int ProjectId)
        {
            try
            {
                //Procced if Session is not Empty

                if (Session["EmpId"] != null)
                {
                    EmployeesService employees = new EmployeesService();
                    TeamEmpDetailsViewModel tempEmpDetialsView = employees.GetAllTeamEmpsAdmin(ProjectId);       //Gets all the Employees of a particular team
                    ViewData["teamEmps"] = tempEmpDetialsView.teamEmps;                                          //Pass the Data to ViewData
                    //ViewData["projectId"] = tempEmpDetialsView.teamEmps[0].ProjectId;
                    ViewData["projectId"] = ProjectId;                                                           //Pass the ProjectId data to ViewData

                    return View(tempEmpDetialsView);


                }
                //Procced if Session is  Empty

                else
                {
                    return RedirectToAction("Login", "Accounts");

                }

            }
            catch (Exception ex)
            {
                ViewBag.GetAllTeamEmpsAdmin = "Cannot not All Team Emps";
                return View();
            }


            return RedirectToAction("Login", "Admin");


        }
       //Bank Account Details Page
        public ActionResult AccountDetails(AddNewEmployeeViewModel emp)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    EmployeesService employee = new EmployeesService();
                    EmployeeIdNameViewModelList empname = employee.AccountDetails();                    //Gets all the employees without account details saved in database
                    ViewData["EmpName"] = empname;                                                      //Pass all the employee Names to ViewData
                    ViewData["EmployeeCode"] = employee.GetLastEmployeeCode();                          //Gets last employee code 
                    int prevempcode = Convert.ToInt32(ViewData["EmployeeCode"].ToString().Substring((ViewData["EmployeeCode"].ToString()).Length - 3, 3)); //Generates the EmployeeCode
                    string empcode = "WB-" + ((prevempcode + 1).ToString()).PadLeft(3, '0');
                    emp.EmployeeCode = empcode;
                    return View(emp);
                }
                //Procced if Session is not Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            catch (Exception e)
            {
                ViewBag.AccountDetails = "Page Loading Error";

            }
            return RedirectToAction("SaveAccountDetails", "Admin");

        }


        //Gets the data of a specific Employee
        public ActionResult GetSpecificUserDetails(Employee emp)
        {
            try
            {
                //Procced if Session is not Empty
                if (HttpContext.Session["EmpId"] != null)
                {
                    int EmpId = Convert.ToInt32(HttpContext.Session["EmpId"]);
                    EmployeesService employees = new EmployeesService();
                    AdminViewModel employeeowndetail = employees.GetSpecificUserDetails(emp.EmployeeId);        //Gets the data of a specific Employee

                    return View(employeeowndetail);
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");


                }

            }
            catch (Exception ex)
            {
                ViewBag.GetUserOwnDetails = "Could not get User Own Details";
                return RedirectToAction("Login", "Accounts");
            }
            return View();

        }

        //Gets the Leave Request of Team Leads
        public ActionResult GetTeamLeadLeaveRequest()

        {

            try
            {
                //Procced if Session is not Empty
                if (HttpContext.Session["EmpId"] != null)
                {
                    int EmpId = Convert.ToInt32(Session["EmpId"]);
                    EmployeesService leavesService = new EmployeesService();
                    GetTeamLeaveRequestViewModel leaveRequests = leavesService.TeamLeadRequest(EmpId);         //Gets the Leave Request of Team Leads
                    ViewData["TeamLeadRequest"] = leaveRequests;
                    return View(leaveRequests);
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
                    
            }
            catch (Exception ex)
            {
                ViewBag.GetTeamLeaveRequest = "Leave request Error";

            }
            //TeamEmps = tempEmpDetialsView;
            return RedirectToAction("GetTeamLeaveRequest");

        }


        //Assign Team Lead Page
        public ActionResult AssignTeamLead(Project project)
        {
            try
            {
                //Procced if Session is not Empty
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    Project projects = projectService.GetProjectsWithoutTeamLead();             //Gets Team Lead who aro not assigned to any Team
                    EmployeeIdNameViewModelList empIdName = projectService.GetAllEmployees();   //Gets all the Employees
                    ViewData["TeamLead"] = empIdName;
                    ViewData["Projects"] = projects;
                }
                //Procced if Session is Empty
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAllEmployeeDetails");
            }
        }

        //Adds Team Lead to the Project
        public ActionResult AddTeamLead(Project project)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    int check = projectService.AssignTeamLead(project);             //Adds Team Lead to the Project and returns 1 if succeed   
                    if (check != 0)
                    {
                        this.AddNotification("Team Lead Assigned Successfully", NotificationType.SUCCESS);      //Pass the Success notification

                    }
                    return RedirectToAction("Departments");


                }
                else
                {
                    return RedirectToAction("Login", "Accounts");


                }
            }
            catch (Exception ex)
            {
                this.AddNotification("Team Lead Assigned Failed", NotificationType.ERROR);
                return RedirectToAction("AssignTeamLead");
            }
        }



        //Change Team Lead Page
        public ActionResult ChangeTeamLead(Project project)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    Project projects = projectService.GetProjects();                    //Gets all the Projects
                    EmployeeIdNameViewModelList empIdName = projectService.GetTeamLead();       //Gets all the Team Leads
                    ViewData["TeamLeadWithProject"] = empIdName;
                    ViewData["AllProjects"] = projects;
                    return View();


                }
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Department");
            }
        }


        //Updates the Team Lead
        public ActionResult UpdateTeamLead(Project project)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    int check = projectService.ChangeTeamLead(project);                //Updates the Team Lead and returns 1 if succeed
                    if (check != 0)
                    {
                        this.AddNotification("Team Lead Updated Successfully", NotificationType.SUCCESS);  //Pass the success notification

                    }
                    return RedirectToAction("Department");


                }
                else
                {
                    return RedirectToAction("Login", "Accounts");

                }
            }
            catch (Exception ex)
            {
                this.AddNotification("Team Lead Assigned Failed", NotificationType.ERROR);
                return RedirectToAction("AssignTeamLead");
            }
        }


        //Removes the Employee from the Team
        public ActionResult RemoveEmployee(TeamEmpDetailsViewModel model)
        {
            try
            {
                if (Session["EmpId"] != null)
                {
                    ProjectService projectService = new ProjectService();
                    int check = projectService.RemoveEmp(model.EmployeeId);              //Removes the Employee from the Team and returns 1 if succeed
                    if (check > 0)
                    {
                        this.AddNotification("Employee Removed", NotificationType.WARNING);         //Pass the success notification
                    }

                    return RedirectToAction("GetAllTeamEmpsAdmin", "Admin", new { projectId = model.ProjectId });
                }
                else
                {
                    return RedirectToAction("Login", "Accounts");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAllTeamEmps");
            }

        }
    }
}