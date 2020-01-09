using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MicroFin.Models;
using MicroFin.DAO;
using Newtonsoft.Json;

namespace MicroFin.Controllers
{
    public class AjaxController : ApiController
    {
        [HttpGet]
        [Route("api/HomePage")]
        public HttpResponseMessage HomePage()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent("<p>Welcome to AMF</p>");
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;
        }

        [HttpGet]
        [Route("api/GetCenterList/{pattern}")]
        public HttpResponseMessage GetCenterList(string pattern)
        {
            List<GroupCenter> centers = CenterDBService.GetAllGroupCentersByPattern(pattern);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(centers));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet]
        [Route("api/GetGroupList/{pattern}")]
        public HttpResponseMessage GetGroupList(string pattern)
        {
            List<MemberGroup> groups = GroupDBService.GetAllMemberGroupsByPattern(pattern);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(groups));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet]
        [Route("api/CheckMember/{memberCode}")]
        public HttpResponseMessage CheckMember(string memberCode)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(LoanDBService.CheckMember(memberCode));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
            return response;
        }


        [HttpGet]
        [Route("api/CheckGroup/{groupCode}")]
        public HttpResponseMessage CheckGroup(string groupCode)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(LoanDBService.CheckGroup(groupCode));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
            return response;
        }

        [HttpGet]
        [Route("api/ApproveLoan/{loanCode}")]
        public HttpResponseMessage ApproveLoan(string loanCode)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            LoanDBService.ApproveLoan(loanCode);
            response.Content = new StringContent("Approved");
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
            return response;
        }

        [HttpGet]
        [Route("api/GetPFReceipt/{loanCode}")]
        public HttpResponseMessage GetPFReceipt(string loanCode)
        {
            PFReceipt pfReceipt = ReceiptDBService.GetPFReceipt(loanCode);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(pfReceipt));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet]
        [Route("api/GetGroupPFReceipt/{groupCode}")]
        public HttpResponseMessage GetGroupPFReceipt(string groupCode)
        {
            GroupPFReceipt groupPFReceipt = ReceiptDBService.GetGroupPFReceipt(groupCode);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(groupPFReceipt));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }


        [HttpGet]
        [Route("api/GetInstalmentReceipt/{loanCode}")]
        public HttpResponseMessage GetInstalmentReceipt(string loanCode)
        {
            InstalmentReceipt instalmentReceipt = ReceiptDBService.GetInstalmentReceipt(loanCode);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(instalmentReceipt));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

      
        [HttpGet]
        [Route("api/GetGroupInstalmentReceipt/{groupCode}")]
        public HttpResponseMessage GetGroupInstalmentReceipt(string groupCode)
        {
            GroupInstalmentReceipt groupInstalmentReceipt = ReceiptDBService.GetGroupInstalmentReceipt(groupCode);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(groupInstalmentReceipt));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet]
        [Route("api/GetMemberByAadhar/{searchText}")]
        public HttpResponseMessage GetMemberByAadhar(string searchText)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(MemberDBService.GetMemberByAadhar(searchText)));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet]
        [Route("api/GetMemberByPhone/{searchText}")]
        public HttpResponseMessage GetMemberByPhone(string searchText)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(MemberDBService.GetMemberByPhone(searchText)));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }
        [HttpGet]
        [Route("api/GetMemberByName/{searchText}")]
        public HttpResponseMessage GetMemberByName(string searchText)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(MemberDBService.GetMemberByName(searchText)));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

    }
}
