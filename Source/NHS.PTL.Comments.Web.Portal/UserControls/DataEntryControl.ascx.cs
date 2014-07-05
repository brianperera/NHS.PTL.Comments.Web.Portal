using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments;
using System.Web.UI.HtmlControls;
using Nhs.Ptl.Comments.Utility;
using Nhs.Ptl.Comments.Contracts.Dto;

public partial class UserControls_DataEntryControl : System.Web.UI.UserControl
{
    #region Private properties

    private string UniqueRowId { get; set; }
    private double PatientPathwayId { get; set; }
    private double Spec { get; set; }
    private DateTime ReferralRecievedDate { get; set; }
    private DateTime BreachDate { get; set; }
    private DateTime FutureClinicDate { get; set; }

    #endregion

    #region Event handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateStatusDropdown();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ReadKeyValues();

        uniqueIdentifier.Text = UniqueRowId;

        LoadCommentsGrid();

        SetBreachDatePassedStatus();
    }

    protected void closeLink_Click(object sender, EventArgs e)
    {
        HtmlGenericControl entryForm = Parent as HtmlGenericControl;
        HtmlGenericControl fadeDiv = Parent.Parent.FindControl("fade") as HtmlGenericControl;

        if (null != entryForm && null != fadeDiv)
        {
            entryForm.Visible = false;
            entryForm.Attributes.Add("style", "display: none;");
            fadeDiv.Attributes.Add("style", "display: none;");

        }

        MessageLabel.Visible = false;
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        DateTime appointmentDate;

        //TODO: check inputs


        PtlComment ptlComment = new PtlComment();

        if (DateTime.TryParse(appointmentDateTextbox.Text, out appointmentDate))
        {
            ptlComment.AppointmentDate = appointmentDate;
        }
        else
        {
            DisplayMessage(false, "Please select an appointment date");
            return;
        }

        ReadKeyValues();

        ptlComment.UniqueCdsRowIdentifier = UniqueRowId;
        ptlComment.PatientPathwayIdentifier = PatientPathwayId;
        ptlComment.Spec = Spec;
        ptlComment.ReferralRequestReceivedDate = ReferralRecievedDate;
        ptlComment.Status = statusDropdown.SelectedItem.Text;

        ptlComment.UpdatedDate = DateTime.Now.Date;
        ptlComment.Comment = commentTextbox.Text;

        DisplayMessage(CommentsManager.AddUpdatePtlComment(ptlComment));
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

    private void LoadCommentsGrid()
    {
        IList<PtlComment> comments = CommentsManager.GetPtlComments(UniqueRowId, PatientPathwayId, Spec, ReferralRecievedDate);

        //if (null != comments)
        //{
        commentsGrid.DataSource = comments;
        commentsGrid.DataBind();
        //}
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

    private void ReadKeyValues()
    {
        HiddenField uniqueIdHiddenField = Parent.FindControl("uniqueIdHiddenField") as HiddenField;
        HiddenField patientPathwayIdHiddenField = Parent.FindControl("patientPathwayIdHiddenField") as HiddenField;
        HiddenField specHiddenField = Parent.FindControl("specHiddenField") as HiddenField;
        HiddenField referralRecDateHiddenField = Parent.FindControl("referralRecDateHiddenField") as HiddenField;
        HiddenField futureClinicDateHiddenField = Parent.FindControl("futureClinicDateHiddenField") as HiddenField;
        HiddenField breachDateHiddenField = Parent.FindControl("breachDateHiddenField") as HiddenField;

        if (null != uniqueIdHiddenField
            && null != patientPathwayIdHiddenField
            && null != specHiddenField
            && null != referralRecDateHiddenField
            && null != futureClinicDateHiddenField
            && null != breachDateHiddenField)
        {
            double pathwayIdValue;
            double specValue;
            DateTime referralDateValue;
            DateTime futureClinicDateValue;
            DateTime breachDateValue;

            if (double.TryParse(patientPathwayIdHiddenField.Value, out pathwayIdValue)
               && double.TryParse(specHiddenField.Value, out specValue)
               && DateTime.TryParse(referralRecDateHiddenField.Value, out referralDateValue))
            {
                UniqueRowId = uniqueIdHiddenField.Value;
                PatientPathwayId = pathwayIdValue;
                Spec = specValue;
                ReferralRecievedDate = referralDateValue;

                DateTime.TryParse(futureClinicDateHiddenField.Value, out futureClinicDateValue);
                DateTime.TryParse(breachDateHiddenField.Value, out breachDateValue);
                FutureClinicDate = futureClinicDateValue;
                BreachDate = breachDateValue;
            }
        }
    }

    private void SetBreachDatePassedStatus()
    {
        if (FutureClinicDate != DateTime.MinValue
            && BreachDate != DateTime.MinValue
            && FutureClinicDate >= BreachDate)
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