﻿using System;
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
        public const string WithDate = "With Date";
        public const string NoDate = "No Date";
        public const string ConsultantFieldName = "Consultant";
        public const string SpecialtyFieldName = "SpecName";
        public const string WeekswaitGroupedFieldName = "WeekswaitGrouped";
        public const string AttStatusFieldName = "AttStatus";
        public const string FutureClinicDateFieldName = "FutureClinicDate";
        public const int ToBeBookedByColumnNo = 4;
        public const int ReferralRequestColumnNo = 11;
        public const int FutureClinicDateColumnNo = 21;
    }
}