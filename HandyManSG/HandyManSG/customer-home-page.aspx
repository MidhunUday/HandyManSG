<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-home-page.aspx.cs" Inherits="HandyManSG.customer_home_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="customer-home-page-css.css"/>
    <title>Home</title>
</head>
<body>
    <form id="form1" runat="server">
        <nav>
            <ul class="nav-list">
                <li class="nav-item"><a href="customer-home-page.aspx"><h1>Handyman</h1></a></li>
                <li class="nav-item"><a href="customer-home-page.aspx">Home</a></li>
                <li class="nav-item"><button class="btn-primary">My Account <svg width="18" height="18" fill="currentColor" class="bi bi-person-fill" viewBox="0 -2 16 16" id="IconChangeColor"> <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z" id="mainIconPathAttribute"></path></svg></button>
                    <ul style="background-color:white;">
                        <li><a href="customer-edit-profile.aspx">My Profile</a></li>
                        <li><a href="customer-view-new-bookings.aspx">My Bookings</a></li>
                        <li><a href="UserLogin.aspx">Sign Out</a></li>
                    </ul>
                </li>
            </ul>
        </nav>
        <main>
            <!--card-one-->
            <div class="container">
                <div class="card">
                    <div class="imgBx"><img src="Images/plumbing.png" /></div>
                    <div class="contentBx"><h2>Plumbing Service</h2><asp:LinkButton ID="lnkPlumbing" class="btn-primary" runat="server" CommandArgument='Plumbing' OnClick="lnkPlumbing_Click" Text="View Services"> </asp:LinkButton> </div>
                </div>
            </div>
            <!--card-two-->
            <div class="container">
                <div class="card">
                    <div class="imgBx"><img src="Images/cleaning.png" /></div>
                    <div class="contentBx"><h2>Cleaning Service</h2><asp:LinkButton ID="lnkCleaning" class="btn-primary" runat="server" CommandArgument='Cleaning' OnClick="lnkCleaning_Click" Text="View Services"> </asp:LinkButton></div>
                </div>
            </div>
            <!--card-three-->
            <div class="container">
                <div class="card">
                    <div class="imgBx"><img src="Images/electrical.png" /></div>
                    <div class="contentBx"><h2>Electrical Service</h2><asp:LinkButton ID="lnkElectrical" class="btn-primary" runat="server" CommandArgument='Electrical' OnClick="lnkElectrical_Click" Text="View Services"> </asp:LinkButton></div>
                </div>
            </div>
            <!--card-four-->
            <div class="container">
                <div class="card">
                    <div class="imgBx"><img src="Images/aircond-service.png" /></div>
                    <div class="contentBx"><h2>Aircon Service</h2><asp:LinkButton ID="lnkAircon" class="btn-primary" runat="server" CommandArgument='Aircon' OnClick="lnkAircon_Click" Text="View Services"> </asp:LinkButton></div>
                </div>
            </div>
        </main>
    </form>
</body>
</html>
