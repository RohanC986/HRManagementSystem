namespace EmployeeManagementSystemCore.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AdminViewModel" />.
    /// </summary>
    public class AdminViewModel
    {
        /// <summary>
        /// Gets or sets the EmployeeId.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeCode.
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the MiddleName.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the LastName.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the DOB.
        /// </summary>
        public string DOB { get; set; }

        /// <summary>
        /// Gets or sets the DOJ.
        /// </summary>
        public string DOJ { get; set; }

        /// <summary>
        /// Gets or sets the BloodGroup.
        /// </summary>
        public string BloodGroup { get; set; }

        /// <summary>
        /// Gets or sets the Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the PersonalContact.
        /// </summary>
        public long PersonalContact { get; set; }

        /// <summary>
        /// Gets or sets the EmergencyContact.
        /// </summary>
        public long EmergencyContact { get; set; }

        /// <summary>
        /// Gets or sets the AadharCardNo.
        /// </summary>
        public long AadharCardNo { get; set; }

        /// <summary>
        /// Gets or sets the PancardNo.
        /// </summary>
        public string PancardNo { get; set; }

        /// <summary>
        /// Gets or sets the PassportNo.
        /// </summary>
        public string PassportNo { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the State.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the Pincode.
        /// </summary>
        public string Pincode { get; set; }

        /// <summary>
        /// Gets or sets the RoleName.
        /// </summary>
        /// 
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the DesignationName.
        /// </summary>
        /// 
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Experienced.
        /// </summary>
        public bool Experienced { get; set; }

        /// <summary>
        /// Gets or sets the PreviousCompanyName.
        /// </summary>
        public string PreviousCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the YearsOfExprience.
        /// </summary>
        public int YearsOfExprience { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the LastModified.
        /// </summary>
        public DateTime LastModified { get; set; }
    }
}
