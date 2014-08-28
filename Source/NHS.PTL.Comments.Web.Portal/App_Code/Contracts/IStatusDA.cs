using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IWardDA
/// </summary>
namespace Nhs.Ptl.Comments
{
    public interface IStatusDA
    {
        List<string> GetAllStatuses();
        bool AddStatus(string name);
        bool UpdateStatus(string name, string name2);
        bool DeleteStatus(string name);
    }
}