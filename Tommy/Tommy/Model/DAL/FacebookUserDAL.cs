using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tommy.Model.DAL
{
    public class FacebookUserDAL : DALBase
    {
        public void InsertUserData(string access_token, string userid, string name)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.InsertUserData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@access_token", SqlDbType.VarChar, 255).Value = access_token;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar, 100).Value = userid;

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public string GetUserData(string userid)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.GetUserData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@userid", SqlDbType.VarChar, 100).Value = userid;

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var useridIndex = reader.GetOrdinal("userid");
                            return userid = reader.GetString(useridIndex);
                        }
                        return null;
                    }
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public string GetAdminData()
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    string userid = null;

                    SqlCommand cmd = new SqlCommand("appSchema.GetAdminData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var useridIndex = reader.GetOrdinal("userid");
                            return userid = reader.GetString(useridIndex);
                        }
                        return null;
                    }
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }



    }
}