using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class CashReceiptStatement
    {

        public int SNo { get; set; }
        public int ReceiptId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int LoanId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}