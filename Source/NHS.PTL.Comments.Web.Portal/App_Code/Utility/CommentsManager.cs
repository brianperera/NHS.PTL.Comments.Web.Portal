using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nhs.Ptl.Comments.DataAccess;
using Nhs.Ptl.Comments.Contracts.Dto;
using System.Data;

/// <summary>
/// Summary description for CommentsManager
/// </summary>
/// 

namespace Nhs.Ptl.Comments.Utility
{
    public static class CommentsManager
    {
        public static IList<string> GetAllUniqueRowIdentifiers()
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            return ptlCommentsData.GetAllUniqueRowIdentifiers();
        }

        public static bool UniqueRowIdentifierExists(string uniqueRowIdentifier)
        {
            bool recordExists = false;

            return recordExists;
        }

        public static IList<OpReferral> GetAllOpReferrals()
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            //DataSet ds = ptlCommentsData.GetDistinctFilterValues();
            IList<OpReferral> oRef = ptlCommentsData.GetAllOpReferrals(CommandType.Text);
            IList<string> uIds = oRef.Select(x => x.UniqueCdsRowIdentifier).ToList();
            return GetStatusForReferral(oRef, GetPtlComments(uIds));

            //return ptlCommentsData.GetAllOpReferrals(CommandType.Text);
        }

        public static IList<OpReferral> GetAllOpReferrals2(int page, int pageSize, out int rowCount)
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            //DataSet ds = ptlCommentsData.GetDistinctFilterValues();
            IList<OpReferral> oRef = ptlCommentsData.GetAllOpReferrals2(page, pageSize, out rowCount);
            //IList<string> uIds = oRef.Select(x => x.UniqueCdsRowIdentifier).ToList();
            //return GetStatusForReferral(oRef, GetPtlComments(uIds));
            return oRef;

            //return ptlCommentsData.GetAllOpReferrals(CommandType.Text);
        }

        public static IList<OpReferral> GetOpReferralsByParams(string speciality, IList<string> rttWait, string futureApptStatus)
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            IList<OpReferral> oRef = ptlCommentsData.GetOpReferralByParams(speciality, rttWait, futureApptStatus);
            IList<string> uIds = oRef.Select(x => x.UniqueCdsRowIdentifier).ToList();
            return GetStatusForReferral(oRef, GetPtlComments(uIds));
        }

        public static IList<OpReferral> GetOpReferralsByParams(string searchText,
                                                        string specialty,
                                                        string consultant,
                                                        string rttWait,
                                                        string attStatus,
                                                        string futureApptStatus)
        {
            PtlCommentsDA ptlCommentsDate = new PtlCommentsDA();
            IList<OpReferral> oRef = ptlCommentsDate.GetOpReferralByParams(searchText,
                                                                                specialty,
                                                                                consultant,
                                                                                rttWait,
                                                                                attStatus,
                                                                                futureApptStatus);
            IList<string> uIds = oRef.Select(x => x.UniqueCdsRowIdentifier).ToList();
            return GetStatusForReferral(oRef, GetPtlComments(uIds));
        }

        public static IList<OpReferral> GetOpReferralByFieldName(string fieldName, string consultant)
        {
            PtlCommentsDA ptlCommentsDate = new PtlCommentsDA();
            return GetStatusForReferral(ptlCommentsDate.GetOpReferralByFieldName(fieldName, consultant), GetAllPtlComments());
        }

        public static IList<PtlComment> GetAllPtlComments()
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            return ptlCommentsData.GetAllPtlComments();
        }

        public static IList<PtlComment> GetPtlComments(IList<string> uniqueIds)
        {
            DataTable uniqueIdsTable = new DataTable();
            uniqueIdsTable.Columns.Add("UniqueCDSRowIdentifier", typeof(string));

            foreach (string id in uniqueIds)
            {
                DataRow row = uniqueIdsTable.NewRow();
                row["UniqueCDSRowIdentifier"] = id;
                uniqueIdsTable.Rows.Add(row);
            }

            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            return ptlCommentsData.GetPtlCommentsByUniqueIds(uniqueIdsTable);
        }

        private static IList<OpReferral> GetStatusForReferral(IList<OpReferral> opReferrals, IList<PtlComment> ptlComments)
        {
            IList<OpReferral> refferalsWithStatus = new List<OpReferral>();

            if (null != opReferrals)
            {
                foreach (OpReferral referral in opReferrals)
                {
                    IList<PtlComment> commentsForReferral = null;

                    if (null != ptlComments)
                    {
                        commentsForReferral = ptlComments.Where(x => x.UniqueCdsRowIdentifier == referral.UniqueCdsRowIdentifier
                                                                                && x.PatientPathwayIdentifier == referral.PatientPathwayIdentifier
                                                                                && x.Spec == referral.Spec
                                                                                && x.ReferralRequestReceivedDate == referral.ReferralRequestReceivedDate).ToList();
                    }

                    if (null != commentsForReferral && commentsForReferral.Count > 0)
                    {
                        PtlComment comment = commentsForReferral.OrderByDescending(x => x.UpdatedDate).FirstOrDefault();


                        //PtlComment comment = from n in commentsForReferral
                        //                     group n by n.UpdatedDate into g
                        //                     select new { AccountId = g.Key, Date = g.Max(t => t.Date) };

                        // Add one row for each comment
                        OpReferral refWithStatus = new OpReferral();
                        refWithStatus = refWithStatus.DeepClone(referral);
                        refWithStatus.Status = comment.Status;
                        refferalsWithStatus.Add(refWithStatus);

                        //foreach (PtlComment comment in commentsForReferral)
                        //{
                        //    // Add one row for each comment
                        //    OpReferral refWithStatus = new OpReferral();
                        //    refWithStatus = refWithStatus.DeepClone(referral);
                        //    refWithStatus.Status = comment.Status;
                        //    refferalsWithStatus.Add(refWithStatus);
                        //}
                    }
                    else
                    {
                        // If no comments found, just add the original OpReferral
                        referral.Status = string.Empty;
                        refferalsWithStatus.Add(referral);
                    }

                    // If no comments found, just add the original OpReferral
                    //refferalsWithStatus.Add(referral);

                }
            }

            return refferalsWithStatus;
        }

        public static IList<PtlComment> GetPtlComments(string uniqueRowIdentifier, string pathwayId, string spec, DateTime referralDate)
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            return ptlCommentsData.GetPtlComments(uniqueRowIdentifier, pathwayId, spec, referralDate);
        }

        public static bool AddUpdatePtlComment(PtlComment ptlComment)
        {
            bool isAdded = false;

            if (null != ptlComment)
            {
                PtlCommentsDA ptlCommentsData = new PtlCommentsDA();

                isAdded = ptlCommentsData.AddPtlComment(ptlComment);
            }

            return isAdded;
        }

        public static DataSet GetDropdownValuesFromReferral()
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            return ptlCommentsData.GetDistinctReferralFilterValues();
        }
    }
}