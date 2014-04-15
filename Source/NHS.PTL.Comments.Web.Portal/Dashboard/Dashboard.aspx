<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="main">
        <div class="pageHeader">
            <h2>
                PTL Comments Dashboard
            </h2>
        </div>
        <div>
            <div class="filterContainer">
                <div>
                    <ul class="formSection">
                        <li><span class="formTitleFields">Patient</span> <span class="formFieldControl">
                            <asp:TextBox ID="patientTextbox" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                        </span></li>
                        <li><span class="formTitleFields">Speciality</span> <span class="formFieldControl">
                            <asp:DropDownList ID="specialityDropdown" runat="server" CssClass="defaultDropDown">
                            </asp:DropDownList>
                        </span></li>
                        <li><span class="formTitleFields">Consultant</span> <span class="formFieldControl">
                            <asp:DropDownList ID="consultantDropdown" runat="server" CssClass="defaultDropDown">
                            </asp:DropDownList>
                        </span></li>
                        <li><span class="formTitleFields">Status</span> <span class="formFieldControl">
                            <asp:DropDownList ID="statusDropdown" runat="server" CssClass="defaultDropDown">
                            </asp:DropDownList>
                        </span></li>
                        <li><span>
                            <asp:Button CssClass="submitButton" Text="Search" runat="server" ID="searchButton" />
                        </span></li>
                    </ul>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="subSections regularTable">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="commentsGrid" AutoGenerateColumns="False" runat="server" CellPadding="3"
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            CssClass="grid" AllowSorting="True" EmptyDataText="No matching records found"
                            ShowHeaderWhenEmpty="true" ShowHeader="true">
                            <Columns>
                                <asp:BoundField HeaderText="UniqueCDSRowIdentifier" />
                                <asp:BoundField HeaderText="Patient Pathway Identifier" />
                                <asp:BoundField HeaderText="MRN" />
                                <asp:BoundField HeaderText="NHS Number" />
                                <asp:BoundField HeaderText="MRN" />
                                <asp:BoundField HeaderText="Date Of Birth" />
                                <asp:BoundField HeaderText="Patient Forename" />
                                <asp:BoundField HeaderText="Patient Surname" />
                                <asp:BoundField HeaderText="Spec Code" />
                                <asp:BoundField HeaderText="Spec Name" />
                                <asp:BoundField HeaderText="Division" />
                                <asp:BoundField HeaderText="Consultant" />
                                <asp:BoundField HeaderText="Referral Date" />
                                <asp:BoundField HeaderText="Source Of Referral" />
                                <asp:BoundField HeaderText="Priority Type" />
                                <asp:BoundField HeaderText="RTT Clock Start" />
                                <asp:BoundField HeaderText="RTT Breach Date" />
                                <asp:BoundField HeaderText="Attendance Date" />
                                <asp:BoundField HeaderText="Attendance Status" />
                                <asp:BoundField HeaderText="RTT Status Code" />
                                <asp:BoundField HeaderText="RTT Status" />
                                <asp:BoundField HeaderText="Waiting List Status" />
                                <asp:BoundField HeaderText="Future Clinic Date" />
                                <asp:BoundField HeaderText="Wait At Future Clinic Date" />
                                <asp:BoundField HeaderText="Status" />
                                <asp:BoundField HeaderText="Appointment Date" />
                                <asp:BoundField HeaderText="Updated Date" />
                                <asp:BoundField HeaderText="Comment" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="gridPagerStyle" />
                            <RowStyle ForeColor="#000066" />
                            <AlternatingRowStyle CssClass="altrow" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
