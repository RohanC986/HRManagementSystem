using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.ViewModels
{
    public class AddAccountViewModel
    {
       
        public int AccountDetailsId { get; set; }
        public int EmployeeID { get; set; }
        public string UANNo { get; set; }
        public string BankAcNo { get; set; }
        public string IFSCCode { get; set; }
        public List<AddAccountViewModel> accountDetails { get; set; }
    }
}