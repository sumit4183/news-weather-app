using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace CombinedServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //converting celsius to fahrenheit
        public double temperatureConversion(double x, string choice)
        {
            //celsius to fahrenheit
            if (choice == "a")
            {
                return (x * 9 / 5) + 32;
            }
            //fahrenheit to celsius 
            else
            {
                return (x - 32) * 5 / 9;
            }
            return 0;
        }
        public string[] Weather5day(string zipcode, string country, string choice)
        {
            string[] result = new string[6];

            //declare apiKey and find the geographical units for the zipcode
            WebClient client1 = new WebClient();
            WebClient client2 = new WebClient();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string apiKey = "df755cdf4f5208b397e2510f4d412ac3";
            string geoURL = string.Format("http://api.openweathermap.org/geo/1.0/zip?zip={0},{1}&appid={2}", zipcode, country, apiKey);
            string geoJSON = client1.DownloadString(geoURL);
            GeoInformation geoInfo = serializer.Deserialize<GeoInformation>(geoJSON);
            String latitude = geoInfo.lat.ToString();
            String longitude = geoInfo.lon.ToString();

            //find the weather information for the polar coordinates
            string weatherURL = "";
            
            //determine the type of API call 
            switch (choice)
            {
                case "Standard":
                    weatherURL = string.Format("https://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=5&appid={2}", latitude, longitude, apiKey);
                    break;
                case "Metric":
                    weatherURL = string.Format("https://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=5&units=metric&appid={2}", latitude, longitude, apiKey);
                    break;
                case "Imperial":
                    weatherURL = string.Format("https://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=5&units=imperial&appid={2}", latitude, longitude, apiKey);
                    break;
                default:
                    weatherURL = string.Format("https://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=5&units=imperial&appid={2}", latitude, longitude, apiKey);
                    break; 
            }

            string weatherJSON = client2.DownloadString(weatherURL);
            WeatherInformation weatherInfo = serializer.Deserialize<WeatherInformation>(weatherJSON);
            string cityInfo = string.Format("City: {0}{1}Country: {2}{3}Population: {4}", weatherInfo.city.name, Environment.NewLine, weatherInfo.city.country, Environment.NewLine, weatherInfo.city.population);
            result[5] = cityInfo;

            //extract the proper information to return 
            double minTemp = 0, maxTemp = 0;
            double dayWind = 0, dayHumidity = 0;
            string currDate = "", dayDesc = "", dayResult = "";

            // 0->7, 8->15, 16->23, 24->31, 32 -> 39
            for (int i = 0; i < 5; i++)
            {
                //reset variables
                currDate = DateTime.Now.AddDays(i).ToString("dddd, dd MMMM yyyy");
                dayDesc = weatherInfo.list[i].weather[0].main;
                minTemp = weatherInfo.list[i].temp.min;
                maxTemp = weatherInfo.list[i].temp.max;
                dayWind = weatherInfo.list[i].speed;
                dayHumidity = weatherInfo.list[i].humidity;

                //put information into result 
                minTemp = Math.Round(minTemp);
                maxTemp = Math.Round(maxTemp);
                dayWind = Math.Round(dayWind);
                dayHumidity = Math.Round(dayHumidity);

                //format the string based on the choice given
                switch (choice)
                {
                    case "Standard":
                        dayResult = string.Format("Date: {0}{1}Description: {2}{3}MinTemp: {4}°K{5}MaxTemp: {6}°K{7}AvgWind: {8}kmph{9}AvgHumidity: {10}%\t", currDate, Environment.NewLine, dayDesc, Environment.NewLine, minTemp, Environment.NewLine, maxTemp, Environment.NewLine, dayWind, Environment.NewLine, dayHumidity);
                        break;
                    case "Metric":
                        dayResult = string.Format("Date: {0}{1}Description: {2}{3}MinTemp: {4}°C{5}MaxTemp: {6}°C{7}AvgWind: {8}kmph{9}AvgHumidity: {10}%\t", currDate, Environment.NewLine, dayDesc, Environment.NewLine, minTemp, Environment.NewLine, maxTemp, Environment.NewLine, dayWind, Environment.NewLine, dayHumidity);
                        break;
                    case "Imperial":
                        dayResult = string.Format("Date: {0}{1}Description: {2}{3}MinTemp: {4}°F{5}MaxTemp: {6}°F{7}AvgWind: {8}mph{9}AvgHumidity: {10}%\t", currDate, Environment.NewLine, dayDesc, Environment.NewLine, minTemp, Environment.NewLine, maxTemp, Environment.NewLine, dayWind, Environment.NewLine, dayHumidity);
                        break;
                    default:
                        dayResult = string.Format("Date: {0}{1}Description: {2}{3}MinTemp: {4}°F{5}MaxTemp: {6}°F{7}AvgWind: {8}mph{9}AvgHumidity: {10}%\t", currDate, Environment.NewLine, dayDesc, Environment.NewLine, minTemp, Environment.NewLine, maxTemp, Environment.NewLine, dayWind, Environment.NewLine, dayHumidity);
                        break;
                }
                result[i] = dayResult + weatherInfo.list[i].weather[0].icon;
            }
            return result;
        }

        //Staff Validation
        public bool StaffValidation(string username, string password)
        {
            XmlDocument xmlDocument = new XmlDocument();
            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/Staff.xml");
            xmlDocument.Load(filePath);

            //Get all nodes that match the given usernamne argument
            XmlNode user = xmlDocument.SelectSingleNode("/StaffMembers/Staff[Username='" + username + "']");

            if (user != null)
            {
                //Get the nodes that matches the given usernamne argument
                string userpass = user["Password"].InnerText;
                if (userpass != null)
                {
                    if (userpass == password)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool CreateStaffMember(string username, string password, string role)
        {

            if (CreateStaffMemberXMLNode(username, password, role))
            {
                //Successfully created the Staff Member
                return true; 
            }
            else
            {
                //Unsuccessful in creating Staff Member
                return false; 
            }
        }

        private bool CreateStaffMemberXMLNode(string username, string password, string role)
        {
            XmlNode addedStaffNode = findStaffMemberXMLNode(username);

            if (addedStaffNode == null)
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    string filePath = HttpContext.Current.Server.MapPath("~/App_Data/Staff.xml");
                    xmlDocument.Load(filePath);

                    //Create XML elements in Staff.xml and assign given arguments
                    XmlElement newStaff = xmlDocument.CreateElement("Staff");
                    XmlElement newStaffUsername = xmlDocument.CreateElement("Username");
                    XmlElement newStaffPassword = xmlDocument.CreateElement("Password");
                    XmlElement newStaffRole = xmlDocument.CreateElement("Role");
                    newStaffUsername.InnerText = username;
                    newStaffPassword.InnerText = password;
                    newStaffPassword.InnerText = role;

                    //Append elements to create the staff element
                    newStaff.AppendChild(newStaffUsername);
                    newStaff.AppendChild(newStaffPassword);
                    newStaff.AppendChild(newStaffRole);
                    xmlDocument.DocumentElement.AppendChild(newStaff);

                    xmlDocument.Save(filePath);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false; 
            }
        }

        public bool RemoveStaffMember(string username)
        {

            if (RemoveStaffMemberXMLNode(username))
            {
                //Successfully removed the Staff Member
                return true; 
            }
            else
            {
                //Unsuccessful in removing Staff Member
                return false; 
            }
        }

        private bool RemoveStaffMemberXMLNode(string username)
        {
            XmlNode removedStaffNode = null;

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                string filePath = HttpContext.Current.Server.MapPath("~/App_Data/Staff.xml");
                xmlDocument.Load(filePath);

                //Get all Staff nodes in the Staff.xml file
                XmlNodeList staffElementList = xmlDocument.GetElementsByTagName("Staff");

                //Checck all nodes to see which node matches the given usernamne argument
                foreach (XmlNode xmlNode in staffElementList)
                {
                    XmlNode staffUsernameElement = xmlNode.SelectSingleNode("Username");

                    if (staffUsernameElement.InnerText == username)
                    {
                        removedStaffNode = xmlNode;
                    }
                }

                if (removedStaffNode != null)
                { 
                    //Successfully Removed the Staff Member that matches given username;
                    xmlDocument.DocumentElement.RemoveChild(removedStaffNode);
                    xmlDocument.Save(filePath);
                    return true;
                }
            }
            catch
            {
                return false; 
            }
            return false;
        }

        private XmlNode findStaffMemberXMLNode(string username)
        {
            XmlNode findStaffNode = null;

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                string filePath = HttpContext.Current.Server.MapPath("~/App_Data/Staff.xml");
                xmlDocument.Load(filePath);

                //Get all Staff nodes in the Staff.xml file
                XmlNodeList staffElementList = xmlDocument.GetElementsByTagName("Staff");

                //Checck all nodes to see which node matches the given usernamne argument
                foreach (XmlNode xmlNode in staffElementList)
                {
                    XmlNode staffUsernameElement = xmlNode.SelectSingleNode("Username");

                    if (staffUsernameElement.InnerText == username)
                    {
                        findStaffNode = xmlNode;
                    }
                }
            }
            catch
            {
                return null;
            }
            return findStaffNode;
        }


        // this is how the geo information is returned from openweathermap
        public class GeoInformation
        {
            public string zip { get; set; }
            public string name { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public string country { get; set; }
        }
        // this is how the weather information is returned from openweathermap
        public class WeatherInformation
        {
            public City city { get; set; }
            public List<list> list { get; set; }
        }
        // in correlation with weather information
        public class City
        {

            public string name { get; set; }
            public string country { get; set; }
            public int population { get; set; }
        }
        public class TemperatureInfo
        {
            public double min { get; set; }
            public double max { get; set; }
        }
        public class Weather
        {
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        // in correlation with weather information 
        public class list
        {
            public TemperatureInfo temp { get; set; }
            public double humidity { get; set; }
            public List<Weather> weather { get; set; }
            public double speed { get; set; }
            public double pop { get; set; }
            //public string dt_txt { get; set; }

        }
    }
}
