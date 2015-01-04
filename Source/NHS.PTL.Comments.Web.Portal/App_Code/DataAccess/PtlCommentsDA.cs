﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Nhs.Ptl.Comments.Contracts.Dto;
using System.Data;
using System.Globalization;
using System.Configuration;

/// <summary>
/// Summary description for PtlCommentsDA
/// </summary>
/// 
namespace Nhs.Ptl.Comments.DataAccess
{
    public class PtlCommentsDA : DataAccessBase, IPtlCommentsDA
    {

        /// <summary>
        /// Gets all PTL comments
        /// </summary>
        /// <returns></returns>
        public IList<OpReferral> GetAllOpReferrals(CommandType commandType)
        {
            IList<OpReferral> opReferrals = null;

            try
            {
                using (SqlConnection connection = GetConnection(true))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = commandType;
                        command.CommandText = "SelectAllPtlReferrals";

                        if (commandType == CommandType.StoredProcedure)
                        {
                            command.CommandText = "SelectAllPtlReferrals";
                        }
                        else if (commandType == CommandType.Text)
                        {
                            command.CommandText = "SELECT * FROM OP_Referral_PTL";
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                opReferrals = new List<OpReferral>();
                                OpReferral opReferral;

                                while (reader.Read())
                                {
                                    opReferral = FillOpReferral(reader);

                                    opReferrals.Add(opReferral);

                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return opReferrals;
        }

        public IList<OpReferral> GetOpReferralByParams(string specialty,
                                                    IList<string> rttWait,
                                                    string futureAppStatus)
        {
            IList<OpReferral> opReferrals = null;

            try
            {
                using (SqlConnection connection = GetConnection(true))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;

                        // Speciality filter
                        command.CommandText = "SELECT * FROM OP_Referral_PTL"
                                                + " WHERE SpecName = '"
                                                + specialty
                                                + "'";

                        // WeekswaitGrouped filter
                        if (rttWait.Count > 0)
                        {
                            command.CommandText += " AND ( WeekswaitGrouped ='" + rttWait[0] + "'";

                            foreach (string wait in rttWait)
                            {
                                // Ignoring the 1st element since it's aready appended
                                if (wait.Equals(rttWait[0], StringComparison.OrdinalIgnoreCase))
                                {
                                    continue;
                                }

                                command.CommandText += " OR WeekswaitGrouped = '" + wait + "'";
                            }

                            command.CommandText += " )";
                        }


                        if (!futureAppStatus.Equals(ConfigurationManager.AppSettings["DropDownAllText"], StringComparison.OrdinalIgnoreCase))
                        {
                            if (futureAppStatus.Equals(Constants.NoDate, StringComparison.OrdinalIgnoreCase))
                            {
                                command.CommandText += " AND FutureClinicDate IS NULL";
                            }
                            else
                            {
                                command.CommandText += " AND FutureClinicDate IS NOT NULL";
                            }

                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                opReferrals = new List<OpReferral>();
                                OpReferral opReferral;

                                while (reader.Read())
                                {
                                    opReferral = FillOpReferral(reader);

                                    opReferrals.Add(opReferral);

                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return opReferrals;
        }

        public IList<OpReferral> GetOpReferralByParams(string searchText,
                                                        string specialty,
                                                        string consultant,
                                                        string rttWait,
                                                        string attStatus,
                                                        string futureApptStatus)
        {
            IList<OpReferral> opReferrals = null;

            try
            {
                using (SqlConnection connection = GetConnection(true))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;

                        command.CommandText = "SELECT * FROM OP_Referral_PTL";

                        if (!string.IsNullOrEmpty(searchText))
                        {
                            command.CommandText += " WHERE ( PatientForename = '"
                                                + searchText
                                                + "' OR PatientSurname = '"
                                                + searchText
                                                + "' OR PatientPathwayIdentifier = '"
                                                + searchText
                                                + "' OR NHSNumber = '"
                                                + searchText
                                                + "' OR LocalPatientID = '"
                                                + searchText
                                                + "' )";
                        }

                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DropDownAllText"]))
                        {
                            if (!string.IsNullOrEmpty(specialty)
                                && !specialty.Equals(ConfigurationManager.AppSettings["DropDownAllText"], StringComparison.OrdinalIgnoreCase))
                            {
                                if (command.CommandText.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    command.CommandText += " AND SpecName = '" + specialty + "'";
                                }
                                else
                                {
                                    command.CommandText += " WHERE SpecName = '" + specialty + "'";
                                }
                            }

                            if (!string.IsNullOrEmpty(consultant)
                                && !consultant.Equals(ConfigurationManager.AppSettings["DropDownAllText"], StringComparison.OrdinalIgnoreCase))
                            {
                                if (command.CommandText.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    command.CommandText += " AND Consultant = '" + consultant + "'";
                                }
                                else
                                {
                                    command.CommandText += " WHERE Consultant = '" + consultant + "'";
                                }
                            }

                            if (!string.IsNullOrEmpty(rttWait)
                                && !rttWait.Equals(ConfigurationManager.AppSettings["DropDownAllText"], StringComparison.OrdinalIgnoreCase))
                            {
                                if (command.CommandText.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    command.CommandText += " AND WeekswaitGrouped = '" + rttWait + "'";
                                }
                                else
                                {
                                    command.CommandText += " WHERE WeekswaitGrouped = '" + rttWait + "'";
                                }
                            }

                            if (!string.IsNullOrEmpty(attStatus)
                                && !attStatus.Equals(ConfigurationManager.AppSettings["DropDownAllText"], StringComparison.OrdinalIgnoreCase))
                            {
                                if (command.CommandText.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    command.CommandText += " AND AttStatus = '" + attStatus + "'";
                                }
                                else
                                {
                                    command.CommandText += " WHERE AttStatus = '" + attStatus + "'";
                                }
                            }

                            if (!string.IsNullOrEmpty(futureApptStatus)
                                && !futureApptStatus.Equals(ConfigurationManager.AppSettings["DropDownAllText"], StringComparison.OrdinalIgnoreCase))
                            {
                                if (command.CommandText.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    if (futureApptStatus.Equals(Constants.NoDate, StringComparison.OrdinalIgnoreCase))
                                    {
                                        command.CommandText += " AND FutureClinicDate IS NULL";
                                    }
                                    else
                                    {
                                        command.CommandText += " AND FutureClinicDate IS NOT NULL";
                                    }
                                }
                                else
                                {
                                    if (futureApptStatus.Equals(Constants.NoDate, StringComparison.OrdinalIgnoreCase))
                                    {
                                        command.CommandText += " WHERE FutureClinicDate IS NULL";
                                    }
                                    else
                                    {
                                        command.CommandText += " WHERE FutureClinicDate IS NOT NULL";
                                    }
                                }

                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                opReferrals = new List<OpReferral>();
                                OpReferral opReferral;

                                while (reader.Read())
                                {
                                    opReferral = FillOpReferral(reader);

                                    opReferrals.Add(opReferral);

                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return opReferrals;
        }

        public IList<OpReferral> GetOpReferralByFieldName(string fieldName, string value)
        {
            IList<OpReferral> opReferrals = null;

            try
            {
                using (SqlConnection connection = GetConnection(true))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;

                        if (fieldName.Equals(Constants.FutureClinicDateFieldName, StringComparison.OrdinalIgnoreCase))
                        {
                            if (value.Equals(Constants.NoDate, StringComparison.OrdinalIgnoreCase))
                            {
                                command.CommandText = "SELECT * FROM OP_Referral_PTL WHERE "
                                                    + fieldName + " IS NULL";
                            }
                            else
                            {
                                command.CommandText = "SELECT * FROM OP_Referral_PTL WHERE "
                                                    + fieldName + " IS NOT NULL";
                            }
                        }
                        else
                        {
                            command.CommandText = "SELECT * FROM OP_Referral_PTL"
                                                    + " WHERE " + fieldName
                                                    + " = '"
                                                    + value
                                                    + "'";
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                opReferrals = new List<OpReferral>();
                                OpReferral opReferral;

                                while (reader.Read())
                                {
                                    opReferral = FillOpReferral(reader);

                                    opReferrals.Add(opReferral);

                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return opReferrals;
        }        

        /// <summary>
        /// Adds new PTL comment
        /// </summary>
        /// <param name="ptlComment"></param>
        /// <returns></returns>
        public bool AddPtlComment(PtlComment ptlComment)
        {
            bool isAdded = false;

            if (null == ptlComment)
            {
                throw new ArgumentNullException("ptlComment", "PTL Comment cannot be null");
            }

            try
            {
                using (SqlConnection connection = GetConnection(false))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "InsertPtlComment";

                        SqlParameter rowIdentifier = GetParameter("@UniqueCDSRowIdentifier", SqlDbType.NVarChar, ptlComment.UniqueCdsRowIdentifier);
                        SqlParameter patientPathwayId = GetParameter("@PatientPathwayIdentifier", SqlDbType.NVarChar, ptlComment.PatientPathwayIdentifier);
                        SqlParameter spec = GetParameter("@Spec", SqlDbType.Float, ptlComment.Spec);
                        SqlParameter referralDate = GetParameter("@ReferralRequestReceivedDate", SqlDbType.DateTime, ptlComment.ReferralRequestReceivedDate);
                        SqlParameter status = GetParameter("@Status", SqlDbType.VarChar, ptlComment.Status);
                        SqlParameter appointmentDate = GetParameter("@AppointmentDate", SqlDbType.Date, ptlComment.AppointmentDate);
                        SqlParameter updatedDate = GetParameter("@UpdatedDate", SqlDbType.DateTime, ptlComment.UpdatedDate);
                        SqlParameter comment = GetParameter("@Comment", SqlDbType.VarChar, ptlComment.Comment);
                        SqlParameter createdBy = GetParameter("@CreatedBy", SqlDbType.VarChar, ptlComment.CreatedBy);

                        command.Parameters.Add(rowIdentifier);
                        command.Parameters.Add(patientPathwayId);
                        command.Parameters.Add(spec);
                        command.Parameters.Add(referralDate);
                        command.Parameters.Add(status);
                        command.Parameters.Add(appointmentDate);
                        command.Parameters.Add(updatedDate);
                        command.Parameters.Add(comment);
                        command.Parameters.Add(createdBy);

                        if (command.ExecuteNonQuery() > -1)
                        {
                            isAdded = true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isAdded;
        }

        public IList<PtlComment> GetAllPtlComments()
        {
            IList<PtlComment> ptlComments = null;

            try
            {
                using (SqlConnection connection = GetConnection(false))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SelectAllPtlComments";

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                ptlComments = new List<PtlComment>();
                                PtlComment comment;

                                while (reader.Read())
                                {
                                    comment = new PtlComment();
                                    comment.UniqueCdsRowIdentifier = !Convert.IsDBNull(reader["UniqueCDSRowIdentifier"]) ? reader["UniqueCDSRowIdentifier"].ToString() : string.Empty;
                                    comment.Comment = !Convert.IsDBNull(reader["Comment"]) ? reader["Comment"].ToString() : string.Empty;
                                    comment.Status = !Convert.IsDBNull(reader["Status"]) ? reader["Status"].ToString() : string.Empty;
                                    comment.PatientPathwayIdentifier = reader["PatientPathwayIdentifier"].ToString();
                                    comment.Spec = reader["Spec"].ToString();
                                    comment.CreatedBy = !Convert.IsDBNull(reader["CreatedBy"]) ? reader["CreatedBy"].ToString() : string.Empty;

                                    DateTime tempDateTime = DateTime.MinValue;

                                    DateTime.TryParse(reader["AppointmentDate"].ToString(), out tempDateTime);
                                    comment.AppointmentDate = tempDateTime;

                                    DateTime.TryParse(reader["UpdatedDate"].ToString(), out tempDateTime);
                                    comment.UpdatedDate = tempDateTime;

                                    DateTime.TryParse(reader["ReferralRequestReceivedDate"].ToString(), out tempDateTime);
                                    comment.ReferralRequestReceivedDate = tempDateTime;

                                    //Short Circuit Logic

                                    ptlComments.Add(comment);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ptlComments;
        }
        /// <summary>
        /// Gets all unique row identifiers
        /// </summary>
        /// <returns></returns>
        /// TODO: Will not be used anymore
        public IList<string> GetAllUniqueRowIdentifiers()
        {
            IList<string> uniqueRowIdentifiers = null;

            try
            {
                using (SqlConnection connection = GetConnection(true))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SelectAllUniqueRowIdentifiers";

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                uniqueRowIdentifiers = new List<string>();
                                while (reader.Read())
                                {
                                    string identifier = reader["UniqueCDSRowIdentifier"].ToString();
                                    uniqueRowIdentifiers.Add(identifier);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return uniqueRowIdentifiers;
        }

        /// <summary>
        /// Gets a PTL comment when unique row identifiers is given
        /// </summary>
        /// <param name="uniqueRowIdentifier"></param>
        /// <returns></returns>        
        public IList<PtlComment> GetPtlComments(string uniqueRowIdentifier, string pathwayId, string spec, DateTime referralDate)
        {
            IList<PtlComment> ptlComments = null;

            try
            {
                using (SqlConnection connection = GetConnection(false))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SelectCommentByUniqueRowId";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter rowIdentifierParam = GetParameter("@UniqueCDSRowIdentifier", SqlDbType.NVarChar, uniqueRowIdentifier);
                        command.Parameters.Add(rowIdentifierParam);

                        SqlParameter pathwayIdParam = GetParameter("@PatientPathwayIdentifier", SqlDbType.NVarChar, pathwayId);
                        command.Parameters.Add(pathwayIdParam);

                        SqlParameter specParam = GetParameter("@Spec", SqlDbType.Float, spec);
                        command.Parameters.Add(specParam);

                        SqlParameter referralDateParam = GetParameter("@ReferralRequestReceivedDate", SqlDbType.DateTime, referralDate);
                        command.Parameters.Add(referralDateParam);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                ptlComments = new List<PtlComment>();
                                PtlComment ptlComment;

                                while (reader.Read())
                                {
                                    ptlComment = new PtlComment();

                                    ptlComment.UniqueCdsRowIdentifier = uniqueRowIdentifier;
                                    ptlComment.Comment = !Convert.IsDBNull(reader["Comment"]) ? reader["Comment"].ToString() : string.Empty;
                                    ptlComment.Status = !Convert.IsDBNull(reader["Status"]) ? reader["Status"].ToString() : string.Empty;
                                    ptlComment.CreatedBy = !Convert.IsDBNull(reader["CreatedBy"]) ? reader["CreatedBy"].ToString() : string.Empty;

                                    DateTime tempDateTime = DateTime.MinValue;

                                    //DateTime.TryParse(reader["AppointmentDate"].ToString(), out tempDateTime);
                                    //ptlComment.AppointmentDate = tempDateTime;

                                    DateTime.TryParse(reader["UpdatedDate"].ToString(), out tempDateTime);
                                    ptlComment.UpdatedDate = tempDateTime;

                                    ptlComments.Add(ptlComment);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ptlComments;
        }

        /// <summary>
        /// Method to update a PTL comment
        /// </summary>
        /// <param name="ptlComment"></param>
        /// <returns></returns>
        /// TODO: will not be used anymore #henagahapan
        public bool UpdatePtlComment(PtlComment ptlComment)
        {

            if (null == ptlComment)
            {
                throw new ArgumentNullException("ptlComment", "PTL Comment cannot be null");
            }

            bool isUpdated = false;

            try
            {
                using (SqlConnection connection = GetConnection(false))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UpdatePtlComments";

                        SqlParameter rowIdentifier = GetParameter("@UniqueCDSRowIdentifier", SqlDbType.BigInt, ptlComment.UniqueCdsRowIdentifier);
                        SqlParameter status = GetParameter("@Status", SqlDbType.VarChar, ptlComment.Status);
                        SqlParameter appointmentDate = GetParameter("@AppointmentDate", SqlDbType.Date, ptlComment.AppointmentDate);
                        SqlParameter updatedDate = GetParameter("@UpdatedDate", SqlDbType.DateTime, ptlComment.UpdatedDate);
                        SqlParameter comment = GetParameter("@Comment", SqlDbType.VarChar, ptlComment.Comment);

                        command.Parameters.Add(rowIdentifier);
                        command.Parameters.Add(status);
                        command.Parameters.Add(appointmentDate);
                        command.Parameters.Add(updatedDate);
                        command.Parameters.Add(comment);

                        if (command.ExecuteNonQuery() > -1)
                        {
                            isUpdated = true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }

        private OpReferral FillOpReferral(SqlDataReader reader)
        {
            OpReferral opReferral = new OpReferral();

            if (null != reader)
            {
                opReferral.UniqueCdsRowIdentifier = reader["UniqueCDSRowIdentifier"].ToString();
                opReferral.LocalPatientID = reader["LocalPatientID"].ToString();
                opReferral.NhsNumber = reader["NHSNumber"].ToString();
                opReferral.PatientForename = reader["PatientForename"].ToString();
                opReferral.PatientSurname = reader["PatientSurname"].ToString();
                opReferral.SpecName = reader["SpecName"].ToString();
                opReferral.NewDivision = reader["NewDivision"].ToString();
                opReferral.Consultant = reader["Consultant"].ToString();
                opReferral.SourceOfReferralText = reader["SourceOfReferralText"].ToString();
                opReferral.PriorityType = reader["PriorityType"].ToString();
                opReferral.AttStatus = reader["AttStatus"].ToString();
                opReferral.RttText = reader["RTTText"].ToString();
                opReferral.WaitingListStatus = reader["WaitingListStatus"].ToString();
                opReferral.PatientPathwayIdentifier = reader["PatientPathwayIdentifier"].ToString();
                opReferral.Spec = reader["Spec"].ToString();
                opReferral.RttStatus = reader["RTTStatus"].ToString();
                opReferral.WeekswaitGrouped = reader["WeekswaitGrouped"].ToString();

                int tempInt = 0;
                int.TryParse(reader["WaitAtFutureClinicDate"].ToString(), out tempInt);
                opReferral.WaitAtFutureClinicDate = tempInt;

                DateTime tempDateTime = DateTime.MinValue;

                DateTime.TryParse(reader["DateOfBirth"].ToString(), out tempDateTime);
                //DateTime.TryParse(reader["DateOfBirth"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                opReferral.DateOfBirth = tempDateTime;

                DateTime.TryParse(reader["ReferralRequestReceivedDate"].ToString(), out tempDateTime);
                //DateTime.TryParse(reader["ReferralRequestReceivedDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                opReferral.ReferralRequestReceivedDate = tempDateTime;

                DateTime.TryParse(reader["RTTBreachDate"].ToString(), out tempDateTime);
                //DateTime.TryParse(reader["RTTBreachDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                opReferral.RttBreachDate = tempDateTime;

                DateTime.TryParse(reader["RTTClockStart"].ToString(), out tempDateTime);
                //DateTime.TryParse(reader["RTTClockStart"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                opReferral.RttClockStart = tempDateTime;

                // TODO: we might want to change below code when we get the acTual schema.
                // SQL server exported below fields as NVARCHAR, not DATETIME                                                                       

                //DateTime.TryParse(reader["AttendanceDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                DateTime.TryParse(reader["AttendanceDate"].ToString(), out tempDateTime);
                opReferral.AttendanceDate = tempDateTime;

                //DateTime.TryParse(reader["FutureClinicDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                DateTime.TryParse(reader["FutureClinicDate"].ToString(), out tempDateTime);
                opReferral.FutureClinicDate = tempDateTime;
            }


            return opReferral;
        }
    }
}