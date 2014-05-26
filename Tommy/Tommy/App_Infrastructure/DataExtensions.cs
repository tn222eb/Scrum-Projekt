using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Tommy.Model;

namespace Tommy
{
    public static class DataExtensions
    {
        /// <summary>
        /// Metod som cachar användarens facebook data
        /// </summary>
        /// <param name="code"></param>
        /// <param name="refresh"></param>
        /// <returns>Returnerar ett facebook-objekt med användarens data</returns>
        public static FacebookUser GetData(string code, bool refresh = false)
        {
            var faceBookUser = HttpContext.Current.Cache["FacebookData"] as FacebookUser;

            // Kollar om det finns något cachat
            if (faceBookUser == null || refresh)
            {
                // Hämtar och lagrar facebook-datan om användaren i cache
                string data = FaceBookConnect.Fetch(code, "me");
                faceBookUser = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
                HttpContext.Current.Cache.Insert("FacebookData", faceBookUser, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            }

            return faceBookUser;
        }


    }
}