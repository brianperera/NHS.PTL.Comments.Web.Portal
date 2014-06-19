<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataEntryControl.ascx.cs"
    Inherits="UserControls_DataEntryControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="pageHeader">
    <h2>
        Data Entry
    </h2>
</div>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="pagedata">
            <div>
                <div class="grid_24 error_msg">
                    <asp:Label ID="MessageLabel" runat="server" />
                </div>
                <ul class="formSection">
                    <li><span class="formTitleFields">UniqueCDSRowIdentifier</span> <span class="formFieldControl uniqueIdentifier">
                        <asp:Literal runat="server" ID="uniqueIdentifier"></asp:Literal>
                    </span></li>
                    <li><span class="formTitleFields">Status</span> <span class="formFieldControl">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="defaultDropDown">
                        </asp:DropDownList>
                    </span></li>
                    <li><span class="formTitleFields">Appointment Date</span> <span class="formFieldControl">
                        <asp:TextBox ID="appointmentDateTextbox" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                        <asp:CalendarExtender Animated="true" Format="dd/MM/yyyy" ID="CalendarExtender1"
                            TargetControlID="appointmentDateTextbox" runat="server" ViewStateMode="Enabled" />
                    </span></li>
                    <li><span class="formTitleFields">Comment</span> <span class="formFieldControl">
                        <asp:TextBox ID="commentTextbox" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </span></li>
                    <li><span>
                        <asp:Button CssClass="submitButton" Text="Submit" runat="server" ID="submitButton" />
                        <asp:HiddenField runat="server" ID="actionHiddenField" />
                    </span></li>
                </ul>
                <div class="subSections regularTable">
                    <div class="gridOuter">
                        <asp:GridView ID="commentsGrid" AutoGenerateColumns="False" runat="server" CellPadding="3"
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            CssClass="grid" AllowSorting="True" EmptyDataText="No matching records found"
                            ShowHeaderWhenEmpty="true" ShowHeader="true">
                            <Columns>
                                <asp:BoundField HeaderText="UniqueCDSRowIdentifier" />
                                <asp:BoundField HeaderText="Status" />
                                <asp:BoundField HeaderText="Appointment Date" />
                                <asp:BoundField HeaderText="Comment" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
