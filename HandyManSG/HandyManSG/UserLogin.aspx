<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="HandyManSG.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" />
    <link rel="stylesheet" href="UserLogin.css"/>
    <title>Login Page</title>
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
                <h2>Login Page</h2>
                <div class="item"><i class="fa-solid fa-user"></i><asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-lock"></i><asp:TextBox ID="txtUserPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox></div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="login-btn" OnClick="btnLogin_Click" />
                <asp:Button ID="btnSignUpCus" runat="server" Text="Sign Up As Customer" CssClass="sign-up-btn" OnClick="btnSignUpCus_Click" />
                <asp:Button ID="btnSignUpBus" runat="server" Text="Sign Up As Business" CssClass="sign-up-btn" OnClick="btnSignUpBus_Click" />
            </div>
        </main>
    </form>
</body>
</html>
