using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class ChemistCampaign
    {
        private string strQry = string.Empty;


        public int RecordAddSubCat(string divcode, string chm_campaign_SName, string chm_campaign_name, DateTime Effective_From, DateTime Effective_To)
        {
            int iReturn = -1;
            if (!RecordExistSubCatSave(chm_campaign_SName, divcode))
            {
                if (!sRecordExistSubCatSave(chm_campaign_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        string EffFromdate = Effective_From.Month.ToString() + "-" + Effective_From.Day + "-" + Effective_From.Year;
                        string EffTodate = Effective_To.Month.ToString() + "-" + Effective_To.Day + "-" + Effective_To.Year;

                        strQry = "SELECT isnull(max(chm_campaign_code)+1,'1') chm_campaign_code from Mas_chemist_campaign ";
                        int Che_SubCatCode = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Chemist_Campaign([chm_campaign_code],[chm_campaign_SName],[chm_campaign_name],[chm_effective_from],[chm_effective_to],[division_code],[active_flag],[created_date],[updated_date])" + "values('" + Che_SubCatCode + "','" + chm_campaign_SName + "','" + chm_campaign_name + "','" + EffFromdate + "','" + EffTodate + "','" + divcode + "',0,getdate(),'')";

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
        public int RecordAddSubCatUpdate(int CheSubCatCode, string divcode, string chm_campaign_SName, string chm_campaign_name, DateTime Effective_From, DateTime Effective_To)
        {
            int iReturn = -1;
            if (!RecordExistSubCatUpdate(CheSubCatCode, chm_campaign_SName, divcode))
            {
                if (!sRecordExistSubCatUpdate(CheSubCatCode, chm_campaign_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE [Mas_chemist_campaign] " +
                                 " SET chm_campaign_SName = '" + chm_campaign_SName + "', " +
                                 " chm_campaign_name = '" + chm_campaign_name + "'," +
                                 " chm_effective_from ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                 " , chm_effective_to ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                 " updated_date = getdate() " +
                                 " WHERE chm_campaign_code = '" + CheSubCatCode + "' ";

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
        public bool RecordExistSubCatUpdate(int CheSubCatCode, string Che_SubCatSName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(chm_campaign_SName) FROM [Mas_chemist_campaign] WHERE chm_campaign_SName= '" + Che_SubCatSName + "' AND chm_campaign_code!='" + CheSubCatCode + "'AND Division_Code='" + divcode + "' ";

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
        public bool sRecordExistSubCatUpdate(int CheSubCatCode, string Che_SubCatName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(chm_campaign_Name) FROM [Mas_chemist_campaign] WHERE chm_campaign_Name= '" + Che_SubCatName + "' AND chm_campaign_code!='" + CheSubCatCode + "'AND Division_Code='" + divcode + "' ";

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
        public bool RecordExistSubCatSave(string chm_campaign_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(chm_campaign_SName) FROM Mas_chemist_campaign WHERE chm_campaign_SName= '" + chm_campaign_SName + "' AND Division_Code='" + divcode + "' ";

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
        public bool sRecordExistSubCatSave(string chm_campaign_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(chm_campaign_name) FROM Mas_chemist_campaign WHERE chm_campaign_name= '" + chm_campaign_name + "' AND Division_Code='" + divcode + "' ";

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
        public DataSet getChemSubCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemSpe = null;

            strQry = " select chm_campaign_code,chm_campaign_SName,chm_campaign_name" +
                     " FROM [Mas_chemist_campaign] where active_flag=0 and division_code='" + divcode + "'" +
                     " ORDER BY chm_campaign_SName ASC";

            try
            {
                dsChemSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemSpe;
        }
        public int RecordUpdateCheSubCat(int chm_campaign_code, string chm_campaign_SName, string chm_campaign_name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistSubCatChe(chm_campaign_code, chm_campaign_SName, divcode))
            {

                if (!sRecordExistSubCatChe(chm_campaign_code, chm_campaign_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_chemist_campaign " +
                                 " SET chm_campaign_SName = '" + chm_campaign_SName + "', " +
                                 " chm_campaign_name = '" + chm_campaign_name + "'," +
                                    " updated_date = getdate() " +
                                 " WHERE chm_campaign_code = '" + chm_campaign_code + "' ";

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
        public bool RecordExistSubCatChe(int chm_campaign_code, string chm_campaign_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(chm_campaign_SName) FROM Mas_chemist_campaign WHERE chm_campaign_SName= '" + chm_campaign_SName + "' AND chm_campaign_code!='" + chm_campaign_code + "' AND Division_Code='" + divcode + "' ";

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
        public bool sRecordExistSubCatChe(int chm_campaign_code, string chm_campaign_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(chm_campaign_name) FROM Mas_chemist_campaign WHERE chm_campaign_name= '" + chm_campaign_name + "' AND chm_campaign_code!='" + chm_campaign_code + "' AND Division_Code='" + divcode + "' ";

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

        public int RecordUpdateSubCatChem(int Che_SubCatCode, string Che_SubCatSName, string Che_SubCatName, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistSubCatChem(Che_SubCatCode, Che_SubCatSName, divcode))
            {

                if (!sRecordExistSubCatChem(Che_SubCatCode, Che_SubCatName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE [Mas_chemist_campaign] " +
                                 " SET chm_campaign_SName = '" + Che_SubCatSName + "', " +
                                 " chm_campaign_name = '" + Che_SubCatName + "'," +
                                    " updated_date = getdate() " +
                                 " WHERE chm_campaign_code = '" + Che_SubCatCode + "' ";

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
        public bool RecordExistSubCatChem(int Che_SubCatCode, string Che_SubCatSName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(chm_campaign_SName) FROM [Mas_chemist_campaign] WHERE chm_campaign_SName= '" + Che_SubCatSName + "' AND chm_campaign_code!='" + Che_SubCatCode + "'AND Division_Code='" + divcode + "' ";

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
        public bool sRecordExistSubCatChem(int Che_SubCatCode, string Che_SubCatName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(chm_campaign_name) FROM [Mas_chemist_campaign] WHERE chm_campaign_name= '" + Che_SubCatName + "' AND chm_campaign_code!='" + Che_SubCatCode + "'AND Division_Code='" + divcode + "' ";

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
        public DataSet getCheSubCat(string divcode, string chm_campaign_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsCheSpe = null;


            strQry = " SELECT chm_campaign_SName,chm_campaign_name,convert(varchar(10),chm_effective_from,103) Effective_From,convert(varchar(10),chm_effective_to,103)Effective_To FROM  [Mas_chemist_campaign] WHERE chm_campaign_code='" + chm_campaign_code + "' AND Division_Code=  '" + divcode + "' ORDER BY 2";
            try
            {
                dsCheSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCheSpe;
        }
        public int DeActivateSubCat(int Che_SubCatCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE [Mas_chemist_campaign] " +
                            " SET active_flag =1 ," +
                            " updated_date = getdate() " +
                            " WHERE chm_campaign_code = '" + Che_SubCatCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int RectivateChe_CamReact(int chm_campaign_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE [Mas_chemist_campaign] " +
                            " SET active_flag=0 ," +
                            " updated_date = getdate() " +
                            " WHERE chm_campaign_code = '" + chm_campaign_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getCheCamp_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT chm_campaign_code,chm_campaign_SName,chm_campaign_name FROM  [Mas_chemist_campaign] " +
                     " WHERE active_flag=1 AND division_code= '" + divcode + "' ";

            try
            {
                dsDocSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe;
        }
        public DataTable getCheSubCatlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsCheSpe = null;

            strQry = " SELECT chm_campaign_code,chm_campaign_SName,chm_campaign_name FROM  Mas_chemist_campaign " +
                     " WHERE active_flag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY chm_campaign_code ";
            try
            {
                dsCheSpe = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCheSpe;
        }
        public DataSet getCheSubCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsCheSpe = null;

            strQry = " SELECT chm_campaign_code,chm_campaign_SName,chm_campaign_name FROM  [Mas_chemist_campaign] " +
                     " WHERE active_flag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY chm_campaign_code";

            try
            {
                dsCheSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCheSpe;
        }
        public int Update_CheCampSno(string chm_campaign_code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE [Mas_chemist_campaign] " +
                         " SET Che_SubCat_SlNo = '" + Sno + "', " +
                         " updated_date = getdate() " +
                         " WHERE chm_campaign_code = '" + chm_campaign_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_Camp(string Chemist_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedChe = null;

            strQry = "Select Chemist_Code,Chemist_Name,Chm_campaign_code from [Map_chemist_campaign] " +
                     " where Chemist_Code =  '" + Chemist_Code + "' and Division_code = '" + div_code + "' ";


            try
            {
                dsListedChe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedChe;
        }
        public DataSet getListedChe_Camp(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "select Chemists_Code, Chemists_Name from Mas_Chemists where Division_Code=" + div_code + " and Sf_Code='" + sfcode + "' ";

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
        public DataSet getListedChe_CampMap(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "select Chemists_Code, Chemists_Name,stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = che.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),che.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, stuff((select ', '+chm_campaign_name from Mas_chemist_campaign dc,Map_Chemist_Campaign MChe where dc.Division_code=" + div_code + " and MChe.Chemists_Code=che.Chemists_Code and charindex(','+cast(dc.chm_campaign_code as varchar)+',',','+MChe.chm_campaign_code)>0 for XML path('')),1,2,'') +', ' chm_campaign_name from Mas_Chemists che where Division_Code=" + div_code + " and Sf_Code='" + sfcode + "' and che.Chemists_Active_Flag=0 ";

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
        public DataSet getListedChe_CampMap_New(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Chemists_Code, Chemists_Name,Chemists_Mobile,stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = che.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),che.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, stuff((select ', '+chm_campaign_name from Mas_chemist_campaign dc,Map_Chemist_Campaign MChe where dc.Division_code=" + div_code + " and MChe.Chemists_Code=che.Chemists_Code and charindex(','+cast(dc.chm_campaign_code as varchar)+',',','+MChe.chm_campaign_code)>0 for XML path('')),1,2,'') +', ' chm_campaign_name from Mas_Chemists che where Division_Code=" + div_code + " and Sf_Code='" + sfcode + "' and che.Chemists_Active_Flag=0 order by Chemists_Name";


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
        public DataSet FetchTerritory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";
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

        public DataSet getListedDrforTerr_Camp(string sfcode, string TerrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Chemists_Code, Chemists_Name,stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = che.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),che.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, stuff((select ', '+chm_campaign_name from Mas_chemist_campaign dc,Map_Chemist_Campaign MChe where dc.Division_code=" + div_code + " and MChe.Chemists_Code=che.Chemists_Code and charindex(','+cast(dc.chm_campaign_code as varchar)+',',','+MChe.chm_campaign_code)>0 for XML path('')),1,2,'') +', ' chm_campaign_name from Mas_Chemists che where Division_Code=" + div_code + " and Sf_Code='" + sfcode + "' " + " and (che.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " che.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or che.Territory_Code like '" + TerrCode + "') " +
                        "and che.Chemists_Active_Flag = 0";
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
        public DataSet getListedDrforTerr_Camp_New(string sfcode, string TerrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Chemists_Code, Chemists_Name,Chemists_Mobile,stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = che.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),che.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, stuff((select ', '+chm_campaign_name from Mas_chemist_campaign dc,Map_Chemist_Campaign MChe where dc.Division_code=" + div_code + " and MChe.Chemists_Code=che.Chemists_Code and charindex(','+cast(dc.chm_campaign_code as varchar)+',',','+MChe.chm_campaign_code)>0 for XML path('')),1,2,'') +', ' chm_campaign_name from Mas_Chemists che where Division_Code=" + div_code + " and Sf_Code='" + sfcode + "' " + " and (che.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " che.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or che.Territory_Code like '" + TerrCode + "') " +
                        "and che.Chemists_Active_Flag = 0 order by Chemists_Name";
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
        //public DataSet getCheSubCat(string divcode)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsDocSpe = null;

        //    strQry = " SELECT Chm_campaign_code,chm_campaign_SName,chm_campaign_name FROM  Map_Chemist_Campaign " +
        //             " WHERE active_flag=0 AND Division_Code=  '" + divcode + "' " +
        //             " ORDER BY Chm_campaign_code";

        //    try
        //    {
        //        dsDocSpe = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsDocSpe;
        //}

        public int Map_Campaign(string Chm_campaign_code, string Chemists_Code, string div_code, string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (!RecordExistMapChemCam(Chemists_Code, div_code, sf_code))
                {
                    if (Chm_campaign_code != "")
                    {
                        strQry = "SELECT isnull(max(sl_no)+1,'1') sl_no from Map_Chemist_Campaign ";
                        int Slno = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Map_Chemist_Campaign (sl_no, Chm_campaign_code, Chemists_Code, Division_code, CreatedDate,UpdatedDate,sf_code)" +
                        " VALUES (" + Slno + ",'" + Chm_campaign_code + "'," + Chemists_Code + "," + div_code + ",getdate(),getdate(),'" + sf_code + "') ";
                    }
                }
                else
                {
                    strQry = "UPDATE Map_Chemist_Campaign " +
                               " SET Chm_campaign_code = '" + Chm_campaign_code + "', " +
                               " UpdatedDate = getdate() " +
                               " WHERE Chemists_Code = '" + Chemists_Code + "' and sf_code='" + sf_code + "' ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Map_Campaign_New(string Chm_campaign_code, string Chemists_Code, string Chemists_mobile, string div_code, string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (!RecordExistMapChemCam(Chemists_Code, div_code, sf_code))
                {
                    if (Chm_campaign_code != "")
                    {
                        strQry = "SELECT isnull(max(sl_no)+1,'1') sl_no from Map_Chemist_Campaign ";
                        int Slno = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Map_Chemist_Campaign (sl_no, Chm_campaign_code, Chemists_Code, Division_code, CreatedDate,UpdatedDate,sf_code)" +
                        " VALUES (" + Slno + ",'" + Chm_campaign_code + "'," + Chemists_Code + "," + div_code + ",getdate(),getdate(),'" + sf_code + "') ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Chemists " +
                                 " SET Chemists_Mobile = '" + Chemists_mobile + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Chemists_Code = '" + Chemists_Code + "' and sf_code='" + sf_code + "' ";

                    }
                }
                else
                {
                    strQry = "UPDATE Map_Chemist_Campaign " +
                               " SET Chm_campaign_code = '" + Chm_campaign_code + "', " +
                               " UpdatedDate = getdate() " +
                               " WHERE Chemists_Code = '" + Chemists_Code + "' and sf_code='" + sf_code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Mas_Chemists " +
                                " SET Chemists_Mobile = '" + Chemists_mobile + "', " +
                                " LastUpdt_Date = getdate() " +
                                " WHERE Chemists_Code = '" + Chemists_Code + "' and sf_code='" + sf_code + "' ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public bool RecordExistMapChemCam(string Chemists_Code, string div_code, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(*) FROM [Map_Chemist_Campaign] WHERE Chemists_Code='" + Chemists_Code + "'AND Division_Code='" + div_code + "' AND sf_code='" + sf_code + "' ";

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
        public DataSet get_CampChk(string ListedCheCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select MC.Chemists_Code,MCS.Chemists_Name,MC.Chm_campaign_code from Map_Chemist_Campaign MC, Mas_Chemists MCS " +
            " where MC.Chemists_Code=" + ListedCheCode + " and MC.Division_code = " + div_code + " ";


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
        public DataSet get_CampChk_New(string ListedCheCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select MC.Chemists_Code,MCS.Chemists_Name,MC.Chm_campaign_code,MCS.Chemists_Mobile from Map_Chemist_Campaign MC, Mas_Chemists MCS " +
            " where MC.Chemists_Code=" + ListedCheCode + " and MC.Division_code = " + div_code + " order by Chemists_Name";


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
        //status zoom for Chemist campaign 
        public DataSet CheCampaignMap(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsCheCat = null;

            strQry = "select distinct che.Chemists_Code,Chemists_Name,Chemists_Address1,Territory_Name,Chemists_Mobile,Chemists_Email," +
           "STUFF((select', ' + CAST(chm_campaign_name AS VARCHAR(500))  from Mas_chemist_campaign where map.Chm_campaign_code like  convert(varchar,Chm_campaign_code) +','+'%' or map.Chm_campaign_code like '%'+','+convert(varchar,Chm_campaign_code)+','+'%'FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ' ) Campaign from " +
" Mas_Chemists che INNER JOIN mas_territory_Creation T on T.territory_code=che.territory_code " +
" INNER JOIN Map_Chemist_Campaign map on che.Chemists_Code=map.Chemists_Code " +
" INNER JOIN Mas_chemist_campaign cheCamp on charindex(cast(cheCamp.chm_campaign_code as varchar),map.chm_campaign_code ) >0 " +
" where che.sf_Code='" + sf_code + "' and Chemists_Active_Flag=0 ";

            try
            {
                dsCheCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCheCat;
        }
        public DataTable getListedChemistList_DataTable_camp(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT che.Chemists_Code,Chemists_Name,Chemists_Address1,Territory_Name,Chemists_Mobile,Chemists_Email," +
                 "STUFF((select', ' + CAST(chm_campaign_name AS VARCHAR(500))  from Mas_chemist_campaign where map.Chm_campaign_code like  convert(varchar,Chm_campaign_code) +','+'%' or map.Chm_campaign_code like '%'+','+convert(varchar,Chm_campaign_code)+','+'%'FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ' ) Campaign from " +
                " Mas_Chemists che INNER JOIN mas_territory_Creation T on T.territory_code=che.territory_code " +
                " INNER JOIN Map_Chemist_Campaign map on che.Chemists_Code=map.Chemists_Code " +
               " INNER JOIN Mas_chemist_campaign cheCamp on charindex(cast(cheCamp.chm_campaign_code as varchar),map.chm_campaign_code ) >0 " +
                " where che.sf_Code='" + sfcode + "' and Chemists_Active_Flag=0  order by Chemists_Name ";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }
    }
}
