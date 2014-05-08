using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using Tommy.Model;
using ASPSnippets.FaceBookAPI;
using System.Web.Script.Serialization;
using Tommy.Model.DAL;

namespace Tommy
{
    public partial class UploadImage : System.Web.UI.Page
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

        public IEnumerable<ImageCategory> ImageCategoryDropDownList_GetData()
        {
            return Service.GetImageCategory();
        }

        private static string PhysicalUploadImagePath = Path.Combine(
                   AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");

        public static bool ImageExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadImagePath, name));
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
                ImageCategoryDropDownList.Visible = false;
                ImageListView.Visible = false;
                ImageTitleTextBox.Visible = false;
                HeaderLabel.Visible = false;

            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                if ((FileUpload.PostedFile.ContentType == "image/jpeg") ||
                    (FileUpload.PostedFile.ContentType == "image/png") ||
                    (FileUpload.PostedFile.ContentType == "image/bmp") ||
                    (FileUpload.PostedFile.ContentType == "image/gif"))
                {
                    if (Convert.ToInt64(FileUpload.PostedFile.ContentLength) < 10000000)
                    {
                        var fileName = FileUpload.FileName;

                        if (ImageExists(FileUpload.FileName))
                        {
                            var imageName = Path.GetFileNameWithoutExtension(fileName);
                            var imageExtension = Path.GetExtension(fileName);
                            int i = 1;

                            while (ImageExists(fileName))
                            {
                                fileName = String.Format("{0}({1}){2}", imageName, i++, imageExtension);
                            }
                        }

                        FileUpload.SaveAs(Path.Combine(PhysicalUploadImagePath, fileName));

                        LabelStatus.Text = "Filen " + fileName + " har laddats upp.";

                        string data = FaceBookConnect.Fetch(Code, "me");
                        FacebookUser faceBookUser = new JavaScriptSerializer().Deserialize<FacebookUser>(data);

                        var categoryIDs = 0;

                        foreach (ListItem bm in ImageCategoryDropDownList.Items)
                        {
                            if (bm.Selected)
                            {
                                categoryIDs = int.Parse(bm.Value);
                            }
                        }

                        var textboxContent = ImageTitleTextBox.Text;
                        Service.InsertImageData(fileName, faceBookUser.Id, categoryIDs, textboxContent);
                        Message = "Bilden har laddats upp.";
                        Response.RedirectToRoute("uploadimage");

                    }
                    else
                    {
                        LabelStatus.Text = "Filen måste vara mindre 10 MB.";
                        LabelStatus.CssClass = "fail";
                    }
                }
                else
                {
                    LabelStatus.Text = "Filen måste vara av formaten jpeg, jpg, png, bmp, or gif.";
                    LabelStatus.CssClass = "fail";
                }
            }
            else
            {
                LabelStatus.Text = "Ingen fil har valts.";
                LabelStatus.CssClass = "fail";
            }
        }

        //public void DisplayUploadedImages(string folder)
        //{
        //    string data = FaceBookConnect.Fetch(Code, "me");
        //    FacebookUser user = new JavaScriptSerializer().Deserialize<FacebookUser>(data);

        //    var userImageFiles = Service.GetUserImages(user.Id);
        //    string[] allImagesFiles = Directory.GetFiles(folder);
        //    IList<string> allImagesPaths = new List<string>();
        //    string fileName;

        //    FacebookUserDAL facebookUserDAL = new FacebookUserDAL();
        //    string adminId = facebookUserDAL.GetAdminData();

        //    if (user.Id == adminId)
        //    {
        //        foreach (var file in allImagesFiles)
        //        {
        //            fileName = Path.GetFileName(file);
        //            allImagesPaths.Add("Images//" + fileName);
        //        }
        //        ImagesRepeater.DataSource = allImagesPaths;
        //        ImagesRepeater.DataBind();
        //    }
        //    else
        //    {
        //        foreach (var file in userImageFiles)
        //        {
        //            fileName = Path.GetFileName(file.imagename);
        //            allImagesPaths.Add("Images//" + fileName);
        //        }

        //        ImagesRepeater.DataSource = allImagesPaths;
        //        ImagesRepeater.DataBind();
        //    }
        //}

        public IEnumerable<Tommy.Model.Image> ImageListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            string data = FaceBookConnect.Fetch(Code, "me");
            FacebookUser user = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
            FacebookUserDAL facebookUserDAL = new FacebookUserDAL();
            string adminId = facebookUserDAL.GetAdminData();

            if (user.Id == adminId)
            {
                return Service.GetImagesPageWise(maximumRows, startRowIndex, out totalRowCount);
            }
            return Service.GetMyImagesPageWiseByID(maximumRows, startRowIndex, out totalRowCount, user.Id);
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            bool deletionOccurs = false;

            foreach (ListViewItem item in ImageListView.Items)
            {
                CheckBox checkbox = item.FindControl("cbDelete") as CheckBox;

                if (checkbox.Checked)
                {
                    string fromImagesToExtension = checkbox.Attributes["special"];
                    string correctFileName = fromImagesToExtension.Replace("Images//", "");
                    string fromRootToHome = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString());
                    string fileToDelete = Path.Combine(fromRootToHome, fromImagesToExtension);
                    File.Delete(fileToDelete);
                    Service.DeleteImageData(correctFileName);


                    Message = "Bild har tagits bort.";
                    deletionOccurs = true;
                }
            }

            if (deletionOccurs)
            {
                Response.RedirectToRoute("uploadimage");
            }
            else
            {
                LabelStatus.Text = "Ingen fil har valts.";
                LabelStatus.CssClass = "fail";
            }
        }
    }
}
