using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System.Collections.Generic;
using System.Data;

namespace EmployeeManagementSystemInfrastructure.AdminBL
{
    public class EmployeesService
    {
        //Creating objects of various required classes
        DataAccessService dal = new DataAccessService();
        DTableToEmployeeIdNameViewModel dtEIN = new DTableToEmployeeIdNameViewModel();
        DTableToDesignationModel DTableToDesignationModel = new DTableToDesignationModel();
        DTableToRolesModel dtRole = new DTableToRolesModel();
        DTableToTeamEmpModel tableToTeamEmpModel = new DTableToTeamEmpModel();
        DTableToAdminViewModel ToAdminViewModel = new DTableToAdminViewModel();
        DTableToTeamLeaveRequestModel dTableToTeamLeaveRequestModel = new DTableToTeamLeaveRequestModel();
   
        //Save the New Employee Details and return the Rows affected number
        public object SaveEmployee(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {            //Pass all the info into Dictionary       
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
                { "@RoleId",model.RoleId},
                { "@DesignationId",model.DesignationId},
                { "@Experienced",model.Experienced},
                {"@PreviousCompanyName",model.PreviousCompanyName },
                { "@YearsOfExprience",model.YearsOfExprience},
            };
            object check = dal.ExecuteNonQuery("uspAddNewEmp", dict);              //Save the new employee info into Database & Pass the rows affected count to check
            return check;                                                          //return check
        }

        //Admin Dashboard - Gets all the Employees Details present in Database
        public AdminViewModelList GetAllEmployeesDetails(LoginViewModel model, string searchEmp)
        {
            //Procced if User has entered any input into search box
            if (searchEmp != null)
            {
                Dictionary<string, object> dict1 = new Dictionary<string, object>()                         //Pass the Search string into Dictionary
                        {
                            { "@FirstName",searchEmp},
                        };
                DataTable EmpTable1 = dal.ExecuteDataSet<DataTable>("uspSearchEmps", dict1);                //Get the filtered data according to String
                AdminViewModelList adminViewModel1 = new AdminViewModelList();
                adminViewModel1.allEmployees = ToAdminViewModel.DataTabletoAdminEmployeeModel(EmpTable1);   //Pass the Data from Datatable to List 
                return adminViewModel1;                                                                     //Return the viewmodel
            };
            //Procced Search String is null
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees", dict);         //Get All the Employees Details present in Database
            AdminViewModelList adminViewModel = new AdminViewModelList();                          
            adminViewModel.allEmployees = ToAdminViewModel.DataTabletoAdminEmployeeModel(EmpTable);  //Pass the Data from Datatable to List 
            return adminViewModel;                                                                    //Return the viewmodel

        }

        //Gets All the Roles present in the Database
        public Role GetRoles()
        {

            Dictionary<string, object> dict1 = new Dictionary<string, object>();
            DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles"/*, dict*/, dict1);        //Gets All the Roles present in the Database
            Role roleOptions = new Role();  
            roleOptions.RolesList = dtRole.DataTableToRolesModel(dt);                                // Pass the Data from Datatable to List
            return roleOptions;                                                                       //Return the viewmodel
        }


        //Gets All the Designation present in the Database
        public Designation GetDesignation()
        {
            Dictionary<string, object> dict1 = new Dictionary<string, object>();                    
            DataTable dtDesignation = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation"/*, dict*/, dict1);        //Gets All the Designation present in the Database
            Designation designation = new Designation();                    
            designation.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(dtDesignation);     // Pass the Data from Datatable to List
            return designation;                                                                                      //Return the viewmodel
        }


        //Gets the Last EmployeeCode present in the Database
        public string GetLastEmployeeCode()
        {
            Dictionary<string, object> dict1 = new Dictionary<string, object>();
            object empCode = dal.ExecuteScalar("uspGetLastEmployeeCode", dict1);                //Gets the Last EmployeeCode present in the Database
            string recentEmployeeCode = empCode.ToString();                                     //Convert the EmployeeCode to String
            return recentEmployeeCode;
        }


        //Saves the new employee Details in Database
        public object SaveNewEmp(Employee model, int empcode)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {
                //{ "@EmployeeId",model.EmployeeId},
                { "@EmployeeCode",empcode},
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
                { "@Role",model.RoleId},
                { "@Designation",model.DesignationId},
                { "@Experienced",model.Experienced},
                {"@PreviousCompanyName",model.PreviousCompanyName },
                { "@YearsOfExprience",model.YearsOfExprience},
            };
            object check = dal.ExecuteNonQuery("uspAddNewEmp", dict);                          //Saves the new employee Details in Database & returns 1 if SP succeed
            return check;

        }

