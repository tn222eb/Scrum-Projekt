using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Tommy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("imagemotor", "bilder/motor", "~/Pages/CategoryPages/Image/Motor.aspx");
            routes.MapPageRoute("imagerandom", "bilder/övrigt", "~/Pages/CategoryPages/Image/Random.aspx");
            routes.MapPageRoute("imageroligt", "bilder/roligt", "~/Pages/CategoryPages/Image/Roligt.aspx");
            routes.MapPageRoute("imagesport", "bilder/sport", "~/Pages/CategoryPages/Image/Sport.aspx");
            routes.MapPageRoute("videoroligt", "videoklipp/roligt", "~/Pages/CategoryPages/Video/Roligt.aspx");
            routes.MapPageRoute("videofilm", "videoklipp/film", "~/Pages/CategoryPages/Video/Film.aspx");
            routes.MapPageRoute("videomusik", "videoklipp/musik", "~/Pages/CategoryPages/Video/Musik.aspx");
            routes.MapPageRoute("videosport", "videoklipp/sport", "~/Pages/CategoryPages/Video/Sport.aspx");
            routes.MapPageRoute("videorandom", "videoklipp/övrigt", "~/Pages/CategoryPages/Video/Random.aspx");
            routes.MapPageRoute("video", "videoklipp/alla", "~/Pages/Shared/Video.aspx");
            routes.MapPageRoute("image", "bilder/alla", "~/Pages/Shared/Image.aspx");
            routes.MapPageRoute("uploadvideo", "minasidor/videoklipp", "~/UploadVideo.aspx");
            routes.MapPageRoute("uploadimage", "minasidor/bilder", "~/UploadImage.aspx");
            routes.MapPageRoute("editvideo", "minasidor/videoklipp/redigera/{id}", "~/EditVideo.aspx");
            routes.MapPageRoute("editimage", "minasidor/bilder/redigera/{id}", "~/EditImage.aspx");
            routes.MapPageRoute("deletevideo", "minasidor/videoklipp/radera/{id}", "~/DeleteVideo.aspx");
            routes.MapPageRoute("deleteimage", "minasidor/bilder/radera/{id}", "~/DeleteImage.aspx");
            routes.MapPageRoute("contactus", "kontaktaoss", "~/Kontakt.aspx");
            routes.MapPageRoute("default", "", "~/Default.aspx");
            routes.MapPageRoute("error", "serverfel", "~/Error.aspx");


        }
    }
}