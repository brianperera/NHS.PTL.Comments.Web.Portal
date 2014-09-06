<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.Account_ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="main">
        <div class="sections">
            <div class="pageHeader">
                <h2>
                    Change Password</h2>
            </div>
            <div>
                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"/>
            </div>
            <div class="pagedata">
                <ul class="formSection">
                    <li><span class="formTitleFields">Username</span> <span class="formFieldControl">
                        <asp:TextBox ID="UsernameTextBox" Enabled="false" runat="server" CssClass="passwordEntry" TextMode="SingleLine"></asp:TextBox>
                    </span><span>
                        <asp:RequiredFieldValidator ID="UsernameRequiredFieldValidator" runat="server" ControlToValidate="UsernameTextBox"
                            CssClass="failureNotification" ErrorMessage="Username is required." ToolTip="Username is required.">Username is required.</asp:RequiredFieldValidator>
                    </span></li>
                    <li><span class="formTitleFields">New Password</span> <span class="formFieldControl">
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    </span><span>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                            CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required.">New Password is required.</asp:RequiredFieldValidator>
                    </span></li>
                    <li><span class="formTitleFields">Confirm New Password</span> <span class="formFieldControl">
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                    </span><span>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                            ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">Confirm New Password is required.</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                            ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="*">*</asp:CompareValidator>
                    </span></li>
                    <li><span>
                        <asp:Button CssClass="submitButton" ID="ChangePasswordPushButton" runat="server"
                            CommandName="ChangePassword" Text="Change Password" 
                            onclick="ChangePasswordPushButton_Click" />
                    </span></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
