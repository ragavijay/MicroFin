using System;
using System.IO;
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
            string loanCode = id;
            if (loanCode == null)
            {
                @ViewBag.DisplayMode = "display:none";
                return View();
            }
            else
            {
                Loan loan = LoanDBService.GetLoan(loanCode);
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
                return ViewLoans(groupLoan.GroupCode.ToString());
            }
            else
            {
                return View("GroupLoanForm");
            }
        }

        [HttpPost]
        public ActionResult Loan(Loan loan)
        {
            if (loan.LoanCode == null)
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
                    return ViewLoans(MemberDBService.GetGroupCode(loan.MemberCode));
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
                    return ViewLoan(loan.LoanCode);
                }
            }
        }

        [HttpGet]
        [Route("ViewLoans/{id?}")]
        public ActionResult ViewLoans(string id)
        {
            string groupCode = id;
            List<Loan> loans = LoanDBService.GetAllLoans(Convert.ToInt32(Session["BranchId"]), groupCode);
            return View("ViewLoans", loans);
        }

        [HttpGet]
        [Route("LoanTransferReport/{id?}")]
        public ActionResult LoanTransferReport(string id)
        {
            string groupCode = id;
            List<MemberLoan> memberLoans = LoanDBService.GetAllMemberLoans(Convert.ToInt32(Session["BranchId"]), groupCode);
            ViewBag.GroupId = id;
            return View("LoanTransferReport", memberLoans);
        }

        [HttpGet]
        [Route("ExportTransferReport/{id?}")]
        public FileStreamResult ExportTransferReport(string id)
        {
            string groupCode = id;
            List<MemberLoan> memberLoans = LoanDBService.GetAllMemberLoans(Convert.ToInt32(Session["BranchId"]), groupCode);
            MemoryStream memory = MemberLoan.GetExportTransferReport(memberLoans);
            return File(memory, "application/vnd.ms-excel", "LoanTransfer.csv");

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
            string loanCode = id;
            MemberLoan memberLoan = new MemberLoan();
            memberLoan.loan = LoanDBService.GetLoan(loanCode);
            memberLoan.member = MemberDBService.GetMember(memberLoan.loan.MemberCode);
            memberLoan.member.FamilyMembers = MemberDBService.GetFamilyMembers(memberLoan.loan.MemberCode);
            return View("ViewLoan",memberLoan);
        }

        [Route("LoanStatusForm/{id?}")]
        public ActionResult LoanStatusForm(string id)
        {
            string loanCode = id;
            MemberLoan memberLoan = new MemberLoan();
            memberLoan.loan = LoanDBService.GetLoan(loanCode);
            memberLoan.member = MemberDBService.GetMember(memberLoan.loan.MemberCode);
            memberLoan.member.FamilyMembers = MemberDBService.GetFamilyMembers(memberLoan.loan.MemberCode);
            return View(memberLoan);
        }

        [HttpPost]
        public ActionResult LoanStatus(FormCollection form)
        {
            string loanCode = form["LoanCode"];
            string memberCode = form["MemberCode"];
            string loanStatus = form["LoanStatus"];
            string statusRemarks = form["StatusRemarks"];
            LoanDBService.UpdateLoanStatus(loanCode, loanStatus, statusRemarks);
            return ViewLoans(MemberDBService.GetGroupCode(memberCode));
        }

        [Route("LoanRepaymentStatus/{id?}")]
        public ActionResult LoanRepaymentStatus(string id)
        {
            string groupCode = id;
            LoanRepaymentStatus loanRepaymentStatus = LoanDBService.GetLoanRepaymentStatus(groupCode);
            return View(loanRepaymentStatus);
        }

        [HttpGet]
        public ActionResult CumulativeReport()
        {
            List<CumulativeReport> cumulativeReport=LoanDBService.GetCumulativeReport();
            return View("CumulativeReport", cumulativeReport);
        }

        [HttpGet]
        [Route("ExportLoans")]
        public FileStreamResult ExportLoans()
        {
            List<MemberLoan> memberLoans = LoanDBService.GetGlobalMemberLoans(1);
            MemoryStream memory = MicroFin.Models.Loan.GetExportLoans(memberLoans);
            return File(memory, "application/vnd.ms-excel", "Loans.csv");

        }
    }
}