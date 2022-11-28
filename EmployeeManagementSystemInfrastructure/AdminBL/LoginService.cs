using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystemInfrastructure.AdminBL
{
    public class LoginService
    {
        DataAccessService dal = new DataAccessService();
        DTableToEmployeeModel cs = new DTableToEmployeeModel();
        EncryptDecryptConversion encryptDecryptConversion = new EncryptDecryptConversion();
        public Employee AddLogin()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpDt = dal.ExecuteDataSet<DataTable>("uspgetLoginEmployees", dict);

            Employee EmpDR = new Employee();

            EmpDR.EmployeeList = cs.DataTabletoEmployeeModel(EmpDt);
            return EmpDR;

        }

        public int SaveLogin(AddNewEmployeeViewModel model)
        {
            var l = encryptDecryptConversion.EncryptPlainTextToCipherText(model.Password);
            Dictionary<string, object> diction = new Dictionary<string, object>() {

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
                { "@UANNo",model.UANNo},
                { "@BankAcNo",model.BankAcNo},
                { "@IFSCCode",model.IFSCCode},
                { "@Username",model.Username},
                { "@Password",encryptDecryptConversion.EncryptPlainTextToCipherText(model.Password)},
                { "@LastLogin",model.LastLogin},
                
            };
            
            int check = dal.ExecuteNonQuery("uspAddNewEmp", diction);
            return check;
        }


    }
}
