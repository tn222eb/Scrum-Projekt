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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


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

        protected void Page_Load(object sender, EventArgs e)
        {
 
            if (Message != null)
            {
                SuccessMessage.Visible = true;
                SuccessLabel.Text = Message;
                Session.Remove("Message");
            }

            if (Code == null)
            {
                LoginStatus.Text = "Du måste vara inloggad genom Facebook för att nå mina bilder.";
                LoginStatus.CssClass = "fail";
                FileUpload.Visible = false;
                UploadButton.Visible = false;
                ImageCategoryDropDownList.Visible = false;
                ImageListView.Visible = false;
                ImageTitleTextBox.Visible = false;
                HeaderLabel.Visible = false;
                InfoPanel.Visible = false;
                UploadBoxContainer.Visible = false;
                categori.Visible = false;
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

        public bool IsValidImage(FileUpload fileupload)
        {
            return  fileupload.PostedFile.ContentType == "image/png" ||
                    fileupload.PostedFile.ContentType == "image/bmp" ||
                    fileupload.PostedFile.ContentType == "image/gif" ||
                    fileupload.PostedFile.ContentType == "image/jpeg" ||
                    fileupload.PostedFile.ContentType == "image/jpg" && fileupload.HasFile;
        }

        public bool IsValidContentLength(int contentlength)
        {
            return contentlength < 10485760;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (FileUpload.FileName.Length <= 128)
                {
                    if (ImageTitleTextBox.Text == String.Empty)
                    {
                        ModelState.AddModelError(String.Empty, "Måste finnas rubrik.");
                    }
                    else
                    {
                        if (IsValidImage(FileUpload))
                        {
                            if (!IsValidContentLength((FileUpload.PostedFile.ContentLength)))
                            {
                                ModelState.AddModelError(String.Empty, "Filen måste vara mindre 10 mb.");
                            }
                            else
                            {
                                string script = "$(document).ready(function () { $('[id*=MainContent_UploadButton]').click(); });";
                                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                                string fileName = FileUpload.FileName;
                                var user = DataExtensions.GetData(Code);
                                string imageTitle = ImageTitleTextBox.Text;
                                var categoryID = 0;

                                if (ImageExists(FileUpload.FileName))
                                {
                                    var videoName = Path.GetFileNameWithoutExtension(fileName);
                                    var imageExtension = Path.GetExtension(fileName);
                                    int i = 1;

                                    while (ImageExists(fileName))
                                    {
                                        fileName = String.Format("{0}({1}){2}", videoName, i++, imageExtension);
                                    }
                                }

                                foreach (ListItem item in ImageCategoryDropDownList.Items)
                                {
                                    if (item.Selected)
                                    {
                                        categoryID = int.Parse(item.Value);
                                    }
                                }

                                FileUpload.SaveAs(Path.Combine(PhysicalUploadImagePath, fileName));
                                Service.InsertImageData(fileName, user.Id, categoryID, imageTitle);

                                Message = "Bilden har laddats upp.";
                                Response.RedirectToRoute("uploadimage");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(String.Empty, "Fil måste ha valts och vara av formaten jpg, bmp, gif, png.");
                        }
                    }
                }
                else 
                {
                    ModelState.AddModelError(String.Empty, "Filnamnet är för stort och måste vara mindre än 123 tecken.");
                }
            }
        }

        public IEnumerable<Tommy.Model.Image> ImageListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            var user = DataExtensions.GetData(Code);
            string adminId = Service.GetAdminData();

            if (user.Id == adminId)
            {
                return Service.GetImagesPageWise(maximumRows, startRowIndex, out totalRowCount);
            }

            return Service.GetMyImagesPageWiseByID(maximumRows, startRowIndex, out totalRowCount, user.Id);
        }
    }
}
