using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhs.PTL.Comments.Web.Portal.Utility;
using System.Data;

namespace Nhs.PTL.Comments.Web.Portal
{
    public partial class UserSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable userTable = new DataTable();
                userTable.Columns.Add("UserName", typeof(string));
                userTable.Columns.Add("FirstName", typeof(string));
                userTable.Columns.Add("LastName", typeof(string));
                userTable.Columns.Add("Email", typeof(string));

                MembershipUserCollection users = Membership.GetAllUsers();

                foreach (MembershipUser user in users)
                {
                    userTable.Rows.Add(user.UserName, Profile.GetProfile(user.UserName).FirstName, Profile.GetProfile(user.UserName).LastName, user.Email);
                }

                // Bind users to Grid.
                Users_ListBox.DataSource = userTable;
                Users_ListBox.DataBind();
            }
        }
    }
}