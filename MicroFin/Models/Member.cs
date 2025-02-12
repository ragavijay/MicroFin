﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.IO;
namespace MicroFin.Models
{
    public class MemberInfo
    {
        public string MemberCode { get; set; }
        public string MemberName { get; set; }
    }
    public class Member
    {
        public string MemberCode { get; set; }
        public int MemberId { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int BranchId { get; set; }
        public string CenterCode { get; set; }

        public string CenterName { get; set; }
        public string LeaderName { get; set; }
        public EMemberType MemberType { get; set; }
        public string MemberName { get; set; }
        public EGender Gender { get; set; }
        public DateTime DOB { get; set; }
        public EMaritalStatus MaritalStatus { get; set; }
        public EReligion Religion { get; set; }
        public string FName { get; set; } // Father's name
        public string HName { get; set; } // Husband's name
        public EOccupation Occupation { get; set; }
        public EOccupationType OccupationType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Taluk { get; set; }
        public string Panchayat { get; set; }
        public string City{ get; set; }
        public string Pincode { get; set; }
        public int NoOfYears { get; set; }
        public EHouseType HouseType { get; set; }
        public EPropertyOwnership PropertyOwnership { get; set; }
        public string Phone { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; }
        public string MemberAadharNumber { get; set; }
        public string RMemberAadharNumber { get; set; }
        public string PAN { get; set; }
        public string RationCardNo { get; set; }
        public string VoterIDNo { get; set; }
        public string AccountNumber { get; set; }
        public string RAccountNumber { get; set; }
        public string IFSC { get; set; }
        public string BankCustomerId { get; set; }
        public string NomineeName { get; set; }
        public ERelationship Relationship { get; set; }
        public string NomineeAadharNumber { get; set; }
        public DateTime NomineeDOB { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public HttpPostedFileBase Aadhar { get; set; }
        public string CurrentLoanCode { get; set; }
        public static int GetAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
        public int GetMemberAge()
        {
            int age = 0;
            age = DateTime.Now.Year - DOB.Year;
            if (DateTime.Now.DayOfYear < DOB.DayOfYear)
                age = age - 1;

            return age;
        }
        public int GetNomineeAge()
        {
            int age = 0;
            age = DateTime.Now.Year - NomineeDOB.Year;
            if (DateTime.Now.DayOfYear < NomineeDOB.DayOfYear)
                age = age - 1;

            return age;
        }
        public string GetPhotoPath()
        {
            return @"http://fileuploads.amftn.in/Img/Member/" + MemberCode + ".jpg";
        }

        public static MemoryStream GetExportMembers(List<Member> members)
        {
            MemoryStream memory = new MemoryStream();
            StreamWriter stream = new StreamWriter(memory);
            foreach (Member member in members)
            {
                stream.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}",
                    member.MemberCode,
                    member.MemberName,
                    member.GetMemberAge() + "/" + member.DOB.ToString("dd-MM-yyyy"),
                    "M" + (int)member.MaritalStatus + 1,
                    member.Religion,
                    (member.Gender == EGender.Female ? "F" : "T"),
                    member.NomineeName,
                    member.GetNomineeAge() + "/" + member.NomineeDOB.ToString("dd-MM-yyyy"),
                    "K" + (int)member.Relationship+1,
                    member.Phone,
                    member.Aadhar,
                    member.VoterIDNo,
                    member.RationCardNo,
                    member.AddressLine1 + "," + member.AddressLine2 + "," + member.AddressLine3 + "," + member.AddressLine4 + "," + member.City + "," + member.Pincode,
                    member.AccountNumber,
                    member.IFSC,
                    member.Occupation)
                );
            }
            stream.Flush();
            memory.Position = 0;
            return memory;
        }
    }
}