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
    public partial class DeleteImage : System.Web.UI.Page
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
                AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");
                var id = int.Parse(e.CommandArgument.ToString());
                var images = service.GetImageDataByID(id);

                string file = Path.Combine(path, images.imagename);
                File.Delete(file);
                service.DeleteImageData(id);

                Page.SetTempData("Message", "Bilden har tagits bort");
                Response.RedirectToRoute("uploadimage");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "");
            }
        }

    }
}