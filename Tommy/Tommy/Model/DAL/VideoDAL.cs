using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Tommy.Model.DAL;

namespace Tommy.Model.DAL
{
    public class VideoDAL : DALBase
    {
        public void InsertVideoData(string videoname, string userid, int videocategoryid, string videotitle)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.InsertVideoData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@videoname", SqlDbType.NVarChar, 128).Value = videoname;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar, 100).Value = userid;
                    cmd.Parameters.Add("@videocategoryid", SqlDbType.Int, 4).Value = videocategoryid;
                    cmd.Parameters.Add("@videotitle", SqlDbType.VarChar, 255).Value = videotitle;

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public void DeleteVideoData(string videoname)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.DeleteVideoData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@videoname", SqlDbType.NVarChar, 128).Value = videoname;

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public List<Video> GetUserVideos(string userid)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    var videos = new List<Video>(100);

                    var cmd = new SqlCommand("appSchema.GetUserVideos", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userid);
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var videonameIndex = reader.GetOrdinal("videoname");

                        while (reader.Read())
                        {
                            videos.Add(new Video
                            {
                                videoname = reader.GetString(videonameIndex)
                            });
                        }
                    }
                    videos.TrimExcess();

                    return videos;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public List<Video> GetCategoryVideos(int videocategoryid)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    var categoryVideos = new List<Video>(100);

                    var cmd = new SqlCommand("appSchema.GetCategoryVideos", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@videocategoryid", videocategoryid);
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var videonameIndex = reader.GetOrdinal("videoname");

                        while (reader.Read())
                        {
                            categoryVideos.Add(new Video
                            {
                                videoname = reader.GetString(videonameIndex)
                            });
                        }
                    }
                    categoryVideos.TrimExcess();

                    return categoryVideos;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public static IEnumerable<Video> GetMyVideosPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, string userid)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var videos = new List<Video>(100);

                    var cmd = new SqlCommand("appSchema.GetMyVideosPageWiseByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar, 100).Value = userid;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var videoidIndex = reader.GetOrdinal("videoid");
                        var videonameIndex = reader.GetOrdinal("videoname");
                        var useridIndex = reader.GetOrdinal("userid");
                        var videocategoryIndex = reader.GetOrdinal("videocategoryid");
                        var videotitleIndex = reader.GetOrdinal("videotitle");
                        var createddateIndex = reader.GetOrdinal("createddate");

                        while (reader.Read())
                        {
                            videos.Add(new Video
                            {
                                videoid = reader.GetInt32(videoidIndex),
                                videoname = reader.GetString(videonameIndex),
                                userid = reader.GetString(useridIndex),
                                videocategoryid = reader.GetInt32(videocategoryIndex),
                                videotitle= reader.GetString(videotitleIndex),
                                createddate = reader.GetDateTime(createddateIndex)
                            });
                        }
                    }

                    videos.TrimExcess();
                    return videos;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public static IEnumerable<Video> GetVideosPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, int videocategoryid)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var videos = new List<Video>(100);

                    var cmd = new SqlCommand("appSchema.GetVideosPageWiseByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@videocategoryid", SqlDbType.Int, 4).Value = videocategoryid;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var videoidIndex = reader.GetOrdinal("videoid");
                        var videonameIndex = reader.GetOrdinal("videoname");
                        var useridIndex = reader.GetOrdinal("userid");
                        var videocategoryIndex = reader.GetOrdinal("videocategoryid");
                        var videotitleIndex = reader.GetOrdinal("videotitle");
                        var createddateIndex = reader.GetOrdinal("createddate");

                        while (reader.Read())
                        {
                            videos.Add(new Video
                            {
                                videoid = reader.GetInt32(videoidIndex),
                                videoname = reader.GetString(videonameIndex),
                                userid = reader.GetString(useridIndex),
                                videocategoryid = reader.GetInt32(videocategoryIndex),
                                videotitle= reader.GetString(videotitleIndex),
                                createddate = reader.GetDateTime(createddateIndex)

                            });
                        }
                    }

                    videos.TrimExcess();
                    return videos;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public static IEnumerable<Video> GetVideosPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var videos = new List<Video>(100);

                    var cmd = new SqlCommand("appSchema.GetVideosPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var videoidIndex = reader.GetOrdinal("videoid");
                        var videonameIndex = reader.GetOrdinal("videoname");
                        var useridIndex = reader.GetOrdinal("userid");
                        var videocategoryIndex = reader.GetOrdinal("videocategoryid");
                        var videotitleIndex = reader.GetOrdinal("videotitle");
                        var createddateIndex = reader.GetOrdinal("createddate");

                        while (reader.Read())
                        {
                            videos.Add(new Video
                            {
                                videoid = reader.GetInt32(videoidIndex),
                                videoname = reader.GetString(videonameIndex),
                                userid = reader.GetString(useridIndex),
                                videocategoryid = reader.GetInt32(videocategoryIndex),
                                videotitle = reader.GetString(videotitleIndex),
                                createddate = reader.GetDateTime(createddateIndex)
                            });
                        }
                    }

                    videos.TrimExcess();
                    return videos;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public Video GetVideoDataByID(int videoid)
        {

            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.GetVideoDataByID", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@videoid", SqlDbType.Int, 4).Value = videoid;

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var videoidIndex = reader.GetOrdinal("videoid");
                            var videonameIndex = reader.GetOrdinal("videoname");
                            var useridIndex = reader.GetOrdinal("userid");
                            var videocategoryIndex = reader.GetOrdinal("videocategoryid");
                            var videotitleIndex = reader.GetOrdinal("videotitle");
                            var createddateIndex = reader.GetOrdinal("createddate");

                            return new Video
                            {
                                videoid = reader.GetInt32(videoidIndex),
                                videoname = reader.GetString(videonameIndex),
                                userid = reader.GetString(useridIndex),
                                videocategoryid = reader.GetInt32(videocategoryIndex),
                                videotitle = reader.GetString(videotitleIndex),
                                createddate = reader.GetDateTime(createddateIndex)
                            };
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

        public void UpdateVideo(Video video, int videocategoryid)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.UpdateVideo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@videoid", SqlDbType.Int, 4).Value = video.videoid;
                    cmd.Parameters.Add("@videotitle", SqlDbType.VarChar, 255).Value = video.videotitle;
                    cmd.Parameters.Add("@videocategoryid", SqlDbType.Int, 4).Value = videocategoryid;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }



    }
}