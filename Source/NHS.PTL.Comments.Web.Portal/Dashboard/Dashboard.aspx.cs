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
                    PopulateStatusDropdown(opReferrals);
                    PopulateSpecialityDropdown(opReferrals);
                    PopulateConsultantDropdown(opReferrals);
                    PopulateAttendanceStatusDropDown(opReferrals);
                    PopulateRTTWaitDropDown(opReferrals);
                    InsertDropdownDefaultValue();
                    //referrelGrid.DataSource = new List<string>();
                    //referrelGrid.DataBind();

                    referrelGrid.DataSource = opReferrals;
                    referrelGrid.DataBind();
                }

            }
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

            DataEntryControl1.ClearField();
            DataEntryControl1.PopulateDefaultValues(DataEntryControl1.GetAllComments());
        }

        protected void referrelGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            referrelGrid.PageIndex = e.NewPageIndex;
            FilterGrid();
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

                        //InsertDropdownDefaultValue();
                        consultantDropdown.Items[0].Selected = true;
                        statusDropdown.Items[0].Selected = true;
                        RTTWaitDropDown.Items[0].Selected = true;
                        AttendanceStatusDropDown.Items[0].Selected = true;
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

                        //InsertDropdownDefaultValue();

                        statusDropdown.Items[0].Selected = true;
                        RTTWaitDropDown.Items[0].Selected = true;
                        AttendanceStatusDropDown.Items[0].Selected = true;
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

                        //InsertDropdownDefaultValue();

                        RTTWaitDropDown.Items[0].Selected = true;
                        AttendanceStatusDropDown.Items[0].Selected = true;
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

                        //InsertDropdownDefaultValue();

                        AttendanceStatusDropDown.Items[0].Selected = true;
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

                        //InsertDropdownDefaultValue();
                    }
                }
            }
            else
            {
                ResetControls();
            }
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
                            AttendanceStatusDropDown
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
                }

                // Filter by Status

                if (null != opReferrals)
                {
                    // Bind to grid
                    referrelGrid.DataSource = opReferrals;
                    referrelGrid.DataBind();

                    referrelGrid.PageIndex = 0;
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
                    dropDown.SelectedItem.Selected = false;
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
            }

            InsertDropdownDefaultValue();
            patientTextbox.Text = string.Empty;
        }

        #endregion


    }
}