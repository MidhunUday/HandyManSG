<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="business-signup.aspx.cs" Inherits="HandyManSG.business_signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" />
    <link rel="stylesheet" href="business-signup.css"/>
    <title></title>
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
                <h2>Business User Sign Up Page</h2>
                <div class="item"><i class="fa-solid fa-user"></i><asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-lock"></i><asp:TextBox ID="txtUserPassword" runat="server" placeholder="Password"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-building"></i><asp:TextBox ID="txtCompName" runat="server" placeholder="Company name"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-envelope"></i><asp:TextBox ID="txtCompEmail" runat="server" placeholder="Company email"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-phone"></i><asp:TextBox ID="txtPhoneNo" runat="server" placeholder="Company phone no"></asp:TextBox></div>
                <div class="item"><i class="fa-solid fa-location-dot"></i><asp:TextBox ID="txtCompAddress" runat="server" placeholder="Company address"></asp:TextBox></div>
                <div class="select">
                    <asp:DropDownList ID="ddlCompanyCategory" runat="server"  AutoPostBack="False"  EnableViewState = "True" OnSelectedIndexChanged="ddlCompanyCategory_SelectedIndexChanged">
                        <asp:ListItem Value="Aircon">Aircon</asp:ListItem>
                        <asp:ListItem Value="Cleaning">Cleaning</asp:ListItem>
                        <asp:ListItem Value="Electrical">Electrical</asp:ListItem>
                        <asp:ListItem Value="Plumbing">Plumbing</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="register-btn" OnClick="btnRegister_Click" />
                <asp:Button ID="btnBack" runat="server" Text="Back to Login" CssClass="register-btn" OnClick="btnBack_Click"  />
            </div>
        </main>
    </form>
</body>
</html>
