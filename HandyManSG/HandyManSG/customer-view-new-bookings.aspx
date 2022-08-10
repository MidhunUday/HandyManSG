<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-view-new-bookings.aspx.cs" Inherits="HandyManSG.customer_view_new_bookings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" />
    <link rel="stylesheet" href="customer-view-new-bookings.css"/>
    <title>New Bookings</title>
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
                <li class="main-item btn-primary"><a href="customer-view-new-bookings.aspx">New Bookings</a></li>
                <li class="main-item"><a href="customer-view-approved-bookings.aspx">Pending Bookings</a></li>
                <li class="main-item"><a href="customer-view-completed-bookings.aspx">Completed Bookings</a></li>
            </ul>
            <asp:GridView ID="gridViewNewBookings" runat="server" AutoGenerateColumns="False" OnPageIndexChanging = "OnPageIndexChanging" HeaderStyle-BorderStyle="None" AllowPaging="True" BorderStyle="None" PageSize="3" style="margin-left: 100px">
                <Columns>
                    <asp:TemplateField ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" ItemStyle-Height="300">
                        <ItemTemplate>
                            <div class="container">
                                    <div class="card">
                                        <div class="left-card">
                                            <div class="card-item">Booking ID: <asp:Label ID="lblBookingID" Text='<%# Eval("BookingID") %>' runat="server" ></asp:Label></div>
                                            <div class="card-item">Title: <asp:Label ID="lblserviceTitle" runat="server" Text='<%# Eval("serviceTitle") %>' Font-Bold="True" Font-Size="Larger"></asp:Label></div>
                                            <div class="card-item">Company: <asp:Label ID="lblcompanyName" runat="server" Text='<%# Eval("companyName") %>' Font-Italic="True"></asp:Label></div>
                                            <div class="card-item"><i class="fa-solid fa-calendar"></i> Appointment Date and Time: <asp:Label ID="lblappoointmentDateTime" runat="server" Text='<%# Eval("appoointmentDateTime") %>'></asp:Label></div>
                                        </div>
                                        <div class="center-card">
                                            <div class="jobdetails"><asp:Label ID="lblcustomerComment" runat="server" Text='<%# Eval("customerComment") %>' Enabled="False"></asp:Label></div>
                                        </div>
                                        <div class="right-card">
                                            <asp:LinkButton ID="lnkEditBooking" runat="server" CommandArgument='<%# Eval("BookingID") %>' Text="Edit Booking"   CssClass="btn-primary" style="border-radius: 40px; padding: 1rem; margin: 0.2rem;" OnClick="lnkEDIT_Click" >   </asp:LinkButton>
                                            <asp:LinkButton ID="lnlCancelBooking" runat="server" CommandArgument='<%# Eval("BookingID") %>' Text="Delete Booking"   CssClass="btn-primary" style="border-radius: 40px; padding: 1rem; margin: 0.2rem;" OnClick="lnkDelete_Click" >   </asp:LinkButton>
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
