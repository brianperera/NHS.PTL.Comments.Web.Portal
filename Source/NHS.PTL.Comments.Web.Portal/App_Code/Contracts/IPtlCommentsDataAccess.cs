using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nhs.Ptl.Comments.Contracts.Dto;

/// <summary>
/// Summary description for IPtlCommentsDataAccess
/// </summary>
/// 
namespace Nhs.Ptl.Comments.DataAccess
{
    public interface IPtlCommentsDA
    {
        IList<PtlComment> GetAllPtlComments();
        bool AddPtlComment(PtlComment ptlComment);
        bool UpdatePtlComment(PtlComment ptlComment);
        PtlComment GetPtlComment(double uniqueRowIdentifier);
    }
}