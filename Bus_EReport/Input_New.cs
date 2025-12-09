using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;
using System.Data.SqlClient;


namespace Bus_EReport
{
    public class Input_New
    {
        private string strQry = string.Empty;


        public DataSet getEffDate_deact(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT 'All' Effective_From_To UNION SELECT DISTINCT (convert(varchar(12),gift_Effective_from,103)+' To '+convert(varchar(12),gift_effective_to,103)) Effective_From_To FROM mas_Gift " +
                       " WHERE gift_active_flag=0 AND division_code=  '" + divcode + "' " +
                       " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                       " order by 1 desc";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }



        public DataSet getGift_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,'0' as Status" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag=0" +
                   " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                   " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                   " union " +
                    "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,'0' as Status" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag=1" +
                   " order by Gift_Active_Flag,Gift_Name";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet getGift_React(string divcode, DateTime efffrom, DateTime effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,'0' as Status" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                " Gift_Effective_From = '" + EffFromdate + "' AND Gift_Effective_To = '" + EffTodate + "'" +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataTable getGiftlist_DataTable_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }


        public DataTable getGiftlist_React_Sort(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag=0" +
                        " AND  Gift_Name like '" + sAlpha + "%'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataTable getGiftDateDiff(string divcode, DateTime efffrom, DateTime effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            strQry = " SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                //" Gift_Effective_From >= '" + EffFromdate + "' AND Gift_Effective_To <= '" + EffTodate + "'" +
                     " (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }


        public int DeActivateGift_New(string GiftCode, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Gift " +
                            " SET Gift_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Gift_Code = '" + GiftCode + "' and Division_Code='" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int RecordUpdateGift_new(string GiftCode, string GiftName, string GiftSName, DateTime GiftVal, int GiftType, int divcode)
        {
            int iReturn = -1;
            // if (!RecordExistgift(GiftName, GiftType,divcode))
            //  {
            if (!snRecordExist(GiftCode, GiftSName, divcode))
            {
                if (!nRecordExist(GiftCode, GiftName, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Gift " +
                                 " SET Gift_Name = '" + GiftName + "', " +
                                 " Gift_SName = '" + GiftSName + "', " +
                                 " Gift_Effective_To = '" + GiftVal + "', " +
                                " Gift_Type = '" + GiftType + "'," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Gift_Code = '" + GiftCode + "' AND  Division_Code= " + divcode + " ";

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

        public bool snRecordExist(string Gift_Code, string Gift_SName, int div_code)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Code) FROM mas_Gift WHERE Gift_Code != '" + Gift_Code + "' AND Gift_SName='" + Gift_SName + "' and Division_Code=" + div_code + "  and Gift_Active_Flag=0";

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


        public bool nRecordExist(string Gift_Code, string Gift_Name, int div_code)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Code) FROM mas_Gift WHERE Gift_Code != '" + Gift_Code + "' AND Gift_Name='" + Gift_Name + "' and  Division_Code=" + div_code + "  and Gift_Active_Flag=0 ";

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


        public DataSet getProdgift(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Gift_Code,Gift_SName,Gift_Name,Gift_Value,case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       " else 'Ordinary Gift' " +
                       " end end end as Gift_Type," +
                       " Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,State_Code,Gift_Active_Flag,'0' as Status" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND LEFT(Gift_Name,1) = '" + sAlpha + "' AND Gift_Active_Flag='0'  " +
                        " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                        " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getProdgift_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "select '1' val,'All' Gift_Name " +
                     " union " +
                     " select distinct LEFT(Gift_Name,1) val, LEFT(Gift_Name,1) Gift_Name" +
                     " FROM Mas_Gift " +
                     " WHERE Division_Code = '" + divcode + "' " +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " AND Gift_Active_flag = 0 " +
                     " ORDER BY 1";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet getGiftName_React(string divcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                       " case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       "else 'Ordinary Gift' " +
                       "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,'0' as Status" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND  Gift_Name like '" + Name + "%'" +
                       "AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
              " union " +
               "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                " case when Gift_Type = '1' then 'Literature/Lable' " +
                " else case when Gift_Type = '2' then 'Special Gift'" +
                " else case when Gift_Type = '3' then 'Doctor Kit'" +
                " else 'Ordinary Gift' " +
                "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,'0' as Status" +
                " FROM Mas_Gift  WHERE Division_Code=" +
                "'" + divcode + "' AND  Gift_Name like '" + Name + "%' AND Gift_Active_Flag=1" +
              " order by Gift_Active_Flag,Gift_Name";
                try
                {
                    dsProCat = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsProCat;
            }
        }


        public DataSet getStategift_React(string div_code, int State_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "SELECT a.Gift_Code,a.Gift_SName,a.Gift_Name,a.Gift_Value," +
                 " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type," +
                    " a.Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,a.State_Code,a.Gift_Active_Flag,'0' as Status" +
                    " FROM mas_state c join Mas_Gift a" +
                     " on" +
                     " a.state_code like '" + State_Code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + State_Code + "' or" +
                     " a.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " a.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,c.state_code)='" + State_Code + "' and Gift_Active_Flag=0 and " +
                     " a.Division_Code ='" + div_code + "' " +
                     " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                     " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +

             " union " +
             "SELECT a.Gift_Code,a.Gift_SName,a.Gift_Name,a.Gift_Value," +
                 " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type," +
                    " a.Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,a.State_Code,a.Gift_Active_Flag,'0' as Status" +
                    " FROM mas_state c join Mas_Gift a" +
                     " on" +
                     " a.state_code like '" + State_Code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + State_Code + "' or" +
                     " a.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " a.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,c.state_code)='" + State_Code + "' and Gift_Active_Flag=1 and " +
                     " a.Division_Code ='" + div_code + "' " +
                      " order by Gift_Active_Flag,Gift_Name";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;

        }

        public DataSet getGiftDeactivated_React(string divcode, string DDl)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,'0' as Status" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag=1" +
                   " order by 1 desc";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet getGiftOnlyClosedDate_React(string divcode, string DDl)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,'0' as Status" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag=0" +
                   " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                   " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                   " order by 1 desc";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getGiftOnly_BulkDeactive(string divcode, string DDl)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type,Gift_Active_Flag,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To, " +
                     "  isnull(rtrim( case  " +
                     "  when Gift_Effective_To <= GETDATE() then 'Eff.ToDate Closed' " +
                     " when Gift_Active_Flag='0' then 'Eff.ToDate NotClosed'    end)," +
                     " case when Gift_Active_Flag='0' then 'Eff.ToDate NotClosed' end) Status  " +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag=0" +
                   " order by Status";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getStatePerDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select state_code from mas_division where division_code='" + div_code + "'";
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


        public DataSet getSt(string state_code)
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


        public string getGiftCode(string div_code, string Gift_Name) 
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string dsDivisionsub = null;
            strQry = "select Gift_Code from mas_Gift where  Division_Code='" + div_code + "' and Gift_Name='" + Gift_Name + "' and Gift_Active_Flag='0'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    dsDivisionsub = dsDivision.Tables[0].Rows[0]["Gift_Code"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivisionsub;
        }


        public string getSubPerDivision(string div_code)   
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string dsDivisionsub = null;
            strQry = "select subdivision_code from mas_subdivision where Div_Code='" + div_code + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
                for (int i = 0; i < dsDivision.Tables[0].Rows.Count; i++)
                {
                    dsDivisionsub = dsDivisionsub + dsDivision.Tables[0].Rows[i]["subdivision_code"].ToString() + ',';
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivisionsub;
        }


        public DataSet getState(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT 0 as state_code,'---All---' as statename,'' as shortname " +
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


        public DataSet getStateAll(string state_code)
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


        public int DeActivate_Input(string Gift_Code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Gift " +
                            " SET Gift_Active_Flag='1'  " +
                            " WHERE Gift_Code = '" + Gift_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public int Re_Activate_Input(string Gift_Code, DateTime To_Date)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                string EffTodate = To_Date.Month.ToString() + "-" + To_Date.Day + "-" + To_Date.Year;
                strQry = "UPDATE Mas_Gift " +
                            " SET Gift_Active_Flag='0', Gift_Effective_To= '" + EffTodate + "' " +
                            " WHERE Gift_Code = '" + Gift_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------



        public DataSet getDivision_Input(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select subdivision_code from mas_subdivision where Div_Code='" + div_code + "'";
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


        public DataSet getDiv_Input(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT 0 as subdivision_code,'---All---' as subdivision_name,'' as subdivision_sname " +
                     " UNION " +
                     " SELECT subdivision_code,subdivision_name,subdivision_sname " +
                     " FROM mas_subdivision " +
                     " WHERE subdivision_code in (" + div_code + ") and SubDivision_Active_Flag=0 " +
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


        public DataSet getBrandiv_Input(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Product_Brd_Code from Mas_Product_Brand where Division_Code='" + div_code + "'";
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



        public DataSet getBrand_Input(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT 0 as Product_Brd_Code,'---All---' as Product_Brd_Name,'' as Product_Brd_SName " +
                     " UNION " +
                     " SELECT Product_Brd_Code,Product_Brd_Name,Product_Brd_SName " +
                     " FROM Mas_Product_Brand " +
                     " WHERE Product_Brd_Code in (" + div_code + ") and Product_Brd_Active_Flag=0 " +
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


        public DataSet ViewGiftAll(string div_code, int State_Code, int iFrom, int iTo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select Gift_code,Gift_Name,Gift_Type,Gift_Value,Gift_SName,Division_Code,Gift_Effective_From,Gift_Effective_To,Created_Date,State_Code from Mas_Gift"+
            //         " where Gift_Active_Flag='0'" +            
            //         " And Division_Code = '" + div_code + "' " +
            //         " and year(Gift_Effective_From) = " + iFrom + " and year(Gift_Effective_To) = " + iTo + " order by 1";

            strQry = " SELECT b.Gift_Code,b.Gift_SName,b.Gift_Name,b.Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type," +
                     " convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,b.State_Code,b.Gift_Active_Flag," +

                     "  isnull(rtrim( case  " +
                     " when b.Gift_Active_Flag='1' then 'Inactive'  " +
                     "  when b.Gift_Effective_To <= GETDATE() then 'Eff.ToDate Closed' " +
                     " when b.Gift_Active_Flag='0' then 'Active'  " +
                     "  end),case when b.Gift_Active_Flag='0' then 'Active' end) mode " +

                     " FROM  Mas_Gift b" +
                     " WHERE  " +
                     " b.Division_Code ='" + div_code + "'" +
                     " and year(Gift_Effective_From) >= " + iFrom + " and year(Gift_Effective_To) <= " + iTo + " order by mode,Gift_SName";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet ViewGift(string div_code, int State_Code, int iFrom, int iTo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select Gift_code,Gift_Name,Gift_Type,Gift_Value,Gift_SName,Division_Code,Gift_Effective_From,Gift_Effective_To,Created_Date,State_Code from Mas_Gift"+
            //         " where Gift_Active_Flag='0'" +            
            //         " And Division_Code = '" + div_code + "' " +
            //         " and year(Gift_Effective_From) = " + iFrom + " and year(Gift_Effective_To) = " + iTo + " order by 1";

            strQry = " SELECT b.Gift_Code,b.Gift_SName,b.Gift_Name,b.Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type," +
                     " convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,b.State_Code,b.Gift_Active_Flag," +

                     "  isnull(rtrim( case  " +
                     " when b.Gift_Active_Flag='1' then 'Inactive'  " +
                     "  when b.Gift_Effective_To <= GETDATE() then 'Eff.ToDate Closed' " +
                     " when b.Gift_Active_Flag='0' then 'Active'  " +
                     "  end),case when b.Gift_Active_Flag='0' then 'Active' end) mode " +

                     " FROM mas_state a join Mas_Gift b" +
                     " on" +
                     " b.state_code like '" + State_Code + ',' + "%'  or " +
                     " b.state_code like '%" + ',' + State_Code + "' or" +
                     " b.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " b.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,a.state_code)='" + State_Code + "' and  " +
                     " b.Division_Code ='" + div_code + "'" +
                     " and year(Gift_Effective_From) >= " + iFrom + " and year(Gift_Effective_To) <= " + iTo + " order by mode,Gift_SName";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet ViewGiftDiv_Input(string div_code, int State_Code, int iFrom, int iTo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select Gift_code,Gift_Name,Gift_Type,Gift_Value,Gift_SName,Division_Code,Gift_Effective_From,Gift_Effective_To,Created_Date,State_Code from Mas_Gift"+
            //         " where Gift_Active_Flag='0'" +            
            //         " And Division_Code = '" + div_code + "' " +
            //         " and year(Gift_Effective_From) = " + iFrom + " and year(Gift_Effective_To) = " + iTo + " order by 1";

            strQry = " SELECT b.Gift_Code,b.Gift_SName,b.Gift_Name,b.Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type," +
                     " convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,b.subdivision_code,b.Gift_Active_Flag," +

                     "  isnull(rtrim( case  " +
                     " when b.Gift_Active_Flag='1' then 'Inactive'  " +
                     "  when b.Gift_Effective_To <= GETDATE() then 'Eff.ToDate Closed' " +
                     " when b.Gift_Active_Flag='0' then 'Active'  " +
                     "  end),case when b.Gift_Active_Flag='0' then 'Active' end) mode " +

                     " FROM mas_subdivision a join Mas_Gift b" +
                     " on" +
                     " b.subdivision_code like '" + State_Code + ',' + "%'  or " +
                     " b.subdivision_code like '%" + ',' + State_Code + "' or" +
                     " b.subdivision_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " b.subdivision_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,a.subdivision_code)='" + State_Code + "' and  " +
                     " b.Division_Code ='" + div_code + "'" +
                     " and year(Gift_Effective_From) >= " + iFrom + " and year(Gift_Effective_To) <= " + iTo + " order by mode,Gift_SName";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }



        public DataSet ViewGiftBrand_Input(string div_code, int State_Code, int iFrom, int iTo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select Gift_code,Gift_Name,Gift_Type,Gift_Value,Gift_SName,Division_Code,Gift_Effective_From,Gift_Effective_To,Created_Date,State_Code from Mas_Gift"+
            //         " where Gift_Active_Flag='0'" +            
            //         " And Division_Code = '" + div_code + "' " +
            //         " and year(Gift_Effective_From) = " + iFrom + " and year(Gift_Effective_To) = " + iTo + " order by 1";

            strQry = " SELECT b.Gift_Code,b.Gift_SName,b.Gift_Name,b.Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type," +
                     " convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,b.Product_Brd_Code,b.Gift_Active_Flag," +

                     "  isnull(rtrim( case  " +
                     " when b.Gift_Active_Flag='1' then 'Inactive'  " +
                     "  when b.Gift_Effective_To <= GETDATE() then 'Eff.ToDate Closed' " +
                     " when b.Gift_Active_Flag='0' then 'Active'  " +
                     "  end),case when b.Gift_Active_Flag='0' then 'Active' end) mode " +

                     " FROM Mas_Product_Brand a join Mas_Gift b" +
                     " on" +
                     " b.Product_Brd_Code like '" + State_Code + ',' + "%'  or " +
                     " b.Product_Brd_Code like '%" + ',' + State_Code + "' or" +
                     " b.Product_Brd_Code like '%" + ',' + State_Code + ',' + "%' or" +
                      " b.Product_Brd_Code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,a.Product_Brd_Code)='" + State_Code + "' and  " +
                     " b.Division_Code ='" + div_code + "'" +
                     " and year(Gift_Effective_From) >= " + iFrom + " and year(Gift_Effective_To) <= " + iTo + " order by mode,Gift_SName";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet ViewChkBox_All(string div_code, string State_Code, string Subdiv_Code, string Brand_Code, int iFrom, int iTo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC Input_StateDivBrand '" + div_code + "', '" + State_Code + "','" + Subdiv_Code + "','" + Brand_Code + "','" + iFrom + "','" + iTo + "' ";

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


        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        public DataSet getEffDate(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT 'All' Effective_From_To UNION SELECT DISTINCT (convert(varchar(12),gift_Effective_from,103)+' To '+convert(varchar(12),gift_effective_to,103)) Effective_From_To FROM mas_Gift " +
                       " WHERE gift_active_flag=0 AND division_code=  '" + divcode + "' " +
                       " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                       " ORDER BY 1 DESC";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getGift(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        " else case when Gift_Type = '4' then 'Ordinary Gift'" +
                     " else case when Gift_Type = '5' then 'HVG'" +
                     " else case when Gift_Type = '6' then 'Paper Gift'" +
                     " else case when Gift_Type = '7' then 'Self'" +
                     " else case when Gift_Type = '8' then 'LVG'" +
                     " else case when Gift_Type = '9' then 'MVG'" +
                     " else case when Gift_Type = '10' then 'Others'" +
                     " else 'Gift' " +
                     " end end end end end end end end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                       " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getGift(string divcode, DateTime efffrom, DateTime effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else case when Gift_Type = '4' then 'Ordinary Gift'" +
                     " else case when Gift_Type = '5' then 'HVG'" +
                     " else case when Gift_Type = '6' then 'Paper Gift'" +
                     " else case when Gift_Type = '7' then 'Self'" +
                     " else case when Gift_Type = '8' then 'LVG'" +
                     " else case when Gift_Type = '9' then 'MVG'" +
                     " else case when Gift_Type = '10' then 'Others'" +
                     //" else case when Gift_Type = '11' then 'Gift'" +
                     " else 'Gift' " +
                     " end end end end end end end end end end as Gift_Type"+
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                     " Gift_Effective_From = '" + EffFromdate + "' AND Gift_Effective_To = '" + EffTodate + "'" +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataTable getGiftlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                        " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataTable getGiftlist_DataTable(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = "SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                    " case when Gift_Type = '1' then 'Literature/Lable' " +
                    " else case when Gift_Type = '2' then 'Special Gift'" +
                    " else case when Gift_Type = '3' then 'Doctor Kit'" +
                    "else 'Ordinary Gift' " +
                    "end end end as Gift_Type" +
                    " FROM Mas_Gift  WHERE Division_Code=" +
                    "'" + divcode + "' AND  Gift_Name like '" + sAlpha + "%'" +
                    "AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                 " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }


        public int DeActivateGift(String GiftCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Gift " +
                            " SET Gift_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Gift_Code = '" + GiftCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataSet getGiftName(string divcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                       " case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        " else case when Gift_Type = '4' then 'Ordinary Gift'" +
                     " else case when Gift_Type = '5' then 'HVG'" +
                     " else case when Gift_Type = '6' then 'Paper Gift'" +
                     " else case when Gift_Type = '7' then 'Self'" +
                     " else case when Gift_Type = '8' then 'LVG'" +
                     " else case when Gift_Type = '9' then 'MVG'" +
                     " else case when Gift_Type = '10' then 'Others'" +
                     " else 'Gift' " +
                     " end end end end end end end end end end as Gift_Type" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND  Gift_Name like '" + Name + "%'" +
                       "AND Gift_Active_Flag='0'" +
                         " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                    " ORDER BY 2";
                try
                {
                    dsProCat = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsProCat;
            }
        }


        public DataSet getStategift(string div_code, int State_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;

            strQry = "SELECT a.Gift_Code,a.Gift_SName,a.Gift_Name,a.Gift_Value,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To," +
                 " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       " else case when Gift_Type = '4' then 'Ordinary Gift'" +
                     " else case when Gift_Type = '5' then 'HVG'" +
                     " else case when Gift_Type = '6' then 'Paper Gift'" +
                     " else case when Gift_Type = '7' then 'Self'" +
                     " else case when Gift_Type = '8' then 'LVG'" +
                     " else case when Gift_Type = '9' then 'MVG'" +
                     " else case when Gift_Type = '10' then 'Others'" +
                     " else 'Gift' " +
                     " end end end end end end end end end end as Gift_Type" +
                     "end end end as Gift_Type," +
                    " a.Gift_Effective_From,a.State_Code" +
                    " FROM mas_state c join Mas_Gift a" +
                     " on" +
                     " a.state_code like '" + State_Code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + State_Code + "' or" +
                     " a.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " a.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,c.state_code)='" + State_Code + "' and Gift_Active_Flag=0 and " +
                     " a.Division_Code ='" + div_code + "' " +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) ";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }



        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public DataSet getGift(string divcode, string giftcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = "  SELECT Gift_SName,Gift_Name,Gift_Value,Gift_Type,Gift_Effective_From,Gift_Effective_To,State_Code" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND Gift_Code = '"+ giftcode + "' " +
            //            " ORDER BY 2";

            strQry = " SELECT Gift_SName,Gift_Name,Gift_Value,Gift_Type,convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From," +
                     " convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,State_Code,subdivision_code,Product_Brd_Code" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     " '" + divcode + "' AND Gift_Code = '" + giftcode + "' ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet getSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT subdivision_code,a.subdivision_sname,a.subdivision_name, " +
                     " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.subdivision_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "' and Product_Active_Flag=0) Sub_Count" +
                     ",(select count(e.subdivision_code) from Mas_Salesforce e where charindex(cast(a.subdivision_code as varchar),e.subdivision_code )> 0 and e.Division_Code ='" + divcode + ",' and sf_status=0 and sf_TP_Active_Flag=0) SubField_Count" +
                    " FROM mas_subdivision a WHERE a.subdivision_active_flag=0 And a.Div_Code= '" + divcode + "'" +
                    " ORDER BY 2";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }


        public DataSet getSubBrand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "select Product_Brd_Name,Product_Brd_Code from Mas_Product_Brand WHERE Product_Brd_Active_Flag=0 And Division_Code= '" + divcode + "'" +
                    " ORDER BY 2";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }


        public DataSet getDivEdit(string div_code, string Ho_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDiv = null;
            strQry = "SELECT Division_Code,Division_Name " +
                     " FROM Mas_Division " +
                     " WHERE Division_Active_Flag=0 " +
                     " AND Division_Code IN (" + div_code + ") " +
                //    " AND Division_Code IN (" + Ho_Div + ") " +
                     " ORDER BY 2";
            try
            {
                dsDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDiv;
        }


        public int RecordAddGift(string GiftSName, string GiftName, int GiftType, string Giftvalue, DateTime EffFrom, DateTime EffTo, int Division_Code, string State_Code, string subdivision_code, string Brand_code)
        {
            int iReturn = -1;
            //
            //    if (!RecordExist(GiftName,GiftType,EffFrom,EffTo,Division_Code, state))
            //   {

            if (!RecordExistGiftSN(GiftSName, Division_Code))
            {
                if (!RecordExistGiftN(GiftName, Division_Code))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Gift_Code)+1,'1') Gift_Code from Mas_Gift ";
                        int Gift_Code = db.Exec_Scalar(strQry);

                        string EffFromdate = EffFrom.Month.ToString() + "-" + EffFrom.Day + "-" + EffFrom.Year;
                        string EffTodate = EffTo.Month.ToString() + "-" + EffTo.Day + "-" + EffTo.Year;

                        strQry = "INSERT INTO Mas_Gift(Gift_Code,Gift_SName,Gift_Name,Gift_Type,Gift_Value,Gift_Effective_From,Gift_Effective_To,Division_Code,State_Code,subdivision_code,Product_Brd_Code, Created_Date,Gift_Active_flag,LastUpdt_Date)" +
                                 "values('" + Gift_Code + "','" + GiftSName + "','" + GiftName + "', '" + GiftType + "' , '" + Giftvalue + "' , " +
                                 " '" + EffFromdate + "' ,'" + EffTodate + "', " + Division_Code + "," +
                                 " '" + State_Code + "','" + subdivision_code + "','" + Brand_code + "', getdate(), '0',getdate()) ";

                        // ",getdate(),getdate()," + Division_Code + ", getdate(), '0')";

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

        public bool RecordExistGiftSN(string GiftSName, int div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_SName) FROM Mas_Gift WHERE Gift_SName='" + GiftSName + "' and Division_code=" + div_code + " and Gift_Active_Flag=0";
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

        public bool RecordExistGiftN(string GiftName, int div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Name) FROM Mas_Gift WHERE Gift_Name='" + GiftName + "' and Division_code=" + div_code + " and Gift_Active_Flag=0";
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



        public string getBrandPerDivision(string div_code) 
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string dsDivisionsub = null;
            strQry = "select Product_Brd_Code from Mas_Product_Brand where Division_Code='" + div_code + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                for (int i = 0; i < dsDivision.Tables[0].Rows.Count; i++)
                {
                    dsDivisionsub = dsDivisionsub + dsDivision.Tables[0].Rows[i]["Product_Brd_Code"].ToString() + ',';
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivisionsub;
        }

        public int RecordUpdateGift(string GiftCode, string GiftSName, string GiftName, int GiftType, string GiftVal, DateTime efffrom, DateTime effto, int divcode, string State_Code, string subdivision_code, string Brand_code)
        {
            int iReturn = -1;
            // if (!RecordExistgift(GiftName, GiftType, divcode))
            // {
            //if (!snRecordExist(GiftCode, GiftSName, divcode))
            {
                //if (!nRecordExist(GiftCode, GiftName, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Gift " +
                                 " SET Gift_Name = '" + GiftName + "', " +
                                 " Gift_SName = '" + GiftSName + "', " +
                                 " Gift_Value = '" + GiftVal + "', " +
                                 " Gift_Type = " + GiftType + " ," +
                            //" StateCode  = " +state +"," +
                                 " Gift_Effective_From = '" + efffrom.Month + '-' + efffrom.Day + '-' + efffrom.Year + "'," +
                                 " Gift_Effective_To = '" + effto.Month + '-' + effto.Day + '-' + effto.Year + "' , " +
                                 " State_Code = '" + State_Code + "', subdivision_code='" + subdivision_code + "',Product_Brd_Code='" + Brand_code + "', Gift_Active_Flag='0', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Gift_Code = '" + GiftCode + "'  AND  Division_Code=" + divcode + " ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                //else
                //{
                //    iReturn = -2;
                //}
            }
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }
        public int GetInput_Code()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Gift_Code)+1,'1') Gift_Code from Mas_Gift ";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
    }
}
