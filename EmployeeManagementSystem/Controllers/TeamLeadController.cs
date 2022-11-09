using EmployeeManagementSystem.ConversionService;
using EmployeeManagementSystem.DataAccessLayer;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EmployeeManagementSystem.Controllers.AccountsController;

namespace EmployeeManagementSystem.Controllers
{
    [NoCache]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class TeamLeadController : Controller
    {
        
        DataAccessService dal = new DataAccessService();
        private LeaveViewModel LeaveView;
        DTableToTeamEmpModel tableToTeamEmpModel = new DTableToTeamEmpModel();
        DTableToTeamLeaveRequestModel DTableToTeamLeaveRequestModel = new DTableToTeamLeaveRequestModel();
        // GET: TeamLead
        public ViewResult GetAllTeamEmps(/*TeamEmpDetailsViewModel obj*/)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@ProjectHeadEmployeeId",Session["EmpId"]},
                };


                DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspTeamEmps", dict);
                TeamEmpDetailsViewModel tempEmpDetialsView = new TeamEmpDetailsViewModel();
                tempEmpDetialsView.teamEmps = tableToTeamEmpModel.DataTabletoTeamEmployeesModel(EmpTable);
                ViewData["teamEmps"] = tempEmpDetialsView.teamEmps;
                //TeamEmps = tempEmpDetialsView;

                return View(ViewData);
            }
            catch(Exception ex)
            {
                ViewBag.GetAllTeamEmps = "GetAllTeamEmps Error";
            }
            return View(ViewData);

        }

        
        public ActionResult GetTeamLeaveRequest()
        {

            try
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>()
                    {
                        { "@ProjectHeadEmployeeId",Session["EmpId"]},
                    };


                    DataTable EmpTable = dal.ExecuteDataSet<DataTable>("uspGetTeamLeaveRequest", dict);
                    GetTeamLeaveRequestViewModel getTeamLeaveRequest = new GetTeamLeaveRequestViewModel();
                    getTeamLeaveRequest.getTeamLeaveRequestViewModels = DTableToTeamLeaveRequestModel.DataTabletoLeaveRequestViewModel(EmpTable);
                    ViewData["TeamLeaveRequest"] = getTeamLeaveRequest.getTeamLeaveRequestViewModels;
                return View(getTeamLeaveRequest);
            }
            catch(Exception ex)
            {
                ViewBag.GetTeamLeaveRequest = "Leave request Error";
            }
            //TeamEmps = tempEmpDetialsView;
            return RedirectToAction("GetTeamLeaveRequest");

        }

        public ActionResult LeaveAccept(GetTeamLeaveRequestViewModel leaveRequest)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@LeaveRequestId",leaveRequest.LeaveRequestId},
                };
                dal.ExecuteScalar("uspAcceptLeave", dict);
               return RedirectToAction("GetTeamLeaveRequest");
            }
            catch(Exception ex)
            {
                ViewBag.LeaveAccept = "Leave Accept Error ";
            }
            return RedirectToAction("LeaveAccept");


        }

        public ActionResult LeaveReject(GetTeamLeaveRequestViewModel leaveRequest)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>()
                {
                    { "@LeaveRequestId",leaveRequest.LeaveRequestId},

                };
                dal.ExecuteScalar("uspRejectLeave", dict);
                return RedirectToAction("GetTeamLeaveRequest");

            }
            catch(Exception ex)
            {
                ViewBag.LeaveReject = "Leave Reject Error";
            }

            return RedirectToAction("LeaveReject");


        }


    }
}