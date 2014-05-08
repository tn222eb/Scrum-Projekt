using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Membership.OpenAuth;
using ASPSnippets.FaceBookAPI;

namespace Tommy
{
    internal static class AuthConfig
    {
        public static void RegisterOpenAuth()
        {
            FaceBookConnect.API_Key = "620141801393270";
            FaceBookConnect.API_Secret = "095b6f97cee8fc9efae480c16e27b298";
        }
    }
}