using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Contracts.Dto;
using Nhs.Ptl.Comments.Utility;
using System.Data;
using Nhs.Ptl.Comments;
using Nhs.Ptl.Comments.DataAccess;

// IMPORTANT!!!: Look at the constants before you change the columns!
// Change the constants accordingly

namespace Nhs.Ptl.Comments.Web
{
    public partial class Validations : System.Web.UI.Page
    {
        public string SpecialtyType
        {
            get
            {
                string specialtyType = string.Empty;

                if (Request.QueryString["Specialty"] != null)
                    specialtyType = Request.QueryString["Specialty"];

                return specialtyType;
            }
        }

        public string Status
        {
            get
            {
                string statusType = string.Empty;

                if (Request.QueryString["status"] != null)
                    statusType = Request.QueryString["status"];

                return statusType;
            }
        }

        public bool IsFiltered
        {
            get
            {
                bool isFiltered = false;

                if (Request.QueryString["isFiltered"] != null)
                {
                    isFiltered = string.Equals(Request.QueryString["isFiltered"].ToString(), "true") ? true : false;
                }

                return isFiltered;
            }
        }

        public string FutureApptStatus
        {
            get
            {
                string futureApptStatus = string.Empty;

                if (!string.IsNullOrEmpty(Request.QueryString["futureAppt"]))
                {
                    futureApptStatus = Request.QueryString["futureAppt"];
                }

                return futureApptStatus;
            }
        }

        public List<string> RttWait
        {
            get
            {
                List<string> rttWait = new List<string>();

                if (Request.QueryString["rttwait"] != null)
                    rttWait = Request.QueryString["rttwait"].Split(';').ToList();

                rttWait.Remove("");

                return rttWait;
            }
        }

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                IList<OpReferral> opReferrals = GetReferralData();

                //Apply filters based on the query string paramenters
                //opReferrals = QueryStringBasedFiltering(opReferrals);

                if (null != opReferrals)
                {
                    PopulateStatusDropdown(opReferrals);
                    PopulateSpecialityDropdown(opReferrals);
                    PopulateConsultantDropdown(opReferrals);
                    PopulateAttendanceStatusDropDown(opReferrals);
                    PopulateRTTWaitDropDown(opReferrals);
                    PopulateFutureApptStatusDropDown();
                    InsertDropdownDefaultValue();
                    RefineDatesInOpReferrals(opReferrals);

                    QueryStringBasedDropdownItemSelection();

                    this.gvMain.DataSource = opReferrals;
                    this.gvMain.DataBind();
                }

            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        protected void resetButton_Click(object sender, EventArgs e)
        {
            isQueryStringFiltering.Value = "false";
            ResetControls();
            FilterGrid();
        }

        protected void refreshButton_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        protected void rowLink_Click(object sender, EventArgs e)
        {
            LinkButton link = sender as LinkButton;
            GridViewRow row = (GridViewRow)link.NamingContainer;

            uniqueIdHiddenField.Value = gvMain.DataKeys[row.DataItemIndex]["UniqueCDSRowIdentifier"].ToString();
            patientPathwayIdHiddenField.Value = gvMain.DataKeys[row.DataItemIndex]["PatientPathwayIdentifier"].ToString();
            specHiddenField.Value = gvMain.DataKeys[row.DataItemIndex]["Spec"].ToString();
            referralRecDateHiddenField.Value = gvMain.DataKeys[row.DataItemIndex]["ReferralRequestReceivedDate"].ToString();
            futureClinicDateHiddenField.Value = gvMain.DataKeys[row.DataItemIndex]["FutureClinicDate"].ToString();
            breachDateHiddenField.Value = gvMain.DataKeys[row.DataItemIndex]["RttBreachDate"].ToString();
            mrnHiddenField.Value = link.Text;

            entryForm.Visible = true;
            entryForm.Attributes.Add("style", "display: block;");
            fade.Attributes.Add("style", "display: block;");

            DataEntryControl1.ClearField();
            DataEntryControl1.PopulateDefaultValues(DataEntryControl1.GetAllComments());
        }

