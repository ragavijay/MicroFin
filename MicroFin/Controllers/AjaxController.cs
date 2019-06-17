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
        [Route("api/CheckMember/{memberId}")]
        public HttpResponseMessage CheckMember(int memberId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(LoanDBService.CheckMember(memberId));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
            return response;
        }


        [HttpGet]
        [Route("api/CheckGroup/{groupId}")]
        public HttpResponseMessage CheckGroup(int groupId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(LoanDBService.CheckGroup(groupId));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
            return response;
        }

        [HttpGet]
        [Route("api/ApproveLoan/{loanId}")]
        public HttpResponseMessage ApproveLoan(int loanId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            LoanDBService.ApproveLoan(loanId);
            response.Content = new StringContent("Approved");
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
            return response;
        }

        [HttpGet]
        [Route("api/GetPFReceipt/{loanId}")]
        public HttpResponseMessage GetPFReceipt(int loanId)
        {
            PFReceipt pfReceipt = ReceiptDBService.GetPFReceipt(loanId);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(pfReceipt));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet]
        [Route("api/GetInstalmentReceipt/{loanId}")]
        public HttpResponseMessage GetInstalmentReceipt(int loanId)
        {
            InstalmentReceipt instalmentReceipt = ReceiptDBService.GetInstalmentReceipt(loanId);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(instalmentReceipt));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet]
        [Route("api/GetGroupPFReceipt/{groupId}")]
        public HttpResponseMessage GetGroupPFReceipt(int groupId)
        {
            GroupPFReceipt groupPFReceipt = ReceiptDBService.GetGroupPFReceipt(groupId);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(groupPFReceipt));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}
