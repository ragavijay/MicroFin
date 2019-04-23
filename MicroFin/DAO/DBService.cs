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
    public class DBService
    {
        public static string GetUserType(Login login)
        {
            String userType = "";
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetUserType", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pUserId", MySqlDbType.VarChar, 20);
                    cmd.Parameters["@pUserId"].Value = login.UserId;
                    cmd.Parameters.Add("@pPasswd", MySqlDbType.VarChar, 16);
                    cmd.Parameters["@pPasswd"].Value = login.Passwd;
                    cmd.Parameters.Add("@ireturnvalue", MySqlDbType.VarChar, 1);
                    cmd.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        userType = rdr[0].ToString();
                    }
                }
            }
            return userType;
        }

        public static string GetBranchName(int branchId)
        {

            String branchName = "";
            using (MySqlConnection con = new MySqlConnection(WebApiApplication.conStr))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetBranchName", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pBranchId", MySqlDbType.Decimal, 5);
                    cmd.Parameters["@pBranchId"].Value = branchId;
                    cmd.Parameters.Add("@ireturnvalue", MySqlDbType.VarChar, 25);
                    cmd.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        branchName = rdr[0].ToString();
                    }
                }
            }
            return branchName;
        }
    }
}