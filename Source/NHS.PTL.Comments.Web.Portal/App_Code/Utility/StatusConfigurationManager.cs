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
        /// <summary>
        /// Gets all Statuses from web.config
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetAllStatuses()
        {
            NameValueCollection statuses = new NameValueCollection();

            // Read the 'statusConfiguration' section in web.config and retrieve values
            if (null != ConfigurationManager.GetSection("statusConfiguration"))
            {
                statuses = (NameValueCollection)ConfigurationManager.GetSection("statusConfiguration");
            }

            IList<string> statusList = null;

            if (null != statuses)
            {
                statusList = new List<string>();

                // Add all Status values to a List since,
                // NameValueCollection isn't bindable to a dropdown
                foreach (string key in statuses)
                {
                    statusList.Add(statuses[key]);
                }
            }

            return statusList;
        }
    }
}