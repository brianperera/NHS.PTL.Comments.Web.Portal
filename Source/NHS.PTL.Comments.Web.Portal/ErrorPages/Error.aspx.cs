using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nhs.PTL.Comments.Web.Portal
{
    public partial class ErrorPages_Error : System.Web.UI.Page
    {
        public string ErrorStatus
        {
            get
            {
                string errorStatus = string.Empty;

                if (Request.QueryString["status"] != null)
                    errorStatus = Request.QueryString["status"];

                return errorStatus;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void DisplayMessage(string executionStatus)
        {
            MessageLabel.Text = ErrorStatus == "404" ? "Record Updated Successfully" : "Record Not Updated";
            MessageLabel.CssClass = ErrorStatus == "404" ? "alert-success" : "alert-danger";
        }
    }
}