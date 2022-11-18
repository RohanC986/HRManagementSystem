using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            if(emp != null)
            {
                Dictionary<string, object> dict1 = new Dictionary<string, object>()
                        {
                            { "@FirstName",emp},
                        };
                DataTable EmpTable1 = dal.ExecuteDataSet<DataTable>("uspSearchEmps", dict1);
                AdminViewModel adminViewModel1 = new AdminViewModel();
                adminViewModel1.allEmployees = cs.DataTabletoEmployeeModel(EmpTable1);
                return adminViewModel1;
            };
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspgetAllEmployees", dict);
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.allEmployees = cs.DataTabletoEmployeeModel(EmpTable);
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
       


    }
}
