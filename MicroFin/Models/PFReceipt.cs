﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class PFReceipt
    {
        public int ReceiptId { get; set; }
        public DateTime ActualReceiptDate { get; set; }
        public int LoanId { get; set; }
        public String LoanStatus { get; set; }
        public int MemberId{ get; set; }
        public string MemberName{ get; set; }
        public int ProcessingFee{ get; set; }
        public int Insurance{ get; set; }
        public int TotalFee { get; set; }
        public string UserId { get; set; }
    }
}