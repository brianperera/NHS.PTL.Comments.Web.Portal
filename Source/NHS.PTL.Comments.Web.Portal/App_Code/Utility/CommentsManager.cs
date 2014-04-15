using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nhs.Ptl.Comments.DataAccess;

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
    }
}