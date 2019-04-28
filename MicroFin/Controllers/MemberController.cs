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
                return ViewMembers(member.GroupId.ToString());
            }
        }

        [HttpGet]
        [Route("ViewMembers/{id?}")]
        public ActionResult ViewMembers(string id)
        {
            int groupId = Convert.ToInt32(id);
            List<Member> members = MemberDBService.GetAllMembers(groupId);
            return View("ViewMembers", members);
        }

        [HttpGet]
        [Route("ViewMember/{id?}")]
        public ActionResult ViewMember(string id)
        {
            Member member = MemberDBService.GetMember(Convert.ToInt32(id));
            member.FamilyMembers = MemberDBService.GetFamilyMembers(Convert.ToInt32(id));
            return View(member);
        }

        [HttpGet]
        [Route("ViewFamilyMembers/{id?}")]
        public ActionResult ViewFamilyMembers(string id)
        {
            List<FamilyMember> familyMembers = MemberDBService.GetFamilyMembers(Convert.ToInt32(id));
            @ViewBag.MemberName = MemberDBService.GetMember(Convert.ToInt32(id)).MemberName;
            @ViewBag.MemberId= id;
            return View("ViewFamilyMembers",familyMembers);
        }

        [HttpGet]
        [Route("FamilyMemberForm/{id?}")]
        public ActionResult FamilyMemberForm(string id)
        {
            string[] input= id.Split(' ');
            if (input.Length == 1)
            {
                Member member = MemberDBService.GetMember(Convert.ToInt32(id));
                FamilyMember familyMember = new FamilyMember();
                familyMember.MemberId = member.MemberId;
                familyMember.OccupationType = EOccupationType.None;
                return View(familyMember);
            } else
            {
                int memberId = Convert.ToInt32(input[0]);
                int sNo = Convert.ToInt32(input[1]);
                FamilyMember familyMember = MemberDBService.GetFamilyMember(memberId, sNo);
                return View(familyMember);
            }

        }

        [HttpPost]
        public ActionResult FamilyMember(FamilyMember familyMember)
        {
            int statusCode;
            if (familyMember.SNo == 0)
            {
                statusCode = MemberDBService.AddFamilyMember(familyMember);

            }
            else
            {
                statusCode = MemberDBService.EditFamilyMember(familyMember);
            }
            if (statusCode == 0)
            {
                @ViewBag.ErrTryAgain = "Try again";
                return View("FamilyMemberForm", familyMember);

            }
            else
            {
               return ViewFamilyMembers(familyMember.MemberId.ToString());
            }
        }
    }
}