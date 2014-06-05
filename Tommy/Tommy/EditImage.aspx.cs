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
    public partial class EditImage : System.Web.UI.Page
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
                LoginStatus.Text = "Du måste vara inloggad genom Facebook för att redigera en bild.";
                LoginStatus.CssClass = "fail";
                EditImageFormView.Visible = false;
            }

        }

        public Tommy.Model.Image EditImageFormView_GetItem([RouteData]int id)
        {
            try
            {
                var imageData = Service.GetImageDataByID(id);
                var FacebookUser = DataExtensions.GetData(Code);
                var admin = Service.GetAdminData();

                if (FacebookUser.Id == admin)
                {
                   return imageData;
                }


                if (FacebookUser.Id != imageData.userid)
                {
                    Response.RedirectToRoute("default");
                    Context.ApplicationInstance.CompleteRequest();
                }
                return imageData;
            }
            
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "");
                return null;
            }
        }

        public IEnumerable<ImageCategory> ImageCategoryDropDownList_GetData()
        {
            return Service.GetImageCategory();
        }

        public void EditImageFormView_UpdateItem(Tommy.Model.Image image)
        {
            if (IsValid)
            {
                try
                {
                    if (TryUpdateModel(image))
                    {
                        var categoryid = 0;

                        DropDownList dropdownList = (DropDownList)EditImageFormView.FindControl("ImageCategoryDropDownList");
                        foreach (ListItem item in dropdownList.Items)
                        {
                            if (item.Selected)
                            {
                                categoryid = int.Parse(item.Value);

                            }
                        }

                        Service.UpdateImage(image, categoryid);
                        Page.SetTempData("Message", "Bilden har uppdaterats.");
                        Response.RedirectToRoute("uploadimage");
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Något fel har uppstått då bilden skulle redigeras.");
                }

            }
        }
    }
}