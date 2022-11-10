using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }


        public long PersonalContact { get; set; }
        public long EmergencyContact { get; set; }
        public long AadharCardNo { get; set; }
        public string PancardNo { get; set; }
        public string PassportNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Role { get; set; }
        public string Designation { get; set; }
        public bool Experienced { get; set; }
        public string PreviousCompanyName { get; set; }
        public int YearsOfExprience { get; set; }

        public  bool? IsActive { get; set; }
        public List<Employee> EmployeeList { get; set; }

    }
}