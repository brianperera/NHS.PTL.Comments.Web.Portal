using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Nhs.Ptl.Comments.Web
{
    public partial class Account_ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            MembershipUser currentUser = Membership.GetUser(UsernameTextBox.Text);

            if (currentUser.ChangePassword(currentUser.ResetPassword(), NewPassword.Text))
                Response.Redirect("ChangePasswordSuccess.aspx");
        }
    }
}