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
        public static PFReceipt GetPFReceipt(string loanCode)
        {
            PFReceipt pfReceipt = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetLoanStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanCode", MySqlDbType.VarChar, 10);
                    cmd.Parameters["@pLoanCode"].Value = loanCode;

                    cmd.Parameters.Add("@pLoanStatus", MySqlDbType.VarChar, 1);
                    cmd.Parameters["@pLoanStatus"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pMemberCode", MySqlDbType.VarChar, 8);
                    cmd.Parameters["@pMemberCode"].Direction = ParameterDirection.Output;

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
                    pfReceipt.LoanCode = loanCode;
                    pfReceipt.LoanStatus = cmd.Parameters["@pLoanStatus"].Value.ToString();
                    if (pfReceipt.LoanStatus == "A")
                    {
                        pfReceipt.MemberCode = cmd.Parameters["@pMemberCode"].Value.ToString();
                        pfReceipt.MemberName = cmd.Parameters["@pMemberName"].Value.ToString();
                        pfReceipt.ProcessingFee = Convert.ToInt32(cmd.Parameters["@pProcessingFee"].Value.ToString());
                        pfReceipt.Insurance = Convert.ToInt32(cmd.Parameters["@pInsurance"].Value.ToString());
                        pfReceipt.TotalFee = pfReceipt.ProcessingFee + pfReceipt.Insurance;
                    }
                }
            }
            return pfReceipt;
        }


        public static InstalmentReceipt GetInstalmentReceipt(string loanCode)
        {
            InstalmentReceipt instalmentReceipt = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetLoanStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanCode", MySqlDbType.VarChar, 10);
                    cmd.Parameters["@pLoanCode"].Value = loanCode;

                    cmd.Parameters.Add("@pLoanStatus", MySqlDbType.VarChar, 1);
                    cmd.Parameters["@pLoanStatus"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pMemberCode", MySqlDbType.VarChar, 8);
                    cmd.Parameters["@pMemberCode"].Direction = ParameterDirection.Output;

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
                    instalmentReceipt.LoanCode = loanCode;
                    instalmentReceipt.LoanStatus = cmd.Parameters["@pLoanStatus"].Value.ToString();
                    if (instalmentReceipt.LoanStatus == "O")
                    {
                        instalmentReceipt.MemberCode = cmd.Parameters["@pMemberCode"].Value.ToString();
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
                    cmd.Parameters.Add("@pLoanCode", MySqlDbType.VarChar, 10);
                    cmd.Parameters["@pLoanCode"].Value = pfReceipt.LoanCode;

                    cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pUserId"].Value = pfReceipt.UserId;

                    cmd.Parameters.Add("@pActualReceiptDate", MySqlDbType.Date);
                    cmd.Parameters["@pActualReceiptDate"].Value = pfReceipt.ActualReceiptDate;

                    cmd.Parameters.Add("@pReceiptId", MySqlDbType.Int32);
                    cmd.Parameters["@pReceiptId"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    pfReceipt.ReceiptId = Convert.ToInt32(cmd.Parameters["@pReceiptId"].Value.ToString());
                }
            }
        }


        public static List<GroupPFReceiptInfo> GenerateGroupPFReceipt(GroupPFReceipt groupPFReceipt)
        {
            string userId = groupPFReceipt.UserId;
            List<GroupPFReceiptInfo> groupPFReceiptInfoList=new List<GroupPFReceiptInfo>();
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetGroupMembersLoan", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pGroupCode", MySqlDbType.VarChar, 6);
                    cmd.Parameters["@pGroupCode"].Value = groupPFReceipt.GroupCode;
                    MySqlDataReader rdr =  cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        GroupPFReceiptInfo groupPFReceiptInfo = new GroupPFReceiptInfo();
                        groupPFReceiptInfo.ReceiptId = 0;
                        groupPFReceiptInfo.MemberCode = rdr["MemberCode"].ToString();
                        groupPFReceiptInfo.MemberName = rdr["MemberName"].ToString();
                        groupPFReceiptInfo.LoanCode = rdr["LoanCode"].ToString();
                        groupPFReceiptInfo.ProcessingFee = Convert.ToInt32(rdr["ProcessingFee"].ToString());
                        groupPFReceiptInfo.Insurance = Convert.ToInt32(rdr["Insurance"].ToString());
                        groupPFReceiptInfo.TotalFee = groupPFReceiptInfo.ProcessingFee + groupPFReceiptInfo.Insurance;
                        groupPFReceiptInfoList.Add(groupPFReceiptInfo);
                    }
                }
            }
            foreach(GroupPFReceiptInfo groupPFReceiptInfo in groupPFReceiptInfoList) {
                using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GeneratePFReceipt", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pLoanCode", MySqlDbType.VarChar, 10);
                        cmd.Parameters["@pLoanCode"].Value = groupPFReceiptInfo.LoanCode;

                        cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                        cmd.Parameters["@pUserId"].Value = userId;

                        cmd.Parameters.Add("@pActualReceiptDate", MySqlDbType.Date);
                        cmd.Parameters["@pActualReceiptDate"].Value = groupPFReceipt.ActualReceiptDate;

                        cmd.Parameters.Add("@pReceiptId", MySqlDbType.Int32);
                        cmd.Parameters["@pReceiptId"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        groupPFReceiptInfo.ReceiptId = Convert.ToInt32(cmd.Parameters["@pReceiptId"].Value.ToString());
                    }
                }
            }
            return groupPFReceiptInfoList;
        }

        public static void GenerateInstalmentReceipt(InstalmentReceipt instalmentReceipt)
        {
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GenerateInstalmentReceipt", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pLoanCode", MySqlDbType.VarChar, 10);
                    cmd.Parameters["@pLoanCode"].Value = instalmentReceipt.LoanCode;

                    cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfInstalments"].Value = instalmentReceipt.NoOfInstalments;

                    cmd.Parameters.Add("@pActualReceiptDate", MySqlDbType.Date);
                    cmd.Parameters["@pActualReceiptDate"].Value = instalmentReceipt.ActualReceiptDate;

                    cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pUserId"].Value = instalmentReceipt.UserId;

                    cmd.Parameters.Add("@pReceiptId", MySqlDbType.Int32);
                    cmd.Parameters["@pReceiptId"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pNextDueDate", MySqlDbType.Date);
                    cmd.Parameters["@pNextDueDate"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    instalmentReceipt.ReceiptId = Convert.ToInt32(cmd.Parameters["@pReceiptId"].Value.ToString());
                    if (cmd.Parameters["@pNextDueDate"].Value == DBNull.Value)
                    {
                        instalmentReceipt.NextDueDate = new DateTime(2000,1,1);
                    }
                    else
                    {
                        instalmentReceipt.NextDueDate = Convert.ToDateTime(cmd.Parameters["@pNextDueDate"].Value.ToString());
                    }
                }
            }
        }

        public static List<GroupInstalmentReceiptInfo> GenerateGroupInstalmentReceipt(GroupInstalmentReceipt groupInstalmentReceipt)
        {
            string userId = groupInstalmentReceipt.UserId;
            List<GroupInstalmentReceiptInfo> groupInstalmentReceiptInfoList = new List<GroupInstalmentReceiptInfo>();
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetGroupMembersOngoingLoan", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pGroupCode", MySqlDbType.VarChar,9);
                    cmd.Parameters["@pGroupCode"].Value = groupInstalmentReceipt.GroupCode;
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        GroupInstalmentReceiptInfo groupInstalmentReceiptInfo = new GroupInstalmentReceiptInfo();
                        groupInstalmentReceiptInfo.ReceiptId = 0;
                        groupInstalmentReceiptInfo.MemberCode = rdr["MemberCode"].ToString();
                        groupInstalmentReceiptInfo.MemberName = rdr["MemberName"].ToString();
                        groupInstalmentReceiptInfo.LoanCode = rdr["LoanCode"].ToString();
                        groupInstalmentReceiptInfo.NoOfInstalments = groupInstalmentReceipt.NoOfInstalments;
                        groupInstalmentReceiptInfo.Ewi = groupInstalmentReceipt.Ewi;
                        groupInstalmentReceiptInfo.TotalDue = groupInstalmentReceipt.NoOfInstalments * groupInstalmentReceipt.Ewi;
                        groupInstalmentReceiptInfoList.Add(groupInstalmentReceiptInfo);
                    }
                }
            }
            foreach (GroupInstalmentReceiptInfo groupInstalmentReceiptInfo in groupInstalmentReceiptInfoList)
            {
                using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GenerateInstalmentReceipt", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pLoanCode", MySqlDbType.VarChar,13);
                        cmd.Parameters["@pLoanCode"].Value = groupInstalmentReceiptInfo.LoanCode;

                        cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                        cmd.Parameters["@pNoOfInstalments"].Value = groupInstalmentReceiptInfo.NoOfInstalments;

                        cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                        cmd.Parameters["@pUserId"].Value = userId;

                        cmd.Parameters.Add("@pActualReceiptDate", MySqlDbType.Date);
                        cmd.Parameters["@pActualReceiptDate"].Value = groupInstalmentReceipt.ActualReceiptDate;

                        cmd.Parameters.Add("@pReceiptId", MySqlDbType.Int32);
                        cmd.Parameters["@pReceiptId"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@pNextDueDate", MySqlDbType.Date);
                        cmd.Parameters["@pNextDueDate"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        groupInstalmentReceiptInfo.ReceiptId = Convert.ToInt32(cmd.Parameters["@pReceiptId"].Value.ToString());
                        if (cmd.Parameters["@pNextDueDate"].Value == DBNull.Value)
                        {
                            groupInstalmentReceiptInfo.NextDueDate = new DateTime(2000, 1, 1);
                        }
                        else
                        {
                            groupInstalmentReceiptInfo.NextDueDate = Convert.ToDateTime(cmd.Parameters["@pNextDueDate"].Value.ToString());
                        }
                    }
                }
            }
            return groupInstalmentReceiptInfoList;
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
                            ewiDue.LoanCode = rdr["LoanCode"].ToString();
                            ewiDue.BranchId = Convert.ToInt32(rdr["BranchId"].ToString());
                            ewiDue.MemberCode = rdr["MemberCode"].ToString();
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

        public static List<UserCashReceiptStatement> GetUserCashReceiptStatement(string userId, string userType, DateTime fromDate, DateTime toDate)
        {
            List<UserCashReceiptStatement> statement = new List<UserCashReceiptStatement>();
            UserCashReceiptStatement statementRow;
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
                            statementRow = new UserCashReceiptStatement();
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

        public static List<CashReceiptStatement> GetCashReceiptStatement(DateTime fromDate, DateTime toDate)
        {
            List<CashReceiptStatement> statementList = new List<CashReceiptStatement>();
            CashReceiptStatement statementRow;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetCashReceiptStatement", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pFromDate", MySqlDbType.Date);
                    cmd.Parameters["@pFromDate"].Value = fromDate;
                    cmd.Parameters.Add("@pToDate", MySqlDbType.Date);
                    cmd.Parameters["@pToDate"].Value = toDate;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (rdr.Read())
                        {
                            statementRow = new CashReceiptStatement();
                            statementRow.SNo = ++i;
                            statementRow.ReceiptId = Convert.ToInt32(rdr["ReceiptId"].ToString());
                            statementRow.MemberCode = rdr["MemberCode"].ToString();
                            statementRow.MemberName = rdr["MemberName"].ToString();
                            statementRow.LoanCode = rdr["LoanCode"].ToString();
                            statementRow.Description = rdr["Description"].ToString();
                            statementRow.Amount = Convert.ToInt32(rdr["Amount"].ToString());
                            statementRow.ActualReceiptDate = Convert.ToDateTime(rdr["ActualReceiptDate"].ToString());
                            statementList.Add(statementRow);
                        }
                    }
                }
            }
            return statementList;
        }

        public static GroupPFReceipt GetGroupPFReceipt(string groupCode)
        {
            GroupPFReceipt groupPfReceipt = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetGroupPFStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pGroupCode", MySqlDbType.VarChar, 6);
                    cmd.Parameters["@pGroupCode"].Value = groupCode;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                    
                    cmd.Parameters.Add("@pProcessingFee", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFee"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pInsurance", MySqlDbType.Int32);
                    cmd.Parameters["@pInsurance"].Direction = ParameterDirection.Output;

                    
                    cmd.ExecuteNonQuery();
                    groupPfReceipt = new GroupPFReceipt();
                    groupPfReceipt.GroupCode = groupCode;
                    groupPfReceipt.StatusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value.ToString());
                    if (groupPfReceipt.StatusCode == 1)
                    {

                        groupPfReceipt.ProcessingFee = Convert.ToInt32(cmd.Parameters["@pProcessingFee"].Value.ToString());
                        groupPfReceipt.Insurance = Convert.ToInt32(cmd.Parameters["@pInsurance"].Value.ToString());
                        groupPfReceipt.TotalFee = groupPfReceipt.ProcessingFee + groupPfReceipt.Insurance;
                    }
                }
            }
            return groupPfReceipt;
        }

        public static GroupInstalmentReceipt GetGroupInstalmentReceipt(string groupCode)
        {
            GroupInstalmentReceipt groupInstalmentReceipt = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetGroupInstalmentStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pGroupCode", MySqlDbType.VarChar, 6);
                    cmd.Parameters["@pGroupCode"].Value = groupCode;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;


                    cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfInstalments"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pEWI", MySqlDbType.Int32);
                    cmd.Parameters["@pEWI"].Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    groupInstalmentReceipt = new GroupInstalmentReceipt();
                    groupInstalmentReceipt.GroupCode = groupCode;
                    groupInstalmentReceipt.StatusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value.ToString());
                    if (groupInstalmentReceipt.StatusCode == 1)
                    {

                        groupInstalmentReceipt.NoOfInstalments = Convert.ToInt32(cmd.Parameters["@pNoOfInstalments"].Value.ToString());
                        groupInstalmentReceipt.Ewi = Convert.ToInt32(cmd.Parameters["@pEWI"].Value.ToString());
                        groupInstalmentReceipt.TotalDue = groupInstalmentReceipt.NoOfInstalments * groupInstalmentReceipt.Ewi;
                    }
                }
            }
            return groupInstalmentReceipt;
        }

    }
}