using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
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

        public int SaveLogin(Login model)
        {
            Dictionary<string, object> diction = new Dictionary<string, object>() {

                        { "@EmployeeId",model.EmployeeId},


                     };
            object roleid = dal.ExecuteScalar("uspGetEmpRole", diction);

            Dictionary<string, object> dict = new Dictionary<string, object>() {

                            { "@EmployeeId",model.EmployeeId},
                            { "@Username",model.Username},
                            { "@Password",encryptDecryptConversion.EncryptPlainTextToCipherText(  (model.Password))},
                            { "@LastLogin",(model.LastLogin)},
                            {"@RoleId",roleid }
                    };
            int check = dal.ExecuteNonQuery("uspAddNewLogin", dict);
            return check;
        }


    }
}
