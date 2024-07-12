<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="Weather.StaffPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Page</title>

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
                    <a href="StaffPage.aspx" class="nav-link text-white"> Staff </a>
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

                <p class="text-center fs-1 m-0"> Welcome to the Staff Page</p>

                <!-- Page Information Redirects -->
                <div class="container mx-auto p-2 fs-5">
                        
                    <p class="text-center m-0">Here you can manage application settings and user data.</p> 
   
                </div>
                <!-- Login Content Ends -->

                <!-- Display Signup/Login Results -->
                <div class="container mx-auto p-2 fs-5 text-center">
                    <asp:Button CssClass="button" ID="ManageUsersBtn" runat="server" Text="Manage Users" OnClick="ManageUsers_Click" />
                    <asp:Button CssClass="button" ID="ViewRecordsBtn" runat="server" Text="View Records" OnClick="ViewRecords_Click" />
                </div>

                <div class="container mx-auto p-2 fs-5 text-center">
                    <asp:Button CssClass="button" ID="LogoutBtn" runat="server" Text="Log out" OnClick="LogoutBtn_Click" />
                </div>
              
            </div>
            <!-- Container for Main Section Ends-->
        </section>
        <!-- Main Section Ends -->
    </form>
</body>
</html>
