namespace EmployeeManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountDetails",
                c => new
                    {
                        AccountDetailsId = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        UANNo = c.String(),
                        BankAcNo = c.Long(nullable: false),
                        IFSCCode = c.String(),
                    })
                .PrimaryKey(t => t.AccountDetailsId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeCode = c.Int(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        DOB = c.String(),
                        DOJ = c.String(),
                        BloodGroup = c.String(),
                        Gender = c.String(),
                        PersonalContact = c.Long(nullable: false),
                        EmergencyContact = c.Long(nullable: false),
                        AadharCardNo = c.Long(nullable: false),
                        PancardNo = c.String(),
                        PassportNo = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Pincode = c.String(),
                        Role = c.String(),
                        Designation = c.String(),
                        Experienced = c.Boolean(nullable: false),
                        PreviousCompanyName = c.String(),
                        YearsOfExprience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.LeaveRequests",
                c => new
                    {
                        LeaveRequestId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        IsHalfday = c.Boolean(nullable: false),
                        LeaveType = c.String(),
                        Reason = c.String(),
                        LengthOfLeave = c.Int(nullable: false),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.LeaveRequestId);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        LeavesAccrued = c.Int(nullable: false),
                        PreviousBalance = c.Int(nullable: false),
                        SatSunWorking = c.Int(nullable: false),
                        BalanceLeaves = c.Int(nullable: false),
                        TotalAvailable = c.Int(nullable: false),
                        LeavesTaken = c.Int(nullable: false),
                        CumilativeLeaves = c.Int(nullable: false),
                        BalanceEOM = c.Int(nullable: false),
                        LeaveWithoutPay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeaveId);
            
            CreateTable(
                "dbo.LeaveTypes",
                c => new
                    {
                        LeaveTypeId = c.Int(nullable: false, identity: true),
                        LeaveTypeName = c.String(),
                    })
                .PrimaryKey(t => t.LeaveTypeId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        LastLogin = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LoginId);
            
            CreateTable(
                "dbo.ProjectMembers",
                c => new
                    {
                        ProjectMembersId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectMembersId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectHeadEmployeeId = c.Int(nullable: false),
                        ProjectName = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Roles");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectMembers");
            DropTable("dbo.Logins");
            DropTable("dbo.LeaveTypes");
            DropTable("dbo.Leaves");
            DropTable("dbo.LeaveRequests");
            DropTable("dbo.Employees");
            DropTable("dbo.Designations");
            DropTable("dbo.AccountDetails");
        }
    }
}
