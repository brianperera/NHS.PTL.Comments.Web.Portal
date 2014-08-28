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
        public string ActionType
        {
            get
            {
                string actionType = Constants.Create;

                if (Request.QueryString["action"] != null)
                    actionType = Request.QueryString["action"];

                return actionType;
            }
        }

        public string ID
        {
            get
            {
                string id = string.Empty;

                if (Request.QueryString["id"] != null)
                    id = Request.QueryString["id"];

                return id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ActionType == Constants.Update && !string.IsNullOrEmpty(ID))
                {
                    WardDataEntryFound_HiddenField.Text = "true";

                    //Update fields
                    ActionName_TextBox.Text = ID;
                }
                else
                {
                    WardDataEntryFound_HiddenField.Text = "false";
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            bool executionStatus = false;

            StatusDA statusDA = new StatusDA();

            if (WardDataEntryFound_HiddenField.Text.Equals("true"))
            {
                // Update
                executionStatus = statusDA.UpdateStatus(ID, ActionName_TextBox.Text);
            }
            else
            {
                // Add
                executionStatus = statusDA.AddStatus(ActionName_TextBox.Text);
            }

            DisplayMessage(executionStatus);
        }

        private void DisplayMessage(bool executionStatus)
        {
            MessageLabel.Visible = true;
            MessageLabel.Text = executionStatus == true ? "Record Updated Successfully" : "Record Not Updated";
            MessageLabel.CssClass = executionStatus == true ? "alert-success" : "alert-danger";
        }

        private void DisplayMessage(bool executionStatus, string message)
        {
            MessageLabel.Visible = true;
            MessageLabel.Text = message;
            MessageLabel.CssClass = executionStatus == true ? "alert-success" : "alert-danger";
        }
    }
}