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
        /// Metod som hämtar facebook data
        /// </summary>
        /// <param name="code"></param>
        /// <param name="refresh"></param>
        /// <returns>Returnerar ett facebook-objekt med användarens data</returns>
        public static FacebookUser GetData(string code)
        {

            string data = FaceBookConnect.Fetch(code, "me");
            var faceBookUser = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
            return faceBookUser;
        }
    }
}