<%@ Page Title="Status" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="StatusDataEntry.aspx.cs" Inherits="Nhs.Ptl.Comments.Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="main">
        <div class="pageHeader">
            <h2>
                Status Data Entry
            </h2>
        </div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <div>
            <asp:Label ID="Msg" ForeColor="maroon" runat="server" />
        </div>
        <div class="pagedata">
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="grid_24 error_msg">
                            <asp:Label ID="MessageLabel" runat="server" />
                        </div>
                        <ul class="formSection">
                            <li><span class="formTitleFields">Name</span> <span class="formFieldControl">
                                <asp:TextBox ID="ActionName_TextBox" runat="server" ViewStateMode="Enabled"
                                    AutoPostBack="True"></asp:TextBox>
                            </span>
                                <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" ControlToValidate="ActionName_TextBox"
                                    ForeColor="red" Display="Static" ErrorMessage="Required" />
                            </li>
                        </ul>
                        <asp:Label runat="server" ID="WardDataEntryFound_HiddenField" CssClass="hideRow" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <span>
                        <asp:Button CssClass="submitButton" Text="Submit" runat="server" ID="SubmitButton"
                            OnClick="SubmitButton_Click" />
                    </span>
                </div>
            </div>
        </div>
</asp:Content>
