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

        public static IList<PtlComment> GetAllPtlComments()
        {
            PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
            return ptlCommentsData.GetAllPtlComments();
        }

        public static PtlComment GetPtlComment(string uniqueRowIdentifier)
        {
            PtlComment ptlComment = null;
            
            double uniqueRowIdentifierValue;
            if (double.TryParse(uniqueRowIdentifier, out uniqueRowIdentifierValue))
            {
                PtlCommentsDA ptlCommentsData = new PtlCommentsDA();
                ptlComment =  ptlCommentsData.GetPtlComment(uniqueRowIdentifierValue);
            }

            return ptlComment;
        }

        public static bool AddUpdatePtlComment(PtlComment ptlComment, bool isUpdate)
        {
            bool isAddedUpdated = false;

            if (null != ptlComment)
            {
                PtlCommentsDA ptlCommentsData = new PtlCommentsDA();

                isAddedUpdated = (isUpdate) ? ptlCommentsData.UpdatePtlComment(ptlComment) : ptlCommentsData.AddPtlComment(ptlComment);
            }

            return isAddedUpdated;
        }
    }
}