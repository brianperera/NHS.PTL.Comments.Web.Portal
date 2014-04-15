using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Configuration;

/// <summary>
/// Summary description for StatusConfigurationManager
/// </summary>
/// 
namespace Nhs.Ptl.Comments.Utility
{
    public static class StatusConfigurationManager
    {
        public static IList<string> GetAllStatuses()
        {
            NameValueCollection statuses = new NameValueCollection();

            if (null != ConfigurationManager.GetSection("statusConfiguration"))
            {
                statuses = (NameValueCollection)ConfigurationManager.GetSection("statusConfiguration");
            }

            IList<string> statusList = null;

            if (null != statuses)
            {
                statusList = new List<string>();

                foreach (string key in statuses)
                {
                    statusList.Add(statuses[key]);
                }
            }

            return statusList;
        }
    }
}