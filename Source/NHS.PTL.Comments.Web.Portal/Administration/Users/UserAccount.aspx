<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UserAccount.aspx.cs" Inherits="Nhs.PTL.Comments.Web.Portal.UserAccount" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="main">
        <div class="sections">
            <div class="pageHeader">
                <h2 id="CreateAccountPageHeader" visible="false" runat="server">
                    Create a New Account
                </h2>
                <h2 id="UpdateAccountPageHeader" visible="false" runat="server">
                    Update Account
                </h2>
            </div>
            <div class="pagedata">
                <div class="grid_24 error_msg">
                    <asp:Label ID="MessageLabel" runat="server" />
                </div>
                <div>
                    <ul class="formSection">

                              <li><span class="formTitleFields">Username</span> <span class="formFieldControl">
                            <asp:TextBox ID="UsernameTextbox" runat="server" />
                        </span><span>
                            <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" ControlToValidate="UserNameTextbox"
                                ForeColor="red" Display="Static" ErrorMessage="Required" />
                        </span></li>
                        <% if (ActionType == Nhs.PTL.Comments.Web.Portal.Constants.Create) %>
                        <% { %>
                        <li><span class="formTitleFields">Password</span> <span class="formFieldControl">
                            <asp:TextBox ID="PasswordTextbox" runat="server" TextMode="Password" />
                        </span><span>
                            <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" ControlToValidate="PasswordTextbox"
                                ForeColor="red" Display="Static" ErrorMessage="Required" />
                        </span></li>
                        <li><span class="formTitleFields">Confirm Password</span> <span class="formFieldControl">
                            <asp:TextBox ID="PasswordConfirmTextbox" runat="server" TextMode="Password" />
                        </span><span>
                            <asp:RequiredFieldValidator ID="PasswordConfirmRequiredValidator" runat="server"
                                ControlToValidate="PasswordConfirmTextbox" ForeColor="red" Display="Static" ErrorMessage="*" />
                            <asp:CompareValidator ID="PasswordConfirmCompareValidator" runat="server" ControlToValidate="PasswordConfirmTextbox"
                                ForeColor="red" Display="Static" ControlToCompare="PasswordTextBox" ErrorMessage="*" />
                        </span></li>
                        <% } %>
                        
                  <li><span class="formTitleFields">First Name</span> <span class="formFieldControl">
                            <asp:TextBox ID="FirstNameTextbox" runat="server" />
                        </span><span>
                            <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" ControlToValidate="FirstNameTextbox"
                                ForeColor="red" Display="Static" ErrorMessage="Required" />
                        </span></li>

                        <li><span class="formTitleFields">Last Name</span> <span class="formFieldControl">
                            <asp:TextBox ID="LastNameTextBox" runat="server" />
                        </span><span>
                            <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server" ControlToValidate="LastNameTextBox"
                                ForeColor="red" Display="Static" ErrorMessage="Required" />
                        </span></li>                        

                        <li><span class="formTitleFields">Email</span> <span class="formFieldControl">
                            <asp:TextBox ID="EmailTextbox" runat="server" />
                        </span><span>
                            <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" ControlToValidate="EmailTextbox"
                                ForeColor="red" Display="Static" ErrorMessage="Required" />
                        </span></li>

                        <li><span class="formTitleFields">Contact Tel No</span> <span class="formFieldControl">
                            <asp:TextBox ID="ContactTelNoTextBox" runat="server" />
                        </span></li>

                        <li><span class="formTitleFields">Role</span> <span class="formFieldControl">
                            <asp:DropDownList ID="DropDownListRoles" runat="server" />
                        </span></li>
                        <li><span class="formTitleFields">Is Active</span> <span class="formFieldControl">
                            <asp:CheckBox ID="IsActive_RadioButton" Checked="true" runat="server" />
                        </span></li>
                        <li><span>
                            <asp:Button CssClass="submitButton" ID="SubmitAccountDetails" OnClick="SubmitAccountDetails_OnClick"
                                runat="server" />
                            <asp:Button CssClass="submitButton" Text="Delete Account" ID="DeleteAccount" Visible="false"
                                runat="server" OnClick="DeleteAccount_Click" />
                        </span></li>
                    </ul>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
