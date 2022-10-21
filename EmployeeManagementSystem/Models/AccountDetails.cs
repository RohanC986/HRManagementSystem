using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class AccountDetails
    {
        public int AccountDetailsId { get; set; }
        public int EmployeeID { get; set; }
        public string UANNo { get; set; }
        public long BankAcNo { get; set; }
        public string IFSCCode { get; set; }
    }
}