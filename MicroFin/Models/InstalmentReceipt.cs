﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class InstalmentReceipt
    {
        public int ReceiptId { get; set; }
        public int LoanId { get; set; }
        public String LoanStatus { get; set; }
        public int MemberId{ get; set; }
        public string MemberName{ get; set; }
        public int NoOfInstalments{ get; set; }
        public int Ewi{ get; set; }
        public int TotalDue { get; set; }
        public string UserId { get; set; }
    }
}