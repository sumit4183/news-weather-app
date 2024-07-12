<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffManageUsers.aspx.cs" Inherits="Weather.StaffManageUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Manage Users</title>

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

                <p class="text-center fs-1 m-0"> Welcome to the Manage Users</p>

                <!-- Page Information Redirects -->
                <div class="container mx-auto p-2 fs-5">
                        
                    <p class="text-center m-0">Here you can add/remove staff members.</p> 
   
                </div>
                <!-- Login Content Ends -->

                <!-- Display Signup/Login Results -->
                <div class="container mx-auto p-2 fs-5 text-center">
                    <asp:Panel ID="Panel1" runat="server">
                        <label> <b> Enter username: </b></label>
                        <asp:TextBox ID="usernameTextBox" runat="server" Placeholder="Ex: 'Admin'"></asp:TextBox>
                        <p></p>
                        <label> <b> Enter password: </b></label>
                        <asp:TextBox ID="passwordTextBox" runat="server" Placeholder="Ex: 'AdminPass'"></asp:TextBox>
                        <p></p>
                        <label> <b> Enter role: </b></label>
                        <asp:TextBox ID="roleTextBox" runat="server" Placeholder="Ex: 'Staff'"></asp:TextBox>
                    </asp:Panel>
                </div>

                <div class="container mx-auto p-2 fs-5 text-center">
                    <asp:Button ID="addBtn" runat="server" Text="Add" CssClass="button" OnClick="addBtn_Click" />
                    <asp:Button ID="rmvBtn" runat="server" Text="Remove" OnClick="rmvBtn_Click" BackColor="IndianRed" ForeColor="White"/>
                    <p></p>
                    <asp:Label font-size="Small" ForeColor="Red" ID="reminderLabel" runat="server" Text="For adding users fill all three fields; for removing users only fill in username."></asp:Label>
                </div>
              
            </div>
            <!-- Container for Main Section Ends-->
        </section>
        <!-- Main Section Ends -->
    </form>
</body>
</html>
