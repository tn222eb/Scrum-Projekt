using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using Tommy.Model.DAL;
using Tommy.Model;
using System.Web.Script.Serialization;
using ASPSnippets.FaceBookAPI;

namespace Tommy
{
    public partial class UploadVideo : System.Web.UI.Page
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

        private string Message
        {
            get
            {
                return Session["Message"] as string;
            }
            set
            {
                Session["Message"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Message != null)
            {
                SuccessLabel.Text = Message;
                SuccessMessage.Visible = true;
                Session.Remove("Message");
            }

            if (Code == null)
            {
                LoginStatus.Text = "Du måste vara inloggad genom Facebook för nå mina videoklipp.";
                LoginStatus.CssClass = "fail";
                FileUpload.Visible = false;
                UploadButton.Visible = false;
                VideoCategoryDropDownList.Visible = false;
                VideoListView.Visible = false;
                VideoTitleTextBox.Visible = false;
                HeaderLabel.Visible = false;
                InfoPanel.Visible = false;
                UploadBoxContainer.Visible = false;
                categori.Visible = false;
            }
        }

        public IEnumerable<VideoCategory> VideoCategoryDropDownList_GetData()
        {
            return Service.GetVideoCategory();
        }

        private static string PhysicalUploadVideoPath = Path.Combine(
                   AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Videos");

        public static bool VideoExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadVideoPath, name));
        }

        public bool IsValidVideo(FileUpload fileupload)
        {
            return fileupload.PostedFile.ContentType == "video/mp4" && fileupload.HasFile;
        }

        public bool IsValidContentLength(int contentlength)
        {
            return contentlength < 31457280;
        }

        public string GetFaceBookUserID()
        {
            string data = FaceBookConnect.Fetch(Code, "me");
            FacebookUser user = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
            return user.Id;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (FileUpload.FileName.Length <= 128)
                {
                    if (VideoTitleTextBox.Text == String.Empty)
                    {
                        ModelState.AddModelError(String.Empty, "Det måste finnas en video rubrik.");
                    }
                    else
                    {
                        if (IsValidVideo(FileUpload))
                        {
                            if (!IsValidContentLength((FileUpload.PostedFile.ContentLength)))
                            {
                                ModelState.AddModelError(String.Empty, "Filen måste vara mindre 30 mb.");
                            }
                            else
                            {
                                string script = "$(document).ready(function () { $('[id*=MainContent_UploadButton]').click(); });";
                                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                                string fileName = FileUpload.FileName;
                                string userId = GetFaceBookUserID();
                                string videoTitle = VideoTitleTextBox.Text;
                                var categoryID = 0;

                                if (VideoExists(FileUpload.FileName))
                                {
                                    var videoName = Path.GetFileNameWithoutExtension(fileName);
                                    var imageExtension = Path.GetExtension(fileName);
                                    int i = 1;

                                    while (VideoExists(fileName))
                                    {
                                        fileName = String.Format("{0}({1}){2}", videoName, i++, imageExtension);
                                    }
                                }

                                foreach (ListItem item in VideoCategoryDropDownList.Items)
                                {
                                    if (item.Selected)
                                    {
                                        categoryID = int.Parse(item.Value);
                                    }
                                }

                                FileUpload.SaveAs(Path.Combine(PhysicalUploadVideoPath, fileName));
                                Service.InsertVideoData(fileName, userId, categoryID, videoTitle);

                                Message = "Videoklippet har laddats upp.";
                                Response.RedirectToRoute("uploadvideo");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(String.Empty, "Fil måste ha valts och vara av formatet mp4.");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Filnamnet är för stort och måste vara mindre än 124 tecken.");
                }
            }
        }

        public IEnumerable<Tommy.Model.Video> VideoListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            string userId = GetFaceBookUserID();
            string adminId = Service.GetAdminData();

            if (userId == adminId)
            {
                return Service.GetVideosPageWise(maximumRows, startRowIndex, out totalRowCount);
            }

            return Service.GetMyVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, userId);
        }
    }
}