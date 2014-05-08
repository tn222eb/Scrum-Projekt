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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Message != null)
            {
                SuccessLabel.Visible = true;
                SuccessLabel.Text = Message;
                Session.Remove("Message");
            }

            if (Code == null)
            {
                LoginStatus.Text = "Du måste vara inloggad";
                LoginStatus.CssClass = "fail";
                FileUpload.Visible = false;
                UploadButton.Visible = false;
                DeleteButton.Visible = false;
                VideoCategoryDropDownList.Visible = false;
                VideoListView.Visible = false;
                VideoTitleTextBox.Visible = false;
                HeaderLabel.Visible = false;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile && VideoTitleTextBox.Text != String.Empty)
            {
                if ((FileUpload.PostedFile.ContentType == "video/mp4"))
                {
                    if (Convert.ToInt64(FileUpload.PostedFile.ContentLength) < 1000000000000)
                    {
                        var fileName = FileUpload.FileName;

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

                        FileUpload.SaveAs(Path.Combine(PhysicalUploadVideoPath, fileName));

                        LabelStatus.Text = "Filen " + fileName + " har laddats upp.";
                        string data = FaceBookConnect.Fetch(Code, "me");
                        FacebookUser user = new JavaScriptSerializer().Deserialize<FacebookUser>(data);

                        var categoryIDs = 0;

                        foreach (ListItem bm in VideoCategoryDropDownList.Items)
                        {
                            if (bm.Selected)
                            {
                                categoryIDs = int.Parse(bm.Value);

                            }
                        }

                        var textboxContent = VideoTitleTextBox.Text;
                        Service.InsertVideoData(fileName, user.Id, categoryIDs, textboxContent);
                        Message = "Bilden har laddats upp.";
                        Response.RedirectToRoute("uploadvideo");
                    }
                    else
                    {
                        LabelStatus.Text = "Filen måste vara mindre 1 GB.";
                        LabelStatus.CssClass = "fail";
                    }
                }

                else
                {
                    LabelStatus.Text = "Filen måste vara av formaten mp4 eller flv";
                    LabelStatus.CssClass = "fail";
                }
            }
            else
            {
                LabelStatus.Text = "Ingen fil har valts.";
                LabelStatus.CssClass = "fail";
            }
        }
   
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool deletionOccurs = false;

            foreach (ListViewItem item in VideoListView.Items)
            {
                CheckBox checkbox = item.FindControl("cbDelete") as CheckBox;

                if (checkbox.Checked)
                {
                    string fromPhotosToExtension = checkbox.Attributes["special"];
                    string correctFileName = fromPhotosToExtension.Replace("Videos//", "");
                    string fromRootToHome = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString());
                    string fileToDelete = Path.Combine(fromRootToHome, fromPhotosToExtension);
                    File.Delete(fileToDelete);
                    Service.DeleteVideoData(correctFileName);

                    Message = "Videoklipp har tagits bort.";
                    deletionOccurs = true;
                }
            }

            if (deletionOccurs)
            {
                Response.RedirectToRoute("uploadvideo");
            }
            else
            {
                LabelStatus.Text = "Ingen fil har valts.";
                LabelStatus.CssClass = "fail";
            }
        }

        public IEnumerable<Tommy.Model.Video> VideoListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            string data = FaceBookConnect.Fetch(Code, "me");
            FacebookUser user = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
            FacebookUserDAL facebookUserDAL = new FacebookUserDAL();
            string adminId = facebookUserDAL.GetAdminData();

            if (user.Id == adminId)
            {
                return Service.GetVideosPageWise(maximumRows, startRowIndex, out totalRowCount);
            }
            return Service.GetMyVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, user.Id);
        }
    }
}