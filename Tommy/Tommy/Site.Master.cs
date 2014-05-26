using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook.Reflection;
using ASPSnippets.FaceBookAPI;
using Tommy.Model;
using Tommy.Model.DAL;
using System.Web.Script.Serialization;

namespace Tommy
{
    public partial class SiteMaster : MasterPage
    {
        private static string code;

        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        public string Code
        {
            get { return code; }
        }


        /// <summary>
        /// Lagrar Facebook access-token i cookies för att man ska kunna förflytta sig mellan sidor och se att man är inloggad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Init(object sender, EventArgs e)
        {
            if (code == null)
            {
                code = Request.QueryString["code"];

                Page.ViewStateUserKey = code;

            }

            else
            {
                Page.ViewStateUserKey = code;
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        /// <summary>
        /// Den kollar om man är inloggad varje gång man kör samma sida eller en annan sida. Ifall man inloggad så presenteras användarens Facebook-data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            AuthConfig.RegisterOpenAuth();

            if (Request.QueryString["error"] == "access_denied")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                return;
            }

            if (Request.QueryString["logout"] == "true")
            {
                code = null;
                return;
            }

            if (!string.IsNullOrEmpty(code))
            {
                var faceBookUser = GetFaceBookUserData();

                ShowAuthentication(faceBookUser);
                IsNewUser(faceBookUser);  
            }

        }

        public void IsNewUser(FacebookUser faceBookUser)
        {
            var comparetoId = Service.GetUserData(faceBookUser.Id);

            if (comparetoId != faceBookUser.Id)
            {
                Service.InsertUserData(faceBookUser.Id, faceBookUser.Name);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Har delvis tagit kod från ASPSnippets och anpassat så det fungerar för min kod
        protected void Login(object sender, EventArgs e)
        {
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);

        }

        protected void Logout(object sender, EventArgs e)
        {
            FaceBookConnect.Logout(Request.QueryString["code"]);

            Response.Redirect("Site.Master");
        }

        public void ShowAuthentication(FacebookUser faceBookUser)
        {
            faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
            FaceBookUserPanel.Visible = true;
            Name.Text = faceBookUser.Name;
            ProfileImage.ImageUrl = faceBookUser.PictureUrl;
            LoginButton.Visible = false;
        }

        public FacebookUser GetFaceBookUserData()
        {
            string data = FaceBookConnect.Fetch(code, "me");
            FacebookUser faceBookUser = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
            return faceBookUser;
        }
    }
}