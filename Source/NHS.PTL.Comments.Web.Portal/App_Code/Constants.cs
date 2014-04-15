using System;
using System.IO;

/// <summary>
/// Summary description for LogProvider
/// </summary>

namespace Nhs.Ptl.Comments
{
    public static class Constants
    {
        public const string Update = "update";
        public const string Create = "add";
        public const string EndDateNotSpecified = "";
        public const string IncorrectCredentialsErrorMessage = "Incorrect username or password. Please try again.";
        public const string UserLockedoutErrorMessage = "Your account has been locked out. Please contact an administrator.";
        public const string UserNotActiveErrorMessage = "Your account has not yet been approved. Please contact an administrator.";
    }
}