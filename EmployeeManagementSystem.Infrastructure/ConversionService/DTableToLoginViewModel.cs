﻿using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ConversionService
{
    public class DTableToLoginViewModel
    {
        public List<LoginViewModel> DTableToLoginViewModels(DataTable dt)
        {
            List<LoginViewModel> Login = new List<LoginViewModel>();
            Login = (from DataRow dr in dt.Rows
                                  select new LoginViewModel
                                  {
                                      EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                                      //Username = dr["Username"].ToString(),
                                      //Password = dr["Password"].ToString(),
                                      RoleId = Convert.ToInt32(dr["RoleId"]),
                                      IsActive = Convert.ToInt32(dr["IsActive"]),


                                  }

                ).ToList();
            return Login;
        }
    }
}