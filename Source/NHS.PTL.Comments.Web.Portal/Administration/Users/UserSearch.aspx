<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UserSearch.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.UserSearch" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="main">
        <div class="sections">
            <div class="pageHeader">
                <h2 runat="server">
                    User Search
                </h2>
            </div>
            <div class="sections">
                <div>
                    <asp:Label ID="Msg" ForeColor="maroon" runat="server" />
                </div>
                <div class="gridtitle">
                    <h3>User(s) Search Result</h3>
                </div>
                <div class="subSections regularTable">
                    <asp:GridView ID="Users_ListBox" AutoGenerateColumns="False" runat="server" 
                        CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                        BorderWidth="1px" CssClass="grid">
                        <Columns>
                            <asp:HyperLinkField HeaderText="User Name" datatextfield="UserName" DataNavigateUrlFormatString="UserAccount.aspx?action=update&username={0}"
                                DataNavigateUrlFields="UserName" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <AlternatingRowStyle CssClass="altrow" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
