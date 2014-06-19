using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Contracts.Dto;
using Nhs.Ptl.Comments.Utility;

namespace Nhs.Ptl.Comments.Web
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Page Events

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
                    InsertDropdownDefaultValue();
                    referrelGrid.DataSource = new List<string>();
                    referrelGrid.DataBind();

                    referrelGrid.DataSource = ptlComments;
                    referrelGrid.DataBind();
                }

            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        protected void rowLink_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            uniqueIdHiddenField.Value = ((LinkButton)sender).Text;

            entryForm.Visible = true;
            entryForm.Attributes.Add("style", "display: block;");
            fade.Attributes.Add("style", "display: block;");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Populate Status dropdown
        /// </summary>
        private void PopulateStatusDropdown()
        {
            List<string> statusList = StatusConfigurationManager.GetAllStatuses().ToList();
            if (null != statusList)
            {
                statusDropdown.DataSource = statusList;
                statusDropdown.DataBind();
            }
        }

        /// <summary>
        ///  Select distinct Speciality values in
        ///  PTL comments and bind to the Speciality dropdown
        /// </summary>
        /// <param name="ptlComments"></param>
        private void PopulateSpecialityDropdown(IList<PtlComment> ptlComments)
        {
            string[] specialitiesList = ptlComments.Select(e => e.Spec).Distinct().ToArray();
            consultantDropdown.DataSource = specialitiesList;
            consultantDropdown.DataBind();
        }

        /// <summary>
        ///  Select distinct Consultant values in
        ///  PTL comments and bind to the Consultant dropdown
        /// </summary>
        /// <param name="ptlComments"></param>
        private void PopulateConsultantDropdown(IList<PtlComment> ptlComments)
        {
            string[] consultantsList = ptlComments.Select(e => e.Consultant).Distinct().ToArray();
            consultantDropdown.DataSource = consultantsList;
            consultantDropdown.DataBind();
        }

        /// <summary>
        /// Add default 'All' item to each dropdown
        /// </summary>
        private void InsertDropdownDefaultValue()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                ListItem defaultItem = new ListItem(ConfigurationManager.AppSettings["DropDownAllText"]);

                specialityDropdown.Items.Insert(0, defaultItem);
                consultantDropdown.Items.Insert(0, defaultItem);
                statusDropdown.Items.Insert(0, defaultItem);
            }
        }

        /// <summary>
        /// Filter PTL comment data upon click on 'Search'
        /// </summary>
        private void FilterGrid()
        {
            // Get all PTL comments
            IEnumerable<PtlComment> ptlComments = CommentsManager.GetAllPtlComments();

            // Filter data
            if (null != ptlComments)
            {
                // Filter by Speciality
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
                {
                    // Filter by Speciality
                    if (!specialityDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        ptlComments = ptlComments.Where(comment => comment.SpecName.Equals(specialityDropdown.SelectedValue));
                    }

                    // Filter by Consultant
                    if (!consultantDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        ptlComments = ptlComments.Where(comment => comment.Consultant.Equals(consultantDropdown.SelectedValue));
                    }

                    // Filter by Status
                    if (!statusDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        ptlComments = ptlComments.Where(comment => comment.Status.Equals(statusDropdown.SelectedValue));
                    }
                }

                // Bind to grid
                referrelGrid.DataSource = ptlComments;
                referrelGrid.DataBind();
            }
        }

        #endregion
    }
}