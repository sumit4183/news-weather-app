<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="Weather.Member" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Default Page</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.9.1/font/bootstrap-icons.css" />
    <link rel="stylesheet" type="text/css" href="Member.css" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <script type="text/javascript">
        function updateDateTime() {
            var now = new Date();
            var dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            var day = dayNames[now.getDay()];
            var month = now.getMonth() + 1;
            var date = now.getDate();
            var year = now.getFullYear();
            var hours = now.getHours();
            var minutes = now.getMinutes();
            var seconds = now.getSeconds();
            var ampm = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12;
            hours = hours ? hours : 12; // the hour '0' should be '12'
            minutes = minutes < 10 ? '0' + minutes : minutes;
            seconds = seconds < 10 ? '0' + seconds : seconds;
            var strTime = (month < 10 ? '0' : '') + month + '/' + (date < 10 ? '0' : '') + date + '/' + year + ', ' + day + ', ' + hours + ':' + minutes + ':' + seconds + ' ' + ampm + ' ' + Intl.DateTimeFormat().resolvedOptions().timeZone;

            document.getElementById("dateTimeDisplay").innerHTML = strTime;
            setTimeout(updateDateTime, 1000);
        }
        window.onload = updateDateTime;

        function setCookie(name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toUTCString();
            }
            document.cookie = name + "=" + (value || "") + expires + "; path=/";
        }

        function deleteCookie(name) {
            setCookie(name, "", -1);
        }

        function handleRedirect() {
            deleteCookie("authcookie");
            window.location.href = "Default.aspx";
        }
    </script>
    <webopt:bundlereference runat="server" path="~/Content/css" />
