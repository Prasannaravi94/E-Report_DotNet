using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class DCR
    {
        private string strQry = string.Empty;

        public DataSet getWorkType(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select '0' WorkType_Code_M, '---Select---' Worktype_Name_M UNION select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr where active_flag=0 and TP_DCR like '%D%'" +
                     " and division_code = '" + div_code + "'  ORDER BY 2";
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

        public DataSet AuditLogin_Details(string sf_code, string div_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_loacalAudit  '" + sf_code + "', '" + div_code + "' ," + imonth + "," + iyear + "";

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
        public DataSet AuditLogin_DetailsMode(string sf_code, string div_code, int imonth, int iyear, string Mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_loacalAuditModewise  '" + sf_code + "', '" + div_code + "' ," + imonth + "," + iyear + ",'" + Mode + "'";

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

        //public DataSet getWorkType_MRDCR(string div_code)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsTP = null;
        //    strQry = "select '0' WorkType_Code_B, '---Select---' Worktype_Name_B UNION select WorkType_Code_B,Worktype_Name_B from Mas_WorkType_BaseLevel where active_flag=0 and TP_DCR like '%D%'" +
        //              " and division_code = '" + div_code + "'  ORDER BY 2";
        //    try
        //    {
        //        dsTP = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsTP;
        //}
        public DataSet getWorkType_MRDCR(string div_code, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            if (sf_type == "1")
            {
                strQry = "select '0' WorkType_Code_B, '---Select---' Worktype_Name_B UNION select WorkType_Code_B,Worktype_Name_B from Mas_WorkType_BaseLevel where active_flag=0 and TP_DCR like '%D%'" +
                          " and division_code = '" + div_code + "'  ORDER BY 2";
            }
            else if (sf_type == "2")
            {
                strQry = "select '0' WorkType_Code_B, '---Select---' Worktype_Name_B UNION select WorkType_Code_M as WorkType_Code_B,Worktype_Name_M as Worktype_Name_B from Mas_WorkType_Mgr where active_flag=0 and TP_DCR like '%D%'" +
                    " and division_code = '" + div_code + "'  ORDER BY 2";
            }
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
        public string get_SFName(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTerr = null;
            string sReturn = string.Empty;
            strQry = "select Sf_Name from Mas_Salesforce where sf_code = '" + sf_code + "' ";
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

        public DataSet getTerrHQ(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC sp_get_Rep '" + sf_code + "' ";

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

        public DataSet getTerrHQ_DCR(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_get_Rep_MGRDCR] '" + sf_code + "' ";

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


        public DataSet LoadWorkwith(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " Select Sf_Code,'SELF' Sf_Name, 'ZZZZ' Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' " +
                         " UNION" +
                         " Select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // AM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                         " UNION" +
                         " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF from Mas_Salesforce " + // RM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                         " UNION " +
                         " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                         " UNION " +
                         " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF from Mas_Salesforce " + // ZM Level
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

        public DataSet getProducts(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select '0' Product_Detail_Code, '-Product-' Product_Detail_Name UNION select b.Prod_Detail_Sl_No as Product_Detail_Code,b.Product_Detail_Name " +
                     " from Mas_Salesforce a, Mas_Product_Detail b " +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and cast(b.Division_Code as varchar) = '" + divcode + "' " +
                     " and b.Product_Active_Flag=0 and " +
                     "(b.state_code like cast(a.state_code as varchar) +','+'%' or b.state_code like '%'+','+ cast(a.state_code as varchar) +','+'%')" +
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

        public DataSet getGift(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select '0' Gift_Code, '-Select-' Gift_Name UNION select a.Gift_Code,a.Gift_Name " +
                     " from mas_gift a, mas_salesforce b" +
                     " Where cast(a.Division_Code as varchar) = '" + divcode + "' " +
                     " and a.Gift_Active_Flag=0 " +
                     " and b.Sf_Code = '" + sf_code + "' " +
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

        public DataSet getProducts_MGR(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select '0' Product_Detail_Code, '-Product-' Product_Detail_Name UNION select b.Product_Detail_Code,b.Product_Detail_Name " +
                     " from Mas_Salesforce a, Mas_Product_Detail b " +
                     " where a.Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
                     " and b.Product_Active_Flag=0 and " +
                       "(b.state_code like cast(a.state_code as varchar) +','+'%' or b.state_code like '%'+','+ cast(a.state_code as varchar) +','+'%')" +
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
        public DataSet getGift_MGR(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select '0' Gift_Code, '-Select-' Gift_Name UNION select a.Gift_Code,a.Gift_Name " +
                     " from mas_gift a, mas_salesforce b" +
                     " where a.Gift_Active_Flag=0 " +
                     " and b.Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
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

        public int RecordAdd_Header(string SF_Code, string sf_name, string emp_id, string employee_id, string Activity_Date, string Work_Type, string SDP, string SDP_Name, string sRemarks, string vConf1, string dcrdate, bool reentry, bool isdelayrel, string vConf, string Start_Date, string WorkType_name)
        {
            int iReturn = -1;
            int iReturnmax = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE Division_Code = '" + Division_Code + "' and  Sf_Code = '" + SF_Code + "' and Activity_Date ='" + Activity_Date + "' ";
                Trans_SlNo = db.Exec_Scalar(strQry);

                if (Trans_SlNo > 0)
                {
                    strQry = "delete from DCRDetail_Lst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_CSH_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_UnLst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRmain_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);
                }
                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Main as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_SlNo as bigint)),0)+1 FROM DCRMain_Temp";
                    Trans_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRMain_Temp (Trans_SlNo, Sf_Code,sf_name,emp_id,employee_id, Activity_Date, Submission_Date, Work_Type,Plan_No,Plan_Name, Division_Code, Remarks, confirmed,Start_Time,End_Time,WorkType_Name) " +
                       " VALUES('" + Trans_SlNo + "', '" + SF_Code + "','" + sf_name + "', '" + emp_id + "','" + employee_id + "','" + Activity_Date + "', getdate(), '" + Work_Type + "', '" + SDP + "', '" + SDP_Name + "', '" + Division_Code + "', '" + sRemarks + "','" + vConf1 + "','" + Start_Date + "', getdate(), '" + WorkType_name + "')";

                iReturn = db.ExecQry(strQry);

                if (iReturn > 0)
                {
                    iReturn = Trans_SlNo; //Inorder to maintain the same sl_no on detail table

                    if (vConf == "1")
                    {
                        if (Trans_SlNo > 1)
                        {
                            strQry = "update DCR_MaxSlNo set Max_Sl_No_Main = '" + Trans_SlNo + "' ";
                            iReturnmax = db.ExecQry(strQry);
                        }
                        else
                        {
                            if (reentry == false)
                            {
                                strQry = "insert into DCR_MaxSlNo  VALUES('" + Division_Code + "','" + Trans_SlNo + "', '0')";
                                iReturnmax = db.ExecQry(strQry);
                            }
                        }
                        //if (isdelayrel == true)
                        //{
                        //    int iReturndel = -1;
                        //    strQry = " Update DCR_Delay_Dtls  set Delayed_Flag =  2 where Sf_Code= '" + SF_Code + "' and Delayed_Date = '" + Activity_Date + "'";

                        //    iReturndel = db.ExecQry(strQry);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int RecordAdd_Detail(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string DCR_Session, string DCR_Time, string Worked_With_Code, string Worked_With_Name, string Prod_Detail, string Gift_Code, string Gift_Name, string GQty, string SDP, string vConf, string sess_code, string minutes, string seconds, string product_detail_code, string gift_detail_code, string Add_Prod_Detail, string Add_Prod_Code, string Add_Gift_Detail, string Add_Gift_Code, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_Lst_Temp";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRDetail_Lst_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, Session, Session_Code,Time,Minutes," +
                         " Seconds,Worked_with_Code, Worked_with_Name, Product_Detail,Product_Code, Additional_Prod_Code,Additional_Prod_Dtls,Gift_Code,Gift_Name,Gift_Qty,Additional_Gift_Code,Additional_Gift_Dtl, SDP, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + DCR_Session + "','" + sess_code + "', '" + DCR_Time + "', '" + minutes + "', '" + seconds + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', " +
                         " '" + Prod_Detail + "', '" + product_detail_code + "','" + Add_Prod_Code + "', '" + Add_Prod_Detail + "','" + Gift_Code + "', '" + Gift_Name + "','" + GQty + "', '" + Add_Gift_Code + "','" + Add_Gift_Detail + "','" + SDP + "',  '" + Division_Code + "',  '" + Trans_Detail_Name + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "' ";
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
        public int RecordAdd_Detail_Chem(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB_Value, string Worked_With_Code, string Worked_With_Name, string SDP, string vConf, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_CSH_Temp";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRDetail_CSH_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB ," +
                         " Worked_with_Code, Worked_with_Name, SDP, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB_Value + "',  '" + Worked_With_Code + "', '" + Worked_With_Name + "',  '" + SDP + "', '" + Division_Code + "', '" + Trans_Detail_Name + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "' ";
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

        public int RecordAdd_Detail_Stockiest(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB, string Worked_With_Code, string Worked_With_Name, string SDP, string Visit_Type, string vConf, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_CSH_Temp";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                strQry = "insert into DCRDetail_CSH_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Visit_Type, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', '" + SDP + "',  '" + Visit_Type + "', '" + Division_Code + "', '" + Trans_Detail_Name + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
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


        public int RecordAdd_Detail_Unlst(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string DCR_Session, string DCR_Time, string Worked_With_Code, string Worked_With_Name, string Prod_Detail, string Gift_Code, string Gift_Name, string GQty, string SDP, string vConf, string sess_code, string minutes, string seconds, string product_detail_code, string gift_detail_code, string Add_Prod_Detail, string Add_Prod_Code, string Add_Gift_Detail, string Add_Gift_Code, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_Unlst_Temp ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRDetail_Unlst_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, Session, Session_Code,Time,Minutes," +
                         " Seconds,Worked_with_Code, Worked_with_Name, Product_Detail,Product_Code, Additional_Prod_Code,Additional_Prod_Dtls,Gift_Code,Gift_Name,Gift_Qty,Additional_Gift_Code,Additional_Gift_Dtl, SDP, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + DCR_Session + "','" + sess_code + "', '" + DCR_Time + "', '" + minutes + "', '" + seconds + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', " +
                         " '" + Prod_Detail + "', '" + product_detail_code + "','" + Add_Prod_Code + "', '" + Add_Prod_Detail + "','" + Gift_Code + "', '" + Gift_Name + "','" + GQty + "', '" + Add_Gift_Code + "','" + Add_Gift_Detail + "','" + SDP + "',  '" + Division_Code + "',  '" + Trans_Detail_Name + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
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

        public int RecordAdd_Detail_Hosp(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB_Value, string Worked_With_Code, string Worked_With_Name, string SDP, string vConf, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_CSH_Temp ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRDetail_CSH_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB_Value + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "',  '" + SDP + "', '" + Division_Code + "' ,'" + Trans_Detail_Name + "')";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
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
        public DataSet getChemists(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sf_code + "' " +
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

        //public DataSet getStockiest(string sf_code)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsProCat = null;

        //    strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,ltrim(Stockist_Name) " +
        //             " from Mas_Stockist " +
        //             " where Stockist_Active_Flag=0 " +
        //             " and Sf_Code like '%" + sf_code + "%'" +
        //             " ORDER BY 2";
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


        public DataSet getStockiest_SSentry(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,ltrim(Stockist_Name) " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' and Division_code = '" + div_code + "'" +
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

        public DataSet getHospital(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Hospital_Code, '---Select---' Hospital_Name UNION select Hospital_Code,Hospital_Name " +
                     " from Mas_Hospital " +
                     " where Hospital_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' " +
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

        public DataSet getChemists(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
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


        public DataSet getStockiest_SS(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,ltrim(Stockist_Name) " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' and Division_code = '" + div_code + "'" +
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

        public DataSet getStockiest(string sf_code, string division_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,Stockist_Name " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  division_code = '" + division_code + "')" +
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

        public DataSet getHospital(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Hospital_Code, '---Select---' Hospital_Name UNION select Hospital_Code,Hospital_Name " +
                     " from Mas_Hospital " +
                     " where Hospital_Active_Flag=0 " +
                     " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
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

        public DataSet getMR(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " Select '0' Sf_Code, '-Select-' Sf_Name " +
                     " UNION " +
                     " Select a.Sf_Code,a.Sf_Name   from Mas_Salesforce a,DCR_MGR_WorkAreaDtls b" +
                     " where a.sf_TP_Active_Flag = 0 and  a.Sf_Code = b.sf_code " +
                     " and b.MGR_Code = '" + sf_code + "' and  b.DCR_Date = '" + DCRDate + "' " +
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
        public int isTerritoryDoctor(string sf_code, string dr_code, string terr_code)// Modified by Sri - 6 Aug
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                            " where ListedDrCode = '" + dr_code + "' and Sf_Code='" + sf_code + "' and Territory_Code= '" + terr_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataTable getListedDoctor(string sfcode, string SName, string doc_disp)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                if (doc_disp == "1")// DR Name
                {

                    strQry = "SELECT ListedDrCode, ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND ListedDr_Name like + '" + SName + "%' Order By 2";

                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = "SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                           " FROM Mas_ListedDr " +
                           " WHERE Sf_Code =  '" + sfcode + "' " +
                           " AND ListedDr_Active_Flag = 0 " +
                           " AND ListedDr_Name like + '" + SName + "%' Order By 2";


                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = "SELECT a.ListedDrCode, " +
                        "a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                             " WHERE a.Sf_Code =  '" + sfcode + "' " +
                          " AND a.ListedDr_Active_Flag = 0 " +
                          " AND a.ListedDr_Name like + '" + SName + "%' Order By 2";

                }
                else if (doc_disp == "4")//Category

                {
                    strQry = "SELECT a.ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 " +
                           " AND a.ListedDr_Name like + '" + SName + "%' Order By 2";

                }
                else if (doc_disp == "5")//Class

                {
                    strQry = "SELECT a.ListedDrCode, " +
                               " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 " +
                           " AND a.ListedDr_Name like + '" + SName + "%' Order By 2";

                }
            }
            else
            {
                if (doc_disp == "1")// DR Name
                {

                    strQry = "SELECT ListedDrCode, ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 Order By 2";

                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = "SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                           " FROM Mas_ListedDr " +
                           " WHERE Sf_Code =  '" + sfcode + "' " +
                           " AND ListedDr_Active_Flag = 0 Order By 2";


                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = "SELECT a.ListedDrCode, " +
                        "a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                             " WHERE a.Sf_Code =  '" + sfcode + "' " +
                          " AND a.ListedDr_Active_Flag = 0 Order By 2";

                }
                else if (doc_disp == "4")//Category
                {
                    strQry = "SELECT a.ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 Order By 2";

                }
                else if (doc_disp == "5")//Class
                {
                    strQry = "SELECT a.ListedDrCode, " +
                               " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 Order By 2";

                }
            }

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

        //Added by sri - Manager DCR Doc

        public DataTable getListedDoctorMGR(string sfcode, string SName, string doc_disp, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                if (doc_disp == "1")// DR Name
                {
                    strQry = " select ListedDrCode,ListedDr_Name,Territory_Code" +
                           " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                            " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                                " union all " +
                               " select  ListedDrCode, ListedDr_Name,Territory_Code" +
                               " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                                " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                               " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'";

                    //strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //          " UNION SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                    //           " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //          " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                    //          " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //          " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //             " UNION ALL " +
                    //             " SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //             " order by Territory_Code";
                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%' " +
                               " union all " +
                              " select ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'";
                    //strQry =  " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //            " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //             " UNION  ALL" +
                    //             " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //            " order by Territory_Code";
                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                               " union all " +
                              " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%' ";

                    //strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //           " UNION SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                    //           " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                    //           " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                    //           " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                    //           " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //              " UNION ALL " +
                    //              " SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //             " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //             " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //             " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //             " order by Territory_Code";

                }
                else if (doc_disp == "4")//Category
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%' " +
                               " union all " +
                              " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'";

                    //strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //          " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                    //          "  DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                    //         " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                    //          " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                    //          " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //             " UNION ALL " +
                    //             " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //             " order by Territory_Code";
                }
                else if (doc_disp == "5")//Class
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                         " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                          " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%' " +
                              " union all " +
                             " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                             " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                              " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'";

                    //strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //          " UNION SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                    //          " Mas_Doc_Class b, DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                    //          " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +    
                    //          " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                    //          " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //             " UNION  ALL" +
                    //             " SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //            " order by Territory_Code";
                }
            }
            else
            {
                if (doc_disp == "1")// DR Name
                {

                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                              " UNION SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                              " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                              " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                              " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                              " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL" +
                                 " SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " order by Territory_Code";

                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                         " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                         " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                         " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                         " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                         " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL " +
                                  "SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " order by Territory_Code";
                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                      " UNION SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                      " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                      " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                      " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL " +
                                  "SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                " order by Territory_Code";
                }
                else if (doc_disp == "4")//Category
                {
                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                              " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                              " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                              " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                              " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                              " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL" +
                                  "SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " order by Territory_Code";


                }
                else if (doc_disp == "5")//Class
                {
                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                                " UNION SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                                " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                                " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL" +
                                 " SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " order by Territory_Code";
                }
            }

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

        public DataTable getChe_src(string sfcode, string SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                   " from Mas_Chemists " +
                   " where Chemists_Active_Flag=0 " +
                   " and Sf_Code = '" + sfcode + "' " +
                   " and Chemists_Name like + '" + SName + "%' Order By 2";
            }

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
        public DataTable getChe_srcMGR(string sfcode, string SName, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                   " from Mas_Chemists " +
                   " where Chemists_Active_Flag=0 " +
                   " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "') " +
                   " and Chemists_Name like + '" + SName + "%' Order By 2";
            }

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

        public DataTable getUnDoctor(string sfcode, string SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND UnListedDr_Name like + '" + SName + "%' Order By 2";
            }

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

        public DataTable getUnDoctor_MGR(string sfcode, string SName, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "') " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND UnListedDr_Name like + '" + SName + "%' Order By 2";
            }

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
        public DataSet getTerrListedDoctor(string sfcode, int doc_disp, string terr_code)// Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {


                strQry = " SELECT ListedDrCode, ListedDr_Name , 1 as slno " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                        " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                                " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                        " UNION ALL" +
                        " SELECT ListedDrCode, ListedDr_Name , 2 as slno" +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                        " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                                " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = "SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = "SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            "  SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

                //strQry = "SELECT a.ListedDrCode AS ListedDrCode, " +
                //            " a.ListedDr_Name + ' - ' + b.Doc_Special_SName  AS ListedDr_Name  " +
                //            " FROM Mas_ListedDr a, Mas_Doctor_Speciality b " +
                //            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                //            " AND a.Doc_Special_Code = b.Doc_Special_Code " +
                //            " AND a.ListedDr_Active_Flag = 0 " +
                //            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                //        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                //            " UNION ALL" +
                //            "  SELECT a.ListedDrCode AS ListedDrCode, " +
                //            " a.ListedDr_Name + ' - ' + b.Doc_Special_SName  AS ListedDr_Name  " +
                //            " FROM Mas_ListedDr a, Mas_Doctor_Speciality b " +
                //            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                //            " AND a.Doc_Special_Code = b.Doc_Special_Code " +
                //            " AND a.ListedDr_Active_Flag = 0 " +
                //            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                //        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

            }
            else if (doc_disp == 4) // Category
            {
                strQry = "SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0" +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0" +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

            }
            else if (doc_disp == 5) //Class
            {
                strQry = "SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

            }
            //else if (doc_disp == 6) //Campaign
            //{
            //    strQry = "Select '0' ListedDrCode,'-Select-' ListedDr_Name UNION SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
            //                " FROM Mas_ListedDr " +
            //                " WHERE Sf_Code =  '" + sfcode + "' " +
            //                " AND ListedDr_Active_Flag = 0 " +
            //                " AND Territory_Code like '%" + terr_code + "%' " +
            //                " UNION " +
            //                " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
            //                " FROM Mas_ListedDr " +
            //                " WHERE Sf_Code =  '" + sfcode + "' " +
            //                " AND ListedDr_Active_Flag = 0 " +
            //                " AND Territory_Code NOT like '%" + terr_code + "%' " +
            //                "  Order By 2";
            //}

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

        public DataSet getTerrListedDoctor_Mgr(string sfcode, int doc_disp, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {
                strQry = " select sf_code, ListedDrCode,ListedDr_Name,Territory_Code" +
                             " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                              " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                                " AND ListedDr_Active_Flag = 0" +
                                  " union all " +
                                 " select sf_code, ListedDrCode, ListedDr_Name,Territory_Code" +
                                 " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                                  " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                                 " AND ListedDr_Active_Flag = 0";

                //strQry = " SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                //       " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //       " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                //       " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //       " AND ListedDr_Active_Flag = 0" +
                //       " UNION ALL " +
                //       " SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                //       " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //       " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //       " AND ListedDr_Active_Flag = 0" +
                //       " order by Territory_Code";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = " select sf_code, ListedDrCode,ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0";
                //strQry = "SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //        " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //        " AND ListedDr_Active_Flag = 0 " +
                //            " UNION  ALL" +
                //        " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //        " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //        " AND ListedDr_Active_Flag = 0" +
                //        " order by Territory_Code";
            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = " select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0";
                //strQry = "SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                //      " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                //      " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                //      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                //      " AND ListedDr_Active_Flag = 0 " +
                //      " order by Territory_Code";
            }
            else if (doc_disp == 4) // Category
            {
                strQry = " select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0";

                //strQry = " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                //      " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                //     " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or  a.Territory_Code like cast(c.Territory_Code as varchar))" +
                //      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                //      " AND ListedDr_Active_Flag = 0 " +
                //         " UNION  ALL" +
                //         " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //        " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //        " AND ListedDr_Active_Flag = 0" +
                //" order by Territory_Code";
            }
            else if (doc_disp == 5) //Class
            {
                strQry = " select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0";

                //strQry = "SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                //        " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or  a.Territory_Code like cast(c.Territory_Code as varchar))" +
                //      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                //       " AND ListedDr_Active_Flag = 0  " +
                //         " UNION ALL " +
                //         "SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //        " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //        " AND ListedDr_Active_Flag = 0" +
                //        " order by Territory_Code";
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
        public DataSet getTerrListedDoctor(string sfcode, string sName, int iVal)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "Select '0' ListedDrCode,'-Select-' ListedDr_Name UNION SELECT ListedDrCode, ListedDr_Name " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND ListedDr_Name like '" + sName + "%' " +
                        " AND ListedDr_Active_Flag = 0 Order By 2";
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

        public DataSet getdoctercolor(string sfcode, string DCRDate, string doccode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "select Color_code from DCR_MGR_WorkAreaDtls Where" +
                        " MGR_Code = '" + sfcode + "' " +
                        " AND DCR_Date =  '" + DCRDate + "' " +
                        " AND Territory_code = (select territory_code from mas_listeddr where ListedDrCode = '" + doccode + "')";


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
        public DataSet getTerrListedDoctor(string sfcode, string Terr_Code, string DR_Name)// Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT ListedDrCode " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND ListedDr_Name = '" + DR_Name + "' " +
                        " AND  (Territory_Code like '" + Terr_Code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + Terr_Code + ',' + "%' or Territory_Code like '" + Terr_Code + "') " +
                        " AND ListedDr_Active_Flag = 0";
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

        public DataSet getTerrUnListedDoctor(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sf_code + "' " +
                        " AND UnListedDr_Active_Flag = 0 Order By 2";
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

        public DataSet getTerrUnListedDoctor_Mgr(string sf_code, int doc_disp, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select '0' UnListedDrCode,'-Select-' UnListedDr_Name UNION SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code = '" + sf_code + "'" +
                        " AND UnListedDr_Active_Flag = 0 Order By 2";
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
        public DataSet getTerrUnListedDoctor_MgrNew(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "') " +
                        " AND UnListedDr_Active_Flag = 0 Order By 2";
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

        public DataSet getTerrListedDoctor(string sfcode, string Terr_Code) // Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            //Modified the qry to accomplish the territory code data type (varchar)
            // by Sridevi on 12/12/14

            ////strQry = "SELECT ListedDrCode, ListedDr_Name " +
            ////            " FROM Mas_ListedDr " +
            ////            " WHERE Sf_Code =  '" + sfcode + "' " +
            ////            " AND Territory_Code = '" + Terr_Code + "' " +
            ////            " AND ListedDr_Active_Flag = 0 Order By 2";

            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name UNION " +
                        "SELECT ListedDrCode, ListedDr_Name" +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND  (Territory_Code like '" + Terr_Code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + Terr_Code + ',' + "%' or Territory_Code like '" + Terr_Code + "') " +
                        " AND ListedDr_Active_Flag = 0";


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

        public DataTable getSF(string sname)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsTP = null;
            strQry = "select sf_code,sf_name from mas_salesforce where sf_name like + '" + sname + "%' " +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getSDP(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            //strQry = "select '0' Territory_Code, '---Select---' Territory_Name  " +
            //         " UNION " +
            //         " select distinct a.Territory_Code, a.Territory_Name " +
            //         " from Mas_Territory_Creation a, Mas_ListedDr b " +
            //         " where a.Territory_Active_Flag=0 and a.SF_Code='" + SF_Code + "' " +
            //         " and a.SF_Code = b.Sf_Code and b.ListedDr_Active_Flag=0 " +
            //         " and cast(a.Territory_Code as varchar) like b.Territory_Code " +                      
            //         " ORDER BY 2";

            strQry = "select '0' Territory_Code, '---Select---' Territory_Name  " +
                     " UNION " +
                     " select distinct  Territory_Code,  Territory_Name " +
                     " from Mas_Territory_Creation  " +
                     " where  Territory_Active_Flag=0 and  SF_Code='" + SF_Code + "' " +
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

        public DataSet getDCRDate(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Last_Dcr_Date  from Mas_Salesforce " +
                     " where Sf_Code='" + SF_Code + "' and sf_TP_Active_Flag=0 ";
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

        public DataSet getsf_dtls(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT  Sf_Name, sf_emp_id, Employee_Id " +
                     " FROM mas_salesforce " +
                     " WHERE (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " AND sf_code  ='" + sf_code + "' ";
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

        public DataSet getDCREntryDate(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select max(activity_date) from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '1'  and " +
                     " activity_date not in (select activity_date from dcrmain_trans where Sf_Code='" + SF_Code + "')";
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
        public DataSet getDCREntryDate_Reject(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select min(activity_date) from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '2' ";
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

        public DataSet getRejectedDCR(string SF_Code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Work_Type,Plan_No,Remarks, ReasonforRejection from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '2'  and  Activity_Date = '" + DCRDate + "'";
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

        public DataSet getDCREntryDate_trans(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select max(activity_date) from dcrmain_trans " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '1' ";
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


        public DataSet getDCREdit(string SF_Code, string sMonth, string sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select  convert(varchar,Activity_Date,103) Activity_Date,Trans_SlNo,worktype_name_b, a.Sf_Code " +
                     " from DCRMain_Trans a, Mas_WorkType_BaseLevel b, Mas_Salesforce c " +
                     " where a.Work_Type = b.WorkType_Code_B and a.sf_code = c.sf_code and c.sf_type = 1 " +
                     " and a.Sf_Code='" + SF_Code + "' and MONTH(Activity_Date) = '" + sMonth + "' and YEAR(Activity_Date) = '" + sYear + "' " +
                     " Union " +
                     " Select  convert(varchar,Activity_Date,103) Activity_Date,Trans_SlNo,Worktype_Name_m, a.Sf_Code " +
                     " from DCRMain_Trans a, Mas_WorkType_Mgr b , Mas_Salesforce c" +
                     " where a.Work_Type = b.WorkType_Code_M and a.sf_code = c.sf_code and c.sf_type = 2 " +
                     " and a.Sf_Code='" + SF_Code + "' and MONTH(Activity_Date) = '" + sMonth + "' and YEAR(Activity_Date) = '" + sYear + "' ";

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
        public int RecordInsertMGRWorkArea(string MGR_Code, string Sf_Code, string WorkArea, string Color_Code, string DCRDate, string Work_Type)
        {
            int iReturn = -1;
            int S_No = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT S_No FROM DCR_MGR_WorkAreaDtls WHERE MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' and Sf_Code = '" + Sf_Code + "' and Territory_Code ='" + WorkArea + "'";
                S_No = db.Exec_Scalar(strQry);

                if (S_No > 0)
                {
                    strQry = "delete from DCR_MGR_WorkAreaDtls where MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' and Sf_Code = '" + Sf_Code + "' and Territory_Code ='" + WorkArea + "'";

                    iReturn = db.ExecQry(strQry);

                }
                strQry = "SELECT isnull(max(S_No)+1,'1') S_No from DCR_MGR_WorkAreaDtls ";
                int Sl_No = db.Exec_Scalar(strQry);
                strQry = "insert into DCR_MGR_WorkAreaDtls (Sl_No,MGR_Code,Sf_Code, DCR_Date, Territory_Code,Color_Code,Work_Type) " +
                            " VALUES('" + Sl_No + "','" + MGR_Code + "', '" + Sf_Code + "', '" + DCRDate + "', '" + WorkArea + "','" + Color_Code + "','" + Work_Type + "' )";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet getMgrWorkAreaDtls(string SF_Code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select DISTINCT Work_Type from DCR_MGR_WorkAreaDtls Where " +
                     " MGR_Code ='" + SF_Code + "' and DCR_Date ='" + DCRDate + "' ";

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

        public int get_Trans_SlNo(string SF_Code, string work_type)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select Trans_SlNo from DCRMain_Temp where Sf_Code = '" + SF_Code + "' and Work_Type= '" + work_type + "' and confirmed=0 ";
                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Update_DCR_Status(string SF_Code, int Trans_SlNo)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "update DCRMain_Temp set confirmed = 1 where Trans_SlNo = '" + Trans_SlNo + "' " +
                         " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }




        public int RecordAdd_Detail_Submit(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string DCR_Session, string DCR_Time, string Worked_With_Code, string Worked_With_Name, string Prod_Detail, string Gift_Detail, string SDP)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "delete from DCRDetail_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                         " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";


                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_Slno as bigint)),0)+1 FROM DCRDetail_Temp WHERE Division_Code = '" + Division_Code + "' ";
                Trans_Detail_SlNo = db.Exec_Scalar(strQry);

                strQry = "insert into DCRDetail_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, Session, Time, " +
                         " Worked_with_Code, Worked_with_Name, Product_Detail, Gift_Detail, SDP, Division_Code) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + DCR_Session + "', '" + DCR_Time + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', '" + Prod_Detail + "', '" + Gift_Detail + "', '" + SDP + "',  '" + Division_Code + "' )";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int RecordAdd_Detail_Chem_Submit(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB_Value, string Worked_With_Code, string Worked_With_Name, string SDP)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_Slno as bigint)),0)+1 FROM DCRDetail_Temp WHERE Division_Code = '" + Division_Code + "'  and Sf_Code = '" + SF_Code + "' ";
                Trans_Detail_SlNo = db.Exec_Scalar(strQry);

                strQry = "insert into DCRDetail_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, POB_Value, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Division_Code) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB_Value + "', '" + POB_Value + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "',  '" + SDP + "', '" + Division_Code + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "' ";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RecordAdd_Detail_Stockiest_Submit(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB, string Worked_With_Code, string Worked_With_Name, string SDP, string Visit_Type)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_Slno as bigint)),0)+1 FROM DCRDetail_Temp WHERE Division_Code = '" + Division_Code + "'  and Sf_Code = '" + SF_Code + "' ";
                Trans_Detail_SlNo = db.Exec_Scalar(strQry);

                strQry = "insert into DCRDetail_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Visit_Code, Division_Code) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', '" + SDP + "',  '" + Visit_Type + "', '" + Division_Code + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "' ";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_DCR_Pending_Approval(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct a.sf_code, a.sf_name,a.Sf_HQ,a.sf_Designation_Short_Name as Designation_Short_Name, " +
                  " MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
                  " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
                  " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
                  " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
                  " from Mas_Salesforce a, DCRMain_Temp b,Mas_Salesforce_AM d    " +
                  " where a.sf_code = b.sf_code " +
                  " and d.DCR_AM = '" + sf_code + "' and   b.Sf_Code=d.Sf_Code and b.confirmed=1" +
                  " and b.Division_code in('" + div_code + "')" +
                  " and (a.sf_type = 1 or a.sf_type = 2) and b.FieldWork_Indicator != 'L' " +
                  " and convert(char(10),b.activity_date,103)!=convert(char(10),getdate(),103) ";
            //" select distinct a.sf_code, a.sf_name,a.Sf_HQ,c.Designation_Short_Name, " +
            //      " MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
            //      " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
            //      " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
            //      " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
            //      " from Mas_Salesforce a, DCRMain_Temp b, Mas_SF_Designation c ,Mas_Salesforce_AM d, Mas_WorkType_Mgr wor     " +
            //      " where a.sf_code = b.sf_code and  a.Designation_Code=c.Designation_Code  " +
            //      " and d.DCR_AM = '" + sf_code + "' and   b.Sf_Code=d.Sf_Code and b.confirmed=1" +
            //      " and b.Division_code in('" + div_code + "')" +
            //      " and b.work_type = wor.worktype_code_m and a.sf_type = 2 and wor.FieldWork_Indicator != 'L' ";
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
        public DataSet get_DCR_Pending_Approval_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select a.sf_Code,a.trans_slno,convert(varchar,a.Activity_Date,103) Activity_Date,month(Activity_Date) as MActDate,Year(Activity_Date) as YActDate,Day(Activity_Date) as DActDate,a.division_code," +
                     "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name)  " +
                     " from DCRDetail_Lst_Temp v where v.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = v.Trans_SlNo  " +
                     " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ) as Plan_Name, " +
                        "  wor.Worktype_Name_B ,(select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                        " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                        " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                        " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                        " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as unlst_cnt," +
                        " a.Remarks from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor, Mas_Salesforce b where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' and Year(a.Activity_date) = '" + year + "' " +
                        " and convert(char(10),a.activity_date,103)!=convert(char(10),getdate(),103)" +
                        " and a.work_type = wor.worktype_code_b and a.sf_code = b.sf_code and b.sf_type = 1 and wor.FieldWork_Indicator != 'L' " +
                        " Union" +
                        " select a.sf_Code,a.trans_slno,convert(varchar,a.Activity_Date,103) Activity_Date,month(Activity_Date) as MActDate,Year(Activity_Date) as YActDate,Day(Activity_Date) as DActDate,a.division_code," +
                        "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name)  " +
                     " from DCRDetail_Lst_Temp v where v.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = v.Trans_SlNo  " +
                     " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ) as Plan_Name, " +
                        "  wor.Worktype_Name_m ,(select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                        " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                        " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                        " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                        " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as unlst_cnt," +
                        " a.Remarks from DCRMain_Temp a ,Mas_WorkType_Mgr wor, Mas_Salesforce b where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' and Year(a.Activity_date) = '" + year + "' " +
                        " and convert(char(10),a.activity_date,103)!=convert(char(10),getdate(),103)" +
                        " and a.work_type = wor.worktype_code_m and a.sf_code = b.sf_code and b.sf_type = 2 and wor.FieldWork_Indicator != 'L' order by Activity_Date ";

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

        public DataSet getDCR_Report(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WType_SName " +
                     " from DCRMain_Trans a, Mas_WorkType_BaseLevel b ,Mas_Salesforce c" +
                     " where a.Work_Type = b.WorkType_Code_B " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 1 " +
                     " Union " +
                     "select WType_SName " +
                     " from DCRMain_Trans a, Mas_WorkType_Mgr b,Mas_Salesforce c " +
                     " where a.Work_Type = b.WorkType_Code_M " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 2 ";
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

        public DataSet getDCR_Report_Det(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select day(Submission_Date) from DCRMain_Trans " +
                     " Where Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear;
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
        public DataSet getDCR_Report_Det_doc(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select count(c.trans_detail_info_code) Doc_count from DCRMain_Trans a,DCRDetail_Lst_Trans  c" +
                     " where a.Trans_SlNo = c.Trans_SlNo " +
                     " and a.Sf_Code = c.sf_code " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(a.Activity_Date) = " + sday +
                     " and MONTH(a.Activity_Date) = " + sMonth + " and YEAR(a.Activity_Date) =  " + sYear;
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

        public DataSet get_WorkType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WType_SName, Worktype_Name_B  from Mas_WorkType_BaseLevel ";

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

        public DataSet get_dcr_ff_details(int imon, int iyear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select distinct a.Sf_Code, b.Sf_Name, " +
                     " (select sf_name from mas_salesforce where sf_code = b.Reporting_To_SF) approved_by " +
                     " from DCRMain_Temp a, Mas_Salesforce b " +
                     " where a.Sf_Code = b.Sf_Code and a.division_code='" + div_code + "' and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;

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

        public DataSet get_dcr_pending_approval(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select day(Activity_Date) pending_date from DCRMain_Temp " +
                     " where Sf_Code = '" + sf_code + "' and Confirmed = 1 and MONTH(Activity_Date) = " + imon + " and YEAR(Activity_Date) = " + iyear;

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

        public int get_Trans_SlNo_toIns(string SF_Code, string Activity_Date)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select Trans_SlNo from DCRMain_Temp where Sf_Code = '" + SF_Code + "' and convert(varchar,Activity_Date,103) = '" + Activity_Date + "'";
                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Create_DCRHead_Trans(string SF_Code, string Trans_SlNo)
        {
            int iReturnmain = -1;
            int iReturntemp = -1;
            int iReturn = -1;
            int slno = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC sp_DCRApproval_Trans '" + SF_Code + "', '" + Trans_SlNo + "'";

                iReturn = db.ExecQry(strQry);

                //strQry = "Insert into DCRMain_Trans select * from DCRMain_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //iReturnmain = db.ExecQry(strQry);

                //if (iReturnmain > 0)
                //{
                //    strQry = "Insert into DCRDetail_Lst_Trans select * from DCRDetail_Lst_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //    iReturntemp = db.ExecQry(strQry);

                //    strQry = "Insert into DCRDetail_UnLst_Trans select * from DCRDetail_UnLst_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //    iReturntemp = db.ExecQry(strQry);

                //    strQry = "Insert into DCRDetail_CSH_Trans select * from DCRDetail_CSH_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //    iReturntemp = db.ExecQry(strQry);
                //}
                //if (iReturntemp >= 0)
                //{
                //    strQry = "DELETE from DCRDetail_Lst_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                //             " (select * from DCRDetail_Lst_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                //    iReturn = db.ExecQry(strQry);

                //    strQry = "DELETE from DCRDetail_UnLst_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                //            " (select * from DCRDetail_UnLst_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                //    iReturn = db.ExecQry(strQry);

                //    strQry = "DELETE from DCRDetail_CSH_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                //            " (select * from DCRDetail_CSH_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                //    iReturn = db.ExecQry(strQry);


                //    strQry = "DELETE from DCRMain_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                //             " (select * from DCRMain_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                //    iReturn = db.ExecQry(strQry);

                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Reject_DCR(string SF_Code, string Trans_SlNo, string ReasonforReject)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "update DCRMain_Temp set confirmed = 2, ReasonforRejection= '" + ReasonforReject + "' where Trans_SlNo = '" + Trans_SlNo + "' " +
                //         " and Sf_Code = '" + SF_Code + "' ";

                strQry = "EXEC sp_Reject_DCR '" + SF_Code + "', '" + Trans_SlNo + "','" + ReasonforReject + "'";

                iReturn = db.ExecQry(strQry);


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int isDCR(string div_code, int imonth, int iyear)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(trans_slno)  from DCRMain_Temp " +
                            " where MONTH(activity_date)= " + imonth + " and YEAR(activity_date)= " + iyear + "  and Division_Code = '" + div_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_dcr_dates(string div_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Trans_SlNo , Sf_Code,day(activity_date),Submission_Date,Activity_Date from DCRMain_Temp " +
                     " where MONTH(activity_date)= " + imon + " and YEAR(activity_date)= " + iyear + "  and Division_Code = '" + div_code + "' ";

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


        public DataSet get_dcr_date(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;

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

        public DataSet get_dcr_details_count(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                     "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                      "  FOR XML PATH(''), TYPE " +
                      "  ).value('.', 'NVARCHAR(MAX)') " +
                      " ,1,1,'') ) as che_POB_Name ," +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                      " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,b.ModTime,b.GeoAddrs,b.lati,b.long,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.SDP_Name " +
                      " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                      " Union all " +
                      " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                     "from DCRDetail_Lst_Temp c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                      "  FOR XML PATH(''), TYPE " +
                      "  ).value('.', 'NVARCHAR(MAX)') " +
                      " ,1,1,'') ) as che_POB_Name ," +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                      " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,b.ModTime,b.GeoAddrs,b.lati,b.long,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.SDP_Name " +
                      " from DCRMain_Temp a, DCRDetail_Lst_Temp b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'  order by b.Time ";



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
        public DataSet get_dcr_che_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB,vstTime,ModTime,Rx,GeoAddrs " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                      " Union " +
                      " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB,vstTime,ModTime,Rx,GeoAddrs " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_dcr_hos_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Hospital c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5 and b.Trans_Detail_Info_Code=c.hospital_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_dcr_stk_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,Rx,GeoAddrs " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                    " Union " +
                    " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,Rx,GeoAddrs " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_unlst_doc_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                           " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,ModTime,Rx,GeoAddrs,b.SDP_Name " +
                           " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
                           " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                           " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                           //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                           " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                           " Union " +
                           " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                           " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,ModTime,Rx,GeoAddrs,b.SDP_Name " +
                           " from DCRMain_Temp a, DCRDetail_Unlst_Temp b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
                           " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                           " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                           //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                           " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet getSfName_HQ(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT Sf_Name,Sf_HQ,sf_Designation_Short_Name, sf_code FROM  Mas_Salesforce WITH (NOLOCK) " +
                     " WHERE sf_code= '" + sfcode + "' ";

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
        public DataSet getRemarks(string sfcode, string trans_slno)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            //strQry = " select a.remarks , wor.Worktype_Name_B  from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor " +
            //         " WHERE a.sf_code= '" + sfcode + "' and a.trans_slno = '" + trans_slno + "'  and a.work_type = wor.worktype_code_b ";
            if (sfcode.Contains("MR"))
            {
                strQry = " select a.remarks , wor.Worktype_Name_B  from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor " +
                         " WHERE a.sf_code= '" + sfcode + "' and a.trans_slno = '" + trans_slno + "'  and a.work_type = wor.worktype_code_b ";
            }
            else
            {
                strQry = " select a.remarks , wor.Worktype_Name_M  from DCRMain_Temp a ,Mas_WorkType_Mgr wor " +
                         " WHERE a.sf_code= '" + sfcode + "' and a.trans_slno = '" + trans_slno + "'  and a.work_type = wor.WorkType_Code_M ";
            }

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

        public DataSet Catg_Visit_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Catg_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
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
        public DataSet Catg_Visit_Report1(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (mode == 2)
            {
                strQry = "EXEC sp_DCR_Catg_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            else if (mode == 1)
            {
                strQry = "EXEC sp_DCR_Catg_VisitCallReport_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
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

        public DataSet Catg_Visit_Report_noofvisit(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int no_of_visit)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_VisitDR_Catg_NoofVisit  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + ", " + no_of_visit + " ";

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

        public DataSet Catg_Visit_Report_noofvisit1(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int no_of_visit, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (mode == 2)
            {
                strQry = "EXEC sp_DCR_VisitDR_Catg_NoofVisit  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + ", " + no_of_visit + " ";
            }
            else if (mode == 1)
            {
                strQry = "EXEC sp_DCR_VisitDR_Catg_NoofVisit_self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + ", " + no_of_visit + " ";
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


        public DataSet Spec_Visit_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Spec_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";

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

        public DataSet Spec_Visit_Report1(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (mode == 2)
            {

                strQry = "EXEC sp_DCR_Spec_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            else if (mode == 1)
            {
                strQry = "EXEC sp_DCR_Spec_VisitCallReport_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
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

        public DataSet Class_Visit_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Class_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";

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


        public DataSet Class_Visit_Report1(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (mode == 2)
            {
                strQry = "EXEC sp_DCR_Class_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            else if (mode == 1)
            {
                strQry = "EXEC sp_DCR_Class_VisitCallReport_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
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

        public DataSet Visit_Doc(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_VisitDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";

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
        public DataSet Visit_Doc_noofvisit(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode, string vMode, string novst)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC [sp_DCR_VisitDR_Catg_visit] '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' , '" + novst + "' ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_VisitDR_Spec '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }
            else if (sMode == "3")
            {
                strQry = " EXEC sp_DCR_VisitDR_Class '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
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


        public DataSet Visit_Doc(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode, string vMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC sp_DCR_VisitDR_Catg '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_VisitDR_Spec '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }
            else if (sMode == "3")
            {
                strQry = " EXEC sp_DCR_VisitDR_Class '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
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

        public DataSet Visit_Doc(string doc_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_Visit_Count '" + doc_code + "', '" + cmon + "', '" + cyear + "' ";

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

        public DataSet SF_ReportingTo_TourPlan(string div_code, string reporting_to)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.sf_code, a.Sf_Name, a.SF_Type, a.Sf_HQ, b.Designation_Short_Name " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.Reporting_To_SF= '" + reporting_to + "' " +
                     " AND lower(a.sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and a.Designation_Code=b.Designation_Code " +
                     " ORDER BY 2";
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


        public DataSet DCR_Visit_FLDWRK(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Visit_FLDWRK  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_Total_Doc_Visit_Report(string sf_code, string div_code, DateTime dtcurrent, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Total_Doc_Visit_Report  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "','" + iMonth + "','" + iYear + "' ";

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

        public DataSet DCR_Doc_Met(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Met  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_Doc_Calls_Seen(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Calls_Seen  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet get_worktype(string sf_code, int iMonth, int iDay, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.Work_Type ,b.Worktype_Name_B  " +
                     " FROM DCRMain_Trans a, Mas_WorkType_BaseLevel b " +
                     " WHERE a.Work_Type = b.WorkType_Code_B  " +
                     " and a.Sf_Code = '" + sf_code + "' " +
                     " and Day(a.Activity_Date ) = " + iDay +
                     " and Month(a.Activity_Date ) = " + iMonth +
                     " and Year(a.Activity_Date ) = " + iYear +
                     " ORDER BY 2";
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

        public DataSet get_Dcr_Exp(string sf_code, int iMonth, int iDay, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "EXEC FetchDCRDtls_forExpCalc '" + sf_code + "' ," + iDay + "," + iMonth + ", " + iYear + " ";

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

        public DataSet get_ExpAll(string sf_code, string Allowtype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (Allowtype == "HQ")
            {
                strQry = " select HQ_allowance from mas_allowance_fixation where sf_code = '" + sf_code + "' ";
            }
            else if (Allowtype == "EX")
            {
                strQry = " select EX_HQ_Allowance from mas_allowance_fixation where sf_code = '" + sf_code + "' ";
            }
            else if (Allowtype == "OS")
            {
                strQry = " select OS_Allowance from mas_allowance_fixation where sf_code = '" + sf_code + "' ";
            }
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

        public DataSet SF_Self_Report(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.sf_code, a.Sf_Name, a.SF_Type, a.Sf_HQ, b.Designation_Short_Name " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.sf_code = '" + sf_code + "' " +
                     " AND lower(a.sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and a.Designation_Code=b.Designation_Code " +
                     " ORDER BY 2";
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

        //Saravana Changes

        public DataSet get_DCRRemarks(string sf_code, int Month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " select convert(char(10),Activity_Date,105) Submission_Date,Remarks from DCRMain_Temp where Sf_Code= '" + sf_code + "' and MONTH(Activity_Date)='" + Month + "' " +
                      " union all " +
                      "select convert(char(10),Activity_Date,105) Submission_Date,Remarks from DCRMain_Trans where Sf_Code= '" + sf_code + "' and MONTH(Activity_Date)='" + Month + "' order by Submission_Date ";

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
        public DataSet get_DCRRemarks_Dev(string sf_code, int Month,string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " select convert(char(10),Activity_Date,105) Submission_Date,Remarks from DCRMain_Temp where Sf_Code= '" + sf_code + "' and MONTH(Activity_Date)='" + Month + "' and YEAR(Activity_Date)='"+Year+"' " +
                      " union all " +
                      "select convert(char(10),Activity_Date,105) Submission_Date,Remarks from DCRMain_Trans where Sf_Code= '" + sf_code + "' and MONTH(Activity_Date)='" + Month + "' and YEAR(Activity_Date)='"+Year+"' order by Submission_Date ";

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

        public DataSet DCR_Visit_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Visit_Days '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_Visit_Leave(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Visit_Leave '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_Visit_TotalDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select COUNT(ListedDrCode)ListedDrCode from Mas_ListedDr where Sf_Code='" + sf_code + "' and " +
                     " month(ListedDr_Created_Date)<= '" + iMonth + "' and year(ListedDr_Created_Date)='" + iYear + "'" +
                     " and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0";

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

        public DataSet DCR_TotalSubDaysQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans where Sf_Code = '" + sf_code + "' and month(Activity_Date)='" + iMonth + "' " +
                     " and YEAR(Activity_Date)='" + iYear + "' and Division_Code='" + div_code + "'";

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
        public DataSet DCR_Reminder_calls(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(Calldate)as Reminder_count from tbremdrcall  where Sf_Code = '" + sf_code + "'" +
            " and Division_Code like '" + div_code + ',' + "' and MONTH(calldate) = " + iMonth + " and YEAR(calldate) = " + iYear + "";


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

        public DataSet DCR_TotalFLDWRKQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F'";

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

        public DataSet DCR_TotalLeaveQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(DCR.FieldWork_Indicator) from DCRMain_Trans DCR " +
                    " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
                    " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.FieldWork_Indicator='L'";

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

        public DataSet DCR_TotalChemistQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(Trans_SlNo) from DCRDetail_CSH_Trans " +
                     "where Sf_Code = '" + sf_code + "' and Trans_Detail_Info_Type=2 and Division_Code='" + div_code + "'";

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

        public DataSet DCR_TotalStockistQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(Trans_SlNo) from DCRDetail_CSH_Trans " +
                     "where Sf_Code = '" + sf_code + "' and Trans_Detail_Info_Type=3 and Division_Code='" + div_code + "' ";

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

        public DataSet DCR_TotalUnlistDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(UnListedDrCode) from Mas_UnListedDr " +
                     " where Sf_Code = '" + sf_code + "' and UnListedDr_Active_Flag=0 and month(UnListedDr_Created_Date)='" + iMonth + "' ";

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

        public DataSet DCR_TotalStockistDocQuery(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRDetail_CSH_Trans " +
                     " where Sf_Code = '" + sf_code + "' and Trans_Detail_Info_Type=3 ";

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

        public DataSet DCR_TotalUnlstDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select COUNT(UnListedDrCode)ListedDrCode from Mas_UnListedDr where Sf_Code='" + sf_code + "' and " +
                     " month(UnListedDr_Created_Date)<='" + iMonth + "' and year(UnListedDr_Created_Date)='" + iYear + "' " +
                     " and Division_Code='" + div_code + "' and UnListedDr_Active_Flag=0 ";

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




        public DataSet Get_Call_Total_Chemist_Visit_Report(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Call_Total_Chemist_Visit_Report  '" + div_code + "', '" + sf_code + "' ";

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



        public DataSet Get_Call_Total_Stock_Visit_Report(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Call_Total_Stock_Visit_Report  '" + div_code + "', '" + sf_code + "' ";

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



        public DataSet DCR_Unlst_Doc_Calls_Seen(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_UnlstDoc_Calls_Seen  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet get_DCRView_Pending_Approval_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_B , " +
                     " case Confirmed when '1' then  '' when '2' then 'DisApproved' End as Temp,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and (a.confirmed=1 or a.confirmed=2) and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.worktype_code_b order by a.Activity_Date ";

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

        // Changes done by Saravanan
        public DataSet get_dcr_Pending_date(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Temp a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "";


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

        public DataSet get_Pending_dcrLstDOC_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                          " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                          " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name , d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time" +
                          " from DCRMain_Temp a, DCRDetail_Lst_Temp b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                          " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                          " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                          " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                          //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                          " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_Single_dcr_date(string sf_code, int imon, int iyear, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and CONVERT(varchar(10),Activity_Date,103)='" + Date + "'";

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
        public DataSet get_dcr_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.Trans_SlNo,b.Trans_Detail_Slno , convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                     "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                      "  FOR XML PATH(''), TYPE " +
                      "  ).value('.', 'NVARCHAR(MAX)') " +
                      " ,1,1,'') ) as che_POB_Name ," +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                      " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,b.ModTime,b.GeoAddrs,b.lati,b.long," +
                      "(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.SDP_Name  " +
                      " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code and a.sf_code=b.sf_code " +
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                      " Union all " +
                      " select a.Trans_SlNo,b.Trans_Detail_Slno , convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                       "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                     "from DCRDetail_Lst_Temp c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                      "  FOR XML PATH(''), TYPE " +
                      "  ).value('.', 'NVARCHAR(MAX)') " +
                      " ,1,1,'') ) as che_POB_Name ," +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                      " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,b.ModTime,b.GeoAddrs,b.lati,b.long, " +
                      " (select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.SDP_Name  " +
                      " from DCRMain_Temp a, DCRDetail_Lst_Temp b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code and a.sf_code=b.sf_code " +
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' order by b.Trans_Detail_Slno ";



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

        public DataSet get_Pending_dcr_che_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB" +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_Pending_unlst_doc_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                      " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name , d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time " +
                      " from DCRMain_Temp a, DCRDetail_Unlst_Temp b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_Pending_dcr_stk_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_Pending_dcr_hos_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Hospital c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5 and b.Trans_Detail_Info_Code=c.hospital_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet DCR_NonFwkQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select DCR.sf_Code,DCR.trans_slno,convert(varchar,DCR.Submission_Date,103) Submission_Date,day(DCR.Activity_Date) Activity_Date,'Sockiest Work' as Sockiest " +
                     " from DCRMain_TEMP DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='S'";

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

        public DataSet get_dcr_Doctor_Detail_View(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Lst.Sf_Code,Lst.ListedDr_Name,Spc.Doc_Special_Name,cat.Doc_Cat_Name,Terr.Territory_Name, " +
                     " Qul.Doc_QuaName,Class.Doc_ClsName " +
                     " from Mas_ListedDr as Lst,Mas_Doctor_Category as cat,Mas_Doctor_Speciality as Spc, " +
                     " Mas_Territory_Creation as Terr,Mas_Doc_Qualification as Qul,Mas_Doc_Class as Class  where lst.Sf_Code='" + sf_code + "' and Lst.Doc_Cat_Code=cat.Doc_Cat_Code " +
                     " and Lst.Doc_Special_Code=Spc.Doc_Special_Code and Lst.Territory_Code=cast(Terr.Territory_Code as varchar)  " +
                     " and Qul.Doc_QuaCode=Lst.Doc_QuaCode and Lst.Doc_ClsCode=Class.Doc_ClsCode " +
                     " and MONTH(ListedDr_Created_Date)='" + iMonth + "' and YEAR(ListedDr_Created_Date)='" + iYear + "'";

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

        public DataSet get_DCRView_Approved_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_B ,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Trans a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.worktype_code_b order by Activity_Date";

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

        public DataSet get_Approved_dcrLstDOC_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                        " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                        " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name  , d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time" +
                        " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                        " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                        " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                        " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                        //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                        " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";
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

        public DataSet get_Approved_dcr_che_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB" +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_Approved_dcr_stk_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_Approved_unlst_doc_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                         " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                         " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time " +
                         " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                         " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                         " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                         " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                         //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                         " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";
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

        public DataSet get_DCRHoliday_Name(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select b.Worktype_Name_M from DCRMain_Trans a,Mas_WorkType_Mgr b " +
                           " where a.Work_Type= b.WorkType_Code_M and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS') " +
                           " Union " +
                           " select b.Worktype_Name_M from DCRMain_Temp a,Mas_WorkType_Mgr b " +
                           " where a.Work_Type= b.WorkType_Code_M and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS') ";

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

        public DataSet get_DCRPendingList(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select 'Pending Approval' Worktype_Name_M from DCRMain_Temp a,Mas_WorkType_Mgr b " +
                     " where a.Work_Type= b.WorkType_Code_M  and  a.Sf_Code='" + sf_code + "' " +
                     " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                     " and b.FieldWork_Indicator in('F') ";

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

        public DataSet get_dcr_DCRPendingdate(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name,a.Remarks " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union all" +
                     " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name,a.Remarks " +
                     " from DCRMain_Temp a" +
                      " where a.Sf_Code = '" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "order by Activity_Date";

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

        //Sridevi - To get all pending approvals
        public DataSet get_dcr_pending_approval_TransSl(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Trans_SlNo  From  DCRMain_Temp " +
                     " where Sf_Code = '" + sf_code + "' " +
                     " UNION " +
                     " select Trans_SlNo  From  DCRMain_Temp a, Mas_Salesforce b" +
                     " where b.sf_code = a.sf_code and b.Reporting_To_SF  = '" + sf_code + "'";

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


        public int Option_EditDCRDates_New(string sf_code, int Month, int Year, string Trans_Slno, string Edit_Date)
        {
            int iReturn = -1;
            int iretmove = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "EXEC sp_DCREdit_Update_New_Dev '" + sf_code + "'," + Month + "," + Year + ", '" + Trans_Slno + "', '" + Edit_Date + "'";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Option_EditDCRDates(string sf_code, int Month, int Year, string Trans_Slno, string Edit_Date)
        {
            int iReturn = -1;
            int iretmove = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Option_DCR_Edit_Dates ";
                int Sl_No = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Option_DCR_Edit_Dates VALUES ( '" + Sl_No + "','" + sf_code + "' ," + Month + "," + Year + ", '" + Trans_Slno + "', '" + Edit_Date + "',getdate(),getdate(),0) ";
                iReturn = db.ExecQry(strQry);

                if (iReturn > 0)
                {
                    // Transfer Trans data to Temp tables.
                    iretmove = Move_Trans_to_Temp(sf_code, Trans_Slno);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Move_Trans_to_Temp(string SF_Code, string Trans_SlNo)
        {
            int iReturnmain = -1;
            int iReturntemp = -1;
            int iReturn = -1;
            int ireturnupdate = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "EXEC sp_DCREdit_Update '" + SF_Code + "', '" + Trans_SlNo + "' ";

                strQry = "EXEC sp_DCREdit_Update_New '" + SF_Code + "', '" + Trans_SlNo + "' ";

                ireturnupdate = db.ExecQry(strQry);

                //strQry = "Insert into DCRMain_Temp select * from DCRMain_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //iReturnmain = db.ExecQry(strQry);

                //if (iReturnmain > 0)
                //{
                //    strQry = "Insert into DCRDetail_Lst_Temp select * from DCRDetail_Lst_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //    iReturntemp = db.ExecQry(strQry);

                //    strQry = "Insert into DCRDetail_UnLst_Temp select * from DCRDetail_UnLst_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //    iReturntemp = db.ExecQry(strQry);

                //    strQry = "Insert into DCRDetail_CSH_Temp select * from DCRDetail_CSH_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //    iReturntemp = db.ExecQry(strQry);
                //}
                //if (iReturntemp >= 0)
                //{
                //    strQry = "DELETE from DCRDetail_Lst_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                //             " (select * from DCRDetail_Lst_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                //    iReturn = db.ExecQry(strQry);

                //    strQry = "DELETE from DCRDetail_UnLst_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                //            " (select * from DCRDetail_UnLst_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                //    iReturn = db.ExecQry(strQry);

                //    strQry = "DELETE from DCRDetail_CSH_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                //            " (select * from DCRDetail_CSH_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                //    iReturn = db.ExecQry(strQry);


                //    strQry = "DELETE from DCRMain_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                //             " (select * from DCRMain_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                //    iReturn = db.ExecQry(strQry);


                //    strQry = "update DCRMain_Temp set Confirmed = 3  where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " "; // 3 - Edit 
                //    ireturnupdate = db.ExecQry(strQry);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        // Added on 6th june - dcr edit - sridevi
        public DataSet getDCREntryDate_Edit(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select min(activity_date) from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '3' ";
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


        public DataSet getWorkType_MR(string worktype_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel " +
                     " where Worktype_Name_B = '" + worktype_name + "' ";
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


        public int RecordAdd_DelayDtls(string sf_code, int Month, int Year, string ddate, DateTime ldate, string div_code) //Modified by Sri - 07-Aug
        {
            int iReturn = -1;
            int Trans_SlNo = 0;

            try
            {
                DB_EReporting db = new DB_EReporting();
                //Delete temp if exists
                strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + ddate + "' ";
                Trans_SlNo = db.Exec_Scalar(strQry);

                if (Trans_SlNo > 0)
                {
                    strQry = "delete from DCRDetail_Lst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + sf_code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_CSH_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + sf_code + "'";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_UnLst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + sf_code + "'";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRmain_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + sf_code + "'";
                    iReturn = db.ExecQry(strQry);
                }

                strQry = "INSERT INTO DCR_Delay_Dtls(Sf_code,Month,Year,Delayed_Date, Delayed_Flag,Delay_Created_Date,Division_Code) VALUES ( '" + sf_code + "' ," + Month + "," + Year + ", '" + ddate + "',0, getdate(),'" + div_code + "') ";
                iReturn = db.ExecQry(strQry);

                if (iReturn > 0)
                {
                    // Update Last Dcr Date in salesforce

                    DateTime dtDCR;
                    int iReturnsf = -1;
                    string Last_Dcr_Date = string.Empty;
                    // dtDCR = Convert.ToDateTime(ddate);
                    dtDCR = ldate.AddDays(1);
                    Last_Dcr_Date = dtDCR.ToString("MM/dd/yyyy");

                    strQry = " Update Mas_Salesforce  set Last_Dcr_Date = '" + Last_Dcr_Date + "' ," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE Sf_Code= '" + sf_code + "' ";

                    iReturnsf = db.ExecQry(strQry);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getDCREntryDelay_Release(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select min(Delayed_Date) from DCR_Delay_Dtls " +
                     " where Sf_Code='" + SF_Code + "' and Delayed_Flag = 1 ";
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

        public DataSet Get_ChkWorkTypeName(string StrChkWorkType, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where Worktype_Name_B in(" + StrChkWorkType + ") and division_code='" + div_Code + "'";

            DataSet dsDCR = null;

            try
            {
                dsDCR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDCR;
        }

        public DataSet Get_CountWorkType(string SF_Code, string div_code, string cmonth, string cyear, string WorkTypeName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Select (select count(Trans_SlNo)  " +
                    "from DCRMain_Trans DCR " +
                    "where DCR.Sf_Code = '" + SF_Code + "' and month(DCR.Activity_Date)='" + cmonth + "' " +
                    "and YEAR(DCR.Activity_Date)='" + cyear + "' " +
                    "and DCR.Division_Code='" + div_code + "' and DCR.WorkType_Name='" + WorkTypeName + "') + " +
                    "(select count(Trans_SlNo)  " +
                    "from DCRMain_Temp DCR " +
                    "where DCR.Sf_Code = '" + SF_Code + "' and month(DCR.Activity_Date)='" + cmonth + "' " +
                    "and YEAR(DCR.Activity_Date)='" + cyear + "' " +
                    "and DCR.Division_Code='" + div_code + "' and DCR.WorkType_Name='" + WorkTypeName + "')";

            DataSet dsDCR = null;

            try
            {
                dsDCR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDCR;
        }
        public DataSet Get_LastDCRDate(string sf_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select isnull(MAX(convert(varchar(10),activity_date,103)),'') from DCRMain_Trans where Sf_Code='" + sf_code + "' and Division_Code='" + Div_Code + "'";

            DataSet dsDCR = null;

            try
            {
                dsDCR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDCR;
        }
        public DataSet DCR_get_WorkType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B, Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel ";

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


        public DataSet DCR_get_WorkType(string div_code, string work_type_code, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            if (sf_type == "1")
            {
                strQry = "select FieldWork_Indicator,Button_Access from Mas_WorkType_BaseLevel Where division_code = '" + div_code + "' and WorkType_Code_B = '" + work_type_code + "'";
            }
            else if (sf_type == "2")
            {
                strQry = "select FieldWork_Indicator,Button_Access from Mas_WorkType_Mgr Where division_code = '" + div_code + "' and WorkType_Code_M = '" + work_type_code + "'";
            }

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

        public DataSet getWorkTypeCode_MR(string worktype_sname, string sf_type, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            if (sf_type == "1")
            {
                strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel " +
                         " where division_code = '" + div_code + "' and  FieldWork_Indicator = '" + worktype_sname + "' ";
            }
            else if (sf_type == "2")
            {
                strQry = "select WorkType_Code_M from Mas_WorkType_Mgr " +
                         " where division_code = '" + div_code + "' and  FieldWork_Indicator = '" + worktype_sname + "' ";
            }
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

        public DataSet getLeave(string sf_code, string leavedate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Leave_id,Leave_Active_Flag from mas_Leave_Form where sf_code = '" + sf_code + "'  and  '" + leavedate + "' between From_Date and To_Date and Leave_Active_Flag != 1";
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

        //Changes done by Priya

        public DataSet getReleaseDate(string SF_Code, string sMonth, string sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select convert(varchar,d.Delayed_Date,103) as Delayed_Date, s.Sf_HQ,s.Sf_Name +' - ' +' ( Delayed )' as Sf_Name,s.Sf_Code,s.sf_Designation_Short_Name," +
                    " 'D' Mode,a.StateName,convert(varchar,dateadd(day,-1,f.last_dcr_date),103) last_dcr_date from DCR_Delay_Dtls d, Mas_Salesforce s ,Mas_State a ,mas_salesforce_DCRTPdate f " +
                    " where d.Sf_Code='" + SF_Code + "' and MONTH(Delayed_Date)='" + sMonth + "' " +
                    " and Year(Delayed_Date)='" + sYear + "' and d.Sf_Code = s.Sf_Code and  Delayed_Flag =0 and s.State_Code=a.State_Code and s.sf_code=f.sf_code and delayed_date>=sf_TP_Active_Dt" +
                    " union " +
                    " select convert(varchar,d.Dcr_Missed_Date,103) as Delayed_Date, s.Sf_HQ,s.Sf_Name +' - ' +' ( APP Missing Dates )' as Sf_Name,s.Sf_Code,s.sf_Designation_Short_Name," +
                    " 'A' Mode,a.StateName,convert(varchar,dateadd(day,-1,f.last_dcr_date),103) last_dcr_date from DCR_MissedDates d, Mas_Salesforce s,Mas_State a,mas_salesforce_DCRTPdate f " +
                    " where d.Sf_Code='" + SF_Code + "' and MONTH(Dcr_Missed_Date)='" + sMonth + "' and  Dcr_Missed_Date not in " +
                   " (select Dcr_Missed_Date from mas_Leave_Form where sf_code=d.sf_code and Leave_Active_Flag " +
                    " in (0,2) and Dcr_Missed_Date between From_Date and To_Date) " +
                    " and Year(Dcr_Missed_Date)='" + sYear + "' and d.Sf_Code = s.Sf_Code and status=0 and s.State_Code=a.State_Code and s.sf_code=f.sf_code and Dcr_Missed_Date>=sf_TP_Active_Dt";
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
        public DataSet get_Release_Sf(string div_code, string imonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' as sf_code, '--Select--' as Sf_Name " +
                     " union " +
                    " select '0' as sf_code,'---All Fieldforce---' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_Delay_Dtls d inner join Mas_Salesforce b on d.division_code = '" + div_code + "' and d.Delayed_Flag=0 and month(delayed_date)='" + imonth + "' and year(delayed_date)='" + iYear + "' and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and b.Sf_Code =d.Sf_Code and delayed_date>=sf_TP_Active_Dt" +
                    " union " +
                    " select '' as sf_code, '--Select--' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_MissedDates d inner join Mas_Salesforce b on d.division_code = '" + div_code + "' and d.status=0 and month(Dcr_Missed_Date)='" + imonth + "' and year(Dcr_Missed_Date)='" + iYear + "' and " +
                   "  Dcr_Missed_Date not in " +
                   " (select Dcr_Missed_Date from mas_Leave_Form where sf_code=d.sf_code and Leave_Active_Flag " +
                   " in (0,2) and Dcr_Missed_Date between From_Date and To_Date) " +
                    " and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and b.Sf_Code =d.Sf_Code and Dcr_Missed_Date>=sf_TP_Active_Dt";
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
        public int Update_Delayed(string sf_code, DateTime ddate, string Mode, string div_code)
        {
            int iReturn = -1;
            DataSet dsDelay;
            //string delaydate = ddate.Year + "-" + ddate.Month.ToString() + "-" + ddate.Day;
            string delaydate = ddate.Month + "-" + ddate.Day + "-" + ddate.Year;
            try
            {

                DB_EReporting db = new DB_EReporting();

                if (Mode == "D")
                {
                    strQry = "UPDATE DCR_Delay_Dtls " +
                                " SET Delayed_Flag = 1 , " +
                                " Delay_Release_Date = getdate(), Released_by_Whom = 'admin' " +
                                " WHERE Sf_Code = '" + sf_code + "' and Delayed_Flag =0 and cast(Delayed_Date as date)='" + delaydate + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = " select Sf_Code from Activity_Delayed_Total where Sf_Code='" + sf_code + "' and Division_Code='" + div_code + "'";

                    dsDelay = db.Exec_DataSet(strQry);

                    if (dsDelay.Tables[0].Rows.Count > 0)
                    {
                        strQry = "Update Activity_Delayed_Total SET Release_Date = getdate() where sf_code='" + sf_code + "' and Division_Code='" + div_code + "'";

                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {

                        strQry = "Insert into Activity_Delayed_Total (Sf_Code,Release_Date,Released_by_Whom,Division_Code) Values " +
                               " ('" + sf_code + "',getdate(),'admin','" + div_code + "') ";

                        iReturn = db.ExecQry(strQry);
                    }
                }
                else
                {
                    strQry = "UPDATE DCR_MissedDates " +
                               " SET [status] = 1 , " +
                               " Missed_Release_Date = getdate(), Released_by_Whom = 'admin' " +
                               " WHERE Sf_Code = '" + sf_code + "' and status =0 and cast(Dcr_Missed_Date as date)='" + delaydate + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = " select Sf_Code from Activity_Delayed_Total where Sf_Code='" + sf_code + "' and Division_Code='" + div_code + "'";

                    dsDelay = db.Exec_DataSet(strQry);

                    if (dsDelay.Tables[0].Rows.Count > 0)
                    {
                        strQry = "Update Activity_Delayed_Total SET Release_Date = getdate() where sf_code='" + sf_code + "' and Division_Code='" + div_code + "'";

                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {

                        strQry = "Insert into Activity_Delayed_Total (Sf_Code,Release_Date,Released_by_Whom,Division_Code) Values " +
                             " ('" + sf_code + "',getdate(),'admin','" + div_code + "') ";

                        iReturn = db.ExecQry(strQry);
                    }
                }

                //iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getDCREdit(string SF_Code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Work_Type,Plan_No,Remarks from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '3'  and  Activity_Date = '" + DCRDate + "'";
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

        public int Update_Header(string sf_code, int trans_slno, bool reentry, string dcrdate, string div_code, string Activity_date, bool isdelayrel)
        {
            int iReturn = -1;
            int iReturnmax = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE DCRMain_Temp  SET Confirmed = 1  " +
                            " WHERE Sf_Code = '" + sf_code + "' and Trans_SlNo =" + trans_slno + "";

                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (reentry == false)
                    {

                        // Modified on 6th June - To update Last Dcr date in Sales force
                        DateTime dtDCR;
                        int iReturnsf = -1;
                        string Last_Dcr_Date = string.Empty;
                        dtDCR = Convert.ToDateTime(dcrdate);
                        dtDCR = dtDCR.AddDays(1);
                        Last_Dcr_Date = dtDCR.ToString("MM/dd/yyyy");

                        strQry = " Update Mas_Salesforce  set Last_Dcr_Date = '" + Last_Dcr_Date + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Sf_Code= '" + sf_code + "' ";

                        iReturnsf = db.ExecQry(strQry);
                    }
                    //if (trans_slno > 1)
                    //{
                    //    strQry = "update DCR_MaxSlNo set Max_Sl_No_Main = '" + trans_slno + "' where Division_Code = '" + div_code + "' ";
                    //    iReturnmax = db.ExecQry(strQry);
                    //}
                    if (isdelayrel == true)
                    {
                        int iReturndel = -1;
                        strQry = " Update DCR_Delay_Dtls  set Delayed_Flag =  2 where Sf_Code= '" + sf_code + "' and Delayed_Date = '" + Activity_date + "' and Division_Code = '" + div_code + "' ";

                        iReturndel = db.ExecQry(strQry);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getMgrWorkAreaDtls_All(string SF_Code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Work_Type, Territory_Code, sf_code from DCR_MGR_WorkAreaDtls Where " +
                     " MGR_Code ='" + SF_Code + "' and  DCR_Date ='" + DCRDate + "' ";

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
        // Changes done by Saravanan
        public DataSet get_Approved_dcr_stk_detailsView(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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
        //Added by sri - 15July
        public DataSet get_dcr_Not_Submitted(string sf_code, int cday, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select day(Activity_Date) pending_date from DCRMain_Temp " +
                     " where Sf_Code = '" + sf_code + "' and Confirmed = 1 and MONTH(Activity_Date) = " + imon + " and YEAR(Activity_Date) = " + iyear + " " +
                     " and DAY(Activity_Date) = " + cday + " " +
                     " UNION" +
                     " select day(Activity_Date) pending_date from DCRMain_Trans " +
                     " where Sf_Code = '" + sf_code + "' and Confirmed = 1 and MONTH(Activity_Date) = " + imon + " and YEAR(Activity_Date) = " + iyear + " " +
                     " and DAY(Activity_Date) = " + cday;

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
        //Changes done by Priya
        public DataSet get_delayed_Status(string sf_code, string smonth, string syear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = "select distinct CONVERT(varchar(10),Delay_Created_Date,103) from DCR_Delay_Dtls " +
            //         " where Month(Delay_Created_Date)=Month(getdate()) and  Sf_Code='" + sf_code + "'";
            strQry = " select distinct CONVERT(varchar(10),delayed_date,103) from DCR_Delay_Dtls " +
                    " where Sf_Code= '" + sf_code + "' and MONTH(delayed_date) = '" + smonth + "' and YEAR(delayed_date) =  '" + syear + "' and Delayed_Flag = 0";
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

        public DataSet getDCR_Report_MR_Calendar(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select WType_SName " +
                                 " from DCRMain_Trans a, Mas_WorkType_BaseLevel b " +
                                 " where a.Work_Type = b.WorkType_Code_B " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear +
                                 " union " +
                                 " select case confirmed  " +
                                 " when '1' then 'FW(NA)' " +
                                 " When '2' then 'FW(R)' End WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_BaseLevel b " +
                                 " where a.Work_Type = b.WorkType_Code_B " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + " and b.WType_SName='FW' and YEAR(Activity_Date) =  " + sYear +
                                 " union " +
                                 " select case confirmed " +
                                 " when '1' then 'L' " +
                                 " When '2' then '' End WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_BaseLevel b " +
                                 " where a.Work_Type = b.WorkType_Code_B " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + "  and YEAR(Activity_Date) =  " + sYear +
                                 " and b.WType_SName='L'" +
                                 " union " +
                                 " select WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_BaseLevel b " +
                                 " where a.Work_Type = b.WorkType_Code_B " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + "  and YEAR(Activity_Date) =  " + sYear +
                                 " and b.WType_SName != 'L'" +
                                 " union " +
                                 " select 'D' WType_SName  from DCR_Delay_Dtls where Sf_Code='" + SF_Code + "' and DAY(Delayed_Date) = " + sday +
                                 " and [Month] = " + sMonth + " and [Year] = " + sYear + " and Delayed_Flag=0 " +
                                 " union " +
                                 " select 'DR' WType_SName  from DCR_Delay_Dtls where Sf_Code='" + SF_Code + "' and DAY(Delayed_Date) = " + sday +
                                 " and [Month] = " + sMonth + " and [Year] = " + sYear + " and Delayed_Flag=1";

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
        public DataSet get_DCR_Rejected_Approval(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            if (sf_code.Contains("MR"))
            {
                strQry = " select distinct a.sf_code, a.sf_name,a.Sf_HQ,c.Designation_Short_Name,W.Worktype_Name_B,b.ReasonforRejection, " +
                            "  CONVERT(char(10),Activity_Date,103) as DCR_Activity_Date,MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
                            " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
                            " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
                            " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
                            " from Mas_Salesforce a, DCRMain_Temp b, Mas_SF_Designation c,Mas_WorkType_BaseLevel W      " +
                            " where a.sf_code = b.sf_code and  a.Designation_Code=c.Designation_Code and b.Work_Type = W.WorkType_Code_B  " +
                            " and a.SF_code  = '" + sf_code + "' and b.confirmed=2";
            }
            else
            {
                strQry = " select distinct a.sf_code, a.sf_name,a.Sf_HQ,c.Designation_Short_Name,Worktype_Name_M as Worktype_Name_B ,b.ReasonforRejection, " +
                            "  CONVERT(char(10),Activity_Date,103) as DCR_Activity_Date,MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
                            " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
                            " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
                            " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
                            " from Mas_Salesforce a, DCRMain_Temp b, Mas_SF_Designation c,Mas_WorkType_Mgr W      " +
                            " where a.sf_code = b.sf_code and  a.Designation_Code=c.Designation_Code and b.Work_Type = W.WorkType_Code_M  " +
                            " and a.SF_code  = '" + sf_code + "' and b.confirmed=2";
            }

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

        public bool chkdcrstpchg(string sf_type, string dcrdate, string div_code, string sf_code)
        {

            bool bRecordExist = false;
            DataSet dsdcr = null;
            DataSet dsadm = null;
            DateTime dcrsubdate = DateTime.Today;

            DateTime dcrsetdate = DateTime.Today;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT Submission_Date FROM DCRMain_Temp WHERE Division_Code = '" + div_code + "' and  Sf_Code = '" + sf_code + "' and Activity_Date ='" + dcrdate + "' ";

                dsdcr = db.Exec_DataSet(strQry);
                if (dsdcr.Tables[0].Rows.Count > 0)
                {
                    dcrsubdate = Convert.ToDateTime(dsdcr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                    if (sf_type == "1")
                    {
                        strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups where Division_Code = '" + div_code + "' ";
                    }
                    else if (sf_type == "2")
                    {
                        strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups_MGR where Division_Code = '" + div_code + "' ";
                    }

                    dsadm = db.Exec_DataSet(strQry);

                    if (dsadm.Tables[0].Rows.Count > 0)
                    {
                        dcrsetdate = Convert.ToDateTime(dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    if ((dsdcr.Tables[0].Rows.Count > 0) && (dsadm.Tables[0].Rows.Count > 0))
                    {
                        if (dcrsubdate < dcrsetdate) // Setup Change
                        {
                            bRecordExist = true;
                        }
                        else
                        {
                            bRecordExist = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool chkxml(string sf_type, DateTime dcrdate, string div_code, string sf_code)
        {

            bool bRecordExist = false;
            DataSet dsdcr = null;
            DataSet dsadm = null;
            DateTime dcrsubdate = DateTime.Today;

            DateTime dcrsetdate = DateTime.Today;
            try
            {
                DB_EReporting db = new DB_EReporting();


                if (sf_type == "1")
                {
                    strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups where Division_Code = '" + div_code + "' ";
                }
                else if (sf_type == "2")
                {
                    strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups_MGR where Division_Code = '" + div_code + "' ";
                }

                dsadm = db.Exec_DataSet(strQry);

                if (dsadm.Tables[0].Rows.Count > 0)
                {
                    if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
                        dcrsetdate = Convert.ToDateTime(dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                }
                if (dsadm.Tables[0].Rows.Count > 0)
                {
                    if (dcrdate < dcrsetdate) // Setup Change
                    {
                        bRecordExist = true;
                    }
                    else
                    {
                        bRecordExist = false;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int Clear_Header(string SF_Code, string Activity_Date)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE Division_Code = '" + Division_Code + "' and  Sf_Code = '" + SF_Code + "' and Activity_Date ='" + Activity_Date + "' ";
                Trans_SlNo = db.Exec_Scalar(strQry);

                if (Trans_SlNo > 0)
                {
                    strQry = "delete from DCRDetail_Lst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_CSH_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_UnLst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRmain_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Added - 7-Sep-15
        public DataSet getTerrChemists(string sfcode, string Terr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " AND Territory_Code = '" + Terr_Code + "' " +
                     " UNION ALL" +
                     " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " AND Territory_Code != '" + Terr_Code + "' ";


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
        //Added - 7-Sep-15
        public DataSet getTerrChemists_color(string sfcode, string Terr_Code) // Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " AND Territory_Code = '" + Terr_Code + "' ";
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

        public DataSet getTerrUnListedDoctorSrc(string sfcode, string Terr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND Territory_Code = '" + Terr_Code + "' " +
                        " UNION ALL" +
                        " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND Territory_Code != '" + Terr_Code + "' ";
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
        public DataSet getTerrUnListedDoctorSrc_Color(string sfcode, string Terr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND Territory_Code = '" + Terr_Code + "' ";
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

        //Added by Sri - To get Activity_date - for SlNo
        public DataSet getDCR_ActivityDate(int slno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Activity_Date from DCRMain_Trans " +
                     " Where Trans_SlNo =" + slno + " ";
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
        // 10-Sep-15 Added to create XML
        public DataSet get_Trans_Head(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Work_Type,Plan_No,Remarks,Start_Time" +
                     " from DCRMain_Temp " +
                     " where Sf_Code = '" + sf_code + "' " +
                     " and Activity_Date = '" + Activity_date + "'";

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


        public DataSet get_Lst_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Session,a.Session_Code,a.Time,a.Minutes,a.Seconds,a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                        "a.Worked_with_Name, a.Product_Code,a.Product_Detail,a.Additional_Prod_Code,a.Additional_Prod_Dtls,a.Gift_Code,a.Gift_Name,a.Gift_Qty, " +
                        "a.Additional_Gift_Code,a.Additional_Gift_Dtl,a.Activity_Remarks from DCRMain_Temp m, DCRDetail_Lst_Temp  a where " +
                        "a.trans_slno = m.trans_slno and " +
                        " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
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

        public DataSet get_Che_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                        "a.Worked_with_Name,POB,a.SDP from DCRMain_Temp m, DCRDetail_CSH_Temp  a where " +
                        "a.trans_slno = m.trans_slno and a.Trans_Detail_Info_Type= 2 and" +
                        " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
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

        public DataSet get_Stk_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                       "a.Worked_with_Name,POB,Visit_Type from DCRMain_Temp m, DCRDetail_CSH_Temp  a where " +
                       "a.trans_slno = m.trans_slno and a.Trans_Detail_Info_Type= 3 and" +
                       " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
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


        public DataSet get_Hos_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                          "a.Worked_with_Name,POB from DCRMain_Temp m, DCRDetail_CSH_Temp  a where " +
                          "a.trans_slno = m.trans_slno and a.Trans_Detail_Info_Type= 5 and" +
                          " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
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

        public DataSet get_UnLst_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Session,a.Session_Code,a.Time,a.Minutes,a.Seconds,a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                        "a.Worked_with_Name, a.Product_Code,a.Product_Detail,a.Additional_Prod_Code,a.Additional_Prod_Dtls,a.Gift_Code,a.Gift_Name,a.Gift_Qty, " +
                        "a.Additional_Gift_Code,a.Additional_Gift_Dtl,a.Activity_Remarks,a.SDP from DCRMain_Temp m, DCRDetail_UnLst_Temp  a where " +
                        "a.trans_slno = m.trans_slno and " +
                        " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
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


        //Added a function to fetch Work Type details by Sridevi on 09/16/15
        public DataSet DCR_get_WorkType(string div_code, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            if (sf_type == "1")
                strQry = "select FieldWork_Indicator, Button_Access, WorkType_Code_B from Mas_WorkType_BaseLevel Where division_code = '" + div_code + "' ";
            else if (sf_type == "2")
                strQry = "select FieldWork_Indicator, Button_Access, WorkType_Code_M from Mas_WorkType_Mgr Where division_code = '" + div_code + "' ";

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
        public int RecordDelMGRWorkArea(string MGR_Code, string DCRDate)
        {
            int iReturn = -1;
            int S_No = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT count(S_No) FROM DCR_MGR_WorkAreaDtls WHERE MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' ";
                S_No = db.Exec_Scalar(strQry);

                if (S_No > 0)
                {
                    strQry = "delete from DCR_MGR_WorkAreaDtls where MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' ";

                    iReturn = db.ExecQry(strQry);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        //Changes done by saravanan
        public DataSet DCR_get_WorkType(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B, Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where Division_Code='" + Division_Code + "' " +
                     "union " +
                     "select '0' WorkType_Code_B,'Missed Date Released' Worktype_Name_B,'MR'WType_SName " +
                     "UNION " +
                     "select '0' WorkType_Code_B,'Missed Date' Worktype_Name_B,'MD'WType_SName " +
                     " union " +
                     "select '0' WorkType_Code_B,'Not Approval' Worktype_Name_B,'NA'WType_SName";

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

        public DataSet LoadMailWorkwith(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " SELECT  'admin' Sf_Code,'admin-Level1' as sf_mail,Name as sf_name,'admin' Designation_Short_Name, 'ZZZ' Reporting_To_SF, '' Sf_HQ,'' sf_type,'-Level1' as sf_color, '' des_color ,'' Designation_Code from Mas_HO_ID_Creation" +
                      " Where (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%' or Division_Code like '" + div_code + "') " +
                      " UNION " +
                    " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color ,b.Designation_Code" +
                    " from Mas_Salesforce  a, Mas_SF_Designation b    " + // AM Level
                            " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                            " UNION" +
                           " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                            " UNION " +
                             " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                           " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                            " UNION " +
                             " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
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
        public DataTable LoadMailWorkwithDes(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color ,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " + // AM Level
                            " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                            " UNION" +
                           " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                            " UNION " +
                             " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                           " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                            " UNION " +
                             " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
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
            string[] TobeDistinct = { "Designation_Code", "Designation_Short_Name" };
            DataTable DesTable = GetDistinctRecords(dsTP, TobeDistinct);
            return DesTable;
        }

        //Following function will return Distinct records for Name, City and State column.
        public static DataTable GetDistinctRecords(DataSet dt, string[] Columns)
        {
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = dt.Tables[0].DefaultView.ToTable(true, Columns);
            return dtUniqRecords;
        }
        public DataSet get_DCRHoliday_Name_MR(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select b.Worktype_Name_B from DCRMain_Trans a,Mas_WorkType_BaseLevel b " +
                     " where a.Work_Type= b.WorkType_Code_B and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS') " +
                           " Union " +
                           " select b.Worktype_Name_B from DCRMain_Temp a,Mas_WorkType_BaseLevel b " +
                     " where a.Work_Type= b.WorkType_Code_B and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS') ";

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
        public DataSet get_DCRView_Approved_MGR_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_M ,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Trans a ,Mas_WorkType_Mgr wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.WorkType_Code_M order by Activity_Date";

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

        public DataSet get_DCRView_Pending_Approval_MGR_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_M as Worktype_Name_B ," +
                     " case Confirmed when '1' then  'Approval Pending' when '2' then 'DisApproved' End as Temp,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Temp a ,Mas_WorkType_Mgr wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.WorkType_Code_M ";

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

        public DataSet get_DCR_Status_Delay(string SF_Code, string sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select day(Delayed_Date) as Delay_Created_Date  FROM DCR_Delay_Dtls" +
                     " WHERE DAY(Delayed_Date) = '" + sday + "' and MONTH(Delayed_Date) = " + sMonth + " " +
                     " and YEAR(Delayed_Date) = " + sYear + " AND Sf_Code='" + SF_Code + "' and Delayed_Flag <> 0 ";

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

        public DataSet get_WorkType_DCR_Status(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B, Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where Division_Code='" + Div_Code + "' and active_flag=0 ";

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

        public DataSet get_Report_DCR_Status(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WType_SName,'0' Activity_Date " +
                     " from DCRMain_Trans a, Mas_WorkType_BaseLevel b ,Mas_Salesforce c" +
                     " where a.Work_Type = b.WorkType_Code_B " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 1  " +
                     " Union " +
                     "select WType_SName,'0' Activity_Date " +
                     " from DCRMain_Trans a, Mas_WorkType_Mgr b,Mas_Salesforce c " +
                     " where a.Work_Type = b.WorkType_Code_M " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 2 " +
                     " Union " +
                     "select case WType_SName " +
                     " when 'L' then 'LP' END AS WType_SName,DAY(a.Activity_Date)Activity_Date " +
                     " from DCRMain_Temp a, Mas_WorkType_BaseLevel b ,Mas_Salesforce c" +
                     " where a.Work_Type = b.WorkType_Code_B " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 1  " +
                     " Union " +
                     "select case WType_SName " +
                     " when 'L' then 'LP' END AS WType_SName,DAY(a.Activity_Date)Activity_Date " +
                     " from DCRMain_Temp a, Mas_WorkType_Mgr b,Mas_Salesforce c " +
                     " where a.Work_Type = b.WorkType_Code_M " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 2 ";

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

        public DataSet DCR_TotalFLDWRKQuery_MGR(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_Mgr B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F'";

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

        public DataSet DCR_CSH_Calls_Seen(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_CSH_Calls_Seen  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_Stock_Calls_Seen(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Stock_Calls_Seen  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        //Added by Sridevi - to improve the performance of DCR STatus Report
        public DataSet getDCR_Report_Det_New(string sf_code, string div_code, int iMonth, int iYear, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sf_type == "1")
            {

                strQry = "EXEC DCR_Status_MR  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";
            }
            else if (sf_type == "2")
            {
                strQry = "EXEC DCR_Status_MGR '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";
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
        public DataSet get_DCR_Status_Delay_Cnt(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select case D.Delayed_Flag " +
                     " when '0' then 'D' " +
                     " when '2' then 'E' " +
                     " when '1' then 'DR' end as Delayed_Flag,day(D.Delayed_Date) as Delay_Created_Date  FROM DCR_Delay_Dtls D,Mas_Salesforce S WHERE S.Sf_Code=D.Sf_Code " +
                     " and DAY(D.Delayed_Date) = " + sday + " and MONTH(D.Delayed_Date) = " + sMonth + " " +
                     " and YEAR(D.Delayed_Date) = " + sYear + " AND S.Sf_Code='" + SF_Code + "' ";

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

        public DataSet get_All_dcr_Pending_date_Count(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name,'1' as Temp,  " +
                     " case a.FieldWork_Indicator " +
                     " when 'L'  THEN 'LP'  " +
                     " else " +
                     " 'DAP' end FieldWork_Indicator " +
                     " from DCRMain_Temp a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union " +
                     "select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name,'' as Temp,  " +
                     " case a.FieldWork_Indicator " +
                     " when 'L'  THEN 'LP'  " +
                     " else " +
                     " 'DAP' end FieldWork_Indicator " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "";

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

        public DataSet get_DCR_Status_Delay_DCRView(string SF_Code, string sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select case D.Delayed_Flag " +
                     " when '0' then '( Delayed )' " +
                     " when '1' then '( Delay Relased )' " +
                     " when '2' then '( Delay )' end as Delayed_Flag  FROM DCR_Delay_Dtls D,Mas_Salesforce S WHERE S.Sf_Code=D.Sf_Code " +
                     " and DAY(D.Delayed_Date) = " + sday + " and MONTH(D.Delayed_Date) = " + sMonth + " " +
                     " and YEAR(D.Delayed_Date) = " + sYear + " AND S.Sf_Code='" + SF_Code + "' ";

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

        public DataSet get_DCRView_Approved_All_Dates(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date,a.FieldWork_Indicator, " +
            //         " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_B,'0' Temp,'Stockist Work' as Stockist," +
            //         " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
            //         "  (select sum(b.POB) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as Doc_POB, " +
            //         "convert(char(5),Start_Time,108) as Start_Time,convert(char(5),End_Time,108) as End_Time, " +
            //         "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
            //         "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
            //          "  FOR XML PATH(''), TYPE " +
            //          "  ).value('.', 'NVARCHAR(MAX)') " +
            //          " ,1,1,'') ) as che_POB_Name ," +
            //         " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
            //         " (select sum(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
            //         " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
            //         " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
            //         " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
            //         " a.Remarks from DCRMain_Trans a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
            //         " and Year(a.Activity_date) = '" + year + "'  " +
            //         " and a.work_type = wor.worktype_code_b " +
            //         " union " +
            //         " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date,a.FieldWork_Indicator, " +
            //         " day(a.Activity_Date) Activity_Date,a.Plan_Name,Worktype_Name_B, case Confirmed " +
            //         " when '1' then  '1' " +
            //         " when '2' then '2' when '3' then '3' End as Temp," +
            //         " 'Stockist Work' as Stockist," +
            //         " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
            //         " (select sum(b.POB) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as Doc_POB," +
            //         "convert(char(5),Start_Time,108) as Start_Time,convert(char(5),End_Time,108) as End_Time, " +
            //         " (STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
            //         " from DCRDetail_Lst_Temp c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
            //         "  FOR XML PATH(''), TYPE " +
            //         "  ).value('.', 'NVARCHAR(MAX)') " +
            //         " ,1,1,'') ) as che_POB_Name ," +
            //         " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
            //         " (select sum(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
            //         " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
            //         " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
            //         " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
            //         " a.Remarks from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and (a.confirmed=2 or a.Confirmed=1 or a.Confirmed=3) and Month(a.Activity_date) = '" + mon + "' " +
            //         " and Year(a.Activity_date) = '" + year + "'  " +
            //         " and a.work_type = wor.worktype_code_b " +
            //         " union " +
            //         " select  D.Sf_Code,'' as trans_slno,'' as Submission_Date,'' AS FieldWork_Indicator, day(D.Delayed_Date) as Activity_Date," +
            //         " '' as Plan_Name,'' as Worktype_Name_B,'0' Temp,'' as Stockist,'' as doc_cnt,'' as Doc_POB,'' as Start_Time,'' as End_Time,'' as che_cnt,'' as che_POB_Name,'' as che_POB,'' as stk_cnt," +
            //         " '' as hos_cnt,'' as Undoc_cnt,'' as Remarks  FROM DCR_Delay_Dtls D" +
            //         " WHERE  MONTH(D.Delayed_Date) = '" + mon + "' and YEAR(D.Delayed_Date) = '" + year + "' AND Sf_Code='" + sf_code + "' and (D.Delayed_Flag='1' or D.Delayed_Flag='0')" +
            //         " Union " +
            //         " select  D.Sf_Code,'' as trans_slno,'' as Submission_Date,'' AS FieldWork_Indicator, day(D.Dcr_Missed_Date) as Activity_Date," +
            //         " '' as Plan_Name,'' as Worktype_Name_B, " +
            //         " case Status " +
            //         " when '0'then'5' " +
            //         " when '1'then'6' end Temp, " +
            //         "'' as Stockist,'' as doc_cnt,'' as Doc_POB,'' as che_cnt,'' as che_POB_Name,'' as che_POB,'' as Start_Time,'' as End_Time,'' as stk_cnt," +
            //         " '' as hos_cnt,'' as Undoc_cnt,'' as Remarks  FROM DCR_MissedDates D" +
            //         " WHERE  MONTH(D.Dcr_Missed_Date) = '" + mon + "' and YEAR(D.Dcr_Missed_Date) = '" + year + "' AND Sf_Code='" + sf_code + "' and (Status=0 or Status=1) " +
            //         " order by Activity_Date";

            strQry = "EXEC GetDcrDetailView '" + sf_code + "','" + mon + "','" + year + "' ";

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

        public DataSet get_DCRView_Approved_MGR_All_Dates(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date,a.FieldWork_Indicator, " +
            //         " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_M,'0' Temp,'Stockist Work' as Stockist," +
            //         " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
            //         " (select sum(b.POB) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as Doc_POB," +
            //         "convert(char(5),Start_Time,108) as Start_Time,convert(char(5),End_Time,108) as End_Time, " +
            //         "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
            //         "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
            //          "  FOR XML PATH(''), TYPE " +
            //          "  ).value('.', 'NVARCHAR(MAX)') " +
            //          " ,1,1,'') ) as che_POB_Name ," +
            //         " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
            //         " (select COUNT(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
            //         " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
            //         " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
            //         " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
            //         " a.Remarks from DCRMain_Trans a ,Mas_WorkType_Mgr wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
            //         " and Year(a.Activity_date) = '" + year + "'  " +
            //         " and a.work_type = wor.WorkType_Code_M " +
            //        " union " +
            //        " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date,a.FieldWork_Indicator, " +
            //        " day(a.Activity_Date) Activity_Date,a.Plan_Name,Worktype_Name_M,case Confirmed " +
            //         " when '1' then  '1' " +
            //         " when '2' then '2' when '3' then '3' End as Temp," +
            //        " 'Stockist Work' as Stockist," +
            //        " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
            //        " (select sum(b.POB) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as Doc_POB," +
            //        "convert(char(5),Start_Time,108) as Start_Time,convert(char(5),End_Time,108) as End_Time, " +
            //        " (STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
            //         " from DCRDetail_Lst_Temp c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
            //         "  FOR XML PATH(''), TYPE " +
            //         "  ).value('.', 'NVARCHAR(MAX)') " +
            //         " ,1,1,'') ) as che_POB_Name ," +
            //        " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
            //        " (select COUNT(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
            //        " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
            //        " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
            //        " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
            //        " a.Remarks from DCRMain_Temp a ,Mas_WorkType_Mgr wor where a.Sf_Code = '" + sf_code + "' and (a.confirmed=2 or a.Confirmed=1 or a.Confirmed=3) and Month(a.Activity_date) = '" + mon + "' " +
            //        " and Year(a.Activity_date) = '" + year + "'  " +
            //        " and a.work_type = wor.WorkType_Code_M " +
            //        " union " +
            //        " select  D.Sf_Code,'' as trans_slno,'' as Submission_Date,'' AS FieldWork_Indicator, day(D.Delayed_Date) as Activity_Date," +
            //        " '' as Plan_Name,'' as Worktype_Name_B,'1' Temp,'' as Stockist,'' as doc_cnt,'' as Doc_POB,'' as Start_Time,'' as End_Time,'' as che_POB_Name, '' as che_cnt,'' as che_POB,'' as stk_cnt," +
            //        " '' as hos_cnt,'' as Undoc_cnt,'' as Remarks  FROM DCR_Delay_Dtls D" +
            //        " WHERE  MONTH(D.Delayed_Date) = '" + mon + "' and YEAR(D.Delayed_Date) = '" + year + "' AND Sf_Code='" + sf_code + "' and (D.Delayed_Flag='1' or D.Delayed_Flag='0') " +
            //        " Union " +
            //        " select  D.Sf_Code,'' as trans_slno,'' as Submission_Date,'' FieldWork_Indicator, day(D.Dcr_Missed_Date) as Activity_Date," +
            //        " '' as Plan_Name,'' as Worktype_Name_B, " +
            //        " case Status " +
            //        " when '0'then'5' " +
            //        " when '1'then'6' end Temp, " +
            //        "'' as Stockist,'' as doc_cnt,'' as che_cnt,'' as Doc_POB,'' as che_POB_Name,'' as che_POB,'' as Worked_With,'' as che_POB,'' as stk_cnt," +
            //        " '' as hos_cnt,'' as Undoc_cnt,'' as Remarks  FROM DCR_MissedDates D" +
            //        " WHERE  MONTH(D.Dcr_Missed_Date) = '" + mon + "' and YEAR(D.Dcr_Missed_Date) = '" + year + "' AND Sf_Code='" + sf_code + "' and (Status=0 or Status=1) " +
            //        " order by Activity_Date";

            strQry = "EXEC GetDcrDetailView_MGR '" + sf_code + "','" + mon + "','" + year + "'";

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

        public DataSet get_dcr_DCRPendingdate_DCRDetail(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union all" +
                     " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Temp a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " Union " +
                     " select convert(char(10),D.Delayed_Date,103) as Activity_Date," +
                     " '' as Submission_Date,'' as Plan_Name FROM DCR_Delay_Dtls D" +
                     " WHERE  MONTH(D.Delayed_Date) = '" + imon + "' and YEAR(D.Delayed_Date) = '" + iyear + "' " +
                     " AND Sf_Code='" + sf_code + "' and (D.Delayed_Flag='1' or D.Delayed_Flag='0') " +
                     " Union " +
                     " select convert(char(10),M.Dcr_Missed_Date,103) as Activity_Date," +
                     " '' as Submission_Date,'' as Plan_Name FROM DCR_MissedDates M" +
                     " WHERE  MONTH(M.Dcr_Missed_Date) = '" + imon + "' and YEAR(M.Dcr_Missed_Date) = '" + iyear + "' " +
                     " AND Sf_Code='" + sf_code + "' and (M.Status='1' or M.Status='0') ";

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

        public DataSet get_All_dcr_Sf_Code_date_Count(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct Sf_Code " +
                     " from DCRMain_Temp a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union " +
                     "select distinct Sf_Code " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "";

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

        public DataSet get_Temp_and_Approved_dcrLstDOC_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                        " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                        " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name  , c.Doc_cat_shortname ,c.Doc_Spec_shortname,c.Doc_qua_name, a.Plan_No, b.SDP_Name ,b.Session,b.Time,b.Trans_Detail_Slno,b.Activity_remarks,c.listeddr_address1,b.GeoAddrs,c.doc_qua_Name " +
                        " ,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx, b.activity_remarks " +
                        " ,(lati+'-'+long) as latilong,lati,long from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c" +
                        " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                        " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                        //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                        " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                        " Union " +
                        " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                        " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                        " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name  , c.Doc_cat_shortname ,c.Doc_Spec_shortname,c.Doc_qua_name, a.Plan_No, b.SDP_Name ,b.Session,b.Time,b.Trans_Detail_Slno,b.Activity_remarks,c.listeddr_address1,b.GeoAddrs,c.doc_qua_Name " +
                        " ,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx, b.activity_remarks  " +
                        " ,(lati+'-'+long) as latilong,lati,long from DCRMain_Temp a, DCRDetail_Lst_Temp b, Mas_ListedDr c " +
                        " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                        " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                        //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                        " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' order by b.Time asc ";
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

        public DataSet get_Temp_and_Approved_dcr_che_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                      " a.Plan_No, b.SDP_Name ,b.POB,b.Session,c.chemists_address1,b.GeoAddrs,b.additional_gift_dtl , b.Additional_Prod_Dtls as Product_Detail," +
                      " (select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.activity_remarks ,convert(char(5),vsttime,108) as Time " +
                      " ,(lati+'-'+long) as latilong,lati,long from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                       " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                       " Union " +
                      " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                      " a.Plan_No, b.SDP_Name ,b.POB,b.Session,c.chemists_address1,b.GeoAddrs,b.additional_gift_dtl, b.Additional_Prod_Dtls as Product_Detail," +
                      " (select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx)  as Rx, b.activity_remarks ,convert(char(5),vsttime,108) as Time " +
                      " ,(lati+'-'+long) as latilong,lati,long from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Chemists c " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                       " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' order by b.trans_detail_slno asc";

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

        public DataSet get_Temp_and_Approved_unlst_doc_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                                    " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                                     " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, b.SDP_Name,b.Session,b.Time,c.Unlisteddr_address1,b.Activity_remarks,b.Trans_Detail_Slno,b.GeoAddrs,d.Doc_cat_name,e.Doc_Special_name,b.GeoAddrs " +
                                    " ,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,  b.activity_remarks  " +
                                    " ,(lati+'-'+long) as latilong,lati,long from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                                    " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                                    " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                                    " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                                    " union " +
                                    " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                                    " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                                    " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, b.SDP_Name,b.Session,b.Time,c.Unlisteddr_address1,b.Activity_remarks,b.Trans_Detail_Slno,b.GeoAddrs,d.Doc_cat_name,e.Doc_Special_name,b.GeoAddrs " +
                                    " ,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx, b.activity_remarks  " +
                                    " ,(lati+'-'+long) as latilong,lati,long from DCRMain_Temp a, DCRDetail_Unlst_Temp b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                                    " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                                    " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                                    " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' order by b.Time asc";
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

        public DataSet get_Temp_and_Approved_dcr_stk_detailsView(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, b.SDP_Name,b.POB,(lati+'-'+long) as latilong,b.GeoAddrs " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                    " union " +
                    " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, b.SDP_Name,b.POB,(lati+'-'+long) as latilong,b.GeoAddrs " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' order by b.trans_detail_slno asc";

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

        public DataSet get_dcr_DCRPendingdate_MR(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a,Mas_WorkType_BaseLevel w" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and a.Work_Type=w.WorkType_Code_B and w.WType_SName='FW'" +
                     " union all" +
                     " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     "  from DCRMain_Temp a,Mas_WorkType_BaseLevel w" +
                      " where a.Sf_Code = '" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and a.Work_Type=w.WorkType_Code_B and w.WType_SName='FW' order by Activity_Date ";

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

        public DataSet get_dcr_DCRPendingdate_MGR(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a,Mas_WorkType_Mgr w" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and a.Work_Type=w.WorkType_Code_M and w.WType_SName='FW'" +
                     " union all" +
                     " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Temp a,Mas_WorkType_Mgr w" +
                      " where a.Sf_Code = '" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and a.Work_Type=w.WorkType_Code_M and w.WType_SName='FW' order by Activity_Date ";

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
        public DataSet get_Pending_Single_Temp_Date(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name, " +
                     " case Confirmed  when '1' then  'Approval Pending'  when '2' then 'DisApproved' End as Temp " +
                     " from DCRMain_Temp a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

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


        // Added by sridevi
        public DataSet New_DCR_Visit_TotalDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "Exec [getCallAvg] '" + sf_code + "','" + iMonth + "-01-" + iYear + "','" + iMonth + "','" + iYear + "'";

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
        public DataSet New_DCR_Visit_TotalDocQuery_Met(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "Exec [getCallAvg_Met] '" + sf_code + "','" + iMonth + "-01-" + iYear + "','" + iMonth + "','" + iYear + "'";

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

        //New_DCR_TotalChemistQuery

        public DataSet New_DCR_TotalChemistQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(c.trans_detail_info_code) Che_count from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and c.Trans_Detail_Info_Type= 2 " +
                    " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear;

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

        //New_DCR_TotalStockistQuery
        public DataSet New_DCR_TotalStockistQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(c.trans_detail_info_code) Stk_count from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and c.Trans_Detail_Info_Type= 3 " +
                    " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear;

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
        //New_DCR_TotalUnlstDocQuery

        public DataSet New_DCR_TotalUnlstDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = strQry = "select count(c.trans_detail_info_code) UnDoc_count from DCRMain_Trans a,DCRDetail_UnLst_Trans  c" +
                     " where a.Trans_SlNo = c.Trans_SlNo " +
                     " and a.Sf_Code = c.sf_code " +
                     " and a.Sf_Code='" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear;


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
        public DataSet get_All_dcr_Sf_Code_date_Count(int imon, int iyear, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct Sf_Code " +
                     " from DCRMain_Temp a" +
                     " where a.division_code = '" + divcode + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union " +
                     "select distinct Sf_Code " +
                     " from DCRMain_Trans a" +
                     " where a.division_code = '" + divcode + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "";

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
        public DataSet getDCR_Report_MGR_Calendar(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select WType_SName " +
                                 " from DCRMain_Trans a, Mas_WorkType_Mgr b " +
                                 " where a.Work_Type = b.WorkType_Code_M " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear +
                                 " union " +
                                 " select case confirmed  " +
                                 " when '1' then 'FW(NA)' " +
                                 " When '2' then 'FW(R)' End WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_Mgr b " +
                                 " where a.Work_Type = b.WorkType_Code_M " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + " and b.WType_SName='FW' and YEAR(Activity_Date) =  " + sYear +
                                 " union  " +
                                 " select case confirmed  " +
                                 " when '1' then 'L'  " +
                                 " When '2' then '' End WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_Mgr b " +
                                 " where a.Work_Type = b.WorkType_Code_M " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date)= " + sMonth + " and YEAR(Activity_Date) =  " + sYear +
                                 " and b.WType_SName='L' " +
                                 " union " +
                                 " select WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_Mgr b " +
                                 " where a.Work_Type = b.WorkType_Code_M " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + "  and YEAR(Activity_Date) =  " + sYear +
                                 " and b.WType_SName != 'L' " +
                                 " union " +
                                 " select 'D' WType_SName  from DCR_Delay_Dtls where Sf_Code='" + SF_Code + "' and DAY(Delayed_Date) = " + sday +
                                 " and [Month] = " + sMonth + " and [Year] = " + sYear + " and Delayed_Flag=0 " +
                                 " union " +
                                 " select 'DR' WType_SName  from DCR_Delay_Dtls where Sf_Code='" + SF_Code + "' and DAY(Delayed_Date) = " + sday +
                                 " and [Month] = " + sMonth + " and [Year] = " + sYear + " and Delayed_Flag=1";

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
        public DataSet getLeave_Mr(string sf_code, DateTime leavedate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            string leave_date = leavedate.Month.ToString() + "-" + leavedate.Day + "-" + leavedate.Year;
            strQry = "select Leave_id,Leave_Active_Flag from mas_Leave_Form where sf_code = '" + sf_code + "'  and  '" + leave_date + "' between From_Date and To_Date and Leave_Active_Flag != 1";
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
        public DataSet getHQ_Mgr(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_get_Rep_MgrHQ] '" + sf_code + "' ";

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

        public DataSet getMonth_Count(int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select day(dateadd(day,-1,dateadd(month,1,CAST(cast('" + imonth + "' as varchar)+'/1/'+CAST('" + iyear + "' as varchar) as DATEtime)))) ";

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

        public DataSet getWorking_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
                      " and FieldWork_Indicator Not IN ('W','H') and Division_Code = '" + div_code + "' ";

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

        public DataSet getFieldwork_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
                      " and FieldWork_Indicator = 'F' and Division_Code = '" + div_code + "' ";

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
        public DataSet getCoverage_anlaysis(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " SELECT 'Total Days in Month', day(dateadd(day,-1,dateadd(month,1,CAST(cast('" + iMonth + "' as varchar)+'/1/'+CAST('" + iYear + "' as varchar) as DATEtime)))) union all " +
                   " SELECT 'Working Days (Excl/Holidays & Sundays )',  count(FieldWork_Indicator) from DCRMain_Trans where  " +
                   " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' and FieldWork_Indicator != 'w'  " +
                   " and FieldWork_Indicator != 'L' and Division_Code = '" + div_code + "'" +
            " union all " +
            " select 'Fieldwork Days', " +
            "  count(FieldWork_Indicator) from DCRMain_Trans where  " +
            " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
            " and FieldWork_Indicator = 'F' and Division_Code = '" + div_code + "'" +
            "  union all " +
            " select  'Leave', " +
            " count(FieldWork_Indicator) from DCRMain_Trans where  " +
            " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
            " and FieldWork_Indicator = 'L' and Division_Code = '" + div_code + "' " +
                  " union all " +
                  " select 'TP Deviation Days', '' union all " +
                  " select 'No of Listed Drs Met', '' union all " +
                  " select 'No of Listed Drs Seen', '' union all " +
                  " select 'Call Average', ''  ";
            //" select Doc_Cat_SName " +
            //" from Mas_Doctor_Category where Division_Code = '" + div_code + "' and Doc_Cat_Active_Flag = 0";

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


        public DataSet getLeave_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
                      " and FieldWork_Indicator = 'L' and Division_Code = '" + div_code + "' ";

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

        public DataSet getCoverage(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT 'Total Days in Month' union all " +
                     " SELECT 'Working Days (Excl/Holidays & Sundays )'  " +
                     " union all " +
                     " select 'Fieldwork Days' union all " +
                     " select  'Leave' union all " +
                     " select 'TP Deviation Days' union all " +
                     " select 'No of Listed Drs Met' union all " +
                     " select 'No of Listed Drs Seen' union all " +
                     " select 'Call Average' union all " +
                     " select Doc_Cat_SName " +
                     " from Mas_Doctor_Category where Division_Code = '" + div_code + "' and Doc_Cat_Active_Flag = 0";

            //" SELECT 'Total Days in Month', " +
            //" day(dateadd(day,-1,dateadd(month,1,CAST(cast('12' as varchar)+'/1/'+CAST('2015' as varchar) as DATEtime)))) union all  " +
            //" SELECT 'Working Days (Excl/Holidays & Sundays )', " +
            //" count(FieldWork_Indicator) from DCRMain_Trans where  " +
            //" MONTH(Activity_Date) = '12'  and YEAR(Activity_Date) = '2015' and Sf_Code='mgr0048' and FieldWork_Indicator != 'w'  " +
            //" and FieldWork_Indicator != 'L' and Division_Code = '7'";


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

        public DataSet DCR_Doc_Met_Self(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Met_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        public DataSet DCR_Doc_Seen_Self(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Seen_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        public DataSet getDaysWorked(string mgr_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
            //         " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
            //          " and FieldWork_Indicator = 'L' and Division_Code = '" + div_code + "' ";

            //strQry = "select COUNT(DISTINCT DCR_Date) from DCR_MGR_WorkAreaDtls where MGR_Code='" + mgr_code + "' and " +
            //         " MONTH(DCR_Date)='" + iMonth + "'  and YEAR(DCR_Date)='" + iYear + "' and Sf_Code='" + sf_code + "'";

            strQry = " select count(distinct a.Activity_Date) " +
           " from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Territory_Creation t    " +
           " where month(a.Activity_Date)='" + iMonth + "' and YEAR(a.Activity_Date)='" + iYear + "' " +
           " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
           " and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
           " and t.Territory_Code= c.Territory_Code   ";

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

        public DataSet getHQCalls(string sf_code, int iMonth, int iYear, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select distinct day(DCR_Date) as dcrdate from DCR_MGR_WorkAreaDtls where Sf_Code='" + sf_code + "' " +
                            " and  MONTH(DCR_Date)='" + iMonth + "'  and YEAR(DCR_Date)='" + iYear + "' and MGR_Code='" + mgr_code + "' ";

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

        public DataSet getHQCalls_Doc(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            //strQry = "select COUNT(DISTINCT ListedDrCode) from DCRDetail_Lst_Trans a, DCRMain_Trans b, Mas_ListedDr c where " +
            //         " a.Trans_Detail_Info_Code=c.ListedDrCode and a.Trans_SlNo=b.Trans_SlNo and c.sf_code = '" + sf_code + "' and  a.sf_code='" + mgr_code + "' " +
            //         " and  Month(b.Activity_Date) ='" + imon + "' and Year(b.Activity_Date) = '" + iyr + "'  ";
            strQry = " select count(distinct b.Trans_Detail_Info_Code) " +
" from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c   " +
" where month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
" and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
" and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "'";

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
        public DataSet getNonFieldwork_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
                      " and FieldWork_Indicator IN ('N')  and Division_Code = '" + div_code + "' ";

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

        public DataSet DCR_UnDoc_Met(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_UnlstDoc_Met  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet Get_workDays(string sf_code, string div_code, int iMonth, int iYear, string smode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_WorkDays  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", '" + smode + "' ";

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
        public DataSet Get_WorkDaysField(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_WorkDaysField  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        //public DataSet DCR_workwithDay(string mgr_code, string div_code, int iMonth, int iYear, string sf_code)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;

        //    strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
        //               " CHARINDEX('" + mgr_code + "',t.Worked_with_Code) > 0 and " +
        //               " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and d.sf_code='" + sf_code + "' ";

        //    try
        //    {
        //        dsAdmin = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsAdmin;
        //}
        public DataSet DCR_workwithDay(string mgr_code, string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_JointWorkDay '" + mgr_code + "', '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_workwithDocMet(string mgr_code, string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_JointWork_DocMet '" + mgr_code + "', '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_workwithDocSeen(string mgr_code, string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_JointWork_DocSeen '" + mgr_code + "', '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        public DataSet DCR_Doc_Met_Team(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Met_Team  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_Total_Call_Doc_Visit_Report(string sf_code, string div_code, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Total_Call_Doc_Visit_Report  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "' ";

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

        public DataSet Special_Visit_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Specg_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
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
        public DataSet DCR_workwithDay_JW(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithDate_JW(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct day(d.Activity_Date) as Activity_Date from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithCalls_JW(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  COUNT(distinct t.Trans_Detail_Info_Code)  from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";

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

        public DataSet DCR_workwithCalls_SfName(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct d.Trans_SlNo, t.Worked_with_Code,t.sf_code from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";


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
        public DataSet DCR_workwithDay_dist(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct t.Worked_with_Code as Worked_with_Code   from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithDay_JW_MR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "'";

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

        public DataSet DCR_workwithDate_JW_MR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct day(d.Activity_Date) as Activity_Date from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "'";

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

        public DataSet DCR_workwithCalls_JW_MR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  COUNT(distinct t.Trans_Detail_Info_Code)  from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "' ";

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

        public DataSet DCR_VisitDR_Catg_NoofVisit_Not_Equal(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int no_of_visit)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_VisitDR_Catg_NoofVisit_Not_Equal  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + ", " + no_of_visit + " ";

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

        public DataSet Visit_Doc_workedwith(string doc_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_Visit_Count_Workedwith '" + doc_code + "', '" + cmon + "', '" + cyear + "' ";

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

        public DataSet get_dcr_details_Maps(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.Trans_SlNo,a.Activity_Date,b.Trans_Detail_Name,b.GeoAddrs,b.Rx,b.lati,b.long " +
                      " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                      " Union all " +
                      " select a.Trans_SlNo,a.Activity_Date,b.Trans_Detail_Name,b.GeoAddrs,b.Rx,b.lati,b.long " +
                      " from DCRMain_Temp a, DCRDetail_Lst_Temp b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";



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

        public DataSet GetTPDayMap_MR(string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_Tb_My_Day_MR  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet GetTPDayMap_MGR(string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC SP_Get_Tb_My_Day_MGR  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        //Added by Sridevi - to improve the performance of DCR STatus Report
        public DataSet getDCR_Report_Det_New_withoutdoccnt(string sf_code, string div_code, int iMonth, int iYear, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sf_type == "1")
            {

                strQry = "EXEC DCR_Status_MR_Nodoc  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";
            }
            else if (sf_type == "2")
            {
                strQry = "EXEC DCR_Status_MGR_Nodoc '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";
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

        //Added by Sridevi - to improve the performance of DCR STatus Report
        public DataSet getDCR_Report_Det_New_per_withoutdoccnt(string sf_code, string div_code, string fdate, string tdate, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sf_type == "1")
            {

                strQry = "EXEC [DCR_Status_MR_Date_Nodoc]  '" + div_code + "', '" + sf_code + "', '" + fdate + "', '" + tdate + "' ";
            }
            else if (sf_type == "2")
            {
                strQry = "EXEC [DCR_Status_MGR_Date_Nodoc] '" + div_code + "', '" + sf_code + "', '" + fdate + "', '" + tdate + "' ";
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


        //Added by Sridevi - to improve the performance of DCR STatus Report
        public DataSet getDCR_Report_Det_New_Date(string sf_code, string div_code, string fdate, string tdate, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sf_type == "1")
            {

                strQry = "EXEC [DCR_Status_MR_Date]  '" + div_code + "', '" + sf_code + "', '" + fdate + "', '" + tdate + "' ";
            }
            else if (sf_type == "2")
            {
                strQry = "EXEC [DCR_Status_MGR_Date] '" + div_code + "', '" + sf_code + "', '" + fdate + "', '" + tdate + "' ";
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
        public DataSet DCR_work_JW(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = " select  distinct day(d.Activity_Date) as Activity_Date from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
            //         " t.Division_Code = '" + div_code + "' and   CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
            //         " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";
            strQry = " select  distinct day(d.Activity_Date) as Activity_Date, d.sf_code, t.Worked_with_Code from DCRDetail_Lst_Trans t, DCRMain_Trans d where  " +
                     " t.Division_Code = '" + div_code + "' and  " +
                     " t.Worked_with_Code like '%" + mgr_code + "%'   and  " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and  " +
                     " YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";

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

        public DataSet get_Release_Sf_MR(string SF_Code, string imonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' as sf_code, '--Select--' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_Delay_Dtls d inner join Mas_Salesforce b on d.Sf_Code = '" + SF_Code + "' and d.Delayed_Flag=0 and month(delayed_date)='" + imonth + "' and year(delayed_date)='" + iYear + "' and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and b.Sf_Code =d.Sf_Code and delayed_date>=sf_TP_Active_Dt " +
                    " union " +
                    " select '' as sf_code, '--Select--' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_MissedDates d inner join Mas_Salesforce b on d.Sf_Code = '" + SF_Code + "' and d.status=0 and month(Dcr_Missed_Date)='" + imonth + "' and year(Dcr_Missed_Date)='" + iYear + "' and " +
                     "  Dcr_Missed_Date not in " +
                   " (select Dcr_Missed_Date from mas_Leave_Form where sf_code=d.sf_code and Leave_Active_Flag " +
                   " in (0,2) and Dcr_Missed_Date between From_Date and To_Date) and " +
                    "b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and b.Sf_Code =d.Sf_Code and Dcr_Missed_Date>=sf_TP_Active_Dt";
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
        //Coverage

        public DataSet Get_WorkDaysField_all(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and sf_code ='" + sf_code + "'" +
                     " and FieldWork_Indicator != 'W' and FieldWork_Indicator != 'L' and Division_Code = '" + div_code + "'   ";

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

        public DataSet Get_WorkDaysFieldWork_All(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                      " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and sf_code ='" + sf_code + "'" +
                      " and FieldWork_Indicator = 'F'  and Division_Code = '" + div_code + "'   ";

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
        public DataSet Get_WorkDaysNoFW_All(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                      " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and sf_code ='" + sf_code + "'" +
                      " and FieldWork_Indicator = 'N'  and Division_Code = '" + div_code + "'   ";

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

        public DataSet Get_WorkDaysLeave_All(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                      " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and sf_code ='" + sf_code + "'" +
                      " and FieldWork_Indicator = 'L'  and Division_Code = '" + div_code + "'   ";

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

        public DataSet Get_Jwday_MR(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                     " CHARINDEX(''' +@mgr_code+ ''',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=' + @cmonth + ' and YEAR(d.Activity_Date)=' + @cyear + ' and t.Trans_SlNo= d.Trans_SlNo and d.sf_code in (''' + @sf_code + ''')'    ";


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

        public DataSet Get_Doc_sumary(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(b.Trans_Detail_Info_Code) " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b, Mas_ListedDr c " +
                     " where a.Sf_Code ='" + sf_code + "' and month(a.Activity_Date)='" + iMonth + "' and YEAR(a.Activity_Date)='" + iYear + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode ";


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

        public DataSet Get_JW_Day_Mr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, " +
                     " DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                     " t.Worked_with_Code like '%mgr%' and  " +
                     " MONTH(d.Activity_Date)='" + iMonth + "' and YEAR(d.Activity_Date)='" + iYear + "' " +
                     "  and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";


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
        public DataSet Get_JW_Day_MGR(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, " +
                     " DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                     " t.Worked_with_Code like '%mr%' and  " +
                     " MONTH(d.Activity_Date)='" + iMonth + "' and YEAR(d.Activity_Date)='" + iYear + "' " +
                     "  and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";


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

        public DataSet Get_JW_DocMet_Mr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct t.Trans_Detail_Info_Code) from DCRDetail_Lst_Trans t, " +
                     " DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                     " t.Worked_with_Code like '%mgr%' and  " +
                     " MONTH(d.Activity_Date)='" + iMonth + "' and YEAR(d.Activity_Date)='" + iYear + "' " +
                     "  and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";


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


        public DataSet Get_JW_DocMet_Mgr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct t.Trans_Detail_Info_Code) from DCRDetail_Lst_Trans t, " +
                     " DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                     " t.Worked_with_Code like '%mr%' and  " +
                     " MONTH(d.Activity_Date)='" + iMonth + "' and YEAR(d.Activity_Date)='" + iYear + "' " +
                     "  and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";


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

        public DataSet Get_JW_DocSeen_Mr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(t.Trans_Detail_Info_Code) from DCRDetail_Lst_Trans t, " +
                     " DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                     " t.Worked_with_Code like '%mgr%' and  " +
                     " MONTH(d.Activity_Date)='" + iMonth + "' and YEAR(d.Activity_Date)='" + iYear + "' " +
                     "  and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";


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


        public DataSet Get_JW_DocSeen_Mgr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(t.Trans_Detail_Info_Code) from DCRDetail_Lst_Trans t, " +
                     " DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                     " t.Worked_with_Code like '%mr%' and  " +
                     " MONTH(d.Activity_Date)='" + iMonth + "' and YEAR(d.Activity_Date)='" + iYear + "' " +
                     "  and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";


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
        public DataSet Get_Doc_Met_cnt(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(distinct b.Trans_Detail_Info_Code) " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b, Mas_ListedDr c " +
                     " where a.Sf_Code ='" + sf_code + "' and month(a.Activity_Date)='" + iMonth + "' and YEAR(a.Activity_Date)='" + iYear + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode ";


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

        public DataSet Doctor_count(string div_code, string sf_code, int cmonth, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_LstDr_CntOnly  '" + div_code + "', '" + sf_code + "'," + cmonth + "," + cyear + ", '" + dtcurrent + "' ";

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
        public DataSet DCR_Doc_Met_Cov(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec sp_coverage_New '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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

        public DataSet DCR_Workdays_SF(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec Workdays_Coverage '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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

        public DataSet DCR_doc_summary(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec Doc_Summary '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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

        public DataSet JW_Workdays_Coverage(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec JW_Workdays_Coverage '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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

        public DataSet JW_Work_met_Coverage(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec JW_Workdays_Met_Coverage '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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

        public DataSet JW_Work_seen_Coverage(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec JW_Workdays_seen_Coverage '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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

        public DataSet Doc_Met_Cnt_Coverage(string sf_code, string div_code, int iMonth, int iYear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec Doc_Met_Cnt_Coverage '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + ", '" + dtcurrent + "'";

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

        public DataSet Doc_Unlst_Met_Cnt(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec Doc_Unlst_Met_Cnt '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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


        public DataSet Doc_Met_Cnt_Repeatedcalls(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec Doc_Met_Cnt_Repeatedcalls '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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

        public DataSet Get_LstDr_Cnt_sf(string div_code, string sf_code, int iMonth, int iYear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec sp_Get_LstDr_Cnt1  '" + div_code + ',' + "','" + sf_code + "', " + iMonth + ", " + iYear + ", '" + dtcurrent + "'";

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
        public DataSet Get_Chemist_Met(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec Chem_Summary  '" + sf_code + "', '" + div_code + ',' + "', " + iMonth + ", " + iYear + "";

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
        public DataSet DCR_Doc_Met_Count(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(distinct b.Trans_Detail_Info_Code)  from DCRMain_Trans a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "'  " +
                     " and YEAR(a.Activity_Date)= '" + iYear + "'  " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     " and a.sf_code = b.sf_code and a.Division_code = '" + div_code + "' ";

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

        //Hq coverage
        public DataSet getHQCalls_HQ(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select count(distinct b.Trans_Detail_Info_Code) " +
               " from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Territory_Creation t    " +
               " where month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
               " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
               " and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
               " and t.Territory_Code= c.Territory_Code and t.Territory_Cat=1  ";

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

        public DataSet getHQCalls_EX(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select count(distinct b.Trans_Detail_Info_Code) " +
               " from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Territory_Creation t    " +
               " where month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
               " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
               " and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
               " and t.Territory_Code= c.Territory_Code and t.Territory_Cat=2  ";

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

        public DataSet getHQCalls_OS(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select count(distinct b.Trans_Detail_Info_Code) " +
               " from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Territory_Creation t    " +
               " where month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
               " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
               " and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
               " and t.Territory_Code= c.Territory_Code and (t.Territory_Cat=3 or t.Territory_Cat=4)  ";

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


        public DataSet getHQDay_HQ(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select count(distinct a.Activity_Date) " +
               " from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Territory_Creation t    " +
               " where month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
               " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
               " and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
               " and t.Territory_Code= c.Territory_Code and t.Territory_Cat=1  ";

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

        public DataSet getHQDay_EX(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select count(distinct a.Activity_Date) " +
               " from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Territory_Creation t    " +
               " where month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
               " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
               " and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
               " and t.Territory_Code= c.Territory_Code and t.Territory_Cat=2  ";

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

        public DataSet getHQDay_OS(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select count(distinct a.Activity_Date) " +
               " from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Territory_Creation t    " +
               " where month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
               " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
               " and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
               " and t.Territory_Code= c.Territory_Code and (t.Territory_Cat=3 or t.Territory_Cat=4)  ";

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
        public DataSet getHQDay_HQ_Cnt(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            //strQry = " select distinct a.Activity_Date, t.Territory_Cat " +
            //   " from DCRMain_Trans  a,DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Territory_Creation t    " +
            //   " where month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
            //   " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
            //   " and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
            //   " and t.Territory_Code= c.Territory_Code  ";

            strQry = "  create table #test(A_Date datetime,Terri_C varchar(50)) " +
                     "  insert into #test(A_Date,Terri_C) " +
                     "  select  a.Activity_Date, t.Territory_Cat  from DCRMain_Trans  a,DCRDetail_Lst_Trans b, " +
                     "  Mas_ListedDr c, Mas_Territory_Creation t where month(a.Activity_Date)='" + imon + "'  " +
                     "  and YEAR(a.Activity_Date)='" + iyr + "'  and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     "  and b.Trans_Detail_Info_Code=c.ListedDrCode and b.sf_code='" + mgr_code + "' and c.Sf_Code='" + sf_code + "' " +
                     "  and t.Territory_Code= c.Territory_Code  " +
                     "  SELECT t.A_Date, STUFF((SELECT distinct ',' + s.Terri_C " +
                     "  FROM #test s WHERE s.A_Date = t.A_Date " +
                     "  FOR XML PATH('')),1,1,'') AS CSV " +
                     "  FROM #test AS t " +
                     "  GROUP BY t.A_Date " +
                     "  drop table #test ";


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

        public DataSet getcombination(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = "select Combination,Output,Territory_Cat from Expense_Combination where Division_code = '" + div_code + "' ";
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

        public DataSet getvisit_drcnt(string sf_code, int imon, int iyr, string listeddrcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select  count(Trans_Detail_Info_Code) from DCRMain_Trans  a,DCRDetail_Lst_Trans b  " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and b.Trans_Detail_Info_Code='" + listeddrcode + "'  ";

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

        public DataSet getvisit_drdate(string sf_code, int imon, int iyr, string listeddrcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select  day(a.Activity_Date) as Activity_Date  from DCRMain_Trans  a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)='" + imon + "' and YEAR(a.Activity_Date)='" + iyr + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and Trans_Detail_Info_Code='" + listeddrcode + "'  " +
                     " order by a.Activity_Date";

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
        public DataSet Get_LstDr_Cnt_dr(string div_code, string sf_code, int iMonth, int iYear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec sp_Get_LstDr_Cnt  '" + div_code + ',' + "','" + sf_code + "', " + iMonth + ", " + iYear + ", '" + dtcurrent + "'";

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

        public DataSet get_Release_All_fieldforce(string div_code, string imonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
            //        "  from DCR_Delay_Dtls d where d.division_code = '" + div_code + "' and d.Delayed_Flag=0 and month(delayed_date)='" + imonth + "' and year(delayed_date)='" + iYear + "'" +
            //        " union " +               
            //        " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
            //        "  from DCR_MissedDates d where d.division_code = '" + div_code + "' and d.status=0 and month(Dcr_Missed_Date)='" + imonth + "' and year(Dcr_Missed_Date)='" + iYear + "'";

            strQry = " select distinct(a.Sf_Code),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ , '' Mode,'' Delayed_Date,c.StateName,convert(varchar,dateadd(day,-1,f.last_dcr_date),103) last_dcr_date from DCR_Delay_Dtls a inner join " +
                   " Mas_Salesforce b on a.Division_Code='" + div_code + "' and month(a.Delayed_Date)='" + imonth + "' and year(a.Delayed_Date)='" + iYear + "' and Delayed_Flag=0 and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and  a.Sf_Code=b.Sf_Code " +
                   " inner join Mas_State c on b.State_Code=c.State_Code inner join mas_salesforce_DCRTPdate f on b.sf_code=f.sf_code and delayed_date>=sf_TP_Active_Dt " +
                   " union  " +
                   " select distinct(a.Sf_Code ),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ,'' Mode ,'' Delayed_Date,c.StateName,convert(varchar,dateadd(day,-1,f.last_dcr_date),103) last_dcr_date from  DCR_MissedDates a " +
                   " inner join Mas_Salesforce b on  a.Division_Code='" + div_code + "' and month(a.Dcr_Missed_Date)='" + imonth + "' and Dcr_Missed_Date not in " +
                   " (select Dcr_Missed_Date from mas_Leave_Form where sf_code=a.sf_code and Leave_Active_Flag " +
                  " in (0,2) and Dcr_Missed_Date between From_Date and To_Date)  " +
                   " and year(a.Dcr_Missed_Date)='" + iYear + "' and Status=0 and  b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and a.Sf_Code=b.Sf_Code inner join Mas_State c on b.State_Code=c.State_Code inner join mas_salesforce_DCRTPdate f on b.sf_code=f.sf_code and Dcr_Missed_Date>=sf_TP_Active_Dt  order by StateName ";

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
        public int Update_Delayed_Dates_ForAll(string sf_code, int month, int year, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                //strQry = "UPDATE DCR_Delay_Dtls " +
                //            " SET Delayed_Flag = 1 , " +
                //            " Delay_Release_Date = getdate(), Released_by_Whom = 'admin' " +
                //            " WHERE Sf_Code = '" + sf_code + "' and Delayed_Flag =0 and month(Delayed_Date)='"+month+"' and year(Delayed_Date)='"+year+"' and Division_code='"+div_code+"' ";

                //iReturn = db.ExecQry(strQry);

                //strQry = "UPDATE DCR_MissedDates " +
                //           " SET [status] = 1 , " +
                //           " Missed_Release_Date = getdate(), Released_by_Whom = 'admin' " +
                //           " WHERE Sf_Code = '" + sf_code + "' and status =0 and month(Dcr_Missed_Date)='" + month + "' and year(Dcr_Missed_Date)='"+year+"' and Division_code='"+div_code+"' ";

                strQry = "EXEC Delayed_Update_withrollback '" + sf_code + "','" + month + "','" + year + "', '" + div_code + "' ";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet DCR_Delayed_missed_dates(string sf_code, int cmon, int cyear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC DCR_Delayed_missed_dates '" + sf_code + "', '" + cmon + "', '" + cyear + "','" + div_code + "' ";

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

        public DataSet get_Release_All_fieldforce_Search(string div_code, string imonth, string iYear, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct(a.Sf_Code),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ , '' Mode,'' Delayed_Date,c.StateName,convert(varchar,dateadd(day,-1,b.last_dcr_date),103) last_dcr_date from DCR_Delay_Dtls a inner join " +
                     " Mas_Salesforce b on a.Division_Code='" + div_code + "' and month(a.Delayed_Date)='" + imonth + "' and year(a.Delayed_Date)='" + iYear + "' and Delayed_Flag=0 and a.Sf_Code=b.Sf_Code and b.State_Code='" + state_code + "' inner join Mas_State c on b.State_Code=c.State_Code and delayed_date>=sf_TP_Active_Dt union  " +
                     " select distinct(a.Sf_Code ),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ,'' Mode ,'' Delayed_Date,c.StateName,convert(varchar,dateadd(day,-1,b.last_dcr_date),103) last_dcr_date from  DCR_MissedDates a " +
                     " inner join Mas_Salesforce b on  a.Division_Code='" + div_code + "' and month(a.Dcr_Missed_Date)='" + imonth + "'  " +
                     " and year(a.Dcr_Missed_Date)='" + iYear + "' and Status=0 and a.Sf_Code=b.Sf_Code and b.State_Code='" + state_code + "' inner join Mas_State c on b.State_Code=c.State_Code and Dcr_Missed_Date>=sf_TP_Active_Dt order by StateName ";

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
        public DataSet Get_tot_doc_Cat(string div_code, string sf_code, int cmonth, int cyear, DateTime cDate, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " SELECT  count(ListedDrCode) FROM Mas_ListedDr WHERE Division_Code= '" + div_code + "' " +
                   " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                   " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null)) AND " +
                   " sf_code='" + sf_code + "' and  Doc_Cat_Code ='" + catg_code + "'";


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

        public DataSet visit_cnt_1(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) = 1)as repcalls1  ";

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
        public DataSet visit_cnt_2(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) = 2)as repcalls1  ";

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
        public DataSet visit_cnt_3(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) = 3)as repcalls1  ";

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
        public DataSet visit_cnt_more(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) > 3)as repcalls1  ";

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
        public DataSet getcall_feed(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Feedback_Id,Feedback_Content,Division_Code,Act_Flag from Mas_App_CallFeedback where Division_Code = '" + div_code + "' and Act_Flag=0 ";
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

        public DataSet getfeed_doc(string div_code, string sf_code, int iMonth, int iYear, string callfeed)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "exec sp_DCR_Doc_Call_Feed  '" + div_code + "','" + sf_code + "', " + iMonth + ", " + iYear + ", '" + callfeed + "'";
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

        public int Update_Delayed_back(string sf_code, DateTime ddate, string Mode)
        {
            int iReturn = -1;
            //string delaydate = ddate.Year + "-" + ddate.Month.ToString() + "-" + ddate.Day;
            string delaydate = ddate.Month + "-" + ddate.Day + "-" + ddate.Year;
            try
            {

                DB_EReporting db = new DB_EReporting();

                if (Mode == "D")
                {
                    strQry = "UPDATE DCR_Delay_Dtls " +
                                " SET Delayed_Flag = 0 ," +
                                " Delay_Release_Date = NULL, Released_by_Whom = '' " +
                                " WHERE Sf_Code = '" + sf_code + "' and Delayed_Flag =1 and cast(Delayed_Date as date)='" + delaydate + "'";
                }
                else
                {
                    strQry = "UPDATE DCR_MissedDates " +
                               " SET [status] = 0 , " +
                               " Missed_Release_Date = NULL , Released_by_Whom = '' " +
                               " WHERE Sf_Code = '" + sf_code + "' and status =1 and cast(Dcr_Missed_Date as date)='" + delaydate + "'";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getReleaseDate_Back(string SF_Code, string sMonth, string sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select convert(varchar,d.Delayed_Date,103) as Delayed_Date, s.Sf_HQ,s.Sf_Name +' - ' +' ( Delayed )' as Sf_Name,s.Sf_Code,s.sf_Designation_Short_Name," +
                    " 'D' Mode,a.StateName,convert(varchar,dateadd(day,-1,s.last_dcr_date),103) last_dcr_date from DCR_Delay_Dtls d, Mas_Salesforce s,Mas_State a  " +
                    " where d.Sf_Code='" + SF_Code + "' and MONTH(Delayed_Date)='" + sMonth + "' " +
                    " and Year(Delayed_Date)='" + sYear + "' and d.Sf_Code = s.Sf_Code and  Delayed_Flag =1 and s.State_Code=a.State_Code" +
                    " union " +
                    " select convert(varchar,d.Dcr_Missed_Date,103) as Delayed_Date, s.Sf_HQ,s.Sf_Name +' - ' +' ( APP Missing Dates )' as Sf_Name,s.Sf_Code,s.sf_Designation_Short_Name," +
                    " 'A' Mode,a.StateName,convert(varchar,dateadd(day,-1,s.last_dcr_date),103) last_dcr_date from DCR_MissedDates d, Mas_Salesforce s,Mas_State a " +
                    " where d.Sf_Code='" + SF_Code + "' and MONTH(Dcr_Missed_Date)='" + sMonth + "' " +
                    " and Year(Dcr_Missed_Date)='" + sYear + "' and d.Sf_Code = s.Sf_Code and status=1 and s.State_Code=a.State_Code ";
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

        public DataSet get_Release_Sf_Back(string div_code, string imonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' as sf_code, '--Select--' as Sf_Name " +
                     " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_Delay_Dtls d inner join Mas_Salesforce b on d.division_code = '" + div_code + "' and d.Delayed_Flag=1 and month(delayed_date)='" + imonth + "' and year(delayed_date)='" + iYear + "' and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and b.Sf_Code =d.Sf_Code" +
                    " union " +
                    " select '' as sf_code, '--Select--' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_MissedDates d inner join Mas_Salesforce b on d.division_code = '" + div_code + "' and d.status=1 and month(Dcr_Missed_Date)='" + imonth + "' and year(Dcr_Missed_Date)='" + iYear + "' and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and b.Sf_Code =d.Sf_Code";
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

        public DataSet get_DCRRemarks1(string sf_code, int Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;



            strQry = " select distinct (select (Sf_Name+'-'+sf_Designation_Short_Name+'-'+Sf_HQ) from Mas_Salesforce where Sf_Code= a.Sf_Code)as Name,a.Sf_Code from DCRMain_Temp a,Mas_Salesforce b"
  + " where a.division_code = '" + sf_code + "' and MONTH(a.Activity_Date) = '" + Month + "' and YEAR(a.Activity_Date) = '" + Year + "'  and a.Sf_Code=b.Sf_Code"
  + " union"
  + " select distinct (select (Sf_Name+'-'+sf_Designation_Short_Name+'-'+Sf_HQ) from Mas_Salesforce where Sf_Code= a.Sf_Code)as Name,a.Sf_Code from DCRMain_Trans a,Mas_Salesforce b  where a.division_code = '" + sf_code + "'"
  + " and MONTH(a.Activity_Date) ='" + Month + "' and YEAR(a.Activity_Date) = '" + Year + "' and a.Sf_Code=b.Sf_Code";
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

        public DataSet getReleaseDate_Back_LockSystem(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct(a.Sf_Code),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ , '' Mode,'' Delayed_Date,c.StateName from DCR_Delay_Dtls a inner join " +
                     " Mas_Salesforce b on a.Division_Code='" + div_code + "' and Delayed_Flag=1 and a.Sf_Code=b.Sf_Code and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and a.Delayed_Date >=CONVERT(varchar,b.Sf_TP_DCR_Active_Dt,107)" +
                     " inner join Mas_State c on b.State_Code=c.State_Code " +
                     " union  " +
                     " select distinct(a.Sf_Code ),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ,'' Mode ,'' Delayed_Date,c.StateName from  DCR_MissedDates a " +
                     " inner join Mas_Salesforce b on  a.Division_Code='" + div_code + "'  " +
                     " and  Status=1 and a.Sf_Code=b.Sf_Code and b.sf_TP_Active_Flag=0 and a.Dcr_Missed_Date >=CONVERT(varchar, b.Sf_TP_DCR_Active_Dt,107) and b.SF_Status !=2  inner join Mas_State c on b.State_Code=c.State_Code order by StateName ";

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

        public DataSet get_Release_All_fieldforce_LockSystem(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;



            strQry = " select distinct(a.Sf_Code),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ , '' Mode,'' Delayed_Date,c.StateName,convert(varchar,dateadd(day,-1,f.last_dcr_date),103) last_dcr_date from DCR_Delay_Dtls a inner join " +
                     " Mas_Salesforce b on a.Division_Code='" + div_code + "' and Delayed_Flag=0 and a.Sf_Code=b.Sf_Code and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and a.Delayed_Date >=CONVERT(varchar,b.Sf_TP_DCR_Active_Dt,107)" +
                     " inner join Mas_State c on b.State_Code=c.State_Code inner join mas_salesforce_DCRTPdate f on b.sf_code=f.sf_code" +
                     " union  " +
                     " select distinct(a.Sf_Code ),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ,'' Mode ,'' Delayed_Date,c.StateName,convert(varchar,dateadd(day,-1,f.last_dcr_date),103) last_dcr_date from  DCR_MissedDates a " +
                     " inner join Mas_Salesforce b on  a.Division_Code='" + div_code + "'  " +
                     " and  Status=0 and a.Sf_Code=b.Sf_Code and b.sf_TP_Active_Flag=0 and a.Dcr_Missed_Date >=CONVERT(varchar, b.Sf_TP_DCR_Active_Dt,107) and b.SF_Status !=2  inner join Mas_State c on b.State_Code=c.State_Code inner join mas_salesforce_DCRTPdate f on b.sf_code=f.sf_code order by StateName ";

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

        public int Update_Delayed_back_LockSystem(string sf_code, string div_code)
        {
            int iReturn = -1;
            DataSet dsDelay;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC Delayed_UpdateBack_withrollback_LockSystem '" + sf_code + "', '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int Update_Delayed_Dates_ForAll_LockSystem(string sf_code, string div_code)
        {
            int iReturn = -1;
            DataSet dsDelay;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC Delayed_Update_withrollback_LockSystem '" + sf_code + "', '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getlock_Delayed(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;



            strQry = " select distinct(a.Sf_Code),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ , '' Mode,'' Delayed_Date,c.StateName from DCR_Delay_Dtls a inner join " +
                     " Mas_Salesforce b on a.Division_Code='" + div_code + "' and Delayed_Flag=0 and a.Sf_Code='" + sf_code + "' and a.Sf_Code=b.Sf_Code and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and a.Delayed_Date >=CONVERT(varchar,b.Sf_TP_DCR_Active_Dt,107)" +
                     " inner join Mas_State c on b.State_Code=c.State_Code order by StateName";

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

        public DataSet getlock_Missed(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;



            strQry = " select distinct(a.Sf_Code ),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ,'' Mode ,'' Delayed_Date,c.StateName from  DCR_MissedDates a " +
                     " inner join Mas_Salesforce b on  a.Division_Code='" + div_code + "' and  a.Sf_Code='" + sf_code + "'  " +
                     " and  Status=0 and a.Sf_Code=b.Sf_Code and b.sf_TP_Active_Flag=0 and a.Dcr_Missed_Date >=CONVERT(varchar, b.Sf_TP_DCR_Active_Dt,107) and b.SF_Status !=2  inner join Mas_State c on b.State_Code=c.State_Code order by StateName ";

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

        public DataSet getAllDelayed(string div_code, string imonth, string iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;



            strQry = " select distinct(a.Sf_Code),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ , '' Mode,'' Delayed_Date,c.StateName from DCR_Delay_Dtls a inner join " +
                     " Mas_Salesforce b on a.Division_Code='" + div_code + "' and a.sf_code='" + sf_code + "' and month(a.Delayed_Date)='" + imonth + "' and year(a.Delayed_Date)='" + iYear + "' and Delayed_Flag=0 and a.Sf_Code=b.Sf_Code " +
                     " inner join Mas_State c on b.State_Code=c.State_Code order by StateName";

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

        public DataSet getAll_Missed(string div_code, string imonth, string iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct(a.Sf_Code ),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ,'' Mode ,'' Delayed_Date,c.StateName from  DCR_MissedDates a " +
                     " inner join Mas_Salesforce b on  a.Division_Code='" + div_code + "' and a.sf_code='" + sf_code + "' and month(a.Dcr_Missed_Date)='" + imonth + "'  " +
                     " and year(a.Dcr_Missed_Date)='" + iYear + "' and Status=0 and a.Sf_Code=b.Sf_Code inner join Mas_State c on b.State_Code=c.State_Code order by StateName ";

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
        public DataSet get_Team(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC Hirarchy_Mr_Only '" + div_code + "' ,'" + sf_code + "' ";

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
        public DataSet get_All_dcr_Sf_Code_date_Count_App(int imon, int iyear, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct Sf_Code " +
                     " from DCRMain_Temp a" +
                     " where a.division_code = '" + divcode + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "   and  Entry_Mode='Apps'" +
                     " union " +
                     "select distinct Sf_Code " +
                     " from DCRMain_Trans a" +
                     " where a.division_code = '" + divcode + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "   and  Entry_Mode='Apps'";

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

        public DataSet DCR_TotalFLDWRKQuery_DateWise(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F'";

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

        public DataSet DCR_TotalFLDWRKQuery_MGR_DateWise(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_Mgr B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F'";

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

        public DataSet DCR_TotalLeaveQuery_DateWise(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(DCR.FieldWork_Indicator) from DCRMain_Trans DCR " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.FieldWork_Indicator='L'";

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

        public DataSet New_DCR_Visit_TotalDocQuery_DateWise(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "Exec [getCallAvg_DateWise] '" + sf_code + "','" + dtFrmDate + "','" + dtToDate + "'";

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

        public DataSet New_DCR_TotalChemistQuery_DateWise(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(c.trans_detail_info_code) Che_count from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and c.Trans_Detail_Info_Type= 2 " +
                    " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) ";

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

        public DataSet New_DCR_TotalStockistQuery_DateWise(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(c.trans_detail_info_code) Stk_count from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and c.Trans_Detail_Info_Type= 3 " +
                    " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)";

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

        public DataSet New_DCR_TotalUnlstDocQuery_DateWise(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = strQry = "select count(c.trans_detail_info_code) UnDoc_count from DCRMain_Trans a,DCRDetail_UnLst_Trans  c" +
                     " where a.Trans_SlNo = c.Trans_SlNo " +
                     " and a.Sf_Code = c.sf_code " +
                     " and a.Sf_Code='" + sf_code + "' " +
                     " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)";


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

        public DataSet DCR_TotalSubDaysQuery_DateWise(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans where Sf_Code = '" + sf_code + "' and convert(date,Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)" +
                     " and Division_Code='" + div_code + "'";

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

        public DataSet New_DCR_Visit_TotalDocCallAvg(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (select count(c.trans_detail_info_code) Doc_count from DCRMain_Trans a,DCRDetail_Lst_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear + ")" +
                    " + " +
                    " (select count(c.trans_detail_info_code) Doc_count from DCRMain_Trans a,DCRDetail_UnLst_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear + ")";

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
        // Login Details
        public DataSet Login_Det(string sf_code, string div_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC LoginDetails  '" + sf_code + "', '" + div_code + "' ," + imonth + "," + iyear + "";

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

        public DataSet Login_Datewise(string sf_code, string div_code, string ifrom, string ito)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC LoginDetails_between_dates  '" + sf_code + "', '" + div_code + "' ,'" + ifrom + "','" + ito + "'";

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

        public DataSet Login_Datewise_Vacant(string sf_code, string div_code, string ifrom, string ito)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC LoginDetails_between_dates_Vacant  '" + sf_code + "', '" + div_code + "' ,'" + ifrom + "','" + ito + "'";

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

        public DataSet Login_Det_Vacant(string sf_code, string div_code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC LoginDetails_Vacant  '" + sf_code + "', '" + div_code + "' ," + imonth + "," + iyear + "";

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

        public DataSet Visit_Doc_unlisted(string doc_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_Visit_Count_unlisted '" + doc_code + "', '" + cmon + "', '" + cyear + "' ";

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

        public DataSet Visit_Doc_workedwith_unlisted(string doc_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_Visit_Count_Workedwith_unlisted '" + doc_code + "', '" + cmon + "', '" + cyear + "' ";

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
        public DataSet One_Visit_Dr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 1 ";

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
        public DataSet Two_Visit_Dr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 2 ";

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
        public DataSet Three_Visit_Dr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                       " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) = 3 ";

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
        public DataSet More_Visit_Dr(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code " +
                     " having count(b.Trans_Detail_Info_Code) > 3 ";

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

        public DataSet getleave_fieldName(string div_code, string month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            //strQry = " select '0' sf_code,'--Select--' as sf_name " +
            //         " union " +
            //         " select distinct a.sf_code,(b.sf_name +' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ) as sf_name " +
            //         " from DCRMain_Trans a,mas_salesforce b where fieldwork_indicator='L' " +
            //         " and a.division_code='"+div_code+"' and month(a.activity_date)='"+month+"' " +
            //         " and year(a.activity_date)='"+Year+"' and a.sf_code=b.sf_code ";

            strQry = " select '0' sf_code,'--Select--' as sf_name " +
                     " union " +
                    " select distinct a.sf_code,(b.sf_name +' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ) as sf_name from " +
                    " mas_leave_form a,mas_salesforce b where " +
                    " month(a.from_date)='" + month + "'  and " +
                    " year(a.from_date)='" + Year + "' and a.sf_code=b.sf_code " +
                    " and a.division_code='" + div_code + "' and a.Leave_Active_Flag=0 and b.sf_TP_Active_Flag=0 and b.sf_status !=2  ";
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

        public DataSet getleave_dates(string div_code, string month, string Year, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            //strQry = " select '0' sf_code,'--Select--' as sf_name " +
            //         " union " +
            //         " select distinct a.sf_code,(b.sf_name +' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ) as sf_name " +
            //         " from DCRMain_Trans a,mas_salesforce b where fieldwork_indicator='L' " +
            //         " and a.division_code='" + div_code + "' and month(a.activity_date)='" + month + "' " +
            //         " and year(a.activity_date)='" + Year + "' and a.sf_code=b.sf_code ";

            strQry = " select  a.Leave_id,a.sf_code,b.sf_emp_id, b.sf_name ,b.sf_Designation_Short_Name , b.Sf_HQ,a.No_of_Days,convert(varchar,a.Created_date,103) Created_date,'' status, " +
                    " (select sf_name from mas_salesforce c where sf_code=b.Reporting_To_SF) Approved_By, convert(varchar,From_Date,103) From_Date,convert(varchar,To_Date,103) To_Date, " +
                    " (select Leave_SName from mas_Leave_Type where division_code=a.division_code and leave_code=a.Leave_type) Leave_type " +
                    " from " +
                    "  mas_leave_form a,mas_salesforce b where " +
                    "  month(a.from_date)='" + month + "' and " +
                    " year(a.from_date)='" + Year + "' and a.sf_code=b.sf_code and a.sf_code='" + sf_code + "' " +
                    " and a.division_code='" + div_code + "' and a.Leave_Active_Flag=0 and " +
                    " b.sf_TP_Active_Flag=0 and b.sf_status !=2 order by 2 ";

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

        public int Update_LeaveCancel(string sf_code, DateTime From_date, DateTime To_date, int Leave_id, string div_code, string Leave_type, string No_Of_Days)
        {
            int iReturn = -1;
            DataSet dsDelay;

            string fromdate2 = From_date.Month.ToString() + '-' + From_date.Day.ToString() + '-' + From_date.Year.ToString();
            string Todate2 = To_date.Month.ToString() + '-' + To_date.Day.ToString() + '-' + To_date.Year.ToString();

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "EXEC Update_LeaveCancel_withrollback '" + sf_code + "','" + fromdate2 + "','" + Todate2 + "','" + Leave_id + "', '" + div_code + "' ,'" + Leave_type + "','" + No_Of_Days + "' ";
                strQry = "EXEC Update_LeaveCancel_withrollback_New '" + sf_code + "','" + fromdate2 + "','" + Todate2 + "','" + Leave_id + "', '" + div_code + "' ,'" + Leave_type + "','" + No_Of_Days + "' ";
                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Update_LeaveCancel(string sf_code, DateTime From_date, DateTime To_date, int Leave_id, string div_code)
        {
            int iReturn = -1;
            DataSet dsDelay;

            string fromdate2 = From_date.Month.ToString() + '-' + From_date.Day.ToString() + '-' + From_date.Year.ToString();
            string Todate2 = To_date.Month.ToString() + '-' + To_date.Day.ToString() + '-' + To_date.Year.ToString();

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC Update_LeaveCancel_withrollback '" + sf_code + "','" + fromdate2 + "','" + Todate2 + "','" + Leave_id + "', '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        // JW MR
        public DataSet DCR_workwithDay_dist_MR(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct t.Worked_with_Code as Worked_with_Code   from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MGR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithDay_JW_MR_MGR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "'";

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

        public DataSet DCR_workwithDate_JW_MR_MGR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct day(d.Activity_Date) as Activity_Date from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "'";

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

        public DataSet DCR_workwithCalls_JW_MR_MGR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  COUNT(t.Trans_Detail_Info_Code)  from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "' ";

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
        public DataSet DCR_workwithCalls_JWk_MR_MGR(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  COUNT( t.Trans_Detail_Info_Code)  from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('MGR',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";

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
        public DataSet DCR_workwithCalls_SfName_MGR(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct d.Trans_SlNo, t.Worked_with_Code,t.sf_code from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MGR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";


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
        public DataSet DCR_workwithDay_JW_MR_MGR(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MGR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithDate_JW_MR_MGR(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct day(d.Activity_Date) as Activity_Date from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('MGR',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithCalls_JW_MR(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  COUNT(t.Trans_Detail_Info_Code)  from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";

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

        public DataSet get_DCR_View_All_Dates_View(string sf_code, int imon, int iyear, string Div_Code, int iType, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = " EXEC [DCR_View_All_Dates_View_AtG] '" + sf_code + "', '" + imon + "', '" + iyear + "','" + Div_Code + "','" + iType + "'";

            strQry = " EXEC [DCR_View_All_Dates_View_Temp] '" + sf_code + "', '" + imon + "', '" + iyear + "','" + Div_Code + "','" + iType + "','" + Date + "'";

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

        public DataSet get_dcr_All_Dates_che_details(string sf_code, int Month, int Year, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No,a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                      " a.Plan_No, b.SDP_Name ,b.POB,b.Session,c.chemists_address1,b.GeoAddrs,b.additional_gift_dtl , b.Additional_Prod_Dtls as Product_Detail," +
                      " (select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.activity_remarks,vsttime ,convert(char(5),vsttime,108) as Time,b.lati,b.long " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and month(a.Activity_Date)= '" + Month + "' and year(a.Activity_Date)='" + Year + "'" +
                      " Union " +
                      " select ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No, a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                      " a.Plan_No, b.SDP_Name ,b.POB,b.Session,c.chemists_address1,b.GeoAddrs,b.additional_gift_dtl , b.Additional_Prod_Dtls as Product_Detail," +
                      " (select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.activity_remarks,vsttime ,convert(char(5),vsttime,108) as Time,b.lati,b.long " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b,Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code   " +
                     //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                     " and month(a.Activity_Date)= '" + Month + "' and year(a.Activity_Date)='" + Year + "' order by Time ";

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

        public DataSet get_DCR_All_Dates_unlst_doc_details(string sf_code, int month, int year, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                           " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, b.SDP_Name,b.Session,b.Time,c.Unlisteddr_address1,b.Activity_remarks,b.Trans_Detail_Slno,b.GeoAddrs,d.Doc_cat_name,e.Doc_Special_name,b.GeoAddrs,b.lati,b.long " +
                           " ,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,  b.activity_remarks  " +
                           " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
                           " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                           " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                           //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                           " and month(a.Activity_Date)= '" + month + "' and year(a.Activity_Date)='" + year + "' " +
                           " Union " +
                           " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                           " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, b.SDP_Name,b.Session,b.Time,c.Unlisteddr_address1,b.Activity_remarks,b.Trans_Detail_Slno,b.GeoAddrs,d.Doc_cat_name,e.Doc_Special_name,b.GeoAddrs,b.lati,b.long " +
                           " ,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,  b.activity_remarks  " +
                           " from DCRMain_Temp a, DCRDetail_Unlst_Temp b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
                           " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                           " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                           //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                           " and month(a.Activity_Date)= '" + month + "' and year(a.Activity_Date)='" + year + "' order by b.Time";

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

        public DataSet get_dcr_All_Date_stk_details(string sf_code, int Month, int Year, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select  ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No, a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,cast(Rx as varchar)Rx,GeoAddrs,b.lati,b.long " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and month(a.Activity_Date)= '" + Month + "' and year(a.Activity_Date)='" + Year + "' " +
                    " Union " +
                    " select ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No,a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type, trans_detail_name as Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,cast(Rx as varchar)Rx,GeoAddrs,b.lati,b.long " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3   " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and month(a.Activity_Date)= '" + Month + "' and year(a.Activity_Date)='" + Year + "' order by a.Trans_SlNo";

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

        public DataSet get_dcr_ALL_Date_hos_details(string sf_code, int Month, int Year, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5   " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and month(a.Activity_Date)= '" + Month + "' and Year(a.Activity_Date)='" + Year + "'" +
                    " Union " +
                    "select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5   " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and month(a.Activity_Date)= '" + Month + "' and Year(a.Activity_Date)='" + Year + "'";

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
        public DataSet getPrevious_Visit(string sf_code, int imon, int iyr, string listeddrcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            int imnt1;
            int imnt2;
            int iyear;
            int iyear2;
            if (imon == 1)
            {
                imnt1 = 12;
                imnt2 = 11;
                iyear = iyr - 1;
                strQry = " select convert(char(10),max(Activity_Date),105)   from DCRMain_Trans  a,DCRDetail_Lst_Trans b " +
                   " where a.Sf_Code = '" + sf_code + "' and (month(a.Activity_Date)='" + imnt1 + "' or month(a.Activity_Date)='" + imnt2 + "') and YEAR(a.Activity_Date)='" + iyear + "'  " +
                   " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                   " and Trans_Detail_Info_Code='" + listeddrcode + "'  ";
            }
            else if (imon == 2)
            {
                imnt1 = 1;
                imnt2 = 12;
                iyear = iyr - 1;
                iyear2 = iyr;
                strQry = " select convert(char(10),max(Activity_Date),105)   from DCRMain_Trans  a,DCRDetail_Lst_Trans b " +
                    " where a.Sf_Code = '" + sf_code + "' and ((month(a.Activity_Date)='" + imnt1 + "' and YEAR(a.Activity_Date)='" + iyear2 + "') or (month(a.Activity_Date)='" + imnt2 + "' and YEAR(a.Activity_Date)='" + iyear + "') )  " +
                    " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                    " and Trans_Detail_Info_Code='" + listeddrcode + "'  ";
            }
            else
            {
                imnt1 = imon - 1;
                imnt2 = imon - 2;
                iyear = iyr;
                strQry = " select convert(char(10),max(Activity_Date),105)   from DCRMain_Trans  a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and (month(a.Activity_Date)='" + imnt1 + "' or month(a.Activity_Date)='" + imnt2 + "') and YEAR(a.Activity_Date)='" + iyear + "'  " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and Trans_Detail_Info_Code='" + listeddrcode + "'  ";
            }


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
        public DataSet get_DCR_View_All_Dates_View_Temp(string sf_code, int imon, int iyear, string Div_Code, int iType, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = " EXEC [DCR_View_All_Dates_View_AtG] '" + sf_code + "', '" + imon + "', '" + iyear + "','" + Div_Code + "','" + iType + "'";

            strQry = " EXEC [DCR_View_All_Dates_View] '" + sf_code + "', '" + imon + "', '" + iyear + "','" + Div_Code + "','" + iType + "'";

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


        public int Update_Delayed_back_totlock(string sf_code, DateTime ddate, string Mode)
        {
            int iReturn = -1;
            // string delaydate = ddate.Year + "-" + ddate.Month.ToString() + "-" + ddate.Day;
            string delaydate = ddate.Month + "-" + ddate.Day + "-" + ddate.Year;
            try
            {

                DB_EReporting db = new DB_EReporting();

                if (Mode == "D")
                {
                    //strQry = "UPDATE DCR_Delay_Dtls " +
                    //            " SET Delayed_Flag = 0 ," +
                    //            " Delay_Release_Date = NULL, Released_by_Whom = '' " +
                    //            " WHERE Sf_Code = '" + sf_code + "' and Delayed_Flag =1 and Delayed_Date='" + delaydate + "'";

                    strQry = "Delete from  DCR_Delay_Dtls " +
                               " WHERE Sf_Code = '" + sf_code + "' and Delayed_Flag =1 and cast(Delayed_Date as date)='" + delaydate + "'";
                }
                else
                {
                    //strQry = "UPDATE DCR_MissedDates " +
                    //           " SET [status] = 0 , " +
                    //           " Missed_Release_Date = NULL , Released_by_Whom = '' " +
                    //           " WHERE Sf_Code = '" + sf_code + "' and status =1 and Dcr_Missed_Date='" + delaydate + "'";

                    strQry = "Delete from DCR_MissedDates " +
                               " WHERE Sf_Code = '" + sf_code + "' and status =1 and cast(Dcr_Missed_Date as date)='" + delaydate + "'";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet DCR_TotalSubDaysQuery_DateWise_WithOut(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (select count(Trans_SlNo) from DCRMain_Trans where Sf_Code = '" + sf_code + "' and convert(date,Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)" +
                     "and Division_Code='" + div_code + "')" +
                     "+" +
                     "(select count(Trans_SlNo) from DCRMain_Temp where Sf_Code = '" + sf_code + "' and convert(date,Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)" +
                     " and Division_Code='" + div_code + "')";

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

        public DataSet DCR_TotalLeaveQuery_DateWise_WithOut(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select (select count(DCR.FieldWork_Indicator) from DCRMain_Trans DCR " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.FieldWork_Indicator='L') " +
                     " + " +
                     " (select count(DCR.FieldWork_Indicator) from DCRMain_Temp DCR " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.FieldWork_Indicator='L')";

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

        public DataSet New_DCR_Visit_TotalDocQuery_DateWise_WithOut(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (select count(c.trans_detail_info_code) Doc_count from DCRMain_Trans a,DCRDetail_Lst_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)) " +
                    " + " +
                    "(select count(c.trans_detail_info_code) Doc_count from DCRMain_Temp a,DCRDetail_Lst_Temp  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)) ";

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

        public DataSet New_DCR_TotalChemistQuery_DateWise_WithOut(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (select count(c.trans_detail_info_code) Che_count from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                     " where a.Trans_SlNo = c.Trans_SlNo " +
                     " and a.Sf_Code = c.sf_code " +
                     " and a.Sf_Code='" + sf_code + "' " +
                     " and c.Trans_Detail_Info_Type= 2 " +
                     " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)) " +
                     " + " +
                     "(select count(c.trans_detail_info_code) Che_count from DCRMain_Temp a,DCRDetail_CSH_Temp  c" +
                     " where a.Trans_SlNo = c.Trans_SlNo " +
                     " and a.Sf_Code = c.sf_code " +
                     " and a.Sf_Code='" + sf_code + "' " +
                     " and c.Trans_Detail_Info_Type= 2 " +
                     " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)) ";

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

        public DataSet New_DCR_TotalStockistQuery_DateWise_WithOut(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select(select count(c.trans_detail_info_code) Stk_count from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and c.Trans_Detail_Info_Type= 3 " +
                    " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126))" +
                    " + " +
                    "(select count(c.trans_detail_info_code) Stk_count from DCRMain_Temp a,DCRDetail_CSH_Temp  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and c.Trans_Detail_Info_Type= 3 " +
                    " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126))";



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

        public DataSet New_DCR_TotalUnlstDocQuery_DateWise_WithOut(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (select count(c.trans_detail_info_code) UnDoc_count from DCRMain_Trans a,DCRDetail_UnLst_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126))" +
                    "+" +
                    "(select count(c.trans_detail_info_code) UnDoc_count from DCRMain_Temp a,DCRDetail_UnLst_Temp  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126))";


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

        public DataSet get_DCR_View_All_Dates_View_WiseOut(string sf_code, string imon, string iyear, string Div_Code, int iType, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " EXEC [DCR_View_All_Dates_View_Without] '" + sf_code + "', '" + imon + "', '" + iyear + "','" + Div_Code + "','" + iType + "'";

            //strQry = " EXEC [DCR_View_All_Dates_View_Temp] '" + sf_code + "', '" + imon + "', '" + iyear + "','" + Div_Code + "','" + iType + "','"+ Date +"'";

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

        public DataSet get_dcr_All_Dates_che_details_WithOut(string sf_code, string FrmDate, string toDate, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select  ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No, a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB,vstTime,ModTime,cast(Rx as varchar)Rx,GeoAddrs " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2  " +
                     " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'" +
                     " Union " +
                     " select ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No, a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB,vstTime,ModTime,cast(Rx as varchar)Rx,GeoAddrs " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2   " +
                     " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'";

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

        public DataSet get_DCR_All_Dates_unlst_doc_details_WithOut(string sf_code, string FrmDate, string toDate, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No, a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                           " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,ModTime,Rx,GeoAddrs,b.SDP_Name " +
                           " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
                           " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                           " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                           " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "' " +
                           " Union " +
                           " select ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No, a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                           " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,ModTime,Rx,GeoAddrs,b.SDP_Name " +
                           " from DCRMain_Temp a, DCRDetail_Unlst_Temp b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
                           " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                           " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                           " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'";

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

        public DataSet get_dcr_All_Date_stk_details_WithOut(string sf_code, string FrmDate, string toDate, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select  ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No, a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,cast(Rx as varchar)Rx,GeoAddrs " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3  " +
                     " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'" +
                     " Union " +
                     " select ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No,a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type, trans_detail_name as Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,cast(Rx as varchar)Rx,GeoAddrs " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3   " +
                     " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'";

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

        public DataSet DCR_TotalFLDWRKQuery_MGR_DateWise_WithOut(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_Mgr B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F') " +
                     " + " +
                     " (select count(Trans_SlNo) from DCRMain_Temp DCR,Mas_WorkType_Mgr B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F')";

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

        public DataSet DCR_TotalFLDWRKQuery_DateWise_WithOut(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F') " +
                     "+ " +
                     "(select count(Trans_SlNo) from DCRMain_Temp DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F') ";

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
        public DataSet get_Release_All_fieldforce_LockSystem_MGR(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;



            strQry = " select distinct(a.Sf_Code),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ , '' Mode,'' Delayed_Date,c.StateName,convert(varchar,dateadd(day,-1,b.last_dcr_date),103) last_dcr_date from DCR_Delay_Dtls a inner join " +
                     " Mas_Salesforce b on a.Division_Code='" + div_code + "' and Delayed_Flag=0 and a.sf_code in (" + sf_code + ") and a.Sf_Code=b.Sf_Code and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and a.Delayed_Date >=CONVERT(varchar,b.Sf_TP_DCR_Active_Dt,107)" +
                     " inner join Mas_State c on b.State_Code=c.State_Code " +
                     " union  " +
                     " select distinct(a.Sf_Code ),b.Sf_Name,b.sf_Designation_Short_Name,b.Sf_HQ,'' Mode ,'' Delayed_Date,c.StateName,convert(varchar,dateadd(day,-1,b.last_dcr_date),103) last_dcr_date from  DCR_MissedDates a " +
                     " inner join Mas_Salesforce b on  a.Division_Code='" + div_code + "'  " +
                     " and  Status=0 and a.sf_code in (" + sf_code + ") and a.Sf_Code=b.Sf_Code and b.sf_TP_Active_Flag=0 and a.Dcr_Missed_Date >=CONVERT(varchar, b.Sf_TP_DCR_Active_Dt,107) and b.SF_Status !=2  inner join Mas_State c on b.State_Code=c.State_Code order by StateName ";

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

        public DataSet sp_DcrViewNameGet(string SF_Code, string iMonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            // strQry = "EXEC sp_get_Rep_audit_with_Vacant '" + sf_code + "', '" + divcode + "' ";

            strQry = "EXEC sp_DcrViewNameGet '" + SF_Code + "', '" + iMonth + "','" + iYear + "' ";

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

        public DataSet get_Geo_details_Maps(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct cust_code,lat,long,listeddr_address1,sf_code,listeddr_name,ListedDr_Created_Date from map_GEO_Customers G inner join mas_listeddr D on G.cust_code=D.listeddrcode and G.division_code=D.division_code where ListedDr_Active_Flag=0 and statflag=0 and sf_code='" + sf_code + "'";
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

        public DataSet DCR_get_WorkType_Status(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B, Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where Division_Code='" + Division_Code + "' and active_flag=0";


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

        public DataSet get_dcr_DCRPendingdate_Without(string sf_code, string FrmDate, string toDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'" +
                     " union all" +
                     " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Temp a" +
                      " where a.Sf_Code = '" + sf_code + "' " +
                    " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'order by Activity_Date";

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

        public DataSet get_dcr_ALL_Date_hos_details_WithOut(string sf_code, string FrmDate, string toDate, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5   " +
                    " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'" +
                    " Union " +
                    "select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5   " +
                    " and a.Activity_Date between '" + FrmDate + "' and '" + toDate + "'";

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

        public DataSet DCR_get_WorkType_Forstatus(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B, Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where Division_Code='" + Division_Code + "' " +
                      "union " +
                      "select '0' WorkType_Code_B,'Missed Date Released' Worktype_Name_B,'MR'WType_SName " +
                      "UNION " +
                      "select '0' WorkType_Code_B,'Missed Date' Worktype_Name_B,'MD'WType_SName " +
                       "union " +
                      "select '0' WorkType_Code_B,'TP Deviation Released' Worktype_Name_B,'TDR'WType_SName " +
                      "UNION " +
                      "select '0' WorkType_Code_B,'TP Deviation' Worktype_Name_B,'TD'WType_SName " +
                       " union " +
                      "select '0' WorkType_Code_B,'Leave Approval Pending' Worktype_Name_B,'LP'WType_SName " +
                      "UNION " +
                      "select '0' WorkType_Code_B,'Not Approved' Worktype_Name_B,'NA'WType_SName " +
                      "UNION " +
                      "select '0' WorkType_Code_B,'Rejected' Worktype_Name_B,'R'WType_SName " +
                      "UNION " +
                      "select '0' WorkType_Code_B,'Delayed' Worktype_Name_B,'D'WType_SName " +
                      "UNION " +
                      "select '0' WorkType_Code_B,'Delay Released' Worktype_Name_B,'DR'WType_SName ";

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
        public DataSet Call_Det_Calendar(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " SELECT distinct sf_code,(tmp.doc_cnt) doc_cnt, (tmp.che_cnt) as che_cnt,(tmp.stk_cnt) as stk_cnt,(tmp.hos_cnt)  as hos_cnt ,(tmp.unlst_cnt) as unlst_cnt " +
                     " FROM (select a.sf_Code,a.trans_slno,convert(varchar,a.Activity_Date,103) Activity_Date,a.division_code, " +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt, " +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt, " +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt, " +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt, " +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as unlst_cnt  " +
                     " from DCRMain_Trans a , Mas_Salesforce b where a.Sf_Code = '" + SF_Code + "' and a.confirmed=1 and  " +
                     " Month(a.Activity_date) = " + sMonth + " and Year(a.Activity_date) = " + sYear + "  " +
                     " and day(a.Activity_date) = " + sday + " and a.sf_code = b.sf_code) as tmp ";

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

        public DataSet getField_Days_Cal(string sf_code, string div_code, int iday, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select FieldWork_Indicator from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = " + iMonth + "  and YEAR(Activity_Date) = " + iYear + " and Sf_Code='" + sf_code + "' " +
                     " and Day(Activity_Date)=" + iday + " and FieldWork_Indicator = 'F' and Division_Code = '" + div_code + "' ";

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
        public DataSet getWorking_Days_W_H(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' and  " +
                      "  FieldWork_Indicator IN ('W','H') and Division_Code = '" + div_code + "' ";

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
        public DataSet Get_Mor_Calls(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " SELECT  count(b.Trans_Detail_Info_Code) cnt FROM dcrmain_trans a, " +
                     " DCRDetail_Lst_Trans b  where a.Trans_SlNo = b.Trans_SlNo AND b.Trans_Detail_Info_Type=1 " +
                     " and a.Sf_Code = b.sf_code and b.session is not null and b.session!='' " +
                     " and month(activity_date)='" + iMonth + "' and year(Activity_Date)='" + iYear + "' " +
                     " and a.sf_code in('" + sf_code + "')  and rtrim(ltrim(b.session))='M' and a.Division_code ='" + div_code + "' ";

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
        public DataSet Get_Eve_Calls(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " SELECT  count(b.Trans_Detail_Info_Code) cnt FROM dcrmain_trans a, " +
                     " DCRDetail_Lst_Trans b  where a.Trans_SlNo = b.Trans_SlNo AND b.Trans_Detail_Info_Type=1 " +
                     " and a.Sf_Code = b.sf_code and b.session is not null and b.session!='' " +
                     " and month(activity_date)='" + iMonth + "' and year(Activity_Date)='" + iYear + "' " +
                     " and a.sf_code in('" + sf_code + "')  and rtrim(ltrim(b.session))='E' and a.Division_code ='" + div_code + "' ";

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
        public DataSet Get_Both_Calls(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " SELECT  count(b.Trans_Detail_Info_Code) cnt FROM dcrmain_trans a, " +
                     " DCRDetail_Lst_Trans b  where a.Trans_SlNo = b.Trans_SlNo AND b.Trans_Detail_Info_Type=1 " +
                     " and a.Sf_Code = b.sf_code and b.session is not null and b.session!='' " +
                     " and month(activity_date)='" + iMonth + "' and year(Activity_Date)='" + iYear + "' " +
                     " and a.sf_code in('" + sf_code + "')  and rtrim(ltrim(b.session))!='M' and rtrim(ltrim(b.session))!='E' and a.Division_code ='" + div_code + "' ";

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

        public DataSet DCR_Atten_App(string div_code, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " EXEC sp_Attendance_App '" + dtToDate + "','" + div_code + "'";

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

        public DataSet sp_DCR_TotalSubDaysQuery_DateWise(string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select (select count(Trans_SlNo) from DCRMain_Trans where Sf_Code = '" + sf_code + "' and convert(date,Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)" +
            //         "and Division_Code='" + div_code + "')" +
            //         "+" +
            //         "(select count(Trans_SlNo) from DCRMain_Temp where Sf_Code = '" + sf_code + "' and convert(date,Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)" +
            //         " and Division_Code='" + div_code + "')";

            strQry = "EXEC [sp_Get_FieldWork] '" + div_code + "', '" + dtFrmDate + "', '" + dtToDate + "'";

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

        public DataSet sp_DCR_Visit_TotalDocQuery_DateWise(string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select (select count(c.trans_detail_info_code) Doc_count from DCRMain_Trans a,DCRDetail_Lst_Trans  c" +
            //        " where a.Trans_SlNo = c.Trans_SlNo " +
            //        " and a.Sf_Code = c.sf_code " +
            //        " and a.Sf_Code='" + sf_code + "' " +
            //        " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)) "+
            //        " + "+
            //        "(select count(c.trans_detail_info_code) Doc_count from DCRMain_Temp a,DCRDetail_Lst_Temp  c" +
            //        " where a.Trans_SlNo = c.Trans_SlNo " +
            //        " and a.Sf_Code = c.sf_code " +
            //        " and a.Sf_Code='" + sf_code + "' " +
            //        " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126)) ";

            strQry = " EXEC [sp_Get_TotalDocQuery_DateWise] '" + div_code + "','" + dtFrmDate + "','" + dtToDate + "'";

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

        public DataSet New_DCR_TotalChemist(string div_code, string dtFrmDate, string dtToDate, int Type_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " EXEC [New_DCR_TotalChemistQuery_DateWise] '" + div_code + "','" + dtFrmDate + "','" + dtToDate + "'," + Type_Code + "";

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



        public DataSet sp_DCR_TotalUnlstDocQuery_DateWise(string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select (select count(c.trans_detail_info_code) UnDoc_count from DCRMain_Trans a,DCRDetail_UnLst_Trans  c" +
            //        " where a.Trans_SlNo = c.Trans_SlNo " +
            //        " and a.Sf_Code = c.sf_code " +
            //        " and a.Sf_Code='" + sf_code + "' " +
            //        " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126))"+
            //        "+"+
            //        "(select count(c.trans_detail_info_code) UnDoc_count from DCRMain_Temp a,DCRDetail_UnLst_Temp  c" +
            //        " where a.Trans_SlNo = c.Trans_SlNo " +
            //        " and a.Sf_Code = c.sf_code " +
            //        " and a.Sf_Code='" + sf_code + "' " +
            //        " and convert(date,a.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126))";

            strQry = " EXEC [New_DCR_TotalUnlstDocQuery_DateWise] '" + div_code + "','" + dtFrmDate + "','" + dtToDate + "'";

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

        public DataSet get_DCR_RCPA(string SF_Code, int sMonth, int sYear, string sday)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string strDate = sday.Substring(3, 2) + "/" + sday.Substring(0, 2) + "/" + sday.Substring(6, 4);

            DataSet dsTP = null;
            strQry = "EXEC [sp_Get_RCPA_Details] '" + SF_Code + "','" + sMonth + "'," + sYear + ",'" + strDate + "'";

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

        //Camp Missed

        public DataSet visit_cnt_1_Camp(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b,mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 and Doc_SubCatCode !=''  and c.listeddrcode=b.Trans_Detail_Info_Code" +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) = 1)as repcalls1  ";

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
        public DataSet visit_cnt_2_Camp(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b,mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  and Doc_SubCatCode !=''  and c.listeddrcode=b.Trans_Detail_Info_Code" +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) = 2)as repcalls1  ";

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
        public DataSet visit_cnt_3_Camp(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b,mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  and Doc_SubCatCode !=''  and c.listeddrcode=b.Trans_Detail_Info_Code " +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) = 3)as repcalls1  ";

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
        public DataSet visit_cnt_more_Camp(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b,mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  and Doc_SubCatCode !=''  and c.listeddrcode=b.Trans_Detail_Info_Code " +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) > 3)as repcalls1  ";

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

        public DataSet One_Visit_Dr_camp(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where dc.division_code=d.division_code and   charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code and Doc_SubCatCode !=''  " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code,Doc_SubCatCode,d.division_code " +
                     " having count(b.Trans_Detail_Info_Code) = 1 ";

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
        public DataSet Two_Visit_Dr_Camp(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where dc.division_code=d.division_code and   charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code and Doc_SubCatCode !=''  " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code,Doc_SubCatCode,d.division_code " +
                     " having count(b.Trans_Detail_Info_Code) = 2 ";

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
        public DataSet Three_Visit_Dr_Camp(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                       " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where dc.division_code=d.division_code and   charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code and Doc_SubCatCode !=''  " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code,Doc_SubCatCode,d.division_code " +
                     " having count(b.Trans_Detail_Info_Code) = 3 ";

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
        public DataSet More_Visit_Dr_Camp(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                     " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name, " +
                     " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where dc.division_code=d.division_code and   charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName " +
                     " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "'" +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                     " and d.ListedDrCode = b.Trans_Detail_Info_Code and Doc_SubCatCode !=''  " +
                     " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                     " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code,Doc_SubCatCode,d.division_code " +
                     " having count(b.Trans_Detail_Info_Code) > 3 ";

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

        public DataSet get_DCR_RCPAConslidate(string SF_Code, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();



            DataSet dsTP = null;
            strQry = "EXEC [sp_Get_RCPA_Cons_Details] '" + SF_Code + "','" + sMonth + "'," + sYear + "";

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

        public DataSet Get_RCPA_Capture(string SF_Code, string Trans_Detail_Slno, string sday)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string strDate = sday.Substring(3, 2) + "/" + sday.Substring(0, 2) + "/" + sday.Substring(6, 4);

            DataSet dsTP = null;
            strQry = "EXEC [sp_Get_RCPA_Capture] '" + SF_Code + "','" + Trans_Detail_Slno + "','" + strDate + "'";

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

        public DataSet get_RCPA_Capture_Head(string Sf_code, string sDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            string strDate = sDate.Substring(3, 2) + "/" + sDate.Substring(0, 2) + "/" + sDate.Substring(6, 4);

            strQry = "EXEC [sp_RCPA_Capture_Head] '" + Sf_code + "','" + strDate + "'";

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

        public DataSet get_RCPA_Capture_Chemist(string Sf_code, string sDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            string strDate = sDate.Substring(3, 2) + "/" + sDate.Substring(0, 2) + "/" + sDate.Substring(6, 4);

            strQry = "EXEC [sp_RCPA_Capture_Chemist] '" + Sf_code + "','" + strDate + "'";

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

        public DataSet GetDCR_View_All_Dates_Detailing(string sf_code, string Div_Code, string frmmonth, string tomnth, string frmYr, string ToYr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [DCR_View_All_Dates_Detailing] '" + sf_code + "','" + frmmonth + "','" + frmYr + "','" + Div_Code + "','1'";

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

        public DataSet Get_Product_Minuts_Detail(string sf_code, string iMonth, string iYear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_Get_Product_Minuts_Detail] '" + sf_code + "','" + iMonth + "','" + iYear + "','" + div_code + "'";

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

        public DataSet Get_sp_GetProductSum(string sf_code, string iMonth, string iYear, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_GetProductSum] '" + sf_code + "','" + iMonth + "','" + iYear + "','" + div_Code + "'";

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

        public DataSet Get_sp_GetProduct_MGR_Sum(string sf_code, string iMonth, string iYear, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_GetProduct_MGR_Sum] '" + sf_code + "','" + iMonth + "','" + iYear + "','" + div_Code + "'";

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

        public int LeaveAppMissed(string Leave_Id)
        {

            int iReturn = -1;
            int slno = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC svLeaveAppMissed '" + Leave_Id + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet get_All_dcr_Date_Leave(string sf_code, string divcode, string from, string to)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct Activity_Date " +
                     " from DCRMain_Temp a" +
                     " where a.division_code = '" + divcode + "' and sf_code='" + sf_code + "'  " +
                     " and convert(date,a.Activity_Date) between convert(char(10),'" + from + "',126) and convert(char(10),'" + to + "',126) " +
                     " union " +
                     "select distinct Activity_Date " +
                     " from DCRMain_Trans a" +
                     " where a.division_code = '" + divcode + "' and sf_code='" + sf_code + "'   " +
                     " and convert(date,a.Activity_Date) between convert(char(10),'" + from + "',126) and convert(char(10),'" + to + "',126)";

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

        public DataSet get_Release_TPdeviation(string div_code, string imonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' as sf_code, '--Select--' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_MissedDates d inner join Mas_Salesforce b on d.division_code = '" + div_code + "' and d.status=3 and month(Dcr_Missed_Date)='" + imonth + "' and year(Dcr_Missed_Date)='" + iYear + "' and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and b.Sf_Code =d.Sf_Code";
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

        public DataSet get_Release_TpDevMRR(string SF_Code, string imonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' as sf_code, '--Select--' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_MissedDates d inner join Mas_Salesforce b on  d.Sf_Code in (" + SF_Code + ") and d.status=3 and month(Dcr_Missed_Date)='" + imonth + "' and year(Dcr_Missed_Date)='" + iYear + "' and b.sf_TP_Active_Flag=0 and b.SF_Status !=2 and b.Sf_Code =d.Sf_Code ";
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

        public DataSet getReleaseDate_Tpdeviation(string SF_Code, string sMonth, string sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select convert(varchar,d.Dcr_Missed_Date,103) as Delayed_Date, s.Sf_HQ,s.Sf_Name,s.Sf_Code,s.sf_Designation_Short_Name,R.Deviation_Reason" +
                   " ,convert(varchar,dateadd(day,-1,f.last_dcr_date),103) last_dcr_date from DCR_MissedDates d, Mas_Salesforce s,mas_salesforce_DCRTPdate f,DCR_TPDev_Reason R " +
                   " where d.Sf_Code='" + SF_Code + "' and MONTH(Dcr_Missed_Date)='" + sMonth + "' and d.sf_code=R.sf_code and d.Dcr_Missed_Date=R.Activity_Date " +
                   " and Year(Dcr_Missed_Date)='" + sYear + "' and d.Sf_Code = s.Sf_Code and status=3  and s.sf_code=f.sf_code ";
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

        public int Update_Delayed_TPdeviation(string sf_code, DateTime ddate, string div_code, string Reason_for_Release)
        {
            int iReturn = -1;
            DataSet dsDelay;
            string delaydate = ddate.Year + "-" + ddate.Month.ToString() + "-" + ddate.Day;
            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE DCR_MissedDates " +
                           " SET [status] = 4 ,Reason_for_Release='" + Reason_for_Release + "', " +
                           " Missed_Release_Date = getdate(), Released_by_Whom = 'admin' " +
                           " WHERE Sf_Code = '" + sf_code + "' and status =3 and Dcr_Missed_Date='" + delaydate + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

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
        public DataSet getPrevious_Visit_Miss_prod(string sf_code, string prod)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select convert(char(10),max(Activity_Date),105)   from DCRMain_Trans  a,DCRDetail_Lst_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1   and  charindex('#'+cast('" + prod + "' as varchar)+'~','#'+ Product_Code) > 0  " +

                     " group by Activity_Date   ";


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

        public int DcrDelLeaveOption(string sf_code, string Frm_date, string Todate)
        {

            int iReturn = -1;
            int slno = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC DcrDelLeaveOption '" + sf_code + "','" + Frm_date + "','" + Todate + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }




        public int Create_DCR_Main_Rejected_Trans(string SF_Code, string Trans_SlNo, string Reason, string Mode, string Activ_Date, string Sub_Date, string Confirmed, string Rej_Date, string DivCode)
        {
            int iReturnmain = -1;
            int iReturntemp = -1;
            int iReturn = -1;
            int slno = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                //strQry = "Insert into DCR_Main_Rejected (Sys_Name,Trans_SlNo,Sf_Code,ReasonforRejection,Activity_Date,Submission_Date,Confirmed,Rejected_Date,Division_Code,Work_Type)" +
                //    " values('" + Mode + "','" + Trans_SlNo + "','" + SF_Code + "','" + Reason + "','" + Activ_Date + "',getdate(),'" + Confirmed + "',getdate(),'" + DivCode + "',1)";

                strQry = "Insert into DCR_Main_Rejected select Trans_SlNo,Sf_Code,Sf_Name,Activity_Date,Submission_Date,Work_Type," +
                 "Plan_No,Plan_Name,Half_Day_FW,Start_Time,End_Time,Sys_Ip,'A',Division_Code,Remarks,'1', " +
                 " '" + Reason + "',Emp_Id,Employee_Id,App_MGR,WorkType_Name,Entry_Mode,FieldWork_Indicator,getdate() from DCRMain_Temp " +
                 "where Sf_Code ='" + SF_Code + "' and Trans_SlNo = '" + Trans_SlNo + "' ";
                iReturntemp = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturntemp;
        }






        public int Reject_DCR_Update(string SF_Code, string Trans_SlNo, string ReasonforReject)
        {
            int iReturn = -1;

            int iretmove = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update DCRMain_Temp set confirmed = 2, ReasonforRejection= '" + ReasonforReject + "' where Trans_SlNo = '" + Trans_SlNo + "' " +
                         " and Sf_Code = '" + SF_Code + "' ";
                iReturn = db.ExecQry(strQry);


                strQry = "Insert into DCR_Main_Rejected select Trans_SlNo,Sf_Code,Sf_Name,Activity_Date,Submission_Date,Work_Type," +
                    "Plan_No,Plan_Name,Half_Day_FW,Start_Time,End_Time,Sys_Ip,'R',Division_Code,Remarks,'2', " +
                    " '" + ReasonforReject + "',Emp_Id,Employee_Id,App_MGR,WorkType_Name,Entry_Mode,FieldWork_Indicator,getdate() from DCRMain_Temp " +
                    "where Sf_Code ='" + SF_Code + "' and Trans_SlNo = '" + Trans_SlNo + "' ";
                iReturn = db.ExecQry(strQry);

                strQry = "Insert into DCR_EditDates select (select max(isnull(sl_no,0))+1 from DCR_EditDates ),Trans_SlNo,SF_Code,Activity_Date,Submission_Date,Fieldwork_Indicator,Work_Type,Confirmed,Division_Code,0,getdate()" +
                " from DCRMain_Temp " +
                "where Sf_Code ='" + SF_Code + "' and Trans_SlNo = '" + Trans_SlNo + "' ";
                iReturn = db.ExecQry(strQry);

                if (iReturn > 0) //Added by Vasanthi.P
                {
                    // Sample Input Update for DCR.
                    iretmove = Sample_Input_Update(SF_Code, Trans_SlNo);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Sample_Input_Update(string SF_Code, string Trans_SlNo)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC sp_DCRReject_Update_Sample_Input '" + SF_Code + "', '" + Trans_SlNo + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Reject_DCR_Update_New(string SF_Code, string Trans_SlNo, string ReasonforReject)
        {
            int iReturn = -1;
            int iretmove = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC sp_Reject_DCR_WithSAmpleInput '" + SF_Code + "', '" + Trans_SlNo + "','" + ReasonforReject + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet CheckStatusDCR(string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select DCR_Approval_Remarks from Setup_Others where Division_Code = '" + DivCode + "'";

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



        public DataSet getDCR_AppRejDate(string slno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Activity_Date from DCRMain_Trans " +
                     " Where Trans_SlNo ='" + slno + "' ";
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




        //Addede by Preethi
        public DataSet getDCR_App_Rej_Report(string div_code, string sMonth, string sYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            //strQry = "select a.Sf_Code,a.Sf_Name,b.Sf_HQ,b.sf_Designation_Short_Name,Sys_Name," +
            //         " isnull(rtrim( case when a.Sys_Name='A' then 'Approve' " +
            //         " when a.Sys_Name='R' then 'Reject' end  ), 'Reject') Mode ," +
            //         " a.WorkType_Name,a.ReasonforRejection,convert(varchar(10),a.Activity_Date,103)Activity_Date,convert(varchar(10),a.Rejected_Date,103)Rejected_Date " +
            //         "from DCR_Main_Rejected a, Mas_Salesforce b where a.Sf_Code=b.Sf_Code  and (a.Division_Code)='" + div_code + "' and month(a.Rejected_Date)='" + sMonth + "' and  year(a.Rejected_Date)='" + sYear + "' order by Mode desc,a.Sf_Name,a.Rejected_Date";


            //strQry = " select distinct RD.Sf_Code,d.Sf_Name,d.Sf_HQ,d.sf_Designation_Short_Name,RD.* from ( " +
            //    " select distinct a.sf_code,Sys_Name,isnull(rtrim( case when a.Sys_Name='A' then 'Approve' when a.Sys_Name='R' then 'Reject' end  ), 'Reject') Mode , " +
            //    " a.WorkType_Name,a.ReasonforRejection,convert(varchar(10),a.Rejected_Date,103)Rejected_Date  " +
            //    "  from DCR_Main_Rejected a where  (a.Division_Code)='" + div_code + "' and month(a.Rejected_Date)='" + sMonth + "' and  year(a.Rejected_Date)='" + sYear + "' " +
            //    "  union All " +
            //    " select distinct b.SF_code, '' as Sys_Name, 'Deviation' as Mode, " +
            //    " b.TP_WorkType,b.Deviation_Reason,convert(varchar(10),b.Activity_Date,103)Rejected_Date " +
            //    "  from DCR_TPDev_Reason b where (b.Division_Code)='" + div_code + "' and month(b.Activity_Date)='" + sMonth + "' and  year(b.Activity_Date)='" + sYear + "' ) RD , " +
            //    "   mas_salesforce d where Rd.sf_code=d.sf_code order by Mode desc,d.Sf_Name,RD.Rejected_Date ";

            strQry = "EXEC DCR_TP_Aprv_Rej '" + div_code + "','" + sMonth + "','" + sYear + "','" + sf_code + "','" + "1" + "'";
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

        public DataSet getTP_Rej_Report(string div_code, string sMonth, string sYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            //strQry = "select distinct a.Sf_Code,b.Sf_Name,b.Sf_HQ,b.sf_Designation_Short_Name,Tour_Month,'Reject' as Mode , a.Rejection_Reason," +
            //         " DATENAME(month, (cast(Tour_Month AS VARCHAR) +'-01-'+ cast(Tour_Year AS VARCHAR)))+'-'+(cast(Tour_Year AS VARCHAR)) AS Activity_Date, " +
            //         " convert(varchar(10),a.Reject_Date,103)Rejected_Date from TP_Reject_B_Mgr a, mas_salesforce b " +
            //         " where a.Sf_Code=b.Sf_Code and (a.Division_Code)='" + div_code + "' and month(a.Reject_Date)='" + sMonth + "' and  year(a.Reject_Date)='" + sYear + "' order by b.Sf_Name";

            //strQry = " select distinct RD.Sf_Code,d.Sf_Name,d.Sf_HQ,d.sf_Designation_Short_Name,RD.* from ( "+
            //    " select distinct a.Sf_Code,'Reject' as Mode,Rejection_Reason, convert(varchar(10),a.Reject_Date,103)Rejected_Date from  TP_Reject_B_Mgr a "+
            //    "    where a.division_code='"+ div_code + "' and month(a.Reject_Date)='"+ sMonth + "' and  year(a.Reject_Date)='"+ sYear + "' "+
            //    " union All "+
            //    " select distinct b.Sf_Code,'Deviation' as Mode,Reason_for_Release,convert(varchar(10),b.Dcr_Missed_Date,103)Rejected_Date  from DCR_MissedDates b  "+
            //    " where b.division_code='"+ div_code + "' and status in (3,4,5) and month(b.Dcr_Missed_Date)='"+ sMonth + "' and  year(b.Dcr_Missed_Date)='"+ sYear + "' ) RD , "+
            //    "  mas_salesforce d where Rd.sf_code=d.sf_code order by Mode ,d.Sf_Name ";

            strQry = "EXEC DCR_TP_Aprv_Rej '" + div_code + "','" + sMonth + "','" + sYear + "','" + sf_code + "','" + "2" + "'";

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
        //Addede by Preethi

        public DataSet get_DCRView_Approved_All_Dates_new(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;



            //  strQry = "EXEC GetDcrDetailView '" + sf_code + "','" + mon + "','" + year + "' ";
            strQry = "EXEC GetDcrDetailView_New '" + sf_code + "','" + mon + "','" + year + "' ";

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

        public DataSet get_DCRView_Approved_MGR_All_Dates_new(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            // strQry = "EXEC GetDcrDetailView_MGR '" + sf_code + "','" + mon + "','" + year + "'";

            strQry = "EXEC GetDcrDetailView_MGR_New '" + sf_code + "','" + mon + "','" + year + "'";

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

        public DataSet get_DCR_Status_Delay_DCRView_new(string SF_Code, string sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select case D.Delayed_Flag " +
                     " when '0' then '( Delayed )' " +
                     " when '1' then '( Delay Relased )' " +
                     " when '2' then '( Delay )' end as Delayed_Flag  FROM DCR_Delay_Dtls D,Mas_Salesforce S WHERE S.Sf_Code=D.Sf_Code " +
                     " and DAY(D.Delayed_Date) = " + sday + " and MONTH(D.Delayed_Date) = " + sMonth + " " +
                     " and YEAR(D.Delayed_Date) = " + sYear + " AND S.Sf_Code='" + SF_Code + "' " +
                       " and D.Delayed_Date not in " +
                    " (select Delayed_Date from mas_Leave_Form where sf_code = '" + SF_Code + "' and Leave_Active_Flag " +
                    " in (0,2) and Delayed_Date between From_Date and To_Date)";

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
        public DataSet Call_monitor_totdr(string div_code, string sf_code, int cmonth, int cyear, DateTime cDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " SELECT  count(ListedDrCode) FROM Mas_ListedDr WHERE Division_Code= '" + div_code + "' " +
                   " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                   " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null)) AND " +
                   " sf_code='" + sf_code + "'";


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
        public DataSet Call_monitor_Met(string sf_code, string div_code, int iMonth, int iYear, DateTime cDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(distinct b.Trans_Detail_Info_Code)  from DCRMain_Trans a,DCRDetail_Lst_Trans b, mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "'  " +
                     " and YEAR(a.Activity_Date)= '" + iYear + "'  " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                     " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null)) AND " +
                     " a.sf_code = b.sf_code and a.Division_code = '" + div_code + "' and b.Trans_Detail_Info_Code=c.ListedDrCode    ";

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
        public DataSet Call_monitor_Seen(string sf_code, string div_code, int iMonth, int iYear, DateTime cDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(b.Trans_Detail_Info_Code)  from DCRMain_Trans a,DCRDetail_Lst_Trans b, mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "'  " +
                     " and YEAR(a.Activity_Date)= '" + iYear + "'  " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                     " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null)) AND " +
                     " a.sf_code = b.sf_code and a.Division_code = '" + div_code + "' and b.Trans_Detail_Info_Code=c.ListedDrCode    ";

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
        public DataSet Call_monitor_Cat_Met(string sf_code, string div_code, int iMonth, int iYear, DateTime cDate, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(distinct b.Trans_Detail_Info_Code)  from DCRMain_Trans a,DCRDetail_Lst_Trans b, mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "'  " +
                     " and YEAR(a.Activity_Date)= '" + iYear + "'  " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 " +
                     " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                     " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null)) AND " +
                     " a.sf_code = b.sf_code and a.Division_code = '" + div_code + "' and b.Trans_Detail_Info_Code=c.ListedDrCode  and  c.Doc_Cat_Code =" + catg_code + "  ";

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
        public DataSet visit_cnt_1_dt(string sf_code, string div_code, int iMonth, int iYear, DateTime cDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b, mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
                      " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                     " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null))  " +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) = 1)as repcalls1  ";

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
        public DataSet visit_cnt_2_dt(string sf_code, string div_code, int iMonth, int iYear, DateTime cDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                     " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b, mas_listeddr c " +
                     " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                     " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                     " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
                      " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                     " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null))  " +
                     " group by b.Trans_Detail_Info_Code  " +
                     "  having count(b.Trans_Detail_Info_Code) = 2)as repcalls1  ";

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
        public DataSet visit_cnt_3_dt(string sf_code, string div_code, int iMonth, int iYear, DateTime cDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                   " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b, mas_listeddr c " +
                   " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                   " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                   " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
                    " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                   " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null))  " +
                   " group by b.Trans_Detail_Info_Code  " +
                   "  having count(b.Trans_Detail_Info_Code) = 3)as repcalls1  ";

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
        public DataSet visit_cnt_more_dt(string sf_code, string div_code, int iMonth, int iYear, DateTime cDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(*) as  repcalls1 from " +
                 " ( select count( b.Trans_Detail_Info_Code) Trans_Detail_Info_Code from DCRMain_Trans a,DCRDetail_Lst_Trans b, mas_listeddr c " +
                 " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                 " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                 " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
                  " AND ((CONVERT(Date, ListedDr_Created_Date) < CONVERT(VARCHAR(50), '" + cDate + "' , 126)) And (CONVERT(Date, ListedDr_Deactivate_Date) >=  " +
                 " CONVERT(VARCHAR(50), '" + cDate + "' , 126) or ListedDr_Deactivate_Date is null))  " +
                 " group by b.Trans_Detail_Info_Code  " +
                 "  having count(b.Trans_Detail_Info_Code) > 3)as repcalls1  ";

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

        public int DcrDele_New(string sf_code, string Frm_date)
        {

            int iReturn = -1;
            int slno = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC DcrDele_New '" + sf_code + "','" + Frm_date + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet DcrDele_Leave_Check(string sf_code, string Frm_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;


            strQry = "EXEC DcrDele_Leave_Check '" + sf_code + "','" + Convert.ToDateTime(Frm_date).ToString("MM/dd/yyyy") + "' ";

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
        public DataSet Payslip_Status(string div_code, string sf_code, string imonth, string iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC Payslip_Status  '" + div_code + "', '" + sf_code + "' ,'" + imonth + "','" + iyear + "'";

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
        public DataSet SpecialityDetails(string sf_code, string div_code, string code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "Exec [getspeciality_monthwise] '" + sf_code + "'," + div_code + "," + code + "," + iMonth + "," + iYear + "";

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
        #region Product Feedback View
        public DataSet getAppProd(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC getAppProd '" + sf_code + "' ";

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

        public DataSet getAppProdAdmin(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select cast(Product_Code_SlNo as varchar) id,Product_Detail_Name name,Prod_Detail_Sl_No pSlNo,Product_Brd_Code cateid, " +
                        " Division_Code,'A' ActFlg, isnull((select top 1 5 Distributor_Price from Mas_Product_State_Rates where " +
                        " Product_Detail_Code = P.Product_Detail_Code),0) DRate from Mas_Product_Detail P " +
                        " where Product_Active_Flag = 0 AND Division_Code = '" + div_code + "' " +
                        " group by Product_Code_SlNo,Product_Detail_Name,Prod_Detail_Sl_No,Division_Code,Product_Brd_Code,Product_Detail_Code " +
                        " order by Product_Detail_Name";

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

        public DataSet getFeedBk(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC getFeedBk '" + div_code + "' ";

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

        public DataSet getprd_feedback(string div_code, string feed_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " EXEC FeedBackColumn '" + div_code + "', '" + feed_Id + "'";


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

        public DataSet getprd_feedback_View(string div_code, string sf_code, string fDate, string tDate, string prdt_code, string feed_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC ProductwiseCallFeedbackView '" + div_code + "', '" + sf_code + "', '" + fDate + "','" + tDate + "','" + prdt_code + "', '" + feed_Id + "'";

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

        public DataSet getprd_feedback_Detail(string div_code, string sf_code, string fDate, string tDate, string prdt_code, string feed_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC Get_Prd_Feedback_Detail '" + div_code + "', '" + sf_code + "', '" + fDate + "', '" + tDate + "', '" + prdt_code + "', '" + feed_Id + "' ";

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
        #endregion

        public DataSet FillMissed_DCR_Date(string Div_Code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT distinct B.Trans_SlNo,B.sf_Code,B.Sf_Name,sf_emp_id,Sf_HQ,Designation_Short_Name as Desig_Name,convert(varchar,B.Activity_Date,103) as Activity_Date,H.Activity_Date as A_Date from DCR_MissedDates A" +
                      " inner join DCRMain_Trans_Deleted B on A.DCR_Missed_Date = B.Activity_Date and A.Sf_Code = B.Sf_Code" +
                      " left join  DCRDetail_CSH_Trans_Deleted C on C.Trans_SlNo = B.Trans_SlNo" +
                      " left join DCRDetail_Lst_Trans_Deleted D on D.Trans_SlNo = B.Trans_SlNo" +
                      " left join DCRDetail_Unlst_Trans_Deleted E on E.Trans_SlNo = B.Trans_SlNo" +
                      " inner join Mas_Salesforce F on F.Sf_Code = A.Sf_Code" +
                      " inner join Mas_SF_Designation G on G.Designation_Code = F.Designation_Code" +
                      " left join DCRMain_Trans H on H.Activity_Date=A.Dcr_Missed_Date and H.sf_COde=A.sf_COde " +
                      " where (C.Trans_SlNo is not null or D.Trans_SlNo is not null or E.Trans_SlNo is not null)" +
                      " and B.FieldWork_Indicator = 'F'  and Status in ('0','1') and A.[Month] = '" + Month + "' and  A.[Year] = '" + Year + "'" +
                      " and A.Division_Code='" + Div_Code + "'  and H.Activity_Date is null ";

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


        public DataSet FillTotal_SfCount(string Div_Code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select SUM(Sf_Count) as Total from (SELECT count(distinct A.Sf_Code ) as Sf_Count from DCR_MissedDates A" +
                             " inner join DCRMain_Trans_Deleted B on A.DCR_Missed_Date = B.Activity_Date and A.Sf_Code = B.Sf_Code" +
                             " left join  DCRDetail_CSH_Trans_Deleted C on C.Trans_SlNo = B.Trans_SlNo" +
                             " left join DCRDetail_Lst_Trans_Deleted D on D.Trans_SlNo = B.Trans_SlNo" +
                             " left join DCRDetail_Unlst_Trans_Deleted E on E.Trans_SlNo = B.Trans_SlNo" +
                             " inner join Mas_Salesforce F on F.Sf_Code = A.Sf_Code" +
                             " inner join Mas_SF_Designation G on G.Designation_Code = F.Designation_Code" +
                              " left join DCRMain_Trans H on H.Activity_Date=A.Dcr_Missed_Date and H.sf_COde=A.sf_COde " +
                             " where (C.Trans_SlNo is not null or D.Trans_SlNo is not null or E.Trans_SlNo is not null)" +
                             " and B.FieldWork_Indicator = 'F'  and Status in ('0','1') and A.[Month] = '" + Month + "' and  A.[Year] = '" + Year + "'" +
                             " and A.Division_Code='" + Div_Code + "'  and H.Activity_Date is null group by  A.sf_Code) M";

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



        public int Insert_Deleted_DCR(string sf_code, string Trans_SlNo, DateTime Activity_Date)
        {
            int iReturn = -1;
            try
            {
                string Activity_Date1 = Activity_Date.Month.ToString() + '-' + Activity_Date.Day.ToString() + '-' + Activity_Date.Year.ToString();

                DB_EReporting db = new DB_EReporting();
                strQry = "EXEC Sp_Missed_DCR_Posted  '" + sf_code + "', '" + Trans_SlNo + "', '" + Activity_Date1 + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_TransDeletedRecord(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select * from DCRMain_Trans_Deleted where Sf_Code = '" + sf_code + "' and convert(varchar,Activity_Date,103)= '" + Activity_date + "'";

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

        public DataSet get_LstTransDeletedRecord(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select ROW_NUMBER() OVER(ORDER BY trans_detail_slno ASC) AS SNo,B.* from DCRMain_Trans_Deleted A,DCRDetail_Lst_Trans_Deleted B where a.Trans_SlNo = b.Trans_SlNo " +
                     " and A.Sf_Code = '" + sf_code + "' and convert(varchar, A.Activity_Date,103)= '" + Activity_date + "'";
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

        public DataSet get_CshTransDeletedRecord(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select ROW_NUMBER() OVER(ORDER BY trans_detail_slno ASC) AS SNo,B.* from DCRMain_Trans_Deleted A,DCRDetail_Csh_Trans_Deleted B where a.Trans_SlNo = b.Trans_SlNo " +
                     " and A.Sf_Code = '" + sf_code + "' and convert(varchar, A.Activity_Date,103)= '" + Activity_date + "'";
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

        public DataSet get_UnLstTransDeletedRecord(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select ROW_NUMBER() OVER(ORDER BY trans_detail_slno ASC) AS SNo,B.* from DCRMain_Trans_Deleted A,DCRDetail_Unlst_Trans_Deleted B where a.Trans_SlNo = b.Trans_SlNo " +
                     " and A.Sf_Code = '" + sf_code + "' and convert(varchar, A.Activity_Date,103)= '" + Activity_date + "'";
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

        public DataSet getDCRDeletedRecord(string sf_code, string sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select * from DCRMain_Temp_Edit_Missed A,DCRMain_Trans_Deleted B Where A.Sf_Code = B.Sf_Code and A.Activity_Date = B.Activity_Date " +
                     " and A.Sf_Code = '" + sf_code + "' and  Day(A.Activity_Date)= '" + sday + "' and  Month(A.Activity_Date)= '" + sMonth + "' and  Year(A.Activity_Date)= '" + sYear + "' ";
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


        public DataSet New_DCR_Visit_TotalDocQuery_Total(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "Exec [getCallAvg_total] '" + sf_code + "','" + iMonth + "-01-" + iYear + "','" + iMonth + "','" + iYear + "'";

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
        public DataSet New_DCR_Visit_TotalDocQuery_Met_Average(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "Exec [getCallAvg_Met_total] '" + sf_code + "','" + iMonth + "-01-" + iYear + "','" + iMonth + "','" + iYear + "'";

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
        //added by gowri
        public DataSet One_Visit_Dr_SVL(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                    "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                   " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,svl_no, " +
                   " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                   " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                   " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                   " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d , #doctor e" +
                   " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                   " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                   " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                   " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.sf_code=e.sf_code and e.listeddrcode=d.listeddrcode " +
                   " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                   " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code,svl_no " +
                   " having count(b.Trans_Detail_Info_Code) = 1 " +
                   " drop table #doctor ";

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
        public DataSet Two_Visit_Dr_SVL(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                              "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                             " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,svl_no, " +
                             " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                             " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                             " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                             " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d , #doctor e" +
                             " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                             " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                             " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                             " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.sf_code=e.sf_code and e.listeddrcode=d.listeddrcode " +
                             " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                             " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code,svl_no " +
                             " having count(b.Trans_Detail_Info_Code) = 2 " +
                             " drop table #doctor ";
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
        public DataSet Three_Visit_Dr_SVL(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                   "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                  " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,svl_no, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                  " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                  " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                  " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d , #doctor e" +
                  " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                  " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                  " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                  " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.sf_code=e.sf_code and e.listeddrcode=d.listeddrcode " +
                  " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                  " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code,svl_no " +
                  " having count(b.Trans_Detail_Info_Code) = 3 " +
                  " drop table #doctor ";

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
        public DataSet More_Visit_Dr_SVL(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select  row_number() over (PARTITION BY sf_code order by ListedDr_Sl_No) as svl_no,ListedDrCode,sf_code " +
                  "into #doctor from Mas_ListedDr where division_code = '" + div_code + "' and ListedDr_Active_Flag = 0   " +
                 " select d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,svl_no, " +
                 " d.Doc_Qua_Name,d.Doc_Class_ShortName, " +
                 " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                 " t.SF_Code = d.Sf_Code and CHARINDEX(cast(t.Territory_Code as varchar),d.Territory_Code) > 0 for XML path('')),1,2,'') territory_Name " +
                 " from DCRMain_Trans a,DCRDetail_Lst_Trans b , Mas_ListedDr d , #doctor e" +
                 " where a.Sf_Code = '" + sf_code + "' and month(a.Activity_Date)= '" + iMonth + "' " +
                 " and YEAR(a.Activity_Date)='" + iYear + "' and a.Division_code='" + div_code + "' " +
                 " and a.Trans_SlNo = b.Trans_SlNo and b.Trans_Detail_Info_Type=1  " +
                 " and d.ListedDrCode = b.Trans_Detail_Info_Code  and d.sf_code=e.sf_code and e.listeddrcode=d.listeddrcode " +
                 " group by d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, " +
                 " d.Doc_Qua_Name,d.Doc_Class_ShortName, d.Sf_Code ,d.Territory_Code,svl_no " +
                 " having count(b.Trans_Detail_Info_Code) > 3 " +
                 " drop table #doctor ";

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
        //ENDED BY GOWRI

        public int RecordUpdate_DCRProduct(string Trans_Slno, string Dr_Code, string FinalProducts, string FinalProductcodes)
        {
            int iReturn = -1;
            int iReturn_Backup = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Insert into DCRDetail_Lst_SampleInput_Adj_BK select *,'Sample' from DCRDetail_Lst_Trans Where Trans_SLNO='" + Trans_Slno + "' and Trans_Detail_Info_Code='" + Dr_Code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update DCRDetail_Lst_Trans set Product_Code = '" + FinalProductcodes + "' , Product_Detail = '" + FinalProducts + "' where " +
                        " Trans_SLNO='" + Trans_Slno + "' and Trans_Detail_Info_Code='" + Dr_Code + "'  ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RecordUpdate_DCRInput(string Trans_Slno, string Dr_Code, string InputCode, string InputName, string InputQty, string FinalInputCodes, string FinalInput)
        {
            int iReturn = -1;
            int iReturn_Backup = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Insert into DCRDetail_Lst_SampleInput_Adj_BK select *,'Input' from DCRDetail_Lst_Trans Where Trans_SLNO='" + Trans_Slno + "' and Trans_Detail_Info_Code='" + Dr_Code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update DCRDetail_Lst_Trans set Gift_Code='" + InputCode + "',Gift_Name='" + InputName + "', Gift_Qty='" + InputQty + "'," +
                         " Additional_Gift_Code = '" + FinalInputCodes + "' , Additional_Gift_Dtl = '" + FinalInput + "' where " +
                         " Trans_SLNO='" + Trans_Slno + "' and Trans_Detail_Info_Code='" + Dr_Code + "'  ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet DCR_TotalFLDWRKQuery_With_HalfDay(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
            //         " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
            //         " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F'";

            strQry = "SELECT a.Field - b.Half_Day  FROM (select count(Trans_SlNo) AS Field from DCRMain_Trans DCR, Mas_WorkType_BaseLevel B " +
                    "where DCR.Sf_Code = '" + sf_code + "'  and month(DCR.Activity_Date) ='" + iMonth + "' and DCR.Division_Code ='" + div_code + "' " +
                    "and YEAR(DCR.Activity_Date) = '" + iYear + "' and DCR.Work_Type = B.WorkType_Code_B and B.FieldWork_Indicator = 'F')a, " +
                    "(select (count(Half_Day_FW) * 0.5) Half_Day from DCRMain_Trans DCR, " +
                    "Mas_WorkType_BaseLevel B where DCR.Sf_Code = '" + sf_code + "'  and month(DCR.Activity_Date) ='" + iMonth + "' and DCR.Division_Code ='" + div_code + "' " +
                    "and YEAR(DCR.Activity_Date) = '" + iYear + "' and DCR.Work_Type = B.WorkType_Code_B and B.FieldWork_Indicator = 'F' " +
                    "and Half_Day_FW != '' and LEN(Half_Day_FW) != 1 )b";

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
        public DataSet DCR_TotalFLDWRKQuery_MGR_With_HalfDay(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_Mgr B " +
            //         " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
            //         " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F'";

            strQry = "SELECT a.Field - b.Half_Day  FROM (select count(Trans_SlNo) AS Field  from DCRMain_Trans DCR, Mas_WorkType_Mgr B " +
                    "where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date) ='" + iMonth + "' and DCR.Division_Code ='" + div_code + "' and YEAR(DCR.Activity_Date) = '" + iYear + "' " +
                    "and DCR.Work_Type = B.WorkType_Code_M and B.FieldWork_Indicator = 'F') a," +
                    "(select(count(Half_Day_FW) * 0.5) Half_Day from DCRMain_Trans DCR, Mas_WorkType_Mgr B where DCR.Sf_Code = '" + sf_code + "'" +
                    "and month(DCR.Activity_Date) = '" + iMonth + "' and DCR.Division_Code ='" + div_code + "' and YEAR(DCR.Activity_Date) = '" + iYear + "' and DCR.Work_Type = B.WorkType_Code_M " +
                    "and B.FieldWork_Indicator = 'F' and Half_Day_FW != '' and LEN(Half_Day_FW) != 1 )b ";


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
        public DataSet DCR_TotalFLDWRKQuery_DateWise_With_HalfDay(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "Select a.Field - b.Half_Day From (select count(Trans_SlNo) as Field  from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F') a," +
                     "(select (count(Half_Day_FW) * 0.5) Half_Day  from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F' and Half_Day_FW != '' and LEN(Half_Day_FW) != 1 ) b";


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
        public DataSet DCR_TotalFLDWRKQuery_MGR_DateWise_With_HalfDay(string sf_code, string div_code, string dtFrmDate, string dtToDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " Select a.Field - b.Half_Day From (select count(Trans_SlNo) as Field from DCRMain_Trans DCR,Mas_WorkType_Mgr B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F')a," +
                     " (select (count(Half_Day_FW) * 0.5) Half_Day from DCRMain_Trans DCR,Mas_WorkType_Mgr B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and convert(date,DCR.Activity_Date) between convert(char(10),'" + dtFrmDate + "',126) and convert(char(10),'" + dtToDate + "',126) and DCR.Division_Code='" + div_code + "' " +
                     " and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F' and Half_Day_FW != '' and LEN(Half_Day_FW) != 1 )b";

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
        public DataSet get_dcr_DCRPendingdate_Tuned(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name,a.Remarks " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " order by Activity_Date";

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
        public DataSet get_DCR_View_All_Dates_View_Temp_Tuned(string sf_code, int imon, int iyear, string Div_Code, int iType, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = " EXEC [DCR_View_All_Dates_View_AtG] '" + sf_code + "', '" + imon + "', '" + iyear + "','" + Div_Code + "','" + iType + "'";

            strQry = " EXEC [DCR_View_All_Dates_View_Tuned] '" + sf_code + "', '" + imon + "', '" + iyear + "','" + Div_Code + "','" + iType + "'";

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
        public DataSet get_dcr_All_Dates_che_details_Tuned(string sf_code, int Month, int Year, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No,a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                      " a.Plan_No, b.SDP_Name ,b.POB,b.Session,c.chemists_address1,b.GeoAddrs,b.additional_gift_dtl , b.Additional_Prod_Dtls as Product_Detail," +
                      " (select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.activity_remarks,vsttime ,convert(char(5),vsttime,108) as Time,b.lati,b.long " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code " +
                      //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and month(a.Activity_Date)= '" + Month + "' and year(a.Activity_Date)='" + Year + "' order by Time ";

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
        public DataSet get_DCR_All_Dates_unlst_doc_details_Tuned_New(string sf_code, int month, int year, string Div_Code, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            //strQry = " select a.Trans_SlNo,b.trans_detail_slno, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
            //               " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
            //               " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, b.SDP_Name,b.Session,b.Time,c.Unlisteddr_address1,b.Activity_remarks,b.Trans_Detail_Slno,b.GeoAddrs,d.Doc_cat_name,e.Doc_Special_name,b.GeoAddrs,b.lati,b.long " +
            //               " ,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,  b.activity_remarks  " +
            //               " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
            //               " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
            //               " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
            //               " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
            //               //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
            //               " and month(a.Activity_Date)= '" + month + "' and year(a.Activity_Date)='" + year + "' order by b.Time ";

            strQry = " EXEC [DCR_View_All_Dates_View_Unlist_Tuned] '" + sf_code + "', '" + month + "', '" + year + "','" + Div_Code + "','" + iType + "'";

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
        public DataSet get_dcr_All_Date_stk_details_Tuned(string sf_code, int Month, int Year, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select  ROW_NUMBER() OVER(ORDER BY a.Trans_SlNo asc) AS SI_No, a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,cast(Rx as varchar)Rx,GeoAddrs,b.lati,b.long " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3  " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and month(a.Activity_Date)= '" + Month + "' and year(a.Activity_Date)='" + Year + "' order by a.Trans_SlNo ";

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
        public DataSet get_dcr_ALL_Date_hos_details_Tuned(string sf_code, int Month, int Year, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,trans_detail_name as Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5   " +
                    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and month(a.Activity_Date)= '" + Month + "' and Year(a.Activity_Date)='" + Year + "'";

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
        public DataSet getSfName_HQ_New(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT Sf_Name,Sf_HQ,sf_Designation_Short_Name, sf_code,Sf_Emp_Id,StateName,CONVERT(varchar, CAST(Sf_Joining_Date AS datetime), 103) Sf_Joining_Date,  " +
                      " stuff((select ', ' + subdivision_name from mas_subdivision sub where charindex(',' + cast(sub.subdivision_code as varchar) + ',', ',' + a.subdivision_code) > 0 " +
                      " for XML path('')),1,1,'') subdivision_name  FROM  Mas_Salesforce a WITH(NOLOCK),Mas_State b " +
                        " WHERE a.State_Code = b.State_Code and sf_code= '" + sfcode + "' ";

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
        public DataSet get_DCR_NF_latlong_MR(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Worktype_Name, b.location from dcrmain_trans a, TbMyDayPlan b where " +
               " a.sf_Code = '" + sf_code + "'  and a.Sf_Code = b.sf_code and FieldWork_Indicator!='F' and " +
               " convert(varchar, b.Pln_Date, 103) = convert(varchar, a.Activity_Date, 103) and convert(varchar, a.Activity_Date,103)= '" + Activity_date + "' " +
               " union " +
               " select Worktype_Name,b.location from DCRMain_Temp a, TbMyDayPlan b where " +
               " a.sf_Code = '" + sf_code + "'  and a.Sf_Code = b.sf_code and FieldWork_Indicator!='F' and " +
               " convert(varchar, b.Pln_Date, 103) = convert(varchar, a.Activity_Date, 103) and convert(varchar, a.Activity_Date,103)= '" + Activity_date + "' ";

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
        public DataSet get_DCRView_Approved_All_Dates_new_Tuned(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;



            strQry = "EXEC DCR_Detail_View_MR_Tuned '" + sf_code + "','" + mon + "','" + year + "' ";

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
        public DataSet get_DCRView_Approved_MGR_All_Dates_new_Tuned(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC DCR_Detail_View_MGR_Tuned '" + sf_code + "','" + mon + "','" + year + "'";

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
        public DataSet get_Temp_and_Approved_dcrLstDOC_Tuned(string sf_code, string Activity_date, string Div_Code, string iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "Exec GET_TEMP_AND_APPROVED_DCRLSTDOC_DETAILS '" + sf_code + "','" + Activity_date + "','" + iType + "'";

            strQry = " EXEC DCR_Temp_APPROVED_View_Tuned '" + sf_code + "', '" + Activity_date + "','" + Div_Code + "','" + iType + "'";

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
        public DataSet get_Temp_and_Approved_dcrUnLst_DOC_Tuned(string sf_code, string Activity_date, string Div_Code, string iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "Exec DCR_Temp_APPROVED_View_Unlist_Tuned '" + sf_code + "','" + Activity_date + "','" + Div_Code + "','" + iType + "'";

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
        public DataSet DcrRemarksHalfDay(string SF_Code, int Day, int Month, int Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Remarks FROM DCRMain_Trans D" +
                     " where DAY(D.Activity_Date) = " + Day + " and MONTH(D.Activity_Date) = " + Month + " " +
                     " and YEAR(D.Activity_Date) = " + Year + " AND D.Sf_Code='" + SF_Code + "' ";

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


    }
}
