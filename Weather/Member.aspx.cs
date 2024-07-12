using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weather.Services;

namespace Weather
{
    public partial class Member : System.Web.UI.Page
    {
        private String CurrCity;
        private String CurrState;
        private String CurrZipCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create a proxy client for the WCF service
            var client = new ServicesClient(); // Creating an instance for NewsFocus Client

            List<String> locationList = new List<String>();
            locationList = getCurrLoc();

            this.CurrCity = locationList[0];
            this.CurrState = locationList[1];
            this.CurrZipCode = locationList[2];
            LocalNews(CurrCity);
            CurrWeather(CurrZipCode, "Imperial");
            if (CurrCity != "Tempe" || CurrZipCode != "85281")
            {
                LocalNews("Tempe");
                CurrWeather("85281", "Imperial");
            }
            else
            {
                LocalNews(CurrCity);
                CurrWeather(CurrZipCode, "Imperial");
            }
            lblWthLoc.Text = "Weather on Current Location";

            if (Request.Cookies["authcookie"] == null)
            {
                Response.Redirect("Default.aspx", false);
                Response.End();
            }
            else
            {
                if (!client.MemberValidation(Request.Cookies["authcookie"]["username"], Request.Cookies["authcookie"]["password"]))
                {
                    Response.Redirect("~/Default.aspx", false);
                    Response.End();
                }
            }
        }

        private List<string> getCurrLoc()
        {
            List<string> locationList = new List<string>();

            // Create a proxy client for the WCF service
            var client = new ServicesClient(); // Creating an instance for NewsFocus Client

            var currLocation = client.currLoc();

            locationList.Add(currLocation.City);
            locationList.Add(currLocation.State);
            locationList.Add(currLocation.ZipCode);

            return locationList;
        }

        private void CurrWeather(string zipCode, string unit)
        {
            CombinedServices.Service1Client combinedServices = new CombinedServices.Service1Client();

            int pos = 0;
            string[] result = combinedServices.Weather5day(zipCode, "US", unit);
            string delimiter = "\t";
            string imageFormat = "";
            string imageURL = "";

            //
            pos = result[0].IndexOf(delimiter);
            imageFormat = result[0].Substring(pos + delimiter.Length);
            imageURL = string.Format("https://openweathermap.org/img/wn/{0}@2x.png", imageFormat);
            weatherImage1.ImageUrl = imageURL;
            weatherDay1.Text = result[0].Substring(0, pos);

            //
            pos = result[1].IndexOf(delimiter);
            imageFormat = result[1].Substring(pos + delimiter.Length);
            imageURL = string.Format("https://openweathermap.org/img/wn/{0}@2x.png", imageFormat);
            weatherImage2.ImageUrl = imageURL;
            weatherDay2.Text = result[1].Substring(0, pos);

            //
            pos = result[2].IndexOf(delimiter);
            imageFormat = result[2].Substring(pos + delimiter.Length);
            imageURL = string.Format("https://openweathermap.org/img/wn/{0}@2x.png", imageFormat);
            weatherImage3.ImageUrl = imageURL;
            weatherDay3.Text = result[2].Substring(0, pos);

            //
            pos = result[3].IndexOf(delimiter);
            imageFormat = result[3].Substring(pos + delimiter.Length);
            imageURL = string.Format("https://openweathermap.org/img/wn/{0}@2x.png", imageFormat);
            weatherImage4.ImageUrl = imageURL;
            weatherDay4.Text = result[3].Substring(0, pos);

            //
            pos = result[4].IndexOf(delimiter);
            imageFormat = result[4].Substring(pos + delimiter.Length);
            imageURL = string.Format("https://openweathermap.org/img/wn/{0}@2x.png", imageFormat);
            weatherImage5.ImageUrl = imageURL;
            weatherDay5.Text = result[4].Substring(0, pos);
        }

        protected void btnLatLonEnb_Click(object sender, EventArgs e)
        {
            pnLatLon.Visible = true;
            pnZipCode.Visible = false;
            pnCity.Visible = false;
            pnBtnSrch.Visible = true;
            lblWthLoc.Text = "Weather on Current Location";
        }

        protected void btnZipCodeEnb_Click(object sender, EventArgs e)
        {
            pnLatLon.Visible = false;
            pnZipCode.Visible = true;
            pnCity.Visible = false;
            pnBtnSrch.Visible = true;
            lblWthLoc.Text = "Weather on Current Location";
        }

