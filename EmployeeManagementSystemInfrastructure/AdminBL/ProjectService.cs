using EmployeeManagementSystemCore.DataAccessLayer;
using EmployeeManagementSystemCore.Models;
using EmployeeManagementSystemCore.ViewModels;
using EmployeeManagementSystemInfrastructure.ConversionService;
using System.Collections.Generic;
using System.Data;

namespace EmployeeManagementSystemInfrastructure.AdminBL
{
    public class ProjectService
    {
        DataAccessService dal = new DataAccessService();
        DTableToEmployeeIdNameViewModel dtEIN = new DTableToEmployeeIdNameViewModel();
        DTableToProjectModel dtP = new DTableToProjectModel();
        DTableToDepartmentsModel dataTabletoDepartmentsModel = new DTableToDepartmentsModel();


        //Gets all the Projects along with its Team Lead and Total Members Count
        public DepartmentListViewModel Departments(string searchProject)
        {

            //Procceed if Search String is not null
            if (searchProject != null)
            {
                Dictionary<string, object> dict1 = new Dictionary<string, object>()
                        {
                            { "@ProjectName",searchProject}
                        };
                DataTable Department1 = dal.ExecuteDataSet<DataTable>("uspGetAllTeamsSearch", dict1);                               //Gets the data of the Searched Project
                DepartmentListViewModel departmentsViewModel1 = new DepartmentListViewModel();
                departmentsViewModel1.DepartmentsViews = dataTabletoDepartmentsModel.DataTabletoDepartmentsModel(Department1);      //Pass the Data from Datatable to the list of type DepartmentListViewModel
                return departmentsViewModel1;
            }
            //Procced  if Search String is null
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable Department = dal.ExecuteDataSet<DataTable>("uspGetAllTeams", dict);                                           //Gets the data of all the Project
            DepartmentListViewModel departmentsViewModel = new DepartmentListViewModel();
            departmentsViewModel.DepartmentsViews = dataTabletoDepartmentsModel.DataTabletoDepartmentsModel(Department);            //Pass the Data from Datatable to the list of type DepartmentListViewModel
            return departmentsViewModel;
        }



        //Gets all the Employees 
        public EmployeeIdNameViewModelList GetAllEmployees()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetEmpIdName", dict);                     //Gets all the employees from the Database
            EmployeeIdNameViewModelList empIdName = new EmployeeIdNameViewModelList();
            empIdName.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpIdname);               //Pass the Data from Datatable to the list of type EmployeeIdNameViewModelList
            return empIdName;
        }

        //Saves the Project 
        public int SaveProject(Project model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@ProjectName",model.ProjectName },
                {"@ProjectHeadEmployeeId",model.ProjectHeadEmployeeId }
            };
            int check = dal.ExecuteNonQuery("uspSaveProject", dict);                                        //Saves the new Project and returns 1 if succeeded                   
            return check;
        }


        //Gets All the EmployeeId
        public EmployeeIdNameViewModelList GetEmpId()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetEmpIdNameAll", dict);            //Gets all the EmployeeId from the Database
            EmployeeIdNameViewModelList empIdnameViewModel = new EmployeeIdNameViewModelList();
            empIdnameViewModel.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpIdname);    //Pass the Data from Datatable to the list of type EmployeeIdNameViewModelList

            return empIdnameViewModel;
        }

        //Gets all the Project from Database
        public Project GetProject(int Projects)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@ProjectId",Projects }            };
            DataTable ProjectsList = dal.ExecuteDataSet<DataTable>("uspGetProjects", dict);            //Gets all the Projects from the Database
            Project projectsList = new Project();
            projectsList.ProjectList = dtP.DataTableToProjectModel(ProjectsList);                       //Pass the Data from Datatable to the list of type Project
            return projectsList;
        }


        //Assigns the Project Members to particular project 
        public int SaveProjectMember(ProjectMembers model)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@EmployeeId",model.EmployeeId },
                {"@ProjectId",model.ProjectId }

            };
            int check = dal.ExecuteNonQuery("uspSaveProjectMember", dict);                         //Assigns the Project Members to particular project and returns 1 if succeeded
            return check;
        }


        //Gets the Projects who are not assigned to any Team Lead
        public Project GetProjectsWithoutTeamLead()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable ProjectsList = dal.ExecuteDataSet<DataTable>("uspGetProjectWithoutTeamLead", dict);       //Gets the Projects who are not assigned to any Team Lead
            Project projectsList = new Project();
            projectsList.ProjectList = dtP.DataTableToProjectModel(ProjectsList);                               //Pass the Data from Datatable to the list of type Project
            return projectsList;

        }

        //Assigns the TeamLead to particular project 
        public int AssignTeamLead(Project project)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                  {"@ProjectId",project.ProjectId },
                {"@ProjectHeadEmployeeId",project.ProjectHeadEmployeeId }
            };
            int check = dal.ExecuteNonQuery("uspAssignTeamLead", dict);                                         //Assigns the TeamLead to particular project
            return check;


        }


        //Changes the Team Lead of a particular project
        public int ChangeTeamLead(Project project)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@ProjectId",project.ProjectId },
                {"@ProjectHeadEmployeeId",project.ProjectHeadEmployeeId }
            };
            int check = dal.ExecuteNonQuery("uspUpdateTeamLead", dict);                                     //Changes the Team Lead of a particular project and returns 1 if succeeded
            return check;
        }


        //Gets all the Projects from Database
        public Project GetProjects()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            DataTable ProjectsList = dal.ExecuteDataSet<DataTable>("uspGetAllProjects", dict);               //Gets all the Projects from Database
            Project projectsList = new Project();
            projectsList.ProjectList = dtP.DataTableToProjectModel(ProjectsList);                            //Pass the Data from Datatable to the list of type Project
            return projectsList;    

        }


        //Gets all the Employee with role as Team Lead
        public EmployeeIdNameViewModelList GetTeamLead()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            DataTable EmpIdname = dal.ExecuteDataSet<DataTable>("uspGetAllTeamLeads", dict);              //Gets all the Employee with role as Team Lead
            EmployeeIdNameViewModelList empIdName = new EmployeeIdNameViewModelList();
            empIdName.EmployeeIdNameList = dtEIN.DataTableToEmployeeIdNameViewModel(EmpIdname);         //Pass the Data from Datatable to the list of type EmployeeIdNameViewModelList
            return empIdName;   
        }


        //Removes the Employee from the Team
        public int RemoveEmp(int EmpId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                {"@EmployeeId",EmpId}
            };
            int check = dal.ExecuteNonQuery("uspRemoveEmployee", dict);                                //Removes the Employee from the Team and returns 1 if succeeded
            return check;
        }

    }
}
