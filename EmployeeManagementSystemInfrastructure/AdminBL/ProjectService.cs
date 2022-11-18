using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystemInfrastructure.AdminBL
{
    public class ProjectService
    {
        private AdminViewModel EmpAllOver;
        DataAccessService dal = new DataAccessService();
        DTableToEmployeeIdNameViewModel dtEIN = new DTableToEmployeeIdNameViewModel();
        DTableToProjectModel dtP = new DTableToProjectModel();
        DTableToEmployeeModel cs = new DTableToEmployeeModel();
        DTableToDepartmentsModel dataTabletoDepartmentsModel = new DTableToDepartmentsModel();
        DTableToDesignationModel DTableToDesignationModel = new DTableToDesignationModel();
        DTableToLeaveRequestModel DTableToLeaveRequestModel = new DTableToLeaveRequestModel();
        DTableToRolesModel dtRole = new DTableToRolesModel();
        DTableToTeamEmpModel tableToTeamEmpModel = new DTableToTeamEmpModel();
        DTableToEmployeeModel dTableToEmployeeModel = new DTableToEmployeeModel();
        DTableToEmployeeIdNameViewModel dtEmpIdName = new DTableToEmployeeIdNameViewModel();
        DTableToAccountDetailsModel dtAccountDetailsModel = new DTableToAccountDetailsModel();
        EncryptDecryptConversion encryptDecryptConversion = new EncryptDecryptConversion();

        public DepartmentListViewModel Departments(string emp)
        {
            if (emp != null)
            {
                Dictionary<string, object> dict1 = new Dictionary<string, object>()
                        {
                            { "@ProjectName",emp}
                        };
                DataTable Department1 = dal.ExecuteDataSet<DataTable>("uspGetAllTeamsSearch", dict1);
                DepartmentListViewModel departmentsViewModel1 = new DepartmentListViewModel();
                departmentsViewModel1.DepartmentsViews = dataTabletoDepartmentsModel.DataTabletoDepartmentsModel(Department1);
                return departmentsViewModel1;
            }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable Department = dal.ExecuteDataSet<DataTable>("uspGetAllTeams", dict);
            DepartmentListViewModel departmentsViewModel = new DepartmentListViewModel();
            departmentsViewModel.DepartmentsViews = dataTabletoDepartmentsModel.DataTabletoDepartmentsModel(Department);
            return departmentsViewModel;
        }

        public EmployeeIdNameViewModel AddProject()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetEmpIdName", dict);
            EmployeeIdNameViewModel empIdName = new EmployeeIdNameViewModel();
            empIdName.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpIdname);
            return empIdName;
        }

        public int SaveProject(Project model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@ProjectName",model.ProjectName },
                {"@ProjectHeadEmployeeId",model.ProjectHeadEmployeeId }
            };
            int op = dal.ExecuteNonQuery("uspSaveProject", dict);
            return op;
        }
        public EmployeeIdNameViewModel GetEmpId()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetEmpIdNameAll", dict);
            EmployeeIdNameViewModel empIdnameViewModel = new EmployeeIdNameViewModel();
            empIdnameViewModel.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpIdname);

            return empIdnameViewModel;
        }
        public Project AddProjectMembers()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable ProjectsList = dal.ExecuteDataSet<DataTable>("uspGetProjects", dict);
            Project projectsList = new Project();
            projectsList.ProjectList = dtP.DataTableToProjectModel(ProjectsList);
            return projectsList;
        }

        public int SaveProjectMember(ProjectMembers model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@EmployeeId",model.EmployeeId },
                {"@ProjectId",model.ProjectId }

            };
            int op = dal.ExecuteNonQuery("uspSaveProjectMember", dict);
            return op;
        }


    }
}
