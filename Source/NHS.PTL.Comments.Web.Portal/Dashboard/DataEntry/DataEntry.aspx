<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="DataEntry.aspx.cs"
    Inherits="Nhs.Ptl.Comments.Web.DataEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html>
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/html-reset.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="~/Plugins/font-awesome/css/font-awesome.min.css" />
    <!--[if IE 7]>
      <link rel="stylesheet" href="~/Plugins/font-awesome/css/font-awesome-ie7.min.css"/>
      <link rel="stylesheet" href="~/Styles/ie7.css" />
    <![endif]-->
</head>
<body>
    <form runat="server">
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
                                <asp:DropDownList ID="uniqueIdentifierDrowpdown" runat="server" CssClass="defaultDropDown"
                                    AutoPostBack="true" OnSelectedIndexChanged="uniqueIdentifierDrowpdown_SelectedIndexChanged">
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
                                <asp:Button CssClass="submitButton" Text="Submit" runat="server" ID="submitButton"
                                    OnClick="submitButton_Click" />
                                <asp:HiddenField runat="server" ID="actionHiddenField" />
                            </span></li>
                        </ul>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
