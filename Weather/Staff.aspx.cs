using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Weather
{
    public partial class Staff : System.Web.UI.Page
    {
        CombinedServices.Service1Client combinedServices = new CombinedServices.Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["authcookie"] != null)
            {
                if (combinedServices.StaffValidation(Request.Cookies["authcookie"]["username"], Request.Cookies["authcookie"]["password"]))
                {
                    Response.Redirect("~/StaffPage.aspx", false);
                    Response.End();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool result = combinedServices.StaffValidation(username, password);

            if (result)
            {

                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Successfully logged in!";
                Response.Cookies["authcookie"]["username"] = username;
                Response.Cookies["authcookie"]["password"] = password;
                Response.Cookies["authcookie"]["role"] = "Staff";
                Response.Cookies["authcookie"].Expires = DateTime.Now.AddDays(1);
                Response.Redirect("~/StaffPage.aspx", false);
                Response.End();
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Please enter valid staff member login!";
            }
        }
    }
}