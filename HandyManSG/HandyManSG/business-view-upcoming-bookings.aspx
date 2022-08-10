<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="business-view-upcoming-bookings.aspx.cs" Inherits="HandyManSG.business_view_upcoming_bookings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="business-view-upcoming-bookings.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin='anonymous'></script>
</head>
<body>
    <form id="form1" runat="server">
           <div class="container">
             <div style=" font-size:larger; padding-left:17px;"><h1>HANDYMAN</h1></div>
                  <div class="menucontainer">
                      <div class="dropdown">
                            <a class="dropbtn" href="#myaccount">My Account  <i class='far fa-user'></i></a>
                              <div class="dropdown-content">
                              <a href="business-edit-profile.aspx">My Profile</a>
                              <a href="business-view-new-bookings.aspx">My Bookings</a>                            
                              <a href="UserLogin.aspx">Sign Out</a>
                              </div>
                      </div>
                  <div class="menucontainer"><a href="business-view-all-services.aspx">View My Services</a></div>   
                  <div class="menucontainer"><a href="business-home.aspx">Home</a></div>
           </div>
          </div>
        <br />

        <!--Booking table-->
        <div class="main">
        <br />
        <h2 class="bookingtitle">My Booking</h2>

        <ul class="bookingmenu">
        <li style="width:220px;"><a href="business-view-new-bookings.aspx"><img src="Images/booking.png" width="50" height="50"/><br />New Bookings</a></li>
        <li style="width:220px;"><a href="business-view-upcoming-bookings.aspx" style="color:#4169E1;"><img src="Images/calendar (1).png" width="50" height="50"/><br />Upcoming Bookings</a></li>
        <li style="width:220px;"><a href="business-view-completed-bookings.aspx"><img src="Images/calendar.png" width="50" height="50"/><br />Completed Bookings</a></li>
        </ul>
<br />
            
        <div class="tablesection">
            <asp:GridView ID="gridViewUpcomingBookings" runat="server"
                CssClass="mybookingdatagrid" PagerStyle-CssClass="bookingpager"
 HeaderStyle-CssClass="bookingheader" RowStyle-CssClass="bookingrows" AllowPaging = "True"  OnPageIndexChanging = "OnPageIndexChanging"  PageSize="5"  AutoGenerateColumns="False" CellPadding="8" ShowHeaderWhenEmpty="True">
                <Columns>
                    
                   
                    <asp:BoundField DataField ="BookingID" HeaderText="Booking ID" >
                    <HeaderStyle Width="12%" />
                    </asp:BoundField>

                    <asp:BoundField DataField ="customerName" HeaderText="Customer Name" >
                    <HeaderStyle Width="14%" />
                    </asp:BoundField>

                    <asp:BoundField DataField ="customerComment" HeaderText="Customer Comment" >
                    <HeaderStyle Width="17%" />
                    </asp:BoundField>

                    <asp:BoundField DataField ="appoointmentDateTime" HeaderText="Appointment Date" >
                    <HeaderStyle Width="13%" />
                    </asp:BoundField>
                  

                    <asp:BoundField DataField ="customerAddress" HeaderText="Customer Address" >
                    <HeaderStyle Width="20%" />
                    </asp:BoundField>


                   <asp:TemplateField HeaderText="Action" >
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="viewbutton" Text="View" OnClick="lnkEDIT_Click" CommandName="View" CommandArgument='<%# Eval("BookingID")%>'>                        
                            </asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="8%" />
                    </asp:TemplateField>



                </Columns>



                <emptydatarowstyle CssClass="emptyrow" backcolor="white" BorderColor="white" borderstyle="none" 
          forecolor="black" Font-Size="Large" Font-Italic="true" Height="50px"/> 
                <EmptyDataTemplate>No Pending Booking</EmptyDataTemplate>
<HeaderStyle CssClass="bookingheader"></HeaderStyle>

<PagerStyle CssClass="bookingpager"></PagerStyle>

<RowStyle CssClass="bookingrows"></RowStyle>



            </asp:GridView>
          </div>
            </div>
        <br />

    </form> 
</body>
</html>

