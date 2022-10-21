using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class Leave
    {
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public int LeavesAccrued { get; set; }
        public int PreviousBalance { get; set; }
        public int SatSunWorking { get; set; }
        public int BalanceLeaves { get; set; }

        public int TotalAvailable { get; set; }
        public int LeavesTaken { get; set; }
        public int CumilativeLeaves { get; set; }
        public int BalanceEOM { get; set; }
        public int LeaveWithoutPay { get; set; }

    }
}