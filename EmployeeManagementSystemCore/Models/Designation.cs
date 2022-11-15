using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystemCore.Models
{
    public class Designation
    {
        public int DesignationId { get; set; }

        [Required(ErrorMessage ="Designation Name required")]
        public string DesignationName { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public List<Designation> DesignationsList { get; set; }

    }
}