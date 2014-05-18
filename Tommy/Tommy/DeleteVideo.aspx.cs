using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tommy.Model;

namespace Tommy
{
    public partial class DeleteVideo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DeleteButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                Service service = new Service();

                string path = Path.Combine(
                AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Videos");
                var id = int.Parse(e.CommandArgument.ToString());
                var video = service.GetVideoDataByID(id);

                string file = Path.Combine(path, video.videoname);
                File.Delete(file);
                service.DeleteVideoData(id);

                Page.SetTempData("Message", "Videoklippet har tagits bort");
                Response.RedirectToRoute("uploadvideo");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "");
            }
        }
    }
}



