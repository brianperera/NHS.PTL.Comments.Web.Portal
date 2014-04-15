using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Utility;

namespace Nhs.Ptl.Comments.Web
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                PopulateStatusDropdown();
                PopulateSpecialityDropdown();
                PopulateConsultantDropdown();
                commentsGrid.DataSource = new List<string>();
                commentsGrid.DataBind();
            }
        }

        private void PopulateStatusDropdown()
        {
            List<string> statusList = StatusConfigurationManager.GetAllStatuses().ToList();
            if (null != statusList)
            {
                statusDropdown.DataSource = statusList;
                statusDropdown.DataBind();
            }
        }

        private void PopulateSpecialityDropdown()
        {
        }

        private void PopulateConsultantDropdown()
        {
        }
    }
}