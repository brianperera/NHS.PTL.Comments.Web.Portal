using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for Utility
/// </summary>
namespace Nhs.Ptl.Comments.Utility
{
    public static class Utility
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                value = value.Substring(0, maxLength);
            }

            return value + "...";
        }

        /// <summary>
        /// Add default 'All' item to each filtered list
        /// </summary>
        public static List<string> GetDropdownDefaultValueToListItems(List<string> filteredItems)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                string defaultItem = ConfigurationManager.AppSettings["DropDownAllText"];

                if (!filteredItems.Contains(defaultItem))
                {
                    filteredItems.Insert(0, defaultItem);
                }
            }

            return filteredItems;
        }

        public static string[] FutureApptStatusList 
        {
            get
            { 
                string[] futureApptStatusList = { "With Date", "No Date" };
                return futureApptStatusList;
            }
        }
    }

    public static class DateTimeHelper
    {
        public static DateTime FirstDateInWeek(this DateTime dt)
        {
            while (dt.DayOfWeek != CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek)
                dt = dt.AddDays(-1);
            return dt;
        }
    }

    public static class FileExporter
    {
        public static void ExportToExcel(DataTable dt, HttpResponse response, string textFileName)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                string filename = textFileName;
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.AllowPaging = false;
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                response.ContentType = "application/vnd.ms-excel";
                response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                //response.EnableViewState = false;
                response.Write(tw.ToString());
                response.End();
            }
        }
    }

}