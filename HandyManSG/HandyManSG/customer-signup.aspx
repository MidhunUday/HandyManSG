<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-signup.aspx.cs" Inherits="HandyManSG.customer_signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" />
    <link rel="stylesheet" href="customer-signup.css"/>
    <title>Customer Sign up Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <nav>
            <ul class="nav-list">
                <li class="nav-item"><h1>HandymanSG</h1></li>
            </ul>
        </nav>
        <main>
            <div class="container">
                <h2>Customer Sign Up Page</h2>
                <div class="item"><i class="fa-solid fa-user"></i><asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-lock"></i><asp:TextBox ID="txtUserPassword" runat="server" placeholder="Password"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-id-card"></i><asp:TextBox ID="txtCusName" runat="server" placeholder="Name"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-envelope"></i><asp:TextBox ID="txtCusEmailID" runat="server" placeholder="Email"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-phone"></i><asp:TextBox ID="txtPhoneNumber" runat="server" placeholder="Phone no"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-location-dot"></i><asp:TextBox ID="txtAddress" runat="server" placeholder="Home address"></asp:TextBox></div>
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="register-btn" OnClick="btnRegister_Click" />
                <asp:Button ID="btnBack" runat="server" Text="Back to Login" CssClass="register-btn" OnClick="btnBack_Click"  />
            </div>
        </main>
    </form>
</body>
</html>
