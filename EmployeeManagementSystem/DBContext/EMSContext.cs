using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EmployeeManagementSystemCore.Models;


namespace EmployeeManagementSystem.DBContext
{
    public class EMSContext : DbContext
    {
        public virtual  DbSet<AccountDetails> AccountDetails{ get; set; }
        public virtual DbSet<Employee> Employees{ get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectMembers> ProjectMember { get; set; }
        public virtual DbSet<Role> Roles { get; set; }







    }
}