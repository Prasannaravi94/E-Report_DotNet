using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class ListedDR
    {
        private string strQry = string.Empty;
        private int Sl_No;
        public DataSet getEmptyListedDR()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT TOP 5 '' ListedDR_Name,'' ListedDR_Address1 " +
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
        public DataSet getmenucrea(int PK_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsdiv = null;


            string strQry = "SELECT PK_Id, Menu_Name, Menu_Type,Menu_Page FROM Tbl_DynamicMenuCreation WHERE PK_Id = '" + PK_Id + "'";

            try
            {

                dsdiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsdiv;
        }
        public DataSet GetMenuItems(string PK_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsDiv = null;


            string strQry = "SELECT FK_PK_Id, OptionMenu_Id, OptionMenu_Name, OptionMenu_Page, OptionMenu_Position " +
                            "FROM Tbl_DynamicOptionMenuCreation " +
                            "WHERE FK_PK_Id = '" + PK_Id + "' AND Is_Active = 0";

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
        public DataSet GetFilteredMenuData(string menuType, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;
            string strQry =
                "SELECT PK_Id, Menu_Name, " +
                "CASE " +
                "    WHEN Menu_Type = 'R' THEN 'Report' " +
                "    WHEN Menu_Type = 'M' THEN 'Menu' " +
                "    ELSE Menu_Type " +
                "END AS Menu_Type, " +
                "Division_Code, Menu_Icon " +
                "FROM Tbl_DynamicMenuCreation " +
                "WHERE Is_Active = 0 " +
                "AND Menu_Type = '" + menuType + "' " +
                "AND Division_Code = '" + div_code + "'";

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
        public DataSet GetSalesViewBill(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Division_Name,convert(varchar, Sale_Date, 105)as Sale_Date,Emp_Code,Emp_Name,HQ_Code,HQ_Name,Chemist_ERP_Code,Chemist_Name,Doctor_Name," +
                     "Doctor_Code,Sale_Qty,Sale_Value,Product_Name,Product_Code from Trans_Secondary_Bill where Division_code ='" + div_code + "'";
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
        //Change done by saravanan
        public DataSet GetDataFromDataBase(string prefixText)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select ListedDr_Name from Mas_ListedDr where ListedDr_Name like '" + prefixText + "%';";

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

        public DataSet GetJoineekitDate(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT Count(*)as CnT FROM Trans_NewJoinee_Kit WHERE Created_Date BETWEEN  dateadd(d,-5,cast(getDate() as date)) AND  dateadd(d,+1,cast(getDate() as date))  and  Division_Code ='" + Division_Code + "'";
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

        public DataSet GetRmdDate(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT Count(*)as CnT FROM Trans_Recommendation_Confirm WHERE Created_Date BETWEEN  dateadd(d,-5,cast(getDate() as date)) AND  dateadd(d,+1,cast(getDate() as date))  and  div_code ='" + Division_Code + "'";
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
        public DataSet Getimagedatecar(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT Count(*)as CnT FROM Trans_carservice WHERE Created_Date BETWEEN  dateadd(d,-5,cast(getDate() as date)) AND  dateadd(d,+1,cast(getDate() as date))  and  Division_Code ='" + Division_Code + "'";
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

        public DataSet GetAsse_MRDate(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT Count(*)as CnT FROM Trans_Assesment_MR WHERE Created_Date BETWEEN  dateadd(d,-5,cast(getDate() as date)) AND  dateadd(d,+1,cast(getDate() as date)) and  Division_Code ='" + Division_Code + "'";
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
        public DataSet GetAsse_MGRDate(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT Count(*)as CnT FROM Trans_Assesment_MGR WHERE Created_Date BETWEEN  dateadd(d,-5,cast(getDate() as date)) AND  dateadd(d,+1,cast(getDate() as date))  and  Division_Code ='" + Division_Code + "'";
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



        public DataSet getTopListedDR()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT TOP 1 '' ListedDR_Name,'' ListedDR_Address1 " +
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
        //changes added 09/08/2019



        public DataSet GetRequest_Service(string sl_no, string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select * from Trans_carservice  Where   Division_Code ='" + div_code + "'and Sl_No='" + sl_no + "'";
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

        public int RecordInsert(string div_code, string sf_code, string sf_Name, string Sf_HQ, string sf_Dsg_Sh,
                                        string Name, string Category, string Place, string MobileNo,
                                       string days, string Pic, string persons, string ADate, string ATime,
                                       string DDate, string Dtime, string trainname, string lastmonth1, string lastmonth2,
                                       string lastmonth3, string nextmonth1, string nextmonth2, string nextmonth3, string earlier,
                                       string lastService, string datenew)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Trans_carservice";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "insert into Trans_carservice (Sl_No,From_Sf_Code,From_Sf_Name,From_HQ,From_Designation,Division_Code,Created_Date,Name_of_Doctor,category,Place," +
                                                        "MobileNo,No_days,C_P,N_P,A_D,A_T,F_No_T_N,D_D,D_T,B_I,B_II,B_III,B_N_I,B_N_II,B_N_III,Earlier_C_S,L_C_S_M)" +
                                                        " VALUES('" + Sl_No + "', '" + sf_code + "', '" + sf_Name + "', '" + Sf_HQ + "', '" + sf_Dsg_Sh + "', '" + div_code + "', " +
                                                        " getdate(), '" + Name + "','" + Category + "','" + Place + "','" + MobileNo + "','" + days + "','" + Pic + "','" + persons + "'," +
                                                        " '" + ADate + "','" + ATime + "','" + trainname + "','" + DDate + "','" + Dtime + "','" + lastmonth1 + "', '" + lastmonth2 + "'," +
                                                        "'" + lastmonth3 + "','" + nextmonth1 + "','" + nextmonth2 + "','" + nextmonth3 + "','" + earlier + "','" + lastService + "')";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet GetTrans(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select a.From_Sf_Name,a.From_HQ,a.From_Designation,b.sf_emp_id,a.Sl_No,convert(varchar, a.Created_Date, 105)dt from Trans_carservice  a inner join Mas_Salesforce b on a.From_Sf_Code=b.sf_code  where  a.Division_Code ='" + div_code + "' order by a.Created_Date ASC";
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
        public DataSet GetCount(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select COUNT(*)cnt from Trans_carservice  Where   Division_Code ='" + Division_Code + "'";
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
        public DataSet getListedDr(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName,d.SDP as Activity_Date, " +
                     //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                     //  " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                    "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.Doc_Special_Code = s.Doc_Special_Code " +
                    "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                    " and d.Doc_QuaCode = g.Doc_QuaCode " +
                    "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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

        public DataSet ViewListedDr_Precall(string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode, " +
                       //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,   " +
                      " d.Territory_Code,d.ListedDr_Address2,d.ListedDr_Address3, d.ListedDr_Pin, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " ListedDr_Email, convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,d.State_Code," +
                      " d.ListedDr_Profile,d.ListedDr_Visit_Days,d.ListedDr_IUI,d.ListedDr_Avg_Patients,d.ListedDr_DayTime," +
                      " d.ListedDr_Hospital,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,d.Hospital_Address,ListedDr_RegNo,ListedDr_Sex,Doc_SubCatCode, " +
                      " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName, " +
                      " case when Doctor_Type =0 then 'No' else case when Doctor_Type =1 then 'Yes' else case when Doctor_Type =null then 'No' end end end Crm, " +
                      " (select top (1) Mgr_Code from Core_Doctor_Map cc where  cc.DR_Code=d.ListedDrCode and cc.DR_Code= '" + drcode + "' ) Core " +
                           " FROM  " + " " +
                      " Mas_ListedDr d " +
                        "WHERE d.ListedDrCode =  '" + drcode + "'  " +

                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getPrevious_Visit_Miss(string sf_code, string listeddrcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select convert(char(10),max(Activity_Date),105)   from DCRMain_Trans  a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  and Trans_Detail_Info_Code='" + listeddrcode + "' ";



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


        //slno

        public DataSet getListedDr_SlNO(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                      //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " order by ListedDr_Sl_No";
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
        //Reactivation
        public DataSet getListedDr_React(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, convert(char(11),ListedDr_Deactivate_Date,103) ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name ,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_Qua_Name as Doc_QuaName, " +
                      // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        " Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 1 and Interchange_drs is null   ";
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
        public DataSet getListedDr_React_approve(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            //strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
            //             //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
            //             " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
            //            "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
            //            "WHERE  " +
            //            " d.Doc_Special_Code = s.Doc_Special_Code " +
            //            "and d.Doc_ClsCode= dc.Doc_ClsCode " +
            //            " and d.Doc_QuaCode = g.Doc_QuaCode " +
            //            "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
            //            "and  d.sf_code = '" + sfcode + "'" +
            //            "and d.ListedDr_Active_Flag = 3 and d.SLVNo not in " +
            //            " (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2 and sf_code = '" + sfcode + "')";

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name ,d.Doc_Class_ShortName as Doc_ClsName ,d.Doc_Qua_Name as Doc_QuaName, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                       "Mas_ListedDr d WHERE  d.sf_code = '" + sfcode + "'" +
                       "and d.ListedDr_Active_Flag = 3 ";
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

        //Reject List

        public DataSet getListedDr_Reject(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                         //"stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.ListedDr_Active_Flag = 4 ";
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

        // Sorting For ListedDR List 
        public DataTable getListedDoctorList_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,  " +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                         "   FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " order by 2";
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

        //Reactivation Sorting

        public DataTable getListedDoctorList_DataTable_React(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name ,d.Doc_Class_ShortName as Doc_ClsName ,d.Doc_Qua_Name as Doc_QuaName, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +

                        "and d.ListedDr_Active_Flag = 1";
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

        //public DataSet getListedDr_MGR(string sfcode, int iVal)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsListedDR = null;

        //    strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ  " +
        //                " from mas_listeddr a, Mas_Salesforce b " +
        //                " where b.Reporting_To_SF = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " ";
        //    try
        //    {
        //        dsListedDR = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsListedDR;
        //}
        public DataSet getListedDr_MGR(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (div_code.Contains(","))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in(" + div_code + ")";
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
        public DataSet getListedDr_MGRNew(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + "  " +
                        " and a.SLVNo not in (select SLVNo from mas_listeddr v where ListedDr_Active_Flag = 2  and v.Division_code = '" + div_code + "' and a.sf_code=v.sf_code)  ";
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
        public DataSet getListedDr_MGRapp(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + "  " +
                        " and a.SLVNo not in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3  and Division_code = '" + div_code + "')  ";
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
        public DataSet getListedDrforSpl(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,d.Doc_Qua_Name,d.Doc_Class_ShortName," +
                       //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        "  FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforCat(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                      // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforQual(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName,d.Doc_Qua_Name, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +

                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforClass(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Qua_Name, d.Doc_Class_ShortName, " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforTerr(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName, d.Doc_Qua_Name, " +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
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

        //Reactivation Dr Search

        public DataSet getListedDrforSpl_React(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name, d.Doc_Class_ShortName as Doc_ClsName ,d.Doc_Qua_Name as Doc_QuaName, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +

                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDrforCat_React(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDrforQual_React(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataTable get_Prod_map_Pre(string ListedDrCode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            strQry = "Select Listeddr_Code,Doctor_Name,Product_Code as Product_Code_SlNo,Product_Name from Map_LstDrs_Product " +
                     " where Listeddr_Code =  '" + ListedDrCode + "' and sf_code='" + sf_code + "' ";


            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataSet getListedDrforClass_React(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName , " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_QuaCode= g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                         "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDrforTerr_React(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDrforName_React(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                         //"stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        "and d.ListedDr_Active_Flag = 1";
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
        //end

        public DataSet getListedDoctorr_Territory(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0 Order By 2";
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

        public DataTable get_Chem_map_Pre(string ListedDrCode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            strQry = " Select m.listeddr_code,c.chemists_name,m.Chemists_Code from Map_LstDrs_Chemists m, mas_chemists c " +
                      " where m.Listeddr_Code =  '" + ListedDrCode + "' and m.sf_code='" + sf_code + "' and m.chemists_code=c.chemists_code ";


            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataTable get_ListedDoctor_Territory(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.Territory_Code, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where  t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        " and d.ListedDr_Active_Flag = 0 Order By 2";
            try
            {
                //dsListedDR = db_ER.Exec_DataSet(strQry);
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public DataSet getListedDrforName(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName, d.Doc_Qua_Name, " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        " and d.ListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctor(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1," +
                         "case isnull(ListedDR_DOB,null)" +
                            " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                         "case isnull(ListedDR_DOW,null)" +
                            " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103)  end ListedDR_DOW," +
                         " a.No_of_Visit, ListedDR_Mobile, " +
                                        " ListedDR_Phone, ListedDR_EMail,ListedDr_PinCode,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution,Town_City,Unique_Dr_Code,Pan_Card   " +
                                        " FROM Mas_ListedDr a, Mas_Doctor_Category dc " +
                                        " WHERE Sf_Code =  '" + sfcode + "' and a.Division_Code = '" + div_code + "' " +
                                        " AND ListedDr_Active_Flag = 0" +
                                        " AND a.Doc_Cat_Code=dc.Doc_Cat_Code " +
                                        " order by Doc_Cat_Sl_No";
            // " order by ListedDr_Name";
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
        public DataSet getListedDoctorforName(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                        "case isnull(ListedDR_DOB,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                        " case isnull(ListedDR_DOW,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103)  end ListedDR_DOW," +
                        // " convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +
                        " a.No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail,ListedDr_PinCode,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution,Unique_Dr_Code   " +
                        " FROM Mas_ListedDr a, Mas_Doctor_Category dc" +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND ListedDr_Name like '%" + Name + "%'" +
                        " AND ListedDr_Active_Flag = 0" +
                        " AND a.Doc_Cat_Code=dc.Doc_Cat_Code  order by Doc_Cat_Sl_No";
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

        public DataTable Get_Business_Pre(string div_Code, string dr_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsUserList = null;
            strQry = "  select distinct product_code,c.Product_Detail_Name from Trans_DCR_BusinessEntry_Details d, " +
                     "  Trans_DCR_BusinessEntry_head h,Mas_Product_Detail c  where h.division_code='" + div_Code + "' and " +
                     "  h.listeddrcode='" + dr_code + "'  and sf_code='" + sf_code + "' and d.Trans_sl_No = h.Trans_sl_No and d.product_code=c.Product_Detail_Code ";

            try
            {
                dsUserList = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }

        public DataSet getListedDoctorforTerr(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                        "case isnull(ListedDR_DOB,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                        "case isnull(ListedDR_DOW,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103)  end ListedDR_DOW," +
                        // " convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +
                        " a.No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail,ListedDr_PinCode,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution,Unique_Dr_Code   " +
                        " FROM Mas_ListedDr a, Mas_Doctor_Category dc " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND (Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + TerrCode + ',' + "%' or Territory_Code like '" + TerrCode + "') " +
                        " AND ListedDr_Active_Flag = 0" +
                        " AND a.Doc_Cat_Code=dc.Doc_Cat_Code  order by Doc_Cat_Sl_No";


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
        public DataSet getListedDoctorforClass(string sfcode, string ClsCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                        "case isnull(ListedDR_DOB,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                        "case isnull(ListedDR_DOW,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103) end ListedDR_DOW," +
                        //" convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +
                        "  a.No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail,ListedDr_PinCode,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution,Unique_Dr_Code   " +
                        " FROM Mas_ListedDr a, Mas_Doctor_Category dc " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_ClsCode ='" + ClsCode + "' " +
                        " AND ListedDr_Active_Flag = 0" +
                        " AND a.Doc_Cat_Code = dc.Doc_Cat_Code  order by Doc_Cat_Sl_No";
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
        public DataSet getListedDoctorforSpl(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                      "case isnull(ListedDR_DOB,null)" +
                      " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                     "case isnull(ListedDR_DOW,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103)  end ListedDR_DOW," +
                        //" convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, "+
                        " a.No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail,ListedDr_PinCode,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution,Unique_Dr_Code   " +
                        " FROM Mas_ListedDr  a, Mas_Doctor_Category dc " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_Special_Code ='" + SplCode + "' " +
                        " AND ListedDr_Active_Flag = 0"+
                        " AND a.Doc_Cat_Code=dc.Doc_Cat_Code " +
                                     " order by Doc_Cat_Sl_No";
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
        public DataSet getListedDoctorforCat(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                      " case isnull(ListedDR_DOB,null)" +
                      " when '1900-01-01 00:00:00.000' then null  else  ListedDR_DOB  end ListedDR_DOB," +
                      " case isnull(ListedDR_DOW,null)" +
                      " when '1900-01-01 00:00:00.000' then null  else  ListedDR_DOW  end ListedDR_DOW," +
                        // " convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +                      
                        " a.No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail,ListedDr_PinCode,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution,Unique_Dr_Code   " +
                        " FROM Mas_ListedDr  a, Mas_Doctor_Category dc " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND a.Doc_Cat_Code ='" + CatCode + "' " +
                        " AND ListedDr_Active_Flag = 0"+
                        " AND a.Doc_Cat_Code=dc.Doc_Cat_Code " +
                                        " order by Doc_Cat_Sl_No";
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
        public DataSet getListedDoctorforQual(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                     "case isnull(ListedDR_DOB,null)" +
                     " when '1900-01-01 00:00:00.000' then null  else  ListedDR_DOB  end ListedDR_DOB," +
                     "case isnull(ListedDR_DOW,null)" +
                      " when '1900-01-01 00:00:00.000' then null  else  ListedDR_DOW  end ListedDR_DOW," +
                        // " convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +
                        " a.No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail,ListedDr_PinCode,Day_1,Day_2,Day_3,Dr_Potential,Dr_Contribution,Unique_Dr_Code   " +
                        " FROM Mas_ListedDr  a, Mas_Doctor_Category dc " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_QuaCode ='" + QuaCode + "' " +
                        " AND ListedDr_Active_Flag = 0" +
                        " AND a.Doc_Cat_Code=dc.Doc_Cat_Code  order by Doc_Cat_Sl_No";

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

        public DataSet getListedDr(string sfcode, string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Qua_Name as Doc_QuaName , d.Doc_Class_ShortName as Doc_ClsName , " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        " and d.ListedDrCode = '" + drcode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet ViewListedDr(string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode, " +
                       //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,   " +
                      " d.Territory_Code,d.ListedDr_Address2,d.ListedDr_Address3, d.ListedDr_Pin, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " ListedDr_Email, convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,d.State_Code," +
                      " d.ListedDr_Profile,d.ListedDr_Visit_Days,d.ListedDr_IUI,d.ListedDr_Avg_Patients,d.ListedDr_DayTime," +
                      " d.ListedDr_Hospital,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,d.Hospital_Address,ListedDr_RegNo,d.Doctor_ERP_Code,d.ListedDr_Sex,d.Pan_Card FROM  " + " " +
                      " Mas_ListedDr d " +
                        "WHERE d.ListedDrCode =  '" + drcode + "'  " +

                        "and d.ListedDr_Active_Flag = 0";
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
        public int DeActivate(string dr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag = 3 , " +
                            " listeddr_deactivate_date = getdate() " +
                            " WHERE listeddrcode = '" + dr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //Reactivate
        public int ReActivate(string dr_code, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag=0 , " +
                            " listeddr_deactivate_date = NULL " +
                            " WHERE listeddrcode = '" + dr_code + "' and Division_Code = '" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int Approve(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "', ListedDr_Deactivate_Date=getdate() " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Reject(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Reject_Flag = 'DR', Listeddr_App_Mgr = '" + sf_name + "',listeddr_deactivate_date=NULL " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int ApproveAdd(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "', ListedDr_Created_Date = getdate()  " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int AddVsDeActivate(string dr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select SLVNo from Mas_ListedDr where listeddrcode = '" + dr_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

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

        public DataSet LoadTerritory(string sf_code, string Territory_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
            //             " UNION " +
            //         " SELECT Territory_Code,Territory_Name " +
            //         " FROM  Mas_Territory_Creation where Territory_Code != '" + Territory_Code + "' " +
            //         " AND Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Code != '" + Territory_Code + "' " +
                     " AND Sf_Code = '" + sf_code + "' AND territory_active_flag=0 " +
                     " UNION " +
                     " SELECT 999 as Territory_Code,'Missed DRs' as Territory_Name ";

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

        public DataSet LoadTerritory(string sf_code, string Territory_Code, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
            //             " UNION " +
            //         " SELECT Territory_Code,Territory_Name " +
            //         " FROM  Mas_Territory_Creation where Territory_Code != '" + Territory_Code + "' " +
            //         " AND Sf_Code = '" + sf_code + "' AND " +
            //         " Territory_Code in (" + terr_code + ") " +
            //         " AND territory_active_flag=0 ";

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                     " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Code != '" + Territory_Code + "' " +
                     " AND Sf_Code = '" + sf_code + "' AND " +
                     " Territory_Code in (" + terr_code + ") " +
                     " AND territory_active_flag=0 " +
                     " UNION " +
                     " SELECT 999 as Territory_Code,'Missed DRs' as Territory_Name ";
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

        public DataSet FetchTerritoryName(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code Territory_Code, " +
                     " (a.Territory_Name +  ' (' + CAST((select COUNT(b.ListedDrCode) from Mas_ListedDr b " +
                     " where a.Territory_Code=b.Territory_Code and b.ListedDr_Active_Flag=0) as CHAR(3)) " +
                     " + ') ' ) Territory_Name " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND a.territory_active_flag=0 ";
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


        public DataSet FetchCategory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Doc_Cat_Code,'---Select---' as Doc_Cat_SName, '---Select---' as Doc_Cat_Name " +
                         " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name " +
                     " FROM  Mas_Doctor_Category where division_Code = '" + div_code + "' AND doc_cat_active_flag=0 ";
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

        public DataSet FetchChemist(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT '' as Chemists_Code, '---Select---' as Chemists_Name " +
                  " UNION " +
                   "SELECT Chemists_Code,Chemists_Name " +
                    "FROM  Mas_Chemists where Division_Code = '" + div_code + "' AND Sf_Code = '" + sf_code + "'" +
                   " and Chemists_Active_Flag = 0 ";

            //strQry = " SELECT '' as Chemists_Code,'---Select---' as Chemists_Name,  " +
            //             " UNION " +
            //         " SELECT Chemists_Code,Chemists_Name" +
            //         " FROM  Mas_Chemists where division_Code = '" + div_code + "' and Sf_Code = '" + sf_code + "' and Chemists_Active_Flag=0 ";
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


        public DataSet FetchSpeciality(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Doc_Special_Code,'---Select---' as Doc_Special_SName, '---Select---' as Doc_Special_Name " +
                         " UNION " +
                     " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name " +
                     " FROM  Mas_Doctor_Speciality where division_Code = '" + div_code + "' AND doc_special_active_flag=0 ";
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

        public bool RecordExist(string Listed_DR_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDr_Name) FROM Mas_ListedDr WHERE ListedDr_Name='" + Listed_DR_Name + "' AND Sf_Code ='" + sf_code + "' and (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
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
        public bool RecordExist(string ListedDrCode, string Listed_DR_Name, string Sf_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDr_Name) FROM Mas_ListedDr WHERE ListedDr_Name = '" + Listed_DR_Name + "' AND ListedDrCode!='" + ListedDrCode + "' AND Sf_Code ='" + Sf_Code + "' AND Listeddr_active_flag = 0  ";

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


       

        public int RecordAdd(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code)
        {
            int iReturn = -1;

            if (!RecordExist(Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;

                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");
                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);

                    //strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                    //         " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                    //         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                    //         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                    //         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', 2, getdate(), '" + Division_Code + "','123','123','TestDR@Test.com', " +
                    //         " '01-01-1900','01-01-1900','12','4','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate())";

                    strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                             " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                             " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                             " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                             " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', 2, getdate(), '" + Division_Code + "','','','', " +
                             " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate())";


                    iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        //Insert a record into LstDoctor_Terr_Map_History table
                        strQry = "SELECT isnull(max(SNo)+1,'1') SNo from LstDoctor_Terr_Map_History ";
                        int SNo = db.Exec_Scalar(strQry);

                        strQry = "insert into LstDoctor_Terr_Map_History values('" + SNo + "','" + sf_code + "',  '" + Listed_DR_Code + "', " +
                                  " '" + Listed_DR_Terr + "',getdate(),getdate(), '" + Division_Code + "')";

                        iReturn = db.ExecQry(strQry);

                        if (Listed_DR_Terr.IndexOf(",") != -1)
                        {
                            string[] subterr;
                            subterr = Listed_DR_Terr.Split(',');
                            foreach (string st in subterr)
                            {
                                if (st.Trim().Length > 0)
                                {
                                    strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                                    int iPlanNo = db.Exec_Scalar(strQry);

                                    //Insert a record into Call Plan
                                    strQry = "insert into Call_Plan values('" + sf_code + "', '" + Convert.ToInt32(st) + "', getdate(), '" + iPlanNo + "', " +
                                            " '" + Listed_DR_Code + "', '" + Division_Code + "', 0,'')";

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
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }


        public int RecordAdd(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, string Terr_Name)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Listed_DR_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                         " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', 0, getdate(), '" + Division_Code + "','','','', " +
                         " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate())";


                iReturn = db.ExecQry(strQry);

                //Insert a record into LstDoctor_Terr_Map_History table
                strQry = "SELECT isnull(max(SNo)+1,'1') SNo from LstDoctor_Terr_Map_History ";
                int SNo = db.Exec_Scalar(strQry);

                strQry = "insert into LstDoctor_Terr_Map_History values('" + SNo + "','" + sf_code + "',  '" + Listed_DR_Code + "', " +
                          " '" + Listed_DR_Terr + "',getdate(), '" + Division_Code + "')";

                iReturn = db.ExecQry(strQry);

                //Insert a record into Call Plan

                strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                int iPlanNo = db.Exec_Scalar(strQry);

                strQry = "insert into Call_Plan values('" + sf_code + "', '" + Listed_DR_Terr + "', getdate(), '" + iPlanNo + "', " +
                        " '" + Listed_DR_Code + "', '" + Division_Code + "', 0,'" + Terr_Name + "')";


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



        public int RecordAdd(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, int SLV_No, string Cat_SName, string Spec_SName, string Cls_SName, string Qual_SName)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Listed_DR_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_QuaCode,Doc_Cat_Code, " +
                         " Territory_Code, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', '" + Listed_DR_Qual + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', 2, getdate(), '" + Division_Code + "','','','', " +
                         " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + SLV_No + "',getdate(),'" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qual_SName + "')";

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


        // DetailAdd ListedDr
        public int RecordAdd(string Listed_DR_Name, string DR_Sex, string DR_DOB, string DR_Qual, string DR_DOW, string DR_Spec, string DR_RegNo,
            string DR_Catg, string DR_Terr, string DR_Comm, string DR_Class, string DR_Address1, string DR_Address2, string DR_Address3,
            string DR_State, string DR_Pin, string DR_Mobile, string DR_Phone, string DR_EMail, string DR_Profile, string DR_Visit_Days,
            string DR_DayTime, string DR_IUI, string DR_Avg_Patients, string DR_Hospital, string DR_Class_Patients, string DR_Consultation_Fee,
            string sf_code, string Hospital_Address, string Cat_SName, string Spec_SName, string Cls_SName, string Qua_SName, string ERPcode)
        {
            int iReturn = -1;

            if (!RecordExist(Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);


                    strQry = "insert into Mas_ListedDr (ListedDrCode, Sf_Code, ListedDr_Name, ListedDr_Sex, ListedDr_DOB,Doc_QuaCode, " +
                           " ListedDr_DOW, Doc_Special_Code,ListedDr_RegNo, Doc_Cat_Code, Territory_Code, " +
                           " ListedDr_Comm, Doc_ClsCode, ListedDr_Address1, ListedDr_Address2, ListedDr_Address3, " +
                           " State_Code, ListedDr_Pin, ListedDr_Mobile, ListedDr_Phone, ListedDr_EMail, ListedDr_Profile, " +
                           " ListedDr_Visit_Days,ListedDr_DayTime, ListedDr_IUI, ListedDr_Avg_Patients, ListedDr_Hospital, ListedDr_Class_Patients, ListedDr_Consultation_Fee, " +
                           " ListedDr_Created_Date, Division_Code, ListedDR_Sl_No, LastUpdt_Date, ListedDr_Active_Flag, Hospital_Address,SLVNo, Doc_Cat_ShortName, Doc_Spec_ShortName, Doc_Class_ShortName, Doc_Qua_Name,Doctor_ERP_Code) " +
                           " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + DR_Sex + "', '" + DR_DOB + "', " +
                           " '" + DR_Qual + "', '" + DR_DOW + "', '" + DR_Spec + "', '" + DR_RegNo + "', '" + DR_Catg + "', " +
                           " '" + DR_Terr + "', '" + DR_Comm + "', '" + DR_Class + "', '" + DR_Address1 + "', '" + DR_Address2 + "', '" + DR_Address3 + "', " +
                           " '" + DR_State + "', '" + DR_Pin + "', '" + DR_Mobile + "', '" + DR_Phone + "' , '" + DR_EMail + "' , '" + DR_Profile + "' , " +
                           " '" + DR_Visit_Days + "', '" + DR_DayTime + "', '" + DR_IUI + "', '" + DR_Avg_Patients + "', '" + DR_Hospital + "', " +
                           " '" + DR_Class_Patients + "', '" + DR_Consultation_Fee + "', getdate(), '" + Division_Code + "', '" + Listed_DR_Code + "', getdate(), 2, '" + Hospital_Address + "', '" + Listed_DR_Code + "', '" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qua_SName + "', '" + ERPcode + "')";


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


        public int BulkEdit(string str, string Doc_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_ListedDr SET " + str + "  Where ListedDrCode='" + Doc_Code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int BulkEdit(string str, string Doc_Code, bool bSDP, string Listed_DR_Terr, string sf_code, string Division_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_ListedDr SET " + str + "  Where ListedDrCode='" + Doc_Code + "'";
                iReturn = db.ExecQry(strQry);

                //if (bSDP)
                //{
                //    if (iReturn != -1)
                //    {
                //        if (Listed_DR_Terr.IndexOf(",") != -1)
                //        {
                //            string[] subterr;
                //            subterr = Listed_DR_Terr.Split(',');
                //            foreach (string st in subterr)
                //            {
                //                if (st.Trim().Length > 0)
                //                {
                //                    string sQry = string.Empty;

                //                    sQry = "select count(Territory_Code) from call_plan Where ListedDrCode='" + Doc_Code + "' and " +
                //                               " Territory_Code = '" + st + "' and sf_code='" + sf_code + "' and Division_code='" + Division_Code + "' ";

                //                    int iRecordExist = db.Exec_Scalar(sQry);

                //                    CallPlan cp = new CallPlan();
                //                    //If the DR is not available on Call_Plan then the DR will be loaded in Call_Plan
                //                    if (iRecordExist <= 0)
                //                    {

                //                        iReturn = cp.Copy_WorkPlan(st, Doc_Code, sf_code);
                //                    }
                //                    else
                //                    {
                //                        iReturn = cp.Update_CallPlan(st, Doc_Code, sf_code);
                //                    }



                //                    //strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                //                    //int iPlanNo = db.Exec_Scalar(strQry);

                //                    ////Insert a record into Call Plan
                //                    //strQry = "insert into Call_Plan values('" + sf_code + "', '" + Convert.ToInt32(st) + "', getdate(), '" + iPlanNo + "', " +
                //                    //        " '" + Doc_Code + "', '" + Division_Code + "', 0,'')";

                //                    //iReturn = db.ExecQry(strQry);
                //                }
                //            }
                //        }
                //    }

                //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int BulkEdit_CallPlan(string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsCallPlan = null;
                string sTerr = string.Empty;
                string Division_Code = string.Empty;
                string sQry = string.Empty;
                CallPlan cp = new CallPlan();

                strQry = "select Territory_Code,SF_Code,Division_code from Mas_ListedDr Where ListedDrCode='" + Doc_Code + "'";
                dsCallPlan = db.Exec_DataSet(strQry);

                if (dsCallPlan.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dsCallPlan.Tables[0].Rows)
                    {
                        sTerr = dataRow["Territory_Code"].ToString();
                        Division_Code = dataRow["Division_Code"].ToString();


                        sQry = "select count(Territory_Code) from call_plan Where ListedDrCode='" + Doc_Code + "' and " +
                                   " Territory_Code = '" + sTerr + "' and sf_code='" + sf_code + "' and Division_code='" + Division_Code + "' ";

                        int iRecordExist = db.Exec_Scalar(sQry);

                        //If the DR is not available on Call_Plan then the DR will be loaded in Call_Plan
                        if (iRecordExist <= 0)
                        {
                            iReturn = cp.Copy_WorkPlan(sTerr, Doc_Code, sf_code);
                        }
                        else
                        {
                            iReturn = cp.Update_CallPlan(sTerr, Doc_Code, sf_code);
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



        public DataSet getDoctor_Terr_Catg_Spec_Class_Qual(string doc_code, string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Territory_Code,Doc_Cat_Code,Doc_Special_Code,Doc_ClsCode,Doc_QuaCode,SDP,Doctor_Type,No_of_Visit,Town_City,Geo_Tag_Count,Unique_Dr_Code FROM  Mas_ListedDr " +
                     " WHERE ListedDrCode='" + doc_code + "' AND sf_Code= '" + sf_code + "' and Division_Code = '" + div_code + "' ";
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
        //alphabetical order
        public DataSet getDoctorlist_Alphabet(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select '1' val,'All' ListedDr_Name " +
                     " union " +
                     " select distinct LEFT(ListedDr_Name,1) val, LEFT(ListedDr_Name,1) ListedDr_Name" +
                     " FROM Mas_ListedDr " +
                     " WHERE ListedDr_Active_Flag=0 " +
                     " AND Sf_Code =  '" + sfcode + "' " +
                     " ORDER BY 1";
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



        public DataSet FetchClass(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Doc_ClsCode,'---Select---' as Doc_ClsSName, '---Select---' Doc_ClsName " +
                         " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName " +
                     " FROM  Mas_Doc_Class where division_Code = '" + div_code + "' AND doc_cls_activeflag=0 ";
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

        public DataSet FetchQualification(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as  Doc_QuaCode,'---Select---' as Doc_QuaName " +
                         " UNION " +
                     " SELECT Doc_QuaCode,Doc_QuaName " +
                     " FROM  Mas_Doc_Qualification where division_Code = '" + div_code + "' AND doc_qua_activeflag=0 ";
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
        public DataSet getDoctorlist_Alphabet(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " AND LEFT(ListedDr_Name,1) = '" + sAlpha + "' " +
                    " ORDER BY 2";
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


        public int Div_Code(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            return div_code;
        }
        public int RecordUpdateDoctor(string Listed_DR_Name, string Territory_Code, string Doc_ClsCode, string Doc_Cat_Code, string Doc_Special_Code, string Doc_QuaCode, string ListedDrCode, string CatSName, string SpecSName, string ClsSName, string QuaSName, string sf_code)
        {
            int iReturn = -1;
            if (!RecordExist(ListedDrCode, Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");
                    strQry = " update Mas_ListedDr" +
                        " set ListedDr_Name='" + Listed_DR_Name + "',Territory_Code='" + Territory_Code + "', Doc_ClsCode='" + Doc_ClsCode + "'," +
                        " Doc_Cat_Code='" + Doc_Cat_Code + "', Doc_Special_Code='" + Doc_Special_Code + "', Doc_QuaCode='" + Doc_QuaCode + "', " +
                        " Doc_Cat_ShortName = '" + CatSName + "', Doc_Spec_ShortName = '" + SpecSName + "', Doc_Class_ShortName = '" + ClsSName + "', Doc_Qua_Name = '" + QuaSName + "' " +
                        " where ListedDrCode='" + ListedDrCode + "'  ";

                    //strQry = " SELECT Territory_Code,Doc_Cat_Code,Doc_Special_Code,Doc_ClsCode,Doc_QuaCode FROM  Mas_ListedDr " +
                    //  " WHERE ListedDrCode='" + doc_code + "' AND sf_Code= '" + sf_code + "' ";

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

        //Changes done by Priya
        public int RecordCount(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDrCode) FROM Mas_ListedDr WHERE Sf_Code = '" + sf_code + "' and ListedDr_Active_Flag = 0 ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end
        public int RecordUpdate(string ListedDrCode, string Listed_DR_Name, string DR_Sex, string DR_DOB, string DR_Qual, string DR_DOW, string DR_Spec, string DR_RegNo,
            string DR_Catg, string DR_Terr, string DR_Comm, string DR_Class, string DR_Address1, string DR_Address2, string DR_Address3,
            string DR_State, string DR_Pin, string DR_Mobile, string DR_Phone, string DR_EMail, string DR_Profile, string DR_Visit_Days,
            string DR_DayTime, string DR_IUI, string DR_Avg_Patients, string DR_Hospital, string DR_Class_Patients, string DR_Consultation_Fee,
            string sf_code, string Hospital_Address, string Cat_SName, string Spec_SName, string Cls_SName, string Qua_SName, string ERPcode,string Pan_card)
        {
            int iReturn = -1;

            if (!RecordExist(ListedDrCode, Listed_DR_Name, sf_code))
            {

                try
                {

                    DB_EReporting db = new DB_EReporting();
                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                    strQry = " update Mas_ListedDr set ListedDr_Name = '" + Listed_DR_Name + "', ListedDr_Sex ='" + DR_Sex + "', ListedDr_DOB='" + DR_DOB + "',Doc_QuaCode='" + DR_Qual + "', " +
                           " ListedDr_DOW ='" + DR_DOW + "', Doc_Special_Code = '" + DR_Spec + "',ListedDr_RegNo ='" + DR_RegNo + "', Doc_Cat_Code='" + DR_Catg + "', Territory_Code='" + DR_Terr + "', " +
                           " ListedDr_Comm ='" + DR_Comm + "', Doc_ClsCode ='" + DR_Class + "', ListedDr_Address1 = '" + DR_Address1 + "', ListedDr_Address2 = '" + DR_Address2 + "', ListedDr_Address3='" + DR_Address3 + "', " +
                           " State_Code='" + DR_State + "', ListedDr_Pin ='" + DR_Pin + "', ListedDr_Mobile='" + DR_Mobile + "', ListedDr_Phone='" + DR_Phone + "', ListedDr_EMail='" + DR_EMail + "', ListedDr_Profile='" + DR_Profile + "', " +
                           " ListedDr_Visit_Days='" + DR_Visit_Days + "',ListedDr_DayTime='" + DR_DayTime + "', ListedDr_IUI='" + DR_IUI + "', ListedDr_Avg_Patients='" + DR_Avg_Patients + "', ListedDr_Hospital='" + DR_Hospital + "', ListedDr_Class_Patients='" + DR_Class_Patients + "', ListedDr_Consultation_Fee='" + DR_Consultation_Fee + "', " +
                           " ListedDR_Sl_No='" + ListedDrCode + "', LastUpdt_Date=getdate(), Hospital_Address='" + Hospital_Address + "', SLVNo = '" + ListedDrCode + "', Doc_Cat_ShortName = '" + Cat_SName + "', Doc_Spec_ShortName = '" + Spec_SName + "', Doc_Class_ShortName = '" + Cls_SName + "', Doc_Qua_Name = '" + Qua_SName + "', Doctor_ERP_Code = '" + ERPcode + "', Pan_Card = '" + Pan_card + "' " +
                           " where ListedDr_Active_Flag=0 and ListedDrCode='" + ListedDrCode + "'";


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
        //Changes done by Saravanan
        public DataTable getListedDrforTerr_Datatable(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataTable getListedDrforSpl_Datatable(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName , d.Doc_Qua_Name,  " +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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
        public DataTable getListedDrforCat_Datatable(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,  " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataTable getListedDrforQual_Datatable(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataTable getListedDrforClass_Datatable(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name , " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataTable getListedDrforName_Datatable(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName ,d.Doc_Qua_Name,  " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name," +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        "and d.ListedDr_Active_Flag = 0";
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
        public DataTable getDoctorlistAlphabet_Datatable(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName ,d.Doc_Qua_Name, d.SDP as Activity_Date, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " AND LEFT(ListedDr_Name,1) = '" + sAlpha + "' " +
                    " ORDER BY 2";
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
        public DataSet getListeddr_Alphabet(string ddlvar, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = " SELECT '' as sf_code, '---Select---' as Sf_Name " +
                     " UNION " +
                     " select sf_code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name from Mas_Salesforce " +
                     " where (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                      " and sf_type=1" +
                      " AND LEFT(Sf_Name,1) = '" + ddlvar + "' and sf_status = 0 ";


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
        public DataSet getListedDr_Add_approve(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE " +
                        " d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 2  and d.SLVNo not in " +
                        " (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 and sf_code = '" + sfcode + "')";
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

        public int Reject_Approve(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Reject_Flag = 'AR', Listeddr_App_Mgr = '" + sf_name + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getSlNO(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = " SELECT ROW_NUMBER() over (ORDER BY ListedDr_Name) AS SlNO" +
                     "  FROM  Mas_ListedDr where ListedDr_Active_Flag=0 and Sf_Code='" + Sf_Code + "'";

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
        public int Single_Multi_Select_Territory(string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select SingleDr_WithMultiplePlan_Required  from Admin_Setups where division_code = '" + div_code + "'";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet ViewListedDr_DobDow_Old(string Sf_Code, string ddlmonth, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (Date == "")
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name, " +
                          //" (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name," +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                          " d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                          " from Mas_ListedDr d, mas_salesforce s " +
                          " WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                          " and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code and ListedDr_DOB !='1900-01-01 00:00:00.000' and ListedDr_DOW !='1900-01-01 00:00:00.000'" +
                          " order by sf_name";
            }
            else
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name, " +
                         // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name," +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                         "  d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                          " from Mas_ListedDr d, mas_salesforce s " +
                            "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                            " and DAY(ListedDr_DOB)='" + Date + "' and DAY(ListedDr_DOW)='" + Date + "' " +
                            "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code and ListedDr_DOB !='1900-01-01 00:00:00.000' and ListedDr_DOW !='1900-01-01 00:00:00.000'" +
                            " order by sf_name";
            }
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
        public DataSet ViewListedDr_DobDow(string Sf_Code, string ddlmonth, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (Date == "")
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name, " +
                          //" (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name," +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                          " d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,Doc_Spec_ShortName," +
                      "STRING_AGG(CONVERT(NVARCHAR(max), CONCAT(dsc.Doc_SubCatName, '/')), CHAR(13)) AS Campaignmap,d.Sf_Code,d.ListedDr_DOB" +
                          " from Mas_ListedDr d inner join mas_salesforce s on s.sf_code = d.Sf_code " +
                          " AND d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                          " and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code and ListedDr_DOB !='1900-01-01 00:00:00.000' and ListedDr_DOW !='1900-01-01 00:00:00.000' left outer join Mas_Doc_SubCategory dsc " +
                             "   on ',' + cast(d.Dr_Campaign_Map as varchar) + ',' like '%,' + cast(dsc.Doc_SubCatCode as nvarchar(20)) + ',%' and d.Division_Code = dsc.Division_Code" +
                             " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,d.Territory_Code, d.ListedDr_Phone" +
                        ", d.ListedDr_Mobile,ListedDr_DOB,ListedDr_DOW,Doc_Spec_ShortName,d.Sf_code " +
                          " order by sf_name";
            }
            else
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name, " +
                         // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name," +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                         "  d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,Doc_Spec_ShortName," +
                        "STRING_AGG(CONVERT(NVARCHAR(max), CONCAT(dsc.Doc_SubCatName, '/')), CHAR(13)) AS Campaignmap,d.Sf_Code,d.ListedDr_DOB" +
                          " from Mas_ListedDr d inner join mas_salesforce s on s.sf_code = d.Sf_code " +
                            "AND d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                            " and DAY(ListedDr_DOB)='" + Date + "' and DAY(ListedDr_DOW)='" + Date + "' " +
                            "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code and ListedDr_DOB !='1900-01-01 00:00:00.000' and ListedDr_DOW !='1900-01-01 00:00:00.000' left outer join Mas_Doc_SubCategory dsc " +
                             "   on ',' + cast(d.Dr_Campaign_Map as varchar) + ',' like '%,' + cast(dsc.Doc_SubCatCode as nvarchar(20)) + ',%' and d.Division_Code = dsc.Division_Code" +
                             " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,d.Territory_Code, d.ListedDr_Phone" +
                        ", d.ListedDr_Mobile,ListedDr_DOB,ListedDr_DOW,Doc_Spec_ShortName,d.Sf_code " +
                            " order by sf_name";
            }
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
        public DataSet ViewListedDr_Dow_Old(string Sf_Code, string ddlmonth, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (Date == "")
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,  " +
                          // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                          "  d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                          " from Mas_ListedDr d, mas_salesforce s " +
                            "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                            "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code  and ListedDr_DOW !='1900-01-01 00:00:00.000'" +
                            " order by sf_name";
            }
            else
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,  " +
                      // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                      //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      "  d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                      " from Mas_ListedDr d, mas_salesforce s " +
                        "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' and DAY(ListedDr_DOW)='" + Date + "'" +
                        "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code  and ListedDr_DOW !='1900-01-01 00:00:00.000'" +
                        " order by sf_name";
            }
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
        public DataSet ViewListedDr_Dow(string Sf_Code, string ddlmonth, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (Date == "")
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,  " +
                          // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                          "  d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,Doc_Spec_ShortName," +
                      "STRING_AGG(CONVERT(NVARCHAR(max), CONCAT(dsc.Doc_SubCatName, '/')), CHAR(13)) AS Campaignmap,d.Sf_Code,d.ListedDr_DOB" +
                          " from Mas_ListedDr d inner join mas_salesforce s on s.sf_code = d.Sf_code " +
                            " and d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                            "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code  and ListedDr_DOW !='1900-01-01 00:00:00.000' left outer join Mas_Doc_SubCategory dsc " +
                             "   on ',' + cast(d.Dr_Campaign_Map as varchar) + ',' like '%,' + cast(dsc.Doc_SubCatCode as nvarchar(20)) + ',%' and d.Division_Code = dsc.Division_Code" +
                             " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,d.Territory_Code, d.ListedDr_Phone" +
                        ", d.ListedDr_Mobile,ListedDr_DOB,ListedDr_DOW,Doc_Spec_ShortName,d.Sf_Code " +
                            " order by sf_name";
            }
            else
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,  " +
                      // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                      //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      "  d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,Doc_Spec_ShortName," +
                      "STRING_AGG(CONVERT(NVARCHAR(max), CONCAT(dsc.Doc_SubCatName, '/')), CHAR(13)) AS Campaignmap,d.Sf_Code,d.ListedDr_DOB " +
                      " from Mas_ListedDr d inner join mas_salesforce s on s.sf_code = d.Sf_code " +
                        " and d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' and DAY(ListedDr_DOW)='" + Date + "'" +
                        "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code  and ListedDr_DOW !='1900-01-01 00:00:00.000' left outer join Mas_Doc_SubCategory dsc" +
                               " on ',' + cast(d.Dr_Campaign_Map as varchar) + ',' like '%,' + cast(dsc.Doc_SubCatCode as nvarchar(20)) + ',%' and d.Division_Code = dsc.Division_Code" +
                        " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,d.Territory_Code, d.ListedDr_Phone" +
                        ", d.ListedDr_Mobile,ListedDr_DOB,ListedDr_DOW,Doc_Spec_ShortName,d.Sf_Code " +
                        " order by sf_name";
            }
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
        public DataSet ViewListedDr_Dob(string Sf_Code, string ddlmonth, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (Date == "")
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,  " +
                          //  " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                          " d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                          " from Mas_ListedDr d, mas_salesforce s " +
                            "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "'  " +
                            "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code  and ListedDr_DOB !='1900-01-01 00:00:00.000'" +
                            " order by sf_name";
            }
            else
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name, " +
                     // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     "  d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                     " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                     " from Mas_ListedDr d, mas_salesforce s " +
                       "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "' and DAY(ListedDr_DOB)='" + Date + "' " +
                       "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code  and ListedDr_DOB !='1900-01-01 00:00:00.000'" +
                       " order by sf_name";
            }
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
        public DataSet Speciality_doc(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name " +
                     " FROM  Mas_Doctor_Speciality where division_Code = '" + div_code + "' AND doc_special_active_flag=0 order by Doc_Special_Name asc";
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

        public DataSet Category_doc(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name " +
                     " FROM  Mas_Doctor_Category where division_Code = '" + div_code + "' AND doc_cat_active_flag=0 order by Doc_Cat_Name asc";
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


        public DataSet Terr_doc(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name " +
                     " FROM  Mas_Territory_Creation " +
                     " where Sf_code = '" + sf_code + "' " +
                     " AND division_Code = '" + div_code + "' AND territory_active_flag=0 ";
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
        public DataSet Load_Territory(string sf_code)
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

        public DataSet Load_Territory_catg(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 " +
                     " UNION " +
                      " SELECT 999 as Territory_Code,'Missed DRs' as Territory_Name ";


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
        // changes done by priya
        public DataSet GetCategory_Special_Code(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Doc_Special_Code,Doc_Special_SName from Mas_Doctor_Speciality where Doc_Special_Name='" + Doc_Special_Name + "' and Division_Code = '" + div_code + "' ";

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

        public DataSet GetTerritory_Code(string Territory_Name, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Territory_Code from Mas_Territory_Creation where Territory_Name='" + Territory_Name + "'  and sf_code='" + sf_code + "' AND territory_active_flag=0";

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

        public DataSet GetDoc_Cat_Code(string Cat_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Doc_Cat_Code,Doc_Cat_SName from Mas_Doctor_Category where Doc_Cat_Name='" + Cat_Name + "' and Division_Code = '" + div_code + "'";

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
        public int GetListedDrCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(Max(ListedDrCode)+1,'1') ListedDrCode from Mas_ListedDr";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }


        public int IsExistingDoctor(string doctorname)//,int territory,int speciality,int category)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT count(ListedDrCode) from Mas_listedDr where ListedDr_Name='" + doctorname + "'";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        // Changes done by Priya
        public int Update_LdDoctorSno(string Listed_DR_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET ListedDr_Sl_No = '" + Sno + "' ," +
                         " LastUpdt_Date = getdate() " +
                         " WHERE ListedDrCode = '" + Listed_DR_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Convert_ListedDr(string dr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag=0 , " +
                            " listeddr_deactivate_date = getdate() " +
                            " WHERE listeddrcode = '" + dr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //Convert Unlist to List --Changes done by Priya
        public int ConvertDoctors(string ListedDrCode, string sf_code)
        {
            int iReturn = -1;


            try
            {

                DB_EReporting db = new DB_EReporting();

                int Listed_DR_Code = -1;

                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = " Insert into Mas_ListedDr(ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
                         " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,Territory_Code, " +
                         " Doc_QuaCode,ListedDr_Active_Flag,ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
                         " ListedDr_Sl_No,ListedDr_Special_No,Division_Code,SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name) " +
                         " Select '" + Listed_DR_Code + "'  as ListedDrCode, SF_Code, UnListedDr_Name as ListedDr_Name,UnListedDr_Address1 as ListedDr_Address1, " +
                         " UnListedDr_Phone as ListedDr_Phone,UnListedDr_Mobile as ListedDr_Mobile,UnListedDr_Email as ListedDr_Email, " +
                         " UnListedDr_DOB as ListedDr_DOB, UnListedDr_DOW as ListedDr_DOW, a.Doc_Special_Code, " +
                         " a.Doc_Cat_Code, a.Territory_Code, a.Doc_QuaCode as Doc_QuaCode,UnListedDr_Active_Flag as ListedDr_Active_Flag,UnListedDr_Created_Date as ListedDr_Created_Date, " +
                         " UnListedDr_Deactivate_Date as ListedDr_Deactivate_Date, '" + Listed_DR_Code + "' as ListedDr_Sl_No,UnListedDr_Special_No as ListedDr_Special_No,a.Division_Code as Division_Code," +
                         " '" + Listed_DR_Code + "' as SLVNo,a.Doc_ClsCode as Doc_ClsCode,a.LastUpdt_Date,visit_days,Visit_Hours, " +
                         " c.Doc_Cat_SName as Doc_Cat_ShortName, s.Doc_Special_SName as Doc_Spec_ShortName, cl.Doc_ClsSName as Doc_Class_ShortName, q .Doc_QuaName as Doc_Qua_Name" +
                         " from Mas_UnListedDr a, Mas_Doctor_Category c, Mas_Doctor_Speciality s, Mas_Doc_Class cl, Mas_Doc_Qualification q where UnListedDrCode ='" + ListedDrCode + "' and Sf_Code= '" + sf_code + "' " +
                         " and a.Doc_Cat_Code = c.Doc_Cat_Code and a.Doc_Special_Code = s.Doc_Special_Code and " +
                         " a.Doc_ClsCode = cl.Doc_ClsCode and a.Doc_QuaCode = q.Doc_QuaCode ";


                iReturn = db.ExecQry(strQry);

                ConvertGeoDoctors(ListedDrCode, Listed_DR_Code);//Added GEO unlisted convert to GEO listed customer By Ferooz

                strQry = "update Mas_UnListedDr set UnListedDr_Active_Flag=1,Transfered_date=getdate() where UnListedDrCode ='" + ListedDrCode + "' and sf_code = '" + sf_code + "'";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int ConvertGeoDoctors(string ListedDrCode, int Listed_DR_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                DataSet dsUlListedDR = null;
                strQry = "select * from [map_GEO_UnlistCustomers]  where Cust_Code = '" + ListedDrCode + "'";
                dsUlListedDR = db.Exec_DataSet(strQry);

                if (dsUlListedDR.Tables[0].Rows.Count > 0)
                {
                    strQry = " Insert into map_GEO_Customers " +
                            " (MapId, Cust_Code, lat, long, addrs, StatFlag, Division_code, imge_name) " +
                            " (select (SELECT ISNULL(MAX(MapId),0) FROM map_GEO_Customers)+ROW_NUMBER() over (order by MapId)MapId, '" + Listed_DR_Code + "', lat, long, addrs, StatFlag, Division_code, imge_name from [map_GEO_UnlistCustomers] " +
                            " where Cust_Code = '" + ListedDrCode + "')";

                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getListedDr_map(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name," +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                     " d.ListedDr_Sl_No,d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName, d.Doc_Class_ShortName as Doc_ClsSName, d.Doc_Qua_Name as Doc_QuaName, " +
                   //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                   // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                   " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where  Territory_Active_Flag=0  and t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name FROM " +
                    "Mas_ListedDr d " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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

        //Doctor type

        public int Update_doctype(string doc_code, string doc_type)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_Type = '" + doc_type + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE ListedDrCode = '" + doc_code + "' and ListedDr_Active_Flag = 0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //Campaign

        public DataSet getListedDr_Camp(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Mobile,d.ListedDr_website as Chemists_Code,d.ListedDr_Sl_No,c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName,dc.Doc_ClsName," +
               " dc.Doc_ClsSName ,g.Doc_QuaName, d.Doc_SubCatCode,d.Doc_Cat_Code, " +
                   //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                   // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                   " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and "+
                  " CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where Division_code='" + div_code + "' and Doc_SubCat_ActiveFlag='0' and charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName   FROM " +
                    "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.Doc_Special_Code = s.Doc_Special_Code " +
                    "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                    " and d.Doc_QuaCode = g.Doc_QuaCode " +
                    "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                    "and d.ListedDr_Active_Flag = 0 and d.Division_code='" + div_code + "' order by Doc_Cat_Sl_No,Doc_Spec_ShortName ";


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
        public int Map_Campaign(string Sub_Cat_code, string doc_code, string div_code, string sf_code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_SubCatCode = '" + Sub_Cat_code + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE ListedDrCode = '" + doc_code + "' and sf_code='" + sf_code + "' and ListedDr_Active_Flag = 0 ";
                //strQry = "EXEC SaveCampaign '" + Sub_Cat_code + "', '" + doc_code + "','" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //Add by Gowri
        //public int Map_Campaign_New(string Sub_Cat_code, string doc_code,string mobile,string chemist_code, string div_code, string sf_code)
        //{
        //    int iReturn = -1;
        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "UPDATE Mas_ListedDr " +
        //                 " SET Doc_SubCatCode = '" + Sub_Cat_code + "',ListedDr_Mobile = '"+ mobile + "',ListedDr_website ='" + chemist_code + "' ," +
        //                 " LastUpdt_Date = getdate() " +
        //                 " WHERE ListedDrCode = '" + doc_code + "' and sf_code='" + sf_code + "' and ListedDr_Active_Flag = 0 ";
        //        //strQry = "EXEC SaveCampaign '" + Sub_Cat_code + "', '" + doc_code + "','" + div_code + "' ";

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return iReturn;

        //}
        public int Map_Campaign_New(string Sub_Cat_code, string doc_code, string mobile, string div_code, string sf_code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_SubCatCode = '" + Sub_Cat_code + "',ListedDr_Mobile = '" + mobile + "' ," +
                         " LastUpdt_Date = getdate() " +
                         " WHERE ListedDrCode = '" + doc_code + "' and sf_code='" + sf_code + "' and ListedDr_Active_Flag = 0 ";
                //strQry = "EXEC SaveCampaign '" + Sub_Cat_code + "', '" + doc_code + "','" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet get_Camp(string ListedDrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select ListedDrCode,ListedDr_Name,Doc_SubCatCode from Mas_ListedDr " +
                     " where ListedDrCode =  '" + ListedDrCode + "' and Division_code = '" + div_code + "' ";


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

        //Campaign Search

        public DataSet getListedDrforSpl_Camp(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Listeddr_Mobile, " +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                     // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 0  ";
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
        public DataSet getListedDrforSpl_Camp_New(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Listeddr_Mobile,d.ListedDr_website as Chemists_Code, " +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                     // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 0 order by Doc_Cat_Sl_No,Doc_Spec_ShortName";
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
        public DataSet getListedDrforCat_Camp(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, " +
                       " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name ,s.Doc_Special_SName,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                     // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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
        public DataSet getListedDrforCat_Camp_New(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Listeddr_Mobile,d.ListedDr_website as Chemists_Code, " +
                       " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name ,s.Doc_Special_SName,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                     // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 0 order by Doc_Cat_Sl_No,Doc_Spec_ShortName";
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
        public DataSet getListedDrforQual_Camp(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name," +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                       " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                      //  " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,  " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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
        public DataSet getListedDrforQual_Camp_New(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Listeddr_Mobile,d.ListedDr_website as Chemists_Code," +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                       " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                      //  " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,  " +
                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 0 order by Doc_Cat_Sl_No,Doc_Spec_ShortName";
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

        public DataSet getListedDrforClass_Camp(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name," +
                      " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                     " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName , " +
                       // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,  " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_QuaCode= g.Doc_QuaCode " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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
        public DataSet getListedDrforClass_Camp_New(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Listeddr_Mobile,d.ListedDr_website as Chemists_Code," +
                      " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                     " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName , " +
                       // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,  " +
                       " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_QuaCode= g.Doc_QuaCode " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 0 order by Doc_Cat_Sl_No,Doc_Spec_ShortName";
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
        public DataSet getListedDrforTerr_Camp(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Listeddr_Mobile, d.Territory_Code, " +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      "c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName, dc.Doc_ClsName, dc.Doc_ClsSName ,g.Doc_QuaName, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0 order by Doc_Cat_Sl_No,Doc_Spec_ShortName";
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
        public DataSet getListedDrforTerr_Camp(string sfcode, string TerrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Listeddr_Mobile, d.Territory_Code,d.ListedDr_website as Chemists_Code, " +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      "c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName, dc.Doc_ClsName, dc.Doc_ClsSName ,g.Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  Division_code='" + div_code + "' and charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
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
        public DataSet getListedDrforName_Camp(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.Listeddr_Mobile,d.ListedDr_website as Chemists_Code,d.Listeddr_Mobile," +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                       " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName,  " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        " and d.ListedDr_Active_Flag = 0";
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
        public DataSet getListedDrforName_Camp_New(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.Listeddr_Mobile,d.ListedDr_website as Chemists_Code," +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                       " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName,  " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        " and d.ListedDr_Active_Flag = 0 order by Doc_Cat_Sl_No,Doc_Spec_ShortName";
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


        public DataSet FetchCampName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;


            strQry = " SELECT Doc_SubCatCode , Doc_SubCatName  " +
                     " FROM  Mas_Doc_SubCategory  where Doc_SubCat_ActiveFlag=0 and Division_code='" + div_code + "'";

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
        public DataSet FetchCamp_Name(string Doc_SubCatCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;


            strQry = " SELECT Doc_SubCatCode , Doc_SubCatName  " +
                     " FROM  Mas_Doc_SubCategory  where Doc_SubCatCode = '" + Doc_SubCatCode + "' and Doc_SubCat_ActiveFlag=0 and Division_code='" + div_code + "'";

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

        public DataSet GetClass_Code(string Doc_ClsName, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Doc_ClsCode,Doc_ClsSName from Mas_Doc_Class where Doc_ClsName='" + Doc_ClsName + "' and Division_Code = '" + div_code + "'";

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
        public DataSet Class_doc(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName " +
                     " FROM  Mas_Doc_Class where division_Code = '" + div_code + "' AND Doc_Cls_ActiveFlag=0 order by Doc_ClsName asc";
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

        //Indhu
        public DataSet Deact_VisitdrCamp(string sf_code, string ListedDrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " select ListedDr_Name from Mas_Doc_SubCategory a,mas_listeddr b where a.division_code='" + div_code + "' and Doc_SubCat_ActiveFlag='0' " +
                     " and  Listeddr_Active_Flag='0' and  b.sf_code='" + sf_code + "' and ListedDrCode='" + ListedDrCode + "' and  " +
                     " charindex(cast(a.Doc_SubCatCode as varchar),b.Doc_SubCatCode )> 0";
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
        public DataSet Deact_VisitdrCore(string sf_code, string ListedDrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " select distinct ListedDr_Name from Core_Doctor_Map a,mas_listeddr b where a.sf_code=b.sf_code and a.Division_Code='" + div_code + "' " +
                     " and a.sf_code='" + sf_code + "' and Listeddr_Active_Flag='0' and a.DR_Code=b.ListedDrCode and a.DR_Code='" + ListedDrCode + "'";
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
        public DataSet Deact_VisitdrCRM(string sf_code, string ListedDrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " select distinct ListedDr_Name from Trans_Doctor_Service_Head a,mas_listeddr b where a.sf_code=b.sf_code and a.Division_Code='" + div_code + "' " +
                     " and a.sf_code='" + sf_code + "' and Listeddr_Active_Flag='0' and a.Doctor_Code=b.ListedDrCode and a.Doctor_Code='" + ListedDrCode + "'";
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
        public DataSet Deact_VisitdrCore_Camp(string sf_code, string ListedDrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " select ListedDr_Name from Mas_Doc_SubCategory a,mas_listeddr b where a.division_code='" + div_code + "' and Doc_SubCat_ActiveFlag='0' " +
                     " and  Listeddr_Active_Flag='0' and  b.sf_code='" + sf_code + "' and cast(ListedDrCode as varchar) in " +
                     " (select distinct ListedDrCode from Core_Doctor_Map cc where cc.sf_code=b.sf_code and cc.DR_Code=b.ListedDrCode " +
                     " and cc.DR_Code='" + ListedDrCode + "') and  charindex(cast(a.Doc_SubCatCode as varchar),b.Doc_SubCatCode )> 0";
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

        //Indhu


        public DataSet Deact_Visitdr(string sf_code, string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            //strQry ="select distinct c.ListedDr_Name,'' ListedDrCode,'' ListedDr_Sl_No, " +
            //         " ''Doc_Cat_Name,'' Doc_Cat_SName,'' Doc_Special_Name,'' Doc_Special_SName,'' Doc_ClsName,''Doc_ClsSName " +
            //         " ,'' Doc_QuaName,b.Activity_Date,'' territory_Name, a.sf_code,a.Trans_Detail_Info_Type,a.Trans_Detail_Info_Code "+
            ////strQry = "select a.sf_code,a.Trans_Detail_Info_Type,a.Trans_Detail_Info_Code, " +
            //        // " c.ListedDr_Name,b.Activity_Date,'' ListedDrCode 
            //         " from DCRDetail_Lst_Trans a, DCRMain_Trans b,Mas_ListedDr c " +
            //         " where  Month(Activity_Date) = Month(getdate()) and YEAR(Activity_Date)=YEAR(GETDATE()) " +
            //         "  and a.Trans_SlNo = b.Trans_SlNo and c.ListedDrCode = a.Trans_Detail_Info_Code and c.sf_code='" + sf_code + "'  and c.ListedDrCode ='" + ListedDrCode + "'";


            strQry = " select distinct convert(varchar(10),b.Activity_Date,103) Activity_Date  from DCRDetail_Lst_Trans a, " +
                     " DCRMain_Trans b  where  Month(b.Activity_Date) = Month(getdate()) " +
                     " and YEAR(b.Activity_Date)=YEAR(GETDATE())   and a.Trans_SlNo = b.Trans_SlNo and " +
                     " a.sf_code='" + sf_code + "'  and a.Trans_Detail_Info_Code ='" + ListedDrCode + "' ";
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

        //Changes done by Priya

        public DataSet getLi_Deactivate(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                    //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                    //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name FROM " +
                    "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.Doc_Special_Code = s.Doc_Special_Code " +
                    "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                    " and d.Doc_QuaCode = g.Doc_QuaCode " +
                    "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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

        public int RecordCount_Transfer(string sf_code, string terr_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDrCode) FROM Mas_ListedDr WHERE Sf_Code = '" + sf_code + "' and Territory_Code ='" + terr_code + "' and ListedDr_Active_Flag = 0 ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //changes done by Priya

        public DataTable getListedDrforTerr_Trans(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Territory_Code, " +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      "c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName, dc.Doc_ClsName, dc.Doc_ClsSName ,g.Doc_QuaName, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') Doc_SubCatName, '' color FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
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
        public DataSet GetTerritory_Upload(string Territory_Name, string SF_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Territory_Code from Mas_Territory_Creation where Territory_Name='" + Territory_Name + "' and SF_Code = '" + SF_Code + "' and Division_Code = '" + div_code + "' and Territory_Active_Flag = 0 ";

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
        //Changes done by Priya
        public int Transfer_Doctor(string Doc_Code, string terr_code, string sf_code, string trans_Code, string from_terr)
        {
            int iReturn = -1;
            int Listed_DR_Code = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                //strQry = "Update Mas_ListedDr " +
                //         " Set Territory_Code ='" + terr_code + "', sf_code = '" + trans_Code + "', Transfer_MR_Listeddr = getdate()" +
                //         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                //iReturn = db.ExecQry(strQry);

                strQry = " Insert into Mas_ListedDr(ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
" ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,Territory_Code, " +
" Doc_QuaCode,ListedDr_Active_Flag,ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
" ListedDr_Sl_No,ListedDr_Special_No,Division_Code,SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name,Transfer_MR_Listeddr,C_Doctor_Code,Visiting_Card) " +
" Select '" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as ListedDrCode,'" + trans_Code + "' as sf_code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
" ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code, " +
" '" + terr_code + "' as Territory_Code, " +
" Doc_QuaCode,'0' as ListedDr_Active_Flag,getdate() as ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
" '" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as ListedDr_Sl_No,ListedDr_Special_No,Division_Code,'" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name, getdate(),C_Doctor_Code,Visiting_Card " +
" from Mas_ListedDr a where  Sf_Code= '" + sf_code + "' and  Territory_Code ='" + from_terr + "' and ListedDr_Active_Flag=0 and ListedDrCode='" + Doc_Code + "'";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_ListedDr set ListedDr_Active_Flag=1,Transfer_MR_Listeddr = getdate(),ListedDr_Deactivate_Date= getdate() where sf_code = '" + sf_code + "' and Territory_Code ='" + from_terr + "' and ListedDrCode='" + Doc_Code + "' and ListedDr_Active_Flag=0";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet doc_dob(string sf_code, string dmonth, string ddate, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "EXEC sp_get_Rep_doctor_dob '" + sf_code + "', '" + dmonth + "', '" + ddate + "', '" + Div_code + "'  ";
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
        public DataSet doc_dow(string sf_code, string dmonth, string ddate, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "EXEC sp_get_Rep_doctor_dow '" + sf_code + "', '" + dmonth + "', '" + ddate + "' , '" + Div_code + "' ";
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
        public DataSet doc_dob_dow(string sf_code, string dmonth, string ddate, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "EXEC sp_get_Rep_dob_dow '" + sf_code + "', '" + dmonth + "', '" + ddate + "', '" + Div_code + "'  ";
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

        public DataSet getterrcode()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "SELECT isnull(max(Territory_Code)+1,'1') Territory_Code from Mas_Territory_Creation";

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
        public DataSet GetDoc_Qua_Code(string Doc_QuaName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Doc_QuaCode from Mas_Doc_Qualification where Doc_QuaName='" + Doc_QuaName + "'";

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
        public DataSet GetQua_Upload(string Doc_QuaName, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Doc_QuaCode,Doc_QuaName from Mas_Doc_Qualification where Doc_QuaName='" + Doc_QuaName + "' and Division_Code = '" + div_code + "' ";

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

        //changes done by Reshmi
        public int RecordAddLDr(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, string DoCatSName, string DoSpecSName, string DocQuaName, string DoClaSName, int iflag)
        {
            int iReturn = -1;

            if (!RecordExist(Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;
                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);


                    strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                             " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                             " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Qua_Name,Doc_Class_ShortName) " +
                             " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                             " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', '" + iflag + "', getdate(), '" + Division_Code + "','','','', " +
                             " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate(),'" + DoCatSName + "','" + DoSpecSName + "','" + DocQuaName + "','" + DoClaSName + "')";


                    iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        //Insert a record into LstDoctor_Terr_Map_History table
                        strQry = "SELECT isnull(max(SNo)+1,'1') SNo from LstDoctor_Terr_Map_History ";
                        int SNo = db.Exec_Scalar(strQry);

                        strQry = "insert into LstDoctor_Terr_Map_History values('" + SNo + "','" + sf_code + "',  '" + Listed_DR_Code + "', " +
                                  " '" + Listed_DR_Terr + "',getdate(),getdate(), '" + Division_Code + "')";

                        iReturn = db.ExecQry(strQry);

                        if (Listed_DR_Terr.IndexOf(",") != -1)
                        {
                            string[] subterr;
                            subterr = Listed_DR_Terr.Split(',');
                            foreach (string st in subterr)
                            {
                                if (st.Trim().Length > 0)
                                {
                                    strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                                    int iPlanNo = db.Exec_Scalar(strQry);

                                    strQry = "insert into Call_Plan values('" + sf_code + "', '" + Convert.ToInt32(st) + "', getdate(), '" + iPlanNo + "', " +
                                            " '" + Listed_DR_Code + "', '" + Division_Code + "', 0,'')";

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
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }

        public DataSet getListedDr_new(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date, " +
                  //  " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name FROM " +
                    "Mas_ListedDr d " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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
        public DataSet getListedDrDiv_new(string div_code, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date, d.Territory_Code,  " +
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    "Mas_ListedDr d " +
                    "WHERE d.Division_Code='" + div_code + "' AND d.Sf_Code =  '" + sfcode + "'" +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Name";

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
        public DataSet getListedDrDiv_new_Inc(string div_code, string sfcode, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT distinct  d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date, d.Territory_Code,  " +
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    "  Mas_ListedDr d inner join Trans_DCR_BusinessEntry_Details s " +
                    " on d.listeddrcode = s.listeddrcode " +
                    " INNER JOIN Trans_DCR_BusinessEntry_Head h on h.Trans_Sl_No = s.Trans_Sl_No " +
                    "WHERE d.Division_Code = '" + div_code + "' AND d.Sf_Code = '" + sfcode + "' and h.Trans_Month = '" + Trans_Month + "' AND h.Trans_Year = '" + Trans_Year + "' " +
                    " order by ListedDr_Name ";
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
        //Changes done by priya

        public DataSet getListedDrdeativate(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,d.Doc_Class_ShortName, d.Doc_Qua_Name,d.SDP as Activity_Date, " +

                  //  " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name FROM  " +
                    "Mas_ListedDr d " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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

        // Sorting For ListedDR List 
        public DataTable getListedDoctorListNew_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,d.SDP as Activity_Date, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +

                        "and d.ListedDr_Active_Flag = 0" +
                        " order by 2";
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

        //Changes done by Priya

        public DataSet getListedDr_RejectList(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " select convert(varchar(10),COUNT(ListedDrCode)) LSTCount,Listeddr_App_Mgr, " +
                     " case Reject_Flag when 'AR' then 'Addition' " +
                     " when 'DR' then 'Deactivation' end  mode  from Mas_ListedDr where Reject_Flag = 'AR' and Sf_Code='" + sfcode + "' " +
                     " and ListedDr_Active_Flag = 4 group by Reject_Flag, Listeddr_App_Mgr " +
                     " union " +
                     " select convert(varchar(10),COUNT(ListedDrCode)) LSTCount,Listeddr_App_Mgr, " +
                     " case Reject_Flag " +
                     " when 'AR' then 'Addition'  " +
                     " when 'DR' then 'Deactivation' end  mode from Mas_ListedDr where Reject_Flag = 'DR' and Sf_Code='" + sfcode + "'  " +
                     " and ListedDr_Active_Flag=0 " +

                     " group by Reject_Flag, Listeddr_App_Mgr ";
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


        //changes done by Reshmi
        public DataSet getListDr_Allow_Admin(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = "Select No_Of_Sl_DoctorsAllowed from Admin_Setups where Division_Code='" + div_code + "'";
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

        public DataSet getListDr_Count(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = strQry = "SELECT COUNT(ListedDrCode) FROM Mas_ListedDr WHERE Sf_Code = '" + sf_code + "' and Division_Code ='" + div_code + "' and (ListedDr_Active_Flag = 0 or ListedDr_Active_Flag = 2)";


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
        //Changes done by Priya
        public DataSet getListedDr_TerritoryName(string TerritoryName, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "select Territory_Code from Mas_Territory_Creation where Territory_Name='" + TerritoryName + "'  and Territory_Active_Flag = 0 and sf_code='" + sf_code + "'";
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

        public DataSet getListDr_allow_app(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = "Select Doc_App_Needed from Admin_Setups where Division_Code='" + div_code + "'";
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

        public DataSet getListedDr_adddeact(string sf_code, int val1, int val2, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name " +
                   " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c  " +
                   " where c.LstDr_AM = '" + sf_code + "' and  a.Sf_Code = b.Sf_Code " +
                   " and b.Sf_Code=c.Sf_Code and  a.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2  and Division_code = '" + div_code + "') and " +
                   " a.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3  and Division_code = '" + div_code + "' ) ";
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

        public DataSet getListedDr_addAgainst(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT  d.ListedDrCode,d.ListedDr_Name, max(d.SLVNo) as SLVNo,d.Doc_Cat_ShortName, d.Doc_Spec_ShortName,d.Doc_Qua_Name, " +
                   //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                   //" t.SF_Code = d.Sf_Code and t.sf_code =  '" + sfcode + "' and " +
                   //" charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                   " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                   " case d.ListedDr_Active_Flag when '2' then 'Addition' when '3' then 'Deactivation' end mode, d.ListedDr_Active_Flag " +
                   " FROM Mas_ListedDr d WHERE d.sf_code =  '" + sfcode + "' and  " +
                   " d.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2 and sf_code =  '" + sfcode + "' ) and " +
                   " d.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 and sf_code =  '" + sfcode + "') and d.ListedDr_Active_Flag !=0 and d.ListedDr_Active_Flag !=1  " +
                   " group by SLVNo,d.ListedDrCode,d.ListedDr_Name,d.Territory_Code, d.Sf_Code,d.ListedDr_Active_Flag, d.Doc_Cat_ShortName, d.Doc_Spec_ShortName,d.Doc_Qua_Name  " +
                   " order by SLVNo, mode ";
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
        public int ApproveAddDeact(string sf_code, string dr_code, int iVal, string sf_name, string slvno)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "' ,listeddr_deactivate_date = NULL" +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and SLVNo = '" + slvno + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int ApproveAddDeact1(string sf_code, string dr_code, int iVal, string sf_name, string slvno)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "',listeddr_deactivate_date = getdate() " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and SLVNo = '" + slvno + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getDoc_Deact_Needed(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = "Select Doc_Deact_Needed from Admin_Setups where Division_Code='" + div_code + "'";
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
        //Changes done by Priya & Reshmi
        public int DeActivate_Dr(string dr_code, int iflag)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag = '" + iflag + "' , " +
                            " listeddr_deactivate_date = getdate() " +
                            " WHERE listeddrcode = '" + dr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getListDr_CountNew(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT COUNT(ListedDrCode) as dr_count FROM Mas_ListedDr WHERE Sf_Code = '" + sf_code + "' and Division_Code ='" + div_code + "' and ListedDr_Active_Flag = 0 ";


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

        public int GetListedDrSlNO()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(Max(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //Changes done by Siva
        public DataSet getDoctorDetailsByName(string div_code, string sfCode, string DocName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_ListedDRByName '" + div_code + "','" + sfCode + "','" + DocName + "'";

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
        //Changes done by Nirmal 
        public DataSet GetDoctorBySearch(string prefixText, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select ListedDr_Name from Mas_ListedDr where Sf_Code = '" + sfCode + "' AND ListedDr_Name like '" + prefixText + "%';";

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
        public DataTable getListedDoctorList_DataTable_camp(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Mobile,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName asDoc_Cat_SName,"+
                "d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Cat_Code,d.Doc_Class_ShortName as Doc_ClsSName,d.Doc_Qua_Name as Doc_QuaName," +
                "d.SDP as Activity_Date, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name , " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  " +
                        " charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'')+', ' Doc_SubCatName  FROM " +

                        " Mas_ListedDr d,Mas_Doctor_Category dc " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " and d.Doc_Cat_Code=dc.Doc_Cat_Code order by Doc_Cat_Sl_No,Doc_Spec_ShortName";
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
        //Changes done by Siva
        public DataSet getListedDoctorBySfCode(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name,ListedDr_Name +'|'+ISNULL(DC.Doc_Cat_Name,'')+'|'+ISNULL(DS.Doc_Special_Name,'')+'|'+ISNULL(MT.Territory_Name,'') AS DoctorDetails " +
                         " FROM Mas_ListedDr  ML" +
                            " LEFT OUTER JOIN Mas_Doctor_Category DC ON DC.Doc_Cat_Code = ML.Doc_Cat_Code" +
                         " LEFT OUTER JOIN Mas_Doctor_Speciality DS ON DS.Doc_Special_Code = ML.Doc_Special_Code" +
                            " LEFT OUTER JOIN (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE SF_Code = '" + sfcode + "') MT " +
                         " ON MT.Territory_Code = ML.Territory_Code WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0";
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
        public int RecordAdd3(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, int SLV_No, string Cat_SName, string Spec_SName, string Cls_SName, string Qual_SName)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Listed_DR_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_QuaCode,Doc_Cat_Code, " +
                         " Territory_Code, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', '" + Listed_DR_Qual + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', 2, getdate(), '" + Division_Code + "','','','', " +
                         " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + SLV_No + "',getdate(),'" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qual_SName + "')";

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
        public DataSet getListedDr_Product(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No, d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Class_ShortName as Doc_ClsSName , d.Doc_Qua_Name as Doc_QuaName, d.Doc_SubCatCode, d.Product_Detail_Code as Product_Code_SlNo, " +
                //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                 //   " stuff((select ', '+Product_Detail_Name from Mas_Product_Detail dc where  charindex(cast(dc.Product_Code_SlNo as varchar)+',',d.Product_Detail_Code+',')>0 for XML path('')),1,2,'') +', ' Product_Detail_Name   FROM " +
                 " isnull((select ProductName+' ( '+CAST(Product_Priority as varchar)+' ), ' from " +
                 " vw_Doc_Prod where d.ListedDrCode=Listeddr_Code for xml path('')),'')  Product_Detail_Name from " +
                 "Mas_ListedDr d " +
                 "WHERE d.Sf_Code =  '" + sfcode + "'" +
                 "and d.ListedDr_Active_Flag = 0";


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
        public DataSet get_Prod(string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select Listeddr_Code,Doctor_Name,Product_Code as Product_Code_SlNo,Product_Priority from Map_LstDrs_Product " +
                     " where Listeddr_Code =  '" + ListedDrCode + "' ";


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
        public int RecordAdd_ProductMap(string Listeddr_Code, string Product_Code, string Product_Priority, string Product_Name, string Doctor_Name, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                int Sl_No = -1;


                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_LstDrs_Product ";
                Sl_No = db.Exec_Scalar(strQry);



                if (DocProd_RecordExist(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "update Map_LstDrs_Product set Product_Code ='" + Product_Code + "',Product_Name='" + Product_Name + "', Product_Priority = '" + Product_Priority + "'  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";
                }
                else
                {

                    strQry = " insert into Map_LstDrs_Product (Sl_No,Listeddr_Code,Product_Code,Product_Priority,Product_Name,Doctor_Name, " +
                       " Sf_Code,Division_Code,Created_Date) " +
                       " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + Product_Code + "', '" + Product_Priority + "', '" + Product_Name + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                       " '" + Division_Code + "',  getdate() )";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool DocProd_RecordExist(string Listeddr_Code, string Sf_Code, string Product_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Map_LstDrs_Product " +
                         " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";

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
        public int Delete_ProductMap(string Listeddr_Code, string Product_Code, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                if (DocProd_RecordExist(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "delete from Map_LstDrs_Product  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getListedDrdeativate_MR(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "EXEC sp_Listeddr_Deact '" + sfcode + "' ";

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

        public DataSet getListedDr_for_Mapp(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            //strQry = " SELECT 0 as ListedDrCode,'---Select---' as ListedDr_Name " +
            //           " UNION " +
            //        " select ListedDrCode, d.ListedDr_Name + ' - '  + t.Territory_Name from Mas_ListedDr d,Mas_Territory_Creation t " +
            //        " where d.Territory_Code=CONVERT(varchar,t.Territory_Code) and d.Division_Code ='" + div_code + "' and d.Sf_Code='" + sfcode + "' " +
            //        " and ListedDr_Active_Flag =0 ";

            strQry = " SELECT 0 as ListedDrCode,'---Select---' as ListedDr_Name " +
                     " UNION " +
                  " select ListedDrCode, d.ListedDr_Name + ' - '  + stuff((select '~ '+territory_Name from Mas_Territory_Creation t where " +
                  " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') from Mas_ListedDr d,Mas_Territory_Creation t " +
                  " where d.Division_Code ='" + div_code + "' and d.Sf_Code='" + sfcode + "' " +
                  " and ListedDr_Active_Flag =0 ";



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

        public int RecordAdd_ProductMap_New(string Listeddr_Code, string Product_Code, string Doctor_Name, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;
            DataSet dsPrd_Name;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Sl_No = -1;
                //string Product_Name = string.Empty;

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_LstDrs_Product ";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "select Product_Detail_Name  from Mas_Product_Detail where Division_Code='" + Division_Code + "' and Product_Code_SlNo='" + Product_Code + "' ";
                dsPrd_Name = db.Exec_DataSet(strQry);

                if (DocProd_RecordExist_New(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "update Map_LstDrs_Product set Product_Code ='" + Product_Code + "',Product_Name='" + dsPrd_Name.Tables[0].Rows[0][0].ToString() + "'  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' and Product_Priority=1 ";
                }
                else
                {

                    strQry = " insert into Map_LstDrs_Product (Sl_No,Listeddr_Code,Product_Code,Product_Name,Doctor_Name, " +
                       " Sf_Code,Division_Code,Created_Date,Product_Priority) " +
                       " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + Product_Code + "', '" + dsPrd_Name.Tables[0].Rows[0][0].ToString() + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                       " '" + Division_Code + "',  getdate(),'1' )";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool DocProd_RecordExist_New(string Listeddr_Code, string Sf_Code, string Product_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Map_LstDrs_Product " +
                         " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";

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

        public DataSet getListedDrCount_MR(string div_code, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select  distinct sf_code, " +
                                 " (select count(ListedDrCode) from mas_listeddr a where ListedDr_Active_Flag=0  and Sf_Code='" + sfcode + "') as doccnt, " +
                                 " (select count(ListedDrCode) from mas_listeddr a where ListedDr_Active_Flag=2 and Sf_Code='" + sfcode + "' ) as docapp, " +
                                 " (select count(ListedDrCode) from mas_listeddr a where ListedDr_Active_Flag=3 and Sf_Code='" + sfcode + "') as deaapp, " +
                                 " (select count(ListedDrCode) from mas_listeddr a where  Sf_Code='" + sfcode + "'  and " +
                                 " a.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2 ) and  " +
                                 " a.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3  ) )  as addagain " +
                                 " from Mas_ListedDr d where d.Division_code = '" + div_code + "' and d.Sf_Code='" + sfcode + "' ";

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
        /*--------------Listed Doctor AutoFill Data--------------- */

        public DataSet GetListedDoctorAutoFill(string prefixText, string Div_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select ListedDr_Name from Mas_ListedDr where ListedDr_Name like '" + prefixText + "%' and Division_Code='" + Div_code + "' and Sf_Code='" + Sf_Code + "'";

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
        public DataTable getListedDoctorListNew_DT(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = " EXEC sp_Listeddr_Deact '" + sfcode + "' ";
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
        public int Interchange_MR(string from_sfcode, string to_sfcode, string from_name, string to_name, string div_code)
        {
            int iReturn = -1;

            try
            {
                int Listed_DR_Code = -1;
                int Listed_DR_Code1 = -1;
                int chemists_cd = -1;
                int chemists_cd1 = -1;
                int terr_New = -1;
                int Hospital_Cd = -1;
                int Hospital_Cd1 = -1;
                int UnListedDrCd = -1;

                int UnListedDrCd1 = -1;
                int terr_New1 = -1;
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation ";
                terr_New = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                chemists_cd = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(Hospital_Code),0)+1 FROM Mas_Hospital ";
                Hospital_Cd = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr ";
                UnListedDrCd = db.Exec_Scalar(strQry);



                strQry = "update Mas_Territory_Creation set Territory_Active_Flag=6 where (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "') and Territory_Active_Flag=0";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                            " SF_Code,Territory_Active_Flag,Created_date,HQ_Code, Territory_Intechange) " +
                                            " Select '" + terr_New + "' - 1 + row_number() over (order by (select NULL)) as Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                            " '" + to_sfcode + "' as SF_Code,'0' as Territory_Active_Flag,getdate() as Created_date,HQ_Code, 'TT' as Territory_Intechange from Mas_Territory_Creation where Sf_Code= '" + from_sfcode + "'  and Territory_Active_Flag=6 ";

                iReturn = db.ExecQry(strQry);


                strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation ";
                terr_New1 = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                           " SF_Code,Territory_Active_Flag,Created_date,HQ_Code, Territory_Intechange) " +
                                           " Select '" + terr_New1 + "' - 1 + row_number() over (order by (select NULL)) as Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                           " '" + from_sfcode + "' as SF_Code, '0' as Territory_Active_Flag,getdate() as Created_date,HQ_Code, 'TT' as Territory_Intechange from Mas_Territory_Creation where Sf_Code= '" + to_sfcode + "'   and Territory_Active_Flag=6 ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Territory_Creation set Territory_Active_Flag=1 where  (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "')  and Territory_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                //strQry = "update mas_Distance_Fixation_001 set Flag=6 where (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "') and Flag=0";

                //iReturn = db.ExecQry(strQry);

                //strQry = "insert into mas_Distance_Fixation_001 (SF_Code,From_Code,To_Code,Town_Cat,Distance_In_Kms,Amount,division_code, " +
                //    " Flag,Created_Date,Territory_Name,approve_flg,level1_flg,level2_flg) " +
                //    " select '" + to_sfcode + "' as SF_Code,'" + to_sfcode + "' as From_Code," +
                //    " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                //    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                //    " Territory_Code= a.To_Code)) as To_Code, " +
                //    " Town_Cat,Distance_In_Kms,Amount,division_code, " +
                //    " '0' as Flag,getdate() as Created_Date," +
                //    " (select Territory_Name from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                //    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                //    " Territory_Code= a.To_Code)) as Territory_Name " +
                //    " ,approve_flg,level1_flg,level2_flg from mas_Distance_Fixation_001 a where Sf_Code= '" + from_sfcode + "' and Flag=6 ";

                //iReturn = db.ExecQry(strQry);

                //strQry = "insert into mas_Distance_Fixation_001 (SF_Code,From_Code,To_Code,Town_Cat,Distance_In_Kms,Amount,division_code, " +
                // " Flag,Created_Date,Territory_Name,approve_flg,level1_flg,level2_flg) " +
                // " select '" + from_sfcode + "' as SF_Code,'" + from_sfcode + "' as From_Code," +
                // " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                // " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                // " Territory_Code= a.To_Code)) as To_Code, " +
                // " Town_Cat,Distance_In_Kms,Amount,division_code, " +
                // " '0' as Flag,getdate() as Created_Date," +
                //  " (select Territory_Name from Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                //    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                //    " Territory_Code= a.To_Code)) as Territory_Name,approve_flg,level1_flg,level2_flg " +
                //  " from mas_Distance_Fixation_001 a where Sf_Code= '" + to_sfcode + "' and Flag=6 ";

                //iReturn = db.ExecQry(strQry);

                //strQry = "update mas_Distance_Fixation_001 set Flag=0 where (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "') and Flag=6";

                //iReturn = db.ExecQry(strQry);


                strQry = "update Mas_ListedDr set ListedDr_Active_Flag=6 where (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "')  and ListedDr_Active_Flag=0";

                iReturn = db.ExecQry(strQry);


                strQry = " Insert into Mas_ListedDr(ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
             " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,Territory_Code, " +
             " Doc_QuaCode,ListedDr_Active_Flag,ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
             " ListedDr_Sl_No,ListedDr_Special_No,Division_Code,SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name,Interchange_Drs) " +
             " Select '" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as ListedDrCode,'" + to_sfcode + "' as sf_code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
             " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code, " +
             //" select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
             //" Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and Territory_Code= a.territory_code) as territory_code " +
             " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "'   and Territory_Active_Flag=0 and " +
             " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
             " Territory_Code= a.territory_code and Territory_Active_Flag=1)) as Territory_Code, " +

             " Doc_QuaCode,'0' as ListedDr_Active_Flag,getdate() as ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
             " '" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as ListedDr_Sl_No,ListedDr_Special_No,Division_Code,'" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name, 'DI' Interchange_Drs " +
             " from Mas_ListedDr a where  Sf_Code= '" + from_sfcode + "' and ListedDr_Active_Flag=6 ";


                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code1 = db.Exec_Scalar(strQry);

                strQry = " Insert into Mas_ListedDr(ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
             " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,Territory_Code, " +
             " Doc_QuaCode,ListedDr_Active_Flag,ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
             " ListedDr_Sl_No,ListedDr_Special_No,Division_Code,SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name,Interchange_Drs) " +
             " Select '" + Listed_DR_Code1 + "' - 1 + row_number() over (order by (select NULL)) as ListedDrCode,'" + from_sfcode + "' as sf_code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
             " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code, " +
             " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "'  and Territory_Active_Flag=0 and " +
             " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
             " Territory_Code= a.territory_code and Territory_Active_Flag=1)) as Territory_Code, " +
             " Doc_QuaCode, '0' as ListedDr_Active_Flag,getdate() as ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
             " '" + Listed_DR_Code1 + "' - 1 + row_number() over (order by (select NULL)) as ListedDr_Sl_No,ListedDr_Special_No,Division_Code,'" + Listed_DR_Code1 + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name, 'DI' Interchange_Drs " +
             " from Mas_ListedDr a where  Sf_Code= '" + to_sfcode + "' and ListedDr_Active_Flag=6 ";


                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_ListedDr set ListedDr_Active_Flag=1,ListedDr_Deactivate_Date= getdate() where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "')   and ListedDr_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Chemists set Chemists_Active_Flag=6 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and Chemists_Active_Flag=0";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
              " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code,Interchang_Chem) " +
              " Select '" + chemists_cd + "' - 1 + row_number() over (order by (select NULL)) as Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact,  " +
              " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "'  and Territory_Active_Flag=0 and " +
              " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
              " Territory_Code= a.territory_code and Territory_Active_Flag=1)) as Territory_Code, " +
              " Division_Code, '" + to_sfcode + "' as Sf_Code, '0' as Chemists_Active_Flag, getdate() as Created_Date,Cat_Code, 'IC' Interchang_Chem " +
               " from Mas_Chemists a  where  Sf_Code= '" + from_sfcode + "'  and Chemists_Active_Flag=6 ";

                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                chemists_cd1 = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
              " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code,Interchang_Chem) " +
              " Select '" + chemists_cd1 + "' - 1 + row_number() over (order by (select NULL)) as Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact,  " +
              " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "'  and Territory_Active_Flag=0 and " +
              " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
              " Territory_Code= a.territory_code and Territory_Active_Flag=1)) as Territory_Code, " +
              " Division_Code, '" + from_sfcode + "' as Sf_Code, '0' as Chemists_Active_Flag, getdate() as Created_Date,Cat_Code, 'IC' Interchang_Chem " +
               " from Mas_Chemists a  where  Sf_Code= '" + to_sfcode + "'  and Chemists_Active_Flag=6 ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Chemists set Chemists_Active_Flag=1 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and Chemists_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Hospital set Hospital_Active_Flag=6 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and Hospital_Active_Flag=0";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into Mas_Hospital (Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact, Territory_Code, " +
                    " Division_Code, Sf_Code, Hospital_Active_Flag, Created_Date) " +
                    " Select '" + Hospital_Cd + "' - 1 + row_number() over (order by (select NULL)) as Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact,  " +
                    " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "'  and Territory_Active_Flag=0 and " +
                    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                    " Territory_Code= a.territory_code and Territory_Active_Flag=1)) as Territory_Code, " +
                    " Division_Code, '" + to_sfcode + "' as Sf_Code, '0' as Hospital_Active_Flag, getdate() as Created_Date from Mas_Hospital a where  Sf_Code= '" + from_sfcode + "' and Hospital_Active_Flag=6 ";


                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(Hospital_Code),0)+1 FROM Mas_Hospital ";
                Hospital_Cd1 = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Hospital (Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact, Territory_Code, " +
                           " Division_Code, Sf_Code, Hospital_Active_Flag, Created_Date) " +
                           " Select '" + Hospital_Cd1 + "' - 1 + row_number() over (order by (select NULL)) as Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact,  " +
                           " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "'  and Territory_Active_Flag=0 and " +
                           " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                           " Territory_Code= a.territory_code and Territory_Active_Flag=1)) as Territory_Code, " +
                           " Division_Code, '" + from_sfcode + "' as Sf_Code, '0' as Hospital_Active_Flag, getdate() as Created_Date from Mas_Hospital a where  Sf_Code= '" + to_sfcode + "' and Hospital_Active_Flag=6   ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Hospital set Hospital_Active_Flag=1 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and Hospital_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_UnListedDr set UnListedDr_Active_Flag=6 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and UnListedDr_Active_Flag=0";

                iReturn = db.ExecQry(strQry);



                strQry = "insert into Mas_UnListedDr (UnListedDrCode,SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                              " Territory_Code,Doc_QuaCode, UnListedDr_Active_Flag, UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                              " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,UnListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                              " select '" + UnListedDrCd + "' - 1 + row_number() over (order by (select NULL)) as UnListedDrCode,'" + to_sfcode + "' as SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                              " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "'  and Territory_Active_Flag=0 and " +
                              " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                              " Territory_Code= a.territory_code and Territory_Active_Flag=1)) as Territory_Code, " +
                              " Doc_QuaCode, '0' as UnListedDr_Active_Flag, getdate() as UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                              " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,'" + UnListedDrCd + "' - 1 + row_number() over (order by (select NULL)) as UnListedDr_Sl_No,Doc_ClsCode,'" + UnListedDrCd + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,LastUpdt_Date " +
                              " from Mas_UnListedDr a where  Sf_Code= '" + from_sfcode + "' and  UnListedDr_Active_Flag =6";

                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr ";
                UnListedDrCd1 = db.Exec_Scalar(strQry);


                strQry = "insert into Mas_UnListedDr (UnListedDrCode,SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                                    " Territory_Code,Doc_QuaCode, UnListedDr_Active_Flag, UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                                    " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,UnListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                                    " select '" + UnListedDrCd1 + "' - 1 + row_number() over (order by (select NULL)) as UnListedDrCode,'" + from_sfcode + "' as SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                                    " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "'  and Territory_Active_Flag=0 and " +
                                    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                                    " Territory_Code= a.territory_code and Territory_Active_Flag=1)) as Territory_Code, " +
                                    " Doc_QuaCode, '0' as UnListedDr_Active_Flag, getdate() as UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                                    " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,'" + UnListedDrCd1 + "' - 1 + row_number() over (order by (select NULL)) as UnListedDr_Sl_No,Doc_ClsCode,'" + UnListedDrCd1 + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,LastUpdt_Date " +
                                    " from Mas_UnListedDr a where  Sf_Code= '" + to_sfcode + "' and  UnListedDr_Active_Flag =6";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_UnListedDr set UnListedDr_Active_Flag=1 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and UnListedDr_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into Mas_InterChange_Det (From_Sf_code, To_Sf_code, From_HQ, To_HQ, From_Name, To_Name, Division_code, Created_date)" +
                         " VALUES('" + from_sfcode + "', '" + to_sfcode + "', '', '','" + from_name + "','" + to_name + "','" + div_code + "',getdate() ) ";

                iReturn = db.ExecQry(strQry);


                strQry = "insert into Mas_InterChange_Det (From_Sf_code, To_Sf_code, From_HQ, To_HQ, From_Name, To_Name, Division_code, Created_date)" +
                         " VALUES('" + to_sfcode + "', '" + from_sfcode + "', '', '','" + to_name + "','" + from_name + "','" + div_code + "',getdate() ) ";

                iReturn = db.ExecQry(strQry);

                strQry = " select sf_hq INTO #HQ_Table1 from Mas_Salesforce where Sf_Code='" + from_sfcode + "' " +
                         " select sf_hq INTO #HQ_Table2 from Mas_Salesforce where Sf_Code='" + to_sfcode + "' " +
                         " update Mas_Salesforce set sf_hq=(select * from #HQ_Table2) where Sf_Code='" + from_sfcode + "' " +
                         " update Mas_Salesforce set sf_hq=(select * from #HQ_Table1) where Sf_Code='" + to_sfcode + "'" +
                         " drop table #HQ_Table1 " +
                         " drop table #HQ_Table2 ";

                iReturn = db.ExecQry(strQry);




            }


            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getListedDr_new_Cnt(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date, " +
                  //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name FROM " +
                  "Mas_ListedDr d " +
                  "WHERE d.Sf_Code =  '" + sfcode + "' and Division_code='" + div_code + "'" +
                  "and d.ListedDr_Active_Flag = 0" +
                  " order by ListedDr_Name";


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

        public int GetQua_code()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Doc_QuaCode)+1,'1') Doc_QuaCode from Mas_Doc_Qualification ";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //missed Filter

        public DataSet getMiss_spl(string sfcode, string SplCode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                     " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                     " a.Doc_Qua_Name as Doc_QuaName,  " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                     //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                     //" territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     "  FROM Mas_ListedDr a  " +
                     " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Doc_Special_Code='" + SplCode + "' and " +
                     " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                     " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                     "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                     "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                     "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                     "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                     " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";





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

        public DataSet getmiss_Cat(string sfcode, string CatCode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                              " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                              " a.Doc_Qua_Name as Doc_QuaName,  " +
                              //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                              //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                              //" territory_Name " +
                              " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                              " FROM Mas_ListedDr a  " +
                              " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Doc_Cat_Code='" + CatCode + "' and " +
                              " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                              " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                              "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                              "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                              "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                              "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                              " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";

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

        public DataSet getmiss_Qual(string sfcode, string Quacode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                             " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                             " a.Doc_Qua_Name as Doc_QuaName,  " +
                             //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                             //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                             //" territory_Name " +
                             " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                             " FROM Mas_ListedDr a  " +
                             " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Doc_QuaCode='" + Quacode + "' and " +
                             " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                             " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                             "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                             "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                             "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                             "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                             " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";
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

        public DataSet getmiss_Class(string sfcode, string Clscode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                             " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                             " a.Doc_Qua_Name as Doc_QuaName,  " +
                             //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                             //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                             //" territory_Name " +
                             " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                             " FROM Mas_ListedDr a  " +
                             " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Doc_ClsCode='" + Clscode + "' and " +
                             " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                             " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                             "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                             "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                             "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                             "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                             " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";
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

        public DataSet getmiss_Terr(string sfcode, string Terrcode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                           " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                           " a.Doc_Qua_Name as Doc_QuaName,  " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                           //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                           //" territory_Name " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                           " FROM Mas_ListedDr a  " +
                           " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Territory_Code='" + Terrcode + "' and " +
                           " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                           " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                           "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                           "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                           "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                           "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                           " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";
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
        // Sorting

        public DataTable getMiss_spl_sort(string sfcode, string SplCode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                     " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                     " a.Doc_Qua_Name as Doc_QuaName,  " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                     //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                     //" territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     "  FROM Mas_ListedDr a  " +
                     " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Doc_Special_Code='" + SplCode + "' and " +
                     " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                     " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                     "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                     "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                     "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                     "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                     " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";





            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataTable getmiss_Cat_sort(string sfcode, string CatCode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;
            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                              " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                              " a.Doc_Qua_Name as Doc_QuaName,  " +
                              //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                              //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                              //" territory_Name " +
                              " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                              " FROM Mas_ListedDr a  " +
                              " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Doc_Cat_Code='" + CatCode + "' and " +
                              " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                              " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                              "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                              "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                              "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                              "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                              " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";

            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataTable getmiss_Qual_sort(string sfcode, string Quacode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                             " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                             " a.Doc_Qua_Name as Doc_QuaName,  " +
                             //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                             //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                             //" territory_Name " +
                             " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                             " FROM Mas_ListedDr a  " +
                             " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Doc_QuaCode='" + Quacode + "' and " +
                             " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                             " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                             "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                             "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                             "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                             "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                             " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";
            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataTable getmiss_Class_sort(string sfcode, string Clscode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;


            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                             " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                             " a.Doc_Qua_Name as Doc_QuaName,  " +
                             //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                             //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                             //" territory_Name " +
                             " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                             " FROM Mas_ListedDr a  " +
                             " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Doc_ClsCode='" + Clscode + "' and " +
                             " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                             " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                             "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                             "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                             "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                             "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                             " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";
            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataTable getmiss_Terr_sort(string sfcode, string Terrcode, string div_code, int cmon, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            strQry = " select a.ListedDrCode, a.ListedDr_Name,  a.Doc_Cat_ShortName as Doc_Cat_SName , " +
                           " a.Doc_Spec_ShortName as Doc_Special_SName, a.Doc_Class_ShortName as Doc_ClsSName , " +
                           " a.Doc_Qua_Name as Doc_QuaName,  " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code " +
                           //" and charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') " +
                           //" territory_Name " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = a.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),a.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                           "  FROM Mas_ListedDr a  " +
                           " where a.Division_Code ='" + div_code + "' and a.sf_code in ('" + sfcode + "') and  a.Territory_Code='" + Terrcode + "' and " +
                           " ((CONVERT(Date, a.ListedDr_Created_Date) < " +
                           " CONVERT(VARCHAR(50), '" + dtcurrent + "' , 126) " +
                           "  ) And (CONVERT(Date, a.ListedDr_Deactivate_Date) >= " +
                           "  CONVERT(VARCHAR(50),  '" + dtcurrent + "' , 126)  or a.ListedDr_Deactivate_Date is null)) and a.ListedDrCode not in " +
                           "   (select distinct c.Trans_Detail_Info_Code from DCRMain_Trans b,DCRDetail_Lst_Trans c where   " +
                           "  b.Trans_SlNo = c.Trans_SlNo   and c.Trans_Detail_Info_Type=1 " +
                           " and month(b.Activity_Date)='" + cmon + "' and YEAR(b.Activity_Date)= '" + cyear + "'   and c.sf_code in ('" + sfcode + "'))";
            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataSet getDoctorDetailsBysfName(string div_code, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "Select ListedDr_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,ListedDrCode,DC.Doc_Cat_Name,DS.Doc_Special_Name,MT.Territory_Code,MT.Territory_Name FROM Mas_ListedDr ML LEFT OUTER JOIN Mas_Doctor_Category DC ON DC.Doc_Cat_Code = ML.Doc_Cat_Code " +
                     "LEFT OUTER JOIN Mas_Doctor_Speciality DS ON DS.Doc_Special_Code = ML.Doc_Special_Code LEFT OUTER JOIN (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE Division_Code ='" + div_code + "' and SF_Code = '" + sfCode + "') MT ON convert(int,MT.Territory_Code) = ML.Territory_Code WHERE ML.Division_Code = '" + div_code + "' AND ML.Sf_Code ='" + sfCode + "' order by ListedDr_Name asc";

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
        public DataSet getDoctorDetailsBycatogory(string div_code, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "Select ListedDr_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,ListedDrCode,DC.Doc_Cat_Name,DS.Doc_Special_Name,MT.Territory_Name FROM Mas_ListedDr ML LEFT OUTER JOIN Mas_Doctor_Category DC ON DC.Doc_Cat_Code = ML.Doc_Cat_Code " +
"LEFT OUTER JOIN Mas_Doctor_Speciality DS ON DS.Doc_Special_Code = ML.Doc_Special_Code LEFT OUTER JOIN (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE Division_Code ='" + div_code + "' and SF_Code = '" + sfCode + "') MT ON MT.Territory_Code = ML.Territory_Code WHERE ML.Division_Code = '" + div_code + "' AND ML.Sf_Code ='" + sfCode + "' order by Doc_Cat_ShortName asc";

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

        public DataSet getDoctorDetailsBydoctorname(string div_code, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "Select ListedDr_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,ListedDrCode,DC.Doc_Cat_Name,DS.Doc_Special_Name,MT.Territory_Name FROM Mas_ListedDr ML LEFT OUTER JOIN Mas_Doctor_Category DC ON DC.Doc_Cat_Code = ML.Doc_Cat_Code " +
"LEFT OUTER JOIN Mas_Doctor_Speciality DS ON DS.Doc_Special_Code = ML.Doc_Special_Code LEFT OUTER JOIN (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE Division_Code ='" + div_code + "' and SF_Code = '" + sfCode + "') MT ON MT.Territory_Code = ML.Territory_Code WHERE ML.Division_Code = '" + div_code + "' AND ML.Sf_Code ='" + sfCode + "' order by ListedDr_Name asc";

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

        public DataSet getDoctorDetailsByspeciality(string div_code, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "Select ListedDr_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,ListedDrCode,DC.Doc_Cat_Name,DS.Doc_Special_Name,MT.Territory_Name FROM Mas_ListedDr ML LEFT OUTER JOIN Mas_Doctor_Category DC ON DC.Doc_Cat_Code = ML.Doc_Cat_Code " +
"LEFT OUTER JOIN Mas_Doctor_Speciality DS ON DS.Doc_Special_Code = ML.Doc_Special_Code LEFT OUTER JOIN (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE Division_Code ='" + div_code + "' and SF_Code = '" + sfCode + "') MT ON MT.Territory_Code = ML.Territory_Code WHERE ML.Division_Code = '" + div_code + "' AND ML.Sf_Code ='" + sfCode + "' order by Doc_Spec_ShortName asc";

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

        public DataSet getDoctorDetailsBysubarea(string div_code, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "Select ListedDr_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,ListedDrCode,DC.Doc_Cat_Name,DS.Doc_Special_Name,MT.Territory_Name FROM Mas_ListedDr ML LEFT OUTER JOIN Mas_Doctor_Category DC ON DC.Doc_Cat_Code = ML.Doc_Cat_Code " +
"LEFT OUTER JOIN Mas_Doctor_Speciality DS ON DS.Doc_Special_Code = ML.Doc_Special_Code LEFT OUTER JOIN (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE Division_Code ='" + div_code + "' and SF_Code = '" + sfCode + "') MT ON MT.Territory_Code = ML.Territory_Code WHERE ML.Division_Code = '" + div_code + "' AND ML.Sf_Code ='" + sfCode + "' order by Territory_Name asc";

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

        public DataSet getDoctorDetailsBysvlno(string div_code, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "Select ListedDr_Name,Doc_Spec_ShortName,Doc_Cat_ShortName,ListedDrCode,DC.Doc_Cat_Name,DS.Doc_Special_Name,MT.Territory_Name FROM Mas_ListedDr ML LEFT OUTER JOIN Mas_Doctor_Category DC ON DC.Doc_Cat_Code = ML.Doc_Cat_Code " +
"LEFT OUTER JOIN Mas_Doctor_Speciality DS ON DS.Doc_Special_Code = ML.Doc_Special_Code LEFT OUTER JOIN (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE Division_Code ='" + div_code + "' and SF_Code = '" + sfCode + "') MT ON MT.Territory_Code = ML.Territory_Code WHERE ML.Division_Code = '" + div_code + "' AND ML.Sf_Code ='" + sfCode + "' order by SLVNo";

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
        public int DeActivate_Dr_Div(string dr_code, int iflag, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag = '" + iflag + "' , " +
                            " listeddr_deactivate_date = getdate() " +
                            " WHERE listeddrcode = '" + dr_code + "' and Division_code='" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet FetchClass_Select(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName " +
                     " FROM  Mas_Doc_Class where division_Code = '" + div_code + "' AND doc_cls_activeflag=0 " +
                     " order by Doc_ClsSNo";
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

        public DataSet FetchQualification_Select(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT Doc_QuaCode,Doc_QuaName " +
                     " FROM  Mas_Doc_Qualification where division_Code = '" + div_code + "' AND doc_qua_activeflag=0 " +
                     " order by DocQuaSNo";
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
        //cat
        public DataSet Cat_OneVisit(string sf_code, string div_code, int iMonth, int iYear, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Cat_Code='" + cat_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 1 ";
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
        public DataSet Cat_TwoVisit(string sf_code, string div_code, int iMonth, int iYear, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Cat_Code='" + cat_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 2 ";
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
        public DataSet Cat_ThreeVisit(string sf_code, string div_code, int iMonth, int iYear, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Cat_Code='" + cat_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 3";
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
        public DataSet Cat_MoreVisit(string sf_code, string div_code, int iMonth, int iYear, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Cat_Code='" + cat_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) > 3";
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

        //Spec 
        public DataSet Spec_OneVisit(string sf_code, string div_code, int iMonth, int iYear, string spec_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Special_Code='" + spec_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 1 ";
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
        public DataSet Spec_TwoVisit(string sf_code, string div_code, int iMonth, int iYear, string spec_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Special_Code='" + spec_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 2 ";
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
        public DataSet Spec_ThreeVisit(string sf_code, string div_code, int iMonth, int iYear, string spec_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Special_Code='" + spec_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 3";
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
        public DataSet Spec_MoreVisit(string sf_code, string div_code, int iMonth, int iYear, string spec_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Special_Code='" + spec_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) > 3";
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

        //Class 
        public DataSet Cls_OneVisit(string sf_code, string div_code, int iMonth, int iYear, string cls_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_ClsCode='" + cls_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 1 ";
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
        public DataSet Cls_TwoVisit(string sf_code, string div_code, int iMonth, int iYear, string cls_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_ClsCode='" + cls_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 2 ";
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
        public DataSet Cls_ThreeVisit(string sf_code, string div_code, int iMonth, int iYear, string cls_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_ClsCode='" + cls_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 3";
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
        public DataSet Cls_MoreVisit(string sf_code, string div_code, int iMonth, int iYear, string cls_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_ClsCode='" + cls_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) > 3";
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

        //Qual 
        public DataSet Qua_OneVisit(string sf_code, string div_code, int iMonth, int iYear, string Qua_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_QuaCode='" + Qua_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 1 ";
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
        public DataSet Qua_TwoVisit(string sf_code, string div_code, int iMonth, int iYear, string Qua_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_QuaCode='" + Qua_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 2 ";
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
        public DataSet Qua_ThreeVisit(string sf_code, string div_code, int iMonth, int iYear, string qua_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_QuaCode='" + qua_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 3";
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
        public DataSet Qua_MoreVisit(string sf_code, string div_code, int iMonth, int iYear, string qua_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_QuaCode='" + qua_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) > 3";
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

        //Territory 
        public DataSet Terr_OneVisit(string sf_code, string div_code, int iMonth, int iYear, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Territory_Code='" + Terr_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 1 ";
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
        public DataSet Terr_TwoVisit(string sf_code, string div_code, int iMonth, int iYear, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Territory_Code='" + Terr_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 2 ";
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
        public DataSet Terr_ThreeVisit(string sf_code, string div_code, int iMonth, int iYear, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Territory_Code='" + Terr_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 3";
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
        public DataSet Terr_MoreVisit(string sf_code, string div_code, int iMonth, int iYear, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Territory_Code='" + Terr_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) > 3";
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
        // Multiplan

        public DataSet getListedDr_Multi_Plan(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName , " +
                     " d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date, " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code  and  " +
                     " CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0  for XML path('')),1,2,'' )  " +
                     " territory_Name  FROM Mas_ListedDr d WHERE d.Sf_Code =  '" + sfcode + "' and d.ListedDr_Active_Flag = 0 " +
                     " order by ListedDr_Name";

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

        public DataTable getListedDr_Multi_Plan_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = " SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName , " +
                           " d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date, " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code  and  " +
                           " CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0  for XML path('')),1,2,'' )  " +
                           " territory_Name  FROM Mas_ListedDr d WHERE d.Sf_Code =  '" + sfcode + "' and d.ListedDr_Active_Flag = 0 " +
                           " order by ListedDr_Name";
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
        public DataSet Get_Terr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and " +
                     " CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name from mas_listeddr d where sf_code='" + sfcode + "' and territory_code like '%" + terr_code + "%' and ListedDr_Active_Flag=0 ";

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

        public int DocProd_RecordUpdate(string Listeddr_Code, string Product_Code, string Sf_Code, string Division_Code)
        {
            int iReturn1 = -1;


            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_ListedDr set Map_ListedDr_Products ='" + Product_Code + "' " +
                         " where ListedDrCode = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Division_Code ='" + Division_Code + "'  ";

                iReturn1 = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn1;
        }
        public int RecordAdd_ProductMap_New(string Listeddr_Code, string Product_Code, string Doctor_Name, string Sf_Code, string Division_Code, string ddlPriority)
        {
            int iReturn = -1;
            DataSet dsPrd_Name;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Sl_No = -1;
                //string Product_Name = string.Empty;

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_LstDrs_Product ";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "select Product_Detail_Name  from Mas_Product_Detail where Division_Code='" + Division_Code + "' and Product_Code_SlNo='" + Product_Code + "' ";
                dsPrd_Name = db.Exec_DataSet(strQry);

                //strQry = "select Product_Brd_Name as Product_Detail_Name  from mas_product_brand where Division_Code='" + Division_Code + "' and Product_Brd_Code='" + Product_Code + "' ";
                //dsPrd_Name = db.Exec_DataSet(strQry);


                if (DocProd_RecordExist_New(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "update Map_LstDrs_Product set Product_Code ='" + Product_Code + "',Product_Name='" + dsPrd_Name.Tables[0].Rows[0][0].ToString() + "', Product_Priority='" + ddlPriority + "'  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "'  ";
                }
                else
                {

                    strQry = " insert into Map_LstDrs_Product (Sl_No,Listeddr_Code,Product_Code,Product_Name,Doctor_Name, " +
                       " Sf_Code,Division_Code,Created_Date,Product_Priority) " +
                       " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + Product_Code + "', '" + dsPrd_Name.Tables[0].Rows[0][0].ToString() + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                       " '" + Division_Code + "',  getdate(),'" + ddlPriority + "' )";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getPrd_Priority(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dtListedDR = null;

            strQry = " select LstDr_Priority,LstDr_Priority_Range from admin_setups where division_code='" + div_code + "' ";
            try
            {
                dtListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }
        public DataSet getListedDr_Spec_Area(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT 0 as ListedDrCode, '---Select---' as ListedDr_Name " +
                    "UNION  " +
                    "SELECT d.ListedDrCode,d.ListedDr_Name +' - ' + Doc_Spec_ShortName +' - ' + stuff((select ', '+territory_Name from Mas_Territory_Creation t  " +
                    "where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  " +
                    "FROM Mas_ListedDr d WHERE d.Sf_Code =  '" + sfcode + "' and d.ListedDr_Active_Flag = 0";

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
        //single analysis

        public DataSet getListedDr_MR(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " SELECT 0 as ListedDrCode,'---Select---' as ListedDr_Name " +
                       " UNION " +
                    " select ListedDrCode, d.ListedDr_Name from Mas_ListedDr d " +
                    " where d.Division_Code ='" + div_code + "' and d.Sf_Code='" + sfcode + "' " +
                    " and ListedDr_Active_Flag =0 ";


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
        public DataSet getListedDr_Single(string sfcode, string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " SELECT d.ListedDrCode,d.ListedDr_Name,ListedDr_Address1,ListedDr_Mobile,ListedDr_Email,Hospital_Address, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName , " +
                     " d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code  and  " +
                     " CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0  for XML path('')),1,2,'' )  " +
                     " territory_Name, " +
                     " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName  " +
                     " FROM Mas_ListedDr d WHERE d.Sf_Code =  '" + sfcode + "' and d.ListedDr_Active_Flag = 0 and ListedDrCode='" + drcode + "'  " +
                     " order by ListedDr_Name";

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

        public DataSet get_Prod_map(string ListedDrCode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select Listeddr_Code,Doctor_Name,Product_Code as Product_Code_SlNo,Product_Name from Map_LstDrs_Product " +
                     " where Listeddr_Code =  '" + ListedDrCode + "' and sf_code='" + sf_code + "' ";


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

        public DataSet get_Prod_Analysis(string ListedDrCode, int cmonth, int cyear, string prodcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Exec Prd_Vst_SingleAnalysis '" + ListedDrCode + "'," + cmonth + "," + cyear + ",'" + prodcode + "','" + sf_code + "'";


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
        public DataSet getDoc_Input(string divcode, string Dr_Code, string sf_Code, int Month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " select day(a.Activity_Date) as Activity_Date,b.Product_Detail, b.Gift_Name,b.Gift_Qty,b.Worked_with_Name from dcrmain_trans a,DCRDetail_lst_trans b " +
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
        public DataSet getDoc_Feedback(string divcode, string Dr_Code, string sf_Code, int Month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " select max(activity_remarks) +' / '+ Feedback_Content " +
                     " from dcrmain_trans a,DCRDetail_lst_trans b ,Mas_App_CallFeedback c " +
                     " where a.trans_slno=b.trans_slno and b.trans_detail_info_code='" + Dr_Code + "'  " +
                     " and c.Feedback_Id=rx and a.sf_code='" + sf_Code + "' and MONTH(a.Activity_Date)='" + Month + "' and year(a.Activity_Date) ='" + year + "' " +
                     " group by Feedback_Content ";

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
        public DataSet get_Chem_map(string ListedDrCode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " Select m.listeddr_code,c.chemists_name,m.Chemists_Code from Map_LstDrs_Chemists m, mas_chemists c " +
                      " where m.Listeddr_Code =  '" + ListedDrCode + "' and m.sf_code='" + sf_code + "' and m.chemists_code=c.chemists_code ";


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
        public DataSet get_Chem_Analysis(string ListedDrCode, int cmonth, int cyear, string chemcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Exec Chem_Vst_SingleAnalysis '" + ListedDrCode + "'," + cmonth + "," + cyear + ",'" + chemcode + "','" + sf_code + "'";


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
        public DataSet getDoc_Rep_Mgr(string divcode, string sf_code, string mgr_code, string iMonth, string iyear, string iday)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT  ListedDr_Name   FROM DCRMain_Trans A, DCRDetail_Lst_Trans B , Mas_ListedDr c  " +
                     " WHERE MONTH(A.Activity_Date) = '" + iMonth + "' AND YEAR(A.Activity_Date) = '" + iyear + "' AND A.Trans_Slno = B.Trans_Slno and B.Trans_Detail_Info_Type=1 " +
                     " and  B.Trans_Detail_Info_Code =c.ListedDrCode AND A.Sf_Code in ('" + sf_code + "') and B.Worked_with_Code like '%" + mgr_code + "%' " +
                     " AND LEN(B.Worked_with_Code)>9 AND DAY(A.Activity_Date) = '" + iday + "'    ";

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

        public DataSet getDoc_Notequal_Visit(string divcode, string sf_code, string mgr_code, string iMonth, string iyear, string iday)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT  ListedDr_Name   FROM DCRMain_Trans A, DCRDetail_Lst_Trans B , Mas_ListedDr c  " +
                     " WHERE MONTH(A.Activity_Date) = '" + iMonth + "' AND YEAR(A.Activity_Date) = '" + iyear + "' AND A.Trans_Slno = B.Trans_Slno and B.Trans_Detail_Info_Type=1 " +
                     " and  B.Trans_Detail_Info_Code =c.ListedDrCode AND A.Sf_Code in ('" + sf_code + "') and B.Worked_with_Code like '%" + mgr_code + "%' " +
                     " AND LEN(B.Worked_with_Code)>9 AND DAY(A.Activity_Date) = '" + iday + "' and " +
                     " ListedDr_Name not in ( SELECT  ListedDr_Name   FROM DCRMain_Trans A, DCRDetail_Lst_Trans B , Mas_ListedDr c   " +
                     " WHERE MONTH(A.Activity_Date) = '" + iMonth + "' AND YEAR(A.Activity_Date) = '" + iyear + "' AND A.Trans_Slno = B.Trans_Slno and B.Trans_Detail_Info_Type=1 " +
                     " and  B.Trans_Detail_Info_Code =c.ListedDrCode AND A.Sf_Code in ('" + mgr_code + "') and B.Worked_with_Code like '%" + sf_code + "%' " +
                     " AND LEN(B.Worked_with_Code)>9 AND DAY(A.Activity_Date) = '" + iday + "')   ";

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
        public DataSet getDoc_Notequal_Visit_MGR(string divcode, string sf_code, string mgr_code, string iMonth, string iyear, string iday)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            strQry = " SELECT  ListedDr_Name   FROM DCRMain_Trans A, DCRDetail_Lst_Trans B , Mas_ListedDr c  " +
                     " WHERE MONTH(A.Activity_Date) = '" + iMonth + "' AND YEAR(A.Activity_Date) = '" + iyear + "' AND A.Trans_Slno = B.Trans_Slno and B.Trans_Detail_Info_Type=1 " +
                     " and  B.Trans_Detail_Info_Code =c.ListedDrCode AND A.Sf_Code in ('" + mgr_code + "') and B.Worked_with_Code like '%" + sf_code + "%' " +
                     " AND LEN(B.Worked_with_Code)>9 AND DAY(A.Activity_Date) = '" + iday + "' and " +
                     " ListedDr_Name not in ( SELECT  ListedDr_Name   FROM DCRMain_Trans A, DCRDetail_Lst_Trans B , Mas_ListedDr c   " +
                     " WHERE MONTH(A.Activity_Date) = '" + iMonth + "' AND YEAR(A.Activity_Date) = '" + iyear + "' AND A.Trans_Slno = B.Trans_Slno and B.Trans_Detail_Info_Type=1 " +
                     " and  B.Trans_Detail_Info_Code =c.ListedDrCode AND A.Sf_Code in ('" + sf_code + "') and B.Worked_with_Code like '%" + mgr_code + "%' " +
                     " AND LEN(B.Worked_with_Code)>9 AND DAY(A.Activity_Date) = '" + iday + "')   ";

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
        public int RecordAddTrans_DrBus_Valuewise(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, int ListedDr_Code, string ListedDr_Name, float Business_Value)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "EXEC sp_Trans_Dr_Business_Valuewise_Insert '" + Sf_Code + "', '" + Trans_Month + "', " +
                      " '" + Trans_Year + "', '" + div_code + "', '" + ListedDr_Code + "', '" + ListedDr_Name + "', '" + Business_Value + "' ";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RecordUpdateTrans_DrBus_Valuewise(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, int ListedDr_Code, float Business_Value)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "EXEC sp_Trans_Dr_Business_Valuewise_Update '" + Sf_Code + "', '" + Trans_Month + "', " +
                      " '" + Trans_Year + "', '" + div_code + "', '" + ListedDr_Code + "', '" + Business_Value + "' ";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordDelTrans_DrBus_Valuewise(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, int ListedDr_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "EXEC sp_Trans_Dr_Business_Valuewise_Del '" + Sf_Code + "', '" + Trans_Month + "', " +
                      " '" + Trans_Year + "', '" + div_code + "', '" + ListedDr_Code + "' ";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet Trans_DrBus_Valuewise_RecordExist(string Listeddr_Code, string Sf_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT h.Sf_Code, h.Division_Code,h.Trans_Month,h.Trans_Year,d.ListedDr_Code, d.ListedDr_Name, d.Business_Value FROM " +
                     " Trans_Dr_Business_Valuewise_Head h " +
                     " LEFT JOIN Trans_Dr_Business_Valuewise_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                     " h.Sf_Code ='" + Sf_Code + "' AND d.ListedDr_Code='" + Listeddr_Code + "' AND " +
                     " Trans_Month ='" + Trans_Month + "' AND Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "' ";

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

        #region Doctor Business Productwise
        public DataSet Trans_ListedDr_Bus_HeadExist(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "  Declare @basedOn varchar(50) = '" + basedOn + "' " +
                " SELECT h.Trans_sl_No, h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year, d.ListedDrCode, d.Product_Detail_Name, d.Product_Sale_Unit, d.Product_Quantity, " +
                    " CASE WHEN @basedOn = 'R' THEN ISNULL((d.Product_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                     " WHEN @basedOn = 'M' THEN ISNULL((d.Product_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                     " WHEN @basedOn = 'D' THEN ISNULL((d.Product_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                     " WHEN @basedOn = 'N' THEN ISNULL((d.Product_Quantity * CAST(d.NSR_Price AS float)), 0)  " +
                     " WHEN @basedOn = 'T' THEN ISNULL((d.Product_Quantity * CAST(d.Sample_Price AS float)), 0)  " +
                     " END AS value from Trans_DCR_BusinessEntry_Head h INNER JOIN Trans_DCR_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where  " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND " +
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

        public DataSet Trans_ListedDr_Bus_HeadSlNo(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Trans_sl_No from Trans_DCR_BusinessEntry_Head " +
                    " where Sf_Code ='" + Sf_Code + "' AND ListedDrCode='" + ListedDrCode + "' AND " +
                    " Trans_Month ='" + Trans_Month + "' AND Trans_Year='" + Trans_Year + "' AND Division_Code ='" + div_code + "' ";

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

        public DataSet Trans_ListedDr_Bus_DetailExist(string Sf_Code, string ListedDrCode, string Product_Code, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "  Declare @basedOn varchar(50) = '" + basedOn + "' " +
                     " SELECT h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,d.ListedDrCode, d.Product_Detail_Name, d.Product_Sale_Unit, d.Product_Quantity, " +
                     " CASE WHEN @basedOn = 'R' THEN ISNULL((d.Product_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                     " WHEN @basedOn = 'M' THEN ISNULL((d.Product_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                     " WHEN @basedOn = 'D' THEN ISNULL((d.Product_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                     " WHEN @basedOn = 'N' THEN ISNULL((d.Product_Quantity * CAST(d.NSR_Price AS float)), 0)  " +
                     " WHEN @basedOn = 'T' THEN ISNULL((d.Product_Quantity * CAST(d.Sample_Price AS float)), 0)  " +
                     " END AS value from Trans_DCR_BusinessEntry_Head h INNER JOIN Trans_DCR_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where  " +
                     " h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND d.Product_Code= '" + Product_Code + "' AND  " +
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

        public int RecordAddTrans_ListedDrBus_Head(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, string active,
            int ListedDrCode, string ListedDr_Name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "INSERT INTO Trans_DCR_BusinessEntry_Head(Sf_Code,Division_Code,Trans_Month,Trans_Year,Created_Date, " +
                        " ListedDrCode, ListedDr_Name) VALUES ('" + Sf_Code + "','" + div_code + "','" + Trans_Month + "','" + Trans_Year + "', " +
                        " getDate(),'" + ListedDrCode + "', '" + ListedDr_Name + "')";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RecordAddTrans_ListedDrBus_Details(decimal Trans_sl_No, string Sf_Code, int div_code, int ListedDrCode, string Product_Code, string Product_Detail_Name,
            string Product_Sale_Unit, string Territory_Code, string Territory_Name, int Product_Quantity, double MRP_Price, double Retailor_Price,
            double Distributor_Price, double NSR_Price, double Target_Price, double value)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "INSERT INTO Trans_DCR_BusinessEntry_Details(Trans_sl_No,Division_Code,ListedDrCode,Territory_Code, Territory_Name,Product_Code, Product_Detail_Name, " +
                         " Product_Sale_Unit, Product_Quantity,MRP_Price,Retailor_Price,Distributor_Price,NSR_Price,Target_Price,value) " +
                         " VALUES ('" + Trans_sl_No + "','" + div_code + "','" + ListedDrCode + "','" + Territory_Code + "', '" + Territory_Name + "', " +
                         " '" + Product_Code + "', '" + Product_Detail_Name + "', '" + Product_Sale_Unit + "', '" + Product_Quantity + "', '" + MRP_Price + "'," + Retailor_Price + ", " +
                         " " + Distributor_Price + ", '" + NSR_Price + "','" + Target_Price + "'," + value + ")";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RecordUpdTrans_ListedDrBus_Head(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, string active,
            int ListedDrCode)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                DateTime Updated_Date = DateTime.Now;
                string sqlUpdated_Date = Updated_Date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                strQry = "";
                strQry = "UPDATE Trans_DCR_BusinessEntry_Head SET Updated_Date='" + sqlUpdated_Date + "' WHERE " +
                        " Sf_Code ='" + Sf_Code + "' AND ListedDrCode='" + ListedDrCode + "' AND Trans_Month ='" + Trans_Month + "' AND " +
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

        public int RecordUpdTrans_ListedDrBus_Details(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, int ListedDrCode, string Product_Code,
            int Product_Quantity, double value)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "UPDATE d SET d.Product_Quantity='" + Product_Quantity + "',d.value='" + value + "' FROM " +
                    " Trans_DCR_BusinessEntry_Details d INNER JOIN Trans_DCR_BusinessEntry_Head h ON d.Trans_Sl_No = h.Trans_Sl_No " +
                    " WHERE h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND d.Product_Code='" + Product_Code + "' " +
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

        public DataSet ListedDrBus_value(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "Declare @basedOn varchar(50) = '" + basedOn + "' " +
                    " ;with cteVal as (SELECT CASE WHEN @basedOn = 'R' THEN ISNULL((d.Product_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'M' THEN ISNULL((d.Product_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'D' THEN ISNULL((d.Product_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'N' THEN ISNULL((d.Product_Quantity * CAST(d.NSR_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'T' THEN ISNULL((d.Product_Quantity * CAST(d.Sample_Price AS float)), 0) " +
                    " END AS Val FROM Trans_DCR_BusinessEntry_Head h " +
                    " INNER JOIN Trans_DCR_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "') " +
                    " Select Sum(Val) from cteVal";

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

        public DataSet rpPrdwiseDrBusMRView(string Sf_Code, string div_code, int FYear, int FMonth, int TYear, int TMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "select Distinct a.Product_Code, b.ListedDrCode, b.ListedDr_Name, a.Product_Code, a.Product_Detail_Name, a.Product_Sale_Unit " +
                " from Trans_DCR_BusinessEntry_Details a " +
                " inner join Trans_DCR_BusinessEntry_Head b on b.Trans_Sl_No=a.Trans_Sl_No AND " +
                " a.Division_Code='" + div_code + "' and b.Sf_Code='" + Sf_Code + "' AND b.Trans_Year BETWEEN " + FYear + " AND " + TYear + " AND b.Trans_Month BETWEEN " + FMonth + " AND " + TMonth + "" +
                " order by b.ListedDr_Name";

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

        public DataSet rpPrdwiseDrBusView(string Sf_Code, string div_code, int Year, int Month, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "EXEC spDocBusLstDrwiseView '" + basedOn + "', '" + div_code + "', '" + Sf_Code + "', '" + Month + "', '" + Year + "'";

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

        public DataSet rpDrwisePrdBusView(string Sf_Code, string div_code, int Year, int Month, string state_code, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "EXEC spDocBusPrdwiseView '" + basedOn + "', '" + div_code + "', '" + Sf_Code + "', '" + Month + "', '" + Year + "', '" + state_code + "'";

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

        public DataSet rpListedDrBus_value(string Sf_Code, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = " Declare @basedOn varchar(50) = '" + basedOn + "' " +
                    " ;with cteVal as (SELECT CASE WHEN @basedOn = 'R' THEN ISNULL((d.Product_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                    " WHEN @basedOn = 'M' THEN ISNULL((d.Product_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                    " WHEN @basedOn = 'D' THEN ISNULL((d.Product_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                    " WHEN @basedOn = 'N' THEN ISNULL((d.Product_Quantity * CAST(d.NSR_Price AS float)), 0) " +
                    " WHEN @basedOn = 'T' THEN ISNULL((d.Product_Quantity * CAST(d.Target_Price AS float)), 0) " +
                    " END AS Val FROM Trans_DCR_BusinessEntry_Head h " +
                    " INNER JOIN Trans_DCR_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "') " +
                    " Select Sum(Val) from cteVal ";

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

        public DataSet Trans_ListedDr_Bus_View(string Sf_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Distinct h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,h.ListedDrCode FROM " +
                    " Trans_DCR_BusinessEntry_Head h " +
                    " INNER JOIN Trans_DCR_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
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

        public DataSet Trans_ListedDr_Bus_ViewMR(string Sf_Code, string div_code, int FYear, int FMonth, int TYear, int TMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Distinct h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,h.ListedDrCode, h.ListedDr_Name FROM " +
                    " Trans_DCR_BusinessEntry_Head h " +
                    " INNER JOIN Trans_DCR_BusinessEntry_Details d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND " +
                    " h.Division_Code ='" + div_code + "' AND h.Trans_Year BETWEEN " + FYear + " AND " + TYear + " AND h.Trans_Month BETWEEN " + FMonth + " AND " + TMonth + " ";

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

        public int Delete_ListedDrBusEntry(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE d FROM Trans_DCR_BusinessEntry_Details d INNER JOIN Trans_DCR_BusinessEntry_Head h " +
                    " ON d.Trans_Sl_No = h.Trans_Sl_No WHERE h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' " +
                    " AND h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "'; " +
                    " DELETE h FROM Trans_DCR_BusinessEntry_Head h INNER JOIN Trans_DCR_BusinessEntry_Details d ON h.Trans_Sl_No = d.Trans_Sl_No " +
                    " WHERE h.Sf_Code ='" + Sf_Code + "' AND h.ListedDrCode='" + ListedDrCode + "' AND h.Trans_Month ='" + Trans_Month + "' " +
                    " AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "';";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Delete_ListedDrProductDetailsBusiness(string Sf_Code, string ListedDrCode, string Product_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE d FROM Trans_DCR_BusinessEntry_Details d INNER JOIN Trans_DCR_BusinessEntry_Head h " +
                    " ON d.Trans_Sl_No = h.Trans_Sl_No WHERE h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND d.Product_Code= '" + Product_Code + "' " +
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

        public DataSet getLstdDr_Wrng_CreationFFWise(string Sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "EXEC sp_get_LstDr_Wrng_CreationFFWise '" + Sf_Code + "', '" + div_code + "'";

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
        public DataSet getLstdDr_Wrng_CreationDivwise(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "EXEC sp_get_LstDr_Wrng_CreationDivwise '" + div_code + "'";

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
        public int RecordUpdateLstdDr_Wrng_Creation(string Sf_Code, int div_code, int ListedDr_Code, DateTime CrtDate, DateTime ActDate)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                string ListedDr_Created_Date = new DateTime(ActDate.Year, ActDate.Month, 1).ToString("yyyy-MM-dd HH:mm:ss.fff");

                strQry = "UPDATE Mas_ListedDr SET ListedDr_Created_Date = '" + ListedDr_Created_Date + "' WHERE Sf_Code='" + Sf_Code + "' AND " +
                        " ListedDrCode='" + ListedDr_Code + "' AND Division_Code='" + div_code + "'";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Reject_Approve_Temp(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Exec ListeddrAdd_App_Reject '" + sf_code + "','" + dr_code + "'," + iVal + "," + oVal + ",'" + sf_name + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getListedDr_Reject_Temp(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                        "stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr_One d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.ListedDr_Active_Flag = 4 ";
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
        public int Interchange_MR_New(string from_sfcode, string to_sfcode, string from_name, string to_name, string div_code)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();


            strQry = "EXEC Hq_Interchange_Rollback '" + from_sfcode + "','" + to_sfcode + "','" + from_name + "','" + to_name + "','" + div_code + "'";

            try
            {
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordAdd_ProductMap_Upload(string Listeddr_Code, string Product_Code, string Doctor_Name, string Sf_Code, string Division_Code, int Priority)
        {
            int iReturn = -1;
            DataSet dsPrd_Name;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Sl_No = -1;
                //string Product_Name = string.Empty;

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_LstDrs_Product ";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "select Product_Detail_Name,Product_Code_SlNo  from Mas_Product_Detail where Division_Code='" + Division_Code + "' and Product_Detail_Code='" + Product_Code + "' and Product_Active_Flag=0 ";
                dsPrd_Name = db.Exec_DataSet(strQry);

                if (dsPrd_Name.Tables[0].Rows.Count > 0)
                {

                    strQry = " insert into Map_LstDrs_Product (Sl_No,Listeddr_Code,Product_Code,Product_Name,Doctor_Name, " +
                       " Sf_Code,Division_Code,Created_Date,Product_Priority) " +
                       " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + dsPrd_Name.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "', '" + dsPrd_Name.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                       " '" + Division_Code + "',  getdate()," + Priority + ")";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet Get_Cat_Met(string sfcode, string Div_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " exec Review_Report_Cat_Visit '" + sfcode + "','" + Div_code + "'," + imonth + "," + iyear + " ";
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
        public DataSet Get_Cat_Seen(string sfcode, string Div_code, string imonth, string iyear, string sDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Doc_Cat_SName as name, count(b.Trans_Detail_Info_Code) cntdrseen " +
                      " FROM DCRMain_Trans a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Doctor_Category d WHERE a.Division_Code = '" + Div_code + "'    " +
                      " AND a.sf_code IN ('" + sfcode + "') AND month(a.Activity_Date)='" + imonth + "' AND YEAR(a.Activity_Date)='" + iyear + "' " +
                      " AND a.Trans_SlNo = b.Trans_SlNo AND b.Trans_Detail_Info_Type=1 AND b.Trans_Detail_Info_Code=c.listeddrcode " +
                      " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + sDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=   " +
                      " CONVERT(VARCHAR(50), '" + sDate + "', 126) or ListedDr_Deactivate_Date is null)) " +
                      " AND c.Doc_cat_code = d.Doc_cat_code GROUP BY Doc_Cat_SName ";
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

        public DataSet Get_Cat_Visit_days(string sfcode, string Div_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " exec Review_Report_Cat_Visit_Time '" + sfcode + "','" + Div_code + "'," + imonth + "," + iyear + " ";
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

        public DataSet Get_Doc_Servie_Tot(string sfcode, string Div_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select sum(cast(Service_Amt as int)) Service_Amt  from Trans_Doctor_Service_Head where sf_code='" + sfcode + "'  " +
                     " and ser_type='2' and trans_month=" + imonth + " and Division_code='" + Div_code + "' and Trans_Year=" + iyear + "";
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
        public DataSet GetCat_DRs(string sfcode, string Div_code, string sDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select  Doc_Cat_SName cat, count(ListedDrCode) cnt,c.No_of_visit " +
                     " from Mas_Doctor_Category c ,Mas_ListedDr d where c.Division_Code = '" + Div_code + "' and Doc_Cat_Active_Flag = 0 " +
                     " and c. Doc_Cat_Code = d.Doc_Cat_Code and sf_code='" + sfcode + "' " +
                     " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + sDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                     " CONVERT(VARCHAR(50), '" + sDate + "'  , 126) or ListedDr_Deactivate_Date is null)) " +
                     " group by Doc_Cat_SName,c.No_of_visit ";
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
        public DataSet FindCommonDr_ALL(string sFindQry, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT C_Doctor_Code,C_Doctor_Name,C_Doctor_Address,C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Doctor_Qual_Code, " +
                     " C_Doctor_Qual_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name, " +
                     " C_Doctor_Cat_Code,C_Doctor_Cat_Name,C_Created_Date,C_Approved_Date,Allocated_IDs,Allocated_Id_Name, " +
                     " C_Territory_Code,C_Territory_Name,C_Active_Flag,Division_Code,C_Doctor_HQ,Drs_City,Drs_Registration_No,Pincode,MR_HQ_Name,Unique_No,Qual_Short_Name,Speciality_Short_Name,C_Doc_Hospital,C_Visiting_Card as Visiting_Card " +

                     " from Mas_Common_Drs where C_Doctor_Name !='' and Allocated_IDs Not like '%" + sf_code + "%' and C_Active_Flag=0 " +
                      sFindQry +
                       " ORDER BY C_Doctor_Name";
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
        public DataSet getCommonDr_List_Edit_Fdc(int C_Doctor_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT C_Doctor_Code,C_Doctor_Name,C_Doctor_Address,C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Doctor_Qual_Code, " +
                     " C_Doctor_Qual_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name, " +
                     " C_Doctor_Cat_Code,C_Doctor_Cat_Name,C_Created_Date,C_Approved_Date,Allocated_IDs,Allocated_Id_Name, " +
                     " C_Territory_Code,C_Territory_Name,C_Active_Flag,Division_Code,C_Doctor_HQ, Ref_No,Drs_Landline_No,Qual_Short_Name, " +
                     " Drs_Registration_No,Pincode,Drs_City,Speciality_Short_Name,Email_ID,MR_HQ_Name,Unique_No,C_Doc_Hospital,State " +
                     "  from Mas_Common_Drs where " +
                     " C_Doctor_Code = " + C_Doctor_Code + "";
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
        public DataSet GetHQ()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry =
                     " SELECT distinct MR_HQ_Name " +
                     " FROM  Mas_Common_Drs where ( MR_HQ_Name!= '' and MR_HQ_Name is not null) order by MR_HQ_Name ";
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

        public DataSet GetDoc_Cat_Unique(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select top 1 Doc_Cat_Code,Doc_Cat_SName from Mas_Doctor_Category where Division_Code = '" + div_code + "' and Doc_Cat_Active_Flag=0 ";

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
        public DataSet GetClass_Unique(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select top 1 Doc_ClsCode,Doc_ClsSName from Mas_Doc_Class where Division_Code = '" + div_code + "' and Doc_Cls_ActiveFlag=0";

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
        public int GetSpec_code()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Doc_Special_Code)+1,'1') Doc_Special_Code from Mas_Doctor_Speciality ";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getListedDr_Add_approve_FDC(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,ListedDr_PinCode,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City, " +
                        " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,'' as Deact_Reason,'' as Listeddr_App_Mgr from " +
                        "mas_listeddr d " +
                        "WHERE " +

                        "  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 2  and d.SLVNo not in " +
                        " (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 )";
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
        public int ApproveAdd_FDC(string sf_code, string dr_code, int iVal, int oVal, string sf_name, string Common_dr)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();



                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "',ListedDr_Created_Date = getdate()  " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getListedDr_Deactivate_FDC(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City, " +
                        " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,C_Doctor_Code, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from  " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code  " +
                        " and b.Sf_Code='" + sfcode + "'  and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date from " +
                        " mas_listeddr d " +
                        " WHERE " +
                        " d.sf_code = '" + sfcode + "'" +
                        " and d.ListedDr_Active_Flag = 0  and " +
                        " C_Doctor_Code != '' ";
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
        public int DeActivate_Dr_Unique(string dr_code, int iflag, string div_code, string sf_code, string Reason)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag = '" + iflag + "' ,Deact_Reason='" + Reason + "' " +

                            " WHERE listeddrcode = '" + dr_code + "' and Division_code='" + div_code + "' and sf_code='" + sf_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Unique_Deact_approve(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City,ListedDr_PinCode, " +
                        " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,C_Doctor_Code,Deact_Reason,'' as Listeddr_App_Mgr " +
                        " from mas_listeddr d " +
                        " WHERE " +
                        " d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 3 and d.SLVNo not in " +
                        " (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2)";
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
        public DataSet getListedDr_MGR_Temp(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "') and C_doctor_code != ''";
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
        public int Uni_Doc_ADD(string Common_DR_Name, string DR_Address, string DR_Catg, string Cat_SName, string DR_Qual, string Qua_SName,
                       string DR_Spec, string Spec_SName, string DR_Cls, string Cls_SName, string Hospital_Address,
                       string DR_Mobile, string DR_Hq, string terr_code, string Terr_Name, string sf_code, string sf_name, string div_code,
                       string email, string Q_SName, string S_SName, string landline, string regNo, string city, string pincode, string Mr_Hq, string Dob, string dow, string state)
        {
            int iReturn = -1;


            try
            {

                DB_EReporting db = new DB_EReporting();

                int C_Doctor_Code = -1;


                //strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";


                strQry = "SELECT ISNULL(MAX(C_Doctor_Code),0)+1 FROM Mas_Common_Drs ";
                C_Doctor_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Common_Drs (C_Doctor_Code, C_Doctor_Name, C_Doctor_Address,C_Doctor_Cat_Code,C_Doctor_Cat_Name, " +
                         " C_Doctor_Qual_Code,C_Doctor_Qual_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,   " +
                         " C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Doctor_HQ,C_Territory_Code,C_Territory_Name,Allocated_IDs,Allocated_Id_Name,C_Created_Date,C_Active_Flag,Division_Code,Deactivate_Flag, " +
                         " Email_ID,Qual_Short_Name,Speciality_Short_Name,Drs_Landline_No,Drs_Registration_No,Drs_City,Pincode ,Ref_No,MR_HQ_Name,Unique_No,DOB,DOW,State) " +
                       " VALUES('" + C_Doctor_Code + "','" + Common_DR_Name + "', " +
                       " '" + DR_Address + "', '" + DR_Catg + "', '" + Cat_SName + "', '" + DR_Qual + "', '" + Qua_SName + "', " +
                       " '" + DR_Spec + "', '" + Spec_SName + "', '" + DR_Cls + "', '" + Cls_SName + "', '" + Hospital_Address + "', '" + DR_Mobile + "', '" + DR_Hq + "', " +
                       " '" + terr_code + "', '" + Terr_Name + "', '" + sf_code + "', '" + sf_name + "'  , " +
                       "  getdate(), 2, '" + div_code + "',0,'" + email + "','" + Q_SName + "','" + S_SName + "','" + landline + "','" + regNo + "', " +
                       " '" + city + "','" + pincode + "','" + C_Doctor_Code + "','" + Mr_Hq + "','" + C_Doctor_Code + "','" + Dob + "','" + dow + "','" + state + "')";


                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int RecordAdd_ProductMap_Unique(string Listeddr_Code, string Product_Code, string Doctor_Name, string Sf_Code, string Division_Code, int Priority, string Prod_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Sl_No = -1;
                //string Product_Name = string.Empty;

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_LstDrs_Product ";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = " insert into Map_LstDrs_Product (Sl_No,Listeddr_Code,Product_Code,Product_Name,Doctor_Name, " +
                   " Sf_Code,Division_Code,Created_Date,Product_Priority) " +
                   " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + Product_Code + "', '" + Prod_Name + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                   " '" + Division_Code + "',  getdate()," + Priority + ")";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Reject_Approve_Temp_FDC(string sf_code, string dr_code, int iVal, int oVal, string sf_name, string reason, string common)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Exec ListeddrAdd_App_Reject_Unique '" + sf_code + "','" + dr_code + "'," + iVal + "," + oVal + ",'" + sf_name + "', '" + reason + "' ,'" + common + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet Unique_Reject(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City, " +
                        " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,C_Doctor_Code,Reject_Reason as Deact_Reason " +
                        " from mas_listeddr_one d " +
                        " WHERE " +
                        " d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 4 ";

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
        public int GetListedDrCode_One()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(ListedDrCode)+1,'1') ListedDrCode from Mas_ListedDr_One ";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getListedDr_MGR_Mode(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name, 'Existing' as mode " +
                        " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "')" +
                        " Union " +
                        " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name,'New' as mode  " +
                        " from mas_listeddr_One a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "')";
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
        public DataSet getListedDr_Add_approve_New(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.ListedDr_PinCode,d.ListedDr_Phone,d.ListedDr_Mobile,d.ListedDr_Email,d.ListedDr_DOB,d.ListedDr_DOW,d.ListedDr_RegNo,d.ListedDr_Hospital,d.Hospital_Address,d.Visiting_Card,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode,ListedDr_Mobile,d.territory_code," +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City ,State_Code,Hospital_Address,'~/Visiting_Card_One/'+Visiting_Card as Visiting_Card,ListedDr_Hospital " +
                        "  from " +
                        "mas_listeddr_One d " +
                        "WHERE " +

                        "  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 2 and d.Division_code='" + div_code + "'  ";
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

        public DataSet getExisting_dr(string sfcode, string c_doctor_code, string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.ListedDr_PinCode,d.ListedDr_Phone,d.ListedDr_Mobile,d.ListedDr_Email,d.ListedDr_DOB,d.ListedDr_DOW,d.ListedDr_RegNo,d.ListedDr_Hospital,d.Hospital_Address,d.Visiting_Card,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode,ListedDr_Mobile,d.territory_code,State_Code," +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City, " +
                           "  (select top(1) product_name from Map_LstDrs_Product where  Listeddr_Code='" + ListedDrCode + "' and Product_Priority=1) product1, " +
                           "  (select top(1)  product_name from Map_LstDrs_Product where  Listeddr_Code='" + ListedDrCode + "' and Product_Priority=2) product2, " +
                           "  (select top(1)  product_name from Map_LstDrs_Product where  Listeddr_Code='" + ListedDrCode + "' and Product_Priority=3) product3, " +
                            "  (select top(1)  product_name from Map_LstDrs_Product where  Listeddr_Code='" + ListedDrCode + "' and Product_Priority=4) product4, " +
                            "  (select top(1)  product_name from Map_LstDrs_Product where  Listeddr_Code='" + ListedDrCode + "' and Product_Priority=5) product5 " +
                        "  from " +
                        "mas_listeddr d " +
                        "WHERE " +

                        "  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 2 and C_doctor_code='" + c_doctor_code + "'  ";

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
        public DataSet getNew_dr(string sfcode, string c_doctor_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.ListedDr_PinCode,d.ListedDr_Phone,d.ListedDr_Mobile,d.ListedDr_Email,d.ListedDr_DOB,d.ListedDr_DOW,d.ListedDr_RegNo,d.ListedDr_Hospital,d.Hospital_Address,d.Visiting_Card,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode,ListedDr_Mobile, d.territory_code," +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City " +
                        "  from " +
                        "mas_listeddr_One d " +
                        "WHERE " +

                        "  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 2 and C_doctor_code='" + c_doctor_code + "'  ";

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
        public int ApproveAdd_New_Unique(string sf_code, string dr_code, int iVal, int oVal, string sf_name, string Common_dr)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();



                strQry = "UPDATE Mas_ListedDr_One " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getListedDr_Unique_admin(int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr_One a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "')";
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
        public DataSet getNew_dr_admin_List(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.ListedDr_PinCode,d.ListedDr_Phone,d.ListedDr_Mobile,d.ListedDr_Email,d.ListedDr_DOB,d.ListedDr_DOW,d.ListedDr_RegNo,d.ListedDr_Hospital,d.Hospital_Address,d.Visiting_Card,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode,ListedDr_Mobile,d.territory_code," +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City " +
                        "  from " +
                        "mas_listeddr_One d " +
                        "WHERE " +

                        "  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 5 and d.Division_code='" + div_code + "' order by ListedDrCode  ";

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

        public DataSet getNew_dr_admin_View(string sfcode, string div_code, string listeddrcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.ListedDr_PinCode,d.ListedDr_Phone,d.ListedDr_Mobile,d.ListedDr_Email,d.ListedDr_DOB,d.ListedDr_DOW,d.ListedDr_RegNo,d.ListedDr_Hospital,d.Hospital_Address,d.Visiting_Card,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode,ListedDr_Mobile,d.territory_code," +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') CCity,State_Code,Hospital_Address,ListedDr_RegNo,City,Product_Map " +
                        "  from " +
                        "mas_listeddr_One d " +
                        "WHERE " +

                        "  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 5 and d.Division_code='" + div_code + "' and ListedDrCode='" + listeddrcode + "'   ";

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
        public DataSet ViewListedDr_New(string drcode, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " d.Territory_Code,d.ListedDr_Address2,d.ListedDr_Address3, d.ListedDr_PinCode, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " ListedDr_Email, convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,d.State_Code," +
                      " d.ListedDr_Profile,d.ListedDr_Visit_Days,d.ListedDr_IUI,d.ListedDr_Avg_Patients,d.ListedDr_DayTime," +
                      " d.ListedDr_Hospital,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,d.Hospital_Address,ListedDr_RegNo,City,Visiting_Card FROM" + " " +
                      " Mas_ListedDr_One d " +
                        "WHERE d.ListedDrCode =  '" + drcode + "' and Division_code='" + Div_code + "' " +

                        "and d.ListedDr_Active_Flag = 5";
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
        public DataSet getNew_dr_MGR_View(string sfcode, string div_code, string listeddrcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " d.Territory_Code,d.ListedDr_Address2,d.ListedDr_Address3, d.ListedDr_PinCode, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " ListedDr_Email, convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,d.State_Code," +
                      " d.ListedDr_Profile,d.ListedDr_Visit_Days,d.ListedDr_IUI,d.ListedDr_Avg_Patients,d.ListedDr_DayTime," +
                      " d.ListedDr_Hospital,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,d.Hospital_Address,ListedDr_RegNo,City,Visiting_Card,Product_Map FROM" + " " +
                      " Mas_ListedDr_One d " +
                        "WHERE d.ListedDrCode =  '" + listeddrcode + "' and Division_code='" + div_code + "' and d.sf_code = '" + sfcode + "'" +

                        "and d.ListedDr_Active_Flag = 2";
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
        public int Final_App_FDC(string sf_code, string dr_code, int iVal, int oVal, string sf_name, string reason, string common)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Exec ListeddrAdd_App_Unique '" + sf_code + "','" + dr_code + "'," + iVal + "," + oVal + ",'" + sf_name + "', '" + reason + "' ,'" + common + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int ApproveDeact_App(string dr_code, int iVal, string div_code, string sf_code, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Exec ListeddrDeact_Approved '" + dr_code + "'," + iVal + ",'" + div_code + "','" + sf_code + "','" + sf_name + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int ApproveDeact_Rej(string dr_code, int iVal, string div_code, string sf_code, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Exec ListeddrDeact_Reject '" + dr_code + "'," + iVal + ",'" + div_code + "','" + sf_code + "','" + sf_name + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Common_Category(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Doc_Cat_Code,'---Select---' as Doc_Cat_SName, '---Select---' as Doc_Cat_Name " +
                         " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name " +
                     " FROM  Mas_Doctor_Category where division_Code = '" + div_code + "' AND doc_cat_active_flag=0 ";
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

        public DataSet Common_Speciality(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Doc_Special_Code,'---Select---' as Doc_Special_SName, '---Select---' as Doc_Special_Name " +
                         " UNION " +
                     " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name " +
                     " FROM  Mas_Doctor_Speciality where division_Code = '" + div_code + "' AND doc_special_active_flag=0 ";
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
        public DataSet Common_Class(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Doc_ClsCode,'---Select---' as Doc_ClsSName, '---Select---' Doc_ClsName " +
                         " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName " +
                     " FROM  Mas_Doc_Class where division_Code = '" + div_code + "' AND doc_cls_activeflag=0 ";
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
        public DataSet Common_Qualification(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as  Doc_QuaCode,'---Select---' as Doc_QuaName " +
                         " UNION " +
                     " SELECT Doc_QuaCode,Doc_QuaName " +
                     " FROM  Mas_Doc_Qualification where division_Code = '" + div_code + "' AND doc_qua_activeflag=0 ";
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
        public int GetCommonDrCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(C_Doctor_Code),0)+1 FROM Mas_Common_Drs ";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getCommon_List(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " SELECT A.C_Doctor_Code,a.C_Doctor_Name,a.C_Doctor_Address, C_Doctor_Cat_Code,C_Doctor_Cat_Name,  " +
                     " C_Doctor_Qual_Code,C_Doctor_Qual_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,   " +
                     " C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Territory_Code,C_Territory_Name, " +
                     " Split.a.value('.', 'VARCHAR(100)') AS sf_code  " +
                     "  FROM  (SELECT [C_Doctor_Code],C_Doctor_Name, C_Doctor_Address, " +
                     "  C_Doctor_Cat_Code,C_Doctor_Cat_Name,  " +
                     "  C_Doctor_Qual_Code,C_Doctor_Qual_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,   " +
                     "  C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Territory_Code,C_Territory_Name, " +
                     "  CAST ('<M>' + REPLACE([Allocated_IDs], ',', '</M><M>') + '</M>' AS XML) AS String  " +
                     "  FROM  mas_common_drs where division_code in ('" + div_code + "') and C_Active_Flag=2) AS A CROSS APPLY String.nodes ('/M') AS Split(a) ";
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
        public DataSet getCommonDr_List(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT C_Doctor_Code,C_Doctor_Name,C_Doctor_Address,C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Doctor_Qual_Code, " +
                     " C_Doctor_Qual_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name, " +
                     " C_Doctor_Cat_Code,C_Doctor_Cat_Name,C_Created_Date,C_Approved_Date,Allocated_IDs,Allocated_Id_Name, " +
                     " C_Territory_Code,C_Territory_Name,C_Active_Flag,Division_Code,C_Doctor_HQ,C_Doctor_Mobile, C_Doc_Hospital, " +
                     "  case when C_Doctor_Code is not null then 'CD' " +
                    " else case when C_Doctor_Code is null then 'SD' " +
                    " end end as St , " +
                     " (CASE WHEN (C_Active_Flag = '0' ) THEN 'Approved' " +
                     " WHEN (C_Active_Flag = '2' )THEN 'Pending' end ) Status from Mas_Common_Drs   where " +
                     " Division_code = '" + div_code + "' ";
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
        public DataSet getListedDr_Add_List_FDC(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode,ListedDr_Mobile, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City, " +
                        " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,State_Code,Hospital_Address,'~/Visiting_Card_One/'+Visiting_Card as Visiting_Card,ListedDr_Hospital from " +
                        "mas_listeddr d " +
                        "WHERE " +

                        "  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 2  and d.SLVNo not in " +
                        " (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 )";
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
        public DataSet Unique_Reject_List(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') City,ListedDr_PinCode, " +
                        " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,C_Doctor_Code,Reject_Reason as Deact_Reason,Listeddr_App_Mgr " +
                        " from mas_listeddr_one d " +
                        " WHERE " +
                        " d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag in (1,4) ";

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
        public int Reject_New_Unique(string sf_code, string dr_code, int iVal, int oVal, string sf_name, string Common_dr)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();



                strQry = "UPDATE Mas_ListedDr_One " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "',C_Doctor_Code=NULL " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);

                strQry = "delete from mas_common_drs where C_Doctor_Code='" + Common_dr + "'";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet GetCity()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry =
                     " SELECT distinct Drs_City " +
                     " FROM  Mas_Common_Drs  order by Drs_City ";
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
        public DataSet GetState_Doc()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry =
                     " SELECT distinct State " +
                     " FROM  Mas_Common_Drs  order by State ";
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
        public int Final_App_FDC_New(string sf_code, string dr_code, int iVal, int oVal, string sf_name, string reason, string common)
        {
            //int iReturn = -1;
            int iTransdr = 0;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Exec ListeddrAdd_App_Unique_New '" + sf_code + "','" + dr_code + "'," + iVal + "," + oVal + ",'" + sf_name + "', '" + reason + "' ,'" + common + "'";

                iTransdr = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iTransdr;

        }
        public DataSet tot_unique()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry =
                     "select count(*) as tot from mas_common_drs where C_Active_Flag=0";
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
        public DataSet Selected_unique()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry =
                     "select count(*) as uni from mas_common_drs where  Allocated_IDs != '' and  C_Active_Flag=0 ";
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
        public int Reject_New_Unique_Reason(string sf_code, string dr_code, int iVal, int oVal, string sf_name, string Common_dr, string Reject)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();



                strQry = "UPDATE Mas_ListedDr_One " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "',C_Doctor_Code=NULL, Reject_Reason ='" + Reject + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);

                strQry = "delete from mas_common_drs where C_Doctor_Code='" + Common_dr + "'";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet FindCommonDr_ALL_Exist(string sFindQry, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT C_Doctor_Code,C_Doctor_Name,C_Doctor_Address,C_Doctor_Hos_Addr,C_Doctor_Mobile,C_Doctor_Qual_Code, " +
                     " C_Doctor_Qual_Name,C_Doctor_Cls_Code,C_Doctor_Cls_Name,C_Doctor_Spl_Code,C_Doctor_Spl_Name, " +
                     " C_Doctor_Cat_Code,C_Doctor_Cat_Name,C_Created_Date,C_Approved_Date,Allocated_IDs,Allocated_Id_Name, " +
                     " C_Territory_Code,C_Territory_Name,C_Active_Flag,Division_Code,C_Doctor_HQ,Drs_City,Drs_Registration_No,Pincode,MR_HQ_Name,Unique_No,Qual_Short_Name,Speciality_Short_Name,C_Doc_Hospital,'~/Visiting_Card_One/'+C_Visiting_Card as Visiting_Card" +

                     " from Mas_Common_Drs where C_Doctor_Name !='' " +
                      sFindQry +
                       " ORDER BY C_Doctor_Name";
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

        public DataSet getListedDr_Unique_admin_cnt(int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name, COUNT(ListedDrCode)  dr_cnt  " +
                        " from mas_listeddr_One a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "')" +
                        " group by b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name ";


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
        public DataSet getNew_dr_MR_Entry(string sfcode, string div_code, string listeddrcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                      " d.Territory_Code,d.ListedDr_Address2,d.ListedDr_Address3, d.ListedDr_PinCode, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " ListedDr_Email, convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,d.State_Code," +
                      " d.ListedDr_Profile,d.ListedDr_Visit_Days,d.ListedDr_IUI,d.ListedDr_Avg_Patients,d.ListedDr_DayTime," +
                      " d.ListedDr_Hospital,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,d.Hospital_Address,ListedDr_RegNo,City,Visiting_Card,Product_Map FROM" + " " +
                      " Mas_ListedDr_One d " +
                        "WHERE d.ListedDrCode =  '" + listeddrcode + "' and Division_code='" + div_code + "' and d.sf_code = '" + sfcode + "'" +

                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDr_Add_approve_FDC_New(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                     " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') cCity, " +
                     " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,'' as Deact_Reason,'' as Listeddr_App_Mgr,sf_code,'Existing' as mode,City,ListedDr_PinCode from " +
                     " mas_listeddr d WHERE d.sf_code = '" + sfcode + "' and d.ListedDr_Active_Flag = 2  and d.SLVNo not in  " +
                     " (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 ) " +
                     " union all " +
                     " SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,   " +
                     " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') cCity, " +
                     " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,'' as Deact_Reason,'' as Listeddr_App_Mgr,sf_code,'New' as mode,City,ListedDr_PinCode from " +
                     " Mas_ListedDr_One d  " +
                     " WHERE " +
                     " d.sf_code = '" + sfcode + "' and d.ListedDr_Active_Flag in (2,5)  ";
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
        public DataSet Unique_Deact_approve_New(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                        " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') cCity, " +
                        " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,C_Doctor_Code, Deact_Reason,'' as Listeddr_App_Mgr,sf_code,'' mode,City ,ListedDr_PinCode" +
                        " from mas_listeddr d " +
                        " WHERE " +
                        " d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 3 and d.SLVNo not in " +
                        " (select SLVNo from mas_listeddr_One where ListedDr_Active_Flag = 2)";
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
        public DataSet Unique_Reject_New(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,ListedDr_Mobile,  " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name,c_doctor_code,  " +
                     " stuff((select ', '+Alias_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') cCity, " +
                     " (select Division_name from mas_division s where s.division_code=d.division_code) Division_name,C_Doctor_Code,Reject_Reason as Deact_Reason,sf_code ,City,Listeddr_App_Mgr,ListedDr_PinCode," +
                     " case when d.ListedDr_Active_Flag=1 then 'New' " +
                     " else case when d.ListedDr_Active_Flag=4 then 'Existing' " +
                     " end end as mode " +
                     " from mas_listeddr_one d WHERE d.sf_code = '" + sfcode + "' and d.ListedDr_Active_Flag in (1,4) ";

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
        public int Interchange_MR_Rollback(string from_sfcode, string to_sfcode, string from_name, string to_name, string div_code, string from_Hq, string to_Hq)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();


            strQry = "EXEC Hq_Interchange_Rollback_SFC '" + from_sfcode + "','" + to_sfcode + "','" + from_name + "','" + to_name + "','" + div_code + "','" + from_Hq + "','" + to_Hq + "'";

            try
            {
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int BulkEdit_New(string str, string Doc_Code, string div_code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_ListedDr SET " + str + "  Where ListedDrCode='" + Doc_Code + "' and Division_code= '" + div_code + "' and sf_code='" + sf_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet BingChemistDDL(string ListedDrCode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " Select m.listeddr_code,c.chemists_name,m.Chemists_Code from Map_LstDrs_Chemists m, mas_chemists c " +
                      " where  m.sf_code='" + sf_code + "' and m.chemists_code=c.chemists_code ";


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
        public DataSet getCity_Alphabet(string div_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select '1' val,'All' Town_Name " +
                     " union " +
                     " select distinct LEFT(Town_Name,1) val, LEFT(Town_Name,1) Town_Name" +
                     " FROM Mas_State_TownCity " +
                     " WHERE State_code='" + state_code + "' and Division_Code = '" + div_code + "' and Active_Flag=0 " +

                     " ORDER BY 1";
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
        public DataSet getCity_Alphabet(string div_code, string state_code, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Sl_No, State_code,State_Name,Town_Name,Town_Type,case when Town_Type='ME' then 'Metro' " +
                     " else case when Town_Type='NM' then 'Non-Metro' end  end as Town from Mas_State_TownCity " +
                     " WHERE State_code='" + state_code + "' and Division_Code = '" + div_code + "' and Active_Flag=0 " +
                     " AND LEFT(Town_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
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
        public DataSet FetchTownCity(string div_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Sl_No,'---Select---' as Town_Name " +
                         " UNION " +
                     " SELECT Sl_No,Town_Name " +
                     " FROM  Mas_State_TownCity where Division_Code = '" + div_code + "' and State_code='" + state_code + "' AND Active_Flag=0 ";
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
        public DataSet FetchTown_MR(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT '---Select---' as Town_City " +
                         " UNION " +
                     " SELECT distinct Town_City " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 and Town_City != '' ";
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
        public DataSet getspeciality(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Doc_Special_Code,Doc_Special_SName,Doc_Spec_Sl_No from [dbo].[Mas_Doctor_Speciality] where division_code='" + div_code + "' and  Doc_Special_Active_Flag=0 order by Doc_Spec_Sl_No";
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




        public DataSet getListedDr_Search(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " SELECT 0 as ListedDrCode,'---Select---' as ListedDr_Name " +
                       " UNION " +
                    " select ListedDrCode, d.ListedDr_Name + ' - '  + t.Territory_Name from Mas_ListedDr d,Mas_Territory_Creation t " +
                    " where d.Territory_Code=CONVERT(varchar,t.Territory_Code)  " +
                    " and ListedDr_Active_Flag =0 ";


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


        public DataSet getListedDr_Hq(string div_code, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            //strQry = " SELECT '' as Sf_Code,'---All---' as Sf_HQ " +
            //           " UNION " +
            //        " select distinct  sf_code,Sf_HQ from mas_salesforce where sf_TP_Active_Flag=0 and Sf_HQ<>''  and Sf_Code like '%MR%' and Division_Code ='" + div_code + "," + "' order by Sf_HQ";

            strQry = " SELECT '' as Sf_Code,'---All---' as Sf_HQ " +
                     " UNION " +
                  " select distinct  sf_code,Sf_HQ from mas_salesforce where sf_TP_Active_Flag=0 and Sf_HQ<>'' and  Sf_Code<>'admin'  and Sf_Code like '%MR%' and convert(int,replace([Division_Code],',','')) in (" + div_code + ") order by Sf_HQ";
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


        public DataSet BindListedDr(string div_code, string ListedDr_Name, string HQ)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (HQ == "0")
            {
                strQry = " select distinct  d.Division_Name,b.Sf_code,b.Sf_Name,Sf_HQ,sf_Designation_Short_Name,a.Division_Code,a.ListedDrCode,ListedDr_Name,Doc_Qua_Name,Doc_Spec_ShortName, Doc_Cat_ShortName,Doc_Class_ShortName,Territory_Name " +
                          " from mas_listeddr a , mas_salesforce b, Mas_Territory_Creation c, mas_division d where a.sf_code=b.sf_code and cast(c.Territory_Code as varchar)= a.Territory_Code and a.Division_Code=d.Division_Code  " +
                       " and a.ListedDr_Active_Flag=0  and b.sf_TP_Active_Flag=0 and  (a.ListedDr_Name like '" + ListedDr_Name + '%' + "' OR a.ListedDr_Name like '" + '%' + ' ' + ListedDr_Name + '%' + "' OR a.ListedDr_Name like '" + '%' + '.' + ListedDr_Name + '%' + "') " +
                       " and a.division_code in (" + div_code + ")  ";
            }

            else
            {
                strQry = " select distinct  d.Division_Name,b.Sf_code,b.Sf_Name,Sf_HQ,sf_Designation_Short_Name,a.Division_Code,a.ListedDrCode,ListedDr_Name,Doc_Qua_Name,Doc_Spec_ShortName, Doc_Cat_ShortName,Doc_Class_ShortName,Territory_Name " +
                           " from mas_listeddr a , mas_salesforce b, Mas_Territory_Creation c, mas_division d where a.sf_code=b.sf_code and cast(c.Territory_Code as varchar)= a.Territory_Code and a.Division_Code=d.Division_Code  " +
                        " and a.ListedDr_Active_Flag=0  and b.sf_TP_Active_Flag=0 and  (a.ListedDr_Name like '" + ListedDr_Name + '%' + "' OR a.ListedDr_Name like '" + '%' + ' ' + ListedDr_Name + '%' + "' OR a.ListedDr_Name like '" + '%' + '.' + ListedDr_Name + '%' + "') " +
                        " and b.sf_code='" + HQ + "' and a.division_code in (" + div_code + ")  ";
            }
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

        public DataSet Bind_ViewdDr(string div_code, string Dr_Code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.sf_emp_id,ListedDr_Name,ListedDr_Address1,ListedDr_Email,ListedDr_Hospital,Hospital_Address,Territory_Name,Doc_Qua_Name,Doc_Spec_ShortName,Doc_Cat_ShortName, Doc_Class_ShortName,ListedDr_Mobile,convert(varchar(10),ListedDr_DOB,103)ListedDr_DOB,convert(varchar(10),ListedDr_DOW,103)ListedDr_DOW,No_of_Visit  " +
                       " from mas_listeddr a Inner join Mas_Territory_Creation c on cast(c.Territory_Code as varchar)= a.Territory_Code  inner join mas_salesforce b on a.Sf_Code=b.sf_code " +
                    " where  a.listeddr_Active_Flag=0 and " +
                    " a.ListedDrCode='" + Dr_Code + "' and a.Division_Code ='" + div_code + "'  ";
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

        public int Update_Order_Booking_Status(string dr_code, int iflag, string div_code, string Trans_SlNo, string Reason, string Remarks)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update trans_Order_Book_Head set Order_Flag=" + iflag + ",reject_Reason='" + Reason + "',reject_Remarks='" + Remarks + "'  where Trans_SlNo='" + Trans_SlNo + "' and division_code='" + div_code + "' and DHP_Code='" + dr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        #region Product_Scheme
        //Added by Preethi
        public DataSet get_Product_Scheme(string State_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct '' as Sl_No,Product_Detail_Code,Product_Code_SlNo,Product_Detail_Name,State_Code,Product_Sale_Unit,'' as Product_Rate,'' Discount_Percentage,'' Scheme_Qty_Fixation,'' Scheme_Qty,'' as Tax_Percentage from mas_product_detail  " +
                   "  where charindex(','+cast('" + State_code + "' as varchar)+',',','+ State_Code) >0 and division_code='" + div_code + "'  and Product_Active_Flag=0 order by Product_Detail_Name";
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


        public DataSet get_Product_Scheme_Date(string State_code, string div_code, string Scheme_For)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "  select ROW_NUMBER() OVER(PARTITION BY State_Code ORDER BY State_Code ) as Filter_No,Effec_Date,State_Code from ( " +
                " select distinct convert(char(11),Effective_From_Date,103)+' to '+ convert(char(11),Effective_To_Date,103) Effec_Date,State_Code  " +
                   "  from mas_product_scheme_fixation  where State_Code='" + State_code + "' and division_code='" + div_code + "' and Scheme_For='" + Scheme_For + "' " +
                   " )tt ";
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


        public DataSet get_Product_Scheme_View(string State_code, string div_code, string EEffec_Dates)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            //strQry = " select * from ( " +
            //    " select distinct ROW_NUMBER() OVER(PARTITION BY Product_Code_SlNo ORDER BY Product_Code_SlNo ) as Filter_No,Sl_No,Effective_From_Date,Effective_To_Date,Product_Code_SlNo,Product_Detail_Name,b.State_Code,Product_Sale_Unit, Product_Rate,  " +
            //        "   Discount_Percentage,Scheme_Qty_Fixation, Scheme_Qty from mas_product_detail a left outer join mas_product_scheme_fixation b on  " +
            //        " a.Product_Active_Flag=0 and a.division_code=a.division_code and a.Product_Code_SlNo=b.Product_Code and   " +
            //        "  charindex(','+cast(b.State_Code as varchar)+',',','+ a.State_Code) >0 where a.division_code='" + div_code + "' and b.State_Code='" + State_code + "' and  " +
            //        "  convert(char(11),Effective_From_Date,103)=LEFT('" + EEffec_Dates + "'+'t', CHARINDEX('t','" + EEffec_Dates + "'+'t')-1) and  " +
            //        "  convert(char(11),Effective_To_Date,103)=replace((replace((right('" + EEffec_Dates + "', len('" + EEffec_Dates + "') - charindex('t', '" + EEffec_Dates + "'))),'o','')),' ','') " +
            //        " ) tt where Filter_No=1 order by Product_Detail_Name ";
            strQry = " select * from ( " +
               " select distinct ROW_NUMBER() OVER(PARTITION BY Product_Code_SlNo ORDER BY Product_Code_SlNo ) as Filter_No,Sl_No,Effective_From_Date,Effective_To_Date,Product_Code_SlNo,Product_Detail_Name,b.State_Code,Product_Sale_Unit, Product_Rate,  " +
                   "   Discount_Percentage,Scheme_Qty_Fixation, Scheme_Qty,Tax_Percentage from mas_product_detail a left outer join mas_product_scheme_fixation b on  " +
                   " a.Product_Active_Flag=0 and a.division_code=a.division_code and a.Product_Code_SlNo=b.Product_Code and   " +
                   "  charindex(','+cast(b.State_Code as varchar)+',',','+ a.State_Code) >0 where a.division_code='" + div_code + "' and b.State_Code='" + State_code + "' and  " +
                   "  (convert(char(11),Effective_From_Date,103)+' to '+ convert(char(11),Effective_To_Date,103))= '" + EEffec_Dates + "' " +
                   " ) tt where Filter_No=1 order by Product_Detail_Name ";
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


        public DataSet get_Product_Scheme_Check(string State_code, string div_code, string From_Dates, string To_Dates, string Scheme_For)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "  select isnull((select distinct count(distinct Product_Code)Product_Code from mas_product_scheme_fixation where convert(char(11),Effective_From_Date,103)='" + From_Dates + "' " +
                    "   and convert(char(11),Effective_To_Date,103)='" + To_Dates + "' and State_Code='" + State_code + "' and Scheme_For='" + Scheme_For + "' and division_code='" + div_code + "' ),0) Product_Code ";
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


        public int RecordAdd_Product_Scheme(string lblPack, string lblProd_Name, string lblProd_Code, string txtRate, string txtDiscount, string txtSchem_Fixa,
        string txtSchem_Qty, string State_Code, DateTime txtEffect_From, DateTime txtEffect_To, string Scheme_For, string div_code, string txtTax)
        {
            int iReturn = -1;
            try
            {
                string From_Date = txtEffect_From.Month.ToString() + "-" + txtEffect_From.Day + "-" + txtEffect_From.Year;
                string To_Date = txtEffect_To.Month.ToString() + "-" + txtEffect_To.Day + "-" + txtEffect_To.Year;

                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from mas_product_scheme_fixation  ";
                int Sl_No = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO mas_product_scheme_fixation(Sl_No,Product_Code,Effective_From_Date,Effective_To_Date,State_Code,Product_Rate,Discount_Percentage, " +
                    " Scheme_Qty_Fixation,Scheme_Qty,Scheme_Type,Scheme_For,division_code,Tax_Percentage,Creation_Date )" +
                    "values('" + Sl_No + "','" + lblProd_Code + "','" + From_Date + "','" + To_Date + "','" + State_Code + "','" + txtRate + "','" + txtDiscount + "', " +
                    " '" + txtSchem_Fixa + "','" + txtSchem_Qty + "','" + "M" + "','" + Scheme_For + "','" + div_code + "','" + txtTax + "',getdate() )";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int RecordUpdate_Product_Scheme(string Sl_No, string lblPack, string lblProd_Name, string lblProd_Code, string txtRate, string txtDiscount, string txtSchem_Fixa,
            string txtSchem_Qty, string State_Code, DateTime txtEffect_From, DateTime txtEffect_To, string Scheme_For, string Div_code, string txtTax)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " Update mas_product_scheme_fixation set Product_Rate='" + txtRate + "',Discount_Percentage='" + txtDiscount + "', " +
                    " Scheme_Qty_Fixation='" + txtSchem_Fixa + "',Scheme_Qty='" + txtSchem_Qty + "',Tax_Percentage='" + txtTax + "'  where  Sl_No='" + Sl_No + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordAdd_Product_Scheme_New(string lblPack, string lblProd_Name, string lblProd_Code, string txtRate, string txtDiscount, string txtSchem_Fixa,
        string txtSchem_Qty, string State_Code, DateTime txtEffect_From, DateTime txtEffect_To, string Scheme_For, string div_code)
        {
            int iReturn = -1;
            try
            {
                string From_Date = txtEffect_From.Month.ToString() + "-" + txtEffect_From.Day + "-" + txtEffect_From.Year;
                string To_Date = txtEffect_To.Month.ToString() + "-" + txtEffect_To.Day + "-" + txtEffect_To.Year;

                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from mas_product_scheme_fixation  ";
                int Sl_No = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO mas_product_scheme_fixation(Sl_No,Product_Code,Effective_From_Date,Effective_To_Date,State_Code,Product_Rate,Discount_Percentage, " +
                    " Scheme_Qty_Fixation,Scheme_Qty,Scheme_Type,Scheme_For,division_code,Creation_Date )" +
                    "values('" + Sl_No + "','" + lblProd_Code + "','" + From_Date + "','" + To_Date + "','" + State_Code + "','" + txtRate + "','" + txtDiscount + "', " +
                    " '" + txtSchem_Fixa + "','" + txtSchem_Qty + "','" + "M" + "','" + Scheme_For + "','" + div_code + "',getdate() )";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int RecordUpdate_Product_Scheme_New(string Sl_No, string lblPack, string lblProd_Name, string lblProd_Code, string txtRate, string txtDiscount, string txtSchem_Fixa,
            string txtSchem_Qty, string State_Code, DateTime txtEffect_From, DateTime txtEffect_To, string Scheme_For, string Div_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " Update mas_product_scheme_fixation set Product_Rate='" + txtRate + "',Discount_Percentage='" + txtDiscount + "', " +
                    " Scheme_Qty_Fixation='" + txtSchem_Fixa + "',Scheme_Qty='" + txtSchem_Qty + "'  where  Sl_No='" + Sl_No + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        #endregion

        public DataSet Get_DetailDrs_Visit_brand(string sf_code, string div_code, string selValues, string mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "exec sp_Detail_Drs_Visitwise_Brand '" + div_code + "','" + selValues + "','" + mode + "'";
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

        public DataSet get_SFHQCode(string sf_cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            //strQry = " select  sf_code,sf_Name from mas_salesforce where sf_cat_code='" + sf_cat_code + "' and sf_code like 'MR%'  ";

            strQry = "select  distinct a.sf_code,a.sf_Name,b.Stockist_Code,b.Stockist_Name,b.Stockist_Designation from mas_salesforce a,mas_stockist b where a.sf_cat_code='"+ sf_cat_code + "' and a.sf_code like 'MR%' and charindex(','+cast(a.sf_code as varchar)+',',','+ b.sf_code) >0  ";

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

        public DataSet get_Stk_SFHQCode(string Sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select distinct Stockist_Code,Stockist_Name,Stockist_Designation from mas_stockist   " +
                " where charindex(','+cast('" + Sf_Code + "' as varchar)+',',','+ sf_code) >0 and Stockist_Active_Flag=0 and Division_Code='" + div_code + "'  order by Stockist_Code ";
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
        //dr Potential by Ferooz
        public DataSet Trans_PotListedDr_Bus_View(string Sf_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Distinct h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,h.ListedDrCode FROM " +
                    " Trans_Doctorwise_Pot_Head h " +
                    " INNER JOIN Trans_Doctorwise_Pot_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
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
        public DataSet getPotListedDrDiv_new(string div_code, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date, d.Territory_Code,  " +
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    "Mas_ListedDr d " +
                    "WHERE d.Division_Code='" + div_code + "' AND d.Sf_Code =  '" + sfcode + "'" +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Name";

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
        public DataSet PotListedDrBus_value(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "Declare @basedOn varchar(50) = '" + basedOn + "' " +
                    " ;with cteVal as (SELECT CASE WHEN @basedOn = 'R' THEN ISNULL((d.Potiental_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'M' THEN ISNULL((d.Potiental_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'D' THEN ISNULL((d.Potiental_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'N' THEN ISNULL((d.Potiental_Quantity * CAST(d.NSR_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'T' THEN ISNULL((d.Potiental_Quantity * CAST(d.Sample_Price AS float)), 0) " +
                    " END AS Val FROM Trans_Doctorwise_Pot_Head h " +
                    " INNER JOIN Trans_Doctorwise_Pot_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "') " +
                    " Select Sum(Val) from cteVal";

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

        public DataSet Trans_ListedPotDr_Bus_HeadExist(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "  Declare @basedOn varchar(50) = '" + basedOn + "' " +
                " SELECT h.Trans_sl_No, h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year, d.ListedDrCode, d.Product_Detail_Name, d.Product_Sale_Unit, d.Potiental_Quantity, " +
                    " CASE WHEN @basedOn = 'R' THEN ISNULL((d.Potiental_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                     " WHEN @basedOn = 'M' THEN ISNULL((d.Potiental_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                     " WHEN @basedOn = 'D' THEN ISNULL((d.Potiental_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                     " WHEN @basedOn = 'N' THEN ISNULL((d.Potiental_Quantity * CAST(d.NSR_Price AS float)), 0)  " +
                     " WHEN @basedOn = 'T' THEN ISNULL((d.Potiental_Quantity * CAST(d.Sample_Price AS float)), 0)  " +
                     " END AS value from Trans_Doctorwise_Pot_Head h INNER JOIN Trans_Doctorwise_Pot_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where  " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND " +
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
        public DataSet Trans_ListedPotDr_Bus_DetailExist(string Sf_Code, string ListedDrCode, string Product_Code, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "  Declare @basedOn varchar(50) = '" + basedOn + "' " +
                     " SELECT h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,d.ListedDrCode, d.Product_Detail_Name, d.Product_Sale_Unit, d.Potiental_Quantity, " +
                     " CASE WHEN @basedOn = 'R' THEN ISNULL((d.Potiental_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                     " WHEN @basedOn = 'M' THEN ISNULL((d.Potiental_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                     " WHEN @basedOn = 'D' THEN ISNULL((d.Potiental_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                     " WHEN @basedOn = 'N' THEN ISNULL((d.Potiental_Quantity * CAST(d.NSR_Price AS float)), 0)  " +
                     " WHEN @basedOn = 'T' THEN ISNULL((d.Potiental_Quantity * CAST(d.Sample_Price AS float)), 0)  " +
                     " END AS value from Trans_Doctorwise_Pot_Head h INNER JOIN Trans_Doctorwise_Pot_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where  " +
                     " h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND d.Product_Code= '" + Product_Code + "' AND  " +
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
        public int RecordAddTrans_ListedPotDrBus_Head(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, string active,
            int ListedDrCode, string ListedDr_Name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "INSERT INTO Trans_Doctorwise_Pot_Head(Sf_Code,Division_Code,Trans_Month,Trans_Year,Created_Date, " +
                        " ListedDrCode, ListedDr_Name) VALUES ('" + Sf_Code + "','" + div_code + "','" + Trans_Month + "','" + Trans_Year + "', " +
                        " getDate(),'" + ListedDrCode + "', '" + ListedDr_Name + "')";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RecordUpdTrans_ListedPotDrBus_Head(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, string active,
            int ListedDrCode)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                DateTime Updated_Date = DateTime.Now;
                string sqlUpdated_Date = Updated_Date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                strQry = "";
                strQry = "UPDATE Trans_Doctorwise_Pot_Head SET Updated_Date='" + sqlUpdated_Date + "' WHERE " +
                        " Sf_Code ='" + Sf_Code + "' AND ListedDrCode='" + ListedDrCode + "' AND Trans_Month ='" + Trans_Month + "' AND " +
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

        public DataSet Trans_ListedPotDr_Bus_HeadSlNo(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Trans_sl_No from Trans_Doctorwise_Pot_Head " +
                    " where Sf_Code ='" + Sf_Code + "' AND ListedDrCode='" + ListedDrCode + "' AND " +
                    " Trans_Month ='" + Trans_Month + "' AND Trans_Year='" + Trans_Year + "' AND Division_Code ='" + div_code + "' ";

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
        public int RecordAddTrans_ListedPotDrBus_Details(decimal Trans_sl_No, string Sf_Code, int div_code, int ListedDrCode, string Product_Code, string Product_Detail_Name,
            string Product_Sale_Unit, string Territory_Code, string Territory_Name, int Potiental_Quantity, double MRP_Price, double Retailor_Price,
            double Distributor_Price, double NSR_Price, double Target_Price, double value)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "INSERT INTO Trans_Doctorwise_Pot_Detail(Trans_sl_No,Division_Code,ListedDrCode,Territory_Code, Territory_Name,Product_Code, Product_Detail_Name, " +
                         " Product_Sale_Unit, Potiental_Quantity,MRP_Price,Retailor_Price,Distributor_Price,NSR_Price,Target_Price,value) " +
                         " VALUES ('" + Trans_sl_No + "','" + div_code + "','" + ListedDrCode + "','" + Territory_Code + "', '" + Territory_Name + "', " +
                         " '" + Product_Code + "', '" + Product_Detail_Name + "', '" + Product_Sale_Unit + "', '" + Potiental_Quantity + "', '" + MRP_Price + "'," + Retailor_Price + ", " +
                         " " + Distributor_Price + ", '" + NSR_Price + "','" + Target_Price + "'," + value + ")";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Delete_ListedPotDrProductDetailsBusiness(string Sf_Code, string ListedDrCode, string Product_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE d FROM Trans_Doctorwise_Pot_Detail d INNER JOIN Trans_Doctorwise_Pot_Head h " +
                    " ON d.Trans_Sl_No = h.Trans_Sl_No WHERE h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND d.Product_Code= '" + Product_Code + "' " +
                    " AND h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "'; ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Delete_ListedPotDrBusEntry(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE d FROM Trans_Doctorwise_Pot_Detail d INNER JOIN Trans_Doctorwise_Pot_Head h " +
                    " ON d.Trans_Sl_No = h.Trans_Sl_No WHERE h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' " +
                    " AND h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "'; " +
                    " DELETE h FROM Trans_Doctorwise_Pot_Head h INNER JOIN Trans_Doctorwise_Pot_Detail d ON h.Trans_Sl_No = d.Trans_Sl_No " +
                    " WHERE h.Sf_Code ='" + Sf_Code + "' AND h.ListedDrCode='" + ListedDrCode + "' AND h.Trans_Month ='" + Trans_Month + "' " +
                    " AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "';";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet ListedPotDrBus_value(string Sf_Code, string ListedDrCode, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "Declare @basedOn varchar(50) = '" + basedOn + "' " +
                    " ;with cteVal as (SELECT CASE WHEN @basedOn = 'R' THEN ISNULL((d.Potiental_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'M' THEN ISNULL((d.Potiental_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'D' THEN ISNULL((d.Potiental_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'N' THEN ISNULL((d.Potiental_Quantity * CAST(d.NSR_Price AS float)), 0) " +
                    "		 WHEN @basedOn = 'T' THEN ISNULL((d.Potiental_Quantity * CAST(d.Sample_Price AS float)), 0) " +
                    " END AS Val FROM Trans_Doctorwise_Pot_Head h " +
                    " INNER JOIN Trans_Doctorwise_Pot_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "') " +
                    " Select Sum(Val) from cteVal";

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
        public DataSet rpListedPotDrBus_value(string Sf_Code, string div_code, int Trans_Month, int Trans_Year, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = " Declare @basedOn varchar(50) = '" + basedOn + "' " +
                    " ;with cteVal as (SELECT CASE WHEN @basedOn = 'R' THEN ISNULL((d.Potiental_Quantity * CAST(d.Retailor_Price AS float)), 0) " +
                    " WHEN @basedOn = 'M' THEN ISNULL((d.Potiental_Quantity * CAST(d.MRP_Price AS float)), 0) " +
                    " WHEN @basedOn = 'D' THEN ISNULL((d.Potiental_Quantity * CAST(d.Distributor_Price AS float)), 0) " +
                    " WHEN @basedOn = 'N' THEN ISNULL((d.Potiental_Quantity * CAST(d.NSR_Price AS float)), 0) " +
                    " WHEN @basedOn = 'T' THEN ISNULL((d.Potiental_Quantity * CAST(d.Target_Price AS float)), 0) " +
                    " END AS Val FROM Trans_Doctorwise_Pot_Head h " +
                    " INNER JOIN Trans_Doctorwise_Pot_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                    " h.Sf_Code ='" + Sf_Code + "' AND " +
                    " h.Trans_Month ='" + Trans_Month + "' AND h.Trans_Year='" + Trans_Year + "' AND h.Division_Code ='" + div_code + "') " +
                    " Select Sum(Val) from cteVal ";

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
        public DataSet rpPrdwiseDrPotBusView(string Sf_Code, string div_code, int Year, int Month, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "EXEC spDocPotLstDrwiseView '" + basedOn + "', '" + div_code + "', '" + Sf_Code + "', '" + Month + "', '" + Year + "'";

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
        public DataSet rpDrwisePotPrdBusView(string Sf_Code, string div_code, int Year, int Month, string state_code, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsValue = null;

            strQry = "EXEC spDocPotPrdwiseView '" + basedOn + "', '" + div_code + "', '" + Sf_Code + "', '" + Month + "', '" + Year + "', '" + state_code + "'";

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

        public DataSet Trans_ListedPotDr_Bus_View(string Sf_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Distinct h.Sf_Code , h.Division_Code,h.Trans_Month,h.Trans_Year,h.ListedDrCode FROM " +
                    " Trans_Doctorwise_Pot_Head h " +
                    " INNER JOIN Trans_Doctorwise_Pot_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
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
        public int RecordUpdTrans_ListedPotDrBus_Details(string Sf_Code, int Trans_Month, int Trans_Year, int div_code, int ListedDrCode, string Product_Code,
    int Potiental_Quantity, double value)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "UPDATE d SET d.Potiental_Quantity='" + Potiental_Quantity + "',d.value='" + value + "' FROM " +
                    " Trans_Doctorwise_Pot_Detail d INNER JOIN Trans_Doctorwise_Pot_Head h ON d.Trans_Sl_No = h.Trans_Sl_No " +
                    " WHERE h.Sf_Code ='" + Sf_Code + "' AND d.ListedDrCode='" + ListedDrCode + "' AND d.Product_Code='" + Product_Code + "' " +
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
        public int GetInput_code()
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
        public DataSet getListedDr_Camp_New(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.ListedDr_Mobile,d.ListedDr_website as Chemists_Code,c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, d.Doc_SubCatCode, " +
            //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
            // " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
            " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
            " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where Division_code='" + div_code + "' and charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
            "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g,Mas_Chemists e " +
            "WHERE d.Sf_Code = '" + sfcode + "'" +
            "and d.Doc_Special_Code = s.Doc_Special_Code " +
            "and d.Doc_ClsCode= dc.Doc_ClsCode " +
            " and d.Doc_QuaCode = g.Doc_QuaCode " +
            "and d.Doc_Cat_Code = c.Doc_Cat_Code " +
            "and d.ListedDr_Active_Flag = 0 and d.Division_code='" + div_code + "'";


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
        public DataTable getListedDoctorList_DataTable_camp_New(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,ListedDr_website as Chemists_Code,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Class_ShortName as Doc_ClsSName,d.Doc_Qua_Name as Doc_QuaName, d.SDP as Activity_Date,ListedDr_Mobile, " +
                        //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name , " +
                        " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  " +
                        " charindex(','+cast(dc.Doc_SubCatCode as varchar)+',',','+d.Doc_SubCatCode)>0 for XML path('')),1,2,'')+', ' Doc_SubCatName  FROM " +

                        " Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " order by 2";
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

        //Added By Gowri
        public DataSet getListedDr_addAgainst_One(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT  d.ListedDrCode,d.ListedDr_Name, max(d.SLVNo) as SLVNo,d.Doc_Cat_ShortName, d.Doc_Spec_ShortName,d.Doc_Qua_Name, " +
                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " case d.ListedDr_Active_Flag when '2' then 'Addition' when '3' then 'Deactivation' end mode, d.ListedDr_Active_Flag " +
                    " FROM Mas_ListedDr_One d WHERE d.sf_code =  '" + sfcode + "' and  " +
                    " d.SLVNo in (select SLVNo from mas_listeddr_one where ListedDr_Active_Flag = 2 and sf_code =  '" + sfcode + "' ) and " +
                    " d.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 and sf_code =  '" + sfcode + "') and d.ListedDr_Active_Flag !=0 and d.ListedDr_Active_Flag !=1  " +
                    " group by SLVNo,d.ListedDrCode,d.ListedDr_Name,d.Territory_Code, d.Sf_Code,d.ListedDr_Active_Flag, d.Doc_Cat_ShortName, d.Doc_Spec_ShortName,d.Doc_Qua_Name  " +
                    " UNION ALL " +
                    " SELECT  d.ListedDrCode,d.ListedDr_Name, max(d.SLVNo) as SLVNo,d.Doc_Cat_ShortName, d.Doc_Spec_ShortName,d.Doc_Qua_Name, " +
                   " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                   " case d.ListedDr_Active_Flag when '2' then 'Addition' when '3' then 'Deactivation' end mode, d.ListedDr_Active_Flag " +
                   " FROM Mas_ListedDr d WHERE d.sf_code =  '" + sfcode + "' and  " +
                   " d.SLVNo in (select SLVNo from mas_listeddr_one where ListedDr_Active_Flag = 2 and sf_code =  '" + sfcode + "' ) and " +
                   " d.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 and sf_code =  '" + sfcode + "') and d.ListedDr_Active_Flag !=0 and d.ListedDr_Active_Flag !=1  " +
                   " group by SLVNo,d.ListedDrCode,d.ListedDr_Name,d.Territory_Code, d.Sf_Code,d.ListedDr_Active_Flag, d.Doc_Cat_ShortName, d.Doc_Spec_ShortName,d.Doc_Qua_Name  ";

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

        public DataSet getListedDr_MGR_Mode_One(string sfcode, string iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name, 'New' as mode " +
                        " from mas_listeddr_One a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "')and a.C_Doctor_Code is null " +
                        " Union " +
                        " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name,'Existing' as mode  " +
                        " from mas_listeddr_One a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "') and a.C_Doctor_Code is not null";
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

        public DataSet getListedDr_MGR_Mode_One(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name, 'New' as mode " +
                        " from mas_listeddr_One a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "')and a.C_Doctor_Code is null " +
                        " Union " +
                        " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name,'Existing' as mode  " +
                        " from mas_listeddr_One a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "') and a.C_Doctor_Code is not null";
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

        public bool RecordExist_One(string Listed_DR_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(ct)from " +
                       "(SELECT ListedDr_Name as ct  FROM Mas_ListedDr_One WHERE ListedDr_Name = '" + Listed_DR_Name + "' AND Sf_Code = '" + sf_code + "' and  ListedDr_Active_Flag = 2 " +
                       "union all " +
                       "SELECT ListedDr_Name as ct  FROM Mas_ListedDr WHERE ListedDr_Name = '" + Listed_DR_Name + "' AND Sf_Code = '" + sf_code + "' and ListedDr_Active_Flag = 0 )a";


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

        public int RecordAdd_Against(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, int SLV_No, string Cat_SName, string Spec_SName, string Cls_SName, string Qual_SName, int flag)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Listed_DR_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_QuaCode,Doc_Cat_Code, " +
                         " Territory_Code, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', '" + Listed_DR_Qual + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', '" + flag + "', getdate(), '" + Division_Code + "','','','', " +
                         " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + SLV_No + "',getdate(),'" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qual_SName + "')";

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
        public int RecordAdd_Against_One(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, int SLV_No, string Cat_SName, string Spec_SName, string Cls_SName, string Qual_SName, int flag)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Listed_DR_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr_One ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_ListedDr_One (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_QuaCode,Doc_Cat_Code, " +
                         " Territory_Code, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', '" + Listed_DR_Qual + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', '" + flag + "', getdate(), '" + Division_Code + "','','','', " +
                         " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + SLV_No + "',getdate(),'" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qual_SName + "')";

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


        public DataSet getListedDr_Add_approve_One(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                         //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr_One d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE " +
                        " d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 2  and d.SLVNo not in " +
                        " (select SLVNo from mas_listeddr_One where ListedDr_Active_Flag = 3 and sf_code = '" + sfcode + "')";



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

        public int Reject_Approve_One(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr_One " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Reject_Flag = 'AR', Listeddr_App_Mgr = '" + sf_name + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }


        public int Add_Approve_Temp(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Exec ListeddrAdd_App_Approved '" + sf_code + "','" + dr_code + "'," + iVal + "," + oVal + ",'" + sf_name + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Add_Against_Approve_Temp(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

        strQry = "Exec ListeddrAdd_Against_Approved '" + sf_code + "','" + dr_code + "'," + iVal + "," + oVal + ",'" + sf_name + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordAddLDr1(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, string DoCatSName, string DoSpecSName, string DocQuaName, string DoClaSName, int iflag)
        {
            int iReturn = -1;

            if (!RecordExist(Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;
                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);


                    strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                             " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                             " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Qua_Name,Doc_Class_ShortName) " +
                             " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                             " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', '" + iflag + "', getdate(), '" + Division_Code + "','','','', " +
                             " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate(),'" + DoCatSName + "','" + DoSpecSName + "','" + DocQuaName + "','" + DoClaSName + "')";


                    iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        //Insert a record into LstDoctor_Terr_Map_History table
                        strQry = "SELECT isnull(max(SNo)+1,'1') SNo from LstDoctor_Terr_Map_History ";
                        int SNo = db.Exec_Scalar(strQry);

                        strQry = "insert into LstDoctor_Terr_Map_History values('" + SNo + "','" + sf_code + "',  '" + Listed_DR_Code + "', " +
                                  " '" + Listed_DR_Terr + "',getdate(),getdate(), '" + Division_Code + "')";

                        iReturn = db.ExecQry(strQry);

                        if (Listed_DR_Terr.IndexOf(",") != -1)
                        {
                            string[] subterr;
                            subterr = Listed_DR_Terr.Split(',');
                            foreach (string st in subterr)
                            {
                                if (st.Trim().Length > 0)
                                {
                                    strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                                    int iPlanNo = db.Exec_Scalar(strQry);

                                    strQry = "insert into Call_Plan values('" + sf_code + "', '" + Convert.ToInt32(st) + "', getdate(), '" + iPlanNo + "', " +
                                            " '" + Listed_DR_Code + "', '" + Division_Code + "', 0,'')";

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
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }

        public DataSet getListDraddagainst_allow_app(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = "Select Add_Deact_Needed from Admin_Setups where Division_Code='" + div_code + "'";
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
        public int RecordAddLDr_One(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, string DoCatSName, string DoSpecSName, string DocQuaName, string DoClaSName, int iflag)
        {
            int iReturn = -1;

            if (!RecordExist_One(Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;
                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr_One ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);


                    strQry = "insert into Mas_ListedDr_One (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                             " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                             " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Qua_Name,Doc_Class_ShortName) " +
                             " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                             " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', '" + iflag + "', getdate(), '" + Division_Code + "','','','', " +
                             " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate(),'" + DoCatSName + "','" + DoSpecSName + "','" + DocQuaName + "','" + DoClaSName + "')";


                    iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        //Insert a record into LstDoctor_Terr_Map_History table
                        strQry = "SELECT isnull(max(SNo)+1,'1') SNo from LstDoctor_Terr_Map_History ";
                        int SNo = db.Exec_Scalar(strQry);

                        strQry = "insert into LstDoctor_Terr_Map_History values('" + SNo + "','" + sf_code + "',  '" + Listed_DR_Code + "', " +
                                  " '" + Listed_DR_Terr + "',getdate(),getdate(), '" + Division_Code + "')";

                        iReturn = db.ExecQry(strQry);

                        if (Listed_DR_Terr.IndexOf(",") != -1)
                        {
                            string[] subterr;
                            subterr = Listed_DR_Terr.Split(',');
                            foreach (string st in subterr)
                            {
                                if (st.Trim().Length > 0)
                                {
                                    strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                                    int iPlanNo = db.Exec_Scalar(strQry);

                                    strQry = "insert into Call_Plan values('" + sf_code + "', '" + Convert.ToInt32(st) + "', getdate(), '" + iPlanNo + "', " +
                                            " '" + Listed_DR_Code + "', '" + Division_Code + "', 0,'')";

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
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }

        public int RecordAdd(string Listed_DR_Name, string DR_Sex, string DR_DOB, string DR_Qual, string DR_DOW, string DR_Spec, string DR_RegNo,
         string DR_Catg, string DR_Terr, string DR_Comm, string DR_Class, string DR_Address1, string DR_Address2, string DR_Address3,
         string DR_State, string DR_Pin, string DR_Mobile, string DR_Phone, string DR_EMail, string DR_Profile, string DR_Visit_Days,
         string DR_DayTime, string DR_IUI, string DR_Avg_Patients, string DR_Hospital, string DR_Class_Patients, string DR_Consultation_Fee,
         string sf_code, string Hospital_Address, string Cat_SName, string Spec_SName, string Cls_SName, string Qua_SName, string ERPcode, int flag,string Pan_card)
        {
            int iReturn = -1;

            if (!RecordExist(Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);


                    strQry = "insert into Mas_ListedDr (ListedDrCode, Sf_Code, ListedDr_Name, ListedDr_Sex, ListedDr_DOB,Doc_QuaCode, " +
                           " ListedDr_DOW, Doc_Special_Code,ListedDr_RegNo, Doc_Cat_Code, Territory_Code, " +
                           " ListedDr_Comm, Doc_ClsCode, ListedDr_Address1, ListedDr_Address2, ListedDr_Address3, " +
                           " State_Code, ListedDr_Pin, ListedDr_Mobile, ListedDr_Phone, ListedDr_EMail, ListedDr_Profile, " +
                           " ListedDr_Visit_Days,ListedDr_DayTime, ListedDr_IUI, ListedDr_Avg_Patients, ListedDr_Hospital, ListedDr_Class_Patients, ListedDr_Consultation_Fee, " +
                           " ListedDr_Created_Date, Division_Code, ListedDR_Sl_No, LastUpdt_Date, ListedDr_Active_Flag, Hospital_Address,SLVNo, Doc_Cat_ShortName, Doc_Spec_ShortName, Doc_Class_ShortName, Doc_Qua_Name,Doctor_ERP_Code,Pan_Card) " +
                           " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + DR_Sex + "', '" + DR_DOB + "', " +
                           " '" + DR_Qual + "', '" + DR_DOW + "', '" + DR_Spec + "', '" + DR_RegNo + "', '" + DR_Catg + "', " +
                           " '" + DR_Terr + "', '" + DR_Comm + "', '" + DR_Class + "', '" + DR_Address1 + "', '" + DR_Address2 + "', '" + DR_Address3 + "', " +
                           " '" + DR_State + "', '" + DR_Pin + "', '" + DR_Mobile + "', '" + DR_Phone + "' , '" + DR_EMail + "' , '" + DR_Profile + "' , " +
                           " '" + DR_Visit_Days + "', '" + DR_DayTime + "', '" + DR_IUI + "', '" + DR_Avg_Patients + "', '" + DR_Hospital + "', " +
                           " '" + DR_Class_Patients + "', '" + DR_Consultation_Fee + "', getdate(), '" + Division_Code + "', '" + Listed_DR_Code + "', getdate(),'" + flag + "', '" + Hospital_Address + "', '" + Listed_DR_Code + "', '" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qua_SName + "', '" + ERPcode + "', '" + Pan_card + "')";


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
        public int RecordAdd_One(string Listed_DR_Name, string DR_Sex, string DR_DOB, string DR_Qual, string DR_DOW, string DR_Spec, string DR_RegNo,
          string DR_Catg, string DR_Terr, string DR_Comm, string DR_Class, string DR_Address1, string DR_Address2, string DR_Address3,
          string DR_State, string DR_Pin, string DR_Mobile, string DR_Phone, string DR_EMail, string DR_Profile, string DR_Visit_Days,
          string DR_DayTime, string DR_IUI, string DR_Avg_Patients, string DR_Hospital, string DR_Class_Patients, string DR_Consultation_Fee,
          string sf_code, string Hospital_Address, string Cat_SName, string Spec_SName, string Cls_SName, string Qua_SName, string ERPcode, int flag,string Pan_card)
        {
            int iReturn = -1;

            if (!RecordExist_One(Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr_One ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);


                    strQry = "insert into Mas_ListedDr_One (ListedDrCode, Sf_Code, ListedDr_Name, ListedDr_Sex, ListedDr_DOB,Doc_QuaCode, " +
                           " ListedDr_DOW, Doc_Special_Code,ListedDr_RegNo, Doc_Cat_Code, Territory_Code, " +
                           " ListedDr_Comm, Doc_ClsCode, ListedDr_Address1, ListedDr_Address2, ListedDr_Address3, " +
                           " State_Code, ListedDr_Pin, ListedDr_Mobile, ListedDr_Phone, ListedDr_EMail, ListedDr_Profile, " +
                           " ListedDr_Visit_Days,ListedDr_DayTime, ListedDr_IUI, ListedDr_Avg_Patients, ListedDr_Hospital, ListedDr_Class_Patients, ListedDr_Consultation_Fee, " +
                           " ListedDr_Created_Date, Division_Code, ListedDR_Sl_No, LastUpdt_Date, ListedDr_Active_Flag, Hospital_Address,SLVNo, Doc_Cat_ShortName, Doc_Spec_ShortName, Doc_Class_ShortName, Doc_Qua_Name,Doctor_ERP_Code,Pan_Card) " +
                           " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + DR_Sex + "', '" + DR_DOB + "', " +
                           " '" + DR_Qual + "', '" + DR_DOW + "', '" + DR_Spec + "', '" + DR_RegNo + "', '" + DR_Catg + "', " +
                           " '" + DR_Terr + "', '" + DR_Comm + "', '" + DR_Class + "', '" + DR_Address1 + "', '" + DR_Address2 + "', '" + DR_Address3 + "', " +
                           " '" + DR_State + "', '" + DR_Pin + "', '" + DR_Mobile + "', '" + DR_Phone + "' , '" + DR_EMail + "' , '" + DR_Profile + "' , " +
                           " '" + DR_Visit_Days + "', '" + DR_DayTime + "', '" + DR_IUI + "', '" + DR_Avg_Patients + "', '" + DR_Hospital + "', " +
                           " '" + DR_Class_Patients + "', '" + DR_Consultation_Fee + "', getdate(), '" + Division_Code + "', '" + Listed_DR_Code + "', getdate(), '" + flag + "', '" + Hospital_Address + "', '" + Listed_DR_Code + "', '" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qua_SName + "', '" + ERPcode + "', '" + Pan_card + "')";


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

        public DataSet getListedDr_MGR_One(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (div_code.Contains(","))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr_One a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in(" + div_code + ")";
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

        public DataSet getListedDr_adddeact_One(string sf_code, int val1, int val2, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name " +
                   " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c  " +
                   " where c.LstDr_AM = '" + sf_code + "' and  a.Sf_Code = b.Sf_Code " +
                   " and b.Sf_Code=c.Sf_Code and  a.SLVNo in (select SLVNo from mas_listeddr_one v where ListedDr_Active_Flag = 2  and v.Division_code = '" + div_code + "' and v.sf_code=a.sf_code) and " +
                   " a.SLVNo in (select SLVNo from mas_listeddr g where ListedDr_Active_Flag = 3  and g.Division_code = '" + div_code + "' and g.sf_code=a.sf_code) ";
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


        //public DataSet FetchChemist(string sf_code, string div_code)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsChe = null;


        //    strQry = " SELECT '' as Chemists_Code, '---Select---' as Chemists_Name " +
        //            " UNION " +
        //            "SELECT Chemists_Code,Chemists_Name, " +
        //            "FROM  Mas_Chemists where Division_Code = '" + div_code + "' AND Sf_Code = '" + sf_code + "'" +
        //            " and Chemists_Active_Flag = 0 ";


        //    try
        //    {
        //        dsChe = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsChe;
        //}

        public DataSet GetTerritory_Upload_Deact(string Territory_Name, string SF_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Territory_Code from Mas_Territory_Creation where Territory_Name='" + Territory_Name + "' and SF_Code = '" + SF_Code + "' and Division_Code = '" + div_code + "' ";

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
        public int GetCls_code()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Doc_ClsCode)+1,'1') Doc_ClsCode from Mas_Doc_Class ";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //ADDED by GOWRI
        //SVL NO
        public DataSet getListedDrforName_SVL(string sfcode, string div_code, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                     " into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                      "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName, d.Doc_Qua_Name, " +
                           //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date " +
                        " FROM " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date,convert(varchar(11),ListedDr_Created_Date,103) ListedDr_Created_Date     FROM " +
                        "Mas_ListedDr d, #doctor e " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        " and d.ListedDr_Active_Flag = 0 and d.Sf_Code = e.Sf_Code and  d.ListedDrCode=e.ListedDrCode" +
                        "drop table #doctor ";
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
        //cat
        public DataSet Cat_OneVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;



            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                  "into #doctor from Mas_ListedDr where division_code ='" + div_code + "' and ListedDr_Active_Flag = 0   " +
                  "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                  " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                  ",#doctor e" +
                  " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Cat_Code='" + cat_code + "' " +
                  " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                  " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                  " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                  " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                  " having count(b.Trans_Detail_Info_Code) =1 " +
                  " DROP TABLE #doctor ";
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
        public DataSet Cat_TwoVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                           "into #doctor from Mas_ListedDr where division_code ='" + div_code + "' and ListedDr_Active_Flag = 0   " +
                           "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                           " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                           " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                           ",#doctor e" +
                           " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Cat_Code='" + cat_code + "' " +
                           " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                           " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                           " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                           " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                           " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                           " having count(b.Trans_Detail_Info_Code) = 2 " +
                           " DROP TABLE #doctor ";

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
        public DataSet Cat_ThreeVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                   "into #doctor from Mas_ListedDr where division_code ='" + div_code + "' and ListedDr_Active_Flag = 0   " +
                   "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                   " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                   " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                   " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                   ",#doctor e" +
                   " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Cat_Code='" + cat_code + "' " +
                   " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                   " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                   " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                   " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                   " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                   " having count(b.Trans_Detail_Info_Code) = 3 " +
                   " DROP TABLE #doctor ";

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
        public DataSet Cat_MoreVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                    "into #doctor from Mas_ListedDr where division_code ='" + div_code + "' and ListedDr_Active_Flag = 0   " +
                    "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                    " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                    " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                    " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                    ",#doctor e" +
                    " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Cat_Code='" + cat_code + "' " +
                    " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                    " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                    " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                    " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                    " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                    " having count(b.Trans_Detail_Info_Code) > 3 " +
                    " DROP TABLE #doctor ";

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

        //Spec 
        public DataSet Spec_OneVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string spec_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;




            strQry = " select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                     "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                     "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                     " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     ",#doctor e" +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Special_Code='" + spec_code + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 1 " +
                     " DROP TABLE #doctor ";

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
        public DataSet Spec_TwoVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string spec_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                                "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                                "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                                " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                                " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                                " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                                ",#doctor e" +
                                " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Special_Code='" + spec_code + "' " +
                                " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                                " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                                " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                                " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                                " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                                " having count(b.Trans_Detail_Info_Code) = 2 " +
                                " DROP TABLE #doctor ";



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
        public DataSet Spec_ThreeVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string spec_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                      "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                      "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                      " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                      " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                      " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                      ",#doctor e" +
                      " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Special_Code='" + spec_code + "' " +
                      " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                      " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                      " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                      " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                      " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                      " having count(b.Trans_Detail_Info_Code) = 3" +
                      " DROP TABLE #doctor ";


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
        public DataSet Spec_MoreVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string spec_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                                "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                                "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                                " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                                " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                                " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                                ",#doctor e" +
                                " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_Special_Code='" + spec_code + "' " +
                                " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                                " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                                " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                                " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                                " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                                " having count(b.Trans_Detail_Info_Code) > 3 " +
                                " DROP TABLE #doctor ";
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

        //Class 
        public DataSet Cls_OneVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string cls_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;



            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                  "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                  "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                  " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                  ",#doctor e" +
                  " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_ClsCode='" + cls_code + "' " +
                  " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                  " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                  " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                  " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                  " having count(b.Trans_Detail_Info_Code) = 1 " +
                  " DROP TABLE #doctor ";







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
        public DataSet Cls_TwoVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string cls_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
               "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
               "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
               " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

               " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
               " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
               ",#doctor e" +
               " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_ClsCode='" + cls_code + "' " +
               " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
               " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
               " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
               " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
               " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
               " having count(b.Trans_Detail_Info_Code) = 2 " +
               " DROP TABLE #doctor ";

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
        public DataSet Cls_ThreeVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string cls_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                         "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                         "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                         " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                         " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                         " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                         ",#doctor e" +
                         " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_ClsCode='" + cls_code + "' " +
                         " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                         " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                         " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                         " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                         " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                         " having count(b.Trans_Detail_Info_Code) = 3 " +
                         " DROP TABLE #doctor ";
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
        public DataSet Cls_MoreVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string cls_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                  "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                  "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                  " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                  ",#doctor e" +
                  " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_ClsCode='" + cls_code + "' " +
                  " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                  " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                  " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                  " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                  " having count(b.Trans_Detail_Info_Code) > 3 " +
                  " DROP TABLE #doctor ";
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

        //Qual 
        public DataSet Qua_OneVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string Qua_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;



            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                 "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                 "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                 " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                 //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                 //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                 " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                 " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                 ",#doctor e" +
                 " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_QuaCode='" + Qua_code + "' " +
                 " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                 " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                 " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                 " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                 " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                 " having count(b.Trans_Detail_Info_Code) = 1 " +
                 " DROP TABLE #doctor ";


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
        public DataSet Qua_TwoVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string Qua_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                  "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                  "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                  " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                  ",#doctor e" +
                  " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_QuaCode='" + Qua_code + "' " +
                  " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                  " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                  " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                  " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                  " having count(b.Trans_Detail_Info_Code) = 2 " +
                  " DROP TABLE #doctor ";
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
        public DataSet Qua_ThreeVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string Qua_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                           "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                           "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                           " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                           " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                           ",#doctor e" +
                           " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_QuaCode='" + Qua_code + "' " +
                           " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                           " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                           " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                           " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                           " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                           " having count(b.Trans_Detail_Info_Code) = 3 " +
                           " DROP TABLE #doctor ";

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
        public DataSet Qua_MoreVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string Qua_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                           "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                           "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                           " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +

                           " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                           " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                           ",#doctor e" +
                           " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and  d.Doc_QuaCode='" + Qua_code + "' " +
                           " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                           " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                           " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                           " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                           " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                           " having count(b.Trans_Detail_Info_Code) > 3 " +
                           " DROP TABLE #doctor ";
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

        //Territory 
        public DataSet Terr_OneVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "  select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                    " into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                    "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                    " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                  //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                  //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                  " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                  " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                 ",#doctor e" +
                 " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and   d.Territory_Code='" + Terr_code + "' " +
                " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                " having count(b.Trans_Detail_Info_Code) = 1 " +
                " DROP TABLE #doctor ";


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
        public DataSet Terr_TwoVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "  select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                               " into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                               "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                               " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                             //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                             //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                             " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                             " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                            ",#doctor e" +
                            " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and   d.Territory_Code='" + Terr_code + "' " +
                           " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                           " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                           " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                           " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                           " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                           " having count(b.Trans_Detail_Info_Code) = 2 " +
                           " DROP TABLE #doctor ";
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
        public DataSet Terr_ThreeVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;



            strQry = "  select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                                 " into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                                 "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                                 " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                               //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                               //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                               " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                               " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                              ",#doctor e" +
                              " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and   d.Territory_Code='" + Terr_code + "' " +
                             " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                             " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                             " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                             " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                             " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                             " having count(b.Trans_Detail_Info_Code) = 3 " +
                             " DROP TABLE #doctor ";
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
        public DataSet Terr_MoreVisit_SVL(string sf_code, string div_code, int iMonth, int iYear, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "  select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                       " into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                        "select d.ListedDrCode,e.svl_no,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                          " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                          //" stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                          //" t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                          " stuff((select '~ '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                           " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                            ",#doctor e" +
                              " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' and   d.Territory_Code='" + Terr_code + "' " +
                             " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                             " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                             " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.ListedDrCode=e.ListedDrCode and d.Sf_Code=e.sf_code " +
                             " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,e.svl_no, " +
                             " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                             " having count(b.Trans_Detail_Info_Code) > 3 " +
                             " DROP TABLE #doctor ";
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

        public DataSet GetImage_Check_Dr(string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;



            strQry = " select Visiting_Card  from mas_listeddr a where ListedDrCode= '" + drcode + "'";

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
        //ENDED BY GOWRI
        public int DocProd_RecordUpdate_App(string Listeddr_Code, string Product_Code, string Sf_Code, string Division_Code)
        {
            int iReturn1 = -1;


            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC Listeddr_Product_Map_Update_Mas_Listeddr '" + Listeddr_Code + "','" + Division_Code + "','" + Sf_Code + "','" + Product_Code + "' ";

                iReturn1 = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn1;
        }

        public DataSet BusinessRange(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select slno,	From_Range,	To_Range,	Division_code from Mas_DrBusiness_Range WHERE Division_code='" + divcode + "' ";

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

        public DataSet getGrid_DR_MPL(string Div_Code, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " select a.sf_code,a.listedDrcode, a.ListedDr_Name,a.Doc_Cat_ShortName,a.Doc_Spec_ShortName,c.Territory_Name from mas_listeddr a , mas_salesforce b,mas_territory_Creation c  " +
                     " where  a.sf_code=b.sf_code and cast((a.territory_code) as Numeric)=c.territory_code and a.division_code='" + Div_Code + "' and a.sf_code='" + sfcode + "' " +
                     " and listeddr_Active_Flag=0 order by a.ListedDr_Name";

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

        public int Delete_ProductMapUnselected(string Listeddr_Code, string Product_Code, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "delete from Map_LstDrs_Product  " +
                    " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code not in (" + Product_Code + ") ";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int Delete_DrProductMapUnselected(string Listeddr_Code, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                if (ClassicDocProd_RecordExist(Listeddr_Code, Sf_Code, Division_Code))
                {
                    strQry = "delete from Map_LstDrs_Product  " +
                    " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Division_Code='" + Division_Code + "' ";

                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool ClassicDocProd_RecordExist(string Listeddr_Code, string Sf_Code, string Division_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Map_LstDrs_Product " +
                         " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Division_Code='" + Division_Code + "' ";

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

        public DataSet Trans_DrBus_Valuewise_RecordExistNew(string Listeddr_Code, string Sf_Code, string div_code, int Trans_Month, int Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            
            strQry = " SELECT h.Sf_Code, h.Division_Code,h.Trans_Month,h.Trans_Year,h.ListedDr_Code, h.ListedDr_Name, h.Business_Value FROM " +
                     " Trans_Dr_Business_Valuewise_Head h where h.Sf_Code = '" + Sf_Code + "' AND h.ListedDr_Code = '" + Listeddr_Code + "' AND Trans_Month = '" + Trans_Month + "' AND Trans_Year = '" + Trans_Year + "' AND h.Division_Code = '" + div_code + "'";
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
        public int RecordAdd_ProductMap_New_Brand(string Listeddr_Code, string Product_Code, string Doctor_Name, string Sf_Code, string Division_Code, string ddlPriority)
        {
            int iReturn = -1;
            DataSet dsPrd_Name;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Sl_No = -1;
                //string Product_Name = string.Empty;

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_LstDrs_Product_Brand ";
                Sl_No = db.Exec_Scalar(strQry);

                //strQry = "select Product_Detail_Name  from Mas_Product_Detail where Division_Code='" + Division_Code + "' and Product_Code_SlNo='" + Product_Code + "' ";
                //dsPrd_Name = db.Exec_DataSet(strQry);

                strQry = "select Product_Brd_Name as Product_Detail_Name  from mas_product_brand where Division_Code='" + Division_Code + "' and Product_Brd_Code='" + Product_Code + "' ";
                dsPrd_Name = db.Exec_DataSet(strQry);



                if (DocProd_RecordExist_New_brand(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "update Map_LstDrs_Product_Brand set Product_Code ='" + Product_Code + "',Product_Name='" + dsPrd_Name.Tables[0].Rows[0][0].ToString() + "', Product_Priority='" + ddlPriority + "'  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "'  ";
                }
                else
                {

                    strQry = " insert into Map_LstDrs_Product_Brand (Sl_No,Listeddr_Code,Product_Code,Product_Name,Doctor_Name, " +
                       " Sf_Code,Division_Code,Created_Date,Product_Priority) " +
                       " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + Product_Code + "', '" + dsPrd_Name.Tables[0].Rows[0][0].ToString() + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                       " '" + Division_Code + "',  getdate(),'" + ddlPriority + "' )";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int DocProd_RecordUpdate_brand(string Listeddr_Code, string Product_Code, string Sf_Code, string Division_Code)
        {
            int iReturn1 = -1;


            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_ListedDr set Map_LstDrs_Product_Brand ='" + Product_Code + "' " +
                         " where ListedDrCode = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Division_Code ='" + Division_Code + "'  ";

                iReturn1 = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn1;
        }
        public bool DocProd_RecordExist_New_brand(string Listeddr_Code, string Sf_Code, string Product_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Map_LstDrs_Product_Brand " +
                         " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";

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
        public bool DocProd_RecordExist_brand(string Listeddr_Code, string Sf_Code, string Product_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Map_LstDrs_Product_Brand " +
                         " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";

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
        public int Delete_ProductMap_brand(string Listeddr_Code, string Product_Code, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                if (DocProd_RecordExist_brand(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "delete from Map_LstDrs_Product_Brand  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";
                }


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



