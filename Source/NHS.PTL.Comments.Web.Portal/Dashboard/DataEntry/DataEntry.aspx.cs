using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Contracts.Dto;
using Nhs.Ptl.Comments.Utility;

namespace Nhs.Ptl.Comments.Web
{
    public partial class DataEntry : Page
    {
        #region Private Properties

        private bool IsUpdate
        {
            get
            {
                return bool.Parse(actionHiddenField.Value);
            }
            set
            {
                actionHiddenField.Value = value.ToString();
            }
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                PopulateUniqueIdentifiers();
                PopulateStatusDropdown();
                InsertDefaultDropdownItem();
                IsUpdate = false;
            }
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            //TODO: check inputs
            PtlComment ptlComment = new PtlComment();
            ptlComment.UniqueCdsRowIdentifier = double.Parse(uniqueIdentifierDrowpdown.SelectedValue);
            ptlComment.Status = statusDropdown.SelectedValue;
            ptlComment.AppointmentDate = DateTime.Parse(appointmentDateTextbox.Text);
            ptlComment.UpdatedDate = DateTime.Now.Date;
            ptlComment.Comment = commentTextbox.Text;

            DisplayMessage(CommentsManager.AddUpdatePtlComment(ptlComment, IsUpdate));
        }

        protected void uniqueIdentifierDrowpdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearInputFields();

            PtlComment ptlComment = CommentsManager.GetPtlComment(uniqueIdentifierDrowpdown.SelectedValue);

            if (null != ptlComment)
            {
                if (ptlComment.UpdatedDate != DateTime.MinValue)
                {
                    // Record already exists for selected row identifier
                    BindCommentData(ptlComment);
                    IsUpdate = true;
                }
                else
                {
                    // New record
                    IsUpdate = false;
                    SetBreachDatePassedStatus(ptlComment.RttBreachDate, ptlComment.FutureClinicDate);
                }
            }
        }

        #endregion

        #region Private Methods

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
            if (null != uniqueIdentifierDrowpdown)
            {
                uniqueIdentifierDrowpdown.DataSource = uniqueRowIdentifiers;
                uniqueIdentifierDrowpdown.DataBind();
            }
        }

        private void BindCommentData(PtlComment ptlComment)
        {
            if (null != statusDropdown.Items.FindByText(ptlComment.Status))
            {
                statusDropdown.SelectedValue = ptlComment.Status;
            }

            appointmentDateTextbox.Text = string.Format(ConfigurationManager.AppSettings["DateTimeFormat"], ptlComment.AppointmentDate);
            commentTextbox.Text = ptlComment.Comment;
        }

        private void InsertDefaultDropdownItem()
        {
            // Inserting the default text to the dropdowns
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownDefaultText"]))
            {
                ListItem defaultItem = new ListItem(ConfigurationManager.AppSettings["DropDownDefaultText"]);

                uniqueIdentifierDrowpdown.Items.Insert(0, defaultItem);
                //statusDropdown.Items.Insert(0, defaultItem);
            }
        }

        /// <summary>
        /// Clears all input fields except unique identifier dropdown
        /// </summary>
        private void ClearInputFields()
        {
            statusDropdown.SelectedIndex = 0;
            appointmentDateTextbox.Text = string.Empty;
            commentTextbox.Text = string.Empty;
            MessageLabel.Visible = false;
        }

        private void DisplayMessage(bool executionStatus)
        {
            MessageLabel.Visible = true;
            MessageLabel.Text = executionStatus == true ? "Record Updated Successfully" : "Record Not Updated";
            MessageLabel.CssClass = executionStatus == true ? "alert-success" : "alert-danger";
        }

        private void SetBreachDatePassedStatus(DateTime breachDate, DateTime futureClinicDate)
        {
            if (futureClinicDate >= breachDate)
            {
                string bringFowardStatus = StatusConfigurationManager.GetStatusValue("BringForward");

                if (!string.IsNullOrEmpty(bringFowardStatus))
                {
                    if (null != statusDropdown.Items.FindByText(bringFowardStatus))
                    {
                        statusDropdown.SelectedValue = bringFowardStatus;
                    }
                }
            }
        }

        #endregion
    }
}