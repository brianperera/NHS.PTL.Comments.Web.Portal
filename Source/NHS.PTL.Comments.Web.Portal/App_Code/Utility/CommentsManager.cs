using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nhs.Ptl.Comments.DataAccess;
using Nhs.Ptl.Comments.Contracts.Dto;

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
            return GetStatusForReferral(ptlCommentsData.GetAllOpReferrals(), GetAllPtlComments());
        }

        public static IList<PtlComment> GetAllPtlComments()
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            return ptlCommentsData.GetAllPtlComments();
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
                        foreach (PtlComment comment in commentsForReferral)
                        {
                            // Add one row for each comment
                            OpReferral refWithStatus = new OpReferral();
                            refWithStatus = refWithStatus.DeepClone(referral);
                            refWithStatus.Status = comment.Status;
                            refferalsWithStatus.Add(refWithStatus);
                        }
                    }
                    else
                    {
                        // If no comments found, just add the original OpReferral
                        refferalsWithStatus.Add(referral);
                    }
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
    }
}