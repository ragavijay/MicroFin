using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroFin.Models;
using MicroFin.DAO;

namespace MicroFin.Controllers
{
    [RoutePrefix("Group")]
    [Authorize]
    public class GroupController : Controller
    {
        [HttpGet]
        [Route("MemberGroupForm/{id?}")]
        public ActionResult MemberGroupForm(string id)
        {
            int groupId = Convert.ToInt32(id);
            if (groupId == 0)
            {
                return View();
            }
            else
            {
                MemberGroup group = GroupDBService.GetMemberGroup(groupId);
                return View(group);
            }
        }

        [HttpPost]
        public ActionResult MemberGroup(MemberGroup group)
        {
            if (group.GroupId == 0)
            {
                int statusCode = GroupDBService.AddMemberGroup(group);
                if (statusCode == 0)
                {
                    @ViewBag.ErrGroupName = "Center already existes";
                    return View("MemberGroupForm", group);
                }
                else if (statusCode == -1)
                {
                    @ViewBag.ErrTryAgain = "Try again";
                    return View("MemberGroupForm", group);
                }
                else
                {
                    return ViewMemberGroups();
                }
            }
            else
            {
                int statusCode = GroupDBService.EditMemberGroup(group);
                if (statusCode == 0)
                {
                    @ViewBag.ErrGroupName = "Group already existes";
                    return View("MemberGroupForm", group);
                }
                else
                {
                    return ViewMemberGroups();
                }
            }
        }
        [HttpGet]
        public ActionResult ViewMemberGroups()
        {
            List<MemberGroup> groups = GroupDBService.GetAllMemberGroups(Convert.ToInt32(Session["BranchId"]));
            return View("ViewMemberGroups",groups);
        }
    }
}