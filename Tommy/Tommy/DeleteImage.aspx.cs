using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tommy.Model;
using Tommy.Model.DAL;

namespace Tommy
{
    public partial class DeleteImage : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        private FacebookUserDAL _facebookUserDAL;

        private FacebookUserDAL FaceBookUserDAL
        {
            get { return _facebookUserDAL ?? (_facebookUserDAL = new FacebookUserDAL()); }
        }

        protected int Id
        {
            get { return int.Parse(RouteData.Values["id"].ToString()); }
        }

        public string Code
        {
            get { return ((SiteMaster)this.Master).Code; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Code == null)
            {
                LoginStatus.Text = "Du måste vara inloggad genom Facebook för att ta bort en bild.";
                LoginStatus.CssClass = "fail";
                DeletionPanel.Visible = false;
            }
            else
            {
                var video = Service.GetImageDataByID(Id);
                var authorizeduser = DataExtensions.GetData(Code);


                if (video.imageid != null)
                {
                    if (authorizeduser.Id != video.userid)
                    {
                        var admin = FaceBookUserDAL.GetAdminData();
                        if (authorizeduser.Id != admin)
                        {
                            Response.RedirectToRoute("default");
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    Response.RedirectToRoute("default");
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void DeleteButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (Code != null)
                {
                    string path = Path.Combine(
                    AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");
                    var images = Service.GetImageDataByID(Id);

                    string file = Path.Combine(path, images.imagename);
                    File.Delete(file);
                    Service.DeleteImageData(Id);

                    Page.SetTempData("Message", "Bilden har tagits bort.");
                    Response.RedirectToRoute("uploadimage");
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Något fel uppstod då bilden skulle tas bort.");
            }
        }

    }
}