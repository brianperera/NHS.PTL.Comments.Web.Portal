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
            UsernameTextBox.Text = Membership.GetUser().UserName;

            //If the user is a Super Admin, Admin, enable the username field
            string[] roleNames = Roles.GetRolesForUser();

            foreach (var role in roleNames)
            {
                if (string.Equals(role, "Super Admin") || string.Equals(role, "Admin"))
                {
                    UsernameTextBox.Enabled = true;
                }
            }
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            MembershipUser currentUser = Membership.GetUser(UsernameTextBox.Text);

            if (currentUser.ChangePassword(currentUser.ResetPassword(), NewPassword.Text))
                Response.Redirect("ChangePasswordSuccess.aspx");
        }
    }
}