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
        /// <summary>
        /// Lagrar användarinformation i databasen
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="userid"></param>
        /// <param name="name"></param>
        public void InsertUserData(string userid, string name)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try

                {
                    SqlCommand cmd = new SqlCommand("appSchema.InsertUserData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

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

        /// <summary>
        /// Hämtar användardata från databasen
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>Returnerar vilken användare</returns>
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

        /// <summary>
        /// Hämtar hårdkodade adminid från databasen
        /// </summary>
        /// <returns>Returnerar adminid</returns>
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


        public IEnumerable<FacebookUser> GetNames()
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.GetNames", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    List<FacebookUser> names = new List<FacebookUser>();

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var useridindex = reader.GetOrdinal("userid");
                        var nameindex = reader.GetOrdinal("name");

                        while (reader.Read())
                        {
                            names.Add(new FacebookUser
                            {
                                Id = reader.GetString(useridindex),
                                Name = reader.GetString(nameindex),
                            });
                        }
                    }
                    names.TrimExcess();

                    return names;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }




    }
}