        //Updates Employee Details of particular employee
        public object UpdateEmpDetails(Employee model)
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
                { "@Role",model.RoleId},
                { "@Designation",model.DesignationId},
                { "@Experienced",model.Experienced},
                { "@YearsOfExprience",model.YearsOfExprience},
                { "@PreviousCompanyName",model.PreviousCompanyName}
            };
            object check = dal.ExecuteNonQuery("uspUpdateEmpDetails", dict);                             //Updates the new employee Details in Database & returns 1 if SP succeed
            return check;
        }


        //Disables the employee
        public int DisableEmp(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@EmployeeId",model.EmployeeId},

            };
            int check = dal.ExecuteNonQuery("uspDisableEmployee", dict);                            //Disables the  employee login in Database & returns 1 if SP succeed
            return check;
        }

        //Enables the employee
        public int EnableEmp(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@EmployeeId",model.EmployeeId},

            };
            int check = dal.ExecuteNonQuery("uspEnableEmployee", dict);                         //Enables the  employee login in Database & returns 1 if SP succeed
            return check;
        }

        //Saves the Designation Details 
        public int SaveDesignation(Designation designation)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
                    {
                        { "@DesignationName",designation.DesignationName}
                    };
            int check = dal.ExecuteNonQuery("uspAddDesignation", dict);                        //Saves the designation Details in Database & returns 1 if SP succeed
            return check;
        }


        //Gets all the Designation 
        public Designation GetAllDesignation()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable datatable = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation", dict);                  //Gets all the Designation
            Designation designation1 = new Designation();
            designation1.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(datatable);   //Pass the data from Datatable into List of type Designation
            return designation1;
        }


        //Updates a particular designation name 
        public int UpdateDesignation(Designation model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@DesignationId",model.DesignationId},
                { "@DesignationName",model.DesignationName}
            };
            int check = dal.ExecuteNonQuery("uspUpdateDesignation", dict);                                     //Updates a particular designation name in Database & returns 1 if SP succeed

            return check;
        }


        //Deletes a particular designation name 
        public int DeleteDesignation(Designation model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@DesignationId",model.DesignationId}
            };
            int check = dal.ExecuteNonQuery("uspDeleteDesignation", dict);                                      //Deletes a particular designation name in Database & returns 1 if SP succeed
            return check;
        }


        public Role RoleView()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles", dict);
            Role role = new Role();
            role.RolesList = dtRole.DataTableToRolesModel(dt);
            return role;
        }


        //Saves a new Role name 

        public int SaveRole(Role model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@RoleName", model.RoleName}
            };
            int check = dal.ExecuteNonQuery("uspSaveRole", dict);                                               //Saves a new role name in Database & returns 1 if SP succeed
            return check;

        }

        //Deletes a Role name 
        public int DeleteRole(Role model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@RoleName", model.RoleName}
            };
            int check = dal.ExecuteNonQuery("uspDeleteRole", dict);                                                  //Deletes a Role name in Database & returns 1 if SP succeed

            return check;
        }


        //Gets List of all employees in a particular project
        public TeamEmpDetailsViewModel GetAllTeamEmpsAdmin(int emp)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@ProjectId",emp},
            };


            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspTeamEmpsAdmin", dict);                       //Gets the data table of all the employees present in a particular team
            TeamEmpDetailsViewModel tempEmpDetialsView = new TeamEmpDetailsViewModel();                 
            tempEmpDetialsView.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable);         //Pass the data to the List of type TeamEmpDetailsViewModel after converting it list from DataTable
            return tempEmpDetialsView;
        }

        //Gets all the employees without account details saved in database
        public EmployeeIdNameViewModelList AccountDetails()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpDataTable = dal.ExecuteDataSet<DataTable>("uspEmpsWithoutAC", dict);                      //Gets the data table of all the employees without account details 
            EmployeeIdNameViewModelList empName = new EmployeeIdNameViewModelList();
            empName.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpDataTable);                   //Pass the data to the List of type EmployeeIdNameViewModel after converting it list from DataTable
            return empName;
        }


        //Saves the account details of a particular employee
        public int SaveAccountDetails(AccountDetails ac)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {

                { "@EmployeeID",ac.EmployeeID},
                { "@UANNo",ac.UANNo},
                { "@BankAcNo",ac.BankAcNo},
                { "@IFSCCode",ac.IFSCCode},

            };

            int check = dal.ExecuteNonQuery("uspAddAccountD", dict);                                         //Saves the account details of a particular employee and returns 1 if successfull
            return check;
        }
        
        //Gets all the details of a specific Employee
        public AdminViewModel GetSpecificUserDetails(int emp)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId", emp}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);        //Gets the data table of all the details of a specific Employee 
            AdminViewModelList employee = new AdminViewModelList();
            employee.allEmployees = ToAdminViewModel.DataTabletoAdminEmployeeModel(EmpTable);       //Pass the data to the List of type AdminViewModelList after converting it list from DataTable
            AdminViewModel employeeowndetail = employee.allEmployees[0];                            //Pass the first element of list into object
            return employeeowndetail;
        }
        
        //Gets all the leaves requested by Employees with role as Team Lead
        public GetTeamLeaveRequestViewModel TeamLeadRequest(int EmpId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetTeamLeadLeaveRequest", dict);         //Gets the data table of all the leaves requested by Employees with role as Team Lead
            GetTeamLeaveRequestViewModel getTeamLeaveRequest = new GetTeamLeaveRequestViewModel();
            getTeamLeaveRequest.getTeamLeaveRequestViewModels = dTableToTeamLeaveRequestModel.DataTabletoLeaveRequestViewModel(EmpTable);   //Pass the data to the List of type GetTeamLeaveRequestViewModel after converting it list from DataTable
            return getTeamLeaveRequest;
        }
    }
}
