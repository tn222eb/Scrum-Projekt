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
        /// <summary>
        /// Lägger till en bild i databasen
        /// </summary>
        /// <param name="imagename"></param>
        /// <param name="userid"></param>
        /// <param name="imagecategoryid"></param>
        /// <param name="imagetitle"></param>
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
                    cmd.Parameters.Add("@imagetitle", SqlDbType.VarChar, 35).Value = imagetitle;

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
        /// Tar bort en bild från databasen
        /// </summary>
        /// <param name="imagename"></param>
        public void DeleteImageData(int imageid)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.DeleteImageData", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@imageid", SqlDbType.Int, 4).Value = imageid;

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
        /// Hämtar en lista med användares bilder från databasen
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <param name="userid"></param>
        /// <returns>Returnerar en lista med referenser till användares bilders namn</returns>
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

        /// <summary>
        /// Hämtar alla bilder till en viss kategori
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <param name="imagecategoryid"></param>
        /// <returns>Returnerar en lista med referenser till kategori bilders namn</returns>
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

        /// <summary>
        /// Hämtar alla bilder från databasen
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <returns>Returnerar en lista med referenser till bilder</returns>
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

        /// <summary>
        /// Hämtar bildinformation genom bildens id
        /// </summary>
        /// <param name="imageid"></param>
        /// <returns>Returnerar information som bilden har</returns>
        public Image GetImageDataByID(int imageid)
        {

            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.GetImageDataByID", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@imageid", SqlDbType.Int, 4).Value = imageid;

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var imageidIndex = reader.GetOrdinal("imageid");
                            var imageameIndex = reader.GetOrdinal("imagename");
                            var useridIndex = reader.GetOrdinal("userid");
                            var imagecategoryIndex = reader.GetOrdinal("imagecategoryid");
                            var imagetitleIndex = reader.GetOrdinal("imagetitle");
                            var createddateIndex = reader.GetOrdinal("createddate");


                            return new Image
                            {
                                imageid = reader.GetInt32(imageidIndex),
                                imagename = reader.GetString(imageameIndex),
                                userid = reader.GetString(useridIndex),
                                imagecategoryid = reader.GetInt32(imagecategoryIndex),
                                imagetitle = reader.GetString(imagetitleIndex),
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

        /// <summary>
        /// Uppdaterar en bilds information
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imagecategoryid"></param>
        public void UpdateImage(Image image, int imagecategoryid)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.UpdateImage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@imageid", SqlDbType.Int, 4).Value = image.imageid;
                    cmd.Parameters.Add("@imagetitle", SqlDbType.VarChar, 35).Value = image.imagetitle;
                    cmd.Parameters.Add("@imagecategoryid", SqlDbType.Int, 4).Value = imagecategoryid;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }

        public List<Image> GetLatestImages()
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    var images = new List<Image>(100);

                    var cmd = new SqlCommand("appSchema.GetLatestImages", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var imageidIndex = reader.GetOrdinal("imageid");
                        var imageameIndex = reader.GetOrdinal("imagename");
                        var useridIndex = reader.GetOrdinal("userid");
                        var imagecategoryIndex = reader.GetOrdinal("imagecategoryid");
                        var imagetitleIndex = reader.GetOrdinal("imagetitle");
                        var createddateIndex = reader.GetOrdinal("createddate");

                        while (reader.Read())
                        {
                            images.Add(new Image
                            {
                                imageid = reader.GetInt32(imageidIndex),
                                imagename = reader.GetString(imageameIndex),
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



    }
}