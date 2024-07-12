using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Weather
{
    public partial class StaffViewRecords : System.Web.UI.Page
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
                else
                {
                    if (!IsPostBack)
                    {
                        DisplayAllStaffMembers();
                        DisplayAllMembers();
                    }
                }
            }
        }

        public void RefreshStaffMembers(object sender, EventArgs e)
        {
            DisplayAllStaffMembers();
            DisplayAllMembers();
        }

        private void DisplayAllMembers()
        {
            XmlDocument xmlDocument = new XmlDocument();
            string filePath = HttpContext.Current.Server.MapPath("~/Page2/Member.xml");
            xmlDocument.Load(filePath);

            //Get all Staff nodes in the Staff.xml file
            XmlNodeList staffElementList = xmlDocument.GetElementsByTagName("User");

            //Checck all nodes to see which node matches the given usernamne argument
            foreach (XmlNode xmlNode in staffElementList)
            {
                //Create row and cells
                TableRow userNodeRow = new TableRow();
                TableCell userUsernameCell = new TableCell();
                TableCell userPasswordCell = new TableCell();

                //Get and store values from xml to store in cells
                XmlNode userUsernameElement = xmlNode.SelectSingleNode("Username");
                XmlNode userPasswordElement = xmlNode.SelectSingleNode("Password");
                string usernameText = userUsernameElement.InnerText;
                string passwordText = userPasswordElement.InnerText;
                userUsernameCell.Text = usernameText;
                userPasswordCell.Text = passwordText.Substring(0,10) + "...";

                //Adds cells to row and then row to rtable
                userNodeRow.Cells.Add(userUsernameCell);
                userNodeRow.Cells.Add(userPasswordCell);
                memberTable.Rows.Add(userNodeRow);
            }
        }

        private void DisplayAllStaffMembers()
        {
            XmlDocument xmlDocument = new XmlDocument();
            string filePath = HttpContext.Current.Server.MapPath("~/Page9/App_Data/Staff.xml");
            xmlDocument.Load(filePath);

            //Get all Staff nodes in the Staff.xml file
            XmlNodeList staffElementList = xmlDocument.GetElementsByTagName("Staff");

            //Checck all nodes to see which node matches the given usernamne argument
            foreach (XmlNode xmlNode in staffElementList)
            {
                //Create row and cells
                TableRow staffNodeRow = new TableRow();
                TableCell staffUsernameCell = new TableCell();
                TableCell staffPasswordCell = new TableCell();
                TableCell staffRoleCell = new TableCell();

                //Get and store values from xml to store in cells
                XmlNode staffUsernameElement = xmlNode.SelectSingleNode("Username");
                XmlNode staffPasswordElement = xmlNode.SelectSingleNode("Password");
                XmlNode staffRoleElement = xmlNode.SelectSingleNode("Role");
                staffUsernameCell.Text = staffUsernameElement.InnerText;
                staffPasswordCell.Text = staffPasswordElement.InnerText;
                staffRoleCell.Text = staffRoleElement.InnerText;


                //Adds cells to row and then row to rtable
                staffNodeRow.Cells.Add(staffUsernameCell);
                staffNodeRow.Cells.Add(staffPasswordCell);
                staffNodeRow.Cells.Add(staffRoleCell);
                staffTable.Rows.Add(staffNodeRow);
            }
        }
    }
}