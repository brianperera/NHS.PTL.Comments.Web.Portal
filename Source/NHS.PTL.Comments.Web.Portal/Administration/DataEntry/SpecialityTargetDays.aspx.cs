using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using Nhs.Ptl.Comments.DataAccess;
using Nhs.Ptl.Comments.Contracts.Dto;

namespace Nhs.Ptl.Comments
{
    public partial class SpecialityTargetDays : System.Web.UI.Page
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

                    //Get the Specility Target record
                    SpecialityTargetDatesDA specialityTargetDatesDA = new SpecialityTargetDatesDA();
                    int currentID = 0;
                    int.TryParse(ID, out currentID);
                    SpecialityTargetDate specialityTargetDate = specialityTargetDatesDA.GetSpecialityTargetDateRecord(currentID);

                    if (specialityTargetDate != null)
                    {
                        SpecialityCode_TextBox.Text = specialityTargetDate.ID.ToString();
                        Speciality_TextBox.Text = specialityTargetDate.Speciality;
                        TargetDays_TextBox.Text = specialityTargetDate.TargetDay.ToString();
                        IsActive_RadioButton.Checked = specialityTargetDate.Activate;
                    }                    
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

            SpecialityTargetDatesDA specialityTargetDatesDA = new SpecialityTargetDatesDA();
            SpecialityTargetDate specialityTargetDays = new SpecialityTargetDate();
            int currentID = 0;
            int.TryParse(SpecialityCode_TextBox.Text, out currentID);
            specialityTargetDays.ID = currentID;
            specialityTargetDays.Speciality = Speciality_TextBox.Text;
            int currentTargetDays = 0;
            int.TryParse(TargetDays_TextBox.Text, out currentTargetDays);
            specialityTargetDays.TargetDay = currentTargetDays;
            specialityTargetDays.Activate = IsActive_RadioButton.Checked;

            if (WardDataEntryFound_HiddenField.Text.Equals("true"))
            {
                // Update
                executionStatus = specialityTargetDatesDA.UpdateSpecialityTargetDate(specialityTargetDays);
            }
            else
            {
                // Add
                executionStatus = specialityTargetDatesDA.AddSpecialityTargetDate(specialityTargetDays);
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