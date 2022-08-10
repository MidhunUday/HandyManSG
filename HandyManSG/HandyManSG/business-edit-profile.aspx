<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="business-edit-profile.aspx.cs" Inherits="HandyManSG.business_edit_profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="business-edit-profile.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin='anonymous'></script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 750px;
        }
        .auto-style3 {
            width: 750px;
            height: 27px;
        }
        .auto-style9 {
            height: 38px;
            padding-top: 15px;
            padding-left: 90px;
            width: 750px;
        }
        .auto-style11 {
            height: 32px;
        }
        .auto-style12 {
            height: 27px;
        }
        </style>
</head>
<body style="height: 860px">
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

        <!--My Account-->
        <div class="main">
        <br />
        <h2 class="bookingtitle">My Account</h2>
            <!--account details-->
        <div class="account-container">

            <table class="auto-style1">
                <tr>
                    <td class="myaccounttitle">Company Name</td>
                    <td class="auto-style11">

                    </td>
                </tr>
                <tr>
                    <td class="textboxtoadjust3">
                        <asp:TextBox ID="txtCompName" runat="server" Width="600px" Height="28px"></asp:TextBox>
                    </td>
                    <td rowspan="10" class="mypictab">
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="myaccounttitle">Service Category</td>
                </tr>
                <tr>
                    <td class="textboxtoadjust3">
                        <asp:TextBox ID="txtCompServiceCat" runat="server" Width="600px" Height="28px" disabled="disabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                </tr>
                <tr>
                    <td class="myaccounttitle">Contact Number</td>
                </tr>
                <tr>
                    <td class="textboxtoadjust3">
                        <asp:TextBox ID="txtContactNumber" runat="server" Width="600px" Height="28px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="myaccounttitle">Email Address</td>
                </tr>
                <tr>
                    <td class="textboxtoadjust3">
                        <asp:TextBox ID="txtCompEmail" runat="server" Width="600px" Height="28px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="myaccounttitle">Company Address</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="companyaddresstextbox">
                        <asp:TextBox cssclass="companyaddresstextbox" ID="txtCompAddress" runat="server" Width="598px" Height="60px" TextMode="MultiLine" Wrap="True" BorderStyle="Solid" BorderColor="Gray" ></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3"></td>
                    <td class="auto-style12"></td>
                </tr>
                <tr>
                    <td class="auto-style9">
                        <asp:Button ID="btnSave" cssclass="savebutton1" runat="server" Text="Save" OnClick="btnSave_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
          </div>
            </div>

    </form> 
</body>
</html>

