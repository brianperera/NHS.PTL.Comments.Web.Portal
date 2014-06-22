<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="blueBackGround">
        <div class="main">
            <asp:Login ID="LoginUser" runat="server" DestinationPageUrl="~/Dashboard/Dashboard.aspx"
                EnableViewState="false" RenderOuterTable="false" OnLoginError="LoginUser_LoginError">
                <LayoutTemplate>
                    <div class="login_container">
                        <img class="lock" src="Images/lock.png" alt="">
                        <div class="clear-fix" />
                        <div class="login_content">
                            <h1>
                                SAFE STAFFING DATA ENTRY PORTAL</h1>
                            <div class="clear-fix" />
                            <div class="error_msg">
                                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="login alert-danger"
                                    ValidationGroup="LoginUserValidationGroup" />
                            </div>
                            <div class="fieldSeperator">
                                <span class="fieldName"><i class="icon-user"></i>username</span> <span>
                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </span>
                                <div class="clear-fix" />
                            </div>
                            <div class="fieldSeperator">
                                <span class="fieldName"><i class="icon-lock"></i>password</span> <span>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </span>
                                <div class="clear-fix" />
                            </div>
                            <div class="fieldSeperator">
                                <asp:Button CssClass="submitButton" ID="LoginButton" runat="server" CommandName="Login"
                                    Text="Log In" ValidationGroup="LoginUserValidationGroup" />
                            </div>
                        </div>
                        <div class="login_footer">
                            <img src="Images/NHSlogo.gif" alt="logo">
                            <span>Copyright © 2014 by NHS Foundation Trust. All rights reserved. </span>
                        </div>
                    </div>
                </LayoutTemplate>
            </asp:Login>
        </div>
    </div>
</asp:Content>
