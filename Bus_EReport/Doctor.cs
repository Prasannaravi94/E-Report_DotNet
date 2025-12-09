using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Doctor
    {
        private string strQry = string.Empty;

        public DataSet getDocSpec(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Special_Code Doc_Cat_Code, Doc_Special_SName  Doc_Cat_Name,  Doc_Special_SName Doc_Cat_SName FROM  Mas_Doctor_Speciality " +
                     " WHERE Doc_Special_Active_Flag =0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        public DataSet getCompetitor_VsOur_prdview(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Sl_No,Our_prd_code,Our_prd_name,replace(Competitor_name,'/','<br>') as Competitor_name, " +
                     " replace(Competitor_prd_name,'/','<br>') as Competitor_prd_name FROM  Map_OurPrd_CompetitorPrd " +
                     " WHERE  Division_Code='" + divcode + "' and Active_Flag=0 ";



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
        public DataSet getDocClass(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_ClsCode Doc_Cat_Code, Doc_ClsSName Doc_Cat_Name,Doc_ClsSName Doc_Cat_SName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag =0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        public DataSet geodrsMap(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select * from geodrssts where sf_code='" + sf_code + "' order by sts,ListedDr_Name ";

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
        public DataSet drsProdMap(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select * from drsprodmap where sf_code='" + sf_code + "' order by ListedDr_Name ";

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


        public DataTable drsProdMap_Details(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "SELECT  listeddr_code,STUFF((SELECT '/ ' + CAST(product_name AS VARCHAR(500)) [text()]" +
                " FROM Map_LstDrs_Product " +
                " WHERE listeddr_code = t.listeddr_code" +
                " FOR XML PATH(''), TYPE)" +
               ".value('.','NVARCHAR(MAX)'),1,2,' ') Product" +
       " FROM Map_LstDrs_Product t where sf_Code='" + sf_code + "'" +
       " GROUP BY listeddr_code";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable drsChemMap_Details(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "SELECT  listeddr_code,STUFF((SELECT '/ ' + CAST(M.Chemists_Name AS VARCHAR(500)) [text()]" +
                " FROM Map_LstDrs_Chemists C inner join mas_chemists M on C.Chemists_Code=M.chemists_code " +
                " WHERE C.listeddr_code = t.listeddr_code" +
                " FOR XML PATH(''), TYPE)" +
               ".value('.','NVARCHAR(MAX)'),1,2,' ') Product" +
       " FROM Map_LstDrs_Chemists t where sf_Code='" + sf_code + "'" +
       " GROUP BY listeddr_code";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
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

        public DataSet getDocQual(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_QuaCode Doc_Cat_Code, Doc_QuaName Doc_Cat_Name, Doc_QuaName Doc_Cat_SName FROM  Mas_Doc_Qualification " +
                     " WHERE Doc_Qua_ActiveFlag =0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        public DataSet getDocCat_terr(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,Doc_Cat_SName Doc_Cat_Name,No_of_visit FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Doc_Cat_Sl_No";
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
        public DataSet getDocCat_terr1(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " select * from Fixed_Variable_Expense_Setup where division_code='" + divcode + "' and param_type='F' order by Expense_Parameter_Code";
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
        public DataSet getDocterr_type(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_ClsCode Doc_Cat_Code, Doc_ClsSName Doc_Cat_Name FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag =0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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


        public DataSet getDocCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name, " +
                     " c.No_of_visit, " +
                     " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code and ListedDr_Active_Flag=0) as Cat_Count" +
                     "  FROM  Mas_Doctor_Category c" +
                     " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Doc_Cat_Sl_No";
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


        public DataSet getDoctorCategory(string sf_code, string cat_code, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' ";
            }

            if (sf_code != "-1")
            {
                swhere = swhere + "and a.Sf_Code = '" + sf_code + "' ";
            }

            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
            //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB,ListedDr_DOW, " +
            //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
            //           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
            //           " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
            //           " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
            //           " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
            //           " and a.Doc_ClsCode=b.Doc_ClsCode " +
            //           swhere +
            //           " order by ListedDr_Name ";

            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code," +
                       " case isnull(ListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  ListedDr_DOB end ListedDr_DOB, " +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode " +
                       swhere +
                       " order by ListedDr_Name ";
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

        public DataSet getDoctorMgr(string mgr_code, string type, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (mgr_code != "admin"))
            {
                swhere = swhere + "and a.Sf_Code in " +
                                "(select sf_code from Mas_Salesforce " +
                                " where TP_Reporting_SF = '" + mgr_code + "' ) and a.ListedDr_Active_Flag = 0";
            }
            else if ((type == "0") && (mgr_code == "admin"))
            {
                swhere = swhere + "and a.ListedDr_Active_Flag = 0 ";
            }
            else
            {
                swhere = swhere + "and a.Sf_Code in " +
                                "(select sf_code from Mas_Salesforce " +
                                " where Sf_Code !='admin' and State_Code = '" + mgr_code + "'  and sf_type = 1)";
            }

            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
            //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB, " +
            //           " ListedDr_DOW,ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
            //           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
            //            " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
            //           " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
            //             " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
            //           " and  a.Doc_ClsCode=b.Doc_ClsCode " +
            //           swhere +
            //           " order by ListedDr_Name ";
            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case isnull(ListedDr_DOB,null)" +
                       " when '1900-01-01 00:00:00.000' then null  else  ListedDr_DOB  end ListedDr_DOB," +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW,ListedDr_Mobile,ListedDr_Email, " +
                       " b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and  a.Doc_ClsCode=b.Doc_ClsCode and a.Division_Code='" + div_code + "'  " +
                       swhere +
                       " order by ListedDr_Name ";

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



        public DataSet getDoctorCategory(string sf_code, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            if (type == "0")
            {
                strQry = " select b.Doc_Cat_Name, count(a.ListedDrCode) as ListedDrCode,b.Doc_Cat_SName,  a.Doc_Cat_Code" +
                           " from Mas_ListedDr a, Mas_Doctor_Category b " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                           " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ,b.Doc_Cat_SName";
            }
            else if (type == "1")
            {
                strQry = " select b.Doc_Special_Name, count(a.ListedDrCode),b.Doc_Special_SName, a.Doc_Special_Code" +
                           " from Mas_ListedDr a, Mas_Doctor_Speciality b " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Doc_Special_Code = b.Doc_Special_Code " +
                           " group by a.Doc_Special_Code  , b.Doc_Special_Name,b.Doc_Special_SName ";
            }
            else if (type == "2")
            {
                strQry = " select b.Doc_ClsName, count(a.ListedDrCode),b.Doc_ClsSName, a.Doc_ClsCode" +
                           " from Mas_ListedDr a, Mas_Doc_Class b " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Doc_ClsCode = b.Doc_ClsCode " +
                           " group by a.Doc_ClsCode  , b.Doc_ClsName,b.Doc_ClsSName ";
            }
            else if (type == "3")
            {
                strQry = " select b.Doc_QuaName, count(a.ListedDrCode), b.Doc_QuaName, a.Doc_QuaCode" +
                           " from Mas_ListedDr a, Mas_Doc_Qualification b " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Doc_QuaCode = b.Doc_QuaCode " +
                           " group by a.Doc_QuaCode  , b.Doc_QuaName ";
            }

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

        // Sorting For DoctorCategoryList 
        public DataTable getDoctorCategorylist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDocCat = null;

            strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name,c.No_of_visit, " +
                    " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
                    "  FROM  Mas_Doctor_Category c" +
                    " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                    " ORDER BY c.Doc_Cat_Sl_No";
            try
            {
                dtDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDocCat;
        }
        public DataSet getDocCat(string divcode, string doccatcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_SName,Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Code='" + doccatcode + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public bool RecordExist(string Doc_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_SName) FROM Mas_Doctor_Category WHERE Doc_Cat_SName='" + Doc_Cat_SName + "'AND Division_Code='" + divcode + "' ";
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


        public int Update_DocClassSno(string Doc_Cat_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_Class " +
                         " SET Doc_ClsSNo = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_ClsCode = '" + Doc_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public int Update_DocCatSno(string Doc_Cat_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doctor_Category " +
                         " SET Doc_Cat_Sl_No = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        ////public int Update_DocClassSno(string Doc_Cat_Code, string Sno)
        ////{
        ////    int iReturn = -1;
        ////    //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
        ////    //{
        ////    try
        ////    {

        ////        DB_EReporting db = new DB_EReporting();

        ////        strQry = "UPDATE Mas_Doctor_Category " +
        ////                 " SET Doc_Cat_Sl_No = '" + Sno + "', " +
        ////                 " LastUpdt_Date = getdate() " +
        ////                 " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

        ////        iReturn = db.ExecQry(strQry);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////    return iReturn;
        ////}


        public int Update_DocCampSno(string Doc_Cat_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_SubCategory " +
                         " SET Doc_SubCat_SlNo = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_SubCatcode = '" + Doc_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public int Update_DocSpecSno(string Doc_Spec_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doctor_Speciality " +
                         " SET Doc_Spec_Sl_No = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Special_Code = '" + Doc_Spec_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public bool RecordExist(int Doc_Cat_Code, string Doc_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_SName) FROM Mas_Doctor_Category WHERE Doc_Cat_SName = '" + Doc_Cat_SName + "' AND Doc_Cat_Code!='" + Doc_Cat_Code + "' AND Division_Code='" + divcode + "' ";

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


        public int RecordAdd(string divcode, string Doc_Cat_SName, string Doc_Cat_Name)
        {
            int iReturn = -1;
            if (!RecordExist(Doc_Cat_SName, divcode))
            {
                if (!sRecordExist(Doc_Cat_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Doc_Cat_Code)+1,'1') Doc_Cat_Code from Mas_Doctor_Category ";
                        int Doc_Cat_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Doctor_Category(Doc_Cat_Code,Division_Code,Doc_Cat_SName,Doc_Cat_Name,Doc_Cat_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Doc_Cat_Code + "','" + divcode + "','" + Doc_Cat_SName + "', '" + Doc_Cat_Name + "',0,getdate(),getdate())";


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

        public int RecordDelete(int Doc_Cat_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Doctor_Category WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public int DeActivate(int Doc_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doctor_Category " +
                            " SET Doc_Cat_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public DataSet getDocSpe(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT s.Doc_Special_Code,s.Doc_Special_SName,s.Doc_Special_Name, " +
                     " (select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = s.Doc_Special_Code and ListedDr_Active_Flag=0) as Spec_Count," +
                       " (select COUNT(d.product_brand_code) from Product_Image_List d where Flag=0 and  	" +
                     " ',' + d.Doc_Special_Code + ',' LIKE '%,' + CAST(s.Doc_Special_Code as VARCHAR(2000)) + ',%')  as slide_count" +
                     " FROM  Mas_Doctor_Speciality s " +
                     " WHERE s.Doc_Special_Active_Flag=0 AND s.Division_Code= '" + divcode + "' " +
                     " ORDER BY s.Doc_Spec_Sl_No ";
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
        // Sorting For DoctorSpecialityList 
        public DataTable getDocSpecialitylist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDocSpe = null;

            strQry = " SELECT s.Doc_Special_Code,s.Doc_Special_SName,s.Doc_Special_Name,  " +
                    " (select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = s.Doc_Special_Code) as Spec_Count," +
                      " (select COUNT(d.product_brand_code) from Product_Image_List d where Flag=0 and  	" +
                     " ',' + d.Doc_Special_Code + ',' LIKE '%,' + CAST(s.Doc_Special_Code as VARCHAR(2000)) + ',%')  as slide_count" +
                    " FROM  Mas_Doctor_Speciality s " +
                    " WHERE s.Doc_Special_Active_Flag=0 AND s.Division_Code= '" + divcode + "' " +
                    " ORDER BY s.Doc_Spec_Sl_No ";
            try
            {
                dtDocSpe = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDocSpe;
        }
        public DataSet getDocSpe(string divcode, string docsplcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Doc_Special_SName,Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Doc_Special_Code='" + docsplcode + "' AND Division_Code= '" + divcode + "'  and Doc_Special_Active_Flag=0 " +
                     " ORDER BY 2";
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
        public bool RecordExistDocSpl(string Doc_Special_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Special_SName) FROM Mas_Doctor_Speciality WHERE Doc_Special_SName='" + Doc_Special_SName + "'AND Division_Code='" + divcode + "' ";
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

        public bool RecordExistDocSpl(int Doc_Special_Code, string Doc_Special_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Special_SName) FROM Mas_Doctor_Speciality WHERE Doc_Special_SName = '" + Doc_Special_SName + "' AND Doc_Special_Code!='" + Doc_Special_Code + "'AND Division_Code='" + divcode + "'";

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

        public int RecordAddDocSpl(string divcode, string Doc_Special_SName, string Doc_Special_Name)
        {
            int iReturn = -1;
            if (!RecordExistDocSpl(Doc_Special_SName, divcode))
            {
                if (!sRecordExistDocSpl(Doc_Special_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Doc_Special_Code)+1,'1') Doc_Special_Code from Mas_Doctor_Speciality ";
                        int Doc_Special_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Doctor_Speciality(Doc_Special_Code,Division_Code,Doc_Special_SName,Doc_Special_Name,Doc_Special_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Doc_Special_Code + "','" + divcode + "','" + Doc_Special_SName + "', '" + Doc_Special_Name + "',0,getdate(),getdate())";


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
        public int RecordUpdateDocSpl(int Doc_Special_Code, string Doc_Special_SName, string Doc_Special_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistDocSpl(Doc_Special_Code, Doc_Special_SName, divcode))
            {
                if (!sRecordExistDocSpl(Doc_Special_Code, Doc_Special_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();


                        strQry = "UPDATE Mas_ListedDr " +
                               "SET Doc_Spec_ShortName='" + Doc_Special_SName + "' " +
                               "WHERE Doc_Special_Code= '" + Doc_Special_Code + "' AND Division_Code='" + divcode + "'";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Doctor_Speciality " +
                                 " SET Doc_Special_SName = '" + Doc_Special_SName + "', " +
                                 " Doc_Special_Name = '" + Doc_Special_Name + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

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

        public int RecordDeleteDocSpl(int Doc_Special_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Doctor_Speciality WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public int DeActivateDocSpl(int Doc_Special_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doctor_Speciality " +
                            " SET Doc_Special_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getDocSubCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                     " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY Doc_SubCat_SlNo";


            //strQry = " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
            //         " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
            //         " and (Effective_From >= Convert(Date, GetDate(), 101) or Effective_To >= Convert(Date, GetDate(), 101))" +
            //         " ORDER BY Doc_SubCat_SlNo";
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
        // Sorting For DoctorCampaignList 
        public DataTable getDocSubCatlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDocSpe = null;

            strQry = " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                     " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY Doc_SubCat_SlNo";
            try
            {
                dtDocSpe = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDocSpe;
        }
        public DataSet getDocSubCat(string divcode, string docsubcatcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;


            strQry = " SELECT Doc_SubCatSName,Doc_SubCatName,convert(varchar(10),Effective_From,103) Effective_From,convert(varchar(10),Effective_To,103)Effective_To,No_Drs_Tagged,Camp_for,All_DrsTagg,No_Visit,Input_Code,Input_Name,Business_Rs,State_Code,State_Name,Sms_Code FROM  Mas_Doc_SubCategory " +
                " WHERE Doc_SubCatCode='" + docsubcatcode + "' AND Division_Code=  '" + divcode + "' " +
                " ORDER BY 2";
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
        public bool RecordExistSubCat(string Doc_SubCatSName)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatSName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatSName='" + Doc_SubCatSName + "' ";
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

        public bool RecordExistSubCat(int Doc_SubCatCode, string Doc_SubCatSName)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatSName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatSName= '" + Doc_SubCatSName + "' AND Doc_SubCatCode!='" + Doc_SubCatCode + "' ";

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

        //public int RecordAddSubCat(string divcode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To)
        //{
        //    int iReturn = -1;
        //    if (!RecordExistSubCat(Doc_SubCatSName))
        //    {
        //        try
        //        {
        //            DB_EReporting db = new DB_EReporting();
        //            string EffFromdate = Effective_From.Month.ToString() + "-" + Effective_From.Day + "-" + Effective_From.Year;
        //            string EffTodate = Effective_To.Month.ToString() + "-" + Effective_To.Day + "-" + Effective_To.Year;

        //            strQry = "INSERT INTO Mas_Doc_SubCategory(Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To)" +
        //                     "values('" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "')";


        //            iReturn = db.ExecQry(strQry);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        iReturn = -2;
        //    }
        //    return iReturn;
        //}
        public int RecordUpdateSubCat(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_Doc_SubCategory " +
                             " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                             " Doc_SubCatName = '" + Doc_SubCatName + "'," +

                                 " LastUpdt_Date = getdate() " +
                             " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

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
        public int RecordUpdateSubCatnew(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_Doc_SubCategory " +
                             " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                             " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                 " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                 " LastUpdt_Date = getdate() " +
                             " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

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

        public int RecordDeleteSubCat(int Doc_SubCatCode)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Doc_SubCategory WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public int DeActivateSubCat(int Doc_SubCatCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_SubCategory " +
                            " SET Doc_SubCat_ActiveFlag    =1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getDocCls(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName,c.Doc_ClsName, " +
                     " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode and ListedDr_Active_Flag=0) as Cls_Count " +
                     "  FROM  Mas_Doc_Class  c" +
                     " WHERE c.Doc_Cls_ActiveFlag=0 AND c.Division_Code=  '" + divcode + "' " +
                     " ORDER BY c.Doc_ClsSNo";
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
        // Sorting For DoctorClassList 
        public DataTable getDocClslist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDocCls = null;
            strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName,c.Doc_ClsName, " +
                                    " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode) as Cls_Count " +
                                    "  FROM  Mas_Doc_Class  c" +
                                    " WHERE c.Doc_Cls_ActiveFlag=0 AND c.Division_Code=  '" + divcode + "' " +
                                    " ORDER BY c.Doc_ClsSNo";
            try
            {
                dtDocCls = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDocCls;
        }


        public int getDoctorcount(string sf_code, string cat_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                          " where Sf_Code = '" + sf_code + "' " +
                          " and Doc_Cat_Code = '" + cat_code + "' and ListedDr_Active_Flag= 0 ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }




        public int getSpecialcount(string sf_code, string spec_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                          " where Sf_Code = '" + sf_code + "' " +
                          " and Doc_Special_Code = '" + spec_code + "' and ListedDr_Active_Flag=0 ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int getClasscount(string sf_code, string Doc_ClsCode)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                          " where Sf_Code = '" + sf_code + "' " +
                          " and Doc_ClsCode = '" + Doc_ClsCode + "' and ListedDr_Active_Flag = 0 ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int getQualcount(string sf_code, string qual_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                          " where Sf_Code = '" + sf_code + "' " +
                          " and Doc_QuaCode = '" + qual_code + "' and ListedDr_Active_Flag = 0 ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getDocCls(string divcode, string DocClsCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = " SELECT Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_ClsCode= '" + DocClsCode + "' AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY 2";
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
        public bool RecordExistCls(string Doc_ClsSName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_ClsSName) FROM Mas_Doc_Class WHERE Doc_ClsSName='" + Doc_ClsSName + "'AND Division_Code='" + divcode + "' ";
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


        public bool RecordExistCls(int Doc_ClsCode, string Doc_ClsSName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_ClsSName) FROM Mas_Doc_Class WHERE Doc_ClsSName= '" + Doc_ClsSName + "' AND Doc_ClsCode!='" + Doc_ClsCode + "'AND Division_Code='" + divcode + "' ";

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

        public int RecordAddCls(string divcode, string Doc_ClsSName, string Doc_ClsName)
        {
            int iReturn = -1;
            if (!RecordExistCls(Doc_ClsSName, divcode))
            {
                if (!sRecordExistCls(Doc_ClsName, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Doc_ClsCode)+1,'1') Doc_ClsCode from Mas_Doc_Class ";
                        int Doc_ClsCode = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Doc_Class(Doc_ClsCode,Division_Code,Doc_ClsSName,Doc_ClsName,Doc_Cls_ActiveFlag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Doc_ClsCode + "','" + divcode + "','" + Doc_ClsSName + "', '" + Doc_ClsName + "',0,getdate(),getdate())";


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
        public int RecordUpdateCls(int Doc_ClsCode, string Doc_ClsSName, string Doc_ClsName, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistCls(Doc_ClsCode, Doc_ClsSName, divcode))
            {
                if (!sRecordExistCls(Doc_ClsCode, Doc_ClsName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_ListedDr " +
                               "SET Doc_Class_ShortName='" + Doc_ClsSName + "' " +
                               "WHERE Doc_ClsCode='" + Doc_ClsCode + "' AND Division_Code='" + divcode + "'";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Doc_Class " +
                                 " SET Doc_ClsSName = '" + Doc_ClsSName + "', " +
                                 " Doc_ClsName = '" + Doc_ClsName + "'," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

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

        public int RecordDeleteCls(int Doc_ClsCode)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Doc_Class WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public int DeActivateCls(int Doc_ClsCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_Class " +
                            " SET Doc_Cls_ActiveFlag    =1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getDocQua(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;

            strQry = " SELECT q.Doc_QuaCode,q.Doc_QuaSName,q.Doc_QuaName, " +
                     " (select count(d.Doc_QuaCode) from Mas_ListedDr d where d.Doc_QuaCode = q.Doc_QuaCode and ListedDr_Active_Flag=0) as Qua_Count " +
                     "  FROM Mas_Doc_Qualification q " +
                     " WHERE q.Doc_Qua_ActiveFlag=0 ANd q.Division_Code= '" + divcode + "' " +
                     " ORDER BY q.DocQuaSNo";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }
        // Sorting For DoctorQualificationList 
        public DataTable getDocQualist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDocQua = null;

            strQry = " SELECT q.Doc_QuaCode,q.Doc_QuaSName,q.Doc_QuaName, " +
                    " (select count(d.Doc_QuaCode) from Mas_ListedDr d where d.Doc_QuaCode = q.Doc_QuaCode) as Qua_Count " +
                    "  FROM Mas_Doc_Qualification q " +
                    " WHERE q.Doc_Qua_ActiveFlag=0 ANd q.Division_Code= '" + divcode + "' " +
                    " ORDER BY q.DocQuaSNo";
            try
            {
                dtDocQua = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDocQua;
        }
        public DataSet getDocQua(string divcode, string DocQuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;

            strQry = "SELECT Doc_QuaSName,Doc_QuaName FROM Mas_Doc_Qualification " +
                     " WHERE Doc_QuaCode= '" + DocQuaCode + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }
        public bool RecordExistQua(string Doc_QuaName, string divcode)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_QuaName) FROM Mas_Doc_Qualification WHERE Doc_QuaName='" + Doc_QuaName + "'AND Division_Code='" + divcode + "' ";
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
        public bool RecordExistQua(int Doc_QuaCode, string Doc_QuaName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_QuaName) FROM Mas_Doc_Qualification WHERE Doc_QuaName='" + Doc_QuaName + "'AND Doc_QuaCode!='" + Doc_QuaCode + "'AND Division_Code='" + divcode + "' ";

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
        public int RecordAddQua(string divcode, string Doc_QuaSName, string Doc_QuaName)
        {
            int iReturn = -1;
            if (!RecordExistQua(Doc_QuaName, divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT isnull(max(Doc_QuaCode)+1,'1') Doc_QuaCode from Mas_Doc_Qualification ";
                    int Doc_QuaCode = db.Exec_Scalar(strQry);

                    strQry = "INSERT INTO Mas_Doc_Qualification(Doc_QuaCode,Division_Code,Doc_QuaSName,Doc_QuaName,Doc_Qua_ActiveFlag,Created_Date,LastUpdt_Date)" +
                             "values('" + Doc_QuaCode + "','" + divcode + "','" + Doc_QuaSName + "', '" + Doc_QuaName + "' ,0,getdate(),getdate())";

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
        public int Update_DocQualificationSno(string Doc_Qua_Code, string Sno)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_Qualification " +
                         " SET DocQuaSNo = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_QuaCode = '" + Doc_Qua_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordUpdateQua(int Doc_QuaCode, string Doc_QuaSName, string Doc_QuaName, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistQua(Doc_QuaCode, Doc_QuaName, divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_ListedDr " +
                           "SET Doc_Qua_Name='" + Doc_QuaName + "'" +
                           "Where Doc_QuaCode='" + Doc_QuaCode + "' AND Division_Code='" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Mas_Doc_Qualification " +
                             " SET Doc_QuaSName = '" + Doc_QuaSName + "', " +
                             " Doc_QuaName = '" + Doc_QuaName + "'," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";
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
        public int RecordDeleteQua(int Doc_QuaCode)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Mas_Doc_Qualification WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeActivateQua(int Doc_QuaCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_Qualification " +
                            " SET Doc_Qua_ActiveFlag  =1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        //Changes done by Priya -14jul // 
        // Begin 

        public DataSet getDocCat_trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "SELECT 0 as Doc_Cat_Code,'--Select--' as Doc_Cat_SName,'' as Doc_Cat_Name " +
                     " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_SName, Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocSpe_Trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = "SELECT 0 as Doc_Special_Code,'--Select--' as Doc_Special_SName,'' as Doc_Special_Name " +
                 " UNION " +
                " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Doc_Special_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2 ";
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
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocCls_Trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_ClsCode,'--Select--' as Doc_ClsSName,'' as Doc_ClsName " +
                     " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY 2";
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


        //end

        //Changes done by Priya
        //begin
        public DataSet getDocQua_Trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;

            strQry = "SELECT 0 as Doc_QuaCode,'--Select--' as Doc_QuaName " +
                     " UNION " +
                     " SELECT Doc_QuaCode,Doc_QuaName FROM Mas_Doc_Qualification " +
                     " WHERE Doc_Qua_ActiveFlag=0 ANd Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }

        //end

        //Changes done by Priya
        //begin
        //Jul 16
        public DataSet getDocSpe_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Doc_Special_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Doc_Spec_Sl_No ";
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
        //end

        //Changes done by Priya
        //begin
        public int RectivateDocSpl(int Doc_Special_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doctor_Speciality " +
                            " SET Doc_Special_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocCat_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Doc_Cat_Sl_No";
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
        //end

        //Changes done by Priya
        //begin
        public int ReActivate(int Doc_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doctor_Category " +
                            " SET Doc_Cat_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocCls_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag=1 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY Doc_ClsSNo";
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
        //end

        //Changes done by Priya
        //begin
        public int ReActivateCls(int Doc_ClsCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_Class " +
                            " SET Doc_Cls_ActiveFlag =0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //end

        //Changes done by Priya
        //begin
        public DataSet getDocQua_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;

            strQry = " SELECT Doc_QuaCode,Doc_QuaSName,Doc_QuaName FROM Mas_Doc_Qualification " +
                     " WHERE Doc_Qua_ActiveFlag=1 ANd Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }
        //end

        //Changes done by Priya
        //begin
        public int ReActivateQua(int Doc_QuaCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_Qualification " +
                            " SET Doc_Qua_ActiveFlag  =0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end
        //Changes Done by Sridevi - Starts
        public DataSet Missed_Doc(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC sp_DCR_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_SDP_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }

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

        public DataSet Missed_Doc(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode, string vMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC sp_DCR_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_SDP_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }
            else if (sMode == "3")
            {
                strQry = " EXEC sp_DCR_MissedDR_Type '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "','" + vMode + "'  ";
            }
            else if (sMode == "4")
            {
                strQry = " EXEC sp_DCR_MissedDR_Catg '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "','" + vMode + "'  ";
            }
            else if (sMode == "5")
            {
                strQry = " EXEC sp_DCR_MissedDR_Spec '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }
            else if (sMode == "6")
            {
                strQry = " EXEC sp_DCR_MissedDR_Class '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "' ,'" + vMode + "' ";
            }
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
        //Changes Done by Sridevi - Ends
        public int RecordUpdate(int Doc_Cat_Code, string Doc_Cat_SName, string Doc_Cat_Name, string no_of_visit, string divcode)
        {
            int iReturn = -1;
            if (!RecordExist(Doc_Cat_Code, Doc_Cat_SName, divcode))
            {
                if (!sRecordExist(Doc_Cat_Code, Doc_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_ListedDr " +
                              "SET Doc_Cat_ShortName = '" + Doc_Cat_SName + "'" +
                              "WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' AND Division_Code='" + divcode + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Doctor_Category " +
                                 " SET Doc_Cat_SName = '" + Doc_Cat_SName + "', " +
                                 " Doc_Cat_Name = '" + Doc_Cat_Name + "' ," +
                                 " No_of_visit = '" + no_of_visit + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

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
        //Changes done by Priya
        public DataSet getDocCls_Transfer(string divcode, string Doc_ClsName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_ClsCode,'--Select--' as Doc_ClsName,'' as Doc_ClsSName " +
                     " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag=0 AND Division_Code=  '" + divcode + "' and Doc_ClsSName!='" + Doc_ClsName + "'  " +
                     " ORDER BY 2";
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
        public DataSet getDocCat_Transfer(string divcode, string Doc_Cat_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_Cat_Code,'--Select--' as Doc_Cat_SName,'' as Doc_Cat_Name " +
                     " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Active_Flag=0 AND Division_Code=  '" + divcode + "' and Doc_Cat_SName!='" + Doc_Cat_SName + "'  " +
                     " ORDER BY 2";
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
        public DataSet getDocCat_count(string Doc_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_Cat_Code) as Doc_Cat_Code from Mas_ListedDr  where Doc_Cat_Code=" + Doc_Cat_Code + " and ListedDr_Active_Flag =0";
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
        public DataSet getUnDoc_Count(string Doc_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_Cat_Code) as Doc_Cat_Code from Mas_UnListedDr  where Doc_Cat_Code=" + Doc_Cat_Code + " and UnListedDr_Active_Flag =0";
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
        //public int GetDocCat_Count(int Doc_Cat_Code)
        //{
        //    int iReturn = -1;

        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "select COUNT(Doc_Cat_Code) as Doc_Cat_Code from Mas_ListedDr  where Doc_Cat_Code=" + Doc_Cat_Code + "";
        //                    //" WHERE Doc_QuaCode = '" + Doc_QuaCode + "' ";

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}

        public int getDoctorMRcount(string Territory_Code, string cat_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr a,Mas_Territory_Creation b " +
                         " where a.Territory_Code=cast(b.Territory_Code as varchar ) and a.ListedDr_Active_Flag=0 and a.Territory_Code in('" + Territory_Code + "') " +
                         " and Doc_Cat_Code = '" + cat_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //Changes done by Priya



        public DataSet getDoctorMgr_list(string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country,  " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case convert(char(10),a.ListedDr_DOB,103)" +
                       " when '01/01/1900' then '' " +
                       " else convert(char(10),a.ListedDr_DOB,103) end ListedDr_DOB, case convert(char(10),a.ListedDr_DOW,103) when '01/01/1900' then '' else convert(char(10),a.ListedDr_DOW,103) end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, case (visit_Session +'/' + Visit_Freq) when '/' then '' else (visit_Session +'/' + Visit_Freq) end  Product_Tagged,   " +
                          " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.sf_code =a.sf_code  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName, " +
                         " (select Alias_Name from  Mas_Territory_Creation h where h.sf_code = a.Sf_code and cast(h.Territory_Code as varchar) = a.Territory_Code ) Territory, " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where Territory_Active_Flag=0  and " +
                       " t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,a.listeddr_pincode ,Doc_Special_Name ,City" +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       "   and a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0 and a.sf_code = '" + mgr_code + "' " +

                       " order by ListedDrCode ";

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

        public DataSet getDoctorCategory_list(string sf_code, string cat_code, string terr_code, string type, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;
            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' ";
            }


            if (terr_code != "-1")
            {
                swhere = swhere + " and CHARINDEX('" + terr_code + "',a.Territory_Code) > 0   and f.Territory_Code like '%" + terr_code + "%'";
            }
            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country,  " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case convert(char(10),a.ListedDr_DOB,103)" +
                       " when '01/01/1900' then '' " +
                       " else convert(char(10),a.ListedDr_DOB,103) end ListedDr_DOB, case convert(char(10),a.ListedDr_DOW,103) when '01/01/1900' then '' else convert(char(10),a.ListedDr_DOW,103) end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name,case (visit_Session +'/' + Visit_Freq) when '/' then '' else (visit_Session +'/' + Visit_Freq) end  Product_Tagged,a.listeddr_pincode,f.Alias_Name as Territory,   " +
                        " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.sf_code =a.sf_code  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName ,Doc_Special_Name ,City" +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code and  cast(f.Territory_Code as varchar) = a.Territory_Code " +
                       " and  a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0 and a.sf_code='" + sf_code + "' and f.Territory_Active_Flag=0 and f.division_code='" + div_Code + "'  " +
                       swhere +
                       " order by ListedDr_Sl_No desc  ";

            //strQry = " SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Class_ShortName as Doc_ClsSName,d.Doc_Qua_Name as Doc_QuaName,d.SDP as Activity_Date, " +
            //    "  ListedDr_Address3, ListedDr_PinCode, d.Doc_Special_Code,d.Doc_Cat_Code,d.Doc_ClsCode,d.Territory_Code,d.ListedDr_DOB,d.ListedDr_DOW," +
            //        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and Territory_Active_Flag=0 and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name   FROM  " +
            //        " Mas_ListedDr d  WHERE d.Sf_Code =  '" + sf_code + "' and d.ListedDr_Active_Flag = 0 " +
            //        swhere +
            //        " order by ListedDr_Sl_No desc ";

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

        public DataSet getDoctorCategory_list(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select b.Doc_Cat_Name, count(a.ListedDrCode),  a.Doc_Cat_Code" +
                       " from Mas_ListedDr a, Mas_Doctor_Category b " +
                       " where a.Sf_Code = '" + sf_code + "' and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                       " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ";


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

        //Change done by Priya

        public DataSet getUnListDoctorMgr_list(string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select UnListedDrCode, UnListedDr_Name, a.Doc_QuaCode,UnListedDR_Address1,UnListedDR_Address2, " +
                       " UnListedDr_Address3, UnListedDR_Phone, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,UnListedDR_DOB,UnListedDr_DOW, " +
                       " UnListedDr_Mobile,UnListedDR_EMail, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name  " +
                       " from Mas_UnListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Territory_Code = cast(f.Territory_Code as varchar)  and a.Doc_ClsCode=b.Doc_ClsCode  and a.UnListedDr_Active_Flag=0 and a.sf_code = '" + mgr_code + "' and f.Territory_Active_Flag=0 " +

                       " order by UnListedDr_Name ";

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

        public DataSet getUnlistDoctorCategory_list(string sf_code, string cat_code, string terr_code, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' ";
            }


            if (terr_code != "-1")
            {
                swhere = swhere + " and a.Territory_Code = '" + terr_code + "' ";
            }
            strQry = " select UnListedDrCode, UnListedDr_Name, a.Doc_QuaCode,UnListedDR_Address1,UnListedDR_Address2, " +
                       " UnListedDR_Address3, UnListedDR_Phone, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,UnListedDR_DOB,UnListedDr_DOW, " +
                       " UnListedDr_Mobile,UnListedDR_EMail, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name  " +
                       " from Mas_UnListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Territory_Code = cast(f.Territory_Code as varchar)  and a.Doc_ClsCode=b.Doc_ClsCode and a.UnListedDr_Active_Flag=0 and a.sf_code='" + sf_code + "' and f.Territory_Active_Flag=0  " +
                       swhere +
                       " order by UnListedDr_Name ";

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

        public DataSet getUnlistDoctorCategory_list(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select b.Doc_Cat_Name, count(a.UnListedDrCode),  a.Doc_Cat_Code" +
                       " from Mas_UnListedDr a, Mas_Doctor_Category b " +
                       " where a.Sf_Code = '" + sf_code + "' and a.UnListedDr_Active_Flag=0 and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                       " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ";


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
        public int getUnlistDoctorMRcount(string Territory_Code, string cat_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr a,Mas_Territory_Creation b " +
                         " where a.Territory_Code=cast(b.Territory_Code as varchar ) and a.UnListedDr_Active_Flag=0 and a.Territory_Code in('" + Territory_Code + "') " +
                         " and Doc_Cat_Code = '" + cat_code + "' and sf_code = '" + sf_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getTerr_Cat_Count(string Territory_Code, string cat_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                         " where ListedDr_Active_Flag=0 and Territory_Code like '%" + Territory_Code + "%' " +
                         " and Doc_Cat_Code = '" + cat_code + "' and sf_code='" + sf_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getTerr_Spec_Count(string Territory_Code, string spec_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                                      " where ListedDr_Active_Flag=0 and Territory_Code like '%" + Territory_Code + "%' " +
                                      " and Doc_Special_Code = '" + spec_code + "' and sf_code = '" + sf_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getFixedValues(string sf_code, int rw)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (rw == 0)
                {
                    strQry = "select fixed_column1 from (select  ROW_NUMBER() over (order by Expense_Parameter_Code) as row,Expense_Parameter_Code,Expense_Parameter_Name,isnull(Fixed_Column1,0)Fixed_Column1,isnull(Fixed_Column2,0)Fixed_Column2,isnull(Fixed_Column3,0)Fixed_Column3,isnull(Fixed_Column4,0)Fixed_Column4,isnull(Fixed_Column5,0)Fixed_Column5,M.Sf_Code,F.Division_code from Fixed_Variable_Expense_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and Param_type='F')as dd where row=1";
                }
                else if (rw == 1)
                {
                    strQry = "select fixed_column2 from (select  ROW_NUMBER() over (order by Expense_Parameter_Code) as row,Expense_Parameter_Code,Expense_Parameter_Name,isnull(Fixed_Column1,0)Fixed_Column1,isnull(Fixed_Column2,0)Fixed_Column2,isnull(Fixed_Column3,0)Fixed_Column3,isnull(Fixed_Column4,0)Fixed_Column4,isnull(Fixed_Column5,0)Fixed_Column5,M.Sf_Code,F.Division_code from Fixed_Variable_Expense_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and Param_type='F')as dd where row=2";
                }
                else if (rw == 2)
                {
                    strQry = "select fixed_column3 from (select  ROW_NUMBER() over (order by Expense_Parameter_Code) as row,Expense_Parameter_Code,Expense_Parameter_Name,isnull(Fixed_Column1,0)Fixed_Column1,isnull(Fixed_Column2,0)Fixed_Column2,isnull(Fixed_Column3,0)Fixed_Column3,isnull(Fixed_Column4,0)Fixed_Column4,isnull(Fixed_Column5,0)Fixed_Column5,M.Sf_Code,F.Division_code from Fixed_Variable_Expense_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and Param_type='F')as dd where row=3";
                }
                else if (rw == 3)
                {
                    strQry = "select fixed_column4 from (select  ROW_NUMBER() over (order by Expense_Parameter_Code) as row,Expense_Parameter_Code,Expense_Parameter_Name,isnull(Fixed_Column1,0)Fixed_Column1,isnull(Fixed_Column2,0)Fixed_Column2,isnull(Fixed_Column3,0)Fixed_Column3,isnull(Fixed_Column4,0)Fixed_Column4,isnull(Fixed_Column5,0)Fixed_Column5,M.Sf_Code,F.Division_code from Fixed_Variable_Expense_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and Param_type='F')as dd where row=4";
                }
                else if (rw == 4)
                {
                    strQry = "select fixed_column5 from (select  ROW_NUMBER() over (order by Expense_Parameter_Code) as row,Expense_Parameter_Code,Expense_Parameter_Name,isnull(Fixed_Column1,0)Fixed_Column1,isnull(Fixed_Column2,0)Fixed_Column2,isnull(Fixed_Column3,0)Fixed_Column3,isnull(Fixed_Column4,0)Fixed_Column4,isnull(Fixed_Column5,0)Fixed_Column5,M.Sf_Code,F.Division_code from Fixed_Variable_Expense_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and Param_type='F')as dd where row=5";
                }
                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int getSpec_drscntSingle(string speccode, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(listeddrcode)cnt from (" +
 "select count(listeddrcode)cnt,listeddrcode,D.Doc_Special_Code,D.division_code from Mas_ListedDr D inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),D.territory_code)>0 and left(D.territory_code,2)<>'0~' and left(D.territory_code,1)<>'~' and listeddr_active_flag=0 " +
 "and D.sf_code='" + sf_code + "' and D.division_code=TR.division_code group by listeddrcode,D.Doc_Special_Code,D.division_code having count(listeddrcode)<=1) as dd " +
 "inner join Mas_Doctor_Speciality TR on charindex(cast(TR.Doc_Special_Code as varchar),DD.Doc_Special_Code)>0 and TR.division_code=dd.division_code and TR.Doc_Special_Code='" + speccode + "'";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int getSpec_drscntMultiple(string speccode, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select count(listeddrcode)cnt from (" +
"select count(listeddrcode)cnt,listeddrcode,D.Doc_Special_Code,D.division_code from Mas_ListedDr D inner join mas_territory_creation TR on charindex(cast(TR.territory_code as varchar),D.territory_code)>0 and left(D.territory_code,2)<>'0~' and left(D.territory_code,1)<>'~' and listeddr_active_flag=0 " +
"and D.sf_code='" + sf_code + "' and D.division_code=TR.division_code group by listeddrcode,D.Doc_Special_Code,D.division_code having count(listeddrcode)>1) as dd " +
"inner join Mas_Doctor_Speciality TR on charindex(cast(TR.Doc_Special_Code as varchar),DD.Doc_Special_Code)>0 and TR.division_code=dd.division_code and TR.Doc_Special_Code='" + speccode + "'";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int getTerr_Cls_Count(string Territory_Code, string cls_code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                         " where ListedDr_Active_Flag=0 and Territory_Code like '%" + Territory_Code + "%' " +
                         " and Doc_ClsCode = '" + cls_code + "' and sf_code = '" + sf_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int getTerr_Qua_Count(string Territory_Code, string Qua_code, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                                      " where ListedDr_Active_Flag=0 and Territory_Code like '%" + Territory_Code + "%' " +
                                      " and Doc_QuaCode = '" + Qua_code + "' and sf_code = '" + sf_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getUnlist_Cat_Count(string Territory_Code, string cat_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr  " +
                         " where UnListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                         " and Doc_Cat_Code = '" + cat_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getUnlist_Spec_Count(string Territory_Code, string spec_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr  " +
                                      " where UnListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_Special_Code = '" + spec_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getUnlist_Cls_Count(string Territory_Code, string cls_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr  " +
                                      " where UnListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_ClsCode = '" + cls_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getUnlist_Qua_Count(string Territory_Code, string Qua_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(UnListedDrCode) from Mas_UnListedDr  " +
                                      " where UnListedDr_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' " +
                                      " and Doc_QuaCode = '" + Qua_code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getCatgName(string catcode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT  Doc_Cat_Name FROM  Mas_Doctor_Category " +
                     " WHERE Division_Code= '" + divcode + "'  and Doc_Cat_Code= '" + catcode + "' ";

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

        public DataSet getSpecName(string specode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT  Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Division_Code= '" + divcode + "'  and Doc_Special_Code= '" + specode + "' ";

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

        public DataSet getClassName(string clscode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT  Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Division_Code= '" + divcode + "'  and Doc_ClsCode = '" + clscode + "' ";

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

        public DataSet Checkdel(string Doc_Cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsDocCat = null;

            strQry = "Update Mas_Doctor_Category set Doc_Cat_Active_Flag = 1 where Doc_Cat_code ='" + Doc_Cat_code + "'";
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

        public int Update_DocCat_Drs(string Doc_Cat_from, string Doc_cat_to, string Doc_Cat_FSName, string Doc_Cat_TSName, string chkdel)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_Cat_Code = '" + Doc_cat_to + "', Doc_Cat_ShortName = '" + Doc_Cat_TSName + "',Transfered_Date = getdate(), " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Cat_Code = '" + Doc_Cat_from + "' and Doc_Cat_ShortName ='" + Doc_Cat_FSName + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_UnListedDr " +
                         " SET Doc_Cat_Code = '" + Doc_cat_to + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Cat_Code = '" + Doc_Cat_from + "' and UnListedDr_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Doctor_Category " +
                        " SET Doc_Cat_Active_Flag = '" + chkdel + "' " +
                        " WHERE Doc_Cat_Code = '" + Doc_Cat_from + "' and Doc_Cat_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Speciality Changes 
        public DataSet getSpec_to(string divcode, string Doc_Special_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_Special_Code,'--Select--' as Doc_Special_SName,'' as Doc_Special_Name " +
                     " UNION " +
                     " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name FROM  Mas_Doctor_Speciality " +
                     " WHERE Doc_Special_Active_Flag=0 AND Division_Code=  '" + divcode + "' and Doc_Special_SName!='" + Doc_Special_SName + "'  " +
                     " ORDER BY 2";
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

        public DataSet getlistSpec_count(string Doc_Special_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_Special_Code) as Doc_Special_Code from Mas_ListedDr  where Doc_Special_Code=" + Doc_Special_Code + " and ListedDr_Active_Flag =0";


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
        public DataSet getUnlistSpec_count(string Doc_Special_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_Special_Code) as Doc_Special_Code from Mas_UnListedDr  where Doc_Special_Code=" + Doc_Special_Code + " and UnListedDr_Active_Flag =0";


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
        public int Update_DocSpec_Drs(string Doc_Spec_from, string Doc_Spec_to, string Spec_Fr_SName, string Spec_To_SName, string chkdel)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_Special_Code = '" + Doc_Spec_to + "', Doc_Spec_ShortName = '" + Spec_To_SName + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Special_Code = '" + Doc_Spec_from + "' and Doc_Spec_ShortName = '" + Spec_Fr_SName + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_UnListedDr " +
                         " SET Doc_Special_Code = '" + Doc_Spec_to + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_Special_Code = '" + Doc_Spec_from + "' and UnListedDr_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Doctor_Speciality " +
                        " SET Doc_Special_Active_Flag = '" + chkdel + "' " +
                        " WHERE Doc_Special_Code = '" + Doc_Spec_from + "' and Doc_Special_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Class Changes 
        public DataSet getCls_to(string divcode, string Doc_ClsSName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_ClsCode,'--Select--' as Doc_ClsSName,'' as Doc_ClsName " +
                     " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName FROM  Mas_Doc_Class " +
                     " WHERE Doc_Cls_ActiveFlag=0 AND Division_Code=  '" + divcode + "' and Doc_ClsSName!='" + Doc_ClsSName + "'  " +
                     " ORDER BY 2";
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

        public DataSet getlistCls_count(string Doc_Class_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_ClsCode) as Doc_ClsCode from Mas_ListedDr  where Doc_ClsCode=" + Doc_Class_Code + " and ListedDr_Active_Flag =0";


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
        public DataSet getUnlistCls_count(string Doc_Class_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_ClsCode) as Doc_ClsCode from Mas_UnListedDr  where Doc_ClsCode=" + Doc_Class_Code + " and UnListedDr_Active_Flag =0";


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
        public int Update_DocClass_Drs(string Doc_Cls_from, string Doc_Cls_to, string Cls_Fr_SName, string Cls_To_SName, string chkdel)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_ClsCode = '" + Doc_Cls_to + "', Doc_Class_ShortName = '" + Cls_To_SName + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_ClsCode = '" + Doc_Cls_from + "' and Doc_Class_ShortName = '" + Cls_Fr_SName + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_UnListedDr " +
                         " SET Doc_ClsCode = '" + Doc_Cls_to + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Doc_ClsCode = '" + Doc_Cls_from + "' and UnListedDr_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Doc_Class " +
                        " SET Doc_Cls_ActiveFlag = '" + chkdel + "' " +
                        " WHERE Doc_ClsCode = '" + Doc_Cls_from + "' and Doc_Cls_ActiveFlag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Qulification Changes 
        public DataSet getQua_to(string divcode, string Doc_QuaName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Doc_QuaCode,'--Select--' as Doc_QuaName " +
                     " UNION " +
                     " SELECT Doc_QuaCode,Doc_QuaName FROM  Mas_Doc_Qualification " +
                     " WHERE Doc_Qua_ActiveFlag=0 AND Division_Code=  '" + divcode + "' and Doc_QuaName!='" + Doc_QuaName + "'  " +
                     " ORDER BY 2";
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

        public DataSet getlistQua_count(string Doc_Qua_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_QuaCode) as Doc_QuaCode from Mas_ListedDr  where Doc_QuaCode in (" + Doc_Qua_Code + ") and ListedDr_Active_Flag =0";


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
        public DataSet getUnlistQua_count(string Doc_Qua_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Doc_QuaCode) as Doc_QuaCode from Mas_UnListedDr  where Doc_QuaCode in (" + Doc_Qua_Code + ") and UnListedDr_Active_Flag =0";


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
        public int Update_DocQua_Drs(string Doc_Qua_from, string Doc_Qua_to, string Qua_Fr_SName, string Qua_To_SName, string chkdel)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET Doc_QuaCode = '" + Doc_Qua_to + "', Doc_Qua_Name = '" + Qua_To_SName + "', " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_QuaCode in (" + Doc_Qua_from + ") ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_UnListedDr " +
                          " SET Doc_QuaCode = '" + Doc_Qua_to + "', " +
                          " LastUpdt_Date = getdate() " +
                          " WHERE Doc_QuaCode in (" + Doc_Qua_from + ") and UnListedDr_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Doc_Qualification " +
                       " SET Doc_Qua_ActiveFlag = '" + chkdel + "' " +
                       " WHERE Doc_QuaCode in (" + Doc_Qua_from + ") and Doc_Qua_ActiveFlag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Campaign

        public int getCamp_Count(string Doc_SubCatCode, string sf_code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select COUNT(listeddrcode) from Mas_ListedDr  " +
                         " where ListedDr_Active_Flag=0 and " +
                         " (Doc_SubCatCode like '" + Doc_SubCatCode + ',' + "%'  or " +
                         " Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + ',' + "%') " +
                         "  and sf_code = '" + sf_code + "' ";


                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public DataSet getCampMgr_list(string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName,   " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',a.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') Doc_SubCatName FROM " +
                       " Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0 and a.sf_code = '" + mgr_code + "' " +

                       " order by ListedDr_Name ";

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

        public DataTable getCamp_list(string sf_code, string Doc_SubCatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;
            if ((Doc_SubCatCode != "-1"))
            {
                swhere = " and  (a.Doc_SubCatCode like '" + Doc_SubCatCode + ',' + "%'  or " +
                         " a.Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + ',' + "%' " +
                         " )  ";
            }
            if (sf_code != "-1")
            {
                swhere = swhere + " and a.sf_code in ('" + sf_code + "') ";
            }

            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,a.ListedDr_DOB,a.ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName,   " +
                           " (select sf_emp_id from mas_salesforce where sf_code = a.Sf_code) Sf_Emp_id,(select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where cast(t.Territory_Code as varchar)=a.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                           " (select sf_hq from mas_salesforce where sf_code = a.Sf_code) sf_hq,stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',a.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') Doc_SubCatName FROM " +
                       "Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0  " +
                         swhere +
                      "";


            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet getDocType(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Type_Code Doc_Cat_Code, Type_Name Doc_Cat_Name FROM  Mas_Distance_Type " +
                     " WHERE Type_Active_Flag =0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public DataSet getTypeName(string catcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT  Type_Name FROM  Mas_Distance_Type " +
                     " WHERE  Type_Code= '" + catcode + "' ";

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
        // Changes done by Reshmi
        public bool sRecordExist(string Doc_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_Name) FROM Mas_Doctor_Category WHERE Doc_Cat_Name='" + Doc_Cat_Name + "'AND Division_Code='" + divcode + "' ";
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
        public int RecordUpdateCat(int Doc_Cat_Code, string Doc_Cat_SName, string Doc_Cat_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExist(Doc_Cat_Code, Doc_Cat_SName, divcode))
            {
                if (!sRecordExist(Doc_Cat_Code, Doc_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_ListedDr " +
                            "SET Doc_Cat_ShortName = '" + Doc_Cat_SName + "' " +
                            "WHERE Doc_Cat_Code='" + Doc_Cat_Code + "' AND Division_Code='" + divcode + "'";
                        iReturn = db.ExecQry(strQry);


                        strQry = "UPDATE Mas_Doctor_Category " +
                                 " SET Doc_Cat_SName = '" + Doc_Cat_SName + "', " +
                                 " Doc_Cat_Name = '" + Doc_Cat_Name + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

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
        public bool sRecordExist(int Doc_Cat_Code, string Doc_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Cat_Name) FROM Mas_Doctor_Category WHERE Doc_Cat_Name = '" + Doc_Cat_Name + "' AND Doc_Cat_Code!='" + Doc_Cat_Code + "'AND Division_Code='" + divcode + "' ";

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
        public bool sRecordExistDocSpl(int Doc_Special_Code, string Doc_Special_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Special_Name) FROM Mas_Doctor_Speciality WHERE Doc_Special_Name = '" + Doc_Special_Name + "' AND Doc_Special_Code!='" + Doc_Special_Code + "'AND Division_Code='" + divcode + "'";

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
        public bool sRecordExistDocSpl(string Doc_Special_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_Special_Name) FROM Mas_Doctor_Speciality WHERE Doc_Special_Name='" + Doc_Special_Name + "'AND Division_Code='" + divcode + "' ";
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
        public bool sRecordExistCls(int Doc_ClsCode, string Doc_ClsName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_ClsName) FROM Mas_Doc_Class WHERE Doc_ClsName= '" + Doc_ClsName + "' AND Doc_ClsCode!='" + Doc_ClsCode + "'AND Division_Code='" + divcode + "' ";

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
        public bool sRecordExistCls(string Doc_ClsName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_ClsName) FROM Mas_Doc_Class WHERE Doc_ClsName='" + Doc_ClsName + "'AND Division_Code='" + divcode + "' ";
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
        public DataSet getDocLstName(string Doc_ListName, string Div_Code, string Address, string Sf_Code, string strTerritorName, string strQualName, string StrSpec_Code, string StrCat_Code, string StrCls_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " select ListedDr_Name from Mas_ListedDr where ListedDr_Name='" + Doc_ListName + "' " +
                                " and Division_Code='" + Div_Code + "' and ListedDr_Address1 ='" + Address + "' " +
                                " and Sf_Code='" + Sf_Code + "' and Territory_Code='" + strTerritorName + "' " +
                                " and Doc_Qua_Name='" + strQualName + "' and Doc_Special_Code='" + StrSpec_Code + "' and Doc_Cat_Code='" + StrCat_Code + "' " +
                                " and Doc_ClsCode='" + StrCls_Code + "' and ListedDr_Active_Flag=0";

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

        //Changes done by Reshmi

        public DataSet getDoctorCat(string Doc_Cat_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "Select Doc_Cat_SName,Doc_Cat_Name,Doc_Cat_Sl_No,No_of_visit,Doc_Cat_Code " +
                    "FROM Mas_Doctor_Category " +
                    "where Doc_Cat_Code='" + Doc_Cat_Code + "' AND Division_Code='" + div_code + "' and Doc_Cat_Active_Flag=0 " +
                    "ORDER BY Doc_Cat_Sl_No ";
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
        public DataSet getDoctorSpec(string Doc_Special_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpec = null;
            strQry = "Select Doc_Special_SName,Doc_Special_Name,Doc_Spec_Sl_No,No_of_visit " +
                    "FROM Mas_Doctor_Speciality " +
                    "where Doc_Special_Code='" + Doc_Special_Code + "' AND Division_Code='" + div_code + "' and Doc_Special_Active_Flag=0 " +
                    "ORDER BY Doc_Spec_Sl_No ";
            try
            {
                dsDocSpec = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpec;

        }
        public DataSet getDoctorQua(string Doc_QuaCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;
            strQry = "Select Doc_QuaName,DocQuaSNo " +
                    "FROM Mas_Doc_Qualification " +
                    "where Doc_QuaCode ='" + Doc_QuaCode + "' AND Division_Code='" + div_code + "'  AND Doc_Qua_ActiveFlag=0 " +
                    "ORDER BY DocQuaSNo ";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }
        public DataSet getDoctorClass(string Doc_ClsCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoCls = null;
            strQry = "Select Doc_ClsSName,Doc_ClsName,Doc_ClsSNo,No_of_visit " +
                    "FROM Mas_Doc_Class " +
                    "where Doc_ClsCode='" + Doc_ClsCode + "' AND Division_Code='" + div_code + "' AND Doc_Cls_ActiveFlag=0 " +
                    "ORDER BY Doc_ClsSNo ";
            try
            {
                dsDoCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoCls;
        }

        //Changes done by Reshmi
        public bool sRecordExistSubCat(int Doc_SubCatCode, string Doc_SubCatName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatName= '" + Doc_SubCatName + "' AND Doc_SubCatCode!='" + Doc_SubCatCode + "'AND Division_Code='" + divcode + "' ";

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
        public bool RecordExistSubCat(int Doc_SubCatCode, string Doc_SubCatSName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatSName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatSName= '" + Doc_SubCatSName + "' AND Doc_SubCatCode!='" + Doc_SubCatCode + "'AND Division_Code='" + divcode + "' ";

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

        public int RecordUpdateSubCat(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName, divcode))
            {

                if (!sRecordExistSubCat(Doc_SubCatCode, Doc_SubCatName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Doc_SubCategory " +
                                 " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                 " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                    " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

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

        public int RecordUpdateSubCatnew(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName, divcode))
            {
                if (!sRecordExistSubCat(Doc_SubCatCode, Doc_SubCatName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Doc_SubCategory " +
                                 " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                 " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                     " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                    " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                     " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

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

        public bool RecordExistSubCat(string Doc_SubCatSName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatSName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatSName='" + Doc_SubCatSName + "'AND Division_Code='" + divcode + "'";
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
        public bool sRecordExistSubCat(string Doc_SubCatName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Doc_SubCatName) FROM Mas_Doc_SubCategory WHERE Doc_SubCatName='" + Doc_SubCatName + "'AND Division_Code='" + divcode + "'";
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

        //public int RecordAddSubCat(string divcode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To)
        //{
        //    int iReturn = -1;
        //    if (!RecordExistSubCat(Doc_SubCatSName, divcode))
        //    {
        //        if (!sRecordExistSubCat(Doc_SubCatName, divcode))
        //        {
        //            try
        //            {
        //                DB_EReporting db = new DB_EReporting();
        //                strQry = "SELECT isnull(max(Doc_SubCatCode)+1,'1') Doc_SubCatCode from Mas_Doc_SubCategory ";
        //                int Doc_SubCatCode = db.Exec_Scalar(strQry);

        //                string EffFromdate = Effective_From.Month.ToString() + "-" + Effective_From.Day + "-" + Effective_From.Year;
        //                string EffTodate = Effective_To.Month.ToString() + "-" + Effective_To.Day + "-" + Effective_To.Year;

        //                strQry = "INSERT INTO Mas_Doc_SubCategory(Doc_SubCatCode,Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To)" +
        //                         "values('" + Doc_SubCatCode + "','" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "')";


        //                iReturn = db.ExecQry(strQry);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //        else
        //        {
        //            iReturn = -2;
        //        }
        //    }
        //    else
        //    {
        //        iReturn = -3;
        //    }
        //    return iReturn;
        //}
        public int getDoctorcount_Total(string sf_code, string cat_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_Cat_Code = '" + cat_code + "' and ListedDr_Active_Flag= 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in (" + strSf_Code + ") " +
                              " and Doc_Cat_Code = '" + cat_code + "' and ListedDr_Active_Flag= 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public int getSpecialcount_Total(string sf_code, string spec_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_Special_Code = '" + spec_code + "' and ListedDr_Active_Flag=0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in(" + strSf_Code + ") " +
                              " and Doc_Special_Code = '" + spec_code + "' and ListedDr_Active_Flag=0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int getClasscount_Total(string sf_code, string Doc_ClsCode, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_ClsCode = '" + Doc_ClsCode + "' and ListedDr_Active_Flag = 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                             " where Sf_Code in(" + strSf_Code + ") " +
                             " and Doc_ClsCode = '" + Doc_ClsCode + "' and ListedDr_Active_Flag = 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getQualcount_Total(string sf_code, string qual_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_QuaCode = '" + qual_code + "' and ListedDr_Active_Flag = 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in(" + strSf_Code + ") " +
                              " and Doc_QuaCode = '" + qual_code + "' and ListedDr_Active_Flag = 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getDoctorMgr_View(string mgr_code, string type, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (mgr_code != "admin"))
            {
                swhere = swhere + "and a.Sf_Code in " +
                                "(select sf_code from Mas_Salesforce " +
                                " where sf_code in (" + mgr_code + ") ) and a.ListedDr_Active_Flag = 0";
            }
            else if ((type == "0") && (mgr_code == "admin"))
            {
                swhere = swhere + "and a.ListedDr_Active_Flag = 0 ";
            }
            else
            {
                swhere = swhere + "and a.Sf_Code in " +
                                "(select sf_code from Mas_Salesforce " +
                                " where Sf_Code !='admin' and sf_code in (" + mgr_code + ")  and sf_type = 1)";
            }


            strQry = " select row_number() over (order by ListedDr_Sl_No) ListedDr_Sl_No,ListedDrCode, ListedDr_Sl_No as ListedDr_Sl, ltrim(ListedDr_Name) as ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country, " +
                       " ListedDr_Address3, ListedDr_PinCode,a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case isnull(ListedDr_DOB,null)" +
                       " when '1900-01-01 00:00:00.000' then null  else  ListedDr_DOB  end ListedDr_DOB," +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW,ListedDr_Mobile,ListedDr_Email, " +
                       " Doc_Class_ShortName as Doc_ClsName,Doc_Qua_Name as Doc_QuaName, Doc_Cat_ShortName as Doc_Cat_Name,Doc_Spec_ShortName as Doc_Special_Name,  " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                        // " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where Territory_Active_Flag=0  and  t.SF_Code = a.Sf_Code and charindex('~ ' + cast(t.Territory_Code as varchar) + '~', '~' + a.Territory_Code) > 0 for XML path('')),1,2,'') +'~ ' territory_Name, " +
                        " (select territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and cast(t.Territory_Code as varchar) = a.Territory_Code) territory_Name, " +
                       " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName, " +
                         " (select Alias_Name from  Mas_Territory_Creation h where h.sf_code = a.Sf_code and cast(h.Territory_Code as varchar) = a.Territory_Code) Territory,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution " +
                       //" (select Sf_Name from Mas_Salesforce where " +
                       //" Sf_Code=(select TP_Reporting_SF from Mas_Salesforce " +
                       //" where Sf_Code=a.Sf_Code)) AS Reporting_Manager1, " +
                       //"(select sf_name from Mas_Salesforce where " +
                       //"Sf_Code=(select top(1)TP_Reporting_SF from Mas_Salesforce where " +
                       //" Sf_Code=(select Sf_Code from Mas_Salesforce where Sf_Code=(select TP_Reporting_SF from Mas_Salesforce where Sf_Code=a.Sf_Code)))) AS Reporting_Manager2" +
                       " from Mas_ListedDr a, Mas_Doctor_Category dc  " +
                       " where  " +
                       "  a.Division_Code='" + div_code + "' and ListedDr_Active_Flag=0  " +
                       swhere +
                       "and a.Doc_Cat_Code=dc.Doc_Cat_Code order by ListedDr_Sl_No";//,Doc_Cat_Sl_No,Doc_Special_Name ";
            //" order by ListedDrCode ";

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

        public DataSet getDoctorCategory(string sf_code, string cat_code, string type, string strsf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }

            if (sf_code != "-1")
            {
                swhere = swhere + "and a.Sf_Code = '" + sf_code + "' ";
            }

            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
            //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB,ListedDr_DOW, " +
            //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
            //           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
            //           " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
            //           " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
            //           " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
            //           " and a.Doc_ClsCode=b.Doc_ClsCode " +
            //           swhere +
            //           " order by ListedDr_Name ";

            strQry = " select row_number() over (order by d.ListedDr_Sl_No) ListedDr_Sl_No,ListedDrCode, ListedDr_Sl_No as ListedDr_Sl , ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code," +
                       " case isnull(ListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  ListedDr_DOB end ListedDr_DOB, " +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                       // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.ListedDr_Active_Flag=0 and  a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode " +
                       swhere +
                       " order by ListedDr_Sl_No ";
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
        public DataSet getDoctorCategory_Chart(string sf_code, string type, string strSf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            if (type == "0")
            {
                if (sf_code != "-1")
                {
                    strQry = " select b.Doc_Cat_Name, count(a.ListedDrCode) as ListedDrCode,b.Doc_Cat_SName,  a.Doc_Cat_Code" +
                               " from Mas_ListedDr a, Mas_Doctor_Category b " +
                               " where a.Sf_Code ='" + sf_code + "' and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                               " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ,b.Doc_Cat_SName";
                }
                else
                {
                    strQry = " select b.Doc_Cat_Name, count(a.ListedDrCode) as ListedDrCode,b.Doc_Cat_SName,  a.Doc_Cat_Code" +
                               " from Mas_ListedDr a, Mas_Doctor_Category b " +
                               " where a.Sf_Code in(" + strSf_Code + ") and a.Doc_Cat_Code = b.Doc_Cat_Code " +
                               " group by a.Doc_Cat_Code  , b.Doc_Cat_Name ,b.Doc_Cat_SName";
                }
            }
            else if (type == "1")
            {
                if (sf_code != "-1")
                {
                    strQry = " select b.Doc_Special_Name, count(a.ListedDrCode),b.Doc_Special_SName, a.Doc_Special_Code" +
                               " from Mas_ListedDr a, Mas_Doctor_Speciality b " +
                               " where a.Sf_Code ='" + sf_code + "' and a.Doc_Special_Code = b.Doc_Special_Code " +
                               " group by a.Doc_Special_Code  , b.Doc_Special_Name,b.Doc_Special_SName ";
                }
                else
                {
                    strQry = " select b.Doc_Special_Name, count(a.ListedDrCode),b.Doc_Special_SName, a.Doc_Special_Code" +
                               " from Mas_ListedDr a, Mas_Doctor_Speciality b " +
                               " where a.Sf_Code in(" + strSf_Code + ") and a.Doc_Special_Code = b.Doc_Special_Code " +
                               " group by a.Doc_Special_Code  , b.Doc_Special_Name,b.Doc_Special_SName ";
                }
            }
            else if (type == "2")
            {
                if (sf_code != "-1")
                {
                    strQry = " select b.Doc_ClsName, count(a.ListedDrCode),b.Doc_ClsSName, a.Doc_ClsCode" +
                               " from Mas_ListedDr a, Mas_Doc_Class b " +
                               " where a.Sf_Code = '" + sf_code + "' and a.Doc_ClsCode = b.Doc_ClsCode " +
                               " group by a.Doc_ClsCode  , b.Doc_ClsName,b.Doc_ClsSName ";
                }
                else
                {
                    strQry = " select b.Doc_ClsName, count(a.ListedDrCode),b.Doc_ClsSName, a.Doc_ClsCode" +
                              " from Mas_ListedDr a, Mas_Doc_Class b " +
                              " where a.Sf_Code in(" + strSf_Code + ") and a.Doc_ClsCode = b.Doc_ClsCode " +
                              " group by a.Doc_ClsCode  , b.Doc_ClsName,b.Doc_ClsSName ";
                }
            }
            else if (type == "3")
            {
                if (sf_code != "-1")
                {
                    strQry = " select b.Doc_QuaName, count(a.ListedDrCode), b.Doc_QuaName, a.Doc_QuaCode" +
                               " from Mas_ListedDr a, Mas_Doc_Qualification b " +
                               " where a.Sf_Code = '" + sf_code + "' and a.Doc_QuaCode = b.Doc_QuaCode " +
                               " group by a.Doc_QuaCode  , b.Doc_QuaName ";
                }
                else
                {
                    strQry = " select b.Doc_QuaName, count(a.ListedDrCode), b.Doc_QuaName, a.Doc_QuaCode" +
                               " from Mas_ListedDr a, Mas_Doc_Qualification b " +
                               " where a.Sf_Code in(" + strSf_Code + ") and a.Doc_QuaCode = b.Doc_QuaCode " +
                               " group by a.Doc_QuaCode  , b.Doc_QuaName ";
                }
            }

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
        public DataSet getDr_Pro_Exp(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, string type, string modee)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            //strQry = "SELECT distinct a.ListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, a.listeddr_name,a.Doc_Qua_Name,STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
            //       " and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,a.Doc_Spec_ShortName," +
            //      "a.Doc_Cat_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a ,DCRDetail_Lst_Trans b ,DCRMain_Trans c " +
            //      "WHERE a.Sf_Code='" + sf_code + "' and  a.Division_Code='" + divcode + "' and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo  " +
            //    //" and (CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '"+cdate+"' , 126) " +
            //    //" And ((CONVERT(Date, ListedDr_Deactivate_Date) >=  CONVERT(VARCHAR(50), '"+cdate+"' , 126) " +
            //    //" or ListedDr_Deactivate_Date is null))) " +
            //      "and b.Trans_Detail_Info_Code=a.ListedDrCode and CHARINDEX('" + Prod + "', b.Product_Code) > 0 and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' order by Fieldforce_Name ";


            strQry = "EXEC sp_Get_LstDr_Prd_Count_Zoom '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + type + "','" + modee + "'";

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
        public DataSet getDr_Pro_Expall(string divcode, string Sf_Code_multiple, int Year, int Month, int Prod, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "SELECT distinct a.ListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, (select Product_Detail_Name  from Mas_Product_Detail where Product_Code_SlNo='" + Prod + "') Product_Name , " +
                      " a.listeddr_name,a.Doc_Qua_Name, " +
                       //" STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
                       //" and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " a.Doc_Spec_ShortName," +
                      "a.Doc_Cat_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a ,DCRDetail_Lst_Trans b ,DCRMain_Trans c " +
                      "WHERE a.Sf_Code in(" + Sf_Code_multiple + ") and  a.Division_Code='" + divcode + "' and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo  " +
                      "and b.Trans_Detail_Info_Code=a.ListedDrCode and charindex('#" + Prod + "~','#'+ b.Product_Code) > 0 " +
                      " and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' order by Fieldforce_Name ";

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

        public DataSet getDocCat_Visit(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name,case isnull(c.No_of_visit,'') " +
                     " when '' then 1 " +
                     " when 0 then 1 " +
                     " else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
                     " FROM  Mas_Doctor_Category c" +
                     " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Doc_Cat_Sl_No";
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
        public DataSet getSpec_Exp(string div_code, string mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            if (mode == "1")
            {
                strQry = "Select Doc_Special_Code,Doc_Special_SName " +
                        "FROM Mas_Doctor_Speciality " +
                        "where  Division_Code='" + div_code + "' and Doc_Special_Active_Flag=0 " +
                        "ORDER BY Doc_Spec_Sl_No ";
            }
            else if (mode == "2")
            {
                strQry = " Select distinct Doc_Special_Code,Doc_Special_SName " +
                        " FROM Mas_Doctor_Speciality a,Mas_Doc_SubCategory b " +
                        " where  a.Division_Code='" + div_code + "' and Doc_Special_Active_Flag=0 and " +
                        " charindex(cast(Doc_Special_Code as varchar),Spec_Code) > 0 ";

            }

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

        public DataSet getCat_Exp(string div_code, string mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            if (mode == "1")
            {


                strQry = "Select Doc_Cat_Code,Doc_Cat_SName " +
                        "FROM Mas_Doctor_Category " +
                        "where  Division_Code='" + div_code + "' and Doc_Cat_Active_Flag=0 " +
                        "ORDER BY Doc_Cat_Sl_No ";
            }

            else if (mode == "2")
            {
                strQry = "Select distinct Doc_Cat_Code,Doc_Cat_SName " +
                        " FROM Mas_Doctor_Category a, Mas_Doc_SubCategory b " +
                        " where  a.Division_Code='" + div_code + "' and Doc_Cat_Active_Flag=0 and " +
                        " charindex(cast(Doc_Cat_Code as varchar),Cat_Code) > 0 ";

            }

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
        public DataSet getDocSpec_ForExpo(string strspec)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Doc_Special_Code,Doc_Special_SName from Mas_Doctor_Speciality where Doc_Special_Active_Flag=0 and  Doc_Special_Code in(" + strspec + ")";

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
        public DataSet getDocCat_ForExpo(string strcat)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Doc_Cat_Code,Doc_Cat_SName from Mas_Doctor_Category where Doc_Cat_Active_Flag=0 and  Doc_Cat_Code in(" + strcat + ")";

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
        public DataSet getDr_Pro_Exp_SpeCat(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, int Speciality)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_Get_LstDr_Prd_SpeCat_Zoom '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + Speciality + "'";


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
        public DataSet getDr_Pro_Expall_SpeCat(string divcode, string Sf_Code_multiple, int Year, int Month, int Prod, string cdate, int speciality)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "SELECT distinct a.ListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, (select Product_Detail_Name  from Mas_Product_Detail where Product_Code_SlNo='" + Prod + "') Product_Name , " +
                      " a.listeddr_name,a.Doc_Qua_Name," +
                       //" STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
                       //" and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " a.Doc_Spec_ShortName," +
                     "a.Doc_Cat_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a ,DCRDetail_Lst_Trans b ,DCRMain_Trans c " +
                     "WHERE a.Sf_Code in(" + Sf_Code_multiple + ") and  a.Division_Code='" + divcode + "' and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo  " +
                     "and b.Trans_Detail_Info_Code=a.ListedDrCode and charindex('#" + Prod + "~','#'+ b.Product_Code) > 0 " +
                     " and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' and a.Doc_Special_Code='" + speciality + "' order by Fieldforce_Name ";

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
        public DataSet getDr_Pro_Exp_categ(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, int Category)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_Get_LstDr_Prd_Category_Zoom '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + Category + "'";


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
        //start
        public DataSet getDr_Pro_Exp_categ_new(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, int Category)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_Get_LstDr_Prd_Category_Zoom_new '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + Category + "'";


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
        //end
        //sujee
        public DataSet getDr_Pro_Expall_categ(string divcode, string Sf_Code_multiple, int Year, int Month, int Prod, string cdate, int category)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "SELECT distinct a.ListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, (select Product_Detail_Name  from Mas_Product_Detail where Product_Code_SlNo='" + Prod + "') Product_Name , " +
                      " a.listeddr_name,a.Doc_Qua_Name," +
                       //" STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
                       //" and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " a.Doc_Spec_ShortName," +
                     "a.Doc_Cat_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a ,DCRDetail_Lst_Trans b ,DCRMain_Trans c " +
                     "WHERE a.Sf_Code in(" + Sf_Code_multiple + ") and  a.Division_Code='" + divcode + "' and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo  " +
                     "and b.Trans_Detail_Info_Code=a.ListedDrCode and (Patindex('" + Prod + "~%', b.Product_Code) > 0 " +
                      " (Patindex('%#" + Prod + "~%', b.Product_Code) > 0 or (Patindex(%~'" + Prod + "%', b.Product_Code) > 0 )" +
                     " and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' and a.Doc_Cat_Code='" + category + "' order by Fieldforce_Name ";

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
        public DataSet getDocCampaign(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT '' as Doc_SubCatCode, '' as Doc_SubCatSName, '---Select---' as Doc_SubCatName " +
                     " UNION " +
                     " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                     " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY Doc_SubCatName";


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
        public DataSet LoadWorkwith_camp(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " Select Sf_Code, Sf_Name, sf_Designation_Short_Name , Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' " +
                         " UNION" +
                         " Select Sf_Code,Sf_Name,sf_Designation_Short_Name,Reporting_To_SF  from Mas_Salesforce " + // AM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                         " UNION" +
                         " select Sf_Code,Sf_Name,sf_Designation_Short_Name,Reporting_To_SF from Mas_Salesforce " + // RM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                         " UNION " +
                         " select Sf_Code,Sf_Name,sf_Designation_Short_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                         " UNION " +
                         " select Sf_Code,Sf_Name,sf_Designation_Short_Name,Reporting_To_SF from Mas_Salesforce " + // ZM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) ) ) order by Reporting_To_SF Desc ";

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
        public DataTable getCamp_list_doc(string sf_code, string Doc_SubCatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;

            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,a.ListedDr_DOB,a.ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, Doc_Cat_ShortName, Doc_Spec_ShortName, Doc_Qua_Name, Doc_Class_ShortName,   " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where cast(t.Territory_Code as varchar)=a.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+a.Doc_SubCatCode)>0 for XML path('')),1,2,'') Doc_SubCatName FROM " +
                       " Mas_ListedDr a  " +
                       " where  " +
                       " a.ListedDr_Active_Flag=0  " +
                       " and  (a.Doc_SubCatCode like '" + Doc_SubCatCode + ',' + "%'  or " +

                       " a.Doc_SubCatCode like '%" + ',' + Doc_SubCatCode + ',' + "%') " +
                       "  and a.sf_code = '" + sf_code + "' order by territory_Name ";


            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public DataSet getDoc_MRCamp(string divcode, string Dr_Code, string sf_Code, int Month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " select day(a.Activity_Date) as Activity_Date,b.Product_Detail, b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,b.Worked_with_Name from dcrmain_trans a,DCRDetail_lst_trans b " +
                            " where a.trans_slno=b.trans_slno and b.trans_detail_info_code='" + Dr_Code + "'  " +
                            " and a.sf_code='" + sf_Code + "' and MONTH(a.Activity_Date)='" + Month + "' and year(a.Activity_Date) ='" + year + "'  ";


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

        public DataSet getDoc_MRCamp_WorkedWith(string divcode, string Dr_Code, string sf_Code, int Month, int day, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " select day(a.Activity_Date) as Activity_Date,b.Product_Detail, b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,  " +
                       //  " case b.Worked_with_Name when 'SELF,' then ''  else ltrim(rtrim(b.Worked_with_Name)) end as  " +
                       " ltrim(rtrim(b.Worked_with_Name)) as Worked_with_Name from dcrmain_trans a,DCRDetail_lst_trans b " +
                            " where a.trans_slno=b.trans_slno and b.trans_detail_info_code='" + Dr_Code + "'  " +
                            " and a.sf_code='" + sf_Code + "' and MONTH(a.Activity_Date)='" + Month + "' and day(a.Activity_Date) ='" + day + "' and year(a.Activity_Date) ='" + year + "'";


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


        public DataSet getPro_Sample_Doctor(string divcode, string sf_code, int Year, int Month, string cdate, int Prod)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;


            //strQry = " select distinct Product_Code_SlNo,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' ) Fieldforce_Name " +
            //           " ,(select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "' )  sf_HQ,  Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name " +
            //           " ,sum(cast(substring(c.product_Code,charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(Product_Code_SlNo as varchar)+'~')-1,charindex('$','#'+ c.Product_Code,charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code))-(charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(Product_Code_SlNo as varchar)+'~'))) as numeric)) sample " +
            //           " from DCRMain_Trans b,DCRDetail_Lst_Trans c ,Mas_Product_Detail e  where " +
            //           " c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and " +
            //           " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  and c.Trans_Detail_Info_Type=1 " +
            //           " and  charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0  and " +
            //           " (charindex('~$','#'+ Product_Code) <= 0 and charindex('~0$','#'+ Product_Code) <= 0)   and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' " +
            //           " and  c.sf_code  in ('" + sf_code + "') and b.sf_code in ('" + sf_code + "') and Product_Code_SlNo='" + Prod + "' " +
            //           " group by b.Sf_Code ,Product_Code_SlNo,Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name ";

            strQry = " select distinct Product_Code_SlNo,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' ) Fieldforce_Name " +
                   " ,(select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "' )  sf_HQ,  (select d.sf_Designation_Short_Name from mas_salesforce d where d.Sf_Code=c.Sf_Code) sf_Designation_Short_Name, Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name, " +
                   "  Doc_Qua_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,Doc_Class_ShortName, " +
                  " (STUFF((select ', '+territory_Name from Mas_Territory_Creation l where l.SF_Code=c.Sf_Code " +
                  " and CHARINDEX(cast(l.Territory_Code as varchar) +',',a.Territory_Code+',')>0 for XML path('')),1,2,''))territory_Name,convert(varchar,activity_date,103) as Date, " +
                   " sum(cast(substring(c.product_Code,charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(Product_Code_SlNo as varchar)+'~')-1,charindex('$','#'+ c.Product_Code,charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code))-(charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(Product_Code_SlNo as varchar)+'~'))) as numeric)) sample " +
                   " from DCRMain_Trans b,DCRDetail_Lst_Trans c ,Mas_Product_Detail e,Mas_ListedDr a  where " +
                   " c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and " +
                   " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  and c.Trans_Detail_Info_Type=1 " +
                   " and  charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0  and " +
                   " (charindex('#'+cast(Product_Code_SlNo as varchar)+'~$','#'+ Product_Code) <= 0 and charindex('#'+cast(Product_Code_SlNo as varchar)+'~0$','#'+ Product_Code) <= 0)   and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' " +
                   " and b.sf_code in ('" + sf_code + "') and Product_Code_SlNo='" + Prod + "'  and a.ListedDrCode=c.Trans_Detail_Info_Code " +
                   "  group by c.Sf_Code ,Product_Code_SlNo,Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name,Doc_Qua_Name,Doc_Spec_ShortName, " +
                   "  Doc_Cat_ShortName,Doc_Class_ShortName,Territory_Code,Activity_Date " +
                   " order by Fieldforce_Name,Trans_Detail_Name,Product_Detail_Name ";



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

        public DataSet getDr_Pro_Sample(string divcode, string sf_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;


            strQry = "select distinct Product_Code_SlNo,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' )   Fieldforce_Name,c.sf_code " +
                     ",(select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "' )  sf_HQ,  Product_Detail_Name " +
                     " ,count(distinct Trans_Detail_Info_Code) Sample " +
                     " from DCRMain_Trans b,DCRDetail_Lst_Trans c ,Mas_Product_Detail e  where  " +
                     " c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and  " +
                     " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  and c.Trans_Detail_Info_Type=1 " +
                     " and  charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0  and " +
                     " (charindex('#'+cast(Product_Code_SlNo as varchar)+'~$','#'+ Product_Code) <= 0 and charindex('#'+cast(Product_Code_SlNo as varchar)+'~0$','#'+ Product_Code) <= 0)  and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' " +
                     " and  c.sf_code  in ('" + sf_code + "') and b.sf_code in ('" + sf_code + "') group by c.Sf_Code , " +
                     " Product_Code_SlNo,Product_Detail_Name ";

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
        public DataSet getDr_Prd_Mapped_Name(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            //strQry = " SELECT distinct c.Listeddr_Code,d.ListedDr_Name ,d.Doc_Spec_ShortName ,d.Doc_Qua_Name ,d.Doc_Cat_ShortName ,d.Doc_Class_ShortName, " +
            //        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and " +
            //        " charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
            //        " isnull((select Product_Name+' ( '+CAST(Product_Priority as varchar)+' ), ' from " +
            //        " Map_LstDrs_Product where d.ListedDrCode=Listeddr_Code for xml path('')),'')  Product_Detail_Name from " +
            //        " Mas_ListedDr d ,Map_LstDrs_Product c " +
            //        " WHERE c.Sf_Code = '" + sf_code + "' and d.ListedDrCode =c.Listeddr_Code and c.Division_Code ='" + divcode + "' " +
            //        " and d.ListedDr_Active_Flag = 0 ";

            strQry = " SELECT distinct c.Listeddr_Code,d.ListedDr_Name ,d.Doc_Spec_ShortName ,d.Doc_Qua_Name ,d.Doc_Cat_ShortName ,d.Doc_Class_ShortName, " +
                  " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and " +
                  " charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                 // " isnull((select Product_Name+' ( '+CAST(Product_Priority as varchar)+' ), ' from " +
                 // " Map_LstDrs_Product where d.ListedDrCode=Listeddr_Code for xml path('')),'')  Product_Detail_Name from " +
                 " (select Product_Name from Map_LstDrs_Product where Listeddr_Code=d.ListedDrCode and Product_Priority='1') Product_Priority1, " +
                 " (select Product_Name from Map_LstDrs_Product where Listeddr_Code=d.ListedDrCode and Product_Priority='2') Product_Priority2, " +
                 "  (select Product_Name from Map_LstDrs_Product where Listeddr_Code=d.ListedDrCode and Product_Priority='3') Product_Priority3, sf_name,sf_hq,sf_Designation_Short_Name " +
                  " from Mas_ListedDr d ,Map_LstDrs_Product c, Mas_Salesforce s " +
                  " WHERE c.Sf_Code = '" + sf_code + "' and d.ListedDrCode =c.Listeddr_Code and c.Division_Code ='" + divcode + "' " +
                  " and d.ListedDr_Active_Flag = 0 and c.sf_code=s.sf_code";


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
        public DataSet getDr_Prd_DCR_Name(string divcode, string sf_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            //strQry = " select distinct Listeddr_Code,e.ListedDr_Name,e.Doc_Spec_ShortName,e.Doc_Qua_Name, " +
            //         " e.Doc_Cat_ShortName,e.Doc_Class_ShortName, " +
            //         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = e.Sf_Code and " +
            //         //" charindex(cast(t.Territory_Code as varchar)+',',e.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
            //          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = e.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),e.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
            //         " isnull((select Product_Name+' ( '+CAST(Product_Priority as varchar)+' ), ' from " +
            //         " Map_LstDrs_Product where e.ListedDrCode=Listeddr_Code for xml path('')),'') Product_Detail_Name " +
            //         " from DCRMain_Trans b, DCRDetail_Lst_Trans c ,Mas_ListedDr e,Mas_Product_Detail P,Map_LstDrs_Product m where " +
            //         " c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo  and  " +
            //         " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  " +
            //         " and c.Trans_Detail_Info_Type=1  and c.Trans_Detail_Info_Code =m.Listeddr_Code and " +
            //         " c.Trans_Detail_Info_Code=e.ListedDrCode and e.ListedDr_Active_Flag=0 " +
            //         " and month(b.Activity_Date)='" + Month + "' and  YEAR(b.Activity_Date)= '" + Year + "' and  c.sf_code in " +
            //         " ('" + sf_code + "') and b.sf_code in ('" + sf_code + "') ";

            strQry = "EXEC getproduct_Mapped_visit '" + divcode + "', '" + sf_code + "','" + Year + "' ,'" + Month + "' ";


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

        public DataSet getDocCat_View(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name,case c.No_of_visit " +
                     " when isnull(c.No_of_visit,'') then 1 " +
                     " when ISNULL(c.No_of_visit,0) then 1 " +
                     " else c.No_of_visit end No_of_visit, " +
                     " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code) as Cat_Count" +
                     " FROM  Mas_Doctor_Category c" +
                     " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Doc_Cat_SName";
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

        public DataSet getPrdCountDoctor(string divcode, string sf_code, int Prod)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_get_PrdCountDoctor_Mapp '" + divcode + "', '" + sf_code + "','" + Prod + "'";


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

        public DataSet Visit_Doctor_DCR(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            //strQry = " EXEC sp_DCR_VisitDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";

            //strQry = " select distinct a.ListedDrCode,a.ListedDr_Name, " +
            //         // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and " +
            //         //" charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
            //         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
            //        " a.Doc_Qua_Name,a.Doc_Cat_ShortName, " +
            //        " a.Doc_Spec_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a where a.Division_Code ='" + div_code + "' and a.listeddr_active_flag=0 " +
            //        " and a.sf_code in ('" + sf_code + "') and ((CONVERT(Date, ListedDr_Created_Date) < '" + dtcurrent + "') " +
            //        " And (CONVERT(Date, ListedDr_Deactivate_Date) >= '" + dtcurrent + "' or " +
            //        " ListedDr_Deactivate_Date is null)) or a.ListedDrCode " +
            //        " in(select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c " +
            //        " where  b.Trans_SlNo = c.Trans_SlNo  and c.Trans_Detail_Info_Type=1 and c.Trans_Detail_Info_Code=a.ListedDrCode " +
            //        " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "' and " +
            //        " c.sf_code in ('" + sf_code + "')) order by 3,2 ";

            strQry = " select distinct a.ListedDrCode,a.ListedDr_Name, " +
                    // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and " +
                    //" charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                    " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                   " a.Doc_Qua_Name,a.Doc_Cat_ShortName, " +
                   " a.Doc_Spec_ShortName,a.Doc_Class_ShortName from Mas_ListedDr a where a.Division_Code ='" + div_code + "' and a.listeddr_active_flag=0 " +
                   " and a.sf_code in ('" + sf_code + "') and ((CONVERT(Date, ListedDr_Created_Date) < '" + dtcurrent + "') " +
                   " And (CONVERT(Date, ListedDr_Deactivate_Date) >= '" + dtcurrent + "' or " +
                   " ListedDr_Deactivate_Date is null)) or a.ListedDrCode=c.Trans_Detail_Info_Code " +
                  // " in(select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c " +
                   " and b.Trans_SlNo = c.Trans_SlNo  and c.Trans_Detail_Info_Type=1 and c.Trans_Detail_Info_Code=a.ListedDrCode " +
                   " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "' and " +
                   " c.sf_code in ('" + sf_code + "')) order by 3,2 ";

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
        public DataSet Visit_Camp(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string cMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " select distinct a.ListedDrCode,a.ListedDr_Name, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and " +
                       " charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " a.Doc_Qua_Name,a.Doc_Cat_ShortName, " +
                       " a.Doc_Spec_ShortName,a.Doc_Class_ShortName, " +
                        " stuff((select '/'+Doc_SubCatName from " +
                       " Mas_Doc_SubCategory sub where division_code='" + div_code + "' and " +
                        " CHARINDEX(cast(sub.Doc_SubCatCode as varchar),a.Doc_SubCatCode) > 0 " +
                        " for XML path('')),1,1,'') campain_name " +
                       " from Mas_ListedDr a where a.Division_Code ='" + div_code + "' and a.listeddr_active_flag=0  " +
                       " and a.sf_code in ('" + sf_code + "') and ((CONVERT(Date, ListedDr_Created_Date) < '" + dtcurrent + "') " +
                       " And (CONVERT(Date, ListedDr_Deactivate_Date) >= '" + dtcurrent + "' or " +
                       " ListedDr_Deactivate_Date is null and a.Doc_SubCatCode !='')) or a.ListedDrCode " +
                       " in(select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c " +
                       " where  b.Trans_SlNo = c.Trans_SlNo  and c.Trans_Detail_Info_Type=1 and c.Trans_Detail_Info_Code=a.ListedDrCode and a.Doc_SubCatCode !='' " +
                       " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "' and " +
                       " c.sf_code in ('" + sf_code + "')) order by 3,2 ";

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

        public DataSet Visit_Doctor_DCR_Dates(string sf_code, string div_code, int cmon, int cyear, string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            //strQry = " EXEC sp_DCR_VisitDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";

            strQry = " select distinct DAY(Activity_Date) Activity_Date ,FieldWork_Indicator,a.Sf_Code ,b.Trans_Detail_Info_Code " +
                    " from DCRMain_Trans a ,DCRDetail_Lst_Trans b ,Mas_ListedDr c where " +
                    " a.Division_Code='" + div_code + "' and a.Sf_Code='" + sf_code + "' " +
                    " and a.FieldWork_Indicator ='F' and b.Trans_Detail_Info_Type =1 and " +
                    " b.Trans_Detail_Info_Code ='" + ListedDrCode + "'  and b.Trans_Detail_Info_Code =c.ListedDrCode and MONTH(a.Activity_Date)='" + cmon + "' and Year(a.Activity_Date)='" + cyear + "' " +
                    " and a.Trans_SlNo = b.Trans_SlNo ";

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

        public DataSet getDCR_Leave_Type(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " select Leave_code,Leave_SName from mas_Leave_Type where Division_Code='" + div_code + "' and Active_Flag =0 ";

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

        public DataSet Visit_Doctor_DCR_Dates_Total(string sf_code, string div_code, int cmon, int cyear, int date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            //strQry = " EXEC sp_DCR_VisitDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";

            strQry = " select COUNT(Activity_Date)Activity_Date " +
                    " from DCRMain_Trans a ,DCRDetail_Lst_Trans b ,Mas_ListedDr c where " +
                    " a.Division_Code='" + div_code + "' and a.Sf_Code='" + sf_code + "' " +
                    " and a.FieldWork_Indicator ='F' and b.Trans_Detail_Info_Type =1 and " +
                    " DAY(activity_date)='" + date + "'   and b.Trans_Detail_Info_Code =c.ListedDrCode and MONTH(a.Activity_Date)='" + cmon + "' and Year(a.Activity_Date)='" + cyear + "' " +
                    " and a.Trans_SlNo = b.Trans_SlNo ";

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
        public DataSet Missed_Doc_cnt(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC sp_DCR_MissedDR_temp '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_SDP_MissedDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            }

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

        public DataSet getDr_Input_Sample(string divcode, string sf_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "select distinct e.Gift_Code ,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' ) Fieldforce_Name ,c.sf_code , " +
                    " (select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "')  sf_HQ,e.Gift_Name, " +
                    " count(distinct Trans_Detail_Info_Code) Sample  from DCRMain_Trans b,DCRDetail_Lst_Trans c , " +
                    " Mas_Gift e ,Mas_ListedDr a where c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and  " +
                    " charindex('#'+cast(e.Gift_Code as varchar)+'~','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar) " +
                    " +'#'+c.Additional_Gift_Code) > 0 and (charindex('~$','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar)+'#'+c.Additional_Gift_Code) <= 0 " +
                    " and charindex('~0$','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar)+'#'+c.Additional_Gift_Code) <= 0 " +
                    " and c.Trans_Detail_Info_Type=1 and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' and  c.sf_code " +
                    " in ('" + sf_code + "') and b.sf_code in ('" + sf_code + "')) and " +
                    " b.Trans_SlNo = c.Trans_SlNo and  ((CONVERT(Date, ListedDr_Created_Date) < '" + cdate + "') " +
                    " And (CONVERT(Date, ListedDr_Deactivate_Date) >= '" + cdate + "' or " +
                    " ListedDr_Deactivate_Date is null)) group by c.Sf_Code ,e.Gift_Code,e.Gift_Name ";

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

        public DataSet getInput_Sample_Doctor(string divcode, string sf_code, int Year, int Month, int Gift_Code, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " select distinct e.Gift_Code ,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' ) Fieldforce_Name ,c.sf_code , " +
                    " (select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "')  sf_HQ,e.Gift_Name,ListedDrCode,ListedDr_Name,convert(varchar,Activity_Date,103)Activity_Date " +
                    " from DCRMain_Trans b,DCRDetail_Lst_Trans c , " +
                    " Mas_Gift e,Mas_ListedDr a  where c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and " +
                    " charindex('#'+cast(e.Gift_Code as varchar)+'~','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar) " +
                    " +'#'+c.Additional_Gift_Code) > 0 and (charindex('~$','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar)+'#'+c.Additional_Gift_Code) <= 0 " +
                    " and charindex('~0$','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar)+'#'+c.Additional_Gift_Code) <= 0 " +
                    " and c.Trans_Detail_Info_Type=1 and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' and  c.sf_code " +
                    " in ('" + sf_code + "') and b.sf_code in ('" + sf_code + "')) and e.Gift_Code='" + Gift_Code + "'  and " +
                    " b.Trans_SlNo = c.Trans_SlNo and a.ListedDrCode=c.Trans_Detail_Info_Code  group by c.Sf_Code ,e.Gift_Code,e.Gift_Name ,ListedDrCode,ListedDr_Name,Activity_Date ";
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

        public DataSet getinput_productname(string divcode, string sf_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select s.Sf_Name as FeildforceName, a.Gift_Name,Despatch_qty from mas_salesforce s inner join Trans_Input_Despatch_Head b on b.Sf_Code=s.Sf_Code INNER JOIN Trans_Input_Despatch_Details a ON a.Trans_sl_No=b.Trans_sl_No and a.Division_Code=b.Division_Code  where s.Sf_Code='" + sf_code + "' and b.Trans_Month='" + Month + "' and b.Trans_Year='" + Year + "' and  b.Division_Code='" + divcode + "'";
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
        public DataSet getinput_productname_Datewise(string divcode, string sf_code, string FromDate, string ToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select s.Sf_Name as FeildforceName, a.Gift_Name,Despatch_qty from mas_salesforce s inner join Trans_Input_Despatch_Head b on b.Sf_Code=s.Sf_Code INNER JOIN Trans_Input_Despatch_Details a ON a.Trans_sl_No=b.Trans_sl_No and a.Division_Code=b.Division_Code  where s.Sf_Code='" + sf_code + "' and a.Create_dt between '" + FromDate + "' and '" + ToDate + "' and  b.Division_Code='" + divcode + "'";
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

        public DataSet getinput_status(string divcode, string sf_code, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select distinct(a.productc),s.Sf_Name as FeildforceName, a.Gift_Name from mas_salesforce s inner join Trans_Input_Despatch_Head b on b.Sf_Code=s.Sf_Code INNER JOIN Trans_Input_Despatch_Details a ON a.Trans_sl_No=b.Trans_sl_No and a.Division_Code=b.Division_Code  where s.Sf_Code='" + sf_code + "'  and  b.Division_Code='" + divcode + "'";
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
        public DataSet getdespatch_productname(string divcode, string sf_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select s.Sf_Name as FeildforceName, a.Product_Code,a.Despatch_qty from mas_salesforce s inner join Trans_Sample_Despatch_Head b on b.Sf_Code=s.Sf_Code INNER JOIN Trans_Sample_Despatch_Details a ON a.Trans_sl_No=b.Trans_sl_No and a.Division_Code=b.Division_Code  where s.Sf_Code='" + sf_code + "' and b.Trans_Month='" + Month + "' and b.Trans_Year='" + Year + "' and  b.Division_Code='" + divcode + "'";
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
        public DataSet getdespatch_productname_Datewise(string divcode, string sf_code, string FromDate, string ToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select s.Sf_Name as FeildforceName, a.Product_Code,a.Despatch_qty from mas_salesforce s inner join Trans_Sample_Despatch_Head b on b.Sf_Code=s.Sf_Code INNER JOIN Trans_Sample_Despatch_Details a ON a.Trans_sl_No=b.Trans_sl_No and a.Division_Code=b.Division_Code  where s.Sf_Code='" + sf_code + "' and create_dt between '" + FromDate + "' and '" + ToDate + "' and  b.Division_Code='" + divcode + "'";
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

        public DataSet getdespatch_status(string divcode, string sf_code, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select  distinct (a.productc),s.Sf_Name as FeildforceName, a.Product_Code,a.Productsaleunit from mas_salesforce s inner join Trans_Sample_Despatch_Head b on b.Sf_Code=s.Sf_Code INNER JOIN Trans_Sample_Despatch_Details a ON a.Trans_sl_No=b.Trans_sl_No and a.Division_Code=b.Division_Code  where s.Sf_Code='" + sf_code + "'and  b.Division_Code='" + divcode + "'";
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

        public DataSet Visit_Doctor_DCR_Matrix(string div_code, string sf_code, int cmon, int cyear, int day, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            DataSet dsAdm = null;
            string swhere = string.Empty;

            //strQry = " EXEC sp_DCR_VisitDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";

            //strQry = "select distinct DAY(Activity_Date) Activity_Date,DATENAME(DW,Activity_Date) Date_Name ,FieldWork_Indicator,a.Sf_Code ,c.ListedDrCode,c.ListedDr_Name , " +
            //         " b.Trans_Detail_Info_Code  from DCRMain_Trans a ,DCRDetail_Lst_Trans b ,Mas_ListedDr c " +
            //        " where  a.Division_Code='" + div_code + "' and a.Sf_Code='" + sf_code + "'  and a.FieldWork_Indicator ='F' and " +
            //        " b.Trans_Detail_Info_Type =1 and  " +
            //        " b.Trans_Detail_Info_Code =c.ListedDrCode and MONTH(a.Activity_Date)='" + cmon + "' and DAY(Activity_Date)='" + day + "' " +
            //        " and Year(a.Activity_Date)='" + cyear + "'  and a.Trans_SlNo = b.Trans_SlNo ";


            strQry = " select '' Activity_Date,'DAY '+'" + day + ":' + DATENAME( DW,'" + sCurrentDate + "') Date_Name ,'F' FieldWork_Indicator,'1' Sf_Code,'1' " +
                    "ListedDrCode ,'Listed Doctor Name' ListedDr_Name,'Qualification' Doc_Qua_Name,'Speciality' Doc_Spec_ShortName,'Category' Doc_Cat_ShortName,'Class' Doc_Class_ShortName,'Product Name' Product_Detail,'1' Trans_Detail_Info_Code union  " +
                    " select distinct convert (varchar,DAY(Activity_Date)) Activity_Date,'' Date_Name , " +
                    " FieldWork_Indicator,a.Sf_Code ,convert(int,c.ListedDrCode),c.ListedDr_Name ,c.Doc_Qua_Name,c.Doc_Spec_ShortName, c.Doc_Cat_ShortName,c.Doc_Class_ShortName,b.Product_Detail, convert(int, b.Trans_Detail_Info_Code) " +
                    " from DCRMain_Trans a ,DCRDetail_Lst_Trans b ,Mas_ListedDr c  where  a.Division_Code='" + div_code + "' " +
                    " and a.Sf_Code='" + sf_code + "'  and a.FieldWork_Indicator ='F' and  b.Trans_Detail_Info_Type =1 and  " +
                    " b.Trans_Detail_Info_Code =c.ListedDrCode and MONTH(a.Activity_Date)='" + cmon + "' and DAY(Activity_Date)='" + day + "' " +
                     " and Year(a.Activity_Date)='" + cyear + "'  and a.Trans_SlNo = b.Trans_SlNo ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);

                if (dsDocCat.Tables[0].Rows.Count > 1)
                {
                    dsAdm = db_ER.Exec_DataSet(strQry);
                }
                else
                {
                    strQry = "  select '' Activity_Date,'DAY '+'" + day + ":' + DATENAME( DW,'" + sCurrentDate + "') Date_Name ,'F' FieldWork_Indicator,'1' Sf_Code,convert(int,'1') " +
                             "  ListedDrCode ,(select WorkType_Name from DCRMain_Trans where sf_code='" + sf_code + "' and MONTH(Activity_Date)='" + cmon + "' and YEAR(Activity_Date)='" + cyear + "' and DAY(Activity_Date)='" + day + "') " +
                             " ListedDr_Name,'.' Doc_Qua_Name,'.' Doc_Spec_ShortName,'.' Doc_Cat_ShortName,'.' Doc_Class_ShortName,'.' Product_Detail,convert(int,'1') Trans_Detail_Info_Code ";
                    dsAdm = db_ER.Exec_DataSet(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }


        public DataTable Missed_sort(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDocCat = null;

            strQry = "  EXEC sp_DCR_MissedDR_temp '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";
            try
            {
                dtDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDocCat;
        }
        public DataSet Missed_Cal_Mon(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC sp_DCR_MissedDR_temp '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";



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

        public DataSet Missed_Cal_Monitor(string div_code, string sf_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Listeddr_CallMonitor '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "'  ";



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

        public DataSet getDocCat_Code(string divcode, string Doc_Sname)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Doc_Cat_Code from Mas_Doctor_Category where Doc_Cat_SName='" + Doc_Sname + "' and Division_Code='" + divcode + "'";
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

        public DataSet getDocSpec_Code(string divcode, string Doc_Special_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Doc_Special_Code from Mas_Doctor_Speciality where Doc_Special_SName='" + Doc_Special_SName + "' and Division_Code='" + divcode + "'";
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


        public DataSet getDocClass_Code(string divcode, string Doc_ClsSName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Doc_ClsCode from Mas_Doc_Class where Doc_ClsSName='" + Doc_ClsSName + "' and Division_Code='" + divcode + "'";
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
        public DataSet getDocQuali_Code(string divcode, string Doc_Special_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Doc_QuaCode from Mas_Doc_Qualification where Doc_QuaSName='" + Doc_Special_SName + "' and Division_Code='" + divcode + "'";
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

        public DataSet rptgetDoctorCategory(string sf_code, string cat_code, string type, string strsf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }

            if (sf_code.Contains("MR"))
            {
                swhere = swhere + "and a.Sf_Code = '" + sf_code + "' ";
            }

            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
            //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB,ListedDr_DOW, " +
            //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
            //           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
            //           " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
            //           " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
            //           " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
            //           " and a.Doc_ClsCode=b.Doc_ClsCode " +
            //           swhere +
            //           " order by ListedDr_Name ";

            strQry = " select row_number() over (order by ListedDr_Sl_No) ListedDr_Sl_No,ListedDrCode, ListedDr_Sl_No as ListedDr_Sl, ltrim(ListedDr_Name) as ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country, " +
                       " ListedDr_Address3, ListedDr_PinCode,a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code," +
                       " case isnull(ListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  ListedDr_DOB end ListedDr_DOB, " +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email,Doc_Class_ShortName as Doc_ClsName,Doc_Qua_Name as Doc_QuaName,Doc_Cat_ShortName as Doc_Cat_Name,Doc_Spec_ShortName as Doc_Special_Name, " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                       //  " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       //  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                       //" stuff((select '~ '+territory_Name from Mas_Territory_Creation t where Territory_Active_Flag=0  and  t.SF_Code = a.Sf_Code and charindex('~ ' + cast(t.Territory_Code as varchar) + '~', '~' + a.Territory_Code) > 0 for XML path('')),1,2,'') +'~ ' territory_Name, " +
                       " (select territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and cast(t.Territory_Code as varchar) = a.Territory_Code) territory_Name, " +
                         " (select Alias_Name from  Mas_Territory_Creation h where h.sf_code = a.Sf_code and cast(h.Territory_Code as varchar) = a.Territory_Code) Territory, " +
                        " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName ,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution,(select No_of_visit from Mas_Doctor_Category c where c.Doc_Cat_Code=a.Doc_Cat_Code) AS visit " +
                       //" (select Sf_Name from Mas_Salesforce where " +
                       //" Sf_Code=(select TP_Reporting_SF from Mas_Salesforce " +
                       //" where Sf_Code=a.Sf_Code)) AS Reporting_Manager1, " +
                       //"(select sf_name from Mas_Salesforce where " +
                       //"Sf_Code=(select top(1)TP_Reporting_SF from Mas_Salesforce where " +
                       //" Sf_Code=(select Sf_Code from Mas_Salesforce where Sf_Code=(select TP_Reporting_SF from Mas_Salesforce where Sf_Code=a.Sf_Code)))) AS Reporting_Manager2" +
                       " from Mas_ListedDr a, Mas_Doctor_Category dc " +
                       " where a.ListedDr_Active_Flag=0  " +

                       swhere +
                       //" order by ListedDrCode ";
                       "and a.Doc_Cat_Code=dc.Doc_Cat_Code order by ListedDr_Sl_No";//,Doc_Cat_Sl_No,Doc_Special_Name ";
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

        public DataSet getDr_Pro_Exp_unlisted(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;



            strQry = "EXEC sp_Get_LstDr_Prd_Count_Zoom_unlisted '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + type + "'";


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

        public DataSet getDr_Pro_Expall_Unlisted(string divcode, string Sf_Code_multiple, int Year, int Month, int Prod, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT distinct a.unListedDrCode,(select sf_name from mas_salesforce s where a.Sf_Code=s.Sf_Code ) Fieldforce_Name, " +
                    " (select Product_Detail_Name  from Mas_Product_Detail where Product_Code_SlNo='" + Prod + "') Product_Name , " +
                    " a.unlisteddr_name,q.Doc_QuaSName as Doc_Qua_Name, " +
                     //" STUFF((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code=a.Sf_Code " +
                     //" and CHARINDEX(cast(t.Territory_Code as varchar) +',' ,cast(a.Territory_Code as varchar)+',')>0 for XML path('')),1,2,'') " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                    " s.Doc_Special_SName as Doc_Spec_ShortName,d.Doc_Cat_SName as Doc_Cat_ShortName, " +
                    " dc.Doc_ClsSName  as Doc_Class_ShortName from Mas_unListedDr a ,DCRDetail_Unlst_Trans b ,DCRMain_Trans c , " +
                    " mas_doc_qualification q,mas_doctor_speciality s,Mas_Doctor_Category d ,mas_Doc_Class dc " +
                    " WHERE a.Sf_Code in(" + Sf_Code_multiple + ") and  a.Division_Code='" + divcode + "' " +
                    " and a.Sf_Code=b.sf_code and c.Trans_SlNo = b.Trans_SlNo " +
                    " and b.Trans_Detail_Info_Code=a.unlisteddrcode andcharindex('#" + Prod + "~','#'+ b.Product_Code) > 0 " +
                    " and MONTH(c.Activity_Date)='" + Month + "' and year(c.Activity_Date)='" + Year + "' " +
                    " and a.Doc_Quacode=q.Doc_Quacode " +
                    " and a.Doc_Special_Code=s.Doc_Special_Code " +
                    " and a.Doc_Cat_Code=d.Doc_Cat_Code " +
                    " and a.Doc_ClsCode=dc.Doc_ClsCode order by Fieldforce_Name ";



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

        public DataSet getDr_Pro_Exp_total(string divcode, string sf_code, int Year, int Month, int Prod, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;



            strQry = "EXEC sp_Get_LstDr_Prd_Count_Zoom_Total '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "' ";


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

        public DataSet Sample_Total(string divcode, string sf_code, int Year, int Month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;


            strQry = "EXEC Sample_Total '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "'";


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

        public DataSet getSamplePrd_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Product_Code_SlNo,Product_Detail_Name from mas_product_detail where division_code='" + div_code + "' and product_Active_Flag=0 ";
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

        public DataSet getPro_RxQty_Doctor(string divcode, string sf_code, int Year, int Month, string cdate, int Prod, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            if (type == "0")
            {

                strQry = "select *,cast(Rx_qty as float)*cast(Retailor_Price as float) as valuee from ( select * ,(SUBSTRING(STUFF(STUFF(new,1,CHARINDEX(cast('' as varchar)+'~$',new + '$'),''), 1, " +
                          " CHARINDEX('$',STUFF(new,1,CHARINDEX(cast('' as varchar)+'~$',new + '$'),'')), ''), 0, " +
                          " CHARINDEX('$', STUFF(STUFF(new,1,CHARINDEX(cast('' as varchar)+'~$',new + '$'),''), 1, " +
                          " CHARINDEX('$',STUFF(new,1,CHARINDEX(cast('' as varchar)+'~$',new + '$'),'')), '')))) as Rx_qty from (" +
                    " select distinct Trans_Detail_Info_Code,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' ) Fieldforce_Name " +
                           " ,(select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "' )  sf_HQ,  (select d.sf_Designation_Short_Name from mas_salesforce d where d.Sf_Code=c.Sf_Code) sf_Designation_Short_Name, Product_Detail_Name,Trans_Detail_Name, " +
                           "  Doc_Qua_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,Doc_Class_ShortName, " +
                          " (STUFF((select ', '+territory_Name from Mas_Territory_Creation l where l.SF_Code=c.Sf_Code " +
                          " and CHARINDEX(cast(l.Territory_Code as varchar) +',',a.Territory_Code+',')>0 for XML path('')),1,2,''))territory_Name,convert(varchar,activity_date,103) as Date, " +
                           " sum(cast(substring(c.product_Code,charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(e.Product_Code_SlNo as varchar)+'~')-1,charindex('$','#'+ c.Product_Code,charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code))-(charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(e.Product_Code_SlNo as varchar)+'~'))) as numeric)) sample, " +
                            " (REPLACE(SUBSTRING(product_Code, CHARINDEX('" + Prod + "', product_Code), LEN(product_Code)), '" + Prod + "', '') ) as new,Retailor_Price " +
                           " from DCRMain_Trans b,DCRDetail_Lst_Trans c ,Mas_Product_Detail e,Mas_ListedDr a ,Mas_Product_State_Rates rate where " +
                           "rate.Product_Detail_Code=e.Product_Detail_Code and c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and " +
                           " charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  and c.Trans_Detail_Info_Type=1 " +
                           " and  charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0  and " +
                           " (charindex('$$','#'+ Product_Code+'#') <= 0 and charindex('$0$','#'+ Product_Code+'#') <= 0) and " +
                           " (charindex('$#','#'+ Product_Code+'#') <= 0 and charindex('$0#','#'+ Product_Code+'#') <= 0) " +
                           " and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' " +
                           " and b.sf_code in ('" + sf_code + "') and e.Product_Code_SlNo='" + Prod + "'  and a.ListedDrCode=c.Trans_Detail_Info_Code " +
                           "  group by c.Sf_Code ,e.Product_Code_SlNo,Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name,Doc_Qua_Name,Doc_Spec_ShortName, " +
                           "  Doc_Cat_ShortName,Doc_Class_ShortName,Territory_Code,Activity_Date,Product_Code,Retailor_Price ) ttt ) ttttt" +
                           " order by Fieldforce_Name,Trans_Detail_Name,Product_Detail_Name ";
            }

            else if (type == "1")
            {

                strQry = "select *,cast(Rx_qty as float)*cast(Retailor_Price as float) as valuee from ( select * ,(SUBSTRING(STUFF(STUFF(new,1,CHARINDEX(cast('' as varchar)+'~$',new + '$'),''), 1, " +
                          " CHARINDEX('$',STUFF(new,1,CHARINDEX(cast('' as varchar)+'~$',new + '$'),'')), ''), 0, " +
                          " CHARINDEX('$', STUFF(STUFF(new,1,CHARINDEX(cast('' as varchar)+'~$',new + '$'),''), 1, " +
                          " CHARINDEX('$',STUFF(new,1,CHARINDEX(cast('' as varchar)+'~$',new + '$'),'')), '')))) as Rx_qty from (" +
                    " select distinct Trans_Detail_Info_Code,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' ) Fieldforce_Name " +
                        " ,(select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "' )  sf_HQ,  (select d.sf_Designation_Short_Name from mas_salesforce d where d.Sf_Code=c.Sf_Code) sf_Designation_Short_Name, Product_Detail_Name,Trans_Detail_Name, " +
                        "  Doc_Qua_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,Doc_Class_ShortName, " +
                       " (STUFF((select ', '+territory_Name from Mas_Territory_Creation l where l.SF_Code=c.Sf_Code " +
                       " and CHARINDEX(cast(l.Territory_Code as varchar) +',',a.Territory_Code+',')>0 for XML path('')),1,2,''))territory_Name,convert(varchar,activity_date,103) as Date, " +
                        " sum(cast(substring(c.product_Code,charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(e.Product_Code_SlNo as varchar)+'~')-1,charindex('$','#'+ c.Product_Code,charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code))-(charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(e.Product_Code_SlNo as varchar)+'~'))) as numeric)) sample, " +
                         " (REPLACE(SUBSTRING(product_Code, CHARINDEX('" + Prod + "', product_Code), LEN(product_Code)), '" + Prod + "', '') ) as new,Retailor_Price " +
                        " from DCRMain_Trans b,DCRDetail_Lst_Trans c ,Mas_Product_Detail e,Mas_ListedDr a,mas_product_brand f,Mas_Product_State_Rates rate where " +
                        "rate.Product_Detail_Code=e.Product_Detail_Code and c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and " +
                        " charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  and c.Trans_Detail_Info_Type=1 " +
                        " and  charindex('#'+cast(e.Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0  and " +
                        " (charindex('$$','#'+ Product_Code+'#') <= 0 and charindex('$0$','#'+ Product_Code+'#') <= 0) and " +
                        " (charindex('$#','#'+ Product_Code+'#') <= 0 and charindex('$0#','#'+ Product_Code+'#') <= 0) " +
                        " and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' " +
                        " and b.sf_code in ('" + sf_code + "') and e.Product_Brd_Code='" + Prod + "' and e.Product_Brd_Code=f.Product_Brd_Code  and a.ListedDrCode=c.Trans_Detail_Info_Code " +
                        "  group by c.Sf_Code ,e.Product_Code_SlNo,Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name,Doc_Qua_Name,Doc_Spec_ShortName, " +
                        "  Doc_Cat_ShortName,Doc_Class_ShortName,Territory_Code,Activity_Date ,Product_Code,Retailor_Price) ttt ) ttttt" +
                        " order by Fieldforce_Name,Trans_Detail_Name,Product_Detail_Name ";
            }


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

        public DataSet SampleRxQty_Total(string divcode, string sf_code, int Year, int Month, int prod, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;


            strQry = "EXEC SampleRxQty_Total '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "','" + prod + "','" + type + "' ";


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

        public DataSet getRx_code(string strspec)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select product_code_slno,product_detail_name from mas_product_detail where Product_Active_flag=0 and  product_code_slno in(" + strspec + ")";

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

        public DataSet getSamplePrd_Brand(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Product_Brd_Code,Product_Brd_Name from Mas_Product_Brand " +
                      " where division_code='" + div_code + "' and Product_Brd_Active_Flag=0 ";
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

        public DataSet getRx_Brand_code(string strspec)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = "select Product_Brd_Code,Product_Brd_Name from Mas_Product_Brand where Product_Brd_Active_Flag=0 and  Product_Brd_Code in(" + strspec + ")";

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
        public DataSet getProduct_Sam(string divcode, string Dr_Code, string sf_Code, int Month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " select distinct Product_Code_SlNo,  Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name " +
                     " ,sum(cast(substring(c.product_Code,charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(Product_Code_SlNo as varchar)+'~')-1,charindex('$','#'+ c.Product_Code,charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code))-(charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code)+len('#'+cast(Product_Code_SlNo as varchar)+'~'))) as int)) sample " +
                     " from DCRMain_Trans b,DCRDetail_Lst_Trans c ,Mas_Product_Detail e  where " +
                     " c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and  " +
                     " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  and c.Trans_Detail_Info_Type=1 " +
                     " and  charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0  " +
                     //(charindex('~$','#'+ Product_Code) = 0 and charindex('~0$','#'+ Product_Code) = 0)  
                     " and month(b.Activity_Date)=" + Month + " and YEAR(b.Activity_Date)= " + year + "  " +
                     " and  c.sf_code  in ('" + sf_Code + "') and b.sf_code in ('" + sf_Code + "') and trans_detail_info_code='" + Dr_Code + "'  " +
                     " group by b.Sf_Code ,Product_Code_SlNo,Product_Detail_Name,Trans_Detail_Info_Code,Trans_Detail_Name  ";

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

        public DataSet getDr_Pro_Priority(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, string type, int priority)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;


            strQry = "EXEC sp_Get_LstDr_Prd_Priority_Count_Zoom '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + type + "','" + priority + "' ";

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

        public DataSet getDr_Pro_Priority_total(string divcode, string sf_code, int Year, int Month, int Prod, string cdate, int priority)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;



            strQry = "EXEC sp_Get_LstDr_Prd_Priority_Count_Zoom_Total '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' , '" + cdate + "' ,'" + Prod + "','" + priority + "' ";


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
        public DataSet getDocQualification(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocQua = null;

            strQry = " SELECT Doc_QuaCode,Doc_QuaName FROM Mas_Doc_Qualification " +
                     " WHERE Doc_Qua_ActiveFlag=0 ANd Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsDocQua = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocQua;
        }
        public DataSet Docbusvalwiseentry_busvaldetail(string divcode, string sf_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            //strQry = "select ML.ListedDr_Name,ML.Doc_Cat_ShortName,ML.Doc_Spec_ShortName,MT.Territory_Name, a.Business_Value  from mas_salesforce s inner join Trans_Dr_Business_Valuewise_Head b on b.Sf_Code=s.Sf_Code INNER JOIN Trans_Dr_Business_Valuewise_Detail a ON a.Trans_sl_No=b.Trans_sl_No and a.Division_Code=b.Division_Code inner join Mas_ListedDr ML  on ML.Sf_Code=s.Sf_Code and a.ListedDr_Code=ML.ListedDrCode inner join (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE Division_Code ='" + divcode + "' and SF_Code = '" + sf_code + "') MT ON MT.Territory_Code = ML.Territory_Code  where s.Sf_Code='" + sf_code + "' and b.Trans_Month='" + Month + "' and b.Trans_Year='" + Year + "' and  b.Division_Code='" + divcode + "'";
            strQry = "select ML.ListedDr_Name,ML.Doc_Cat_ShortName,ML.Doc_Spec_ShortName,MT.Territory_Name, b.Business_Value  from mas_salesforce s inner join Trans_Dr_Business_Valuewise_Head b on b.Sf_Code=s.Sf_Code  inner join Mas_ListedDr ML  on ML.Sf_Code=s.Sf_Code and b.ListedDr_Code=ML.ListedDrCode inner join (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE Division_Code ='" + divcode + "' and SF_Code = '" + sf_code + "') MT ON MT.Territory_Code = ML.Territory_Code  where s.Sf_Code='" + sf_code + "' and b.Trans_Month='" + Month + "' and b.Trans_Year='" + Year + "' and  b.Division_Code='" + divcode + "'";


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
        public DataSet Docbusvalwiseentry_busvaldetailMR(string divcode, string sf_code, int FYear, int FMonth, int TYear, int TMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_get_DocBusValwiseMRView '" + sf_code + "', '" + divcode + "','" + FYear + "' ,'" + FMonth + "','" + TYear + "', '" + TMonth + "'";
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
        public DataTable Missed_Doc_Camp_Sort(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode, string vMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;


            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                     " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , a.Doc_Qua_Name as Doc_QuaName, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where dc.division_code=a.division_code and   charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+a.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName " +
                     " from Mas_ListedDr a where a.Division_Code ='" + div_code + "'   and Doc_SubCatCode !='' and a.sf_code in ('" + sf_code + "') and " +
                     " ((CONVERT(Date, a.ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126)) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                     " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                     " (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where " +
                     " b.Trans_SlNo = c.Trans_SlNo and c.Trans_Detail_Info_Type=1  and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "')   ";


            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet Missed_Doc_Camp(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode, string vMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                     " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , a.Doc_Qua_Name as Doc_QuaName, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where dc.division_code=a.division_code and   charindex(cast(dc.Doc_SubCatCode as varchar)+',',a.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName " +
                     " from Mas_ListedDr a where a.Division_Code ='" + div_code + "'   and Doc_SubCatCode !='' and a.sf_code in ('" + sf_code + "') and " +
                     " ((CONVERT(Date, a.ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126)) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                     " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                     " (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where " +
                     " b.Trans_SlNo = c.Trans_SlNo and c.Trans_Detail_Info_Type=1  and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "')   ";


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

        public DataSet getCompetitor(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Comp_Sl_No,Comp_Name FROM  Mas_Competitor " +
                     " WHERE Active_Flag=0 AND Division_Code=  '" + divcode + "' order by Comp_Name";



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

        public int RecordAddCompetitor(string divcode, string Competitor_Name, string type)
        {
            int iReturn = -1;


            if (type == "1")
            {
                if (!sRecordExistCompetitor(Competitor_Name, divcode, type))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "INSERT INTO Mas_Competitor(Division_Code,Comp_Name,Active_Flag,Created_Date,Last_Updtdate)" +
                                 "values('" + divcode + "','" + Competitor_Name + "',0,getdate(),getdate())";


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
                if (!sRecordExistCompetitor(Competitor_Name, divcode, type))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "INSERT INTO Mas_Competitor_Product(Division_Code,Comp_Prd_Name,Active_Flag,Created_Date,Last_Updtdate)" +
                                 "values('" + divcode + "','" + Competitor_Name + "',0,getdate(),getdate())";


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




            return iReturn;

        }
        public bool sRecordExistCompetitor(string Competitor_Name, string divcode, string type)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                if (type == "1")
                {

                    strQry = "SELECT COUNT(Comp_Name) FROM Mas_Competitor WHERE Comp_Name='" + Competitor_Name + "'AND Division_Code='" + divcode + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);
                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (type == "2")
                {
                    strQry = "SELECT COUNT(Comp_Prd_Name) FROM Mas_Competitor_Product WHERE Comp_Prd_Name='" + Competitor_Name + "'AND Division_Code='" + divcode + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);
                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (type == "3")
                {
                    strQry = "SELECT COUNT(Comp_Sl_No) FROM Map_Competitor_Product WHERE Comp_Sl_No='" + Competitor_Name + "'AND Division_Code='" + divcode + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);
                    if (iRecordExist > 0)
                        bRecordExist = true;
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }


        public DataSet getCompetitor_Prd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Comp_Prd_Sl_No,Comp_Prd_Name FROM  Mas_Competitor_Product " +
                     " WHERE Active_Flag=0 AND Division_Code=  '" + divcode + "'";



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
        public DataSet get_OurProduct(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "  select Product_Code_SlNo,Product_Detail_Name " +
                     " from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag=0 ";
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
        public DataSet get_Comp_prdcode(string div_code, string prd_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Comp_Sl_No,Comp_Name,Comp_prd_sl_no from (  select Comp_Sl_No,Comp_Name, ('/'+Comp_prd_sl_no+'/') as Comp_prd_sl_no " +
                    " from Map_Competitor_Product where division_code='" + div_code + "' ) tt where Comp_prd_sl_no like '%/" + prd_code + "/%' ";
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
        public int UpdateOur_Prd_Competitor_Prd(string Our_prd_code, string Our_prd_name, string Competitor_Prd_bulk, string Competitor_prd_code, string Competitor_prd_name, string div_code, string Competitor_code, string Competitor_name)
        {
            int iReturn = -1;


            try
            {
                DB_EReporting db = new DB_EReporting();



                strQry = "Update Map_OurPrd_CompetitorPrd set Competitor_Prd_bulk='" + Competitor_Prd_bulk + "', " +
                         " Competitor_prd_code='" + Competitor_prd_code + "',Competitor_prd_name='" + Competitor_prd_name + "', " +
                         "Competitor_code='" + Competitor_code + "',Competitor_name='" + Competitor_name + "'," +
                         " Last_Updtdate=getdate() where division_code='" + div_code + "' and Our_prd_code='" + Our_prd_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;
        }
        public int AddOur_Prd_Competitor_Prd(string Our_prd_code, string Our_prd_name, string Competitor_Prd_bulk, string Competitor_prd_code, string Competitor_prd_name, string div_code, string Competitor_code, string Competitor_name)
        {
            int iReturn = -1;


            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Map_OurPrd_CompetitorPrd ";
                int Sl_No = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Map_OurPrd_CompetitorPrd(Sl_No,Our_prd_code,Our_prd_name,Competitor_Prd_bulk,Competitor_prd_code,Competitor_prd_name,Division_Code,Created_Date,Last_Updtdate,Active_Flag,Competitor_code,Competitor_name)" +
                         "values('" + Sl_No + "','" + Our_prd_code + "','" + Our_prd_name + "','" + Competitor_Prd_bulk + "','" + Competitor_prd_code + "','" + Competitor_prd_name + "','" + div_code + "', getdate(),getdate(),0,'" + Competitor_code + "','" + Competitor_name + "')";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet get_OurProduct_Comp_avail(string Our_prd_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "  SELECT competitor_prd_code,Our_prd_code from Map_OurPrd_CompetitorPrd where Our_prd_code='" + Our_prd_code + "' and active_flag=0 ";
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

        public DataSet getCompetitor_VsPrd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Sl_No,Comp_Name,Comp_Prd_name,Comp_Sl_No FROM  Map_Competitor_Product " +
                     " WHERE  Division_Code=  '" + divcode + "' and Active_Flag=0";



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

        public int RecordAddCompetitor_VsPrd(string divcode, string Comp_Sl_No, string Comp_Name, string Comp_Prd_name, string Comp_Prd_Sl_No, string type)
        {
            int iReturn = -1;



            if (!sRecordExistCompetitor(Comp_Sl_No, divcode, type))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "INSERT INTO Map_Competitor_Product (Division_Code,Comp_Sl_No,Comp_Name,Comp_Prd_Sl_No,Comp_Prd_name,Created_Date,Last_Updtdate,Active_Flag)" +
                             "values('" + divcode + "','" + Comp_Sl_No + "','" + Comp_Name + "','" + Comp_Prd_Sl_No + "','" + Comp_Prd_name + "',getdate(),getdate(),0)";


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

        public int DeActivate_Competitor(int Comp_Sl_No)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Competitor " +
                            " SET Active_Flag =1 ," +
                            " Last_Updtdate = getdate() " +
                            " WHERE Comp_Sl_No = '" + Comp_Sl_No + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int DeActivate_CompetPrd(int Comp_Prd_Sl_No)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Competitor_Product " +
                            " SET Active_Flag =1 ," +
                            " Last_Updtdate = getdate() " +
                            " WHERE Comp_Prd_Sl_No = '" + Comp_Prd_Sl_No + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int DeActivate_Compet_VsPrd(int Sl_No)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Map_Competitor_Product " +
                            " SET Active_Flag =1 ," +
                            " Last_Updtdate = getdate() " +
                            " WHERE Sl_No = '" + Sl_No + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdateCompetitor(string divcode, string Competitor_Name, string Comp_Sl_No, string type)
        {
            int iReturn = -1;


            if (type == "1")
            {

                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "Update Mas_Competitor set " +
                             "Comp_Name='" + Competitor_Name + "',Last_Updtdate=getdate() where Comp_Sl_No='" + Comp_Sl_No + "' and  Division_Code='" + divcode + "'  ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "Update Map_Competitor_Product set " +
                          "Comp_Name='" + Competitor_Name + "',Last_Updtdate=getdate() where Comp_Sl_No='" + Comp_Sl_No + "' and  Division_Code='" + divcode + "'  ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {

                try
                {
                    DB_EReporting db = new DB_EReporting();



                    strQry = "Update Mas_Competitor_Product set " +
                             "Comp_Prd_Name='" + Competitor_Name + "',Last_Updtdate=getdate() where Comp_Prd_Sl_No='" + Comp_Sl_No + "' and  Division_Code='" + divcode + "'  ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            return iReturn;

        }

        public int RecordUpdateCompetitor_VsPrd(string divcode, string Comp_Sl_No, string Comp_Prd_name, string Comp_Prd_Sl_No, string type)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Update Map_Competitor_Product set " +
                         "Comp_Prd_Sl_No='" + Comp_Prd_Sl_No + "', Comp_Prd_name ='" + Comp_Prd_name + "' ,Last_Updtdate=getdate() where Comp_Sl_No='" + Comp_Sl_No + "' and  Division_Code='" + divcode + "'  ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;

        }
        public DataSet getDocCat_Visit_Name(string divcode, string No_of_Visit)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_SName + ' ( ' +cast(No_of_visit as varchar) + ' times ) ' as Doc_Cat_SName,Doc_Cat_Name  FROM  Mas_Doctor_Category " +
                     " WHERE No_of_visit='" + No_of_Visit + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Doc_Cat_SName";
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

        public DataSet getDoctorMgr_list_view(string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select row_number() over (order by ListedDr_Sl_No) ListedDr_Sl_No,ListedDrCode, ListedDr_Sl_No as ListedDr_Sl , ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country,  " +
                     " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case convert(char(10),a.ListedDr_DOB,103) " +
                     " when '01/01/1900' then '' " +
                     " else convert(char(10),a.ListedDr_DOB,103) end ListedDr_DOB, case convert(char(10),a.ListedDr_DOW,103) when '01/01/1900' then '' else convert(char(10),a.ListedDr_DOW,103) end ListedDr_DOW, " +
                     " ListedDr_Mobile,ListedDr_Email,Doc_Class_ShortName as  Doc_ClsSName, Doc_Qua_Name as Doc_QuaName, Doc_Cat_ShortName as Doc_Cat_SName,Doc_Spec_ShortName as Doc_Special_SName, case (visit_Session +'/' + Visit_Freq) when '/' then '' else (visit_Session +'/' + Visit_Freq) end  Product_Tagged,    " +
                     " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName, " +
                     " (select Alias_Name from  Mas_Territory_Creation h where h.sf_code = a.Sf_code and cast(h.Territory_Code as varchar) = a.Territory_Code ) Territory, " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where Territory_Active_Flag=0  and " +
                       " t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " a.listeddr_pincode ,City, " +
                     " (select Doc_Special_Name from  Mas_Doctor_Speciality e where a.Doc_Special_Code = e.Doc_Special_Code ) as Doc_Special_Name, " +
                     " (select case when New_Unique ='N' then 'New' " +
                     " else case when New_Unique = '' then 'Existing' end end as mode from mas_common_drs d where a.c_doctor_code= d.c_doctor_code) as mode,Dr_Potential,Dr_Contribution  " +
                     " from Mas_ListedDr a, Mas_Doctor_Category dc  where  a.ListedDr_Active_Flag=0 and a.sf_code = '" + mgr_code + "' and a.Doc_Cat_Code=dc.Doc_Cat_Code" +
                     " order by ListedDr_Sl_No";// Doc_Cat_Sl_No,Doc_Special_SName ";
            //" order by ListedDrCode ";

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
        public DataSet getDoctorCategory_list_View(string sf_code, string cat_code, string terr_code, string type, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;
            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' ";
            }


            if (terr_code != "-1")
            {
                swhere = swhere + " and CHARINDEX('" + terr_code + "',a.Territory_Code) > 0  ";
            }
            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country,  " +
            //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case convert(char(10),a.ListedDr_DOB,103)" +
            //           " when '01/01/1900' then '' " +
            //           " else convert(char(10),a.ListedDr_DOB,103) end ListedDr_DOB, case convert(char(10),a.ListedDr_DOW,103) when '01/01/1900' then '' else convert(char(10),a.ListedDr_DOW,103) end ListedDr_DOW, " +
            //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name,case (visit_Session +'/' + Visit_Freq) when '/' then '' else (visit_Session +'/' + Visit_Freq) end  Product_Tagged,a.listeddr_pincode,f.Alias_Name as Territory,   " +
            //            " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.sf_code =a.sf_code  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName ,Doc_Special_Name ,City" +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code and  cast(f.Territory_Code as varchar) = a.Territory_Code " +
            //           " and  a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0 and a.sf_code='" + sf_code + "' and f.Territory_Active_Flag=0 and f.division_code='" + div_Code + "'  " +
            //           swhere +
            //           " order by ListedDr_Sl_No desc  ";
            strQry = " select row_number() over (order by ListedDr_Sl_No) ListedDr_Sl_No,ListedDrCode, ListedDr_Sl_No as  ListedDr_Sl, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country,  " +
                     " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case convert(char(10),a.ListedDr_DOB,103) " +
                     " when '01/01/1900' then '' " +
                     " else convert(char(10),a.ListedDr_DOB,103) end ListedDr_DOB, case convert(char(10),a.ListedDr_DOW,103) when '01/01/1900' then '' else convert(char(10),a.ListedDr_DOW,103) end ListedDr_DOW, " +
                     " ListedDr_Mobile,ListedDr_Email,Doc_Class_ShortName as  Doc_ClsSName, Doc_Qua_Name as Doc_QuaName, Doc_Cat_ShortName as Doc_Cat_SName,Doc_Spec_ShortName as Doc_Special_SName, case (visit_Session +'/' + Visit_Freq) when '/' then '' else (visit_Session +'/' + Visit_Freq) end  Product_Tagged,    " +
                     " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName, " +
                     " a.listeddr_pincode ,City, " +
                     " (select Doc_Special_Name from  Mas_Doctor_Speciality e where a.Doc_Special_Code = e.Doc_Special_Code ) as Doc_Special_Name, " +
                     " (select case when New_Unique ='N' then 'New' " +
                     " else case when New_Unique = '' then 'Existing' end end as mode from mas_common_drs d where a.c_doctor_code= d.c_doctor_code) as mode,'' as Territory, " +
                     " stuff((select '~ ' + territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and Territory_Active_Flag=0  and CHARINDEX(cast(t.Territory_Code as varchar), a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " Dr_Potential,Dr_Contribution " +
                     " from Mas_ListedDr a, Mas_Doctor_Category dc  where  a.ListedDr_Active_Flag=0 and a.sf_code = '" + sf_code + "' and a.division_code='" + div_Code + "' and a.Doc_Cat_Code=dc.Doc_Cat_Code " +
                     swhere +
                     " order by ListedDr_Sl_No"; //Doc_Cat_Sl_No,Doc_Special_SName ";
            //" order by ListedDrCode ";

            //strQry = " SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Class_ShortName as Doc_ClsSName,d.Doc_Qua_Name as Doc_QuaName,d.SDP as Activity_Date, " +
            //    "  ListedDr_Address3, ListedDr_PinCode, d.Doc_Special_Code,d.Doc_Cat_Code,d.Doc_ClsCode,d.Territory_Code,d.ListedDr_DOB,d.ListedDr_DOW," +
            //        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and Territory_Active_Flag=0 and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name   FROM  " +
            //        " Mas_ListedDr d  WHERE d.Sf_Code =  '" + sf_code + "' and d.ListedDr_Active_Flag = 0 " +
            //        swhere +
            //        " order by ListedDr_Sl_No desc ";

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

        public DataSet getDoctorMgr_list_view_New(string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select row_number() over (order by ListedDr_Sl_No) ListedDr_Sl_No,ListedDrCode, ListedDr_Sl_No as ListedDr_Sl , ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country,  " +
                     " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case convert(char(10),a.ListedDr_DOB,103) " +
                     " when '01/01/1900' then '' " +
                     " else convert(char(10),a.ListedDr_DOB,103) end ListedDr_DOB, case convert(char(10),a.ListedDr_DOW,103) when '01/01/1900' then '' else convert(char(10),a.ListedDr_DOW,103) end ListedDr_DOW, " +
                     " ListedDr_Mobile,ListedDr_Email,Doc_Class_ShortName as  Doc_ClsSName, Doc_Qua_Name as Doc_QuaName, Doc_Cat_ShortName as Doc_Cat_SName,Doc_Spec_ShortName as Doc_Special_SName, case (visit_Session +'/' + Visit_Freq) when '/' then '' else (visit_Session +'/' + Visit_Freq) end  Product_Tagged,    " +
                     " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=1 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand1,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=2 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand2,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=3 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand3,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=4 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand4,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=5 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand5, " +
                     " (select Alias_Name from  Mas_Territory_Creation h where h.sf_code = a.Sf_code and cast(h.Territory_Code as varchar) = a.Territory_Code ) Territory, " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where Territory_Active_Flag=0  and " +
                       " t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " a.listeddr_pincode ,City, " +
                     " (select Doc_Special_Name from  Mas_Doctor_Speciality e where a.Doc_Special_Code = e.Doc_Special_Code ) as Doc_Special_Name, " +
                     " (select case when New_Unique ='N' then 'New' " +
                     " else case when New_Unique = '' then 'Existing' end end as mode from mas_common_drs d where a.c_doctor_code= d.c_doctor_code) as mode,Dr_Potential,Dr_Contribution  " +
                     " from Mas_ListedDr a, Mas_Doctor_Category dc  where  a.ListedDr_Active_Flag=0 and a.sf_code = '" + mgr_code + "' and a.Doc_Cat_Code=dc.Doc_Cat_Code" +
                     " order by ListedDr_Sl_No";// Doc_Cat_Sl_No,Doc_Special_SName ";
            //" order by ListedDrCode ";

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
        public DataSet getDoctorCategory_list_View_New(string sf_code, string cat_code, string terr_code, string type, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;
            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' ";
            }


            if (terr_code != "-1")
            {
                swhere = swhere + " and CHARINDEX('" + terr_code + "',a.Territory_Code) > 0  ";
            }
            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country,  " +
            //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case convert(char(10),a.ListedDr_DOB,103)" +
            //           " when '01/01/1900' then '' " +
            //           " else convert(char(10),a.ListedDr_DOB,103) end ListedDr_DOB, case convert(char(10),a.ListedDr_DOW,103) when '01/01/1900' then '' else convert(char(10),a.ListedDr_DOW,103) end ListedDr_DOW, " +
            //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsSName,c.Doc_QuaName, d.Doc_Cat_SName,e.Doc_Special_SName, f.Territory_Name,case (visit_Session +'/' + Visit_Freq) when '/' then '' else (visit_Session +'/' + Visit_Freq) end  Product_Tagged,a.listeddr_pincode,f.Alias_Name as Territory,   " +
            //            " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.sf_code =a.sf_code  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName ,Doc_Special_Name ,City" +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code and  cast(f.Territory_Code as varchar) = a.Territory_Code " +
            //           " and  a.Doc_ClsCode=b.Doc_ClsCode and a.ListedDr_Active_Flag=0 and a.sf_code='" + sf_code + "' and f.Territory_Active_Flag=0 and f.division_code='" + div_Code + "'  " +
            //           swhere +
            //           " order by ListedDr_Sl_No desc  ";
            strQry = " select row_number() over (order by ListedDr_Sl_No) ListedDr_Sl_No,ListedDrCode, ListedDr_Sl_No as  ListedDr_Sl, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2,ListedDr_Hospital,Hospital_Address,Hospital_City,Hospital_State,Hospital_Country,  " +
                     " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case convert(char(10),a.ListedDr_DOB,103) " +
                     " when '01/01/1900' then '' " +
                     " else convert(char(10),a.ListedDr_DOB,103) end ListedDr_DOB, case convert(char(10),a.ListedDr_DOW,103) when '01/01/1900' then '' else convert(char(10),a.ListedDr_DOW,103) end ListedDr_DOW, " +
                     " ListedDr_Mobile,ListedDr_Email,Doc_Class_ShortName as  Doc_ClsSName, Doc_Qua_Name as Doc_QuaName, Doc_Cat_ShortName as Doc_Cat_SName,Doc_Spec_ShortName as Doc_Special_SName, case (visit_Session +'/' + Visit_Freq) when '/' then '' else (visit_Session +'/' + Visit_Freq) end  Product_Tagged,    " +
                     " stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode  and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') ProductName,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=1 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand1,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=2 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand2,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=3 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand3,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=4 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand4,stuff((select '/ '+ Product_Name from Map_LstDrs_Product p where p.Listeddr_Code =a.ListedDrCode and Product_Priority=5 and CHARINDEX(cast(p.Listeddr_Code as varchar),a.ListedDrCode) > 0 for XML path('')),1,2,'') Focus_brand5 , " +
                     " a.listeddr_pincode ,City, " +
                     " (select Doc_Special_Name from  Mas_Doctor_Speciality e where a.Doc_Special_Code = e.Doc_Special_Code ) as Doc_Special_Name, " +
                     " (select case when New_Unique ='N' then 'New' " +
                     " else case when New_Unique = '' then 'Existing' end end as mode from mas_common_drs d where a.c_doctor_code= d.c_doctor_code) as mode,'' as Territory, " +
                     " stuff((select '~ ' + territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and Territory_Active_Flag=0  and CHARINDEX(cast(t.Territory_Code as varchar), a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " Dr_Potential,Dr_Contribution " +
                     " from Mas_ListedDr a, Mas_Doctor_Category dc  where  a.ListedDr_Active_Flag=0 and a.sf_code = '" + sf_code + "' and a.division_code='" + div_Code + "' and a.Doc_Cat_Code=dc.Doc_Cat_Code " +
                     swhere +
                     " order by ListedDr_Sl_No"; //Doc_Cat_Sl_No,Doc_Special_SName ";
            //" order by ListedDrCode ";

            //strQry = " SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Class_ShortName as Doc_ClsSName,d.Doc_Qua_Name as Doc_QuaName,d.SDP as Activity_Date, " +
            //    "  ListedDr_Address3, ListedDr_PinCode, d.Doc_Special_Code,d.Doc_Cat_Code,d.Doc_ClsCode,d.Territory_Code,d.ListedDr_DOB,d.ListedDr_DOW," +
            //        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and Territory_Active_Flag=0 and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name   FROM  " +
            //        " Mas_ListedDr d  WHERE d.Sf_Code =  '" + sf_code + "' and d.ListedDr_Active_Flag = 0 " +
            //        swhere +
            //        " order by ListedDr_Sl_No desc ";

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

        public DataSet getDocCamp_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                     " WHERE Doc_SubCat_ActiveFlag=1 AND Division_Code= '" + divcode + "' ";

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

        public int RectivateDoc_CamReact(int Doc_Special_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Doc_SubCategory " +
                            " SET Doc_SubCat_ActiveFlag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_SubCatCode = '" + Doc_Special_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getCat_ForDrCam(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "Select Doc_Cat_Code,Doc_Cat_Name " +
                    "FROM Mas_Doctor_Category " +
                    "where  Division_Code='" + div_code + "' and Doc_Cat_Active_Flag=0 " +
                    "ORDER BY Doc_Cat_Sl_No ";
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

        public DataSet getSpec_DrCamp(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "Select Doc_Special_Code,Doc_Special_Name " +
                    "FROM Mas_Doctor_Speciality " +
                    "where  Division_Code='" + div_code + "' and Doc_Special_Active_Flag=0 " +
                    "ORDER BY Doc_Spec_Sl_No ";
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

        public DataSet getSpec_DrClass(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "select Doc_ClsCode,Doc_ClsName from Mas_Doc_Class " +
                     " where Division_Code='" + div_code + "' and Doc_Cls_ActiveFlag=0 ";
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


        public DataSet getSpec_DrBrand(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Product_Brd_Code,Product_Brd_Name " +
                     " from Mas_Product_Brand where Division_Code='" + div_code + "' and Product_Brd_Active_Flag=0 ";
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

        public DataSet getSpec_DrProduct(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "  select Product_Code_SlNo,Product_Detail_Name " +
                     " from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag=0 and product_mode='sale'";
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

        public DataSet getInput_ForDrCam(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Gift_Code,Gift_Name from Mas_Gift where Division_Code='" + div_code + "' and Gift_Active_Flag=0 ";
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

        public DataSet getDocSubCat_Campfor(string divcode, string docsubcatcode, string Camp_for)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe2 = null;

            if (Camp_for == "Category")
            {
                strQry = " SELECT Cat_Code,Cat_Name FROM  Mas_Doc_SubCategory " +
                         " WHERE Doc_SubCatCode='" + docsubcatcode + "' AND Division_Code=  '" + divcode + "'  " +
                         " ORDER BY 2";
            }
            else if (Camp_for == "Speciality")
            {
                strQry = " SELECT Spec_Code,Spec_Name FROM  Mas_Doc_SubCategory " +
                         " WHERE Doc_SubCatCode='" + docsubcatcode + "' AND Division_Code=  '" + divcode + "'  " +
                         " ORDER BY 2";
            }
            else if (Camp_for == "Class")
            {
                strQry = " SELECT Class_Code,Class_Name FROM  Mas_Doc_SubCategory " +
                         " WHERE Doc_SubCatCode='" + docsubcatcode + "' AND Division_Code=  '" + divcode + "'  " +
                         " ORDER BY 2";
            }
            else if (Camp_for == "Brand")
            {
                strQry = " SELECT Brand_Code,Brand_Name FROM  Mas_Doc_SubCategory " +
                         " WHERE Doc_SubCatCode='" + docsubcatcode + "' AND Division_Code=  '" + divcode + "'  " +
                         " ORDER BY 2";
            }
            else if (Camp_for == "Product")
            {
                strQry = " SELECT Product_Code,Product_Name FROM  Mas_Doc_SubCategory " +
                         " WHERE Doc_SubCatCode='" + docsubcatcode + "' AND Division_Code=  '" + divcode + "'  " +
                         " ORDER BY 2";
            }
            else
            {
                strQry = " SELECT Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                        " WHERE Doc_SubCatCode='" + docsubcatcode + "' AND Division_Code=  '" + divcode + "' " +
                        " ORDER BY 2";
            }


            try
            {
                dsDocSpe2 = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe2;
        }

        public int RecordAddSubCat(string divcode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To, string No_DrTagg, string Camp_for, string for_type_Code, string for_type_name, int All_DrsTagg, string No_visit, string strinput_value, string strinput_name, string Businnes_Rs, string strstate_value, string strstate_name, string Sms_Code)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatSName, divcode))
            {
                if (!sRecordExistSubCat(Doc_SubCatName, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        string EffFromdate = Effective_From.Month.ToString() + "-" + Effective_From.Day + "-" + Effective_From.Year;
                        string EffTodate = Effective_To.Month.ToString() + "-" + Effective_To.Day + "-" + Effective_To.Year;


                        strQry = "SELECT isnull(max(Doc_SubCatCode)+1,'1') Doc_SubCatCode from Mas_Doc_SubCategory ";
                        int Doc_SubCatCode = db.Exec_Scalar(strQry);

                        if (Camp_for == "Category")
                        {

                            strQry = "INSERT INTO Mas_Doc_SubCategory(Doc_SubCatCode,Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To,No_Drs_Tagged,Camp_for,Cat_Code,Cat_Name,All_DrsTagg,No_Visit,Input_Code,Input_Name,Business_Rs,State_Code,State_Name,Sms_Code)" +
                                 "values('" + Doc_SubCatCode + "','" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "','" + No_DrTagg + "','" + Camp_for + "','" + for_type_Code + "','" + for_type_name + "','" + All_DrsTagg + "','" + No_visit + "','" + strinput_value + "','" + strinput_name + "','" + Businnes_Rs + "','" + strstate_value + "','" + strstate_name + "','" + Sms_Code + "')";


                            iReturn = db.ExecQry(strQry);

                        }
                        else if (Camp_for == "Speciality")
                        {
                            strQry = "INSERT INTO Mas_Doc_SubCategory(Doc_SubCatCode,Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To,No_Drs_Tagged,Camp_for,Spec_Code,Spec_Name,All_DrsTagg,No_Visit,Input_Code,Input_Name,Business_Rs,State_Code,State_Name,Sms_Code)" +
                                "values('" + Doc_SubCatCode + "','" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "','" + No_DrTagg + "','" + Camp_for + "','" + for_type_Code + "','" + for_type_name + "','" + All_DrsTagg + "','" + No_visit + "','" + strinput_value + "','" + strinput_name + "','" + Businnes_Rs + "','" + strstate_value + "','" + strstate_name + "','" + Sms_Code + "')";


                            iReturn = db.ExecQry(strQry);
                        }
                        else if (Camp_for == "Class")
                        {
                            strQry = "INSERT INTO Mas_Doc_SubCategory(Doc_SubCatCode,Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To,No_Drs_Tagged,Camp_for,Class_Code,Class_Name,All_DrsTagg,No_Visit,Input_Code,Input_Name,Business_Rs,State_Code,State_Name,Sms_Code)" +
                                 "values('" + Doc_SubCatCode + "','" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "','" + No_DrTagg + "','" + Camp_for + "','" + for_type_Code + "','" + for_type_name + "','" + All_DrsTagg + "','" + No_visit + "','" + strinput_value + "','" + strinput_name + "','" + Businnes_Rs + "','" + strstate_value + "','" + strstate_name + "','" + Sms_Code + "')";


                            iReturn = db.ExecQry(strQry);
                        }
                        else if (Camp_for == "Brand")
                        {
                            strQry = "INSERT INTO Mas_Doc_SubCategory(Doc_SubCatCode,Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To,No_Drs_Tagged,Camp_for,Brand_Code,Brand_Name,All_DrsTagg,No_Visit,Input_Code,Input_Name,Business_Rs,State_Code,State_Name,Sms_Code)" +
                                  "values('" + Doc_SubCatCode + "','" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "','" + No_DrTagg + "','" + Camp_for + "','" + for_type_Code + "','" + for_type_name + "','" + All_DrsTagg + "','" + No_visit + "','" + strinput_value + "','" + strinput_name + "','" + Businnes_Rs + "','" + strstate_value + "','" + strstate_name + "','" + Sms_Code + "')";


                            iReturn = db.ExecQry(strQry);
                        }
                        else if (Camp_for == "Product")
                        {
                            strQry = "INSERT INTO Mas_Doc_SubCategory(Doc_SubCatCode,Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To,No_Drs_Tagged,Camp_for,Product_Code,Product_Name,All_DrsTagg,No_Visit,Input_Code,Input_Name,Business_Rs,State_Code,State_Name,Sms_Code)" +
                                   "values('" + Doc_SubCatCode + "','" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "','" + No_DrTagg + "','" + Camp_for + "','" + for_type_Code + "','" + for_type_name + "','" + All_DrsTagg + "','" + No_visit + "','" + strinput_value + "','" + strinput_name + "','" + Businnes_Rs + "','" + strstate_value + "','" + strstate_name + "','" + Sms_Code + "')";


                            iReturn = db.ExecQry(strQry);
                        }
                        else
                        {
                            strQry = "INSERT INTO Mas_Doc_SubCategory(Doc_SubCatCode,Division_Code,Doc_SubCatSName,Doc_SubCatName,Doc_SubCat_ActiveFlag,Group_Sl_No,Created_Date,LastUpdt_Date,Effective_From,Effective_To,No_Drs_Tagged,Camp_for,All_DrsTagg,No_Visit,Input_Code,Input_Name,Business_Rs,State_Code,State_Name,Sms_Code)" +
                                "values('" + Doc_SubCatCode + "','" + divcode + "','" + Doc_SubCatSName + "', '" + Doc_SubCatName + "',0,1,getdate(),getdate(),'" + EffFromdate + "','" + EffTodate + "','" + No_DrTagg + "','" + Camp_for + "','" + All_DrsTagg + "','" + No_visit + "','" + strinput_value + "','" + strinput_name + "','" + Businnes_Rs + "','" + strstate_value + "','" + strstate_name + "','" + Sms_Code + "')";


                            iReturn = db.ExecQry(strQry);
                        }
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


        public int RecordUpdateSubCatnew(int Doc_SubCatCode, string Doc_SubCatSName, string Doc_SubCatName, DateTime Effective_From, DateTime Effective_To, string divcode, string No_DrTagg, string Camp_for, string for_type_Code, string for_type_name, int All_DrsTagg, string No_visit, string strinput_value, string strinput_name, string Businnes_Rs, string strstate_value, string strstate_name, string Sms_Code)
        {
            int iReturn = -1;
            if (!RecordExistSubCat(Doc_SubCatCode, Doc_SubCatSName, divcode))
            {
                if (!sRecordExistSubCat(Doc_SubCatCode, Doc_SubCatName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        if (Camp_for == "Category")
                        {

                            strQry = "UPDATE Mas_Doc_SubCategory " +
                                     " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                     " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                     " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                     " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                     " No_Drs_Tagged='" + No_DrTagg + "', " +
                                     " Camp_for='" + Camp_for + "', " +
                                     " Cat_Code='" + for_type_Code + "', " +
                                     " Cat_Name='" + for_type_name + "', " +
                                     " All_DrsTagg='" + All_DrsTagg + "', " +
                                     " No_Visit='" + No_visit + "', " +
                                     " Input_Code='" + strinput_value + "', " +
                                     " Input_Name='" + strinput_name + "', " +
                                     " Business_Rs='" + Businnes_Rs + "', " +
                                     " State_Code='" + strstate_value + "', " +
                                     " State_Name='" + strstate_name + "', " +
                                     " Sms_Code='" + Sms_Code + "', " +
                                     " LastUpdt_Date = getdate() " +
                                     " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                            iReturn = db.ExecQry(strQry);
                        }
                        else if (Camp_for == "Speciality")
                        {
                            strQry = "UPDATE Mas_Doc_SubCategory " +
                                    " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                    " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                    " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                    " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                    " No_Drs_Tagged='" + No_DrTagg + "', " +
                                    " Camp_for='" + Camp_for + "', " +
                                    " Spec_Code='" + for_type_Code + "', " +
                                    " Spec_Name='" + for_type_name + "', " +
                                    " All_DrsTagg='" + All_DrsTagg + "', " +
                                    " No_Visit='" + No_visit + "', " +
                                    " Input_Code='" + strinput_value + "', " +
                                    " Input_Name='" + strinput_name + "', " +
                                    " Business_Rs='" + Businnes_Rs + "', " +
                                    " State_Code='" + strstate_value + "', " +
                                    " State_Name='" + strstate_name + "', " +
                                    " Sms_Code='" + Sms_Code + "', " +
                                    " LastUpdt_Date = getdate() " +
                                    " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                            iReturn = db.ExecQry(strQry);
                        }
                        else if (Camp_for == "Class")
                        {
                            strQry = "UPDATE Mas_Doc_SubCategory " +
                                    " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                    " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                    " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                    " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                    " No_Drs_Tagged='" + No_DrTagg + "', " +
                                    " Camp_for='" + Camp_for + "', " +
                                    " Class_Code='" + for_type_Code + "', " +
                                    " Class_Name='" + for_type_name + "', " +
                                    " All_DrsTagg='" + All_DrsTagg + "', " +
                                    " No_Visit='" + No_visit + "', " +
                                    " Input_Code='" + strinput_value + "', " +
                                    " Input_Name='" + strinput_name + "', " +
                                    " Business_Rs='" + Businnes_Rs + "', " +
                                    " State_Code='" + strstate_value + "', " +
                                    " State_Name='" + strstate_name + "', " +
                                    " Sms_Code='" + Sms_Code + "', " +
                                    " LastUpdt_Date = getdate() " +
                                    " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                            iReturn = db.ExecQry(strQry);
                        }
                        else if (Camp_for == "Brand")
                        {
                            strQry = "UPDATE Mas_Doc_SubCategory " +
                                   " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                   " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                   " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                   " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                   " No_Drs_Tagged='" + No_DrTagg + "', " +
                                   " Camp_for='" + Camp_for + "', " +
                                   " Brand_Code='" + for_type_Code + "', " +
                                   " Brand_Name='" + for_type_name + "', " +
                                   " All_DrsTagg='" + All_DrsTagg + "', " +
                                   " No_Visit='" + No_visit + "', " +
                                   " Input_Code='" + strinput_value + "', " +
                                   " Input_Name='" + strinput_name + "', " +
                                   " Business_Rs='" + Businnes_Rs + "', " +
                                   " State_Code='" + strstate_value + "', " +
                                   " State_Name='" + strstate_name + "', " +
                                   " Sms_Code='" + Sms_Code + "', " +
                                   " LastUpdt_Date = getdate() " +
                                   " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                            iReturn = db.ExecQry(strQry);
                        }
                        else if (Camp_for == "Product")
                        {
                            strQry = "UPDATE Mas_Doc_SubCategory " +
                                     " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                     " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                     " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                     " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                     " No_Drs_Tagged='" + No_DrTagg + "', " +
                                     " Camp_for='" + Camp_for + "', " +
                                     " Product_Code='" + for_type_Code + "', " +
                                     " Product_Name='" + for_type_name + "', " +
                                     " All_DrsTagg='" + All_DrsTagg + "', " +
                                     " No_Visit='" + No_visit + "', " +
                                     " Input_Code='" + strinput_value + "', " +
                                     " Input_Name='" + strinput_name + "', " +
                                     " Business_Rs='" + Businnes_Rs + "', " +
                                     " State_Code='" + strstate_value + "', " +
                                     " State_Name='" + strstate_name + "', " +
                                     " Sms_Code='" + Sms_Code + "', " +
                                     " LastUpdt_Date = getdate() " +
                                     " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                            iReturn = db.ExecQry(strQry);
                        }
                        else
                        {
                            strQry = "UPDATE Mas_Doc_SubCategory " +
                                   " SET Doc_SubCatSName = '" + Doc_SubCatSName + "', " +
                                   " Doc_SubCatName = '" + Doc_SubCatName + "'," +
                                   " Effective_From ='" + Effective_From.Month.ToString() + '-' + Effective_From.Day.ToString() + '-' + Effective_From.Year.ToString() + "'" +
                                   " , Effective_To ='" + Effective_To.Month.ToString() + '-' + Effective_To.Day.ToString() + '-' + Effective_To.Year.ToString() + "'," +
                                   " No_Drs_Tagged='" + No_DrTagg + "', " +
                                   " Camp_for='" + Camp_for + "', " +
                                   " All_DrsTagg='" + All_DrsTagg + "', " +
                                   " No_Visit='" + No_visit + "', " +
                                   " Input_Code='" + strinput_value + "', " +
                                   " Input_Name='" + strinput_name + "', " +
                                   " Business_Rs='" + Businnes_Rs + "', " +
                                   " State_Code='" + strstate_value + "', " +
                                   " State_Name='" + strstate_name + "', " +
                                   " Sms_Code='" + Sms_Code + "', " +
                                   " LastUpdt_Date = getdate() " +
                                   " WHERE Doc_SubCatCode = '" + Doc_SubCatCode + "' ";

                            iReturn = db.ExecQry(strQry);
                        }
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

        public DataSet getDocCat_Visit_Name_Cat(string divcode, string Doc_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_SName + ' ( ' +cast(No_of_visit as varchar) + ' times ) ' as Doc_Cat_SName,Doc_Cat_Name  FROM  Mas_Doctor_Category " +
                     " WHERE Doc_Cat_Code='" + Doc_Cat_Code + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Doc_Cat_Sl_No";
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
        public DataTable Get_Camp_MR(string sf_code, string div_code, string Camp_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            strQry = "EXEC Listeddr_Campaign_MR '" + sf_code + "', '" + div_code + "','" + Camp_code + "' ";

            try
            {
                dsSF = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataTable Get_Camp_MR_Single(string sf_code, string div_code, string Camp_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            strQry = " select Row_Number() Over(ORDER by sf_name) as sno, a.ListedDrCode,b.Sf_Code,b.sf_name +' - '  +b.sf_Designation_Short_Name+' - ' + b.Sf_HQ as sf_name,a.ListedDr_Name,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Qua_Name,Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where dc.division_code=a.division_code and   charindex(cast(dc.Doc_SubCatCode as varchar)+',',a.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName " +
                     " from mas_listeddr a,mas_salesforce b where a.sf_code ='" + sf_code + "' " +
                     " and a.sf_code=b.sf_code and listeddr_active_flag=0 and Doc_SubCatCode !='' and a.Division_code='" + div_code + "' and  " +
                     " ( Doc_SubCatCode like '" + Camp_code + ',' + "%'  or " +
                     " Doc_SubCatCode like '%" + ',' + Camp_code + ',' + "%')  " +
                     " order by b.sf_name  ";

            try
            {
                dsSF = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataSet getDoc_MRCamp_MGR(string divcode, string Dr_Code, string Des_Code, int Month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " select day(a.Activity_Date) as Activity_Date,b.Product_Detail, b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,b.Worked_with_Name  " +
                     " from DCRMain_Trans  a,DCRDetail_Lst_Trans b ,mas_salesforce c  where month(a.Activity_Date)='" + Month + "' and YEAR(a.Activity_Date)='" + year + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1   and c.sf_code=a.sf_code and Trans_Detail_Info_Code ='" + Dr_Code + "' " +
                     " and Designation_Code='" + Des_Code + "'  order by a.Activity_Date,c.sf_code ";


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
        public DataTable Get_Core_Dr_MR(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            strQry = "EXEC Listeddr_Core_Map_Dr '" + sf_code + "', '" + div_code + "' ";

            try
            {
                dsSF = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet getDocCampaign_all(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT '' as Doc_SubCatCode, '' as Doc_SubCatSName, '---Select---' as Doc_SubCatName " +
                     " UNION " +
                     " SELECT '-1' as Doc_SubCatCode, '' as Doc_SubCatSName,'---ALL---' as Doc_SubCatName " +
                      " UNION " +
                     " SELECT Doc_SubCatCode,Doc_SubCatSName,Doc_SubCatName FROM  Mas_Doc_SubCategory " +
                     " WHERE Doc_SubCat_ActiveFlag=0 AND Division_Code=  '" + divcode + "' " +
                     " ORDER BY Doc_SubCatName";


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
        public DataSet getChemCampaign_all(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = "SELECT '' as chm_campaign_code, '' as chm_campaign_SName, '---Select---' as chm_campaign_name " +
                      "UNION " +
                      "SELECT '-1' as chm_campaign_code, '' as chm_campaign_SName,'---ALL---' as chm_campaign_name " +
                      "UNION " +
                      "SELECT chm_campaign_code,chm_campaign_SName,chm_campaign_name FROM  mas_chemist_campaign " +
                      "WHERE active_flag = 0 AND Division_Code = '"+ divcode +"' " +
                      "ORDER BY chm_campaign_name ";


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

        public DataSet filldr_terr(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = " SELECT ListedDrCode,ListedDr_Name FROM  Mas_ListedDr " +
                     " WHERE   sf_code=  '" + sf_code + "' and ListedDr_Active_Flag=0 " +
                     " ORDER BY ListedDrCode";
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
        public DataSet CoredrsMap(string mgr_code,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select distinct ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case isnull(ListedDr_DOB,null)" +
                       " when '1900-01-01 00:00:00.000' then null  else  ListedDr_DOB  end ListedDr_DOB," +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW,ListedDr_Mobile,ListedDr_Email, " +
                       " b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e,core_doctor_map f ,Mas_salesforce g " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and  a.Doc_ClsCode=b.Doc_ClsCode and g.sf_code=a.sf_code and f.sf_code=a.sf_code and f.dr_code=a.Listeddrcode and f.mgr_code='" + mgr_code + "' and f.Division_Code='"+div_code+"' " +
                                              " order by sf_name,ListedDr_Name ";

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

        public DataSet filldr_terr_count(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = " SELECT count(ListedDrCode) as totdrr,(select COUNT( Chemists_Code) from Mas_Chemists where SF_Code='" + sf_code + "' and Chemists_Active_Flag=0) as totchemm FROM  Mas_ListedDr " +
                     " WHERE   sf_code='" + sf_code + "' and ListedDr_Active_Flag=0 ";

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
        public DataTable Missed_List_MR_Sort(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";



            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataTable Missed_List_MR_Spec_Sort(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Spec)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Spec '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Spec + "'  ";



            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataTable Missed_List_MR_Cat_Sort(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Cat)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Cat '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Cat + "'  ";



            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataTable Missed_List_MR_Qual_Sort(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Qual)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Qual '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Qual + "'  ";



            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataTable Missed_List_MR_Cls_Sort(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Cls)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Cls '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Cls + "'  ";



            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataTable Missed_List_MR_Terr_Sort(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Terr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Terr '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Terr + "'  ";



            try
            {
                dsDocCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public DataSet Missed_List_MR(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";



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
        public DataSet Missed_List_MR_Spec(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Spec)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Spec '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Spec + "'  ";



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

        public DataSet Missed_List_MR_Cat(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Cat)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Cat '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Cat + "'  ";



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
        public DataSet Missed_List_MR_Qual(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Qual)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Qual '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Qual + "'  ";



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
        public DataSet Missed_List_MR_Cls(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Cls)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Cls '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Cls + "'  ";



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
        public DataSet Missed_List_MR_Terr(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string Terr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;


            strQry = " EXEC Missed_Dr_List_Terr '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + Terr + "'  ";



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
        public DataSet getDocSpe_Graph(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT s.Doc_Special_Code,s.Doc_Special_SName,s.Doc_Special_Name, " +
                     " (select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = s.Doc_Special_Code and ListedDr_Active_Flag=0) as Spec_Count" +
                     " FROM  Mas_Doctor_Speciality s " +
                     " WHERE s.Doc_Special_Active_Flag=0 AND s.Division_Code= '" + divcode + "' " +
                     " ORDER BY Spec_Count ";
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

        public DataSet drsCampaignMap(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = "select distinct listeddrcode,ListedDr_Name,Doc_QuaName,ListedDr_Address1,Doc_Special_Name,Doc_Cat_Name,Doc_ClsName,Territory_Name,ListedDr_DOB,ListedDr_DOW,ListedDr_Mobile,ListedDr_Email from Mas_Doc_SubCategory C " +
         "inner join mas_listeddr DR on charindex(','+cast(C.Doc_SubCatCode as varchar)+',',','+ DR.Doc_SubCatCode) >0   " +
         "inner join mas_territory_Creation T on T.territory_code=DR.territory_code " +
         "inner join Mas_Doc_Qualification Q on Q.Doc_QuaCode=DR.Doc_QuaCode " +
         "inner join Mas_Doctor_Category CT on CT.Doc_Cat_Code=DR.Doc_Cat_Code " +
         "inner join Mas_Doctor_Speciality S on S.Doc_Special_Code=DR.Doc_Special_Code " +
         "inner join Mas_Doc_Class CS on CS.Doc_ClsCode=DR.Doc_ClsCode " +
         "where DR.sf_Code='" + sf_code + "' and ListedDr_Active_Flag=0 and C.Doc_SubCat_ActiveFlag=0";

            //         strQry = "select distinct listeddrcode,ListedDr_Name,Doc_QuaName,ListedDr_Address1,Doc_Special_Name,Doc_Cat_Name,Doc_ClsName,Territory_Name,ListedDr_DOB,ListedDr_DOW,ListedDr_Mobile,ListedDr_Email from Mas_Doc_SubCategory C " +
            //"inner join mas_listeddr DR on charindex(','+cast(C.Doc_SubCatCode as varchar)+',',','+ DR.Doc_SubCatCode) >0   " +
            //"inner join mas_territory_Creation T on T.territory_code=DR.territory_code " +
            //"inner join Mas_Doc_Qualification Q on Q.Doc_QuaCode=DR.Doc_QuaCode " +
            //"inner join Mas_Doctor_Category CT on CT.Doc_Cat_Code=DR.Doc_Cat_Code " +
            //"inner join Mas_Doctor_Speciality S on S.Doc_Special_Code=DR.Doc_Special_Code " +
            //"inner join Mas_Doc_Class CS on CS.Doc_ClsCode=DR.Doc_ClsCode " +
            //"where DR.sf_Code='" + sf_code + "' and ListedDr_Active_Flag=0 ";

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

        public DataTable drsCampaignMap_Details(string sf_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "SELECT  ListedDrCode,STUFF((SELECT  distinct ', ' + CAST(C.Doc_SubCatName AS VARCHAR(500)) [text()]" +
                 " FROM  mas_listeddr M inner join Mas_Doc_SubCategory C on charindex(','+cast(C.Doc_SubCatCode as varchar)+',',','+ M.Doc_SubCatCode) >0 " +
                " WHERE M.ListedDrCode = t.ListedDrCode" +
                " FOR XML PATH(''), TYPE)" +
               ".value('.','NVARCHAR(MAX)'),1,2,' ') campaign" +
       " FROM mas_listeddr t where sf_Code='" + sf_code + "' and ListedDr_Active_Flag=0" +
       " GROUP BY ListedDrCode";

            //strQry = "select distinct ListedDrCode  ,STUFF((SELECT ', ' + CAST(C.Doc_SubCatSName AS VARCHAR(500)) [text()] " +
            //   " FROM   Mas_Doc_SubCategory C WHERE  Division_Code='" + Div_Code + "' and charindex(','+cast(C.Doc_SubCatCode as varchar)+',',','+ a.Doc_SubCatCode) >0  " +
            //  " FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') campaign " +
            //  " from Mas_ListedDr a  where  Sf_Code='" + sf_code + "' and a.Division_Code='" + Div_Code + "'  and a.ListedDr_Active_Flag=0  ";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }

        public DataSet SlideExistSpeciality(string DocSpeCode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSpe = null;

            strQry = " SlideExistSpeciality '" + DocSpeCode + "','" + divcode + "' ";
            try
            {
                dsSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSpe;
        }
        #region doctor category added analysis need by Ferooz
        public DataSet getDocCatNew(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Doc_Cat_Code,c.Doc_Cat_SName,c.Doc_Cat_Name, " +
                     " c.No_of_visit,c.Analysis_needed, " +
                     " (select count(d.Doc_Cat_Code) from Mas_ListedDr d where d.Doc_Cat_Code = c.Doc_Cat_Code and ListedDr_Active_Flag=0) as Cat_Count" +
                     "  FROM  Mas_Doctor_Category c" +
                     " WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Doc_Cat_Sl_No";
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

        public int RecordUpdateCatNew(int Doc_Cat_Code, string Doc_Cat_SName, string Doc_Cat_Name, string no_of_visit, string divcode, string AnalysisNd)
        {
            int iReturn = -1;
            if (!RecordExist(Doc_Cat_Code, Doc_Cat_SName, divcode))
            {
                if (!sRecordExist(Doc_Cat_Code, Doc_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_ListedDr " +
                              "SET Doc_Cat_ShortName = '" + Doc_Cat_SName + "'" +
                              "WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' AND Division_Code='" + divcode + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Doctor_Category " +
                                 " SET Doc_Cat_SName = '" + Doc_Cat_SName + "', " +
                                 " Doc_Cat_Name = '" + Doc_Cat_Name + "' ," +
                                 " No_of_visit = '" + no_of_visit + "' ," +
                                 " analysis_needed = '" + AnalysisNd + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Doc_Cat_Code = '" + Doc_Cat_Code + "' ";

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
        #endregion

        public DataSet getInput_Sample_Doctor_gift_cnt(string divcode, string sf_code, int Year, int Month, int Gift_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;


            strQry = " select row_number() over (order by gift_code) slno ,* from (" +
                " select distinct e.Gift_Code ,(select s.Sf_Name from mas_salesforce s where s.Sf_Code='" + sf_code + "' ) Fieldforce_Name ,c.sf_code , " +
                      " (select d.sf_Designation_Short_Name from mas_salesforce d where d.Sf_Code='" + sf_code + "') sf_Designation_Short_Name, " +
                     " (select t.Sf_HQ from mas_salesforce t where t.Sf_Code='" + sf_code + "')  sf_HQ,e.Gift_Name,ListedDrCode,ListedDr_Name,Doc_Qua_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,Doc_Class_ShortName, " +
                     " (STUFF((select ', '+territory_Name from Mas_Territory_Creation l where l.SF_Code='" + sf_code + "' " +
                     " and CHARINDEX(cast(l.Territory_Code as varchar) +',' " +
                     " ,a.Territory_Code+',')>0 for XML path('')),1,2,''))territory_Name,gift_Qty,convert(varchar,activity_date,103) as Date,'' campain_name " +
                     " from DCRMain_Trans b,DCRDetail_Lst_Trans c , " +
                     " Mas_Gift e,Mas_ListedDr a  where c.Division_Code ='" + divcode + "' and b.Trans_SlNo = c.Trans_SlNo and " +
                     " charindex('#'+cast(e.Gift_Code as varchar)+'~','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar) " +
                     " +'#'+c.Additional_Gift_Code) > 0 and (charindex('~$','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar)+'#'+c.Additional_Gift_Code) <= 0 " +
                     " and charindex('~0$','#'+c.Gift_Code+'~'+cast(Gift_Qty as varchar)+'#'+c.Additional_Gift_Code) <= 0 " +
                     " and c.Trans_Detail_Info_Type=1 and month(b.Activity_Date)='" + Month + "' and YEAR(b.Activity_Date)= '" + Year + "' and  c.sf_code " +
                     " in ('" + sf_code + "') and b.sf_code in ('" + sf_code + "')) and e.Gift_Code='" + Gift_Code + "'  and " +
                     " b.Trans_SlNo = c.Trans_SlNo and a.ListedDrCode=c.Trans_Detail_Info_Code)b order by Fieldforce_Name,ListedDr_Name ";

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
        public int VisitCard_Delete(string sf_code, string DrCode, string div_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_listeddr SET visiting_card = ''  WHERE Sf_Code = '" + sf_code + "' and ListedDrCode ='" + DrCode + "' and Division_Code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
    }



}
