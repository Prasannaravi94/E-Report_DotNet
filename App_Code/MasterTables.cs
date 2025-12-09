using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MasterTables
/// </summary>
namespace Bus_EReport
{
    public class ApiDetails
    {

        public string tableName { get; set; }
        public string coloumns { get; set; }
        public string today { get; set; }
        public string where { get; set; }
        public string or { get; set; }
        public string wt { get; set; }
        public string orderBy { get; set; }
        public string divisionCode { get; set; }
        public string tp_year { get; set; }
        public string tp_month { get; set; }
        public string SF { get; set; }

    }
    public class TpData
    {

        public string SFCode { get; set; }

        public string SFName { get; set; }
        public string Div { get; set; }
        public string Mnth { get; set; }
        public string Yr { get; set; }
        public string dayno { get; set; }

        public string Change_Status { get; set; }
        public string Rejection_Reason { get; set; }
        public DateFmt TPDt { get; set; }
        public string WTCode { get; set; }
        public string WTCode2 { get; set; }
        public string WTCode3 { get; set; }
        public string WTName { get; set; }
        public string WTName2 { get; set; }
        public string WTName3 { get; set; }
        public string ClusterCode { get; set; }
        public string ClusterCode2 { get; set; }
        public string ClusterCode3 { get; set; }
        public string ClusterName { get; set; }
        public string ClusterName2 { get; set; }
        public string ClusterName3 { get; set; }
        public string ClusterSFs { get; set; }
        public string ClusterSFNms { get; set; }
        public string JWCodes { get; set; }
        public string JWNames { get; set; }
        public string JWCodes2 { get; set; }
        public string JWNames2 { get; set; }
        public string JWCodes3 { get; set; }
        public string JWNames3 { get; set; }
        public string Dr_Code { get; set; }
        public string Dr_Name { get; set; }
        public string Dr_two_code { get; set; }
        public string Dr_two_name { get; set; }
        public string Dr_three_code { get; set; }
        public string Dr_three_name { get; set; }
        public string Chem_Code { get; set; }
        public string Chem_Name { get; set; }
        public string Chem_two_code { get; set; }
        public string Chem_two_name { get; set; }
        public string Chem_three_code { get; set; }
        public string Chem_three_name { get; set; }
        public string Stockist_Code { get; set; }
        public string Stockist_Name { get; set; }
        public string Stockist_two_code { get; set; }
        public string Stockist_two_name { get; set; }
        public string Stockist_three_code { get; set; }
        public string Stockist_three_name { get; set; }
        public string Day { get; set; }
        public string Tour_Month { get; set; }
        public string Tour_Year { get; set; }
        public string tpmonth { get; set; }
        public string tpday { get; set; }
        public string DayRemarks { get; set; }
        public string DayRemarks2 { get; set; }
        public string DayRemarks3 { get; set; }
        public string access { get; set; }
        public string EFlag { get; set; }
        public string FWFlg { get; set; }
        public string FWFlg2 { get; set; }
        public string FWFlg3 { get; set; }
        public string HQCodes { get; set; }
        public string HQNames { get; set; }
        public string HQCodes2 { get; set; }
        public string HQNames2 { get; set; }
        public string HQCodes3 { get; set; }
        public string HQNames3 { get; set; }
        public DateFmt1 submitted_time { get; set; }
        public string Entry_mode { get; set; }


    }

    public class DateFmt
    {
        public string date { get; set; }
        public string timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class DateFmt1
    {
        public string date { get; set; }
        public string timezone_type { get; set; }
        public string timezone { get; set; }
    }


    public class vwMyDayPlan
    {
        public string SFCode { get; set; }
        public DateFmt TPDt { get; set; }
        public string worktype { get; set; }

        public string WTNm { get; set; }
        public string FWFlg { get; set; }
        public string subordinateid { get; set; }
        public string HQNm { get; set; }
        public string clusterid { get; set; }

        public string clstrName { get; set; }
        public string remarks { get; set; }
        public string TpVwFlg { get; set; }
        public string TP_Doctor { get; set; }
        public string TP_cluster { get; set; }
        public string TP_worktype { get; set; }
        public string TP_HQCode { get; set; }
    }
    public class subordinate
    {
        public string id { get; set; }
        
        public string name { get; set; }

