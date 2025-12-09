using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
        public class State
        {
            private string strQry = string.Empty;

            public DataSet getState()
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = "SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " where State_Active_Flag=0 " +
                         " ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
            // Sorting
            public DataTable getStateLocationlist_DataTable()
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataTable dtState = null;
            
                //strQry = " select b.State_Code,b.StateName,b.ShortName, " +
                //       " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(b.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') division_name,  sf_count" +
                //       " from Mas_State b where State_Active_Flag=0 " +
                //       "  order by b.StateName";
                strQry = " select State_Code,StateName,ShortName, stuff((select ', '+Alias_Name from Mas_Division a where Division_Active_Flag = 0 and charindex(','+cast(st.State_Code as varchar)+',',','+a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name, " +
                    " stuff(isnull((select ', '+sf_Designation_Short_Name+' ( '+CAST(Cnt as varchar)+' )' from  vwSFDesgCnt where State_code=ST.State_code for xml path('')),''),1,2,'') sf_count " +
                    " from Mas_State ST  where State_Active_Flag=0  " +
                    " order by ST.StateName";

                try
                {
                    dtState = db_ER.Exec_DataTable(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dtState;
            }
            public DataTable getStateLocationlist_DataTable(string sAlpha)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataTable dtState = null;
             
                strQry = " select State_Code,StateName,ShortName, stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(st.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name, " +
                    " stuff(isnull((select ', '+sf_Designation_Short_Name+' ( '+CAST(Cnt as varchar)+' )' from  vwSFDesgCnt where State_code=ST.State_code for xml path('')),''),1,2,'') sf_count " +
                    " from Mas_State ST  where State_Active_Flag=0 and LEFT(ST.StateName,1) = '" + sAlpha + "' " +
                    " order by ST.StateName";

                try
                {
                    dtState = db_ER.Exec_DataTable(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dtState;
            }
            public DataSet getStateEd(string statecode)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = "SELECT shortname, statename" +
                         " FROM mas_state WHERE State_Active_Flag=0 And state_code= '" + statecode + "'" +
                         " ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }

        public DataSet getStmulti(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry =
                     " SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") and State_Active_Flag=0" +
                     " ORDER BY 2";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        //Changes done by priya
        public bool RecordExist(string shortname)
            {

                bool bRecordExist = false;
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(state_code) FROM mas_state WHERE shortname='" + shortname + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return bRecordExist;
            }

            public bool sRecordExist(string statename, int statecode)
            {

                bool bRecordExist = false;
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(state_code) FROM mas_state WHERE statename='" + statename + "'AND State_Code!='" + statecode + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return bRecordExist;
            }
       
            public bool sRecordExist(int statecode, string statename)
            {

                bool bRecordExist = false;
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(state_code) FROM mas_state WHERE state_code != '" + statecode + "' AND statename='" + statename + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return bRecordExist;
            }

            public int RecordAdd(string shortname, string statename)
            {
                int iReturn = -1;
                if (!RecordExistAdd(shortname))
                {
                    if (!sRecordExistAdd(statename))
                    {
                        try
                        {
                            DB_EReporting db = new DB_EReporting();

                            strQry = "SELECT isnull(max(State_Code)+1,'1') State_Code from mas_state ";
                            int State_Code = db.Exec_Scalar(strQry);

                            strQry = "INSERT INTO mas_state(State_Code,shortname,statename,Created_Date,LastUpdt_Date,State_Active_Flag)" +
                                     "values('" + State_Code + "','" + shortname + "', '" + statename + "',getdate(),getdate(),0) ";

                            iReturn = db.ExecQry(strQry);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        iReturn = -2;
                    }
                }
                else
                {
                    iReturn = -3;
                }
                return iReturn;
            }
            public int RecordUpdate(int statecode,  string statename)
            {
                int iReturn = -1;

                if (!sRecordExist(statename, statecode))
                {                      
                        try
                        {

                            DB_EReporting db = new DB_EReporting();

                            strQry = "UPDATE mas_state " +
                                     " SET statename = '" + statename + "' ," +
                                     " LastUpdt_Date = getdate() " +
                                     " WHERE state_code = '" + statecode + "' and State_Active_Flag=0 ";

                            iReturn = db.ExecQry(strQry);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        iReturn = -2;
                    }
                
                return iReturn;

            }

            //end
            public DataSet getStateProd(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = "SELECT 0 as state_code,'ALL' as statename,'' as shortname " +
                         " UNION " +
                         " SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
                         " ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
            public DataSet getState_new(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                //strQry = "SELECT 0 as state_code,'---Select State---' as statename,'' as shortname " +
                //         " UNION " +
                //         " SELECT state_code,statename,shortname " +
                //         " FROM mas_state " +
                //         " WHERE state_code in (" + state_code + ") " +
                //         " ORDER BY 2";
                strQry = " SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                        " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
                        " ORDER BY 2";

                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }

            
            public DataSet getState(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = "SELECT 0 as state_code,'---Select---' as statename,'' as shortname " +
                         " UNION " +
                         " SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
                         " ORDER BY 2";
                //strQry = " SELECT state_code,statename,shortname " +
                //         " FROM mas_state " +
                //         " WHERE state_code in (" + state_code + ") " +
                //         " ORDER BY 2";

                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }

            /* new  */


            /*  */
            public DataSet getStateChkBox(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = " SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
                         " ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }

            public DataSet getStateAddChkBox(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = " SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0" +
                         " union select '' state_code,'ALL' statename,'ALL' shortname ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
           


           // alphabetical
            public DataSet getState_Alphabet()
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsDivision = null;
                strQry = "select '1' val,'All' StateName " +
                         " union " +
                         " select distinct LEFT(StateName,1) val, LEFT(StateName,1) StateName" +
                         " FROM mas_State where State_Active_Flag=0 " +                                     
                         " ORDER BY 1";
                try
                {
                    dsDivision = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsDivision;
            }
            public DataSet getState_Report(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = "SELECT 0 as state_code,'---All State---' as statename " +
                         " UNION " +
                         " SELECT state_code,statename " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
                         " ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
            
      
            public DataSet getState_Alphabet(string sAlpha)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                //strQry = "SELECT State_Code,ShortName,StateName " +
                //         " FROM mas_State " +
                //         " where LEFT(StateName,1) = '" + sAlpha + "' and State_Active_Flag=0 " +
                //         " ORDER BY 2";
                //strQry = " select b.State_Code,b.StateName,b.ShortName, " +
                //       " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(b.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') division_name, sf_count" +
                //       " from Mas_State b where State_Active_Flag=0 and LEFT(b.StateName,1) = '" + sAlpha + "'" +
                //       "  order by b.StateName";
                strQry = " select State_Code,StateName,ShortName, stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(st.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name, " +
                       " stuff(isnull((select ', '+sf_Designation_Short_Name+' ( '+CAST(Cnt as varchar)+' )' from  vwSFDesgCnt where State_code=ST.State_code for xml path('')),''),1,2,'') sf_count " +
                       " from Mas_State ST  where State_Active_Flag=0 and LEFT(ST.StateName,1) = '" + sAlpha + "' " +
                       " order by ST.StateName";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }                       

            //

            public int RecordDelete(int  statecode)
            {
                int iReturn = -1;
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "DELETE FROM  mas_state WHERE state_code = '" + statecode + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return iReturn;

            }
            public DataSet getSt(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = " Select '0' state_code,'---Select---' as statename,'' as shortname" +
                         " Union" +
                         " SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0" +
                         " ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
            public bool RecordExist(int State_Code)
            {

                bool bRecordExist = false;
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    //strQry = "SELECT COUNT(state_code) FROM mas_division WHERE state_code = '" + State_Code + "'  ";
                    strQry = " SELECT COUNT(state_code) FROM mas_division WHERE state_code like  '%" + State_Code + "%'" ;


                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return bRecordExist;
            }

            public int DeActivateNew(int State_Code)
            {
                int iReturn = -1;
               
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE mas_state " +
                                     " SET State_active_flag=1 , " +
                                     " LastUpdt_Date = getdate(), State_Deactivate_Date = getdate()" +
                                     " WHERE State_Code = '" + State_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }                
               
                return iReturn;
            }
            public DataSet getStateName(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = "SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") " +
                         " ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
            public DataSet getState_Code(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = "SELECT state_code " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") ";
                        
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }

            // Changes done by Priya
            public DataSet getState_Division()
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
      
                //strQry = " select b.State_Code,b.StateName,b.ShortName, " +
                //         " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(b.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') division_name" +                       
                //         " from Mas_State b where State_Active_Flag=0 " +
                //         "  order by b.StateName";

                strQry = " select State_Code,StateName,ShortName, stuff((select ', '+Alias_Name from Mas_Division a where Division_Active_Flag = 0 and charindex(','+cast(st.State_Code as varchar)+',',','+a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name, " +
                         " stuff(isnull((select ', '+sf_Designation_Short_Name+' ( '+CAST(Cnt as varchar)+' )' from  vwSFDesgCnt where State_code=ST.State_code for xml path('')),''),1,2,'') sf_count " +
                         " from Mas_State ST where State_Active_Flag = 0 " +
                         "  group by State_Code,StateName,ShortName";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
            public DataSet getState_Reactivate()
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;

                strQry = " select b.State_Code,b.StateName,b.ShortName, " +
                         " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(b.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') division_name" +
                         " from Mas_State b where State_Active_Flag=1 " +
                         "  order by b.StateName";
          
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
            public int ReActivate_State(int State_Code)
            {
                int iReturn = -1;

                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_State " +
                                " SET State_Active_Flag=0 ," +
                                " LastUpdt_Date = getdate(), State_Reactivate_Date= getdate() " +
                                " WHERE State_Code = '" + State_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return iReturn;
            }
            //changes by Reshmi
            public bool RecordExistAdd(string shortname)
            {

                bool bRecordExist = false;
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(state_code) FROM mas_state WHERE shortname='" + shortname + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return bRecordExist;
            }

            public bool sRecordExistAdd(string statename)
            {

                bool bRecordExist = false;
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(state_code) FROM mas_state WHERE statename='" + statename + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return bRecordExist;
            }

            public DataSet getStateChkBox_WeekOff(string state_code, string div_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                //strQry = " SELECT state_code,statename,shortname " +
                //         " FROM mas_state " +
                //         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
                //         " ORDER BY 2";
                strQry = " SELECT state_code,statename,shortname, " +
                         " (select top(1) Holiday_Mode from Mas_Statewise_Holiday_Fixation_Weekdays a where a.state_code=b.state_code and a.Division_Code='" + div_code + "') as Holiday_Mode" +
                         " FROM mas_state b " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
                         " ORDER BY statename ASC";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
            public DataSet getState_Stock(string state_code, string Div_Code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = " SELECT 0 as state_code,'---Select---' as statename " +
                          " UNION " +
                          " select count(sf_code) state_code,state as statename " +
                          " from mas_stockist where Division_Code = '" + Div_Code + "' and " +
                          " State in  (select statename  FROM mas_state  " +
                          " WHERE state_code  in (" + state_code + ")) " +
                          " group by state";
                //strQry = " SELECT state_code,statename,shortname " +
                //         " FROM mas_state " +
                //         " WHERE state_code in (" + state_code + ") " +
                //         " ORDER BY 2";

                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }

            public DataSet getStcode(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = " SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0" +
                         " ORDER BY 2";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }

            public DataSet getStateAddChkBox_ForCamp(string state_code)
            {
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsState = null;
                strQry = " SELECT state_code,statename,shortname " +
                         " FROM mas_state " +
                         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0";
                try
                {
                    dsState = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsState;
            }
        }
 }
