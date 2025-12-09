using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;
namespace Bus_EReport
{
    public class Expense
    {
        private string strQry = string.Empty;

        public DataSet getEmptyTerritory()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT TOP 10 '' Territory_Name,'' Territory_SName " +
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

        public int DeActivate(string terr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                            " SET territory_active_flag=1, Territory_Deactive_Date=getdate()   " +
                            " WHERE Territory_Code = '" + terr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate(string Territory_Code, string Territory_Name, string Territory_SName, string Territory_Type)
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
                         " Territory_SName = '" + Territory_SName + "' " +
                         " WHERE Territory_Code = '" + Territory_Code + "' ";

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
                                " WHERE Territory_Code = '" + Territory_Code + "' ";

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
                                            " SET territory_active_flag=1, Territory_Deactive_Date=getdate()   " +
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




        public DataSet getTerritory(string sf_code, string terr_code)
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

        public DataSet getTerritory(string sf_code)
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
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "'  and a.Territory_Active_Flag=0" +
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
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
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

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where convert(varchar(10),a.Territory_Code)=b.Territory_Code " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
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
                     " end end end as Territory_Cat,Alias_Name " +
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
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 " +
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

                strQry = "SELECT COUNT(Territory_Name) FROM Mas_Territory_Creation WHERE Territory_Name='" + Territory_Name + "' and sf_code = '" + sf_code + "'";
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

        public int RecordAdd(string Territory_Name, string Territory_SName, string Territory_Type, string sf_code)
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
                             " SF_Code,Territory_Active_Flag,Created_date) " +
                             " VALUES('" + Territory_Code + "', '" + Territory_Name + "', '" + Territory_Type + "', '" + Territory_SName + "', " +
                             " '" + Division_Code + "', '" + sf_code + "', 0, getdate())";

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

            strQry = " select SF_Name,Sf_HQ from Mas_Salesforce where Sf_Code='" + Sf_Code + "'";

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

            strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups where Division_Code='" + div_code + "'";

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
        public DataSet getWorkAreaName()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups ";

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
        //End
        public DataSet getSFCode(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as sf_code, '---Select---' as Sf_Name " +
                     " UNION " +
                     " select sf_code,Sf_Name from Mas_Salesforce where (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and sf_type=1 ";
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

        public DataSet getOSEXCondn(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select T.Territory_Code FCode,T.Territory_Name +'('+T.alias_name+')' FName, case when T.Territory_Cat=1 then 'HQ' " +
                       " else case when T.Territory_Cat=2 then 'EX' " +
                       " else case when T.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                        " end end end as Fcat,T1.Territory_Name +'('+T1.alias_name+')' TName, " +
          " T1.Territory_Code TCode, case when T1.Territory_Cat=1 then 'HQ' " +
                       " else case when T1.Territory_Cat=2 then 'EX' " +
                       " else case when T1.Territory_Cat=3 then 'OS' " +
                      " else 'OS-EX' " +
                       "  end end end as Tcat, '' Distance " +
         " from Mas_Territory_Creation  As T,Mas_Territory_Creation  As T1 " +
          " where T.sf_code='" + sf_code + "'  and T.Territory_Cat='3' AND  " +
         " t1.Territory_Code != t.Territory_Code and T1.Territory_Cat='4' and T1.sf_code='" + sf_code + "'" +
          " AND t.Territory_Active_Flag='0' " +
         " AND t1.Territory_Active_Flag='0' order by T.alias_name,T.Territory_Name ";
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

        public DataSet getOSEXDistCondn(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " Select Sf_Code,From_Code,To_Code,Distance_in_kms as Distance from Mas_Distance_Fixation Where Sf_Code='" + sf_code + "'";
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
        public DataSet getDist(string sf_code)//without calculating deactivated territory
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = "select From_Code,Sf_HQ,To_Code,Territory_Name,Territory_Cat,Territory_Code,town_cat,Distance_In_Kms Distance,m.sf_code " +
            //    " from Mas_Territory_Creation m inner join Mas_Salesforce S on S.Sf_Code=m.SF_Code " +
            //    "left outer join Mas_Distance_Fixation d on m.SF_Code=d.SF_Code and m.territory_code=d.to_code " +
            //    " where m.SF_Code='" + sf_code + "'";

            strQry = "select From_Code,Sf_HQ,To_Code,m.Territory_Name+ '('+m.alias_name+')' Territory_Name,Territory_Cat,Territory_Code,town_cat,Distance_In_Kms Distance,m.sf_code " +
                     " from Mas_Territory_Creation m inner join Mas_Salesforce S on S.Sf_Code=m.SF_Code and Territory_Active_Flag=0 " +
                     "left outer join Mas_Distance_Fixation d on m.SF_Code=d.SF_Code and m.territory_code=d.to_code and m.Territory_Cat=d.Town_Cat " +
                     " where m.SF_Code='" + sf_code + "' order by M.alias_name,m.Territory_Name";



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
        public DataSet getDist1(string sf_code, int cat)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name,  " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, Distance_In_Kms Distance,c.Sf_HQ,a.sf_code,Territory_Cat " +
                     " FROM  Mas_Territory_Creation a, Mas_Salesforce c,Mas_Distance_Fixation b where a.Sf_Code = '" + sf_code + "' and Territory_Cat=" + cat + "  and a.Territory_Active_Flag=0" +
                      " and a.Sf_Code=c.Sf_Code and a.SF_Code=b.SF_Code and a.Territory_Code=b.To_Code" +
                     " order by Territory_Name";
            Console.WriteLine(strQry);
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

            strQry = " SELECT a.Territory_Code, a.Territory_Name,a.sf_code,  " +
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
                   " (select * from Mas_Territory_Creation where Territory_Cat=4 and Territory_Active_Flag=0) select f.SF_Code,f.Territory_Name as Terr_From,e.Territory_Name as Terr_To,e.Distance from terr1 e, " +
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
                         " SET Distance = '" + EX_Distance + "' " +
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
                         " SET Distance = '" + OS_Distance + "' " +
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
                         " SET Distance = '" + OSEX_Distance + "' " +
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

        public DataSet getSfname_Desig(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select a.sf_name,a.Sf_HQ,a.sf_emp_id,b.Designation_Name,c.Division_Name " +
                     " from Mas_Salesforce a,Mas_SF_Designation b, Mas_Division c where Sf_Code= '" + Sf_Code + "'" +
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
        public int WrkTypeMGR_Expense_Update(string Worktype_Name, string Expense_Type)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_Mgr set Expense_Type='" + Expense_Type + "' where Worktype_Name_M='" + Worktype_Name + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {

            }
            return iReturn;
        }

        public int ExpenseParameter_Insert(string Exp_Parameter_Name, string Exp_Type, string Div_Code, string Level_Value, string Level_Code, string lblWorktype_Code)
        {
            int iReturn = -1;
            if (!ExpParameter_RecordExist(Exp_Parameter_Name))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    int Parameter_Code = 0;
                    int ExpensePara_Orby_Code = 0;
                    int Count = 0;
                    int InsertColumn = 0;

                    strQry = "select isnull(max(Expense_Parameter_Code),0)+1 Expense_Parameter_Code from Mas_Expense_Parameter";
                    Parameter_Code = db.Exec_Scalar(strQry);

                    strQry = "select isnull(max(ExpensePara_OrderBy_Code),0)+1 Expense_Parameter_Code from Mas_Expense_Parameter";
                    ExpensePara_Orby_Code = db.Exec_Scalar(strQry);

                    strQry = "Insert into Mas_Expense_Parameter (Expense_Parameter_Code,ExpensePara_OrderBy_Code," +
                             "Expense_Parameter_Name,Division_Code,Expense_Type," + Level_Code + ") values('" + Parameter_Code + "','" + ExpensePara_Orby_Code + "'," +
                             "'" + Exp_Parameter_Name + "','" + Div_Code + "','" + Exp_Type + "','" + Level_Value + "')";


                    iReturn = db.ExecQry(strQry);


                    //DataSet dsTerr = null;
                    //DataTable dt=null;
                    //strQry =  "SELECT REPLACE(LTRIM(RTRIM(Expense_Parameter_Name)), SPACE(1), '_' ) Expense_Parameter_Name "+
                    //          " FROM Mas_Expense_Parameter where Expense_Type=1 and Expense_Parameter_Name='" + Exp_Parameter_Name + "' ";
                    //dsTerr = db.Exec_DataSet(strQry);
                    //dt = dsTerr.Tables[0];

                    //for (int i = 0; i < dsTerr.Tables[0].Rows.Count; i++)
                    //{
                    //    //string strColumn = dt.Rows[i].Field<string>(0);
                    //    string strColumn = "Fixed_Column"+Convert.ToString(i);

                    //    //strQry = "ALTER TABLE Mas_Allowance_Fixation DROP COLUMN"+strColumn+"";
                    //    //InsertColumn = db.ExecQry(strQry);

                    //    strQry = "Alter table Mas_Allowance_Fixation add " + strColumn + " float";
                    //    InsertColumn = db.ExecQry(strQry);

                    //}

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

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Expense_Type=1 then 'Fixed' " +
                     "when Expense_Type=2  then 'Variable' " +
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

        public bool ExpParameter_RecordExist(string Exp_Parameter_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Expense_Parameter_Name) Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Parameter_Name='" + Exp_Parameter_Name + "'";

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

        public int ExpRecordUpdate(string Exp_Parameter_Code, string ExpParameter_Name, string Exp_Type)
        {
            int iReturn = -1;
            int InsertColumn = 0;
            //if (!RecordExist(ExpParameter_Name))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_Expense_Parameter set Expense_Parameter_Name='" + ExpParameter_Name + "',Expense_Type='" + Exp_Type + "' where Expense_Parameter_Code='" + Exp_Parameter_Code + "'";

                iReturn = db.ExecQry(strQry);


                DataSet dsTerr = null;
                DataTable dt = null;
                strQry = "SELECT REPLACE(LTRIM(RTRIM(Expense_Parameter_Name)), SPACE(1), '_' ) Expense_Parameter_Name " +
                          " FROM Mas_Expense_Parameter where Expense_Type=1 and Expense_Parameter_Code='" + Exp_Parameter_Code + "'";
                dsTerr = db.Exec_DataSet(strQry);

                dt = dsTerr.Tables[0];

                for (int i = 0; i < dsTerr.Tables[0].Rows.Count; i++)
                {
                    string strColumn = dt.Rows[i].Field<string>(0);

                    //strQry = "ALTER TABLE Mas_Allowance_Fixation DROP COLUMN" + strColumn + "";
                    //InsertColumn = db.ExecQry(strQry);

                    strQry = "Alter table Mas_Allowance_Fixation add " + strColumn + " float";
                    InsertColumn = db.ExecQry(strQry);

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



        public DataSet getExp_ParameterBL(int BL_workcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Expense_Type=1 then 'Fixed' " +
                     "when Expense_Type=2  then 'Variable' " +
                     "end Expense_Type from Mas_Expense_Parameter where BL_workcode='" + BL_workcode + "'";

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

        public DataSet getExp_ParameterMGR(int MGR_workcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,case when Expense_Type=1 then 'Fixed' " +
                     "when Expense_Type=2  then 'Variable' " +
                     "end Expense_Type from Mas_Expense_Parameter where MGR_workcode='" + MGR_workcode + "'";

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

        public DataSet getExp_FixedType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            int iCount = 0;

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

        public int WrkTypeBase_Expense_Update(string Worktype_Name, string Expense_Type)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_BaseLevel set Expense_Type='" + Expense_Type + "' where Worktype_Name_B='" + Worktype_Name + "'";

                iReturn = db.ExecQry(strQry);

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
                         " SET Distance = '" + OSEX_Distance + "' " +
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
                     " where Sf_code = '" + sf_code + "' ";

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

                strQry = "SELECT isnull(max(Territory_Code)+1,'1') Territory_Code from Mas_Territory_Creation";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int getTransFlag(string sfCode)
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
                if (flg == 3)
                {
                    flg = 4;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flg;

        }

        public DataSet getSF_flag(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select Report_Level from Mas_Salesforce SF " +
                     " inner join Mas_SF_Designation D on D.Designation_Code=SF.Designation_Code " +
                     " where sf_code = '" + sfcode + "'";
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
        public DataSet getExp_approve1(string sfcode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = " select distinct SF.Sf_Code,SF.Sf_Name,SF.Sf_HQ,SF.sf_emp_id,D.Designation_Short_Name,expense_month Mon,expense_year year,h.division_code div_code,mgr_flag," +
            //         "'Click here to Approve '+ DateName(mm,DATEADD(mm,Expense_Month,-1)) + ' - '+ convert(varchar(10), expense_year) as mondt  from Trans_Expense_Head H inner join Mas_Salesforce SF on H.SF_Code=SF.Sf_Code " +
            //         " inner join Mas_SF_Designation D on D.Designation_Code=SF.Designation_Code " +
            //         " where sf.Reporting_To_SF = '" + sfcode + "' and H.division_code = '" + divcode + "' and sndhqfl=7";
            strQry = " select distinct SF.Sf_Code,SF.Sf_Name,SF.Sf_HQ,SF.sf_emp_id,D.Designation_Short_Name,expense_month Mon,expense_year year,h.division_code div_code,mgr_flag," +
                  "'Click here to Approve '+ DateName(mm,DATEADD(mm,Expense_Month,-1)) + ' - '+ convert(varchar(10), expense_year) as mondt  from Trans_Expense_Head H inner join Mas_Salesforce SF on H.SF_Code=SF.Sf_Code " +
                  " inner join Mas_SF_Designation D on D.Designation_Code=SF.Designation_Code inner join mas_salesforce_am AM on AM.sf_code=SF.sf_code and AM.sf_code=H.sf_code" +
                  " where AM.Expense_AM = '" + sfcode + "' and H.division_code = '" + divcode + "' and sndhqfl=7";



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
        public DataSet getFare_HQ(string sf_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select isnull(sum(expense_total),0)cnt,sf_code,count(Expense_All_Type) days from trans_expense_detail D inner join " +
                     " trans_expense_Head H on D.sl_no=H.sl_no where expense_month='" + imonth + "' and expense_year='" + iyear + "' " +
                     " and sf_code='" + sf_code + "' and Expense_All_Type='HQ' group by sf_code";



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

        public DataSet getFare_EX(string sf_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select isnull(sum(expense_total),0)cnt,sf_code,count(Expense_All_Type) days from trans_expense_detail D inner join " +
                     " trans_expense_Head H on D.sl_no=H.sl_no where expense_month='" + imonth + "' and expense_year='" + iyear + "' " +
                     " and sf_code='" + sf_code + "' and Expense_All_Type='EX' group by sf_code";



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
        public DataSet getFare_OS(string sf_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select isnull(sum(expense_total),0)cnt,sf_code,count(Expense_All_Type) days from trans_expense_detail D inner join " +
                     " trans_expense_Head H on D.sl_no=H.sl_no where expense_month='" + imonth + "' and expense_year='" + iyear + "' " +
                     " and sf_code='" + sf_code + "' and (Expense_All_Type='OS' or Expense_All_Type='OS-EX') group by sf_code";



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


        public DataSet getmisel(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "select SF_Code,isnull(SUM(Amt),0)mis_Amt from Exp_Others O inner join Trans_Expense_Head H on O.sl_No=H.Sl_No where Expense_Month=" + month + " and Expense_Year=" + year + " and sf_code='" + sf_code + "' group by SF_Code";
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

        public DataSet EXP_Total(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = " select sf_code,sum(cnt)cnt from (select isnull(sum(expense_total),0)cnt,sf_code from trans_expense_detail D inner join  " +
                     " trans_expense_Head H on D.sl_no=H.sl_no where expense_month='" + month + "' and expense_year='" + year + "' " +
                     " and h.sf_code='" + sf_code + "' group by h.sf_code union select isnull(sum(amt),0)cnt,sf_code from exp_others O inner join " +
                     " trans_expense_head H on O.sl_no=H.sl_no where expense_month='" + month + "' and expense_year='" + year + "' and h.sf_code='" + sf_code + "' " +
                     " group by h.sf_code union " +
                     " select isnull(sum(amt),0)cnt,H.sf_code from exp_fixed O inner join " +
                     " trans_expense_head H on O.sl_no=H.sl_no where expense_month='" + month + "' and expense_year='" + year + "' " +
                     " and h.sf_code='" + sf_code + "' " +
                     " group by h.sf_code ) as d  " +
                     " group by sf_code";
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
        public DataSet get_exp_info_adj(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = " SELECT case when Typ=1 then '+' else '-' end Typ,Amt,grand_total from Exp_AccInf " +
                     " where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month='" + month + "' and Expense_Year='" + year + "') ";
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
        public DataSet exp_others(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = " select sf_code,sum(cnt)cnt from ( select isnull(sum(amt),0)cnt,sf_code from exp_others O inner join " +
                     " trans_expense_head H on O.sl_no=H.sl_no where expense_month='" + month + "' and expense_year='" + year + "' and h.sf_code='" + sf_code + "' " +
                     " group by h.sf_code union " +
                     " select isnull(sum(amt),0)cnt,H.sf_code from exp_fixed O inner join " +
                     " trans_expense_head H on O.sl_no=H.sl_no where expense_month='" + month + "' and expense_year='" + year + "' " +
                     " and h.sf_code='" + sf_code + "' " +
                     " group by h.sf_code ) as d  " +
                     " group by sf_code";
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


    }
}
