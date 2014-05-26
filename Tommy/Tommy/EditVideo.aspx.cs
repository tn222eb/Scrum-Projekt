using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tommy.Model;
using Tommy.Model.DAL;

namespace Tommy
{
    public partial class EditVideo : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        public string Code
        {
            get { return ((SiteMaster)this.Master).Code; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Code == null)
            {
                LoginStatus.Text = "Du måste vara inloggad genom Facebook för att redigera ett videoklipp.";
                LoginStatus.CssClass = "fail";
                EditVideoFormView.Visible = false;
            }

        }

        public string GetFaceBookUserID()
        {
            string data = FaceBookConnect.Fetch(Code, "me");
            FacebookUser user = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
            return user.Id;
        }

        public Tommy.Model.Video EditVideoFormView_GetItem([RouteData]int id)
        {
            try
            {
                var videoData = Service.GetVideoDataByID(id);
                var FacebookUser = GetFaceBookUserID();

                var admin = Service.GetAdminData();

                if (FacebookUser == admin)
                {
                    return videoData;
                }

                if (videoData.userid != FacebookUser)
                {
                    Response.RedirectToRoute("default");
                    Context.ApplicationInstance.CompleteRequest();
                }
                return videoData;
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "");
                return null;
            }
        }
        public IEnumerable <VideoCategory> VideoCategoryDropDownList_GetData()
        {
            return Service.GetVideoCategory();
        }

        public void EditVideoFormView_UpdateItem(Video video)
        {
            if (IsValid)
            {
                try
                {
                    if (TryUpdateModel(video))
                    {
                        var categoryID = 0;

                        DropDownList dropdownList = (DropDownList)EditVideoFormView.FindControl("VideoCategoryDropDownList");
                        foreach (ListItem item in dropdownList.Items)
                        {
                            if (item.Selected)
                            {
                                categoryID = int.Parse(item.Value);

                            }
                        } 

                        Service.UpdateVideo(video, categoryID);
                        Page.SetTempData("Message", "Videoklippet har uppdaterats.");
                        Response.RedirectToRoute("uploadvideo");
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Något fel har uppstått då videoklippet skulle redigeras.");
                }

            }
        }
    }
}