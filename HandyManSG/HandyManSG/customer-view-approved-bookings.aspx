<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-view-approved-bookings.aspx.cs" Inherits="HandyManSG.customer_view_approved_bookings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" />
    <link rel="stylesheet" href="customer-view-approved-bookings.css"/>
    <title>Pending Bookings</title>
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
            <ul class="main-list">
                <li class="main-item"><a href="customer-view-new-bookings.aspx">New Bookings</a></li>
                <li class="main-item btn-primary"><a href="customer-view-approved-bookings.aspx">Pending Bookings</a></li>
                <li class="main-item"><a href="customer-view-completed-bookings.aspx">Completed Bookings</a></li>
            </ul>
            <asp:GridView ID="gridViewApprovedBookings" runat="server" AutoGenerateColumns="False" OnPageIndexChanging = "OnPageIndexChanging" BorderStyle="None" AllowPaging="True" PageSize="3" style="margin-left: 100px" >
                <Columns>
                    <asp:TemplateField HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None" ItemStyle-Height="300">
                        <ItemTemplate>
                           <div class="container">
                                    <div class="card">
                                        <div class="left-card">
                                            <div class="card-item">Title: <asp:Label ID="Label3" runat="server" Text='<%# Eval("serviceTitle") %>' Font-Size="Larger" Font-Bold="True"></asp:Label></div>
                                            <div class="card-item">Company: <asp:Label ID="Label1" runat="server" Text='<%# Eval("companyName") %>' Font-Italic="True"></asp:Label></div>
                                            <div class="card-item"><i class="fa-solid fa-calendar"></i> Appointment Date and Time: <asp:Label ID="Label2" runat="server" Text='<%# Eval("appoointmentDateTime") %>'></asp:Label></div>
                                            <div class="card-item"><i class="fa-solid fa-dollar-sign"></i> Booking Price: <asp:Label ID="Label5" runat="server" Text='<%# Eval("bookingPrice") %>'></asp:Label></div>
                                        </div>
                                        <div class="center-card">
                                            <div class="jobdetails"><asp:Label ID="Label4" runat="server" Text='<%# Eval("customerComment") %>'></asp:Label></div>
                                        </div>
                                        <div class="right-card">
                                            <asp:LinkButton ID="lnlCancelBooking" runat="server" CssClass="btn-primary delete-button" CommandArgument='<%# Eval("BookingID") %>'  Text="Delete Booking"  OnClick="lnkDelete_Click" ></asp:LinkButton>
                                        </div>
                                    </div>
                                </div> 
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
        </main>
    </form>
</body>
</html>
