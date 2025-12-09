using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class Chemist
    {
        private string strQry = string.Empty;

        public int RecordAdd(string Chemists_Name, string Chemists_Address1, string Chemists_Contact, string Chemists_Phone, string Chemists_Terr, string Chemists_Cat, string sf_code)
        {
            int iReturn = -1;

            if (!sRecordExist(Chemists_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int chemists_code = -1;

                    Chemists_Name = Chemists_Name.Replace("  ", " ");

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //   strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                    chemists_code = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
                             " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code) " +
                             " VALUES('" + chemists_code + "', '" + Chemists_Name + "', '" + Chemists_Address1 + "', '" + Chemists_Phone + "', " +
                             " '" + Chemists_Contact + "',  '" + Chemists_Terr + "', '" + Division_Code + "','" + sf_code + "', 0, getdate(),'" + Chemists_Cat + "')";


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

        public bool RecordExist(string Chemists_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chemists_Name) FROM Mas_Chemists WHERE Chemists_Name='" + Chemists_Name + "' ";
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

        public int DeActivate(string chem_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                            " SET Chemists_Active_Flag=1 , " +
                            " chemists_deactivate_date = getdate() " +
                            " WHERE Chemists_Code = '" + chem_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int ReActivate(string chem_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_Chemists" +
                         " SET Chemists_Active_Flag=0, " +
                         " LastUpdt_Date = getdate() " +
                         " where Chemists_Code = '" + chem_code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int BulkEdit(string str, string chem_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_Chemists SET " + str + "  Where Chemists_Code='" + chem_Code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getTerritory_Chemists(string chem_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = " SELECT Territory_Code FROM  Mas_Chemists " +
                     " WHERE Chemists_Code='" + chem_code + "' AND sf_Code= '" + sf_code + "' ";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        public DataSet getCat_Chemists(string chem_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = " SELECT Cat_Code FROM  Mas_Chemists " +
                     " WHERE Chemists_Code='" + chem_code + "' AND sf_Code= '" + sf_code + "' ";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        public DataSet getChemistsDetails(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT Chemists_Code, Chemists_Name, Chemists_Address1, Chemists_Contact, Territory_Code, Chemists_Phone, Chemists_Fax, Chemists_EMail, Chemists_Mobile,Chemist_ERP_Code  " +
                        " FROM Mas_Chemists " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        public DataSet getChemists(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 0 order by Chemists_Name";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        // Sorting For ChemistsList 
        public DataTable getChemistslist_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemists = null;

            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                         "Mas_Chemists d, Mas_Territory_Creation t " +
                         " WHERE d.Sf_Code = '" + sfcode + "' and d.Territory_Code = t.Territory_Code " +
                         " and d.Chemists_Active_Flag = 0";
            try
            {
                dtChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemists;
        }

        //sorting Reactivation

        public DataTable getChemistslist_DataTable_ReAct(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemists = null;

            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                         "Mas_Chemists d, Mas_Territory_Creation t " +
                         " WHERE d.Sf_Code = '" + sfcode + "' and d.Territory_Code = t.Territory_Code " +
                         " and d.Chemists_Active_Flag = 1";
            try
            {
                dtChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemists;
        }

        public DataTable getChemistsAlpha_DataTable(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemists = null;
            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                         "Mas_Chemists d, Mas_Territory_Creation t " +
                         "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                         "and d.Chemists_Active_Flag = 0" +
                     " AND LEFT(Chemists_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dtChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemists;
        }
        public int RecordCount(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chemists_Code) FROM Mas_Chemists WHERE Sf_Code = '" + sf_code + "' and Chemists_Active_Flag = 0";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getChemists(string sfcode, string chemists_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Code = '" + chemists_code + "' and d.Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        //alphabetical order

        public DataSet getChemist_Alphabet(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;
            strQry = "select '1' val,'All' Chemists_Name " +
                     " union " +
                     " select distinct LEFT(Chemists_Name,1) val, LEFT(Chemists_Name,1) Chemists_Name" +
                     " FROM Mas_Chemists " +
                     " WHERE Chemists_Active_Flag=0 " +
                     " AND Sf_Code =  '" + sfcode + "' " +
                     " ORDER BY 1";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        public DataSet getChemist_Alphabet(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;
            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                         "Mas_Chemists d, Mas_Territory_Creation t " +
                         "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                         "and d.Chemists_Active_Flag = 0" +
                     " AND LEFT(Chemists_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }


        //
        public int RecordUpdate_Chem(string Chemists_Code, string Chemists_Name, string Chemists_Contact, string Territory_Code, string sf_code)
        {
            int iReturn = -1;
            if (!RecordExist(Chemists_Code, Chemists_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();
                    Chemists_Name = Chemists_Name.Replace("  ", " ");

                    strQry = "UPDATE Mas_Chemists " +
                             " SET Chemists_Name = '" + Chemists_Name + "', " +
                             " Chemists_Contact = '" + Chemists_Contact + "', " +
                             " Territory_Code = '" + Territory_Code + "'," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE Chemists_Code = '" + Chemists_Code + "'  and  sf_code = '" + sf_code + "'";

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
        public DataSet get_Territory(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                           " UNION " +
                       " SELECT Territory_Code,Territory_Name " +
                       " FROM  Mas_Territory_Creation where Sf_Code='" + sfcode + "'" +
                       " and territory_active_flag=0 ";
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

        public DataSet getEmptyChemist()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemist = null;

            strQry = " SELECT TOP 10 '' Chemists_Name,'' Chemists_Address1, '' Chemists_Contact, '' Chemists_Phone " +
                     " FROM  sys.tables ";
            try
            {
                dsChemist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemist;
        }

        //Chemists Reactivation
        public DataSet getChemists_React(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 1";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        //Changes done by Priya

        public int getTerr_Chem_Count(string Territory_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Chemists_Code) from Mas_Chemists  " +
                         " where Chemists_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getChemist_terr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            //strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Address1, d.Chemists_Contact, " +
            //         " d.Territory_Code, d.Chemists_Phone, d.Chemists_Fax, " +
            //         " d.Chemists_EMail, d.Chemists_Mobile,t.territory_Name " +
            //         " FROM Mas_Chemists d, Mas_Territory_Creation t  WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code = '" + terr_code + "'" +
            //         " and d.Territory_Code =t.Territory_Code AND d.Chemists_Active_Flag = 0 and t.Territory_Active_Flag=0 ";

            strQry = " SELECT a.Sf_Name,a.sf_Designation_Short_Name,a.Sf_HQ,a.sf_emp_id,d.Chemists_Code, d.Chemists_Name,Class_Code,Chem_Cat_Name, d.Chemists_Address1, d.Chemists_Contact, " +
                     " d.Territory_Code, d.Chemists_Phone, d.Chemists_Fax, " +
                     " d.Chemists_EMail, d.Chemists_Mobile,t.territory_Name " +
                     " FROM Mas_Chemists d left join  Mas_Chemist_Category u on  u.Cat_Code=d.Cat_Code, Mas_Territory_Creation t,Mas_Salesforce a  WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code = '" + terr_code + "'" +
                     " and d.Territory_Code =t.Territory_Code AND d.Chemists_Active_Flag = 0 and t.Territory_Active_Flag=0 and a.Sf_Code=d.Sf_Code";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        public DataSet getChemistsDetails_terr(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;
            strQry = " SELECT a.Sf_Name,a.sf_Designation_Short_Name,a.Sf_HQ,a.sf_emp_id,d.Chemists_Code, d.Chemists_Name,Class_Code,Chem_Cat_Name, d.Chemists_Address1, d.Chemists_Contact, " +
                            " d.Territory_Code, d.Chemists_Phone, d.Chemists_Fax, " +
                            " d.Chemists_EMail, d.Chemists_Mobile,t.territory_Name " +
                            " FROM Mas_Chemists d left join  Mas_Chemist_Category u on  u.Cat_Code=d.Cat_Code, Mas_Territory_Creation t,Mas_Salesforce a  WHERE d.Sf_Code =  '" + sfcode + "'" +
                            " and d.Territory_Code =t.Territory_Code AND d.Chemists_Active_Flag = 0  and t.Territory_Active_Flag=0 and a.Sf_Code=d.Sf_Code";
            //strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Address1, d.Chemists_Contact, " +
            //                   " d.Territory_Code, d.Chemists_Phone, d.Chemists_Fax, " +
            //                   " d.Chemists_EMail, d.Chemists_Mobile,t.territory_Name " +
            //                   " FROM Mas_Chemists d, Mas_Territory_Creation t  WHERE d.Sf_Code =  '" + sfcode + "'" +
            //                   " and d.Territory_Code =t.Territory_Code AND d.Chemists_Active_Flag = 0  and t.Territory_Active_Flag=0";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        public int GetChemistCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(Max(Chemists_Code)+1,'1') Chemists_Code from Mas_Chemists";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //Changes done by Priya

        public DataSet getChemists_transfer(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code='" + terr_code + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        public DataTable getChem_transfer(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name,'' color FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code='" + terr_code + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 0";
            try
            {
                dtChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemists;
        }

        //Changes done by Priya
        public int Transfer_Chemists(string Chemists_Code, string terr_code, string sf_code, string trans_Code, string from_terr)
        {
            int iReturn = -1;
            int chemists_cd = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                chemists_cd = db.Exec_Scalar(strQry);

                //strQry = "Update Mas_Chemists " +
                //         " Set Territory_Code ='" + terr_code + "', sf_code = '" + trans_Code + "', Transfer_MR_Chemist = getdate()" +
                //         " WHERE Chemists_Code='" + Chemists_Code + "' AND sf_code = '" + sf_code + "' ";

                //iReturn = db.ExecQry(strQry);
                strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
  " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code) " +
  " Select '" + chemists_cd + "' - 1 + row_number() over (order by (select NULL)) as Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact,  " +

  "'" + terr_code + "' as Territory_Code, " +
  " Division_Code, '" + trans_Code + "' as Sf_Code, '0' as Chemists_Active_Flag, getdate() as Created_Date,Cat_Code " +
   " from Mas_Chemists a  where  Sf_Code= '" + sf_code + "' and  Territory_Code ='" + from_terr + "' and Chemists_Active_Flag=0 and Chemists_Code='" + Chemists_Code + "' ";

                iReturn = db.ExecQry(strQry);


                strQry = "update Mas_Chemists set Chemists_Active_Flag=1,Transfer_MR_Chemist=getdate(),Chemists_DeActivate_Date=getdate() where sf_code = '" + sf_code + "' and Territory_Code ='" + from_terr + "' and Chemists_Active_Flag=0 and Chemists_Code='" + Chemists_Code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        // Changes Done by Reshmi
        public DataSet getListedChemforName(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Name like '" + Name + "%'" +
                        "and d.Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        public DataSet getTerritory(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                           " UNION " +
                       " SELECT Territory_Code,Territory_Name " +
                       " FROM  Mas_Territory_Creation where Sf_Code='" + sfcode + "'" +
                       " and territory_active_flag=0 ";
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


        public DataSet getListedChemforTerr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Territory_Code = '" + terr_code + "' and d.Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        public DataTable getDTChemist_Nam(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Name like '" + Name + "%'" +
                        "and d.Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        public DataTable getDTTerr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Territory_Code = '" + terr_code + "' and d.Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        //Added by Sri - For DCR - New Chemists
        public int RecordAdd_DcrChem(string Chemists_Name, string Chemists_Address1, string Chemists_Contact, string Chemists_Phone, string Chemists_Terr, string Chemists_Cat, string sf_code, string dcr_date, string created_by)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            if (!sRecordExist(Chemists_Name, sf_code))
            {
                try
                {



                    int Division_Code = -1;
                    int chemists_code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    // strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                    chemists_code = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
                             " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code,dcr_date,created_by) " +
                             " VALUES('" + chemists_code + "', '" + Chemists_Name + "', '" + Chemists_Address1 + "', '" + Chemists_Phone + "', " +
                             " '" + Chemists_Contact + "',  '" + Chemists_Terr + "', '" + Division_Code + "','" + sf_code + "', 0, getdate(),'" + Chemists_Cat + "','" + dcr_date + "','" + created_by + "')";


                    iReturn = db.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        iReturn = chemists_code;
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                strQry = "SELECT Chemists_Code FROM Mas_Chemists WHERE Chemists_Name ='" + Chemists_Name + "' and sf_code = '" + sf_code + "'  ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                {
                    //strQry = "delete from Mas_Chemists where sf_code = '" + sf_code + "' and  Chemists_Code = " + iRecordExist;
                    //iReturn = db.ExecQry(strQry);

                    //int Division_Code = -1;
                    //int chemists_code = -1;

                    //strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    //Division_Code = db.Exec_Scalar(strQry);

                    //// strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists WHERE Division_Code = '" + Division_Code + "' ";
                    //strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                    //chemists_code = db.Exec_Scalar(strQry);

                    //strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
                    //         " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code,dcr_date,created_by) " +
                    //         " VALUES('" + chemists_code + "', '" + Chemists_Name + "', '" + Chemists_Address1 + "', '" + Chemists_Phone + "', " +
                    //         " '" + Chemists_Contact + "',  '" + Chemists_Terr + "', '" + Division_Code + "','" + sf_code + "', 0, getdate(),'" + Chemists_Cat + "','" + dcr_date + "','" + created_by + "')";


                    //iReturn = db.ExecQry(strQry);
                    //if (iReturn > 0)
                    //{
                    iReturn = iRecordExist;
                }


                //}
                //else
                //{
                //    iReturn = -2;
                //}

            }
            return iReturn;
        }
        //changes done by reshmi
        public DataSet getChemist_Allow_Admin(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemist = null;

            strQry = "Select No_Of_Sl_ChemistsAllowed from  Admin_Setups where Division_Code='" + div_code + "' ";

            try
            {
                dsChemist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemist;
        }

        public DataSet getChemist_Count(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsChemist = null;

            strQry = "SELECT COUNT(Chemists_Code) from Mas_Chemists WHERE Sf_Code = '" + sf_code + "' and Division_Code='" + div_code + "' and Chemists_Active_Flag= 0 ";

            try
            {
                dsChemist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemist;
        }
        public bool RecordExist(string Chemists_Code, string Chemists_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chemists_Name) FROM Mas_Chemists WHERE Chemists_Name = '" + Chemists_Name + "' AND Chemists_Code!='" + Chemists_Code + "' AND Sf_Code ='" + sf_code + "' ";

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

        public bool sRecordExist(string Chemists_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chemists_Name) FROM Mas_Chemists WHERE Chemists_Name='" + Chemists_Name + "' AND Sf_Code='" + sf_code + "'";
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

        //start for chemist category
        public DataSet getChemCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCat = null;

            strQry = " SELECT Cat_Code,c.Chem_Cat_SName,c.Chem_Cat_Name, " +
                     " (select count(d.Cat_Code) from Mas_Chemists d where d.Cat_Code = c.Cat_Code and Division_Code='" + divcode + "') as Cat_Count" +
                     "  FROM  Mas_Chemist_Category c" +
                     " WHERE c.Chem_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Chem_Cat_Sl_No";
            try
            {
                dsChemCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCat;
        }

        public int RecordUpdate_Chem_code(int Chem_Cat_Code, string Chem_Cat_SName, string Chem_Cat_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistS(Chem_Cat_Code, Chem_Cat_SName, divcode))
            {
                if (!sRecordExistN(Chem_Cat_Code, Chem_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        //strQry = "UPDATE Mas_Chemists " +
                        //      "SET Chem_Cat_ShortName = '" + Chem_Cat_SName + "'" +
                        //      "WHERE Chem_Cat_Code = '" + Chem_Cat_Code + "' AND Division_Code='" + divcode + "' ";

                        //iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Chemist_Category " +
                                 " SET Chem_Cat_SName = '" + Chem_Cat_SName + "', " +
                                 " Chem_Cat_Name = '" + Chem_Cat_Name + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

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

        public DataSet getChemCat_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Cat_Code,Chem_Cat_SName,Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Chem_Cat_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Chem_Cat_Sl_No";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public int ReActivate_Chemcat(int Chem_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Category " +
                            " SET Chem_Cat_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getChemCat_trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCat = null;
            strQry = "SELECT 0 as Cat_Code,'--Select--' as Chem_Cat_SName,'' as Chem_Cat_Name " +
                     " UNION " +
                     " SELECT Cat_Code,Chem_Cat_SName, Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Chem_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsChemCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCat;
        }

        public DataSet getChemCat_Transfer(string divcode, string Chem_Cat_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCls = null;

            strQry = "SELECT 0 as Cat_Code,'--Select--' as Chem_Cat_SName,'' as Chem_Cat_Name " +
                     " UNION " +
                     " SELECT Cat_Code,Chem_Cat_SName,Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Chem_Cat_Active_Flag=0 AND Division_Code=  '" + divcode + "' and Chem_Cat_SName!='" + Chem_Cat_SName + "'  " +
                     " ORDER BY 2";
            try
            {
                dsChemCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCls;
        }

        public DataSet getChemCat_count(string Chem_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Cat_Code) as Cat_Code from Mas_Chemists  where Cat_Code=" + Chem_Cat_Code + " and Chemists_Active_Flag =0";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }

        public int Update_ChemCat_Chemists(string Chem_Cat_from, string Chem_cat_to, string chkdel)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                         " SET Cat_Code = '" + Chem_cat_to + "',  " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Cat_Code = '" + Chem_Cat_from + "'  ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Chemist_Category " +
                        " SET Chem_Cat_Active_Flag = '" + chkdel + "' " +
                        " WHERE Cat_Code = '" + Chem_Cat_from + "' and Chem_Cat_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataTable getChemistCategorylist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemCat = null;

            strQry = " SELECT Cat_Code,c.Chem_Cat_SName,c.Chem_Cat_Name, " +
                    " (select count(d.Cat_Code) from Mas_Chemists d where d.Cat_Code = c.Cat_Code) as Cat_Count" +
                    "  FROM  Mas_Chemist_Category c" +
                    " WHERE c.Chem_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                    " ORDER BY c.Chem_Cat_Sl_No";
            try
            {
                dtChemCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemCat;
        }

        public int Update_ChemCatSno(string Chem_Cat_Code, string Sno)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Category " +
                         " SET Chem_Cat_Sl_No = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getChemCat_code(string divcode, string Chem_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCat = null;

            strQry = " SELECT Chem_Cat_SName,Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Cat_Code='" + Chem_Cat_Code + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsChemCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCat;
        }

        public int RecordAdd_chem(string divcode, string Chem_Cat_SName, string Chem_Cat_Name)
        {
            int iReturn = -1;
            int Chem_Cat_Code = -1;
            if (!RecordExistche(Chem_Cat_SName, divcode))
            {
                if (!sRecordExistname(Chem_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT ISNULL(COUNT(Cat_Code),0)+1 FROM Mas_Chemist_Category ";
                        Chem_Cat_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Chemist_Category(Cat_Code,Division_Code,Chem_Cat_SName,Chem_Cat_Name,Chem_Cat_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Chem_Cat_Code + "','" + divcode + "','" + Chem_Cat_SName + "', '" + Chem_Cat_Name + "',0,getdate(),getdate())";


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

        public bool RecordExistche(string Chem_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Cat_SName) FROM Mas_Chemist_Category WHERE Chem_Cat_SName='" + Chem_Cat_SName + "' AND Division_Code='" + divcode + "' ";
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

        public bool sRecordExistname(string Chem_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Cat_Name) FROM Mas_Chemist_Category WHERE Chem_Cat_Name='" + Chem_Cat_Name + "' AND Division_Code='" + divcode + "' ";
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
        public int RecordDeleteChem(int Chem_Cat_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Chemist_Category WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int DeActivateChem(int Chem_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Category " +
                            " SET Chem_Cat_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet FetchCategory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Cat_Code,'---Select---' as Chem_Cat_SName, '' as Chem_Cat_Name " +
                         " UNION " +
                     " SELECT Cat_Code,Chem_Cat_SName,Chem_Cat_Name " +
                     " FROM  Mas_Chemist_Category where division_Code = '" + div_code + "' AND Chem_Cat_Active_Flag=0 ";
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

        public bool RecordExistS(int Chem_Cat_Code, string Chem_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Cat_SName) FROM Mas_Chemist_Category WHERE Chem_Cat_SName = '" + Chem_Cat_SName + "' AND Cat_Code!='" + Chem_Cat_Code + "' AND Division_Code='" + divcode + "' ";

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

        public bool sRecordExistN(int Chem_Cat_Code, string Chem_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Cat_Name) FROM Mas_Chemist_Category WHERE Chem_Cat_Name = '" + Chem_Cat_Name + "' AND Cat_Code!='" + Chem_Cat_Code + "'AND Division_Code='" + divcode + "' ";

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
        //
        #region get Chemists For Map Doctor
        public DataSet getChemistsfor_Mappdr(string Listeddr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Chemists_Code as Chemists_Code, Chemists_Name  from Map_LstDrs_Chemists " +
                      " where Listeddr_Code ='" + Listeddr_Code + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        #endregion
        //
        #region ListedDr_Chemist_Map
        public int RecordAdd_ChemistsMap_New(string Listeddr_Code, string Chemists_Code, string Doctor_Name, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;
            DataSet dsChemists_Name;

            try
            {
                DB_EReporting db = new DB_EReporting();

                int Sl_No = -1;
                //string Product_Name = string.Empty;

                strQry = "SELECT COALESCE(MAX(CAST(Sl_No AS Int)),0)+1 FROM Map_LstDrs_Chemists ";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "select Chemists_Name  from Mas_Chemists where Division_Code='" + Division_Code + "' ";
                dsChemists_Name = db.Exec_DataSet(strQry);

                if (DocChemists_RecordExist_New(Listeddr_Code, Sf_Code, Chemists_Code))
                {
                    strQry = "update Map_LstDrs_Chemists set Chemists_Code ='" + Chemists_Code + "',Chemists_Name='" + dsChemists_Name.Tables[0].Rows[0][0].ToString() + "'  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Chemists_Code ='" + Chemists_Code + "' and Product_Priority=1 ";
                }
                else
                {
                    strQry = " insert into Map_LstDrs_Chemists (Sl_No,Listeddr_Code,Chemists_Code,Chemists_Name,Doctor_Name, " +
                       " Sf_Code,Division_Code,Created_Date,Chemists_Priority) " +
                       " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + Chemists_Code + "', '" + dsChemists_Name.Tables[0].Rows[0][0].ToString() + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                       " '" + Division_Code + "',  getdate(),'1' )";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            return iReturn;
        }

        private bool DocChemists_RecordExist_New(string Listeddr_Code, string Sf_Code, string Chemists_Code)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Map_LstDrs_Chemists " +
                         " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Chemists_Code ='" + Chemists_Code + "' ";

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
        #endregion
        //
        #region Delete_ChemistsMap
        public int Delete_ChemistsMap(string Listeddr_Code, string Chemists_Code, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (DocChemists_RecordExist_New(Listeddr_Code, Sf_Code, Chemists_Code))
                {
                    strQry = "delete from Map_LstDrs_Chemists  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Chemists_Code ='" + Chemists_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        #endregion
        //
        public DataSet getChemistsTerr(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name +' - '+ t.territory_Name as Chemists_Name FROM " +
                      "Mas_Chemists d, Mas_Territory_Creation t " +
                      "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                      "and d.Chemists_Active_Flag = 0";

            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        //
        #region ChemistProductwiseBusinessEntry
        public DataSet Trans_ChPr_Bus_HeadExist(string Sf_Code, string Chemists_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year, d.Chemists_Code, d.Product_Detail_Name, d.Product_Sale_Unit, d.Product_Quantity, d.value FROM " +
                    " Trans_Ch_Prd_BusinessEntry_Head h " +
                    " INNER JOIN Trans_Ch_Prd_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.Chemists_Code='" + Chemists_Code + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "' ";

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
        public DataSet Trans_ChPr_Bus_DetailExist(string Sf_Code, string Chemists_Code, string Product_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,d.Chemists_Code, d.Product_Detail_Name, d.Product_Sale_Unit, d.Product_Quantity, d.value FROM " +
                    " Trans_Ch_Prd_BusinessEntry_Head h " +
                    " INNER JOIN Trans_Ch_Prd_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.Chemists_Code='" + Chemists_Code + "' AND d.Product_Code= '" + Product_Code + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "' ";

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

        public int RecordAddTrans_ChPrBus_Head(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, string active,
            int Chemists_Code, string Chemists_Name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "INSERT INTO Trans_Ch_Prd_BusinessEntry_Head(Sf_Code,Division_Code,Trans_Month,Trans_Year,Active,Created_Date, " +
                        " Chemists_Code, Chemists_Name) VALUES ('" + Sf_Code + "','" + div_code + "','" + Trans_Month + "','" + Trans_Year + "','" + active + "', " +
                        " getDate(),'" + Chemists_Code + "', '" + Chemists_Name + "')";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordAddTrans_ChPrBus_Details(string Sf_Code, int div_code, int Chemists_Code, string Product_Code, string Product_Detail_Name,
            string Product_Sale_Unit, string Territory_Code, string Territory_Name, int Product_Quantity, int? MRP_Price, decimal Retailor_Price,
            int? Distributor_Price, int? NSR_Price, int? Sample_Price, decimal value)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                decimal Trans_sl_No;

                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_Ch_Prd_BusinessEntry_Head";
                Trans_sl_No = db.Exec_Scalar(strQry);

                strQry = "";
                strQry = "INSERT INTO Trans_Ch_Prd_BusinessEntry_Details(Trans_sl_No,Division_Code,Chemists_Code,Territory_Code, Territory_Name,Product_Code, Product_Detail_Name, " +
                         " Product_Sale_Unit, Product_Quantity,MRP_Price,Retailor_Price,Distributor_Price,NSR_Price,Sample_Price,value) " +
                         " VALUES ('" + Trans_sl_No + "','" + div_code + "','" + Chemists_Code + "','" + Territory_Code + "', '" + Territory_Name + "', " +
                         " '" + Product_Code + "', '" + Product_Detail_Name + "', '" + Product_Sale_Unit + "', '" + Product_Quantity + "', NULL," + Retailor_Price + ", " +
                         " NULL, NULL,NULL," + value + ")";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordUpdTrans_ChPrBus_Head(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, string active,
            int Chemists_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                DateTime Updated_Date = DateTime.Now;
                string sqlUpdated_Date = Updated_Date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                strQry = "";
                strQry = "UPDATE Trans_Ch_Prd_BusinessEntry_Head SET Updated_Date='" + sqlUpdated_Date + "' WHERE " +
                        " Sf_Code ='" + Sf_Code + "' AND Chemists_Code='" + Chemists_Code + "' AND Trans_Month ='" + Trans_Month + "' AND " +
                        " Trans_Year='" + Trans_Year + "' AND Division_Code ='" + div_code + "'";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordUpdTrans_ChPrBus_Details(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, int Chemists_Code, string Product_Code,
            string Territory_Code, int Product_Quantity, int? MRP_Price, decimal Retailor_Price,
            int? Distributor_Price, int? NSR_Price, int? Sample_Price, decimal value)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "UPDATE d SET d.Product_Quantity='" + Product_Quantity + "',d.value='" + value + "' FROM " +
                    " Trans_Ch_Prd_BusinessEntry_Details d INNER JOIN Trans_Ch_Prd_BusinessEntry_Head h ON d.Trans_Sl_No = h.Trans_Sl_No " +
                    " WHERE h.Sf_Code ='" + Sf_Code + "' AND d.Chemists_Code='" + Chemists_Code + "' AND d.Product_Code='" + Product_Code + "' " +
                    " AND Trans_Month ='" + Trans_Month + "' AND Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "'";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet ChPrBus_value(string Sf_Code, string Chemists_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "SELECT SUM(d.value) FROM " +
                    " Trans_Ch_Prd_BusinessEntry_Head h " +
                    " INNER JOIN Trans_Ch_Prd_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.Chemists_Code='" + Chemists_Code + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "'";

            try
            {
                dsValue = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsValue;

        }
        public DataSet ChPrBus_valueMR(string Sf_Code, string Chemists_Code, string div_code, int FYear, int FMonth, int TYear, int TMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "SELECT SUM(d.value) FROM " +
                    " Trans_Ch_Prd_BusinessEntry_Head h " +
                    " INNER JOIN Trans_Ch_Prd_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.Chemists_Code='" + Chemists_Code + "' AND " +
                    " h.Division_Code ='" + div_code + "' AND h.Trans_Year + h.Trans_Month BETWEEN " + FYear + "+" + FMonth + " AND " + TYear + "+" + TMonth + " ";

            try
            {
                dsValue = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsValue;

        }
        public DataSet rpChPrBus_value(string Sf_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "SELECT SUM(d.value) FROM " +
                    " Trans_Ch_Prd_BusinessEntry_Head h " +
                    " INNER JOIN Trans_Ch_Prd_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "'";

            try
            {
                dsValue = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsValue;

        }
        public DataSet Trans_ChPr_Bus_View(string Sf_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Distinct h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,h.Chemists_Code, h.Chemists_Name, d.Territory_Code, d.Territory_Name FROM " +
                    " Trans_Ch_Prd_BusinessEntry_Head h " +
                    " INNER JOIN Trans_Ch_Prd_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "' ";

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
        public DataSet Trans_ChPr_Bus_ViewMR(string Sf_Code, string div_code, int FYear, int FMonth, int TYear, int TMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Distinct h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,h.Chemists_Code, h.Chemists_Name, d.Territory_Code, d.Territory_Name FROM " +
                    " Trans_Ch_Prd_BusinessEntry_Head h " +
                    " INNER JOIN Trans_Ch_Prd_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND " +
                    " h.Division_Code ='" + div_code + "' AND h.Trans_Year + h.Trans_Month BETWEEN " + FYear + "+" + FMonth + " AND " + TYear + "+" + TMonth + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public int Delete_ChemistProductBusiness(string Sf_Code, string Chemists_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE d FROM Trans_Ch_Prd_BusinessEntry_Details d INNER JOIN Trans_Ch_Prd_BusinessEntry_Head h " +
                    " ON d.Trans_Sl_No = h.Trans_Sl_No WHERE h.Sf_Code ='" + Sf_Code + "' AND d.Chemists_Code='" + Chemists_Code + "' " +
                    " AND h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "'; " +
                    " DELETE h FROM Trans_Ch_Prd_BusinessEntry_Head h INNER JOIN Trans_Ch_Prd_BusinessEntry_Details d ON h.Trans_Sl_No = d.Trans_Sl_No " +
                    " WHERE h.Sf_Code ='" + Sf_Code + "' AND h.Chemists_Code='" + Chemists_Code + "' AND h.Trans_Month ='" + Trans_Month + "' " +
                    " AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "';";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Delete_ChemistProductDetailsBusiness(string Sf_Code, string Chemists_Code, string Product_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE d FROM Trans_Ch_Prd_BusinessEntry_Details d INNER JOIN Trans_Ch_Prd_BusinessEntry_Head h " +
                    " ON d.Trans_Sl_No = h.Trans_Sl_No WHERE h.Sf_Code ='" + Sf_Code + "' AND d.Chemists_Code='" + Chemists_Code + "' AND d.Product_Code= '" + Product_Code + "' " +
                    " AND h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "'; ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        #endregion
        //
        #region Chemist Class
        public DataSet getChemClass(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemClass = null;

            strQry = " SELECT Class_Code,c.Chem_Class_SName,c.Chem_Class_Name, " +
                     " (select count(d.Class_Code) from Mas_Chemists d where d.Class_Code = c.Class_Code and Division_Code='" + divcode + "') as Class_Count" +
                     "  FROM  Mas_Chemist_Class c" +
                     " WHERE c.Chem_Class_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Chem_Class_Sl_No";
            try
            {
                dsChemClass = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemClass;
        }

        public int RecordUpdate_ChemClass_code(int Chem_Class_Code, string Chem_Class_SName, string Chem_Class_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistSChemClass(Chem_Class_Code, Chem_Class_SName, divcode))
            {
                if (!sRecordExistNChemClass(Chem_Class_Code, Chem_Class_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Chemist_Class " +
                                 " SET Chem_Class_SName = '" + Chem_Class_SName + "', " +
                                 " Chem_Class_Name = '" + Chem_Class_Name + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Class_Code = '" + Chem_Class_Code + "' ";

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

        public DataSet getChemClass_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocClass = null;

            strQry = " SELECT Class_Code,Chem_Class_SName,Chem_Class_Name FROM  Mas_Chemist_Class " +
                     " WHERE Chem_Class_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Chem_Class_Sl_No";
            try
            {
                dsDocClass = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocClass;
        }

        public int ReActivate_Chemclass(int Chem_Class_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Class " +
                            " SET Chem_Class_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Class_Code = '" + Chem_Class_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getChemClass_trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemClass = null;
            strQry = "SELECT 0 as Class_Code,'--Select--' as Chem_Class_SName,'' as Chem_Class_Name " +
                     " UNION " +
                     " SELECT Class_Code,Chem_Class_SName, Chem_Class_Name FROM  Mas_Chemist_Class " +
                     " WHERE Chem_Class_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsChemClass = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemClass;
        }

        public DataSet getChemClass_Transfer(string divcode, string Chem_Class_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCls = null;

            strQry = "SELECT 0 as Class_Code,'--Select--' as Chem_Class_SName,'' as Chem_Class_Name " +
                     " UNION " +
                     " SELECT Class_Code,Chem_Class_SName,Chem_Class_Name FROM  Mas_Chemist_Class " +
                     " WHERE Chem_Class_Active_Flag=0 AND Division_Code=  '" + divcode + "' and Chem_Class_SName!='" + Chem_Class_SName + "'  " +
                     " ORDER BY 2";
            try
            {
                dsChemCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCls;
        }

        public DataSet getChemClass_Count(string Chem_Class_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Class_Code) as Class_Code from Mas_Chemists  where Class_Code=" + Chem_Class_Code + " and Chemists_Active_Flag =0";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }

        public int Update_ChemClass_Chemists(string Chem_Class_from, string Chem_class_to, string chkdel)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                         " SET Class_Code = '" + Chem_class_to + "',  " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Class_Code = '" + Chem_Class_from + "'  ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Chemist_Class " +
                        " SET Chem_Class_Active_Flag = '" + chkdel + "' " +
                        " WHERE Class_Code = '" + Chem_Class_from + "' and Chem_Class_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataTable getChemistClasslist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemClass = null;

            strQry = " SELECT Class_Code,c.Chem_Class_SName,c.Chem_Class_Name, " +
                    " (select count(d.Class_Code) from Mas_Chemists d where d.Class_Code = c.Class_Code) as Class_Count" +
                    "  FROM  Mas_Chemist_Class c" +
                    " WHERE c.Chem_Class_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                    " ORDER BY c.Chem_Class_Sl_No";
            try
            {
                dtChemClass = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemClass;
        }

        public int Update_ChemClassSno(string Chem_Class_Code, string Sno)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Class " +
                         " SET Chem_Class_Sl_No = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Class_Code = '" + Chem_Class_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getChemClass_code(string divcode, string Chem_Class_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemClass = null;

            strQry = " SELECT Chem_Class_SName,Chem_Class_Name FROM  Mas_Chemist_Class " +
                     " WHERE Class_Code='" + Chem_Class_Code + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsChemClass = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemClass;
        }

        public int RecordAdd_chemClass(string divcode, string Chem_Class_SName, string Chem_Class_Name)
        {
            int iReturn = -1;
            int Chem_Class_Code = -1;
            if (!RecordExistchmClass(Chem_Class_SName, divcode))
            {
                if (!sRecordExistnameChmClass(Chem_Class_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT ISNULL(COUNT(Class_Code),0)+1 FROM Mas_Chemist_Class ";
                        Chem_Class_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Chemist_Class(Class_Code,Division_Code,Chem_Class_SName,Chem_Class_Name,Chem_Class_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Chem_Class_Code + "','" + divcode + "','" + Chem_Class_SName + "', '" + Chem_Class_Name + "',0,getdate(),getdate())";


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

        public bool RecordExistchmClass(string Chem_Class_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Class_SName) FROM Mas_Chemist_Class WHERE Chem_Class_SName='" + Chem_Class_SName + "' AND Division_Code='" + divcode + "' ";
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

        public bool sRecordExistnameChmClass(string Chem_Class_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Class_Name) FROM Mas_Chemist_Class WHERE Chem_Class_Name='" + Chem_Class_Name + "' AND Division_Code='" + divcode + "' ";
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
        public int RecordDeleteChemClass(int Chem_Class_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Chemist_Class WHERE Class_Code = '" + Chem_Class_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int DeActivateChemClass(int Chem_Class_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Class " +
                            " SET Chem_Class_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Class_Code = '" + Chem_Class_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet FetchClass(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Class_Code,'---Select---' as Chem_Class_SName, '' as Chem_Class_Name " +
                         " UNION " +
                     " SELECT Class_Code,Chem_Class_SName,Chem_Class_Name " +
                     " FROM  Mas_Chemist_Class where division_Code = '" + div_code + "' AND Chem_Class_Active_Flag=0 ";
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

        public bool RecordExistSChemClass(int Chem_Class_Code, string Chem_Class_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Class_SName) FROM Mas_Chemist_Class WHERE Chem_Class_SName = '" + Chem_Class_SName + "' AND Class_Code!='" + Chem_Class_Code + "' AND Division_Code='" + divcode + "' ";

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

        public bool sRecordExistNChemClass(int Chem_Class_Code, string Chem_Class_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Class_Name) FROM Mas_Chemist_Class WHERE Chem_Class_Name = '" + Chem_Class_Name + "' AND Class_Code!='" + Chem_Class_Code + "'AND Division_Code='" + divcode + "' ";

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
        #endregion
        public DataSet getCCat_code(string divcode, string Chem_Cat_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCat = null;

            strQry = " SELECT Cat_Code, Chem_Cat_SName,Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Chem_Cat_SName='" + Chem_Cat_SName + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsChemCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCat;
        }

        public int GridColumnShowHideInsert(string screen_name, string column_name, string user, bool visible, int columncount)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(*) FROM GridShowHideColumn WHERE screen_name = '" + screen_name + "' AND userid= '" + user + "'";
                int iRecordExist = db.Exec_Scalar(strQry);
                if (columncount == 1)
                {
                    if ((iRecordExist == 0) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 2)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 1) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 3)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 2) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 4)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 3) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
              
                //else 
                //{
                //    if ((iRecordExist == 0 || iRecordExist <= 6) && screen_name != "" && user != "")
                //    {
                //        strQry = "";
                //        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                //             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                //        if (strQry != "")
                //            iReturn = db.ExecQry(strQry);
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet GridColumnShowHideGet(string screen_name, string user)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select [screen_name],[column_name],[userid],[visible] FROM [dbo].[GridShowHideColumn] WHERE [screen_name] ='" + screen_name + "' AND userid= '" + user + "'";

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

        public int GridColumnShowHideUpdate(string screen_name, string hide_columns, string show_columns, string user)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                if (user != "")
                {
                    if (show_columns != "")
                    {
                        strQry = "";
                        strQry = "UPDATE [dbo].[GridShowHideColumn] SET visible = 1 WHERE screen_name = '" + screen_name + "' AND userid= '" + user + "' AND column_name IN (" + show_columns + ")";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }

                    if (hide_columns != "")
                    {
                        strQry = "";
                        strQry = "UPDATE [dbo].[GridShowHideColumn] SET visible = 0 WHERE screen_name = '" + screen_name + "' AND userid= '" + user + "' AND column_name IN (" + hide_columns + ")";

                        if (strQry != "")
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

        public DataSet GridColumnShowHideGet1(string screen_name, string column_name, string user)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select [visible] FROM [dbo].[GridShowHideColumn] WHERE [screen_name] ='" + screen_name + "' AND column_name = '" + column_name + "' AND userid= '" + user + "'";

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
        public DataSet getChemists1(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, " +
                        " t.territory_Name,convert(char(11),d.Created_Date ,103)Created_Date FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 0 order by Chemists_Name";//convert(char(11),ListedDr_Deactivate_Date,103)
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        public DataSet getChemists_React1(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name," +
                        "convert(char(11),d.Chemists_DeActivate_Date,103) Chemists_DeActivate_Date FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 1";//convert(char(11),ListedDr_Deactivate_Date,103)
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        public DataSet FetchCategory_chem(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;


            strQry = " SELECT Cat_Code,Chem_Cat_SName,Chem_Cat_Name " +
                  " FROM  Mas_Chemist_Category where division_Code = '" + div_code + "' AND Chem_Cat_Active_Flag=0 ";
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
        public int GridColumnShowHideInsert_New(string screen_name, string column_name, string user, bool visible, int columncount)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(*) FROM GridShowHideColumn WHERE screen_name = '" + screen_name + "' AND userid= '" + user + "'";
                int iRecordExist = db.Exec_Scalar(strQry);
                if (columncount == 1)
                {
                    if ((iRecordExist == 0) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 2)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 1) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 3)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 2) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 4)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 3) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 5)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 4) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 6)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 5) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
                else if (columncount == 7)
                {
                    if ((iRecordExist == 0 || iRecordExist <= 6) && screen_name != "" && user != "")
                    {
                        strQry = "";
                        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }

                //else 
                //{
                //    if ((iRecordExist == 0 || iRecordExist <= 6) && screen_name != "" && user != "")
                //    {
                //        strQry = "";
                //        strQry = "INSERT INTO [dbo].[GridShowHideColumn]([screen_name], [column_name], [userid], [visible])" +
                //             "VALUES ('" + screen_name + "','" + column_name + "','" + user + "','" + visible + "')";

                //        if (strQry != "")
                //            iReturn = db.ExecQry(strQry);
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

    }
}
