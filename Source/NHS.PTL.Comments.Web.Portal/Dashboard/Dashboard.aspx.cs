using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Contracts.Dto;
using Nhs.Ptl.Comments.Utility;
using System.Data;
using System.Collections;
using AjaxControlToolkit;

// IMPORTANT!!!: Look at the constants before you change the columns!
// Change the constants accordingly

namespace Nhs.Ptl.Comments.Web
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Constants

        private const string blankStatus = "Blank Status";

        #endregion

        string RTTWaitString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();
                PopulateRTTWaitDropDown(opReferrals);
                PopulateFutureApptStatusDropDown();
                InsertDropdownDefaultValue();
                GenerateDataGrid(opReferrals);
            }
        }

        private void GenerateDataGrid(IList<OpReferral> opReferrals)
        {
            if (null != opReferrals)
            {

                //Refine the referrals
                foreach (OpReferral opReferral in opReferrals)
                {
                    if (string.IsNullOrEmpty(opReferral.Status))
                    {
                        opReferral.Status = blankStatus;
                    }

                }

                opReferrals = opReferrals.OrderBy(f => f.SpecName).ToList();

                IList<string> specialites = opReferrals.Select(x => x.SpecName).Distinct().ToList();
                IList<string> statusTypes = opReferrals.Select(x => x.Status).Distinct().ToList();

                //
                // Here we create a DataTable with four columns.
                //
                DataTable specialitesTable = new DataTable();
                specialitesTable.Columns.Add("Specialty", typeof(string));

                //Auto generate the columns
                foreach (var statusType in statusTypes)
                {
                    if (!string.IsNullOrEmpty(statusType))
                        specialitesTable.Columns.Add(statusType, typeof(string));
                }

                specialitesTable.Columns.Add("Totals", typeof(string));

                //
                // Here we add five DataRows.

                int filteredListCount = 0;
                int fullTotalCount = 0;
                ArrayList rowDataParam = new ArrayList();

                foreach (string speciality in specialites)
                {
                    int rowTotal = 0;
                    rowDataParam.Add(speciality);

                    foreach (var status in statusTypes)
                    {
                        if (!string.IsNullOrEmpty(status))
                        {
                            filteredListCount = opReferrals.Where(x => x.SpecName == speciality).Where(y => y.Status == status).Count();
                            rowDataParam.Add(filteredListCount.ToString());
                            rowTotal += filteredListCount;
                        }
                    }

                    rowDataParam.Add(rowTotal.ToString());
                    specialitesTable.Rows.Add(rowDataParam.ToArray());
                    rowDataParam.Clear();
                    fullTotalCount += rowTotal;
                }

                //Add footer summary row
                rowDataParam.Add("Total");
                List<decimal> statusCount = new List<decimal>();

                foreach (var status in statusTypes)
                {
                    if (!string.IsNullOrEmpty(status))
                    {
                        filteredListCount = opReferrals.Where(y => y.Status == status).Count();
                        rowDataParam.Add(filteredListCount.ToString());
                        statusCount.Add(filteredListCount);
                    }
                }

                rowDataParam.Add(fullTotalCount.ToString());
                specialitesTable.Rows.Add(rowDataParam.ToArray());

                statusSummaryGrid.DataSource = specialitesTable;
                statusSummaryGrid.DataBind();

                //Barchart
                //Truncate the text
                //Render the chart only if data exists
                if (statusCount.Count > 0)
                {
                    string values = string.Empty;
                    System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                    javaScript.Append("<script type=\"text/javascript\"> google.load(\"visualization\", \"1\", {packages:[\"corechart\"]});");
                    javaScript.Append("google.setOnLoadCallback(drawChart); function drawChart() { var data = google.visualization.arrayToDataTable([ ['Status', 'Total', { role: 'annotation' }],");

                    int i = 0;
                    foreach (var statusType in statusTypes)
                    {
                        if (statusType != "")
                        {
                            values = values + "['" + statusType + "'," + statusCount[i] + ",'" + statusCount[i] + "'],";
                            i++;
                        }
                    }

                    //where i initialize my data for google chart...
                    values = values.Substring(0, values.Length - 1);
                    javaScript.Append(values);
                    javaScript.Append("]);");
                    javaScript.Append("var options = {" +
                        "chartArea:{top:30}, " +
                        "focusTarget: 'category'," +
                        "tooltip: {isHtml: true}," +
                        "legend: 'none'," +
                        "colors:['#58D3F7']," +
                        "vAxis:{ format: '0'}," +
                        "hAxis:{ title: 'Status', titleTextStyle: { italic: false} }" +
                        "};"
                        + " var chart = new google.visualization.ColumnChart(document.getElementById('chart_div')); chart.draw(data, options);}");
                    javaScript.Append("</script>");

                    Page.RegisterStartupScript("Graph", javaScript.ToString());
                }
            }
        }

        List<string> headerList = new List<string>();

        protected void statusSummaryGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dataRow = null;
            dataRow = ((System.Data.DataRowView)(e.Row.DataItem));

            if (dataRow != null && dataRow.Row != null && dataRow.Row.ItemArray[0] == "Total")
            {
                e.Row.CssClass = "tableFooter";

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].CssClass = "tableFooter";

                }
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    headerList.Add(e.Row.Cells[i].Text);
                }
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i != 0)
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;

                if (i == e.Row.Cells.Count - 1)
                    e.Row.Cells[i].CssClass = "tableFooter";

                if (e.Row.RowType == DataControlRowType.DataRow && i != 0 && i != e.Row.Cells.Count - 1 && e.Row.Cells[i].CssClass != "tableFooter")
                {
                    // Insert a real hyperlink into Cells[1] using HyperLinkValue.
                    HyperLink myLink = new HyperLink();
                    myLink.NavigateUrl = "Validations.aspx?specialty=" + e.Row.Cells[0].Text + "&status=" + headerList[i] + "&rttwait=" + RTTWaitString;
                    myLink.Text = e.Row.Cells[i].Text;
                    // then add the control to the cell.
                    e.Row.Cells[i].Controls.Add(myLink);
                }
            }
        }

        private void ResetControls()
        {
            IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();
            GenerateDataGrid(opReferrals);
        }

        private void PopulateRTTWaitDropDown(IList<OpReferral> opReferrals)
        {
            string[] rttWaitList = opReferrals.Select(e => e.WeekswaitGrouped).Distinct().ToArray();
            RTTWaitDropDown.DataSource = rttWaitList;
            RTTWaitDropDown.DataBind();

            //RTTWaitCheckBoxList.DataSource = rttWaitList;
            //RTTWaitCheckBoxList.DataBind();
        }

        private void InsertDropdownDefaultValue()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                ListItem defaultItem = new ListItem(ConfigurationManager.AppSettings["DropDownAllText"]);

                if (!RTTWaitDropDown.Items.Contains(defaultItem))
                {
                    RTTWaitDropDown.Items.Insert(0, defaultItem);
                }

                if (!FutureApptStatusDropDownList.Items.Contains(defaultItem))
                {
                    FutureApptStatusDropDownList.Items.Insert(0, defaultItem);
                }
            }
        }

        protected void FutureApptStatusDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<OpReferral> filtered = null;
            IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

            //Filter based on RTT Wait
            filtered = FilterRecordsByFutureApptStatus(opReferrals);

            //Filter based on future status
            filtered = FilterBasedOnRTTWait(filtered);

            //DataBind
            GenerateDataGrid(filtered);
        }

        protected void RTTWaitDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<OpReferral> filtered = null;
            IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

            //Filter based on RTT Wait
            filtered = FilterBasedOnRTTWait(opReferrals);

            //Filter based on future status
            filtered = FilterRecordsByFutureApptStatus(filtered);

            //DataBind
            GenerateDataGrid(filtered);
        }

        private IList<OpReferral> FilterRecordsByFutureApptStatus(IList<OpReferral> oref)
        {
            IList<OpReferral> filtered = null;

            if (null != oref)
            {
                string mainFilterCriteria = FutureApptStatusDropDownList.SelectedItem.Text;

                if (!mainFilterCriteria.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                {
                    if (string.Equals(mainFilterCriteria, Constants.NoDate))
                        filtered = oref.Where(x => x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                    else
                        filtered = oref.Where(x => !x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                }
                else
                {
                    filtered = oref;
                }
            }

            return filtered;
        }

        private IList<OpReferral> FilterBasedOnRTTWait(IList<OpReferral> oref)
        {

            IList<OpReferral> filtered = null;

            List<string> rttWaitListItems = new List<string>();

            foreach (ListItem item in RTTWaitDropDown.Items)
            {
                if (item.Selected)
                {
                    rttWaitListItems.Add(item.Text);
                    RTTWaitString = RTTWaitString + ";" + item.Text;
                }
            }

            if (!rttWaitListItems.Contains(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                if (null != oref)
                {
                    filtered = oref.Where(x => rttWaitListItems.Contains(x.WeekswaitGrouped)).ToList();
                }
            }
            else
            {
                filtered = oref;
            }

            return filtered;
        }

        private void PopulateFutureApptStatusDropDown()
        {
            //TODO: Need to move to config file
            FutureApptStatusDropDownList.DataSource = Utility.Utility.FutureApptStatusList;
            FutureApptStatusDropDownList.DataBind();
        }

        protected void ExportButton_Click(object sender, EventArgs e)
        {
            IList<OpReferral> filtered = null;
            IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

            //Filter based on RTT Wait
            filtered = FilterBasedOnRTTWait(opReferrals);

            //Filter based on future status
            filtered = FilterRecordsByFutureApptStatus(filtered);

            //DataBind
            GenerateDataGrid(filtered);

            FileExporter.ExportToExcel(statusSummaryGrid.DataSource as DataTable, this.Page.Response, "StatusSummary.xls");
        }
    }
}
