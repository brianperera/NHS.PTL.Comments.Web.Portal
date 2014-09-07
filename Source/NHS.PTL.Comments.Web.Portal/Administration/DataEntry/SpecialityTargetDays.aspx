<%@ Page Title="Speciality Target Days" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SpecialityTargetDays.aspx.cs" Inherits="Nhs.Ptl.Comments.SpecialityTargetDays" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="main">
        <div class="pageHeader">
            <h2>
                Speciality Target Days
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
                            <li><span class="formTitleFields">Speciality Code</span> <span class="formFieldControl">
                                <asp:TextBox ID="SpecialityCode_TextBox" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                            </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TargetDays_TextBox"
                                    ForeColor="red" Display="Dynamic" ErrorMessage="Required" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer" 
                                    ControlToValidate="SpecialityCode_TextBox" ForeColor="red" Display="Dynamic" ErrorMessage="Value must be a whole number" />
                            </li>
                            <li><span class="formTitleFields">Speciality</span> <span class="formFieldControl">
                                <asp:TextBox ID="Speciality_TextBox" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                            </span>
                                <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" ControlToValidate="Speciality_TextBox"
                                    ForeColor="red" Display="Dynamic" ErrorMessage="Required" />
                            </li>
                            <li><span class="formTitleFields">Target Days</span> <span class="formFieldControl">
                                <asp:TextBox ID="TargetDays_TextBox" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                            </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TargetDays_TextBox"
                                    ForeColor="red" Display="Dynamic" ErrorMessage="Required" />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck" Type="Integer" 
                                    ControlToValidate="TargetDays_TextBox" ForeColor="red" Display="Dynamic" ErrorMessage="Value must be a whole number" />
                            </li>
                            <li><span class="formTitleFields">Is Active</span> <span class="formFieldControl">
                            <asp:CheckBox ID="IsActive_RadioButton" Checked="true" runat="server" />
                        </span></li>
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
