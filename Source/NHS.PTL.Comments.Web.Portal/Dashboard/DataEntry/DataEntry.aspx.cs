using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Utility;
using Nhs.Ptl.Comments.Contracts.Dto;
using System.Configuration;

namespace Nhs.Ptl.Comments.Web
{
    public partial class DataEntry : Page
    {
        public bool IsUpdate 
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

        protected void submitButton_Click(object sender, EventArgs e)
        {
            PtlComment ptlComment = new PtlComment();
            ptlComment.UniqueCdsRowIdentifier = double.Parse(uniqueIdentifierDrowpdown.SelectedValue);
            ptlComment.Status = statusDropdown.SelectedValue;
            ptlComment.AppointmentDate = DateTime.Parse(appointmentDateTextbox.Text);
            ptlComment.UpdatedDate = DateTime.Now.Date;
            ptlComment.Comment = commentTextbox.Text;

            CommentsManager.AddUpdatePtlComment(ptlComment, IsUpdate);
        }

        protected void uniqueIdentifierDrowpdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            PtlComment ptlComment = CommentsManager.GetPtlComment(uniqueIdentifierDrowpdown.SelectedValue);

            if (null != ptlComment)
            {
                BindCommentData(ptlComment);
                IsUpdate = true;
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
                statusDropdown.Items.Insert(0, defaultItem);
            }
        }
    }
}