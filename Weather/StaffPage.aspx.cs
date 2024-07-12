using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Weather
{
    public partial class StaffPage : System.Web.UI.Page
    {
        CombinedServices.Service1Client combinedServices = new CombinedServices.Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["authcookie"] == null)
            {
                Response.Redirect("~/Staff.aspx", false);
                Response.End();
            }
            else
            {
                if (!combinedServices.StaffValidation(Request.Cookies["authcookie"]["username"], Request.Cookies["authcookie"]["password"]))
                {
                    Response.Redirect("~/Staff.aspx", false);
                    Response.End();
                }
            }
        }

        protected void ManageUsers_Click(object sender, EventArgs e)
        {
            Response.Cookies["authcookie"].Value = Request.Cookies["authcookie"].Value;
            Response.Cookies["authcookie"].Expires = Request.Cookies["authcookie"].Expires;
            Response.Cookies["authcookie"]["staff"] = "Staff";
            Response.Redirect("~/StaffManageUsers.aspx", false);
            Response.End();
        }

        protected void ViewRecords_Click(object sender, EventArgs e)
        {
            Response.Cookies["authcookie"].Value = Request.Cookies["authcookie"].Value;
            Response.Cookies["authcookie"].Expires = Request.Cookies["authcookie"].Expires;
            Response.Cookies["authcookie"]["staff"] = "Staff";
            Response.Redirect("~/StaffViewRecords.aspx", false);
            Response.End();
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Response.Cookies["authcookie"].Expires = DateTime.Now.AddDays(-1d);
            Response.Redirect("~/Staff.aspx");
            Response.End();
        }
    }
}