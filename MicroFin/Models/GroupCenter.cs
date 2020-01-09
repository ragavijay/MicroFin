using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class GroupCenter
    {
        public string CenterCode { get; set; }
        public int CenterId { get; set; }
        public String CenterName { get; set; }
        public int BranchId { get; set; }
    }
}