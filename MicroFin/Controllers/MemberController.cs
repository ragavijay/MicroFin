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
            string memberCode = id;
            if (memberCode == null)
            {
                return View();
            }
            else
            {
                Member member = MemberDBService.GetMember(memberCode);
                return View(member);
            }
        }

        [HttpPost]
        public ActionResult Member(Member member)
        {
            int statusCode;
            if (member.MemberCode == null)
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
                    fileName = member.MemberCode + ".jpg";
                    member.Photo.SaveAs(Path.Combine(directory, fileName));

                }
                if (member.Aadhar != null && member.Aadhar.ContentLength > 0)
                {
                    directory = directory = path + @"\FileUploads\Img\Aadhar\";
                    fileName = member.MemberCode + ".jpg";
                    member.Aadhar.SaveAs(Path.Combine(directory, fileName));
                }
                return ViewMembers(member.GroupCode.ToString());
            }
        }

        [HttpGet]
        [Route("ViewMembers/{id?}")]
        public ActionResult ViewMembers(string id)
        {
            string groupCode = id;
            List<Member> members = MemberDBService.GetAllMembers(groupCode);
            ViewBag.GroupCode = id;
            return View("ViewMembers", members);
        }

        [HttpGet]
        [Route("ViewMember/{id?}")]
        public ActionResult ViewMember(string id)
        {
            Member member = MemberDBService.GetMember(id);
            member.FamilyMembers = MemberDBService.GetFamilyMembers(id);
            return View(member);
        }

        [HttpGet]
        [Route("ViewFamilyMembers/{id?}")]
        public ActionResult ViewFamilyMembers(string id)
        {
            List<FamilyMember> familyMembers = MemberDBService.GetFamilyMembers(id);
            @ViewBag.MemberName = MemberDBService.GetMember(id).MemberName;
            @ViewBag.MemberCode= id;
            return View("ViewFamilyMembers",familyMembers);
        }

        [HttpGet]
        [Route("FamilyMemberForm/{id?}")]
        public ActionResult FamilyMemberForm(string id)
        {
            string[] input= id.Split(' ');
            if (input.Length == 1)
            {
                Member member = MemberDBService.GetMember(id);
                FamilyMember familyMember = new FamilyMember();
                familyMember.MemberCode = member.MemberCode;
                familyMember.OccupationType = EOccupationType.None;
                return View(familyMember);
            } else
            {
                string memberCode = input[0];
                int sNo = Convert.ToInt32(input[1]);
                FamilyMember familyMember = MemberDBService.GetFamilyMember(memberCode, sNo);
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
               return ViewFamilyMembers(familyMember.MemberCode);
            }
        }
        [HttpGet]
        public ActionResult SearchMember()
        {
            return View();
        }

        [HttpGet]
        [Route("ExportMembers/{id?}")]
        public FileStreamResult ExportMembers(string id)
        {
            string groupCode = id;
            List<Member> members = MemberDBService.GetAllMembers(groupCode);
            MemoryStream memory = MicroFin.Models.Member.GetExportMembers(members);
            return File(memory, "application/vnd.ms-excel", "Members.csv");

        }
    }
}