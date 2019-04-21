using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroFin.Models;
using MicroFin.DAO;
namespace MicroFin.Controllers
{
    [Authorize]
    public class ReceiptController : Controller
    {
        public ActionResult PFReceiptForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PFReceipt(PFReceipt pfReceipt)
        {
            pfReceipt.UserId = Session["UserId"].ToString();
            ReceiptDBService.GeneratePFReceipt(pfReceipt);
            return View(pfReceipt);
        }

        public ActionResult InstalmentReceiptForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InstalmentReceipt(InstalmentReceipt instalmentReceipt)
        {
            instalmentReceipt.UserId = Session["UserId"].ToString();
            ReceiptDBService.GenerateInstalmentReceipt(instalmentReceipt);
            return View(instalmentReceipt);
        }
        [HttpGet]
        public ActionResult ViewEwiDueList()
        {
            List<EWIDue> ewiDues = ReceiptDBService.GetEwiDue(Convert.ToInt32(Session["BranchId"]));
            return View(ewiDues);
        }
        [HttpGet]
        public ActionResult CashReceiptStatementForm()
        {
            return View();

        }
        [HttpPost]
        public ActionResult CashReceiptStatement()
        {

            string userId = Session["UserId"].ToString();
            string userType = Session["UserType"].ToString();
            DateTime fromDate = DateTime.Parse(Request["FromDate"]);
            DateTime toDate = DateTime.Parse(Request["ToDate"]);
            List<CashReceiptStatement> statement = ReceiptDBService.GetCashReceiptStatement(userId, userType, fromDate, toDate);
            return View(statement);
        }
    }
}