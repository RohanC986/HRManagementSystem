using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.AdminBL
{
    public class Employees
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
        DTableToAdminViewModel ToAdminViewModel = new DTableToAdminViewModel();

        //public List<T> AddEmp(LoginViewModel model)
        //{
        //    Dictionary<string, object> dict1 = new Dictionary<string, object>();
        //    DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles"/*, dict*/, dict1);
        //    Role roleOptions = new Role();
        //    roleOptions.RolesList = dtRole.DataTableToRolesModel(dt);
        //    ViewData["roleOptions"] = roleOptions;

        //    DataTable dtDesignation = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation"/*, dict*/, dict1);
        //    Designation designation = new Designation();
        //    designation.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(dtDesignation);
        //    ViewData["designationOptions"] = designation;
        //}


        public object SaveEmployee(Employee model)
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
                { "@RoleId",model.RoleId},
                { "@DesignationId",model.DesignationId},
                { "@Experienced",model.Experienced},
                {"@PreviousCompanyName",model.PreviousCompanyName },
                { "@YearsOfExprience",model.YearsOfExprience},
            };
            object check = dal.ExecuteNonQuery("uspAddNewEmp", dict);
            return check;
        }

        public AdminViewModel GetAllEmployeesDetails(LoginViewModel model, string emp)
        {
            if (emp != null)
            {
                Dictionary<string, object> dict1 = new Dictionary<string, object>()
                        {
                            { "@FirstName",emp},
                        };
                DataTable EmpTable1 = dal.ExecuteDataSet<DataTable>("uspSearchEmps", dict1);
                AdminViewModel adminViewModel1 = new AdminViewModel();
                adminViewModel1.allEmployees = ToAdminViewModel.DataTabletoAdminEmployeeModel(EmpTable1);
                return adminViewModel1;
            };
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees", dict);
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.allEmployees = ToAdminViewModel.DataTabletoAdminEmployeeModel(EmpTable);
            return adminViewModel;

        }

        public Role GetRoles()
        {

            Dictionary<string, object> dict1 = new Dictionary<string, object>();
            DataTable dt = dal.ExecuteDataSet<DataTable>("uspGetAllRoles"/*, dict*/, dict1);
            Role roleOptions = new Role();
            roleOptions.RolesList = dtRole.DataTableToRolesModel(dt);
            return roleOptions;
        }

        public Designation GetDesignation()
        {
            Dictionary<string, object> dict1 = new Dictionary<string, object>();
            DataTable dtDesignation = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation"/*, dict*/, dict1);
            Designation designation = new Designation();
            designation.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(dtDesignation);
            return designation;
        }

        public object SaveNewEmp(Employee model)
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
                { "@Role",model.RoleId},
                { "@Designation",model.DesignationId},
                { "@Experienced",model.Experienced},
                {"@PreviousCompanyName",model.PreviousCompanyName },
                { "@YearsOfExprience",model.YearsOfExprience},
            };
            object check = dal.ExecuteNonQuery("uspAddNewEmp", dict);
            return check;

        }

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
            object check = dal.ExecuteNonQuery("uspUpdateEmpDetails", dict);
            return check;
        }
        public int DisableEmp(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@EmployeeId",model.EmployeeId},

            };
            int check = dal.ExecuteNonQuery("uspDisableEmployee", dict);
            return check;
        }

        public int EnableEmp(Employee model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@EmployeeId",model.EmployeeId},

            };
            int check = dal.ExecuteNonQuery("uspEnableEmployee", dict);
            return check;
        }

        public int SaveDesignation(Designation designation)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
                    {
                        { "@DesignationName",designation.DesignationName}
                    };
            int check = dal.ExecuteNonQuery("uspAddDesignation", dict);
            return check;
        }

        public Designation GetAllDesignation()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable datatable = dal.ExecuteDataSet<DataTable>("uspGetAllDesignation", dict);
            Designation designation1 = new Designation();
            designation1.DesignationsList = DTableToDesignationModel.DataTabletoDesignationsModel(datatable);
            return designation1;
        }

        public int UpdateDesignation(Designation model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@DesignationId",model.DesignationId},
                { "@DesignationName",model.DesignationName}
            };
            int check = dal.ExecuteNonQuery("uspUpdateDesignation", dict);
            return check;
        }

        public int DeleteDesignation(Designation model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>() {

                { "@DesignationId",model.DesignationId}
            };
            int check = dal.ExecuteNonQuery("uspDeleteDesignation", dict);
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

        public int SaveRole(Role model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@RoleName", model.RoleName}
            };
            int check = dal.ExecuteNonQuery("uspSaveRole", dict);
            return check;

        }
        public int DeleteRole(Role model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@RoleName", model.RoleName}
            };
            int check = dal.ExecuteNonQuery("uspDeleteRole", dict);
            return check;
        }

        public TeamEmpDetailsViewModel GetAllTeamEmpsAdmin(Project emp)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@ProjectName",emp.ProjectName},
            };


            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspTeamEmpsAdmin", dict);
            TeamEmpDetailsViewModel tempEmpDetialsView = new TeamEmpDetailsViewModel();
            tempEmpDetialsView.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable);
            return tempEmpDetialsView;
        }

        public EmployeeIdNameViewModel AccountDetails(Project emp)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpDt = dal.ExecuteDataSet<DataTable>("uspEmpsWithoutAC", dict);
            EmployeeIdNameViewModel empname = new EmployeeIdNameViewModel();
            empname.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpDt);
            return empname;
        }

        public int SaveAccountDetails(AccountDetails ac)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {

                { "@EmployeeID",ac.EmployeeID},
                { "@UANNo",ac.UANNo},
                { "@BankAcNo",ac.BankAcNo},
                { "@IFSCCode",ac.IFSCCode},

            };

            int check =  dal.ExecuteNonQuery("uspAddAccountD", dict);
            return check;
        }

        public AdminViewModel GetSpecificUserDetails(int emp)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@EmployeeId", emp}
            };
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetAllEmpDetails", dict);
            AdminViewModel employee = new AdminViewModel();
            employee.allEmployees = ToAdminViewModel.DataTabletoAdminEmployeeModel(EmpTable);
            return employee;   
        }
    }
}
