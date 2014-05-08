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
            routes.MapPageRoute("imagenaket", "bilder/naket", "~/Pages/CategoryPages/Image/Naket.aspx");
            routes.MapPageRoute("imagerandom", "bilder/random", "~/Pages/CategoryPages/Image/Random.aspx");
            routes.MapPageRoute("imageroligt", "bilder/roligt", "~/Pages/CategoryPages/Image/Roligt.aspx");
            routes.MapPageRoute("imagesport", "bilder/sport", "~/Pages/CategoryPages/Image/Sport.aspx");
            routes.MapPageRoute("videoroligt", "videoklipp/roligt", "~/Pages/CategoryPages/Video/Roligt.aspx");
            routes.MapPageRoute("videofilm", "videoklipp/film", "~/Pages/CategoryPages/Video/Film.aspx");
            routes.MapPageRoute("videomusik", "videoklipp/musik", "~/Pages/CategoryPages/Video/Musik.aspx");
            routes.MapPageRoute("videosport", "videoklipp/sport", "~/Pages/CategoryPages/Video/Sport.aspx");
            routes.MapPageRoute("videorandom", "videoklipp/random", "~/Pages/CategoryPages/Video/Random.aspx");
            routes.MapPageRoute("video", "videoklipp/alla", "~/Pages/Shared/Video.aspx");
            routes.MapPageRoute("image", "bilder/alla", "~/Pages/Shared/Image.aspx");
            routes.MapPageRoute("uploadvideo", "laddaupp/videoklipp", "~/UploadVideo.aspx");
            routes.MapPageRoute("uploadimage", "laddaupp/bilder", "~/UploadImage.aspx");
            routes.MapPageRoute("editvideo", "laddaupp/videoklipp/redigera/{id}", "~/EditVideo.aspx");
            routes.MapPageRoute("default", "", "~/Default.aspx");

        }
    }
}