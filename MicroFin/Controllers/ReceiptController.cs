using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
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

        public ActionResult GroupPFReceiptForm()
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

        [HttpPost]
        public ActionResult GroupPFReceipt(GroupPFReceipt groupPFReceipt)
        {
            groupPFReceipt.UserId = Session["UserId"].ToString();
            List<GroupPFReceiptInfo> groupPFReceiptInfoList = ReceiptDBService.GenerateGroupPFReceipt(groupPFReceipt);
            return View(groupPFReceiptInfoList);
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

            string userId = Session["userId"].ToString();
            string userType = Session["userType"].ToString();
            DateTime fromDate = DateTime.ParseExact(Request["FromDate"],"dd/MM/yyyy",CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(Request["ToDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<CashReceiptStatement> statement = ReceiptDBService.GetCashReceiptStatement(userId, userType, fromDate, toDate);
            return View(statement);
        }
    }
}