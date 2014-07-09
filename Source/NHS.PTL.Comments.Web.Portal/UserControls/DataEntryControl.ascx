<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataEntryControl.ascx.cs"
    Inherits="UserControls_DataEntryControl" ViewStateMode="Enabled" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <div>
            <span style="float: left;">
                Data Entry
            </span>
            <asp:ImageButton ID="closeLink" runat="server" ImageUrl="~/Images/close2.png"
                CssClass="floatRight" OnClick="closeLink_Click" />
        </div>
        <div class="clearNoMargin">
        </div>
        <div class="pagedata popup">
            <div>
                <div class="grid_24 error_msg">
                    <asp:Label ID="MessageLabel" runat="server" />
                </div>
                <ul class="formSection">
                    <li><span >CDS ID:</span> <span class="formFieldControl uniqueIdentifier">
                        <asp:Literal runat="server" ID="uniqueIdentifier"></asp:Literal>
                    </span></li>
                    <li><span>Status:</span> <span class="formFieldControl">
                        <asp:DropDownList ID="statusDropdown" runat="server" CssClass="defaultDropDown">
                        </asp:DropDownList>
                    </span></li>
                    <%--                    <li><span class="formTitleFields">Appointment Date</span> <span class="formFieldControl">
                        <asp:TextBox ID="appointmentDateTextbox" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                        <asp:CalendarExtender Animated="true" Format="dd/MM/yyyy" ID="CalendarExtender1"
                            TargetControlID="appointmentDateTextbox" runat="server" ViewStateMode="Enabled" />
                    </span></li>--%>
                    <li><span>Add your comment on the below textbox:</span></li>

                    <li><div>
                        <asp:TextBox ID="commentTextbox" runat="server" Width="615px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    </div></li>

                    <li><span>
                        <asp:Button CssClass="submitButton" Text="Submit" runat="server" ID="submitButton"
                            OnClick="submitButton_Click" />
                        <asp:HiddenField runat="server" ID="actionHiddenField" />
                    </span></li>
                </ul>
                <ul class="formSection noBottonBorders noPadding">
                    <li style="float: left"><span class="">User:</span> <span
                        class="formFieldControl">
                        <asp:DropDownList ID="createdUserDropdown" runat="server" CssClass="defaultDropDown">
                        </asp:DropDownList>
                    </span></li>
                    <li><span class="">Created Date:</span> <span class="formFieldControl">
                        <asp:TextBox ID="createdDateTextbox" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                        <asp:CalendarExtender Animated="true" Format="dd/MM/yyyy" ID="createdDateCalendarExtender"
                            TargetControlID="createdDateTextbox" runat="server" ViewStateMode="Enabled" />
                    </span></li>
                </ul>
                <ul class="formSection">
                    <li><span>
                        <asp:Button CssClass="submitButton" Text="Search" runat="server" ID="Button1" OnClick="searchButton_Click" />
                    </span></li>
                </ul>
                <div class="subSections regularTable">
                    <div>
                        <span>
                            <strong>Comments History</strong>
                        </span>
                        <asp:GridView ID="commentsGrid" AutoGenerateColumns="False" runat="server" CellPadding="3"
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            CssClass="grid" AllowSorting="True" EmptyDataText="No matching records found"
                            ShowHeaderWhenEmpty="true" ShowHeader="true" AllowPaging="True" 
                            PageSize="5">
                            <Columns>
                                <asp:BoundField HeaderText="Created Date" DataField="UpdatedDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                <asp:BoundField HeaderText="Created By" DataField="CreatedBy"/>
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                                <%--<asp:BoundField HeaderText="Appointment Date" DataField="AppointmentDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />--%>
                                <asp:TemplateField HeaderText="Comment" SortExpression="SortComment">
                                     <ItemTemplate>
                                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("SortComment") %>' ToolTip ='<%# Bind("Comment") %>'></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="gridPagerStyle" />
                                <RowStyle ForeColor="#000066" />
                                <AlternatingRowStyle CssClass="altrow" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