</head>
<body style="background-image: linear-gradient(rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2)), url('images/weatherbackground.jpg');">
    <form id="form1" runat="server">

        <!-- Left Side navigation Bar -->
        <div id="mySidenav" class="sidenav">
            <!-- Close button for Side Navigation Bar -->
            <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
            <!-- Icons in Side navigation Bar -->
            <div class="split">               
                <a href="javascript:void(0);" onclick="handleRedirect()"><i class="bi bi-person-plus"></i></a>
                <a href="http://webstrar5.fulton.asu.edu/Staff"><i class="bi bi-person-circle"></i></a>
                <a href="http://webstrar5.fulton.asu.edu/index.html"><i class="bi bi-info-circle-fill"></i></a>
                <a href="javascript:void(0);" onclick="handleRedirect()"><i class="bi bi-box-arrow-left"></i></a>
            </div>
            <!-- Icons in Side navigation Bar Ends -->
            <!-- Links in Side Navigation Bar -->
            <a href="Member.aspx" class="nav-link"><i class="bi bi-house"></i> Home </a>
            <a href="http://webstrar5.fulton.asu.edu/Page10/InteractiveMap.html" class="nav-link"> WeatherMap  </a>
            <!-- Link in Side Navigation Bar Ends -->
        </div>
        <!-- Left Side Navigation Bar Ends -->
        
        <!-- Weather Information Container -->
        <div id="main">
            <!-- Menu Button and Location -->
            <div class="container">
                <div class="row">
                    <!-- Menu Button -->
                    <div class="col text-start">
                        <span style="font-size:30px;cursor:pointer" onclick="openNav()">&#9776; Menu</span>
                    </div>
                    <!-- Menu Button Ends -->
                    <!-- Current Location -->
                    <div class="col text-center" style="font-size: 20px;">
                        Current <asp:Label ID="lblLoc" runat="server" Text=""></asp:Label>
                    </div>
                    <div id="dateTimeDisplay" style="margin-top: 20px; font-family: 'Arial', sans-serif; font-size: 20px;"></div>
                    <!-- Current Location Ends -->
                </div>
            </div>
            <!-- Menu Button and Location Ends -->

            <div class="container mx-auto px-2 pb-2 pt-3 transbox text-center">

                <!-- Lat/Lon, Zipcode, City Buttons -->
                <div class="row mx-4 mt-3">
                    <div class="col-1"></div>
                    <div class="col">
                        <asp:Button ID="btnLatLonEnb" runat="server" Text="Lat/Lon" OnClick="btnLatLonEnb_Click" />
                        <asp:Button ID="btnZipCodeEnb" runat="server" Text="ZipCode" OnClick="btnZipCodeEnb_Click" />
                        <asp:Button ID="btnCityEnb" runat="server" Text="City" OnClick="btnCityEnb_Click" />
                    </div>
                    <div class="col-1"></div>
                </div>
                <!-- Lat/Lon, Zipcode, City Buttons Ends -->
                
                <div class="row mx-4 mt-3">
                    <!-- Input for Lat/Lon -->
                    <asp:Panel ID="pnLatLon" runat="server" Visible="false">
                        <label> <b> Enter Latitude: </b> </label>
                        <asp:TextBox ID="txtLat" runat="server" Placeholder="Ex: '33.42815'" />
                        <label> <b> Enter Longitude: </b> </label>
                        <asp:TextBox ID="txtLon" runat="server" Placeholder="Ex: '-111.93115'" />
                    </asp:Panel>
                    <!-- Input for Lat/Lon Ends -->
                    <!-- Input for ZipCode -->
                    <asp:Panel ID="pnZipCode" runat="server" Visible="false">
                        <label> <b> Enter Zipcode: </b> </label>
                        <asp:TextBox ID="txtZipcode" runat="server" Placeholder="Ex: '85281'" />
                    </asp:Panel>
                    <!-- Input for ZipCode Ends -->
                    <!-- Input for City -->
                    <asp:Panel ID="pnCity" runat="server" Visible="false">
                        <label> <b> Enter City: </b> </label>
                        <asp:TextBox ID="txtCity" runat="server" Placeholder="Ex: 'Tempe'" />
                    </asp:Panel>
                    <!-- Input for City Ends -->
                </div>
                
                
                <!-- Search Button -->
                <div class="row mx-4 mt-3">
                    <asp:Panel ID="pnBtnSrch" runat="server" Visible="false">
                        <label> <b> Enter temperature choice: </b></label>
                        <asp:TextBox ID="TempChoice" runat="server" Placeholder="Ex: 'Celsius', 'Fahrenheit', 'Kelvin'" Width="240px" />
                        <asp:Button ID="btnSearchWth" runat="server" Text="Search" OnClick="btnSearchWth_Click" />
                    </asp:Panel>
                </div>
                <!-- Search Button Ends -->

                <!-- 5-day Weather Forecast -->
                <div class="row pt-4">
                    <asp:Label ID="lblWthLoc" runat="server" Text=""></asp:Label>
                </div>
                <div class="row pt-4">
                    <section class="col-1" aria-labelledby="weatherDay1"></section>
                    <section class="col-2" aria-labelledby="weatherDay1">
                        <asp:Image ID="weatherImage1" runat="server" ImageAlign="Middle"/>
                        <p/>
                        <asp:Label ID="weatherDay1" runat="server" Text="" Width="150px" Height="200px"></asp:Label>
                    </section>
                    <section class="col-2" aria-labelledby="weatherDay2">
                        <asp:Image ID="weatherImage2" runat="server" ImageAlign="Middle"/>
                        <p/>
                        <asp:Label ID="weatherDay2" runat="server" Text="" Width="150px" Height="200px"></asp:Label>
                    </section>
                    <section class="col-2" aria-labelledby="weatherDay3">
                        <asp:Image ID="weatherImage3" runat="server" ImageAlign="Middle"/>
                        <p/>
                        <asp:Label ID="weatherDay3" runat="server" Text="" Width="150px" Height="200px"></asp:Label>
                    </section>
                    <section class="col-2" aria-labelledby="weatherDay4">
                        <asp:Image ID="weatherImage4" runat="server" ImageAlign="Middle"/>
                        <p/>
                        <asp:Label ID="weatherDay4" runat="server" Text="" Width="150px" Height="200px"></asp:Label>
                    </section>
                    <section class="col-2" aria-labelledby="weatherDay5">
                        <asp:Image ID="weatherImage5" runat="server" ImageAlign="Middle"/>
                        <p/>
                        <asp:Label ID="weatherDay5" runat="server" Text="" Width="150px" Height="200px"></asp:Label>
                    </section>
                    <section class="col-1" aria-labelledby="weatherDay1"></section>
                </div>
                <!-- 5-day Weather Forecast -->
            </div>
        </div>
        <!-- Weather Information Container Ends -->
        
        <!-- Right SideBar -->
        <div id="news" class="rightBar text-white">
            <!-- News Search Box and Search Button -->
            <div id="newsSearch">
                <asp:TextBox ID="txtNews" Placeholder="Search News" runat="server" />
                <asp:Button ID="btnNews" runat="server" Text="Search" OnClick="btnNews_Click" />
            </div>
            <!-- News Search Box and Search Button Ends -->
            
            <!-- News Links -->
            <asp:Panel ID="pnNews" runat="server">
                <asp:Label ID="lblNews" runat="server" Text=""></asp:Label> 
            </asp:Panel>
            <!-- News Links Ends -->
        </div>
        <!-- Right SideBar Ends -->


    </form>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
        <%: Scripts.Render("Member.js") %>
    </asp:PlaceHolder>
</body>
</html>
