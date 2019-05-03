using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int MemberId{ get; set; }
        public string MemberName{ get; set; }
        public int BranchId{ get; set; }
        public string LoanPurpose{ get; set; }
        public int LoanAmount { get; set; }
        public float ProcessingFeeRate{ get; set; }
        public int ProcessingFee{ get; set; }
        public float InsuranceRate{ get; set; }
        public int Insurance{ get; set; }
        public int Tenure{ get; set; }
        public float InterestRate{ get; set; }
        public float RepaymentAmount { get; set; }
        public int Ewi{ get; set; }
        public string LoanStatus{ get; set; }
        public DateTime LoanDate{ get; set; }
        public string StatusRemarks { get; set; }
    }
}