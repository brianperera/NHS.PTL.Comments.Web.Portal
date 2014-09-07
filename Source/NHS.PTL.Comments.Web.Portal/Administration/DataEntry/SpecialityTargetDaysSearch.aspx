<%@ Page Title="Speciality Target Days Search" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SpecialityTargetDaysSearch.aspx.cs" Inherits="Nhs.Ptl.Comments.SpecialityTargetDaysSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="main">
        <div class="pageHeader">
            <h2>
                Statuses
            </h2>
        </div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <div>
            <div class="gridtitle">
                <h3>Speciality Target Days</h3>
            </div>
            <div class="subSections regularTable">
                <asp:GridView ID="Status_Grid" Width="50%" AutoGenerateColumns="False" runat="server" CellPadding="3"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                    CssClass="grid" AllowPaging="True" AllowSorting="True" EnableSortingAndPagingCallbacks="True"
                    PageSize="50">
                    <Columns>
                        <asp:HyperLinkField HeaderText="Spec Code" DataTextField="ID" DataNavigateUrlFormatString="SpecialityTargetDays.aspx?action=update&id={0}"
                            DataNavigateUrlFields="ID" />
                        <asp:BoundField HeaderText="Speciality" DataField="Speciality" />
                        <asp:BoundField HeaderText="Target Days" DataField="TargetDay" />                      
                        <asp:BoundField HeaderText="State" DataField="DisplayActivate" />                      
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle CssClass="altrow" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
