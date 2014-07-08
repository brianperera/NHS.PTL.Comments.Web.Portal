﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.Dashboard" ViewStateMode="Enabled"
    EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/DataEntryControl.ascx" TagName="DataEntryControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/effects.core.js" type="text/javascript"></script>
    <script src="../Scripts/ui.dialog.js" type="text/javascript"></script>
    <script src="../Scripts/CommentsScript.js" type="text/javascript"></script>
    <link href="../Styles/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="main">
        <div class="pageHeader">
            <h2>               
            </h2>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="entryForm" class="overlay" runat="server" visible="false">
                    <uc1:DataEntryControl ID="DataEntryControl1" runat="server" />
                    <asp:HiddenField ID="uniqueIdHiddenField" runat="server" />
                    <asp:HiddenField ID="patientPathwayIdHiddenField" runat="server" />
                    <asp:HiddenField ID="specHiddenField" runat="server" />
                    <asp:HiddenField ID="referralRecDateHiddenField" runat="server" />
                    <asp:HiddenField ID="futureClinicDateHiddenField" runat="server" />
                    <asp:HiddenField ID="breachDateHiddenField" runat="server" />
                </div>
                <div>
                    <div class="filterContainer">
                        <div>
                            <ul class="formSection">
                                <li><span class="formTitleFields">Patient</span> <span class="formFieldControl">
                                    <asp:TextBox ID="patientTextbox" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                                </span></li>
                                <li><span class="formTitleFields">Speciality</span> <span class="formFieldControl">
                                    <asp:DropDownList ID="specialityDropdown" runat="server" CssClass="defaultDropDown">
                                    </asp:DropDownList>
                                </span></li>
                                <li><span class="formTitleFields">Consultant</span> <span class="formFieldControl">
                                    <asp:DropDownList ID="consultantDropdown" runat="server" CssClass="defaultDropDown">
                                    </asp:DropDownList>
                                </span></li>
                                <li><span class="formTitleFields">Status</span> <span class="formFieldControl">
                                    <asp:DropDownList ID="statusDropdown" runat="server" CssClass="defaultDropDown">
                                    </asp:DropDownList>
                                </span></li>
                                <li><span class="formTitleFields">RTT Wait</span> <span class="formFieldControl">
                                    <asp:DropDownList ID="RTTWaitDropDown" runat="server" CssClass="defaultDropDown">
                                    </asp:DropDownList>
                                </span></li>
                                <li><span class="formTitleFields">Attendance Status</span> <span class="formFieldControl">
                                    <asp:DropDownList ID="AttendanceStatusDropDown" runat="server" CssClass="defaultDropDown">
                                    </asp:DropDownList>
                                </span></li>
                                <li><span>
                                    <asp:Button CssClass="submitButton" Text="Search" runat="server" ID="searchButton"
                                        OnClick="searchButton_Click" />
                                </span></li>
                            </ul>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="subSections regularTable">
                        <div class="gridOuter">
                            <asp:GridView ID="referrelGrid" AutoGenerateColumns="False" runat="server" CellPadding="3"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CssClass="grid" AllowSorting="True" EmptyDataText="No matching records found"
                                ShowHeaderWhenEmpty="true" ShowHeader="true" DataKeyNames="UniqueCDSRowIdentifier,PatientPathwayIdentifier,Spec,ReferralRequestReceivedDate,FutureClinicDate,RttBreachDate"
                                PageSize="10" AllowPaging="true" 
                                onpageindexchanging="referrelGrid_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="rowLink" CssClass="rowLink" Text='<%# Eval("UniqueCDSRowIdentifier")%>'
                                                runat="server" OnClick="rowLink_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Pathway ID" DataField="PatientPathwayIdentifier" />
                                    <asp:BoundField HeaderText="MRN" DataField="LocalPatientID" />
                                    <asp:BoundField HeaderText="NHS Number" DataField="NhsNumber" />
                                    <asp:BoundField HeaderText="DOB" DataField="DateOfBirth" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                    <asp:BoundField HeaderText="Forename" DataField="PatientForename" />
                                    <asp:BoundField HeaderText="Surname" DataField="PatientSurname" />
                                    <asp:BoundField HeaderText="Spec Code" DataField="Spec" />
                                    <asp:BoundField HeaderText="Spec Name" DataField="SpecName" />
                                    <asp:BoundField HeaderText="Division" DataField="NewDivision" />
                                    <asp:BoundField HeaderText="Consultant" DataField="Consultant" />
                                    <asp:BoundField HeaderText="Ref Date" DataField="ReferralRequestReceivedDate"
                                        DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                    <asp:BoundField HeaderText="Source Of Referral" DataField="SourceOfReferralText" />
                                    <asp:BoundField HeaderText="Priority Type" DataField="PriorityType" />
                                    <asp:BoundField HeaderText="RTT Clock Start" DataField="RttClockStart" />
                                    <asp:BoundField HeaderText="RTT Breach Date" DataField="RttBreachDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                    <asp:BoundField HeaderText="Att Date" DataField="AttendanceDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                    <asp:BoundField HeaderText="Att Status" DataField="AttStatus" />
                                    <asp:BoundField HeaderText="RTT Status Code" DataField="RttStatus" />
                                    <asp:BoundField HeaderText="RTT Status" DataField="RttText" />
                                    <asp:BoundField HeaderText="Waiting List Status" DataField="WaitingListStatus" />
                                    <asp:BoundField HeaderText="Future Clinic Date" DataField="FutureClinicDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                    <asp:BoundField HeaderText="Wait" DataField="WaitAtFutureClinicDate" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" />
                                    <%-- <asp:BoundField HeaderText="Status" DataField="Status" />
                                    <asp:BoundField HeaderText="Appointment Date" DataField="AppointmentDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                    <asp:BoundField HeaderText="Updated Date" DataField="UpdatedDate" DataFormatString="<%$ AppSettings:DateTimeFormat %>" />
                                    <asp:BoundField HeaderText="Comment" DataField="Comment" />--%>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="gridPagerStyle" />
                                <RowStyle ForeColor="#000066" />
                                <AlternatingRowStyle CssClass="altrow" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="fade" runat="server" id="fade">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
