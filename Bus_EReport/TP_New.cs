using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class TP_New
    {
        private string strQry = string.Empty;

        // This function is to initiate TP for a month
        public DataSet getEmptyTourPlan(DateTime dtCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DateTime dtDiff = dtCurrentDate;
            DataSet dsListedDR = null;
            DataSet dsDiff = null;
            int iDiff = -1;


            strQry = "Select dateadd(day, 0 - day(dateadd(month, 1 , '" + dtCurrentDate.Month + "-" + dtCurrentDate.Day + "-" + dtCurrentDate.Year + "' )), " +
                                                  "dateadd(month, 1 , '" + dtCurrentDate.Month + "-" + dtCurrentDate.Day + "-" + dtCurrentDate.Year + "')) ";

            dsDiff = db_ER.Exec_DataSet(strQry);

            if (dsDiff.Tables[0].Rows.Count > 0)
            {
                dtDiff = Convert.ToDateTime(dsDiff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            }

            strQry = "Select datediff(day, '" + dtCurrentDate.Month + "-" + dtCurrentDate.Day + "-" + dtCurrentDate.Year + "' ," +
                                           "'" + dtDiff.Month + "-" + dtDiff.Day + "-" + dtDiff.Year + "'  ) ";

            dsDiff = db_ER.Exec_DataSet(strQry);
            if (dsDiff.Tables[0].Rows.Count > 0)
            {
                iDiff = Convert.ToInt32(dsDiff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                iDiff = iDiff + 1;
            }

            strQry = " SELECT TOP " + iDiff + " '' TP_Date,'' TP_Day " +
                     " FROM  sys.tables ";
            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

         //Added by Sridevi - 13-Nov-15
        public int RecordAdd_New(string TP_Date, string TP_Day, string TP_Terr, string TP_Terr1, string TP_Terr2, string worktype_Code, string worktype_Name, string TP_Objective, bool TP_Submit, string sf_code, string Territory_Code1, string Territory_Code2, string Territory_Code3, string ddlValueWT1, string ddlTextWT1, string ddlValueWT2, string ddlTextWT2, string SF_Name)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();

                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);


                if (TP_Submit == false)
                {
                    strQry = " select Tour_Month from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";


                    dsListedDR = db.Exec_DataSet(strQry);

                    if (dsListedDR.Tables[0].Rows.Count == 0)
                    {
                        strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                " '" + TP_Terr + "','" + TP_Terr1 + "','" + TP_Terr2 + "','" + TP_Objective + "', 'Tour Schedule', " +
                                " '" + worktype_Code + "','" + worktype_Name + "','" + Division_Code + "', 0, getdate(),'" + Territory_Code1 + "','" + Territory_Code2 + "','" + Territory_Code3 + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + SF_Name + "') ";

                        iReturn = db.ExecQry(strQry);

                    }
                    else
                    {
                        strQry = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule', " +
                                 " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                 " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "', Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "', " +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "' " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);


                    }

                }
                else if (TP_Submit == true)
                {

                    strQry = " select Tour_Month from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    dsListedDR = db.Exec_DataSet(strQry);

                    if (dsListedDR.Tables[0].Rows.Count == 0)
                    {
                        strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                 " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                 " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                 " '" + TP_Terr + "','" + TP_Terr1 + "','" + TP_Terr2 + "','" + TP_Objective + "', 'Tour Schedule', " +
                                 " '" + worktype_Code + "','" + worktype_Name + "','" + Division_Code + "', 1, getdate(),'" + Territory_Code1 + "'," +
                                 "'" + Territory_Code2 + "','" + Territory_Code3 + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + SF_Name + "') ";

                        iReturn = db.ExecQry(strQry);

                        //strQry = "update Mas_Salesforce set Last_TP_Date='" + strTPdt + "' where Sf_Code='" + sf_code + "'";

                        //iReturn = db.ExecQry(strQry);
                    }
                    else
                    {
                        strQry = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=1, " +
                                 " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                 " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "',Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "'," +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "' " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        // Added by Sridevi
        //public DataSet get_TP_Active_Date_New(string sf_code)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsListedDR = null;

        //    strQry = "select top 1'01-' + substring(convert(char(7),dateadd(mm,1,Tour_Date),120),6,2)+'-'+ " +
        //             "substring(convert(char(7),dateadd(mm,1,Tour_Date),120),0,5)+' 00:00:00.000' " +
        //             " from Trans_TP  where SF_Code = '" + sf_code + "' and confirmed=1 " +
        //             "order by Tour_Date desc";

        //    dsListedDR = db_ER.Exec_DataSet(strQry);



        //    if (dsListedDR.Tables[0].Rows.Count > 0)
        //    {
        //       // Do Nothing
        //    }

        //    else
        //    {
        //        strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

        //        try
        //        {
        //            dsListedDR = db_ER.Exec_DataSet(strQry);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    return dsListedDR;
        //}
        // Added by Sridevi

        public DataSet get_TP_Active_Date_New(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            DataSet dsListedMasSales = null;

            //strQry = "select top 1 convert(char(2),DAY(Last_TP_Date),103)+'-' + substring(convert(char(7),Last_TP_Date,120),6,2)+'-'+ " +
            //        "substring(convert(char(7),Last_TP_Date,120),0,5)+' 00:00:00.000' AS Last_TP_Date " +
            //        " from Mas_Salesforce  where SF_Code = '" + sf_code + "' ";


            strQry = "select top 1 convert(char(2),DAY(Tour_Date),103)+'-' + substring(convert(char(7),Tour_Date,120),6,2)+'-'+ " +
                   "substring(convert(char(7),Tour_Date,120),0,5)+' 00:00:00.000' AS Tour_Date " +
                   " from trans_tp_one  where SF_Code = '" + sf_code + "' and Confirmed=0 and (Change_Status=1 or Change_Status=2) ";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count == 0)
            {
                strQry = "select top 1 convert(char(2),DAY(Last_TP_Date),103)+'-' + substring(convert(char(7),Last_TP_Date,120),6,2)+'-'+ " +
                        "substring(convert(char(7),Last_TP_Date,120),0,5)+' 00:00:00.000' AS Last_TP_Date " +
                        " from Mas_Salesforce_DCRTPdate  where SF_Code = '" + sf_code + "' ";


                dsListedDR = db_ER.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count > 0)
                {
                    // Do Nothing
                }
                else
                {

                    strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

                    try
                    {
                        dsListedDR = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return dsListedDR;
        }
        public DataSet checkmonth_new(string sfcode, string tourmonth)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select top 1 Tour_Month as Tour_Date, Change_Status,REPLACE(Rejection_Reason,'asdf','''') as Rejection_Reason from Trans_TP_One where SF_Code='" + sfcode + "' and tour_month='" + tourmonth + "' and Confirmed=0 and (Change_Status=1 or Change_Status=2) ";


            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
               // Do Nothing
            }

            return dsListedDR;
        }

       // Added by Sridevi 
        public DataSet get_TP_Submission_Date_New(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            try
            {

                strQry = "Declare @SF_Code varchar(100)" +
                         "set @SF_Code=(select TP_Reporting_SF from Mas_Salesforce where Sf_Code='" + sf_code + "')" +
                         "Select convert(char(3),Tour_Date,107)+' '+ convert(char(4),Tour_Date,111) Tour_Date from Trans_TP_One " +
                         "where SF_Code='" + sf_code + "'and isnull(Confirmed,'')='0'  and (Change_Status=1 or Change_Status=2) " +
                         "select Sf_Name from Mas_Salesforce where Sf_Code=@SF_Code " +
                         "select sf_Designation_Short_Name from Mas_Salesforce where Sf_Code=@SF_Code " +
                         "select Sf_HQ from Mas_Salesforce where Sf_Code=@SF_Code ";

                dsListedDR = db_ER.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsListedDR;
        }
       //  Added by Sridevi
        public DataSet get_TP_Active_Edit(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select tour_date as tour_date from Trans_TP_One" +
                     " where SF_Code = '" + sf_code + "' and (Change_Status=1 or Change_Status=2) order by tour_date";


            dsListedDR = db_ER.Exec_DataSet(strQry);

            return dsListedDR;
        }
        // Added by Sridevi
        public DataSet get_TP_Approval(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select tour_date as tour_date from Trans_TP_One" +
                     " where SF_Code = '" + sf_code + "' and  Change_Status=1" +
                     " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " " +
                     " order by tour_date";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            return dsListedDR;
        }
        // Needed
        public DataSet GetTPWorkTypePlaceInvolved(string WorkType, string Div_Code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "select Place_Involved from Mas_WorkType_BaseLevel where Place_Involved='N' and Worktype_Name_B='" + WorkType + "' and Division_Code='" + Div_Code + "' ";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
         //Needed
        public DataSet getHolidays(string state_code, string divcode, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);

            DataSet dsHoliday = null;
           //  Modified by sridevi - 8-July-15  replaced '=' by 'like' as statecode is multiple
            strQry = "SELECT convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name" +
                      " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
                      " WHERE a.Division_Code = '" + divcode + "' AND (a.State_Code like '" + state_code + ',' + "%'  or " +
                      " a.State_Code like '%" + ',' + state_code + "' or a.State_Code like '%" + ',' + state_code + ',' + "%') " +
                      " and a.Holiday_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "'  " +
                      " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        // Added by jas
        public DataSet get_TP_Details_New(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);


            strQry = " Select objective,Worked_With_SF_Code,"+                  
                    " CASE when Tour_Schedule1 like'%,' then CAST(LEFT(ltrim(Tour_Schedule1),CHARINDEX(',', ltrim(Tour_Schedule1)) - 1) AS varchar(100)) else Tour_Schedule1 end Tour_Schedule1,"+
                    " CASE when Tour_Schedule2 like'%,' then CAST(LEFT(ltrim(Tour_Schedule2),CHARINDEX(',', ltrim(Tour_Schedule2)) - 1) AS varchar(100)) else Tour_Schedule2 end Tour_Schedule2," +
                    " CASE when Tour_Schedule3 like'%,' then CAST(LEFT(ltrim(Tour_Schedule3),CHARINDEX(',', ltrim(Tour_Schedule3)) - 1) AS varchar(100)) else Tour_Schedule3 end Tour_Schedule3,Worktype_code_B,Rejection_Reason,Worktype_Name_B, " +
                    " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,"+
                    " CASE when Territory_Code1 like'%,'  then CAST(LEFT(ltrim(Territory_Code1),  CHARINDEX(',', ltrim(Territory_Code1)) - 1) AS varchar(100)) else Territory_Code1 end Territory_Code1,"+
                    " CASE when Territory_Code2 like'%,'  then CAST(LEFT(ltrim(Territory_Code2),  CHARINDEX(',', ltrim(Territory_Code2)) - 1) AS varchar(100)) else Territory_Code2 end Territory_Code2," +
                    " CASE when Territory_Code3 like'%,'  then CAST(LEFT(ltrim(Territory_Code3),  CHARINDEX(',', ltrim(Territory_Code3)) - 1) AS varchar(100)) else Territory_Code3 end Territory_Code3 from Trans_TP_One " +
                    " where (Change_Status =1 or Change_Status=2) and sf_code = '" + sf_code + "' and Confirmed=0 " +
                    " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        // Added by jas 
        public DataSet get_TP_Reject(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);


            strQry = " Select objective,Worked_With_SF_Code,"+
                    " CASE when Tour_Schedule1 like'%,' then CAST(LEFT(ltrim(Tour_Schedule1), CHARINDEX(',', ltrim(Tour_Schedule1)) - 1) AS varchar(100)) else Tour_Schedule1 end Tour_Schedule1,"+
                    " CASE when Tour_Schedule2 like'%,' then CAST(LEFT(ltrim(Tour_Schedule2), CHARINDEX(',', ltrim(Tour_Schedule2)) - 1) AS varchar(100)) else Tour_Schedule2 end Tour_Schedule2," +
                    " CASE when Tour_Schedule3 like'%,' then CAST(LEFT(ltrim(Tour_Schedule3), CHARINDEX(',', ltrim(Tour_Schedule3)) - 1) AS varchar(100)) else Tour_Schedule3 end Tour_Schedule3,Worktype_code_B,Rejection_Reason,Worktype_Name_B, " +
                    " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,"+
                    " CASE when Territory_Code1 like'%,' then CAST(LEFT(ltrim(Territory_Code1), CHARINDEX(',', ltrim(Territory_Code1)) - 1) AS varchar(100)) else Territory_Code1 end Territory_Code1, "+
                    " CASE when Territory_Code2 like'%,' then CAST(LEFT(ltrim(Territory_Code2), CHARINDEX(',', ltrim(Territory_Code2)) - 1) AS varchar(100)) else Territory_Code2 end Territory_Code2," +
                    " CASE when Territory_Code3 like'%,' then CAST(LEFT(ltrim(Territory_Code3), CHARINDEX(',', ltrim(Territory_Code3)) - 1) AS varchar(100)) else Territory_Code3 end Territory_Code3 from Trans_TP_One " +
                    " where Change_Status=2 and sf_code = '" + sf_code + "' " +
                    " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
         //Added by jas
        public DataSet get_TP_Details_Approve_New(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);


            strQry = " Select objective,Worked_With_SF_Code,"+
                     " CASE when Tour_Schedule1 like'%,' then CAST(LEFT(ltrim(Tour_Schedule1), CHARINDEX(',', ltrim(Tour_Schedule1)) - 1) AS varchar(100)) else Tour_Schedule1 end Tour_Schedule1,"+
                     " CASE when Tour_Schedule2 like'%,' then CAST(LEFT(ltrim(Tour_Schedule2), CHARINDEX(',', ltrim(Tour_Schedule2)) - 1) AS varchar(100)) else Tour_Schedule2 end Tour_Schedule2," +
                     " CASE when Tour_Schedule3 like'%,' then CAST(LEFT(ltrim(Tour_Schedule3), CHARINDEX(',', ltrim(Tour_Schedule3)) - 1) AS varchar(100)) else Tour_Schedule3 end Tour_Schedule3,Worktype_code_B,Rejection_Reason,Worktype_Name_B, " +
                     " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,"+
                     " CASE when Territory_Code1 like'%,' then CAST(LEFT(ltrim(Territory_Code1), CHARINDEX(',', ltrim(Territory_Code1)) - 1) AS varchar(100)) else Territory_Code1 end Territory_Code1, "+
                     " CASE when Territory_Code2 like'%,' then CAST(LEFT(ltrim(Territory_Code2), CHARINDEX(',', ltrim(Territory_Code2)) - 1) AS varchar(100)) else Territory_Code2 end Territory_Code2," +
                     " CASE when Territory_Code3 like'%,' then CAST(LEFT(ltrim(Territory_Code3), CHARINDEX(',', ltrim(Territory_Code3)) - 1) AS varchar(100)) else Territory_Code3 end Territory_Code3 from Trans_TP_One " +
                     " where Change_Status =1 and sf_code = '" + sf_code + "' and Confirmed=0 " +
                     " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        // Added by jas
        public DataSet get_TP_Draft_New(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DateTime dt = Convert.ToDateTime(sCurrentDate);
            string strCurrentDate = dt.Month + "-" + dt.Day + "-" + dt.Year;
            DataSet dsTP = null;

            strQry = " select objective,Worked_With_SF_Code,"+
                     " CASE when Tour_Schedule1 like'%,' then CAST(LEFT(ltrim(Tour_Schedule1), CHARINDEX(',', ltrim(Tour_Schedule1)) - 1) AS varchar(100)) else Tour_Schedule1 end Tour_Schedule1,"+
                     " CASE when Tour_Schedule2 like'%,' then CAST(LEFT(ltrim(Tour_Schedule2), CHARINDEX(',', ltrim(Tour_Schedule2)) - 1) AS varchar(100)) else Tour_Schedule2 end Tour_Schedule2," +
                     " CASE when Tour_Schedule3 like'%,' then CAST(LEFT(ltrim(Tour_Schedule3), CHARINDEX(',', ltrim(Tour_Schedule3)) - 1) AS varchar(100)) else Tour_Schedule3 end Tour_Schedule3,WorkType_Code_B,Rejection_Reason,Worktype_Name_B " +
                     " ,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,"+
                     " CASE when Territory_Code1 like'%,' then CAST(LEFT(ltrim(Territory_Code1), CHARINDEX(',', ltrim(Territory_Code1)) - 1) AS varchar(100)) else Territory_Code1 end Territory_Code1, "+
                     " CASE when Territory_Code2 like'%,' then CAST(LEFT(ltrim(Territory_Code2), CHARINDEX(',', ltrim(Territory_Code2)) - 1) AS varchar(100)) else Territory_Code2 end Territory_Code2," +
                     " CASE when Territory_Code3 like'%,' then CAST(LEFT(ltrim(Territory_Code3), CHARINDEX(',', ltrim(Territory_Code3)) - 1) AS varchar(100)) else Territory_Code3 end Territory_Code3 from Trans_TP_One " +
                     " where sf_code = '" + sf_code + "'and Tour_Date = '" + strCurrentDate + "' and Change_Status =0";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        // Needed
        public DataSet FetchTerritory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            int iCount;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                    " UNION " +
                    " SELECT Territory_Code,Territory_Name FROM  Mas_Territory_Creation " +
                    " where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
         //Needed
        public DataSet FetchWorkType_New(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as WorkType_Code_B,'---Select---' as WorkType_Name_B " +
                         " UNION" +
                     " SELECT WorkType_Code_B,WorkType_Name_B FROM Mas_WorkType_BaseLevel where active_flag=0 and TP_DCR like '%T%' and Division_Code='" + Division_Code + "'";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
       // Added by Sridevi
        public int Approve_New(string MR_Code, string MR_Month, string MR_Year, string sf_code, string Division_Code, string ddlWT, string TP_Terr_Name, string TP_Terr1_Name, string TP_Terr2_Name, string TP_Date, string TP_Terr_Value, string TP_Terr1_Value, string TP_Terr2_Value, string ddlWT_Value, string ddlValueWT1, string ddlTextWT1, string ddlValueWT2, string ddlTextWT2, string SF_Name)
        {
            int iReturn = -1;
            int iCount = -1;

            DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
            string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
            DataSet dsListedDR = new DataSet();

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " Insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                            " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                            " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name," +
                            " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR) " +
                            " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                            " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                            " GETDATE() as Confirmed_Date,Rejection_Reason,'1' as Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name, " +
                            " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR from Trans_TP_One " +
                            " where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Date='" + strTPdt + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
       // Added by sridevi
        public int TP_Confirm(string MR_Code, string Tour_Month, string Tour_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsTPdate = new DataSet();

                //strQry = "Update Trans_TP set Confirmed = 1 where SF_Code='" + MR_Code + "' and Tour_Month='" + Tour_Month + "' and Tour_Year='" + Tour_Year + "' ";

                strQry = "EXEC sp_TP_Approved_MR '" + MR_Code + "', '" + Tour_Month + "','" + Tour_Year + "' ";

                iReturn = db.ExecQry(strQry);

               // iReturn = db.ExecQry(strQry);

                //strQry = "Select substring(convert(char(10),Tour_Date,103),4,2)+'/'+'01'+'/'+substring(convert(char(10),Tour_Date,103),7,4) as Tour_Date from Trans_TP where Confirmed = 1 and SF_Code='" + MR_Code + "' and Tour_Month='" + Tour_Month + "' and Tour_Year='" + Tour_Year + "' ";

                //dsTPdate = db.Exec_DataSet(strQry);

                //strQry = "update Mas_Salesforce set Last_TP_Date=DateAdd(MM,1,'" + dsTPdate.Tables[0].Rows[0]["Tour_Date"].ToString() + "') where Sf_Code='" + MR_Code + "'";

                //iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
       // Needed
        public int TP_Delete(string MR_Code, string Tour_Month, string Tour_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Delete from Trans_TP_One where SF_Code='" + MR_Code + "' and Tour_Month='" + Tour_Month + "' and Tour_Year='" + Tour_Year + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
       // Added By Sridevi
        public int Reject_New(string sf_code, string Tour_Month, string Tour_Year, string RejectReason, string Sf_Name)
        {
            int iReturn = -1;
            int iCount = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Trans_TP_One set Change_Status='2', TP_Approval_MGR='" + Sf_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + sf_code + "' and Tour_Month='" + Tour_Month + "' and Tour_Year = '" + Tour_Year + "' ";

                iReturn = db.ExecQry(strQry);

                //strQry = "select COUNT(*) from TP_Reject_B_Mgr where Rejection_Reason='" + RejectReason + "'";

                //iCount = db.Exec_Scalar(strQry);

                //if (iCount <= 0)
                //{

                //    strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) " +
                //             " Select SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,GETDATE() from Trans_TP_One where SF_Code='" + sf_code + "' " +
                //             " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' and Tour_Schedule3='" + TP_Terr2_Name + "' " +
                //             " and Tour_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "'";

                //    iReturn = db.ExecQry(strQry);

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        // Needed
        public DataSet get_WeekOff_Divcode(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where Fieldwork_Indicator LIKE '%W%' and Division_Code='" + Div_Code + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        // Needed
        public DataSet GetTPWorkTypeFieldWork(string sf_code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                         " select distinct a.Territory_Code,a.Territory_Name from Mas_Territory_Creation a,Mas_ListedDr b where " +
                         " CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and a.SF_Code='" + sf_code + "' "+
                         " and a.territory_active_flag=0 and b.ListedDr_Active_Flag=0 ";

                //strQry = " SELECT '0' as Territory_Code,'---Select---' as Territory_Name " +
                //        " UNION " +
                //        " SELECT stuff((select ', '+cast(Territory_Code as varchar) from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code  and " + 
                //        " CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0  for XML path('')),1,2,'') ,"+
                //        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code  and  " +
                //        " CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0  for XML path('')),1,2,'') " +
                //        " territory_Name  FROM Mas_ListedDr d WHERE d.Sf_Code =  '" + sf_code + "' and d.ListedDr_Active_Flag = 0 ";

                

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
         //Needed
        public DataSet get_Holiday_DivCode(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where fieldwork_indicator LIKE '%H%' and Division_Code='" + Div_Code + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

         //Added
         public DataSet get_WeekOff(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            // strQry = " select WeekOff from Mas_Division a, Mas_Salesforce_AM b Where a.Division_Code = b.Division_Code and b.Sf_Code = '" + sf_code + "' ";
            strQry = " EXEC TP_SateWise_Weekly '" + sf_code + "' ";


            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
       // Needed
        
         //Needed
         public DataSet Get_TP_ApprovalTitle(string sfcode)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsListedDR = null;

             strQry = "Select Sf_Name,convert(char(3),sf_TP_Active_Dt,107)+' '+ convert(char(4),sf_TP_Active_Dt,111) " +
                      " Sf_Joining_Date,convert(char(10),Sf_Joining_Date,103) as DOJ,Sf_HQ, convert(char(10),sf_TP_Active_Dt,103) Date,sf_Designation_Short_Name from Mas_Salesforce where Sf_Code='" + sfcode + "' ";

             dsListedDR = db_ER.Exec_DataSet(strQry);

             if (dsListedDR.Tables[0].Rows.Count > 0)
             {
                // Do Nothing
             }

             return dsListedDR;
         }

         //Modified
         public DataSet get_TP_Pending_Approval(string sf_code, string Div_Code)
         {
             DB_EReporting db_ER = new DB_EReporting();
               
             DataSet dsTP = null;

             if (Div_Code.Contains(','))
             {
                 Div_Code = Div_Code.Remove(Div_Code.Length - 1);
             }

            //if (sf_code != "admin")
            //{

            //    strQry = "select distinct a.sf_code, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,(select sf_Designation_Short_Name  from Mas_Salesforce c where c.sf_code=a.Sf_Code) as sf_Designation_Short_Name ,  " +
            //             " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
            //             "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
            //             " from Mas_Salesforce_AM a, Trans_TP b " +
            //             " where a.sf_code = b.sf_code and a.TP_AM  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
            //             " and b.Change_Status=1 and b.confirmed=0" +
            //             " union all " +
            //             "select distinct a.sf_code, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,(select sf_Designation_Short_Name  from Mas_Salesforce c where c.sf_code=a.Sf_Code) as sf_Designation_Short_Name ,  " +
            //             " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
            //             "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
            //             " from Mas_Salesforce_AM a, Trans_TP_One b " +
            //             " where a.sf_code = b.sf_code and a.TP_AM  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
            //             " and b.Change_Status=1 and b.confirmed=0";

            //}
            //else
            //{
            //    strQry = "select distinct a.sf_code, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,(select sf_Designation_Short_Name  from Mas_Salesforce c where c.sf_code=a.Sf_Code) as sf_Designation_Short_Name ,  " +
            //            " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
            //            "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
            //            " from Mas_Salesforce_AM a, Trans_TP b " +
            //            " where a.sf_code = b.sf_code and a.TP_AM  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
            //            " and b.Change_Status=1 and b.confirmed=0" +
            //            " union all " +
            //            "select distinct a.sf_code, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,(select sf_Designation_Short_Name  from Mas_Salesforce c where c.sf_code=a.Sf_Code) as sf_Designation_Short_Name ,  " +
            //            " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
            //            "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
            //            " from Mas_Salesforce_AM a, Trans_TP_One b " +
            //            " where a.sf_code = b.sf_code and a.TP_AM  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
            //            " and b.Change_Status=1 and b.confirmed=0";
            //}

            strQry = "select distinct a.sf_code, a.sf_name +'-'+sf_Designation_Short_Name +'-'+a.Sf_HQ as sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
                       " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
                       "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
                       " from Mas_Salesforce a, Trans_TP_One b,Mas_Salesforce_AM d " +
                       " where a.sf_code = b.sf_code and a.Sf_Code=d.Sf_Code and d.TP_AM  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
                       " and b.Change_Status=1 and b.confirmed=0";

            try
             {
                 dsTP = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return dsTP;
         }
        public DataSet GetTPConfirmed_Date(string Tour_Month, string Tour_Year, string sf_code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;
            //if (!RecordExist(div_sname))
            //{
            try
            {
                strQry = "select count(Confirmed_Date) Confirmed_Date from Trans_TP where Tour_Month='" + Tour_Month + "' and Tour_Year='" + Tour_Year + "' and SF_Code='" + sf_code + "' and confirmed=0 and change_status=1";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return dsTP;
        }
        public int RecordAddMGR(string TP_Date, string TP_Day, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int iCount = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation WHERE Division_Code = '" + Division_Code + "' ";
                //Territory_Code = db.Exec_Scalar(strQry);

                if (TP_Submit == false)
                {
                    // Change_Status - 0 : Not Completed
                    // WorkType_Code - 0 : MR

                    strQry = "select COUNT(Objective) from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + dt_TourPlan + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                    iCount = db.Exec_Scalar(strQry);

                    if (iCount <= 0)
                    {
                        strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule,Objective, " +
                             " Worked_With_SF_Code,WorkType,Division_Code,Change_Status,WorkType_Code,submission_date) " +
                             " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan + "',  '" + TP_WW + "', " +
                             " '" + TP_Objective + "', '" + TP_Terr + "','" + worktype + "', '" + Division_Code + "', 0, 0,getdate()) ";
                    }
                    else
                    {
                        strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "'," +
                                 " WorkType = '" + worktype + "' , " +
                                 " Tour_Schedule='" + TP_WW + "', submission_date=getdate() " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + dt_TourPlan + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                    }

                }
                else if (TP_Submit == true)
                {
                    // Change_Status - 1 : Completed
                    // WorkType_Code - 0 : MR

                    strQry = "select COUNT(Objective) from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + dt_TourPlan + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                    iCount = db.Exec_Scalar(strQry);

                    if (iCount <= 0)
                    {
                        strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule,Objective, " +
                             " Worked_With_SF_Code,WorkType,Division_Code,Change_Status,WorkType_Code,submission_date) " +
                             " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan + "',  '" + TP_WW + "', " +
                             " '" + TP_Objective + "', '" + TP_Terr + "',  '" + worktype + "','" + Division_Code + "', 1, 0,getdate()) ";
                    }
                    else
                    {
                        strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "'," +
                             " WorkType = '" + worktype + "' , Tour_Schedule='" + TP_WW + "',Change_Status=1,submission_date=getdate() " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + dt_TourPlan + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                    }
                }

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }

        public int RecordAddMGRApproval_TP(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName, string Div_Code)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();
                DataSet dsWT = new DataSet();

                int Division_Code = -1;
                int iCount = -1;

                strQry = "select WorkType_Code_M from Mas_WorkType_Mgr where WorkType_Code_M='" + TP_WW + "' and Division_Code='" + Div_Code + "'";
                dsWT = db.Exec_DataSet(strQry);
                TP_WW = dsWT.Tables[0].Rows[0][0].ToString();

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                // Change_Status - 1 : Completed
                // WorkType_Code - 0 : MR

                strQry = "select * from Trans_TP " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' " +
                            " and Tour_Schedule1='" + WorkType_Name + "' and Worktype_Name_B='" + worktype + "' " +
                            " and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                            " union all " +
                            " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " + " " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' " +
                            " and Tour_Schedule1='" + WorkType_Name + "' and Worktype_Name_B='" + worktype + "' " +
                            " and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    //strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                    //        " Worked_With_SF_Code,Objective,Tour_Schedule1,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Worked_With_SF_Name) " +
                    //        " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "', " +
                    //        "'" + TP_Terr + "','" + TP_Objective + "','" + WorkType_Name + "','" + TP_WW + "',  '" + worktype + "', '" + Division_Code + "', 0,getdate(),'" + TP_Terr_Code + "','" + TP_WWName + "') ";

                    //iReturn = db.ExecQry(strQry);

                    strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                             "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                             "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                             "Division_Code,GETDATE(),'C'Mode from Trans_TP where SF_Code='" + sf_code + "' " +
                             "and Tour_Date='" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                           "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                           "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                           "Division_Code,GETDATE(),'C'Mode from Trans_TP_One where SF_Code='" + sf_code + "' " +
                           "and Tour_Date='" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' ";

                    iReturn = db.ExecQry(strQry);
                }

                strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                         "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                         "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                         "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                         "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }



        public int RecordEditMGR_TP(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();

                int Division_Code = -1;
                int iCount = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = " select * from Trans_TP " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and Worktype_Name_B='" + worktype + "' " +
                            " and Tour_Schedule1='" + WorkType_Name + "' and Worked_With_SF_Name='" + TP_WWName + "' and Objective='" + TP_Objective + "' " +
                            " and Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                            " union all " +
                            " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and Worktype_Name_B='" + worktype + "' " +
                            " and Tour_Schedule1='" + WorkType_Name + "' and Worked_With_SF_Name='" + TP_WWName + "' and Objective='" + TP_Objective + "' " +
                            " and Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                //else
                //{                   

                //    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                //             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                //             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                //             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                //    iReturn = db.ExecQry(strQry);

                //    strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                //             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                //             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                //             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                //    iReturn = db.ExecQry(strQry);
                //}



            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }

        public int RecordAddMGR_TP(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName, string Div_Code)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();
                DataSet dsWT = new DataSet();

                int Division_Code = -1;
                int iCount = -1;

                strQry = "select WorkType_Code_M from Mas_WorkType_Mgr where WorkType_Code_M='" + TP_WW + "' and Division_Code='" + Div_Code + "'";
                dsWT = db.Exec_DataSet(strQry);
                TP_WW = dsWT.Tables[0].Rows[0][0].ToString();

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "select * from Trans_TP " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                            " union all " +
                            " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " + " " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                            " Worked_With_SF_Code,Objective,Tour_Schedule1,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Worked_With_SF_Name) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "', " +
                            "'" + TP_Terr + "','" + TP_Objective + "','" + WorkType_Name + "','" + TP_WW + "',  '" + worktype + "', '" + Division_Code + "', 0,getdate(),'" + TP_Terr_Code + "','" + TP_WWName + "') ";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    //strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                    //         "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                    //         "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                    //         "Division_Code,GETDATE(),'C'Mode from Trans_TP_One where SF_Code='" + sf_code + "' and Tour_Date='" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' ";

                    //iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }
        // Added by Sridevi
        public int RecordAddMGR_TP_New(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName, string Div_Code)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();
                DataSet dsWT = new DataSet();

                int Division_Code = -1;
               
                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry =    " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " + " " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                            " Worked_With_SF_Code,Objective,Tour_Schedule1,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Worked_With_SF_Name,Confirmed,HQCodes,HQNames) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "', " +
                            "'" + TP_Terr + "','" + TP_Objective + "','" + WorkType_Name + "','" + TP_WW + "',  '" + worktype + "', '" + Division_Code + "', 0,getdate(),'" + TP_Terr_Code + "','" + TP_WWName + "',0,'" + TP_Terr + "','" + TP_WWName + "') ";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "', HQCodes='" + TP_Terr + "',HQNames='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Approve(string MR_Code, string MR_Month, string MR_Year, string sf_code, string Division_Code, string ddlWT, string TP_Terr_Name, string TP_Terr1_Name, string TP_Terr2_Name, string TP_Date, string TP_Terr_Value, string TP_Terr1_Value, string TP_Terr2_Value, string ddlWT_Value, string ddlValueWT1, string ddlTextWT1, string ddlValueWT2, string ddlTextWT2, string SF_Name)
        {
            int iReturn = -1;
            int iCount = -1;

            DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
            string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
            DataSet dsListedDR = new DataSet();

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " Select SF_Code from Trans_TP_One " +
                         " where SF_Code='" + MR_Code + "' and Tour_Month=" + MR_Month + " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' " +
                         " and Tour_Schedule3='" + TP_Terr2_Name + "' and Worktype_Name_B ='" + ddlWT + "' and Tour_Year=" + MR_Year + " " +
                         " and Division_Code= '" + Division_Code + "'and Tour_Date='" + strTPdt + "' " +
                         " union all " +
                         " Select SF_Code from Trans_TP " +
                         " where SF_Code='" + MR_Code + "' and Tour_Month=" + MR_Month + " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' " +
                         " and Tour_Schedule3='" + TP_Terr2_Name + "' and Worktype_Name_B ='" + ddlWT + "' and Tour_Year=" + MR_Year + " " +
                         " and Division_Code= '" + Division_Code + "'and Tour_Date='" + strTPdt + "'";


                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                             "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                             "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                             "Division_Code,GETDATE(),'C'Mode from Trans_TP_One where SF_Code='" + MR_Code + "' and Tour_Date='" + strTPdt + "'";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    //strQry = " Update Trans_TP set confirmed=1, confirmed_date=getdate(),Change_Status =1" +
                    //         " where SF_Code='" + MR_Code + "' and Tour_Date='" + strTPdt + "'";

                    //iReturn = db.ExecQry(strQry);
                }

                strQry = " Update Trans_TP set confirmed=1, confirmed_date=getdate(),Change_Status =2,Tour_Schedule1='" + TP_Terr_Name + "'," +
                         " Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "',Territory_Code1='" + TP_Terr_Value + "', " +
                         " Territory_Code2='" + TP_Terr1_Value + "',Territory_Code3='" + TP_Terr2_Value + "',WorkType_Code_B='" + ddlWT_Value + "', " +
                         " Worktype_Name_B ='" + ddlWT + "',WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "', " +
                         " WorkType_Code_B2='" + ddlValueWT2 + "',Worktype_Name_B2='" + ddlTextWT2 + "',TP_Approval_MGR='" + SF_Name + "' where SF_Code='" + MR_Code + "'  " +
                         " and Tour_Date='" + strTPdt + "'";

                iReturn = db.ExecQry(strQry);

                strQry = " Update Trans_TP_One set confirmed=1, confirmed_date=getdate(),Change_Status =2,Tour_Schedule1='" + TP_Terr_Name + "'," +
                         " Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "',Territory_Code1='" + TP_Terr_Value + "', " +
                         " Territory_Code2='" + TP_Terr1_Value + "',Territory_Code3='" + TP_Terr2_Value + "',WorkType_Code_B='" + ddlWT_Value + "', " +
                         " Worktype_Name_B ='" + ddlWT + "',WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "', " +
                         " WorkType_Code_B2='" + ddlValueWT2 + "',Worktype_Name_B2='" + ddlTextWT2 + "',TP_Approval_MGR='" + SF_Name + "' where SF_Code='" + MR_Code + "'  " +
                         " and Tour_Date='" + strTPdt + "'";

                iReturn = db.ExecQry(strQry);

                strQry = " Insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name," +
                         " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR) " +
                         " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name, " +
                         " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR from Trans_TP_One " +
                         " where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Date='" + strTPdt + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Reject(string sf_code, string RejectReason, string TP_Terr_Name, string TP_Terr1_Name, string TP_Terr2_Name, string lblDate, string txtReason, string strMonth, string Sf_Name)
        {
            int iReturn = -1;
            int iCount = -1;
            DateTime dt = Convert.ToDateTime(lblDate.ToString());

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Trans_TP_One set Change_Status='0',confirmed=0,TP_Approval_MGR='" + Sf_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + sf_code + "' and Tour_Month='" + strMonth + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Trans_TP set Change_Status='0',confirmed=0,TP_Approval_MGR='" + Sf_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + sf_code + "' and Tour_Month='" + strMonth + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "select COUNT(*) from TP_Reject_B_Mgr where Rejection_Reason='" + txtReason + "'";

                iCount = db.Exec_Scalar(strQry);

                if (iCount <= 0)
                {

                    strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) " +
                             " Select SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,GETDATE() from Trans_TP_One where SF_Code='" + sf_code + "' " +
                             " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' and Tour_Schedule3='" + TP_Terr2_Name + "' " +
                             " and Tour_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) " +
                            " Select SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,GETDATE() from Trans_TP where SF_Code='" + sf_code + "' " +
                            " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' and Tour_Schedule3='" + TP_Terr2_Name + "' " +
                            " and Tour_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "'";

                    iReturn = db.ExecQry(strQry);

                }

                //else
                //{
                //    strQry = " Update TP_Reject_B_Mgr set Tour_Schedule1='" + TP_Terr_Name + "'," +
                //           "  Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "' where SF_Code='" + sf_code + "'  ";

                //    iReturn = db.ExecQry(strQry);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int ReadyforCalanderEdit(string MR_Code, string MR_Month, string MR_Year, string SF_Name)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update Trans_TP_One set change_status=1,Confirmed=0,Confirmed_Date=GETDATE(),TP_Sf_Name='" + SF_Name + "'" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int ReadyforCalanderApproval(string MR_Code, string MR_Month, string MR_Year, string sDate, string SF_Name)
        {
            int iReturn = -1;
            int iCount = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsTPdate = new DataSet();

                strQry = "EXEC sp_TP_Approved_MGR '" + MR_Code + "', '" + MR_Month + "','" + MR_Year + "','"+ SF_Name +"' ";

                iReturn = db.Exec_Scalar(strQry);

                //if (iCount <= 0)
                //{
                //    strQry = " update Trans_TP_one set change_status=1,TP_Approval_MGR='" + SF_Name + "',Confirmed=1,Confirmed_Date=GETDATE()" +
                //     " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                //     " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                //    iReturn = db.ExecQry(strQry);

                //    strQry = " Insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                //             " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                //             " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name" +
                //             " ,TP_Sf_Name,TP_Approval_MGR) " +
                //             " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                //             " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                //             " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name,TP_Sf_Name,TP_Approval_MGR from Trans_TP_One " +
                //             " where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "'";

                //    iReturn = db.ExecQry(strQry);
                //}
                //else
                //{
                //    strQry = " update Trans_TP set change_status=1,Confirmed=1,Confirmed_Date=GETDATE()" +
                //             " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                //             " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                //    iReturn = db.ExecQry(strQry);
                //}

                //strQry = "Select substring(convert(char(10),Tour_Date,103),4,2)+'/'+'01'+'/'+substring(convert(char(10),Tour_Date,103),7,4) as Tour_Date from Trans_TP where Confirmed = 1 and SF_Code='" + MR_Code + "' and Tour_Month='" + Convert.ToInt32(MR_Month) + "' and Tour_Year='" + Convert.ToInt32(MR_Year) + "' ";

                //dsTPdate = db.Exec_DataSet(strQry);

                //strQry = "update Mas_Salesforce set Last_TP_Date=DateAdd(MM,1,'" + dsTPdate.Tables[0].Rows[0]["Tour_Date"].ToString() + "') where Sf_Code='" + MR_Code + "'";

                //iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int ReadyforReject(string MR_Code, string MR_Month, string MR_Year, string RejectReason, DateTime sDate, string div_code, string SF_Name)
        {

            int iReturn = -1;
            int iCount = -1;
            DateTime dt = Convert.ToDateTime(sDate.ToString());

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Trans_TP_One set Change_Status='0',confirmed=0,TP_Approval_MGR='" + SF_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Trans_TP set Change_Status='0',confirmed=0,TP_Approval_MGR='" + SF_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "select COUNT(*) from TP_Reject_B_Mgr where Rejection_Reason='" + RejectReason + "' and SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' ";

                iCount = db.Exec_Scalar(strQry);

                if (iCount <= 0)
                {
                    //strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) " +
                    //         " Select SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,GETDATE() from Trans_TP_One where SF_Code='" + MR_Code + "' " +
                    //         " and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' and Tour_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "' ";

                    strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) values " +
                             " ('" + MR_Code + "','" + MR_Month + "','" + MR_Year + "','" + RejectReason + "','" + "9" + "',GETDATE())";


                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = " Update TP_Reject_B_Mgr set Rejection_Reason='" + RejectReason + "',Reject_Date=GETDATE() where SF_Code='" + MR_Code + "' " +
                               " and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' and Reject_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "' ";

                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int ReadyforApproval(string MR_Code, string MR_Month, string MR_Year, string SF_Name)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update Trans_TP_One set change_status=1,Confirmed=0,TP_Sf_Name='" + SF_Name + "'" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Trans_TP set change_status=1,Confirmed=0,TP_Sf_Name='" + SF_Name + "'" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int ReadyforCalanderDraft(string MR_Code, string MR_Month, string MR_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update Trans_TP_One set change_status=0" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



       

        public DataSet getManagerList(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Sf_Code,Sf_Name from Mas_Salesforce" +
                     " WHERE sf_type=2 and Reporting_To_SF = '" + sf_code + "' and sf_TP_Active_Flag=0  " +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getWorkType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr " +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getManagerHQ(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select distinct Sf_HQ from Mas_Salesforce" +
                     " WHERE sf_code = '" + sf_code + "' " +
                     " ORDER BY 1";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public int getDivision(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            strQry = "select division_code from Mas_Salesforce" +
                     " WHERE sf_code = '" + sf_code + "' " +
                     " ORDER BY 1";
            try
            {
                iReturn = db_ER.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int get_TP_Count(string sf_code, string sDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            strQry = "select COUNT(SF_Code) from Trans_TP_One WHERE sf_code = '" + sf_code + "' and Tour_Date='" + sDate + "' ";

            try
            {
                iReturn = db_ER.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_TP_Count_one(string sf_code, string sDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select COUNT(SF_Code) from Trans_TP_One WHERE sf_code = '" + sf_code + "' and Tour_Date='" + sDate + "' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataSet get_TP_HQ(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string divcode = getDivision(sf_code).ToString();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_TP_HQ '" + divcode + "', '" + sf_code + "' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataSet get_TP_FieldForce(string sf_HQ)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select sf_code,sf_name from Mas_Salesforce " +
                      " where Sf_Code != 'admin' and Sf_HQ='" + sf_HQ + "' and sf_TP_Active_Flag=0 ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataSet get_MR_Reporting(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            int iCount = -1;



            strQry = "select '0' sf_code,'' sf_name " +
                     " union " +
                     " select  sf_code,sf_name+' - '+Sf_HQ+' - '+sf_Designation_Short_Name from Mas_Salesforce " +
                      " where Sf_Code != 'admin' and TP_Reporting_SF ='" + sf_code + "' and sf_TP_Active_Flag=0 " +
                      " order by 2";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

         public DataSet get_sf_name_schedule(string sf_code)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsSF = null;
             strQry = "select sf_name from Mas_Salesforce " +
                       " where Sf_Code = '" + sf_code + "' ";

             try
             {
                 dsSF = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsSF;
         }

        public DataSet get_TP_Territory(string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Territory_Code,Territory_Name  from Mas_Territory_Creation  " +
                      " where Sf_code ='" + sf_Code + "' and Territory_Active_Flag=0 ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataSet get_TP_EntryforMGR(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = "select convert(char(10),tour_Date,103) tour_date, Worked_With_SF_Name,worktype_name_b,worktype_name_b1,worktype_name_b2, Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worked_With_SF_Name,objective" +
            //            " from Trans_TP_One " +
            //            " where  sf_code = '" + sf_code + "' " +
            //            " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " " +
            //            " Union All " +
            //            "select convert(char(10),tour_Date,103) tour_date,Worked_With_SF_Name,worktype_name_b,worktype_name_b1,worktype_name_b2, Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worked_With_SF_Name,objective" +
            //            " from Trans_TP " +
            //            " where sf_code = '" + sf_code + "' " +
            //            " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " order by 1";

            strQry = "select convert(char(10),tour_Date,103) tour_date,Worked_With_SF_Name, " +
                               "case worktype_name_b when '' then 'Weekly Off' else worktype_name_b end worktype_name_b,worktype_name_b1,worktype_name_b2," +
                               "case Tour_Schedule1 when convert(varchar(50),0) then '' else Tour_Schedule1 end Tour_Schedule1," +
                               "case Tour_Schedule2 when convert(varchar(50),0)then '' else Tour_Schedule2 end Tour_Schedule2," +
                               "case Tour_Schedule3 when convert(varchar(50),0) then '' else Tour_Schedule3 end Tour_Schedule3," +
                               " Worked_With_SF_Name,objective" +
                               " from Trans_TP " +
                               " where sf_code = '" + sf_code + "' " +
                               " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " and Confirmed=1 order by 1";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Entry(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select tour_date, objective, Worked_With_SF_Code, " +
                     " case when Change_Status = 0 then 'Saved. But not yet submitted' else " +
                     " case when Change_Status = 1 then 'Submitted. But not yet approved' else 'Approved' end end Change_Status from Trans_TP_One " +
                     " where WorkType_Code=0 and sf_code = '" + sf_code + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Status(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select distinct submission_date,Confirmed_Date," +
                     "case Confirmed " +
                     " when 1  then 'Approved'  " +
                     " when 1 & Change_Status then 'Saved. But not yet submitted'else 'Submitted. But not yet approved' " +
                     " end as Change_Status " +
                     " from Trans_TP_One where sf_code = '" + sf_code + "' " +
                     " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }



        public DataSet get_TP_Entry(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select tour_date, objective,Worktype_Name_B, Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3, " +
                     "case Change_Status " +
                     "when 0 then 'Saved. But not yet submitted' " +
                     "when 1 & Confirmed then 'Approved' else 'Submitted. But not yet approved' end as Change_Status from Trans_TP_One " +
                     "where sf_code = '" + sf_code + "' " +
                     "and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " order by 1";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet get_TP_Entry(string sf_code, int iMonth, int iYear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "declare @Desingation_Code varchar(5) " +
                     "set @Desingation_Code=(select Designation_Code from Mas_Salesforce where Sf_Code='" + sf_code + "') " +
                     "select CONVERT(char(10),a.Tour_Date,103) Tour_Date,a.objective,a.Worked_With_SF_Name,a.Worktype_Name_B,a.Worktype_Name_B1,a.Worktype_Name_B2,a.Worked_With_SF_Code,a.Tour_Schedule1,a.Tour_Schedule2,a.Tour_Schedule3, " +
                     "a.Submission_date,a.Confirmed_Date,case a.Change_Status when 0 then 'Saved. But not yet submitted' " +
                     "when 1 & a.Confirmed then 'Approved' else 'Submitted. But not yet approved' end as Change_Status,b.Sf_HQ,b.sf_desgn, " +
                     "(select Designation_Short_Name from Mas_SF_Designation where Designation_Code= @Desingation_Code) Designation_Short_Name " +
                     "from Trans_TP_One a,Mas_Salesforce b " +
                     "where a.sf_code = '" + sf_code + "' and a.sf_code=b.sf_code " +
                     "and month(a.tour_date) = " + iMonth + " and year(a.tour_date) = " + iYear + " " +
                     "union all " +
                     "select CONVERT(char(10),a.Tour_Date,103) Tour_Date, a.objective,a.Worked_With_SF_Name,Worktype_Name_B,Worktype_Name_B1,Worktype_Name_B2,a.Worked_With_SF_Code,a.Tour_Schedule1,a.Tour_Schedule2,a.Tour_Schedule3, " +
                     "a.Submission_date,a.Confirmed_Date,case a.Change_Status when 0 then 'Saved. But not yet submitted' " +
                     "when 1 & a.Confirmed then 'Approved' else 'Submitted. But not yet approved' end as Change_Status,b.Sf_HQ,b.sf_desgn, " +
                     "(select Designation_Short_Name from Mas_SF_Designation where Designation_Code= @Desingation_Code) Designation_Short_Name " +
                     "from Trans_TP a,Mas_Salesforce b " +
                     "where a.sf_code = '" + sf_code + "' and a.sf_code=b.sf_code " +
                     "and month(a.tour_date) = " + iMonth + " and year(a.tour_date) = " + iYear + " ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Draft(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DateTime dt = Convert.ToDateTime(sCurrentDate);
            string strCurrentDate = dt.Month + "-" + dt.Day + "-" + dt.Year;
            DataSet dsTP = null;

            //strQry = "select objective,Worked_With_SF_Code,Tour_Schedule,SDP1,SDP2,WorkType from Trans_TP_One " +
            //         " where WorkType_Code=0 and Change_Status =0 and sf_code = '" + sf_code + "' " +
            //         " and Tour_Date = '" + strCurrentDate + "'  ";

            strQry = " select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,WorkType_Code_B,Rejection_Reason,Worktype_Name_B " +
                     " ,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2 from Trans_TP " +
                     " where sf_code = '" + sf_code + "'and Tour_Date = '" + strCurrentDate + "' and Change_Status =0 " +
                     " union all " +
                     " select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,WorkType_Code_B,Rejection_Reason,Worktype_Name_B " +
                     " ,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2 from Trans_TP_One " +
                     " where sf_code = '" + sf_code + "'and Tour_Date = '" + strCurrentDate + "' and Change_Status =0";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Details(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);


            strQry = " Select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worktype_code_B,Worktype_Name_B, " +
                     " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2 from Trans_TP_One " +
                     " where (Change_Status =1 or Change_Status=2) and sf_code = '" + sf_code + "' and Confirmed=0 " +
                     " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' " +
                     " union all  " +
                     " Select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worktype_code_B,Worktype_Name_B, " +
                     " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2 from Trans_TP " +
                     " where (Change_Status =1 or Change_Status=2) and sf_code = '" + sf_code + "' and Confirmed=0 " +
                     " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TourPlan_MGRDetails(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select Tour_Schedule1,objective,Worked_With_SF_Code,WorkType_Code_B from Trans_TP_One " +
                     " where sf_code = '" + sf_code + "' " +
                     " and Tour_Date = '" + sCurrentDate + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }



        public DataSet get_WeekOff()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where TP_Flag LIKE '%W%'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_FieldWork(string strlike)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where Worktype_Name_B LIKE '" + strlike + "%'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Holiday()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where TP_Flag LIKE '%H%'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Calander_Date(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct(b.sf_TP_Active_Dt)sf_TP_Active_Dt " +
                     " from Trans_TP_One a,Mas_Salesforce b where a.SF_Code = '" + sf_code + "' and a.SF_Code=b.Sf_Code " +
                     " and a.Change_Status=1 and a.confirmed=0";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            else
            {
                strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

                try
                {
                    dsListedDR = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dsListedDR;
        }


        public DataSet get_TP_Active_Date(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            int iCount = -1;

            //strQry = "select top 1'01-' + RTRIM(cast((Tour_Month + 1) as CHAR(2)))+'-' + cast(Tour_Year as CHAR(4))+' 00:00:00.000'  from Trans_TP_One" +
            //         " where SF_Code = '" + sf_code + "' and Change_Status=1 and confirmed=1 "+
            //         "order by Tour_Date desc";           


            strQry = "select top(1)(Tour_Date) as Tour_Month from Trans_TP where " +
                     "SF_Code = '" + sf_code + "' and confirmed=0 order by Tour_Month asc ";

            dsListedDR = db_ER.Exec_DataSet(strQry);
            if (dsListedDR.Tables[0].Rows.Count == 0)
            {
                strQry = "select top 1'01-' + substring(convert(char(7),dateadd(mm,1,Tour_Date),120),6,2)+'-'+ " +
                         "substring(convert(char(7),dateadd(mm,1,Tour_Date),120),0,5)+' 00:00:00.000' " +
                         " from Trans_TP  where SF_Code = '" + sf_code + "' and (Change_Status=1 or Change_Status=2) and confirmed=1 " +
                         "order by Tour_Date desc";

                dsListedDR = db_ER.Exec_DataSet(strQry);
            }


            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            else
            {
                strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

                try
                {
                    dsListedDR = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dsListedDR;
        }


        public DataSet get_TPCalender_Active_Date(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            int iCount = -1;

            //strQry = "select top 1'01-' + RTRIM(cast((Tour_Month + 1) as CHAR(2)))+'-' + cast(Tour_Year as CHAR(4))+' 00:00:00.000'  from Trans_TP_One" +
            //         " where SF_Code = '" + sf_code + "' and Change_Status=1 and confirmed=1 "+
            //         "order by Tour_Date desc";           

            //strQry = "select top(1)(Tour_Date) as Tour_Month from Trans_TP_One where " +
            //         "SF_Code = '" + sf_code + "' and confirmed=0 " +
            //         " union all " +
            //         "select top(1)(Tour_Date) as Tour_Month from Trans_TP where " +
            //         "SF_Code = '" + sf_code + "' and confirmed=0 order by Tour_Month asc ";

            strQry = "select top(1)(Tour_Date) as Tour_Month from Trans_TP where " +
                     "SF_Code = '" + sf_code + "' and confirmed=0 order by Tour_Month asc ";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count == 0)
            {
                strQry = "select isnull(max('01-' +  substring(convert(char(7),dateadd(mm,1,Tour_Date),120),6,2)+'-'+ " +
                         "substring(convert(char(7),dateadd(mm,1,Tour_Date),120),0,5)+' 00:00:00.000'),'') as Tour_Date " +
                         "from Trans_TP where SF_Code=SF_Code and SF_Code = '" + sf_code + "' " +
                         "and Change_Status=1 and confirmed=1 " +
                         "union all " +
                         "select isnull(max('01-' +  substring(convert(char(7),dateadd(mm,1,Tour_Date),120),6,2)+'-'+ " +
                         "substring(convert(char(7),dateadd(mm,1,Tour_Date),120),0,5)+' 00:00:00.000'),'') as Tour_Date " +
                         "from Trans_TP_One  where SF_Code=SF_Code and SF_Code = '" + sf_code + "' " +
                         "and Change_Status=1 and confirmed=1";

                dsListedDR = db_ER.Exec_DataSet(strQry);
            }



            if (dsListedDR.Tables[0].Rows[0][0] != "")
            {
                //Do Nothing
            }

            else
            {
                strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

                try
                {
                    dsListedDR = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dsListedDR;
        }
        public DataSet get_TP_Submission_Date(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            try
            {

                strQry = "Declare @SF_Code varchar(100)" +
                         "set @SF_Code=(select TP_Reporting_SF from Mas_Salesforce where Sf_Code='" + sf_code + "')" +
                         "Select convert(char(3),Tour_Date,107)+' '+ convert(char(4),Tour_Date,111) Tour_Date from Trans_TP  " +
                         "where SF_Code='" + sf_code + "'and isnull(Confirmed,'')='0'  and (Change_Status=1 or Change_Status=2) " +
                         "union all  " +
                         "Select convert(char(3),Tour_Date,107)+' '+ convert(char(4),Tour_Date,111) Tour_Date from Trans_TP_One " +
                         "where SF_Code='" + sf_code + "'and isnull(Confirmed,'')='0'  and (Change_Status=1 or Change_Status=2) " +
                         "select Sf_Name from Mas_Salesforce where Sf_Code=@SF_Code";

                dsListedDR = db_ER.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsListedDR;
        }

              public DataSet TPCalander_checkmonth(string sfcode, string tourmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select top 1 Tour_Month  from Trans_TP" +
                     " where SF_Code = '" + sfcode + "' and tour_month='" + tourmonth + "' and  Change_Status=1 " +
                     " order by Tour_Date desc ";


            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            return dsListedDR;
        }

        public DataSet checkmonth(string sfcode, string tourmonth)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select top 1 Tour_Month as Tour_Date from Trans_TP_One where SF_Code='" + sfcode + "' and tour_month='" + tourmonth + "' and Confirmed=0 and (Change_Status=1 or Change_Status=2) " +
                     "union all " +
                     "select top 1 Tour_Month as Tour_Date from Trans_TP where SF_Code='" + sfcode + "'  and tour_month='" + tourmonth + "' and Confirmed=0 and (Change_Status=1 or Change_Status=2) order by Tour_Date desc";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            return dsListedDR;
        }
        public DataSet get_TP_Calendar(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTP = null;
            DateTime dtTP;
            int iMonth;
            int iYear;

            string sDate = string.Empty;
            //int iDays;
            //int iCount;

            strQry = "SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

            dsTP = db_ER.Exec_DataSet(strQry);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                dtTP = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                iMonth = dtTP.Month;
                iYear = dtTP.Year;
                sDate = getActualTPDate(iMonth, iYear);
                string[] TP_Month_Year;
                TP_Month_Year = sDate.Split('~');
                iMonth = Convert.ToInt32(TP_Month_Year[0]);
                iYear = Convert.ToInt32(TP_Month_Year[1]);
                string curDate = iMonth.ToString() + "-01-" + iYear.ToString();

                strQry = "select '" + curDate + "' ";

                dsTP = db_ER.Exec_DataSet(strQry);
            }

            return dsTP;
        }

         public DataSet get_TourPlan_Details(string sf_code, string sTP)
         {
             DB_EReporting db_ER = new DB_EReporting();
             DataSet dsTP = null;
             DateTime dtTP = Convert.ToDateTime(sTP);
             string sReturn = string.Empty;

             strQry = " select Tour_Schedule1,Worked_With_SF_Code,objective,WorkType_Code_B,Worked_With_SF_Name from Trans_TP_One  " +
                      " where sf_code = '" + sf_code + "' and  Tour_Month =  " + dtTP.Month + " and Tour_Year = " + dtTP.Year + " and DAY(tour_date) = " + dtTP.Day + " ";
                    

             dsTP = db_ER.Exec_DataSet(strQry);

             return dsTP;
         }


        public string getActualTPDate(int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iDays;
            int iCount;
            string sReturn = string.Empty;

            iDays = getLastDay(iMonth, iYear);

            strQry = "select count(Tour_Month) from Trans_TP where Tour_Month = " + iMonth + " and Tour_Year = " + iYear;
            iCount = db_ER.Exec_Scalar(strQry);

            if (iDays == iCount)
            {
                //Next Month
                if (iMonth == 12)
                {
                    iMonth = 1;
                    iYear = iYear + 1;
                }
                else
                {
                    iMonth = iMonth + 1;
                }
                sReturn = getActualTPDate(iMonth, iYear);
            }
            else
            {
                sReturn = iMonth.ToString() + "~" + iYear.ToString();
            }
            return sReturn;
        }

        public int getLastDay(int iMonth, int iYear)
        {
            int iDay = -1;

            if (iMonth == 1)
            {
                iDay = 31;
            }
            else if (iMonth == 2)
            {
                iDay = 28;
                //if ((iYear % 4)==0)
                //{
                //    iDay = 29;
                //}
                //else
                //{
                //    iDay = 28;
                //}
            }
            else if (iMonth == 3)
            {
                iDay = 31;
            }
            else if (iMonth == 4)
            {
                iDay = 30;
            }
            else if (iMonth == 5)
            {
                iDay = 31;
            }
            else if (iMonth == 6)
            {
                iDay = 30;
            }
            else if (iMonth == 7)
            {
                iDay = 31;
            }
            else if (iMonth == 8)
            {
                iDay = 31;
            }
            else if (iMonth == 9)
            {
                iDay = 30;
            }
            else if (iMonth == 10)
            {
                iDay = 31;
            }
            else if (iMonth == 11)
            {
                iDay = 30;
            }
            else if (iMonth == 12)
            {
                iDay = 31;
            }
            return iDay;
        }

        public DataSet get_TP_Active_Date(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select tour_date as tour_date from Trans_TP_One" +
                     " where SF_Code = '" + sf_code + "' and (Change_Status=1 or Change_Status=2)" +
                     " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " " +
                     " union all " +
                     " select tour_date as tour_date from Trans_TP" +
                     " where SF_Code = '" + sf_code + "' and (Change_Status=1 or Change_Status=2)" +
                     " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " " +
                     "  order by tour_date";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count != 0)
            {
                //Do Nothing
            }

            else
            {
                strQry = "SELECT sf_TP_Active_Dt as tour_date from mas_salesforce where sf_code = '" + sf_code + "' ";

                try
                {
                    dsListedDR = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dsListedDR;
        }


       public DataSet GetTPOptionDate(string sf_code, string ddlMonth, string ddlYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select Selected_Date from Options_TP_Edit where SF_Code='" + sf_code + "' " +
                     " and Tour_Month='" + ddlMonth + "' and Tour_Year='" + ddlYear + "' ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet GetTPEdit(string sf_code, string ddlMonth, string ddlYear, string lblDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            string strDate = lblDate.Substring(3, 2) + "-" + lblDate.Substring(0, 2) + "-" + lblDate.Substring(6, 4);

            strQry = " select Selected_Date from Options_TP_Edit where SF_Code='" + sf_code + "' " +
                     " and Tour_Month='" + ddlMonth + "' and Tour_Year='" + ddlYear + "' and (Selected_Date like '%" + strDate + "%' or Selected_Date like '%ALL%') order by created_dtm desc ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }


        public DataSet FetchWorkType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as WorkType_Code_B,'---Select---' as WorkType_Name_B " +
                         " UNION" +
                     " SELECT WorkType_Code_B,WorkType_Name_B FROM Mas_WorkType_BaseLevel where active_flag=0 and TP_DCR like '%T%'";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }


        public DataSet FetchTerritory(string sf_code, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' ";

            /* " UNION " +
             " SELECT WorkType_Code_B as Territory_Code,Worktype_Name_B as Territory_Name " +
             " FROM Mas_WorkType_BaseLevel where active_flag=0 ";*/
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet FetchTerritory_MGR(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Code in (" + terr_code + ")";

            /* " UNION " +
             " SELECT WorkType_Code_B as Territory_Code,Worktype_Name_B as Territory_Name " +
             " FROM Mas_WorkType_BaseLevel where active_flag=0 ";*/
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public string FetchTerritoryCode(string terr_code,string Sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            string sReturn = string.Empty;

            strQry = " SELECT Territory_Code " +
                     " FROM  Mas_Territory_Creation where Territory_Name='" + terr_code + "' and sf_code in('"+ Sf_code +"') " +
                     " AND territory_active_flag=0 ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    sReturn = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sReturn;
        }

        public string FetchTerritoryName(string terr_code,string Sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            string sReturn = string.Empty;

            strQry = " SELECT Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Name='" + terr_code + "' and sf_code in('"+ Sf_code+"') " +
                     " AND territory_active_flag=0 ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    sReturn = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sReturn;
        }

        public string FetchSFCode(string SF_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            string sReturn = string.Empty;

            strQry = " SELECT SF_Code FROM  Mas_SalesForce where SF_Name='" + SF_Name + "' ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    sReturn = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sReturn;
        }



         public DataSet FetchSFName(string terr_code)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsTerr = null;


             strQry = " SELECT SF_Name " +
                      " FROM  Mas_SalesForce where Sf_Code='" + terr_code + "' " +
                      " AND sf_TP_Active_Flag =0 ";

             try
             {
                 dsTerr = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsTerr;
         }

        public DataSet getTerritoryName(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;


            strQry = " SELECT Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Code='" + terr_code + "' " +
                     " AND territory_active_flag=0 ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return dsTerr;
        }

        public DataSet getHolidays_TP(string sf_code, int iMonth, int iDay, int iYear, string div_code, string state_code)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsHoliday = null;

             strQry = "SELECT convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name" +
                     " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
                     " WHERE a.Division_Code = '" + div_code + "' AND (a.State_Code like '" + state_code + ',' + "%'  or " +
                     " a.State_Code like '%" + ',' + state_code + "' or a.State_Code like '%" + ',' + state_code + ',' + "%') " +
                     " and a.Holiday_Date = '" + iMonth + "-" + iDay + "-" + iYear + "'  " +
                     " ORDER BY 1";

             try
             {
                 dsHoliday = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsHoliday;
         }

        public DataSet TPFetchWorktype(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select Tour_Schedule1,Tour_Schedule2,Tour_Schedule3 from Trans_TP_One where Tour_Schedule1='" + terr_code + "' ";

            /* " UNION " +
             " SELECT WorkType_Code_B as Territory_Code,Worktype_Name_B as Territory_Name " +
             " FROM Mas_WorkType_BaseLevel where active_flag=0 ";*/
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet FetchWorkType(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Worktype_Name_B " +
                     " FROM  Mas_WorkType_BaseLevel where WorkType_Code_B='" + terr_code + "' ";

            /* " UNION " +
             " SELECT WorkType_Code_B as Territory_Code,Worktype_Name_B as Territory_Name " +
             " FROM Mas_WorkType_BaseLevel where active_flag=0 ";*/
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
        //16Jul - Sridevi - New function for Calender - TP
        public DataSet FetchTerritory_Chkbox(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";

            //strQry = " SELECT '0' as Territory_Code,'---Select---' as Territory_Name " +
            //           " UNION " +
            //           " SELECT left(replace(d.Territory_Code,'~',','),datalength(replace(d.Territory_Code,'~',','))-1)Territory_Code, " +
            //           " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code  and  " +
            //           " CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0  for XML path('')),1,2,'') " +
            //           " territory_Name  FROM Mas_ListedDr d WHERE d.Sf_Code =  '" + sf_code + "' and d.ListedDr_Active_Flag = 0 ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet FetchTerritory_TourSchedule(string Territory_Name, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Name = '" + Territory_Name + "' and sf_code='" + sf_code + "' AND territory_active_flag=0 ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        //16Jul - ClearTPCalender
        public int ClearTPCalender(string MR_Code, string MR_Month, string MR_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_TP_One" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //16 July
         public DataSet get_TourPlan_MGRWorkType(string WT_Code)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsTP = null;

             strQry = "select Worktype_Name_M from Mas_WorkType_Mgr Where WorkType_Code_M = '" + WT_Code + "' ";
             try
             {
                 dsTP = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsTP;
         }

        public DataSet get_TourPlan_MGRSF_Name(string WT_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select Sf_Name from  Mas_Salesforce where Sf_Code = '" + WT_Code + "' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_UserList_TP_Report(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC sp_UserList_TP_Report '" + div_code + "', '" + sf_code + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }

        public DataSet get_UserList_TP_Report_Level(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC sp_UserList_TP_Report_Level '" + div_code + "', '" + sf_code + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }

        public DataSet get_TP_Detail(string sf_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC sp_get_tp_detail '" + sf_code + "', " + cmonth + ", " + cyear + " ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }

        public DataSet get_TP_Entry_Confirm(string sf_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "select isnull(convert(char(10),submission_date,103),'0') submission_date,isnull(CONVERT(char(10),Confirmed_Date,103),'0') Confirmed_Date from Trans_TP " +
                      " where tour_month=" + cmonth + " and tour_year = " + cyear +
                      " and Change_Status=2 and Confirmed=1 and SF_Code='" + sf_code + "'" +
                      " union all " +
                      "select isnull(convert(char(10),submission_date,103),'0') submission_date,isnull(CONVERT(char(10),Confirmed_Date,103),'0') Confirmed_Date from Trans_TP_One  " +
                      "where tour_month=" + cmonth + " and tour_year =" + cyear +
                      "and Change_Status=1 and Confirmed=0 and SF_Code='" + sf_code + "'";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }


        public DataSet FillTourPlan(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_tp  '" + div_code + "', '" + sf_code + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        //public int get_TP_Count_FieldForce(string sf_code, string sMonth, string sYear, string ddlDay)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    int iReturn = -1;
        //    strQry = "select COUNT(tour_month) from Trans_TP " +
        //             " WHERE sf_code = '" + sf_code + "' " +
        //             " and confirmed = 1 " +
        //             " and tour_month = '" + sMonth + "'  " +
        //             " and tour_year = '" + sYear + "' and Tour_Date='" + ddlDay + "' ";

        //    try
        //    {
        //        iReturn = db_ER.Exec_Scalar(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}

        public DataSet FillFutureTourPlan(string sf_code, string sMonth, string sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            DateTime dtTP;
            string sTP = sMonth + "-01-" + sYear;
            dtTP = Convert.ToDateTime(sTP);

            strQry = "select distinct Tour_Month, Tour_Year from Trans_TP_One " +
                     " WHERE sf_code = '" + sf_code + "' " +
                     " and confirmed = 1 " +
                     " and tour_date >= '" + dtTP + "'  ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public int DeleteTP(string SF_Code, string sMonth, string sYear)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "delete from Trans_TP_One " +
                            " where SF_Code='" + SF_Code + "' and Tour_Month=" + Convert.ToInt32(sMonth) + " and " +
                            " Tour_Year=" + Convert.ToInt32(sYear) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Change done by saravanan 23/10/14
        public DataSet Get_TP_Edit_Year(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        //Change done by saravanan 23/10/14
        public DataSet Get_TP_Edit_Month(string div_code, string strMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select distinct top(3)Tour_Month from Trans_TP WHERE sf_code = '" + strMonth + "' and confirmed = 1 order by Tour_Month desc";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet Get_TP_Entry_Edit_Year(string Division_Code, string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select distinct Tour_Month,Tour_Year from Trans_TP where isnull(Confirmed,'')=0 " +
                     "and Division_Code='" + Division_Code + "' and SF_Code='" + SF_Code + "' " +
                     "union all " +
                     "select distinct Tour_Month,Tour_Year from Trans_TP_One where isnull(Confirmed,'')=0 " +
                     "and Division_Code='" + Division_Code + "' and SF_Code='" + SF_Code + "' order by Tour_Month asc ";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public int TP_Edit_RecordAdd(string sf_code, string smonth, string syear, string sdate)
        {
            int iReturn = -1;
            int iCount = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(*) from Options_TP_Edit where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "' and Tour_Year='" + syear + "' ";

                iCount = db.Exec_Scalar(strQry);

                //strQry = " Insert into Trans_TP_One(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                //         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                //         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name) " +
                //         " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                //         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                //         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name from trans_tp " +
                //         " where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "'";

                //iReturn = db.ExecQry(strQry);


                if (iCount <= 0)
                {
                    //if (sdate != "All")
                    //{
                    strQry = "insert into Options_TP_Edit " +
                          " VALUES('" + sf_code + "', '" + smonth + "', '" + syear + "', '" + sdate + "',getdate()) ";

                    iReturn = db.ExecQry(strQry);
                    //}

                    strQry = "update Trans_TP set Confirmed='0',Change_Status=1 where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "' and Tour_Year='" + syear + "'";

                    iReturn = db.ExecQry(strQry);

                    //if (sdate == "ALL")
                    //{
                    //    strQry = " Insert into Options_TP_Edit(sf_code,tour_month,tour_year,selected_date) " +
                    //            " Select SF_Code,Tour_Month,Tour_Year,tour_date from Trans_TP SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "' and Tour_Year='" + syear + "'";
                    //}

                }
                else
                {
                    strQry = " update Options_TP_Edit set Selected_Date='" + sdate + "',created_dtm=getdate() " +
                             " where SF_Code='" + sf_code + "' and Tour_Month ='" + smonth + "'  " +
                             " and Tour_Year='" + syear + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP set Confirmed='0',Change_Status=1 where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "' and Tour_Year='" + syear + "'";

                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet FillTourPlan_Delete(string sf_code, string div_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_tp_delete  '" + div_code + "', '" + sf_code + "', " + cmonth + ", " + cyear;

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet get_TP_Count_FieldForce(string sf_code, string sMonth, string sYear, string Day)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTP = null;
            int iReturn = -1;

            strQry = "select COUNT(tour_month) from Trans_TP " +
                     " WHERE sf_code = '" + sf_code + "' " +
                     " and confirmed = 1 " +
                     " and tour_month = '" + sMonth + "'  " +
                     " and tour_year = '" + sYear + "' and DAY(Tour_Date)='" + Day + "' " +
                     " union all " +
                     " select COUNT(tour_month) from Trans_TP_One " +
                     " WHERE sf_code = '" + sf_code + "' " +
                     " and confirmed = 1 " +
                     " and tour_month = '" + sMonth + "'  " +
                     " and tour_year = '" + sYear + "' and DAY(Tour_Date)='" + Day + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        //Saravanan Changes
        public DataSet GetTPEditConfirmed_Date(string Tour_Month, string Tour_Year, string Sf_code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;
            //if (!RecordExist(div_sname))
            //{
            try
            {
                strQry = "select count(Confirmed_Date) Confirmed_Date from Trans_TP where Tour_Month='" + Tour_Month + "' and Tour_Year='" + Tour_Year + "' and sf_code='" + Sf_code + "'";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return dsTP;
        }

       
        //Changes done by Saravana
        public DataSet GetExpenseMGRWorkType()
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "select Worktype_Name_M Worktype_Name_B,Expense_Type from Mas_WorkType_Mgr";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        public DataSet GetExpenseBaseWorkType()
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "select Worktype_Name_B,Expense_Type from Mas_WorkType_BaseLevel where ExpNeed=1";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        //Changes done by Saravanan
        public DataSet getWorkType_Select(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' WorkType_Code_M,'---Select---' Worktype_Name_M,'' WType_SName " +
                     " union " +
                     " select WorkType_Code_M,Worktype_Name_M,WType_SName from Mas_WorkType_Mgr where Division_Code='" + Div_Code + "' " +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public int RecordAddMGR_TPWeekOff(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();

                int Division_Code = -1;
                int iCount = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry =    " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " + " " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                            " Worked_With_SF_Code,Objective,Tour_Schedule1,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Worked_With_SF_Name) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "', " +
                            "'" + TP_Terr + "','" + TP_Objective + "','" + WorkType_Name + "','" + TP_WW + "',  '" + worktype + "', '" + Division_Code + "', 0,getdate(),'" + TP_Terr_Code + "','" + TP_WWName + "') ";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    
                    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                 
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }

        // Changes done by Saravanan
         public DataSet get_TP_ApprovalStatus(string sf_code, string Month, string Year)
         {
             DB_EReporting db_ER = new DB_EReporting();
             DataSet dsTP = null;

             //strQry = " select distinct TP.sf_code,case TP.confirmed " +
             //         " when '0' then 'Prepared & Not Completed' " +
             //         " when '1' then 'Completed'" +
             //         "  end as [Status]  from Trans_TP_One TP,Mas_Salesforce S " +
             //         " where TP.SF_Code='" + sf_code + "' AND TP.SF_Code=S.Sf_Code and s.sf_TP_Active_Flag=0  and Tour_Month='" + Month + "' " +
             //         " union all " +
             //         " select distinct TP.sf_code,case TP.confirmed " +
             //         " when '1' then 'Completed' " +
             //         " when '2' then 'Rejected'" +
             //         "  end as [Status]  from Trans_TP TP,Mas_Salesforce S " +
             //         " where TP.SF_Code='" + sf_code + "' AND TP.SF_Code=S.Sf_Code and s.sf_TP_Active_Flag=0 and Tour_Month='" + Month + "' ";

             strQry = " select distinct TP.sf_code,case TP.Change_Status " +
                    " when '1' then 'Prepard & Not Approved' " +
                    " when '2' then 'Rejected'" +
                    "  end as [Status]  from Trans_TP_one TP,Mas_Salesforce S " +
                    " where TP.SF_Code='" + sf_code + "' AND TP.SF_Code=S.Sf_Code and s.sf_TP_Active_Flag=0 and Tour_Month='" + Month + "' " +
                    "Union All" +
                     " select distinct TP.sf_code,case TP.Change_Status " +
                    " when '1' then 'Completed' " +
                    " when '2' then 'Rejected'" +
                    "  end as [Status]  from Trans_TP TP,Mas_Salesforce S " +
                    " where TP.SF_Code='" + sf_code + "' AND TP.SF_Code=S.Sf_Code and s.sf_TP_Active_Flag=0 and Tour_Month='" + Month + "' ";


             try
             {
                 dsTP = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsTP;
         }

       
        //Changes done by Saravanan
        public DataSet get_TP_MR(string sf_code, string Worktype, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select " +
                     " case Tour_Schedule1 when convert(varchar(50),0) then '' else Tour_Schedule1 end Tour_Schedule1," +
                     " case Tour_Schedule2 when convert(varchar(50),0)then '' else Tour_Schedule2 end Tour_Schedule2," +
                     " case Tour_Schedule3 when convert(varchar(50),0) then '' else Tour_Schedule3 end Tour_Schedule3" +
                     " from Trans_TP where Worktype_Name_B='" + Worktype + "' and SF_Code='" + sf_code + "' and convert(char(10),Tour_Date,103)='" + Date + "'"+
                     " union "+
                      " select " +
                     " case Tour_Schedule1 when convert(varchar(50),0) then '' else Tour_Schedule1 end Tour_Schedule1," +
                     " case Tour_Schedule2 when convert(varchar(50),0)then '' else Tour_Schedule2 end Tour_Schedule2," +
                     " case Tour_Schedule3 when convert(varchar(50),0) then '' else Tour_Schedule3 end Tour_Schedule3" +
                     " from Trans_TP_one where Worktype_Name_B='" + Worktype + "' and SF_Code='" + sf_code + "' and convert(char(10),Tour_Date,103)='" + Date + "'";


            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        //Changes done by Saravanan

        public DataSet get_TP_Rejected_Approval(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = "select distinct a.sf_code,b.Rejection_Reason, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
                        " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
                        "'Send to Manager Approval '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month],b.TP_Approval_MGR" +
                        " from Mas_Salesforce a, Trans_TP b " +
                        " where a.sf_code = b.sf_code and a.Sf_Code  = '" + sf_code + "' and b.Change_Status=0 and b.confirmed=0 and isnull(b.Rejection_Reason,'')!=''" +
                        " union all " +
                        "select distinct a.sf_code,b.Rejection_Reason, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
                        " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
                        "'Send to Manager Approval '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month],b.TP_Approval_MGR" +
                        " from Mas_Salesforce a, Trans_TP_One b " +
                        " where a.sf_code = b.sf_code and a.Sf_Code  = '" + sf_code + "' and b.Change_Status=0 and b.confirmed=0 and isnull(b.Rejection_Reason,'')!=''";


            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        //Needed - to check later
        public DataSet Get_TP_Start_Title(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select SF_Code from Trans_TP where SF_Code='" + sfcode + "' ";

            dsListedDR = db_ER.Exec_DataSet(strQry);



            return dsListedDR;
        }
        public DataSet get_TP_Edit_MonthDDL(string sf_code, string strYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select Tour_Month from Trans_TP_One where SF_Code='" + sf_code + "' and Tour_Year='" + strYear + "' " +
                     " union " +
                     " select Tour_Month from Trans_TP where SF_Code='" + sf_code + "' and Tour_Year='" + strYear + "'";


            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        //Changes done by Saravanan
        public DataSet Get_HOID_TP_Edit_Year(string sf_Type, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            if (sf_Type == "3" && div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
                strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code in(" + div_code + ")";
            }
            else
            {
                strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
            }

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsAdmin;
        }
      
        public DataSet FetchWorkCode_New(string WorkType_Code, string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Place_Involved from Mas_WorkType_Mgr where WorkType_Code_M='" + WorkType_Code + "' and Division_Code='" + Division_Code + "'";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet FetchTerritory_Active_Flag(string sf_code, string Territory_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            int iCount;

            strQry = " select Territory_Active_Flag from Mas_Territory_Creation where SF_Code='" + sf_code + "' " +
                     " and Territory_Code='" + Territory_Code + "' ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }




        public DataSet FetchTerritory_Self(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Division_Code='" + Div_Code + "' and territory_active_flag=0 ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet GetTPHolidayData(string sf_Code, string Division_Code, string Tour_Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            DateTime dt = Convert.ToDateTime(Tour_Date);

            strQry = "select Tour_Schedule1 from Trans_TP_One where SF_Code='" + sf_Code + "' and Division_Code='" + Division_Code + "' " +
                     " and day(Tour_Date)='" + dt.Day + "' and month(Tour_Date)='" + dt.Month + "' and year(Tour_Date)='" + dt.Year + "'";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet Tp_get_WorkType(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select Place_Involved, WorkType_Code_B as WorkType_Code,FieldWork_Indicator from Mas_WorkType_BaseLevel Where division_code = '" + div_code + "' ";


            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Active_Date_New_Index(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            DataSet dsListedMasSales = null;

            strQry = "select top 1 convert(char(2),DAY(Tour_Date),103)+'-' + substring(convert(char(7),Tour_Date,120),6,2)+'-'+ " +
                    "substring(convert(char(7),Tour_Date,120),0,5)+' 00:00:00.000' AS Tour_Date " +
                    " from trans_tp_one  where SF_Code = '" + sf_code + "' and Confirmed=0 and (Change_Status=1 or Change_Status=2) ";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count == 0)
            {
                strQry = "select top 1 convert(char(2),DAY(Last_TP_Date),103)+'-' + substring(convert(char(7),Last_TP_Date,120),6,2)+'-'+ " +
                        "substring(convert(char(7),Last_TP_Date,120),0,5)+' 00:00:00.000' AS Last_TP_Date " +
                        " from Mas_Salesforce  where SF_Code = '" + sf_code + "' ";


                dsListedDR = db_ER.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count > 0)
                {
                    // Do Nothing
                }
                else
                {

                    strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

                    try
                    {
                        dsListedDR = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                strQry = "select '01-01-1900' ";

                dsListedDR = db_ER.Exec_DataSet(strQry);
            }

            return dsListedDR;
        }

        public int update_tpdate_tpvalidation(string sf_code, DateTime Last_TP_Date)
        {
            int iReturn = -1;
            int iCount = -1;
            string Last_TP_Date2 = Last_TP_Date.Month + "-" + Last_TP_Date.Day + "-" + Last_TP_Date.Year;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " update Mas_Salesforce set Last_TP_Date ='" + Last_TP_Date2 + "' where Sf_Code='" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);


                strQry = " update mas_salesforce_DCRTPdate set Last_TP_Date ='" + Last_TP_Date2 + "' where Sf_Code='" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public bool IsTpChk_NextMonth(string sf_code, string div_code, int month, int year)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select COUNT(SF_Code) from Trans_TP where SF_Code='" + sf_code + "' and Division_Code='" + div_code + "' and Tour_Month ='" + month + "'  and Tour_Year ='" + year + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
                else
                {
                    strQry = " select COUNT(SF_Code) from Trans_TP_One where SF_Code='" + sf_code + "' and Division_Code='" + div_code + "' and Tour_Month ='" + month + "'  and Tour_Year ='" + year + "' and Change_Status !=0 and Confirmed=0";
                    int iRecordExist1 = db.Exec_Scalar(strQry);
                    if (iRecordExist1 > 0)
                        bRecordExist = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int insertDelete_tpdate_tpvalidation(string sf_code, string div_code, DateTime current_date)
        {
            int iReturn = -1;
            int iCount = -1;

            string current_date2 = current_date.Month + "-" + current_date.Day + "-" + current_date.Year;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC InsertDelete_tpdate_tpvalidation '" + sf_code + "', '" + div_code + "','" + current_date2 + "' ";

                iReturn = db.ExecQry(strQry);

                //strQry = "Insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                //         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                //         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name, " +
                //         " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR) " +
                //         " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                //         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,'1', " +
                //         " GETDATE() as Confirmed_Date,'Auto Approved by admin','1',Territory_Code1,Territory_Code2, " +
                //         " Territory_Code3,Worked_With_SF_Name, " +
                //         " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR from Trans_TP_One " +
                //         " where SF_Code='" + sf_code + "' and Tour_Date <='" + current_date2 + "' " +
                //         " and Division_Code='" + div_code + "' and Change_Status !=0 and Confirmed=0 ";

                //iReturn = db.ExecQry(strQry);

                //if (iReturn > 0)
                //{
                //    strQry = " delete from Trans_TP_One where SF_Code ='" + sf_code + "' and Division_Code ='" + div_code + "' and Tour_Date <='" + current_date2 + "' " +
                //            " and Change_Status !=0 and Confirmed=0 ";

                //    iReturn = db.ExecQry(strQry);
                //}

                //strQry = " delete from Trans_TP_One where SF_Code ='" + sf_code + "' and Division_Code ='" + div_code + "' and Tour_Date <='" + current_date2 + "' " +
                //       "   and Change_Status =0 and Confirmed=0 ";

                //iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_lastTPdate_forTpValida(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select convert(varchar,Last_TP_Date,103) Last_DCR_Date from mas_salesforce_DCRTPdate where Sf_Code ='" + sf_code + "'  ";


            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet IsTpChk_Rejection(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select COUNT(SF_Code) Days,tour_Month,Tour_Year,Tp_Approval_MGR from Trans_TP_One where SF_Code='" + sf_code + "' and Division_Code='" + div_code + "' " +
                     " and Confirmed=0 and Change_Status=2 group by tour_Month,Tour_Year,Tp_Approval_MGR ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public DataSet Tp_NewfillDay_Edit(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = " select CONVERT(varchar,Tour_Date,103) as date,DATENAME(dw,Tour_Date) as day_name,'' Holiday,'' weekoff,'' Field_Work,Objective,Territory_Code1 " +
                       "  as territory_code,WorkType_Code_B,Tour_Schedule2 as days_check " +
                      " from Trans_TP_One where SF_Code='" + sf_code + "' and month(Tour_Date)='" + month + "' and year(Tour_Date)='" + year + "' and confirmed=0 and change_status=1 order by Tour_Date  ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataSet last_Tp_Datecheck_Day_draft(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = " select CONVERT(varchar,Tour_Date,103) as date,DATENAME(dw,Tour_Date) as day_name,'' Holiday,'' weekoff,'' Field_Work,Objective,Territory_Code1 " +
                       "  as territory_code,WorkType_Code_B,Tour_Schedule2 as days_check,confirmed,change_status,Rejection_Reason " +
                      " from Trans_TP_One where SF_Code='" + sf_code + "' and month(Tour_Date)='" + month + "' and year(Tour_Date)='" + year + "'  order by Tour_Date ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public int Tp_Modify_Apprv_Daywise(string TP_Date, string worktype_Code, string worktype_Name, string TP_Objective, string sf_code, string SF_Name, string div_code, string dayy, string dayyy_name, string TP_Approval_MGR,string daydd)
        {
            int iReturn = -1;
            string terr_codeee = string.Empty;
            string terr_nameee = string.Empty;

            try
            {



                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsTp = new DataSet();
                DataSet dsTerr = new DataSet();

                strQry = " select Tour_Month from Trans_TP " +
                         " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + div_code + "' ";


                dsTp = db.Exec_DataSet(strQry);

                if (dsTp.Tables[0].Rows.Count == 0)
                {


                    if (dayy != "0")
                    {

                        strQry = "select  (select  STUFF((Select '/'+ cast(a.Territory_Code as varchar(max))   from Mas_Territory_Creation a " +
                        " where Territory_Active_Flag=0 and Territory_Code in ( select distinct Territory_Code from Mas_ListedDr where SF_Code='" + sf_code + "' and ListedDr_Active_Flag=0 " +
                        " and (day_1='" + daydd + "' or day_2='" + daydd + "' or day_3='" + daydd + "') ) " +
                        " FOR XML PATH('')),1,1,'') ) as terr_code, " +
                        " (select  STUFF((Select '/'+ cast(a.Territory_Name as varchar(max))   from Mas_Territory_Creation a " +
                        " where Territory_Active_Flag=0 and Territory_Code in " +
                        " ( select distinct Territory_Code from Mas_ListedDr where SF_Code='" + sf_code + "' and ListedDr_Active_Flag=0  " +
                        " and (day_1='" + daydd + "' or day_2='" + daydd + "' or day_3='" + daydd + "') ) " +
                        " FOR XML PATH('')),1,1,'') ) as terr_name ";

                        dsTerr = db.Exec_DataSet(strQry);

                        if (dsTerr.Tables[0].Rows.Count > 0)
                        {
                            terr_codeee = dsTerr.Tables[0].Rows[0]["terr_code"].ToString();
                            terr_nameee = dayyy_name + " ( " + dsTerr.Tables[0].Rows[0]["terr_name"].ToString() + " )";
                        }

                    }



                    strQry = "Update Trans_TP_One set Tour_Schedule1='" + terr_nameee + "' ," +
                             "Territory_Code1='" + terr_codeee + "', " +
                              "Objective='" + TP_Objective + "', " +
                             "WorkType_Code_B ='" + worktype_Code + "' ," +
                             "Worktype_Name_B ='" + worktype_Name + "' ," +
                             "submission_date =getdate() ," +
                             "TP_Sf_Name='" + SF_Name + "', " +
                             "Tour_Schedule2 ='" + dayy + "', " +
                             " TP_Approval_MGR='" + TP_Approval_MGR + "' " +
                             " where sf_code='" + sf_code + "' and Tour_Month='" + dt_TourPlan.Month + "' and Tour_Year='" + dt_TourPlan.Year + "' " +
                             " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' and Confirmed=0 and Change_Status=1 ";

                    iReturn = db.ExecQry(strQry);

                    //strQry = " Insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                    //    " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                    //    " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name," +
                    //    " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR) " +
                    //    " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                    //    " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,'1', " +
                    //    " GETDATE() as Confirmed_Date,Rejection_Reason,'1',Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name, " +
                    //    " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR from Trans_TP_One " +
                    //    " where SF_Code='" + sf_code + "' and Tour_Month='" + dt_TourPlan.Month + "' and Tour_Year='" + dt_TourPlan.Year + "' and Tour_Date='" + strTPdt + "' and Confirmed=0 and Change_Status=1 ";

                    //iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int TP_Apprv_Day_Update(string MR_Code, string Tour_Month, string Tour_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsTPdate = new DataSet();

                strQry = "EXEC TP_Apprv_Day_Update '" + MR_Code + "', '" + Tour_Month + "','" + Tour_Year + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Reject_DaywiseTp(string sf_code, string Tour_Month, string Tour_Year, string RejectReason, string Sf_Name)
        {
            int iReturn = -1;
            int iCount = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                //  strQry = "Update Trans_TP_One set Change_Status='2', TP_Approval_MGR='" + Sf_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + sf_code + "' and Tour_Month='" + Tour_Month + "' and Tour_Year = '" + Tour_Year + "' ";

                strQry = "EXEC Reject_DaywiseTp '" + sf_code + "', '" + Tour_Month + "','" + Tour_Year + "','" + Sf_Name + "','" + RejectReason + "' ";
                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet last_Tp_Datecheck(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            DataSet dsTrans_tp_one = null;


            DataSet dsTrans_tp = null;

            strQry = " select last_tp_date,month(last_tp_date) as month, year(last_tp_date) as year from mas_salesforce_dcrtpdate" +
                     " where SF_Code = '" + sf_code + "' ";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            strQry = " select tour_date,month(tour_date) as month, year(tour_date) as year from trans_tp_one" +
                       " where SF_Code = '" + sf_code + "' and month(tour_date)='" + dsListedDR.Tables[0].Rows[0]["month"].ToString() + "' and year(tour_date)='" + dsListedDR.Tables[0].Rows[0]["year"].ToString() + "' and confirmed=0 and change_status=1 order by Tour_Date";


            // dsTrans_tp_one = db_ER.Exec_DataSet(strQry);


            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public int Tp_Insert_Daywise(string TP_Date, string worktype_Code, string worktype_Name, string TP_Objective, string sf_code, string SF_Name, string div_code, string mode, string dayy, string dayyy_name, string TP_Approval_MGR,string daydd)
        {
            int iReturn = -1;

            int Change_Status = 0;
            int Confirmed = 0;
            string terr_codeee = string.Empty;
            string terr_nameee = string.Empty;

            try
            {

                if (mode == "submit")
                {
                    Change_Status = 1;
                    Confirmed = 0;

                }
                else if (mode == "draft")
                {
                    Change_Status = 0;
                    Confirmed = 0;
                }

                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsTp = new DataSet();
                DataSet dsTerr = new DataSet();

                strQry = " select Tour_Month from Trans_TP_One " +
                         " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + div_code + "' ";


                dsTp = db.Exec_DataSet(strQry);



                if (dsTp.Tables[0].Rows.Count == 0)
                {

                    if (dayy != "0")
                    {

                        strQry = "select  (select  STUFF((Select '/'+ cast(a.Territory_Code as varchar(max))   from Mas_Territory_Creation a " +
                        " where Territory_Active_Flag=0 and Territory_Code in ( select distinct Territory_Code from Mas_ListedDr where SF_Code='" + sf_code + "' and ListedDr_Active_Flag=0 " +
                        " and (day_1='" + daydd + "' or day_2='" + daydd + "' or day_3='" + daydd + "') ) " +
                        " FOR XML PATH('')),1,1,'') ) as terr_code, " +
                        " (select  STUFF((Select '/'+ cast(a.Territory_Name as varchar(max))   from Mas_Territory_Creation a " +
                        " where Territory_Active_Flag=0 and Territory_Code in " +
                        " ( select distinct Territory_Code from Mas_ListedDr where SF_Code='" + sf_code + "' and ListedDr_Active_Flag=0  " +
                        " and (day_1='" + daydd + "' or day_2='" + daydd + "' or day_3='" + daydd + "') ) " +
                        " FOR XML PATH('')),1,1,'') ) as terr_name ";

                        dsTerr = db.Exec_DataSet(strQry);

                        if (dsTerr.Tables[0].Rows.Count > 0)
                        {
                            terr_codeee = dsTerr.Tables[0].Rows[0]["terr_code"].ToString();
                            terr_nameee = dayyy_name + " ( " + dsTerr.Tables[0].Rows[0]["terr_name"].ToString() + " )";
                        }
                    }


                    strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Territory_Code1, " +
                            " Objective,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Confirmed,TP_Sf_Name,Tour_Schedule2,TP_Approval_MGR) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                            " '" + terr_nameee + "','" + terr_codeee + "','" + TP_Objective + "','" + worktype_Code + "', '" + worktype_Name + "', " +
                            " '" + div_code + "','" + Change_Status + "', getdate(),'" + Confirmed + "','" + SF_Name + "','" + dayy + "','" + TP_Approval_MGR + "') ";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    if (dayy != "0")
                    {

                        strQry = "select  (select  STUFF((Select '/'+ cast(a.Territory_Code as varchar(max))   from Mas_Territory_Creation a " +
                        " where Territory_Active_Flag=0 and Territory_Code in ( select distinct Territory_Code from Mas_ListedDr where SF_Code='" + sf_code + "' and ListedDr_Active_Flag=0 " +
                        " and (day_1='" + daydd + "' or day_2='" + daydd + "' or day_3='" + daydd + "' )) " +
                        " FOR XML PATH('')),1,1,'') ) as terr_code, " +
                        " (select  STUFF((Select '/'+ cast(a.Territory_Name as varchar(max))   from Mas_Territory_Creation a " +
                        " where Territory_Active_Flag=0 and Territory_Code in " +
                        " ( select distinct Territory_Code from Mas_ListedDr where SF_Code='" + sf_code + "' and ListedDr_Active_Flag=0  " +
                        " and (day_1='" + daydd + "' or day_2='" + daydd + "' or day_3='" + daydd + "') ) " +
                        " FOR XML PATH('')),1,1,'') ) as terr_name ";

                        dsTerr = db.Exec_DataSet(strQry);

                        if (dsTerr.Tables[0].Rows.Count > 0)
                        {
                            terr_codeee = dsTerr.Tables[0].Rows[0]["terr_code"].ToString();
                            terr_nameee = dayyy_name + " ( " + dsTerr.Tables[0].Rows[0]["terr_name"].ToString() + " )";
                        }

                    }



                    strQry = "Update Trans_TP_One set Tour_Schedule1='" + terr_nameee + "' ," +
                             "Territory_Code1='" + terr_codeee + "', " +
                              "Objective='" + TP_Objective + "', " +
                             "WorkType_Code_B ='" + worktype_Code + "' ," +
                             "Worktype_Name_B ='" + worktype_Name + "' ," +
                             "Change_Status ='" + Change_Status + "' ," +
                             "submission_date =getdate() ," +
                             "Confirmed ='" + Confirmed + "' ," +
                             "TP_Sf_Name='" + SF_Name + "', " +
                             "Tour_Schedule2 ='" + dayy + "', " +
                             " TP_Approval_MGR='" + TP_Approval_MGR + "' " +
                             " where sf_code='" + sf_code + "' and Tour_Month='" + dt_TourPlan.Month + "' and Tour_Year='" + dt_TourPlan.Year + "' " +
                             " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' ";


                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet CCp_View(string month, string year)
        {


            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = "EXEC Tp_CCPView '" + month + "', '" + year + "' ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public int Tp_Insert_Multiterr(string TP_Date, string worktype_Code, string worktype_Name, string TP_Objective, string sf_code, string SF_Name, string div_code, string mode, string TP_Approval_MGR, string Dr_Code, string Dr_Name, string Chem_Code, string Chem_Name, string terr_code, string terr_name)
        {
            int iReturn = -1;

            int Change_Status = 0;
            int Confirmed = 0;
            //string terr_codeee = string.Empty;
            //string terr_nameee = string.Empty;

            try
            {

                if (mode == "submit")
                {
                    Change_Status = 1;
                    Confirmed = 0;

                }
                else if (mode == "draft")
                {
                    Change_Status = 0;
                    Confirmed = 0;
                }

                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsTp = new DataSet();
                DataSet dsTerr = new DataSet();

                strQry = " select Tour_Month from Trans_TP_One " +
                         " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + div_code + "' ";


                dsTp = db.Exec_DataSet(strQry);



                if (dsTp.Tables[0].Rows.Count == 0)
                {

                    strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Territory_Code1, " +
                            " Objective,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Confirmed,TP_Sf_Name,TP_Approval_MGR,Dr_Code,Dr_Name,Chem_Code,Chem_Name) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                            " '" + terr_name + "','" + terr_code + "','" + TP_Objective + "','" + worktype_Code + "', '" + worktype_Name + "', " +
                            " '" + div_code + "','" + Change_Status + "', getdate(),'" + Confirmed + "','" + SF_Name + "','" + TP_Approval_MGR + "','" + Dr_Code + "','" + Dr_Name + "','" + Chem_Code + "','" + Chem_Name + "') ";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {


                    strQry = "Update Trans_TP_One set Tour_Schedule1='" + terr_name + "' ," +
                             "Territory_Code1='" + terr_code + "', " +
                              "Objective='" + TP_Objective + "', " +
                             "WorkType_Code_B ='" + worktype_Code + "' ," +
                             "Worktype_Name_B ='" + worktype_Name + "' ," +
                             "Change_Status ='" + Change_Status + "' ," +
                             "submission_date =getdate() ," +
                             "Confirmed ='" + Confirmed + "' ," +
                             "TP_Sf_Name='" + SF_Name + "', " +
                             "Dr_Code ='" + Dr_Code + "', " +
                              "Dr_Name ='" + Dr_Name + "', " +
                              "Chem_Code ='" + Chem_Code + "', " +
                            "Chem_Name ='" + Chem_Name + "', " +
                             " TP_Approval_MGR='" + TP_Approval_MGR + "' " +
                             " where sf_code='" + sf_code + "' and Tour_Month='" + dt_TourPlan.Month + "' and Tour_Year='" + dt_TourPlan.Year + "' " +
                             " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' ";


                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Tp_approve_Multiterr(string TP_Date, string worktype_Code, string worktype_Name, string TP_Objective, string sf_code, string SF_Name, string div_code, string TP_Approval_MGR, string Dr_Code, string Dr_Name, string Chem_Code, string Chem_Name, string terr_code, string terr_name)
        {
            int iReturn = -1;
            //string terr_codeee = string.Empty;
            //string terr_nameee = string.Empty;

            try
            {



                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsTp = new DataSet();
                DataSet dsTerr = new DataSet();

                strQry = " select Tour_Month from Trans_TP " +
                         " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + div_code + "' ";


                dsTp = db.Exec_DataSet(strQry);

                if (dsTp.Tables[0].Rows.Count == 0)
                {


                    strQry = "Update Trans_TP_One set Tour_Schedule1='" + terr_name + "' ," +
                             "Territory_Code1='" + terr_code + "', " +
                              "Objective='" + TP_Objective + "', " +
                             "WorkType_Code_B ='" + worktype_Code + "' ," +
                             "Worktype_Name_B ='" + worktype_Name + "' ," +
                             "submission_date =getdate() ," +
                             "TP_Sf_Name='" + SF_Name + "', " +
                             " TP_Approval_MGR='" + TP_Approval_MGR + "', " +
                             " Dr_Code ='" + Dr_Code + "', " +
                             " Dr_Name ='" + Dr_Name + "', " +
                            "Chem_Code ='" + Chem_Code + "', " +
                             "Chem_Name ='" + Chem_Name + "' " +
                             " where sf_code='" + sf_code + "' and Tour_Month='" + dt_TourPlan.Month + "' and Tour_Year='" + dt_TourPlan.Year + "' " +
                             " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' and Confirmed=0 and Change_Status=1 ";


                    iReturn = db.ExecQry(strQry);


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet Tp_Multerr_Edit(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = " select CONVERT(varchar,Tour_Date,103) as date,DATENAME(dw,Tour_Date) as day_name,'' Holiday,'' weekoff,'' Field_Work,Objective,Territory_Code1 " +
                       "  as territory_code,WorkType_Code_B,Tour_Schedule2 as days_check,dr_code,dr_name,chem_code,chem_name " +
                      " from Trans_TP_One where SF_Code='" + sf_code + "' and month(Tour_Date)='" + month + "' and year(Tour_Date)='" + year + "' and confirmed=0 and change_status=1 order by Tour_Date  ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataSet last_Tp_terr_draft(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = " select CONVERT(varchar,Tour_Date,103) as date,DATENAME(dw,Tour_Date) as day_name,'' Holiday,'' weekoff,'' Field_Work,Objective,Territory_Code1 " +
                       "  as territory_code,WorkType_Code_B,Tour_Schedule2 as days_check,confirmed,change_status,Rejection_Reason,dr_code,dr_name,chem_code,chem_name " +
                      " from Trans_TP_One where SF_Code='" + sf_code + "' and month(Tour_Date)='" + month + "' and year(Tour_Date)='" + year + "'  order by Tour_Date ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }


        public int TP_Apprv_terr_Update(string MR_Code, string Tour_Month, string Tour_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsTPdate = new DataSet();

                strQry = "EXEC TP_Apprv_terr_Update '" + MR_Code + "', '" + Tour_Month + "','" + Tour_Year + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet FetchTerritory_new(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            int iCount;

            strQry =
                    " SELECT Territory_Code,Territory_Name FROM  Mas_Territory_Creation " +
                    " where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet Tp_Newfill_Edit(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = " select CONVERT(varchar,Tour_Date,103) as date,DATENAME(dw,Tour_Date) as day_name,'' Holiday,'' weekoff,'' Field_Work,Objective,Territory_Code1 " +
                       "  as territory_code,WorkType_Code_B " +
                      " from Trans_TP_One where SF_Code='" + sf_code + "' and month(Tour_Date)='" + month + "' and year(Tour_Date)='" + year + "' and confirmed=0 and change_status=1 order by Tour_Date  ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataSet last_Tp_Datecheck_draft(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = " select CONVERT(varchar,Tour_Date,103) as date,DATENAME(dw,Tour_Date) as day_name,'' Holiday,'' weekoff,'' Field_Work,Objective,Territory_Code1 " +
                       "  as territory_code,WorkType_Code_B " +
                      " from Trans_TP_One where SF_Code='" + sf_code + "' and month(Tour_Date)='" + month + "' and year(Tour_Date)='" + year + "' and confirmed=0 and change_status=0 order by Tour_Date  ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public int Tp_Insert_Record(string TP_Date, string TP_Terr_Name, string Territory_Code1, string worktype_Code, string worktype_Name, string TP_Objective, string sf_code, string SF_Name, string div_code, string mode)
        {
            int iReturn = -1;

            int Change_Status = 0;
            int Confirmed = 0;

            try
            {

                if (mode == "submit")
                {
                    Change_Status = 1;
                    Confirmed = 0;

                }
                else if (mode == "draft")
                {
                    Change_Status = 0;
                    Confirmed = 0;
                }

                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsTp = new DataSet();

                strQry = " select Tour_Month from Trans_TP_One " +
                         " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + div_code + "' ";


                dsTp = db.Exec_DataSet(strQry);



                if (dsTp.Tables[0].Rows.Count == 0)
                {
                    strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Territory_Code1, " +
                            " Objective,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Confirmed,TP_Sf_Name) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                            " '" + TP_Terr_Name + "','" + Territory_Code1 + "','" + TP_Objective + "','" + worktype_Code + "', '" + worktype_Name + "', " +
                            " '" + div_code + "','" + Change_Status + "', getdate(),'" + Confirmed + "','" + SF_Name + "') ";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    strQry = "Update Trans_TP_One set Tour_Schedule1='" + TP_Terr_Name + "' ," +
                             "Territory_Code1='" + Territory_Code1 + "', " +
                              "Objective='" + TP_Objective + "', " +
                             "WorkType_Code_B ='" + worktype_Code + "' ," +
                             "Worktype_Name_B ='" + worktype_Name + "' ," +
                             "Change_Status ='" + Change_Status + "' ," +
                             "submission_date =getdate() ," +
                             "Confirmed ='" + Confirmed + "' ," +
                             "TP_Sf_Name='" + SF_Name + "' " +
                             " where sf_code='" + sf_code + "' and Tour_Month='" + dt_TourPlan.Month + "' and Tour_Year='" + dt_TourPlan.Year + "' " +
                             " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' ";


                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet STP_EDIT(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = "EXEC STP_EDIT '" + div_code + "','" + sf_code + "' ";
            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataSet STP_Creation()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = "EXEC STP_Creation";
            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataSet FetchTerritory_STP(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            int iCount;

            strQry = "SELECT distinct a.Territory_Code,a.Territory_Name FROM  Mas_Territory_Creation a,Mas_ListedDr b " +
                    " where a.SF_Code = '" + sf_code + "' AND a.Territory_Active_Flag=0 and a.SF_Code=b.sf_code " +
                    " and CAST( a.Territory_Code as varchar)=b.Territory_Code and ListedDr_Active_Flag=0 ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet Fillcateg_STP(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            int iCount;

            strQry = "EXEC Fillcateg_STP '" + div_code + "','" + sf_code + "'";


            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataSet Fillcateg_STP_Vst(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            int iCount;

            strQry = "EXEC Fillcateg_STP_Vst '" + div_code + "','" + sf_code + "'";


            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public int STP_Creation_Insert(string sf_code, string div_code, string Day_Plan_Name, string Day_Plan_ShortName, string Day_Plan_Code, string Patch_Code, string Patch_Name, string Dr_Code, string Dr_Name, string Chem_Code, string Chem_Name, string Active_Flag)
        {
            int iReturn = -1;

            int Change_Status = 0;
            int Confirmed = 0;
            //string terr_codeee = string.Empty;
            //string terr_nameee = string.Empty;

            try
            {




                DB_EReporting db = new DB_EReporting();
                DataSet dsTp = new DataSet();
                DataSet dsTerr = new DataSet();

                strQry = " select Day_Plan_Name from mas_stp " +
                         " where SF_Code='" + sf_code + "' and division_code='" + div_code + "' and" +
                         " Day_Plan_Code = '" + Day_Plan_Code + "' and Day_Plan_Name='" + Day_Plan_Name + "' and Day_Plan_ShortName ='" + Day_Plan_ShortName + "'  ";


                dsTp = db.Exec_DataSet(strQry);



                if (dsTp.Tables[0].Rows.Count == 0)
                {

                    strQry = " insert into mas_stp (SF_Code,Division_Code,Day_Plan_Name,Day_Plan_ShortName,Day_Plan_Code,Patch_Code, " +
                            " Patch_Name,Dr_Code,Dr_Name,Chem_Code,Chem_Name,Active_Flag,Created_Date,Last_Updated_Date) " +
                            " VALUES('" + sf_code + "','" + div_code + "','" + Day_Plan_Name + "','" + Day_Plan_ShortName + "','" + Day_Plan_Code + "','" + Patch_Code + "',  " +
                            " '" + Patch_Name + "','" + Dr_Code + "','" + Dr_Name + "','" + Chem_Code + "', '" + Chem_Name + "', " +
                            " '" + Active_Flag + "', getdate(),getdate()) ";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {


                    strQry = "Update mas_stp set Patch_Code='" + Patch_Code + "' ," +
                             "Patch_Name='" + Patch_Name + "', " +
                              "Dr_Code='" + Dr_Code + "', " +
                             "Dr_Name ='" + Dr_Name + "' ," +
                             "Chem_Code ='" + Chem_Code + "' ," +
                             "Chem_Name ='" + Chem_Name + "' ," +
                             "Last_Updated_Date =getdate() " +

                             " where sf_code='" + sf_code + "' and Day_Plan_Code='" + Day_Plan_Code + "' and Day_Plan_Name='" + Day_Plan_Name + "' " +
                             " and Day_Plan_ShortName ='" + Day_Plan_ShortName + "' and Division_Code='" + div_code + "' ";


                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public DataSet Tp_STP_Edit(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = " select CONVERT(varchar,Tour_Date,103) as date,DATENAME(dw,Tour_Date) as day_name,'' Holiday,'' weekoff,'' Field_Work,Objective,Territory_Code1 " +
                       "  as territory_code,WorkType_Code_B,Tour_Schedule2 as days_check,dr_code,dr_name,chem_code,chem_name,others_code as weekdays " +
                      " from Trans_TP_One where SF_Code='" + sf_code + "' and month(Tour_Date)='" + month + "' and year(Tour_Date)='" + year + "' and confirmed=0 and change_status=1 order by Tour_Date  ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataSet last_Tp_STP_draft(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = " select CONVERT(varchar,Tour_Date,103) as date,DATENAME(dw,Tour_Date) as day_name,'' Holiday,'' weekoff,'' Field_Work,Objective,Territory_Code1 " +
                       "  as territory_code,WorkType_Code_B,Tour_Schedule2 as days_check,confirmed,change_status,Rejection_Reason,dr_code,dr_name,chem_code,chem_name,others_code as weekdays " +
                      " from Trans_TP_One where SF_Code='" + sf_code + "' and month(Tour_Date)='" + month + "' and year(Tour_Date)='" + year + "'  order by Tour_Date ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }


        public int Tp_Insert_STP(string TP_Date, string worktype_Code, string worktype_Name, string TP_Objective, string sf_code, string SF_Name, string div_code, string mode, string TP_Approval_MGR, string Dr_Code, string Dr_Name, string Chem_Code, string Chem_Name, string STP_Code, string STP_Name)
        {
            int iReturn = -1;

            int Change_Status = 0;
            int Confirmed = 0;
            string terr_code = string.Empty;
            string terr_name = string.Empty;
            //string terr_codeee = string.Empty;
            //string terr_nameee = string.Empty;

            try
            {

                if (mode == "submit")
                {
                    Change_Status = 1;
                    Confirmed = 0;

                }
                else if (mode == "draft")
                {
                    Change_Status = 0;
                    Confirmed = 0;
                }

                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsTp = new DataSet();
                DataSet dsTerr = new DataSet();
                DataSet dsSTp = new DataSet();



                strQry = " select Patch_Code,Patch_Name from Mas_STP " +
                        " where SF_Code='" + sf_code + "' and Day_Plan_ShortName='" + STP_Code + "' and " +
                        " Day_Plan_Name =  '" + STP_Name + "' and Division_Code= '" + div_code + "' ";
                dsSTp = db.Exec_DataSet(strQry);

                if (dsSTp.Tables[0].Rows.Count > 0)
                {
                    terr_code = dsSTp.Tables[0].Rows[0]["Patch_Code"].ToString();
                    terr_name = dsSTp.Tables[0].Rows[0]["Patch_Name"].ToString();
                }

                strQry = " select Tour_Month from Trans_TP_One " +
                         " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + div_code + "' ";


                dsTp = db.Exec_DataSet(strQry);



                if (dsTp.Tables[0].Rows.Count == 0)
                {

                    strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Territory_Code1, " +
                            " Objective,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Confirmed,TP_Sf_Name,TP_Approval_MGR,Dr_Code,Dr_Name,Chem_Code,Chem_Name,Others_Code,Others_Name) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                            " '" + terr_name + "','" + terr_code + "','" + TP_Objective + "','" + worktype_Code + "', '" + worktype_Name + "', " +
                            " '" + div_code + "','" + Change_Status + "', getdate(),'" + Confirmed + "','" + SF_Name + "','" + TP_Approval_MGR + "','" + Dr_Code + "','" + Dr_Name + "','" + Chem_Code + "','" + Chem_Name + "','" + STP_Code + "','" + STP_Name + "') ";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {


                    strQry = "Update Trans_TP_One set Tour_Schedule1='" + terr_name + "' ," +
                             "Territory_Code1='" + terr_code + "', " +
                              "Objective='" + TP_Objective + "', " +
                             "WorkType_Code_B ='" + worktype_Code + "' ," +
                             "Worktype_Name_B ='" + worktype_Name + "' ," +
                             "Change_Status ='" + Change_Status + "' ," +
                             "submission_date =getdate() ," +
                             "Confirmed ='" + Confirmed + "' ," +
                             "TP_Sf_Name='" + SF_Name + "', " +
                             "Dr_Code ='" + Dr_Code + "', " +
                              "Dr_Name ='" + Dr_Name + "', " +
                              "Chem_Code ='" + Chem_Code + "', " +
                            "Chem_Name ='" + Chem_Name + "', " +
                            "Others_Code ='" + STP_Code + "', " +
                            "Others_Name ='" + STP_Name + "' , " +
                             " TP_Approval_MGR='" + TP_Approval_MGR + "' " +
                             " where sf_code='" + sf_code + "' and Tour_Month='" + dt_TourPlan.Month + "' and Tour_Year='" + dt_TourPlan.Year + "' " +
                             " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' ";


                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Tp_approve_STP(string TP_Date, string worktype_Code, string worktype_Name, string TP_Objective, string sf_code, string SF_Name, string div_code, string TP_Approval_MGR, string Dr_Code, string Dr_Name, string Chem_Code, string Chem_Name, string STP_Code, string STP_Name)
        {
            int iReturn = -1;
            string terr_code = string.Empty;
            string terr_name = string.Empty;

            try
            {



                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsTp = new DataSet();
                DataSet dsTerr = new DataSet();
                DataSet dsSTp = new DataSet();


                strQry = " select Patch_Code,Patch_Name from Mas_STP " +
                       " where SF_Code='" + sf_code + "' and Day_Plan_ShortName='" + STP_Code + "' and " +
                       " Day_Plan_Name =  '" + STP_Name + "' and Division_Code= '" + div_code + "' ";
                dsSTp = db.Exec_DataSet(strQry);

                if (dsSTp.Tables[0].Rows.Count > 0)
                {
                    terr_code = dsSTp.Tables[0].Rows[0]["Patch_Code"].ToString();
                    terr_name = dsSTp.Tables[0].Rows[0]["Patch_Name"].ToString();
                }


                strQry = " select Tour_Month from Trans_TP " +
                         " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + div_code + "' ";


                dsTp = db.Exec_DataSet(strQry);

                if (dsTp.Tables[0].Rows.Count == 0)
                {


                    strQry = "Update Trans_TP_One set Tour_Schedule1='" + terr_name + "' ," +
                             "Territory_Code1='" + terr_code + "', " +
                              "Objective='" + TP_Objective + "', " +
                             "WorkType_Code_B ='" + worktype_Code + "' ," +
                             "Worktype_Name_B ='" + worktype_Name + "' ," +
                             "submission_date =getdate() ," +
                             "TP_Sf_Name='" + SF_Name + "', " +
                             " TP_Approval_MGR='" + TP_Approval_MGR + "', " +
                             " Dr_Code ='" + Dr_Code + "', " +
                             " Dr_Name ='" + Dr_Name + "', " +
                             "Others_Code ='" + STP_Code + "', " +
                            "Others_Name ='" + STP_Name + "' , " +
                            "Chem_Code ='" + Chem_Code + "', " +
                             "Chem_Name ='" + Chem_Name + "' " +
                             " where sf_code='" + sf_code + "' and Tour_Month='" + dt_TourPlan.Month + "' and Tour_Year='" + dt_TourPlan.Year + "' " +
                             " and Tour_Date ='" + strTPdt + "' and Division_Code='" + div_code + "' and Confirmed=0 and Change_Status=1 ";


                    iReturn = db.ExecQry(strQry);


                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet STP_TP_Fill()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = "EXEC STP_TP_Fill";
            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataSet Fillstp_drcode(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = "select Day_Plan_ShortName,Dr_Code from mas_stp where sf_code='" + sf_code + "' and division_code='" + div_code + "' and Active_Flag=0";
            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataSet Fillstp_chemcode(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = "select Day_Plan_ShortName,chem_code from mas_stp where sf_code='" + sf_code + "' and division_code='" + div_code + "' and Active_Flag=0";
            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }


        public DataSet Fillstp_Patch(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTrans_tp_one = null;



            strQry = "select Patch_Code,Patch_Name from mas_stp where sf_code='" + sf_code + "' and division_code='" + div_code + "' and Active_Flag=0";
            try
            {
                dsTrans_tp_one = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataTable STP_DR_VIEW(string sf_code, string div_code)
        {


            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsTrans_tp_one = null;



            strQry = " EXEC STP_DR_VIEW '" + div_code + "','" + sf_code + "' ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        public DataTable STP_CHEM_VIEW(string sf_code, string div_code)
        {


            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsTrans_tp_one = null;



            strQry = " EXEC STP_CHEM_VIEW '" + div_code + "','" + sf_code + "' ";

            try
            {
                dsTrans_tp_one = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTrans_tp_one;
        }

        //added by jas
        public DataSet gridTpviewAppRej(string sfcode, string div, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select convert(varchar(10),tour_date,103) TP_Date ,UPPER( DATENAME(dw,tour_date)) TP_Day ,worktype_name_b as work, " +
                     " isnull(tour_schedule1,'')+isnull(tour_schedule2,'')+isnull(tour_schedule3,'') Territory,dr_name,Objective from trans_tp_one where  " +
                     " tour_month='" + month + "' and tour_year='" + year + "' and sf_code='" + sfcode + "' and   division_code='" + div + "' order by tour_date ";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                // Do Nothing
            }

            return dsListedDR;
        }

        //added by jas
        public int Approve_TpNewMGR(string MR_Code, string MR_Month, string MR_Year, string mgr_name)
        {
            int iReturn = -1;
            int icount = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "   insert into trans_tp  " +

                         " (SF_Code, Tour_Month, Tour_Year, Submission_date, Tour_Date, WorkType_Code_B, Worktype_Name_B, Tour_Schedule1,                                                               " +
                         " Tour_Schedule2, Tour_Schedule3, Objective, Worked_With_SF_Code, Division_Code, Confirmed, Confirmed_Date, Rejection_Reason,                                                  " +
                         " Change_Status, Territory_Code1, Territory_Code2, Territory_Code3, Worked_With_SF_Name, WorkType_Code_B1, Worktype_Name_B1, WorkType_Code_B2,                                 " +
                         " Worktype_Name_B2, TP_Sf_Name, TP_Approval_MGR, Entry_mode, Dr_Code, Dr_Name, Chem_Code, Chem_Name,                                                                           " +
                         " Stockist_Code, Stockist_Name, Hosptial_Code, Hosptial_Name, Others_Code, Others_Name, Deviate, Dr_two_code,                                                                  " +
                         " Dr_two_name, Dr_three_code, Dr_three_name, Chem_two_code, Chem_two_name, Chem_three_code, Chem_three_name, Unlistdr_one_code,                                                " +
                         " Unlistdr_one_name, Unlistdr_two_code, Unlistdr_two_name, Unlistdr_three_code, Unlistdr_three_name, Remark_two, Remark_three, Jointwork_two_code,                             " +
                         " Jointwork_two_name, Jointwork_three_code, Jointwork_three_name, Approval_mode, Stockist_two_code, Stockist_two_name, Stockist_three_code, Stockist_three_name,               " +
                         " HQCodes, HQNames)                                                                                                                                                            " +
                         "                                                                                                                                                                              " +
                         "                                                                                                                                                                              " +
                         " select SF_Code, Tour_Month, Tour_Year, Submission_date, Tour_Date, WorkType_Code_B, Worktype_Name_B, Tour_Schedule1,                                                         " +
                         " Tour_Schedule2, Tour_Schedule3, Objective, Worked_With_SF_Code, Division_Code,'1'            ,getdate()      ,Rejection_Reason   ,'1'        ,                               " +
                         " Territory_Code1     ,Territory_Code2        ,Territory_Code3        ,Worked_With_SF_Name    ,WorkType_Code_B1   ,Worktype_Name_B1   ,WorkType_Code_B2   ,Worktype_Name_B2   ," +
                         " TP_Sf_Name          ,'" + mgr_name + "' ,Entry_mode         ,Dr_Code                ,Dr_Name                ,Chem_Code          ,Chem_Name          ,Stockist_Code      ,    " +
                         " Stockist_Name       ,Hosptial_Code      ,Hosptial_Name      ,Others_Code            ,Others_Name            ,Deviate                ,Dr_two_code            ,Dr_two_name  ,  " +
                         " Dr_three_code       ,Dr_three_name      ,Chem_two_code      ,Chem_two_name      ,Chem_three_code        ,Chem_three_name        ,Unlistdr_one_code  ,Unlistdr_one_name  ,    " +
                         " Unlistdr_two_code   ,Unlistdr_two_name  ,Unlistdr_three_code    ,Unlistdr_three_name    ,Remark_two         ,Remark_three       ,Jointwork_two_code ,Jointwork_two_name ,    " +
                         " Jointwork_three_code,Jointwork_three_name,Approval_mode     ,Stockist_two_code  ,Stockist_two_name  ,Stockist_three_code    ,Stockist_three_name    ,HQCodes                ," +
                         " HQNames from trans_tp_one where sf_code='" + MR_Code + "' and tour_month='" + MR_Month + "' and tour_year='" + MR_Year + "'   order by tour_date                                                                        ";

                iReturn = db.ExecQry(strQry);

                strQry = " SELECT COUNT(*) FROM tourplan_detail WHERE sfcode='" + MR_Code + "' " +
                         " and mnth='" + MR_Month + "' and yr='" + MR_Year + "' ";
                icount = db.Exec_Scalar(strQry);
                if (icount > 0)
                {
                    strQry = " update tourplan_detail set change_status='3' WHERE sfcode='" + MR_Code + "' " +
                         " and mnth='" + MR_Month + "' and yr='" + MR_Year + "' ";
                    iReturn = db.ExecQry(strQry);
                }


                strQry = "delete from trans_tp_one where sf_code='" + MR_Code + "' and tour_month='" + MR_Month + "' and tour_year='" + MR_Year + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet get_Expense_Range(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select isnull(Delayed_Days,0)+2 cnt,State_Code from mas_salesforce  where sf_code='" + sfcode + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public bool IsExpChk_lastMonth(string sf_code, string div_code, int month, int year)
        {

            bool bRecordExist = false;
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "  SELECT COUNT(trans_slno) FROM DCRMain_trans a , mas_salesforce b WHERE a.sf_code='" + sf_code + "' " +
                     " and month(activity_date)='" + month + "' and year(activity_date)='" + year + "' " +
                     " and activity_date>=sf_tp_dcr_Active_dt and a.sf_code=b.sf_code ";
                iReturn = db.Exec_Scalar(strQry);

                if (iReturn > 0)
                {

                    strQry = " select COUNT(SF_Code) from Trans_Expense_Head where SF_Code='" + sf_code + "' and Expense_Month ='" + month + "'  and Expense_Year ='" + year + "' and (sndhqfl !=0 and sndhqfl !=10) ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else
                {
                    bRecordExist = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int Approve_TpNewMGRnew1(string MR_Code, string MR_Month, string MR_Year, string mgr_name)
        {
            int iReturn = -1;
            int icount = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsTPdate = new DataSet();
                strQry = " insert into trans_tp " +
                            " (SF_Code, Tour_Month, Tour_Year, Submission_date, Tour_Date, WorkType_Code_B, Worktype_Name_B,                                " +
                            " Tour_Schedule1, Tour_Schedule2, Tour_Schedule3, Objective, Worked_With_SF_Code, Division_Code,                                " +
                            " Confirmed, Confirmed_Date, Rejection_Reason, Change_Status, Territory_Code1, Territory_Code2,                                 " +
                            " Territory_Code3, Worked_With_SF_Name, WorkType_Code_B1, Worktype_Name_B1, WorkType_Code_B2, Worktype_Name_B2, TP_Sf_Name,     " +
                            " TP_Approval_MGR, Dr_Code, Dr_Name, Chem_Code, Chem_Name, Stockist_Code,                                                       " +
                            " Stockist_Name, Hosptial_Code, Hosptial_Name, Others_Code, Others_Name, Jointwork_two_code,                                    " +
                            " Jointwork_two_name, Jointwork_three_code, Jointwork_three_name, Dr_two_code, Dr_two_name, Dr_three_code,                      " +
                            " Dr_three_name, Chem_two_code, Chem_two_name, Chem_three_code, Chem_three_name, Remark_two,                                    " +
                            " Remark_three, HQCodes, HQNames, Deviate, Entry_mode, Unlistdr_one_code,                                                       " +
                            " Unlistdr_one_name, Unlistdr_two_code, Unlistdr_two_name, Unlistdr_three_code, Unlistdr_three_name, Approval_mode,             " +
                            " Stockist_two_code, Stockist_two_name, Stockist_three_code, Stockist_three_name " +
                           // "  Objective_id1, Objective_Name1, Objective_id2,Objective_Name2, Objective_id3, Objective_Name3
                          " )                                                                              " +
                            " select SF_Code, Tour_Month, Tour_Year, Submission_date,                                                                       " +
                            " Tour_Date, WorkType_Code_B, Worktype_Name_B, Tour_Schedule1, Tour_Schedule2, Tour_Schedule3, Objective,                       " +
                            " Worked_With_SF_Code, Division_Code, '1', getdate(), Rejection_Reason, '1', Territory_Code1,              " +
                            " Territory_Code2, Territory_Code3, Worked_With_SF_Name, WorkType_Code_B1, Worktype_Name_B1, WorkType_Code_B2,                  " +
                            " Worktype_Name_B2, TP_Sf_Name, '" + mgr_name + "', Dr_Code, Dr_Name, Chem_Code, Chem_Name,                                     " +
                            " Stockist_Code, Stockist_Name, Hosptial_Code, Hosptial_Name, Others_Code, Others_Name, Jointwork_two_code,                     " +
                            " Jointwork_two_name, Jointwork_three_code, Jointwork_three_name, Dr_two_code, Dr_two_name, Dr_three_code,                      " +
                            " Dr_three_name, Chem_two_code, Chem_two_name, Chem_three_code, Chem_three_name, Remark_two,                                    " +
                            " Remark_three, HQCodes, HQNames, Deviate, Entry_mode, Unlistdr_one_code,                                                       " +
                            " Unlistdr_one_name, Unlistdr_two_code, Unlistdr_two_name, Unlistdr_three_code, Unlistdr_three_name, Approval_mode,             " +
                            " Stockist_two_code, Stockist_two_name, Stockist_three_code, Stockist_three_name  " +
                            //Objective_id1, Objective_Name1,               " +
                            // " Objective_id2, Objective_Name2, Objective_id3, Objective_Name3                                                                " +
                            " from trans_tp_one where sf_code='" + MR_Code + "' and tour_month='" + MR_Month + "' and tour_year='" + MR_Year + "'  order by tour_date     ";


                iReturn = db.ExecQry(strQry);

                strQry = " SELECT COUNT(*) FROM tourplan_detail WHERE sfcode='" + MR_Code + "' " +
                        " and mnth='" + MR_Month + "' and yr='" + MR_Year + "' ";
                icount = db.Exec_Scalar(strQry);
                if (icount > 0)
                {
                    strQry = " update tourplan_detail set change_status='3' WHERE sfcode='" + MR_Code + "' " +
                         " and mnth='" + MR_Month + "' and yr='" + MR_Year + "' ";
                    iReturn = db.ExecQry(strQry);
                }

                strQry = "delete from trans_tp_one where sf_code='" + MR_Code + "' and tour_month='" + MR_Month + "' and tour_year='" + MR_Year + "'";
                iReturn = db.ExecQry(strQry);

                strQry = "Select substring(convert(char(10),Tour_Date,103),4,2)+'/'+'01'+'/'+substring(convert(char(10),Tour_Date,103),7,4) as Tour_Date from Trans_TP where Confirmed = 1 and SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' ";
                dsTPdate = db.Exec_DataSet(strQry);

                strQry = "update Mas_Salesforce set Last_TP_Date=DateAdd(MM,1,'" + dsTPdate.Tables[0].Rows[0]["Tour_Date"].ToString() + "') where Sf_Code='" + MR_Code + "'";
                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Salesforce_DCRTPdate set Last_TP_Date=DateAdd(MM,1,'" + dsTPdate.Tables[0].Rows[0]["Tour_Date"].ToString() + "') where Sf_Code='" + MR_Code + "'";
                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getweekoffcode(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

           strQry = "select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr where division_code='"+ Division_Code + "' and worktype_name_m='Weekly Off'";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
        public DataSet getholidaycode(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr where division_code='" + Division_Code + "' and worktype_name_m='Holiday'";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

    }

}
