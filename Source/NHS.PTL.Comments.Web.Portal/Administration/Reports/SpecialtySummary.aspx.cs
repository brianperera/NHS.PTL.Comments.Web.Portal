using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments.Contracts.Dto;
using Nhs.Ptl.Comments.Utility;
using System.Data;
using System.Collections;

// IMPORTANT!!!: Look at the constants before you change the columns!
// Change the constants accordingly

namespace Nhs.Ptl.Comments.Web
{
    public partial class SpecialtySummary : System.Web.UI.Page
    {
        #region Constants


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                IList<OpReferral> opReferrals = CommentsManager.GetAllOpReferrals();

                if (null != opReferrals)
                {
                    IList<string> specialites = opReferrals.Select(x => x.SpecName).Distinct().ToList();
                    IList<string> statusTypes = opReferrals.Select(x => x.Status).Distinct().ToList();

                    //
                    // Here we create a DataTable with four columns.
                    //
                    DataTable specialitesTable = new DataTable();
                    specialitesTable.Columns.Add("Specialty", typeof(string));
                    
                    //Auto generate the columns
                    foreach (var statusType in statusTypes)
                    {
                        if(!string.IsNullOrEmpty(statusType))
                            specialitesTable.Columns.Add(statusType, typeof(string));
                    }

                    specialitesTable.Columns.Add("Totals", typeof(string));

                    //
                    // Here we add five DataRows.

                    int filteredListCount = 0;
                    ArrayList rowDataParam = new ArrayList();

                    foreach (string speciality in specialites)
                    {
                        int rowTotal = 0;
                        rowDataParam.Add(speciality);

                        foreach (var status in statusTypes)
                        {
                            if (!string.IsNullOrEmpty(status))
                            {
                                filteredListCount = opReferrals.Where(x => x.SpecName == speciality).Where(y => y.Status == status).Count();
                                rowDataParam.Add(filteredListCount.ToString());
                                rowTotal += filteredListCount;
                            }
                        }

                        rowDataParam.Add(rowTotal.ToString());

                        specialitesTable.Rows.Add(rowDataParam.ToArray());
                        rowDataParam.Clear();
                    }

                    statusSummaryGrid.DataSource = specialitesTable;
                    statusSummaryGrid.DataBind();
                }

            }
        }


    }
}