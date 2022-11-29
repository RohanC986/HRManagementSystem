using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Code required")]
        [RegularExpression("[0-9]{1,3}",ErrorMessage ="Invalid Employee Code")]
        public string EmployeeCode { get; set; }



        [Required(ErrorMessage = "First Name is required")]
        [StringLength(20, MinimumLength =2)]
        [RegularExpression("[A-Za-z]{2,20}", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [RegularExpression("[A-Za-z]{2,20}", ErrorMessage = "Invalid First Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(20, MinimumLength = 2)]
        [RegularExpression("[A-Za-z]{2,20}", ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; }




        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "E-mail id is not valid")]
        public string Email { get; set; }




        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DOB { get; set; }




        [Required(ErrorMessage = "Date of Joining is required")]
        public DateTime DOJ { get; set; }




        [Required(ErrorMessage = "Blood Group is required")]
        public string BloodGroup { get; set; }




        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }




        [Required(ErrorMessage = "Personal Contact is required")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Invalid phone number")]
        public long PersonalContact { get; set; }




        [Required(ErrorMessage = "Emergency Contact is required")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Invalid phone number")]
        public long EmergencyContact { get; set; }




        [Required(ErrorMessage = "Aadhar Card Number is required")]
        [RegularExpression("[0-9]{12}", ErrorMessage = "Invalid Aadhar number")]
        public long AadharCardNo { get; set; }




        [Required(ErrorMessage = "Pancard Number is required")]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Pancard Number is not valid")]
        public string PancardNo { get; set; }




        [StringLength(12, MinimumLength = 12)]
        [RegularExpression("[A-Z]{4}([0-9]{8})", ErrorMessage = "Pancard Number is not valid")]
        public string PassportNo { get; set; }




        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }




        [Required(ErrorMessage = "City is required")]
        [StringLength(20, MinimumLength = 3)]
        public string City { get; set; }




        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }




        [Required(ErrorMessage = "Pincode is required")]
        [StringLength(6, MinimumLength = 6)]
        public string Pincode { get; set; }




        [Required(ErrorMessage = "Role is required")]
        public int RoleId { get; set; }




        [Required(ErrorMessage = "Designation is required")]
        public int DesignationId { get; set; }




        [Required(ErrorMessage = "Experienced Status is required")]
        public bool Experienced { get; set; }



        [StringLength (50, MinimumLength = 2)] 
        public string PreviousCompanyName { get; set; }




        [Range(0, 38, ErrorMessage = "Experience should be between 0 and 38")]
        public int? YearsOfExprience { get; set; }




        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public List<Employee> EmployeeList { get; set; }

    }
}