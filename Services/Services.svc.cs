using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.Emit;
using System.CodeDom.Compiler;
using static System.Net.WebRequestMethods;
using System.Web;
using System.Xml;
using System.Security.Cryptography;

namespace Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Services : IServices
    {
        // Get the path to the XML file where user data is stored.
        private string UsersXmlPath
        {
            get
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(baseDirectory, "Member.xml");
            }
        }

        // Method for user registration (sign-up).
        public bool SignUp(User newUser)
        {
            // Load the list of existing users from the XML file.
            List<User> users = LoadUsersFromXml();

            // Check if the provided username is already taken.
            if (users.Exists(u => u.Username == newUser.Username))
            {
                // Username is already taken, registration failed.
                return false;
            }

            // Add the new user to the list of users and save it to the XML file.
            users.Add(newUser);
            SaveUsersToXml(users);

            // Registration successful.
            return true;
        }

        // Method for user authentication (login).
        public bool Login(User user)
        {
            // Load the list of existing users from the XML file.
            List<User> users = LoadUsersFromXml();

            // Check if the provided username and password match any stored user.
            if (users.Exists(u => u.Username == user.Username && u.Password == user.Password))
            {
                // Authentication successful.
                return true;
            }

            // Authentication failed.
            return false;
        }

        // Load user data from the XML file and return it as a list of User objects.
        private List<User> LoadUsersFromXml()
        {
            List<User> users = new List<User>();

            // Check if the XML file exists.
            if (System.IO.File.Exists(UsersXmlPath))
            {
                // Load and parse the XML document.
                XDocument doc = XDocument.Load(UsersXmlPath);

                // Iterate through user elements in the XML and create User objects.
                foreach (XElement userElement in doc.Descendants("User"))
                {
                    User user = new User
                    {
                        Username = userElement.Element("Username").Value,
                        Password = userElement.Element("Password").Value
                    };
                    users.Add(user);
                }
            }

            // Return the list of loaded User objects.
            return users;
        }

        // Save a list of User objects to the XML file.
        private void SaveUsersToXml(List<User> users)
        {
            // Create an XML document and populate it with user data.
            XDocument doc = new XDocument(
                new XElement("Users",
                    users.Select(u =>
                        new XElement("User",
                            new XElement("Username", u.Username),
                            new XElement("Password", u.Password)
                        )
                    )
                )
            );

            // Save the XML document to the specified file path.
            doc.Save(UsersXmlPath);
        }

        // This method generates a CAPTCHA image and returns it as a byte array.
        public byte[] GenerateCaptchaImage()
        {
            // Generate a random CAPTCHA text and store it in the CaptchaClass.GeneratedText property
            CaptchaClass.GeneratedText = GenerateRandomCaptchaText();

            // Generate a CAPTCHA image using the generated text with a width of 200 pixels and height of 100 pixels
            Bitmap captchaImage = CaptchaGenerator.GenerateCaptcha(CaptchaClass.GeneratedText, 200, 100);

            // Create a memory stream to temporarily store the image data
            using (MemoryStream stream = new MemoryStream())
            {
                // Save the CAPTCHA image to the memory stream in PNG format
                captchaImage.Save(stream, ImageFormat.Png);

                // Convert the image in the memory stream to a byte array and return it
                return stream.ToArray();
            }
        }

        // Generate a random CAPTCHA text consisting of 5 characters from the set of uppercase letters (A-Z) and digits (0-9).
        private static string GenerateRandomCaptchaText()
        {
            // Define the characters that can be used in the CAPTCHA text
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Create a random number generator
            Random random = new Random();

            // Create an array to store the CAPTCHA text characters (5 characters in this case)
            char[] captchaText = new char[5];

            // Generate each character of the CAPTCHA text by randomly selecting from the 'chars' string
            for (int i = 0; i < 5; i++)
            {
                captchaText[i] = chars[random.Next(chars.Length)];
            }

            // Create a string from the array of characters to represent the CAPTCHA text
            string text = new string(captchaText);

            // Return the generated CAPTCHA text
            return text;
        }

        // Gets the text of the currently generated CAPTCHA.
        public string GetText()
        {
            return CaptchaClass.GeneratedText;
        }

        // Implementing SolarIntensity method from the interface that take latitude and longitude
        public decimal SolarIntensity(decimal latitude, decimal longitude)
        {
            // Initialize the annualAvg variable to -1, indicating an error or no data
            decimal annualAvg = -1;

            // Define the API key for accessing the solar energy data
            string apikey = "eCfqvwWO7OcNaKGUrkSHTA8nypNXwwlvcGHAEYTM";

            // Create an HttpClient to send HTTP requests
            using (var client = new HttpClient())
            {
                // Construct the request URL with the provided latitude, longitude, and API key
                string requestUrl = $"https://developer.nrel.gov/api/solar/solar_resource/v1.json?api_key={apikey}&lat={latitude}&lon={longitude}";

                // Send an asynchronous GET request to the API endpoint
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;

                // Check if the response indicates success
                if (response.IsSuccessStatusCode)
                {
                    // Read the response body as a string
                    var body = response.Content.ReadAsStringAsync().Result;

                    // Parse the JSON response into a JObject
                    JObject jsonResult = JsonConvert.DeserializeObject<JObject>(body);

                    // Extract the average direct normal irradiance (DNI) value
                    var avgDniToken = jsonResult["outputs"]["avg_dni"];

                    // Check if the extracted token exists and is of the expected type
                    if (avgDniToken != null && avgDniToken.Type == JTokenType.Object)
                    {
                        // Retrieve the annual average DNI value and store it in the annualAvg variable
                        annualAvg = avgDniToken["annual"].Value<decimal>();
                    }
                }
                else
                {
                    // Print an error message if the HTTP request was not successful
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                }

                // Return the annual average DNI value or -1 if there was an error
                return annualAvg;
            }
        }

        // This method retrieves location information based on a given ZIP code.
        /*public LocationInfo GetLocationInfo(string zipcode)
        {
            // Initialize latitude and longitude to default values
            string latitude = "0.0";
            string longitude = "0.0";

            // Create an instance of HttpClient to make an HTTP request to a location information API
            using (var client = new HttpClient())
            {
                // Create an HTTP request message to make a GET request to the specified API endpoint
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://us-zip-code-information.p.rapidapi.com/?zipcode={zipcode}"),
                    Headers =
                    {
                        // Set API key and host headers for authentication
                        { "X-RapidAPI-Key", "59048659afmsh750ea6c4d154156p1779ecjsn97e7e7198590" },
                        { "X-RapidAPI-Host", "us-zip-code-information.p.rapidapi.com" },
                    },
                };

                // Send the HTTP request and wait for the response
                using (var response = client.SendAsync(request).Result)
                {
                    // Check if the response is successful (status code 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        var body = response.Content.ReadAsStringAsync().Result;
                        
                        // Deserialize the JSON response into a List<LocationInfo>
                        List<LocationInfo> zipCodeInfos = JsonConvert.DeserializeObject<List<LocationInfo>>(body);

                        // Check if any location information was returned
                        if (zipCodeInfos.Count > 0)
                        {
                            // Retrieve the latitude and longitude from the first item in the list (assuming only one item in the response)
                            var firstZipCodeInfo = zipCodeInfos[0];
                            zipcode = firstZipCodeInfo.ZipCode;
                            latitude = firstZipCodeInfo.Latitude;
                            longitude = firstZipCodeInfo.Longitude;
                        }
                    }
                }
            }

            // Create and return a new LocationInfo object with the retrieved or default latitude, longitude, and ZIP code
            return new LocationInfo
            {
                ZipCode = zipcode,
                Latitude = latitude,
                Longitude = longitude
            };
        }*/

        public LocationInfo GetLocationInfo(string zipcode, string latitude, string longitude, string city)
        {
            // Create an instance of HttpClient to make an HTTP request to a location information API
            using (var client = new HttpClient())
            {
                if (zipcode != null || city != null)
                {
                    string temp = "";
                    if (city != null)
                    {
                        temp = "https://us-zipcodes.p.rapidapi.com/codes?q=" + city;
                    }
                    else if (zipcode != "")
                    {                        
                        temp = "https://us-zipcodes.p.rapidapi.com/codes/" + zipcode;
                    }
                    // Create an HTTP request message to make a GET request to the specified API endpoint
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(temp),
                        Headers =
                        {
                            { "X-RapidAPI-Key", "59048659afmsh750ea6c4d154156p1779ecjsn97e7e7198590" },
                            { "X-RapidAPI-Host", "us-zipcodes.p.rapidapi.com" },
                        },
                    };

                    // Send the HTTP request and wait for the response
                    using (var response = client.SendAsync(request).Result)
                    {
                        // Check if the response is successful (status code 2xx)
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as a string
                            var body = response.Content.ReadAsStringAsync().Result;

                            if (city != null)
                            {
                                // Deserialize the JSON response into a List<LocationInfo>
                                var jsonArray = JArray.Parse(body);

                                if (jsonArray.Count > 0)
                                {
                                    var firstResult = jsonArray[0];
                                    latitude = firstResult["latitude"].ToString();
                                    longitude = firstResult["longitude"].ToString();
                                    zipcode = firstResult["zip_code"].ToString();
                                }
                            }
                            else if (zipcode != null)
                            {
                                var responseObj = JObject.Parse(body);

                                latitude = responseObj["latitude"].ToString();
                                longitude = responseObj["longitude"].ToString();
                                city = responseObj["city"].ToString();
                            }
                        }
                        else
                        {
                            city = "Tempe";
                            zipcode = "85281";
                        }
                    }
                }
                else
                {
                    // Create an HTTP request message to make a GET request to the specified API endpoint
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://geocoding-by-api-ninjas.p.rapidapi.com/v1/reversegeocoding?lat={latitude}&lon={longitude}"),
                        Headers =
                        {
                            { "X-RapidAPI-Key", "59048659afmsh750ea6c4d154156p1779ecjsn97e7e7198590" },
                            { "X-RapidAPI-Host", "geocoding-by-api-ninjas.p.rapidapi.com" },
                        },
                    };

                    // Send the HTTP request and wait for the response
                    using (var response = client.SendAsync(request).Result)
                    {
                        // Check if the response is successful (status code 2xx)
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as a string
                            var body = response.Content.ReadAsStringAsync().Result;

                            // Deserialize the JSON response into a List<LocationInfo>
                            var responseObj = JArray.Parse(body);

                            var locationObject = responseObj.First;

                            city = locationObject["name"].ToString();
                        }
                    }
                }
            }

            // Create and return a new LocationInfo object with the retrieved or default latitude, longitude, and ZIP code
            return new LocationInfo
            {
                ZipCode = zipcode,
                Latitude = latitude,
                Longitude = longitude, 
                City = city
            };
        }

        // Implementing GetNews method from the interface that take topic
        public News GetNews(string topics)
        {
            // Initializing new string to return the news URLs
            List<string> newsUrls = new List<string>();

            List<string> titles = new List<string>();

            // Create an HttpClient for sending HTTP requests
            using (var client = new HttpClient())
            {
                // Create an HttpRequestMessage for the GET request to the news API
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://news-api14.p.rapidapi.com/search?q={Uri.EscapeDataString(topics)}&language=en"),
                    Headers =
                    {
                        // Set the headers required for making the API request
                        { "X-RapidAPI-Key", "59048659afmsh750ea6c4d154156p1779ecjsn97e7e7198590" },
                        { "X-RapidAPI-Host", "news-api14.p.rapidapi.com" },
                    },
                };

                // Send the HTTP request asynchronously and get the response
                using (var response = client.SendAsync(request).Result)
                {
                    // Check if the response indicates success
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response body as a string
                        var body = response.Content.ReadAsStringAsync().Result;

                        // Deserialize the JSON response into a NewsApiResponse object
                        var jsonResult = JsonConvert.DeserializeObject<NewsApiResponse>(body);

                        // Check if the deserialized object and articles list are not null
                        if (jsonResult != null && jsonResult.Articles != null)
                        {
                            // Iterate through the articles and add their URLs to the list
                            foreach (var article in jsonResult.Articles)
                            {
                                newsUrls.Add(article.Url);
                                titles.Add(article.Title);
                            }
                        }
                    }
                    else
                    {
                        // Print an error message if the HTTP request was not successful
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
            }

            // Return the list of news URLs
            return new News
            {
                URLs = newsUrls,
                Titles = titles
            };
        }

        public CurrentLocation currLoc()
        {
            string city = "";
            string state = "";
            string zipCode = "";

            // Create an HttpClient for sending HTTP requests
            using (var client = new HttpClient())
            {
                // Create an HttpRequestMessage for the GET request to the news API
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://ip-geo-location.p.rapidapi.com/ip/check?format=json"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "59048659afmsh750ea6c4d154156p1779ecjsn97e7e7198590" },
                        { "X-RapidAPI-Host", "ip-geo-location.p.rapidapi.com" },
                    },
                };

                // Send the HTTP request asynchronously and get the response
                using (var response = client.SendAsync(request).Result)
                {
                    // Check if the response indicates success
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response body as a string
                        var body = response.Content.ReadAsStringAsync().Result;
                        var responseObj = JObject.Parse(body);
                        city = responseObj["city"]["name"].ToString();
                        state = responseObj["area"]["name"].ToString();
                        zipCode = responseObj["postcode"].ToString();
                    }
                    else
                    {
                        // Print an error message if the HTTP request was not successful
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
            }

            // Return the list of news URLs
            return new CurrentLocation
            {
                City = city,
                State = state,
                ZipCode = zipCode
            };
        }

        public bool MemberValidation(string username, string password)
        {
            XmlDocument xmlDocument = new XmlDocument();
            string filePath = HttpContext.Current.Server.MapPath("Member.xml");
            xmlDocument.Load(filePath);
            
            //Get all nodes that match the given usernamne argument
            XmlNode user = xmlDocument.SelectSingleNode("/Users/User[Username='" + username + "']");
            
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

        private string HashString(string input)
        {
            HashAlgorithm hashAlgorithm = SHA256.Create();
            byte[] bytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            return CreateString(bytes);
        }


        private string CreateString(byte[] byteArray)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < byteArray.Length; i++)
            {
                stringBuilder.Append(byteArray[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }

    // This class stores CAPTCHA text generated by the CaptchaGenerator class.
    public class CaptchaClass
    {
        public static string GeneratedText = "";

        // Property to get or set the CAPTCHA text.
        public string GenCaptcha
        {
            get { return GeneratedText; }
            set { GeneratedText = value; }
        }
    }

    // This class is responsible for generating CAPTCHA images.
    public class CaptchaGenerator
    {
        // Generates a CAPTCHA image with the provided text, width, and height.
        public static Bitmap GenerateCaptcha(string text, int width, int height)
        {
            // Create a bitmap with the specified width and height.
            Bitmap captchaImage = new Bitmap(width, height);

            // Create a Graphics object to draw on the image.
            using (Graphics graphics = Graphics.FromImage(captchaImage))
            {
                // Define fonts, brushes, and other properties for drawing.
                Font font = new Font("Arial", 12, FontStyle.Bold);
                SolidBrush brush = new SolidBrush(Color.Black);

                // Draw the provided text on the image.
                graphics.DrawString(text, font, brush, new PointF(10, 10));

                // Add noise to the image for security (e.g., lines and dots).
                AddNoise(graphics, width, height);
            }
            return captchaImage;
        }

        // Adds noise elements to the CAPTCHA image to enhance security.
        private static void AddNoise(Graphics graphics, int width, int height)
        {
            // You can add noise to the CAPTCHA image, such as lines, dots, or other elements.
            // This step is important for making CAPTCHAs more secure.
            Random random = new Random();

            // Add random lines as noise
            for (int i = 0; i < 5; i++)
            {
                Pen pen = new Pen(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));
                int x1 = random.Next(0, width);
                int y1 = random.Next(0, height);
                int x2 = random.Next(0, width);
                int y2 = random.Next(0, height);
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            // Add random dots as noise
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(0, width);
                int y = random.Next(0, height);
                SolidBrush brush = new SolidBrush(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));
                graphics.FillEllipse(brush, x, y, 2, 2);
            }
        }
    }

    // Define a class to represent the response from the news API
    public class NewsApiResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }
    }

    // Define a class to represent an article retrieved from the news API
    public class Article
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime PublishedDate { get; set; }
        public Publisher Publisher { get; set; }
    }

    // Define a class to represent a publisher of an article
    public class Publisher
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
