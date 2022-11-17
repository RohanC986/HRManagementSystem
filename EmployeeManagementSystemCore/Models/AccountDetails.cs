﻿using System;
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
        [RegularExpression("[0-9{12-20}]")]
        public string UANNo { get; set; }


        [Required(ErrorMessage = "Bank Account Number is required")]
        [RegularExpression("[0-9{12-20}]")]
        public string BankAcNo { get; set; }



        [RegularExpression("^[A-Z]{4}0[0-9]{7}$", ErrorMessage = "UAN id is not valid")]
        [Required(ErrorMessage = "IFSC Code is required")]
        public DateTime IFSCCode { get; set; }

        public DateTime Created { get; set; }
        public string LastModified { get; set; }
        public List<AccountDetails> accountDetails { get; set; }
    }
}