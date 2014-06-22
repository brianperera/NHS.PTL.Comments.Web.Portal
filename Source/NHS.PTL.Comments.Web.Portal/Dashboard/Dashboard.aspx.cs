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
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {
                    PopulateStatusDropdown();
                    PopulateSpecialityDropdown(opReferrals);
                    PopulateConsultantDropdown(opReferrals);
                    InsertDropdownDefaultValue();
                    referrelGrid.DataSource = new List<string>();
                    referrelGrid.DataBind();

                    referrelGrid.DataSource = opReferrals;
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
            LinkButton link = sender as LinkButton;
            GridViewRow row = (GridViewRow)link.NamingContainer;

            uniqueIdHiddenField.Value = referrelGrid.DataKeys[row.DataItemIndex]["UniqueCDSRowIdentifier"].ToString();
            patientPathwayIdHiddenField.Value = referrelGrid.DataKeys[row.DataItemIndex]["PatientPathwayIdentifier"].ToString();
            specHiddenField.Value = referrelGrid.DataKeys[row.DataItemIndex]["Spec"].ToString();
            referralRecDateHiddenField.Value = referrelGrid.DataKeys[row.DataItemIndex]["ReferralRequestReceivedDate"].ToString();
            futureClinicDateHiddenField.Value = referrelGrid.DataKeys[row.DataItemIndex]["FutureClinicDate"].ToString();
            breachDateHiddenField.Value = referrelGrid.DataKeys[row.DataItemIndex]["RttBreachDate"].ToString();

            entryForm.Visible = true;
            entryForm.Attributes.Add("style", "display: block;");
            fade.Attributes.Add("style", "display: block;");
        }

        protected void referrelGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            referrelGrid.PageIndex = e.NewPageIndex;
            FilterGrid();
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
        /// <param name="opReferrals"></param>
        private void PopulateSpecialityDropdown(IList<OpReferral> opReferrals)
        {
            string[] specialitiesList = opReferrals.Select(e => e.SpecName).Distinct().ToArray();
            specialityDropdown.DataSource = specialitiesList;
            specialityDropdown.DataBind();
        }

        /// <summary>
        ///  Select distinct Consultant values in
        ///  PTL comments and bind to the Consultant dropdown
        /// </summary>
        /// <param name="ptlComments"></param>
        private void PopulateConsultantDropdown(IList<OpReferral> opReferrals)
        {
            string[] consultantsList = opReferrals.Select(e => e.Consultant).Distinct().ToArray();
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
            IEnumerable<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

            // Filter data
            if (null != opReferrals)
            {
                // Filter by patient name
                if (!string.IsNullOrEmpty(patientTextbox.Text))
                {
                    opReferrals = opReferrals.Where(comment => (comment.PatientForename.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                                            || comment.PatientSurname.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                // Filter by Speciality
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
                {
                    // Filter by Speciality
                    if (!specialityDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        opReferrals = opReferrals.Where(comment => comment.SpecName.Equals(specialityDropdown.SelectedValue));
                    }

                    // Filter by Consultant
                    if (!consultantDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        opReferrals = opReferrals.Where(comment => comment.Consultant.Equals(consultantDropdown.SelectedValue));
                    }

                    // Filter by Status
                    if (!statusDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        opReferrals = opReferrals.Where(comment => comment.Status.Equals(statusDropdown.SelectedValue));
                    }
                }

                if (null != opReferrals)
                {
                    // Bind to grid
                    referrelGrid.DataSource = opReferrals.ToList();
                    referrelGrid.DataBind();

                    referrelGrid.PageIndex = 0;
                }

            }
        }

        #endregion

    }
}