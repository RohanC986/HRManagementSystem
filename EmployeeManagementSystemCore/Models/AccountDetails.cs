using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class AccountDetails
    {
        public int AccountDetailsId { get; set; }


        [Required(ErrorMessage = "Employee is required")]
        public int EmployeeID { get; set; }


        [Required(ErrorMessage = "UAN is required")]
        [StringLength(12)]
        public string UANNo { get; set; }


        [Required(ErrorMessage = "Bank Account Number is required")]
        [StringLength(9-18)]
        public string BankAcNo { get; set; }



        [RegularExpression("^[A-Z]{4}0[0-9]{5}$", ErrorMessage = "UAN id is not valid")]
        [Required(ErrorMessage = "IFSC Code is required")]
        public string IFSCCode { get; set; }

        public string Created { get; set; }
        public string LastModified { get; set; }
        public List<AccountDetails> accountDetails { get; set; }
    }
}