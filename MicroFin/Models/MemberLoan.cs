using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class MemberLoan
    {
        public Member member { get; set; }
        public Loan loan { get; set; }
    }
}