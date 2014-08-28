using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Contracts.Dto;
using Nhs.Ptl.Comments.Utility;
using System.Data;

// IMPORTANT!!!: Look at the constants before you change the columns!
// Change the constants accordingly

namespace Nhs.Ptl.Comments.Web
{
    public partial class Validations : System.Web.UI.Page
    {
        #region Constants

        private const int ReferralRequestColumnNo = 9;
        private const int FutureClinicDateColumnNo = 18;

        #endregion

        string[] futureApptStatusList = { "With Date", "No Date" };

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

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (!string.IsNullOrEmpty(SpecialtyType))
                {
                    opReferrals = opReferrals.Where(x => string.Equals(x.SpecName, SpecialtyType)).ToList();
                }

                if (null != opReferrals)
                {
                    PopulateStatusDropdown(opReferrals);
                    PopulateSpecialityDropdown(opReferrals);
                    PopulateConsultantDropdown(opReferrals);
                    PopulateAttendanceStatusDropDown(opReferrals);
                    PopulateRTTWaitDropDown(opReferrals);
                    PopulateFutureApptStatusDropDown();
                    InsertDropdownDefaultValue();

                    //referrelGrid.DataSource = new List<string>();
                    //referrelGrid.DataBind();

                    RefineDatesInOpReferrals(opReferrals);
                    //referrelGrid.DataSource = opReferrals;
                    //referrelGrid.DataBind();
                    //referrelGrid.HeaderRow.TableSection = TableRowSection.TableHeader;

                    this.gvMain.DataSource = opReferrals;
                    this.gvMain.DataBind();
                }

            }
        }

        private DateTime? ConvertDefaultDateTimeToNullConverter(DateTime? currentDateTime)
        {
            if (string.Equals(currentDateTime.Value, DateTime.MinValue))
                return null;
            else
                return currentDateTime;
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        protected void resetButton_Click(object sender, EventArgs e)
        {
            //specialityDropdown.SelectedIndex = 0;
            //consultantDropdown.SelectedIndex = 0;
            //statusDropdown.SelectedIndex = 0;
            //RTTWaitDropDown.SelectedIndex = 0;
            //AttendanceStatusDropDown.SelectedIndex = 0;
            //IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();
            //if (null != opReferrals)
            //{
            //    PopulateSpecialityDropdown(opReferrals);
            //    PopulateConsultantDropdown(opReferrals);
            //    PopulateAttendanceStatusDropDown(opReferrals);
            //    PopulateRTTWaitDropDown(opReferrals);
            //}

            //PopulateStatusDropdown();
            //InsertDropdownDefaultValue();
            //patientTextbox.Text = string.Empty;

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
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {

                    IList<OpReferral> filtered = opReferrals.Where(x => x.SpecName == specialityDropdown.SelectedItem.Text).ToList();

                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        consultantDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        statusDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        RTTWaitDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.WeekswaitGrouped).Distinct().ToList());
                        RTTWaitDropDown.DataBind();
                        SetSavedValue(RTTWaitDropDown, rttWaitDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = GetDropdownDefaultValueToListItems(futureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        //InsertDropdownDefaultValue();
                        //consultantDropdown.ClearSelection();
                        //statusDropdown.ClearSelection();
                        //RTTWaitDropDown.ClearSelection();
                        //AttendanceStatusDropDown.ClearSelection();
                        //consultantDropdown.Items[0].Selected = true;
                        //statusDropdown.Items[0].Selected = true;
                        //RTTWaitDropDown.Items[0].Selected = true;
                        //AttendanceStatusDropDown.Items[0].Selected = true;
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
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {

                    IList<OpReferral> filtered = opReferrals.Where(x => x.Consultant == consultantDropdown.SelectedItem.Text).ToList();

                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        statusDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        RTTWaitDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.WeekswaitGrouped).Distinct().ToList());
                        RTTWaitDropDown.DataBind();
                        SetSavedValue(RTTWaitDropDown, rttWaitDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = GetDropdownDefaultValueToListItems(futureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        //InsertDropdownDefaultValue();
                        //statusDropdown.Items[0].Selected = true;
                        //RTTWaitDropDown.Items[0].Selected = true;
                        //AttendanceStatusDropDown.Items[0].Selected = true;
                    }
                }
            }
            else
            {
                ResetControls();
            }
        }

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

                        specialityDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        consultantDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        RTTWaitDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.WeekswaitGrouped).Distinct().ToList());
                        RTTWaitDropDown.DataBind();
                        SetSavedValue(RTTWaitDropDown, rttWaitDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = GetDropdownDefaultValueToListItems(futureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        //InsertDropdownDefaultValue();
                        //RTTWaitDropDown.ClearSelection();
                        //AttendanceStatusDropDown.ClearSelection();
                        //RTTWaitDropDown.Items[0].Selected = true;
                        //AttendanceStatusDropDown.Items[0].Selected = true;
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
            if (!RTTWaitDropDown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {

                    IList<OpReferral> filtered = opReferrals.Where(x => x.WeekswaitGrouped == RTTWaitDropDown.SelectedItem.Text).ToList();

                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        consultantDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        statusDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = GetDropdownDefaultValueToListItems(futureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);

                        //InsertDropdownDefaultValue();
                        //AttendanceStatusDropDown.Items[0].Selected = true;
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
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {

                    IList<OpReferral> filtered = opReferrals.Where(x => x.AttStatus == AttendanceStatusDropDown.SelectedItem.Text).ToList();

                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        consultantDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        statusDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        RTTWaitDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.RttStatus).Distinct().ToList());
                        RTTWaitDropDown.DataBind();
                        SetSavedValue(RTTWaitDropDown, rttWaitDdHiddenField.Value);

                        FutureApptStatusDropDownList.DataSource = GetDropdownDefaultValueToListItems(futureApptStatusList.ToList());
                        FutureApptStatusDropDownList.DataBind();
                        SetSavedValue(FutureApptStatusDropDownList, futureApptStatusDdHiddenField.Value);
                        //InsertDropdownDefaultValue();
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
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {
                    string mainFilterCriteria = FutureApptStatusDropDownList.SelectedItem.Text;
                    IList<OpReferral> filtered;

                    if (string.Equals(mainFilterCriteria, Constants.NoDate))
                        filtered = opReferrals.Where(x => x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                    else
                        filtered = opReferrals.Where(x => !x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();


                    if (null != filtered)
                    {
                        // Save current selected values
                        SaveCurrentDropdownValues();

                        specialityDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.SpecName).Distinct().ToList());
                        specialityDropdown.DataBind();
                        SetSavedValue(specialityDropdown, specDdHiddenField.Value);

                        consultantDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Consultant).Distinct().ToList());
                        consultantDropdown.DataBind();
                        SetSavedValue(consultantDropdown, consultantDdHiddenField.Value);

                        statusDropdown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.Status).Distinct().ToList());
                        statusDropdown.DataBind();
                        SetSavedValue(statusDropdown, statusDdHiddenField.Value);

                        AttendanceStatusDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.AttStatus).Distinct().ToList());
                        AttendanceStatusDropDown.DataBind();
                        SetSavedValue(AttendanceStatusDropDown, attStatusDdHiddenField.Value);

                        RTTWaitDropDown.DataSource = GetDropdownDefaultValueToListItems(filtered.Select(x => x.RttStatus).Distinct().ToList());
                        RTTWaitDropDown.DataBind();
                        SetSavedValue(RTTWaitDropDown, rttWaitDdHiddenField.Value);

                        //InsertDropdownDefaultValue();
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
                if (e.Row.Cells[ReferralRequestColumnNo].Text.Equals(DateTime.MinValue.ToShortDateString()))
                {
                    e.Row.Cells[ReferralRequestColumnNo].Text = string.Empty;
                }

                if (e.Row.Cells[FutureClinicDateColumnNo].Text.Equals(DateTime.MinValue.ToShortDateString()))
                {
                    e.Row.Cells[FutureClinicDateColumnNo].Text = string.Empty;
                }  
            }
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            FilterGrid();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add default 'All' item to each filtered list
        /// </summary>
        private List<string> GetDropdownDefaultValueToListItems(List<string> filteredItems)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
            {
                string defaultItem = ConfigurationManager.AppSettings["DropDownAllText"];

                if (!filteredItems.Contains(defaultItem))
                {
                    filteredItems.Insert(0, defaultItem);
                }
            }

            return filteredItems;
        }

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
            RTTWaitDropDown.DataSource = rttWaitList;
            RTTWaitDropDown.DataBind();
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
                ListItem defaultItem = new ListItem(ConfigurationManager.AppSettings["DropDownAllText"]);

                // This is just to avoid checking one by one :)
                List<DropDownList> dropDownLists = new List<DropDownList>() 
                { 
                            specialityDropdown,
                            consultantDropdown,
                            statusDropdown,
                            RTTWaitDropDown,
                            AttendanceStatusDropDown,
                            FutureApptStatusDropDownList
                };

                foreach (DropDownList ddl in dropDownLists)
                {
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
            List<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals().ToList();

            // Filter data
            if (null != opReferrals)
            {
                // Filter by patient name
                if (!string.IsNullOrEmpty(patientTextbox.Text))
                {
                    opReferrals = opReferrals.Where(comment => (comment.PatientForename.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                                            || comment.PatientSurname.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                                                            || comment.PatientPathwayIdentifier.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                                            || comment.NhsNumber.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                                            || comment.LocalPatientID.IndexOf(patientTextbox.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                // Filter by Speciality
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
                {
                    // Filter by Speciality
                    if (!specialityDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        opReferrals = opReferrals.Where(comment => comment.SpecName.Equals(specialityDropdown.SelectedValue)).ToList();
                    }

                    // Filter by Consultant
                    if (!consultantDropdown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        opReferrals = opReferrals.Where(comment => comment.Consultant.Equals(consultantDropdown.SelectedValue)).ToList();
                    }

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
                    if (!RTTWaitDropDown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        opReferrals = opReferrals.Where(comment => comment.WeekswaitGrouped.Equals(RTTWaitDropDown.SelectedValue)).ToList();
                    }

                    // Filter by AttStatus
                    if (!AttendanceStatusDropDown.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        opReferrals = opReferrals.Where(comment => comment.AttStatus.Equals(AttendanceStatusDropDown.SelectedValue)).ToList();
                    }

                    // Filter by Future Appt Status
                    if (!FutureApptStatusDropDownList.SelectedValue.Equals(ConfigurationManager.AppSettings["DropDownAllText"]))
                    {
                        string mainFilterCriteria = FutureApptStatusDropDownList.SelectedItem.Text;

                        if (string.Equals(mainFilterCriteria, Constants.NoDate))
                            opReferrals = opReferrals.Where(x => x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                        else
                            opReferrals = opReferrals.Where(x => !x.FutureClinicDate.ToString().Equals("01/01/0001 00:00:00")).ToList();
                    }
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

            if (RTTWaitDropDown.Items.Count > 0)
            {
                rttWaitDdHiddenField.Value = RTTWaitDropDown.SelectedItem.Text;
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
            IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

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
            FutureApptStatusDropDownList.DataSource = futureApptStatusList;
            FutureApptStatusDropDownList.DataBind();
        }

        private void RefineDatesInOpReferrals(IList<OpReferral> opReferrals)
        {
            //Apply date time rules
            foreach (OpReferral item in opReferrals)
            {
                item.DateOfBirth = this.ConvertDefaultDateTimeToNullConverter(item.DateOfBirth);
                // Can't do this since ReferralRequestReceivedDate is a key
                //item.ReferralRequestReceivedDate = this.ConvertDefaultDateTimeToNullConverter(item.ReferralRequestReceivedDate);
                item.RttClockStart = this.ConvertDefaultDateTimeToNullConverter(item.RttClockStart);
                item.RttBreachDate = this.ConvertDefaultDateTimeToNullConverter(item.RttBreachDate);
                item.AttendanceDate = this.ConvertDefaultDateTimeToNullConverter(item.AttendanceDate);
                // Can't do this since FutureClinicDate is a key
                //item.FutureClinicDate = this.ConvertDefaultDateTimeToNullConverter(item.FutureClinicDate); ;
            }
        }

        #endregion
}
}