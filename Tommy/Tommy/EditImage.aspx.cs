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
    public partial class EditImage : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Tommy.Model.Image EditImageFormView_GetItem([RouteData]int id)
        {
            try
            {
                return Service.GetImageDataByID(id);
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
                    Page.SetTempData("Message", image.imagetitle + " har uppdaterats.");
                    Response.RedirectToRoute("uploadimage");
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