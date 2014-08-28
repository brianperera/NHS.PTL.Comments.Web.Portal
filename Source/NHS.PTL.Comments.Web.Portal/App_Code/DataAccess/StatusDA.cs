using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for WardDA
/// </summary>
namespace Nhs.Ptl.Comments.DataAccess
{
    public class StatusDA : DataAccessBase, IStatusDA
    {
        public StatusDA()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<string> GetAllStatuses()
        {
            List<string> statuses = new List<string>();

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();

                SqlCommand command = new SqlCommand("GetStatuses", con);
                command.CommandType = CommandType.StoredProcedure;

                var results = command.ExecuteReader();

                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string currentAction = string.Empty;

                        currentAction = results["Name"].ToString();
                        statuses.Add(currentAction);
                    }
                }
            }

            return statuses;
        }

        public bool AddStatus(string record)
        {
            bool isAddedOrUpdatedSuccessfully = false;

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();

                SqlCommand command = new SqlCommand("InsertStatus", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter action = GetParameter("@Name", SqlDbType.VarChar, record);
                command.Parameters.Add(action);

                var results = command.ExecuteNonQuery();

                if (results > -1)
                    isAddedOrUpdatedSuccessfully = true;
            }

            return isAddedOrUpdatedSuccessfully;
        }

        public bool UpdateStatus(string oldName, string newName)
        {
            bool isAddedOrUpdatedSuccessfully = false;

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();

                SqlCommand command = new SqlCommand("UpdateStatus", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter actionName1 = GetParameter("@OldName", SqlDbType.VarChar, oldName);
                SqlParameter actionName2 = GetParameter("@Name", SqlDbType.VarChar, newName);
                command.Parameters.Add(actionName1);
                command.Parameters.Add(actionName2);

                var results = command.ExecuteNonQuery();

                if (results > -1)
                    isAddedOrUpdatedSuccessfully = true;
            }

            return isAddedOrUpdatedSuccessfully;
        }

        public bool DeleteStatus(string name)
        {
            bool isAddedOrUpdatedSuccessfully = false;

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();

                SqlCommand command = new SqlCommand("DeleteStatus", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlParameter = GetParameter("@Name", SqlDbType.VarChar, name);
                command.Parameters.Add(sqlParameter);

                var results = command.ExecuteNonQuery();

                if (results > -1)
                    isAddedOrUpdatedSuccessfully = true;
            }

            return isAddedOrUpdatedSuccessfully;
        }

        public bool AddOrUpdateStatusAction(string record, string spName)
        { 
            bool isAddedOrUpdatedSuccessfully = false;

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();

                SqlCommand command = new SqlCommand(spName, con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter action = GetParameter("@Name", SqlDbType.VarChar, record);
                command.Parameters.Add(action);

                var results = command.ExecuteNonQuery();

                if (results > -1)
                    isAddedOrUpdatedSuccessfully = true;
            }

            return isAddedOrUpdatedSuccessfully;
        }
    }
}