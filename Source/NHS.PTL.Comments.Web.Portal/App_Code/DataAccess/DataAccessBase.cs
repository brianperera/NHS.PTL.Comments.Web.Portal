using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DataAccessBase
/// </summary>
namespace Nhs.Ptl.Comments.DataAccess
{
    public class DataAccessBase
    {
        string PortalConnectionString;
        string ReferralConnectionString;

        public DataAccessBase()
        {
            PortalConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            ReferralConnectionString = ConfigurationManager.ConnectionStrings["PTLReferralConnection"].ConnectionString;
        }

        internal SqlConnection GetConnection(bool isReferralRequest)
        {
            if (isReferralRequest)
            {
                return new SqlConnection(ReferralConnectionString);
            }

            return new SqlConnection(PortalConnectionString);
        }

        internal SqlParameter GetParameter(string parameterName, SqlDbType sqlDbType,object value)
        {
            SqlParameter parameter = new SqlParameter();

            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = parameterName;
            parameter.SqlDbType = sqlDbType;
            parameter.Value = value;

            return parameter;
        }        
    }
}