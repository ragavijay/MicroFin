using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MicroFin.Models;
using MicroFin.DAO;
namespace MicroFin.Controllers
{
    public class AppController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.errMsg = "";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Home(Login login)
        {
            string userType;
            userType = DBService.GetUserType(login);
            if (userType.Equals("A") || userType.Equals("D") || userType.Equals("M"))
            {
                Session["userType"] = userType;
                Session["userId"] = login.UserId;
                Session["branchId"] = 1;
                Session["branch"] = DBService.GetBranchName(1);
                FormsAuthentication.SetAuthCookie(login.UserId,true);
                return View();
            }
            else 
            {
                ViewBag.ErrMsg = "Invalid Credentials";
                return View("Login");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            ViewBag.ErrMsg = "";
            return View("Login");
        }
    }
}