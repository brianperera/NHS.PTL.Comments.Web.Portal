using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments;

public partial class UserControls_DataEntryControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        HiddenField uniqueIdHiddenField = Parent.FindControl("uniqueIdHiddenField") as HiddenField;
        if (null != uniqueIdHiddenField)
        {
            uniqueIdentifier.Text = uniqueIdHiddenField.Value;
        }
    }
}