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
    public class ReceiptDBService
    {
        public static PFReceipt GetPFReceipt(int loanId)
        {
            PFReceipt pfReceipt = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetLoanStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = loanId;

                    cmd.Parameters.Add("@pLoanStatus", MySqlDbType.VarChar, 1);
                    cmd.Parameters["@pLoanStatus"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pMemberName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pMemberName"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pProcessingFee", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFee"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pInsurance", MySqlDbType.Int32);
                    cmd.Parameters["@pInsurance"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pEwi", MySqlDbType.Int32);
                    cmd.Parameters["@pEwi"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfInstalments"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pFromDate", MySqlDbType.Date);
                    cmd.Parameters["@pFromDate"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pToDate", MySqlDbType.Date);
                    cmd.Parameters["@pToDate"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    pfReceipt = new PFReceipt();
                    pfReceipt.LoanId = loanId;
                    pfReceipt.LoanStatus = cmd.Parameters["@pLoanStatus"].Value.ToString();
                    if (pfReceipt.LoanStatus == "A")
                    {
                        pfReceipt.MemberId = Convert.ToInt32(cmd.Parameters["@pMemberId"].Value.ToString());
                        pfReceipt.MemberName = cmd.Parameters["@pMemberName"].Value.ToString();
                        pfReceipt.ProcessingFee = Convert.ToInt32(cmd.Parameters["@pProcessingFee"].Value.ToString());
                        pfReceipt.Insurance = Convert.ToInt32(cmd.Parameters["@pInsurance"].Value.ToString());
                        pfReceipt.TotalFee = pfReceipt.ProcessingFee + pfReceipt.Insurance;
                    }
                }
            }
            return pfReceipt;
        }


        public static InstalmentReceipt GetInstalmentReceipt(int loanId)
        {
            InstalmentReceipt instalmentReceipt = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetLoanStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = loanId;

                    cmd.Parameters.Add("@pLoanStatus", MySqlDbType.VarChar, 1);
                    cmd.Parameters["@pLoanStatus"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pMemberName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pMemberName"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pProcessingFee", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFee"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pInsurance", MySqlDbType.Int32);
                    cmd.Parameters["@pInsurance"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pEwi", MySqlDbType.Int32);
                    cmd.Parameters["@pEwi"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfInstalments"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pFromDate", MySqlDbType.Date);
                    cmd.Parameters["@pFromDate"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pToDate", MySqlDbType.Date);
                    cmd.Parameters["@pToDate"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    instalmentReceipt = new InstalmentReceipt();
                    instalmentReceipt.LoanId = loanId;
                    instalmentReceipt.LoanStatus = cmd.Parameters["@pLoanStatus"].Value.ToString();
                    if (instalmentReceipt.LoanStatus == "O")
                    {
                        instalmentReceipt.MemberId = Convert.ToInt32(cmd.Parameters["@pMemberId"].Value.ToString());
                        instalmentReceipt.MemberName = cmd.Parameters["@pMemberName"].Value.ToString();
                        instalmentReceipt.NoOfInstalments = Convert.ToInt32(cmd.Parameters["@pNoOfInstalments"].Value.ToString());
                        instalmentReceipt.Ewi = Convert.ToInt32(cmd.Parameters["@pEwi"].Value.ToString());
                        instalmentReceipt.TotalDue = instalmentReceipt.NoOfInstalments * instalmentReceipt.Ewi;
                    }
                }
            }
            return instalmentReceipt;
        }

        public static void GeneratePFReceipt(PFReceipt pfReceipt)
        {
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GeneratePFReceipt", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = pfReceipt.LoanId;

                    cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pUserId"].Value = pfReceipt.UserId;

                    cmd.Parameters.Add("@pReceiptId", MySqlDbType.Int32);
                    cmd.Parameters["@pReceiptId"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    pfReceipt.ReceiptId = Convert.ToInt32(cmd.Parameters["@pReceiptId"].Value.ToString());
                }
            }
        }

        public static void GenerateInstalmentReceipt(InstalmentReceipt instalmentReceipt)
        {
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GenerateInstalmentReceipt", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = instalmentReceipt.LoanId;

                    cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfInstalments"].Value = instalmentReceipt.NoOfInstalments;

                    cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pUserId"].Value = instalmentReceipt.UserId;

                    cmd.Parameters.Add("@pReceiptId", MySqlDbType.Int32);
                    cmd.Parameters["@pReceiptId"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    instalmentReceipt.ReceiptId = Convert.ToInt32(cmd.Parameters["@pReceiptId"].Value.ToString());
                }
            }
        }

        public static List<EWIDue> GetEwiDue(int branchId)
        {
            EWIDue ewiDue;
            List<EWIDue> ewiDues = new List<EWIDue>();
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetEwiDue", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                    cmd.Parameters["@pBranchId"].Value = branchId;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ewiDue = new EWIDue();
                            ewiDue.LoanId = Convert.ToInt32(rdr["LoanId"].ToString());
                            ewiDue.BranchId = Convert.ToInt32(rdr["BranchId"].ToString());
                            ewiDue.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                            ewiDue.MemberName = rdr["MemberName"].ToString();
                            ewiDue.Phone = rdr["Phone"].ToString();
                            ewiDue.NoOfInstalments = Convert.ToInt32(rdr["NoOfInstalments"].ToString());
                            ewiDue.Ewi = Convert.ToInt32(rdr["Ewi"].ToString());
                            ewiDue.DueDate = rdr["DueDate"].ToString();
                            ewiDues.Add(ewiDue);
                        }
                    }
                }
            }
            return ewiDues;
        }

        public static List<CashReceiptStatement> GetCashReceiptStatement(string userId, string userType, DateTime fromDate, DateTime toDate)
        {
            List<CashReceiptStatement> statement = new List<CashReceiptStatement>();
            CashReceiptStatement statementRow;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                String qry;
                if (userType == "D" || userType == "A")
                {
                    qry = "SELECT R.UserId, U.UserName, R.Amount FROM AppUser U INNER JOIN "
                        + "( SELECT  UserId, SUM(ReceiptAmount) AS Amount FROM CashReceipt WHERE "
                        + "ReceiptDate BETWEEN @pFromDate AND @pToDate GROUP BY UserId)  R "
                        + "ON U.UserId = R.UserId";
                }
                else if (userType == "M")
                {
                    qry = "SELECT R.UserId, U.UserName, R.Amount FROM (SELECT * FROM AppUser WHERE UserId=@pUserId OR "
                        + "UserId IN(Select UserId FROM BranchUser WHERE BranchId IN"
                        + "(SELECT BranchId FROM Branch WHERE BranchManager=@pUserId))) U INNER JOIN "
                        + "(SELECT  UserId, SUM(ReceiptAmount) AS Amount FROM CashReceipt WHERE ReceiptDate BETWEEN @pFromDate AND @pToDate GROUP BY UserId)  R "
                        + "ON U.UserId = R.UserId";
                }
                else
                {
                    qry = "SELECT R.UserId, U.UserName, R.Amount FROM (SELECT * FROM AppUser WHERE UserId=@pUserId) U INNER JOIN "
                        + "( SELECT  UserId, SUM(ReceiptAmount) AS Amount FROM CashReceipt WHERE ReceiptDate BETWEEN @pFromDate AND @pToDate GROUP BY UserId)  R "
                        + "ON U.UserId = R.UserId";
                }
                using (MySqlCommand cmd = new MySqlCommand(qry, con))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@pFromDate", MySqlDbType.Date);
                    cmd.Parameters["@pFromDate"].Value = fromDate;
                    cmd.Parameters.Add("@pToDate", MySqlDbType.Date);
                    cmd.Parameters["@pToDate"].Value = toDate;
                    if (userType == "M" || userType == "C")
                    {
                        cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                        cmd.Parameters["@pUserId"].Value = userId;
                    }
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            statementRow = new CashReceiptStatement();
                            statementRow.UserId = rdr["UserId"].ToString();
                            statementRow.UserName = rdr["UserName"].ToString();
                            statementRow.Amount = Convert.ToInt32(rdr["Amount"].ToString());
                            statement.Add(statementRow);
                        }
                    }
                }
            }
            return statement;
        }
    }
}