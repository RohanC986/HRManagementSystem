using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
namespace EmployeeManagementSystemInfrastructure.ConversionService
{
    public class AdminViewModelToEmployeeModel
    {
        //Returns  object of Class Employee after Converting data AdminToEmployee

        public Employee AdminToEmployee(AdminViewModel dr)
        {
            Employee employees = new Employee();
            employees =
                new Employee
                {

                    EmployeeId = Convert.ToInt32(dr.EmployeeId),
                    EmployeeCode = Convert.ToString(dr.EmployeeCode),
                    FirstName = Convert.ToString(dr.FirstName),
                    MiddleName = dr.MiddleName,
                    LastName = Convert.ToString(dr.LastName),
                    Email = Convert.ToString(dr.Email),
                    //DOB = Convert.ToDateTime(dr["DOB"]),
                    DOB = DateTime.ParseExact(dr.DOB,"dd/MM/yyyy", new CultureInfo("en-CA")),
                    DOJ = DateTime.ParseExact(dr.DOJ, "dd/MM/yyyy", new CultureInfo("en-CA")),
                    BloodGroup = Convert.ToString(dr.BloodGroup),
                    Gender = Convert.ToString(dr.Gender),
                    PersonalContact = Convert.ToInt64(dr.PersonalContact),
                    EmergencyContact = Convert.ToInt64(dr.EmergencyContact),
                    AadharCardNo = Convert.ToInt64(dr.AadharCardNo),
                    PancardNo = Convert.ToString(dr.PancardNo),
                    PassportNo = Convert.ToString(dr.PassportNo),
                    Address = Convert.ToString(dr.Address),
                    City = Convert.ToString(dr.City),
                    State = Convert.ToString(dr.State),
                    Pincode = Convert.ToString(dr.Pincode),
                    RoleId = Convert.ToInt16(dr.RoleId),
                    DesignationId = Convert.ToInt16(dr.DesignationId),
                    Experienced = Convert.ToBoolean(dr.Experienced),
                    PreviousCompanyName = Convert.ToString(dr.PreviousCompanyName),
                    YearsOfExprience = Convert.ToInt32(dr.YearsOfExprience),
                    IsActive = Convert.ToBoolean(dr.IsActive),
                    Created = DateTime.Parse(dr.Created.ToString()),
                    LastModified = DateTime.Parse(dr.LastModified.ToString())
                };
            


            return employees;
        }
    }
}