        public string SF_HQ { get; set; }
        public DateFmt SF_DOB { get; set; }
        public DateFmt SF_DOW { get; set; }
        public string steps { get; set; }
        public string Reporting_To_SF { get; set; }
       
    }
    public class appSetup
    {
        public bool success { get; set; }
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string UserN { get; set; }
        public string username { get; set; }
        public string Pass { get; set; }
        public string divisionCode { get; set; }
        public string sftype { get; set; }
        public string desigCode { get; set; }
        public string SFStat { get; set; }
        public DateFmt SFTPDate { get; set; }
        public string AppTyp { get; set; }
        public string sample_validation { get; set; }
        public string input_validation { get; set; }
        public string GeoChk { get; set; }
        public string GEOTagNeed { get; set; }
        public string GEOTagNeedche { get; set; }
        public string GEOTagNeedstock { get; set; }
        public string GEOTagNeedunlst { get; set; }
        public string Android_App { get; set; }
        public string Tp_Start_Date { get; set; }
        public string Tp_End_Date { get; set; }
        public string TBase { get; set; }
        public string UNLNeed { get; set; }
        public string DrCap { get; set; }
        public string ChmCap { get; set; }
        public string StkCap { get; set; }
        public string NLCap { get; set; }
        public string ChmNeed { get; set; }
        public string StkNeed { get; set; }
        public string DPNeed { get; set; }
        public string DINeed { get; set; }
        public string CPNeed { get; set; }
        public string CINeed { get; set; }
        public string SPNeed { get; set; }
        public string SINeed { get; set; }
        public string NPNeed { get; set; }
        public string NINeed { get; set; }
        public string DRxCap { get; set; }
        public string DSmpCap { get; set; }
        public string CQCap { get; set; }
        public string SQCap { get; set; }
        public string NRxCap { get; set; }
        public string NSmpCap { get; set; }
        public string DisRad { get; set; }
        public string Attendance { get; set; }
        public string MCLDet { get; set; }
        public string doctor_dobdow { get; set; }
        public string Doc_Pob_Mandatory_Need { get; set; }
        public string Chm_Pob_Mandatory_Need { get; set; }
        public string multiple_doc_need { get; set; }
        public string mailneed { get; set; }
        public string circular { get; set; }
        public string DrRxNd { get; set; }
        public string DrRxQMd { get; set; }
        public string DrSmpQMd { get; set; }
        public string FeedNd { get; set; }
        public string DrPrdMd { get; set; }
        public string DrInpMd { get; set; }
        public string RcpaNd { get; set; }
        public string VstNd { get; set; }
        public string MsdEntry { get; set; }
        public string TPDCR_Deviation { get; set; }
        public string TPDCR_MGRAppr { get; set; }
        public string NextVst { get; set; }
        public string NextVst_Mandatory_Need { get; set; }
        public string Appr_Mandatory_Need { get; set; }
        public string RCPAQty_Need { get; set; }
        public string Prod_Stk_Need { get; set; }
        public string TP_Mandatory_Need { get; set; }
        public string dayplan_tp_based { get; set; }
        public string Sep_RcpaNd { get; set; }
        public string DlyCtrl { get; set; }
        public string wrk_area_Name { get; set; }
        public string prod_det_need { get; set; }
        public string quiz_need { get; set; }
        public string cip_need { get; set; }
        public string CIP_PNeed { get; set; }
        public string CIP_INeed { get; set; }
        public string mediaTrans_Need { get; set; }
        public string DrFeedMd { get; set; }
        public string prdfdback { get; set; }
        public string Doc_Pob_Need { get; set; }
        public string Chm_Pob_Need { get; set; }
        public string Stk_Pob_Need { get; set; }
        public string Ul_Pob_Need { get; set; }
        public string Stk_Pob_Mandatory_Need { get; set; }
        public string Ul_Pob_Mandatory_Need { get; set; }
        public string Doc_jointwork_Need { get; set; }
        public string Chm_jointwork_Need { get; set; }
        public string Stk_jointwork_Need { get; set; }
        public string Ul_jointwork_Need { get; set; }
        public string days { get; set; }
        public string product_pob_need_msg { get; set; }
        public string sfEmail { get; set; }
        public string sfMobile { get; set; }
        public string DS_name { get; set; }
        public string Doc_jointwork_Mandatory_Need { get; set; }
        public string Chm_jointwork_Mandatory_Need { get; set; }
        public string Stk_jointwork_Mandatory_Need { get; set; }
        public string Ul_jointwork_Mandatory_Need { get; set; }
        public string Doc_Product_caption { get; set; }
        public string Chm_Product_caption { get; set; }
        public string Stk_Product_caption { get; set; }
        public string Ul_Product_caption { get; set; }
        public string DFNeed { get; set; }
        public string CFNeed { get; set; }
        public string SFNeed { get; set; }
        public string CIP_FNeed { get; set; }
        public string NFNeed { get; set; }
        public string HFNeed { get; set; }
        public string CIP_QNeed { get; set; }
        public string DENeed { get; set; }
        public string CENeed { get; set; }
        public string SENeed { get; set; }
        public string NENeed { get; set; }
        public string CIP_ENeed { get; set; }
        public string HENeed { get; set; }
        public string Expenseneed { get; set; }
        public string Catneed { get; set; }
        public string Campneed { get; set; }
        public string Approveneed { get; set; }
        public string Doc_Input_caption { get; set; }
        public string call_report { get; set; }
        public string Chm_Input_caption { get; set; }
        public string Stk_Input_caption { get; set; }
        public string Ul_Input_caption { get; set; }
        public string RmdrNeed { get; set; }
        public string TempNd { get; set; }
        public string DrSampNd { get; set; }
        public string CmpgnNeed { get; set; }
        public string quote_Text { get; set; }
        public string entryFormNeed { get; set; }
        public string mydayplan_need { get; set; }
        public string CHEBase { get; set; }
        public string TPDCR_Deviation_Appr_Status { get; set; }
        public string tp_new { get; set; }
        public string tp_need { get; set; }
        public string currentDay { get; set; }
        public string past_leave_post { get; set; }
        public string myplnRmrksMand { get; set; }
        public string entryFormMgr { get; set; }
        public string prod_remark { get; set; }
        public string stp { get; set; }
        public string Remainder_geo { get; set; }
        public string geoTagImg { get; set; }
        public string cntRemarks { get; set; }
        public string hosp_need { get; set; }
        public string HPNeed { get; set; }
        public string HINeed { get; set; }
        public string chmsamQty_need { get; set; }
        public string Pwdsetup { get; set; }
        public string RcpaMd { get; set; }
        public string Rcpa_Competitor_extra { get; set; }
        public string dashboard { get; set; }
        public string Order_management { get; set; }
        public string Order_caption { get; set; }
        public string Primary_order_caption { get; set; }
        public string Secondary_order_caption { get; set; }
        public string Primary_order { get; set; }
        public string Secondary_order { get; set; }
        public string Gst_option { get; set; }
        public string TPbasedDCR { get; set; }
        public string CIP_jointwork_Need { get; set; }
        public string CIP_Caption { get; set; }
        public string hosp_caption { get; set; }
        public string misc_expense_need { get; set; }
        public string Location_track { get; set; }
        public string tracking_time { get; set; }
        public string Taxname_caption { get; set; }
        public string SurveyNd { get; set; }
        public string SrtNd { get; set; }
        public string quiz_need_mandt { get; set; }
        public string quiz_heading { get; set; }
        public string Product_Rate_Editable { get; set; }
        public string CustSrtNd { get; set; }
        public string ActivityNd { get; set; }
        public string ChmSmpCap { get; set; }
        public string product_pob_need { get; set; }
        public string secondary_order_discount { get; set; }
        public string GeoTagNeedcip { get; set; }
        public string Target_report_md { get; set; }
        public string RCPA_unit_nd { get; set; }
        public string Chm_RCPA_Need { get; set; }
        public string DrRCPA_competitor_Need { get; set; }
        public string ChmRCPA_competitor_Need { get; set; }
        public string Currentday_TPplanned { get; set; }
        public string Doc_cluster_based { get; set; }
        public string Chm_cluster_based { get; set; }
        public string Stk_cluster_based { get; set; }
        public string UlDoc_cluster_based { get; set; }
        public string multi_cluster { get; set; }
        public string Terr_based_Tag { get; set; }
        public string RcpaMd_Mgr { get; set; }
        public string DrNeed { get; set; }
        public string faq { get; set; }
        public string edit_holiday { get; set; }
        public string edit_weeklyoff { get; set; }
        public string Target_report_Nd { get; set; }
        public string DcrLockDays { get; set; }
        public string Doc_pob_caption { get; set; }
        public string Stk_pob_caption { get; set; }
        public string Chm_pob_caption { get; set; }
        public string Uldoc_pob_caption { get; set; }
        public string CIP_pob_caption { get; set; }
        public string Hosp_pob_caption { get; set; }
        public string Remainder_call_cap { get; set; }
        public string DrEvent_Md { get; set; }
        public string StkEvent_Md { get; set; }
        public string UlDrEvent_Md { get; set; }
        public string CipEvent_Md { get; set; }
        public string HospEvent_Md { get; set; }
        public string sequential_dcr { get; set; }
        public string Leave_entitlement_need { get; set; }
        public string ChmEvent_Md { get; set; }
        public string primarysec_need { get; set; }
        public string Territory_VstNd { get; set; }
        public string Dcr_firstselfie { get; set; }
        public string CipSrtNd { get; set; }
        public string travelDistance_Need { get; set; }
        public string ChmRxQty { get; set; }
        public string missedDateMand { get; set; }
        public string doc_business_product { get; set; }
        public string doc_business_value { get; set; }
        public string dcr_doc_business_product { get; set; }
        public string Dr_mappingproduct { get; set; }
        public string pro_det_need { get; set; }
        public string HosPOBNd { get; set; }
        public string HosPOBMd { get; set; }
        public string CIPPOBNd { get; set; }
        public string CIPPOBMd { get; set; }
        public string Remainder_prd_Md { get; set; }
        public string Dcr_summary_need { get; set; }
        public string tracking_interval { get; set; }
        public string GeoTagging { get; set; }
        public string No_of_TP_View { get; set; }
        public string call_report_from_date { get; set; }
        public string call_report_to_date { get; set; }
        public string Sample_Val_Qty { get; set; }

