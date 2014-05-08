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

namespace Tommy.Pages.Shared
{
    public partial class Video : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Tommy.Model.Video> VideoListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetVideosPageWise(maximumRows, startRowIndex, out totalRowCount);
        }
    }
}