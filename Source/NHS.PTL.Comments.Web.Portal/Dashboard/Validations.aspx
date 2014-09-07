<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Validations.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.Validations" ViewStateMode="Enabled"
    EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/DataEntryControl.ascx" TagName="DataEntryControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/ClientSideUtility.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../Styles/GridviewScroll.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#MainContent_gvMain').gridviewScroll({
                width: $(window).width() - 100,
                height:600,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1
            });
        } 
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchBoxContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:TextBox ID="patientTextbox" CssClass="searchInput" runat="server"></asp:TextBox>
            <input type="text" id="txtWaterMark" class="waterMarkText searchInput" value="e.g. Brian Thomas" />
            <asp:ImageButton CssClass="searchButton" runat="server" ID="globalSearchButton" OnClick="searchButton_Click"
                ImageUrl="~/Images/searchIcon.png" ToolTip="Also search by MRN, Pathway ID and NHS number" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="main">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(gridviewScroll);
            </script>
        <div id="entryForm" class="overlay" runat="server" visible="false">
            <uc1:DataEntryControl ID="DataEntryControl1" runat="server" />
            <asp:HiddenField ID="uniqueIdHiddenField" runat="server" />
            <asp:HiddenField ID="patientPathwayIdHiddenField" runat="server" />
            <asp:HiddenField ID="specHiddenField" runat="server" />
            <asp:HiddenField ID="referralRecDateHiddenField" runat="server" />
            <asp:HiddenField ID="futureClinicDateHiddenField" runat="server" />
            <asp:HiddenField ID="breachDateHiddenField" runat="server" />
            <asp:HiddenField ID="mrnHiddenField" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div>
            <div class="filterContainer">
                <div>
                    <asp:HiddenField ID="specDdHiddenField" runat="server" />
                    <asp:HiddenField ID="consultantDdHiddenField" runat="server" />
                    <asp:HiddenField ID="statusDdHiddenField" runat="server" />
                    <asp:HiddenField ID="rttWaitDdHiddenField" runat="server" />
                    <asp:HiddenField ID="attStatusDdHiddenField" runat="server" />
                    <asp:HiddenField ID="futureApptStatusDdHiddenField" runat="server" />
                    <ul class="formSection noBottonBorders noPadding clearNoMargin">
                        <li style="float: left"><span class="shortFormTitleFieldsWithoutFloat">Speciality</span>
                            <span class="formFieldControl">
                                <asp:DropDownList ID="specialityDropdown" runat="server" CssClass="defaultDropDown"
                                    AutoPostBack="True" OnSelectedIndexChanged="specialityDropdown_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span></li>
                        <li style="float: left"><span class="shortFormTitleFieldsWithoutFloat">Consultant</span>
                            <span class="formFieldControl">
                                <asp:DropDownList ID="consultantDropdown" runat="server" CssClass="defaultDropDown"
                                    AutoPostBack="True" OnSelectedIndexChanged="consultantDropdown_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span></li>
                        <li><span class="shortFormTitleFieldsWithoutFloat">Status</span> <span class="formFieldControl">
                            <asp:DropDownList ID="statusDropdown" runat="server" CssClass="defaultDropDown" AutoPostBack="True"
                                OnSelectedIndexChanged="statusDropdown_SelectedIndexChanged">
                            </asp:DropDownList>
                        </span></li>
                    </ul>
                    <ul class="formSection noBottonBorders noPadding clearNoMargin">
                        <li style="float: left"><span class="shortFormTitleFieldsWithoutFloat">RTT Wait</span>
                            <span class="formFieldControl">
                                <asp:DropDownList ID="RTTWaitDropDown" runat="server" CssClass="defaultDropDown"
                                    AutoPostBack="True" OnSelectedIndexChanged="RTTWaitDropDown_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span></li>
                        <li style="float: left"><span class="shortFormTitleFieldsWithoutFloat">Attendance Status</span>
                            <span class="formFieldControl">
                                <asp:DropDownList ID="AttendanceStatusDropDown" runat="server" CssClass="defaultDropDown"
                                    AutoPostBack="True" OnSelectedIndexChanged="AttendanceStatusDropDown_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span></li>
                        <li><span class="shortFormTitleFieldsWithoutFloat">Future Appt Status</span> <span
                            class="formFieldControl">
                            <asp:DropDownList ID="FutureApptStatusDropDownList" runat="server" CssClass="defaultDropDown"
                                AutoPostBack="True" OnSelectedIndexChanged="FutureApptStatusDropDown_SelectedIndexChanged">
                            </asp:DropDownList>
                        </span></li>
                    </ul>
                    <ul class="formSection clearNoMargin">
                        <li><span>
                            <asp:Button CssClass="submitButton" Text="Search" runat="server" ID="searchButton"
                                OnClick="searchButton_Click" />
                        </span><span>
                            <asp:Button CssClass="submitButton" Text="Refresh" runat="server" ID="refreshButton"
                                OnClick="refreshButton_Click" />
                        </span><span>
                            <asp:Button CssClass="submitButton" Text="Reset" runat="server" ID="resetButton"
                                OnClick="resetButton_Click" />
                        </span></li>
                    </ul>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="regularTable subSections">
            <asp:GridView ID="gvMain" runat="server" Width="100%" CssClass="" AutoGenerateColumns="False"
            GridLines="None" onpageindexchanging="gvMain_PageIndexChanging" 
                    onrowdatabound="gvMain_RowDataBound"  
                    DataKeyNames="UniqueCDSRowIdentifier,PatientPathwayIdentifier,Spec,ReferralRequestReceivedDate,FutureClinicDate,RttBreachDate" 
                    AllowPaging="True">
            <Columns>
                <asp:TemplateField HeaderText="MRN" ItemStyle-BackColor="#EEEEEE">
                    <ItemTemplate>
                        <asp:LinkButton ID="rowLink" CssClass="rowLink" Text='<%# Eval("LocalPatientID")%>'
                            runat="server" OnClick="rowLink_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Forename" DataField="PatientForename" ItemStyle-BackColor="#EEEEEE" />
                <asp:BoundField HeaderText="Surname" DataField="PatientSurname" ItemStyle-BackColor="#EEEEEE"  ItemStyle-CssClass="rightDropShadow"/>
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="To be booked by" DataField="ToBeBookedByDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                <asp:BoundField HeaderText="NHS Number" DataField="NhsNumber" />
                <asp:BoundField HeaderText="DOB" DataField="DateOfBirth" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                <asp:BoundField HeaderText="Spec Code" DataField="Spec" />
                <asp:BoundField HeaderText="Spec Name" DataField="SpecName" />
                <asp:BoundField HeaderText="Division" DataField="NewDivision" />
                <asp:BoundField HeaderText="Consultant" DataField="Consultant" />
                <asp:BoundField HeaderText="Ref Date" DataField="ReferralRequestReceivedDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                <asp:BoundField HeaderText="Source Of Referral" DataField="SourceOfReferralText" />
                <asp:BoundField HeaderText="Priority Type" DataField="PriorityType" />
                <asp:BoundField HeaderText="RTT Clock Start" DataField="RttClockStart" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                <asp:BoundField HeaderText="RTT Breach Date" DataField="RttBreachDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                <asp:BoundField HeaderText="Att Date" DataField="AttendanceDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                <asp:BoundField HeaderText="Att Status" DataField="AttStatus" />
                <asp:BoundField HeaderText="RTT Status Code" DataField="RttStatus" />
                <asp:BoundField HeaderText="Waiting List Status" DataField="WaitingListStatus" />
                <asp:BoundField HeaderText="Future Clinic Date" DataField="FutureClinicDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                <asp:BoundField HeaderText="Wait" DataField="WeekswaitGrouped" />
            </Columns>
            <HeaderStyle CssClass="GridviewScrollC1Header" />
            <RowStyle CssClass="GridviewScrollC1Item" />
            <PagerStyle CssClass="GridviewScrollC1Pager" />
            <AlternatingRowStyle CssClass="altrow" />
        </asp:GridView>
        </div>
        </div>
        <div class="fade" runat="server" id="fade">
        </div>
                    </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