        protected void specialityDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!specialityDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                IList<OpReferral> filtered = CommentsManager.GetOpReferralByFieldName(Constants.SpecialtyFieldName, specialityDropdown.SelectedItem.Text);

                if (null != filtered)
                {

                    //IList<OpReferral> filtered = opReferrals.Where(x => x.SpecName == specialityDropdown.SelectedItem.Text).ToList();

                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        consultantDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        statusDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        ValidationRTTWaitDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.WeekswaitGrouped).Distinct().ToList());
                        ValidationRTTWaitDropDown.DataBind();
                        SetSavedValue(ValidationRTTWaitDropDown, rttWaitDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(Utility.Utility.FutureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        isQueryStringFiltering.Value = "false";
                    }
                }

            }
            else
            {
                ResetControls();
            }
        }

        protected void consultantDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!consultantDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                IList<OpReferral> filtered = CommentsManager.GetOpReferralByFieldName(Constants.ConsultantFieldName, consultantDropdown.SelectedItem.Text);

                if (null != filtered)
                {                    
                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        statusDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        ValidationRTTWaitDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.WeekswaitGrouped).Distinct().ToList());
                        ValidationRTTWaitDropDown.DataBind();
                        SetSavedValue(ValidationRTTWaitDropDown, rttWaitDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(Utility.Utility.FutureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        isQueryStringFiltering.Value = "false";
                    }
                }
            }
            else
            {
                ResetControls();
            }
        }

        // TODO: Problem here.
        protected void statusDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!statusDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {

                    IList<OpReferral> filtered = opReferrals.Where(x => x.Status == statusDropdown.SelectedItem.Text).ToList();

                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        consultantDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        ValidationRTTWaitDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.WeekswaitGrouped).Distinct().ToList());
                        ValidationRTTWaitDropDown.DataBind();
                        SetSavedValue(ValidationRTTWaitDropDown, rttWaitDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(Utility.Utility.FutureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        isQueryStringFiltering.Value = "false";
                    }
                }
            }
            else
            {
                ResetControls();
            }
        }

        protected void RTTWaitDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ValidationRTTWaitDropDown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                IList<OpReferral> filtered = CommentsManager.GetOpReferralByFieldName(Constants.WeekswaitGroupedFieldName, 
                                                                                    ValidationRTTWaitDropDown.SelectedItem.Text);

                if (null != filtered)
                {
                    //IList<OpReferral> filtered = opReferrals.Where(x => x.WeekswaitGrouped == ValidationRTTWaitDropDown.SelectedItem.Text).ToList();

                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        consultantDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        statusDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(Utility.Utility.FutureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        isQueryStringFiltering.Value = "false";
                    }
                }
            }
            else
            {
                ResetControls();
            }
        }

        protected void AttendanceStatusDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AttendanceStatusDropDown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                IList<OpReferral> filtered = CommentsManager.GetOpReferralByFieldName(Constants.AttStatusFieldName, AttendanceStatusDropDown.SelectedItem.Text);

                if (null != filtered)
                {

                    //IList<OpReferral> filtered = opReferrals.Where(x => x.AttStatus == AttendanceStatusDropDown.SelectedItem.Text).ToList();

                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        consultantDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        statusDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        ValidationRTTWaitDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.WeekswaitGrouped).Distinct().ToList());
                        ValidationRTTWaitDropDown.DataBind();
                        SetSavedValue(ValidationRTTWaitDropDown, rttWaitDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(Utility.Utility.FutureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        isQueryStringFiltering.Value = "false";
                    }
                }
            }
            else
            {
                ResetControls();
            }
        }

        protected void FutureApptStatusDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FutureApptStatusDropDownList.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                IList<OpReferral> filtered = CommentsManager.GetOpReferralByFieldName(Constants.FutureClinicDateFieldName, 
                                                                    FutureApptStatusDropDownList.SelectedItem.Text);

                if (null != filtered)
                {
                    string mainFilterCriteria = FutureApptStatusDropDownList.SelectedItem.Text;
                    //IList<OpReferral> filtered;

                    if (string.Equals(mainFilterCriteria, Constants.NoDate))
                        filtered = filtered.Where(x => x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                    else
                        filtered = filtered.Where(x => !x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();


                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        consultantDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        statusDropdown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        ValidationRTTWaitDropDown.DataSource = Utility.Utility.GetDropdownDefaultValueToListItems(filtered.Select(x => x.WeekswaitGrouped).Distinct().ToList());
                        ValidationRTTWaitDropDown.DataBind();
                        SetSavedValue(ValidationRTTWaitDropDown, rttWaitDdHiddenField.Value);

                        isQueryStringFiltering.Value = "false";
                    }
                }
            }
            else
            {
                ResetControls();
            }
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                // Special care taken for key values
                if (e.Row.Cells[Constants.ReferralRequestColumnNo].Text.Equals(DateTime.MinValue.ToShortDateString()))
                {
                    e.Row.Cells[Constants.ReferralRequestColumnNo].Text = string.Empty;
                }

                if (e.Row.Cells[Constants.FutureClinicDateColumnNo].Text.Equals(DateTime.MinValue.ToShortDateString()))
                {
                    e.Row.Cells[Constants.FutureClinicDateColumnNo].Text = string.Empty;
                }

                if (e.Row.Cells[Constants.ToBeBookedByColumnNo].Text.Equals(DateTime.MinValue.ToShortDateString()))
                {
                    e.Row.Cells[Constants.ToBeBookedByColumnNo].Text = string.Empty;
                }
            }
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;

            if (isQueryStringFiltering.Value != "true")
            {
                FilterGrid();
            }
            else
            {
                //IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                //Apply filters based on the query string paramenters
                //opReferrals = QueryStringBasedFiltering(opReferrals);
                IList<OpReferral>  opReferrals = GetReferralData();
                this.gvMain.DataSource = opReferrals;
                this.gvMain.DataBind();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Populate Status dropdown
        /// </summary>
        private void PopulateStatusDropdown(IList<OpReferral> opReferrals)
        {
            if (null != opReferrals)
            {
                string[] statusList = opReferrals.Select(e => e.Status).Distinct().ToArray();
                statusDropdown.DataSource = statusList;
                statusDropdown.DataBind();
            }
        }

        private void PopulateRTTWaitDropDown(IList<OpReferral> opReferrals)
        {
            string[] rttWaitList = opReferrals.Select(e => e.WeekswaitGrouped).Distinct().ToArray();
            ValidationRTTWaitDropDown.DataSource = rttWaitList;
            ValidationRTTWaitDropDown.DataBind();
        }

        private void PopulateAttendanceStatusDropDown(IList<OpReferral> opReferrals)
        {
            string[] attStatusList = opReferrals.Select(e => e.AttStatus).Distinct().ToArray();
            AttendanceStatusDropDown.DataSource = attStatusList;
            AttendanceStatusDropDown.DataBind();
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


                // This is just to avoid checking one by one :)
                List<DropDownList> dropDownLists = new List<DropDownList>() 
                { 
                            specialityDropdown,
                            consultantDropdown,
                            statusDropdown,
                            ValidationRTTWaitDropDown,
                            AttendanceStatusDropDown,
                            FutureApptStatusDropDownList
                };

                foreach (DropDownList ddl in dropDownLists)
                {
                    // Create a new list item for each dropdown. 
                    // Otherwise an error would throw - Cannot have multiple items selected in a DropDownList
                    ListItem defaultItem = new ListItem(ConfigurationManager.AppSettings["DropDownAllText"]);

                    if (!ddl.Items.Contains(defaultItem))
                    {
                        ddl.Items.Insert(0, defaultItem);
                    }
                }
            }
        }

        /// <summary>
        /// Filter PTL comment data upon click on 'Search'
        /// </summary>
        private void FilterGrid()
        {
            // Get all PTL comments
            //List<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals().ToList();

            IList<OpReferral> opReferrals = CommentsManager.GetOpReferralsByParams(patientTextbox.Text,
                                                                                    specialityDropdown.SelectedValue,
                                                                                    consultantDropdown.SelectedValue,
                                                                                    ValidationRTTWaitDropDown.SelectedValue,
                                                                                    AttendanceStatusDropDown.SelectedValue,
                                                                                    FutureApptStatusDropDownList.SelectedValue);
            // Filter data
            if (null != opReferrals)
            {
                // Filter by patient name
                //if (!string.IsNullOrEmpty(patientTextbox.Text))
                //{
                //    opReferrals = opReferrals.Where(comment => (comment.PatientForename.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                //                                            || comment.PatientSurname.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                //                                            || comment.PatientPathwayIdentifier.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                //                                            || comment.NhsNumber.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                //                                            || comment.LocalPatientID.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                //}

                // Filter by Speciality
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
                {
                    // Filter by Speciality
                    //if (!specialityDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    //{
                    //    opReferrals = opReferrals.Where(comment => comment.SpecName.Equals(specialityDropdown.SelectedValue)).ToList();
                    //}

                    // Filter by Consultant
                    //if (!consultantDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    //{
                    //    opReferrals = opReferrals.Where(comment => comment.Consultant.Equals(consultantDropdown.SelectedValue)).ToList();
                    //}

                    // Filter by Status
                    if (!statusDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        List<OpReferral> tempOpReferrals = new List<OpReferral>();

                        foreach (var item in opReferrals)
                        {
                            if (string.Equals(item.Status, statusDropdown.SelectedValue))
                            {
                                tempOpReferrals.Add(item);
                            }
                        }

                        opReferrals = tempOpReferrals;
                    }

                    // Filter by RTT Wait
                    //if (!ValidationRTTWaitDropDown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    //{
                    //    opReferrals = opReferrals.Where(comment => comment.WeekswaitGrouped.Equals(ValidationRTTWaitDropDown.SelectedValue)).ToList();
                    //}

                    // Filter by AttStatus
                    //if (!AttendanceStatusDropDown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    //{
                    //    opReferrals = opReferrals.Where(comment => comment.AttStatus.Equals(AttendanceStatusDropDown.SelectedValue)).ToList();
                    //}

                    // Filter by Future Appt Status
                    //if (!FutureApptStatusDropDownList.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    //{
                    //    string mainFilterCriteria = FutureApptStatusDropDownList.SelectedItem.Text;

                    //    if (string.Equals(mainFilterCriteria, Constants.NoDate))
                    //        opReferrals = opReferrals.Where(x => x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                    //    else
                    //        opReferrals = opReferrals.Where(x => !x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                    //}
                }



                if (null != opReferrals)
                {
                    // Bind to grid
                    RefineDatesInOpReferrals(opReferrals);
                    gvMain.DataSource = opReferrals;
                    gvMain.DataBind();
                    //gvMain.HeaderRow.TableSection = TableRowSection.TableHeader;

                    gvMain.PageIndex = 0;
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveCurrentDropdownValues()
        {
            if (specialityDropdown.Items.Count > 0)
            {
                specDdHiddenField.Value = specialityDropdown.SelectedItem.Text;
            }

            if (consultantDropdown.Items.Count > 0)
            {
                consultantDdHiddenField.Value = consultantDropdown.SelectedItem.Text;
            }

            if (statusDropdown.Items.Count > 0)
            {
                statusDdHiddenField.Value = statusDropdown.SelectedItem.Text;
            }

            if (ValidationRTTWaitDropDown.Items.Count > 0)
            {
                rttWaitDdHiddenField.Value = ValidationRTTWaitDropDown.SelectedItem.Text;
            }

            if (AttendanceStatusDropDown.Items.Count > 0)
            {
                attStatusDdHiddenField.Value = AttendanceStatusDropDown.SelectedItem.Text;
            }

            if (FutureApptStatusDropDownList.Items.Count > 0)
            {
                futureApptStatusDdHiddenField.Value = FutureApptStatusDropDownList.SelectedItem.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dropDown"></param>
        /// <param name="value"></param>
        private void SetSavedValue(DropDownList dropDown, string value)
        {
            if (dropDown.Items.Count > 0)
            {
                if (null != dropDown.Items.FindByText(value))
                {
                    dropDown.Items.FindByText(value).Selected = true;
                }
                else
                {
                    dropDown.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetControls()
        {
            IList<OpReferral> opReferrals = GetReferralData();

            if (null != opReferrals)
            {
                PopulateSpecialityDropdown(opReferrals);
                PopulateConsultantDropdown(opReferrals);
                PopulateAttendanceStatusDropDown(opReferrals);
                PopulateRTTWaitDropDown(opReferrals);
                PopulateStatusDropdown(opReferrals);
                PopulateFutureApptStatusDropDown();
            }

            InsertDropdownDefaultValue();
            patientTextbox.Text = string.Empty;
        }

        private void PopulateFutureApptStatusDropDown()
        {
            //TODO: Need to move to config file
            if (!string.IsNullOrEmpty(FutureApptStatus) &&
                !FutureApptStatus.Equals(ConfigurationManager.AppSettings["DropDownAllText"], StringComparison.OrdinalIgnoreCase))
            {
                FutureApptStatusDropDownList.Items.Add(new ListItem(FutureApptStatus));
            }
            else
            {
                FutureApptStatusDropDownList.DataSource = Utility.Utility.FutureApptStatusList;
                FutureApptStatusDropDownList.DataBind();
            }

        }

        private void RefineDatesInOpReferrals(IList<OpReferral> opReferrals)
        {
            //Add the 'To be booked by' column
            SpecialityTargetDatesDA specialityTargetDatesDA = new SpecialityTargetDatesDA();
            List<SpecialityTargetDate> specialityTargetDates = specialityTargetDatesDA.GetAllActiveSpecialityTargetDates();

            //Apply date time rules
            foreach (OpReferral item in opReferrals)
            {
                if (item.ReferralRequestReceivedDate != DateTime.MinValue)
                {
                    var specialityTargetDate = (from targetDate in specialityTargetDates
                                                where string.Equals(targetDate.ID.ToString(), item.Spec, StringComparison.OrdinalIgnoreCase)
                                                select targetDate).SingleOrDefault();

                    if (specialityTargetDate != null)
                        item.ToBeBookedByDate = item.ReferralRequestReceivedDate.Value.AddDays(specialityTargetDate.TargetDay);
                }

                item.DateOfBirth = this.ConvertDefaultDateTimeToNullConverter(item.DateOfBirth);
                // Can't do this since ReferralRequestReceivedDate is a key
                //item.ReferralRequestReceivedDate = this.ConvertDefaultDateTimeToNullConverter(item.ReferralRequestReceivedDate);
                item.RttClockStart = this.ConvertDefaultDateTimeToNullConverter(item.RttClockStart);
                item.RttBreachDate = this.ConvertDefaultDateTimeToNullConverter(item.RttBreachDate);
                item.AttendanceDate = this.ConvertDefaultDateTimeToNullConverter(item.AttendanceDate);
                // Can't do this since FutureClinicDate is a key
                //item.FutureClinicDate = this.ConvertDefaultDateTimeToNullConverter(item.FutureClinicDate);
            }
        }

        protected void exportButton_Click(object sender, EventArgs e)
        {
            //IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

            if (isQueryStringFiltering.Value != "true")
            {
                FilterGrid();
            }
            else
            {
                //Apply filters based on the query string paramenters
                //opReferrals = QueryStringBasedFiltering(opReferrals);
                IList<OpReferral>  opReferrals = GetReferralData();
                this.gvMain.DataSource = opReferrals;
                this.gvMain.DataBind();
            }

            FileExporter.ExportToExcel(GenerateDataTableForExport(), this.Page.Response, "StatusSummary.xls");
        }

        private DataTable GenerateDataTableForExport()
        {
            gvMain.AllowPaging = false;
            gvMain.DataBind();

            DataTable dataTable = new DataTable();
            foreach (DataControlField item in gvMain.Columns)
            {
                dataTable.Columns.Add(new DataColumn(item.HeaderText, typeof(string)));
            }

            foreach (GridViewRow gridViewRow in gvMain.Rows)
            {
                DataRow dataRow = dataTable.NewRow();

                for (int i = 0; i < gridViewRow.Cells.Count; i++)
                {
                    if (i == 0)
                    {
                        LinkButton linkButton = gridViewRow.FindControl("rowLink") as LinkButton;
                        dataRow[i] = linkButton.Text;
                    }
                    else
                    {
                        dataRow[i] = gridViewRow.Cells[i].Text.ToString();
                    }
                }

                dataTable.Rows.Add(dataRow);
                gvMain.DataBind();
            }

            gvMain.AllowPaging = true;

            return dataTable;
        }

        private IList<OpReferral> QueryStringBasedFiltering()
        {
            IList<OpReferral> opReferrals = CommentsManager.GetOpReferralsByParams(SpecialtyType, RttWait, FutureApptStatus);

            //if (!string.IsNullOrEmpty(SpecialtyType))
            //{
            //    opReferrals = opReferrals.Where(x => string.Equals(x.SpecName, SpecialtyType)).ToList();
            //}

            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "Blank Status")
                    opReferrals = opReferrals.Where(x => string.Equals(x.Status, "")).ToList();
                else
                    opReferrals = opReferrals.Where(x => string.Equals(x.Status, Status)).ToList();
            }

            //if (RttWait.Count > 0)
            //{
            //    opReferrals = opReferrals.Where(x => RttWait.Contains(x.WeekswaitGrouped)).ToList();
            //}

            //if (!string.IsNullOrEmpty(FutureApptStatus))
            //{
            //    opReferrals = FilterRecordsByFutureApptStatus(opReferrals, FutureApptStatus);
            //}

            if (IsFiltered)
            {
                isQueryStringFiltering.Value = "true";
            }

            return opReferrals;
        }

        private void QueryStringBasedDropdownItemSelection()
        {
            // If only a single items gets into the dropdown, there will be 2 items altogether.
            // Filtered list item and the 'All' list item
            if (!string.IsNullOrEmpty(SpecialtyType) && specialityDropdown.Items.Count == 2)
            {
                specialityDropdown.SelectedIndex = 1; // 'All' is index 0
            }

            if (!string.IsNullOrEmpty(Status) && statusDropdown.Items.Count == 2)
            {
                statusDropdown.SelectedIndex = 1;
            }

            if (RttWait.Count > 0 && ValidationRTTWaitDropDown.Items.Count == 2)
            {
                ValidationRTTWaitDropDown.SelectedIndex = 1;
            }

            if (!string.IsNullOrEmpty(FutureApptStatus) && FutureApptStatusDropDownList.Items.Count == 2)
            {
                FutureApptStatusDropDownList.SelectedIndex = 1;
            }
        }

        private IList<OpReferral> FilterRecordsByFutureApptStatus(IList<OpReferral> oref, string futureAppStatus)
        {
            IList<OpReferral> filtered = null;

            if (null != oref)
            {
                if (!futureAppStatus.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                {
                    if (string.Equals(futureAppStatus, Constants.NoDate))
                        filtered = oref.Where(x => x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                    else
                        filtered = oref.Where(x => !x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                }
                else
                {
                    filtered = oref;
                }
            }

            return filtered;
        }

        private IList<OpReferral> GetReferralData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Specialty"]))
            {
                return QueryStringBasedFiltering();
            }
            else
            {
                return CommentsManager.GetAllOpReferrals();
            }
        }

        private DateTime? ConvertDefaultDateTimeToNullConverter(DateTime? currentDateTime)
        {
            if (string.Equals(currentDateTime.Value, DateTime.MinValue))
                return null;
            else
                return currentDateTime;
        }

        #endregion
    }
}