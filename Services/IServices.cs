using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServices
    {

        // Operation to sign up a new user, accessible via a POST request to "/signup".
        [WebInvoke(Method = "POST", UriTemplate = "/signup")]
        [OperationContract]
        bool SignUp(User newUser);

        // Operation to authenticate a user, accessible via a POST request to "/login".
        [WebInvoke(Method = "POST", UriTemplate = "/login")]
        [OperationContract]
        bool Login(User user);

        // Operation to generate a CAPTCHA image and return it as a byte array.
        [OperationContract]
        byte[] GenerateCaptchaImage();

        // Operation to get text content (possibly for CAPTCHA validation).
        [OperationContract]
        string GetText();

        [OperationContract]
        decimal SolarIntensity(decimal latitude, decimal longitude);

        // Operation to retrieve location information based on a ZIP code.
        [OperationContract]
        LocationInfo GetLocationInfo(string zipcode, string latitude, string longitude, string city);

        [OperationContract]
        News GetNews(string searchString);

        [OperationContract]
        CurrentLocation currLoc();

        [OperationContract]
        bool MemberValidation(string username, string password);
    }


    // Data contract for user information.
    [DataContract]
    public class User
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }
    }

    // Data contract for storing location information.
    [DataContract]
    public class LocationInfo
    {
        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string City { get; set; }
    }

    [DataContract]
    public class News
    {
        [DataMember]
        public List<string> URLs { get; set; }

        [DataMember]
        public List<string> Titles { get; set; }
    }

    [DataContract]
    public class CurrentLocation
    {
        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ZipCode { get; set; }
    }
}
