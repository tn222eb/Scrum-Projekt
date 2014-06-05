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
using Facebook;
using System.Reflection;
using System.Collections.Specialized;


namespace Tommy
{
    public partial class SiteMaster : MasterPage
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        public string Code
        {
            get
            {
                return Session["code"] as string;
            }
            set
            {
                Session["code"] = value;
            }
        
        }


        /// <summary>
        /// Lagrar Facebook access-token i session för att man ska kunna förflytta sig mellan sidor och se att man är inloggad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Init(object sender, EventArgs e)
        {
            if (Code == null)
            {
                Code = Request.QueryString["code"];


                // Kod som tar bort access-token från urln
                if (Code != null)
                {

                    /// Tagit koden från stack-overflow
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                    string[] separateURL = url.Split('?');
                    NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(separateURL[1]);
                    queryString.Remove("code");
                    url = separateURL[0] + queryString.ToString();
                    Response.Redirect(url);
                }
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
                Code = null;
                return;
            }

            if (!string.IsNullOrEmpty(Code))
            {
                var faceBookUser = DataExtensions.GetData(Code);

                ShowAuthentication(faceBookUser);
                IsNewUser(faceBookUser);  
            }
        }

        /// <summary>
        /// Kollar om det är en ny användare och ifall det är så lagrar man användarens namn och id i databasen
        /// </summary>
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
            FaceBookConnect.Logout(Code);
        }

        
        /// <summary>
        ///  Visar informationen om användaren
        /// </summary>
        /// <param name="faceBookUser"></param>
        public void ShowAuthentication(FacebookUser faceBookUser)
        {
            faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
            FaceBookUserPanel.Visible = true;

            var adminId = Service.GetAdminData();
            if (faceBookUser.Id == adminId)
            {
                Admin.Visible = true;
            }

            Name.Text = faceBookUser.Name;
            ProfileImage.ImageUrl = faceBookUser.PictureUrl;
            LoginButton.Visible = false;
        }


    }
}