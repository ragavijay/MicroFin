using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroFin.Models;
using MicroFin.DAO;

namespace MicroFin.Controllers
{
    [RoutePrefix("Center")]
    [Authorize]
    public class CenterController : Controller
    {
       
        [HttpGet]
        [Route("GroupCenterForm/{id?}")]
        public ActionResult GroupCenterForm(string id)
        {
            string centerCode = id;
            if (centerCode == null)
            {
                return View();
            }
            else
            {
                GroupCenter center = CenterDBService.GetGroupCenter(centerCode);
                return View(center);
            }
        }

        [HttpPost]
        public ActionResult GroupCenter(GroupCenter center)
        {
            if (center.CenterCode == null)
            {
                center.BranchId = Convert.ToInt32(Session["BranchId"]);
                int statusCode = CenterDBService.AddGroupCenter(center);
                if (statusCode == 0)
                {
                    @ViewBag.ErrCenterName = "Center already existes";
                    return View("GroupCenterForm", center);
                }
                else if(statusCode == -1)
                {
                    @ViewBag.ErrTryAgain = "Try again";
                    return View("GroupCenterForm", center);
                }
                else { 
                    return ViewGroupCenters();
                }
            }
            else
            {
                center.BranchId = Convert.ToInt32(Session["BranchId"]);
                int statusCode = CenterDBService.EditGroupCenter(center);
                if (statusCode == 0)
                {
                    @ViewBag.ErrCenterName = "Center already existes";
                    return View("GroupCenterForm", center);
                }
                else
                {
                    return ViewGroupCenters();
                }
            }
        }

        [HttpGet]
        public ActionResult ViewGroupCenters()
        {
            List<GroupCenter> centers = CenterDBService.GetAllGroupCenters(Convert.ToInt32(Session["BranchId"]));
            return View("ViewGroupCenters",centers);
        }
    }
}