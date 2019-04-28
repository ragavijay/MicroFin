using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class FamilyMember
    {
        public int MemberId { get; set; }
        public int SNo { get; set; }
        public string FamilyMemberName { get; set; }
        public string Relationship { get; set; }
        public EOccupationType OccupationType { get; set; }
        public int MonthlyIncome { get; set; }
        public string Qualification { get; set; }
    }
}