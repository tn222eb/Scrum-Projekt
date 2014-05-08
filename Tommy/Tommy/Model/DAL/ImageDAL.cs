using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Tommy.Model.DAL;

namespace Tommy.Model
{
    public class ImageDAL : DALBase
    {
        public void InsertImageData(string imagename, string userid, int imagecategoryid, string imagetitle)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.InsertImageData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@imagename", SqlDbType.NVarChar, 128).Value = imagename;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar, 100).Value = userid;
                    cmd.Parameters.Add("@imagecategoryid", SqlDbType.Int, 4).Value = imagecategoryid;
                    cmd.Parameters.Add("@imagetitle", SqlDbType.VarChar, 255).Value = imagetitle;

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public void DeleteImageData(string imagename)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.DeleteImageData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@imagename", SqlDbType.NVarChar, 128).Value = imagename;

                    connection.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public List<Image> GetUserImages(string userid)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    var images = new List<Image>(100);

                    var cmd = new SqlCommand("appSchema.GetUserImages", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userid);
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var imagenameIndex = reader.GetOrdinal("imagename");

                        while (reader.Read())
                        {
                            images.Add(new Image
                            {
                                imagename = reader.GetString(imagenameIndex)
                            });
                        }
                    }
                    images.TrimExcess();

                    return images;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public List<Image> GetCategoryImages(int imagecategoryid)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    var categoryImages = new List<Image>(100);

                    var cmd = new SqlCommand("appSchema.GetCategoryImages", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@imagecategoryid", imagecategoryid);
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var imagenameIndex = reader.GetOrdinal("imagename");

                        while (reader.Read())
                        {
                            categoryImages.Add(new Image
                            {
                                imagename = reader.GetString(imagenameIndex)
                            });
                        }
                    }
                    categoryImages.TrimExcess();

                    return categoryImages;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public static IEnumerable<Image> GetImagesPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var images = new List<Image>(100);

                    var cmd = new SqlCommand("appSchema.GetImagesPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var imageidIndex = reader.GetOrdinal("imageid");
                        var imagenameIndex = reader.GetOrdinal("imagename");
                        var useridIndex = reader.GetOrdinal("userid");
                        var imagecategoryIndex = reader.GetOrdinal("imagecategoryid");
                        var imagetitleIndex = reader.GetOrdinal("imagetitle");
                        var createddateIndex = reader.GetOrdinal("createddate");

                        while (reader.Read())
                        {
                            images.Add(new Image
                            {
                                imageid = reader.GetInt32(imageidIndex),
                                imagename = reader.GetString(imagenameIndex),
                                userid = reader.GetString(useridIndex),
                                imagecategoryid = reader.GetInt32(imagecategoryIndex),
                                imagetitle = reader.GetString(imagetitleIndex),
                                createddate = reader.GetDateTime(createddateIndex)
                            });
                        }
                    }

                    images.TrimExcess();
                    return images;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public static IEnumerable<Image> GetImagesPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, int imagecategoryid)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var images = new List<Image>(100);

                    var cmd = new SqlCommand("appSchema.GetImagesPageWiseByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@imagecategoryid", SqlDbType.Int, 4).Value = imagecategoryid;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var imageidIndex = reader.GetOrdinal("imageid");
                        var imagenameIndex = reader.GetOrdinal("imagename");
                        var useridIndex = reader.GetOrdinal("userid");
                        var imagecategoryIndex = reader.GetOrdinal("imagecategoryid");
                        var imagetitleIndex = reader.GetOrdinal("imagetitle");
                        var createddateIndex = reader.GetOrdinal("createddate");

                        while (reader.Read())
                        {
                            images.Add(new Image
                            {
                                imageid = reader.GetInt32(imageidIndex),
                                imagename = reader.GetString(imagenameIndex),
                                userid = reader.GetString(useridIndex),
                                imagecategoryid = reader.GetInt32(imagecategoryIndex),
                                imagetitle = reader.GetString(imagetitleIndex),
                                createddate = reader.GetDateTime(createddateIndex)
                            });
                        }
                    }

                    images.TrimExcess();
                    return images;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public static IEnumerable<Image> GetMyImagesPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, string userid)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var images = new List<Image>(100);

                    var cmd = new SqlCommand("appSchema.GetMyImagesPageWiseByID", conn);
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
                        var imageidIndex = reader.GetOrdinal("imageid");
                        var imagenameIndex = reader.GetOrdinal("imagename");
                        var useridIndex = reader.GetOrdinal("userid");
                        var imagecategoryIndex = reader.GetOrdinal("imagecategoryid");
                        var imagetitleIndex = reader.GetOrdinal("imagetitle");
                        var createddateIndex = reader.GetOrdinal("createddate");

                        while (reader.Read())
                        {
                            images.Add(new Image
                            {
                                imageid = reader.GetInt32(imageidIndex),
                                imagename = reader.GetString(imagenameIndex),
                                userid = reader.GetString(useridIndex),
                                imagecategoryid = reader.GetInt32(imagecategoryIndex),
                                imagetitle= reader.GetString(imagetitleIndex),
                                createddate = reader.GetDateTime(createddateIndex)
                            });
                        }
                    }

                    images.TrimExcess();
                    return images;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
    }
}