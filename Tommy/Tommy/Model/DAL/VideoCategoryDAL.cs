using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tommy.Model.DAL
{
    public class VideoCategoryDAL : DALBase
    {
        /// <summary>
        /// Returnerar en lista av video kategorier
        /// </summary>
        /// <returns>En lista med referenser till video kategorier</returns>
        public IEnumerable<VideoCategory> GetVideoCategory()
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.GetVideoCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    List<VideoCategory> categorys = new List<VideoCategory>(20);
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var videocategoryidindex = reader.GetOrdinal("videocategoryid");
                        var videocategorynameindex = reader.GetOrdinal("videocategoryname");

                        while (reader.Read())
                        {
                            categorys.Add(new VideoCategory
                            {
                                videocategoryid = reader.GetInt32(videocategoryidindex),
                                videocategoryname = reader.GetString(videocategorynameindex),
                            });
                        }
                    }
                    categorys.TrimExcess();

                    return categorys;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
    }
}