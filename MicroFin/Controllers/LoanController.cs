using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroFin.Models;
using MicroFin.DAO;

namespace MicroFin.Controllers
{
    [RoutePrefix("Loan")]
    [Authorize]
    public class LoanController : Controller
    {
        [HttpGet]
        [Route("LoanForm/{id?}")]
        public ActionResult LoanForm(string id)
        {
            int loanId = Convert.ToInt32(id);
            if (loanId == 0)
            {
                @ViewBag.DisplayMode = "display:none";
                return View();
            }
            else
            {
                Loan loan = LoanDBService.GetLoan(loanId);
                return View(loan);
            }
        }

        [HttpGet]
        public ActionResult GroupLoanForm()
        {
            return View("GroupLoanForm");
        }

        [HttpPost]
        public ActionResult GroupLoan(GroupLoan groupLoan)
        {
            groupLoan.BranchId = Convert.ToInt32(Session["BranchId"]);
            int status = LoanDBService.AddGroupLoan(groupLoan);
            if (status == 1)
            {
                return ViewLoans(groupLoan.GroupId.ToString());
            }
            else
            {
                return View("GroupLoanForm");
            }
        }

        [HttpPost]
        public ActionResult Loan(Loan loan)
        {
            if (loan.LoanId == 0)
            {
                loan.BranchId = Convert.ToInt32(Session["BranchId"]);
                int status = LoanDBService.AddLoan(loan);
                if (status == 0)
                {
                    @ViewBag.ErrEwi = "Loan already existes";
                    @ViewBag.DisplayMode = "display:block";
                    return View("LoanForm", loan);
                }
                else if (status == -1)
                {
                    @ViewBag.ErrEwi = "Try again";
                    @ViewBag.DisplayMode = "display:block";
                    return View("LoanForm", loan);
                }
                else
                {
                    return ViewLoans(MemberDBService.GetGroupId(loan.MemberId).ToString());
                }
            }
            else
            {
                int statusCode = LoanDBService.EditLoan(loan);
                if (statusCode == 0)
                {
                    @ViewBag.ErrTryAgain = "Try Again";
                    return View("LoanForm", loan);
                }
                else
                {
                    return ViewLoan(loan.LoanId.ToString());
                }
            }
        }

        [HttpGet]
        [Route("ViewLoans/{id?}")]
        public ActionResult ViewLoans(string id)
        {
            int groupId = Convert.ToInt32(id);
            List<Loan> loans = LoanDBService.GetAllLoans(Convert.ToInt32(Session["BranchId"]), groupId);
            return View("ViewLoans", loans);
        }

        [HttpGet]
        [Route("LoanTransferReport/{id?}")]
        public ActionResult LoanTransferReport(string id)
        {
            int groupId = Convert.ToInt32(id);
            List<MemberLoan> memberLoans = LoanDBService.GetAllMemberLoans(Convert.ToInt32(Session["BranchId"]), groupId);

            return View("LoanTransferReport", memberLoans);
        }
        [HttpGet]
        public ActionResult ViewPendingLoans()
        {
            List<Loan> loans = LoanDBService.GetAllPendingLoans(Convert.ToInt32(Session["BranchId"]));
            return View(loans);
        }


        [Route("ViewLoan/{id?}")]
        public ActionResult ViewLoan(string id)
        {
            int loanId = Convert.ToInt32(id);
            MemberLoan memberLoan = new MemberLoan();
            memberLoan.loan = LoanDBService.GetLoan(loanId);
            memberLoan.member = MemberDBService.GetMember(memberLoan.loan.MemberId);
            memberLoan.member.FamilyMembers = MemberDBService.GetFamilyMembers(memberLoan.loan.MemberId);
            return View("ViewLoan",memberLoan);
        }

        [Route("LoanStatusForm/{id?}")]
        public ActionResult LoanStatusForm(string id)
        {
            int loanId = Convert.ToInt32(id);
            MemberLoan memberLoan = new MemberLoan();
            memberLoan.loan = LoanDBService.GetLoan(loanId);
            memberLoan.member = MemberDBService.GetMember(memberLoan.loan.MemberId);
            memberLoan.member.FamilyMembers = MemberDBService.GetFamilyMembers(memberLoan.loan.MemberId);
            return View(memberLoan);
        }

        [HttpPost]
        public ActionResult LoanStatus(FormCollection form)
        {
            int loanId = Convert.ToInt32(form["LoanId"]);
            int memberId = Convert.ToInt32(form["MemberId"]);
            string loanStatus = form["LoanStatus"];
            string statusRemarks = form["StatusRemarks"];
            LoanDBService.UpdateLoanStatus(loanId, loanStatus, statusRemarks);
            return ViewLoans(MemberDBService.GetGroupId(memberId).ToString());
        }

        [Route("LoanRepaymentStatus/{id?}")]
        public ActionResult LoanRepaymentStatus(string id)
        {
            int groupId = Convert.ToInt32(id);
            LoanRepaymentStatus loanRepaymentStatus = LoanDBService.GetLoanRepaymentStatus(groupId);
            return View(loanRepaymentStatus);
        }

        [HttpGet]
        public ActionResult CumulativeReport()
        {
            List<CumulativeReport> cumulativeReport=LoanDBService.GetCumulativeReport();
            return View("CumulativeReport", cumulativeReport);
        }
    }
}