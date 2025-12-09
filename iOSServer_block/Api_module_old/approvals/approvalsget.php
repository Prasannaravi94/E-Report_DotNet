<?php
function approvalsget() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");
	
	//get approvals
	switch ( strtolower( $data[ 'tableName' ] ) ) {
	 
        case "getlvlapproval":
		    $query="exec iOS_getLvApproval_Edet '".$data['sfcode']."'";
		    outputJSON( performQuery($query));
            break; 
        case "getvwdcr":
         	 $query = "select d.Plan_Name,d.Trans_SlNo,d.Sf_Code,d.FieldWork_Indicator,d.WorkType_Name,d.Sf_Name,convert(varchar,Activity_Date,103) Activity_Date,s.Reporting_To_SF from DCRMain_Temp d
            inner join Mas_Salesforce_One s on d.Sf_Code=s.Sf_Code
            where d.Confirmed=1 and d.FieldWork_Indicator<>'L' and s.Reporting_To_SF='".$data['sfcode']."' and cast(Activity_Date as date)<cast(GETDATE() as date)";
            outputJSON( performQuery($query));
            break; 
        case "gettpapproval":
         	$query="exec iOS_getTPApproval_Edet '".$data['sfcode']."'";
		    outputJSON( performQuery($query));
            break; 	
       /* case "getsurvey":
         	$query="exec iOS_getTourPlanDetail_Edet '".$data['sfcode']."','".$data['Month']."','".$data['Year']."'";
            outputJSON($res);
            break; 	*/			
		
	    case "getvwdcrone":
            $sql = "exec ios_getDCRApprovalApp_Edet '" . $data['Trans_SlNo'] . "'";
            outputJSON(performQuery($sql));
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

