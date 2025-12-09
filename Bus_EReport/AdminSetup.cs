using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class AdminSetup
    {
        private string strQry = string.Empty;
        private int Sl_No;
        public int ReactivateWorktype(string worktype_Code, int ddltype)
        {
            int iReturn = -1;

            try
            {

                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_WorkType_BaseLevel " +
                                " SET active_flag=0  " +
                                " WHERE WorkType_Code_B = '" + worktype_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_WorkType_Mgr " +
                               " SET active_flag=0  " +
                               " WHERE WorkType_Code_M = '" + worktype_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet ReactivategetWorkTye(string div_code, int ddltype)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            if (ddltype == 1)
            {

                strQry = "Select WorkType_Code_B,Worktype_Name_B,WType_SName,WorkType_Orderly,TP_Flag,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Designation_Code" +
                          " From Mas_WorkType_BaseLevel where Division_Code='" + div_code + "' and active_flag=1 " +
                          "ORDER BY WorkType_Orderly ";
            }
            else if (ddltype == 2)
            {
                strQry = "Select WorkType_Code_M WorkType_Code_B,Worktype_Name_M Worktype_Name_B,WType_SName,WorkType_Orderly,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Designation_Code" +
                        " FROM Mas_WorkType_Mgr where Division_Code='" + div_code + "' and active_flag=1 " +
                        "ORDER BY WorkType_Orderly ";
            }

            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadm;
        }
        public DataSet getAdminSetup()
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
            "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
            "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
            " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand, " +
            " Remarks_length_Allowed,TpBased,No_of_TP_View, " +
            " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
            " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,Division_Code  from Admin_Setups ";

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
        public DataSet getting_TpEntry(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "Select Parameter_Name from Setup_Tp_Entry  where division_code='" + divcode + "'";

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
        //----Start by VP
        public DataSet getinputEntry(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select Input_Ack from Setup_Others where Division_Code='" + div_code + "' and Input_Ack='Y'";

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

        public DataSet getinput(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select b.Gift_Name,b.Despatch_Qty,b.Despatch_Actual_qty,a.Trans_month_year," +
          " b.Received_Flag,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Input_Despatch_Head] a,[dbo].[Trans_Input_Despatch_Details] b " +
          " where a.Trans_sl_No=b.Trans_sl_No and  a.sf_code='" + sf_code + "'  and a.Division_Code ='" + div_code + "' " +
          " and  b.Received_Flag is null and a.Trans_Year=Year(GetDate())  and (a.Trans_Month=Month(GetDate())-1  or a.Trans_Month=Month(GetDate()) or a.Trans_Month=Month(Getdate())+1) order by Trans_Month";
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

        public DataSet getsampleEntry(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select sample_Ack from Setup_Others where Division_Code='" + div_code + "' and sample_Ack='Y'";
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

        public DataSet getsample(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            // strQry = "select a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty," +
            //" Despatch_Actual_qty,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Sample_Despatch_Head] a," +
            //"[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
            //" where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Actual_qty is null " +
            //" and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "' and (a.Trans_Month=Month(Getdate()) " +
            //" or a.Trans_Month=Month(Getdate())-1 or a.Trans_Month=Month(Getdate())+1) order by Trans_Month ";

            strQry = "select a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty," +
         " Despatch_Actual_qty,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Sample_Despatch_Head] a," +
         "[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
         " where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Qty is null " +
         " and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "' and a.Trans_Year=Year(GetDate()) and (a.Trans_Month=Month(GetDate())-1  or a.Trans_Month=Month(GetDate()) or a.Trans_Month=Month(Getdate())+1) " +
         "order by Trans_Month ";
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

        public int updateinput(string sf_code, string div_code, string Despatch_Qty, string Despatch_Actual_qty, string month, string code, string num)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = " UPDATE Trans_Input_Despatch_Details SET Despatch_Qty = '" + Despatch_Actual_qty + "',Despatch_Actual_qty='" + Despatch_Qty + "',Received_Flag='1' " +
                     " FROM Trans_Input_Despatch_Head WHERE Trans_Input_Despatch_Head.Trans_sl_No=Trans_Input_Despatch_Details.Trans_sl_No " +
                     " and Trans_Input_Despatch_Head.Sf_Code='" + sf_code + "' and Trans_Input_Despatch_Head.Division_Code='" + div_code + "' " +
                     " and Trans_Input_Despatch_Head.Trans_Month='" + month + "' and Trans_Input_Despatch_Details.productc='" + code + "' and  Trans_Input_Despatch_Details.upl_sl_no='" + num + "'";

            iReturn = db_ER.ExecQry(strQry);
            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int updatesample(string sf_code, string div_code, string Despatch_Qty, string Despatch_Actual_qty, string month, string product_code, string num)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = " UPDATE Trans_Sample_Despatch_Details SET Despatch_Qty = '" + Despatch_Actual_qty + "',Despatch_Actual_qty='" + Despatch_Qty + "',Received_Flag='1' " +
                     " FROM Trans_Sample_Despatch_Head  " +
                     " WHERE Trans_Sample_Despatch_Head.Trans_sl_No=Trans_Sample_Despatch_Details.Trans_sl_No and Trans_Sample_Despatch_Head.Trans_Month='" + month + "' " +
                     " and Trans_Sample_Despatch_Head.Sf_Code='" + sf_code + "' and Trans_Sample_Despatch_Head.Division_Code='" + div_code + "' and Trans_Sample_Despatch_Details.productc='" + product_code + "' and Trans_Sample_Despatch_Details.upl_sl_no='" + num + "'";

            iReturn = db_ER.ExecQry(strQry);


            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        //----End by VP
        public DataSet UploadImgae(string filename, string filepath, string subject, string div_code, string sf_code, string sf_name, DateTime eff_from, DateTime eff_to)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string From_Date = eff_from.Month.ToString() + "-" + eff_from.Day + "-" + eff_from.Year;
            string To_Date = eff_to.Month.ToString() + "-" + eff_to.Day + "-" + eff_to.Year;

            strQry = "EXEC Upload_Image  '" + filename + "', '" + filepath + "' , '" + subject + "' , '" + div_code + "', '" + sf_code + "', '" + sf_name + "','" + From_Date + "', '" + To_Date + "'   ";

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
        public int RecordUpdateSession(string Sfcode, string ID)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "update Startworks_Permission set flag='0' " +
                         " where  sf_code='" + Sfcode + "' and ID='" + ID + "'  ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int TpEntry(string div_code, string Radio1)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Setup_Tp_Entry";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "SELECT count(*) FROM Setup_Tp_Entry where Division_Code='" + div_code + "'";
                int cnt = db.Exec_Scalar(strQry);
                if (cnt > 0)
                {
                    strQry = " UPDATE Setup_Tp_Entry  SET  Parameter_Name = '" + Radio1 + "' , " +

                                  " Updated_Date = GetDate()" +
                                  " WHERE Division_Code = '" + div_code + "'";
                }
                else
                {

                    strQry = "insert into Setup_Tp_Entry(Sl_No,Parameter_Name,Division_Code,Created_Date,Updated_Date)" +
                                                           " VALUES('" + Sl_No + "','" + Radio1 + "' ," +
                                                           " '" + div_code + "',getdate(),getdate())";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getAdminSetup_MGR()
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
              "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
              "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
              " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand,Remarks_length_Allowed,TpBased,No_of_TP_View " +
              " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
              " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,Division_Code  from Admin_Setups_Mgr";

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

        public DataSet getAdminSetup(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
                     "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
                     "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
                     " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand, " +
                     " Remarks_length_Allowed,TpBased,No_of_TP_View, " +
                     " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
                     " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr, Doc_App_Needed, wrk_area_Name, Doc_Deact_Needed, Add_Deact_Needed,LockSystem_Needed,LockSystem_Timelimit , " +
                     " ProductFeedback_Needed,PrdRx_Qty_Needed,LstPOB_Updation,ChemPOB_Updation,HalfDayWordEntryNeed,No_of_UnlistDr_Allowed,LstDr_Priority,Display_Patchwise_DR,TpBasesd_DCR,LstDr_Priority_Range,DCR_Dr_Mandatory,TPDCR_Deviation,TPDCR_MGRAppr,ChemPOB_Qty_Needed,PrdRx_Qty_Caption,ChemPOB_Qty_Caption,PrdSample_Qty_Needed,ChemSample_Qty_Needed,Input_Mand," +
                     " PrdSample_Qty_Caption,ChemSample_Qty_Caption,Dr_POBQty_DefaZero,Chem_SampleQty_DefaZero,Chem_POBQty_DefaZero,FieldForceWise_Delay,FFWise_Delay_Days,isnull(Prod_SampleQty_Validation_Needed,0)as Prod_SampleQty_Validation_Needed,isnull(InputQty_Validation_Needed,0) as InputQty_Validation_Needed from Admin_Setups where Division_Code = '" + divcode + "'";



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

        public DataSet GetLeaveStatus(int Leave_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " Select Active_Flag from Mas_Leave_Type where Leave_code = '" + Leave_Code + "'";

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

        public int DeActLeave_Id_New(int Leave_Code, int div_code, int Mode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec Leave_Active_Flag_Change '" + Leave_Code + "','" + div_code + "','" + Mode + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet GetLeaveCount(string div_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(Leave_code) as Leave_Count  from Mas_Leave_Type  where Division_Code='" + div_Code + "'";

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

        public DataSet getLeaveName_New(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Leave_code,Leave_SName,Leave_Name,Active_Flag from mas_Leave_Type where division_code='" + div_code + "'";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public int RecordInsert_Mob_App2(string div_code, string Radio, string Radio2, string Radio3, string Radio4, string Radio5, string Radio6, string Radio7,
         string Radio8, string Radio9, string Radio10, string Radio17, string txtrxqty, string txtsampqty, string Radio11, string Radio14, string Radio15, string Radio16)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "select count(id) from access_master  where RcpaNd='" + Radio + "' and MsdEntry='" + Radio2 + "' and VstNd='" + Radio3 + "'and mailneed='" + Radio4 + "'and "+
                //         "circular='" + Radio5 + "'and FeedNd='" + Radio6 + "'and DrPrdMd='" + Radio7 + "'and DrInpMd='" + Radio8 + "'and DrSmpQMd='" + Radio9 + "'and "+
                //         "DrRxQMd='" + Radio10 + "'and DrRxNd='" + Radio17 + "' and DrRxQCap='" + txtrxqty + "'and DrSmpQCap='" + txtsampqty + "'and " +
                //         "Doc_Pob_Mandatory_Need='" + Radio11 + "'and Attendance='" + Radio14 + "'and MCLDet='" + Radio15 + "'and Chm_Pob_Mandatory_Need='" + Radio16 + "' and division_code='"+div_code+"'";

                //int count = db.Exec_Scalar(strQry);

                //if(count>0)
                //{
                strQry = "update access_master set company_code='1',mobile_access='1',computer_access='1',RcpaNd='" + Radio + "',MsdEntry='" + Radio2 + "',VstNd='" + Radio3 + "',mailneed='" + Radio4 + "'," +
                         "circular='" + Radio5 + "',FeedNd='" + Radio6 + "',DrPrdMd='" + Radio7 + "',DrInpMd='" + Radio8 + "',DrSmpQMd='" + Radio9 + "'," +
                         "DrRxQMd='" + Radio10 + "',DrRxNd='" + Radio17 + "',DrRxQCap='" + txtrxqty + "',DrSmpQCap='" + txtsampqty + "', " +
                         "Doc_Pob_Mandatory_Need='" + Radio11 + "',Attendance='" + Radio14 + "',MCLDet='" + Radio15 + "',Chm_Pob_Mandatory_Need='" + Radio16 + "' where division_code='" + div_code + "'";

                //}
                //else
                //{
                //    strQry = "insert into access_master(company_code,mobile_access,computer_access,RcpaNd,MsdEntry,VstNd,mailneed,circular,FeedNd,DrPrdMd,DrInpMd,DrSmpQMd,DrRxQMd,DrRxNd,DrRxQCap,DrSmpQCap, " +
                //            "Doc_Pob_Mandatory_Need,Attendance,MCLDet,Chm_Pob_Mandatory_Need)values('1','1','1','" + Radio + "','" + Radio2 + "','" + Radio3 + "', " +
                //            "'" + Radio4 + "','" + Radio5 + "','" + Radio6 + "','" + Radio7 + "','" + Radio8 + "','" + Radio9 + "','" + Radio10 + "', " +
                //            "'" + Radio17 + "','" + txtrxqty + "','" + txtsampqty + "','" + Radio11 + "','" + Radio14 + "','" + Radio15 + "','" + Radio16 + "')";
                //}


                iReturn = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;


        }
        public DataSet getting_mob_app_record2(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "select TPDCR_Deviation,TP_Mandatory_Need,Tp_Start_Date,Tp_End_Date,dayplan_tp_based, NextVst,NextVst_Mandatory_Need,Appr_Mandatory_Need,RCPAQty_Need,multiple_doc_need,Cluster_Cap," +
                     "allProdBd,Speciality_prod,FcsNd,Prod_Stk_Need,OtherNd,DlyCtrl,Sep_RcpaNd,doctor_dobdow,Appr_Mandatory_Need,DlyCtrl," +
                     "cip_need,DFNeed,CFNeed,SFNeed,CIP_FNeed,NFNeed,HFNeed,DQNeed,CQNeed, SQNeed," +
                     "NQNeed,CIP_QNeed,HQNeed, DENeed,CENeed, SENeed,NENeed,CIP_ENeed,HENeed,quiz_need," +
                     "pro_det_need,Prodfd_Need,mediaTrans_Need, SpecFilter,Doc_Pob_Need,Chm_Pob_Need,Stk_Pob_Need,Ul_Pob_Need, Stk_Pob_Mandatory_Need,Ul_Pob_Mandatory_Need," +
                     "Doc_jointwork_Need, Chm_jointwork_Need,Stk_jointwork_Need,Ul_jointwork_Need,  Doc_jointwork_Mandatory_Need,Chm_jointwork_Mandatory_Need,Stk_jointwork_Mandatory_Need,Ul_jointwork_Mandatory_Need,Doc_Product_caption, Chm_Product_caption," +
                     "Stk_Product_caption,Ul_Product_caption,DrFeedMd,TPDCR_MGRAppr,MissedDateMand,RmdrNeed,TempNd from access_master  where division_code='" + divcode + "'";

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
        public int RecordInsert_Mob_App3(string div_code, string Radio, string Radio1, string txt_srtdate,
                string txt_enddate, string Radio2, string Radio3, string Radio4, string Radio5, string Radio6, string Radio7,
                string Radio8, string Radio9, string Radio10, string Radio11, string Radio12, string Radio13, string Radio14, string Radio15)
        {
            int iReturn = -1;
            //string startdate = txt_srtdate.Month.ToString() + "-" + txt_srtdate.Day.ToString() + "-" + txt_srtdate.Year.ToString();
            //string enddate = txt_enddate.Month.ToString() + "-" + txt_enddate.Day.ToString() + "-" + txt_enddate.Year.ToString();
            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "select count(id) from access_master  where RcpaNd='" + Radio + "' and MsdEntry='" + Radio2 + "' and VstNd='" + Radio3 + "'and mailneed='" + Radio4 + "'and "+
                //         "circular='" + Radio5 + "'and FeedNd='" + Radio6 + "'and DrPrdMd='" + Radio7 + "'and DrInpMd='" + Radio8 + "'and DrSmpQMd='" + Radio9 + "'and "+
                //         "DrRxQMd='" + Radio10 + "'and DrRxNd='" + Radio17 + "' and DrRxQCap='" + txtrxqty + "'and DrSmpQCap='" + txtsampqty + "'and " +
                //         "Doc_Pob_Mandatory_Need='" + Radio11 + "'and Attendance='" + Radio14 + "'and MCLDet='" + Radio15 + "'and Chm_Pob_Mandatory_Need='" + Radio16 + "' and division_code='"+div_code+"'";

                //int count = db.Exec_Scalar(strQry);

                //if(count>0)
                //{
                strQry = "update access_master set company_code='1',mobile_access='1',computer_access='1',TPDCR_Deviation='" + Radio + "'," +
                         "TP_Mandatory_Need='" + Radio1 + "',Tp_Start_Date='" + txt_srtdate + "',Tp_End_Date='" + txt_enddate + "',dayplan_tp_based='" + Radio2 + "'," +
                         "NextVst='" + Radio3 + "',NextVst_Mandatory_Need='" + Radio4 + "',Appr_Mandatory_Need='" + Radio5 + "',RCPAQty_Need='" + Radio6 + "'," +
                         "multiple_doc_need='" + Radio7 + "',Cluster_Cap='" + Radio8 + "',allProdBd='" + Radio9 + "',Speciality_prod='" + Radio10 + "'," +
                         "FcsNd='" + Radio11 + "',Prod_Stk_Need='" + Radio12 + "',OtherNd='" + Radio13 + "',DlyCtrl='" + Radio14 + "',Sep_RcpaNd='" + Radio15 + "' where division_code='" + div_code + "'";

                //}
                //else
                //{
                //    strQry = "insert into access_master(company_code,mobile_access,computer_access,RcpaNd,MsdEntry,VstNd,mailneed,circular,FeedNd,DrPrdMd,DrInpMd,DrSmpQMd,DrRxQMd,DrRxNd,DrRxQCap,DrSmpQCap, " +
                //            "Doc_Pob_Mandatory_Need,Attendance,MCLDet,Chm_Pob_Mandatory_Need)values('1','1','1','" + Radio + "','" + Radio2 + "','" + Radio3 + "', " +
                //            "'" + Radio4 + "','" + Radio5 + "','" + Radio6 + "','" + Radio7 + "','" + Radio8 + "','" + Radio9 + "','" + Radio10 + "', " +
                //            "'" + Radio17 + "','" + txtrxqty + "','" + txtsampqty + "','" + Radio11 + "','" + Radio14 + "','" + Radio15 + "','" + Radio16 + "')";
                //}


                iReturn = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;


        }

        public DataSet getAdminSetup_MGR(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
            "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
            "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
            " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand,Remarks_length_Allowed,TpBased,No_of_TP_View, " +
            " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
            " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr,wrk_area_Name,LockSystem_Needed,LockSystem_Timelimit," +
            " ProductFeedback_Needed,PrdRx_Qty_Needed,LstPOB_Updation,ChemPOB_Updation,HalfDayWordEntryNeed,TpBased,Display_Patchwise_DR,TpBasesd_DCR,TPDCR_Deviation,TPDCR_MGRAppr," +
            " ChemPOB_Qty_Needed,PrdRx_Qty_Caption,ChemPOB_Qty_Caption,PrdSample_Qty_Needed,ChemSample_Qty_Needed,PrdSample_Qty_Caption,ChemSample_Qty_Caption,Dr_POBQty_DefaZero," +
            " Chem_SampleQty_DefaZero,Chem_POBQty_DefaZero,FieldForceWise_Delay,FFWise_Delay_Days,isnull(Prod_SampleQty_Validation_Needed,0)as Prod_SampleQty_Validation_Needed,isnull(InputQty_Validation_Needed,0) as InputQty_Validation_Needed from Admin_Setups_MGR where Division_Code = '" + divcode + "'";


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
        public int Recorddelete_UntagdrsChem(string chem_code, string div_code, string Mapid)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "delete from map_GEO_ChemiCustomers where cust_code='" + chem_code + "' " +
                         " and Division_code='" + div_code + "' and Mapid='" + Mapid + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Recorddelete_UntagdrsStock(string chem_code, string div_code, string mapid)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "delete from map_GEO_StockCustomers where cust_code='" + chem_code + "' " +
                         " and Division_code='" + div_code + "' and MapId='" + mapid + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getMail(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (Division_Code.Contains(","))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }

            strQry = "select Move_MailFolder_Id,Move_MailFolder_Name from Mas_Mail_Folder_Name where Division_Code in (" + Division_Code + ")";
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

        public DataSet ViewMail(int mailid)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select a.mail_sf_from, a.To_SFName, a.Mail_Subject, a.Mail_Content ,a.Mail_Sent_Time,a.Mail_CC,a.Mail_Content, " +
                      " a.Mail_SF_Name,a.CC_SfName,a.Bcc_SfName,Mail_CC,Mail_BCC,Mail_SF_To,CC_SfName,Bcc_SfName,b.Mail_Read_date " +
                      " from Trans_Mail_Head a,Trans_Mail_detail b " +
                      " where a.Trans_Sl_No = " + mailid + " and a.Trans_Sl_No=b.Trans_Sl_No";

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

        public DataSet FillSalesForce(string des_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select a.sf_code+'-Level1' as sf_mail, a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                     " a.Designation_Code,'' sf_color,'' sf_Type from Mas_Salesforce a,Mas_SF_Designation b " +
                     " where a.Designation_Code in (" + des_code + ")  and a.sf_TP_Active_Flag=0 and a.Designation_Code=b.Designation_Code " +
                     " order by a.Sf_Name";

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

        public DataSet FillDesignation(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (Div_Code != "")
            {
                if (Div_Code.Contains(','))
                {
                    Div_Code = Div_Code.Remove(Div_Code.Length - 1);
                }
                strQry = "select Designation_Code, Designation_Name, Designation_Short_Name  from Mas_sf_Designation where Division_Code='" + Div_Code + "' ";
            }
            else
            {
                strQry = "select Designation_Code, Designation_Name, Designation_Short_Name  from Mas_sf_Designation ";
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
        public DataTable getOtherSetup(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;
            strQry = " select Target_Yearbasedon,isnull(leave_allowed,0)leave_allowed from Setup_Others where Division_Code='" + div_code + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet getMailInbox(string sf_code, string div_code, string type, string folder, string month, string year, string sort, string sortexp, string StrSearch)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "EXEC MailInbox_DivCode  '" + sf_code + "', '" + div_code + "' , '" + type + "' , '" + folder + "', '" + year + "', '" + month + "' , '" + sort + "', '" + sortexp + "','" + StrSearch + "' ";
            //}

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


        //#region Get Mail Function Created by Ramesh
        //public DataSet getMailInbox(string sf_code, string div_code, string type, string folder, string month, string year, string StrSearch)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;

        //    if (div_code.Contains(','))
        //    {
        //        div_code = div_code.Remove(div_code.Length - 1);
        //    }

        //    strQry = "EXEC MailInbox_DivCode_New  '" + sf_code + "', '" + div_code + "' , '" + type + "' , '" + folder + "', '" + year + "', '" + month + "' , '" + StrSearch + "' ";


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
        //#endregion

        #region Get Mail Function Created by Ramesh
        public DataSet getMailInbox(string sf_code, string div_code, string type, string folder, string month, string year, string StrSearch)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //if (div_code == "")
            //{
            //    if (div_code.Contains(','))
            //    {
            //        div_code = div_code.Remove(div_code.Length - 1);
            //    }
            //    strQry = "EXEC MailInbox  '" + sf_code + "', '" + div_code + "' , '" + type + "' , '" + folder + "', '" + year + "', '" + month + "' , '" + sort + "', '" + sortexp + "','" + StrSearch + "' ";
            //}
            ////else
            ////{

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "EXEC MailInbox_DivCode_New  '" + sf_code + "', '" + div_code + "' , '" + type + "' , '" + folder + "', '" + year + "', '" + month + "' , '" + StrSearch + "' ";
            //}

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
        public DataSet getMailAttach(string Trans_Sl_No)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select isnull(Mail_Attachement,'') Mail_Attachement from trans_mail_head where Trans_Sl_No='" + Trans_Sl_No + "'";

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
        public DataSet ComposeMail(string sf_code, string to_sf_code, string subject, string msg, string sAttach, string cc_sf_code, string bcc_sf_code, string div_code, string sRemote, string sToAddr, string sCCAddr, string sBCCAddr, string mail_to_sf_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            subject = subject.Replace("'", "asdf");
            msg = msg.Replace("'", "asdf");

            strQry = "EXEC MailInsert  '" + sf_code + "', '" + to_sf_code + "' , '" + subject + "' , '" + msg + "', '" + sAttach + "', '" + cc_sf_code + "' , '" + bcc_sf_code + "', '" + div_code + "' , '" + sRemote + "', '" + sToAddr + "', '" + sCCAddr + "', '" + sBCCAddr + "','" + mail_to_sf_Name + "'  ";

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
        //public DataSet ComposeMail(string sf_code, string to_sf_code, string subject, string msg, string sAttach, string cc_sf_code, string bcc_sf_code, string div_code, string sRemote, string sToAddr, string sCCAddr, string sBCCAddr, string mail_to_sf_Name)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;

        //    strQry = "EXEC MailInsert  '" + sf_code + "', '" + to_sf_code + "' , '" + subject + "' , '" + msg + "', '" + sAttach + "', '" + cc_sf_code + "' , '" + bcc_sf_code + "', '" + div_code + "' , '" + sRemote + "', '" + sToAddr + "', '" + sCCAddr + "', '" + sBCCAddr + "','" + mail_to_sf_Name + "'  ";

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
        public int ChangeMailStatus(string sf_code, int mail_id, int status, string sf_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            //strQry = "update Trans_Mail_detail set open_mail_id = '" + sf_code + "' , Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id;

            strQry = "update Trans_Mail_detail set Mail_Read_Date=GETDATE(), open_mail_id = '" + sf_code + "' , Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id + " and open_mail_id = '" + sf_code + "'";

            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public bool sRecordExist(string Division_Code)
        {
            if (Division_Code.Contains(','))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Admin_Setups where Division_Code = '" + Division_Code + "'";
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

        public bool sRecordExistMGR(string Division_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Admin_Setups_MGR where Division_Code = '" + Division_Code + "'";
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
        public int RecordUpdate(string Doc_MulPlan, string strWorkAra, string strNoofTPView, int Doc_Count_DCR, int Chem_Count_DCR, int Stk_Count_DCR, int UnLstDr_Count_DCR, int Hos_Count_DCR, int doc_disp, int sess_dcr, int time_dcr, int UnLstDr_reqd, int prod_Qty_zero, int prod_selection, int pob, int DCRSess_Mand, int DCRTime_Mand, int DCRProd_Mand, string wrk_area_SName, int iDelayedSystem, int iHolidayCalc, int iDelayAllowDays, int iHolidayStatus, int iSundayStatus, int iApprovalSystem, string Division_Code, string strDCRTP, int remarkslength, int iDrRem, int inewchem, int inewudr, string ListedDr_Allowed, string Chemist_Allowed, int iDocApp, int iDeactApp, int iAddDeact, string Stockist_Allowed, int lock_sysyem, string lock_time_limit, int ProductFeedback_Needed, int PrdRx_Qty_Needed, int LstPOB_Updation, int ChemPOB_Updation, int halfday_wrk, string No_of_UnlistDr_Allowed, int LstDr_Priority, string Display_Patchwise_DR, int TpBasesd_DCR, string LstDr_Priority_Range, string DCR_Dr_Mandatory, int TPDCR_Deviation, int TPDCR_MGRAppr)
        {
            int iReturn = -1;
            int Count = 0;
            DataSet dsadmin = null;
            bool isdcrsetupupdt = false;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "UPDATE Admin_Setups " +
                //            " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "',No_of_TP_View='" + strNoofTPView + "'";
                bool value = sRecordExist(Division_Code);

                if (value == false)
                {

                    strQry = "Insert into Admin_Setups (SingleDr_WithMultiplePlan_Required,Wrk_Area_Name,No_of_TP_View,No_Of_DCR_Drs_Count,No_Of_DCR_Chem_Count,No_Of_DCR_Stockist_Count,"
                         + " No_Of_DCR_Ndr_Count, No_of_dcr_Hosp, Doctor_disp_in_Dcr, DCRSess_Entry_Permission, NonDrNeeded, DCRTime_Entry_Permission ,"
                         + " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, POBType, DCRSess_Mand, DCRTime_Mand, DCRProd_Mand, wrk_area_SName, "
                         + " DelayedSystem_Required_Status,Delay_Holiday_Status,No_Of_Days_Delay,AutoPost_Holiday_Status,AutoPost_Sunday_Status, Approval_System, Division_Code, TpBased,Remarks_length_Allowed,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr,No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed, Doc_App_Needed, Doc_Deact_Needed, Add_Deact_Needed,No_Of_Sl_StockistAllowed,LockSystem_Needed,LockSystem_Timelimit,ProductFeedback_Needed,PrdRx_Qty_Needed,LstPOB_Updation,ChemPOB_Updation,HalfDayWordEntryNeed,No_of_UnlistDr_Allowed,LstDr_Priority,Display_Patchwise_DR,TpBasesd_DCR,LstDr_Priority_Range,DCR_Dr_Mandatory,TPDCR_Deviation,TPDCR_MGRAppr) values "
                         + " ('" + Doc_MulPlan + "','" + strWorkAra + "','" + strNoofTPView + "','" + Doc_Count_DCR + "' ,'" + Chem_Count_DCR + "','" + Stk_Count_DCR + "', "
                         + " '" + UnLstDr_Count_DCR + "', '" + Hos_Count_DCR + "','" + doc_disp + "', '" + sess_dcr + "','" + UnLstDr_reqd + "', '" + time_dcr + "',"
                         + " '" + prod_Qty_zero + "','" + prod_selection + "','" + pob + "','" + DCRSess_Mand + "','" + DCRTime_Mand + "','" + DCRProd_Mand + "','" + wrk_area_SName + "', "
                         + " '" + iDelayedSystem + "', '" + iHolidayCalc + "', '" + iDelayAllowDays + "','" + iHolidayStatus + "','" + iSundayStatus + "', '" + iApprovalSystem + "', '" + Division_Code + "', '" + strDCRTP + "' ,'" + remarkslength + "' ,'" + iDrRem + "' ,'" + inewchem + "' ,'" + inewudr + "','" + ListedDr_Allowed + "' ,'" + Chemist_Allowed + "', '" + iDocApp + "', '" + iDeactApp + "', '" + iAddDeact + "','" + Stockist_Allowed + "','" + lock_sysyem + "','" + lock_time_limit + "','" + ProductFeedback_Needed + "','" + PrdRx_Qty_Needed + "','" + LstPOB_Updation + "','" + ChemPOB_Updation + "','" + halfday_wrk + "','" + No_of_UnlistDr_Allowed + "','" + LstDr_Priority + "','" + Display_Patchwise_DR + "','" + TpBasesd_DCR + "','" + LstDr_Priority_Range + "','" + DCR_Dr_Mandatory + "','" + TPDCR_Deviation + "','" + TPDCR_MGRAppr + "')";
                }
                else
                {
                    dsadmin = getAdminSetup(Division_Code);

                    if (sess_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()))
                        isdcrsetupupdt = true;
                    if (time_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()))
                        isdcrsetupupdt = true;
                    if (Doc_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()))
                        isdcrsetupupdt = true;
                    if (Chem_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString()))
                        isdcrsetupupdt = true;
                    if (Stk_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()))
                        isdcrsetupupdt = true;
                    if (Hos_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString()))
                        isdcrsetupupdt = true;
                    if (doc_disp != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_reqd != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_Qty_zero != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_selection != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString()))
                        isdcrsetupupdt = true;
                    if (pob != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRSess_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRTime_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRProd_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayedSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayCalc != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayAllowDays != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString()))
                        isdcrsetupupdt = true;
                    if (iSundayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString()))
                        isdcrsetupupdt = true;
                    if (iApprovalSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString()))
                        isdcrsetupupdt = true;
                    if (remarkslength != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString()))
                        isdcrsetupupdt = true;
                    if (iDrRem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString()))
                        isdcrsetupupdt = true;
                    if (inewchem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString()))
                        isdcrsetupupdt = true;
                    if (inewudr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString()))
                        isdcrsetupupdt = true;
                  

                    strQry = "UPDATE Admin_Setups " +
                               " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "', "
                                + " No_of_TP_View = '" + strNoofTPView + "',"
                                + " No_Of_DCR_Drs_Count ='" + Doc_Count_DCR + "' ,"
                                + " No_Of_DCR_Chem_Count ='" + Chem_Count_DCR + "', "
                                + " No_Of_DCR_Stockist_Count ='" + Stk_Count_DCR + "', "
                                + " No_Of_DCR_Ndr_Count ='" + UnLstDr_Count_DCR + "', "
                                + " No_of_dcr_Hosp ='" + Hos_Count_DCR + "', "
                                + " Doctor_disp_in_Dcr = '" + doc_disp + "', "
                                + " DCRSess_Entry_Permission = '" + sess_dcr + "', "
                                + " NonDrNeeded = '" + UnLstDr_reqd + "', "
                                + " DCRTime_Entry_Permission = '" + time_dcr + "', "
                                + " SampleProQtyDefaZero = '" + prod_Qty_zero + "', "
                                + " No_Of_Product_selection_Allowed_in_Dcr = '" + prod_selection + "', "
                                + " POBType = '" + pob + "', "
                                + " DCRSess_Mand = '" + DCRSess_Mand + "', "
                                + " DCRTime_Mand = '" + DCRTime_Mand + "', "
                                + " DCRProd_Mand = '" + DCRProd_Mand + "', "
                                + " wrk_area_SName='" + wrk_area_SName + "', "
                                + " DelayedSystem_Required_Status ='" + iDelayedSystem + "', "
                                + " Delay_Holiday_Status ='" + iHolidayCalc + "', "
                                + " No_Of_Days_Delay ='" + iDelayAllowDays + "', "
                                + " AutoPost_Holiday_Status ='" + iHolidayStatus + "', "
                                + " Approval_System = " + iApprovalSystem + ", "
                                + " TpBased='" + strDCRTP + "',"
                                + " Remarks_length_Allowed='" + remarkslength + "',"
                                + " AutoPost_Sunday_Status ='" + iSundayStatus + "', "
                                + " DCRLDR_Remarks ='" + iDrRem + "', "
                                + " DCRNew_Chem ='" + inewchem + "', "
                                + " DCRNew_ULDr ='" + inewudr + "',"
                                + " No_Of_Sl_DoctorsAllowed='" + ListedDr_Allowed + "',"
                                + " No_Of_Sl_ChemistsAllowed='" + Chemist_Allowed + "', Doc_App_Needed='" + iDocApp + "', Doc_Deact_Needed = '" + iDeactApp + "', Add_Deact_Needed = '" + iAddDeact + "', No_Of_Sl_StockistAllowed='" + Stockist_Allowed + "',LockSystem_Needed='" + lock_sysyem + "',LockSystem_Timelimit='" + lock_time_limit + "',ProductFeedback_Needed='" + ProductFeedback_Needed + "',PrdRx_Qty_Needed='" + PrdRx_Qty_Needed + "',LstPOB_Updation='" + LstPOB_Updation + "',ChemPOB_Updation='" + ChemPOB_Updation + "',HalfDayWordEntryNeed='" + halfday_wrk + "',No_of_UnlistDr_Allowed='" + No_of_UnlistDr_Allowed + "',LstDr_Priority='" + LstDr_Priority + "',Display_Patchwise_DR='" + Display_Patchwise_DR + "',TpBasesd_DCR='" + TpBasesd_DCR + "',DCR_Dr_Mandatory='" + DCR_Dr_Mandatory + "',TPDCR_Deviation='" + TPDCR_Deviation + "',TPDCR_MGRAppr='" + TPDCR_MGRAppr + "',LstDr_Priority_Range='" + LstDr_Priority_Range + "' where Division_Code = '" + Division_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);

                if (isdcrsetupupdt == true)
                {
                    strQry = "UPDATE Admin_Setups set LastUpdt_DCRStp = getdate() where Division_Code = '" + Division_Code + "' ";
                }

                iReturn = db.ExecQry(strQry);




            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;


        }

        //public int RecordUpdate_MGR(int Doc_Count_DCR, int Chem_Count_DCR, int Stk_Count_DCR, int UnLstDr_Count_DCR, int Hos_Count_DCR, int doc_disp, int sess_dcr, int time_dcr, int UnLstDr_reqd, int prod_Qty_zero, int prod_selection, int pob, int DCRSess_Mand, int DCRTime_Mand, int DCRProd_Mand, int iDelayedSystem, int iHolidayCalc, int iDelayAllowDays, int iHolidayStatus, int iSundayStatus, int iApprovalSystem, string Division_Code, int remarkslength, int iDrRem, int inewchem, int inewudr, string strWorkAra, int lock_sysyem, string lock_time_limit, int ProductFeedback_Needed, int PrdRx_Qty_Needed, int LstPOB_Updation, int ChemPOB_Updation, int halfday_wrk, string strDCRTP, string Display_Patchwise_DR, int TpBasesd_DCR, int TPDCR_Deviation, int TPDCR_MGRAppr)
        //{
        //    int iReturn = -1;
        //    int Count = 0;
        //    DataSet dsadmin = null;
        //    bool isdcrsetupupdt = false;

        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        //strQry = "UPDATE Admin_Setups " +
        //        //            " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "',No_of_TP_View='" + strNoofTPView + "'";
        //        bool value = sRecordExistMGR(Division_Code);

        //        if (value == false)
        //        {

        //            strQry = "Insert into Admin_Setups_MGR (No_Of_DCR_Drs_Count,No_Of_DCR_Chem_Count,No_Of_DCR_Stockist_Count,"
        //                 + " No_Of_DCR_Ndr_Count, No_of_dcr_Hosp,Doctor_disp_in_Dcr, DCRSess_Entry_Permission, NonDrNeeded, DCRTime_Entry_Permission ,"
        //                 + " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, POBType, DCRSess_Mand, DCRTime_Mand, DCRProd_Mand ,"
        //                 + " DelayedSystem_Required_Status,Delay_Holiday_Status,No_Of_Days_Delay,AutoPost_Holiday_Status,AutoPost_Sunday_Status, Approval_System, Division_Code ,Remarks_length_Allowed,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr,wrk_area_Name,LockSystem_Needed,LockSystem_Timelimit,ProductFeedback_Needed,PrdRx_Qty_Needed,LstPOB_Updation,ChemPOB_Updation,HalfDayWordEntryNeed,TpBased,string Display_Patchwise_DR,TpBasesd_DCR,TPDCR_Deviation,TPDCR_MGRAppr) values "
        //                 + " ('" + Doc_Count_DCR + "' ,'" + Chem_Count_DCR + "','" + Stk_Count_DCR + "', "
        //                 + " '" + UnLstDr_Count_DCR + "', '" + Hos_Count_DCR + "','" + doc_disp + "', '" + sess_dcr + "','" + UnLstDr_reqd + "', '" + time_dcr + "',"
        //                 + " '" + prod_Qty_zero + "','" + prod_selection + "','" + pob + "','" + DCRSess_Mand + "','" + DCRTime_Mand + "','" + DCRProd_Mand + "' , "
        //                 + " '" + iDelayedSystem + "', '" + iHolidayCalc + "', '" + iDelayAllowDays + "','" + iHolidayStatus + "','" + iSundayStatus + "', '" + iApprovalSystem + "', '" + Division_Code + "','" + remarkslength + "','" + iDrRem + "' ,'" + inewchem + "' ,'" + inewudr + "','" + strWorkAra + "','" + lock_sysyem + "','" + lock_time_limit + "','" + ProductFeedback_Needed + "','" + PrdRx_Qty_Needed + "','" + LstPOB_Updation + "','" + ChemPOB_Updation + "','" + halfday_wrk + "','" + strDCRTP + "','"+Display_Patchwise_DR+"','"+TpBasesd_DCR+"','"+TPDCR_Deviation+"','"+TPDCR_MGRAppr+"')";
        //        }
        //        else
        //        {
        //            dsadmin = getAdminSetup_MGR(Division_Code);

        //            if (sess_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()))
        //                isdcrsetupupdt = true;
        //            if (time_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()))
        //                isdcrsetupupdt = true;
        //            if (Doc_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()))
        //                isdcrsetupupdt = true;
        //            if (Chem_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()))
        //                isdcrsetupupdt = true;
        //            if (UnLstDr_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString()))
        //                isdcrsetupupdt = true;
        //            if (Stk_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()))
        //                isdcrsetupupdt = true;
        //            if (Hos_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString()))
        //                isdcrsetupupdt = true;
        //            if (doc_disp != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()))
        //                isdcrsetupupdt = true;
        //            if (UnLstDr_reqd != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()))
        //                isdcrsetupupdt = true;
        //            if (prod_Qty_zero != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString()))
        //                isdcrsetupupdt = true;
        //            if (prod_selection != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString()))
        //                isdcrsetupupdt = true;
        //            if (pob != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString()))
        //                isdcrsetupupdt = true;
        //            if (DCRSess_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString()))
        //                isdcrsetupupdt = true;
        //            if (DCRTime_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString()))
        //                isdcrsetupupdt = true;
        //            if (DCRProd_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString()))
        //                isdcrsetupupdt = true;
        //            if (iDelayedSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString()))
        //                isdcrsetupupdt = true;
        //            if (iHolidayCalc != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString()))
        //                isdcrsetupupdt = true;
        //            if (iDelayAllowDays != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString()))
        //                isdcrsetupupdt = true;
        //            if (iHolidayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString()))
        //                isdcrsetupupdt = true;
        //            if (iSundayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString()))
        //                isdcrsetupupdt = true;
        //            if (iApprovalSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString()))
        //                isdcrsetupupdt = true;
        //            if (remarkslength != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString()))
        //                isdcrsetupupdt = true;
        //            if (iDrRem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString()))
        //                isdcrsetupupdt = true;
        //            if (inewchem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString()))
        //                isdcrsetupupdt = true;
        //            if (inewudr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString()))
        //                isdcrsetupupdt = true;

        //            strQry = "UPDATE Admin_Setups_MGR " +
        //                       " SET Doctor_disp_in_Dcr = '" + doc_disp + "', "
        //                        + " No_Of_DCR_Drs_Count ='" + Doc_Count_DCR + "' ,"
        //                        + " No_Of_DCR_Chem_Count ='" + Chem_Count_DCR + "', "
        //                        + " No_Of_DCR_Stockist_Count ='" + Stk_Count_DCR + "', "
        //                        + " No_Of_DCR_Ndr_Count ='" + UnLstDr_Count_DCR + "', "
        //                        + " No_of_dcr_Hosp ='" + Hos_Count_DCR + "', "
        //                        + " DCRSess_Entry_Permission = '" + sess_dcr + "', "
        //                        + " NonDrNeeded = '" + UnLstDr_reqd + "', "
        //                        + " DCRTime_Entry_Permission = '" + time_dcr + "', "
        //                        + " SampleProQtyDefaZero = '" + prod_Qty_zero + "', "
        //                        + " No_Of_Product_selection_Allowed_in_Dcr = '" + prod_selection + "', "
        //                        + " POBType = '" + pob + "', "
        //                        + " DCRSess_Mand = '" + DCRSess_Mand + "', "
        //                        + " DCRTime_Mand = '" + DCRTime_Mand + "', "
        //                        + " DCRProd_Mand = '" + DCRProd_Mand + "', "
        //                        + " DelayedSystem_Required_Status ='" + iDelayedSystem + "', "
        //                        + " Delay_Holiday_Status ='" + iHolidayCalc + "', "
        //                        + " No_Of_Days_Delay ='" + iDelayAllowDays + "', "
        //                        + " AutoPost_Holiday_Status ='" + iHolidayStatus + "', "
        //                        + " Approval_System = " + iApprovalSystem + ", "
        //                        + " Remarks_length_Allowed='" + remarkslength + "',"
        //                        + " AutoPost_Sunday_Status ='" + iSundayStatus + "',"
        //                        + " DCRLDR_Remarks ='" + iDrRem + "', "
        //                        + " DCRNew_Chem ='" + inewchem + "', "
        //                        + " DCRNew_ULDr ='" + inewudr + "' ,"
        //                        + " wrk_area_Name ='" + strWorkAra + "',LockSystem_Needed='" + lock_sysyem + "',LockSystem_Timelimit='" + lock_time_limit + "',ProductFeedback_Needed='" + ProductFeedback_Needed + "',PrdRx_Qty_Needed='" + PrdRx_Qty_Needed + "',LstPOB_Updation='" + LstPOB_Updation + "',ChemPOB_Updation='" + ChemPOB_Updation + "',HalfDayWordEntryNeed='" + halfday_wrk + "',TpBased='" + strDCRTP + "',Display_Patchwise_DR='" + Display_Patchwise_DR + "',TpBasesd_DCR='" + TpBasesd_DCR + "',TPDCR_Deviation='" + TPDCR_Deviation + "',TPDCR_MGRAppr='"+TPDCR_MGRAppr+"' where Division_Code = '" + Division_Code + "' ";
        //        }


        //        iReturn = db.ExecQry(strQry);
        //        // strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strTPApprovalSystem + "' where Designation_Short_Name='DM'";

        //        if (isdcrsetupupdt == true)
        //        {
        //            strQry = "UPDATE Admin_Setups_MGR set LastUpdt_DCRStp = getdate() where Division_Code = '" + Division_Code + "' ";
        //        }

        //        iReturn = db.ExecQry(strQry);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return iReturn;
        //}
        public int FlashRecordAdd(string cont1, string div_code, string chkback)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int sl_no = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Flash_News";
                sl_no = db.Exec_Scalar(strQry);
                if (Flash_RecordExist(div_code))
                {
                    strQry = "update Mas_Flash_News set FN_Cont1 ='" + cont1 + "',FNHome_Page_Flag='" + chkback + "' " +
                      " where FN_Active_Flag=0 and Division_Code='" + div_code + "'";
                }
                else
                {

                    strQry = " INSERT INTO Mas_Flash_News(Sl_No,FN_Cont1,Division_Code,Created_Date,FN_Active_Flag,FNHome_Page_Flag) " +
                             " VALUES ( " + sl_no + " , '" + cont1 + "' , '" + div_code + "', getdate(),0,'" + chkback + "') ";
                }
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet Get_Flash_News(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,FN_Cont1,Division_Code,created_Date,FN_Active_Flag,FNHome_Page_Flag " +
                " from Mas_Flash_News where Division_Code='" + div_code + "' and FN_Active_Flag=0 ";

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
        public DataSet Get_Flash_News_Home(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,FN_Cont1,Division_Code,created_Date,FN_Active_Flag,FNHome_Page_Flag " +
                " from Mas_Flash_News where Division_Code='" + div_code + "' and FN_Active_Flag=0 and FNHome_Page_Flag=1  ";

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

        public DataSet Get_NB_Record(string div_code, string Start_Date, string End_Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "select Sl_No, NB_Cont1, NB_Cont2, NB_Cont3" +
                " from Mas_Notice_Board " +
                " where Division_Code ='" + div_code + "' and  Start_Date='" + Start_Date.Substring(6, 4) + "-" + Start_Date.Substring(3, 2) + "-" + Start_Date.Substring(0, 2) + "' and  End_Date='" + End_Date.Substring(6, 4) + "-" + End_Date.Substring(3, 2) + "-" + End_Date.Substring(0, 2) + "'";
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
        public DataSet Get_Notice(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            if (div_code != "")
            {
                strQry = "  declare @Date as datetime " +
                      " set @Date=(select max(End_Date)as End_Date  from Mas_Notice_Board where End_Date>=GETDATE() and Division_Code ='" + div_code + "') " +
                      " select Sl_No, NB_Cont1,NB_Cont2, NB_Cont3,Start_Date,End_Date  from Mas_Notice_Board  where  Division_Code ='" + div_code + "' and " +
                      " NB_Active_Flag=0 and (Start_Date <= GETDATE() and End_Date=@Date) ";
            }
            else
            {
                strQry = "  declare @Date as datetime " +
                       " set @Date=(select max(End_Date)as End_Date  from Mas_Notice_Board where End_Date>=GETDATE() and Division_Code ='" + div_code + "')" +
                       " select Sl_No, NB_Cont1,NB_Cont2, NB_Cont3,Start_Date,End_Date  from Mas_Notice_Board  where " +
                       " NB_Active_Flag=0 and (Start_Date <= GETDATE() and End_Date=@Date) ";
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

        public int NB_RecordAdd(string cont1, string cont2, string cont3, string start_date, string end_date, string div_code, string chkback)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int sl_no = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Notice_Board";
                sl_no = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Mas_Notice_Board(Sl_No, NB_Cont1, NB_Cont2, NB_Cont3, Start_Date, End_Date, Division_Code, Created_Date,NB_Active_Flag,NBHome_Page_Flag) " +
                         " VALUES ( " + sl_no + " , '" + cont1 + "', '" + cont2 + "', '" + cont3 + "', '" + start_date.Substring(6, 4) + "-" + start_date.Substring(3, 2) + "-" + start_date.Substring(0, 2) + "', '" + end_date.Substring(6, 4) + "-" + end_date.Substring(3, 2) + "-" + end_date.Substring(0, 2) + "', '" + div_code + "',  getdate(),0,'" + chkback + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //  public int Add_Admin_FieldForce_Setup(string sf_code, string div_code, int iDoctorAdd, int iDoctorEdit, int iDoctorDel, int iDoctorDeAct, int iNewDoctorAdd, 
        //    int iNewDoctorDeAct, int iChemAdd, int iChemEdit, int iChemDeAct, int iTerrAdd, int iTerrEdit, int iTerrDel, int iTerrDeAct)
        //{
        //    int iReturn = -1;

        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        int sl_no = -1;
        //        DateTime deactDt = DateTime.Now.AddDays(-1);

        //        strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Admin_FieldForce_Setup";
        //        sl_no = db.Exec_Scalar(strQry);

        //        strQry = " INSERT INTO Admin_FieldForce_Setup(Sl_No, Sf_Code, Division_Code, ListedDr_Add_Option, ListedDr_Edit_Option, ListedDr_Delete_Option, " +
        //                    " ListedDr_Deactivate_Option, NewDoctor_Entry_OptioninDCR, Chemist_Add_Option, Chemist_Edit_Option, Chemist_Deactivate_Option , " +
        //                    " Territory_Add_Option, Territory_Edit_Option, Territory_Deactivate_Option, Territory_Delete_Option, " +
        //                    " ListedDr_View_Option,Chemist_Delete_Option,Chemist_View_Option, Stockist_Add_Option,Stockist_Deactivate_Option, " +
        //                    " Stockist_Delete_Option,Stockist_Edit_Option,Stockist_View_Option,Territory_View_Option ) " +
        //                 " VALUES ( " + sl_no + " , '" + sf_code + "', '" + div_code + "',  " + iDoctorAdd + ",  " + iDoctorEdit + ",  " + iDoctorDel + ", " +
        //                 " " + iDoctorDeAct + ", " + iNewDoctorAdd + ", " + iChemAdd + ", " + iChemEdit + ", " + iChemDeAct + ", " + iTerrAdd + ", " +
        //                 " " + iTerrEdit + ", " + iTerrDeAct + ", " + iTerrDel + ", 0, 0, 0, 0, 0, 0, 0, 0, 0) ";

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return iReturn;
        //}
        public int Add_Admin_FieldForce_Setup(string sf_code, string div_code,
            int iDoctorAdd, int iDoctorEdit, int iDoctorDeAct, int iDoctorView,
            int iNewDoctorAdd, int iNewDoctorEdit, int iNewDoctorDeAct, int iNewDoctorView,
            int iChemAdd, int iChemEdit, int iChemDeAct, int iChemView,
            int iTerrAdd, int iTerrEdit, int iTerrDeAct, int iTerrView,
            int iClassAdd, int iClassEdit, int iClassDeAct, int iClassView,
            int iDoctorReAct, int iNewDoctorReAct, int iChemReAct, int iClassReAct, int iDocName)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int sl_no = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);

                if (Admin_FieldForce_Setup_RecordExist(sf_code))
                {
                    //Admin_FieldForce_Setup_RecordExist(sf_code);

                    strQry = "SELECT Sl_No FROM Admin_FieldForce_Setup where sf_code = '" + sf_code + "' ";
                    sl_no = db.Exec_Scalar(strQry);

                    strQry = " UPDATE Admin_FieldForce_Setup " +
                                " SET ListedDr_Add_Option = '" + iDoctorAdd + "', ListedDr_Edit_Option = '" + iDoctorEdit + "' , " +
                                " ListedDr_Deactivate_Option = '" + iDoctorDeAct + "', ListedDr_View_Option = '" + iDoctorView + "', " +
                                " NewDoctor_Entry_Option = '" + iNewDoctorAdd + "', NewDoctor_Edit_Option = '" + iNewDoctorEdit + "', " +
                                " NewDoctor_DeAct_Option = '" + iNewDoctorDeAct + "', NewDoctor_View_Option = '" + iNewDoctorView + "', " +
                                " Chemist_Add_Option = '" + iChemAdd + "', Chemist_Edit_Option = '" + iChemEdit + "' , " +
                                " Chemist_Deactivate_Option = '" + iChemDeAct + "', Chemist_View_Option = '" + iChemView + "', " +
                                " Territory_Add_Option = '" + iTerrAdd + "', Territory_Edit_Option = '" + iTerrEdit + "', " +
                                " Territory_Deactivate_Option = '" + iTerrDeAct + "', Territory_View_Option = '" + iTerrView + "', " +
                                " Class_Add_Option = '" + iClassAdd + "', Class_Edit_Option = '" + iClassEdit + "', " +
                                " Class_Deactivate_Option = '" + iClassDeAct + "', Class_View_Option = '" + iClassView + "', " +
                                " ListedDr_Reactivate_Option ='" + iDoctorReAct + "', " +
                                " NewDoctor_ReAct_Option ='" + iNewDoctorReAct + "'," +
                                " Chemist_Reactivate_Option ='" + iChemReAct + "', " +
                                " Class_Reactivate_Option = '" + iClassReAct + "', Doc_Name_Chg = '" + iDocName + "' " +
                                " WHERE Sl_No = " + sl_no + " and sf_code = '" + sf_code + "' ";
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Admin_FieldForce_Setup";
                    sl_no = db.Exec_Scalar(strQry);

                    strQry = " INSERT INTO Admin_FieldForce_Setup(Sl_No, Sf_Code, Division_Code, " +
                                " ListedDr_Add_Option, ListedDr_Edit_Option, ListedDr_Deactivate_Option, ListedDr_View_Option, " +
                                " NewDoctor_Entry_Option, NewDoctor_Edit_Option, NewDoctor_DeAct_Option, NewDoctor_View_Option, " +
                                " Chemist_Add_Option, Chemist_Edit_Option, Chemist_Deactivate_Option, Chemist_View_Option, " +
                                " Territory_Add_Option, Territory_Edit_Option, Territory_Deactivate_Option, Territory_View_Option, " +
                                " Class_Add_Option, Class_Edit_Option, Class_Deactivate_Option, Class_View_Option, " +
                                " ListedDr_Reactivate_Option, NewDoctor_ReAct_Option, Chemist_Reactivate_Option, Class_Reactivate_Option, Doc_Name_Chg) " +
                             " VALUES ( " + sl_no + " , '" + sf_code + "', '" + div_code + "',  " +
                             iDoctorAdd + ",  " + iDoctorEdit + ",  " + iDoctorDeAct + ", " + iDoctorView + ", " +
                             iNewDoctorAdd + ", " + iNewDoctorEdit + ", " + iNewDoctorDeAct + ", " + iNewDoctorView + ", " +
                             iChemAdd + ", " + iChemEdit + ", " + iChemDeAct + ", " + iChemView + ", " +
                             iTerrAdd + ", " + iTerrEdit + ", " + iTerrDeAct + ", " + iTerrView + ", " +
                             iClassAdd + ", " + iClassEdit + ", " + iClassDeAct + ", " + iClassView + ", " +
                             iDoctorReAct + ", " + iNewDoctorReAct + ", " + iChemReAct + ", " + iClassReAct + ", '" + iDocName + "' ) ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getMR_MGR(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Sf_Code, sf_name, sf_Designation_Short_Name,sf_hq " +
                        "  from Mas_Salesforce " +
                        "  where sf_TP_Active_Flag=0 and sf_type=1 and TP_Reporting_SF = '" + sf_code + "' ";

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

        public DataSet Get_Admin_FieldForce_Setup(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Sf_Code, Division_Code , " +
                        " ListedDr_Add_Option, ListedDr_Edit_Option, ListedDr_Deactivate_Option, ListedDr_View_Option, " +
                        " NewDoctor_Entry_Option, NewDoctor_Edit_Option, NewDoctor_Deact_Option, NewDoctor_View_Option, " +
                        " Chemist_Add_Option, Chemist_Edit_Option, Chemist_Deactivate_Option, Chemist_View_Option, " +
                        " Territory_Add_Option,Territory_Edit_Option,Territory_Deactivate_Option,Territory_View_Option, " +
                        " Class_Add_Option, Class_Edit_Option, Class_Deactivate_Option, Class_View_Option, " +
                        " ListedDr_Reactivate_Option, NewDoctor_ReAct_Option, Chemist_Reactivate_Option, Class_Reactivate_Option, Doc_Name_Chg " +
                        "  from dbo.Admin_FieldForce_Setup " +
                        "  where Sf_Code = '" + sf_code + "' and Division_Code='" + div_code + "' ";

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
        public bool Admin_FieldForce_Setup_RecordExist(string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Admin_FieldForce_Setup " +
                         " where sf_code = '" + sf_code + "' ";

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
        public int QuoteAdd(string Quote_Text, string div_code, string chkback)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Trans_Quote ";
                int Sl_No = db.Exec_Scalar(strQry);
                if (Quote_RecordExist(div_code))
                {
                    strQry = "update Trans_Quote set Quote_Text ='" + Quote_Text + "', Division_Code='" + div_code + "', Home_Page_Flag='" + chkback + "' " +
                        " where Quote_Active_Flag=0";
                }
                else
                {

                    strQry = " INSERT INTO Trans_Quote(Sl_No,Quote_Text,Division_Code,Created_Date,Quote_Active_Flag,Home_Page_Flag) " +
                             " VALUES ('" + Sl_No + "' ,'" + Quote_Text + "' , '" + div_code + "', getdate(),0,'" + chkback + "') ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet Get_Quote(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Sl_No,Quote_Text,Division_Code,Created_Date,Quote_Active_Flag,Home_Page_Flag " +
                " from Trans_Quote where Quote_Active_Flag=0 and Division_Code='" + div_code + "' ";

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
        public bool Quote_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Trans_Quote " +
                         " where Division_Code = '" + div_code + "' and Quote_Active_Flag=0 ";

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
        public int Delete_Quote(string Quote_Text, string div_code)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();

            try
            {
                strQry = "update Trans_Quote set Quote_Active_Flag=1 " +
                 " where Quote_Active_Flag=0 and Quote_Text ='" + Quote_Text + "' and Division_Code = '" + div_code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return iReturn;
        }
        public DataSet Get_Quote_Home(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,Quote_Text,Division_Code,Created_Date,Quote_Active_Flag,Home_Page_Flag " +
                " from Trans_Quote where Quote_Active_Flag=0 and Division_Code='" + div_code + "' and Home_Page_Flag=1 ";

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

        //Changes Done by Sridevi - Starts
        public DataSet Get_FileUpload(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm " +
            //            "  from file_info " +
            //            "  where div_Code = '" + div_code + "' ";
            strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                     "  from file_info " +
                     "  where div_Code = '" + div_code + "'";

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

        public int FileUpload_Add(string div_code, string FileName, string FileSubject)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " INSERT INTO file_info(FileName,FileSubject,Div_Code,Update_dtm) " +
                         " VALUES ( '" + FileName + "' , '" + FileSubject + "', '" + div_code + "',  getdate()) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Core_Doctor_Map_Add(string sf_code, string div_code, string Mgr_Code, string DR_Code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " INSERT INTO Core_Doctor_Map(sf_code,Division_Code,Mgr_Code,DR_Code) " +
                         " VALUES ( '" + sf_code + "' , '" + div_code + "', '" + Mgr_Code + "', '" + DR_Code + "' ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Core_Doctor_Map_Delete(string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " DELETE FROM Core_Doctor_Map " +
                         " WHERE sf_code= '" + sf_code + "' and Division_code= '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool Core_Doctor_Map_RecordExist(string Mgr_Code, string sf_code, string div_code, string doc_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(DR_Code) from Core_Doctor_Map " +
                         " where Mgr_Code = '" + Mgr_Code + "' and " +
                         " sf_code = '" + sf_code + "' and " +
                         " Division_code = '" + div_code + "' and " +
                         " DR_Code = '" + doc_code + "' ";

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


        //public int Screen_Lock_Add(string mgr_code, string sf_code, string div_code, int DCR_Lock, int TP_Lock, int SDP_Lock, int Camp_Lock, int DR_Lock, string Unlst, int Core_Lock)
        //{
        //    int iReturn = -1;

        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();
        //        DB_EReporting db_ER = new DB_EReporting();

        //        DataSet dsAdmin = null;

        //        strQry = "SELECT isnull(max([S.No])+1,'1') [S.No] from Screen_Lock ";
        //        int SNo = db.Exec_Scalar(strQry);

        //        strQry = "select count(sf_code) from Screen_Lock " +
        //                "where sf_code='" + sf_code + "'  ";


        //        dsAdmin = db_ER.Exec_DataSet(strQry);
        //        if (dsAdmin.Tables[0].Rows[0][0].ToString() == "0")
        //        {
        //            strQry = " INSERT INTO Screen_Lock([S.No],Mgr_Code, SF_Code,Div_Code,DCR_Lock,TP_Lock,SDP_Lock,Camp_Lock,DR_Lock,Update_dtm,Unlst_Cnt,Coredr_Lock) " +
        //                 " VALUES ( '" + SNo + "','" + mgr_code + "' , '" + sf_code + "' , '" + div_code + "', " + DCR_Lock + " , " + TP_Lock + ", " + SDP_Lock + ", " + Camp_Lock + ", " + DR_Lock + ", getdate(),'" + Unlst + "','" + Core_Lock + "' ) ";
        //        }
        //        else
        //        {
        //            strQry = "update Screen_Lock set DCR_Lock='" + DCR_Lock + "',TP_Lock='" + TP_Lock + "',SDP_Lock='" + SDP_Lock + "',Camp_Lock='" + Camp_Lock + "',DR_Lock='" + DR_Lock + "',Unlst_Cnt='" + Unlst + "',Coredr_Lock='" + Core_Lock + "' where SF_Code='" + sf_code + "' ";

        //        }

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return iReturn;
        //}

        public int Screen_Lock_Delete(string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " DELETE FROM Screen_Lock " +
                         " WHERE Mgr_Code= '" + sf_code + "' and Div_code= '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet Mail_Status(string From_Dt, string To_Dt, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = " select count(Distinct Open_Mail_Id)cnt,Mail_SF_From,Mail_Subject,sf_name,sf_code,Designation_Short_Name,sf_type,mail_sent_time,H.trans_sl_no from Trans_Mail_Head H " +
"inner join trans_mail_detail D on H.Trans_Sl_No=D.Trans_Sl_No " +
"inner join mas_salesforce S on H.Mail_SF_From=S.Sf_Code " +
"inner join mas_sf_designation DF on S.sf_Designation_Short_Name=DF.Designation_Short_Name " +
"where mail_sent_time between '" + From_Dt + "' and dateadd(day, 1, '" + To_Dt + "') and H.division_code='" + div_code + "' " +
"group by Mail_SF_From,Mail_Subject,sf_name,sf_code,Designation_Short_Name,sf_type,mail_sent_time,H.trans_sl_no order by " +
"replace(replace(replace(sf_type,'','A'),'2','B'),'1','C')asc,Sf_Name,mail_sent_time desc ";
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
        public DataSet Mail_Status_Zoom(string sl_no)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = " select sf_name,mail_read_date,Sf_HQ,sf_Designation_Short_Name from(select sf_name,mail_read_date,Sf_HQ,sf_Designation_Short_Name from trans_mail_detail D inner join Trans_Mail_Head H on D.trans_Sl_no=H.trans_sl_no inner join mas_salesforce s on s.sf_Code=D.open_mail_id where H.trans_sl_no='" + sl_no + "' Union select name sf_name,mail_read_date,'' Sf_HQ,'' sf_Designation_Short_Name from trans_mail_detail D inner join Trans_Mail_Head H on D.trans_Sl_no=H.trans_sl_no inner join mas_ho_id_creation s on convert(varchar(20),s.ho_id)=D.open_mail_id where H.trans_sl_no='" + sl_no + "')as D order by sf_name,mail_read_date desc ";
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
        public int Permission_MR_Add(string mgr_code, string sf_code, string div_code, int Level1, int Level2, int Level3)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(pid)+1,'1') pid from Permission_MR ";
                int pid = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Permission_MR(pid,Mgr_Code, SF_Code,Div_Code,Level1,Level2,Level3,Update_dtm) " +
                         " VALUES ('" + pid + "' ,'" + mgr_code + "' , '" + sf_code + "' , '" + div_code + "', " + Level1 + " , " + Level2 + ", " + Level3 + ", getdate() ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public int Permission_MR_Delete(string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " DELETE FROM Permission_MR " +
                         " WHERE Mgr_Code= '" + sf_code + "' and Div_code= '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //Changes Done by Sridevi - Ends

        //changes done by Priya
        public DataSet getNBDate(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT 'Select' NB_Start_End UNION SELECT DISTINCT (convert(varchar(12),Start_Date,103)+' To '+convert(varchar(12),End_Date,103)) NB_Start_End FROM Mas_Notice_Board " +
                       " WHERE NB_Active_Flag=0 AND division_code=  '" + divcode + "' ORDER BY 1 DESC";

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
        public int Delete_Flash(string Flash_Text, string div_code)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();

            try
            {
                strQry = "update Mas_Flash_News set FN_Active_Flag=1 " +
                 " where FN_Active_Flag=0 and FN_Cont1 ='" + Flash_Text + "' and  Division_Code = '" + div_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return iReturn;
        }
        public bool Flash_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Flash_News " +
                         " where Division_Code = '" + div_code + "' and FN_Active_Flag=0 ";

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
        public DataSet Get_Notice_Home(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select Sl_No, NB_Cont1,NB_Cont2, NB_Cont3,Start_Date,End_Date,NB_Active_Flag,NBHome_Page_Flag " +
            //  " from Mas_Notice_Board " +
            //  " where Division_Code ='" + div_code + "' and NBHome_Page_Flag=1 and NB_Active_Flag=0 ";

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No, NB_Cont1,NB_Cont2, NB_Cont3,Start_Date,End_Date,NB_Active_Flag,NBHome_Page_Flag " +
             " from Mas_Notice_Board " +
             " where Division_Code ='" + div_code + "' and (Start_Date <= GETDATE() AND End_Date >=GETDATE()) " +
             " and NBHome_Page_Flag=1 and NB_Active_Flag=0 order by sl_no desc ";

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

        //Included this function to update the listed doctor plan from multi to single thru set up
        //by Sridevi on 12/08/14
        public int RecordUpdate_ListedDR(string terr_code, string doc_code, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_listeddr " +
                         " SET Territory_Code = '" + terr_code + "' " +
                         " WHERE ListedDrCode = '" + doc_code + "' and Division_code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet get_listed_doctor(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select ListedDrCode , Territory_Code " +
                        "  from mas_listeddr " +
                        "  where Territory_Code is not null and Territory_Code <> '' and Division_code = '" + div_code + "'";

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
        //Changes done by Saravnan
        public DataSet getMoved(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select '0' as Move_MailFolder_Id,'' as Move_MailFolder_Name " +
                     "Union ALL " +
                     "select Move_MailFolder_Id,Move_MailFolder_Name from Mas_Mail_Folder_Name where Division_Code = '" + Division_Code + "'";
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

        public DataSet GetDivision(string Division_Code) // move
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT a.sf_code+'-Level1' as sf_mail, a.Sf_Name, a.Sf_UserName, a.sf_Type,'' sf_color," +
                     "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF,b.Designation_Short_Name," +
                     "a.sf_hq,a.sf_desgn, a.sf_password FROM mas_salesforce a,Mas_SF_Designation b " +
                     "WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 " +
                     "and a.Designation_Code=b.Designation_Code " +
                     "and a.Division_Code = '" + Division_Code + "'  ";

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

        public int ChangeMailDeleteStatus(string sf_code, int mail_id, int status) // move
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "update Trans_Mail_detail set open_mail_id = '" + sf_code + "' , Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id;

            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int ChangeMailFolder(string sf_code, int mail_id, string ddlMovedFolder, int status) // move
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "update Trans_Mail_detail set open_mail_id = '" + sf_code + "' ,Mail_Moved_to='" + ddlMovedFolder + "', Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id;

            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Changes done by Saravanan
        public int get_MailCount(string division_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (division_code.Contains(","))
            {
                division_code = division_code.Remove(division_code.Length - 1);
            }

            int iReturn = -1;
            //strQry = " select count(*) from trans_mail_head a,Trans_Mail_Detail b where a.Trans_Sl_No=b.Trans_Sl_No and " +
            //         " month(a.mail_sent_time)=month(getdate())and year(a.mail_sent_time)=year(getdate()) " +
            //         " and  b.Mail_Active_Flag=0 and a.Mail_SF_To like '%admin%'";
            if (division_code != "")
            {
                strQry = " select count(*) from Trans_Mail_Detail b where b.Mail_Active_Flag=0 and Open_Mail_Id like '" + sf_code + '%' + "' and b.Division_code in(" + division_code + ")";

                try
                {
                    iReturn = db_ER.Exec_Scalar(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;
        }

        public DataSet FillSalesForce_Level(string des_code, string div_code, string HO_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "")
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                strQry = "select a.sf_code+'-Level1' as sf_mail, a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                         " a.Designation_Code,b.Desig_Color des_color,'' sf_Type,'' sf_color from Mas_Salesforce a,Mas_SF_Designation b " +
                         " where a.Designation_Code in (" + des_code + ")  and a.sf_TP_Active_Flag=0 and a.Designation_Code=b.Designation_Code  " +
                         " AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + div_code + ',' + "%') and Sf_Name !='admin'" +
                         " group by a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                         " a.Designation_Code,b.Desig_Color,sf_code " +
                         " union all" +
                         " select 'admin'+'-Level1' as sf_mail,[USER_NAME] as sf_name,'' as sf_desgn,'' as Sf_HQ,'' as Designation_Short_Name," +
                         " '' as sf_username,'' as Designation_Code,'' as des_color,'' sf_Type,'' sf_color from Mas_HO_ID_Creation where division_code like '%" + div_code + "%' " +
                         " order by b.Designation_Short_Name desc";
            }
            else
            {
                strQry = "select a.sf_code+'-Level1' as sf_mail, a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                        " a.Designation_Code,b.Desig_Color des_color,'' sf_Type,'' sf_color from Mas_Salesforce a,Mas_SF_Designation b " +
                        " where a.Designation_Code in (" + des_code + ")  and a.sf_TP_Active_Flag=0 and a.Designation_Code=b.Designation_Code  " +
                        " group by a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                        " a.Designation_Code,b.Desig_Color,sf_code " +
                        " union all " +
                        " select 'admin'+'-Level1' as sf_mail,[USER_NAME] as sf_name,'' as sf_desgn,'' as Sf_HQ,'' as Designation_Short_Name," +
                        " '' as sf_username,'' as Designation_Code,'' as des_color,'' sf_Type,'' sf_color from Mas_HO_ID_Creation " +
                        " order by b.Designation_Short_Name desc";
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
        //Changes done by Priya
        public DataSet FillLeave_Type(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "SELECT 0 as Leave_code,'--Select--' as Leave_SName,'' as Leave_Name " +
                       " UNION " +
                       " SELECT Leave_code,Leave_SName,Leave_Name " +
                       " FROM mas_Leave_Type where Active_Flag = 0 and Division_Code='" + div_code + "' ";



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
        public int Insert_Leave(string Leave_Type, DateTime From_Date, DateTime To_Date, string Reason, string Address, string No_of_Days, string Inform_by, string Valid_Reason, string sf_code, string Division_Code, string Informed_Ho, string Leave_Type_text, string sf_emp_id, string mgr_code, string mgr_emp_id, string Entry_Mode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Leave_Id)+1,'1') Leave_Id from mas_Leave_Form ";
                int Leave_Id = db.Exec_Scalar(strQry);

                string leave_From_Date = From_Date.Month.ToString() + "-" + From_Date.Day + "-" + From_Date.Year;
                string leave_To_Date = To_Date.Month.ToString() + "-" + To_Date.Day + "-" + To_Date.Year;

                strQry = " INSERT INTO mas_Leave_Form(Leave_Id,Leave_Type, From_Date,To_Date,Reason,Address,No_of_Days,Inform_by,Valid_Reason,Leave_Active_Flag,Created_Date,sf_code,Division_Code,Informed_Ho,Sf_Emp_Id,Mgr_Code,Mgr_Emp_Id,Entry_Mode) " +
                         " VALUES ('" + Leave_Id + "', '" + Leave_Type + "' , '" + leave_From_Date + "' , '" + leave_To_Date + "', '" + Reason + "' , '" + Address + "', " +
                         " '" + No_of_Days + "','" + Inform_by + "','" + Valid_Reason + "',2, getdate(),'" + sf_code + "','" + Division_Code + "','" + Informed_Ho + "','" + sf_emp_id + "','" + mgr_code + "','" + mgr_emp_id + "','" + Entry_Mode + "' ) ";

                iReturn = db.ExecQry(strQry);
                //strQry =   " update Trans_Leave_Entitle_Details set leave_balance_days=leave_balance_days - " + No_of_Days + " " +
                //           " from Trans_Leave_Entitle_Head h, Trans_Leave_Entitle_Details d  where sf_code='" + sf_code + "' and " +
                //           " h.Sl_no =d.Sl_NO and Trans_Year='2017'  and Leave_Type_Code='" + Leave_Type_text + "' ";

                //iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getLeave_approve(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select distinct a.Sf_Code,a.Sf_Name,a.Sf_HQ,a.sf_emp_id,a.sf_Designation_Short_Name as Designation_Short_Name,c.No_of_Days,convert(varchar(10),c.From_Date,103) From_Date,convert(varchar(10),c.To_Date,103) To_Date, c.Leave_Id " +
                     " from Mas_Salesforce a,  mas_Leave_Form c , Mas_Salesforce_AM d" +
                     " where d.Leave_AM = '" + sfcode + "' and a.Sf_Code = d.Sf_Code and  a.Sf_Code = c.Sf_Code  and c.Leave_Active_Flag = '" + iVal + "' and c.Division_code in('" + div_code + "')";
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

        public DataSet getLeave(string sf_code, string Leave_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select a.Leave_Id,a.Leave_Type, convert(varchar(12), a.From_Date,103) From_Date, convert(varchar(12),a.To_Date,103) To_Date,a.Reason,a.Address,a.No_of_Days,a.Inform_by,a.Valid_Reason,a.Informed_Ho " +
                     //  " b.Designation_Name,c.sf_name+' - '+b.Designation_Short_Name+' - '+ sf_HQ sf_name,c.sf_emp_id " +
                     " from  mas_Leave_Form a " +
                     " where a.sf_code = '" + sf_code + "' and a.Leave_Id = '" + Leave_Id + "' ";
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

        public DataSet GetStatus(string sf_code, string FromMonth, string FromYear, string ToMonth, string ToYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select a.Leave_Id,a.Leave_Type, Convert(varchar(12), a.From_Date,103) From_Date, Convert(varchar(12),a.To_Date,103) To_Date , a.Address, a.Reason,a.No_of_Days,c.Designation_Short_Name, " +
                     " a.Valid_Reason,b.sf_code, b.Sf_Name,b.sf_HQ,b.sf_emp_id,  " +
                     " (select sf_name from mas_salesforce where sf_code=b.Reporting_To_SF) as Reporting_To_SF," +
                     " CASE a.Leave_Active_Flag when '0' then 'Approved' when '2' then 'Pending' when '4' then 'Reject' end Leave_Active_Flag " +
                     " from mas_Leave_Form a, mas_salesforce b, Mas_SF_Designation c " +
                     " where a.sf_code = '" + sf_code + "' and b.Designation_Code=c.Designation_Code " +
                     " and a.sf_code = b.Sf_Code and " +
                     " (year(a.From_Date) >= '" + FromYear + "' and year(a.To_Date) <= '" + ToYear + "' ) " +
                     " and (MONTH(a.From_Date) >='" + FromMonth + "' and MONTH(a.To_Date) <= '" + ToMonth + "') ";

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

        public int Leave_Appprove(string sf_code, string Leave_Id, string No_of_Days, string Leave_Type_text, string app_mgr)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadmn;
            DateTime Leave_From = DateTime.Now;
            DateTime Leave_To = DateTime.Now;
            DCR dc = new DCR();
            int iReturn = -1;

            DataSet Trans_SlNo = new DataSet();

            strQry =

                       " update Trans_Leave_Entitle_Details set leave_balance_days=leave_balance_days - " + No_of_Days + " " +
                       " from Trans_Leave_Entitle_Head h, Trans_Leave_Entitle_Details d  where sf_code='" + sf_code + "' and " +
                       " h.Sl_no =d.Sl_NO and Trans_Year=YEAR(GETDATE())  and Leave_Type_Code='" + Leave_Type_text + "' and h.active_flag=0 ";

            iReturn = db_ER.ExecQry(strQry);

            strQry = " update mas_Leave_Form set Leave_Active_Flag = 0 ,LastUpdt_Date= getdate(),Leave_App_Mgr ='" + app_mgr + "'" +
                     "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2  and Leave_Id = '" + Leave_Id + "'";

            try
            {
                iReturn = db_ER.ExecQry(strQry);
                // Added  by Sridevi - To Update DCR - for Leave
                if (iReturn > 0)
                {
                    strQry = "select From_Date,TO_Date from mas_Leave_Form where sf_code = '" + sf_code + "' and Leave_Id = '" + Leave_Id + "'";

                    dsadmn = db_ER.Exec_DataSet(strQry);

                    if (dsadmn.Tables[0].Rows.Count > 0)
                    {
                        Leave_From = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        Leave_To = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                        while (Leave_From <= Leave_To)
                        {
                            DateTime dcrdate = Leave_From;
                            string Leave_Date = dcrdate.ToString("MM/dd/yyyy");
                            strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + Leave_Date + "' ";
                            Trans_SlNo = db_ER.Exec_DataSet(strQry);
                            if (Trans_SlNo.Tables[0].Rows.Count > 0)
                            {
                                strQry = "UPDATE DCRMain_Temp  SET Confirmed = 1 ,ReasonforRejection = '' " +
                                    " WHERE Sf_Code = '" + sf_code + "' and Trans_SlNo ='" + Trans_SlNo.Tables[0].Rows[0][0].ToString() + "'";

                                iReturn = db_ER.ExecQry(strQry);

                                int iretmain = dc.Create_DCRHead_Trans(sf_code, Trans_SlNo.Tables[0].Rows[0]["Trans_SlNo"].ToString());
                            }
                            Leave_From = Leave_From.AddDays(1);
                        }
                    }

                    int LeaveMiss = dc.LeaveAppMissed(Leave_Id);
                }


                // Code Changes Ends - To Update DCR - for Leave
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //public int Leave_Appprove(string sf_code, string Leave_Id)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    int iReturn = -1;

        //    strQry = " update mas_Leave_Form set Leave_Active_Flag = 0 ,LastUpdt_Date= getdate()" +
        //             "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2  and Leave_Id = '" + Leave_Id + "'";

        //    try
        //    {
        //        iReturn = db_ER.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}
        //Changes done by Priya

        public DataSet FillWorkArea()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry =
                       " SELECT WorkArea_Code,wrk_area_Name,wrk_area_SName " +
                       " FROM Mas_WorkArea_Type " +
                       " ORDER BY 2";


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

        //Changes done by saravanan
        //public int RecordUpdate_AdminSetUp_MGR(string strGMValue, string strSMValue, string strDMValue, string strSZValue, string strZSMValue,
        //                                       string strRSMValue, string strASMValue, string strDGMValue)
        //{
        //    int iReturn = -1;
        //    int Count = 0;

        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        //strQry = "UPDATE Admin_Setups " +
        //        //            " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "',No_of_TP_View='" + strNoofTPView + "'";

        //        //                bool value = sRecordExistMGR();

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strGMValue + "' where Designation_Short_Name='GM'";

        //        iReturn = db.ExecQry(strQry);

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strSMValue + "' where Designation_Short_Name='SM'";

        //        iReturn = db.ExecQry(strQry);

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strDMValue + "' where Designation_Short_Name='DM'";

        //        iReturn = db.ExecQry(strQry);

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strSZValue + "' where Designation_Short_Name='SZ'";

        //        iReturn = db.ExecQry(strQry);

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strZSMValue + "' where Designation_Short_Name='ZSM'";

        //        iReturn = db.ExecQry(strQry);

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strRSMValue + "' where Designation_Short_Name='RSM'";

        //        iReturn = db.ExecQry(strQry);

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strASMValue + "' where Designation_Short_Name='ASM'";

        //        iReturn = db.ExecQry(strQry);

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strDGMValue + "' where Designation_Short_Name='DGM'";

        //        iReturn = db.ExecQry(strQry);



        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return iReturn;

        //}
        public DataSet Get_Flash_News_adm(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,FN_Cont1,Division_Code,created_Date,FN_Active_Flag,FNHome_Page_Flag " +
                " from Mas_Flash_News where FN_Active_Flag=0 and Division_Code = '" + div_code + "'";

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
        //Added by sridevi
        public DataSet getMR_MGR_New(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select Sf_Code, sf_name, sf_Designation_Short_Name,sf_hq " +
            //            "  from Mas_Salesforce " +
            //            "  where sf_TP_Active_Flag=0 and sf_type=1 and TP_Reporting_SF = '" + sf_code + "' ";

            strQry = "EXEC sp_get_Rep_access  '" + sf_code + "', '" + div_code + "' ";
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
        public DataSet sp_get_MRWithVacant_access(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select Sf_Code, sf_name, sf_Designation_Short_Name,sf_hq " +
            //            "  from Mas_Salesforce " +
            //            "  where sf_TP_Active_Flag=0 and sf_type=1 and TP_Reporting_SF = '" + sf_code + "' ";

            strQry = "EXEC sp_get_MRWithVacant_access  '" + sf_code + "', '" + div_code + "' ";
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



        //Changes done by Saravanan
        //public int RecordUpdate_DesigMR(int StrId, string strDesignation)
        //{
        //    int iReturn = -1;

        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + StrId + "' where Designation_Short_Name='" + strDesignation + "'";

        //        iReturn = db.ExecQry(strQry);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return iReturn;

        //}
        //public DataSet getLeave_Reject(string sfcode, int iVal)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;

        //    strQry = " select distinct a.Sf_Code,a.Sf_Name,a.Sf_HQ,a.sf_emp_id,c.No_of_Days,convert(varchar(10),c.From_Date,103) From_Date,convert(varchar(10),c.To_Date,103) To_Date, c.Leave_Id,c.Reason, c.Approved_to, c.Leave_App_Mgr   " +
        //             " from Mas_Salesforce a, mas_Leave_Form c " +
        //             " where a.Sf_Code = '" + sfcode + "' and  a.Sf_Code = c.Sf_Code and c.Leave_Active_Flag = '" + iVal + "'";
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
        public DataSet getLeave_Reject(string sfcode, int iVal)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select distinct a.Sf_Code,a.Sf_Name,a.Sf_HQ,a.sf_emp_id,c.No_of_Days,convert(varchar(10),c.From_Date,103) From_Date,convert(varchar(10),c.To_Date,103) To_Date, c.Leave_Id,c.Reason, c.Approved_to, c.Leave_App_Mgr, c.Rejected_Reason  " +
                     " from Mas_Salesforce a, mas_Leave_Form c " +
                     " where a.Sf_Code = '" + sfcode + "' and  a.Sf_Code = c.Sf_Code and c.Leave_Active_Flag = '" + iVal + "'";
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
        public int Leave_Reject_Mgr(string sf_code, string sf_name, string Leave_Id, string reject)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadmn;
            DateTime Leave_From = DateTime.Now;
            DateTime Leave_To = DateTime.Now;
            DataSet Trans_SlNo = new DataSet();
            DCR_New dc = new DCR_New();
            int iReturn = -1;

            strQry = " update mas_Leave_Form set Leave_Active_Flag = 1 ,LastUpdt_Date= getdate(), Leave_App_Mgr ='" + sf_name + "', Rejected_Reason = '" + reject + "'" +
                     "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2 and Leave_Id='" + Leave_Id + "'";

            try
            {
                iReturn = db_ER.ExecQry(strQry);

                // Added  by Sridevi - To Update DCR - for Leave
                if (iReturn > 0)
                {
                    strQry = "select From_Date,TO_Date from mas_Leave_Form where sf_code = '" + sf_code + "' and Leave_Id = '" + Leave_Id + "'";

                    dsadmn = db_ER.Exec_DataSet(strQry);

                    if (dsadmn.Tables[0].Rows.Count > 0)
                    {
                        Leave_From = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        Leave_To = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());


                        while (Leave_From <= Leave_To)
                        {
                            DateTime dcrdate = Leave_From;
                            string Leave_Date = dcrdate.ToString("MM/dd/yyyy");
                            strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + Leave_Date + "' ";
                            Trans_SlNo = db_ER.Exec_DataSet(strQry);
                            if (Trans_SlNo.Tables[0].Rows.Count > 0)
                            {
                                int iretmain = dc.Reject_DCR(sf_code, Trans_SlNo.Tables[0].Rows[0]["Trans_SlNo"].ToString(), reject);
                            }

                            Leave_From = Leave_From.AddDays(1);
                        }
                    }
                }
                // Code Changes Ends - To Update DCR - for Leave
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //public int Leave_Reject_Mgr(string sf_code, string sf_name, string Leave_Id)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    int iReturn = -1;

        //    strQry = " update mas_Leave_Form set Leave_Active_Flag = 1 ,LastUpdt_Date= getdate(), Leave_App_Mgr ='" + sf_name + "'" +
        //             "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2 and Leave_Id='" + Leave_Id + "'";

        //    try
        //    {
        //        iReturn = db_ER.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}



        public int get_Mail_MR_MGR_Count(string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            //strQry = "select count(*) from trans_mail_head a,Trans_Mail_Detail b where a.Trans_Sl_No=b.Trans_Sl_No and " +
            //         " month(a.mail_sent_time)=month(getdate())and year(a.mail_sent_time)=year(getdate()) " +
            //         " and  b.Mail_Active_Flag=0 and a.Mail_SF_To like'%" + sf_Code + "%'";
            strQry = " select count(*) from Trans_Mail_Detail b where b.Mail_Active_Flag=0 and Open_Mail_Id like '%" + sf_Code + "%'";
            try
            {
                iReturn = db_ER.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Changes done by Priya
        public int AddQuery(string Querytype, string Query, string div_code, string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Query_Id)+1,'1') Query_Id from Mas_Query_Box ";
                int Query_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Query_Box(Query_Id,Query_Type,Query_Text,Created_Date,Division_Code,sf_code,Query_Active_Flag)" +
                         "values('" + Query_Id + "','" + Querytype + "', '" + Query + "',getdate(),'" + div_code + "','" + sf_code + "',0) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;
        }
        public DataSet getQuery_List(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select a.Query_Id, a.Query_Type, a.Query_Text, a.Division_Code, a.sf_code,b.sf_name,b.Sf_UserName,b.Sf_Password,c.Division_Name,a.created_date, a.Completed_Date, b.UsrDfd_UserName  " +
                      " from Mas_Query_Box a, mas_salesforce b, mas_division c " +
                      " where a.Division_Code = '" + div_code + "' and a.sf_code = b.sf_code and a.Division_Code = c.Division_Code and a.Query_Active_Flag = 0" +
                        "  order by Created_Date desc ";

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

        public DataSet getQuery_Reportingto(string sf_code, string Query_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select a.Query_Id, a.Query_Type, a.Query_Text, a.Division_Code, a.sf_code,b.sf_name,b.Sf_UserName,b.Sf_Password,c.Division_Name,a.created_date, b.UsrDfd_UserName  " +
                     " (select sf_name from mas_salesforce where sf_code=b.Reporting_To_SF) as Reporting_To_SF," +
                       " (select UsrDfd_UserName from mas_salesforce where sf_code=b.Reporting_To_SF) as Reporting_User," +
                         " (select Sf_Password from mas_salesforce where sf_code=b.Reporting_To_SF) as Reporting_Pass" +
                      " from Mas_Query_Box a, mas_salesforce b, mas_division c " +
                      " where a.sf_code = '" + sf_code + "' and a.Query_Id = '" + Query_Id + "' and a.sf_code = b.sf_code and a.Division_Code = c.Division_Code ";

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
        //done by reshmi
        public DataSet getMailFolderName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT m.Move_MailFolder_Id,m.Move_MailFolder_Name ," +
                     " (select count(d.Mail_Moved_to) from Trans_Mail_Detail d where  d.Mail_Moved_to = m.Move_MailFolder_Name and d.Division_code=m.Division_Code) as mail_count" +
                     " From Mas_Mail_Folder_Name m " +
                     "WHERE m.Division_Code='" + div_code + "' and Folder_Act_flag=0 " +
                     "ORDER BY Move_MailFolder_Id ";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public int MailUpdate(int MailFolder_Id, string MailFolder_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE Mas_Mail_Folder_Name " +
                          " SET Move_MailFolder_Name = '" + MailFolder_Name + "'" +
                          " WHERE Move_MailFolder_Id = '" + MailFolder_Id + "' and Division_Code='" + div_code + "' and Folder_Act_flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RecordAdd(string MailName, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Move_MailFolder_Id)+1,'1') Move_MailFolder_Id from Mas_Mail_Folder_Name ";
                int Move_MailFolder_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Mail_Folder_Name (Move_MailFolder_Id,Move_MailFolder_Name,Division_Code,Folder_Act_flag)" +
                        "values('" + Move_MailFolder_Id + "','" + MailName + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeleteMail_Id(int Mail_Id)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "DELETE FROM Mas_Mail_Folder_Name Where Move_MailFolder_Id='" + Mail_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        //Changes done by Priya

        public int Query_Com(int Query_Id, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Query_Box " +
                             " SET Query_Active_Flag=1 , " +
                             " LastUpdt_Date = getdate(), Completed_Date = getdate()" +
                             " WHERE Query_Id = '" + Query_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getQuery_List_com(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select a.Query_Id, a.Query_Type, a.Query_Text, a.Division_Code, a.sf_code,b.sf_name,b.Sf_UserName,b.Sf_Password,c.Division_Name,a.created_date,a.Completed_Date, b.UsrDfd_UserName   " +
                      " from Mas_Query_Box a, mas_salesforce b, mas_division c " +
                      " where a.Division_Code = '" + div_code + "' and a.sf_code = b.sf_code and a.Division_Code = c.Division_Code and a.Query_Active_Flag =1";

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

        public DataSet Get_talktous(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,TalktoUs_Text,Division_Code,Created_Date " +
                " from mas_Talk_to_Us where Talk_Active_Flag=0 and Division_Code='" + div_code + "' ";

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

        public int talkAdd(string talk_Text, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from mas_Talk_to_Us ";
                int Sl_No = db.Exec_Scalar(strQry);
                if (talk_RecordExist(div_code))
                {
                    strQry = "update mas_Talk_to_Us set TalktoUs_Text ='" + talk_Text + "' " +
                        " where Talk_Active_Flag=0 and Division_Code='" + div_code + "'";
                }
                else
                {

                    strQry = " INSERT INTO mas_Talk_to_Us(Sl_No,TalktoUs_Text,Division_Code,Created_Date,Talk_Active_Flag) " +
                             " VALUES ( '" + Sl_No + "','" + talk_Text + "' , '" + div_code + "', getdate(),0) ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool talk_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from mas_Talk_to_Us " +
                         " where Division_Code = '" + div_code + "' and Talk_Active_Flag=0 ";

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
        public DataSet getHomePage_Restrict(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " Select DCR_Home,TP_Home,Leave_Home, Expense_Home, Listeddr_Add_Home, Listeddr_Deact_Home, Listeddr_Add_Deact_Home, SS_Entry_Home, Doctor_Ser_Home " +
                     " from Mas_Home_Page_Restrict where Division_Code = '" + divcode + "'";

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

        public int Home_Restrict(int DCR_Home, int TP_Home, int Leave_Home, int Expense_Home, int Listeddr_Add_Home, int Listeddr_Deact_Home, int Listeddr_Add_Deact_Home, int SS_Entry_Home, int Doctor_Ser_Home, string divcode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                // int sl_no = -1;
                // DateTime deactDt = DateTime.Now.AddDays(-1);

                //strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Notice_Board";
                //sl_no = db.Exec_Scalar(strQry);
                if (HomePageRes_RecordExist(divcode))
                {
                    strQry = " update Mas_Home_Page_Restrict set DCR_Home ='" + DCR_Home + "', TP_Home = '" + TP_Home + "', Leave_Home = '" + Leave_Home + "', Expense_Home = '" + Expense_Home + "'," +
                             " Listeddr_Add_Home = '" + Listeddr_Add_Home + "', Listeddr_Deact_Home ='" + Listeddr_Deact_Home + "', Listeddr_Add_Deact_Home = '" + Listeddr_Add_Deact_Home + "', SS_Entry_Home = '" + SS_Entry_Home + "', Doctor_Ser_Home = '" + Doctor_Ser_Home + "'" +
                             " where  Division_Code='" + divcode + "'";
                }
                else
                {

                    strQry = " INSERT INTO Mas_Home_Page_Restrict(DCR_Home, TP_Home, Leave_Home, Expense_Home, Listeddr_Add_Home,Listeddr_Deact_Home, Listeddr_Add_Deact_Home,SS_Entry_Home, Doctor_Ser_Home,Division_Code, Created_Date) " +
                             " VALUES ( '" + DCR_Home + "' , '" + TP_Home + "', '" + Leave_Home + "', '" + Expense_Home + "', '" + Listeddr_Add_Home + "', '" + Listeddr_Deact_Home + "', '" + Listeddr_Add_Deact_Home + "','" + SS_Entry_Home + "','" + Doctor_Ser_Home + "', '" + divcode + "',  getdate()) ";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool HomePageRes_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Home_Page_Restrict " +
                         " where Division_Code = '" + div_code + "'  ";

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

        //done by resh for App
        //start
        public DataSet getFeedback(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT Feedback_Id,Feedback_Content From Mas_App_CallFeedback " +
                     "WHERE Division_Code='" + div_code + "' and Act_Flag=0 " +
                     " ORDER BY Feedback_Id ";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public int FeedbackAdd(string Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "INSERT INTO Mas_App_CallFeedback (Feedback_Content,Division_Code,Act_Flag)" +
                         "values('" + Name + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int FeedbackUpdate(int Feedback_Id, string Feedback_Content, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE Mas_App_CallFeedback " +
                          " SET Feedback_Content = '" + Feedback_Content + "'" +
                          " WHERE Feedback_Id = '" + Feedback_Id + "' and Division_Code='" + div_code + "' and Act_Flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int FeedbackDelete(string Feedback_Id)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Delete From Mas_App_CallFeedback " +
                          " WHERE Feedback_Id = '" + Feedback_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getRemarks(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT Remarks_Id,Remarks_Content From Mas_App_CallRemarks " +
                     "WHERE Division_Code='" + div_code + "' and Act_Flag=0 " +
                     " ORDER BY Remarks_Id ";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public int RemarksAdd(string Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "INSERT INTO Mas_App_CallRemarks (Remarks_Content,Division_Code,Act_Flag)" +
                         "values('" + Name + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RemarksUpdate(int Remarks_Id, string Remarks_Content, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE Mas_App_CallRemarks " +
                          " SET Remarks_Content = '" + Remarks_Content + "'" +
                          " WHERE Remarks_Id = '" + Remarks_Id + "' and Division_Code='" + div_code + "' and Act_Flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RemarksDelete(string Remarks_Id)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Delete From Mas_App_CallRemarks " +
                          " WHERE Remarks_Id = '" + Remarks_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int DeActMail_Id(int Mail_Id)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Mail_Folder_Name " +
                         " SET Folder_Act_flag=1 " +
                         " WHERE Move_MailFolder_Id = '" + Mail_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int MailAdd(string MailFolder_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Move_MailFolder_Id)+1,'1') Move_MailFolder_Id from Mas_Mail_Folder_Name ";
                int Move_MailFolder_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Mail_Folder_Name (Move_MailFolder_Id,Move_MailFolder_Name,Division_Code,Folder_Act_flag)" +
                         "values('" + Move_MailFolder_Id + "','" + MailFolder_Name + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //start for mail remaining
        public DataSet getMail_TransFrom(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "SELECT 0 as Move_MailFolder_Id,'--Select--' as Move_MailFolder_Name " +
                     " UNION " +
                     " SELECT Move_MailFolder_Id,Move_MailFolder_Name FROM  Mas_Mail_Folder_Name " +
                     " WHERE Folder_Act_flag=0 AND Division_Code= '" + divcode + "' " +
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
        public DataSet getMail_TransTo(string divcode, string MailFolder_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Move_MailFolder_Id,'--Select--' as Move_MailFolder_Name " +
                     " UNION " +
                     " SELECT Move_MailFolder_Id,Move_MailFolder_Name FROM  Mas_Mail_Folder_Name " +
                     " WHERE Folder_Act_flag=0 AND Division_Code=  '" + divcode + "' and Move_MailFolder_Name!='" + MailFolder_Name + "'  " +
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
        public DataSet getMailcount(string Move_MailFolder_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(d.Mail_Moved_to) as Move_MailFolder_Id from Trans_Mail_Detail d,Mas_Mail_Folder_Name m " +
                      "where d.Mail_Moved_to = m.Move_MailFolder_Name and d.Division_code=m.Division_Code  and m.Move_MailFolder_Id = '" + Move_MailFolder_Id + "' and m.Folder_Act_flag=0";
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
        public int Updatmail(string Trans_From, string Trans_To, string Trans_FromName, string Trans_ToName, string chkdel, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Trans_Mail_Detail set Mail_Moved_to= '" + Trans_ToName + "' where Mail_Moved_to= '" + Trans_FromName + "' and Division_Code='" + div_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Mail_Folder_Name " +
                       " SET Folder_Act_flag = '" + chkdel + "' " +
                       " WHERE Move_MailFolder_Id = '" + Trans_From + "' and Folder_Act_flag=0 and Division_Code='" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet Get_Block_Reason(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sf_blkreason " +
                " from Mas_Salesforce where SF_Status=1 and Division_Code = '" + div_code + "' and sf_code = '" + sf_code + "'";

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

        //Done by Reshmi-- start
        public DataSet getWorkTye(string div_code, int ddltype)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            if (ddltype == 1)
            {

                strQry = "Select WorkType_Code_B,Worktype_Name_B,WType_SName,WorkType_Orderly,TP_Flag,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Designation_Code" +
                          " From Mas_WorkType_BaseLevel where Division_Code='" + div_code + "' and active_flag=0 " +
                          "ORDER BY WorkType_Orderly ";
            }
            else if (ddltype == 2)
            {
                strQry = "Select WorkType_Code_M WorkType_Code_B,Worktype_Name_M Worktype_Name_B,WType_SName,WorkType_Orderly,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Designation_Code" +
                        " FROM Mas_WorkType_Mgr where Division_Code='" + div_code + "' and active_flag=0 " +
                        "ORDER BY WorkType_Orderly ";
            }

            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadm;
        }

        public int Wrk_RecordUpdate(int WorkType_Code, string WType_SName, string WorkType_Orderly, string TP_Flag, string TP_DCR, string Place_Involved, string Button_Access, string FieldWork_Indicator, string divcode, int ddltype, string Designation_short_name, string Designation_Code)
        {
            int iReturn = -1;
            //if (!RecordExistWork_Slno(WorkType_Code, WorkType_Orderly, divcode,ddltype))
            //{
            if (!RecordExistWrk_S(WorkType_Code, WType_SName, divcode, ddltype))
            {

                try
                {
                    if (ddltype == 1)
                    {


                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_WorkType_BaseLevel " +
                                 " SET WType_SName = '" + WType_SName + "' ," +
                                 " WorkType_Orderly = '" + WorkType_Orderly + "' ," +
                                 " TP_Flag = '" + TP_Flag + "' ," +
                                 " TP_DCR = '" + TP_DCR + "' ," +
                                 " Place_Involved = '" + Place_Involved + "' ," +
                                 " Button_Access = '" + Button_Access + "' ," +
                                 " FieldWork_Indicator = '" + FieldWork_Indicator + "' ," +
                                 " Designation_Short_Name = '" + Designation_short_name + "' ," +
                                 " Designation_Code ='" + Designation_Code + "' " +
                                 " WHERE WorkType_Code_B = '" + WorkType_Code + "' and Division_Code='" + divcode + "' and active_flag = 0 ";

                        iReturn = db.ExecQry(strQry);
                    }
                    else if (ddltype == 2)
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_WorkType_Mgr " +
                                 " SET WType_SName ='" + WType_SName + "' ," +
                                 " WorkType_Orderly = '" + WorkType_Orderly + "', " +
                                 //" TP_Flag = '" + TP_Flag + "' ," +
                                 " TP_DCR = '" + TP_DCR + "' ," +
                                 " Place_Involved = '" + Place_Involved + "' ," +
                                 " Button_Access = '" + Button_Access + "' ," +
                                 " FieldWork_Indicator = '" + FieldWork_Indicator + "' ," +
                                 " Designation_Short_Name = '" + Designation_short_name + "' ," +
                                 " Designation_Code ='" + Designation_Code + "' " +
                                 " WHERE WorkType_Code_M = '" + WorkType_Code + "' and Division_Code ='" + divcode + "' and active_flag = 0";

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
            //}
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }

        public bool RecordExistWrk_S(int WorkType_Code, string WType_SName, string divcode, int ddltype)
        {

            bool bRecordExist = false;
            try
            {
                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WType_SName) FROM Mas_WorkType_BaseLevel WHERE WType_SName = '" + WType_SName + "' AND WorkType_Code_B!='" + WorkType_Code + "' AND Division_Code= '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WType_SName) FROM Mas_WorkType_Mgr WHERE WType_SName ='" + WType_SName + "' AND WorkType_Code_M !='" + WorkType_Code + "' AND Division_Code = '" + divcode + "' ";

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

        public bool RecordExistWork_Slno(int WorkType_Code, string WorkType_Orderly, string divcode, int ddltype)
        {
            bool bRecordExist = false;

            try
            {
                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WorkType_Orderly) FROM Mas_WorkType_BaseLevel Where WorkType_Orderly ='" + WorkType_Orderly + "'AND WorkType_Code_B!='" + WorkType_Code + "' AND Division_Code= '" + divcode + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WorkType_Orderly) FROM Mas_WorkType_Mgr where WorkType_Orderly ='" + WorkType_Orderly + "' AND WorkType_Code_M !='" + WorkType_Code + "' AND Division_Code ='" + divcode + "' ";
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

        public DataSet getTp_Flag(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            strQry = "Select distinct TP_Flag from Mas_WorkType_BaseLevel where Division_Code= '" + div_code + "'";
            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadm;
        }
        public DataSet get_WorkType_Code(string WorkType_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            strQry = "Select Worktype_Name_B,WType_SName ,WorkType_Orderly,TP_Flag,TP_DCR ,Place_Involved,Button_Access,FieldWork_Indicator,WorkType_Code_B " +
                     "From Mas_WorkType_BaseLevel WHERE active_flag =0 and WorkType_Code_B ='" + WorkType_Code + "' " +
                     "ORDER BY 2";

            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return dsadm;
        }
        public DataSet getFieldWork_Indicator(string div_code, int ddltype)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            if (ddltype == 1)
            {

                strQry = "Select distinct FieldWork_Indicator from Mas_WorkType_BaseLevel where Division_Code='" + div_code + "'";
            }
            else if (ddltype == 2)
            {
                strQry = "Select distinct FieldWork_Indicator from Mas_WorkType_Mgr where Division_Code='" + div_code + "'";
            }
            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadm;
        }

        public DataSet getWrkTP_Indicator(string WorkType_Code, string divcode, int ddltype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            if (ddltype == 1)
            {

                strQry = " SELECT TP_Flag,TP_DCR,Place_Involved,FieldWork_Indicator,Button_Access,Designation_Short_Name FROM  Mas_WorkType_BaseLevel " +
                         " WHERE WorkType_Code_B='" + WorkType_Code + "' AND Division_Code= '" + divcode + "' ";
            }
            else if (ddltype == 2)
            {

                strQry = " SELECT '',TP_DCR,Place_Involved,FieldWork_Indicator,Button_Access,Designation_Short_Name FROM  Mas_WorkType_Mgr " +
                         " WHERE WorkType_Code_M ='" + WorkType_Code + "' AND Division_Code= '" + divcode + "' ";
            }
            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public int Addwrktype(string name, string short_name, string order, string TpFlag, string TpDcr, string place_involved, string butt, string Indicator, string div_code, int ddltype, string designation_code, string desig_short_name)
        {

            int iReturn = -1;


            if (!RecordExistWrk_Name(name, div_code, ddltype))
            {

                if (!RecordExistWrk_Short(short_name, div_code, ddltype))
                {
                    try
                    {
                        //if (div_code.Contains(','))
                        //{ 
                        //    div_code = div_code.Remove(div_code.Length - 1);
                        //}


                        if (ddltype == 1)
                        {

                            DB_EReporting db = new DB_EReporting();

                            strQry = "SELECT isnull(max(WorkType_Code_B)+1,'1') WorkType_Code_B from Mas_WorkType_BaseLevel ";
                            int WorkType_Code_B = db.Exec_Scalar(strQry);

                            strQry = "INSERT INTO Mas_WorkType_BaseLevel (WorkType_Code_B,Worktype_Name_B,WType_SName,WorkType_Orderly,TP_Flag,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Division_Code,active_flag,Designation_Code,Designation_Short_Name,ExpNeed)" +
                                     "values('" + WorkType_Code_B + "','" + name + "' ,'" + short_name + "','" + order + "','" + TpFlag + "','" + TpDcr + "','" + place_involved + "','" + butt + "','" + Indicator + "', '" + div_code + "',0,'" + designation_code + "','" + desig_short_name + "',1)";


                            iReturn = db.ExecQry(strQry);
                        }
                        else if (ddltype == 2)
                        {
                            DB_EReporting db = new DB_EReporting();

                            strQry = "SELECT isnull(max(WorkType_Code_M)+1,'1') WorkType_Code_M from Mas_WorkType_Mgr ";
                            int WorkType_Code_M = db.Exec_Scalar(strQry);

                            strQry = "INSERT INTO Mas_WorkType_Mgr (WorkType_Code_M,WorkType_Name_M,WType_SName,WorkType_Orderly,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Division_Code,active_flag,Designation_Code,Designation_Short_Name,ExpNeed)" +
                                     "values('" + WorkType_Code_M + "','" + name + "' ,'" + short_name + "','" + order + "','" + TpDcr + "','" + place_involved + "','" + butt + "','" + Indicator + "', '" + div_code + "',0,'" + designation_code + "','" + desig_short_name + "',1)";


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

        public bool RecordExistWrk_Short(string short_name, string divcode, int ddltype)
        {

            bool bRecordExist = false;
            try
            {
                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WType_SName) FROM Mas_WorkType_BaseLevel WHERE WType_SName = '" + short_name + "' AND Division_Code= '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WType_SName) FROM Mas_WorkType_Mgr WHERE WType_SName ='" + short_name + "' AND Division_Code = '" + divcode + "' ";

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

        public bool RecordExistWrk_Name(string name, string divcode, int ddltype)
        {

            bool bRecordExist = false;
            try
            {
                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(Worktype_Name_B) FROM Mas_WorkType_BaseLevel WHERE Worktype_Name_B = '" + name + "' AND Division_Code= '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(Worktype_Name_M) FROM Mas_WorkType_Mgr WHERE Worktype_Name_M ='" + name + "' AND Division_Code = '" + divcode + "' ";

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

        public int DeactivateWorktype(string worktype_Code, int ddltype)
        {
            int iReturn = -1;

            try
            {

                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_WorkType_BaseLevel " +
                                " SET active_flag=1  " +
                                " WHERE WorkType_Code_B = '" + worktype_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_WorkType_Mgr " +
                               " SET active_flag=1  " +
                               " WHERE WorkType_Code_M = '" + worktype_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Get_UserManual(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm " +
            //            "  from file_info " +
            //            "  where div_Code = '" + div_code + "' ";
            strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                     "  from usermanual " +
                     "  where div_Code = '" + div_code + "'";

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
        //end

        public DataSet GetStatus_MGR(string div_code, string sf_code, string FromMonth, string FromYear, string ToMonth, string ToYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " Exec Leave_Status '" + div_code + "','" + sf_code + "', '" + FromMonth + "', '" + FromYear + "', '" + ToMonth + "', '" + ToYear + "' ";

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

        public DataSet get_DCR_Setups_Entry(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select typ,Msl,Chm,Stk,Unl,Hos,desig from DCRSetups where Division_Code='" + divcode + "'";



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

        public DataSet getDCRSetup_Desig(string typ, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " SELECT desig,desig_v FROM  DCRSetups " +
                         " WHERE typ='" + typ + "' AND Division_Code= '" + divcode + "' ";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public DataSet get_DCR_Setup_Display(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select typ,Msl_v,Chm_v,Stk_v,Unl_v,Hos_v,desig_v from DCRSetups where Division_Code='" + divcode + "'";



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

        public int RecordUpdate_EntryDCR_Setups(int strlistdr, int strchem, int strstk, int strunlist, int strhos, string type, string div_code, string Designation_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update DCRSetups set Msl='" + strlistdr + "' ," +
                         " Chm ='" + strchem + "' ," +
                         " Stk ='" + strstk + "' ," +
                         " Unl ='" + strunlist + "'," +
                         " Hos ='" + strhos + "' ," +
                         " desig ='" + Designation_Code + "' " +
                         " where typ='" + type + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate_DisplayDCR_Setups(int strlistdr_v, int strchem_v, int strstk_v, int strunlist_v, int strhos_v, string type_v, string div_code, string Designation_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update DCRSetups set Msl_v='" + strlistdr_v + "' ," +
                         " Chm_v ='" + strchem_v + "' ," +
                         " Stk_v ='" + strstk_v + "' ," +
                         " Unl_v ='" + strunlist_v + "'," +
                         " Hos_v ='" + strhos_v + "', " +
                         " desig_v ='" + Designation_Code + "' " +
                         " where typ='" + type_v + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getDCR_EntryMode(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsadmin = null;

            strQry = " select typ,Msl,Chm,Stk,Unl,Hos,desig from DCRSetups where Division_Code='" + divcode + "' ";
            try
            {
                dsadmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadmin;
        }

        public DataSet getDCR_DisplayMode(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsadmin = null;

            strQry = " select typ,Msl_v,Chm_v,Stk_v,Unl_v,Hos_v,desig from DCRSetups where Division_Code='" + divcode + "' ";
            try
            {
                dsadmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadmin;
        }

        public DataSet getprd_feedback(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select FeedBack_Id,FeedBack_Name from Mas_Product_Feedback where Division_Code='" + div_code + "' ";


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

        public DataSet GetStatus_MGR_Self(string div_code, string sf_code, string FromMonth, string FromYear, string ToMonth, string ToYear, string chk_self)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (chk_self == "0")
            {

                strQry = " Exec Leave_Status_Self '" + div_code + "','" + sf_code + "', '" + FromMonth + "', '" + FromYear + "', '" + ToMonth + "', '" + ToYear + "' ";
            }
            else
            {
                strQry = "select a.Leave_Id,a.Leave_Type, Convert(varchar(12), a.From_Date,103) From_Date, " +
                        " Convert(varchar(12),a.To_Date,103) To_Date , a.Address, a.Reason,a.No_of_Days,c.Designation_Short_Name, " +
                        " a.Valid_Reason,b.sf_code, b.Sf_Name,b.sf_HQ,b.sf_emp_id,  " +
                        " Leave_App_Mgr as Reporting_To_SF, " +
                        " CASE a.Leave_Active_Flag when '0' then 'Approved' when '2' then 'Pending' when '1' then " +
                        " 'Reject' end Leave_Active_Flag, '' status, " +
                        " (select Leave_SName from mas_Leave_Type l where a.Leave_Type=l.Leave_code ) Type_SName,Convert(varchar(12), a.Created_Date,103) Created_Date,Convert(varchar(12), a.LastUpdt_Date,103) LastUpdt_Date  " +
                        " from mas_Leave_Form a, mas_salesforce b, Mas_SF_Designation c " +
                        " where a.sf_code = '" + sf_code + "' and b.Designation_Code=c.Designation_Code " +
                        " and a.sf_code = b.Sf_Code  and a.division_code ='" + div_code + "' and " +
                        " (year(a.From_Date) >  '" + FromYear + "'  OR (year(a.From_Date) = '" + FromYear + "' AND " +
                        " month(a.From_Date) >= '" + FromMonth + "')) " +
                        " AND (year(a.To_Date) <  '" + ToYear + "'  OR (year(a.To_Date) =  '" + ToYear + "' " +
                        " AND month(a.To_Date) <= '" + ToMonth + "' )) order by a.From_Date desc  ";
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

        
        public int RecordUpdate_MobApp(int GeoChk, int GEOTagNeed,
             float DisRad, string DrCap,
             string ChmCap, string StkCap,
             string NLCap, int DPNeed,

             int DINeed, int ChmNeed,
             int CPNeed, string ChmQCap,
             int CINeed, int StkNeed,
             int SPNeed, string StkQCap,
             int SINeed, int UNLNeed,
             int NPNeed, string NLRxQCap,
             string NLSmpQCap, int NINeed,
             string Division_Code, int DeviceId_Need,

             int Radio, int Radio2,
             int Radio3, int Radio4,
             int Radio5, int Radio6,
             int Radio7, int Radio8,
             int Radio9, int Radio10,
             string txtrxqty, string txtsampqty,
             int Radio11, int Radio14,
             int Radio15, int Radio16,


             int Radio31, int Radio32,
             string txtRx_Cap_doc, string txtSamQty_Cap_doc,
             int Radio33, int Radio34,
             int Radio35,

             int Radio37, int Radio38,
             int Radio39, int Radio40,
             int Radio41, int Radio42,
             int Radio43, int Radio44,

             int Radio46,

              string txtDpc, string txtCPC,
              string txtSPC, string txtUPC,

              int Radio85, int Radio86,
              int Radio87, int Radio88,

              int Radio81, int Radio82,
              int Radio83, int Radio84,

              int Radio75, int Radio76,
              int Radio77, int Radio78,
              int Radio79, int Radio80,

              int Radio47, int Radio48,
              int Radio49,
               int Radio50, int Radio51,
              int Radio52, int Radio53,
              int Radio54, int Radio55, int Radio56,
               int Radio57, int Radio58,
               int Radio59, int Radio60,
               int Radio61, int Radio62,
               int Radio63, int Radio64,
               int Radio65, int Radio66,
               int Radio67, int Radio68,
               int Radio69, int Radio70,
               int Radio71, int Radio72,
               int Radio73,
                 int Radio90, int Radio91,
             int Radio17, int Radio89, int Radio92,
             int RadioBtnList1, int RadioBtnList2, int RadioBtnList3
             )

        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Access_Master " +
                               " SET GeoChk= '" + GeoChk + "',GEOTagNeed='" + GEOTagNeed + "', "

                                + " DisRad = '" + DisRad + "',"
                                + " DrCap ='" + DrCap + "' ,"
                                + " ChmCap ='" + ChmCap + "', "
                                + " StkCap ='" + StkCap + "', "
                                + " NLCap ='" + NLCap + "', "
                                + " DPNeed ='" + DPNeed + "', "
                                + " DINeed = '" + DINeed + "', "
                                + " ChmNeed = '" + ChmNeed + "', "
                                + " CPNeed = '" + CPNeed + "', "
                                + " ChmQCap = '" + ChmQCap + "', "
                                + " CINeed = '" + CINeed + "', "
                                + " StkNeed = '" + StkNeed + "', "
                                + " SPNeed = '" + SPNeed + "', "
                                + " StkQCap = '" + StkQCap + "', "
                                + " SINeed='" + SINeed + "', "
                                + " UNLNeed ='" + UNLNeed + "', "
                                + " NPNeed ='" + NPNeed + "', "
                                + " NLRxQCap ='" + NLRxQCap + "', "
                                + " NLSmpQCap ='" + NLSmpQCap + "', "
                                + " NINeed =' " + NINeed + "' ,"
                                + " DeviceId_Need ='" + DeviceId_Need + "' ,"



                                + "MsdEntry='" + Radio2 + "',"

                                + "VstNd='" + Radio3 + "',"

                                + "mailneed='" + Radio4 + "',"

                                + "circular='" + Radio5 + "',"

                                + "FeedNd='" + Radio6 + "',"

                                + "DrPrdMd='" + Radio7 + "',"

                                + "DrInpMd='" + Radio8 + "',"

                                + "DrSmpQMd='" + Radio9 + "',"

                                + "DrRxQMd='" + Radio10 + "',"

                                + "DrRxQCap='" + txtrxqty + "',"

                                + "DrSmpQCap='" + txtsampqty + "', "

                               + "Doc_Pob_Mandatory_Need='" + Radio11 + "',"


                                + "Attendance='" + Radio14 + "',"

                                + "MCLDet='" + Radio15 + "',"
                                + "Chm_Pob_Mandatory_Need='" + Radio16 + "',"

                                + "DrRxNd='" + Radio17 + "',"
                                + "RcpaNd ='" + Radio + "',"
                                + "TPDCR_Deviation='" + Radio31 + "',"
                                + "TP_Mandatory_Need='" + Radio32 + "',"
                                + "Tp_Start_Date='" + txtRx_Cap_doc + "',"
                                + "Tp_End_Date='" + txtSamQty_Cap_doc + "',"
                                + "dayplan_tp_based='" + Radio33 + "',"
                                + "NextVst='" + Radio34 + "',"
                                + "NextVst_Mandatory_Need='" + Radio35 + "',"

                                + "RCPAQty_Need='" + Radio37 + "',"
                                + "multiple_doc_need='" + Radio38 + "',"
                                + "Cluster_Cap='" + Radio39 + "',"
                                + "allProdBd='" + Radio40 + "',"
                                + "Speciality_prod='" + Radio41 + "',"
                                + "FcsNd='" + Radio42 + "',"
                                + "Prod_Stk_Need='" + Radio43 + "',"
                                + "OtherNd='" + Radio44 + "',"
                                + "Sep_RcpaNd='" + Radio46 + "',"
                                + "Ul_Product_caption='" + txtUPC + "',"
                                + "Stk_Product_caption='" + txtSPC + "',"
                                + "Chm_Product_caption='" + txtCPC + "',"
                                + "Doc_Product_caption='" + txtDpc + "' ,"
                                + "doctor_dobdow='" + Radio47 + "',"
                                + "Appr_Mandatory_Need='" + Radio48 + "' ,"
                               + "DlyCtrl='" + Radio49 + "' ,"
                                + "cip_need='" + Radio50 + "' ,"
                                + "DFNeed='" + Radio51 + "' ,"
                                + "CFNeed='" + Radio52 + "',"
                                + "SFNeed='" + Radio53 + "' ,"
                                + "CIP_FNeed='" + Radio54 + "' ,"
                                + "NFNeed='" + Radio55 + "' ,"
                                + "HFNeed='" + Radio56 + "' ,"
                                + "DQNeed='" + Radio57 + "' ,"
                                + "CQNeed='" + Radio58 + "',"
                                + "SQNeed='" + Radio59 + "' ,"
                                + "NQNeed='" + Radio60 + "',"
                                + "CIP_QNeed='" + Radio61 + "',"
                                + "HQNeed='" + Radio62 + "',"
                                + "DENeed='" + Radio63 + "' ,"
                                + "CENeed='" + Radio64 + "',"
                                + "SENeed='" + Radio65 + "' ,"
                                + "NENeed='" + Radio66 + "',"
                                + "CIP_ENeed='" + Radio67 + "' ,"
                                + "HENeed='" + Radio68 + "',"
                                + "quiz_need='" + Radio69 + "' ,"
                                + "pro_det_need='" + Radio70 + "',"
                                + "Prodfd_Need='" + Radio71 + "' ,"
                                + "mediaTrans_Need='" + Radio72 + "',"
                                + "SpecFilter='" + Radio73 + "',"
                                + "Doc_Pob_Need='" + Radio75 + "' ,"
                                + "Chm_Pob_Need='" + Radio76 + "' ,"
                                + "Stk_Pob_Need='" + Radio77 + "' ,"
                                + "Ul_Pob_Need='" + Radio78 + "' ,"
                                + "Stk_Pob_Mandatory_Need='" + Radio79 + "' ,"
                                + "Ul_Pob_Mandatory_Need='" + Radio80 + "',"
                                + "Doc_jointwork_Need='" + Radio81 + "' ,"
                                + "Chm_jointwork_Need='" + Radio82 + "' ,"
                                + "Stk_jointwork_Need='" + Radio83 + "' ,"
                                + "Ul_jointwork_Need='" + Radio84 + "',"
                                + "Doc_jointwork_Mandatory_Need='" + Radio85 + "',"
                                + "Chm_jointwork_Mandatory_Need='" + Radio86 + "',"
                                + "Stk_jointwork_Mandatory_Need='" + Radio87 + "',"
                                + "Ul_jointwork_Mandatory_Need='" + Radio88 + "' ,"
                                + "CIP_PNeed='" + Radio90 + "',"
                                + "CIP_INeed='" + Radio91 + "',"
                                + "DrFeedMd='" + Radio89 + "',"
                                + "TPDCR_MGRAppr='" + Radio92 + "', MissedDateMand='" + RadioBtnList1 + "', RmdrNeed='" + RadioBtnList2 + "', "
                                + "TempNd='" + RadioBtnList3 + "' where Division_Code = '" + Division_Code + "' ";
                iReturn = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;


        }

        public DataSet get_Managers(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            //strQry = "EXEC AllFieldforce_Novacant_wtRptmgr  '" + sf_code + "', '" + div_code + "' ";
            // strQry = "EXEC AllFieldforce_Novacant_withhold  '" + sf_code + "', '" + div_code + "' ";
            //strQry = "EXEC ScreenCheck_Test1  '" + sf_code + "', '" + div_code + "' ";
            strQry = "EXEC ScreenCheck_New  '" + sf_code + "', '" + div_code + "' ";
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
        public int RecordUpdate_geosf_code(string sf_code, int GeoNeed, int GeoFencing, int Fencingche, int Fencingstock, int DigitalOffline)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Access_Table set GeoNeed='" + GeoNeed + "' ,GeoFencing='" + GeoFencing + "',GeoFencingche='" + Fencingche + "',GeoFencingstock='" + Fencingstock + "',Digital_offline='" + DigitalOffline + "' " +
                         " where sf_code='" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int RecordUpdate_geosf_code(string sf_code, int GeoNeed, int GeoFencing, int Fencingche, int Fencingstock)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Access_Table set GeoNeed='" + GeoNeed + "' ,GeoFencing='" + GeoFencing + "',GeoFencingche='" + Fencingche + "',GeoFencingstock='" + Fencingstock + "' " +
                         " where sf_code='" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Get_UserManual_MR(string div_code, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType,Designation_Code,Designation_Short_Name " +
                     "  from File_Info " +
                     "  where div_Code = '" + div_code + "' and Designation_Code like '%" + Designation_Code + "%'";

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

        public int RecordUpdate_baselevel_tp(string strDesignation, int tp_start_date, int tp_end_date, string div_code)
        {
            int iReturn = -1;

            //string start_date = tp_start_date.Month + "-" + tp_start_date.Day + "-" + tp_start_date.Date.Year;
            //string end_date = tp_end_date.Month + "-" + tp_end_date.Day + "-" + tp_end_date.Date.Year;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_SF_Designation set Tp_Start_Date='" + tp_start_date + "', Tp_End_Date='" + tp_end_date + "' where Designation_Code='" + strDesignation + "' and Division_Code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate_LockSystemMGR(string div_code, int lock_sysyem, string Doc_MulPlan)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Admin_Setups_MGR set LockSystem_Needed='" + lock_sysyem + "', " +
                         " SingleDr_WithMultiplePlan_Required ='" + Doc_MulPlan + "' " +
                         " where Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate_LockSystemMR(string div_code, int lock_sysyem)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Admin_Setups set LockSystem_Needed='" + lock_sysyem + "' " +
                         " where Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getLockSystem_AdmMR(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select LockSystem_Needed from Admin_Setups where Division_Code='" + div_code + "' ";

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

        public DataSet getLockSystem_AdmMGR(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select LockSystem_Needed from Admin_Setups_MGR where Division_Code='" + div_code + "' ";

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

        public DataSet chk_tpbasedsystem_MR(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select TpBased from Admin_Setups where Division_Code='" + divcode + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public DataSet chkRange_tpbased(string divcode, int Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select Tp_Start_Date,Tp_End_Date from Mas_SF_Designation where Division_Code='" + divcode + "' and Designation_Code='" + Designation_Code + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public DataSet chk_tpbasedsystem_MGR(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select TpBased from Admin_Setups_MGR where Division_Code='" + divcode + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public DataSet getMR_Vacant(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_get_Rep_access_Vacant  '" + sf_code + "', '" + div_code + "' ";
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

        public DataSet getSetup_forTargetFix(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select Target_Yearbasedon,Targer_Cal_Based,Drbus_Cal_Based,Leave_Allowed,Delayed_Caption,SS_From_Range,SS_To_Range,Delayed_ShortName,Fieldforce_Resig,Fieldforce_Resig_Caption,Crm_Mgr,Crm_Approval,Hosbus_Cal_Based,SS_Lock_Mgr,SI_EM_Month,SI_EM_Year,IN_EM_Month,IN_EM_Year,ListDr_Chembus_Cal_Based,SS_Lock_Day,LE_MGR,LE_MR,DCR_Approval_Remarks, Stockist_Primary_Sale_Based_On,sample_Ack,Input_Ack,Mail_system from Setup_Others where Division_Code='" + divcode + "' ";

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


        public DataSet getSetup_BindYear(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select max([SI_EM_Year]-1) as Year from Setup_Others where Division_Code='" + divcode + "' ";

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
        public DataSet getSetup_Bind_InputYear(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select max([IN_EM_Year]-1) as Year from Setup_Others where Division_Code='" + divcode + "' ";

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


        public int TargetFix_Setup(string div_code, string Target_Yearbasedon, string Targer_Cal_Based, string Drbus_Cal_Based, string Leave_Allowed, string Delayed_Caption, string SS_From_Range, string SS_To_Range,
            string Delayed_ShortName, int Fieldforce_Resig, string Fieldforce_Resig_Caption, string Crm_Mgr, string Crm_Approval, string Hosbus_Cal_Based, string SS_Lock_Mgr,
            string SI_EM_Month, string SI_EM_Year, string IN_EM_Month, string IN_EM_Year, string ListDr_Chembus_Cal_Based, int SS_Lock_Day, string LE_MGR, string LE_MR, string DCR_Approval_Remarks, string sample, string input, string strmail)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                bool count = sRecordExist_Target(div_code);

                if (count == false)
                {
                    strQry = "Insert into Setup_Others(Division_Code,Target_Yearbasedon,Targer_Cal_Based,Drbus_Cal_Based,Leave_Allowed,Delayed_Caption,SS_From_Range,SS_To_Range,Delayed_ShortName,Fieldforce_Resig," +
                    "Fieldforce_Resig_Caption,Crm_Mgr,Crm_Approval,Hosbus_Cal_Based,SS_Lock_Mgr,SI_EM_Month,SI_EM_Year,IN_EM_Month,IN_EM_Year,ListDr_Chembus_Cal_Based,SS_Lock_Day,LE_MGR,LE_MR,DCR_Approval_Remarks,sample_Ack,Input_Ack,Mail_System) " +
                             " values ('" + div_code + "', '" + Target_Yearbasedon + "','" + Targer_Cal_Based + "','" + Drbus_Cal_Based + "','" + Leave_Allowed + "','" + Delayed_Caption + "','" + SS_From_Range + "','" + SS_To_Range + "','" + Delayed_ShortName + "','" + Fieldforce_Resig + "'," +
                             "'" + Fieldforce_Resig_Caption + "','" + Crm_Mgr + "','" + Crm_Approval + "','" + Hosbus_Cal_Based + "','" + SS_Lock_Mgr + "','" + SI_EM_Month + "','" + SI_EM_Year + "','" + IN_EM_Month + "','" + IN_EM_Year + "','" + ListDr_Chembus_Cal_Based + "','" + SS_Lock_Day + "','" + LE_MGR + "','" + LE_MR + "','" + DCR_Approval_Remarks + "','" + sample + "','" + input + "','" + strmail + "') ";
                }

                else
                {
                    strQry = "Update Setup_Others set Target_Yearbasedon='" + Target_Yearbasedon + "', " +
                             " Targer_Cal_Based ='" + Targer_Cal_Based + "', " +
                             " Drbus_Cal_Based ='" + Drbus_Cal_Based + "', " +
                             " Leave_Allowed ='" + Leave_Allowed + "', " +
                             " Delayed_Caption ='" + Delayed_Caption + "', " +
                             " SS_From_Range ='" + SS_From_Range + "', " +
                             " SS_To_Range ='" + SS_To_Range + "' ," +
                             " Delayed_ShortName ='" + Delayed_ShortName + "', " +
                             " Fieldforce_Resig ='" + Fieldforce_Resig + "', " +
                             " Fieldforce_Resig_Caption ='" + Fieldforce_Resig_Caption + "' ," +
                             " Crm_Mgr='" + Crm_Mgr + "'," +
                             " Crm_Approval='" + Crm_Approval + "', " +
                              " Hosbus_Cal_Based ='" + Hosbus_Cal_Based + "', " +
                                " SS_Lock_Mgr ='" + SS_Lock_Mgr + "' ," +
                                 " SI_EM_Month ='" + SI_EM_Month + "' ," +
                                  " SI_EM_Year ='" + SI_EM_Year + "' ," +
                                   " IN_EM_Month ='" + IN_EM_Month + "' ," +
                                    " IN_EM_Year ='" + IN_EM_Year + "', " +
                                    " ListDr_Chembus_Cal_Based ='" + ListDr_Chembus_Cal_Based + "', " +
                                      " LE_MGR ='" + LE_MGR + "', " +
                                        " LE_MR ='" + LE_MR + "', " +
                                          " SS_Lock_Day ='" + SS_Lock_Day + "', " +
                                            " DCR_Approval_Remarks ='" + DCR_Approval_Remarks + "', " +
                                             " sample_Ack ='" + sample + "', " +
                                              " Input_Ack ='" + input + "' , " +
                                              " Mail_System ='" + strmail + "' " +
                             " where Division_Code='" + div_code + "'";
                }

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public bool sRecordExist_Target(string Division_Code)
        {
            if (Division_Code.Contains(','))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Setup_Others where Division_Code = '" + Division_Code + "'";
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
        public DataSet getOtherSetupfor_Targetyear(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select Target_Yearbasedon from Setup_Others where Division_Code='" + div_code + "' ";

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
        public DataSet get_day(int month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;



            strQry = "EXEC Month_date  " + month + " ";
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

        public DataSet getSetup_Expense(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select MgrAppr_Remark,MgrAppr_Row_Wise,ExpSub_Basedon,BasedOn_Peri_Date,Last_Os_Wrkconsider,Exp_Subm_Range_Start,Exp_Subm_Range_End ," +
                      " Fieldforce_HQ_Ex_Max,ExCalls_Minimum,MgrAppr_Sameadmin,OS_Package,Row_wise_textbox,Single_OS_Consider_as from Expense_Setup where Division_Code='" + divcode + "' ";

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

        public int Expense_Setup(string div_code, string MgrAppr_Remark, string MgrAppr_Row_Wise, string ExpSub_Basedon, string BasedOn_Peri_Date, string Last_Os_Wrkconsider, string Exp_Subm_Range_Start, string Exp_Subm_Range_End, string Fieldforce_HQ_Ex_Max, string ExCalls_Minimum, string MgrAppr_Sameadmin, string os_work_Calculation, string Row_wise_textbox, string rdoSingle_OS)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                bool count = sRecordExist_Expense(div_code);

                if (count == false)
                {
                    strQry = "Insert into Expense_Setup(Division_Code,MgrAppr_Remark,MgrAppr_Row_Wise,ExpSub_Basedon,BasedOn_Peri_Date,Last_Os_Wrkconsider,Exp_Subm_Range_Start,Exp_Subm_Range_End,Fieldforce_HQ_Ex_Max,ExCalls_Minimum,MgrAppr_Sameadmin,OS_Package,Row_wise_textbox,Single_OS_Consider_as) values " +
                             " ('" + div_code + "', '" + MgrAppr_Remark + "','" + MgrAppr_Row_Wise + "','" + ExpSub_Basedon + "','" + BasedOn_Peri_Date + "','" + Last_Os_Wrkconsider + "','" + Exp_Subm_Range_Start + "','" + Exp_Subm_Range_End + "','" + Fieldforce_HQ_Ex_Max + "','" + ExCalls_Minimum + "','" + MgrAppr_Sameadmin + "','" + os_work_Calculation + "','" + Row_wise_textbox + "','" + rdoSingle_OS + "') ";
                }

                else
                {

                    strQry = "Update Expense_Setup set MgrAppr_Remark='" + MgrAppr_Remark + "', " +
                             " MgrAppr_Row_Wise ='" + MgrAppr_Row_Wise + "', " +
                             " ExpSub_Basedon ='" + ExpSub_Basedon + "', " +
                             " BasedOn_Peri_Date ='" + BasedOn_Peri_Date + "', " +
                             " Last_Os_Wrkconsider ='" + Last_Os_Wrkconsider + "', " +
                             " Single_OS_Consider_as ='" + rdoSingle_OS + "', " +
                             " Exp_Subm_Range_Start ='" + Exp_Subm_Range_Start + "', " +
                             " Exp_Subm_Range_End ='" + Exp_Subm_Range_End + "', " +
                             " Fieldforce_HQ_Ex_Max ='" + Fieldforce_HQ_Ex_Max + "', " +
                             " ExCalls_Minimum ='" + ExCalls_Minimum + "', " +
                             " MgrAppr_Sameadmin ='" + MgrAppr_Sameadmin + "', " +
                             " OS_Package ='" + os_work_Calculation + "', " +
                             " Row_wise_textbox ='" + Row_wise_textbox + "' " +
                             " where Division_Code='" + div_code + "'";
                }

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public bool sRecordExist_Expense(string Division_Code)
        {
            if (Division_Code.Contains(','))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Expense_Setup where Division_Code = '" + Division_Code + "'";
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
        public DataSet getUploadFolderName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = " select Cate_Id,File_Cat_SName,File_Cat_Name,Division_Code from File_Upload_Category " +
                     " WHERE Division_Code='" + div_code + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public int RecordAdd_File(string File_Cat_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Cate_Id)+1,'1') Cate_Id from File_Upload_Category ";
                int Cate_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO File_Upload_Category (Cate_Id,File_Cat_Name,Division_Code,Created_Date)" +
                        "values('" + Cate_Id + "','" + File_Cat_Name + "' , '" + div_code + "',getdate())";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeleteFile_Id(int Cate_Id)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "DELETE FROM File_Upload_Category Where Cate_Id='" + Cate_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int File_Update(int Cate_Id, string File_Cat_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE File_Upload_Category " +
                          " SET File_Cat_Name = '" + File_Cat_Name + "'" +
                          " WHERE Cate_Id = '" + Cate_Id + "' and Division_Code='" + div_code + "'  ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public DataSet getFileCategory(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Cate_Id, '---Select---' as File_Cat_Name " +
                     " UNION " +
                     " SELECT Cate_Id,File_Cat_Name FROM  File_Upload_Category " +
                     " WHERE Division_Code= '" + divcode + "' " +
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
        public DataSet FindFileUpl(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType,Designation_Short_Name,Designation_Code  " +
                     " from file_info where " +
                      sFindQry +
                      " ";

            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataSet Cate_File_Upload(string div_code, string Cate_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType,Designation_Short_Name,Designation_Code,Cate_Id  " +
                     " from file_info where div_code='" + div_code + "' and Cate_Id = '" + Cate_Id + "'";


            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }

        public DataSet getScreenAdd_Check(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select count(sf_code) from Screen_Lock " +
                         "where sf_code='" + sf_code + "'  ";
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
        public int Recorddelete_Untagdrs(string dr_code, string div_code, string mpid)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "delete from map_GEO_Customers where cust_code='" + dr_code + "' " +
                         " and Division_code='" + div_code + "' and mapid='" + mpid + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet chkRange_tpbased_MGR(string divcode, string Designation_Short_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select Tp_Start_Date,Tp_End_Date from Mas_SF_Designation where Division_Code='" + divcode + "' and Designation_Short_Name='" + Designation_Short_Name + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public DataSet getHo_Password(string ho_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Password from Mas_HO_ID_Creation where Ho_id='" + ho_id + "' ";

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

        public int Update_Ho_Password(string ho_id, string pwd)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE Mas_HO_ID_Creation " +
                         " SET Password = '" + pwd + "', Upt_NewPwd_date = getdate() " +
                         " WHERE Ho_id = '" + ho_id + "' ";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public DataSet getHo_Pwd_Uptdt(string ho_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Upt_NewPwd_date from Mas_HO_ID_Creation where Ho_id='" + ho_id + "' ";

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

        public DataSet GetLeaveDates(string div_code, string sf_code, string Leave_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select From_Date,TO_Date from mas_Leave_Form where sf_code = '" + sf_code + "' and Leave_Id = '" + Leave_Id + "'";
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
        public DataSet getAllDaysBetweenTwoDate(string from_date, string to_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getAllDaysBetweenTwoDate '" + from_date + "','" + to_date + "'";
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
        public int Leave_Reject_Mgr_Rollback(string sf_code, string sf_name, string Leave_Id, string reject, string div_code, string dates)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC Reject_Leave_Rollback '" + sf_code + "', '" + sf_name + "','" + Leave_Id + "','" + reject + "','" + div_code + "'," + dates + " ";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //
        public DataSet getHome_Dash_Display(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " Select DOB_DOW " +
                     " from Option_Dash_Display where Division_Code = '" + divcode + "'";

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
        public int Home_Dash_Display(int DOB_DOW, string divcode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                if (HomeDashDisplay_RecordExist(divcode))
                {
                    strQry = " update Option_Dash_Display set DOB_DOW ='" + DOB_DOW + "' " +
                             " where  Division_Code='" + divcode + "'";
                }
                else
                {

                    strQry = " INSERT INTO Option_Dash_Display(DOB_DOW, Division_Code) " +
                             " VALUES ( '" + DOB_DOW + "', '" + divcode + "')";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool HomeDashDisplay_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Option_Dash_Display " +
                         " where Division_Code = '" + div_code + "'  ";

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

        public DataSet Get_Hold_Reason(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sf_blkreason " +
                " from Mas_Salesforce where sf_tp_active_flag=1 and Division_Code = '" + div_code + "' and sf_code = '" + sf_code + "'";

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
        public DataSet Max_activity_date(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " WITH allDates AS (SELECT activity_date FROM   DCRMain_Trans WHERE  sf_code='" + sf_code + "' and division_code='" + div_code + "' " +
                     " UNION " +
                     " SELECT activity_date FROM  DCRMain_Temp  WHERE sf_code='" + sf_code + "' and division_code='" + div_code + "' ) " +
                     " SELECT max(activity_date) AS MaxDate FROM   allDates ";
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
        public int FeedBack_RecordAdd(string Div_Name, string div_code, string Product_Rate, string Service_Rate, string area, string Contact_No, string Comments, string Remarks, string Recommend, int status)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Feed_ID = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);

                strQry = "SELECT ISNULL(max(Feed_ID),0)+1 FROM Cus_Feedback_Form";
                Feed_ID = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Cus_Feedback_Form(Feed_ID, Div_Name, Division_Code, Product_Rate, Service_Rate, Area_Of_Problem, Contact_No, Comments,Remarks, Created_Date,Status,Recommend) " +
                         " VALUES ( " + Feed_ID + " , '" + Div_Name + "', '" + div_code + "', '" + Product_Rate + "', '" + Service_Rate + "', '" + area + "', '" + Contact_No + "','" + Comments + "','" + Remarks + "',  getdate()," + status + ",'" + Recommend + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet GetFeedback(string div_code, string Feed_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Feed_ID, Div_Name, Division_Code, Product_Rate, Service_Rate, Area_Of_Problem, Contact_No, " +
                     "Comments,Remarks, Created_Date,Status,Recommend from Cus_Feedback_Form where Division_code='" + div_code + "' and  Feed_ID ='" + Feed_id + "'";

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
        public DataSet GetFeedback_All()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Feed_ID, Div_Name, Division_Code, Product_Rate, Service_Rate, Area_Of_Problem, Contact_No, " +
                     "Comments,Remarks, Created_Date,Status,Recommend from Cus_Feedback_Form where status=5";

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
        public DataSet Feedback_post()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Activate_Flag from customer_feedback";

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
        public DataSet Feedback_Exist(string Division_Code, int status)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Division_Code from Cus_Feedback_Form where Division_Code='" + Division_Code + "' and status=" + status + " ";

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
        public DataSet Get_Leave_Bal(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select Leave_Type_Code,No_Of_Days,Leave_Balance_Days,sf_code from Trans_Leave_Entitle_Head h," +
                     " Trans_Leave_Entitle_Details d  where sf_code='" + sf_code + "' and h.Division_code='" + div_code + "' and h.Sl_no =d.Sl_NO and Trans_Year=YEAR(GETDATE()) and h.active_flag=0";



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
        public DataSet Leave_App_Pending_Days(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select No_Of_Days ,Leave_SName from mas_leave_form l, mas_leave_type t where Leave_Active_Flag=2 and sf_code='" + sf_code + "' and l.Division_Code='" + div_code + "'  " +
                     " and l.Leave_Type=t.Leave_code and year(from_date)=YEAR(GETDATE())  ";



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
        public DataSet Leave_Policy_Setup(string div_code, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " SELECT Sl_no,Type,Max_Continuation_Days,Combination_Allow_CL,Combination_Allow_PL,Combination_Allow_SL " +
                     " ,Combination_Allow_LOP,Holi_Sun_As_Leave,Leave_take_before,No_Mini_Days,Start_End_Not_Allow_Sun_Holi " +
                     " ,Division_Code FROM Leave_Policy_Setup where division_code='" + div_code + "' and Type='" + type + "'";



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
        public DataSet getLast_date_Policy(string sf_code, string leavedate, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select Leave_id,Leave_Active_Flag,No_of_days from mas_Leave_Form l , mas_leave_type t " +
                     " where sf_code = '" + sf_code + "'  and  To_Date ='" + leavedate + "' " +
                     " and Leave_Active_Flag != 1 and  l.Leave_Type=t.Leave_code and t.Leave_SName ='" + type + "'";
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

        public DataSet getOtherSetupfor_Drbus_Cal_Based(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select Drbus_Cal_Based from Setup_Others where Division_Code='" + div_code + "' ";

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
        public int Insert_Leave_admin(string Leave_Type, DateTime From_Date, DateTime To_Date, string Reason, string Address, string No_of_Days, string Inform_by, string Valid_Reason, string sf_code, string Division_Code, string Informed_Ho, string Leave_Type_text, string sf_emp_id, string mgr_code, string mgr_emp_id, string Entry_Mode)
        {
            int iReturn = -1;
            DataSet dsadmn;
            DateTime Leave_From = DateTime.Now;
            DateTime Leave_To = DateTime.Now;
            DCR dc = new DCR();
            DataSet Trans_SlNo = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Leave_Id)+1,'1') Leave_Id from mas_Leave_Form ";
                int Leave_Id = db.Exec_Scalar(strQry);

                string leave_From_Date = From_Date.Month.ToString() + "-" + From_Date.Day + "-" + From_Date.Year;
                string leave_To_Date = To_Date.Month.ToString() + "-" + To_Date.Day + "-" + To_Date.Year;

                strQry = " INSERT INTO mas_Leave_Form(Leave_Id,Leave_Type, From_Date,To_Date,Reason,Address,No_of_Days,Inform_by,Valid_Reason,Leave_Active_Flag,Created_Date,sf_code,Division_Code,Informed_Ho,Sf_Emp_Id,Mgr_Code,Mgr_Emp_Id,Entry_Mode,Leave_App_Mgr) " +
                         " VALUES ('" + Leave_Id + "', '" + Leave_Type + "' , '" + leave_From_Date + "' , '" + leave_To_Date + "', '" + Reason + "' , '" + Address + "', " +
                         " '" + No_of_Days + "','" + Inform_by + "','" + Valid_Reason + "',0, getdate(),'" + sf_code + "','" + Division_Code + "','" + Informed_Ho + "','" + sf_emp_id + "','','','" + Entry_Mode + "','admin' ) ";

                iReturn = db.ExecQry(strQry);

                strQry =

                      " update Trans_Leave_Entitle_Details set leave_balance_days=leave_balance_days - " + No_of_Days + " " +
                      " from Trans_Leave_Entitle_Head h, Trans_Leave_Entitle_Details d  where sf_code='" + sf_code + "' and " +
                      " h.Sl_no =d.Sl_NO and Trans_Year=YEAR(GETDATE())  and Leave_Type_Code='" + Leave_Type_text + "' and h.active_flag=0 ";

                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    strQry = "select From_Date,TO_Date from mas_Leave_Form where sf_code = '" + sf_code + "' and Leave_Id = '" + Leave_Id + "'";

                    dsadmn = db.Exec_DataSet(strQry);

                    if (dsadmn.Tables[0].Rows.Count > 0)
                    {
                        Leave_From = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        Leave_To = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                        while (Leave_From <= Leave_To)
                        {
                            DateTime dcrdate = Leave_From;
                            string Leave_Date = dcrdate.ToString("MM/dd/yyyy");
                            strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + Leave_Date + "' ";
                            Trans_SlNo = db.Exec_DataSet(strQry);
                            if (Trans_SlNo.Tables[0].Rows.Count > 0)
                            {
                                strQry = "UPDATE DCRMain_Temp  SET Confirmed = 1 ,ReasonforRejection = '' " +
                                    " WHERE Sf_Code = '" + sf_code + "' and Trans_SlNo ='" + Trans_SlNo.Tables[0].Rows[0][0].ToString() + "'";

                                iReturn = db.ExecQry(strQry);

                                int iretmain = dc.Create_DCRHead_Trans(sf_code, Trans_SlNo.Tables[0].Rows[0]["Trans_SlNo"].ToString());
                            }
                            Leave_From = Leave_From.AddDays(1);
                        }
                    }

                    int LeaveMiss = dc.LeaveAppMissed(Leave_Id.ToString());
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int Leave_Appprove_Mode(string sf_code, string Leave_Id, string No_of_Days, string Leave_Type_text, string app_mgr, string Entry)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadmn;
            DateTime Leave_From = DateTime.Now;
            DateTime Leave_To = DateTime.Now;
            DCR dc = new DCR();
            int iReturn = -1;

            DataSet Trans_SlNo = new DataSet();

            strQry =

                       " update Trans_Leave_Entitle_Details set leave_balance_days=leave_balance_days - " + No_of_Days + " " +
                       " from Trans_Leave_Entitle_Head h, Trans_Leave_Entitle_Details d  where sf_code='" + sf_code + "' and " +
                       " h.Sl_no =d.Sl_NO and Trans_Year=YEAR(GETDATE())  and Leave_Type_Code='" + Leave_Type_text + "' and h.active_flag=0 ";

            iReturn = db_ER.ExecQry(strQry);

            strQry = " update mas_Leave_Form set Leave_Active_Flag = 0 ,LastUpdt_Date= getdate(),Leave_App_Mgr ='" + app_mgr + "', Approve_Mode ='" + Entry + "'" +
                     "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2  and Leave_Id = '" + Leave_Id + "'";

            try
            {
                iReturn = db_ER.ExecQry(strQry);
                // Added  by Sridevi - To Update DCR - for Leave
                if (iReturn > 0)
                {
                    strQry = "select From_Date,TO_Date from mas_Leave_Form where sf_code = '" + sf_code + "' and Leave_Id = '" + Leave_Id + "'";

                    dsadmn = db_ER.Exec_DataSet(strQry);

                    if (dsadmn.Tables[0].Rows.Count > 0)
                    {
                        Leave_From = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        Leave_To = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                        while (Leave_From <= Leave_To)
                        {
                            DateTime dcrdate = Leave_From;
                            string Leave_Date = dcrdate.ToString("MM/dd/yyyy");
                            strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + Leave_Date + "' ";
                            Trans_SlNo = db_ER.Exec_DataSet(strQry);
                            if (Trans_SlNo.Tables[0].Rows.Count > 0)
                            {
                                strQry = "UPDATE DCRMain_Temp  SET Confirmed = 1 ,ReasonforRejection = '' " +
                                    " WHERE Sf_Code = '" + sf_code + "' and Trans_SlNo ='" + Trans_SlNo.Tables[0].Rows[0][0].ToString() + "'";

                                iReturn = db_ER.ExecQry(strQry);

                                int iretmain = dc.Create_DCRHead_Trans(sf_code, Trans_SlNo.Tables[0].Rows[0]["Trans_SlNo"].ToString());
                            }
                            Leave_From = Leave_From.AddDays(1);
                        }
                    }

                    int LeaveMiss = dc.LeaveAppMissed(Leave_Id);
                }


                // Code Changes Ends - To Update DCR - for Leave
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getLast_date_Policy_New(string sf_code, string leavedate, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select Leave_id,Leave_Active_Flag,No_of_days from mas_Leave_Form l , mas_leave_type t " +
                     " where sf_code = '" + sf_code + "'  and  '" + leavedate + "' between From_Date and To_Date  " +
                     " and Leave_Active_Flag != 1 and  l.Leave_Type=t.Leave_code and t.Leave_SName ='" + type + "'";
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
        public int Core_Doctor_Map_Delete(string sf_code, string div_code, string mgr_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " DELETE FROM Core_Doctor_Map " +
                         " WHERE sf_code= '" + sf_code + "' and Division_code= '" + div_code + "' and mgr_code='" + mgr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int Insert_Leave_admin_Entry(string Leave_Type, DateTime From_Date, DateTime To_Date, string Reason, string Address, string No_of_Days, string Inform_by, string Valid_Reason, string sf_code, string Division_Code, string Informed_Ho, string Leave_Type_text, string sf_emp_id, string mgr_code, string mgr_emp_id, string Entry_Mode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Leave_Id)+1,'1') Leave_Id from mas_Leave_Form ";
                int Leave_Id = db.Exec_Scalar(strQry);

                string leave_From_Date = From_Date.Month.ToString() + "-" + From_Date.Day + "-" + From_Date.Year;
                string leave_To_Date = To_Date.Month.ToString() + "-" + To_Date.Day + "-" + To_Date.Year;

                strQry = " INSERT INTO mas_Leave_Form(Leave_Id,Leave_Type, From_Date,To_Date,Reason,Address,No_of_Days,Inform_by,Valid_Reason,Leave_Active_Flag,Created_Date,sf_code,Division_Code,Informed_Ho,Sf_Emp_Id,Mgr_Code,Mgr_Emp_Id,Entry_Mode,Leave_App_Mgr) " +
                         " VALUES ('" + Leave_Id + "', '" + Leave_Type + "' , '" + leave_From_Date + "' , '" + leave_To_Date + "', '" + Reason + "' , '" + Address + "', " +
                         " '" + No_of_Days + "','" + Inform_by + "','" + Valid_Reason + "',2, getdate(),'" + sf_code + "','" + Division_Code + "','" + Informed_Ho + "','" + sf_emp_id + "','" + mgr_code + "','" + mgr_emp_id + "','" + Entry_Mode + "','admin' ) ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int Campaign_Lock(string sf_code, string div_code, string iFlag)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                if (Campl_RecordExist(div_code, sf_code))
                {
                    strQry = "update Mas_Campaign_Lock set Campaign_Lock_flag= '" + iFlag + "' " +
                        " where Division_Code = '" + div_code + "' and sf_code='" + sf_code + "'";
                }
                else
                {

                    strQry = " INSERT INTO Mas_Campaign_Lock(sf_code,Division_Code,Campaign_Lock_flag) " +
                             " VALUES ('" + sf_code + "' , '" + div_code + "','" + iFlag + "') ";
                }
                iReturn = db.ExecQry(strQry);

                strQry = "update Screen_Lock set Camp_Lock= '" + iFlag + "' " +
                        " where Div_Code = '" + div_code + "' and sf_code='" + sf_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool Campl_RecordExist(string div_code, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Campaign_Lock " +
                         " where Division_Code = '" + div_code + "' and sf_code='" + sf_code + "' ";

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
        public DataSet get_camp_lock(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select camp_lock from Screen_Lock " +
                         "where sf_code='" + sf_code + "' and  Div_Code='" + div_code + "'  ";
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
        public DataSet get_core_lock(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            //strQry = "select Coredr_Lock from Screen_Lock " +
            //             "where sf_code='" + sf_code + "' and  Div_Code='" + div_code + "'  ";
            strQry = "select isnull((select distinct Coredr_Lock from Screen_Lock where sf_code='" + sf_code + "' and Div_Code='" + div_code + "' ),'0') Coredr_Lock ";
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
        public int Core_DR_Lock(string sf_code, string div_code, string iFlag)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                if (Coredr_RecordExist(div_code, sf_code))
                {
                    strQry = "update Mas_Core_DR_Lock set CoreDr_Lock_flag= '" + iFlag + "' " +
                        " where Division_Code = '" + div_code + "' and sf_code='" + sf_code + "'";
                }
                else
                {

                    strQry = " INSERT INTO Mas_Core_DR_Lock(sf_code,Division_Code,CoreDr_Lock_flag) " +
                             " VALUES ('" + sf_code + "' , '" + div_code + "','" + iFlag + "') ";
                }
                iReturn = db.ExecQry(strQry);

                if (Coredr_RecordExist1(div_code, sf_code))
                {

                    strQry = "update Screen_Lock set Coredr_Lock= '" + iFlag + "' " +
                            " where Div_Code = '" + div_code + "' and sf_code='" + sf_code + "'";
                }
                else
                {
                    strQry = "SELECT isnull(max([S.No])+1,'1') [S.No] from Screen_Lock ";
                    int SNo = db.Exec_Scalar(strQry);


                    strQry = "insert into Screen_Lock ([S.No],Mgr_code,SF_Code,Div_Code,DCR_Lock,TP_Lock,SDP_Lock,Camp_Lock,DR_Lock,Update_dtm,Unlst_Cnt,Coredr_Lock)" +
                    "values(" + SNo + ",'" + "admin" + "','" + sf_code + "','" + div_code + "',0,0,0,0,0,getdate(),'','" + iFlag + "') ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool Coredr_RecordExist(string div_code, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Core_DR_Lock " +
                         " where Division_Code = '" + div_code + "' and sf_code='" + sf_code + "' ";

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
        public bool Coredr_RecordExist1(string div_code, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Screen_Lock " +
                         " where Div_Code = '" + div_code + "' and sf_code='" + sf_code + "' ";

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
        public int DeActLeave_Id(int Leave_Code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_Leave_Type " +
                         " SET Active_Flag=1 " +
                         " WHERE Leave_code = '" + Leave_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int LeaveTypeAdd(string Leave_shtnme, string Leave_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT isnull(max(Leave_code)+1,'1') Leave_code from mas_Leave_Type ";
                //int Leave_code = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO mas_Leave_Type (Leave_SName,Leave_Name,Division_Code,Active_Flag,Created_Date)" +
                         "values('" + Leave_shtnme + "','" + Leave_Name + "' , '" + div_code + "',0,getdate())";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getLeaveName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Leave_code,Leave_SName,Leave_Name from mas_Leave_Type where division_code='" + div_code + "' and Active_Flag='0'";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public int LeaveUpdate(int Leave_Id, string LeaveShtnme, string LeaveName, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE mas_Leave_Type " +
                          " SET Leave_SName = '" + LeaveShtnme + "',Leave_Name='" + LeaveName + "'" +
                          " WHERE Leave_code = '" + Leave_Id + "' and Division_Code='" + div_code + "' and Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int personal_data(string Division_code, string Sf_code, string Sf_name, string HQ_name, string Photogragph, string Refer_by_vivo_emp, string Emp_Name, string Emp_Code, string Emp_FullName, string Pan_Card_No, string Name_In_Aadhar, string BAnk_Name, string Branch_Name,
       string Aadhar_Card_number, string Account_Num, string IFSC_code, string Address_Communication, string Comm_mob_no, string comm_mail_id, string comm_tel_no,
       string Permanent_Address, string per_mob_no, string per_tel_no, DateTime DOB, string Village, string Taluk, string District, string City, string state, string Country, string gender,
       string Religion, string Marital_Status, string Date_Of_Wedding, string Blood_group, string Long_sight, string Short_sight, string Illness_Surgery, string Disease,
       string Mother_Tongue, string lang_1, string lang_2, string lang_3, string lang_under_1, string lang_under_2, string lang_under_3, string lang_speak_1, string lang_speak_2,
       string lang_speak_3, string lang_read_1, string lang_read_2, string lang_read_3, string lang_write_1, string lang_write_2, string lang_write_3,
       string Father_Name, string Father_Age, string Father_DOB, string Father_Occupation, string Family_Address_1, string Family_Address_2, string Mother_Name, string Mother_Age,
       string Mother_DOB, string Mother_Occupation, string Spouse_Name, string Spouse_Age, string Spouse_DOB, string Spouse_Occupation, string Chil_Name, string Chil_Age, string Chil_DOB,
       string Chil_Occupation, string Chil_2_Name, string Chil_2_Age, string Chil_2_DOB, string Chil_2_Occupation, string Bro_Name, string Bro_Age, string Bro_DOB, string Bro_Occupation,
       string Bro_2_Name, string Bro_2_Age, string Bro_2_DOB, string Bro_2_Occupation, string Sis_Name, string Sis_age, string Sis_DOB, string Sis_Occupation, string Sis_2_Name, string Sis_2_age,
       string Sis_2_2DOB, string Sis_2_Occupation, string Nomini_Name, string Nomini_Address, string Nomini_Relation, string Nomini_Contact,
       string X_institude_Name, string X_Board_university, string X_From_Year, string X_To_Year, string X_Medium, string X_Specialization, string X_Mark, string Inter_inst_Name,
       string Inter_Board_university, string Inter_From_Year, string Inter_To_Year, string Inter_Medium, string Inter_Specialization, string Inter_Mark, string Grad_inst_Name,
       string Grad_Board_university, string Grad_From_Year, string Grad_To_Year, string Grad_Medium, string Grad_Specialization, string Grad_Mark, string PG_inst_Name,
       string PG_Board_university, string PG_From_Year, string PG_To_Year, string PG_Medium, string PG_Specialization, string PG_Mark, string Others_inst_Name, string Others_Board_university,
       string Others_From_Year, string Others_To_Year, string Others_Medium, string Others_Specialization, string Others_Mark, string Course_Name, string Course_University_nme,
       string Course_Duration, string Course_Mode, string Course_Comp_yr, string Academic_achieve, string Extra_Curricular, string Work_organ_1, string Work_organ_2,
       string Work_organ_3, string Wrk_from_yr_1, string Wrk_from_yr_2, string Wrk_from_yr_3, string Wrk_To_yr_1, string Wrk_To_yr_2, string Wrk_To_yr_3, string wrk_Duration_1, string wrk_Duration_2, string wrk_Duration_3,
       string Full_part_1, string Full_part_2, string Full_part_3, string Designation_1, string Designation_2, string Designation_3, string Reason_for_leaving_1, string Reason_for_leaving_2,
       string Reason_for_leaving_3, string last_drawn_salary_1, string last_drawn_salary_2, string last_drawn_salary_3, string Refer_Name_Add_1, string Refer_Name_Add_2, string Refer_occupation_1,
       string Refer_occupation_2, string Refer_Email_1, string Refer_Email_2, string Refer_contact_num_1, string Refer_contact_num_2, string Vivo_employee, string vivo_emp_name,
       string vivo_emp_designation, string vivo_emp_Relationship, string vivo_emp_contact, string Misc_legal_obligue, string legal_obligue_detail, string Misc_crime, string crime_detail, DateTime Misc_date, string Misc_Signature,
       DateTime off_Dte_application, DateTime off_joining_dte, string off_emp_code, string off_designation, string off_dte_accep, string off_report_relation, string off_department, string off_loction, string off_sign_hr, string off_email
                      )
        {
            int iReturn = -1;


            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = "select top 1 Creation_date from Trans_Personal_Data_Head order by Creation_date desc";



                strQry = "select count(Sl_no) from Trans_Personal_Data_Head where sf_code='" + Sf_code + "' and Creation_date in(select top 1 Creation_date from Trans_Personal_Data_Head order by Creation_date desc)";
                int cnt_sl_no = db.Exec_Scalar(strQry);

                string Emp_DOB = DOB.Month.ToString() + "-" + DOB.Day + "-" + DOB.Year;
                //string Emp_wedding = Date_Of_Wedding.Month.ToString() + "-" + Date_Of_Wedding.Day + "-" + Date_Of_Wedding.Year;
                string Miscdate = Misc_date.Month.ToString() + "-" + Misc_date.Day + "-" + Misc_date.Year;
                string off_date = off_Dte_application.Month.ToString() + "-" + off_Dte_application.Day + "-" + off_Dte_application.Year;
                string of_join_dte = off_joining_dte.Month.ToString() + "-" + off_joining_dte.Day + "-" + off_joining_dte.Year;

                if (cnt_sl_no > 0)
                {
                    strQry = "select sl_no from Trans_Personal_Data_Head where sf_code='" + Sf_code + "'";
                    int sl_no1 = db.Exec_Scalar(strQry);

                    strQry = "update Trans_Personal_Data_Head set Emp_Name='" + Emp_Name + "',Emp_Code='" + Emp_Code + "',Emp_FullName='" + Emp_FullName + "',Pan_Card_No='" + Pan_Card_No + "',Name_In_Aadhar='" + Name_In_Aadhar + "', " +
                             "BAnk_Name='" + BAnk_Name + "',Branch_Name='" + Branch_Name + "',Aadhar_Card_number='" + Aadhar_Card_number + "',Account_Num='" + Account_Num + "',IFSC_code='" + IFSC_code + "',Address_Communication='" + Address_Communication + "', " +
                             "Comm_mob_no='" + Comm_mob_no + "',comm_mail_id='" + comm_mail_id + "',comm_tel_no='" + comm_tel_no + "',Permanent_Address='" + Permanent_Address + "',per_mob_no='" + per_mob_no + "',per_tel_no='" + per_tel_no + "',DOB='" + Emp_DOB + "',Village='" + Village + "'," +
                             "Taluk='" + Taluk + "',District='" + District + "',City='" + City + "',state='" + state + "',Country='" + Country + "',gender='" + gender + "',Religion='" + Religion + "',Marital_Status='" + Marital_Status + "',Date_Of_Wedding='" + Date_Of_Wedding + "',Blood_group='" + Blood_group + "', " +
                             "Long_sight='" + Long_sight + "',Short_sight='" + Short_sight + "',[Illness/Surgery]='" + Illness_Surgery + "',Disease='" + Disease + "', " +
                             "Mother_Tongue='" + Mother_Tongue + "',lang_1='" + lang_1 + "',lang_2='" + lang_2 + "',lang_3='" + lang_3 + "',lang_under_1='" + lang_under_1 + "',lang_under_2='" + lang_under_2 + "',lang_under_3='" + lang_under_3 + "'," +
                             "lang_speak_1='" + lang_speak_1 + "',lang_speak_2='" + lang_speak_2 + "',lang_speak_3='" + lang_speak_3 + "',lang_read_1='" + lang_read_1 + "',lang_read_2='" + lang_read_2 + "',lang_read_3='" + lang_read_3 + "', " +
                             "lang_write_1='" + lang_write_1 + "',lang_write_2='" + lang_write_2 + "',lang_write_3='" + lang_write_3 + "' where sl_no='" + sl_no1 + "'";
                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_Personal_Family_detail set Father_Name='" + Father_Name + "',Father_Age='" + Father_Age + "',Father_DOB='" + Father_DOB + "',Father_Occupation='" + Father_Occupation + "', " +
                             "Family_Address_1='" + Family_Address_1 + "',Family_Address_2='" + Family_Address_2 + "',Mother_Name='" + Mother_Name + "',Mother_Age='" + Mother_Age + "', " +
                             "Mother_DOB='" + Mother_DOB + "',Mother_Occupation='" + Mother_Occupation + "',Spouse_Name='" + Spouse_Name + "',Spouse_Age='" + Spouse_Age + "',Spouse_DOB='" + Spouse_DOB + "',Spouse_Occupation='" + Spouse_Occupation + "'," +
                             "Child_Name='" + Chil_Name + "',Child_Age='" + Chil_Age + "',Child_DOB='" + Chil_DOB + "',Child_Occupation='" + Chil_Occupation + "',Child_2_Name='" + Chil_2_Name + "',Child_2_Age='" + Chil_2_Age + "',Child_2_DOB='" + Chil_2_DOB + "',Child_2_Occupation='" + Chil_2_Occupation + "'," +
                             "Bro_Name='" + Bro_Name + "',Bro_Age='" + Bro_Age + "',Bro_DOB='" + Bro_Age + "',Bro_Occupation='" + Bro_Occupation + "',Bro_2_Name='" + Bro_2_Name + "',Bro_2_Age='" + Bro_2_Age + "',Bro_2_DOB='" + Bro_2_DOB + "',Bro_2_Occupation='" + Bro_2_Occupation + "'," +
                             "Sis_Name='" + Sis_Name + "',Sis_age='" + Sis_age + "',Sis_DOB='" + Sis_DOB + "',Sis_Occupation='" + Sis_Occupation + "',Sis_2_Name='" + Sis_2_Name + "',Sis_2_age='" + Sis_2_age + "', " +
                             "Sis_2_2DOB='" + Sis_2_2DOB + "',Sis_2_Occupation='" + Sis_2_Occupation + "',Nomini_Name='" + Nomini_Name + "',Nomini_Address='" + Nomini_Address + "',Nomini_Relation='" + Nomini_Relation + "',Nomini_Contact='" + Nomini_Contact + "' where sl_no='" + sl_no1 + "'";
                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_Personal_Education_Detail set X_institude_Name='" + X_institude_Name + "',X_Board_university='" + X_Board_university + "',X_From_Year='" + X_From_Year + "',X_To_Year='" + X_To_Year + "',X_Medium='" + X_Medium + "',X_Specialization='" + X_Specialization + "', " +
                             "X_Mark='" + X_Mark + "',Inter_inst_Name='" + Inter_inst_Name + "',Inter_Board_university='" + Inter_Board_university + "',Inter_From_Year='" + Inter_From_Year + "',Inter_To_Year='" + Inter_To_Year + "',Inter_Medium='" + Inter_Medium + "',Inter_Specialization='" + Inter_Specialization + "',Inter_Mark='" + Inter_Mark + "', " +
                             "Grad_inst_Name='" + Grad_inst_Name + "',Grad_Board_university='" + Grad_Board_university + "',Grad_From_Year='" + Grad_From_Year + "',Grad_To_Year='" + Grad_To_Year + "',Grad_Medium='" + Grad_Medium + "',Grad_Specialization='" + Grad_Specialization + "',Grad_Mark='" + Grad_Mark + "',PG_inst_Name='" + PG_inst_Name + "', " +
                             "PG_Board_university='" + PG_Board_university + "',PG_From_Year='" + PG_From_Year + "',PG_To_Year='" + PG_To_Year + "',PG_Medium='" + PG_Medium + "',PG_Specialization='" + PG_Specialization + "',PG_Mark='" + PG_Mark + "',Others_inst_Name='" + Others_inst_Name + "',Others_Board_university='" + Others_Board_university + "', " +
                             "Others_From_Year='" + Others_From_Year + "',Others_To_Year='" + Others_To_Year + "',Others_Medium='" + Others_Medium + "',Others_Specialization='" + Others_Specialization + "',Others_Mark='" + Others_Mark + "',Course_Name='" + Course_Name + "',Course_University_nme='" + Course_University_nme + "', " +
                             "Course_Duration='" + Course_Duration + "',Course_Mode='" + Course_Mode + "',Course_Comp_yr='" + Course_Comp_yr + "',Academic_achieve='" + Academic_achieve + "',Extra_Curricular='" + Extra_Curricular + "' where sl_no='" + sl_no1 + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_Personal_Working_detail set Work_organ_1='" + Work_organ_1 + "',Work_organ_2='" + Work_organ_2 + "',Work_organ_3='" + Work_organ_3 + "'," +
                             "Wrk_from_yr_1='" + Wrk_from_yr_1 + "',Wrk_from_yr_2='" + Wrk_from_yr_2 + "',Wrk_from_yr_3='" + Wrk_from_yr_3 + "',Wrk_To_yr_1='" + Wrk_To_yr_1 + "',Wrk_To_yr_2='" + Wrk_To_yr_2 + "', " +
                             "Wrk_To_yr_3='" + Wrk_To_yr_3 + "',wrk_Duration_1='" + wrk_Duration_1 + "',wrk_Duration_2='" + wrk_Duration_2 + "',wrk_Duration_3='" + wrk_Duration_3 + "',Full_part_1='" + Full_part_1 + "'," +
                             "Full_part_2='" + Full_part_2 + "',Full_part_3='" + Full_part_3 + "',Designation_1='" + Designation_1 + "',Designation_2='" + Designation_2 + "',Designation_3='" + Designation_3 + "',Reason_for_leaving_1='" + Reason_for_leaving_1 + "', " +
                             "Reason_for_leaving_2='" + Reason_for_leaving_2 + "',Reason_for_leaving_3='" + Reason_for_leaving_3 + "',last_drawn_salary_1='" + last_drawn_salary_1 + "', " +
                             "last_drawn_salary_2='" + last_drawn_salary_2 + "',last_drawn_salary_3='" + last_drawn_salary_3 + "' where sl_no='" + sl_no1 + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_Personal_References_Detail set Refer_Name_Add_1='" + Refer_Name_Add_1 + "',Refer_Name_Add_2='" + Refer_Name_Add_2 + "',Refer_occupation_1='" + Refer_occupation_1 + "', " +
                             "Refer_occupation_2='" + Refer_occupation_2 + "',Refer_Email_1='" + Refer_Email_1 + "',Refer_Email_2='" + Refer_Email_2 + "', " +
                             "Refer_contact_num_1='" + Refer_contact_num_1 + "',Refer_contact_num_2='" + Refer_contact_num_2 + "',Vivo_employee='" + Vivo_employee + "',vivo_emp_name='" + vivo_emp_name + "'," +
                             "vivo_emp_designation='" + vivo_emp_designation + "',vivo_emp_Relationship='" + vivo_emp_Relationship + "',vivo_emp_contact='" + vivo_emp_contact + "',Misc_crime='" + Misc_crime + "',Misc_legal_obligue='" + Misc_legal_obligue + "', " +
                             "Misc_date='" + Miscdate + "',Misc_Signature='" + Misc_Signature + "',off_Dte_application='" + off_date + "',off_joining_dte='" + of_join_dte + "', " +
                             "off_emp_code='" + off_emp_code + "',off_designation='" + off_designation + "',off_dte_accep='" + off_dte_accep + "',off_report_relation='" + off_report_relation + "',off_department='" + off_department + "'," +
                             "off_loction='" + off_loction + "',off_sign_hr='" + off_sign_hr + "',off_email='" + off_email + "',Misc_obligue_detail='" + legal_obligue_detail + "',Misc_crime_detail='" + crime_detail + "'  where sl_no='" + sl_no1 + "'";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    strQry = "insert into Trans_Personal_Data_Head(Emp_Name,Emp_Code,Emp_FullName,Pan_Card_No,Name_In_Aadhar,BAnk_Name,Branch_Name,Aadhar_Card_number,Account_Num,IFSC_code,Address_Communication,Comm_mob_no,comm_mail_id,comm_tel_no, " +
                             "Permanent_Address,per_mob_no,per_tel_no,DOB,Village,Taluk,District,City,state,Country,gender,Religion,Marital_Status,Date_Of_Wedding,Blood_group,Long_sight,Short_sight,[Illness/Surgery],Disease, " +
                             "Mother_Tongue,lang_1,lang_2,lang_3,lang_under_1,lang_under_2,lang_under_3,lang_speak_1,lang_speak_2,lang_speak_3,lang_read_1,lang_read_2,lang_read_3,lang_write_1,lang_write_2,lang_write_3,Creation_date,Updated_date, " +
                             "Division_code,Sf_code,Sf_name,HQ_name,Photogragh,Refer_by_vivo_emp)values('" + Emp_Name + "','" + Emp_Code + "','" + Emp_FullName + "','" + Pan_Card_No + "', " +
                             "'" + Name_In_Aadhar + "','" + BAnk_Name + "','" + Branch_Name + "','" + Aadhar_Card_number + "','" + Account_Num + "','" + IFSC_code + "','" + Address_Communication + "','" + Comm_mob_no + "','" + comm_mail_id + "','" + comm_tel_no + "','" + Permanent_Address + "','" + per_mob_no + "', " +
                             "'" + per_tel_no + "','" + Emp_DOB + "','" + Village + "','" + Taluk + "','" + District + "','" + City + "','" + state + "','" + Country + "','" + gender + "','" + Religion + "','" + Marital_Status + "', " +
                             "'" + Date_Of_Wedding + "','" + Blood_group + "','" + Long_sight + "','" + Short_sight + "','" + Illness_Surgery + "','" + Disease + "','" + Mother_Tongue + "','" + lang_1 + "','" + lang_2 + "','" + lang_3 + "','" + lang_under_1 + "', " +
                             "'" + lang_under_2 + "','" + lang_under_3 + "','" + lang_speak_1 + "','" + lang_speak_2 + "','" + lang_speak_3 + "','" + lang_read_1 + "','" + lang_read_2 + "','" + lang_read_3 + "', " +
                             "'" + lang_write_1 + "','" + lang_write_2 + "','" + lang_write_3 + "',getdate(),getdate(),'" + Division_code + "','" + Sf_code + "','" + Sf_name + "','" + HQ_name + "','" + Photogragph + "','" + Refer_by_vivo_emp + "')";
                    iReturn = db.ExecQry(strQry);

                    strQry = "select max(sl_no) from Trans_Personal_Data_Head where sf_code='" + Sf_code + "'";
                    int Sl_No = db.Exec_Scalar(strQry);

                    strQry = "insert into Trans_Personal_Family_detail(sl_no,Father_Name,Father_Age,Father_DOB,Father_Occupation,Family_Address_1,Family_Address_2,Mother_Name,Mother_Age, " +
                             "Mother_DOB,Mother_Occupation,Spouse_Name,Spouse_Age,Spouse_DOB,Spouse_Occupation,Child_Name,Child_Age,Child_DOB, " +
                             "Child_Occupation,Child_2_Name,Child_2_Age,Child_2_DOB,Child_2_Occupation,Bro_Name,Bro_Age,Bro_DOB,Bro_Occupation, " +
                             "Bro_2_Name,Bro_2_Age,Bro_2_DOB,Bro_2_Occupation,Sis_Name,Sis_age,Sis_DOB,Sis_Occupation,Sis_2_Name,Sis_2_age, " +
                             "Sis_2_2DOB,Sis_2_Occupation,Nomini_Name,Nomini_Address,Nomini_Relation,Nomini_Contact)values('" + Sl_No + "','" + Father_Name + "','" + Father_Age + "','" + Father_DOB + "', " +
                             "'" + Father_Occupation + "','" + Family_Address_1 + "','" + Family_Address_2 + "','" + Mother_Name + "','" + Mother_Age + "','" + Mother_DOB + "','" + Mother_Occupation + "','" + Spouse_Name + "' ," +
                             "'" + Spouse_Age + "','" + Spouse_DOB + "','" + Spouse_Occupation + "','" + Chil_Name + "','" + Chil_Age + "','" + Chil_DOB + "','" + Chil_Occupation + "','" + Chil_2_Name + "','" + Chil_2_Age + "','" + Chil_2_DOB + "','" + Chil_2_Occupation + "','" + Bro_Name + "','" + Bro_Age + "', " +
                             "'" + Bro_DOB + "','" + Bro_Occupation + "','" + Bro_2_Name + "','" + Bro_2_Age + "','" + Bro_2_DOB + "','" + Bro_2_Occupation + "','" + Sis_Name + "','" + Sis_age + "','" + Sis_DOB + "','" + Sis_Occupation + "','" + Sis_2_Name + "','" + Sis_2_age + "','" + Sis_2_2DOB + "', " +
                             "'" + Sis_2_Occupation + "','" + Nomini_Name + "','" + Nomini_Address + "','" + Nomini_Relation + "','" + Nomini_Contact + "')";

                    iReturn = db.ExecQry(strQry);

                    strQry = "insert into Trans_Personal_Education_Detail(sl_no,X_institude_Name,X_Board_university,X_From_Year,X_To_Year,X_Medium,X_Specialization,X_Mark,Inter_inst_Name, " +
                             "Inter_Board_university,Inter_From_Year,Inter_To_Year,Inter_Medium,Inter_Specialization,Inter_Mark,Grad_inst_Name,Grad_Board_university,Grad_From_Year,Grad_To_Year,Grad_Medium,Grad_Specialization,Grad_Mark,PG_inst_Name, " +
                             "PG_Board_university,PG_From_Year,PG_To_Year,PG_Medium,PG_Specialization,PG_Mark,Others_inst_Name,Others_Board_university,Others_From_Year,Others_To_Year,Others_Medium,Others_Specialization,Others_Mark,Course_Name,Course_University_nme, " +
                             "Course_Duration,Course_Mode,Course_Comp_yr,Academic_achieve,Extra_Curricular)values('" + Sl_No + "','" + X_institude_Name + "','" + X_Board_university + "','" + X_From_Year + "','" + X_To_Year + "', " +
                             "'" + X_Medium + "','" + X_Specialization + "','" + X_Mark + "','" + Inter_inst_Name + "','" + Inter_Board_university + "','" + Inter_From_Year + "','" + Inter_To_Year + "','" + Inter_Medium + "','" + Inter_Specialization + "', " +
                             "'" + Inter_Mark + "','" + Grad_inst_Name + "','" + Grad_Board_university + "','" + Grad_From_Year + "','" + Grad_To_Year + "','" + Grad_Medium + "','" + Grad_Specialization + "','" + Grad_Mark + "','" + PG_inst_Name + "', " +
                             "'" + PG_Board_university + "','" + PG_From_Year + "','" + PG_To_Year + "','" + PG_Medium + "','" + PG_Specialization + "','" + PG_Mark + "','" + Others_inst_Name + "','" + Others_Board_university + "','" + Others_From_Year + "','" + Others_To_Year + "','" + Others_Medium + "','" + Others_Specialization + "','" + Others_Mark + "','" + Course_Name + "', " +
                             "'" + Course_University_nme + "','" + Course_Duration + "','" + Course_Mode + "','" + Course_Comp_yr + "','" + Academic_achieve + "','" + Extra_Curricular + "')";

                    iReturn = db.ExecQry(strQry);

                    strQry = "insert into Trans_Personal_Working_detail(sl_no,Work_organ_1,Work_organ_2,Work_organ_3,Wrk_from_yr_1,Wrk_from_yr_2,Wrk_from_yr_3,Wrk_To_yr_1,Wrk_To_yr_2, " +
                             "Wrk_To_yr_3,wrk_Duration_1,wrk_Duration_2,wrk_Duration_3,Full_part_1,Full_part_2,Full_part_3,Designation_1,Designation_2,Designation_3,Reason_for_leaving_1,Reason_for_leaving_2,Reason_for_leaving_3,last_drawn_salary_1, " +
                             "last_drawn_salary_2,last_drawn_salary_3)values('" + Sl_No + "','" + Work_organ_1 + "','" + Work_organ_2 + "','" + Work_organ_3 + "','" + Wrk_from_yr_1 + "','" + Wrk_from_yr_2 + "','" + Wrk_from_yr_3 + "','" + Wrk_To_yr_1 + "','" + Wrk_To_yr_2 + "','" + Wrk_To_yr_3 + "','" + wrk_Duration_1 + "','" + wrk_Duration_2 + "', " +
                             "'" + wrk_Duration_3 + "','" + Full_part_1 + "','" + Full_part_2 + "','" + Full_part_3 + "','" + Designation_1 + "','" + Designation_2 + "','" + Designation_3 + "','" + Reason_for_leaving_1 + "','" + Reason_for_leaving_2 + "','" + Reason_for_leaving_3 + "','" + last_drawn_salary_1 + "','" + last_drawn_salary_2 + "', " +
                             "'" + last_drawn_salary_3 + "')";

                    iReturn = db.ExecQry(strQry);

                    strQry = "insert into Trans_Personal_References_Detail (sl_no,Refer_Name_Add_1,Refer_Name_Add_2,Refer_occupation_1,Refer_occupation_2,Refer_Email_1,Refer_Email_2, " +
                             "Refer_contact_num_1,Refer_contact_num_2,Vivo_employee,vivo_emp_name,vivo_emp_designation,vivo_emp_Relationship,vivo_emp_contact,Misc_crime,Misc_legal_obligue,Misc_date,Misc_Signature,off_Dte_application,off_joining_dte, " +
                             "off_emp_code,off_designation,off_dte_accep,off_report_relation,off_department,off_loction,off_sign_hr,off_email,Misc_obligue_detail,Misc_crime_detail)values('" + Sl_No + "','" + Refer_Name_Add_1 + "','" + Refer_Name_Add_2 + "','" + Refer_occupation_1 + "','" + Refer_occupation_2 + "','" + Refer_Email_1 + "','" + Refer_Email_2 + "','" + Refer_contact_num_1 + "', " +
                             "'" + Refer_contact_num_2 + "','" + Vivo_employee + "','" + vivo_emp_name + "','" + vivo_emp_designation + "','" + vivo_emp_Relationship + "','" + vivo_emp_contact + "','" + Misc_crime + "','" + Misc_legal_obligue + "','" + Miscdate + "','" + Misc_Signature + "','" + off_date + "','" + of_join_dte + "','" + off_emp_code + "', " +
                             "'" + off_designation + "','" + off_dte_accep + "','" + off_report_relation + "','" + off_department + "','" + off_loction + "','" + off_sign_hr + "','" + off_email + "','" + legal_obligue_detail + "','" + crime_detail + "')";

                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet personal_data_view(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select top 1 a.sl_no,a.Refer_by_vivo_emp,Emp_Name,Emp_Code,Emp_FullName,Pan_Card_No,Name_In_Aadhar,BAnk_Name,Branch_Name,Aadhar_Card_number,Account_Num,IFSC_code,Address_Communication,Comm_mob_no,comm_mail_id,comm_tel_no, " +
                     "Permanent_Address,per_mob_no,per_tel_no,DOB,Village,Taluk,District,City,state,Country,gender,Religion,Marital_Status,Date_Of_Wedding,Blood_group,Long_sight,Short_sight,[Illness/Surgery],Disease,b.Father_Name,b.Mother_Name,b.Spouse_Name,b.Child_Name,b.Child_2_Name,b.Bro_Name,b.Bro_2_Name,b.Sis_Name,b.Sis_2_Name,b.Father_Age,b.Mother_Age,b.Spouse_Age, " +
                     "b.Child_Age,b.Child_2_Age,b.Bro_Age,b.Bro_2_Age,b.Sis_age,b.Sis_2_age,b.Father_DOB,b.Mother_DOB,b.Spouse_DOB,b.Child_DOB,b.Child_2_DOB,b.Bro_Age,b.Bro_2_Age,b.Sis_DOB,b.Sis_2_2DOB,b.Father_Occupation,b.Mother_Occupation,b.Spouse_occupation,b.Child_Occupation,b.Child_2_Occupation,b.Bro_Occupation, " +
                     "b.Bro_2_Occupation,b.Sis_Occupation,b.Sis_2_Occupation,b.Family_Address_1,b.Family_Address_2,c.X_institude_Name,c.Inter_inst_Name,c.Grad_inst_Name,c.PG_inst_Name,c.Others_inst_Name,c.X_Board_university,c.Inter_Board_university,c.Grad_Board_university,c.PG_Board_university, " +
                     "c.Others_Board_university,c.X_From_Year,c.Inter_From_Year,c.Grad_From_Year,c.PG_From_Year,c.Others_From_Year,c.X_To_Year,c.Inter_To_Year, " +
                     "c.Grad_To_Year,c.PG_To_Year,c.Others_To_Year,c.X_Medium,c.Inter_Medium,c.Grad_Medium,c.PG_Medium,c.Others_Medium,c.X_Specialization,c.Inter_Specialization,c.Grad_Specialization,c.Others_Specialization,c.X_Mark,c.Inter_Mark,c.Grad_Mark,c.PG_Mark,c.Others_Mark, " +
                     "c.Course_Name,c.Course_University_nme,c.Course_Duration,c.Course_Mode,c.Course_Comp_yr,c.Academic_achieve,c.Extra_Curricular, " +
                     "d.Work_organ_1,d.Work_organ_2,d.Work_organ_3,d.Wrk_from_yr_1,d.Wrk_from_yr_2,d.Wrk_from_yr_3,d.Wrk_To_yr_1,d.Wrk_To_yr_2,d.Wrk_To_yr_3,d.wrk_Duration_1,d.wrk_Duration_2,d.wrk_Duration_3,d.Full_part_1,d.Full_part_2,d.Full_part_3,d.Designation_1,d.Designation_2,d.Designation_3,d.Reason_for_leaving_1, " +
                     "d.Reason_for_leaving_2,d.Reason_for_leaving_3,d.last_drawn_salary_1,d.last_drawn_salary_2,d.last_drawn_salary_3,b.Nomini_Name,b.Nomini_Relation,b.Nomini_Contact,b.Nomini_Address,e.Refer_Name_Add_1,e.Refer_Name_Add_2,e.Refer_occupation_1,e.Refer_occupation_2, " +
                     "e.Refer_Email_1,e.Refer_Email_2,e.Refer_contact_num_1,e.Refer_contact_num_2,e.Vivo_employee,e.vivo_emp_name,e.vivo_emp_designation,e.vivo_emp_Relationship,e.vivo_emp_contact,a.Mother_Tongue,a.lang_1,a.lang_2,a.lang_3,a.lang_under_1,a.lang_under_2,a.lang_under_3,a.lang_speak_1,a.lang_speak_2, " +
                     "a.lang_speak_3,a.lang_read_1,a.lang_read_2,a.lang_read_3,a.lang_write_1,a.lang_write_2,a.lang_write_3,e.Misc_legal_obligue,e.Misc_obligue_detail,e.Misc_crime,e.Misc_crime_detail,e.Misc_date,e.Misc_Signature,e.off_Dte_application,e.off_emp_code,e.off_dte_accep,e.off_department,e.off_sign_hr, " +
                     "e.off_joining_dte,e.off_designation,e.off_report_relation,e.off_loction,e.off_email from Trans_Personal_Data_Head a,Trans_Personal_Family_detail b,Trans_Personal_Education_Detail c,Trans_Personal_Working_detail d,Trans_Personal_References_Detail e " +
                     "where a.sl_no=b.sl_no and a.sl_no=c.sl_no and a.sl_no=d.sl_no and a.sl_no=e.sl_no  and sf_code='" + sf_code + "' order by a.sl_no desc";
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
        public DataSet getqualcnt(string div_code, string fromspecial)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select distinct Doc_QuaName,count(ListedDrCode)cnt,b.Doc_QuaCode from Mas_Doctor_Speciality a,mas_listeddr b,Mas_Doc_Qualification c " +
                     "  where a.division_code=b.division_code and a.Doc_Special_Code=b.Doc_Special_Code and ListedDr_Active_Flag=0 " +
                     "  and a.Division_Code='" + div_code + "' and b.Doc_QuaCode=c.Doc_QuaCode and Doc_Special_Active_Flag=0 and a.Doc_Special_Code='" + fromspecial + "' " +
                     " and Doc_Qua_ActiveFlag=0 group by Doc_QuaName,b.Doc_QuaCode order by b.Doc_QuaCode";

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
        public int updatespeciality(string Qual_code, string div_code, string Tospecial_code, string Fromspecial_code, string Tospecialshortname, string Fromspecialshortname)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "update mas_listeddr set Doc_Special_Code='" + Tospecial_code + "',Doc_Spec_ShortName='" + Tospecialshortname + "',LastUpdt_Date = getdate()  where  Division_Code='" + div_code + "' and Doc_Special_Code='" + Fromspecial_code + "' " +
                     " and Doc_QuaCode='" + Qual_code + "'  ";

            iReturn = db_ER.ExecQry(strQry);

            strQry = "update Mas_UnListedDr set Doc_Special_Code='" + Tospecial_code + "',LastUpdt_Date = getdate()  where  Division_Code='" + div_code + "' and Doc_Special_Code='" + Fromspecial_code + "' " +
                   " and Doc_QuaCode='" + Qual_code + "'   ";

            iReturn = db_ER.ExecQry(strQry);

            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet Get_Quiz_Process(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = " declare @surveyid int  " +
            //         " set @surveyid =( select Survey_Id from QuizTitleCreation where " +
            //         " Division_Code='" + div_code + "' and Status=0 and Active=0) " +
            //         " select ProcessId,Sf_Code,Sf_Name,SurveyId as Survey_Id, " +
            //         " ( select top(1) status from Quiz_Result where Sf_Code ='" + sf_code + "' " +
            //         " and Division_Code = '" + div_code + "' and Survey_Id=@surveyid  order by  status desc) as res " +
            //         " from Processing_UserList p where Sf_Code ='" + sf_code + "' and  Div_Code='" + div_code + "' and SurveyId= @surveyid";

            strQry = "Exec Quiz_Test_Link '" + div_code + "','" + sf_code + "'";

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
        public DataSet get_FileQuiz(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select  filepath,convert(char(10), Effective_Date,101) Effective_Date  from QuizTitleCreation where division_code='" + div_code + "' and status='0'";


            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
        public DataSet Get_Quiz(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            //if (Quiz_RecordExist(div_code, sf_code))
            //{
            //    strQry = "select  Survey_Id from QuizTitleCreation where  Division_Code='50'";
            //}
            //else
            //{
            //    strQry = "select Survey_Id,Quiz_Title,Effective_Date,Division_Code, Status " +
            //       " from QuizTitleCreation where Status=0 and Division_Code='" + div_code + "'";
            //}
            //strQry = " select Survey_Id,Quiz_Title,Effective_Date,Division_Code, Status, " +
            //      " ( select top(1) status from Quiz_Result where Sf_Code ='" + sf_code + "' " +
            //      " and Division_Code = '" + div_code + "' order by  status desc) as res " +
            //      " from QuizTitleCreation where Status=0 and Division_Code='" + div_code + "' ";

            strQry = " declare @surveyid int " +
                   " set @surveyid =( select Survey_Id from QuizTitleCreation where  Division_Code='" + div_code + "' and Status=0 and Active=0) " +
                   " select Survey_Id,Quiz_Title,Effective_Date,Division_Code, Status, " +
                   " ( select top(1) status from Quiz_Result where Sf_Code ='" + sf_code + "' " +
                   " and Division_Code = '" + div_code + "' and Survey_Id=@surveyid  order by  status desc) as res " +
                   " from QuizTitleCreation where Status=0 and Division_Code='" + div_code + "' ";
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

        public bool Quiz_RecordExist(string div_code, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Sf_Code) from Quiz_Result where Sf_Code ='" + sf_code + "' and Division_Code = '" + div_code + "' ";

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
        public int AddQuiz(string Sf_Code, string Sf_Name, string div_code, string Quiz_Id, string Input_Id, string Survey_Id, string start_time, string end_time)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Result_Id = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);
                string starttime = start_time.Substring(6, 4) + "-" + start_time.Substring(3, 2) + "-" + start_time.Substring(0, 2) + start_time.Substring(10, 8);
                string endtime = end_time.Substring(6, 4) + "-" + end_time.Substring(3, 2) + "-" + end_time.Substring(0, 2) + end_time.Substring(10, 8);
                strQry = "select  isnull(max(Cast((Result_Id) as int) ),0) + 1 from Quiz_Result  ";
                Result_Id = db.Exec_Scalar(strQry);
                if (Quiz_RecordExist_Attem(div_code, Sf_Code, Quiz_Id))
                {
                    strQry = " update Quiz_Result set Second_Input_Id= '" + Input_Id + "', Status=2, Second_Start_time = '" + starttime + "' ,Second_End_time = '" + endtime + "' where Status=1 and  Sf_Code ='" + Sf_Code + "' and Division_Code = '" + div_code + "'  and Quiz_Id = '" + Quiz_Id + "' ";
                }
                else
                {
                    strQry = " INSERT INTO Quiz_Result(Result_Id,Sf_Code,Sf_Name,Division_Code,Quiz_Id,Input_Id,Status,Survey_Id,Created_Date,First_Start_time,First_End_time) " +
                               " VALUES ('" + Result_Id + "', '" + Sf_Code + "', '" + Sf_Name + "' , '" + div_code + "', '" + Quiz_Id + "', '" + Input_Id + "',1," + Survey_Id + ", getdate(),'" + starttime + "','" + endtime + "') ";
                }

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public bool Quiz_RecordExist_Attem(string div_code, string sf_code, string Quiz_Id)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Sf_Code) from Quiz_Result where Sf_Code ='" + sf_code + "' and Division_Code = '" + div_code + "' and Quiz_Id = '" + Quiz_Id + "' and  Status=1;";

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
        public DataSet Get_Result(string div_code, string survey_id, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select CAST( COUNT(a.Input_Id) as varchar)+ ' / '+(select cast(COUNT(Question_Id)as varchar) from AddQuestions " +
                     " where Division_Code='" + div_code + "' and SurveyID=" + survey_id + "),COUNT(a.Input_Id) ans, " +
                     " ( select COUNT(Question_Id)   from AddQuestions where Division_Code='" + div_code + "' and SurveyID=" + survey_id + " ) qus " +
                     "  from AddInputOptions a, Quiz_Result q  " +
                     "  where  a.Question_Id=q.Quiz_Id and a.Input_Id = q.Input_Id and Correct_Ans=1 and q.Sf_Code='" + sf_code + "' and Survey_Id = " + survey_id + "  ";

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
        public DataSet GetSecondQuiz(string div_code, string sf_code, string survey_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select Status,Survey_Id from Quiz_Result where Division_code='" + div_code + "' and sf_code='" + sf_code + "' and Survey_Id = " + survey_id + "  ";

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

        public DataSet Get_Result_New(string div_code, string survey_id, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select CAST( COUNT(a.Input_Id) as varchar)+ ' / '+(select cast(COUNT(Question_Id)as varchar) from AddQuestions " +
                     " where Division_Code='" + div_code + "' and SurveyID=" + survey_id + "),COUNT(a.Input_Id) ans, " +
                     " ( select COUNT(Question_Id)   from AddQuestions where Division_Code='" + div_code + "' and SurveyID=" + survey_id + " ) qus " +
                     "  from AddInputOptions a, Quiz_Result q  " +
                     "  where  a.Question_Id=q.Quiz_Id and a.Input_Id = q.Second_Input_Id and Correct_Ans=1 and q.Sf_Code='" + sf_code + "' and Survey_Id = " + survey_id + "   ";

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
        //Leave
        public DataSet getSetup_Leave_Ent_MGR(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select LE_MGR from Setup_Others where Division_Code='" + divcode + "' ";



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
        public DataSet getSetup_Leave_Ent_MR(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select LE_MR from Setup_Others where Division_Code='" + divcode + "' ";



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
        public DataSet Leave_type_Setup(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select Leave_Type_Reg from Mas_leave_setup where Division_Code='" + div_code + "' ";



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
        public DataSet get_Mr_type(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select Fieldforce_Type from mas_salesforce where Division_Code='" + div_code + ",' and sf_code='" + sf_code + "'";



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
        public DataSet FillLeave_Type_stup(string div_code, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " SELECT 0 as Leave_code,'--Select--' as Leave_SName,'' as Leave_Name " +
                     " UNION " +
                     " SELECT Leave_code,Leave_SName,Leave_Name " +
                     " FROM mas_Leave_Type l,Mas_leave_setup s where Leave_Type_Code=Leave_code and Active_Flag = 0 and l.Division_Code='" + div_code + "' " +
                     " and Leave_Entry='Y' and Leave_Type_Sl_no='" + type + "' ";



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
        public int Insert_Leave_Mgr(string Leave_Type, DateTime From_Date, DateTime To_Date, string Reason, string Address, string No_of_Days, string Inform_by, string Valid_Reason, string sf_code, string Division_Code, string Informed_Ho, string Leave_Type_text, string sf_emp_id, string mgr_code, string mgr_emp_id, string Entry_Mode, string Sf_Name)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Leave_Id)+1,'1') Leave_Id from mas_Leave_Form ";
                int Leave_Id = db.Exec_Scalar(strQry);

                string leave_From_Date = From_Date.Month.ToString() + "-" + From_Date.Day + "-" + From_Date.Year;
                string leave_To_Date = To_Date.Month.ToString() + "-" + To_Date.Day + "-" + To_Date.Year;

                strQry = " INSERT INTO mas_Leave_Form(Leave_Id,Leave_Type, From_Date,To_Date,Reason,Address,No_of_Days,Inform_by,Valid_Reason,Leave_Active_Flag,Created_Date,sf_code,Division_Code,Informed_Ho,Sf_Emp_Id,Mgr_Code,Mgr_Emp_Id,Entry_Mode,Sf_Name) " +
                         " VALUES ('" + Leave_Id + "', '" + Leave_Type + "' , '" + leave_From_Date + "' , '" + leave_To_Date + "', '" + Reason + "' , '" + Address + "', " +
                         " '" + No_of_Days + "','" + Inform_by + "','" + Valid_Reason + "',2, getdate(),'" + sf_code + "','" + Division_Code + "','" + Informed_Ho + "','" + sf_emp_id + "','" + mgr_code + "','" + mgr_emp_id + "','" + Entry_Mode + "','" + Sf_Name + "' ) ";

                iReturn = db.ExecQry(strQry);
                //strQry =   " update Trans_Leave_Entitle_Details set leave_balance_days=leave_balance_days - " + No_of_Days + " " +
                //           " from Trans_Leave_Entitle_Head h, Trans_Leave_Entitle_Details d  where sf_code='" + sf_code + "' and " +
                //           " h.Sl_no =d.Sl_NO and Trans_Year='2017'  and Leave_Type_Code='" + Leave_Type_text + "' ";

                //iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet Get_Admin_FieldForce_Setup_New(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Sf_Code, Division_Code , " +
                        " ListedDr_Add_Option, ListedDr_Edit_Option, ListedDr_Deactivate_Option, ListedDr_View_Option, " +
                        " NewDoctor_Entry_Option, NewDoctor_Edit_Option, NewDoctor_Deact_Option, NewDoctor_View_Option, " +
                        " Chemist_Add_Option, Chemist_Edit_Option, Chemist_Deactivate_Option, Chemist_View_Option, " +
                        " Territory_Add_Option,Territory_Edit_Option,Territory_Deactivate_Option,Territory_View_Option, " +
                        " Class_Add_Option, Class_Edit_Option, Class_Deactivate_Option, Class_View_Option, " +
                        " ListedDr_Reactivate_Option, NewDoctor_ReAct_Option, Chemist_Reactivate_Option, Class_Reactivate_Option, Doc_Name_Chg,Territory_Reactivation_Option " +
                        " ,(select distinct isnull(PCPM_Access,0) from mas_salesforce  where sf_Code =a.sf_Code)PCPM_Access from dbo.Admin_FieldForce_Setup a " +
                        "  where Sf_Code = '" + sf_code + "' and Division_Code='" + div_code + "' ";

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

        public int Add_Admin_FieldForce_Setup_New(string sf_code, string div_code,
           int iDoctorAdd, int iDoctorEdit, int iDoctorDeAct, int iDoctorView,
           int iNewDoctorAdd, int iNewDoctorEdit, int iNewDoctorDeAct, int iNewDoctorView,
           int iChemAdd, int iChemEdit, int iChemDeAct, int iChemView,
           int iTerrAdd, int iTerrEdit, int iTerrDeAct, int iTerrView,
           int iClassAdd, int iClassEdit, int iClassDeAct, int iClassView,
           int iDoctorReAct, int iNewDoctorReAct, int iChemReAct, int iClassReAct, int iDocName, int iTerrReact, int iPCPM)
        {
            int iReturn = -1;
            int iReturn1 = -1;

            try
            {
                string strQry1 = string.Empty; 
                DB_EReporting db = new DB_EReporting();


                strQry1 = " UPDATE mas_salesforce SET PCPM_Access = '" + iPCPM + "' WHERE sf_code = '" + sf_code + "' ";
                iReturn1 = db.ExecQry(strQry1);

                int sl_no = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);

                if (Admin_FieldForce_Setup_RecordExist(sf_code))
                {
                    //Admin_FieldForce_Setup_RecordExist(sf_code);

                    strQry = "SELECT Sl_No FROM Admin_FieldForce_Setup where sf_code = '" + sf_code + "' ";
                    sl_no = db.Exec_Scalar(strQry);

                   

                    strQry = " UPDATE Admin_FieldForce_Setup " +
                                " SET ListedDr_Add_Option = '" + iDoctorAdd + "', ListedDr_Edit_Option = '" + iDoctorEdit + "' , " +
                                " ListedDr_Deactivate_Option = '" + iDoctorDeAct + "', ListedDr_View_Option = '" + iDoctorView + "', " +
                                " NewDoctor_Entry_Option = '" + iNewDoctorAdd + "', NewDoctor_Edit_Option = '" + iNewDoctorEdit + "', " +
                                " NewDoctor_DeAct_Option = '" + iNewDoctorDeAct + "', NewDoctor_View_Option = '" + iNewDoctorView + "', " +
                                " Chemist_Add_Option = '" + iChemAdd + "', Chemist_Edit_Option = '" + iChemEdit + "' , " +
                                " Chemist_Deactivate_Option = '" + iChemDeAct + "', Chemist_View_Option = '" + iChemView + "', " +
                                " Territory_Add_Option = '" + iTerrAdd + "', Territory_Edit_Option = '" + iTerrEdit + "', " +
                                " Territory_Deactivate_Option = '" + iTerrDeAct + "', Territory_View_Option = '" + iTerrView + "', " +
                                " Class_Add_Option = '" + iClassAdd + "', Class_Edit_Option = '" + iClassEdit + "', " +
                                " Class_Deactivate_Option = '" + iClassDeAct + "', Class_View_Option = '" + iClassView + "', " +
                                " ListedDr_Reactivate_Option ='" + iDoctorReAct + "', " +
                                " NewDoctor_ReAct_Option ='" + iNewDoctorReAct + "'," +
                                " Chemist_Reactivate_Option ='" + iChemReAct + "', " +
                                " Class_Reactivate_Option = '" + iClassReAct + "', Doc_Name_Chg = '" + iDocName + "',Territory_Reactivation_Option='" + iTerrReact + "' " +
                                " WHERE Sl_No = " + sl_no + " and sf_code = '" + sf_code + "' ";
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Admin_FieldForce_Setup";
                    sl_no = db.Exec_Scalar(strQry);

                    strQry = " INSERT INTO Admin_FieldForce_Setup(Sl_No, Sf_Code, Division_Code, " +
                                " ListedDr_Add_Option, ListedDr_Edit_Option, ListedDr_Deactivate_Option, ListedDr_View_Option, " +
                                " NewDoctor_Entry_Option, NewDoctor_Edit_Option, NewDoctor_DeAct_Option, NewDoctor_View_Option, " +
                                " Chemist_Add_Option, Chemist_Edit_Option, Chemist_Deactivate_Option, Chemist_View_Option, " +
                                " Territory_Add_Option, Territory_Edit_Option, Territory_Deactivate_Option, Territory_View_Option, " +
                                " Class_Add_Option, Class_Edit_Option, Class_Deactivate_Option, Class_View_Option, " +
                                " ListedDr_Reactivate_Option, NewDoctor_ReAct_Option, Chemist_Reactivate_Option, Class_Reactivate_Option, Doc_Name_Chg,Territory_Reactivation_Option) " +
                             " VALUES ( " + sl_no + " , '" + sf_code + "', '" + div_code + "',  " +
                             iDoctorAdd + ",  " + iDoctorEdit + ",  " + iDoctorDeAct + ", " + iDoctorView + ", " +
                             iNewDoctorAdd + ", " + iNewDoctorEdit + ", " + iNewDoctorDeAct + ", " + iNewDoctorView + ", " +
                             iChemAdd + ", " + iChemEdit + ", " + iChemDeAct + ", " + iChemView + ", " +
                             iTerrAdd + ", " + iTerrEdit + ", " + iTerrDeAct + ", " + iTerrView + ", " +
                             iClassAdd + ", " + iClassEdit + ", " + iClassDeAct + ", " + iClassView + ", " +
                             iDoctorReAct + ", " + iNewDoctorReAct + ", " + iChemReAct + ", " + iClassReAct + ", '" + iDocName + "','" + iTerrReact + "' ) ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int RecordUpdate(string Doc_MulPlan, string strWorkAra, string strNoofTPView,
            int Doc_Count_DCR, int Chem_Count_DCR, int Stk_Count_DCR, int UnLstDr_Count_DCR,
            int Hos_Count_DCR, int doc_disp, int sess_dcr, int time_dcr, int UnLstDr_reqd,
            int prod_Qty_zero, int prod_selection, int pob, int DCRSess_Mand, int DCRTime_Mand,
            int DCRProd_Mand, string wrk_area_SName, int iDelayedSystem, int iHolidayCalc,
            int iDelayAllowDays, int iHolidayStatus, int iSundayStatus, int iApprovalSystem, string Division_Code,
            string strDCRTP, int remarkslength, int iDrRem, int inewchem, int inewudr, string ListedDr_Allowed, string Chemist_Allowed, int iDocApp, int iDeactApp, int iAddDeact, string Stockist_Allowed, int lock_sysyem, string lock_time_limit, int ProductFeedback_Needed, int PrdRx_Qty_Needed, int LstPOB_Updation, int ChemPOB_Updation, int halfday_wrk, string No_of_UnlistDr_Allowed, int LstDr_Priority, int TpBasesd_DCR, string LstDr_Priority_Range,
            int TPDCR_Deviation, int TPDCR_MGRAppr, int ChemPOB_Qty_Needed, string PrdRx_Qty_Caption, string ChemPOB_Qty_Caption,
            int PrdSample_Qty_Needed, int ChemSample_Qty_Needed, int Input_Mand, string PrdSample_Qty_Caption, string ChemSample_Qty_Caption,
            int Dr_POBQty_DefaZero, int Chem_SampleQty_DefaZero, int Chem_POBQty_DefaZero, int FFWiseDly, string FFWiseDly_Days,string Display_Patchwise_DR,string
            DCR_Dr_Mandatory, int ProductSampleValid, int InputSampleValid)
        {
            int iReturn = -1;
            int Count = 0;
            DataSet dsadmin = null;
            bool isdcrsetupupdt = false;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "UPDATE Admin_Setups " +
                //            " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "',No_of_TP_View='" + strNoofTPView + "'";
                bool value = sRecordExist(Division_Code);

                if (value == false)
                {

                    strQry = "Insert into Admin_Setups (SingleDr_WithMultiplePlan_Required,Wrk_Area_Name,No_of_TP_View,No_Of_DCR_Drs_Count,No_Of_DCR_Chem_Count,No_Of_DCR_Stockist_Count,"
                         + " No_Of_DCR_Ndr_Count, No_of_dcr_Hosp, Doctor_disp_in_Dcr, DCRSess_Entry_Permission, NonDrNeeded, DCRTime_Entry_Permission ,"
                         + " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, POBType, DCRSess_Mand, DCRTime_Mand, DCRProd_Mand, wrk_area_SName, "
                         + " DelayedSystem_Required_Status,Delay_Holiday_Status,No_Of_Days_Delay,AutoPost_Holiday_Status,AutoPost_Sunday_Status, Approval_System,"
                         + " Division_Code, TpBased,Remarks_length_Allowed,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr,No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,"
                         + " Doc_App_Needed, Doc_Deact_Needed, Add_Deact_Needed,No_Of_Sl_StockistAllowed,LockSystem_Needed,LockSystem_Timelimit,ProductFeedback_Needed,"
                         + " PrdRx_Qty_Needed,LstPOB_Updation,ChemPOB_Updation,HalfDayWordEntryNeed,No_of_UnlistDr_Allowed,LstDr_Priority,TpBasesd_DCR,LstDr_Priority_Range,"
                         + " TPDCR_Deviation,TPDCR_MGRAppr,ChemPOB_Qty_Needed,PrdRx_Qty_Caption,ChemPOB_Qty_Caption,PrdSample_Qty_Needed,ChemSample_Qty_Needed,Input_Mand,"
                         + " PrdSample_Qty_Caption,ChemSample_Qty_Caption,Dr_POBQty_DefaZero,Chem_SampleQty_DefaZero,Chem_POBQty_DefaZero,FieldForceWise_Delay,FFWise_Delay_Days,Display_Patchwise_DR,DCR_Dr_Mandatory,Prod_SampleQty_Validation_Needed,InputQty_Validation_Needed) values "
                         + " ('" + Doc_MulPlan + "','" + strWorkAra + "','" + strNoofTPView + "','" + Doc_Count_DCR + "' ,'" + Chem_Count_DCR + "','" + Stk_Count_DCR + "', "
                         + " '" + UnLstDr_Count_DCR + "', '" + Hos_Count_DCR + "','" + doc_disp + "', '" + sess_dcr + "','" + UnLstDr_reqd + "', '" + time_dcr + "',"
                         + " '" + prod_Qty_zero + "','" + prod_selection + "','" + pob + "','" + DCRSess_Mand + "','" + DCRTime_Mand + "','" + DCRProd_Mand + "','" + wrk_area_SName + "', "
                         + " '" + iDelayedSystem + "', '" + iHolidayCalc + "', '" + iDelayAllowDays + "','" + iHolidayStatus + "','" + iSundayStatus + "', '" + iApprovalSystem + "', '" + Division_Code + "',"
                         + " '" + strDCRTP + "' ,'" + remarkslength + "' ,'" + iDrRem + "' ,'" + inewchem + "' ,'" + inewudr + "','" + ListedDr_Allowed + "' ,'" + Chemist_Allowed + "', '" + iDocApp + "',"
                         + " '" + iDeactApp + "', '" + iAddDeact + "','" + Stockist_Allowed + "','" + lock_sysyem + "','" + lock_time_limit + "','" + ProductFeedback_Needed + "','" + PrdRx_Qty_Needed + "',"
                         + " '" + LstPOB_Updation + "','" + ChemPOB_Updation + "','" + halfday_wrk + "','" + No_of_UnlistDr_Allowed + "','" + LstDr_Priority + "','" + TpBasesd_DCR + "','" + LstDr_Priority_Range + "',"
                         + " '" + TPDCR_Deviation + "','" + TPDCR_MGRAppr + "','" + ChemPOB_Qty_Needed + "','" + PrdRx_Qty_Caption + "','" + ChemPOB_Qty_Caption + "','" + PrdSample_Qty_Needed + "','" + ChemSample_Qty_Needed + "','" + Input_Mand + "',"
                         + " '" + PrdSample_Qty_Caption + "','" + ChemSample_Qty_Caption + "','" + Dr_POBQty_DefaZero + "','" + Chem_SampleQty_DefaZero + "','" + Chem_POBQty_DefaZero + "','" + FFWiseDly + "','" + FFWiseDly_Days + "','"+ Display_Patchwise_DR + "','"+ DCR_Dr_Mandatory + "','" + ProductSampleValid + "','" + InputSampleValid + "')";
                }
                else
                {
                    dsadmin = getAdminSetup(Division_Code);

                    if (sess_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()))
                        isdcrsetupupdt = true;
                    if (time_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()))
                        isdcrsetupupdt = true;
                    if (Doc_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()))
                        isdcrsetupupdt = true;
                    if (Chem_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString()))
                        isdcrsetupupdt = true;
                    if (Stk_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()))
                        isdcrsetupupdt = true;
                    if (Hos_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString()))
                        isdcrsetupupdt = true;
                    if (doc_disp != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_reqd != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_Qty_zero != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_selection != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString()))
                        isdcrsetupupdt = true;
                    if (pob != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRSess_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRTime_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRProd_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayedSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayCalc != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayAllowDays != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString()))
                        isdcrsetupupdt = true;
                    if (iSundayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString()))
                        isdcrsetupupdt = true;
                    if (iApprovalSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString()))
                        isdcrsetupupdt = true;
                    if (remarkslength != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString()))
                        isdcrsetupupdt = true;
                    if (iDrRem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString()))
                        isdcrsetupupdt = true;
                    if (inewchem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString()))
                        isdcrsetupupdt = true;
                    if (inewudr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString()))
                        isdcrsetupupdt = true;
                    if (ProductSampleValid != Convert.ToInt16(dsadmin.Tables[0].Rows[0]["Prod_SampleQty_Validation_Needed"].ToString()))
                        isdcrsetupupdt = true;
                    if (InputSampleValid != Convert.ToInt16(dsadmin.Tables[0].Rows[0]["InputQty_Validation_Needed"].ToString()))
                        isdcrsetupupdt = true;

                    strQry = "UPDATE Admin_Setups " +
                               " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "', "
                                + " No_of_TP_View = '" + strNoofTPView + "',"
                                + " No_Of_DCR_Drs_Count ='" + Doc_Count_DCR + "' ,"
                                + " No_Of_DCR_Chem_Count ='" + Chem_Count_DCR + "', "
                                + " No_Of_DCR_Stockist_Count ='" + Stk_Count_DCR + "', "
                                + " No_Of_DCR_Ndr_Count ='" + UnLstDr_Count_DCR + "', "
                                + " No_of_dcr_Hosp ='" + Hos_Count_DCR + "', "
                                + " Doctor_disp_in_Dcr = '" + doc_disp + "', "
                                + " DCRSess_Entry_Permission = '" + sess_dcr + "', "
                                + " NonDrNeeded = '" + UnLstDr_reqd + "', "
                                + " DCRTime_Entry_Permission = '" + time_dcr + "', "
                                + " SampleProQtyDefaZero = '" + prod_Qty_zero + "', "
                                + " No_Of_Product_selection_Allowed_in_Dcr = '" + prod_selection + "', "
                                + " POBType = '" + pob + "', "
                                + " DCRSess_Mand = '" + DCRSess_Mand + "', "
                                + " DCRTime_Mand = '" + DCRTime_Mand + "', "
                                + " DCRProd_Mand = '" + DCRProd_Mand + "', "
                                + " wrk_area_SName='" + wrk_area_SName + "', "
                                + " DelayedSystem_Required_Status ='" + iDelayedSystem + "', "
                                + " Delay_Holiday_Status ='" + iHolidayCalc + "', "
                                + " No_Of_Days_Delay ='" + iDelayAllowDays + "', "
                                + " AutoPost_Holiday_Status ='" + iHolidayStatus + "', "
                                + " Approval_System = " + iApprovalSystem + ", "
                                + " TpBased='" + strDCRTP + "',"
                                + " Remarks_length_Allowed='" + remarkslength + "',"
                                + " AutoPost_Sunday_Status ='" + iSundayStatus + "', "
                                + " DCRLDR_Remarks ='" + iDrRem + "', "
                                + " DCRNew_Chem ='" + inewchem + "', "
                                + " DCRNew_ULDr ='" + inewudr + "',"
                                + " No_Of_Sl_DoctorsAllowed='" + ListedDr_Allowed + "',"
                                + " No_Of_Sl_ChemistsAllowed='" + Chemist_Allowed + "', Doc_App_Needed='" + iDocApp + "', Doc_Deact_Needed = '" + iDeactApp + "', Add_Deact_Needed = '" + iAddDeact + "', No_Of_Sl_StockistAllowed='" + Stockist_Allowed + "',LockSystem_Needed='" + lock_sysyem + "',LockSystem_Timelimit='" + lock_time_limit + "',ProductFeedback_Needed='" + ProductFeedback_Needed + "',PrdRx_Qty_Needed='" + PrdRx_Qty_Needed + "',LstPOB_Updation='" + LstPOB_Updation + "',ChemPOB_Updation='" + ChemPOB_Updation + "',HalfDayWordEntryNeed='" + halfday_wrk + "',No_of_UnlistDr_Allowed='" + No_of_UnlistDr_Allowed + "',LstDr_Priority='" + LstDr_Priority + "',TpBasesd_DCR='" + TpBasesd_DCR + "',LstDr_Priority_Range='" + LstDr_Priority_Range + "',TPDCR_Deviation='" + TPDCR_Deviation + "',TPDCR_MGRAppr='" + TPDCR_MGRAppr + "',ChemPOB_Qty_Needed='" + ChemPOB_Qty_Needed + "',PrdRx_Qty_Caption='" + PrdRx_Qty_Caption + "',ChemPOB_Qty_Caption='" + ChemPOB_Qty_Caption + "',PrdSample_Qty_Needed='" + PrdSample_Qty_Needed + "',ChemSample_Qty_Needed='" + ChemSample_Qty_Needed + "',Input_Mand='" + Input_Mand + "',"
                                + " PrdSample_Qty_Caption='" + PrdSample_Qty_Caption + "',ChemSample_Qty_Caption='" + ChemSample_Qty_Caption + "',Dr_POBQty_DefaZero='" + Dr_POBQty_DefaZero + "',Chem_SampleQty_DefaZero='" + Chem_SampleQty_DefaZero + "',Chem_POBQty_DefaZero='" + Chem_POBQty_DefaZero + "',FieldForceWise_Delay='" + FFWiseDly + "',FFWise_Delay_Days='" + FFWiseDly_Days + "',Display_Patchwise_DR='"+ Display_Patchwise_DR + "',DCR_Dr_Mandatory='"+ DCR_Dr_Mandatory + "',Prod_SampleQty_Validation_Needed='" + ProductSampleValid + "',InputQty_Validation_Needed='" + InputSampleValid + "' where Division_Code = '" + Division_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);

                if (isdcrsetupupdt == true)
                {
                    strQry = "UPDATE Admin_Setups set LastUpdt_DCRStp = getdate() where Division_Code = '" + Division_Code + "' ";
                }

                iReturn = db.ExecQry(strQry);




            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;


        }
        public int RecordUpdate_MGR(int Doc_Count_DCR, int Chem_Count_DCR, int Stk_Count_DCR, int UnLstDr_Count_DCR, int Hos_Count_DCR, int doc_disp, int sess_dcr, int time_dcr, int UnLstDr_reqd, int prod_Qty_zero, int prod_selection, int pob, int DCRSess_Mand, int DCRTime_Mand, int DCRProd_Mand, int iDelayedSystem, int iHolidayCalc, int iDelayAllowDays, int iHolidayStatus, int iSundayStatus, int iApprovalSystem, string Division_Code, int remarkslength, int iDrRem, int inewchem, int inewudr, string strWorkAra, int lock_sysyem, string lock_time_limit, int ProductFeedback_Needed, int PrdRx_Qty_Needed, int LstPOB_Updation, int ChemPOB_Updation, int halfday_wrk, string strDCRTP, int TpBasesd_DCR, int TPDCR_Deviation, int TPDCR_MGRAppr, int ChemPOB_Qty_Needed, string PrdRx_Qty_Caption, string ChemPOB_Qty_Caption, int PrdSample_Qty_Needed, int ChemSample_Qty_Needed, string PrdSample_Qty_Caption, string ChemSample_Qty_Caption, int Dr_POBQty_DefaZero, int Chem_SampleQty_DefaZero, int Chem_POBQty_DefaZero, int FFWiseDly, string FFWiseDly_Days, int ProductSample_Valid, int InputSample_Valid)

        {
            int iReturn = -1;
            int Count = 0;
            DataSet dsadmin = null;
            bool isdcrsetupupdt = false;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "UPDATE Admin_Setups " +
                //            " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "',No_of_TP_View='" + strNoofTPView + "'";
                bool value = sRecordExistMGR(Division_Code);

                if (value == false)
                {

                    strQry = "Insert into Admin_Setups_MGR (No_Of_DCR_Drs_Count,No_Of_DCR_Chem_Count,No_Of_DCR_Stockist_Count,"
                         + " No_Of_DCR_Ndr_Count, No_of_dcr_Hosp,Doctor_disp_in_Dcr, DCRSess_Entry_Permission, NonDrNeeded, DCRTime_Entry_Permission ,"
                         + " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, POBType, DCRSess_Mand, DCRTime_Mand, DCRProd_Mand ,"
                         + " DelayedSystem_Required_Status,Delay_Holiday_Status,No_Of_Days_Delay,AutoPost_Holiday_Status,AutoPost_Sunday_Status, Approval_System, Division_Code ,Remarks_length_Allowed,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr,wrk_area_Name,LockSystem_Needed,LockSystem_Timelimit,ProductFeedback_Needed,PrdRx_Qty_Needed,LstPOB_Updation,ChemPOB_Updation,HalfDayWordEntryNeed,TpBased,TpBasesd_DCR,TPDCR_Deviation,TPDCR_MGRAppr,ChemPOB_Qty_Needed,PrdRx_Qty_Caption,ChemPOB_Qty_Caption,PrdSample_Qty_Needed,ChemSample_Qty_Needed,PrdSample_Qty_Caption,ChemSample_Qty_Caption,Dr_POBQty_DefaZero,Chem_SampleQty_DefaZero,Chem_POBQty_DefaZero,FieldForceWise_Delay,FFWise_Delay_Days,Prod_SampleQty_Validation_Needed,InputQty_Validation_Needed) values "
                         + " ('" + Doc_Count_DCR + "' ,'" + Chem_Count_DCR + "','" + Stk_Count_DCR + "', "
                         + " '" + UnLstDr_Count_DCR + "', '" + Hos_Count_DCR + "','" + doc_disp + "', '" + sess_dcr + "','" + UnLstDr_reqd + "', '" + time_dcr + "',"
                         + " '" + prod_Qty_zero + "','" + prod_selection + "','" + pob + "','" + DCRSess_Mand + "','" + DCRTime_Mand + "','" + DCRProd_Mand + "' , "
                         + " '" + iDelayedSystem + "', '" + iHolidayCalc + "', '" + iDelayAllowDays + "','" + iHolidayStatus + "','" + iSundayStatus + "', '" + iApprovalSystem + "', '" + Division_Code + "','" + remarkslength + "','" + iDrRem + "' ,'" + inewchem + "' ,'" + inewudr + "','" + strWorkAra + "','" + lock_sysyem + "','" + lock_time_limit + "','" + ProductFeedback_Needed + "','" + PrdRx_Qty_Needed + "','" + LstPOB_Updation + "','" + ChemPOB_Updation + "','" + halfday_wrk + "','" + strDCRTP + "','" + TpBasesd_DCR + "','" + TPDCR_Deviation + "','" + TPDCR_MGRAppr + "','" + ChemPOB_Qty_Needed + "','" + PrdRx_Qty_Caption + "','" + ChemPOB_Qty_Caption + "','" + PrdSample_Qty_Needed + "','" + ChemSample_Qty_Needed + "','" + PrdSample_Qty_Caption + "','" + ChemSample_Qty_Caption + "','" + Dr_POBQty_DefaZero + "','" + Chem_SampleQty_DefaZero + "','" + Chem_POBQty_DefaZero + "','" + FFWiseDly + "','" + FFWiseDly_Days + "','" + ProductSample_Valid + "','" + InputSample_Valid + "')";
                }
                else
                {
                    dsadmin = getAdminSetup_MGR(Division_Code);

                    if (sess_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()))
                        isdcrsetupupdt = true;
                    if (time_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()))
                        isdcrsetupupdt = true;
                    if (Doc_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()))
                        isdcrsetupupdt = true;
                    if (Chem_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString()))
                        isdcrsetupupdt = true;
                    if (Stk_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()))
                        isdcrsetupupdt = true;
                    if (Hos_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString()))
                        isdcrsetupupdt = true;
                    if (doc_disp != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_reqd != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_Qty_zero != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_selection != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString()))
                        isdcrsetupupdt = true;
                    if (pob != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRSess_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRTime_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRProd_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayedSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayCalc != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayAllowDays != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString()))
                        isdcrsetupupdt = true;
                    if (iSundayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString()))
                        isdcrsetupupdt = true;
                    if (iApprovalSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString()))
                        isdcrsetupupdt = true;
                    if (remarkslength != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString()))
                        isdcrsetupupdt = true;
                    if (iDrRem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString()))
                        isdcrsetupupdt = true;
                    if (inewchem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString()))
                        isdcrsetupupdt = true;
                    if (inewudr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString()))
                        isdcrsetupupdt = true;
                    if (ProductSample_Valid != Convert.ToInt16(dsadmin.Tables[0].Rows[0]["Prod_SampleQty_Validation_Needed"].ToString()))
                        isdcrsetupupdt = true;
                    if (InputSample_Valid != Convert.ToInt16(dsadmin.Tables[0].Rows[0]["InputQty_Validation_Needed"].ToString()))
                        isdcrsetupupdt = true;

                    strQry = "UPDATE Admin_Setups_MGR " +
                               " SET Doctor_disp_in_Dcr = '" + doc_disp + "', "
                                + " No_Of_DCR_Drs_Count ='" + Doc_Count_DCR + "' ,"
                                + " No_Of_DCR_Chem_Count ='" + Chem_Count_DCR + "', "
                                + " No_Of_DCR_Stockist_Count ='" + Stk_Count_DCR + "', "
                                + " No_Of_DCR_Ndr_Count ='" + UnLstDr_Count_DCR + "', "
                                + " No_of_dcr_Hosp ='" + Hos_Count_DCR + "', "
                                + " DCRSess_Entry_Permission = '" + sess_dcr + "', "
                                + " NonDrNeeded = '" + UnLstDr_reqd + "', "
                                + " DCRTime_Entry_Permission = '" + time_dcr + "', "
                                + " SampleProQtyDefaZero = '" + prod_Qty_zero + "', "
                                + " No_Of_Product_selection_Allowed_in_Dcr = '" + prod_selection + "', "
                                + " POBType = '" + pob + "', "
                                + " DCRSess_Mand = '" + DCRSess_Mand + "', "
                                + " DCRTime_Mand = '" + DCRTime_Mand + "', "
                                + " DCRProd_Mand = '" + DCRProd_Mand + "', "
                                + " DelayedSystem_Required_Status ='" + iDelayedSystem + "', "
                                + " Delay_Holiday_Status ='" + iHolidayCalc + "', "
                                + " No_Of_Days_Delay ='" + iDelayAllowDays + "', "
                                + " AutoPost_Holiday_Status ='" + iHolidayStatus + "', "
                                + " Approval_System = " + iApprovalSystem + ", "
                                + " Remarks_length_Allowed='" + remarkslength + "',"
                                + " AutoPost_Sunday_Status ='" + iSundayStatus + "',"
                                + " DCRLDR_Remarks ='" + iDrRem + "', "
                                + " DCRNew_Chem ='" + inewchem + "', "
                                + " DCRNew_ULDr ='" + inewudr + "' ,"
                                + " wrk_area_Name ='" + strWorkAra + "',LockSystem_Needed='" + lock_sysyem + "',LockSystem_Timelimit='" + lock_time_limit + "',ProductFeedback_Needed='" + ProductFeedback_Needed + "',PrdRx_Qty_Needed='" + PrdRx_Qty_Needed + "',LstPOB_Updation='" + LstPOB_Updation + "',ChemPOB_Updation='" + ChemPOB_Updation + "',HalfDayWordEntryNeed='" + halfday_wrk + "',TpBased='" + strDCRTP + "',TpBasesd_DCR='" + TpBasesd_DCR + "',TPDCR_Deviation='" + TPDCR_Deviation + "',TPDCR_MGRAppr='" + TPDCR_MGRAppr + "',ChemPOB_Qty_Needed='" + ChemPOB_Qty_Needed + "',PrdRx_Qty_Caption='" + PrdRx_Qty_Caption + "',ChemPOB_Qty_Caption='" + ChemPOB_Qty_Caption + "',PrdSample_Qty_Needed='" + PrdSample_Qty_Needed + "',ChemSample_Qty_Needed='" + ChemSample_Qty_Needed + "',"
                                + " PrdSample_Qty_Caption='" + PrdSample_Qty_Caption + "',ChemSample_Qty_Caption='" + ChemSample_Qty_Caption + "',Dr_POBQty_DefaZero='" + Dr_POBQty_DefaZero + "',Chem_SampleQty_DefaZero='" + Chem_SampleQty_DefaZero + "',Chem_POBQty_DefaZero='" + Chem_POBQty_DefaZero + "',FieldForceWise_Delay='" + FFWiseDly + "',FFWise_Delay_Days='" + FFWiseDly_Days + "',Prod_SampleQty_Validation_Needed='" + ProductSample_Valid + "',InputQty_Validation_Needed='" + InputSample_Valid + "' where Division_Code = '" + Division_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);
                // strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strTPApprovalSystem + "' where Designation_Short_Name='DM'";

                if (isdcrsetupupdt == true)
                {
                    strQry = "UPDATE Admin_Setups_MGR set LastUpdt_DCRStp = getdate() where Division_Code = '" + Division_Code + "' ";
                }

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Screen_Lock_Add(string mgr_code, string sf_code, string div_code, int DCR_Lock, int TP_Lock, int SDP_Lock, int Camp_Lock, int DR_Lock, string Unlst, int Core_Lock)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsAdmin = null;

                strQry = "SELECT isnull(max([S.No])+1,'1') [S.No] from Screen_Lock ";
                int SNo = db.Exec_Scalar(strQry);

                strQry = "select count(sf_code) from Screen_Lock " +
                        "where sf_code='" + sf_code + "'  ";


                dsAdmin = db_ER.Exec_DataSet(strQry);
                if (dsAdmin.Tables[0].Rows[0][0].ToString() == "0")
                {
                    strQry = " INSERT INTO Screen_Lock([S.No],Mgr_Code, SF_Code,Div_Code,DCR_Lock,TP_Lock,SDP_Lock,Camp_Lock,DR_Lock,Update_dtm,Unlst_Cnt,Coredr_Lock) " +
                         " VALUES ( '" + SNo + "','" + mgr_code + "' , '" + sf_code + "' , '" + div_code + "', " + DCR_Lock + " , " + TP_Lock + ", " + SDP_Lock + ", " + Camp_Lock + ", " + DR_Lock + ", getdate(),'" + Unlst + "','" + Core_Lock + "' ) ";
                }
                else
                {
                    strQry = "update Screen_Lock set DCR_Lock='" + DCR_Lock + "',TP_Lock='" + TP_Lock + "',SDP_Lock='" + SDP_Lock + "',Camp_Lock='" + Camp_Lock + "',DR_Lock='" + DR_Lock + "',Unlst_Cnt='" + Unlst + "',Coredr_Lock='" + Core_Lock + "' where SF_Code='" + sf_code + "' ";

                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet get_UnreadMailCount(string Division_Code, string sf_Code)
        {
            DataSet dsAdmin = null;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select count(*)'Mail_Count' from Trans_Mail_Head H inner join Trans_Mail_Detail D on H.Trans_Sl_No=D.Trans_Sl_No " +
                          " where Open_Mail_Id='" + sf_Code + "' and D.Division_code='" + Division_Code + "' and  Sent_Flag=0 and Mail_Active_Flag=0" +
                          " and Month (Mail_Sent_Time) = Month(Getdate()) and year (Mail_Sent_Time) = year(Getdate()) ";

                dsAdmin = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsAdmin;
        }
        public DataSet get_TotalMailCount(string Division_Code, string sf_Code)
        {
            DataSet dsAdmin = null;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "select count(*)'Mail_Count' from Trans_Mail_Head H inner join Trans_Mail_Detail D on H.Trans_Sl_No=D.Trans_Sl_No " +
                          " where Open_Mail_Id='" + sf_Code + "' and D.Division_code='" + Division_Code + "' and  Sent_Flag=0 and Mail_Active_Flag in (0,10)" +
                          " and Month (Mail_Sent_Time) = Month(Getdate()) and year (Mail_Sent_Time) = year(Getdate()) ";

                dsAdmin = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsAdmin;
        }
        public DataSet Gettablet_PaySlip(string sf_Code, string Month, string Year, string tblDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserData = null;



            strQry = " select Paymonth,Payyear,p.Employee_Id,Employee_Name,Basic,House_Rent_Allowance,conveyance,City_Comp_Allowance,Metro,Education_Allowance,Special_Allowance," +
                     " ta,Grosspay,Present,Loss_of_Pay_Days,Pf_Ded,Esi_Ded,Incometaxdeduction,Prof_Tax_Ded,Tour_Advance_Ded,Totalded,Netsalary,Department_Name,Pf_Number, " +
                     " Division_Name,State_Name,Designation_Name,cast(DATE_OF_JOINING as varchar(10)) DATE_OF_JOINING,Pay_Month,Pay_Year,Other_Dedections,UAN_No,Esic_No,PF_Basic,Loans_if_Any,LTA,S.Bank_Name, " +
                     " Bank_AcNo,IFS_Code,Adv_against_statutory_Bonus from " +
                     " dbo.PaySlip_" + tblDivCode + " P inner join dbo.Mas_Salesforce S on P.Employee_Id=S.sf_emp_id  " +
                      "  where Paymonth='" + Month + "' and Payyear='" + Year + "'  and Sf_Code='" + sf_Code + "'";

            try
            {
                dsUserData = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserData;

        }

        public DataSet Gettablet_PaySlip_New(string sf_Code, string Month, string Year, string tblDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserData = null;



            strQry = " select Paymonth,Payyear,p.Employee_Id,Employee_Name,Basic,House_Rent_Allowance,conveyance,City_Comp_Allowance,Metro,Education_Allowance,Special_Allowance," +
                     " ta,Grosspay,Present,Loss_of_Pay_Days,Pf_Ded,Esi_Ded,Incometaxdeduction,Prof_Tax_Ded,Tour_Advance_Ded,Totalded,Netsalary,Department_Name,Pf_Number, " +
                     " Division_Name,State_Name,Designation_Name,cast(DATE_OF_JOINING as varchar(10)) DATE_OF_JOINING,Pay_Month,Pay_Year,Other_Dedections,UAN_No,Esic_No,PF_Basic,Loans_if_Any,LTA,S.Bank_Name, " +
                     " Bank_AcNo,IFS_Code,Adv_against_statutory_Bonus from " +
                     " dbo.PaySlip_" + tblDivCode + " P inner join dbo.Mas_Salesforce S on P.Employee_Id=S.sf_emp_id  " +
                      "  where Paymonth='" + Month + "' and Payyear='" + Year + "'  and p.Employee_Id='" + sf_Code + "'";

            try
            {
                dsUserData = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserData;

        }
        public DataSet Gettablet_PaySlip_Resigned(string sf_Code, string Month, string Year, string tblDivCode, string emp_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserData = null;



            strQry = " select Paymonth,Payyear,p.Employee_Id,Employee_Name,Basic,House_Rent_Allowance,conveyance,City_Comp_Allowance,Metro,Education_Allowance,Special_Allowance," +
                     " ta,Grosspay,Present,Loss_of_Pay_Days,Pf_Ded,Esi_Ded,Incometaxdeduction,Prof_Tax_Ded,Tour_Advance_Ded,Totalded,Netsalary,Department_Name,Pf_Number, " +
                     " Division_Name,State_Name,Designation_Name,cast(DATE_OF_JOINING as varchar(10)) DATE_OF_JOINING,Pay_Month,Pay_Year,Other_Dedections,UAN_No,Esic_No,PF_Basic,Loans_if_Any,LTA," +
                     "case when P.Employee_Id=S.sf_emp_id then S.Bank_Name else '' end as Bank_Name, " +
                     " case when P.Employee_Id=S.sf_emp_id then Bank_AcNo else '' end as Bank_AcNo,case when P.Employee_Id=S.sf_emp_id then IFS_Code else '' end as IFS_Code,Adv_against_statutory_Bonus from " +
                     " dbo.PaySlip_" + tblDivCode + " P inner join dbo.Mas_Salesforce S on P.Employee_Id='" + emp_code + "'  " +
                      "  where Paymonth='" + Month + "' and Payyear='" + Year + "'  and Sf_Code='" + sf_Code + "'";

            try
            {
                dsUserData = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserData;

        }

        public int RecordUpdate_DesigMGRMailcrn(int StrId, string strDesignation, string divcode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_SF_Designation set Mail_Allowed='" + StrId + "' where Designation_Short_Name='" + strDesignation + "' and Division_Code='" + divcode + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate_MobAppNew(int GeoChk, int GEOTagNeed,
        float DisRad, string DrCap,
        string ChmCap, string StkCap,
        string NLCap, int DPNeed,
        int rdoinput_Ent_doc, int ChmNeed,
        int CPNeed, string ChmQCap,
        int CINeed, int StkNeed,
        int SPNeed, string StkQCap,
        int inpu_entry_stock, int UNLNeed,
        int NPNeed, string NLRxQCap,
        string NLSmpQCap, int NINeed,
        string Division_Code, int DeviceId_Need,
         int Radio2,
        int Radio3, int Radio4,
        int Radio5, int Radio6,
        int Radio7, int Radio8,
        int Radio9, int Radio10,
        string txtrxqty, string txtSamQty_Cap_doc,
        int Radio11, int Radio14,
        //int Radio15,
        int Radio16,
        int Radio31, int Radio32,
        string txt_srtdate, string txt_enddate,
        int Radio33, int Radio34,
        int Radio35,
        int Radio38,
        string cluster, int Radio40,
        //int Radio41, 
        int Radio42,
        //int Radio43,
        int Radio44,
        int Radio46,
        string txtDpc, string txtCPC,
        string txtSPC, string txtUPC,
        int Radio85, int Radio86,
        int Radio87, int Radio88,
        int Radio81, int Radio82,
        int Radio83, int Radio84,
        int Radio75, int Radio76,
        int Radio77, int Radio78,
        int Radio79, int Radio80,
        int Radio47, int Radio48,
        int Radio49,
        int Radio50, int Radio51,
        int Radio52, int Radio53,
        int Radio54, int Radio55, int Radio56,
        //int Radio57, 
        int Radio58,
        //int Radio59, 
        //int Radio60,
        //int Radio61,
        int Radio63, int Radio64,
        int Radio65, int Radio66,
        int Radio67, int Radio68,
        int Radio69, int Radio70,
        //int Radio71, 
        int Radio72,
        int Radio90, int Radio91,
        int Radio17, int Radio89, int Radio92,
        int RadioBtnList1,
        //int RadioBtnList3,
        int RadiochmsamQty_need,
        int RadioPwdsetup, int Radioexpenseneed,
        int Radiochm_ad_qty, int RadioCampneed,
        int RadioApproveneed, string Doc_Input_caption,
        string Chm_Input_caption, string Stk_Input_caption,
        string Ul_Input_caption, int RadioChmRxNd,
        int RadioDrSampNd, int RadioCmpgnNeed,
        int RadiorefDoc,
        //int RadioCHEBase,
        int Radiotp_need,
        //int RadiocurrentDay,
        int RadiocntRemarks, int Radiopast_leave_post,
        int RadiomyplnRmrksMand,
        int Radioprod_remark, int Radioprod_remark_md,
        int Radiostp, int RadioRemainder_geo,
        //int Radiodcr_edit_rej,
        int RadioDrRcpaQMd,
        int RadiogeoTagImg, int RadioHINeed,
        int Radiohosp_need, int RadioHPNeed,
        int Radioprdfdback,
        //int Radioques_need,
        //int Radiorcpaextra, 
        string pob_minvalue,
        int RadioRcpaMd, int RadioRcpa_Competitor_extra,
        string GeoTagCap, int rdoExpenceNd,
        int rdoExpenceNd_mandatory, int RadioRcpaNd,
        int Radioleavestatus, int rdoOrder_management,
        string txtOrder_caption, int rdoPrimary_order,
        string txtPrimary_order_caption, int rdoSecondary_order,
        string txtSecondary_order_caption, int rdoGst_option, string txtTaxname_caption,
        int rdosecondary_order_discount, int Radiomisc_expense_need,
        //int RadioentryFormNeed_entryFormMgr, 
        int Radiosurveynd,
        int Radiodashboard

)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Access_Master " +
                               " SET GeoChk= '" + GeoChk + "',GEOTagNeed='" + GEOTagNeed + "', "
                                + " DisRad = '" + DisRad + "',"
                                + " DrCap ='" + DrCap + "' ,"
                                + " ChmCap ='" + ChmCap + "', "
                                + " StkCap ='" + StkCap + "', "
                                + " NLCap ='" + NLCap + "', "
                                + " DPNeed ='" + DPNeed + "', "
                                //+ " DINeed = '" + DINeed + "', "
                                //+ " DrSmpQCap = '" + rdoinput_Ent_doc + "', "
                                + " DINeed = '" + rdoinput_Ent_doc + "', "
                                + " ChmNeed = '" + ChmNeed + "', "
                                + " CPNeed = '" + CPNeed + "', "
                                + " ChmQCap = '" + ChmQCap + "', "
                                + " CINeed = '" + CINeed + "', "
                                + " StkNeed = '" + StkNeed + "', "
                                + " SPNeed = '" + SPNeed + "', "
                                + " StkQCap = '" + StkQCap + "', "
                                + " SINeed='" + inpu_entry_stock + "', "
                                + " UNLNeed ='" + UNLNeed + "', "
                                + " NPNeed ='" + NPNeed + "', "
                                + " NLRxQCap ='" + NLRxQCap + "', "
                                + " NLSmpQCap ='" + NLSmpQCap + "', "
                                + " NINeed =' " + NINeed + "' ,"
                                + " DeviceId_Need ='" + DeviceId_Need + "' ,"
                                + "MsdEntry='" + Radio2 + "',"
                                + "VstNd='" + Radio3 + "',"
                                + "mailneed='" + Radio4 + "',"
                                + "circular='" + Radio5 + "',"
                                + "DrFeedMd='" + Radio6 + "',"
                                + "DrPrdMd='" + Radio7 + "',"
                                + "DrInpMd='" + Radio8 + "',"
                                + "DrSmpQMd='" + Radio9 + "',"
                                + "DrRxQMd='" + Radio10 + "',"
                                + "DrRxQCap='" + txtrxqty + "',"
                                + "DrSmpQCap='" + txtSamQty_Cap_doc + "', "
                               + "Doc_Pob_Mandatory_Need='" + Radio11 + "',"
                                + "srtnd='" + Radio14 + "',"
                                //+ "MCLDet='" + Radio15 + "',"
                                + "Chm_Pob_Mandatory_Need='" + Radio16 + "',"
                                + "DrRxNd='" + Radio17 + "',"
                                + "TPDCR_Deviation='" + Radio31 + "',"
                                + "TP_Mandatory_Need='" + Radio32 + "',"
                                + "Tp_Start_Date='" + txt_srtdate + "',"
                                + "Tp_End_Date='" + txt_enddate + "',"
                                + "TPbasedDCR='" + Radio33 + "',"
                                + "NextVst='" + Radio34 + "',"
                                + "NextVst_Mandatory_Need='" + Radio35 + "',"
                                + "multiple_doc_need='" + Radio38 + "',"
                                + "Cluster_Cap='" + cluster + "',"
                                + "allProdBd='" + Radio40 + "',"
                                //+ "Speciality_prod='" + Radio41 + "',"
                                + "FcsNd='" + Radio42 + "',"
                                //+ "Prod_Stk_Need='" + Radio43 + "',"
                                + "OtherNd='" + Radio44 + "',"
                                + "Sep_RcpaNd='" + Radio46 + "',"
                                + "Ul_Product_caption='" + txtUPC + "',"
                                + "Stk_Product_caption='" + txtSPC + "',"
                                + "Chm_Product_caption='" + txtCPC + "',"
                                + "Doc_Product_caption='" + txtDpc + "' ,"
                                + "doctor_dobdow='" + Radio47 + "',"
                                + "Appr_Mandatory_Need='" + Radio48 + "' ,"
                               + "DlyCtrl='" + Radio49 + "' ,"
                                + "cip_need='" + Radio50 + "' ,"
                                + "DFNeed='" + Radio51 + "' ,"
                                + "CFNeed='" + Radio52 + "',"
                                + "SFNeed='" + Radio53 + "' ,"
                                + "CIP_FNeed='" + Radio54 + "' ,"
                                + "NFNeed='" + Radio55 + "' ,"
                                + "HFNeed='" + Radio56 + "' ,"
                                //+ "DQNeed='" + Radio57 + "' ,"
                                + "CQNeed='" + Radio58 + "',"
                                //+ "SQNeed='" + Radio59 + "' ,"
                                //+ "NQNeed='" + Radio60 + "',"
                                //+ "CIP_QNeed='" + Radio61 + "',"
                                + "DENeed='" + Radio63 + "' ,"
                                + "CENeed='" + Radio64 + "',"
                                + "SENeed='" + Radio65 + "' ,"
                                + "NENeed='" + Radio66 + "',"
                                + "CIP_ENeed='" + Radio67 + "' ,"
                                + "HENeed='" + Radio68 + "',"
                                + "quiz_need='" + Radio69 + "' ,"
                                + "pro_det_need='" + Radio70 + "',"
                                //+ "prdfdback='" + Radio71 + "' ,"
                                + "mediaTrans_Need='" + Radio72 + "',"
                                + "Doc_Pob_Need='" + Radio75 + "' ,"
                                + "Chm_Pob_Need='" + Radio76 + "' ,"
                                + "Stk_Pob_Need='" + Radio77 + "' ,"
                                + "Ul_Pob_Need='" + Radio78 + "' ,"
                                + "Stk_Pob_Mandatory_Need='" + Radio79 + "' ,"
                                + "Ul_Pob_Mandatory_Need='" + Radio80 + "',"
                                + "Doc_jointwork_Need='" + Radio81 + "' ,"
                                + "Chm_jointwork_Need='" + Radio82 + "' ,"
                                + "Stk_jointwork_Need='" + Radio83 + "' ,"
                                + "Ul_jointwork_Need='" + Radio84 + "',"
                                + "Doc_jointwork_Mandatory_Need='" + Radio85 + "',"
                                + "Chm_jointwork_Mandatory_Need='" + Radio86 + "',"
                                + "Stk_jointwork_Mandatory_Need='" + Radio87 + "',"
                                + "Ul_jointwork_Mandatory_Need='" + Radio88 + "' ,"
                                + "CIP_PNeed='" + Radio90 + "',"
                                + "CIP_INeed='" + Radio91 + "',"
                                + "Prodfd_Need='" + Radio89 + "',"
                                + "TPDCR_MGRAppr='" + Radio92 + "', MissedDateMand='" + RadioBtnList1 + "', "
                                //+ "TempNd='" + RadioBtnList3 + "',"
                                + "chmsamQty_need='" + RadiochmsamQty_need + "',"
                                + "Pwdsetup='" + RadioPwdsetup + "',"
                                + "expenseneed='" + Radioexpenseneed + "',"
                                + "chm_ad_qty='" + Radiochm_ad_qty + "',"
                                + "Campneed='" + RadioCampneed + "',"
                                + "Approveneed='" + RadioApproveneed + "',"
                                + "Doc_Input_caption='" + Doc_Input_caption + "',"
                                + "Chm_Input_caption='" + Chm_Input_caption + "',"
                                + "Stk_Input_caption='" + Stk_Input_caption + "',"
                                + "Ul_Input_caption='" + Ul_Input_caption + "',"
                                + "ChmRxNd='" + RadioChmRxNd + "',"
                                 + "DrSampNd='" + RadioDrSampNd + "',"
                                + "CmpgnNeed='" + RadioCmpgnNeed + "',"
                                + "refDoc='" + RadiorefDoc + "',"
                                //+ "CHEBase='" + RadioCHEBase + "',"
                                + "tp_need='" + Radiotp_need + "',"
                                //+ "currentDay='" + RadiocurrentDay + "',"
                                + "cntRemarks='" + RadiocntRemarks + "',"
                                + "past_leave_post='" + Radiopast_leave_post + "',"
                                + "myplnRmrksMand='" + RadiomyplnRmrksMand + "',"
                                + "prod_remark='" + Radioprod_remark + "',"
                                + "prod_remark_md='" + Radioprod_remark_md + "',"
                                + "stp='" + Radiostp + "',"
                                + "Remainder_geo='" + RadioRemainder_geo + "',"
                                //+ "dcr_edit_rej='" + Radiodcr_edit_rej + "',"
                                + "DrRcpaQMd='" + RadioDrRcpaQMd + "',"
                                + "geoTagImg='" + RadiogeoTagImg + "',"
                                + "HINeed='" + RadioHINeed + "',"
                                + "hosp_need='" + Radiohosp_need + "',"
                                + "HPNeed='" + RadioHPNeed + "',"
                                + "prdfdback='" + Radioprdfdback + "',"
                                //+ "ques_need='" + Radioques_need + "',"
                                //+ "rcpaextra='" + Radiorcpaextra + "',"
                                + "pob_minvalue='" + pob_minvalue + "',"
                                + "RcpaMd='" + RadioRcpaMd + "',"
                                + "Rcpa_Competitor_extra='" + RadioRcpa_Competitor_extra + "',"

                                //Updated parameters
                                + "geotag_caption='" + GeoTagCap + "',"
                                + "ExpenceNd='" + rdoExpenceNd + "',"
                                + "ExpenceNd_mandatory='" + rdoExpenceNd_mandatory + "',"
                                + "RcpaNd='" + RadioRcpaNd + "',"
                                + "leavestatus='" + Radioleavestatus + "',"
                                + "Order_management='" + rdoOrder_management + "',"
                                + "Order_caption='" + txtOrder_caption + "',"
                                + "Primary_order='" + rdoPrimary_order + "',"
                                + "Primary_order_caption='" + txtPrimary_order_caption + "',"
                                + "Secondary_order='" + rdoSecondary_order + "',"
                                + "Secondary_order_caption='" + txtSecondary_order_caption + "',"
                                + "Gst_option='" + rdoGst_option + "',"
                                + "Taxname_caption='" + txtTaxname_caption + "',"
                                + "secondary_order_discount='" + rdosecondary_order_discount + "',"
                                + "misc_expense_need='" + Radiomisc_expense_need + "',"
                                //+ "entryFormNeed='" + RadioentryFormNeed_entryFormMgr + "',"
                                //+ "entryFormMgr='" + RadioentryFormNeed_entryFormMgr + "',"
                                + "surveynd='" + Radiosurveynd + "',"
                                + "dashboard='" + Radiodashboard + "' "

                                + " where Division_Code = '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int RecordUpdate_MobAppNew2(int rdoTerr_based_Tag, int RadioProd_Stk_Need, int RadioDrEvent_Md, int RadioMCLDet, string txtChmSmpCap,
            int RadioStkEvent_Md, string txtcip_caption, int RadioCIPPOBNd, int RadioCIPPOBMd, int RadioCipEvent_Md, string txthosp_caption, int RadioHosPOBNd, int RadioHosPOBMd, int RadioHospEvent_Md, int RadioUlDrEvent_Md, int RadioRcpaMd_Mgr, int RadioDrRCPA_competitor_Need,
            int RadioRCPAQty_Need, int RadioChm_RCPA_Need, int RadioChmRcpaMd, int RadioChmRcpaMd_Mgr, int Radiosequential_dcr, string txtDcrLockDays,
            int Radiomydayplan_need, int RadioLeave_entitlement_need, int rdoProduct_Rate_Editable, int Radiomulti_cluster, int RadioCustSrtNd,
            int RadioTarget_report_Nd, int Radiofaq, int RadioRmdrNeed, int RadioChmRCPA_competitor_Need, int RadioSpecFilter, int RadioCurrentday_TPplanned, int RadioChmEvent_Md,
            string div_code)
        {
            {
                int iReturn = -1;

                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "UPDATE Access_Master " +
                                   " SET Terr_based_Tag= '" + rdoTerr_based_Tag + "', "
                                    + "Prod_Stk_Need= '" + RadioProd_Stk_Need + "', "
                                    + " DrEvent_Md= '" + RadioDrEvent_Md + "', "
                                    + " MCLDet='" + RadioMCLDet + "', "
                                    + " ChmSmpCap='" + txtChmSmpCap + "', "
                                    + " StkEvent_Md='" + RadioStkEvent_Md + "', "
                                    + " cip_caption='" + txtcip_caption + "', "
                                    + " CIPPOBNd='" + RadioCIPPOBNd + "', "
                                    + " CIPPOBMd='" + RadioCIPPOBMd + "', "
                                    + " CipEvent_Md='" + RadioCipEvent_Md + "', "
                                    + " hosp_caption='" + txthosp_caption + "', "
                                    + " HosPOBNd='" + RadioHosPOBNd + "', "
                                    + " HosPOBMd='" + RadioHosPOBMd + "', "
                                    + " HospEvent_Md='" + RadioHospEvent_Md + "', "
                                    + " UlDrEvent_Md='" + RadioUlDrEvent_Md + "', "
                                    + " RcpaMd_Mgr='" + RadioRcpaMd_Mgr + "', "
                                    + " DrRCPA_competitor_Need='" + RadioDrRCPA_competitor_Need + "', "
                                    + " RCPAQty_Need='" + RadioRCPAQty_Need + "', "
                                    + " Chm_RCPA_Need='" + RadioChm_RCPA_Need + "', "
                                    + " ChmRcpaMd='" + RadioChmRcpaMd + "', "
                                    + " ChmRcpaMd_Mgr='" + RadioChmRcpaMd_Mgr + "', "
                                    + " sequential_dcr='" + Radiosequential_dcr + "', "
                                    + " DcrLockDays='" + txtDcrLockDays + "', "
                                    + " mydayplan_need='" + Radiomydayplan_need + "', "
                                    + " Leave_entitlement_need='" + RadioLeave_entitlement_need + "', "
                                    + " Product_Rate_Editable='" + rdoProduct_Rate_Editable + "', "
                                    + " multi_cluster='" + Radiomulti_cluster + "', "
                                    + " CustSrtNd='" + RadioCustSrtNd + "', "
                                    + " Target_report_Nd='" + RadioTarget_report_Nd + "', "
                                    + " faq='" + Radiofaq + "', "
                                    + " RmdrNeed='" + RadioRmdrNeed + "', "
                                    + " SpecFilter='" + RadioSpecFilter + "', "
                                    + " Currentday_TPplanned='" + RadioCurrentday_TPplanned + "', "
                                    + " ChmEvent_Md='" + RadioChmEvent_Md + "', "
                                    + " ChmRCPA_competitor_Need='" + RadioChmRCPA_competitor_Need + "' "
                                    + " where Division_Code = '" + div_code + "' ";
                    iReturn = db.ExecQry(strQry);
                }


                catch (Exception ex)
                {
                    throw ex;
                }

                return iReturn;
            }
        }

        public int RecordUpdate_MobAppNewCustom(int RadioProduct_Stockist, int Radiohosp_filter, int RadioDetailing_chem, int RadioDetailing_stk, int RadioDetailing_undr, int RadioDcrapprvNd, int RadioaddChm, int RadioaddDr, int Radioundr_hs_nd, int RadioaddAct,
            int RadioshowDelete, int RadioPresentNd, int RadioCustNd, int Radioyetrdy_call_del_Nd, int Radiotheraptic,
            string div_code)
        {
            int iReturnTP = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE custom_table " +
                               " SET hosp_filter= '" + Radiohosp_filter + "',"
                              + "addDr='" + RadioaddDr + "', "
                              + "showDelete='" + RadioshowDelete + "',"
                              + "Detailing_chem='" + RadioDetailing_chem + "',"
                              + "Detailing_stk='" + RadioDetailing_stk + "',"
                              + "Detailing_undr='" + RadioDetailing_undr + "',"
                              + "addChm='" + RadioaddChm + "',"
                              + "DcrapprvNd='" + RadioDcrapprvNd + "',"
                              + "Product_Stockist='" + RadioProduct_Stockist + "',"
                              + "undr_hs_nd='" + Radioundr_hs_nd + "',"
                              //+ "Target_sales='" + RadioTarget_sales + "',"
                              + "addAct='" + RadioaddAct + "' "
                              + " where division_code = '" + div_code + "'";

                iReturnTP = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }
            return iReturnTP;
        }

        public DataSet App_Usage_View(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "Exec App_Usage '" + divcode + "'";

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
        public int InsertAuditSetup(string txtSupport, string txtDateTime, string txtremark, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                DataSet dsDiv = null;
                strQry = " select Division_Name from Mas_Division WHERE Division_Code= '" + div_code + "'";
                dsDiv = db.Exec_DataSet(strQry);
                string div_Name = dsDiv.Tables[0].Rows[0]["Division_Name"].ToString();

                strQry = string.Empty;
                strQry = "insert into audit_setup(Support_person_name, Access_datetime, Remark, Division_name, Division_code, Actual_created_datetime)" +
                " VALUES('" + txtSupport + "','" + txtDateTime + "','" + txtremark + "','" + div_Name + "','" + div_code + "',getdate()) ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getMailDetails(string Trans_Sl_No)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (select Sf_Name from Mas_Salesforce a where t.Open_Mail_Id=a.Sf_Code)FieldForceName,Mail_Read_Date from Trans_Mail_Detail t  where Trans_Sl_No='" + Trans_Sl_No + "'and Mail_Read_Date is not null";

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



        public DataSet getVacant_List(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
             
            strQry = "EXEC sp_SalesForceVacMrAgainstMgr '" + div_code + "' ,  '" + sf_code + "' ";
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

        //Added By Gowri
        public int UpdateSample_AS_ON_Date(string sf_code, string div_code, int Despatch_Qty, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "UPDATE Trans_Sample_Stock_FFWise_AsonDate SET  Sample_AsonDate=" + Despatch_Qty + " where cast(Division_Code as varchar) = '" + div_code + "' and SF_Code = '" + sf_code + "' and Prod_Detail_Sl_No = '" + product_code + "' ";

            iReturn = db_ER.ExecQry(strQry);


            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int UpdateInput_AS_ON_Date(string sf_code, string div_code, int Despatch_Qty, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "UPDATE Trans_Input_Stock_FFWise_AsonDate SET  InputQty_AsonDate=" + Despatch_Qty + " where cast(Division_Code as varchar) = '" + div_code + "' and SF_Code = '" + sf_code + "' and Gift_Code = '" + product_code + "' ";

            iReturn = db_ER.ExecQry(strQry);


            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getsample_AsonDate(string sf_code, string div_code, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select Sample_AsonDate,Prod_Detail_Sl_No from Trans_Sample_Stock_FFWise_AsonDate where cast(Division_Code as varchar) ='" + div_code + "'  and SF_Code ='" + sf_code + "'  and Prod_Detail_Sl_No = '" + product_code + "' ";
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


        public DataSet getinput_AsonDate(string sf_code, string div_code, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select InputQty_AsonDate,Gift_Code from Trans_Input_Stock_FFWise_AsonDate where cast(Division_Code as varchar) ='" + div_code + "'  and SF_Code ='" + sf_code + "'  and Gift_Code = '" + product_code + "' ";
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
        public int InsertInput_AS_ON_Date(string sf_code, string div_code, string Despatch_Qty, string product_code, string giftname)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "Insert Trans_input_Stock_FFWise_AsonDate (SF_Code,Gift_Code,Gift_SName,InputQty_AsonDate,Division_Code) values('" + sf_code + "','" + product_code + "','" + giftname + "','" + Despatch_Qty + "','" + div_code + "') ";

            // iReturn = db_ER.ExecQry(strQry);


            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int InsertSample_AS_ON_Date(string sf_code, string div_code, string Despatch_Qty, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "Insert Trans_Sample_Stock_FFWise_AsonDate (SF_Code,Prod_Detail_Sl_No,Sample_AsonDate,Division_Code) values('" + sf_code + "','" + product_code + "','" + Despatch_Qty + "','" + div_code + "') ";

            // iReturn = db_ER.ExecQry(strQry);


            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        // mary -start -23/11/2022
        public DataSet getRange(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }


            strQry = "SELECT slno,From_Range,To_Range From Mas_DrBusiness_Range " +
                     "WHERE Division_Code='" + div_code + "'" +
                     "Order by slno";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public int RangeAdd1(string txtfrmrange, string txttorange, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "INSERT INTO Mas_DrBusiness_Range (slno,From_Range,To_Range,Division_Code,Created_date)" +
                         "values((SELECT max(isnull (slno,0))+1 [slno] from Mas_DrBusiness_Range),'" + txtfrmrange + "' ,'" + txttorange + "' , '" + div_code + "',getdate())";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RangeAdd2(string txtfrmrange, string txttorange, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "INSERT INTO Mas_DrBusiness_Range (From_Range,To_Range,Division_Code,Created_date)" +
                         "values('" + txtfrmrange + "' ,'" + txttorange + "' , '" + div_code + "',getdate())";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RangeUpdate1(int slno, string Fromrange, string Torange, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE Mas_DrBusiness_Range " +
                          " SET From_Range = '" + Fromrange + "',To_Range = '" + Torange + "'" +
                          " WHERE slno = '" + slno + "' and Division_Code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        //end


        #region App Setup version By Ferooz

        public DataSet getting_mob_app_record2New(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "select TPDCR_Deviation,TP_Mandatory_Need,Tp_Start_Date,Tp_End_Date,dayplan_tp_based, NextVst,NextVst_Mandatory_Need,Appr_Mandatory_Need,RCPAQty_Need,multiple_doc_need,Cluster_Cap," +
                     "allProdBd,Speciality_prod,FcsNd,Prod_Stk_Need,OtherNd,DlyCtrl,Sep_RcpaNd,doctor_dobdow,Appr_Mandatory_Need,DlyCtrl," +
                     "cip_need,DFNeed,CFNeed,SFNeed,CIP_FNeed,NFNeed,HFNeed,DQNeed,CQNeed, SQNeed," +
                     "NQNeed,CIP_QNeed,HQNeed, DENeed,CENeed, SENeed,NENeed,CIP_ENeed,HENeed,quiz_need," +
                     "pro_det_need,Prodfd_Need,mediaTrans_Need, SpecFilter,Doc_Pob_Need,Chm_Pob_Need,Stk_Pob_Need,Ul_Pob_Need, Stk_Pob_Mandatory_Need,Ul_Pob_Mandatory_Need," +
                     "Doc_jointwork_Need, Chm_jointwork_Need,Stk_jointwork_Need,Ul_jointwork_Need,  Doc_jointwork_Mandatory_Need,Chm_jointwork_Mandatory_Need,Stk_jointwork_Mandatory_Need,Ul_jointwork_Mandatory_Need,Doc_Product_caption, Chm_Product_caption," +
                     "Stk_Product_caption,Ul_Product_caption,DrFeedMd,TPDCR_MGRAppr,MissedDateMand,RmdrNeed,TempNd,isnull(mydayplan_need,'1')as mydayplan_need,isnull(chmsamQty_need,'1')as chmsamQty_need," +
                     "isnull(Pwdsetup,'1')as Pwdsetup,isnull(ExpenceNd,'1')as ExpenceNd, isnull(ExpenceNd_mandatory,'1')as ExpenceNd_mandatory," +
                     "isnull(Catneed,'1')as Catneed, isnull(chm_ad_qty,'1')as chm_ad_qty,isnull(Campneed,'1')as Campneed, isnull(Approveneed,'1')as Approveneed," +
                     "Doc_Input_caption,Chm_Input_caption,Stk_Input_caption,Ul_Input_caption,isnull(ChmRxNd,'1')as ChmRxNd,isnull(DrSampNd,'0')as DrSampNd," +
                     "isnull(CmpgnNeed,'1')as CmpgnNeed,isnull(entryFormNeed,'1')as entryFormNeed, isnull(refDoc,'1')as refDoc,isnull(CHEBase,'1')as CHEBase," +
                     "isnull(TPDCR_Deviation_Appr_Status,'1')as TPDCR_Deviation_Appr_Status, isnull(tp_need,'1')as tp_need,isnull(currentDay,'1')as currentDay," +
                     "isnull(cntRemarks,'1')as cntRemarks,isnull(past_leave_post,'1')as past_leave_post,isnull(myplnRmrksMand,'1')as myplnRmrksMand," +
                     "isnull(entryFormMgr,'1')as entryFormMgr, isnull(prod_remark,'1')as prod_remark,  isnull(prod_remark_md,'1')as prod_remark_md," +
                     "isnull(stp,'1')as stp,isnull(Remainder_geo,'1')as Remainder_geo, isnull(dcr_edit_rej,'1')as dcr_edit_rej," +
                     "isnull(past_leave_post,'1')as past_leave_post,isnull(entryFormNeed,'1')as entryFormNeed,isnull(call_feed_enterable,'0')as call_feed_enterable," +
                     "isnull(DrRcpaQMd,'0')as DrRcpaQMd,isnull(expense_need,'1')as expense_need,isnull(geoTagImg,'1')as geoTagImg," +
                     "isnull(HINeed,'1')as HINeed,isnull(hosp_need,'1')as hosp_need,isnull(HPNeed,'1')as HPNeed," +
                     "isnull(prdfdback,'1')as prdfdback,isnull(ques_need,'1')as ques_need,isnull(rcpaextra,'1')as rcpaextra,pob_minvalue,isnull(RcpaMd, '1') as RcpaMd,isnull(Rcpa_Competitor_extra, '1') as Rcpa_Competitor_extra, " +

                     "geotag_caption, ExpenceNd, ExpenceNd_mandatory, RcpaNd,leavestatus, Order_management,Order_caption, Primary_order, Primary_order_caption, Secondary_order, Secondary_order_caption, Gst_option,Taxname_caption, secondary_order_discount, misc_expense_need, entryFormNeed, entryFormMgr, surveynd,dashboard,expenseneed, " +
                     " hosp_caption, CIP_Caption,TPbasedDCR,prod_det_need " +
                     " from access_master  where division_code='" + divcode + "'";

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

        public DataSet getting_mob_app_record2NewTP(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdminTP = null;
            strQry = "select AddsessionNeed,AddsessionCount,DrNeed,ChmNeed,JWNeed,ClusterNeed,clustertype,div,StkNeed,Cip_Need,HospNeed,FW_meetup_mandatory,tp_objective, " +
                    " Holiday_Editable,Weeklyoff_Editable " +
                    " from  tpsetup where div ='" + divcode + "'";

            try
            {
                dsAdminTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdminTP;
        }
        public DataSet getting_mob_app_record3New(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "select Prod_Stk_Need,DrEvent_Md,MCLDet,ChmSmpCap,ChmQCap,StkEvent_Md,CIPPOBNd,CIPPOBMd,CipEvent_Md,edit_holiday,edit_weeklyoff,UlDrEvent_Md,HosPOBNd, " +
                " HosPOBMd,HospEvent_Md,RcpaMd_Mgr,DrRCPA_competitor_Need,Chm_RCPA_Need,ChmRcpaMd,ChmRcpaMd_Mgr,sequential_dcr,DcrLockDays,mydayplan_need,Product_rate_editable, " +
                " faq,CustSrtNd,RmdrNeed,Target_report_Nd,myplnRmrksMand,Leave_entitlement_need,multi_cluster,CIP_Caption,hosp_caption,RCPAQty_Need," +
                "ChmRCPA_competitor_Need,Terr_based_Tag,SpecFilter,Currentday_TPplanned,ChmEvent_Md,Territory_VstNd,Dcr_firstselfie, " +
                " CIP_jointwork_Need,CipSrtNd,Remainder_call_cap,quiz_heading,Remainder_prd_Md,Location_track,tracking_interval,travelDistance_Need, " +
                " ActivityNd,ActivityCap,quiz_need_mandt,primarysec_need,Target_report_md,tracking_time " +
                "from access_master where division_code='" + divcode + "'";

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

        public DataSet getting_mob_app_recordCustom(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "select hosp_filter, addDr,showDelete,	Detailing_chem,	Detailing_stk,	Detailing_undr,	addChm,	DcrapprvNd,Product_Stockist,undr_hs_nd,Target_sales,addAct,PresentNd,CustNd,yetrdy_call_del_Nd,theraptic from " +
                " custom_table where division_code='" + divcode + "'";

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

        public DataSet getting_mob_app_record(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select RcpaNd,MsdEntry,VstNd,mailneed,circular,FeedNd,DrPrdMd,DrInpMd,DrSmpQMd,DrRxQMd,DrRxNd, " +
                    "DrRxQCap,DrSmpQCap,Doc_Pob_Mandatory_Need,Attendance,MCLDet,Chm_Pob_Mandatory_Need,CIP_PNeed,CIP_INeed,srtnd from access_master where division_code='" + divcode + "'";
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

        public DataSet getMobApp_Setting(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select GeoChk,GEOTagNeed,DisRad,DrCap,ChmCap,StkCap, NLCap,DPNeed,DrRxQCap,DrSmpQCap,DINeed,ChmNeed,CPNeed, " +
                     " ChmQCap,CINeed,StkNeed,SPNeed,StkQCap,SINeed,UNLNeed,NPNeed,NLRxQCap,NLSmpQCap,NINeed,DeviceId_Need,CIP_PNeed,CIP_INeed,TPDCR_MGRAppr from Access_Master " +
                     " where Division_Code = '" + divcode + "'";



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
        public DataSet getMobApp_Setting_halfday(string divcode, string WorkType_Code_B)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "  select Hlfdy_flag,WorkType_Code_B from Mas_WorkType_BaseLevel where Division_Code='" + divcode + "' and WorkType_Code_B='" + WorkType_Code_B + "' ";

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

        public DataSet getMobApp_geo(string sf_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select GeoNeed,sf_code,GeoFencing,GeoFencingche,GeoFencingstock,GeoFencingulDr,GeoFencingcip from Access_Table where sf_code='" + sf_code + "' ";

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

        public DataSet gethalf_Daywrk(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select WorkType_Code_B,Worktype_Name_B from Mas_WorkType_BaseLevel " +
                    " where Division_Code='" + div_code + "' and active_flag=0 and fieldwork_indicator='N'";
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

        public int RecordUpdate_MobAppNew(int GeoChk,
         //int rdogeo,
         float DisRad, string DrCap,
         string ChmCap, string StkCap,
         string NLCap, int DPNeed,
         int rdoinput_Ent_doc, int ChmNeed,
         int CPNeed, string ChmQCap,
         int CINeed, int StkNeed,
         int SPNeed, string StkQCap,
         int inpu_entry_stock, int UNLNeed,
         int NPNeed, string NLRxQCap,
         string NLSmpQCap, int NINeed,
         string Division_Code, int DeviceId_Need,
          int Radio2,
         int Radio3, int Radio4,
         int Radio5, int Radio6,
         int Radio7, int Radio8,
         int Radio9, int Radio10,
         string txtrxqty, string txtSamQty_Cap_doc,
         int Radio11, int Radio14,
         //int Radio15,
         int Radio16,
         int Radio31, int Radio32,
         string txt_srtdate, string txt_enddate,
         int Radio33, int Radio34,
         int Radio35,
         int Radio38,
         string cluster,
         //int Radio40,
         //int Radio41, 
         int Radio42,
         //int Radio43,
         //int Radio44,
         int Radio46,
         string txtDpc, string txtCPC,
         string txtSPC, string txtUPC,
         int Radio85, int Radio86,
         int Radio87, int Radio88,
         int Radio81, int Radio82,
         int Radio83, int Radio84,
         int Radio75, int Radio76,
         int Radio77, int Radio78,
         int Radio79, int Radio80,
         int Radio47, int Radio48,
         int Radio49,
         int Radio50, int Radio51,
         int Radio52, int Radio53,
         int Radio54, int Radio55, int Radio56,
         //int Radio57, 
         int Radio58,
         //int Radio59, 
         //int Radio60,
         //int Radio61,
         int Radio63, int Radio64,
         int Radio65, int Radio66,
         int Radio67, int Radio68,
         int Radio69, int Radio70,
         //int Radio71, 
         int Radio72,
         int Radio90, int Radio91,
         int Radio17, int Radio89, int Radio92,
         int RadioBtnList1,
         //int RadioBtnList3,
         int RadiochmsamQty_need,
         int RadioPwdsetup, int Radioexpenseneed,
         int Radiochm_ad_qty, int RadioCampneed,
         int RadioApproveneed, string Doc_Input_caption,
         string Chm_Input_caption, string Stk_Input_caption,
         string Ul_Input_caption, int RadioChmRxNd,
         int RadioDrSampNd, int RadioCmpgnNeed,
         int RadiorefDoc,
         //int RadioCHEBase,
         int Radiotp_need,
         //int RadiocurrentDay,
         int RadiocntRemarks, int Radiopast_leave_post,
         int RadiomyplnRmrksMand,
         int Radioprod_remark, int Radioprod_remark_md,
         //int Radiostp, 
         int RadioRemainder_geo,
         //int Radiodcr_edit_rej,
         int RadioDrRcpaQMd,
         int RadiogeoTagImg,
         int RadioHINeed,
         int Radiohosp_need, int RadioHPNeed,
         int Radioprdfdback,
         //int Radioques_need,
         //int Radiorcpaextra, 
         string pob_minvalue,
         int RadioRcpaMd, int RadioRcpa_Competitor_extra,
         //string GeoTagCap, 
         int rdoExpenceNd,
         int rdoExpenceNd_mandatory, int RadioRcpaNd,
         int Radioleavestatus, int rdoOrder_management,
         string txtOrder_caption, int rdoPrimary_order,
         string txtPrimary_order_caption, int rdoSecondary_order,
         string txtSecondary_order_caption, int rdoGst_option, string txtTaxname_caption,
         int rdosecondary_order_discount, int Radiomisc_expense_need,
         //int RadioentryFormNeed_entryFormMgr, 
         int Radiosurveynd,
         int Radiodashboard

 )
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Access_Master " +
                               " SET GeoChk= '" + GeoChk + "',"
                                //"geoTagImg='" + rdogeoTagImg + "', "
                                + " DisRad = '" + DisRad + "',"
                                + " DrCap ='" + DrCap + "' ,"
                                + " ChmCap ='" + ChmCap + "', "
                                + " StkCap ='" + StkCap + "', "
                                + " NLCap ='" + NLCap + "', "
                                + " DPNeed ='" + DPNeed + "', "
                                //+ " DINeed = '" + DINeed + "', "
                                //+ " DrSmpQCap = '" + rdoinput_Ent_doc + "', "
                                + " DINeed = '" + rdoinput_Ent_doc + "', "
                                + " ChmNeed = '" + ChmNeed + "', "
                                + " CPNeed = '" + CPNeed + "', "
                                + " ChmQCap = '" + ChmQCap + "', "
                                + " CINeed = '" + CINeed + "', "
                                + " StkNeed = '" + StkNeed + "', "
                                + " SPNeed = '" + SPNeed + "', "
                                + " StkQCap = '" + StkQCap + "', "
                                + " SINeed='" + inpu_entry_stock + "', "
                                + " UNLNeed ='" + UNLNeed + "', "
                                + " NPNeed ='" + NPNeed + "', "
                                + " NLRxQCap ='" + NLRxQCap + "', "
                                + " NLSmpQCap ='" + NLSmpQCap + "', "
                                + " NINeed =' " + NINeed + "' ,"
                                + " DeviceId_Need ='" + DeviceId_Need + "' ,"
                                + "MsdEntry='" + Radio2 + "',"
                                + "VstNd='" + Radio3 + "',"
                                + "mailneed='" + Radio4 + "',"
                                + "circular='" + Radio5 + "',"
                                + "DrFeedmd='" + Radio6 + "',"
                                + "DrPrdMd='" + Radio7 + "',"
                                + "DrInpMd='" + Radio8 + "',"
                                + "DrSmpQMd='" + Radio9 + "',"
                                + "DrRxQMd='" + Radio10 + "',"
                                + "DrRxQCap='" + txtrxqty + "',"
                                + "DrSmpQCap='" + txtSamQty_Cap_doc + "', "
                               + "Doc_Pob_Mandatory_Need='" + Radio11 + "',"
                                + "srtnd='" + Radio14 + "',"
                                //+ "MCLDet='" + Radio15 + "',"
                                + "Chm_Pob_Mandatory_Need='" + Radio16 + "',"
                                + "DrRxNd='" + Radio17 + "',"
                                + "TPDCR_Deviation='" + Radio31 + "',"
                                + "TP_Mandatory_Need='" + Radio32 + "',"
                                + "Tp_Start_Date='" + txt_srtdate + "',"
                                + "Tp_End_Date='" + txt_enddate + "',"
                                + "TPbasedDCR='" + Radio33 + "',"
                                + "NextVst='" + Radio34 + "',"
                                + "NextVst_Mandatory_Need='" + Radio35 + "',"
                                + "multiple_doc_need='" + Radio38 + "',"
                                + "Cluster_Cap='" + cluster + "',"
                                // + "allProdBd='" + Radio40 + "',"
                                //+ "Speciality_prod='" + Radio41 + "',"
                                + "FcsNd='" + Radio42 + "',"
                                //+ "Prod_Stk_Need='" + Radio43 + "',"
                                //+ "OtherNd='" + Radio44 + "',"
                                + "Sep_RcpaNd='" + Radio46 + "',"
                                + "Ul_Product_caption='" + txtUPC + "',"
                                + "Stk_Product_caption='" + txtSPC + "',"
                                + "Chm_Product_caption='" + txtCPC + "',"
                                + "Doc_Product_caption='" + txtDpc + "' ,"
                                + "doctor_dobdow='" + Radio47 + "',"
                                + "Appr_Mandatory_Need='" + Radio48 + "' ,"
                               + "DlyCtrl='" + Radio49 + "' ,"
                                + "cip_need='" + Radio50 + "' ,"
                                + "DFNeed='" + Radio51 + "' ,"
                                + "CFNeed='" + Radio52 + "',"
                                + "SFNeed='" + Radio53 + "' ,"
                                + "CIP_FNeed='" + Radio54 + "' ,"
                                + "NFNeed='" + Radio55 + "' ,"
                                + "HFNeed='" + Radio56 + "' ,"
                                //+ "DQNeed='" + Radio57 + "' ,"
                                + "CQNeed='" + Radio58 + "',"
                                //+ "SQNeed='" + Radio59 + "' ,"
                                //+ "NQNeed='" + Radio60 + "',"
                                //+ "CIP_QNeed='" + Radio61 + "',"
                                + "DENeed='" + Radio63 + "' ,"
                                + "CENeed='" + Radio64 + "',"
                                + "SENeed='" + Radio65 + "' ,"
                                + "NENeed='" + Radio66 + "',"
                                + "CIP_ENeed='" + Radio67 + "' ,"
                                + "HENeed='" + Radio68 + "',"
                                + "quiz_need='" + Radio69 + "' ,"
                                + "prod_det_need='" + Radio70 + "',"
                                //+ "prdfdback='" + Radio71 + "' ,"
                                + "mediaTrans_Need='" + Radio72 + "',"
                                + "Doc_Pob_Need='" + Radio75 + "' ,"
                                + "Chm_Pob_Need='" + Radio76 + "' ,"
                                + "Stk_Pob_Need='" + Radio77 + "' ,"
                                + "Ul_Pob_Need='" + Radio78 + "' ,"
                                + "Stk_Pob_Mandatory_Need='" + Radio79 + "' ,"
                                + "Ul_Pob_Mandatory_Need='" + Radio80 + "',"
                                + "Doc_jointwork_Need='" + Radio81 + "' ,"
                                + "Chm_jointwork_Need='" + Radio82 + "' ,"
                                + "Stk_jointwork_Need='" + Radio83 + "' ,"
                                + "Ul_jointwork_Need='" + Radio84 + "',"
                                + "Doc_jointwork_Mandatory_Need='" + Radio85 + "',"
                                + "Chm_jointwork_Mandatory_Need='" + Radio86 + "',"
                                + "Stk_jointwork_Mandatory_Need='" + Radio87 + "',"
                                + "Ul_jointwork_Mandatory_Need='" + Radio88 + "' ,"
                                + "CIP_PNeed='" + Radio90 + "',"
                                + "CIP_INeed='" + Radio91 + "',"
                                + "Prodfd_Need='" + Radio89 + "',"
                                + "TPDCR_MGRAppr='" + Radio92 + "', MissedDateMand='" + RadioBtnList1 + "', "
                                //+ "TempNd='" + RadioBtnList3 + "',"
                                + "chmsamQty_need='" + RadiochmsamQty_need + "',"
                                + "Pwdsetup='" + RadioPwdsetup + "',"
                                + "expenseneed='" + Radioexpenseneed + "',"
                                + "chm_ad_qty='" + Radiochm_ad_qty + "',"
                                + "Campneed='" + RadioCampneed + "',"
                                + "Approveneed='" + RadioApproveneed + "',"
                                + "Doc_Input_caption='" + Doc_Input_caption + "',"
                                + "Chm_Input_caption='" + Chm_Input_caption + "',"
                                + "Stk_Input_caption='" + Stk_Input_caption + "',"
                                + "Ul_Input_caption='" + Ul_Input_caption + "',"
                                + "ChmRxNd='" + RadioChmRxNd + "',"
                                 + "DrSampNd='" + RadioDrSampNd + "',"
                                + "CmpgnNeed='" + RadioCmpgnNeed + "',"
                                + "refDoc='" + RadiorefDoc + "',"
                                //+ "CHEBase='" + RadioCHEBase + "',"
                                + "tp_need='" + Radiotp_need + "',"
                                //+ "currentDay='" + RadiocurrentDay + "',"
                                + "cntRemarks='" + RadiocntRemarks + "',"
                                + "past_leave_post='" + Radiopast_leave_post + "',"
                                + "myplnRmrksMand='" + RadiomyplnRmrksMand + "',"
                                + "prod_remark='" + Radioprod_remark + "',"
                                + "prod_remark_md='" + Radioprod_remark_md + "',"
                                //+ "stp='" + Radiostp + "',"
                                + "Remainder_geo='" + RadioRemainder_geo + "',"
                                //+ "dcr_edit_rej='" + Radiodcr_edit_rej + "',"
                                + "DrRcpaQMd='" + RadioDrRcpaQMd + "',"
                                + "geoTagImg='" + RadiogeoTagImg + "',"
                                + "HINeed='" + RadioHINeed + "',"
                                + "hosp_need='" + Radiohosp_need + "',"
                                + "HPNeed='" + RadioHPNeed + "',"
                                + "prdfdback='" + Radioprdfdback + "',"
                                //+ "ques_need='" + Radioques_need + "',"
                                //+ "rcpaextra='" + Radiorcpaextra + "',"
                                + "pob_minvalue='" + pob_minvalue + "',"
                                + "RcpaMd='" + RadioRcpaMd + "',"
                                + "Rcpa_Competitor_extra='" + RadioRcpa_Competitor_extra + "',"

                                //Updated parameters
                                //+ "geotag_caption='" + GeoTagCap + "',"
                                + "ExpenceNd='" + rdoExpenceNd + "',"
                                + "ExpenceNd_mandatory='" + rdoExpenceNd_mandatory + "',"
                                + "RcpaNd='" + RadioRcpaNd + "',"
                                + "leavestatus='" + Radioleavestatus + "',"
                                + "Order_management='" + rdoOrder_management + "',"
                                + "Order_caption='" + txtOrder_caption + "',"
                                + "Primary_order='" + rdoPrimary_order + "',"
                                + "Primary_order_caption='" + txtPrimary_order_caption + "',"
                                + "Secondary_order='" + rdoSecondary_order + "',"
                                + "Secondary_order_caption='" + txtSecondary_order_caption + "',"
                                + "Gst_option='" + rdoGst_option + "',"
                                + "Taxname_caption='" + txtTaxname_caption + "',"
                                + "secondary_order_discount='" + rdosecondary_order_discount + "',"
                                + "misc_expense_need='" + Radiomisc_expense_need + "',"
                                //+ "entryFormNeed='" + RadioentryFormNeed_entryFormMgr + "',"
                                //+ "entryFormMgr='" + RadioentryFormNeed_entryFormMgr + "',"
                                + "surveynd='" + Radiosurveynd + "',"
                                + "dashboard='" + Radiodashboard + "' "

                                + " where Division_Code = '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int RecordUpdate_MobAppNew2(int rdoTerr_based_Tag, int RadioProd_Stk_Need, int RadioDrEvent_Md, int RadioMCLDet, string txtChmSmpCap,
            int RadioStkEvent_Md, string txtcip_caption, int RadioCIPPOBNd, int RadioCIPPOBMd, int RadioCipEvent_Md, string txthosp_caption, int RadioHosPOBNd, int RadioHosPOBMd, int RadioHospEvent_Md, int RadioUlDrEvent_Md, int RadioRcpaMd_Mgr, int RadioDrRCPA_competitor_Need,
            int RadioRCPAQty_Need,
            int RadioChm_RCPA_Need, int RadioChmRcpaMd, int RadioChmRcpaMd_Mgr, int Radiosequential_dcr, string txtDcrLockDays,
            int Radiomydayplan_need, int RadioLeave_entitlement_need, int rdoProduct_Rate_Editable, int Radiomulti_cluster, int RadioCustSrtNd,
            int RadioTarget_report_Nd, int Radiofaq, int RadioRmdrNeed, int RadioChmRCPA_competitor_Need, int RadioSpecFilter,
            //int RadioCurrentday_TPplanned, 
            int RadioChmEvent_Md,
            int RadioTerritory_VstNd, int RadioDcr_firstselfie,
            int RadioCIP_jointwork_Need, int RadioTempNd, int RadioCipSrtNd, int RadioentryFormNeed, int RadioentryFormMgr, string TxtRemainder_call_cap,
            int RadioRemainder_prd_Md, string Txtquiz_heading, int RadioLocation_track, string Txttracking_interval, int RadiotravelDistance_Need,
            string txtActivityCap, int RadioActivityNd, int Radioquiz_need_mandt, int Radioprimarysec_need, int RadioTarget_report_md,
            string tracking_time,
            string div_code)
        {
            {
                int iReturn = -1;

                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "UPDATE Access_Master " +
                                   " SET Terr_based_Tag= '" + rdoTerr_based_Tag + "', "
                                    + " Prod_Stk_Need= '" + RadioProd_Stk_Need + "', "
                                    + " DrEvent_Md= '" + RadioDrEvent_Md + "', "
                                    + " MCLDet='" + RadioMCLDet + "', "
                                    + " ChmSmpCap='" + txtChmSmpCap + "', "
                                    + " StkEvent_Md='" + RadioStkEvent_Md + "', "
                                    + " cip_caption='" + txtcip_caption + "', "
                                    + " CIPPOBNd='" + RadioCIPPOBNd + "', "
                                    + " CIPPOBMd='" + RadioCIPPOBMd + "', "
                                    + " CipEvent_Md='" + RadioCipEvent_Md + "', "
                                    + " hosp_caption='" + txthosp_caption + "', "
                                    + " HosPOBNd='" + RadioHosPOBNd + "', "
                                    + " HosPOBMd='" + RadioHosPOBMd + "', "
                                    + " HospEvent_Md='" + RadioHospEvent_Md + "', "
                                    + " UlDrEvent_Md='" + RadioUlDrEvent_Md + "', "
                                    + " RcpaMd_Mgr='" + RadioRcpaMd_Mgr + "', "
                                    + " DrRCPA_competitor_Need='" + RadioDrRCPA_competitor_Need + "', "
                                    + " RCPAQty_Need='" + RadioRCPAQty_Need + "', "
                                    + " Chm_RCPA_Need='" + RadioChm_RCPA_Need + "', "
                                    + " ChmRcpaMd='" + RadioChmRcpaMd + "', "
                                    + " ChmRcpaMd_Mgr='" + RadioChmRcpaMd_Mgr + "', "
                                    + " sequential_dcr='" + Radiosequential_dcr + "', "
                                    + " DcrLockDays='" + txtDcrLockDays + "', "
                                    + " mydayplan_need='" + Radiomydayplan_need + "', "
                                    + " Leave_entitlement_need='" + RadioLeave_entitlement_need + "', "
                                    + " Product_Rate_Editable='" + rdoProduct_Rate_Editable + "', "
                                    + " multi_cluster='" + Radiomulti_cluster + "', "
                                    + " CustSrtNd='" + RadioCustSrtNd + "', "
                                    + " Target_report_Nd='" + RadioTarget_report_Nd + "', "
                                    + " faq='" + Radiofaq + "', "
                                    + " RmdrNeed='" + RadioRmdrNeed + "', "
                                    + " SpecFilter='" + RadioSpecFilter + "', "
                                    //+ " Currentday_TPplanned='" + RadioCurrentday_TPplanned + "', "
                                    + " ChmEvent_Md='" + RadioChmEvent_Md + "', "
                                    + " Territory_VstNd='" + RadioTerritory_VstNd + "', "
                                    + " Dcr_firstselfie='" + RadioDcr_firstselfie + "', "

                                    + " CIP_jointwork_Need='" + RadioCIP_jointwork_Need + "', "
                                    + " TempNd='" + RadioTempNd + "', "
                                    + " CipSrtNd='" + RadioCipSrtNd + "', "
                                    + " entryFormNeed='" + RadioentryFormNeed + "', "
                                    + " entryFormMgr='" + RadioentryFormMgr + "', "
                                    + " Remainder_call_cap='" + TxtRemainder_call_cap + "', "
                                    + " Remainder_prd_Md='" + RadioRemainder_prd_Md + "', "
                                    + " quiz_heading='" + Txtquiz_heading + "', "
                                    + " Location_track='" + RadioLocation_track + "', "
                                    + " tracking_interval='" + Txttracking_interval + "', "
                                    + " travelDistance_Need='" + RadiotravelDistance_Need + "', "
                                    + " ActivityCap='" + txtActivityCap + "', "
                                    + " ActivityNd='" + RadioActivityNd + "', "
                                    + " quiz_need_mandt='" + Radioquiz_need_mandt + "', "
                                    + " primarysec_need='" + Radioprimarysec_need + "', "
                                    + " Target_report_md='" + RadioTarget_report_md + "', "
                                    + " tracking_time='" + tracking_time + "', "
                                    + " ChmRCPA_competitor_Need='" + RadioChmRCPA_competitor_Need + "' "
                                    + " where Division_Code = '" + div_code + "' ";
                    iReturn = db.ExecQry(strQry);
                }


                catch (Exception ex)
                {
                    throw ex;
                }

                return iReturn;
            }
        }

        public int RecordUpdate_MobAppNewCustom(int RadioProduct_Stockist, int Radiohosp_filter, int RadioDetailing_chem, int RadioDetailing_stk, int RadioDetailing_undr, int RadioDcrapprvNd, int RadioaddChm, int RadioaddDr, int Radioundr_hs_nd, //int RadioaddAct,
            int RadioshowDelete, int RadioPresentNd, int RadioCustNd, int Radioyetrdy_call_del_Nd, int Radiotheraptic,
            string div_code)
        {
            int iReturnTP = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE custom_table " +
                               " SET hosp_filter= '" + Radiohosp_filter + "',"
                              + "addDr='" + RadioaddDr + "', "
                              + "showDelete='" + RadioshowDelete + "',"
                              + "Detailing_chem='" + RadioDetailing_chem + "',"
                              + "Detailing_stk='" + RadioDetailing_stk + "',"
                              + "Detailing_undr='" + RadioDetailing_undr + "',"
                              + "addChm='" + RadioaddChm + "',"
                              + "DcrapprvNd='" + RadioDcrapprvNd + "',"
                              + "Product_Stockist='" + RadioProduct_Stockist + "',"
                              + "undr_hs_nd='" + Radioundr_hs_nd + "',"
                              + "PresentNd='" + RadioPresentNd + "',"
                              + "CustNd='" + RadioCustNd + "',"
                              + "theraptic='" + Radiotheraptic + "',"
                              + "yetrdy_call_del_Nd='" + Radioyetrdy_call_del_Nd + "' "
                              //+ "addAct='" + RadioaddAct + "' "
                              + " where division_code = '" + div_code + "'";

                iReturnTP = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }
            return iReturnTP;
        }

        public int RecordUpdate_MobAppNewTP(int RadioDrNeed, int RadioTpChmNeed, int RadioTpStkNeed, int RadioTpJWNeed, int RadioTpFW_meetup_mandatory, int RadioTpHospNeed, int RadioTpCip_Need, int RadioTpClusterNeed, int RadioTpAddsessionNeed, int RadioTpAddsessionCount,
            int Radioclustertype, int Radioedit_holiday, int Radioedit_weeklyoff,
            string div_code)
        {
            int iReturnTP = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE tpsetup " +
                               " SET DrNeed= '" + RadioDrNeed + "',ChmNeed='" + RadioTpChmNeed + "', "
                              + "StkNeed='" + RadioTpStkNeed + "',"
                              + "JWNeed='" + RadioTpJWNeed + "',"
                              + "FW_meetup_mandatory='" + RadioTpFW_meetup_mandatory + "',"
                              + "HospNeed='" + RadioTpHospNeed + "',"
                              + "Cip_Need='" + RadioTpCip_Need + "',"
                              + "ClusterNeed='" + RadioTpClusterNeed + "',"
                              + "AddsessionNeed='" + RadioTpAddsessionNeed + "',"
                              + "AddsessionCount='" + RadioTpAddsessionCount + "',"
                              //+ "tp_objective='" + Radiotp_objective + "',"
                              + "clustertype='" + Radioclustertype + "',"
                              + "Holiday_Editable='" + Radioedit_holiday + "',"
                              + "Weeklyoff_Editable='" + Radioedit_weeklyoff + "' "
                              + " where div = '" + div_code + "'";

                iReturnTP = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }
            return iReturnTP;
        }

        public int RecordUpdate_Forhalfday(string WorkType_Code_B, string div_code, int Hlfdy_flag)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_BaseLevel set Hlfdy_flag='" + Hlfdy_flag + "' " +
                         " where WorkType_Code_B='" + WorkType_Code_B + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet SalesForceListMgrGet(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_SalesForceMgrGet '" + divcode + "', '" + sf_code + "'   ";

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

        public int RecordUpdate_geosf_code2(string sf_code, int GeoNeed, int GeoFencing, int Fencingche, int Fencingstock, int FencingUnlisted, int FencingCIP)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = " Update Access_Table set GeoNeed='" + GeoNeed + "' ,GeoFencing='" + GeoFencing + "',GeoFencingche='" + Fencingche + "',GeoFencingstock='" + Fencingstock + "', " +
                        " GeoFencingulDr='" + FencingUnlisted + "',GeoFencingcip='" + FencingCIP + "' " +
                        " where sf_code='" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        #endregion
    }
}