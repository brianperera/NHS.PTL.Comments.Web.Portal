<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DataEntry.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.DataEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="main">
        <div class="pageHeader">
            <h2>
                Data Entry
            </h2>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="pagedata">
                    <div>
                        <div class="grid_24 error_msg">
                            <asp:Label ID="MessageLabel" runat="server" />
                        </div>
                        <ul class="formSection">
                            <li><span class="formTitleFields">UniqueCDSRowIdentifier</span> <span class="formFieldControl">
                                <asp:DropDownList ID="uniqueIdentifireDrowpdown" runat="server" CssClass="defaultDropDown">
                                </asp:DropDownList>
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
                                <asp:Button CssClass="submitButton" Text="Submit" runat="server" ID="SubmitButton"
                                    OnClick="SubmitButton_Click" />
                            </span></li>
                        </ul>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
