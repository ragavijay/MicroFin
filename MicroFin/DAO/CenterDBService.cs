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
    public class CenterDBService
    {
        public static int AddGroupCenter(GroupCenter center)
        {
            MySqlConnection con = WebApiApplication.getConnection();
            int statusCode = 0;
            using (MySqlCommand cmd = new MySqlCommand("AddGroupCenter", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@pCenterId", MySqlDbType.Int32);
                cmd.Parameters["@pCenterId"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@pCenterName", MySqlDbType.VarChar, 40);
                cmd.Parameters["@pCenterName"].Value = center.CenterName;

                cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                cmd.Parameters["@pBranchId"].Value = center.BranchId;

                cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value);
                if (statusCode == 1)
                {
                    int centerId = Convert.ToInt32(cmd.Parameters["@pCenterId"].Value);
                    center.CenterId = centerId;
                }
            }
            return statusCode;
        }

        public static List<GroupCenter> GetAllGroupCenters(int branchId)
        {
            List<GroupCenter> centers = new List<GroupCenter>();
            using (MySqlConnection con = WebApiApplication.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("GetAllGroupCenters", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Int32);
                    cmd.Parameters["@pBranchId"].Value = branchId;
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        GroupCenter center = new GroupCenter();
                        center.CenterId = Convert.ToInt32(rdr["CenterId"].ToString());
                        center.CenterName = rdr["CenterName"].ToString();
                        centers.Add(center);
                    }
                }
            }
            return centers;
        }
        public static GroupCenter GetGroupCenter(int centerId)
        {
            GroupCenter center = null;
            MySqlConnection con = WebApiApplication.getConnection();
            using (MySqlCommand cmd = new MySqlCommand("GetGroupCenter", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@pCenterId", MySqlDbType.Int32);
                cmd.Parameters["@pCenterId"].Value = centerId;
                using(MySqlDataReader rdr = cmd.ExecuteReader()) {
                    if (rdr.Read())
                    {
                        center = new GroupCenter();
                        center.CenterId = Convert.ToInt32(rdr["CenterId"].ToString());
                        center.CenterName = rdr["CenterName"].ToString();
                    }
                }
            }
            return center;
        }

        public static int EditGroupCenter(GroupCenter center)
        {
            MySqlConnection con = WebApiApplication.getConnection();
            int statusCode = 0 ;
            using (MySqlCommand cmd = new MySqlCommand("EditGroupCenter", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@pCenterId", MySqlDbType.Int32);
                cmd.Parameters["@pCenterId"].Value = center.CenterId;

                cmd.Parameters.Add("@pCenterName", MySqlDbType.VarChar, 40);
                cmd.Parameters["@pCenterName"].Value = center.CenterName;

                cmd.Parameters.Add("@pStatusCode", MySqlDbType.Int32);
                cmd.Parameters["@pStatusCode"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                statusCode = Convert.ToInt32(cmd.Parameters["@pStatusCode"].Value.ToString());
            }
            return statusCode;
        }

        public static List<GroupCenter> GetAllGroupCentersByPattern(String centerNamePattern)
        {
            List<GroupCenter> centers = new List<GroupCenter>();
            MySqlConnection con = WebApiApplication.getConnection();
            using (MySqlCommand cmd = new MySqlCommand("GetAllGroupCentersByPattern", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@pCenterNamePattern", MySqlDbType.VarChar, 40);
                cmd.Parameters["@pCenterNamePattern"].Value = centerNamePattern;
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        GroupCenter center = new GroupCenter();
                        center.CenterId = Convert.ToInt32(rdr["CenterId"].ToString());
                        center.CenterName = rdr["CenterName"].ToString();
                        centers.Add(center);
                    }
                }
            }
            return centers;
        }
    }
}