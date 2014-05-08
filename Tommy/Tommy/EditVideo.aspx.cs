using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tommy.Model;

namespace Tommy
{
    public partial class EditVideo : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Tommy.Model.Video EditVideoFormView_GetItem([RouteData]int id)
        {
            try
            {
                return Service.GetVideoDataByID(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då Bilannonsen hämtades vid redigering.");
                return null;
            }
        }

        public IEnumerable <VideoCategory> VideoCategoryDropDownList_GetData()
        {
            return Service.GetVideoCategory();
        }

        public void EditVideoFormView_UpdateItem(Video video)
        {
            var videocategoryID = 0;

            DropDownList dropdownList = (DropDownList)EditVideoFormView.FindControl("VideoCategoryDropDownList");
            foreach (ListItem item in dropdownList.Items)
            {
                if (item.Selected)
                {
                    videocategoryID = int.Parse(item.Value);

                }
            }
            try
            {
                //if (video == null)
                //{
                //    // The item wasn't found
                //    ModelState.AddModelError(String.Empty, String.Format("CarAd with id {0} was not found", video));
                //    return;
                //}

                if (TryUpdateModel(video))
                {
                    Service.UpdateVideo(video, videocategoryID);
                    Page.SetTempData("Message", "Bilannonsen har uppdaterats.");
                    Response.RedirectToRoute("uploadvideo");
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "");
            }
      
        }
    }
}