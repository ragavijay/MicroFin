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
                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = groupPFReceipt.GroupId;
                    MySqlDataReader rdr =  cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        GroupPFReceiptInfo groupPFReceiptInfo = new GroupPFReceiptInfo();
                        groupPFReceiptInfo.ReceiptId = 0;
                        groupPFReceiptInfo.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                        groupPFReceiptInfo.MemberName = rdr["MemberName"].ToString();
                        groupPFReceiptInfo.LoanId = Convert.ToInt32(rdr["LoanId"].ToString());
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
                        cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                        cmd.Parameters["@pLoanId"].Value = groupPFReceiptInfo.LoanId;

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
                    cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                    cmd.Parameters["@pLoanId"].Value = instalmentReceipt.LoanId;

                    cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfInstalments"].Value = instalmentReceipt.NoOfInstalments;

                    cmd.Parameters.Add("@pActualReceiptDate", MySqlDbType.Date);
                    cmd.Parameters["@pActualReceiptDate"].Value = instalmentReceipt.ActualReceiptDate;

                    cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pUserId"].Value = instalmentReceipt.UserId;

                    cmd.Parameters.Add("@pReceiptId", MySqlDbType.Int32);
                    cmd.Parameters["@pReceiptId"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    instalmentReceipt.ReceiptId = Convert.ToInt32(cmd.Parameters["@pReceiptId"].Value.ToString());
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
                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = groupInstalmentReceipt.GroupId;
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        GroupInstalmentReceiptInfo groupInstalmentReceiptInfo = new GroupInstalmentReceiptInfo();
                        groupInstalmentReceiptInfo.ReceiptId = 0;
                        groupInstalmentReceiptInfo.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                        groupInstalmentReceiptInfo.MemberName = rdr["MemberName"].ToString();
                        groupInstalmentReceiptInfo.LoanId = Convert.ToInt32(rdr["LoanId"].ToString());
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
                        cmd.Parameters.Add("@pLoanId", MySqlDbType.Int32);
                        cmd.Parameters["@pLoanId"].Value = groupInstalmentReceiptInfo.LoanId;

                        cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                        cmd.Parameters["@pNoOfInstalments"].Value = groupInstalmentReceiptInfo.NoOfInstalments;

                        cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                        cmd.Parameters["@pUserId"].Value = userId;

                        cmd.Parameters.Add("@pActualReceiptDate", MySqlDbType.Date);
                        cmd.Parameters["@pActualReceiptDate"].Value = groupInstalmentReceipt.ActualReceiptDate;

                        cmd.Parameters.Add("@pReceiptId", MySqlDbType.Int32);
                        cmd.Parameters["@pReceiptId"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        groupInstalmentReceiptInfo.ReceiptId = Convert.ToInt32(cmd.Parameters["@pReceiptId"].Value.ToString());
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
                            statementRow.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                            statementRow.MemberName = rdr["MemberName"].ToString();
                            statementRow.LoanId = Convert.ToInt32(rdr["LoanId"].ToString());
                            statementRow.Description = rdr["Description"].ToString();
                            statementRow.Amount = Convert.ToInt32(rdr["Amount"].ToString());
                            statementList.Add(statementRow);
                        }
                    }
                }
            }
            return statementList;
        }

        public static GroupPFReceipt GetGroupPFReceipt(int groupId)
        {
            GroupPFReceipt groupPfReceipt = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetGroupPFStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = groupId;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                    
                    cmd.Parameters.Add("@pProcessingFee", MySqlDbType.Int32);
                    cmd.Parameters["@pProcessingFee"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pInsurance", MySqlDbType.Int32);
                    cmd.Parameters["@pInsurance"].Direction = ParameterDirection.Output;

                    
                    cmd.ExecuteNonQuery();
                    groupPfReceipt = new GroupPFReceipt();
                    groupPfReceipt.GroupId = groupId;
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

        public static GroupInstalmentReceipt GetGroupInstalmentReceipt(int groupId)
        {
            GroupInstalmentReceipt groupInstalmentReceipt = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetGroupInstalmentStatus", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = groupId;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;


                    cmd.Parameters.Add("@pNoOfInstalments", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfInstalments"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pEWI", MySqlDbType.Int32);
                    cmd.Parameters["@pEWI"].Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    groupInstalmentReceipt = new GroupInstalmentReceipt();
                    groupInstalmentReceipt.GroupId = groupId;
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