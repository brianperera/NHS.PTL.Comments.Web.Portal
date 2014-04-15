using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Utility;
using Nhs.Ptl.Comments.Contracts.Dto;

namespace Nhs.Ptl.Comments.Web
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                IList<PtlComment> ptlComments = CommentsManager.GetAllPtlComments();

                if (null != ptlComments)
                {
                    PopulateStatusDropdown();
                    PopulateSpecialityDropdown(ptlComments);
                    PopulateConsultantDropdown(ptlComments);
                    commentsGrid.DataSource = new List<string>();
                    commentsGrid.DataBind();

                    commentsGrid.DataSource = ptlComments;
                    commentsGrid.DataBind();
                }

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

        private void PopulateSpecialityDropdown(IList<PtlComment> ptlComments)
        {
        }

        private void PopulateConsultantDropdown(IList<PtlComment> ptlComments)
        {
            string[] consultantsList = ptlComments.Select(e => e.Consultant).Distinct().ToArray();
            consultantDropdown.DataSource = consultantsList;
            consultantDropdown.DataBind();
        }
    }
}