        protected void btnCityEnb_Click(object sender, EventArgs e)
        {
            pnLatLon.Visible = false;
            pnZipCode.Visible = false;
            pnCity.Visible = true;
            pnBtnSrch.Visible = true;
            lblWthLoc.Text = "Weather on Current Location";
        }

        protected void btnSearchWth_Click(object sender, EventArgs e)
        {
            // Create a proxy client for the WCF service
            var client = new ServicesClient(); // Creating an instance for NewsFocus Client

            string choice = "";
            if (TempChoice.Text == "")
            {
                return;
            }
            else
            {
                switch (TempChoice.Text)
                {
                    case "Celsius":
                        choice = "Metric";
                        break;
                    case "celsius":
                        choice = "Metric";
                        break;
                    case "Fahrenheit":
                        choice = "Imperial";
                        break;
                    case "fahrenheit":
                        choice = "Imperial";
                        break;
                    case "Kelvin":
                        choice = "Standard";
                        break;
                    case "kelvin":
                        choice = "Standard";
                        break;
                    default:
                        choice = "Imperial";
                        break;
                }
            }

            if (txtZipcode.Text != "")
            {
                var location = client.GetLocationInfo(txtZipcode.Text, null, null, null);
                var city = location.City;
                var zipcode = location.ZipCode;
                LocalNews(city);
                CurrWeather(zipcode, choice);
                lblWthLoc.Text = "Weather on Zipcode: " + txtZipcode.Text;
                txtZipcode.Text = "";
            }
            else if (txtCity.Text != "")
            {
                var location = client.GetLocationInfo(null, null, null, txtCity.Text);
                var city = location.City;
                var zipcode = location.ZipCode;
                LocalNews(city);
                CurrWeather(zipcode, choice);
                lblWthLoc.Text = "Weather in " + txtCity.Text + " City";
                txtCity.Text = "";
            }
            else if (txtLat.Text != "" && txtLon.Text != "")
            {
                var location = client.GetLocationInfo(null, txtLat.Text, txtLon.Text, null);
                var city = location.City;
                location = client.GetLocationInfo(null, null, null, city);
                city = location.City;
                var zipcode = location.ZipCode;
                LocalNews(city);
                CurrWeather(zipcode, choice);
                lblWthLoc.Text = "Weather at " + txtLat.Text + " latitude and " + txtLon.Text + " longitudes";
                txtLat.Text = "";
                txtLon.Text = "";
            }
            TempChoice.Text = "";
        }

        private void LocalNews(string city)
        {
            // Create a proxy client for the WCF service
            var client = new ServicesClient(); // Creating an instance for NewsFocus Client

            lblLoc.Text = $"Location: {CurrCity}, {CurrState}, {CurrZipCode}";

            if (CurrCity != "Tempe" && CurrZipCode != "85281")
            {
                lblLoc.Text = $"Location: Tempe, Arizona, 85281";
            }
            else
            {
                lblLoc.Text = $"Location: {CurrCity}, {CurrState}, {CurrZipCode}";
            }

            try
            {
                // Call the NewsFocus method to get news URLs based on user input
                var news = client.GetNews(city);  // Calling GetNews method from the WCF Service

                // Display the retrieved news URLs or handle the result as needed
                if (news.URLs != null && news.URLs.Length > 0)
                {
                    lblNews.Text = $"<div id='newsTitle'>News on {city}</span>";      // Setting label to display result
                    // Printing each url from newsUrls list
                    for (int i = 0; i < news.URLs.Length; i++)
                    {
                        lblNews.Text += $"<a href='{news.URLs[i]}' target='_blank'>{news.Titles[i]}</a><hr />";
                    }
                }
                // Displaying result if no result found
                else
                {
                    lblNews.Text = "No links found for the given search term";
                }
            }

            finally
            {
                // Close the client when done
                if (client.State == System.ServiceModel.CommunicationState.Opened)
                {
                    client.Close();
                }
            }
        }

        protected void btnNews_Click(object sender, EventArgs e)
        {
            // Create a proxy client for the WCF service
            var client = new ServicesClient(); // Creating an instance for NewsFocus Client

            try
            {
                // Call the NewsFocus method to get news URLs based on user input
                var topics = txtNews.Text;            // Getting user input from the txtTopics Textbox
                LocalNews(topics);
            }

            finally
            {
                // Close the client when done
                if (client.State == System.ServiceModel.CommunicationState.Opened)
                {
                    client.Close();
                }
            }
        }
    }
}