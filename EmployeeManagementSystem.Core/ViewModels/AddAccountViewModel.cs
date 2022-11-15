using EmployeeManagementSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class AddAccountViewModel
    {
       
        public int AccountDetailsId { get; set; }
        public int EmployeeID { get; set; }
        public string UANNo { get; set; }
        public int BankAcNo { get; set; }
        public string IFSCCode { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
    
         public List<AddAccountViewModel> accountDetails { get; set; }
    }
}