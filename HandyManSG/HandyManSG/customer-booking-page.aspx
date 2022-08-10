<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-booking-page.aspx.cs" Inherits="HandyManSG.customer_booking_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" />
    <link rel="stylesheet" href="customer-booking-page-css.css"/>
    <title>Book services page</title>
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
            <h2>Booking Details</h2>
            <div class="container">
                <div class="card">
                    <div class="left-card">
                        <div class="card-item">Title:<asp:Label ID="lblServiceTitle" runat="server" Text="aircond chemical washing" Font-Bold="True" Font-Size="Larger"></asp:Label></div>
                        <div class="card-item"><asp:Label ID="lblCompName" runat="server" Text="Company name" Font-Italic="True" Font-Size="Larger"></asp:Label></div>
                        <div class="card-item"><i class="fa-solid fa-location-dot"></i> Address: <asp:Label ID="lblCompAddress" runat="server" Text="Blk123, 123456"></asp:Label></div>
                        <p>Service Description: </p>
                        <asp:TextBox ID="txtServiceDetails" class="description" runat="server" Rows="4" ReadOnly="True" TextMode="MultiLine" Font-Size="X-Large"></asp:TextBox>
                    </div>
                    <div class="right-card">
                        <div class="card-item"><i class="fa-solid fa-star yellow"></i> Rating: <asp:Label ID="txtCompRating" runat="server" Text="4.5"></asp:Label></div>
                        <asp:TextBox ID="txtUserComment" class="description" runat="server" TextMode="MultiLine" Rows="4" Font-Size="X-Large" placeholder="Please comment"></asp:TextBox>
                        <div class="date-and-time"><i class="fa-solid fa-calendar-check"></i> Select Booking Date-Time: </div>
                        <asp:TextBox ID="txtBookingDate" class="date-and-time" runat="server" Text='<%# Bind ("bookingDate","{0: dd-MM-yyyy}")%>' textmode="Date"  ></asp:TextBox>
                        <asp:TextBox ID="txtBookingTime" class="date-and-time" runat="server" Text='<%# Bind ("bookingTime","{HH:mm}")%>' textmode="Time"  ></asp:TextBox>                       
                        <asp:Button ID="btnMakeBooking" runat="server" Text="Make Booking" CssClass="btn-primary btn" OnClick="btnMakeBooking_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-primary btn" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </main>
    </form>
</body>
</html>
