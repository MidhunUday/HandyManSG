<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="business-home.aspx.cs" Inherits="HandyManSG.business_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="business-home.css" />
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


        <!--Homepage div1-->
        <div class="homemain1">
        <br />
        <div class="homemain1left">
            <img class= "image1" src="Images/business_home_bg_img2.jpg" height="500"/>
        </div>
        <div class="homemain1right">
            <div class="box1">
                <div>You have </div><br />
                <asp:TextBox cssclass="counttextbox" ID="txtNewBookingCount" Text="0" runat="server" Width="100px" Height="60px" disabled="disabled" BorderStyle="none" font-size="50pt"></asp:TextBox>
                <br />   
                <br />   
                <div>new bookings today. </div>
                <br />   
                <asp:HiddenField ID="TextBoxbusinessUserID" runat="server" />
                <asp:Button CssClass="checknowbutton1" ID="CheckNowButton1" Text="Check Now" runat="server" OnClick="CheckNowButton1_Click" > </asp:Button>
            </div>
        </div>  
       </div>
        <br />


        <!--Homepage div2-->
       <div class="homemain2">
       <br />
        <div class="homemain2left">
            <div class="box2">
                <img src="Images/business_home_plumber_img.png" width="100" height="100"/>
                <br />
                <br /> 
                <div>Got new exciting    <br />    service to share? </div>

                  <br /> 
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:Button CssClass="clickherebutton1" ID="btnaddNewService" Text="Click Here" runat="server" OnClick="btnaddNewService_Click" > </asp:Button>
            </div>
        </div>
        <div class="homemain2right">
            <img class= "image2" src="Images/business_home_bg_img.jpg" height="500"/>
        </div>
      </div>

    </form> 
</body>
</html>
