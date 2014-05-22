using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tommy.Model;

namespace Tommy
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Tommy.Model.Image> ImageListView_GetData()
        {
            return Service.GetLatestImages();
        }

       
        public IEnumerable<Tommy.Model.Video> VideoListView_GetData()
        {
            return Service.GetLatestVideos();
        }
    }
}