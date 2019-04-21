using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using MicroFin.Models;
using MicroFin.DAO;
using System.Web.Hosting;

namespace MicroFin.Controllers
{
    [RoutePrefix("Member")]
    [Authorize]
    public class MemberController : Controller
    {
       
        [HttpGet]
        [Route("MemberForm/{id?}")]
        public ActionResult MemberForm(string id)
        {
            int memberId = Convert.ToInt32(id);
            if (memberId == 0)
            {
                return View();
            }
            else
            {
                Member member = MemberDBService.GetMember(memberId);
                return View(member);
            }
        }

        [HttpPost]
        public ActionResult Member(Member member)
        {
            int statusCode;
            if (member.MemberId == 0)
            {
                statusCode = MemberDBService.AddMember(member);
               
            }
            else
            {
                statusCode = MemberDBService.EditMember(member);
            }
            if (statusCode == -1)
            {
                @ViewBag.ErrMemberAadharNumber = "Aadhar already existes";
                return View("MemberForm", member);
            }
            else if (statusCode == -2)
            {
                @ViewBag.ErrMemberType = "Leader already exists for the group";
                return View("MemberForm", member);

            }
            else if (statusCode == -3)
            {
                @ViewBag.ErrTryAgain = "Try again";
                return View("MemberForm", member);

            }
            else
            {
                string path, directory, fileName;
                path = Server.MapPath("~");
                path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
                if (member.Photo != null && member.Photo.ContentLength>0)
                {
                    directory = path +  @"\FileUploads\Img\Member\";
                    fileName = member.MemberId + ".jpg";
                    member.Photo.SaveAs(Path.Combine(directory, fileName));

                }
                if (member.Aadhar != null && member.Aadhar.ContentLength > 0)
                {
                    directory = directory = path + @"\FileUploads\Img\Aadhar\";
                    fileName = member.MemberId + ".jpg";
                    member.Aadhar.SaveAs(Path.Combine(directory, fileName));
                }
                return View(member);
            }
        }

        [HttpGet]
        public ActionResult ViewMembers()
        {
            List<Member> members = MemberDBService.GetAllMembers(Convert.ToInt32(Session["BranchId"]));
            return View(members);
        }

        [HttpGet]
        [Route("ViewMember/{id?}")]
        public ActionResult ViewMember(string id)
        {
            Member member = MemberDBService.GetMember(Convert.ToInt32(id));
            String path = Server.MapPath("~");
            path = @"http://fileuploads.amftn.in/Img/Member/" + id + ".jpg";
            @ViewBag.Photo = path;
            @ViewBag.Age = MicroFin.Models.Member.GetAge(member.DOB);
            @ViewBag.NomineeAge = MicroFin.Models.Member.GetAge(member.NomineeDOB);
            return View(member);
        }

    }
}