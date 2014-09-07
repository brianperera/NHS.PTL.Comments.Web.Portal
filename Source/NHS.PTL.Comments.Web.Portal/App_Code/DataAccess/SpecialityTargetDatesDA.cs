using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Nhs.Ptl.Comments.Contracts.Dto;

/// <summary>
/// Summary description for WardDA
/// </summary>
namespace Nhs.Ptl.Comments.DataAccess
{
    public class SpecialityTargetDatesDA : DataAccessBase, ISpecialityTargetDatesDA
    {
        public SpecialityTargetDatesDA()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<SpecialityTargetDate> GetAllActiveSpecialityTargetDates()
        {
            return GetSpecialityTargetDates(true);
        }

        public List<SpecialityTargetDate> GetAllSpecialityTargetDates()
        {
            return GetSpecialityTargetDates(false);
        }

        public List<SpecialityTargetDate> GetSpecialityTargetDates(bool isActive)
        {
            List<SpecialityTargetDate> specialityTargetDates = new List<SpecialityTargetDate>();

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();
                SqlCommand command;
                
                if(isActive)
                    command = new SqlCommand("GetAllActiveSpecialityTargetDates", con);
                else
                    command = new SqlCommand("GetSpecialityTargetDates", con);
                
                command.CommandType = CommandType.StoredProcedure;

                var results = command.ExecuteReader();

                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        int tempInt = 0;
                        SpecialityTargetDate currentspecialityTargetDate = new SpecialityTargetDate();

                        //ID
                        int.TryParse(results["ID"].ToString(), out tempInt);
                        currentspecialityTargetDate.ID = tempInt;

                        //Spec
                        currentspecialityTargetDate.Speciality = results["Spec"].ToString();
                        //currentspecialityTargetDate.Resp = results["Resp"].ToString();

                        //TargetDay
                        int.TryParse(results["TargetDays"].ToString(), out tempInt);
                        currentspecialityTargetDate.TargetDay = tempInt;

                        bool tempBool = false;
                        //TargetDay
                        bool.TryParse(results["Active"].ToString(), out tempBool);
                        currentspecialityTargetDate.Activate = tempBool;

                        specialityTargetDates.Add(currentspecialityTargetDate);
                    }
                }
            }

            return specialityTargetDates;
        }

        public bool AddSpecialityTargetDate(SpecialityTargetDate specialityTargetDate)
        {
            bool isAddedSuccessfully = false;

            try
            {
                using (SqlConnection con = GetConnection(false))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand("InsertSpecialityTargetDate", con);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter specialityIDSqlParameter = GetParameter("@ID", SqlDbType.Int, specialityTargetDate.ID);
                    SqlParameter specialitySqlParameter = GetParameter("@Spec", SqlDbType.VarChar, specialityTargetDate.Speciality);
                    //SqlParameter respSqlParameter = GetParameter("@Resp", SqlDbType.VarChar, specialityTargetDate.Resp);
                    SqlParameter targetDaySqlParameter = GetParameter("@TargetDays", SqlDbType.VarChar, specialityTargetDate.TargetDay);
                    SqlParameter activeSqlParameter = GetParameter("@Active", SqlDbType.VarChar, specialityTargetDate.TargetDay);
                    command.Parameters.Add(specialityIDSqlParameter);
                    command.Parameters.Add(specialitySqlParameter);
                    //command.Parameters.Add(respSqlParameter);
                    command.Parameters.Add(targetDaySqlParameter);
                    command.Parameters.Add(activeSqlParameter);

                    var results = command.ExecuteNonQuery();

                    if (results > -1)
                        isAddedSuccessfully = true;
                }

                return isAddedSuccessfully;
            }
            catch (Exception)
            {
                return isAddedSuccessfully;
            }
        }

        public bool UpdateSpecialityTargetDate(SpecialityTargetDate specialityTargetDate)
        {
            bool isUpdatedSuccessfully = false;

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();

                SqlCommand command = new SqlCommand("UpdateSpecialityTargetDate", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter idSqlParameter = GetParameter("@ID", SqlDbType.VarChar, specialityTargetDate.ID);
                SqlParameter specialitySqlParameter = GetParameter("@Spec", SqlDbType.VarChar, specialityTargetDate.Speciality);
                //SqlParameter respSqlParameter = GetParameter("@Resp", SqlDbType.VarChar, specialityTargetDate.Resp);
                SqlParameter targetDaySqlParameter = GetParameter("@TargetDays", SqlDbType.VarChar, specialityTargetDate.TargetDay);
                SqlParameter activeSqlParameter = GetParameter("@Active", SqlDbType.Bit, specialityTargetDate.Activate);
                command.Parameters.Add(idSqlParameter);
                command.Parameters.Add(specialitySqlParameter);
                //command.Parameters.Add(respSqlParameter);
                command.Parameters.Add(targetDaySqlParameter);
                command.Parameters.Add(activeSqlParameter);

                var results = command.ExecuteNonQuery();

                if (results > -1)
                    isUpdatedSuccessfully = true;
            }

            return isUpdatedSuccessfully;
        }

        public bool ActivateInactivateSpecialityTargetDate(int id, int status)
        {
            bool isStateChangeSuccessfully = false;

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();

                SqlCommand command = new SqlCommand("ActiveInactiveSpecialityTargetDate", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idSqlParameter = GetParameter("@ID", SqlDbType.VarChar, id);
                SqlParameter statusSqlParameter = GetParameter("@Active", SqlDbType.Bit, status);
                command.Parameters.Add(idSqlParameter);
                command.Parameters.Add(statusSqlParameter);

                var results = command.ExecuteNonQuery();

                if (results > -1)
                    isStateChangeSuccessfully = true;
            }

            return isStateChangeSuccessfully;
        }


        public SpecialityTargetDate GetSpecialityTargetDateRecord(int ID)
        {
            SpecialityTargetDate specialityTargetDateRecord = new SpecialityTargetDate();

            using (SqlConnection con = GetConnection(false))
            {
                con.Open();
                SqlCommand command;

                command = new SqlCommand("GetSpecialityTargetDateRecord", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter idSqlParameter = GetParameter("@ID", SqlDbType.VarChar, ID);
                command.Parameters.Add(idSqlParameter);

                var results = command.ExecuteReader();

                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        int tempInt = 0;

                        //ID
                        int.TryParse(results["ID"].ToString(), out tempInt);
                        specialityTargetDateRecord.ID = tempInt;

                        //Spec
                        specialityTargetDateRecord.Speciality = results["Spec"].ToString();
                        //specialityTargetDateRecord.Resp = results["Resp"].ToString();

                        //TargetDay
                        int.TryParse(results["TargetDays"].ToString(), out tempInt);
                        specialityTargetDateRecord.TargetDay = tempInt;

                        bool tempBool = false;
                        //TargetDay
                        bool.TryParse(results["Active"].ToString(), out tempBool);
                        specialityTargetDateRecord.Activate = tempBool;

                        return specialityTargetDateRecord;
                    }
                }
            }

            return specialityTargetDateRecord; 
        }
    }
}