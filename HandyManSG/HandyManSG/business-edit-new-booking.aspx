<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="business-edit-new-booking.aspx.cs" Inherits="HandyManSG.business_edit_new_booking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="business-edit-new-booking.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin='anonymous'></script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style11 {
            width: 370px;
        }
        .auto-style12 {
            width: 370px;
            height: 38px;
        }
        .auto-style17 {
            width: 370px;
        }
        .auto-style18 {
            width: 370px;
            height: 38px;
        }
        .auto-style21 {
            width: 370px;
        }

        .auto-style23 {
            width: 370px;
            height: 26px;
        }
        .auto-style24 {
            margin-left: 42px;
        }
        </style>
</head>
<body style="height: 975px">
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
        <li style="width:220px;"><a href="business-view-upcoming-bookings.aspx"><img src="Images/calendar (1).png" width="50" height="50"/><br />Upcoming Bookings</a></li>
        <li style="width:220px;"><a href="business-view-completed-bookings.aspx"><img src="Images/calendar.png" width="50" height="50"/><br />Completed Bookings</a></li>
        </ul>
<br />
       <div class="viewbookingtable">
           <table class="auto-style1">
               <tr>
                   <td class="tableheader" colspan="3">New Booking</td>
               </tr>
               <tr class="unusedbookingrow">
                   <td class="auto-style11"></td>
                   <td class="auto-style17"></td>
                   <td class="auto-style21"></td>
               </tr>
               <tr class="bookingtitlerow">
                   <td class="bookingtitle2">Booking ID</td>
                   <td class="bookingtitle2">Booking Creation Date</td>
                   <td class="bookingtitle2">User Comment</td>
               </tr>
               <tr>
                   <td class="textboxtoadjust">
                       <asp:TextBox ID="txtBookingID" runat="server" CssClass="auto-style24" Width="320px" disabled="disabled"></asp:TextBox>
                   </td>
                   <td class="textboxtoadjust">
                       <asp:TextBox ID="txtBookingCreationDate" runat="server" Width="320px" disabled="disabled"></asp:TextBox>
                   </td>
                   <td class="textboxtoadjust">
                       <asp:TextBox ID="txtUserComment" runat="server" Width="320px" disabled="disabled"></asp:TextBox>
                   </td>
               </tr>
               <tr class="unusedbookingrow2">
                   <td class="auto-style11"></td>
                   <td class="auto-style17"></td>
                   <td class="auto-style21"></td>
               </tr>
               <tr>
                   <td class="bookingtitle2">Appointment Date</td>
                   <td class="bookingtitle2">Customer Name</td>
                   <td class="bookingtitle2">Customer Address</td>
               </tr>
               <tr>
                   <td class="textboxtoadjust">
                       <asp:TextBox ID="txtAppointmentDate" runat="server" Width="320px" disabled="disabled"></asp:TextBox>
                   </td>
                   <td class="textboxtoadjust">
                       <asp:TextBox ID="txtCusName" runat="server" Width="320px" disabled="disabled"></asp:TextBox>
                   </td>
                   <td class="addresstextbox">
                       <asp:TextBox CssClass="addresstextbox" ID="txtCusAddress" runat="server" Width="320px" Height="60px" TextMode="MultiLine" Wrap="True" disabled="disabled" BorderStyle="Solid" BorderColor="Gray"></asp:TextBox>
                   </td>
               </tr>
               <tr class="unusedbookingrow3">
                   <td class="auto-style11"></td>
                   <td class="auto-style17"></td>
                   <td class="auto-style21"></td>
               </tr>
               <tr>
                   <td class="bookingtitle2">Final Price ($)</td>
                   <td class="bookingtitle2">Change Booking Status</td>
                   <td class="auto-style23"></td>
               </tr>
               <tr>
                   <td class="textboxtoadjust">
                       <asp:TextBox ID="txtBookingFinalPrice" runat="server" Width="320px"></asp:TextBox>
                   </td>
                   <td class="statusbar">
                       <asp:DropDownList class="statusdropdown" ID="ddlChangeBookingStatus" runat="server" DataValueField="status" DataTextField="status" AutoPostBack="False" OnSelectedIndexChanged="ddlChangeBookingStatus_SelectedIndexChanged" Height="38px" Width="253px" EnableViewState = "True">
                           <asp:ListItem Value="Approved">Approve</asp:ListItem>
                           <asp:ListItem Value="Declined">Decline</asp:ListItem>
                       </asp:DropDownList>
                   </td>
                   <td class="auto-style22"></td>
               </tr>
               <tr>
                   <td class="auto-style12"></td>
                   <td class="auto-style18"></td>
                   <td class="tdcancelandupdatebutton">
                       <asp:LinkButton CssClass="cancelbutton1" ID="btnCancel" runat="server" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
                       <asp:LinkButton CssClass="updatebutton1" ID="btnUpdateBooking" runat="server" OnClick="btnUpdateBooking_Click">Update Booking</asp:LinkButton>
                   </td>
               </tr>
           </table>
           </div>
             </div>
       <br />
   </form> 
</body>
</html>
