using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using Weather.Services;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Weather
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create an instance of your WCF service client
            ServicesClient serviceClient = new ServicesClient();

            if (Request.Cookies["authcookie"] != null)
            {
                if (serviceClient.MemberValidation(Request.Cookies["authcookie"]["username"], Request.Cookies["authcookie"]["password"]))
                {
                    Response.Redirect("~/Member.aspx", false);
                    Response.End();
                }
            }
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {

            // Create an instance of your WCF service client
            ServicesClient serviceClient = new ServicesClient();

            // Create a User object from the input fields
            User newUser = new User
            {
                Username = txtUsername.Text,
                Password = HashString(txtPassword.Text)
            };

            // Call the Signup operation
            bool result = serviceClient.SignUp(newUser);

            // Display the result
            if (result)
            {
                lblResult.Text = "You are successfully signed in. Please login in to access all features.";
            }
            else
            {
                lblResult.Text = "Unsuccessful Signup.";
            }
            ErrorMethod();

        }

        private void ErrorMethod()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            pnCaptcha.Visible = true;
            pnError.Visible = false;
            lblValidationStatus.Text = "";
            pnLoginButton.Visible = false;
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Create an instance of your WCF service client
            ServicesClient serviceClient = new ServicesClient();

            // Create a User object from the input fields
            User user = new User
            {
                Username = txtUsername.Text,
                Password = HashString(txtPassword.Text)

            };

            // Call the Login operation
            bool result = serviceClient.Login(user);

            if (result)
            {
                Response.Cookies["authcookie"]["username"] = user.Username;
                Response.Cookies["authcookie"]["password"] = user.Password;
                Response.Cookies["authcookie"].Expires = DateTime.Now.AddDays(1);
                Response.Redirect("~/Member.aspx", false);
                Response.End();
                lblResult.Text = "You are logged in successfully.";
                pnLogin.Visible = false;
            }
            else
            {
                lblResult.Text = "Log in information incorrect. Please Try again";
                pnLogin.Visible = true;
                ErrorMethod();
            }
        }

        // Variable to store the generated CAPTCHA text
        public string generatedCaptchaText;

        // Event handler for generating and displaying a CAPTCHA image
        protected void btnGenerateCaptcha_Click(object sender, EventArgs e)
        {
            // Make an AJAX request to your WCF service
            // Replace 'YourServiceName.svc' with the actual service URL
            ServicesClient client = new ServicesClient();

            // Request the service to generate a CAPTCHA image
            byte[] captchaImageBytes = client.GenerateCaptchaImage();

            // Display the CAPTCHA image on the web page
            pnError.Visible = true;
            lblResult.Text = "";
            imgCaptcha.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(captchaImageBytes);
            imgCaptcha.Visible = true;
            lblValidationStatus.Text = "";
        }

        // Event handler for validating the user's input against the generated CAPTCHA text
        protected void btnValidateCaptcha_Click(object sender, EventArgs e)
        {
            // Create a client to communicate with the Elective service
            ServicesClient client = new ServicesClient();

            // Retrieve the generated CAPTCHA text from the service
            generatedCaptchaText = client.GetText();

            // Get the user's input from the text input field
            string userInput = txtCaptchaInput.Text;

            // Check if the user's input matches the generated CAPTCHA text
            if (userInput.Equals(generatedCaptchaText))
            {
                lblValidationStatus.Text = "CAPTCHA validated successfully!";
                pnLoginButton.Visible = true;
                pnCaptcha.Visible = false;
            }
            else
            {
                lblValidationStatus.Text = "CAPTCHA validation failed. Please try again.";
            }
            txtCaptchaInput.Text = "";
        }


    }
}