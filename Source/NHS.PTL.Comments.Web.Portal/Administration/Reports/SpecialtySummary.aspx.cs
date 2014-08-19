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
    public partial class SpecialtySummary : System.Web.UI.Page
    {
        #region Constants


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {
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

                    //Truncate the text
                    List<string> truncatedStatus = new List<string>();
                    foreach (var item in statusTypes)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            truncatedStatus.Add("    " + item.Truncate(5));
                        }
                    }
                    truncatedStatus.Insert(0, "     ");

                    //Barchart
                    statusSummaryBarChart.CategoriesAxis = (String.Join(",", truncatedStatus).TrimEnd(','));
                    statusSummaryBarChart.ChartType = ChartType.StackedColumn;

                    statusSummaryBarChart.Series[0].Name = "Status";
                    statusSummaryBarChart.Series[0].Data = statusCount.ToArray();

                }

            }
        }


        protected void statusSummaryGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView dataRow = null;
            dataRow = ((System.Data.DataRowView)(e.Row.DataItem));

            if (dataRow!= null && dataRow.Row != null && dataRow.Row.ItemArray[0] == "Total")
            {
                e.Row.CssClass = "tableFooter";
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if(i != 0)
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;

                if (i == e.Row.Cells.Count - 1)
                    e.Row.Cells[i].CssClass = "tableFooter";
            }
        }
    }
}
