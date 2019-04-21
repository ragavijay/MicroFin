using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class MemberGroup
    {
        public int GroupId { get; set; }
        public String GroupName { get; set; }
        public int CenterId { get; set; }
        public String CenterName { get; set; }
        public String LeaderName { get; set; }
    }
}