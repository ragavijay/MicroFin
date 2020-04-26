using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class MemberGroup
    {
        public string GroupCode { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string CenterCode { get; set; }
        public string CenterName { get; set; }
        public string LeaderName { get; set; }
        public bool isLoanRunning { get; set; } 
      
    }
}