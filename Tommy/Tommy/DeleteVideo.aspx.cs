﻿using ASPSnippets.FaceBookAPI;
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
    public partial class DeleteVideo : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
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
                LoginStatus.Text = "Du måste vara inloggad genom Facebook för att ta bort ett videoklipp.";
                LoginStatus.CssClass = "fail";
                DeletionPanel.Visible = false;
            }
            else
            {
                var video = Service.GetVideoDataByID(Id);
                var authorizeduser = DataExtensions.GetData(Code);

                if (video.videoid != null)
                {
                    if (authorizeduser.Id != video.userid)
                    {
                        var admin = Service.GetAdminData();
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
                    AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Videos");
                    var video = Service.GetVideoDataByID(Id);

                    string file = Path.Combine(path, video.videoname);
                    File.Delete(file);
                    Service.DeleteVideoData(Id);

                    Page.SetTempData("Message", "Videoklippet har tagits bort.");
                    Response.RedirectToRoute("uploadvideo");
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Något fel uppstod då videoklippet skulle tas bort.");
            }
        }
    }
}



