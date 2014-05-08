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

        public VideoCategory GetCategoryByVideoID(int videoid)
        {
            // Skapar ett anslutningsobjekt.
            using (var connection = CreateConnection())
            {
                try
                {
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("appSchema.GetCategoryByVideoID", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@videoid", videoid);

                    VideoCategory videocategorys = new VideoCategory();

                    // Öppnar anslutningen till databasen.
                    connection.Open();

                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {

                        var videocategoryidIndex = reader.GetOrdinal("videocategoryid");
                        var videocategorynameIndex = reader.GetOrdinal("videocategoryname");


                        if (reader.Read())
                        {

                            return (new VideoCategory
                            {
                                videocategoryid = reader.GetInt32(videocategoryidIndex),
                                videocategoryname = reader.GetString(videocategorynameIndex)

                            });
                        }
                    }
                    return null;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }



    }
}