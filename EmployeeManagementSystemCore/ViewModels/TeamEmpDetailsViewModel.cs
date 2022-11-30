using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class TeamEmpDetailsViewModel
    {
        public int EmployeeId { get; set; }

       
        public string EmployeeCode { get; set; }



       
        public string FirstName { get; set; }


        public string MiddleName { get; set; }

       
        public string LastName { get; set; }




      
        public string Email { get; set; }




       
        public DateTime DOB { get; set; }




        
        public DateTime DOJ { get; set; }




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

        public int RoleId { get; set; }


       
        public string RoleName { get; set; }




      
        public string DesignationName { get; set; }




       
        public bool Experienced { get; set; }




        public string PreviousCompanyName { get; set; }





        public int YearsOfExprience { get; set; }




        public bool? IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        
        public List<TeamEmpDetailsViewModel> teamEmps { get; set; }


    }
}