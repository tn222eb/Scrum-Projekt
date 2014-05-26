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

namespace Tommy.Pages.CategoryPages.Video
{
    public partial class Roligt : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Tommy.Model.Video> VideoListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, 4);
        }

        protected void WindowButton_Command(object sender, CommandEventArgs e)
        {
            ListViewDataItem one = (ListViewDataItem)(sender as Control).NamingContainer;
            Label Window = (Label)one.FindControl("Window");
            Window.Visible = true;

            ListViewDataItem two = (ListViewDataItem)(sender as Control).NamingContainer;
            Label Close = (Label)two.FindControl("Close");
            Close.Visible = true;
        }

        protected void CommentCloseButton_Command(object sender, CommandEventArgs e)
        {
            ListViewDataItem two = (ListViewDataItem)(sender as Control).NamingContainer;
            Label Close = (Label)two.FindControl("Close");

            Close.Visible = false;
        }

        protected void VideoListView_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var label = e.Item.FindControl("UploadersName") as LinkButton;
            if (label != null)
            {
                // Typ omvandlar så att man kan använda nyckel
                var user = (Tommy.Model.Video)e.Item.DataItem;

                // Hämtar sedan namnen och väljer ut den som har samma ID
                var facebookUser = Service.GetNames()
                    .Single(c => c.Id == user.userid);

                // Skriver ut namnen
                label.Text = String.Format(label.Text, facebookUser.Name);
            }
        }

        protected void UploadersName_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(String.Format("http://www.facebook.com/{0}", e.CommandArgument));

        }
    }
}