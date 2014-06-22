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

            if (opReferrals == null || ptlComments == null)
            {
                throw new ArgumentNullException("opRefferals or ptlComments cannot be null");
            }

            foreach (PtlComment comment in ptlComments)
            {
                OpReferral referral = opReferrals.SingleOrDefault(x => x.UniqueCdsRowIdentifier == comment.UniqueCdsRowIdentifier
                                                                        && x.PatientPathwayIdentifier == comment.PatientPathwayIdentifier
                                                                        && x.Spec == comment.Spec
                                                                        && x.ReferralRequestReceivedDate == comment.ReferralRequestReceivedDate);

                if (null != referral)
                {
                    OpReferral refWithStatus = new OpReferral();
                    refWithStatus = refWithStatus.DeepClone(referral);
                    refWithStatus.Status = comment.Status;
                    refferalsWithStatus.Add(refWithStatus);
                }
            }

            return refferalsWithStatus;
        }

        public static IList<PtlComment> GetPtlComments(string uniqueRowIdentifier, double pathwayId, double spec, DateTime referralDate)
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