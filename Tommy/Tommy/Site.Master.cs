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

        private void Page_Init(object sender, EventArgs e)
        {
            var requestCookie = Request.Cookies[code];

            if (code == null)
            {
                code = Request.QueryString["code"];

                Page.ViewStateUserKey = code;

                var responseCookie = new HttpCookie(code)
                {
                    HttpOnly = true,
                    Value = code
                };

                Response.Cookies.Set(responseCookie);
            }

            else
            {
                Page.ViewStateUserKey = code;
                var responseCookie = new HttpCookie(code)
                {
                    HttpOnly = true,
                    Value = code
                };
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }


        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            AuthConfig.RegisterOpenAuth();

                if (Request.QueryString["logout"] == "true")
                {
                    code = null;
                    return;
                }

                if (Request.QueryString["error"] == "access_denied")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                    return;
                }

                if (!string.IsNullOrEmpty(code))
                {
                    string data = FaceBookConnect.Fetch(code, "me");
                    FacebookUser faceBookUser = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
                    faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
                    FaceBookUserPanel.Visible = true;
                    Name.Text = faceBookUser.Name;
                    ProfileImage.ImageUrl = faceBookUser.PictureUrl;
                    LoginButton.Visible = false;

                    var comparisontoId = Service.GetUserData(faceBookUser.Id);

                    if (comparisontoId != faceBookUser.Id)
                    {
                        Service.InsertUserData(code, faceBookUser.Id, faceBookUser.Name);
                    }
                }
              
            }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login(object sender, EventArgs e)
        {
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);

        }

        protected void Logout(object sender, EventArgs e)
        {
            FaceBookConnect.Logout(Request.QueryString["code"]);

            Response.Redirect("Site.Master");
        }
    }
}