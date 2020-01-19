using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class EWIDue
    {
        public string LoanCode { get; set; }
        public int BranchId { get; set; }
        public string MemberCode { get; set; }
        public string MemberName{ get; set; }
        public string Phone { get; set; }
        public int NoOfInstalments{ get; set; }
        public int Ewi{ get; set; }
        public string DueDate { get; set; }
    }
}