using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using MicroFin.Models;
using MicroFin;
namespace MicroFin.DAO
{
    public class LoanDBService
    {
        public static string CheckMember(int memberId)
        {
            String memberName;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("CheckMember", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Decimal, 5);
                    cmd.Parameters["@pMemberId"].Value = memberId;
                    cmd.Parameters.Add("@ireturnvalue", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        memberName = rdr[0].ToString();
                    }
                }
            }
            return memberName;
        }

        public static string CheckGroup(int groupId)
        {
            String response="error";
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("CheckGroup", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Decimal, 5);
                    cmd.Parameters["@pGroupId"].Value = groupId;
                    cmd.Parameters.Add("@ireturnvalue", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        response = rdr[0].ToString();
                    }
                }
            }
            return response;
        }
        public static int AddLoan(Loan loan)
        {
            int status = 0;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddLoan", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Value = loan.MemberId;

                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                    cmd.Parameters["@pBranchId"].Value = loan.BranchId;

                    cmd.Parameters.Add("@pLoanPurpose", MySqlDbType.VarChar,40);
                    cmd.Parameters["@pLoanPurpose"].Value = loan.LoanPurpose;

                    cmd.Parameters.Add("@pLoanAmount", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanAmount"].Value = loan.LoanAmount;

                    cmd.Parameters.Add("@pProcessingFeeRate", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFeeRate"].Value = loan.ProcessingFeeRate;

                    cmd.Parameters.Add("@pProcessingFee", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFee"].Value = loan.ProcessingFee;

                    cmd.Parameters.Add("@pInsuranceRate", MySqlDbType.Int32);
                    cmd.Parameters["@pInsuranceRate"].Value = loan.InsuranceRate;

                    cmd.Parameters.Add("@pInsurance", MySqlDbType.Int32);
                    cmd.Parameters["@pInsurance"].Value = loan.Insurance;

                    cmd.Parameters.Add("@pTenure", MySqlDbType.Int32);
                    cmd.Parameters["@pTenure"].Value = loan.Tenure;

                    cmd.Parameters.Add("@pInterestRate", MySqlDbType.Int32);
                    cmd.Parameters["@pInterestRate"].Value = loan.InterestRate;

                    cmd.Parameters.Add("@pEwi", MySqlDbType.Int32);
                    cmd.Parameters["@pEwi"].Value = loan.Ewi;

                    cmd.Parameters.Add("@pStatus", MySqlDbType.Int32);
                    cmd.Parameters["@pStatus"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    status = Convert.ToInt32(cmd.Parameters["@pStatus"].Value);
                    if (status == 1)
                    {
                        int loanId = Convert.ToInt32(cmd.Parameters["@pLoanId"].Value);
                        loan.LoanId = loanId;
                    }
                }
            }
            return status;
        }

        public static int AddGroupLoan(GroupLoan groupLoan)
        {
            int status = 0;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddGroupLoan", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = groupLoan.GroupId;

                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                    cmd.Parameters["@pBranchId"].Value = groupLoan.BranchId;

                    cmd.Parameters.Add("@pLoanPurpose", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pLoanPurpose"].Value = groupLoan.LoanPurpose;

                    cmd.Parameters.Add("@pLoanAmount", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanAmount"].Value = groupLoan.LoanAmount;

                    cmd.Parameters.Add("@pProcessingFeeRate", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFeeRate"].Value = groupLoan.ProcessingFeeRate;

                    cmd.Parameters.Add("@pProcessingFee", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFee"].Value = groupLoan.ProcessingFee;

                    cmd.Parameters.Add("@pInsuranceRate", MySqlDbType.Int32);
                    cmd.Parameters["@pInsuranceRate"].Value = groupLoan.InsuranceRate;

                    cmd.Parameters.Add("@pInsurance", MySqlDbType.Int32);
                    cmd.Parameters["@pInsurance"].Value = groupLoan.Insurance;

                    cmd.Parameters.Add("@pTenure", MySqlDbType.Int32);
                    cmd.Parameters["@pTenure"].Value = groupLoan.Tenure;

                    cmd.Parameters.Add("@pInterestRate", MySqlDbType.Int32);
                    cmd.Parameters["@pInterestRate"].Value = groupLoan.InterestRate;

                    cmd.Parameters.Add("@pEwi", MySqlDbType.Int32);
                    cmd.Parameters["@pEwi"].Value = groupLoan.Ewi;

                    cmd.Parameters.Add("@pStatus", MySqlDbType.Int32);
                    cmd.Parameters["@pStatus"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    status = Convert.ToInt32(cmd.Parameters["@pStatus"].Value);
                }
            }
            return status;
        }
        public static List<Loan> GetAllLoans(int branchId, int groupId)
        {
            Loan loan;
            List<Loan> loans = new List<Loan>();
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetAllLoans", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                    cmd.Parameters["@pBranchId"].Value = branchId;
                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = groupId;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            loan = new Loan();
                            loan.LoanId = Convert.ToInt32(rdr["LoanId"].ToString());
                            loan.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                            loan.MemberName = rdr["MemberName"].ToString();
                            loan.LoanPurpose = rdr["LoanPurpose"].ToString();
                            loan.LoanAmount = Convert.ToInt32(rdr["LoanAmount"].ToString());
                            loan.ProcessingFeeRate = Convert.ToSingle(rdr["ProcessingFeeRate"].ToString());
                            loan.ProcessingFee = Convert.ToInt32(rdr["ProcessingFee"].ToString());
                            loan.InsuranceRate = Convert.ToSingle(rdr["InsuranceRate"].ToString());
                            loan.Insurance = Convert.ToInt32(rdr["Insurance"].ToString());
                            loan.Tenure = Convert.ToInt32(rdr["Tenure"].ToString());
                            loan.InterestRate = Convert.ToSingle(rdr["InterestRate"].ToString());
                            loan.Ewi = Convert.ToInt32(rdr["Ewi"].ToString());
                            loan.LoanStatus = rdr["LoanStatus"].ToString();
                            //loan.RepaymentAmount = loan.Tenure * loan.Ewi + loan.ProcessingFee + loan.Insurance;
                            loan.RepaymentAmount = loan.Tenure * loan.Ewi;
                            loan.StatusRemarks = rdr["StatusRemarks"].ToString();
                            loans.Add(loan);
                        }
                    }
                }
            }
            return loans;
        }

        public static List<MemberLoan> GetAllMemberLoans(int branchId, int groupId)
        {
            Loan loan;
            Member member;
            MemberLoan memberLoan;
            List<MemberLoan> memberLoans = new List<MemberLoan>();
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetAllMemberLoans", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                    cmd.Parameters["@pBranchId"].Value = branchId;
                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = groupId;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            memberLoan = new MemberLoan();
                            loan = new Loan();
                            loan.LoanId = Convert.ToInt32(rdr["LoanId"].ToString());
                            loan.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                            loan.MemberName = rdr["MemberName"].ToString();
                            loan.LoanAmount = Convert.ToInt32(rdr["LoanAmount"].ToString());
                            member = new Member();
                            member.MemberId = loan.MemberId;
                            member.MemberName = loan.MemberName;
                            member.AccountNumber = rdr["AccountNumber"].ToString();
                            member.IFSC = rdr["IFSC"].ToString();
                            memberLoan.member = member;
                            memberLoan.loan = loan;
                            memberLoans.Add(memberLoan);
                        }
                    }
                }
            }
            return memberLoans;
        }

        public static List<Loan> GetAllPendingLoans(int branchId)
        {
            Loan loan;
            List<Loan> loans = new List<Loan>();
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetAllPendingLoans", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                    cmd.Parameters["@pBranchId"].Value = branchId;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            loan = new Loan();
                            loan.LoanId = Convert.ToInt32(rdr["LoanId"].ToString());
                            loan.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                            loan.MemberName = rdr["MemberName"].ToString();
                            loan.LoanPurpose = rdr["LoanPurpose"].ToString();
                            loan.LoanAmount = Convert.ToInt32(rdr["LoanAmount"].ToString());
                            loan.ProcessingFeeRate = Convert.ToSingle(rdr["ProcessingFeeRate"].ToString());
                            loan.ProcessingFee = Convert.ToInt32(rdr["ProcessingFee"].ToString());
                            loan.InsuranceRate = Convert.ToSingle(rdr["InsuranceRate"].ToString());
                            loan.Insurance = Convert.ToInt32(rdr["Insurance"].ToString());
                            loan.Tenure = Convert.ToInt32(rdr["Tenure"].ToString());
                            loan.InterestRate = Convert.ToSingle(rdr["InterestRate"].ToString());
                            loan.Ewi = Convert.ToInt32(rdr["Ewi"].ToString());
                            loan.LoanStatus = rdr["LoanStatus"].ToString();
                            //loan.RepaymentAmount = loan.Tenure * loan.Ewi + loan.ProcessingFee + loan.Insurance;
                            loan.RepaymentAmount = loan.Tenure * loan.Ewi;
                            loan.StatusRemarks = rdr["StatusRemarks"].ToString();
                            loans.Add(loan);
                        }
                    }
                }
            }
            return loans;
        }
        public static Loan GetLoan(int loanId)
        {
            Loan loan = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetLoan", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = loanId;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            loan = new Loan();
                            loan.LoanId = Convert.ToInt32(rdr["LoanId"].ToString());
                            loan.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                            loan.MemberName = rdr["MemberName"].ToString();
                            loan.LoanPurpose = rdr["LoanPurpose"].ToString();
                            loan.LoanAmount = Convert.ToInt32(rdr["LoanAmount"].ToString());
                            loan.ProcessingFeeRate = Convert.ToSingle(rdr["ProcessingFeeRate"].ToString());
                            loan.ProcessingFee = Convert.ToInt32(rdr["ProcessingFee"].ToString());
                            loan.InsuranceRate = Convert.ToSingle(rdr["InsuranceRate"].ToString());
                            loan.Insurance = Convert.ToInt32(rdr["Insurance"].ToString());
                            loan.Tenure = Convert.ToInt32(rdr["Tenure"].ToString());
                            loan.InterestRate = Convert.ToSingle(rdr["InterestRate"].ToString());
                            loan.Ewi = Convert.ToInt32(rdr["Ewi"].ToString());
                            loan.LoanStatus = rdr["LoanStatus"].ToString();
                            //loan.RepaymentAmount = loan.Tenure * loan.Ewi + loan.ProcessingFee + loan.Insurance;
                            loan.RepaymentAmount = loan.Tenure * loan.Ewi;
                            loan.StatusRemarks = rdr["StatusRemarks"].ToString();
                        }
                    }
                }
            }
            return loan;
        }



        public static int EditLoan(Loan loan)
        {
            int statusCode = 0;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("EditLoan", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = loan.LoanId;

                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Value = loan.MemberId;

                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                    cmd.Parameters["@pBranchId"].Value = loan.BranchId;

                    cmd.Parameters.Add("@pLoanPurpose", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pLoanPurpose"].Value = loan.LoanPurpose;

                    cmd.Parameters.Add("@pLoanAmount", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanAmount"].Value = loan.LoanAmount;

                    cmd.Parameters.Add("@pProcessingFeeRate", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFeeRate"].Value = loan.ProcessingFeeRate;

                    cmd.Parameters.Add("@pProcessingFee", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFee"].Value = loan.ProcessingFee;

                    cmd.Parameters.Add("@pInsuranceRate", MySqlDbType.Int32);
                    cmd.Parameters["@pInsuranceRate"].Value = loan.InsuranceRate;

                    cmd.Parameters.Add("@pInsurance", MySqlDbType.Int32);
                    cmd.Parameters["@pInsurance"].Value = loan.Insurance;

                    cmd.Parameters.Add("@pTenure", MySqlDbType.Int32);
                    cmd.Parameters["@pTenure"].Value = loan.Tenure;

                    cmd.Parameters.Add("@pInterestRate", MySqlDbType.Int32);
                    cmd.Parameters["@pInterestRate"].Value = loan.InterestRate;

                    cmd.Parameters.Add("@pEwi", MySqlDbType.Int32);
                    cmd.Parameters["@pEwi"].Value = loan.Ewi;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value);
                }
            }
            return statusCode;
        }
        public static void ApproveLoan(int loanId)
        {
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("ApproveLoan", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = loanId;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateLoanStatus(int loanId, string loanStatus, string statusRemarks)
        {
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("UpdateLoanStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = loanId;
                    cmd.Parameters.Add("@pLoanStatus", MySqlDbType.VarChar,1);
                    cmd.Parameters["@pLoanStatus"].Value = loanStatus;
                    cmd.Parameters.Add("@pStatusRemarks", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pStatusRemarks"].Value = statusRemarks;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}