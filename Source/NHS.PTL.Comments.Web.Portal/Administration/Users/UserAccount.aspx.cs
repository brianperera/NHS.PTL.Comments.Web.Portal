using System;
using System.Linq;
using System.Web.Security;
using Nhs.Ptl.Comments;
using Nhs.Ptl.Comments.Utility;

namespace Nhs.Ptl.Comments.Web
{
    public partial class UserAccount : System.Web.UI.Page
    {
        public string ActionType
        {
            get
            {
                string actionType = Constants.Create;

                if (Request.QueryString["action"] != null)
                    actionType = Request.QueryString["action"];

                return actionType;
            }
        }

        public string UserName
        {
            get
            {
                string username = string.Empty;

                if (Request.QueryString["username"] != null)
                    username = Request.QueryString["username"];

                return username;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the roles
                DropDownListRoles.DataSource = Roles.GetAllRoles();
                DropDownListRoles.DataBind();

                if (ActionType == Constants.Update)
                {
                    UpdateAccountPageHeader.Visible = true;
                    SubmitAccountDetails.Text = "Update Account";
                    DeleteAccount.Visible = true;
                    UsernameTextbox.Enabled = false;

                    if (!string.IsNullOrEmpty(UserName))
                    {
                        MembershipUser currentUser = Membership.GetUser(UserName);
                        ProfileCommon currentProfile = new ProfileCommon();
                        
                        currentProfile = Profile.GetProfile(currentUser.UserName);
                        
                        UsernameTextbox.Text = currentUser.UserName;
                        EmailTextbox.Text = currentUser.Email;
                        DropDownListRoles.SelectedValue = Roles.GetRolesForUser(UserName).FirstOrDefault();
                        IsActive_RadioButton.Checked = currentUser.IsApproved;
                        FirstNameTextbox.Text = currentProfile.FirstName;
                        LastNameTextBox.Text = currentProfile.LastName;
                        ContactTelNoTextBox.Text = currentProfile.Phone;
                    }
                }
                else
                {
                    CreateAccountPageHeader.Visible = true;
                    SubmitAccountDetails.Text = "Create Account";
                }
            }
        }

        public void SubmitAccountDetails_OnClick(object sender, EventArgs args)
        {
            if (ActionType == Constants.Update)
            {
                UpdateUserAccount();
            }
            else
            {
                CreateUserAccount();
            }
        }

        private void UpdateUserAccount()
        {
            MembershipUser currentUser = Membership.GetUser(UserName);            
            currentUser.Email = EmailTextbox.Text;

            // remove user from current roles
            // the user will only be assinged 1 role
            Roles.RemoveUserFromRoles(UserName, Roles.GetRolesForUser(UserName));

            // add new role to user
            if (!string.IsNullOrEmpty(DropDownListRoles.SelectedValue))
            {
                Roles.AddUserToRole(UserName, DropDownListRoles.SelectedValue);
            }
            
            // Update active property
            currentUser.IsApproved = IsActive_RadioButton.Checked;

            Membership.UpdateUser(currentUser);

            // Update profile
            ProfileCommon currentProfile = new ProfileCommon(); 
            currentProfile = Profile.GetProfile(currentUser.UserName);

            currentProfile.FirstName = FirstNameTextbox.Text;
            currentProfile.LastName = LastNameTextBox.Text;
            currentProfile.Phone = ContactTelNoTextBox.Text;
            currentProfile.Save();
            
            Response.Redirect("UserSearch.aspx");
        }

        private void CreateUserAccount()
        {
            // Create new user and retrieve create status result.
            MembershipCreateStatus status;
            string passwordQuestion = "Question";
            string passwordAnswer = "Answer";

            try
            {                
                MembershipUser newUser = Membership.CreateUser(UsernameTextbox.Text, PasswordTextbox.Text,
                                                               EmailTextbox.Text, passwordQuestion,
                                                               passwordAnswer, true, out status);
                if (newUser == null)
                {
                    DisplayMessage(false, MembershipHelper.GetErrorMessage(status));
                }
                else
                {
                    DisplayMessage(true, "User created successfully");

                    // Assign the role to the user
                    if (!string.IsNullOrEmpty(DropDownListRoles.SelectedValue))
                        Roles.AddUserToRole(newUser.UserName, DropDownListRoles.SelectedValue);

                    // Update profile
                    ProfileCommon currentProfile = new ProfileCommon();
                    currentProfile = Profile.GetProfile(newUser.UserName);

                    currentProfile.FirstName = FirstNameTextbox.Text;
                    currentProfile.LastName = LastNameTextBox.Text;
                    currentProfile.Phone = ContactTelNoTextBox.Text;
                    currentProfile.Save();
                }
            }
            catch
            {
                DisplayMessage(false, "An exception occurred creating the user.");
            }
        }

        private void DisplayMessage(bool executionStatus, string message)
        {
            MessageLabel.Text = message;
            MessageLabel.CssClass = executionStatus == true ? "alert-success" : "alert-danger";
        }

        protected void DeleteAccount_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                Membership.DeleteUser(UserName);
                Response.Redirect("UserSearch.aspx");
            }
        }
    }
}