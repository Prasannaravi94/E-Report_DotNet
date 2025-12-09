using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class SubDivision
    {
        private string strQry = string.Empty;

        public DataSet getSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT subdivision_code,a.subdivision_sname,a.subdivision_name, " +
                     " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.subdivision_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "' and Product_Active_Flag=0) Sub_Count" +
                     ",(select count(e.subdivision_code) from Mas_Salesforce_two e where charindex(cast(a.subdivision_code as varchar),e.subdivision_code )> 0 and e.Division_Code ='" + divcode + ",' and sf_status=0 and sf_TP_Active_Flag=0) SubField_Count" +
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
        // Sorting
        public DataTable getSubDivisionlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtSubDiv = null;

            strQry = "SELECT subdivision_code,subdivision_sname,subdivision_name, " +
                            " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.subdivision_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Count" +
                            ",(select count(e.subdivision_code) from Mas_Salesforce e where charindex(cast(a.subdivision_code as varchar),e.subdivision_code )> 0 and e.Division_Code ='" + divcode + ",') SubField_Count" +
                           " FROM mas_subdivision a WHERE subdivision_active_flag=0 And Div_Code= '" + divcode + "'" +
                           " ORDER BY 2";
            try
            {
                dtSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtSubDiv;
        }
        public DataSet getSubDiv(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT subdivision_sname,subdivision_name " +
                     " FROM mas_subdivision WHERE subdivision_active_flag=0 And subdivision_code= '" + subdivcode + "' AND Div_Code= '" + divcode + "'" +
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
        public bool RecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_sname='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";
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
        public bool sRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_name='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";
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

        public bool RecordExist(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_code != '" + subdivision_code + "' AND subdivision_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool sRecordExist(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_code != '" + subdivision_code + "' AND subdivision_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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

        public int RecordAdd(string divcode, string subdiv_sname, string subdiv_name)
        {
            int iReturn = -1;
            if (!RecordExist(subdiv_sname, divcode))
            {
                if (!sRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(subdivision_code)+1,'1') subdivision_code from mas_subdivision ";
                        int subdivision_code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO mas_subdivision(subdivision_code,div_code,subdivision_sname,subdivision_name,created_Date,LastUpdt_Date,SubDivision_Active_Flag)" +
                                 "values('" + subdivision_code + "','" + divcode + "','" + subdiv_sname + "', '" + subdiv_name + "',getdate(),getdate(),0) ";

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
        public int RecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExist(subdivision_code, subdiv_sname, divcode))
            {
                if (!sRecordExist(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE mas_subdivision " +
                                 " SET subdivision_sname = '" + subdiv_sname + "', " +
                                 " subdivision_name = '" + subdiv_name + "' , " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE subdivision_code = '" + subdivision_code + "' ";

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

        public int RecordDelete(int subdivision_code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  mas_subdivision WHERE subdivision_code = '" + subdivision_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        //Changes done by priya

        public int DeActivate(int subdivision_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_subdivision " +
                            " SET subdivision_active_flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE subdivision_code = '" + subdivision_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //end
        public DataSet getSubdivision(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = " SELECT 0 as subdivision_code,'---Select---' as subdivision_name " +
                      " UNION " +
                  " SELECT subdivision_code,subdivision_name " +
                  " FROM  mas_subdivision where Div_Code='" + divcode + "'" +
                  " and subdivision_active_flag=0 ";
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
        public DataSet getSubdiv_Prod(string divcode, string subdivision_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                      " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name," +
                       " c.Product_Grp_Name, d.subdivision_code FROM  mas_subdivision d,Mas_Product_Category b, " +
                       " Mas_Product_Group c join Mas_Product_Detail a on a.subdivision_code like '" + subdivision_code + ',' + "%'  or " +
                        " a.subdivision_code like '%" + ',' + subdivision_code + "' or a.subdivision_code like '%" + ',' + subdivision_code + ',' + "%' " +
                        " WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code" +
                        " AND d.SubDivision_Active_Flag=0 AND  a.Division_Code= '" + divcode + "' and d.subdivision_code ='" + subdivision_code + "'" +
                        "order by Product_Detail_Name";
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
        public DataSet getSubdiv_Sales(string divcode, string subdivision_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.SF_Code, a.Sf_Name, a.Division_Code,a.Sf_HQ,case when a.sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_type,  " +
                " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To,c.Designation_Name " +
                   " FROM Mas_SF_Designation c, mas_subdivision b join  mas_salesforce a on a.subdivision_code like '" + subdivision_code + ',' + "%'  or " +
                    " a.subdivision_code like '%" + ',' + subdivision_code + "' or a.subdivision_code like '%" + ',' + subdivision_code + ',' + "%' " +
                    " WHERE SF_Status=0  AND lower(sf_code) != 'admin' AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                    " and b.SubDivision_Active_Flag=0  and  b.subdivision_code ='" + subdivision_code + "' AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                    " a.Division_Code like '%" + ',' + divcode + ',' + "%') and a.Designation_Code=c.Designation_Code ORDER BY 2";
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

        //Changes done by Priya

        public DataSet getSubDiv_Create(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT subdivision_code,subdivision_sname,subdivision_name " +
                     " FROM mas_subdivision WHERE subdivision_active_flag=0 And Div_Code= '" + divcode + "'" +
                     " union select '' subdivision_code,'ALL' subdivision_sname,'ALL' subdivision_name ORDER BY 2";
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

        public DataSet GetSubdiv_Code(string subdivision_name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
            strQry = "select subdivision_code from mas_subdivision where subdivision_name='" + subdivision_name + "' and Div_Code = '" + div_code + "'";

            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataSet getSub_sf(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT subdivision_code,Division_Code  " +
                        " FROM Mas_Salesforce " +
                        " WHERE Sf_Code =  '" + sfcode + "' ";
            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
        public DataSet getSubDiv_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select subdivision_code,subdivision_sname,subdivision_name from mas_subdivision " +
                     " where  Div_Code = '" + div_code + "' and subdivision_active_flag=0 ";
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


        #region activity_Master By Ferooz

        public DataSet getActivityMaster(string divcode, string Activity_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Activity_S_Name,Activity_Name " +
                     " FROM Mas_Upload_activity WHERE Activity_ID= '" + Activity_Code + "' AND Division_code= '" + divcode + "'" +
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

        public int MasActivityRecordAdd(string divcode, string activity_sname, string activity_name)
        {
            int iReturn = -1;
            if (!MasActivityRecordExist(activity_sname, divcode))
            {
                if (!MasActivitysRecordExist(activity_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Activity_ID)+1,'1') Activity_ID from Mas_Upload_activity ";
                        int Activity_ID = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Upload_activity(Activity_ID,Activity_S_Name,Activity_Name,Division_code,Created_Date,Active_Flag)" +
                                 "values('" + Activity_ID + "','" + activity_sname + "', '" + activity_name + "','" + divcode + "',getdate(),0) ";

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

        public bool MasActivityRecordExist(string activity_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Activity_ID) FROM Mas_Upload_activity WHERE Activity_S_Name='" + activity_sname + "' and Division_code= '" + divcode + "' ";
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

        public bool MasActivitysRecordExist(string activity_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Activity_ID) FROM Mas_Upload_activity WHERE Activity_Name='" + activity_name + "' and Division_code= '" + divcode + "' ";
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

        public int MasActivityRecordUpdate(int ActivityID, string Activity_sname, string Activity_name, string divcode)
        {
            int iReturn = -1;
            if (!MasActivityRecordExist(ActivityID, Activity_sname, divcode))
            {
                if (!MasActivitysRecordExist(ActivityID, Activity_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = " UPDATE Mas_Upload_activity " +
                                 " SET Activity_S_Name = '" + Activity_sname + "', " +
                                 " Activity_Name = '" + Activity_name + "', " +
                                 " Updated_Date = getdate() " +
                                 " WHERE Activity_ID = '" + ActivityID + "' ";

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

        public bool MasActivityRecordExist(int ActivityID, string Activity_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Activity_ID) FROM Mas_Upload_activity WHERE Activity_ID != '" + ActivityID + "' AND Activity_S_Name ='" + Activity_sname + "' and Division_code= '" + divcode + "' ";

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

        public bool MasActivitysRecordExist(int ActivityID, string Activity_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Activity_ID) FROM Mas_Upload_activity WHERE Activity_ID != '" + ActivityID + "' AND Activity_Name ='" + Activity_name + "' and Division_code= '" + divcode + "' ";

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

        public DataSet getActUpload(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = " SELECT Activity_ID,	Activity_S_Name,	Activity_Name,	Division_code FROM Mas_Upload_activity WHERE Active_Flag=0 and Division_code='" + divcode + "' " +
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

        public int ActUploadDeActivate(int Activity_ID)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Upload_activity " +
                            " SET Active_Flag=1 , " +
                            " Updated_Date = getdate() " +
                            " WHERE Activity_ID = '" + Activity_ID + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataTable getActMasterUploadlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtSubDiv = null;

            strQry = " SELECT Activity_ID,	Activity_S_Name,	Activity_Name,	Division_code FROM Mas_Upload_activity WHERE Active_Flag=1 and Division_code='" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dtSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtSubDiv;
        }

        public DataSet getActivityUpload(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Activity_ID,	Activity_S_Name,	Activity_Name,	Division_code FROM Mas_Upload_activity WHERE Active_Flag=1 and Division_code='" + divcode + "'  ORDER BY 2";
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

        public int ActivityUploadReActivate(int Activity_ID)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Upload_activity " +
                            " SET Active_Flag=0 ," +
                            " Updated_Date = getdate() " +
                            " WHERE Activity_ID = '" + Activity_ID + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        #endregion

        #region trans_upload Activity By Ferooz
        public DataSet getTransActivityUpload(string div_code, string sf_code, int MonthNumber, int Year, string Date, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsActUpload = null;

            strQry = " EXEC Sp_GetProcessingActivity '" + div_code + "','" + sf_code + "','" + MonthNumber + "','" + Year + "','" + Date + "','" + sf_type + "' ";
            try
            {
                dsActUpload = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsActUpload;
        }

        public int TransActivityRecordUpdate(string ActivityID, string sf_code, string Date_Activity_Approval, string divcode, int Process, string lblActivity_Approved_Bill_Amount, string Month_Activity, string Year_Activity, string txtProcess_Date)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                //DataSet dsActUpload = null;
                //strQry = " SELECT Process_Date FROM trans_upload_activity  " +
                //    " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'" +
                //    " AND Month_Activity='" + Month_Activity + "'AND Year_Activity='" + Year_Activity + "' ";

                //dsActUpload = db.Exec_DataSet(strQry);
                //if (dsActUpload.Tables[0].Rows[0]["Process_Date"].ToString() == string.Empty && Process == 1)
                //{
                //    strQry = " UPDATE trans_upload_activity " +
                //     " SET Process_Flag = '" + Process + "', " +
                //     " Process_Date = getdate() " +
                //     " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'" +
                //     " AND Month_Activity='" + Month_Activity + "'AND Year_Activity='" + Year_Activity + "' ";
                //}
                //else if (dsActUpload.Tables[0].Rows[0]["Process_Date"].ToString() != string.Empty && Process == 0)
                //{
                //    strQry = " UPDATE trans_upload_activity " +
                //             " SET Process_Flag = '" + Process + "', " +
                //             " Process_Date = getdate() " +
                //             " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'" +
                //             " AND Month_Activity='" + Month_Activity + "'AND Year_Activity='" + Year_Activity + "' AND Process_Date IS NOT NULL ";
                //}
                //else
                //{
                //    strQry = " UPDATE trans_upload_activity " +
                //             " SET Process_Flag = '" + Process + "' " +
                //             //" Process_Date = getdate() " +
                //             " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'" +
                //             " AND Month_Activity='" + Month_Activity + "'AND Year_Activity='" + Year_Activity + "' ";
                //}

                if (txtProcess_Date == string.Empty)
                {
                    strQry = " UPDATE trans_upload_activity " +
                             " SET Process_Flag = '" + Process + "' ," +
                             " Process_Date = null " +
                             " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "' ";
                }
                else
                {
                    strQry = " UPDATE trans_upload_activity " +
                             " SET Process_Flag = '" + Process + "', " +
                             " Process_Date = Convert(varchar(10),'" + txtProcess_Date + "',110) " +
                             " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "' ";
                }
                iReturn = db.ExecQry(strQry);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getPrintTransActivityUpload(string div_code, string sf_code, string Activity_ID, string Date_Activity_Approval)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsActUpload = null;

            strQry = " EXEC Sp_getPrintTransActivityUpload '" + div_code + "','" + sf_code + "','" + Activity_ID + "','" + Date_Activity_Approval + "' ";
            try
            {
                dsActUpload = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsActUpload;
        }
        #endregion
        public int TransActivityRecordDelete(string ActivityID, string sf_code, string Date_Activity_Approval, string divcode, string lblActivity_Approved_Bill_Amount, string Month_Activity, string Year_Activity, string txtProcess_Date)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                //DataSet dsActUpload = null;
                //strQry = " SELECT Process_Date FROM trans_upload_activity  " +
                //    " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'" +
                //    " AND Month_Activity='" + Month_Activity + "'AND Year_Activity='" + Year_Activity + "' ";

                //dsActUpload = db.Exec_DataSet(strQry);
                //if (dsActUpload.Tables[0].Rows[0]["Process_Date"].ToString() == string.Empty && Process == 1)
                //{
                //    strQry = " UPDATE trans_upload_activity " +
                //     " SET Process_Flag = '" + Process + "', " +
                //     " Process_Date = getdate() " +
                //     " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'" +
                //     " AND Month_Activity='" + Month_Activity + "'AND Year_Activity='" + Year_Activity + "' ";
                //}
                //else if (dsActUpload.Tables[0].Rows[0]["Process_Date"].ToString() != string.Empty && Process == 0)
                //{
                //    strQry = " UPDATE trans_upload_activity " +
                //             " SET Process_Flag = '" + Process + "', " +
                //             " Process_Date = getdate() " +
                //             " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'" +
                //             " AND Month_Activity='" + Month_Activity + "'AND Year_Activity='" + Year_Activity + "' AND Process_Date IS NOT NULL ";
                //}
                //else
                //{
                //    strQry = " UPDATE trans_upload_activity " +
                //             " SET Process_Flag = '" + Process + "' " +
                //             //" Process_Date = getdate() " +
                //             " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'" +
                //             " AND Month_Activity='" + Month_Activity + "'AND Year_Activity='" + Year_Activity + "' ";
                //}


                strQry = " Insert trans_upload_activity_Bkp select * from trans_upload_activity" +
             " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "'";


                iReturn = db.ExecQry(strQry);


                strQry = " Delete trans_upload_activity " +
                             " WHERE Activity_ID = '" + ActivityID + "' AND Format(Date_Activity_Approval,'dd/MM/yyyy')='" + Date_Activity_Approval + "' and SF_Code='" + sf_code + "' and Division_Code='" + divcode + "' AND Activity_Approved_Bill_Amount='" + lblActivity_Approved_Bill_Amount + "' ";

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
