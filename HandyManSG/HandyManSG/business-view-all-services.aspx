<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="business-view-all-services.aspx.cs" Inherits="HandyManSG.business_view_all_services" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="business-view-all-services.css" />
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

        <!--Service table-->
        <div class="main">
        <br />
        <h2 class="servicetitle">My Services</h2>

        <ul class="servicemenu">
        <li style="width:200px;"><a href="business-view-all-services.aspx" style="color:#4169E1;"><img src="Images/repairing-service.png" width="50" height="50"/><br />My Services</a></li>
        <li style="width:200px;" ><a href="business-add-new-service.aspx" ><img src="Images/plus.png" width="50" height="50"/><br />Add New Service</a></li>
        </ul>
<br />
            
            <!--table-Gridview-->
        <div class="servicetablesection">
            <asp:GridView ID="gridViewServices" runat="server"
                CssClass="myservicedatagrid" PagerStyle-CssClass="servicepager"
  HeaderStyle-CssClass="serviceheader" RowStyle-CssClass="servicerows" AllowPaging = "True"  OnPageIndexChanging = "OnPageIndexChanging"  PageSize="5"  AutoGenerateColumns="False" CellPadding="8" OnSelectedIndexChanged="gridViewServices_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                
                <Columns>
                    
                    <asp:BoundField DataField ="ServiceID" HeaderText="Service ID" >
                    <HeaderStyle Width="14%" />
                    </asp:BoundField>

                     <asp:BoundField DataField ="serviceTitle" HeaderText="Title" >
                    <HeaderStyle Width="17%" />
                    </asp:BoundField>

                    <asp:BoundField DataField ="serviceDesc" HeaderText="Description" >
                    <HeaderStyle Width="33%" />
                    </asp:BoundField>

                    <asp:BoundField DataField ="servicePrice" HeaderText="Booking Price Range($)" >
                    <HeaderStyle Width="12%" />
                    </asp:BoundField>


                    <asp:TemplateField HeaderText="Action" >
                        <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="viewbutton2" CommandArgument='<%# Eval("ServiceID") %>' Text="Edit Service"   OnClick="lnkEDIT_Click" >  </asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="" >
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="deletebutton1" CommandArgument='<%# Eval("ServiceID") %>' Text="Edit Booking"   OnClick="lnkDelete_Click" >
                                <img src="Images/delete.png" alt="delete" width="25" height="25" style="vertical-align:top;"/></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="8%" />
                    </asp:TemplateField>

                </Columns>


                <emptydatarowstyle CssClass="emptyrow" backcolor="white" BorderColor="white" borderstyle="none" 
          forecolor="black" Font-Size="Large" Font-Italic="true" Height="50px"/> 
                <EmptyDataTemplate>No Service Listed</EmptyDataTemplate>
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
