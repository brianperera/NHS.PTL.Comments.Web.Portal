using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Utility;

namespace Nhs.Ptl.Comments.Web
{
    public partial class DataEntry : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                PopulateUniqueIdentifiers();
                PopulateStatusDropdown();
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

        private void PopulateUniqueIdentifiers()
        {
            List<string> uniqueRowIdentifiers = CommentsManager.GetAllUniqueRowIdentifiers().ToList();
            if (null != uniqueIdentifireDrowpdown)
            {
                uniqueIdentifireDrowpdown.DataSource = uniqueRowIdentifiers;
                uniqueIdentifireDrowpdown.DataBind();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {

        }
    }
}