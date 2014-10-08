<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs"
    Title="Dashboard" Inherits="Nhs.Ptl.Comments.Web.Dashboard" ViewStateMode="Enabled"
    EnableViewState="true" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jsapi.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchBoxContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="main">
        <div class="pageHeader">
            <h2>
                Outpatient PTL by validation status
            </h2>
        </div>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <div class="subSections">
            <ul class="formSection noBottonBorders noPadding clearNoMargin">
                <li style="float: left"><span class="shortFormTitleFieldsWithoutFloat" style="display:inline;float:left">RTT Wait</span>
                    <span class="formFieldControl">
                        <cc1:DropDownCheckBoxes ID="RTTWaitDropDown" OnSelectedIndexChanged="RTTWaitDropDown_SelectedIndexChanged"
                            AutoPostBack="true" runat="server" UseButtons="false" AddJQueryReference="True"
                            UseSelectAllNode="False">
                            <Style2 SelectBoxWidth="130px" />
                            <Texts SelectBoxCaption="Select" />
                            
                        </cc1:DropDownCheckBoxes>
                        <%--<asp:DropDownList ID="RTTWaitDropDown" runat="server" CssClass="defaultDropDown"
                            AutoPostBack="True" OnSelectedIndexChanged="RTTWaitDropDown_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                    </span></li>
                <li>

                <span style="margin-left:40px" class="shortFormTitleFieldsWithoutFloat">Future Appt Status</span> <span
                    class="formFieldControl">
                    <asp:DropDownList ID="FutureApptStatusDropDownList" Width="130px" runat="server"
                        CssClass="defaultDropDown" AutoPostBack="True" OnSelectedIndexChanged="FutureApptStatusDropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                </span></li>
            </ul>
            <div>
                <ul class="formSection clearNoMargin">
                    <li><span style="margin:5px;">
                        <asp:Button CssClass="submitButton" Text="Export" runat="server" ID="ExportButton"
                            OnClick="ExportButton_Click" />
                    </span><span>
                        <button onclick="print()" class="submitButton">
                            Print</button>
                    </span></li>
                </ul>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="subSections regularTable" style="float: left; display: inline-block;
            width: 600px">
            <asp:GridView Width="90%" ID="statusSummaryGrid" AutoGenerateColumns="True" runat="server"
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CssClass="grid" EmptyDataText="No matching records found" OnRowDataBound="statusSummaryGrid_RowDataBound">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#BDBDBD" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="gridPagerStyle" />
                <RowStyle ForeColor="#000066" />
                <AlternatingRowStyle CssClass="altrow" />
            </asp:GridView>
        </div>
        <div class="subSections" style="display: inline-block;">
            <div id="chart_div">
            </div>
        </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
