using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Nhs.Ptl.Comments.Contracts.Dto;
using System.Data;

/// <summary>
/// Summary description for PtlCommentsDA
/// </summary>
/// 
namespace Nhs.Ptl.Comments.DataAccess
{
    public class PtlCommentsDA : DataAccessBase, IPtlCommentsDA
    {

        public IList<PtlComment> GetAllPtlComments()
        {
            IList<PtlComment> ptlComments = null;

            try
            {
                using (SqlConnection connection = GetConnection())
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

                                    comment.PatientPathwayIdentifier = reader["PatientPathwayIdentifier"].ToString();
                                    comment.LocalPatientID = reader["LocalPatientID"].ToString();
                                    comment.NhsNumber = reader["NHSNumber"].ToString();
                                    comment.PatientForename = reader["PatientForename"].ToString();
                                    comment.PatientSurname = reader["PatientSurname"].ToString();
                                    comment.Spec = reader["Spec"].ToString();
                                    comment.SpecName = reader["SpecName"].ToString();
                                    comment.NewDivision = reader["NewDivision"].ToString();
                                    comment.Consultant = reader["Consultant"].ToString();
                                    comment.SourceOfReferralText = reader["SourceOfReferralText"].ToString();
                                    comment.PriorityType = reader["PriorityType"].ToString();
                                    comment.AttStatus = reader["AttStatus"].ToString();
                                    comment.RttStatus = reader["RTTStatus"].ToString();
                                    comment.RttText = reader["RTTText"].ToString();
                                    comment.WaitingListStatus = reader["WaitingListStatus"].ToString();
                                    comment.WaitAtFutureClinicDate = reader["WaitAtFutureClinicDate"].ToString();
                                    comment.Status = reader["Status"].ToString();
                                    comment.Comment = reader["Comment"].ToString();

                                    double tempDouble = 0;
                                    double.TryParse(reader["UniqueCDSRowIdentifier"].ToString(), out tempDouble);
                                    comment.UniqueCdsRowIdentifier = tempDouble;

                                    DateTime tempDateTime = DateTime.MinValue;

                                    DateTime.TryParse(reader["DateOfBirth"].ToString(), out tempDateTime);
                                    comment.DateOfBirth = tempDateTime;

                                    DateTime.TryParse(reader["ReferralRequestReceivedDate"].ToString(), out tempDateTime);
                                    comment.ReferralRequestReceivedDate = tempDateTime;

                                    DateTime.TryParse(reader["RTTClockStart"].ToString(), out tempDateTime);
                                    comment.RttClockStart = tempDateTime;

                                    DateTime.TryParse(reader["RTTBreachDate"].ToString(), out tempDateTime);
                                    comment.RttBreachDate = tempDateTime;

                                    DateTime.TryParse(reader["AttendanceDate"].ToString(), out tempDateTime);
                                    comment.AttendanceDate = tempDateTime;

                                    DateTime.TryParse(reader["FutureClinicDate"].ToString(), out tempDateTime);
                                    comment.FutureClinicDate = tempDateTime;

                                    DateTime.TryParse(reader["AppointmentDate"].ToString(), out tempDateTime);
                                    comment.AppointmentDate = tempDateTime;

                                    DateTime.TryParse(reader["UpdatedDate"].ToString(), out tempDateTime);
                                    comment.UpdatedDate = tempDateTime;

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

        public bool AddPtlComment(PtlComment ptlComment)
        {
            bool isAdded = false;

            if (null == ptlComment)
            {
                throw new ArgumentNullException("ptlComment", "PTL Comment cannot be null");
            }            

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "InsertPtlComment";

                        SqlParameter rowIdentifier = GetParameter("@UniqueCDSRowIdentifier", SqlDbType.BigInt, ptlComment.UniqueCdsRowIdentifier);
                        SqlParameter status = GetParameter("@Status", SqlDbType.VarChar, ptlComment.Status);
                        SqlParameter appointmentDate = GetParameter("@AppointmentDate", SqlDbType.Date, ptlComment.AppointmentDate);
                        SqlParameter updatedDate = GetParameter("@UpdatedDate", SqlDbType.Date, ptlComment.UpdatedDate);
                        SqlParameter comment = GetParameter("@Comment", SqlDbType.VarChar, ptlComment.Comment);

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

        public IList<string> GetAllUniqueRowIdentifiers()
        {
            IList<string> uniqueRowIdentifiers = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetAllUniqueRowIdentifiers";

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
    }
}