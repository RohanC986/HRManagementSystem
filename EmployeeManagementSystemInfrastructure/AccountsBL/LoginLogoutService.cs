using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;

namespace EmployeeManagementSystemInfrastructure.AccountsBL
{
    public class LoginLogoutService
    {
        //Creating objects of various required classes

        DataAccessService dal = new DataAccessService();
        DTableToLoginViewModel dTable = new DTableToLoginViewModel();
        EncryptDecryptConversion encryptDecryptConversion = new EncryptDecryptConversion();
        LoginViewModel LoginViewModel = new LoginViewModel();


        //Checks if the user is valid or not.If validate returns RoleId and EmployeeId,else returns error message &
        //keeps a check of attempts if user is present in DataBase and enters wrong password
        public LoginViewModel Login(LoginViewModel model)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>() {                     //Passing User input into Dictionary
                { "@Username",model.Username},
                { "@Password",model.Password}
                };
                Dictionary<string, object> dict1 = new Dictionary<string, object>() {
                { "@Username",model.Username}

                };
                object outputUserEmployeeId = dal.ExecuteScalar("uspGetUserEmployeeId", dict1);                   // Gets EmployeeId of matching UserName
                var EncryptedPassoword = encryptDecryptConversion.EncryptPlainTextToCipherText(model.Password);    //Encrypts Password entered by user 

                Dictionary<string, object> dict2 = new Dictionary<string, object>() {
                { "@Password",EncryptedPassoword}

                };
                Dictionary<string, object> DictRoleId = new Dictionary<string, object>() {
                { "@EmployeeId",outputUserEmployeeId}

                };
                DataTable dataTableRoleId = dal.ExecuteDataSet<DataTable>("uspGetRole", DictRoleId);             //Gets the Role of the User
                AdminViewModel adminViewModel = new AdminViewModel();                                //Creating object of AdminViewModel
                LoginViewModel.loginViewModels = dTable.DTableToLoginViewModels(dataTableRoleId);           //Convert Datatable of UserDetails into LoginViewModel List
                int RoleId = LoginViewModel.loginViewModels[0].RoleId;                                 //Pass the RoleId from the List to model
                object outputPassEmployeeId = dal.ExecuteScalar("uspGetPassEmployeeId", dict2);                             //Gets EmployeeId of matching Password
                 
                //Procced if both EmployeeId of matching UserName and Passoword is present in Database
                if(outputPassEmployeeId != null && outputUserEmployeeId!=null)
                {
                    //Procced if EmployeeId of matching UserName and Passoword are equal 
                    if (Convert.ToInt32(outputUserEmployeeId) == Convert.ToInt32(outputPassEmployeeId))
                    {
                        Dictionary<string, object> dict3 = new Dictionary<string, object>()
                    {
                            { "EmployeeId",outputUserEmployeeId }
                    };
                        DataTable EmpTableUser = dal.ExecuteDataSet<DataTable>("uspGetRole", dict3);             //Gets the Role of the User
                        AdminViewModel adminViewModelUser = new AdminViewModel();                                //Creating object of AdminViewModel
                        LoginViewModel.loginViewModels = dTable.DTableToLoginViewModels(EmpTableUser);           //Convert Datatable of UserDetails into LoginViewModel List
                        model.RoleId = LoginViewModel.loginViewModels[0].RoleId;                                 //Pass the RoleId from the List to model
                        model.EmployeeId = LoginViewModel.loginViewModels[0].EmployeeId;                         //Pass the EmployeeId from the List to model
                        model.IsActive = LoginViewModel.loginViewModels[0].IsActive;                             //Pass the IsActive from the List to model
                        dal.ExecuteNonQuery("uspResetAttempts", dict3);                                          //Set the attempts count to 0 beacuse of sucessfull login
                        LoginViewModel.EmployeeDict = dict3;
                        return model;                                                                            //Return Model
                    }

                }
                //Proceed if only EmployeeId of matching UserName is present i.e user has provided incorrect password
                else 
                {
                    //procced if User is not an Admin 
                    if (RoleId != 1) 
                    {
                        if (outputUserEmployeeId != null && outputPassEmployeeId == null)
                        {
                            Dictionary<string, object> dict6 = new Dictionary<string, object>()
                        {
                            {"@EmployeeId",outputUserEmployeeId}

                        };
                            object attempts = dal.ExecuteScalar("uspGetLoginAttempts", dict6);    //Get the Login attempts of User
                            int attempts2 = Convert.ToInt32(attempts);

                            //Increment the Login attempt to 1 if it is 0
                            if (attempts == null)
                            {
                                Dictionary<string, object> dict7 = new Dictionary<string, object>()
                                {
                                    {"@EmployeeId",outputUserEmployeeId},
                                    {"@Attempts",1 }

                                };
                                dal.ExecuteNonQuery("uspAddAttempts", dict7);
                                model.PasswordMessage = "Invalid Password";                     //Pass login error message into model
                                return model;                                                   //return Model
                            }

                            //Procced if Login attempt is less than 4
                            else if (attempts2 < 4)
                            {
                                dal.ExecuteNonQuery("uspIncreaseAttempts", dict6);           //Increment the Login Attempts count
                                model.PasswordMessage = "Invalid Password";                  //Pass login error message into model
                                return model;                                                //return Model
                            }

                            //Procced if Login attempt is equal to 4
                            else if (attempts2 == 4)
                            {
                                dal.ExecuteNonQuery("uspIncreaseAttempts", dict6);          //Increment the Login Attempts count
                                model.PasswordMessage = "Invalid Password";                 //Pass login error message into model
                                return model;                                               //return Model
                            }

                            //Procced if Login attempt is equal to 5
                            else if (attempts2 == 5)
                            {
                                dal.ExecuteNonQuery("uspDisableEmployee", dict6);          //Block the User
                                model.PasswordMessage = "User blocked due to exceeded limit of attempts with wrong Password";  //Pass login error message into model
                                return model;                                               //return Model
                            }
                        }
                    }
                   //Procced if Only EmployeeId of password is present i.e User has entered wrong Username
                    else if (outputUserEmployeeId == null && outputPassEmployeeId != null)
                    {
                        model.UsernameMessage = "Invalid Username";                       //Pass login error message into model
                        return model;                                                     //return Model
                    }
                    //Procced if both EmployeeId of password & UserName is not present i.e User has entered wrong Username & Passoword
                    else if (outputPassEmployeeId == null && outputUserEmployeeId == null)
                    {
                        model.UsernameMessage = "Invalid Credentials";                      //Pass login error message into model
                        return model;                                                        //return Model
                    }
                }
            }
            //Catch Any Exception if occurred
            catch (Exception e)
            {
                model.UsernameMessage = "User Not Found";

                return model;
            }
            return model;
        }
    }
    }

