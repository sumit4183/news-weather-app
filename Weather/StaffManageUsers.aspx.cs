using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weather
{
    public partial class StaffManageUsers : System.Web.UI.Page
    {
        CombinedServices.Service1Client combinedServices = new CombinedServices.Service1Client();
        string username, password, role;

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
                else if (Request.Cookies["authcookie"]["username"] != "TA" && Request.Cookies["authcookie"]["password"] != "Cse44598!")
                {
                    Response.Redirect("~/StaffPage.aspx", false);
                    Response.End();
                }
            }
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            username = usernameTextBox.Text;
            password = passwordTextBox.Text;
            role = roleTextBox.Text;

            if (username == "" || password == "" || role == "")
            {
                reminderLabel.ForeColor = Color.Red;
                reminderLabel.Text = "Please remember to fill out all three fields when adding users.";
                return; 
            }

            //check to see if the user exists and if they don't add them 
            if (combinedServices.CreateStaffMember(username, password, role))
            {
                reminderLabel.ForeColor = Color.Green;
                reminderLabel.Text = "User has successfully been added!";
            }
            else
            {
                reminderLabel.ForeColor = Color.Red;
                reminderLabel.Text = "User cannot be added!";
            }
        }

        protected void rmvBtn_Click(object sender, EventArgs e)
        {
            username = usernameTextBox.Text;

            //ensure fields are filled in 
            if (username == "")
            {
                reminderLabel.ForeColor = Color.Red;
                reminderLabel.Text = "Please remember to fill username removing users.";
                return; 
            }

            //before removing ensure the user exists
            if (combinedServices.RemoveStaffMember(username)) 
            {
                reminderLabel.ForeColor = Color.Green; 
                reminderLabel.Text = "User has successfully been removed!";
            }
            else
            {
                reminderLabel.ForeColor = Color.Red;
                reminderLabel.Text = "User cannot be removed as they do not exist!";
            }
        }

    }
}