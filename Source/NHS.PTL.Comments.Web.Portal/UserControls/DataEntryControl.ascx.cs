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
using System.Web.Security;
using System.Configuration;
using Nhs.Ptl.Comments.DataAccess;

public partial class UserControls_DataEntryControl : System.Web.UI.UserControl
{
    #region Private properties

    private string UniqueRowId { get; set; }
    private string PatientPathwayId { get; set; }
    private string Spec { get; set; }
    private string Mrn { get; set; }
    private DateTime ReferralRecievedDate { get; set; }
    private DateTime BreachDate { get; set; }
    private DateTime FutureClinicDate { get; set; }
    public List<PtlComment> ModifiedComments { get; set; }

    #endregion

    #region Event handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateStatusDropdown();   
        }
    }

    public IList<PtlComment> GetAllComments()
    {
        ReadKeyValues();
        return CommentsManager.GetPtlComments(UniqueRowId, PatientPathwayId, Spec, ReferralRecievedDate);
    }

    public void PopulateDefaultValues(IList<PtlComment> comments)
    {
        //Get the latest comment
        if (comments == null)
            return;

        PtlComment currentComment = comments.OrderByDescending(c => c.UpdatedDate).FirstOrDefault();
        if (currentComment != null)
        {
            //commentTextbox.Text = currentComment.Comment;
            statusDropdown.SelectedValue = currentComment.Status;
            if(createdUserDropdown.Items.Count > 0)
                createdUserDropdown.SelectedIndex = 0;
            //createdDateTextbox.Text = currentComment.UpdatedDate.ToString("dd/MM/yyyy");
        }
    }

    public void ClearField()
    {
        commentTextbox.Text = string.Empty;
        createdUserDropdown.Items.Clear();
        createdDateTextbox.Text = string.Empty;
        commentsGrid.DataSource = null;
        commentsGrid.DataBind();
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ReadKeyValues();

        uniqueIdentifier.Text = Mrn;

        ModifiedComments = LoadCommentsGrid();

        if (!IsPostBack || createdUserDropdown.Items.Count <= 0)
        {
            PopulateCreatedByDropdown(ModifiedComments);
            InsertDropdownDefaultValue();
        }

        SetBreachDatePassedStatus(ModifiedComments);
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

    protected void searchButton_Click(object sender, EventArgs e)
    {
        //do nothing. Let the postback handle it
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        //DateTime appointmentDate;
        PtlComment ptlComment = new PtlComment();

        //if (DateTime.TryParse(appointmentDateTextbox.Text, out appointmentDate))
        //{
        //    ptlComment.AppointmentDate = appointmentDate;
        //}
        //else
        //{
        //    DisplayMessage(false, "Please select an appointment date");
        //    return;
        //}

        ReadKeyValues();

        ptlComment.UniqueCdsRowIdentifier = UniqueRowId;
        ptlComment.PatientPathwayIdentifier = PatientPathwayId;
        ptlComment.Spec = Spec;
        ptlComment.ReferralRequestReceivedDate = ReferralRecievedDate;
        ptlComment.Status = statusDropdown.SelectedItem.Text;

        MembershipUser userInfo = Membership.GetUser();
        ptlComment.CreatedBy = userInfo.UserName;

        ptlComment.UpdatedDate = DateTime.Now;
        ptlComment.Comment = commentTextbox.Text;

        DisplayMessage(CommentsManager.AddUpdatePtlComment(ptlComment));
    }

    #endregion

    #region Private Methods

    private void PopulateStatusDropdown()
    {
        StatusDA statusDA = new StatusDA();
        List<string> statusList = statusDA.GetAllStatuses();
        if (null != statusList)
        {
            statusDropdown.DataSource = statusList;
            statusDropdown.DataBind();
        }
    }

    private void PopulateCreatedByDropdown(List<PtlComment> comments)
    {
        if (null != comments)
        {
            List<string> createdUsers = comments.Select(x => x.CreatedBy).Distinct().ToList();

            createdUserDropdown.DataSource = createdUsers;
            createdUserDropdown.DataBind();
        }
    }

    private List<PtlComment> LoadCommentsGrid()
    {
        List<PtlComment> filteredList = new List<PtlComment>();
        var comments = GetAllComments();
        
        if (comments == null)
        {
            return null;
        }

        filteredList = comments.ToList();
               

        if (!createdUserDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]) && !string.IsNullOrEmpty(createdUserDropdown.SelectedValue))
        {
            filteredList = filteredList.Where(comment => comment.CreatedBy.Equals(createdUserDropdown.SelectedValue)).ToList();
        }
        if (!string.IsNullOrEmpty(createdDateTextbox.Text))
        {
            filteredList = filteredList.Where(comment => comment.UpdatedDate.ToString("dd/MM/yyyy").Equals(createdDateTextbox.Text)).ToList();
        }
        
        //Sort the comments
        filteredList = filteredList.OrderByDescending(c => c.UpdatedDate).ToList();

        if (filteredList.Count > 5)
        {
            PopupGridWrapper.Attributes["Class"] = "overlayWithScrollBar";
        }
        else
        {
            PopupGridWrapper.Attributes["Class"] = string.Empty;
        }

        commentsGrid.DataSource = filteredList;
        commentsGrid.DataBind();

        return filteredList;
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
        HiddenField mrnHiddenField = Parent.FindControl("mrnHiddenField") as HiddenField;

        if (null != uniqueIdHiddenField
            && null != patientPathwayIdHiddenField
            && null != specHiddenField
            && null != referralRecDateHiddenField
            && null != futureClinicDateHiddenField
            && null != breachDateHiddenField
            && null != mrnHiddenField)
        {
            DateTime referralDateValue;
            DateTime futureClinicDateValue;
            DateTime breachDateValue;

            if (!string.IsNullOrEmpty(patientPathwayIdHiddenField.Value)
               && !string.IsNullOrEmpty(specHiddenField.Value)
               && DateTime.TryParse(referralRecDateHiddenField.Value, out referralDateValue))
            {
                UniqueRowId = uniqueIdHiddenField.Value;
                PatientPathwayId = patientPathwayIdHiddenField.Value;
                Spec = specHiddenField.Value;
                ReferralRecievedDate = referralDateValue;
                Mrn = mrnHiddenField.Value;

                DateTime.TryParse(futureClinicDateHiddenField.Value, out futureClinicDateValue);
                DateTime.TryParse(breachDateHiddenField.Value, out breachDateValue);
                FutureClinicDate = futureClinicDateValue;
                BreachDate = breachDateValue;
            }
        }
    }

    private void SetBreachDatePassedStatus(List<PtlComment> comments)
    {
        if (comments == null || comments.Count <= 0)
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
            else
            {
                statusDropdown.SelectedIndex = 0;
            }
        }
    }

    /// <summary>
    /// Add default 'All' item to each dropdown
    /// </summary>
    private void InsertDropdownDefaultValue()
    {
        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
        {
            ListItem defaultItem = new ListItem(ConfigurationManager.AppSettings["DropDownAllText"]);
            createdUserDropdown.Items.Insert(0, defaultItem);
        }
    }

    #endregion
}