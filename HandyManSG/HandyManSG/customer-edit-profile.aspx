<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-edit-profile.aspx.cs" Inherits="HandyManSG.customer_edit_profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" />
    <link rel="stylesheet" href="customer-edit-profile.css"/>
    <title>My Profile</title>
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
            <h2>My Profile</h2>
            <div class="container">
                <div class="card">
                    <hr /><div class="card-item"><i class="fa-solid fa-id-badge"></i> Name:  <asp:TextBox ID="txtCusName" runat="server" CssClass="input"></asp:TextBox></div>
                    <hr /><div class="card-item"><i class="fa-solid fa-envelope"></i> Email:  <asp:TextBox ID="txtEmail" runat="server" CssClass="input"></asp:TextBox></div>
                    <hr /><div class="card-item"><i class="fa-solid fa-address-book"></i> Contact No:  <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="input"></asp:TextBox></div>
                    <hr /><div class="card-item"><i class="fa-solid fa-location-dot"></i> Address:  <asp:TextBox ID="txtCusAddress" runat="server" CssClass="input address"></asp:TextBox></div>
                    <hr /><asp:Button ID="btnSave" cssclass="btn-primary save-btn" runat="server" Text="Save" OnClick="btnSave_Click" />
                </div>
            </div>
        </main>
    </form>
</body>
</html>
