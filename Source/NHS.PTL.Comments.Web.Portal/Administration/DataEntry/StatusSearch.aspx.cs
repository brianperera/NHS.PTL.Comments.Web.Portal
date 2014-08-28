using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using Nhs.Ptl.Comments.DataAccess;

namespace Nhs.Ptl.Comments
{
    public partial class Status : System.Web.UI.Page
    {
        List<string> statuses = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindInitialData();
        }

        private void BindInitialData()
        {
            // Bind users to Grid.
            StatusDA statusDA = new StatusDA();
            statuses = statusDA.GetAllStatuses();

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));

            foreach (string item in statuses)
            {
                table.Rows.Add(item);
            }

            Status_Grid.DataSource = table;
            Status_Grid.DataBind();
        }
    }
}