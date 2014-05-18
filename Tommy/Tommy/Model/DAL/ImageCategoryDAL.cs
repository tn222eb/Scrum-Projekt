using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tommy.Model.DAL
{
    public class ImageCategoryDAL : DALBase
    {
        /// <summary>
        /// Returnerar en lista av bild kategorier
        /// </summary>
        /// <returns>En lista med referenser till bild kategorier</returns>
        public IEnumerable<ImageCategory> GetImageCategory()
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.GetImageCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    List<ImageCategory> categorys = new List<ImageCategory>(20);

                    connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var imagecategoryidindex = reader.GetOrdinal("imagecategoryid");
                        var imagecategorynameindex = reader.GetOrdinal("imagecategoryname");

                        while (reader.Read())
                        {
                            categorys.Add(new ImageCategory
                            {
                                imagecategoryid = reader.GetInt32(imagecategoryidindex),
                                imagecategoryname = reader.GetString(imagecategorynameindex),
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