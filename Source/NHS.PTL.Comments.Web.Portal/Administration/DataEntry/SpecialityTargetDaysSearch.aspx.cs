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
    public partial class SpecialityTargetDaysSearch : System.Web.UI.Page
    {
        List<SpecialityTargetDate> specialityTargetDates = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindInitialData();
        }

        private void BindInitialData()
        {
            // Bind users to Grid.
            SpecialityTargetDatesDA specialityTargetDatesDA = new SpecialityTargetDatesDA();
            specialityTargetDates = specialityTargetDatesDA.GetAllSpecialityTargetDates();

            foreach (var item in specialityTargetDates)
            {
                if (item.Activate)
                    item.DisplayActivate = "Active";
                else
                    item.DisplayActivate = "Inactive";
            }

            Status_Grid.DataSource = specialityTargetDates;
            Status_Grid.DataBind();
        }
    }
}