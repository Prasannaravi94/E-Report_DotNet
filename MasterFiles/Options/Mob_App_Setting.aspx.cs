using Bus_EReport;
using DBase_EReport;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Options_Mob_App_Setting : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsadmin = new DataSet();
    DataSet dsadm = new DataSet();
    DataSet dsAdminSetup = null;
    DataSet dsAdminSetupTP = null;
    DataSet dsAdminSetup3 = null;
    DataSet dsAdminSetupCustom = null;
    DataSet dsSalesForce = new DataSet();
    DataSet dsadmi = new DataSet();
    int iIndex = -1;
    string chkhaf = string.Empty;

    int check = 0;
    int geo_code = 0;
    int geo_fencing = 0;
    private int Fencingche;
    private int Fencingstock;
    private int FencingUnlisted;
    private int FencingCIP;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Audit_setup"] == null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('kindly enter audit setup');window.location.href = '/../../Default.aspx';</script>");
        }
        div_code = Session["div_code"].ToString();
        hHeading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {
            Fillsalesforce();
            FillHalfDay_Work();
            FillHalfDay_Work_MGR();
            app_record();
            appset_record();
            app_record_2();
        }
    }
    private void appset_record()
    {
        AdminSetup aa = new AdminSetup();
        dsAdminSetup = aa.getting_mob_app_record2New(div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            Radio6.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DrFeedMd"].ToString();
            Radio31.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["TPDCR_Deviation"].ToString();
            Radio32.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["TP_Mandatory_Need"].ToString();
            txt_srtdate.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Tp_Start_Date"].ToString();
            txt_enddate.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Tp_End_Date"].ToString();
            Radio33.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["TPbasedDCR"].ToString();
            Radio34.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["NextVst"].ToString();
            Radio35.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["NextVst_Mandatory_Need"].ToString();

            Radio38.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["multiple_doc_need"].ToString();
            txtCluster.Text = dsAdminSetup.Tables[0].Rows[0]["Cluster_Cap"].ToString();
            //Radio40.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["allProdBd"].ToString();
            //Radio41.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Speciality_prod"].ToString();
            Radio42.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["FcsNd"].ToString();
            //Radio43.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            //Radio44.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["OtherNd"].ToString();
            Radio46.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Sep_RcpaNd"].ToString();
            Radio47.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["doctor_dobdow"].ToString();
            Radio48.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Appr_Mandatory_Need"].ToString();
            Radio49.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DlyCtrl"].ToString();
            Radio50.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["cip_need"].ToString();
            Radio51.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DFNeed"].ToString();
            Radio52.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CFNeed"].ToString();
            Radio53.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["SFNeed"].ToString();
            Radio54.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CIP_FNeed"].ToString();
            Radio55.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["NFNeed"].ToString();
            Radio56.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["HFNeed"].ToString();
            //Radio57.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DQNeed"].ToString();
            //Radio58.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CQNeed"].ToString();
            //Radio59.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["SQNeed"].ToString();
            //Radio60.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["NQNeed"].ToString();
            //Radio61.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CIP_QNeed"].ToString();

            Radio63.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DENeed"].ToString();
            Radio64.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CENeed"].ToString();
            Radio65.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["SENeed"].ToString();
            Radio66.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["NENeed"].ToString();
            Radio67.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CIP_ENeed"].ToString();
            Radio68.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["HENeed"].ToString();
            Radio69.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["quiz_need"].ToString();
            Radio70.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["prod_det_need"].ToString();
            //Radio71.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["prdfdback"].ToString();
            Radio72.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["mediaTrans_Need"].ToString();
            Radio75.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Doc_Pob_Need"].ToString();
            Radio76.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Chm_Pob_Need"].ToString();
            Radio77.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Stk_Pob_Need"].ToString();
            Radio78.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Ul_Pob_Need"].ToString();
            Radio79.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Stk_Pob_Mandatory_Need"].ToString();
            Radio80.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Ul_Pob_Mandatory_Need"].ToString();
            Radio81.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Doc_jointwork_Need"].ToString();
            Radio82.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Chm_jointwork_Need"].ToString();
            Radio83.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Stk_jointwork_Need"].ToString();
            Radio84.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Ul_jointwork_Need"].ToString();
            Radio85.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Doc_jointwork_Mandatory_Need"].ToString();
            Radio86.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Chm_jointwork_Mandatory_Need"].ToString();
            Radio87.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Stk_jointwork_Mandatory_Need"].ToString();
            Radio88.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Ul_jointwork_Mandatory_Need"].ToString();
            Radio89.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Prodfd_Need"].ToString();
            txtDpc.Text = dsAdminSetup.Tables[0].Rows[0]["Doc_Product_caption"].ToString();
            txtCPC.Text = dsAdminSetup.Tables[0].Rows[0]["Chm_Product_caption"].ToString();
            txtSPC.Text = dsAdminSetup.Tables[0].Rows[0]["stk_Product_caption"].ToString();
            txtUPC.Text = dsAdminSetup.Tables[0].Rows[0]["Ul_Product_caption"].ToString();
            Radio92.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["TPDCR_MGRAppr"].ToString();
            RadioBtnList1.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["MissedDateMand"].ToString();
            //RadioBtnList2.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(66).ToString();
            RadioTempNd.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["TempNd"].ToString();

            //Radiomydayplan_need1.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["mydayplan_need"].ToString();
            RadiochmsamQty_need.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["chmsamQty_need"].ToString();
            RadioPwdsetup.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Pwdsetup"].ToString();
            Radioexpenseneed.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["expenseneed"].ToString();
            //RadioExpenceNd_mandatory.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["ExpenceNd_mandatory"].ToString();
            //RadioCatneed.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Catneed"].ToString();
            Radiochm_ad_qty.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["chm_ad_qty"].ToString();
            RadioCampneed.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Campneed"].ToString();
            RadioApproveneed.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Approveneed"].ToString();
            txtDoc_Input_caption.Text = dsAdminSetup.Tables[0].Rows[0]["Doc_Input_caption"].ToString();
            txtChm_Input_caption.Text = dsAdminSetup.Tables[0].Rows[0]["Chm_Input_caption"].ToString();
            txtStk_Input_caption.Text = dsAdminSetup.Tables[0].Rows[0]["Stk_Input_caption"].ToString();
            txtUl_Input_caption.Text = dsAdminSetup.Tables[0].Rows[0]["Ul_Input_caption"].ToString();
            RadioChmRxNd.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["ChmRxNd"].ToString();
            RadioDrSampNd.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DrSampNd"].ToString();
            RadioCmpgnNeed.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CmpgnNeed"].ToString();
            RadioentryFormNeed.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["entryFormNeed"].ToString();
            RadiorefDoc.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["refDoc"].ToString();
            //RadioCHEBase.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CHEBase"].ToString();
            //RadioTPDCR_Deviation_Appr_Status.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["TPDCR_Deviation_Appr_Status"].ToString();
            Radiotp_need.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["tp_need"].ToString();
            //RadiocurrentDay.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["currentDay"].ToString();
            RadiocntRemarks.Text = dsAdminSetup.Tables[0].Rows[0]["cntRemarks"].ToString();
            Radiopast_leave_post.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["past_leave_post"].ToString();
            RadiomyplnRmrksMand.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["myplnRmrksMand"].ToString();
            RadioentryFormMgr.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["entryFormMgr"].ToString();
            Radioprod_remark.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["prod_remark"].ToString();
            Radioprod_remark_md.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["prod_remark_md"].ToString();
            //Radiostp.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["stp"].ToString();
            RadioRemainder_geo.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Remainder_geo"].ToString();
            //Radiodcr_edit_rej.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["dcr_edit_rej"].ToString();
            //Radiocall_feed_enterable.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["call_feed_enterable"].ToString();
            RadioDrRcpaQMd.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DrRcpaQMd"].ToString();
            //Radioexpense_need.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["expense_need"].ToString();
            RadiogeoTagImg.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["geoTagImg"].ToString();
            RadioHINeed.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["HINeed"].ToString();
            Radiohosp_need.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["hosp_need"].ToString();
            RadioHPNeed.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["HPNeed"].ToString();
            Radioprdfdback.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["prdfdback"].ToString();
            //Radioques_need.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["ques_need"].ToString();
            //Radiorcpaextra.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["rcpaextra"].ToString();
            txtpob_minvalue.Text = dsAdminSetup.Tables[0].Rows[0]["pob_minvalue"].ToString();
            RadioRcpaMd.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["RcpaMd"].ToString();
            RadioRcpa_Competitor_extra.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Rcpa_Competitor_extra"].ToString();

            //txtGeoTagCap.Text=dsAdminSetup.Tables[0].Rows[0]["geotag_caption"].ToString();
            rdoExpenceNd.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["ExpenceNd"].ToString();
            rdoExpenceNd_mandatory.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["ExpenceNd_mandatory"].ToString();
            RadioRcpaNd.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["RcpaNd"].ToString();
            Radioleavestatus.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["leavestatus"].ToString();
            rdoOrder_management.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Order_management"].ToString();
            txtOrder_caption.Text = dsAdminSetup.Tables[0].Rows[0]["Order_caption"].ToString();
            rdoPrimary_order.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Primary_order"].ToString();
            txtPrimary_order_caption.Text = dsAdminSetup.Tables[0].Rows[0]["Primary_order_caption"].ToString();
            rdoSecondary_order.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Secondary_order"].ToString();
            txtSecondary_order_caption.Text = dsAdminSetup.Tables[0].Rows[0]["Secondary_order_caption"].ToString();
            rdoGst_option.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Gst_option"].ToString();
            txtTaxname_caption.Text = dsAdminSetup.Tables[0].Rows[0]["Taxname_caption"].ToString();
            rdosecondary_order_discount.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["secondary_order_discount"].ToString();
            Radiomisc_expense_need.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["misc_expense_need"].ToString();
            //RadioentryFormNeed_entryFormMgr.SelectedValue=dsAdminSetup.Tables[0].Rows[0]["entryFormNeed"].ToString();
            Radiosurveynd.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["surveynd"].ToString();
            Radiodashboard.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["dashboard"].ToString();


        }
        //Tour Plan 
        dsAdminSetupTP = aa.getting_mob_app_record2NewTP(div_code);
        if (dsAdminSetupTP.Tables[0].Rows.Count > 0)
        {
            RadioDrNeed.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["DrNeed"].ToString();
            RadioTpChmNeed.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["ChmNeed"].ToString();
            RadioTpStkNeed.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["StkNeed"].ToString();
            RadioTpJWNeed.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["JWNeed"].ToString();
            RadioTpFW_meetup_mandatory.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["FW_meetup_mandatory"].ToString();
            RadioTpHospNeed.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["HospNeed"].ToString();
            RadioTpCip_Need.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["Cip_Need"].ToString();
            RadioTpClusterNeed.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["ClusterNeed"].ToString();
            RadioTpAddsessionNeed.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["AddsessionNeed"].ToString();
            ddlTpAddsessionCount.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["AddsessionCount"].ToString();
            //Radiotp_objective.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["tp_objective"].ToString();
            Radioclustertype.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["clustertype"].ToString();
            Radioedit_holiday.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["Holiday_Editable"].ToString();
            Radioedit_weeklyoff.SelectedValue = dsAdminSetupTP.Tables[0].Rows[0]["Weeklyoff_Editable"].ToString();

        }

        //Access_master 2
        dsAdminSetup3 = aa.getting_mob_app_record3New(div_code);
        {
            if (dsAdminSetup3.Tables[0].Rows.Count > 0)
            {
                RadioProd_Stk_Need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Prod_Stk_Need"].ToString();
                RadioDrEvent_Md.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["DrEvent_Md"].ToString();
                RadioMCLDet.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["MCLDet"].ToString();
                txtChmSmpCap.Text = dsAdminSetup3.Tables[0].Rows[0]["ChmSmpCap"].ToString();
                RadioStkEvent_Md.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["StkEvent_Md"].ToString();
                txtcip_caption.Text = dsAdminSetup3.Tables[0].Rows[0]["CIP_Caption"].ToString();
                RadioCIPPOBNd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["CIPPOBNd"].ToString();
                RadioCIPPOBMd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["CIPPOBMd"].ToString();
                RadioCipEvent_Md.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["CipEvent_Md"].ToString();
                txthosp_caption.Text = dsAdminSetup3.Tables[0].Rows[0]["hosp_caption"].ToString();
                RadioHosPOBNd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["HosPOBNd"].ToString();
                RadioHosPOBMd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["HosPOBMd"].ToString();
                RadioHospEvent_Md.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["HospEvent_Md"].ToString();
                RadioUlDrEvent_Md.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["UlDrEvent_Md"].ToString();
                RadioRcpaMd_Mgr.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["RcpaMd_Mgr"].ToString();
                RadioDrRCPA_competitor_Need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["DrRCPA_competitor_Need"].ToString();
                RadioRCPAQty_Need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["RCPAQty_Need"].ToString();
                RadioChm_RCPA_Need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Chm_RCPA_Need"].ToString();
                RadioChmRcpaMd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["ChmRcpaMd"].ToString();
                RadioChmRcpaMd_Mgr.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["ChmRcpaMd_Mgr"].ToString();
                Radiosequential_dcr.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["sequential_dcr"].ToString();
                txtDcrLockDays.Text = dsAdminSetup3.Tables[0].Rows[0]["DcrLockDays"].ToString();
                Radiomydayplan_need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["mydayplan_need"].ToString();
                RadioLeave_entitlement_need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Leave_entitlement_need"].ToString();
                rdoProduct_Rate_Editable.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Product_rate_editable"].ToString();
                Radiomulti_cluster.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["multi_cluster"].ToString();
                RadioCustSrtNd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["CustSrtNd"].ToString();
                RadioTarget_report_Nd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Target_report_Nd"].ToString();
                Radiofaq.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["faq"].ToString();
                RadioRmdrNeed.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["RmdrNeed"].ToString();
                RadioChmRCPA_competitor_Need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["ChmRCPA_competitor_Need"].ToString();
                rdoTerr_based_Tag.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Terr_based_Tag"].ToString();
                RadioSpecFilter.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["SpecFilter"].ToString();
                //RadioCurrentday_TPplanned.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Currentday_TPplanned"].ToString();
                RadioChmEvent_Md.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["ChmEvent_Md"].ToString();

                RadioTerritory_VstNd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Territory_VstNd"].ToString();
                RadioDcr_firstselfie.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Dcr_firstselfie"].ToString();

                RadioCIP_jointwork_Need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["CIP_jointwork_Need"].ToString();
                RadioCipSrtNd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["CipSrtNd"].ToString();

                TxtRemainder_call_cap.Text = dsAdminSetup3.Tables[0].Rows[0]["Remainder_call_cap"].ToString();
                RadioRemainder_prd_Md.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Remainder_prd_Md"].ToString();
                Txtquiz_heading.Text = dsAdminSetup3.Tables[0].Rows[0]["quiz_heading"].ToString();

                RadioLocation_track.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Location_track"].ToString();
                Txttracking_interval.Text = dsAdminSetup3.Tables[0].Rows[0]["tracking_interval"].ToString();

                RadiotravelDistance_Need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["travelDistance_Need"].ToString();
                txtActivityCap.Text = dsAdminSetup3.Tables[0].Rows[0]["ActivityCap"].ToString();
                RadioActivityNd.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["ActivityNd"].ToString();

                Radioquiz_need_mandt.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["quiz_need_mandt"].ToString();
                Radioprimarysec_need.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["primarysec_need"].ToString();
                RadioTarget_report_md.SelectedValue = dsAdminSetup3.Tables[0].Rows[0]["Target_report_md"].ToString();

                string[] txttracking_time = dsAdminSetup3.Tables[0].Rows[0]["tracking_time"].ToString().Split('-');
                txttracking_time1.Text = txttracking_time[0].ToString();
                txttracking_time2.Text = txttracking_time.Length == 1 ? txttracking_time[0].ToString() : txttracking_time[1].ToString();
            }
        }

        //Custom  table
        dsAdminSetupCustom = aa.getting_mob_app_recordCustom(div_code);
        {
            if (dsAdminSetupCustom.Tables[0].Rows.Count > 0)
            {
                RadioProduct_Stockist.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["Product_Stockist"].ToString();
                Radiohosp_filter.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["hosp_filter"].ToString();
                RadioDetailing_chem.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["Detailing_chem"].ToString();
                RadioDetailing_stk.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["Detailing_stk"].ToString();
                RadioDetailing_undr.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["Detailing_undr"].ToString();
                RadioDcrapprvNd.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["DcrapprvNd"].ToString();
                RadioaddChm.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["addChm"].ToString();
                RadioaddDr.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["adddr"].ToString();
                Radioundr_hs_nd.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["undr_hs_nd"].ToString();
                //RadioaddAct.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["addAct"].ToString();
                RadioshowDelete.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["showDelete"].ToString();
                //RadioTarget_sales.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["Target_sales"].ToString();
                RadioPresentNd.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["PresentNd"].ToString();
                RadioCustNd.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["CustNd"].ToString();
                Radioyetrdy_call_del_Nd.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["yetrdy_call_del_Nd"].ToString();
                Radiotheraptic.SelectedValue = dsAdminSetupCustom.Tables[0].Rows[0]["theraptic"].ToString();
            }
        }

        //

    }

    private void app_record()
    {
        AdminSetup aa = new AdminSetup();
        dsAdminSetup = aa.getting_mob_app_record(div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            //Radio.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Radio2.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["MsdEntry"].ToString();
            Radio3.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["VstNd"].ToString();
            Radio4.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["mailneed"].ToString();
            Radio5.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            //Radio6.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            Radio7.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DrPrdMd"].ToString();
            Radio8.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DrInpMd"].ToString();
            Radio9.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DrSmpQMd"].ToString();
            Radio10.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DrRxQMd"].ToString();
            Radio17.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["DrRxNd"].ToString();
            Radio11.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Doc_Pob_Mandatory_Need"].ToString();
            Radio14.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["srtnd"].ToString();
            //Radio15.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
            Radio16.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["Chm_Pob_Mandatory_Need"].ToString();
            Radio90.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CIP_PNeed"].ToString();
            Radio91.SelectedValue = dsAdminSetup.Tables[0].Rows[0]["CIP_INeed"].ToString();
        }
    }

    private void app_record_2()
    {
        AdminSetup dv = new AdminSetup();
        dsadmin = dv.getMobApp_Setting(div_code);

        if (dsadmin.Tables[0].Rows.Count > 0)
        {
            if (dsadmin.Tables[0].Rows[0]["GeoChk"].ToString() == "0")
            {
                rdomandt.SelectedValue = "0";
            }
            else
            {
                rdomandt.SelectedValue = "1";
            }


            Radio90.SelectedValue = "1";
            if (dsadmin.Tables[0].Rows[0]["CIP_PNeed"].ToString() == "0")
            {
                Radio90.SelectedValue = "0";
            }
            else
            {
                Radio90.SelectedValue = "1";
            }

            Radio91.SelectedValue = "1";
            if (dsadmin.Tables[0].Rows[0]["CIP_INeed"].ToString() == "0")
            {
                Radio91.SelectedValue = "0";
            }
            else
            {
                Radio91.SelectedValue = "1";
            }
            if (dsadmin.Tables[0].Rows[0]["TPDCR_MGRAppr"].ToString() == "0")
            {
                Radio92.SelectedValue = "0";
            }
            else
            {
                Radio92.SelectedValue = "1";
            }

            //if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
            //{
            //    rdogeo.SelectedValue = "1";
            //}
            //else
            //{
            //    rdogeo.SelectedValue = "0";
            //}
            txtcover.Text = dsadmin.Tables[0].Rows[0]["DisRad"].ToString();
            txtvisit1.Text = dsadmin.Tables[0].Rows[0]["DrCap"].ToString();
            txtvisit2.Text = dsadmin.Tables[0].Rows[0]["ChmCap"].ToString();
            txtvisit3.Text = dsadmin.Tables[0].Rows[0]["StkCap"].ToString();
            txtvisit4.Text = dsadmin.Tables[0].Rows[0]["NLCap"].ToString();

            if (dsadmin.Tables[0].Rows[0]["DPNeed"].ToString() == "0")
            {
                rdoprd_entry_doc.SelectedValue = "0";
            }
            else
            {
                rdoprd_entry_doc.SelectedValue = "1";
            }
            txtRx_Cap_doc.Text = dsadmin.Tables[0].Rows[0]["DrRxQCap"].ToString();
            txtSamQty_Cap_doc.Text = dsadmin.Tables[0].Rows[0]["DrSmpQCap"].ToString();

            if (dsadmin.Tables[0].Rows[0]["DINeed"].ToString() == "0")
            {
                rdoinput_Ent_doc.SelectedValue = "0";
            }
            else
            {
                rdoinput_Ent_doc.SelectedValue = "1";
            }

            if (dsadmin.Tables[0].Rows[0]["ChmNeed"].ToString() == "0")
            {
                rdoNeed_chem.SelectedValue = "0";
            }
            else
            {
                rdoNeed_chem.SelectedValue = "1";
            }

            if (dsadmin.Tables[0].Rows[0]["CPNeed"].ToString() == "0")
            {
                rdoProduct_entr_chem.SelectedValue = "0";
            }
            else
            {
                rdoProduct_entr_chem.SelectedValue = "1";
            }

            txtqty_Cap_chem.Text = dsadmin.Tables[0].Rows[0]["ChmQCap"].ToString();

            if (dsadmin.Tables[0].Rows[0]["CINeed"].ToString() == "0")
            {
                rdoinpu_entry_chem.SelectedValue = "0";
            }
            else
            {
                rdoinpu_entry_chem.SelectedValue = "1";
            }

            if (dsadmin.Tables[0].Rows[0]["StkNeed"].ToString() == "0")
            {
                rdoNeed_stock.SelectedValue = "0";
            }
            else
            {
                rdoNeed_stock.SelectedValue = "1";
            }

            if (dsadmin.Tables[0].Rows[0]["SPNeed"].ToString() == "0")
            {
                rdoprdentry_stock.SelectedValue = "0";
            }
            else
            {
                rdoprdentry_stock.SelectedValue = "1";
            }

            txtQty_Cap_stock.Text = dsadmin.Tables[0].Rows[0]["StkQCap"].ToString();

            if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "0")
            {
                rdoinpu_entry_stock.SelectedValue = "0";
            }
            else
            {
                rdoinpu_entry_stock.SelectedValue = "1";
            }

            if (dsadmin.Tables[0].Rows[0]["UNLNeed"].ToString() == "0")
            {
                rdoneed_unlistDr.SelectedValue = "0";
            }
            else
            {
                rdoneed_unlistDr.SelectedValue = "1";
            }

            if (dsadmin.Tables[0].Rows[0]["NPNeed"].ToString() == "0")
            {
                rdoprdentry_unlistDr.SelectedValue = "0";
            }
            else
            {
                rdoprdentry_unlistDr.SelectedValue = "1";
            }

            txtRxQty_Cap_unlistDr.Text = dsadmin.Tables[0].Rows[0]["NLRxQCap"].ToString();

            txtSamQty_Cap_unlistDr.Text = dsadmin.Tables[0].Rows[0]["NLSmpQCap"].ToString();

            if (dsadmin.Tables[0].Rows[0]["NINeed"].ToString() == "0")
            {
                rdoinpuEnt_Need_unlistDr.SelectedValue = "0";
            }
            else
            {
                rdoinpuEnt_Need_unlistDr.SelectedValue = "1";
            }

            if (dsadmin.Tables[0].Rows[0]["DeviceId_Need"].ToString() == "0")
            {
                rdodevice.SelectedValue = "0";
            }
            else
            {
                rdodevice.SelectedValue = "1";
            }
        }

        for (int i = 0; i < chkhaf_work.Items.Count; i++)
        {

            AdminSetup adm = new AdminSetup();
            dsadm = dv.getMobApp_Setting_halfday(div_code, chkhaf_work.Items[i].Value);

            if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
            {
                chkhaf_work.Items[i].Selected = true;
            }
            else
            {
                chkhaf_work.Items[i].Selected = false;
            }

        }

        for (int i = 0; i < chkhaf_work_mgr.Items.Count; i++)
        {

            AdminSetup adm = new AdminSetup();
            DataSet dsAdmm = new DataSet();
            string strQry = string.Empty;
            DataSet dsAdmin = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "  select Hlfdy_flag,WorkType_Code_M from Mas_WorkType_Mgr where Division_Code='" + div_code + "' and WorkType_Code_M='" + chkhaf_work_mgr.Items[i].Value + "' ";
            dsAdmm = db_ER.Exec_DataSet(strQry);

            //dsAdmm = dv.getMobApp_Setting_halfday(div_code, chkhaf_work_mgr.Items[i].Value);

            if (dsAdmm.Tables[0].Rows.Count > 0)
            {

                if (dsAdmm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                {
                    chkhaf_work_mgr.Items[i].Selected = true;
                }
                else
                {
                    chkhaf_work_mgr.Items[i].Selected = false;
                }
            }

        }



        foreach (GridViewRow gridRow in grdgps.Rows)
        {
            CheckBox chkId = (CheckBox)gridRow.Cells[0].FindControl("chkId");
            bool bCheck = chkId.Checked;
            Label lblSF_Code = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
            string sf_Code = lblSF_Code.Text.ToString();
            CheckBox chkfencing = (CheckBox)gridRow.Cells[0].FindControl("chkfencing");
            CheckBox GeoFencingche = (CheckBox)gridRow.Cells[4].FindControl("chkfencingche");
            CheckBox GeoFencingstock = (CheckBox)gridRow.Cells[5].FindControl("chkfencingstock");
            CheckBox GeoFencingUnlisted = (CheckBox)gridRow.Cells[6].FindControl("chkfencingUnlisted");
            CheckBox GeoFencingCIP = (CheckBox)gridRow.Cells[7].FindControl("chkfencingCIP");

            if (sf_Code != "")
            {

                AdminSetup ad = new AdminSetup();
                dsadmi = dv.getMobApp_geo(sf_Code);

                if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                {
                    chkId.Checked = true;
                    hdnmandt.Value = "1";
                }
                else
                {
                    chkId.Checked = false;
                }
                if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "1")
                {

                    chkfencing.Checked = true;

                }
                else
                {
                    chkfencing.Checked = false;
                }

                if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() == "1")
                {

                    GeoFencingche.Checked = true;

                }
                else
                {
                    GeoFencingche.Checked = false;
                }
                if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "1")
                {
                    GeoFencingstock.Checked = true;
                }
                else
                {
                    GeoFencingstock.Checked = false;
                }

                if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "1")
                {
                    GeoFencingUnlisted.Checked = true;
                }
                else
                {
                    GeoFencingUnlisted.Checked = false;
                }

                if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(6).ToString() == "1")
                {
                    GeoFencingCIP.Checked = true;
                }
                else
                {
                    GeoFencingCIP.Checked = false;
                }
            }
        }
    }

    private void FillHalfDay_Work()
    {
        AdminSetup adm = new AdminSetup();

        dsadmin = adm.gethalf_Daywrk(div_code);

        chkhaf_work.DataSource = dsadmin;
        chkhaf_work.DataTextField = "Worktype_Name_B";
        chkhaf_work.DataValueField = "WorkType_Code_B";
        chkhaf_work.DataBind();
    }

    private void FillHalfDay_Work_MGR()
    {
        AdminSetup adm = new AdminSetup();
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsAd = new DataSet();
        string strQry = string.Empty;
        strQry = " select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr " +
                  " where Division_Code='" + div_code + "' and active_flag=0 and fieldwork_indicator='N' ";
        dsAd = db_ER.Exec_DataSet(strQry);
        if (dsAd.Tables[0].Rows.Count > 0)
        {
            chkhaf_work_mgr.DataSource = dsAd;
            chkhaf_work_mgr.DataTextField = "Worktype_Name_M";
            chkhaf_work_mgr.DataValueField = "WorkType_Code_M";
            chkhaf_work_mgr.DataBind();
        }
    }

    protected void btnSubmitNew_Click(object sender, EventArgs e)
    {

        AdminSetup admin = new AdminSetup();
        string strQry = string.Empty;
        int iReturn = admin.RecordUpdate_MobAppNew(Convert.ToInt16(rdomandt.SelectedValue.ToString()),
        //Convert.ToInt16(rdogeo.SelectedValue.ToString()),
        float.Parse(txtcover.Text.ToString()), txtvisit1.Text.ToString(),
        txtvisit2.Text.ToString(), txtvisit3.Text.ToString(),
        txtvisit4.Text.ToString(), Convert.ToInt16(rdoprd_entry_doc.SelectedValue.ToString()),
        Convert.ToInt16(rdoinput_Ent_doc.SelectedValue.ToString()), Convert.ToInt16(rdoNeed_chem.SelectedValue.ToString()),
        Convert.ToInt16(rdoProduct_entr_chem.SelectedValue.ToString()), txtqty_Cap_chem.Text.ToString(),
        Convert.ToInt16(rdoinpu_entry_chem.SelectedValue.ToString()), Convert.ToInt16(rdoNeed_stock.SelectedValue.ToString()),
        Convert.ToInt16(rdoprdentry_stock.SelectedValue.ToString()), txtQty_Cap_stock.Text.ToString(),
        Convert.ToInt16(rdoinpu_entry_stock.SelectedValue.ToString()), Convert.ToInt16(rdoneed_unlistDr.SelectedValue.ToString()),
        Convert.ToInt16(rdoprdentry_unlistDr.SelectedValue.ToString()), txtRxQty_Cap_unlistDr.Text.ToString(),
        txtSamQty_Cap_unlistDr.Text.ToString(), Convert.ToInt16(rdoinpuEnt_Need_unlistDr.SelectedValue.ToString()),
        div_code, Convert.ToInt16(rdodevice.SelectedValue),
        Convert.ToInt16(Radio2.SelectedValue.ToString()),
        Convert.ToInt16(Radio3.SelectedValue.ToString()),
        Convert.ToInt16(Radio4.SelectedValue.ToString()), Convert.ToInt16(Radio5.SelectedValue.ToString()),
        Convert.ToInt16(Radio6.SelectedValue.ToString()), Convert.ToInt16(Radio7.SelectedValue.ToString()),
        Convert.ToInt16(Radio8.SelectedValue.ToString()), Convert.ToInt16(Radio9.SelectedValue.ToString()),
        Convert.ToInt16(Radio10.SelectedValue.ToString()),
        txtRx_Cap_doc.Text.ToString(), txtSamQty_Cap_doc.Text.ToString(),
        Convert.ToInt16(Radio11.SelectedValue.ToString()),
        Convert.ToInt16(Radio14.SelectedValue.ToString()),
        Convert.ToInt16(Radio16.SelectedValue.ToString()),
        Convert.ToInt16(Radio31.SelectedValue.ToString()),
        Convert.ToInt16(Radio32.SelectedValue.ToString()), txt_srtdate.SelectedValue,
        txt_enddate.SelectedValue, Convert.ToInt16(Radio33.SelectedValue.ToString()),
        Convert.ToInt16(Radio34.SelectedValue.ToString()), Convert.ToInt16(Radio35.SelectedValue.ToString()),
        Convert.ToInt16(Radio38.SelectedValue.ToString()),
        txtCluster.Text.ToString(),
        //Convert.ToInt16(Radio40.SelectedValue.ToString()),
        //Convert.ToInt16(Radio41.SelectedValue.ToString()), 
        Convert.ToInt16(Radio42.SelectedValue.ToString()),
        //Convert.ToInt16(Radio43.SelectedValue.ToString()), 
        //Convert.ToInt16(Radio44.SelectedValue.ToString()),
        Convert.ToInt16(Radio46.SelectedValue.ToString()),
        txtDpc.Text, txtCPC.Text, txtSPC.Text, txtUPC.Text,
        Convert.ToInt16(Radio85.SelectedValue.ToString()), Convert.ToInt16(Radio86.SelectedValue.ToString()),
        Convert.ToInt16(Radio87.SelectedValue.ToString()), Convert.ToInt16(Radio88.SelectedValue.ToString()),
        Convert.ToInt16(Radio81.SelectedValue.ToString()), Convert.ToInt16(Radio82.SelectedValue.ToString()),
        Convert.ToInt16(Radio83.SelectedValue.ToString()), Convert.ToInt16(Radio84.SelectedValue.ToString()),
         Convert.ToInt16(Radio75.SelectedValue.ToString()), Convert.ToInt16(Radio76.SelectedValue.ToString()),
         Convert.ToInt16(Radio77.SelectedValue.ToString()), Convert.ToInt16(Radio78.SelectedValue.ToString()),
         Convert.ToInt16(Radio79.SelectedValue.ToString()), Convert.ToInt16(Radio80.SelectedValue.ToString()),
          Convert.ToInt16(Radio47.SelectedValue.ToString()), Convert.ToInt16(Radio48.SelectedValue.ToString()),
          Convert.ToInt16(Radio49.SelectedValue.ToString()),
        Convert.ToInt16(Radio50.SelectedValue.ToString()), Convert.ToInt16(Radio51.SelectedValue.ToString()),
        Convert.ToInt16(Radio52.SelectedValue.ToString()), Convert.ToInt16(Radio53.SelectedValue.ToString()),
        Convert.ToInt16(Radio54.SelectedValue.ToString()), Convert.ToInt16(Radio55.SelectedValue.ToString()),
        Convert.ToInt16(Radio56.SelectedValue.ToString()), 0,
        //Convert.ToInt16(Radio57.SelectedValue.ToString()),
        //Convert.ToInt16(Radio58.SelectedValue.ToString()),
        //Convert.ToInt16(Radio59.SelectedValue.ToString()),
        //Convert.ToInt16(Radio60.SelectedValue.ToString()),
        //Convert.ToInt16(Radio61.SelectedValue.ToString()),
        Convert.ToInt16(Radio63.SelectedValue.ToString()),
        Convert.ToInt16(Radio64.SelectedValue.ToString()), Convert.ToInt16(Radio65.SelectedValue.ToString()),
        Convert.ToInt16(Radio66.SelectedValue.ToString()), Convert.ToInt16(Radio67.SelectedValue.ToString()),
        Convert.ToInt16(Radio68.SelectedValue.ToString()), Convert.ToInt16(Radio69.SelectedValue.ToString()),
        Convert.ToInt16(Radio70.SelectedValue.ToString()),
        //Convert.ToInt16(Radio71.SelectedValue.ToString()),
        Convert.ToInt16(Radio72.SelectedValue.ToString()),
        Convert.ToInt16(Radio90.SelectedValue.ToString()),
       Convert.ToInt16(Radio91.SelectedValue.ToString()), Convert.ToInt16(Radio17.SelectedValue.ToString()),
       Convert.ToInt16(Radio89.SelectedValue.ToString()), Convert.ToInt16(Radio92.SelectedValue.ToString()),
       Convert.ToInt16(RadioBtnList1.SelectedValue.ToString()),
       //Convert.ToInt16(RadioBtnList3.SelectedValue.ToString()), 
       Convert.ToInt16(RadiochmsamQty_need.SelectedValue.ToString()),
      Convert.ToInt16(RadioPwdsetup.SelectedValue.ToString()), Convert.ToInt16(Radioexpenseneed.SelectedValue.ToString()),
       Convert.ToInt16(Radiochm_ad_qty.SelectedValue.ToString()), Convert.ToInt16(RadioCampneed.SelectedValue.ToString()),
         Convert.ToInt16(RadioApproveneed.SelectedValue.ToString()), txtDoc_Input_caption.Text,
         txtChm_Input_caption.Text, txtStk_Input_caption.Text,
         txtUl_Input_caption.Text, Convert.ToInt16(RadioChmRxNd.SelectedValue.ToString()),
         Convert.ToInt16(RadioDrSampNd.SelectedValue.ToString()), Convert.ToInt16(RadioCmpgnNeed.SelectedValue.ToString()),
         Convert.ToInt16(RadiorefDoc.SelectedValue.ToString()),
          //Convert.ToInt16(RadioCHEBase.SelectedValue.ToString()),
          Convert.ToInt16(Radiotp_need.SelectedValue.ToString()),
           //Convert.ToInt16(RadiocurrentDay.SelectedValue.ToString()),
           Convert.ToInt16(RadiocntRemarks.Text.ToString()), Convert.ToInt16(Radiopast_leave_post.SelectedValue.ToString()),
         Convert.ToInt16(RadiomyplnRmrksMand.SelectedValue.ToString()),
         Convert.ToInt16(Radioprod_remark.SelectedValue.ToString()), Convert.ToInt16(Radioprod_remark_md.SelectedValue.ToString()),
         //Convert.ToInt16(Radiostp.SelectedValue.ToString()),
         Convert.ToInt16(RadioRemainder_geo.SelectedValue.ToString()),
               //Convert.ToInt16(Radiodcr_edit_rej.SelectedValue.ToString()),
               Convert.ToInt16(RadioDrRcpaQMd.SelectedValue.ToString()),
               Convert.ToInt16(RadiogeoTagImg.SelectedValue.ToString()),
               Convert.ToInt16(RadioHINeed.SelectedValue.ToString()),
                Convert.ToInt16(Radiohosp_need.SelectedValue.ToString()), Convert.ToInt16(RadioHPNeed.SelectedValue.ToString()),
                 Convert.ToInt16(Radioprdfdback.SelectedValue.ToString()),
                  //Convert.ToInt16(Radioques_need.SelectedValue.ToString()),
                  //Convert.ToInt16(Radiorcpaextra.SelectedValue.ToString()), 
                  txtpob_minvalue.Text,
                   Convert.ToInt16(RadioRcpaMd.SelectedValue.ToString()), Convert.ToInt16(RadioRcpa_Competitor_extra.SelectedValue.ToString()),

            //Updated parameters by Ferooz
            //txtGeoTagCap.Text.ToString(),
            Convert.ToInt16(rdoExpenceNd.SelectedValue.ToString()), Convert.ToInt16(rdoExpenceNd_mandatory.SelectedValue.ToString()),
            Convert.ToInt16(RadioRcpaNd.SelectedValue.ToString()), Convert.ToInt16(Radioleavestatus.SelectedValue.ToString()),
            Convert.ToInt16(rdoOrder_management.SelectedValue.ToString()), txtOrder_caption.Text.ToString(),
            Convert.ToInt16(rdoPrimary_order.SelectedValue.ToString()), txtPrimary_order_caption.Text.ToString(),
            Convert.ToInt16(rdoSecondary_order.SelectedValue.ToString()), txtSecondary_order_caption.Text.ToString(),
            Convert.ToInt16(rdoGst_option.SelectedValue.ToString()), txtTaxname_caption.Text.ToString(),
            Convert.ToInt16(rdosecondary_order_discount.SelectedValue.ToString()),
            Convert.ToInt16(Radiomisc_expense_need.SelectedValue.ToString()),
            //Convert.ToInt16(RadioentryFormNeed_entryFormMgr.SelectedValue.ToString()),
            Convert.ToInt16(Radiosurveynd.SelectedValue.ToString()),
            Convert.ToInt16(Radiodashboard.SelectedValue.ToString())
          );

        int iReturnUpdated2 = admin.RecordUpdate_MobAppNew2(
            Convert.ToInt16(rdoTerr_based_Tag.SelectedValue.ToString()), Convert.ToInt16(RadioProd_Stk_Need.SelectedValue.ToString()),
            Convert.ToInt16(RadioDrEvent_Md.SelectedValue.ToString()), Convert.ToInt16(RadioMCLDet.SelectedValue.ToString()),
            txtChmSmpCap.Text.ToString(), Convert.ToInt16(RadioStkEvent_Md.SelectedValue.ToString()),
            txtcip_caption.Text.ToString(), Convert.ToInt16(RadioCIPPOBNd.SelectedValue.ToString()),
            Convert.ToInt16(RadioCIPPOBMd.SelectedValue.ToString()), Convert.ToInt16(RadioCipEvent_Md.SelectedValue.ToString()),
            txthosp_caption.Text.ToString(), Convert.ToInt16(RadioHosPOBNd.SelectedValue.ToString()),
            Convert.ToInt16(RadioHosPOBMd.SelectedValue.ToString()), Convert.ToInt16(RadioHospEvent_Md.SelectedValue.ToString()),
            Convert.ToInt16(RadioUlDrEvent_Md.SelectedValue.ToString()), Convert.ToInt16(RadioRcpaMd_Mgr.SelectedValue.ToString()),
            Convert.ToInt16(RadioDrRCPA_competitor_Need.SelectedValue.ToString()),
            Convert.ToInt16(RadioRCPAQty_Need.SelectedValue.ToString()),
            Convert.ToInt16(RadioChm_RCPA_Need.SelectedValue.ToString()), Convert.ToInt16(RadioChmRcpaMd.SelectedValue.ToString()),
            Convert.ToInt16(RadioChmRcpaMd_Mgr.SelectedValue.ToString()), Convert.ToInt16(Radiosequential_dcr.SelectedValue.ToString()), txtDcrLockDays.Text.ToString(),
            Convert.ToInt16(Radiomydayplan_need.SelectedValue.ToString()), Convert.ToInt16(RadioLeave_entitlement_need.SelectedValue.ToString()), Convert.ToInt16(rdoProduct_Rate_Editable.SelectedValue.ToString()), Convert.ToInt16(Radiomulti_cluster.SelectedValue.ToString()), Convert.ToInt16(RadioCustSrtNd.SelectedValue.ToString()), Convert.ToInt16(RadioTarget_report_Nd.SelectedValue.ToString()),
            Convert.ToInt16(Radiofaq.SelectedValue.ToString()), Convert.ToInt16(RadioRmdrNeed.SelectedValue.ToString()),
            Convert.ToInt16(RadioChmRCPA_competitor_Need.SelectedValue.ToString()), Convert.ToInt16(RadioSpecFilter.SelectedValue.ToString()),
            //Convert.ToInt16(RadioCurrentday_TPplanned.SelectedValue.ToString()), 
            Convert.ToInt16(RadioChmEvent_Md.SelectedValue.ToString()),
            Convert.ToInt16(RadioTerritory_VstNd.SelectedValue.ToString()), Convert.ToInt16(RadioDcr_firstselfie.SelectedValue.ToString()),
            Convert.ToInt16(RadioCIP_jointwork_Need.SelectedValue.ToString()), Convert.ToInt16(RadioTempNd.SelectedValue.ToString()),

            Convert.ToInt16(RadioCipSrtNd.SelectedValue.ToString()), Convert.ToInt16(RadioentryFormNeed.SelectedValue.ToString()),
            Convert.ToInt16(RadioentryFormMgr.SelectedValue.ToString()), TxtRemainder_call_cap.Text.ToString(),
            Convert.ToInt16(RadioRemainder_prd_Md.SelectedValue.ToString()), Txtquiz_heading.Text.ToString(),
            Convert.ToInt16(RadioLocation_track.SelectedValue.ToString()), Txttracking_interval.Text.ToString(),
            Convert.ToInt16(RadiotravelDistance_Need.SelectedValue.ToString()), txtActivityCap.Text.ToString(),
            Convert.ToInt16(RadioActivityNd.SelectedValue.ToString()), Convert.ToInt16(Radioquiz_need_mandt.SelectedValue.ToString()),
            Convert.ToInt16(Radioprimarysec_need.SelectedValue.ToString()), Convert.ToInt16(RadioTarget_report_md.SelectedValue.ToString()),
            txttracking_time1.Text + "-" + txttracking_time2.Text,

            div_code);

        int iReturnCustom = admin.RecordUpdate_MobAppNewCustom(
            Convert.ToInt16(RadioProduct_Stockist.SelectedValue.ToString()), Convert.ToInt16(Radiohosp_filter.SelectedValue.ToString()),
            Convert.ToInt16(RadioDetailing_chem.SelectedValue.ToString()), Convert.ToInt16(RadioDetailing_stk.SelectedValue.ToString()),
            Convert.ToInt16(RadioDetailing_undr.SelectedValue.ToString()), Convert.ToInt16(RadioDcrapprvNd.SelectedValue.ToString()),
            Convert.ToInt16(RadioaddChm.SelectedValue.ToString()), Convert.ToInt16(RadioaddDr.SelectedValue.ToString()),
            Convert.ToInt16(Radioundr_hs_nd.SelectedValue.ToString()),
            //Convert.ToInt16(RadioaddAct.SelectedValue.ToString()),
            Convert.ToInt16(RadioshowDelete.SelectedValue.ToString()),
            //Convert.ToInt16(RadioTarget_sales.SelectedValue.ToString()),
            Convert.ToInt16(RadioPresentNd.SelectedValue.ToString()), Convert.ToInt16(RadioCustNd.SelectedValue.ToString()),
            Convert.ToInt16(Radioyetrdy_call_del_Nd.SelectedValue.ToString()), Convert.ToInt16(Radiotheraptic.SelectedValue.ToString()),
            div_code
        );

        int iReturnTP = admin.RecordUpdate_MobAppNewTP(Convert.ToInt16(RadioDrNeed.SelectedValue.ToString()), Convert.ToInt16(RadioTpChmNeed.SelectedValue.ToString()),
            Convert.ToInt16(RadioTpStkNeed.SelectedValue.ToString()), Convert.ToInt16(RadioTpJWNeed.SelectedValue.ToString()),
            Convert.ToInt16(RadioTpFW_meetup_mandatory.SelectedValue.ToString()),
            Convert.ToInt16(RadioTpHospNeed.SelectedValue.ToString()),
            Convert.ToInt16(RadioTpCip_Need.SelectedValue.ToString()), Convert.ToInt16(RadioTpClusterNeed.SelectedValue.ToString()),
            Convert.ToInt16(RadioTpAddsessionNeed.SelectedValue.ToString()), Convert.ToInt16(ddlTpAddsessionCount.SelectedValue.ToString()),
            Convert.ToInt16(Radioclustertype.SelectedValue.ToString()),
            Convert.ToInt16(Radioedit_holiday.SelectedValue.ToString()), Convert.ToInt16(Radioedit_weeklyoff.SelectedValue.ToString()),
            div_code
        );

        for (int i = 0; i < chkhaf_work.Items.Count; i++)
        {
            if (chkhaf_work.Items[i].Selected)
            {

                check = 1;
                AdminSetup dv = new AdminSetup();
                iReturn = dv.RecordUpdate_Forhalfday(chkhaf_work.Items[i].Value, div_code, check);
            }
            else
            {
                check = 0;
                AdminSetup dv = new AdminSetup();
                iReturn = dv.RecordUpdate_Forhalfday(chkhaf_work.Items[i].Value, div_code, check);
            }
        }

        for (int i = 0; i < chkhaf_work_mgr.Items.Count; i++)
        {
            if (chkhaf_work_mgr.Items[i].Selected)
            {

                check = 1;

                AdminSetup dv = new AdminSetup();
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_Mgr set Hlfdy_flag='" + check + "' " +
                         " where WorkType_Code_M='" + chkhaf_work_mgr.Items[i].Value + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            else
            {
                check = 0;
                AdminSetup dv = new AdminSetup();
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_Mgr set Hlfdy_flag='" + check + "' " +
                         " where WorkType_Code_M='" + chkhaf_work_mgr.Items[i].Value + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }

        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been updated Successfully');</script>");
            Fillsalesforce();
            FillHalfDay_Work();
            FillHalfDay_Work_MGR();
            app_record();
            appset_record();
            app_record_2();
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }

    private void Fillsalesforce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dsSalesForce.Tables[0].Rows[0].Delete();
            grdgps.Visible = true;
            grdgps.DataSource = dsSalesForce;
            grdgps.DataBind();
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        int iReturn = -1;

        foreach (GridViewRow gridRow in grdgps.Rows)
        {
            CheckBox chkId = (CheckBox)gridRow.Cells[0].FindControl("chkId");
            bool bCheck = chkId.Checked;
            Label lblSF_Code = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
            string sf_Code = lblSF_Code.Text.ToString();
            CheckBox chkfencing = (CheckBox)gridRow.Cells[3].FindControl("chkfencing");

            CheckBox GeoFencingche = (CheckBox)gridRow.Cells[4].FindControl("chkfencingche");
            CheckBox GeoFencingstock = (CheckBox)gridRow.Cells[5].FindControl("chkfencingstock");
            CheckBox GeoFencingUnlisted = (CheckBox)gridRow.Cells[6].FindControl("chkfencingUnlisted");
            CheckBox GeoFencingCIP = (CheckBox)gridRow.Cells[7].FindControl("chkfencingCIP");

            AdminSetup ad = new AdminSetup();
            if (chkId.Checked)
            {
                geo_code = 0;
            }
            else
            {
                geo_code = 1;
            }

            if (chkfencing.Checked)
            {
                geo_fencing = 1;
            }
            else
            {
                geo_fencing = 0;
            }
            if (GeoFencingche.Checked)
            {
                Fencingche = 1;
            }
            else
            {
                Fencingche = 0;
            }

            if (GeoFencingstock.Checked)
            {
                Fencingstock = 1;
            }
            else
            {
                Fencingstock = 0;
            }

            if (GeoFencingUnlisted.Checked)
            {
                FencingUnlisted = 1;
            }
            else
            {
                FencingUnlisted = 0;
            }

            if (GeoFencingCIP.Checked)
            {
                FencingCIP = 1;
            }
            else
            {
                FencingCIP = 0;
            }

            iReturn = ad.RecordUpdate_geosf_code2(sf_Code, geo_code, geo_fencing, Fencingche, Fencingstock, FencingUnlisted, FencingCIP);
        }

        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
            hdnmandt.Value = "0";
            app_record_2();
        }
    }

    protected void txt_srtdateSelectedChanged(object sender, EventArgs e)
    {
        int FromDate = Convert.ToInt32(txt_srtdate.SelectedValue);
        txt_enddate.Items.Clear();
        for (int k = 1; k < 31 + 1; k++)
        {
            if (k >= FromDate)
            {
                txt_enddate.Enabled = true;
                txt_enddate.Items.Add(new ListItem("" + k + "", "" + k + ""));
            }
        }
    }

    protected void linkgps_Click(object sender, EventArgs e)
    {
        pnlpopup.Style.Add("display", "block");
        pnlpopup.Style.Add("visibility", "visible");
    }
}

