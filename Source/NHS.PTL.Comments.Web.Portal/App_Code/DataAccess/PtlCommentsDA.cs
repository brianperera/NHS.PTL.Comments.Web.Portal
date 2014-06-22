using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Nhs.Ptl.Comments.Contracts.Dto;
using System.Data;
using System.Globalization;

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
        public IList<OpReferral> GetAllOpReferrals()
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
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SelectAllPtlReferrals";

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                opReferrals = new List<OpReferral>();
                                OpReferral opRefferal;

                                while (reader.Read())
                                {
                                    opRefferal = new OpReferral();

                                    opRefferal.UniqueCdsRowIdentifier = reader["UniqueCDSRowIdentifier"].ToString();
                                    opRefferal.LocalPatientID = reader["LocalPatientID"].ToString();
                                    opRefferal.NhsNumber = reader["NHSNumber"].ToString();
                                    opRefferal.PatientForename = reader["PatientForename"].ToString();
                                    opRefferal.PatientSurname = reader["PatientSurname"].ToString();
                                    opRefferal.SpecName = reader["SpecName"].ToString();
                                    opRefferal.NewDivision = reader["NewDivision"].ToString();
                                    opRefferal.Consultant = reader["Consultant"].ToString();
                                    opRefferal.SourceOfReferralText = reader["SourceOfReferralText"].ToString();
                                    opRefferal.PriorityType = reader["PriorityType"].ToString();
                                    opRefferal.AttStatus = reader["AttStatus"].ToString();
                                    opRefferal.RttText = reader["RTTText"].ToString();
                                    opRefferal.WaitingListStatus = reader["WaitingListStatus"].ToString();
                                    opRefferal.WaitAtFutureClinicDate = reader["WaitAtFutureClinicDate"].ToString();

                                    double tempDouble = 0;
                                    double.TryParse(reader["PatientPathwayIdentifier"].ToString(), out tempDouble);
                                    opRefferal.PatientPathwayIdentifier = tempDouble;

                                    double.TryParse(reader["Spec"].ToString(), out tempDouble);
                                    opRefferal.Spec = tempDouble;

                                    double.TryParse(reader["RTTStatus"].ToString(), out tempDouble);
                                    opRefferal.RttStatus = tempDouble;

                                    DateTime tempDateTime = DateTime.MinValue;

                                    DateTime.TryParse(reader["DateOfBirth"].ToString(), out tempDateTime);
                                    //DateTime.TryParse(reader["DateOfBirth"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                                    opRefferal.DateOfBirth = tempDateTime;

                                    DateTime.TryParse(reader["ReferralRequestReceivedDate"].ToString(), out tempDateTime);
                                    //DateTime.TryParse(reader["ReferralRequestReceivedDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                                    opRefferal.ReferralRequestReceivedDate = tempDateTime;

                                    DateTime.TryParse(reader["RTTBreachDate"].ToString(), out tempDateTime);
                                    //DateTime.TryParse(reader["RTTBreachDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                                    opRefferal.RttBreachDate = tempDateTime;

                                    DateTime.TryParse(reader["RTTClockStart"].ToString(), out tempDateTime);
                                    //DateTime.TryParse(reader["RTTClockStart"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                                    opRefferal.RttClockStart = tempDateTime;

                                    // TODO: we might want to change below code when we get the acTual schema.
                                    // SQL server exported below fields as NVARCHAR, not DATETIME                                                                       

                                    DateTime.TryParse(reader["AttendanceDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                                    //DateTime.TryParse(reader["AttendanceDate"].ToString(), out tempDateTime);
                                    opRefferal.AttendanceDate = tempDateTime;

                                    DateTime.TryParse(reader["FutureClinicDate"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDateTime);
                                    //DateTime.TryParse(reader["FutureClinicDate"].ToString(), out tempDateTime);
                                    opRefferal.FutureClinicDate = tempDateTime;

                                    opReferrals.Add(opRefferal);

                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
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
                        SqlParameter patientPathwayId = GetParameter("@PatientPathwayIdentifier", SqlDbType.Float, ptlComment.PatientPathwayIdentifier);
                        SqlParameter spec = GetParameter("@Spec", SqlDbType.Float, ptlComment.Spec);
                        SqlParameter referralDate = GetParameter("@ReferralRequestReceivedDate", SqlDbType.DateTime, ptlComment.ReferralRequestReceivedDate);
                        SqlParameter status = GetParameter("@Status", SqlDbType.VarChar, ptlComment.Status);
                        SqlParameter appointmentDate = GetParameter("@AppointmentDate", SqlDbType.Date, ptlComment.AppointmentDate);
                        SqlParameter updatedDate = GetParameter("@UpdatedDate", SqlDbType.Date, ptlComment.UpdatedDate);
                        SqlParameter comment = GetParameter("@Comment", SqlDbType.VarChar, ptlComment.Comment);

                        command.Parameters.Add(rowIdentifier);
                        command.Parameters.Add(patientPathwayId);
                        command.Parameters.Add(spec);
                        command.Parameters.Add(referralDate);
                        command.Parameters.Add(status);
                        command.Parameters.Add(appointmentDate);
                        command.Parameters.Add(updatedDate);
                        command.Parameters.Add(comment);

                        if (command.ExecuteNonQuery() > -1)
                        {
                            isAdded = true;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            { }
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

                                    double tempDouble;
                                    double.TryParse(reader["PatientPathwayIdentifier"].ToString(), out tempDouble);
                                    comment.PatientPathwayIdentifier = tempDouble;

                                    double.TryParse(reader["Spec"].ToString(), out tempDouble);
                                    comment.Spec = tempDouble;

                                    DateTime tempDateTime = DateTime.MinValue;

                                    DateTime.TryParse(reader["AppointmentDate"].ToString(), out tempDateTime);
                                    comment.AppointmentDate = tempDateTime;

                                    DateTime.TryParse(reader["UpdatedDate"].ToString(), out tempDateTime);
                                    comment.UpdatedDate = tempDateTime;

                                    DateTime.TryParse(reader["ReferralRequestReceivedDate"].ToString(), out tempDateTime);
                                    comment.ReferralRequestReceivedDate = tempDateTime;

                                    ptlComments.Add(comment);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
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
            }
            catch (Exception ex)
            {
            }

            return uniqueRowIdentifiers;
        }

        /// <summary>
        /// Gets a PTL comment when unique row identifiers is given
        /// </summary>
        /// <param name="uniqueRowIdentifier"></param>
        /// <returns></returns>        
        public IList<PtlComment> GetPtlComments(string uniqueRowIdentifier, double pathwayId, double spec, DateTime referralDate)
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

                        SqlParameter pathwayIdParam = GetParameter("@PatientPathwayIdentifier", SqlDbType.Float, pathwayId);
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

                                    DateTime tempDateTime = DateTime.MinValue;

                                    DateTime.TryParse(reader["AppointmentDate"].ToString(), out tempDateTime);
                                    ptlComment.AppointmentDate = tempDateTime;

                                    DateTime.TryParse(reader["UpdatedDate"].ToString(), out tempDateTime);
                                    ptlComment.UpdatedDate = tempDateTime;

                                    ptlComments.Add(ptlComment);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
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
                        SqlParameter updatedDate = GetParameter("@UpdatedDate", SqlDbType.Date, ptlComment.UpdatedDate);
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
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }
    }
}