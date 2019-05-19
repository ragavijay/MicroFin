﻿using System;
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
    public class MemberDBService
    {

        public static int AddMember(Member member)
        {
            int statusCode = 0;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddMember", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = member.GroupId;

                    cmd.Parameters.Add("@pMemberType", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberType"].Value = member.MemberType;

                    cmd.Parameters.Add("@pMemberName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pMemberName"].Value = member.MemberName;

                    cmd.Parameters.Add("@pGender", MySqlDbType.Int32);
                    cmd.Parameters["@pGender"].Value = member.Gender;

                    cmd.Parameters.Add("@pDOB", MySqlDbType.Date);
                    cmd.Parameters["@pDOB"].Value = member.DOB;

                    cmd.Parameters.Add("@pMaritalStatus", MySqlDbType.Int32);
                    cmd.Parameters["@pMaritalStatus"].Value = member.MaritalStatus;

                    cmd.Parameters.Add("@pReligion", MySqlDbType.Int32);
                    cmd.Parameters["@pReligion"].Value = member.Religion;

                    cmd.Parameters.Add("@pFName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pFName"].Value = member.FName;

                    cmd.Parameters.Add("@pHName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pHName"].Value = member.HName;

                    cmd.Parameters.Add("@pOccupation", MySqlDbType.Int32);
                    cmd.Parameters["@pOccupation"].Value = member.Occupation;

                    cmd.Parameters.Add("@pOccupationType", MySqlDbType.Int32);
                    cmd.Parameters["@pOccupationType"].Value = member.OccupationType;

                    cmd.Parameters.Add("@pAddressLine1", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pAddressLine1"].Value = member.AddressLine1;

                    cmd.Parameters.Add("@pAddressLine2", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pAddressLine2"].Value = member.AddressLine2;

                    cmd.Parameters.Add("@pAddressLine3", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pAddressLine3"].Value = member.AddressLine3;

                    cmd.Parameters.Add("@pAddressLine4", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pAddressLine4"].Value = member.AddressLine4;

                    cmd.Parameters.Add("@pTaluk", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pTaluk"].Value = member.Taluk;

                    cmd.Parameters.Add("@pPanchayat", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pPanchayat"].Value = member.Panchayat;

                    cmd.Parameters.Add("@pCity", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pCity"].Value = member.City;

                    cmd.Parameters.Add("@pPincode", MySqlDbType.VarChar, 6);
                    cmd.Parameters["@pPincode"].Value = member.Pincode;

                    cmd.Parameters.Add("@pNoOfYears", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfYears"].Value = member.NoOfYears;

                    cmd.Parameters.Add("@pHouseType", MySqlDbType.Int32);
                    cmd.Parameters["@pHouseType"].Value = member.HouseType;

                    cmd.Parameters.Add("@pPhone", MySqlDbType.VarChar, 10);
                    cmd.Parameters["@pPhone"].Value = member.Phone;

                    cmd.Parameters.Add("@pMemberAadharNumber", MySqlDbType.VarChar, 12);
                    cmd.Parameters["@pMemberAadharNumber"].Value = member.MemberAadharNumber;

                    cmd.Parameters.Add("@pPAN", MySqlDbType.VarChar, 10);
                    cmd.Parameters["@pPAN"].Value = member.PAN;

                    cmd.Parameters.Add("@pRationCardNo", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pRationCardNo"].Value = member.RationCardNo;

                    cmd.Parameters.Add("@pVoterIDNo", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pVoterIDNo"].Value = member.VoterIDNo;

                    cmd.Parameters.Add("@pAccountNumber", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pAccountNumber"].Value = member.AccountNumber;

                    cmd.Parameters.Add("@pIFSC", MySqlDbType.VarChar, 11);
                    cmd.Parameters["@pIFSC"].Value = member.IFSC;

                    cmd.Parameters.Add("@pNomineeName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pNomineeName"].Value = member.NomineeName;

                    cmd.Parameters.Add("@pRelationship", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pRelationship"].Value = member.Relationship;

                    cmd.Parameters.Add("@pNomineeAadharNumber", MySqlDbType.VarChar, 12);
                    cmd.Parameters["@pNomineeAadharNumber"].Value = member.NomineeAadharNumber;

                    cmd.Parameters.Add("@pNomineeDOB", MySqlDbType.Date);
                    cmd.Parameters["@pNomineeDOB"].Value = member.NomineeDOB;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value);
                    if (statusCode == 1)
                    {
                        int memberId = Convert.ToInt32(cmd.Parameters["@pMemberId"].Value);
                        member.MemberId = memberId;
                    }
                }
            }
            return statusCode;
        }

        public static List<Member> GetAllMembers(int groupId)
        {
            Member member;
            List<Member> members = new List<Member>();
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetAllMembers", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = groupId;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            member = new Member();
                            member.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                            member.MemberName = rdr["MemberName"].ToString();
                            member.GroupName = rdr["GroupName"].ToString();
                            member.CenterName = rdr["CenterName"].ToString();
                            member.LeaderName = rdr["LeaderName"].ToString();
                            members.Add(member);
                        }
                    }
                }
            }
            return members;
        }

        public static Member GetMember(int memberId)
        {
            Member member = null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetMember", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Value = memberId;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            member = new Member();
                            member.MemberId = Convert.ToInt32(rdr["MemberId"].ToString());
                            member.GroupId = Convert.ToInt32(rdr["GroupId"].ToString());
                            member.GroupName = rdr["GroupName"].ToString();
                            member.CenterName = rdr["CenterName"].ToString();
                            member.LeaderName = rdr["LeaderName"].ToString();
                            member.MemberType = (EMemberType)Convert.ToInt32(rdr["MemberType"].ToString());
                            member.MemberName = rdr["MemberName"].ToString();
                            member.Gender = (EGender)Convert.ToInt32(rdr["Gender"].ToString());
                            member.DOB = DateTime.Parse(rdr["DOB"].ToString());
                            member.MaritalStatus = (EMaritalStatus)Convert.ToInt32(rdr["MaritalStatus"].ToString());
                            member.Religion = (EReligion)Convert.ToInt32(rdr["Religion"].ToString());
                            member.FName = rdr["FName"].ToString();
                            member.HName = rdr["HName"].ToString();
                            member.Occupation = (EOccupation)Convert.ToInt32(rdr["Occupation"].ToString());
                            member.OccupationType = (EOccupationType)Convert.ToInt32(rdr["OccupationType"].ToString());
                            member.AddressLine1 = rdr["AddressLine1"].ToString();
                            member.AddressLine2 = rdr["AddressLine2"].ToString();
                            member.AddressLine3 = rdr["AddressLine3"].ToString();
                            member.AddressLine4 = rdr["AddressLine4"].ToString();
                            member.Taluk = rdr["Taluk"].ToString();
                            member.Panchayat = rdr["Panchayat"].ToString();
                            member.City = rdr["City"].ToString();
                            member.Pincode = rdr["Pincode"].ToString();
                            member.NoOfYears = Convert.ToInt32(rdr["NoOfYears"].ToString());
                            member.HouseType = (EHouseType)Convert.ToInt32(rdr["HouseType"].ToString());
                            member.Phone = rdr["Phone"].ToString();
                            member.MemberAadharNumber = rdr["MemberAadharNumber"].ToString();
                            member.PAN = rdr["PAN"].ToString();
                            member.RationCardNo = rdr["RationCardNo"].ToString();
                            member.VoterIDNo = rdr["VoterIdNo"].ToString();
                            member.AccountNumber = rdr["AccountNumber"].ToString();
                            member.RAccountNumber = member.AccountNumber;
                            member.IFSC = rdr["IFSC"].ToString();
                            member.NomineeName = rdr["NomineeName"].ToString();
                            member.Relationship = rdr["Relationship"].ToString();
                            member.NomineeAadharNumber = rdr["NomineeAadharNumber"].ToString();
                            member.NomineeDOB = DateTime.Parse(rdr["NomineeDOB"].ToString());
                        }
                    }
                }
            }
            return member;
        }

        public static int EditMember(Member member)
        {
            int statusCode = 0;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("EditMember", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Value = member.MemberId;

                    cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                    cmd.Parameters["@pGroupId"].Value = member.GroupId;

                    cmd.Parameters.Add("@pMemberType", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberType"].Value = member.MemberType;

                    cmd.Parameters.Add("@pMemberName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pMemberName"].Value = member.MemberName;

                    cmd.Parameters.Add("@pGender", MySqlDbType.Int32);
                    cmd.Parameters["@pGender"].Value = member.Gender;

                    cmd.Parameters.Add("@pDOB", MySqlDbType.Date);
                    cmd.Parameters["@pDOB"].Value = member.DOB;

                    cmd.Parameters.Add("@pMaritalStatus", MySqlDbType.Int32);
                    cmd.Parameters["@pMaritalStatus"].Value = member.MaritalStatus;

                    cmd.Parameters.Add("@pReligion", MySqlDbType.Int32);
                    cmd.Parameters["@pReligion"].Value = member.Religion;

                    cmd.Parameters.Add("@pFName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pFName"].Value = member.FName;

                    cmd.Parameters.Add("@pHName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pHName"].Value = member.HName;

                    cmd.Parameters.Add("@pOccupation", MySqlDbType.Int32);
                    cmd.Parameters["@pOccupation"].Value = member.Occupation;

                    cmd.Parameters.Add("@pOccupationType", MySqlDbType.Int32);
                    cmd.Parameters["@pOccupationType"].Value = member.OccupationType;

                    cmd.Parameters.Add("@pAddressLine1", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pAddressLine1"].Value = member.AddressLine1;

                    cmd.Parameters.Add("@pAddressLine2", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pAddressLine2"].Value = member.AddressLine2;

                    cmd.Parameters.Add("@pAddressLine3", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pAddressLine3"].Value = member.AddressLine3;

                    cmd.Parameters.Add("@pAddressLine4", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pAddressLine4"].Value = member.AddressLine4;

                    cmd.Parameters.Add("@pTaluk", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pTaluk"].Value = member.Taluk;

                    cmd.Parameters.Add("@pPanchayat", MySqlDbType.VarChar, 40);
                    cmd.Parameters["@pPanchayat"].Value = member.Panchayat;

                    cmd.Parameters.Add("@pCity", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pCity"].Value = member.City;

                    cmd.Parameters.Add("@pPincode", MySqlDbType.VarChar, 6);
                    cmd.Parameters["@pPincode"].Value = member.Pincode;

                    cmd.Parameters.Add("@pNoOfYears", MySqlDbType.Int32);
                    cmd.Parameters["@pNoOfYears"].Value = member.NoOfYears;

                    cmd.Parameters.Add("@pHouseType", MySqlDbType.Int32);
                    cmd.Parameters["@pHouseType"].Value = member.HouseType;

                    cmd.Parameters.Add("@pPhone", MySqlDbType.VarChar, 10);
                    cmd.Parameters["@pPhone"].Value = member.Phone;

                    cmd.Parameters.Add("@pMemberAadharNumber", MySqlDbType.VarChar, 12);
                    cmd.Parameters["@pMemberAadharNumber"].Value = member.MemberAadharNumber;

                    cmd.Parameters.Add("@pPAN", MySqlDbType.VarChar, 10);
                    cmd.Parameters["@pPAN"].Value = member.PAN;

                    cmd.Parameters.Add("@pRationCardNo", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pRationCardNo"].Value = member.RationCardNo;

                    cmd.Parameters.Add("@pVoterIDNo", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pVoterIDNo"].Value = member.VoterIDNo;

                    cmd.Parameters.Add("@pAccountNumber", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pAccountNumber"].Value = member.AccountNumber;

                    cmd.Parameters.Add("@pIFSC", MySqlDbType.VarChar, 11);
                    cmd.Parameters["@pIFSC"].Value = member.IFSC;

                    cmd.Parameters.Add("@pNomineeName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pNomineeName"].Value = member.NomineeName;

                    cmd.Parameters.Add("@pRelationship", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pRelationship"].Value = member.Relationship;

                    cmd.Parameters.Add("@pNomineeAadharNumber", MySqlDbType.VarChar, 12);
                    cmd.Parameters["@pNomineeAadharNumber"].Value = member.NomineeAadharNumber;

                    cmd.Parameters.Add("@pNomineeDOB", MySqlDbType.Date);
                    cmd.Parameters["@pNomineeDOB"].Value = member.NomineeDOB;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value);
                }
            }
            return statusCode;
        }

        //GetFamilyMembers
        public static List<FamilyMember> GetFamilyMembers(int memberId)
        {
            FamilyMember familyMember;
            List<FamilyMember> familyMembers = new List<FamilyMember>();
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetFamilyMembers", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Value = memberId;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            familyMember = new FamilyMember();
                            familyMember.MemberId = memberId;
                            familyMember.SNo = Convert.ToInt32(rdr["SNo"].ToString());
                            familyMember.FamilyMemberName = rdr["FamilyMemberName"].ToString();
                            familyMember.Relationship = rdr["Relationship"].ToString();
                            familyMember.OccupationType = (EOccupationType)Convert.ToInt32(rdr["OccupationType"].ToString());
                            familyMember.MonthlyIncome = Convert.ToInt32(rdr["MonthlyIncome"].ToString());
                            familyMember.Qualification = rdr["Qualification"].ToString();
                            familyMembers.Add(familyMember);
                        }
                    }
                }
            }
            return familyMembers;
        }

        public static int AddFamilyMember(FamilyMember familyMember)
        {
            int statusCode = 0;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddFamilyMember", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Value = familyMember.MemberId;

                    cmd.Parameters.Add("@pSNo", MySqlDbType.Int32);
                    cmd.Parameters["@pSNo"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@pFamilyMemberName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pFamilyMemberName"].Value = familyMember.FamilyMemberName;

                    cmd.Parameters.Add("@pRelationship", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pRelationship"].Value = familyMember.Relationship;

                    cmd.Parameters.Add("@pOccupationType", MySqlDbType.Int32);
                    cmd.Parameters["@pOccupationType"].Value = familyMember.OccupationType;

                    cmd.Parameters.Add("@pMonthlyIncome", MySqlDbType.Int32);
                    cmd.Parameters["@pMonthlyIncome"].Value = familyMember.MonthlyIncome;

                    cmd.Parameters.Add("@pQualification", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pQualification"].Value = familyMember.Qualification;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value);
                    if (statusCode == 1)
                    {
                        int sNo = Convert.ToInt32(cmd.Parameters["@pMemberId"].Value);
                        familyMember.SNo = sNo;
                    }
                }
            }
            return statusCode;
        }

        public static int EditFamilyMember(FamilyMember familyMember)
        {
            int statusCode = 0;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("EditFamilyMember", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Value = familyMember.MemberId;

                    cmd.Parameters.Add("@pSNo", MySqlDbType.Int32);
                    cmd.Parameters["@pSNo"].Value = familyMember.SNo;

                    cmd.Parameters.Add("@pFamilyMemberName", MySqlDbType.VarChar, 50);
                    cmd.Parameters["@pFamilyMemberName"].Value = familyMember.FamilyMemberName;

                    cmd.Parameters.Add("@pRelationship", MySqlDbType.VarChar, 30);
                    cmd.Parameters["@pRelationship"].Value = familyMember.Relationship;

                    cmd.Parameters.Add("@pOccupationType", MySqlDbType.Int32);
                    cmd.Parameters["@pOccupationType"].Value = familyMember.OccupationType;

                    cmd.Parameters.Add("@pMonthlyIncome", MySqlDbType.Int32);
                    cmd.Parameters["@pMonthlyIncome"].Value = familyMember.MonthlyIncome;

                    cmd.Parameters.Add("@pQualification", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pQualification"].Value = familyMember.Qualification;

                    cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                    cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value);
                    if (statusCode == 1)
                    {
                        int sNo = Convert.ToInt32(cmd.Parameters["@pMemberId"].Value);
                        familyMember.SNo = sNo;
                    }
                }
            }
            return statusCode;
        }

        public static FamilyMember GetFamilyMember(int memberId,int sNo)
        {
            FamilyMember familyMember=null;
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetFamilyMember", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pMemberId", MySqlDbType.Int32);
                    cmd.Parameters["@pMemberId"].Value = memberId;
                    cmd.Parameters.Add("@pSNo", MySqlDbType.Int32);
                    cmd.Parameters["@pSNo"].Value = sNo;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            familyMember = new FamilyMember();
                            familyMember.MemberId = memberId;
                            familyMember.SNo = Convert.ToInt32(rdr["SNo"].ToString());
                            familyMember.FamilyMemberName = rdr["FamilyMemberName"].ToString();
                            familyMember.Relationship = rdr["Relationship"].ToString();
                            familyMember.OccupationType = (EOccupationType)Convert.ToInt32(rdr["OccupationType"].ToString());
                            familyMember.MonthlyIncome = Convert.ToInt32(rdr["MonthlyIncome"].ToString());
                            familyMember.Qualification = rdr["Qualification"].ToString();
                        }
                    }
                }
            }
            return familyMember;
        }
    }
}