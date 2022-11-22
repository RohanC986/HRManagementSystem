using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeManagementSystemInfrastructure.AccountsBL
{
    public class LoginLogout
    {
        private readonly string constr;
        DataAccessService dal = new DataAccessService();
        DTableToEmployeeModel cs = new DTableToEmployeeModel();
        DTableToLoginViewModel dTable = new DTableToLoginViewModel();
        EncryptDecryptConversion encryptDecryptConversion = new EncryptDecryptConversion();
        LoginViewModel LoginViewModel = new LoginViewModel();



        public LoginViewModel Login(LoginViewModel model)
        {
            try
            {

                Dictionary<string, object> dict = new Dictionary<string, object>() {
                { "@Username",model.Username},
                { "@Password",model.Password}
                };

                Dictionary<string, object> dict1 = new Dictionary<string, object>() {
                { "@Username",model.Username}

                };
                object outputUser = dal.ExecuteScalar("uspGetUserEmployeeId", dict1);
                var l = encryptDecryptConversion.EncryptPlainTextToCipherText(model.Password);

                Dictionary<string, object> dict2 = new Dictionary<string, object>() {
                { "@Password",l}

                };
                
                object outputPass = dal.ExecuteScalar("uspGetPassEmployeeId", dict2);

                if(outputPass != null && outputUser!=null)
                {
                    if (Convert.ToInt32(outputUser) == Convert.ToInt32(outputPass))
                    {
                        Dictionary<string, object> dict3 = new Dictionary<string, object>()
                    {

                            { "EmployeeId",outputUser }
                    };
                        DataTable EmpTableUser = dal.ExecuteDataSet<DataTable>("uspGetRole", dict3);
                        AdminViewModel adminViewModelUser = new AdminViewModel();
                        LoginViewModel.loginViewModels = dTable.DTableToLoginViewModels(EmpTableUser);
                        model.RoleId = LoginViewModel.loginViewModels[0].RoleId;
                        model.EmployeeId = LoginViewModel.loginViewModels[0].EmployeeId;
                        model.IsActive = LoginViewModel.loginViewModels[0].IsActive;
                        dal.ExecuteNonQuery("uspResetAttempts", dict3);
                        //this.AddNotification("Logged In Successfully", NotificationType.SUCCESS);
                        LoginViewModel.EmployeeDict = dict3;

                        return model;




                    }

                }
                //if (Convert.ToInt32(outputUser) == Convert.ToInt32(outputPass))
                //{
                //    Dictionary<string, object> dict3 = new Dictionary<string, object>()
                //    {

                //            { "EmployeeId",outputUser }
                //    };
                //    DataTable EmpTableUser = dal.ExecuteDataSet<DataTable>("uspGetRole", dict3);
                //    AdminViewModel adminViewModelUser = new AdminViewModel();
                //    LoginViewModel.loginViewModels = dTable.DTableToLoginViewModels(EmpTableUser);
                //    model.RoleId = LoginViewModel.loginViewModels[0].RoleId;
                //    model.EmployeeId = LoginViewModel.loginViewModels[0].EmployeeId;
                //    model.IsActive = LoginViewModel.loginViewModels[0].IsActive;
                //    dal.ExecuteNonQuery("uspResetAttempts", dict3);
                //    //this.AddNotification("Logged In Successfully", NotificationType.SUCCESS);
                //    LoginViewModel.EmployeeDict = dict3;
                  
                //    return model;




                //}
                else 
                {
                    if(Convert.ToInt32(outputUser) !=0) 
                    {
                        if (outputUser != null && outputPass == null)
                        {
                            Dictionary<string, object> dict6 = new Dictionary<string, object>()
                        {
                            {"@EmployeeId",outputUser}

                        };
                            object attempts = dal.ExecuteScalar("uspGetLoginAttempts", dict6);
                            int attempts2 = Convert.ToInt32(attempts);


                            if (attempts == null)
                            {
                                Dictionary<string, object> dict7 = new Dictionary<string, object>()
                                {
                                    {"@EmployeeId",outputUser},
                                    {"@Attempts",1 }

                                };
                                dal.ExecuteNonQuery("uspAddAttempts", dict7);
                                model.PasswordMessage = "Invalid Password";
                                //ViewBag.LoginError = "Invalid Password";
                                //this.AddNotification("Invalid Password", NotificationType.ERROR);
                                return model;
                            }
                            else if (attempts2 < 4)
                            {



                                dal.ExecuteNonQuery("uspIncreaseAttempts", dict6);
                                model.PasswordMessage = $"Invalid Password({5 - attempts2} remaining out of 5)";
                                //ViewBag.LoginError = $"Invalid Password({5 - attempts2} remaining out of 5)";
                                //this.AddNotification($"Invalid Password({5 - attempts2} remaining out of 5)", NotificationType.ERROR);
                                return model;



                            }
                            else if (attempts2 == 4)
                            {




                                dal.ExecuteNonQuery("uspIncreaseAttempts", dict6);
                                model.PasswordMessage = "Last Attempt Remaining";
                                //ViewBag.LoginError = "Last Attempt Remaining";
                                //this.AddNotification("Last Attempt Remaining", NotificationType.ERROR);
                                return model;
                            }
                            else if (attempts2 == 5)
                            {

                                dal.ExecuteNonQuery("uspDisableEmployee", dict6);
                                model.PasswordMessage = "User blocked due to exceeded limit of attempts with wrong Password";
                                //ViewBag.LoginError = "User blocked due to exceeded limit of attempts with wrong Password";

                                //this.AddNotification("User blocked due to exceeded limit of attempts with wrong Password", NotificationType.ERROR);
                                return model;
                            }

                        }
                    }
                   
                    else if (outputUser == null && outputPass != null)
                    {
                        model.UsernameMessage = "Invalid Username";
                        //ViewBag.LoginError = "Invalid Username";
                        return model;
                    }
                    else if (outputPass == null && outputUser == null)
                    {
                        model.UsernameMessage = "Invalid Credentials";
                        //this.AddNotification("Invalid Credentials", NotificationType.ERROR);
                        //ViewBag.LoginError = "Invalid Credentials";
                        return model;
                    }

                }

            }
            catch (Exception e)
            {
                model.UsernameMessage = "User Not Found";

                return model;
            }
            return model;
            //return RedirectToAction("login");

        }
    }
    }

