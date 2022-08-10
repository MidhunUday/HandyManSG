<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-view-services.aspx.cs" Inherits="HandyManSG.customer_view_services" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,700" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" />
    <link rel="stylesheet" href="customer-view-services-css.css"/>
    <title>View Booking</title>
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
            <asp:GridView ID="gridViewShowServices" runat="server" AutoGenerateColumns="False"  BorderStyle="None" OnPageIndexChanging = "OnPageIndexChanging"  OnSelectedIndexChanged="gridViewShowServices_SelectedIndexChanged" AllowPaging="True" PageSize="5" style="margin-top: 0px">
                <Columns>
                    <asp:TemplateField ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" HeaderStyle-Height="70px" ItemStyle-Height="250px">
                        <HeaderTemplate><asp:Label ID="Label6" runat="server" Text="Booking Service" Font-Size="X-Large"></asp:Label></HeaderTemplate>
                        <ItemTemplate>
                           <div class="container">
                               <div class="card">
                                   <div class="left-card">
                                       <div class="card-item"><asp:Label ID="Label3" runat="server" Text='<%# Eval("serviceTitle") %>' Font-Bold="True" Font-Size="Larger"></asp:Label></div>
                                       <div class="card-item"><asp:Label ID="Label1" runat="server" Text='<%# Eval("companyName") %>' Font-Italic="True"></asp:Label></div>
                                       <div class="card-item"><i class="fa-solid fa-address-book"></i> Address: <asp:Label ID="Label2" runat="server" Text='<%# Eval("companyAddress") %>'></asp:Label></div>
                                   </div>
                                   <div class="right-card">
                                       <div class="card-item"><i class="fa-solid fa-star yellow"></i> Rating: <asp:Label ID="Label4" runat="server" Text='<%# Eval("companyRating") %>'></asp:Label></div>
                                       <div class="card-item"><i class="fa-solid fa-dollar-sign"></i> Price range: <asp:Label ID="Label5" runat="server" Text='<%# Eval("servicePrice") %>'></asp:Label></div>
                                       <div class="card-item"><asp:LinkButton ID="lnkView" class="btn-primary" runat="server" CommandArgument='<%# Eval("ServiceID") %>' OnClick="lnkView_Click" Text="Book" Width="200" Height="40"></asp:LinkButton></div>
                                   </div>
                               </div>
                           </div>
                        </ItemTemplate>

<HeaderStyle BorderStyle="None" Height="70px"></HeaderStyle>

<ItemStyle BorderStyle="None" Height="250px"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
        </main>
    </form>
</body>
</html>
