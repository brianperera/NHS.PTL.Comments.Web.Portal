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

        public IList<Contracts.Dto.PtlComment> GetAllPtlComments()
        {
            IList<PtlComment> ptlComments = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SelectAllPtlComments";

                        SqlDataReader reader = command.ExecuteReader();

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

                                ptlComments.Add(comment);

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

        public bool AddPtlComment(Contracts.Dto.PtlComment ptlComment)
        {
            throw new NotImplementedException();
        }
    }
}