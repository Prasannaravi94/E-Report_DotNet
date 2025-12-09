<?php
function setups() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
	// need to get app setup's
	switch ( strtolower( $data[ 'tableName' ] ) ) {
        case "getsetups":
            $sql = "exec iOS_getSetups_Edet '" . $data['sfcode'] . "'";
			//$sql = "exec iOS_getSetups_Edet_Test '" . $data['sfcode'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getcustomsetup":
            $sql = "select division_code,hosp_filter,addDr,showDelete,Detailing_chem,Detailing_stk,Detailing_undr,addChm,DcrapprvNd,Product_Stockist,undr_hs_nd,Target_sales,yetrdy_call_del_Nd,addAct,PresentNd,CustNd,theraptic,addCoach,DayCoach_Md,Detailing_rpt,productFB,DrProfile,Product_Stockist_Md,Tourplan_MGR,addAct_Md,add_dashboard,Pob_Stockist_Nd,Pob_Unlstdr_Nd,Pob_Stockist_Mandatory_Need,Pob_Unlstdr_Mandatory_Need,Additional_Call from Custom_Table where division_code='" . $div_code[0] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "gettpsetup":
	        $sql = "select SF_code,AddsessionNeed,AddsessionCount,DrNeed,ChmNeed,JWNeed,ClusterNeed,clustertype,div,StkNeed,Cip_Need,HospNeed,FW_meetup_mandatory,max_doc,tp_objective,Holiday_Editable,Weeklyoff_Editable from tpSetup where div='".$div_code[0]."'";	
			outputJSON( performQuery( $sql ) );
            break;        

        default:
            $result = array();
			$result['success'] = false;
			$result['msg'] = 'Try Again';
			outputJSON($result);
            break;
    } 
}
?>


