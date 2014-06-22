<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataEntryControl.ascx.cs"
    Inherits="UserControls_DataEntryControl" ViewStateMode="Enabled" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="pageHeader">
            <h2 style="float: left;">
                Data Entry
            </h2>
            <asp:LinkButton ID="closeLink" runat="server" Text="[ Close ]" CssClass="floatRight"
                OnClick="closeLink_Click"></asp:LinkButton>
        </div>
        <div class="clear">
        </div>
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
                        <asp:DropDownList ID="statusDropdown" runat="server" CssClass="defaultDropDown">
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
                        <asp:Button CssClass="submitButton" Text="Submit" runat="server" ID="submitButton"
                            OnClick="submitButton_Click" />
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
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                                <asp:BoundField HeaderText="Appointment Date" DataField="AppointmentDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                <asp:BoundField HeaderText="Comment" DataField="Comment" />
                                <asp:BoundField HeaderText="Updated Date" DataField="UpdatedDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
