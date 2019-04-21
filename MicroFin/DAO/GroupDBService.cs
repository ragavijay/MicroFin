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
    public class GroupDBService
    {
        public static int AddMemberGroup(MemberGroup group)
        {
            MySqlConnection con = WebApiApplication.getConnection();
            int statusCode = 0;
            using (MySqlCommand cmd = new MySqlCommand("AddMemberGroup", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
                cmd.Parameters["@pGroupId"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@pGroupName", MySqlDbType.VarChar, 40);
                cmd.Parameters["@pGroupName"].Value = group.GroupName;

                cmd.Parameters.Add("@pCenterId", MySqlDbType.Int32);
                cmd.Parameters["@pCenterId"].Value = group.CenterId;

                cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value);
                if (statusCode == 1)
                {
                    int groupId = Convert.ToInt32(cmd.Parameters["@pGroupId"].Value);
                    group.GroupId = groupId;
                }
            }
            return statusCode;
        }

        public static List<MemberGroup> GetAllMemberGroups(int branchId)
        {
            MemberGroup group;
            List<MemberGroup> groups = new List<MemberGroup>();
            MySqlConnection con = WebApiApplication.getConnection();
            MySqlCommand cmd = new MySqlCommand("GetAllMemberGroups", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
            cmd.Parameters["@pBranchId"].Value = branchId;
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                group = new MemberGroup();
                group.GroupId = Convert.ToInt32(rdr["GroupId"].ToString());
                group.GroupName = rdr["GroupName"].ToString();
                group.CenterId = Convert.ToInt32(rdr["CenterId"].ToString());
                group.CenterName = rdr["CenterName"].ToString();
                groups.Add(group);
            }
            rdr.Close();
            cmd.Dispose();
            return groups;
        }
        public static MemberGroup GetMemberGroup(int groupId)
        {
            MemberGroup group = null;
            string conStr = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("GetMemberGroup", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
            cmd.Parameters["@pGroupId"].Value = groupId;
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                group = new MemberGroup();
                group.GroupId = Convert.ToInt32(rdr["GroupId"].ToString());
                group.GroupName = rdr["GroupName"].ToString();
                group.CenterId = Convert.ToInt32(rdr["CenterId"].ToString());
                group.CenterName = rdr["CenterName"].ToString();
            }
            rdr.Close();
            cmd.Dispose();
            con.Close();
            return group;
        }

        public static int EditMemberGroup(MemberGroup group)
        {
            MySqlConnection con = WebApiApplication.getConnection();
            MySqlCommand cmd = new MySqlCommand("EditMemberGroup", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@pGroupId", MySqlDbType.Int32);
            cmd.Parameters["@pGroupId"].Value = group.GroupId;

            cmd.Parameters.Add("@pGroupName", MySqlDbType.VarChar, 40);
            cmd.Parameters["@pGroupName"].Value = group.GroupName;

            cmd.Parameters.Add("@pCenterId", MySqlDbType.Int32);
            cmd.Parameters["@pCenterId"].Value = group.CenterId;

            cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
            cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            int statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value.ToString());
            cmd.Dispose();
            return statusCode;
        }

        public static List<MemberGroup> GetAllMemberGroupsByPattern(String groupNamePattern)
        {
            List<MemberGroup> groups = new List<MemberGroup>();
            MySqlConnection con = WebApiApplication.getConnection();
            using (MySqlCommand cmd = new MySqlCommand("GetAllMemberGroupsByPattern", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@pGroupNamePattern", MySqlDbType.VarChar, 40);
                cmd.Parameters["@pGroupNamePattern"].Value = groupNamePattern;
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        MemberGroup group = new MemberGroup();
                        group.GroupId = Convert.ToInt32(rdr["GroupId"].ToString());
                        group.GroupName = rdr["GroupName"].ToString();
                        group.CenterName = rdr["CenterName"].ToString();
                        group.LeaderName = rdr["LeaderName"].ToString();
                        groups.Add(group);
                    }
                }
            }
            return groups;
        }
    }
}