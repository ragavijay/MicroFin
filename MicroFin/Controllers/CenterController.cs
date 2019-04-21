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
            int centerId = Convert.ToInt32(id);
            if (centerId == 0)
            {
                return View();
            }
            else
            {
                GroupCenter center = CenterDBService.GetGroupCenter(centerId);
                return View(center);
            }
        }

        [HttpPost]
        public ActionResult GroupCenter(GroupCenter center)
        {
            if (center.CenterId == 0)
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
                {
                    return View(center);
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
                    return View(center);
                }
            }
        }

        [HttpGet]
        public ActionResult ViewGroupCenters()
        {
            List<GroupCenter> centers = CenterDBService.GetAllGroupCenters(Convert.ToInt32(Session["BranchId"]));
            return View(centers);
        }
    }
}