        public string Input_Val_Qty { get; set; }

        public string Authentication { get; set; }
        public string GeoTagApprovalNeed { get; set; }
        public string ChmSrtNd { get; set; }
        public string UnlistSrtNd { get; set; }
        public string RCPA_competitor_add { get; set; }
        public string rcpaextra { get; set; }
        




    }
    public class survey_head
    {
        public string id { get; set; }
        // public DateFmt TPDt { get; set; }
        public string name { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public List<survey_detail> survey_for { get; set; }


    }


    public class survey_detail
    {
        public string id { get; set; }
        // public DateFmt TPDt { get; set; }
        public string Survey { get; set; }

        public string DrCat { get; set; }
        public string DrSpl { get; set; }
        public string DrCls { get; set; }
        public string HosCls { get; set; }
        public string ChmCat { get; set; }
        public string Stkstate { get; set; }
        public string StkHQ { get; set; }
        public string Stype { get; set; }
        public string Qc_id { get; set; }
        public string Qtype { get; set; }
        public string Qlength { get; set; }
        public string Mandatory { get; set; }
        public string Qname { get; set; }

        public string Qanswer { get; set; }
        public string Active_Flag { get; set; }


    }


    public class appSetup_new
    {
        public bool success { get; set; }
    }


    public class day_checkin
    {

        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public DateFmt Activity_date { get; set; }
        public string Start_Time { get; set; }

    }
    public class cus_checkin
    {

        public string id { get; set; }
        public string name { get; set; }

        public DateFmt Activity_date { get; set; }
        public DateFmt Checkin_time { get; set; }
        public DateFmt Checkout_time { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

    }
    public class Chemist_checkin
    {

        public string id { get; set; }
        public string name { get; set; }

        public DateFmt Activity_date { get; set; }
        public DateFmt Checkin_time { get; set; }
        public DateFmt Checkout_time { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

    }
    public class unlisted_checkin
    {

        public string id { get; set; }
        public string name { get; set; }

        public DateFmt Activity_date { get; set; }
        public DateFmt Checkin_time { get; set; }
        public DateFmt Checkout_time { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

    }
    public class sample
    {
        public string SF { get; set; }
        public string DivisionCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Pack { get; set; }
        public string Balance_Stock { get; set; }
    }
    public class input
    {
        public string SF { get; set; }
        public string DivisionCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        // public string Pack { get; set; }
        public string Balance_Stock { get; set; }
    }

}