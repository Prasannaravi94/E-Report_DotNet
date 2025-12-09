using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class Territory
    {
        private string strQry = string.Empty;

        public DataSet getEmptyTerritory()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT TOP 10 '' Territory_Name,'' Territory_SName,'' Alias_Name,'' Town_City,'' Territory_Visit " +
                     " FROM  sys.tables ";
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
        public DataSet getTerritory_BulkDeact(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            strQry = "EXEC Fieldworce_Territory_Deactivate '" + div_code + "','" + sf_code + "'";

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

        public int DeActivate(string terr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                            " SET territory_active_flag=1,Territory_Deactive_Date=getdate()   " +
                            " WHERE Territory_Code = '" + terr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getTerritory_Deact(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT t.Territory_Code, t.Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,count(m.listeddrcode)cnt,count(c.Chemists_Code)chemcnt,count(h.Hospital_code)Hoscnt into #temp  " +
                     " FROM  Mas_Territory_Creation t " +
                     " left join mas_listeddr m on CHARINDEX(cast(t.Territory_Code as varchar),m.Territory_Code) > 0 " +
                     " and t.SF_Code = m.Sf_Code and m.ListedDr_Active_Flag=0 " +
                     " left join Mas_Chemists c on CHARINDEX(cast(t.Territory_Code as varchar),c.Territory_Code) > 0 " +
                     " and t.SF_Code = c.Sf_Code and c.Chemists_Active_Flag=0 " +
                     " left join Mas_Hospital h on CHARINDEX(cast(t.Territory_Code as varchar),h.Territory_Code) > 0 " +
                     " and t.SF_Code = h.Sf_Code and h.Hospital_Active_Flag=0 " +
                     " where t.Sf_Code = '" + sf_code + "' AND territory_active_flag=0 " +
                     " group by t.territory_code,t.Territory_Name, t.Territory_SName,t.Territory_Cat " +
                     " select * from #temp where cnt=0 and chemcnt=0 and hoscnt=0 " +
                     " ORDER BY Territory_Name " +
                     " drop table #temp ";


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

        public int RecordUpdate(string Territory_Code, string Territory_Name, string Territory_SName, string Territory_Type, string sf_code)
        {
            int iReturn = -1;
            //if (!RecordExist(div_code, div_sname))
            //{
                try
                {

                    DB_EReporting db = new DB_EReporting();


                    strQry = "UPDATE Mas_Territory_Creation " +
                             " SET Territory_Name = '" + Territory_Name + "', " +
                             " Territory_Cat = '" + Territory_Type + "', " +
                             " Territory_SName = '" + Territory_SName + "', LastUpdt_Date= getdate() " +
                             " WHERE Territory_Code = '" + Territory_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = " update mas_distance_Fixation set   " +
                        " To_Code_Code= STUFF(To_Code_Code, LEN(To_Code_Code), 1, '" + Territory_Type + "'), Town_Cat='" + Territory_Type + "' " +
                        " where  to_code='" + Territory_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from mas_distance_fixation where from_code in(select cast(territory_code as varchar) from mas_territory_creation where territory_cat='1')";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from mas_distance_fixation where to_code in(select cast(territory_code as varchar) from mas_territory_creation where territory_cat='1')";
                    iReturn = db.ExecQry(strQry);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            //}
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }


        public int TransferTerritory(string Territory_Code, string Target_Territory)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                            " SET Territory_Code = '" + Target_Territory + "'  " +
                            " WHERE Territory_Code = '" + Territory_Code + "'";

                iReturn = db.ExecQry(strQry);

                if (iReturn != -1)
                {
                    strQry = "UPDATE Mas_ListedDr " +
                                " SET Territory_Code = '" + Target_Territory + "'  " +
                                " WHERE Territory_Code  = '" + Territory_Code + "'";

                    iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        strQry = "UPDATE Mas_UnListedDr " +
                                    " SET Territory_Code = '" + Target_Territory + "'  " +
                                    " WHERE Territory_Code = '" + Territory_Code + "' ";

                        iReturn = db.ExecQry(strQry);

                        if (iReturn != -1)
                        {
                            strQry = "UPDATE Mas_Hospital " +
                                        " SET Territory_Code = '" + Target_Territory + "'  " +
                                        " WHERE Territory_Code = '" + Territory_Code + "' ";

                            iReturn = db.ExecQry(strQry);

                            if (iReturn != -1)
                            {
                                strQry = "UPDATE Mas_Territory_Creation " +
                                            " SET territory_active_flag=1, Territory_Deactive_Date=getdate()  " +
                                            " WHERE Territory_Code = '" + Territory_Code + "' ";

                                iReturn = db.ExecQry(strQry);
                            }


                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }



        
        public DataSet getTerritory(string sf_code,string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT '-1' Territory_Code, '---Select---' Territory_Name " +
                     " UNION " +
                     " SELECT Territory_Code, Territory_Name " +
                     " FROM  Mas_Territory_Creation " +
                     " where Sf_code = '" + sf_code + "' " +
                     " AND territory_code != '" + terr_code + "' AND territory_active_flag=0 ";
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

        public DataSet getTerritory_lat_long(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "with cte1 as (select a.Territory_Code,a.Territory_Name,case when a.Territory_Cat=1 then 'HQ' " +
                     "else case when a.Territory_Cat=2 then 'EX' else case when a.Territory_Cat=3 then 'OS' " +
                     "else 'OS-EX'  end end end as Territory_Cat,a.Alias_Name " +
                     "from Mas_Territory_Creation a where a.sf_code='" + sf_code + "' and a.Territory_Active_Flag=0 ) " +
                     "select Territory_Code,Territory_Name,Territory_Cat,Alias_Name, " +
                     "(select top 1 lat from  Mas_Territory_Creation a,Mas_ListedDr b,map_GEO_Customers c where " +
                     "a.SF_Code='" + sf_code + "' and c.Cust_Code=b.ListedDrCode and a.Territory_Active_Flag=0 and b.Territory_Code=e.Territory_Code order by MapId asc)lat, " +
                     "(select top 1 long from  Mas_Territory_Creation a,Mas_ListedDr b,map_GEO_Customers c where " +
                     "a.SF_Code='" + sf_code + "' and c.Cust_Code=b.ListedDrCode and a.Territory_Active_Flag=0 and b.Territory_Code=e.Territory_Code order by MapId asc)long, " +
                     "(select top 1 lat from  Mas_Territory_Creation a,Mas_ListedDr b,map_GEO_Customers c where  " +
                     "a.SF_Code='" + sf_code + "' and c.Cust_Code=b.ListedDrCode and a.Territory_Active_Flag=0 and b.Territory_Code=e.Territory_Code order by MapId desc)lat1, " +
                     "(select top 1 long from  Mas_Territory_Creation a,Mas_ListedDr b,map_GEO_Customers c where  " +
                     "a.SF_Code='" + sf_code + "' and c.Cust_Code=b.ListedDrCode and a.Territory_Active_Flag=0 and b.Territory_Code=e.Territory_Code order by MapId desc)long1, " +
                     "(select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code = cast(e.Territory_Code as varchar  ) " +
                     "and b.ListedDr_Active_Flag=0 and b.Territory_Code=e.Territory_Code) ListedDR_Count " +
                     "from cte1 e  order by  Territory_Name";

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
       
        public DataSet getTerritory_Create(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
            //         " case when a.Territory_Cat=1 then 'HQ' " +
            //         " else case when a.Territory_Cat=2 then 'EX' " +
            //         " else case when a.Territory_Cat=3 then 'OS' " +
            //         " else 'OS-EX' " +
            //         " end end end as Territory_Cat, " +
            //    //   " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
            //         " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code = cast(a.Territory_Code as varchar ) " +
            //         " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
            //         " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
            //         " and b.Chemists_Active_Flag=0) Chemists_Count, " +
            //         " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
            //         " and b.Hospital_Active_Flag =0) Hospital_Count, " +
            //         " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
            //         " and b.UnListedDr_Active_Flag=0) UnListedDR_Count,Territory_Active_Flag " +
            //         " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0 " +
            //         " order by Territory_Name";
            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                " case when a.Territory_Cat=1 then 'HQ' " +
                " else case when a.Territory_Cat=2 then 'EX' " +
                " else case when a.Territory_Cat=3 then 'OS' " +
                " else 'OS-EX'  " +
                " end end end as Territory_Cat, " +
                " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and a.SF_Code = b.Sf_Code " +
                "  and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                "  (select COUNT(b.Chemists_Code) from Mas_Chemists b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
                " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                " (select COUNT(b.Hospital_Code) from Mas_Hospital b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
                " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
                " and b.UnListedDr_Active_Flag=0) UnListedDR_Count, Alias_Name,Territory_Visit " +
                " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0 " +
                " order by Territory_Name";
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
        public DataSet getTerritory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
            //      " case when a.Territory_Cat=1 then 'HQ' " +
            //      " else case when a.Territory_Cat=2 then 'EX' " +
            //      " else case when a.Territory_Cat=3 then 'OS' " +
            //      " else 'OS-EX' " +
            //      " end end end as Territory_Cat, " +
            //        //   " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
            //        //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where " +
            //        //" (b.Territory_Code like cast(a.Territory_Code as varchar) +','+'%' or b.Territory_Code like '%'+','+ cast(a.Territory_Code as varchar) +','+'%'" +
            //        //" or b.Territory_Code like cast(a.Territory_Code as varchar ) )" +
            //        //" and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
            //        " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code = cast(a.Territory_Code as varchar ) and b.Sf_Code = '" + sf_code + "'  " +
            //      " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
            //      " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code  and b.Sf_Code = '" + sf_code + "' " +
            //      " and b.Chemists_Active_Flag=0) Chemists_Count, " +
            //      " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code  and b.Sf_Code = '" + sf_code + "' " +
            //      " and b.Hospital_Active_Flag =0) Hospital_Count, " +
            //      " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code  and b.Sf_Code = '" + sf_code + "' " +
            //      " and b.UnListedDr_Active_Flag=0) UnListedDR_Count, " +
            //       "(select sf_name from mas_salesforce b   where a.sf_code=b.sf_code ) sf_name " +
            //      " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0" +
            //      " order by Territory_Name";




            strQry = " SELECT a.Territory_Code, trim(a.Territory_Name) Territory_Name, a.Territory_SName, " +
       " case when a.Territory_Cat=1 then 'HQ' " +
       " else case when a.Territory_Cat=2 then 'EX' " +
       " else case when a.Territory_Cat=3 then 'OS' " +
       " else 'OS-EX'  " +
       " end end end as Territory_Cat, " +
       " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and a.SF_Code = b.Sf_Code " +
       "  and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
       "  (select COUNT(b.Chemists_Code) from Mas_Chemists b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
       " and b.Chemists_Active_Flag=0) Chemists_Count, " +
       " (select COUNT(b.Hospital_Code) from Mas_Hospital b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
       " and b.Hospital_Active_Flag =0) Hospital_Count, " +
       " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
       " and b.UnListedDr_Active_Flag=0) UnListedDR_Count, " +
       " (select sf_name from mas_salesforce b where a.sf_code=b.sf_code ) sf_name,Alias_Name,Town_City,Territory_Visit " +
       " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0 order by trim(Territory_Name)";

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


        public DataSet getTerritory1(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                  " case when a.Territory_Cat=1 then 'HQ' " +
                  " else case when a.Territory_Cat=2 then 'EX' " +
                  " else case when a.Territory_Cat=3 then 'OS' " +
                  " else 'OS-EX' " +
                  " end end end as Territory_Cat, " +
                    //   " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
                    //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where " +
                    //" (b.Territory_Code like cast(a.Territory_Code as varchar) +','+'%' or b.Territory_Code like '%'+','+ cast(a.Territory_Code as varchar) +','+'%'" +
                    //" or b.Territory_Code like cast(a.Territory_Code as varchar ) )" +
                    //" and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                    " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code = cast(a.Territory_Code as varchar ) and b.Sf_Code = '" + sf_code + "'  " +
                  " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                  " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code  and b.Sf_Code = '" + sf_code + "' " +
                  " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                  " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code  and b.Sf_Code = '" + sf_code + "' " +
                  " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                  " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code  and b.Sf_Code = '" + sf_code + "' " +
                  " and b.UnListedDr_Active_Flag=0) UnListedDR_Count, " +
                   "(select sf_name from mas_salesforce b   where a.sf_code=b.sf_code ) sf_name " +
                  " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0" +
                  " order by Territory_Name";




            //     strQry = " SELECT a.Territory_Code, trim(a.Territory_Name) Territory_Name, a.Territory_SName, " +
            //" case when a.Territory_Cat=1 then 'HQ' " +
            //" else case when a.Territory_Cat=2 then 'EX' " +
            //" else case when a.Territory_Cat=3 then 'OS' " +
            //" else 'OS-EX'  " +
            //" end end end as Territory_Cat, " +
            //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and a.SF_Code = b.Sf_Code " +
            //"  and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
            //"  (select COUNT(b.Chemists_Code) from Mas_Chemists b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
            //" and b.Chemists_Active_Flag=0) Chemists_Count, " +
            //" (select COUNT(b.Hospital_Code) from Mas_Hospital b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
            //" and b.Hospital_Active_Flag =0) Hospital_Count, " +
            //" (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
            //" and b.UnListedDr_Active_Flag=0) UnListedDR_Count, " +
            //" (select sf_name from mas_salesforce b where a.sf_code=b.sf_code ) sf_name,Alias_Name,Town_City,Territory_Visit " +
            //" FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0 order by trim(Territory_Name)";

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
        public DataSet getTerritory_New(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

           
            strQry = " SELECT a.Territory_Code, trim(a.Territory_Name) Territory_Name, a.Territory_SName, " +
       " case when a.Territory_Cat=1 then 'HQ' " +
       " else case when a.Territory_Cat=2 then 'EX' " +
       " else case when a.Territory_Cat=3 then 'OS' " +
       " else 'OS-EX'  " +
       " end end end as Territory_Cat, " +
       " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and a.SF_Code = b.Sf_Code " +
       "  and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
       "  (select COUNT(b.Chemists_Code) from Mas_Chemists b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
       " and b.Chemists_Active_Flag=0) Chemists_Count, " +
       " (select COUNT(b.Hospital_Code) from Mas_Hospital b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
       " and b.Hospital_Active_Flag =0) Hospital_Count, " +
       " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
       " and b.UnListedDr_Active_Flag=0) UnListedDR_Count, " +
       " (select sf_name from mas_salesforce b where a.sf_code=b.sf_code ) sf_name,Alias_Name,Town_City,Territory_Visit,Territory_Allowance " +
       " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0 order by trim(Territory_Name)";

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

        // Sorting For Territory Creation

        public DataTable getTerritory_DataTable(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count,Alias_Name, Town_City  " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND a.territory_active_flag=0 ";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        } 
        // Sorting For Territory List 
        public DataTable getTerritorylist_DataTable(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            //strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
            //         " case when a.Territory_Cat=1 then 'HQ' " +
            //         " else case when a.Territory_Cat=2 then 'EX' " +
            //         " else case when a.Territory_Cat=3 then 'OS' " +
            //         " else 'OS-EX' " +
            //         " end end end as Territory_Cat, " +
            //         " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where convert(varchar(10),a.Territory_Code)=b.Territory_Code " +
            //         " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
            //         " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
            //         " and b.Chemists_Active_Flag=0) Chemists_Count, " +
            //         " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
            //         " and b.Hospital_Active_Flag =0) Hospital_Count, " +
            //         " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
            //         " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
            //         " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND a.territory_active_flag=0 ";
            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
             " case when a.Territory_Cat=1 then 'HQ' " +
             " else case when a.Territory_Cat=2 then 'EX' " +
             " else case when a.Territory_Cat=3 then 'OS' " +
             " else 'OS-EX'  " +
             " end end end as Territory_Cat, " +
             " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and a.SF_Code = b.Sf_Code " +
             "  and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
             "  (select COUNT(b.Chemists_Code) from Mas_Chemists b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
             " and b.Chemists_Active_Flag=0) Chemists_Count, " +
             " (select COUNT(b.Hospital_Code) from Mas_Hospital b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
             " and b.Hospital_Active_Flag =0) Hospital_Count, " +
             " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where  CHARINDEX(cast(a.Territory_Code as varchar),b.Territory_Code) > 0 and  a.SF_Code = b.Sf_Code " +
             " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
             " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0 " +
             " order by Territory_SNo";
            
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        //Sorting for G.No
        public DataTable getTerr_DtTable(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat " +               
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }

        //End
     
        public DataSet getTerritory_Det(string sf_code, string Territory_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,Alias_Name,Hill_Station,Fare,Exp_Allow_Cat,Additional_Allowance ,Territory_Visit   " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND Territory_Code='" + Territory_Code + "' and territory_active_flag=0 ";
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
        public DataTable getTerr_DistAllowTable(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " select to_code,Distance_in_kms,((Distance_in_kms*2)*FareKm_allowance)fare,FareKm_allowance,case when TR.Territory_Cat=1 then HQ_Allowance " +
                     "else case when TR.Territory_Cat=2 then EX_HQ_Allowance " +
                      "else case when TR.Territory_Cat=3 then OS_Allowance " +
                     "else OS_Allowance " +
                      "end end end as Allow from Mas_Distance_Fixation D " +
"inner join mas_territory_creation TR on D.to_code=TR.territory_code and D.sf_code=TR.sf_code " +
"left outer join mas_allowance_fixation S on D.sf_code=S.sf_code where D.sf_code='" + sf_code + "'";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        public DataTable getTerr_AllowTable(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " select territory_code,case when TR.Territory_Cat=1 then HQ_Allowance " +
                     "else case when TR.Territory_Cat=2 then EX_HQ_Allowance " +
                      "else case when TR.Territory_Cat=3 then OS_Allowance " +
                     "else OS_Allowance " +
                      "end end end as Allow from mas_territory_creation TR " +
"left outer join mas_allowance_fixation S on TR.sf_code=S.sf_code where TR.sf_code='" + sf_code + "'";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }


        public DataTable getTerr_drscntMultiple(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " select count(listeddrcode)cnt,TR.territory_code from (" +
"select count(listeddrcode)cnt,listeddrcode,D.territory_code,D.division_code from Mas_ListedDr D inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),D.territory_code)>0 and left(D.territory_code,2)<>'0~' and left(D.territory_code,1)<>'~' and listeddr_active_flag=0 " +
"and D.sf_code='" + sf_code + "' and D.division_code=TR.division_code group by listeddrcode,D.territory_code,D.division_code having count(listeddrcode)>1) as dd " +
"inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),DD.territory_code)>0 and TR.division_code=dd.division_code group by TR.territory_code";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }


        public DataTable getSpec_drscntMultiple(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " select count(listeddrcode)cnt,TR.Doc_Special_Code,2 mode from (" +
"select count(listeddrcode)cnt,listeddrcode,D.Doc_Special_Code,D.division_code from Mas_ListedDr D inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),D.territory_code)>0 and left(D.territory_code,2)<>'0~' and left(D.territory_code,1)<>'~' and listeddr_active_flag=0 " +
"and D.sf_code='" + sf_code + "' and D.division_code=TR.division_code group by listeddrcode,D.Doc_Special_Code,D.division_code having count(listeddrcode)>1) as dd " +
"inner join Mas_Doctor_Speciality TR on charindex(cast(TR.Doc_Special_Code as varchar),DD.Doc_Special_Code)>0 and TR.division_code=dd.division_code group by TR.Doc_Special_Code";

            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        public DataTable getSpec_drscntSingle(string speccode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = "select count(listeddrcode)cnt,TR.Doc_Special_Code,1 mode from (" +
"select count(listeddrcode)cnt,listeddrcode,D.Doc_Special_Code,D.division_code from Mas_ListedDr D inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),D.territory_code)>0 and left(D.territory_code,2)<>'0~' and left(D.territory_code,1)<>'~' and listeddr_active_flag=0 " +
"and D.sf_code='" + sf_code + "' and D.division_code=TR.division_code group by listeddrcode,D.Doc_Special_Code,D.division_code having count(listeddrcode)<=1) as dd " +
"inner join Mas_Doctor_Speciality TR on charindex(cast(TR.Doc_Special_Code as varchar),DD.Doc_Special_Code)>0 and TR.division_code=dd.division_code and TR.Doc_Special_Code='" + speccode + "' group by TR.Doc_Special_Code";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        public DataTable getTerr_drscntSingle(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " select count(listeddrcode)cnt,TR.territory_code from (" +
"select count(listeddrcode)cnt,listeddrcode,D.territory_code,D.division_code from Mas_ListedDr D inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),D.territory_code)>0 and left(D.territory_code,2)<>'0~' and left(D.territory_code,1)<>'~' and listeddr_active_flag=0 " +
"and D.sf_code='" + sf_code + "' and D.division_code=TR.division_code group by listeddrcode,D.territory_code,D.division_code having count(listeddrcode)<=1) as dd " +
"inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),DD.territory_code)>0 and TR.division_code=dd.division_code group by TR.territory_code";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        public DataTable getTerr_drscntTot(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " select count(listeddrcode)cnt,TR.territory_code from (" +
"select count(listeddrcode)cnt,listeddrcode,D.territory_code,D.division_code from Mas_ListedDr D inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),D.territory_code)>0 and left(D.territory_code,2)<>'0~' and left(D.territory_code,1)<>'~' and listeddr_active_flag=0 " +
"and D.sf_code='" + sf_code + "' and D.division_code=TR.division_code group by listeddrcode,D.territory_code,D.division_code) as dd " +
"inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),DD.territory_code)>0 and TR.division_code=dd.division_code group by TR.territory_code";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        public DataSet getTerritory_Cnthq(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT count(territory_code)HQ,(select count(territory_code) from Mas_Territory_Creation where sf_code='" + sf_code + "' and territory_Cat=2 AND territory_active_flag=0)EX,(select count(territory_code) from Mas_Territory_Creation where sf_code='" + sf_code + "' and territory_Cat=3 AND territory_active_flag=0)OS FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' and Territory_Cat=1 AND territory_active_flag=0 ";
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
        public DataSet getmod()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select '2**' mode union select '1*' mode";
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
        public int getTerritory_Al(string Territory_Code, string Alias_Name, string Territory_SName)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                             " SET Alias_Name='" + Alias_Name + "', Territory_SName='" + Territory_SName + "' " +
                             " WHERE Territory_Code = '" + Territory_Code + "' and territory_active_flag=0  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Terr_SlNO(string Territory_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET Territory_SNo = '" + Sno + "', " +
                        " LastUpdt_Date = getdate() " +
                         " WHERE Territory_Code = '" + Territory_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getTerritory_Slno(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat " +                   
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 "+
                     " ORDER BY Territory_SNo"; ;
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

        //Changes done by Saravana     

        //public DataSet getWorkAreaName()
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsProCat = null;

        //    strQry = "select wrk_area_Name from Admin_Setups";
        //    try
        //    {
        //        dsProCat = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsProCat;
        //}
        //End
        public bool RecordExist(string Territory_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_Name) FROM Mas_Territory_Creation WHERE Territory_Name='" + Territory_Name + "' and sf_code = '"+sf_code+"'";
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

        public int RecordAdd(string Territory_Name, string Alias_SName, string Territory_Type, string sf_code,string Territory_Visit)
        {
            int iReturn = -1;
            
            if (!RecordExist(Territory_Name, sf_code))
            {
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Territory_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation ";
                Territory_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                         " SF_Code,Territory_Active_Flag,Created_date,Alias_Name,Territory_Visit) " +
                         " VALUES('" + Territory_Code + "', '" + Territory_Name + "', '" + Territory_Type + "', '', " +
                         " '" + Division_Code + "', '" + sf_code + "', 0, getdate(),'" + Alias_SName + "','"+ Territory_Visit + "')";                                              
                
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
        public int delete_lat_long(string lat, string Long, string sf_code, string terr_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from map_GEO_Customers where Cust_Code in( select a.ListedDrCode from Mas_ListedDr a,Mas_Territory_Creation b " +
                         " where a.Territory_Code='" + terr_code + "' and b.SF_Code='" + sf_code + "')";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;
        }
        public int Add_lat_long(string lat, string Long, string terr_code, string sf_code, string terr_name)
        {
            int iReturn = -1;

            if (!RecordExist(lat, Long))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    strQry = "select division_code from Mas_Territory_Creation where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);
                    //int Listed_dr_code = -1;
                    //strQry = "select ListedDrCode from Mas_ListedDr where Territory_Code='" + terr_code + "' and Sf_Code = '" + sf_code + "'";
                    //Listed_dr_code = db.Exec_Scalar(strQry);

                    strQry = "  insert into map_GEO_Customers (mapid,lat,long,Cust_Code,StatFlag,Division_Code) " +
                             "  select (select max(mapid) from map_GEO_Customers)+ ROW_NUMBER() OVER (ORDER BY (select 1)),'" + lat + "','" + Long + "', ListedDrCode,'0', " +
                             "  division_code from Mas_ListedDr where sf_code='" + sf_code + "' and territory_code='" + terr_code + "'";

                    iReturn = db.ExecQry(strQry);
                //    strQry = "  insert into map_GEO_Customers (mapid,lat,long,Cust_Code,StatFlag,Division_Code) " +
                //"  select (select max(mapid) from map_GEO_Customers)+ ROW_NUMBER() OVER (ORDER BY (select 1)),'" + lat1 + "','" + long1 + "', ListedDrCode,'0', " +
                //"  division_code from Mas_ListedDr where sf_code='" + sf_code + "' and territory_code='" + terr_code + "'";

                //    iReturn = db.ExecQry(strQry);
                    strQry = "insert into Mas_Territory_Latlong(Sf_code,Territory_Code,Territory_Name,Lat,Long,Division_Code,Created_Date) " +
                             "select distinct '" + sf_code + "','" + terr_code + "','" + terr_name + "','" + lat + "','" + Long + "',division_code,getdate() from Mas_ListedDr where sf_code='" + sf_code + "' and territory_code='" + terr_code + "'";

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
        public int RecordCount(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_Code) FROM Mas_Territory_Creation WHERE Sf_Code = '" + sf_code + "' and Territory_Active_Flag = 0";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Changes done by Saravanan
        public DataSet getTerrritoryView(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select SF_Name as sfName,Sf_HQ, sf_Designation_Short_Name from Mas_Salesforce where Sf_Code='" + Sf_Code + "'";          

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
        //Changes done by Saravana     
        public DataSet getWorkAreaName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
                strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups where Division_Code in(" + div_code + ")";
            }
            else
            {

                strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups where Division_Code='" + div_code + "'";
            }

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
        
        //public DataSet getWorkAreaName()
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsProCat = null;

        //    strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups ";

        //    try
        //    {
        //        dsProCat = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsProCat;
        //}
        //End
        public DataSet getSFCode(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as sf_code, '---Select---' as Sf_Name " +
                     " UNION " +
                     " select sf_code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq + ' - '+sf_emp_id as sf_Name from Mas_Salesforce where (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and sf_type=1 and sf_status != 2 ";
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
        
        public DataSet getHQ_Dist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                       " case when a.Territory_Cat=1 then 'HQ' " +
                       " else case when a.Territory_Cat=2 then 'EX' " +
                       " else case when a.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Territory_Cat, " +
                       " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                       " and b.ListedDr_Active_Flag=0) ListedDR_Count  " +
                       " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' and  Territory_Cat=1  and a.Territory_Active_Flag=0" +

                       " order by Territory_Name";
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
        public DataSet getOS_Dist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name,  " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,a.OS_Distance,c.Sf_HQ " +
                //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                     //" and b.ListedDr_Active_Flag=0) ListedDR_Count " +
                     " FROM  Mas_Territory_Creation a, Mas_Salesforce c where a.Sf_Code = '" + sf_code + "' and Territory_Cat=3  and a.Territory_Active_Flag=0" +
                      " and a.Sf_Code=c.Sf_Code " +
                     " order by Territory_Name";
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
        public DataSet getEX_Dist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name,  " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,a.Ex_Distance, c.Sf_HQ" +
                //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                     //" and b.ListedDr_Active_Flag=0) ListedDR_Count " +
                     " FROM  Mas_Territory_Creation a, Mas_Salesforce c where a.Sf_Code = '" + sf_code + "' and Territory_Cat=2  and a.Territory_Active_Flag=0" +
                      " and a.Sf_Code=c.Sf_Code " +
                     " order by Territory_Name";
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

        public DataSet getOSEX_Dist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT * FROM( SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName,case when a.Territory_Cat=1 then 'HQ' "+ 
            //         " else case when a.Territory_Cat=2 then 'EX'  else case when a.Territory_Cat=3 then 'OS' "+
            //          " else 'OSEX'  end end end as Territory_Cat,  (select COUNT(b.ListedDrCode) "+                      
            //           " from Mas_ListedDr b where a.Territory_Code=b.Territory_Code  and "+                       
            //           " b.ListedDr_Active_Flag=0) ListedDR_Count   FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'" +                
            //           " and  (Territory_Cat=3 or Territory_Cat=4) and a.Territory_Active_Flag=0 ) as a PIVOT ( max(a.Territory_Name)  FOR [Territory_Cat] IN (OS,OSEX)) AS pivot1";

            strQry = ";with terr as (select * from Mas_Territory_Creation where Territory_Cat=3 and Territory_Active_Flag=0),terr1 as" +
                   " (select * from Mas_Territory_Creation where Territory_Cat=4 and Territory_Active_Flag=0) select f.SF_Code,f.Territory_Name as Terr_From,e.Territory_Name as Terr_To,e.OSEX_Distance from terr1 e, " +
                   " terr f where f.SF_Code='" + sf_code + "' and e.sf_code='" + sf_code + "'  ";


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

        public int get_ExKms(string sf_code, string EX_Distance, string Territory_Name)
        {
            int iReturn = -1;            
            try
            {                
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET EX_Distance = '" + EX_Distance + "' " +
                         " WHERE Sf_Code = '" + sf_code + "' and Territory_Name='" + Territory_Name + "'  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int get_OsKms(string sf_code, string OS_Distance, string Territory_Name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET OS_Distance = '" + OS_Distance + "' " +
                         " WHERE Sf_Code = '" + sf_code + "' and Territory_Name='" + Territory_Name + "'  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int get_OsExKms(string sf_code, string OSEX_Distance)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET OSEX_Distance = '" + OSEX_Distance + "' " +
                         " WHERE Sf_Code = '" + sf_code + "'   ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
      
      //Changes done by Priya

         public DataSet getSfname_Desig(string Sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select a.sf_name,a.Sf_HQ,a.sf_emp_id,b.Designation_Name,c.Division_Name,a.state_code " +                     
                     " from Mas_Salesforce a,Mas_SF_Designation b, Mas_Division c where a.Sf_Code= '" + Sf_Code + "' and c.division_code = '"+div_code+"'" +
                     " and a.Designation_Code=b.Designation_Code ";

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
         // Changes done by Saravanan 20/03/15
         public int WrkTypeMGR_Expense_Update(string Worktype_Name, string Expense_Type, string Div_Code, string Type, string Work_Type_Code)
         {
             int iReturn = -1;
             try
             {
                 DB_EReporting db = new DB_EReporting();
                 DataSet dsData = new DataSet();

                 strQry = "select COUNT(*) as [Value] from Worktype_Allowance_Setup where work_type_Name='" + Worktype_Name + "' and division_Code='" + Div_Code + "' and TYPE='" + Type + "'";

                 dsData = db.Exec_DataSet(strQry);

                 if (dsData.Tables[0].Rows[0]["Value"].ToString() == "0")
                 {
                     strQry = "Insert into Worktype_Allowance_Setup (Work_Type_Name,Type,Division_code,Allow_Type,Work_Type_Code)values('" + Worktype_Name + "','" + Type + "','" + Div_Code + "','" + Expense_Type + "','" + Work_Type_Code + "')";

                     iReturn = db.ExecQry(strQry);
                 }
                 else
                 {
                     strQry = "Update Worktype_Allowance_Setup set Allow_type='" + Expense_Type + "',Work_Type_Code='" + Work_Type_Code + "' where Work_Type_Name='" + Worktype_Name + "' and Division_Code='" + Div_Code + "' and TYPE='" + Type + "'";

                     iReturn = db.ExecQry(strQry);
                 }

                 strQry = "Update Mas_WorkType_Mgr set Expense_Type='" + Expense_Type + "' where Worktype_Name_M='" + Worktype_Name + "' and division_code='" + Div_Code + "'";

                 iReturn = db.ExecQry(strQry);

             }
             catch (Exception ex)
             {

             }
             return iReturn;
         }

         public int ExpenseParameter_Insert(string Exp_Parameter_Name, string Exp_Type, string Div_Code, string txtFixedAmount, string Level_Value, string txtFixedAmount1, string chkbselvl, string chkmgrlvl)
         {
             int iReturn = -1;

             if (!ExpParameter_RecordExist(Exp_Parameter_Name, Div_Code, Level_Value))
             {
                 try
                 {
                     DB_EReporting db = new DB_EReporting();

                     int Parameter_Code = 0;

                     strQry = "select isnull(max(Expense_Parameter_Code),0)+1 Expense_Parameter_Code from Fixed_Variable_Expense_Setup ";
                     Parameter_Code = db.Exec_Scalar(strQry);

                     //strQry = "select  count(Expense_Parameter_Code) as Expense_Parameter_Code from Fixed_Variable_Expense_Setup where Division_code='" + Div_Code + "'";
                     //dsCount = db.Exec_DataSet(strQry);


                     strQry = "Insert into Fixed_Variable_Expense_Setup (Expense_Parameter_Code,Type," +
                                 "Expense_Parameter_Name,Division_Code,Param_type,Fixed_Amount,Fixed_Amount1,Base_Level,MGR_Lines) values('" + Parameter_Code + "','" + Level_Value + "'," +
                                 "'" + Exp_Parameter_Name + "','" + Div_Code + "','" + Exp_Type + "','" + txtFixedAmount + "','" + txtFixedAmount1 + "','" + chkbselvl + "','" + chkmgrlvl + "')";


                     iReturn = db.ExecQry(strQry);

                 }
                 catch (Exception ex)
                 {

                 }
             }
             else
             {
                 iReturn = -2;
             }
             return iReturn;

         }

        public DataSet getExp_Parameter()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Expense_Type=1 then 'Fixed' "+
                     "when Expense_Type=2  then 'Variable' "+
                     "end Expense_Type from Mas_Expense_Parameter";

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

        public bool ExpParameter_RecordExist(string Exp_Parameter_Name, string Div_Code, string Level_Value)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Expense_Parameter_Name) Expense_Parameter_Name from Fixed_Variable_Expense_Setup where Expense_Parameter_Name='" + Exp_Parameter_Name + "' and division_Code='" + Div_Code + "' and TYPE='" + Level_Value + "' ";

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

      

  public int ExpRecordUpdate(string Exp_Parameter_Code, string ExpParameter_Name, string Exp_Type, string Fixed_Amount, string Fixed_Amount1, string Base_Level, string MGR_Lines)
        {
            int iReturn = -1;
            //int InsertColumn = 0;
            //if (!RecordExist(ExpParameter_Name))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();
                int x = 0;
                int y = 0;
                int z = 0;
                int w = 0;
                x = Int32.Parse(Fixed_Amount);
                y = Int32.Parse(Fixed_Amount1);
                z = Int32.Parse(Base_Level);
                w = Int32.Parse(MGR_Lines);
                if (x > 0)
                {
                    z = 1;
                }
                else
                {
                    z = 0;
                }
                if (y > 0)
                {
                    w = 1;
                }
                else
                {
                    w = 0;
                }
                strQry = "Update Fixed_Variable_Expense_Setup set Expense_Parameter_Name='" + ExpParameter_Name + "',Param_type='" + Exp_Type + "',Fixed_Amount='" + Fixed_Amount + "',Fixed_Amount1='" + Fixed_Amount1 + "' where Expense_Parameter_Code='" + Exp_Parameter_Code + "'";

                iReturn = db.ExecQry(strQry);


                //DataSet dsTerr = null;
                //DataTable dt = null;
                //strQry = "SELECT REPLACE(LTRIM(RTRIM(Expense_Parameter_Name)), SPACE(1), '_' ) Expense_Parameter_Name " +
                //          " FROM Mas_Expense_Parameter where Expense_Type=1 and Expense_Parameter_Code='" + Exp_Parameter_Code + "'";
                //dsTerr = db.Exec_DataSet(strQry);                   

                //dt = dsTerr.Tables[0];

                //for (int i = 0; i < dsTerr.Tables[0].Rows.Count; i++)
                //{
                //    string strColumn = dt.Rows[i].Field<string>(0);

                //    //strQry = "ALTER TABLE Mas_Allowance_Fixation DROP COLUMN" + strColumn + "";
                //    //InsertColumn = db.ExecQry(strQry);

                //    strQry = "Alter table Mas_Allowance_Fixation add " + strColumn + " float";
                //    InsertColumn = db.ExecQry(strQry);

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


        public int Exp_Parameter_RecordDelete(int Expense_Parameter_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Expense_Parameter WHERE Expense_Parameter_Code = '" + Expense_Parameter_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public DataSet getExp_ParameterBL(int BL_workcode, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Param_type='F' then 'Fixed' " +
                     "when Param_type='V'  then 'Variable' when Param_type='L'  then 'Variable with Limit'  " +
                     "end Param_type,Fixed_Amount,Param_type,Fixed_Amount1,Base_Level,MGR_Lines from Fixed_Variable_Expense_Setup where division_Code='" + div_Code + "'";

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
        public DataSet getExp_ParameterMGR(int BL_workcode, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Type=1 then 'Fixed' " +
                      "when Type=2  then 'Variable' " +
                      "end Param_type,Fixed_Amount from Fixed_Variable_Expense_Setup where Type='" + BL_workcode + "' and division_Code='" + div_Code + "'";

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

        public DataSet getExp_Managers(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select '0' Sf_Code,'---Select---' Sf_Name union select a.Sf_Code,a.Sf_HQ+' - '+Sf_Name+' - '+b.Designation_Short_Name + '-' +a.sf_emp_id  Sf_Name from mas_salesforce a," +
                     " Mas_SF_Designation b" +
                     " where a.Designation_Code=b.Designation_Code and sf_TP_Active_Flag in (0,2) " +
                     " and Sf_Code in  (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and  " +
                     " (Division_Code like '" + div_code + ',' + "%'  or  Division_Code like '%" + ',' + div_code + ',' + "%' ))";

          

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

        public DataSet getExp_FixedType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            int iCount=0;

            strQry = "select count(Expense_Parameter_Name) from Mas_Expense_Parameter where Expense_Type=1";
            iCount = db_ER.Exec_Scalar(strQry);

            if (iCount != 0)
            {
                strQry = "DECLARE @cols AS NVARCHAR(MAX),    @query  AS NVARCHAR(MAX) select @cols = STUFF((SELECT ',' + QUOTENAME(Expense_Parameter_Name) " +
                         " from Mas_Expense_Parameter where Expense_Type=1 " +
                         " group by Expense_Parameter_Name, Expense_Parameter_Code " +
                         " order by Expense_Parameter_Code " +
                         " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                         "set @query = N'SELECT ' + @cols + N' from (select Expense_Parameter_Code, Expense_Parameter_Name from Mas_Expense_Parameter ) x " +
                         "pivot " +
                         "(max(Expense_Parameter_Code) " +
                         "for Expense_Parameter_Name in (' + @cols + N') ) p ' " +
                         "exec sp_executesql @query;";

                //strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";

            }
            else
            {
                strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";
            }

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
        public DataSet getExp_FixedType1(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            int iCount = 0;
            // strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            //int Division_Code = db_ER.Exec_Scalar(strQry);
            strQry = "select  count(Expense_Parameter_Name) from Fixed_Variable_Expense_Setup where division_code=" + Division_Code + " and param_type='F'";
            iCount = db_ER.Exec_Scalar(strQry);

            if (iCount != 0)
            {
                strQry = "DECLARE @cols AS NVARCHAR(MAX),    @query  AS NVARCHAR(MAX) select @cols = STUFF((SELECT ',' + QUOTENAME(Expense_Parameter_Name) " +
                         " from Fixed_Variable_Expense_Setup where division_code=" + Division_Code + " and param_type='F' " +
                         " group by Expense_Parameter_Name, Expense_Parameter_Code " +
                         " order by Expense_Parameter_Code " +
                         " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                         "set @query = N'SELECT ' + @cols + N' from (select Expense_Parameter_Code, Expense_Parameter_Name from Fixed_Variable_Expense_Setup ) x " +
                         "pivot " +
                         "(max(Expense_Parameter_Code) " +
                         "for Expense_Parameter_Name in (' + @cols + N') ) p ' " +
                         "exec sp_executesql @query;";

                //strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";

            }
            else
            {
                strQry = "select Expense_Parameter_Name from Fixed_Variable_Expense_Setup where division_code=" + Division_Code + " and param_type='F'";
            }

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

        public DataSet getWorkType_allow_type(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            int iCount = 0;
            // strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            //int Division_Code = db_ER.Exec_Scalar(strQry);
            strQry = "select  count(Work_type_Name) from Worktype_Allowance_Setup where division_code=" + Division_Code + " and work_type_code='VA' and Type='1'";
            iCount = db_ER.Exec_Scalar(strQry);

            if (iCount != 0)
            {
                strQry = "DECLARE @cols AS NVARCHAR(MAX),    @query  AS NVARCHAR(MAX) select @cols = STUFF((SELECT ',' + QUOTENAME(Work_type_Name) " +
                         " from Worktype_Allowance_Setup where division_code=" + Division_Code + " and work_type_code='VA' and Type='1' " +
                         " group by Work_type_Name, WorkType_Code_B " +
                         " order by WorkType_Code_B " +
                         " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                         "set @query = N'SELECT ' + @cols + N' from (select WorkType_Code_B, Work_type_Name from Worktype_Allowance_Setup where division_code=" + Division_Code + " and work_type_code=''VA'' and Type=''1'' ) x " +
                         "pivot " +
                         "(max(WorkType_Code_B) " +
                         "for Work_type_Name in (' + @cols + N') ) p ' " +
                         "exec sp_executesql @query;";

                //strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";

            }
            else
            {
                strQry = "select Work_type_Name from Worktype_Allowance_Setup where division_code=" + Division_Code + " and work_type_code='VA' and Type='1'";
            }

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
        //public int WrkTypeBase_Expense_Update(string Worktype_Name, string Expense_Type, string Div_Code, string Type, string Work_Type_Code, double fixed_amount, double fixed_amount1, double fixed_amount2, string code)
        //{
        //    int iReturn = -1;
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();
        //        DataSet dsData = new DataSet();

        //        strQry = "select COUNT(*) as [Value] from Worktype_Allowance_Setup where work_type_Name='" + Worktype_Name + "' and division_Code='" + Div_Code + "' and TYPE='" + Type + "'";

        //        dsData = db.Exec_DataSet(strQry);


        //        if (dsData.Tables[0].Rows[0]["Value"].ToString() == "0")
        //        {
        //            strQry = "Insert into Worktype_Allowance_Setup (WorkType_Code_B,Work_Type_Name,Type,Division_code,Allow_Type,Work_Type_Code,fixed_amount,fixed_amount1,fixed_amount2)values('" + code + "','" + Worktype_Name + "','" + Type + "','" + Div_Code + "','" + Expense_Type + "','" + Work_Type_Code + "'," + fixed_amount + "," + fixed_amount1 + "," + fixed_amount2 + ")";

        //            iReturn = db.ExecQry(strQry);
        //        }
        //        else
        //        {
        //            strQry = "Update Worktype_Allowance_Setup set WorkType_Code_B='" + code + "', Allow_type='" + Expense_Type + "',Work_Type_Code='" + Work_Type_Code + "',fixed_amount=" + fixed_amount + ",fixed_amount1=" + fixed_amount1 + ",fixed_amount2=" + fixed_amount2 + " where Work_Type_Name='" + Worktype_Name + "' and Division_Code='" + Div_Code + "' and TYPE='" + Type + "'";

        //            iReturn = db.ExecQry(strQry);
        //        }

        //        strQry = "Update Mas_WorkType_BaseLevel set Expense_Type='" + Expense_Type + "',fixed_amount=" + fixed_amount + ",fixed_amount1=" + fixed_amount1 + ",fixed_amount2=" + fixed_amount2 + " where Worktype_Name_B='" + Worktype_Name + "' and Division_Code='" + Div_Code + "'";

        //        iReturn = db.ExecQry(strQry);


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return iReturn;

        //}
        public int WrkTypeBase_Expense_Update_MGR(string Worktype_Name, string Expense_Type, string Div_Code, string Type, string Work_Type_Code, double fixed_amount, double fixed_amount1, double fixed_amount2, double fixed_amount3, double fixed_amount4, double fixed_amount5, double fixed_amount6, double fixed_amount7, double fixed_amount8, double fixed_amount9, double fixed_amount10, double fixed_amount11, double fixed_amount12, double fixed_amount13, double fixed_amount14, double fixed_amount15, double fixed_amount16, double fixed_amount17, string code, string Desig)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();

                strQry = "select COUNT(*) as [Value] from Worktype_Allowance_Setup_Designation where work_type_Name='" + Worktype_Name + "' and division_Code='" + Div_Code + "' and TYPE='" + Type + "' and Desig_code='" + Desig + "'";

                dsData = db.Exec_DataSet(strQry);


                if (dsData.Tables[0].Rows[0]["Value"].ToString() == "0")
                {
                    strQry = "Insert into Worktype_Allowance_Setup_Designation (WorkType_Code_B,Work_Type_Name,Type,Division_code,Allow_Type,Work_Type_Code,fixed_amount,fixed_amount1,fixed_amount2,fixed_amount3,fixed_amount4,fixed_amount5,fixed_amount6,fixed_amount7,fixed_amount8,fixed_amount9,fixed_amount10,fixed_amount11,fixed_amount12,fixed_amount13,fixed_amount14,fixed_amount15,fixed_amount16,fixed_amount17,Desig_code)values('" + code + "','" + Worktype_Name + "','" + Type + "','" + Div_Code + "','" + Expense_Type + "','" + Work_Type_Code + "'," + fixed_amount + "," + fixed_amount1 + "," + fixed_amount2 + "," + fixed_amount3 + "," + fixed_amount4 + "," + fixed_amount5 + "," + fixed_amount6 + "," + fixed_amount7 + "," + fixed_amount8 + "," + fixed_amount9 + "," + fixed_amount10 + "," + fixed_amount11 + "," + fixed_amount12 + "," + fixed_amount13 + "," + fixed_amount14 + "," + fixed_amount15 + "," + fixed_amount16 + "," + fixed_amount17 + ",'" + Desig + "')";
                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "Update Worktype_Allowance_Setup_Designation set WorkType_Code_B='" + code + "', Allow_type='" + Expense_Type + "',Work_Type_Code='" + Work_Type_Code + "',fixed_amount=" + fixed_amount + ",fixed_amount1=" + fixed_amount1 + ",fixed_amount2=" + fixed_amount2 + ",fixed_amount3=" + fixed_amount3 + ",fixed_amount4=" + fixed_amount4 + ",fixed_amount5=" + fixed_amount5 + ", " +
                    "fixed_amount6=" + fixed_amount6 + ",fixed_amount7=" + fixed_amount7 + ",fixed_amount8=" + fixed_amount8 + ",fixed_amount9=" + fixed_amount9 + ",fixed_amount10=" + fixed_amount10 + ",fixed_amount11=" + fixed_amount11 + ",fixed_amount12=" + fixed_amount12 + ",fixed_amount13=" + fixed_amount13 + " ,fixed_amount14=" + fixed_amount14 + ",fixed_amount15=" + fixed_amount15 + ",fixed_amount16=" + fixed_amount16 + ",fixed_amount17=" + fixed_amount17 + " " +
                    "where Work_Type_Name='" + Worktype_Name + "' and Division_Code='" + Div_Code + "' and TYPE='" + Type + "' and desig_code='" + Desig + "'";
                    iReturn = db.ExecQry(strQry);
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int WrkTypeBase_Expense_Update(string Worktype_Name, string Expense_Type, string Div_Code, string Type, string Work_Type_Code, double fixed_amount, double fixed_amount1, double fixed_amount2, double fixed_amount3, double fixed_amount4, double fixed_amount5, double fixed_amount6, double fixed_amount7, double fixed_amount8, double fixed_amount9, double fixed_amount10, double fixed_amount11, double fixed_amount12, double fixed_amount13, double fixed_amount14, double fixed_amount15, double fixed_amount16, double fixed_amount17, string code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();
                if (Type == "1")
                {
                    strQry = "select COUNT(*) as [Value] from Worktype_Allowance_Setup where work_type_Name='" + Worktype_Name + "' and division_Code='" + Div_Code + "' and TYPE='" + Type + "'";

                    dsData = db.Exec_DataSet(strQry);


                    if (dsData.Tables[0].Rows[0]["Value"].ToString() == "0")
                    {
                        strQry = "Insert into Worktype_Allowance_Setup (WorkType_Code_B,Work_Type_Name,Type,Division_code,Allow_Type,Work_Type_Code,fixed_amount,fixed_amount1,fixed_amount2,fixed_amount3,fixed_amount4,fixed_amount5,fixed_amount6,fixed_amount7,fixed_amount8,fixed_amount9,fixed_amount10,fixed_amount11,fixed_amount12,fixed_amount13,fixed_amount14,fixed_amount15,fixed_amount16,fixed_amount17)values('" + code + "','" + Worktype_Name + "','" + Type + "','" + Div_Code + "','" + Expense_Type + "','" + Work_Type_Code + "'," + fixed_amount + "," + fixed_amount1 + "," + fixed_amount2 + "," + fixed_amount3 + "," + fixed_amount4 + "," + fixed_amount5 + "," + fixed_amount6 + "," + fixed_amount7 + "," + fixed_amount8 + "," + fixed_amount9 + "," + fixed_amount10 + "," + fixed_amount11 + "," + fixed_amount12 + "," + fixed_amount13 + "," + fixed_amount14 + "," + fixed_amount15 + "," + fixed_amount16 + "," + fixed_amount17 + ")";

                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {
                        strQry = "Update Worktype_Allowance_Setup set WorkType_Code_B='" + code + "', Allow_type='" + Expense_Type + "',Work_Type_Code='" + Work_Type_Code + "',fixed_amount=" + fixed_amount + ",fixed_amount1=" + fixed_amount1 + ",fixed_amount2=" + fixed_amount2 + ",fixed_amount3=" + fixed_amount3 + ",fixed_amount4=" + fixed_amount4 + ",fixed_amount5=" + fixed_amount5 + "," +
                        " fixed_amount6=" + fixed_amount6 + ",fixed_amount7=" + fixed_amount7 + ",fixed_amount8=" + fixed_amount8 + ",fixed_amount9=" + fixed_amount9 + ",fixed_amount10=" + fixed_amount10 + ",fixed_amount11=" + fixed_amount11 + ",fixed_amount12=" + fixed_amount12 + ",fixed_amount13=" + fixed_amount13 + " ,fixed_amount14=" + fixed_amount14 + ",fixed_amount15=" + fixed_amount15 + ",fixed_amount16=" + fixed_amount16 + ",fixed_amount17=" + fixed_amount17 + "" +
                       " where Work_Type_Name='" + Worktype_Name + "' and Division_Code='" + Div_Code + "' and TYPE='" + Type + "'";

                        iReturn = db.ExecQry(strQry);
                    }

                    strQry = "Update Mas_WorkType_BaseLevel set Expense_Type='" + Expense_Type + "',fixed_amount=" + fixed_amount + ",fixed_amount1=" + fixed_amount1 + ",fixed_amount2=" + fixed_amount2 + ",fixed_amount3=" + fixed_amount3 + ",fixed_amount4=" + fixed_amount4 + ",fixed_amount5=" + fixed_amount5 + ", " +
                        "fixed_amount6=" + fixed_amount6 + ",fixed_amount7=" + fixed_amount7 + ",fixed_amount8=" + fixed_amount8 + ",fixed_amount9=" + fixed_amount9 + ",fixed_amount10=" + fixed_amount10 + ",fixed_amount11=" + fixed_amount11 + ",fixed_amount12=" + fixed_amount12 + ",fixed_amount13=" + fixed_amount13 + " ,fixed_amount14=" + fixed_amount14 + ",fixed_amount15=" + fixed_amount15 + ",fixed_amount16=" + fixed_amount16 + ",fixed_amount17=" + fixed_amount17 + " " +
                        "where Worktype_Name_B='" + Worktype_Name + "' and Division_Code='" + Div_Code + "'";

                    iReturn = db.ExecQry(strQry);
                }
                
                


            }
            catch (Exception ex)
            {

            }
            return iReturn;

        }
   
        public int get_OsExKms(string sf_code, string OSEX_Distance, string Terr_To)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET OSEX_Distance = '" + OSEX_Distance + "' " +
                         " WHERE Sf_Code = '" + sf_code + "' and Territory_Name='" + Terr_To + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
      //Changes done by Priya
        public DataSet getTerritory_Transfer(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT '-1' Territory_Code, '---Select---' Territory_Name " +
                     " UNION " +
                     " SELECT Territory_Code, Territory_Name " +
                     " FROM  Mas_Territory_Creation " +
                     " where Sf_code = '" + sf_code + "' and Territory_Active_Flag = 0";

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

        //Changes done by Priya

        public int GetterrCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(Max(Territory_Code)+1,'1') Territory_Code from Mas_Territory_Creation";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getTerritory_Total(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                //   " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code = cast(a.Territory_Code as varchar ) " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count, " +
                     "(select sf_name from mas_salesforce b   where a.sf_code=b.sf_code ) sf_name "+
                     " FROM  Mas_Territory_Creation a where a.Sf_Code in(" + sf_code + ")  and a.Territory_Active_Flag=0" +
                     " order by Territory_SNo";
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


        public int Reactivate_Terr(int Territory_Code, string div_code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = " Update Mas_Territory_Creation " +
                          " SET Territory_Active_Flag=0 , " +
                          "Territory_Deactive_Date = null " +
                          "WHERE Territory_Code = '" + Territory_Code + "' and Division_code='" + div_code + "' and Territory_Active_Flag=1 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public DataSet getTerritory_React(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=1 " +
                     " ORDER BY Territory_Name";
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
        public DataTable getTerritory_DataTable_Create(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where cast(a.Territory_Code as int)=b.Territory_Code " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count,territory_active_flag " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND (a.Territory_Active_Flag=0 or a.Territory_Active_Flag=1 or a.Territory_Active_Flag=2) ";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        public int getTerrFlag(string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt = null;
            int flg = 0;
            strQry = "select TP_Reporting_SF,(select Report_Level from Mas_SF_Designation D inner join Mas_Salesforce SF on " +
            "D.Designation_Code=SF.Designation_Code where Sf_Code=S.TP_Reporting_SF) flg from Mas_Salesforce S where Sf_Code='" + sfCode + "'";
            try
            {
                dt = db_ER.Exec_DataTable(strQry);
                if (dt.Rows.Count > 0)
                {
                    flg = Convert.ToInt32(dt.Rows[0]["flg"].ToString());
                }
                if (flg == 3 || flg == 0 || flg == 2)
                {
                    flg = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flg;

        }

        public int GetdeleteMGRDist(string SF_Code, string mgr_code, string div)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "delete from Mgr_Dist_Fixation where sf_code='" + SF_Code + "' and MGR_sf_code='" + mgr_code + "' and division_Code='" + div + "'";
                iReturn = db.ExecQry(strQry);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int GetinsertMGRDist(string SF_Code, string mgr_code, string From_Code, string To_Code, string MR_Cat, string MGR_Cat, string Dist, string Rtn_HQ, string div)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();



                //strQry = "Insert into Mgr_Dist_Fixation (sf_code,MGR_sf_code,From_Code,To_Code,MR_Cat,MGR_Cat,Dist,Rtn_HQ,Division_code,Created_Date)values('" + SF_Code + "','" + mgr_code + "','" + From_Code + "','" + To_Code + "','" + MR_Cat + "','" + MGR_Cat + "','" + Dist + "','" + Rtn_HQ + "','" + div + "',getdate())";
                //iReturn = db.ExecQry(strQry);
                strQry = "update mas_territory_creation set mgr_territory_cat='" + MGR_Cat + "' where territory_code='" + To_Code + "' and sf_code='" + SF_Code + "'";
                iReturn = db.ExecQry(strQry);
                strQry = "update mas_distance_fixation set To_Code_Code= STUFF(To_Code_Code, LEN(To_Code_Code), 1, '" + MGR_Cat + "'), Town_Cat='" + MGR_Cat + "'  where to_code='" + To_Code + "' and sf_code in(select mgr_code from mgr_allowance_setup where type_code='CA')";
                iReturn = db.ExecQry(strQry);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int WrkMGR_Expense_Update(string sf_code, string Div_Code, string type, string typ_name, string mgrcode, string Dist)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();

                strQry = "select COUNT(*) as [Value] from Mgr_Allowance_Setup where sf_code='" + sf_code + "' and mgr_code='" + mgrcode + "' and division_Code='" + Div_Code + "'";

                dsData = db.Exec_DataSet(strQry);

                if (dsData.Tables[0].Rows[0]["Value"].ToString() == "0")
                {

                    strQry = "Insert into Mgr_Allowance_Setup (sf_code,Type_Code,Type_Name,Division_code,mgr_code,Dist,Created_Date)values('" + sf_code + "','" + type + "','" + typ_name + "','" + Div_Code + "','" + mgrcode + "','" + Dist + "',getdate())";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "Update Mgr_Allowance_Setup set Type_Code='" + type + "',Type_Name='" + typ_name + "',mgr_code='" + mgrcode + "',Dist='" + Dist + "',Created_Date=getdate() where sf_code='" + sf_code + "' and mgr_code='" + mgrcode + "' and Division_Code='" + Div_Code + "'";

                    iReturn = db.ExecQry(strQry);
                }


            }
            catch (Exception ex)
            {

            }
            return iReturn;
        }

        public int RecordUpdate_aliasName(string Territory_Code, string Territory_Name, string Territory_SName, string Territory_Type, string Territory_Ali_Name,string sf_code)
        {
            int iReturn = -1;
            //if (!RecordExist(div_code, div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                    " SET Territory_Name = '" + Territory_Name + "', " +
                    " Territory_Cat = '" + Territory_Type + "', " +
                    " Territory_SName = '" + Territory_SName + "', LastUpdt_Date= getdate(),Alias_Name='" + Territory_Ali_Name + "' " +
                    " WHERE Territory_Code = '" + Territory_Code + "' ";
                iReturn = db.ExecQry(strQry);


                strQry = " update mas_distance_Fixation set  " +
                         " To_Code_Code= STUFF(To_Code_Code, LEN(To_Code_Code), 1, '" + Territory_Type + "'), Town_Cat='" + Territory_Type + "' " +
                         " where to_code='" + Territory_Code + "' ";
                iReturn = db.ExecQry(strQry);

                strQry = "delete from mas_distance_fixation where from_code in(select cast(territory_code as varchar) from mas_territory_creation where territory_cat='1')";
                iReturn = db.ExecQry(strQry);

                strQry = "delete from mas_distance_fixation where to_code in(select cast(territory_code as varchar) from mas_territory_creation where territory_cat='1')";
                iReturn = db.ExecQry(strQry);

           

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }
        public DataSet get_Territory_WeekMap(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;



            strQry = "with cte1 as (select a.Territory_Code,a.Territory_Name,a.Territory_SNo,case when a.Territory_Cat=1 then 'HQ' " +
                     "else case when a.Territory_Cat=2 then 'EX' else case when a.Territory_Cat=3 then 'OS' " +
                     "else 'OS-EX'  end end end as Territory_Cat,Master_Week_TP_Name as Category from Mas_Territory_Creation a where a.sf_code='" + sf_code + "' and a.Territory_Active_Flag=0 ) " +
                     "select Territory_Code,Territory_Name,Territory_Cat,Category  from cte1 e  order by  Territory_SNo";

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
        public int Update_Territory_WeekMap(string weekcode, string weekname, string sf_code, string terr_code, string div_code)
        {
            int iReturn = -1;


            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "update Mas_Territory_Creation set Master_Week_TP_Name='" + weekname + "' , Master_Week_TP_SName='" + weekcode + "' " +
                         "where Division_Code='" + div_code + "' and Territory_Code='" + terr_code + "' and SF_Code='" + sf_code + "' and Territory_Active_Flag=0";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int getTerritory_Hill(string Territory_Code, string Alias_Name, string Territory_SName, string hill, string Fare)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                             " SET Alias_Name='" + Alias_Name + "', Territory_SName='" + Territory_SName + "',Hill_Station='" + hill + "',Fare='"+Fare+"' " +
                             " WHERE Territory_Code = '" + Territory_Code + "' and territory_active_flag=0  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int TransferTerritory_New(string Territory_Code, string Target_Territory, string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                            " SET Territory_Code = '" + Target_Territory + "'  " +
                            " WHERE Territory_Code = '" + Territory_Code + "' and sf_code='" + sf_code + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

                if (iReturn != -1)
                {
                    strQry = "UPDATE Mas_ListedDr " +
                                " SET Territory_Code = '" + Target_Territory + "'  " +
                                " WHERE Territory_Code = '" + Territory_Code + "' and sf_code='" + sf_code + "' and Division_code='" + div_code + "'";

                    iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        strQry = "UPDATE Mas_UnListedDr " +
                                    " SET Territory_Code = '" + Target_Territory + "'  " +
                                    " WHERE Territory_Code = '" + Territory_Code + "' and sf_code='" + sf_code + "' and Division_code='" + div_code + "'";

                        iReturn = db.ExecQry(strQry);

                        if (iReturn != -1)
                        {
                            strQry = "UPDATE Mas_Hospital " +
                                        " SET Territory_Code = '" + Target_Territory + "'  " +
                                        " WHERE Territory_Code = '" + Territory_Code + "' and sf_code='" + sf_code + "' and Division_code='" + div_code + "'";

                            iReturn = db.ExecQry(strQry);

                            if (iReturn != -1)
                            {
                                strQry = "UPDATE Mas_Territory_Creation " +
                                            " SET territory_active_flag=1, Territory_Deactive_Date=getdate()  " +
                                            " WHERE Territory_Code = '" + Territory_Code + "' and sf_code='" + sf_code + "' and Division_code='" + div_code + "'";

                                iReturn = db.ExecQry(strQry);
                            }


                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int RecordAdd_Town(string Territory_Name, string town_Name, string Territory_Type, string sf_code,string Territory_Visit)
        {
            int iReturn = -1;

            if (!RecordExist(Territory_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Territory_Code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation ";
                    Territory_Code = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                             " SF_Code,Territory_Active_Flag,Created_date,Town_City,Territory_Visit) " +
                             " VALUES('" + Territory_Code + "', '" + Territory_Name + "', '" + Territory_Type + "', '', " +
                             " '" + Division_Code + "', '" + sf_code + "', 0, getdate(),'" + town_Name + "','"+ Territory_Visit + "')";

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
        public int getTerritory_Hill_Metro(string Territory_Code, string Alias_Name, string Territory_SName, string hill, string Fare,string Metro,string Allowance,string Terr_Visit)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                             " SET Alias_Name='" + Alias_Name + "', Territory_SName='" + Territory_SName + "',Hill_Station='" + hill + "',Fare='" + Fare + "',Exp_Allow_Cat='"+Metro+ "',Additional_Allowance='" + Allowance + "' ,Territory_Visit='" + Terr_Visit + "' " +
                             " WHERE Territory_Code = '" + Territory_Code + "' and territory_active_flag=0  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getTerritory_Hill_Metro_Terr(string Territory_Code, string Alias_Name, string Territory_SName, string hill, string Fare, string Metro, string Allowance)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                             " SET Alias_Name='" + Alias_Name + "', Territory_SName='" + Territory_SName + "',Hill_Station='" + hill + "',Fare='" + Fare + "',Exp_Allow_Cat='" + Metro + "',Additional_Allowance='" + Allowance + "' " +
                             " WHERE Territory_Code = '" + Territory_Code + "' and territory_active_flag=0  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getExp_Managers_View(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select a.Sf_Code,a.Sf_HQ+' - '+Sf_Name+' - '+b.Designation_Short_Name Sf_Name from mas_salesforce a," +
                     " Mas_SF_Designation b" +
                     " where a.Designation_Code=b.Designation_Code and sf_TP_Active_Flag in (0,2) " +
                     " and Sf_Code in  (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and  " +
                     " (Division_Code like '" + div_code + ',' + "%'  or  Division_Code like '%" + ',' + div_code + ',' + "%' ))";



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
