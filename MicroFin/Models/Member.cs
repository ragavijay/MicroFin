using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroFin.Models
{
    public class MemberInfo
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
    }
    public class Member
    {
        public int MemberId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
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
        public string NomineeName { get; set; }
        public string Relationship { get; set; }
        public string NomineeAadharNumber { get; set; }
        public DateTime NomineeDOB { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public HttpPostedFileBase Aadhar { get; set; }
        public int CurrentLoanId { get; set; }
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
            return @"http://fileuploads.amftn.in/Img/Member/" + MemberId + ".jpg";
        }
    }
}