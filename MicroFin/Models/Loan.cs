using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.IO;

namespace MicroFin.Models
{
    public class Loan
    {
        public string LoanCode { get; set; }
        public int LoanId { get; set; }
        public int LoanCycle { get; set; }
        public int MemberId{ get; set; }
        public string MemberCode { get; set; }
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
        public DateTime LoanDisposalDate { get; set; }
        public string StatusRemarks { get; set; }
        public DateTime LastPaymentDate { get; set; }

        public static MemoryStream GetExportLoans(List<MemberLoan> memberLoans)
        {
            MemoryStream memory = new MemoryStream();
            StreamWriter stream = new StreamWriter(memory);
            foreach (MemberLoan memberLoan in memberLoans)
            {
                stream.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}",
                    new DateTime().ToString("dd-MM-yyyy"),  
                    "T04",
                    memberLoan.loan.LoanCycle,
                    memberLoan.loan.LoanPurpose,
                    (memberLoan.loan.LoanStatus.Equals("P") ? "S01" : memberLoan.loan.LoanStatus.Equals("A") ? "S02" : memberLoan.loan.LoanStatus.Equals("O") ? "S04" : "S07"),
                    memberLoan.loan.LoanDate.ToString("dd-MM-yyyy"),
                    memberLoan.loan.LoanDate.ToString("dd-MM-yyyy"),
                    memberLoan.loan.LoanDate.ToString("dd-MM-yyyy"),
                    "NA",
                    memberLoan.loan.LastPaymentDate.ToString("dd-MM-yyyy"),
                    memberLoan.loan.LoanAmount,
                    memberLoan.loan.LoanAmount,
                    memberLoan.loan.LoanAmount,
                    memberLoan.loan.Tenure,
                    "F01",
                    memberLoan.loan.Ewi,
                    "0",
                    "0",
                    "NA",
                    "0",
                    "NA",
                    "NA",
                    "",
                    0,
                    0
                    )
                );
            }
            stream.Flush();
            memory.Position = 0;
            return memory;
        }
    }
}