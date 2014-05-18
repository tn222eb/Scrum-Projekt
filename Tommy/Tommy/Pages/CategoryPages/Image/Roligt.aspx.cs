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

namespace Tommy.Pages.CategoryPages.Image
{
    public partial class Roligt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Tommy.Model.Image> ImageListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetImagesPageWiseByID(maximumRows, startRowIndex, out totalRowCount, 3);
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
    }
}