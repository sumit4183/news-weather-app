<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="Weather.Staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Login Page</title>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.9.1/font/bootstrap-icons.css" />
    <link rel="stylesheet" type="text/css" href="Staff.css" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navbar -->
        <nav class="navbar navbar-expand-md fixed-top bg-black" id="Navbar">
            <!-- Container for Navbar -->
            <div class="container px-5">
                <!-- Home Icon -->
                <a href="http://webstrar5.fulton.asu.edu/Default" class="navbar-brand"><i class="bi bi-house text-white" id="home-icon"></i></a>
                <!-- Button after the Navbar Collapse -->
                <button class="navbar-toggler ml-auto" type="button" data-bs-toggle="collapse" data-bs-target="#navbarDropdown" aria-controls="navbarDropdown" aria-expanded="false" aria-label="ToggleNavigation">
                <span class="navbar-toggler-icon"></span>
                </button>
                <!-- Navbar Links http://webstrar5.fulton.asu.edu/Default -->
                <div class="collapse navbar-collapse" id="navbarDropdown">
                <div class="navbar-nav text-end ms-auto">
                    <a href="Default.aspx" class="nav-link text-white"> Signup </a>
                    <a href="Member.aspx" class="nav-link text-white"> Member </a>
                    <a href="Staff.aspx" class="nav-link text-white"> Staff </a>
                    <a href="http://webstrar5.fulton.asu.edu/index.html" class="nav-link text-white"> About </a>
                </div>
                </div>
                <!-- Navbar Links Ends -->
            </div>
            <!-- Container for Navbar Ends -->
        </nav>
        <!-- Navbar Ends -->

                <!-- Main Section -->
        <section id="main-section">

            <!-- Container for Content of Main Section -->
            <div class="container text-black transbox mx-auto p-2"><br />

                <p class="text-center fs-1 m-0"> Welcome to the Staff Login Page</p>

                <!-- Hide below content once user logs in  -->
                <asp:Panel ID="pnLogin" runat="server" Visible="true">

                    <!-- Login Content -->
                    <div class="container mx-auto p-2 fs-5">
                        
                        <!-- Input Box For Username -->
                        <p>Username: &emsp;<asp:TextBox ID="txtUsername" runat="server" placeholder="Enter Username" CssClass="light"/> </p>

                        <!-- Input Box For Password -->
                        <p>Password: &nbsp;&emsp;<asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Password" TextMode="Password" CssClass="light" /> </p>

                        <!-- Button for Login -->
                        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="dark"/>
   
                    </div>
                    <!-- Login Content Ends -->
                </asp:Panel>
                <!-- Panel with ID="pnLogin" Ends -->

                <!-- Display Signup/Login Results -->
                <div class="container mx-auto p-2 fs-5 text-center">
                    <asp:Label ID="lblResult" runat="server" Text="" CssClass="loggedIn"></asp:Label>
                </div>
                
            </div>
            <!-- Container for Main Section Ends-->
        </section>
        <!-- Main Section Ends -->
    </form>
    <asp:PlaceHolder runat="server">
         <%: Scripts.Render("~/Scripts/bootstrap.js") %>
    </asp:PlaceHolder>
</body>
</html>
