using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using Nhs.Ptl.Comments;
using System.Web.UI;

namespace Nhs.Ptl.Comments.Web
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.FindControl("NavigationBar").Visible = false;
            Master.FindControl("HosName").Visible = false;
            Master.FindControl("NhsName").Visible = false;
        }

        protected void LoginUser_LoginError(object sender, EventArgs e)
        {
            string errorMessage = Constants.IncorrectCredentialsErrorMessage;    
        
            MembershipUser userInfo = Membership.GetUser(LoginUser.UserName);

            if(null != userInfo)
            {
                if (userInfo.IsLockedOut)
                {
                    errorMessage = Constants.UserLockedoutErrorMessage;
                }
                else if (!userInfo.IsApproved)
                {
                    errorMessage = Constants.UserNotActiveErrorMessage;
                }
                else
                {
                    errorMessage = Constants.IncorrectCredentialsErrorMessage;
                }
            }

            // Add the failure text to the validation group
            CustomValidator failureTextValidator = new CustomValidator();
            failureTextValidator.IsValid = false;
            failureTextValidator.ErrorMessage = errorMessage;
            failureTextValidator.ValidationGroup = "LoginUserValidationGroup";
            failureTextValidator.CssClass = "login alert-danger";
            this.Page.Validators.Add(failureTextValidator);
            
        }
    }
}