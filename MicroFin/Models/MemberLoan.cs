using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace MicroFin.Models
{
    public class MemberLoan
    {
        public Member member { get; set; }
        public Loan loan { get; set; }

        public static MemoryStream GetExportTransferReport(List<MemberLoan> memberLoans)
        {
            MemoryStream memory = new MemoryStream();
            StreamWriter stream = new StreamWriter(memory);
            foreach (MemberLoan memberLoan in memberLoans)
            {
                stream.WriteLine(String.Format("{0},{1},{2},{3}",
                    memberLoan.loan.LoanAmount,
                    memberLoan.member.IFSC,
                    memberLoan.member.AccountNumber,
                    memberLoan.member.MemberName)
                );
            }
            stream.Flush();
            memory.Position = 0;
            return memory;
        }
    }
}