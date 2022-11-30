using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystemCore.ViewModels
{
    public class ProjectMembersViewModel
    {
        public int ProjectMembersId { get; set; }
        [Required]

        public string ProjectName { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
    }
}
