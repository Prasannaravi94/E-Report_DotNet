<?php
function reportsget() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");
	
	//get approvals
	switch ( strtolower( $data[ 'tableName' ] ) ) {

        case "getdayrpt":
		    $query = "exec ios_getDayReportApp_Edet '" . $data['sfcode'] . "','" . $data['rptDt'] . "'"; 			
		    outputJSON( performQuery($query));
            break; 
        case "getmnthsumm":
         	$query = "exec ios_getMonthSummaryApp_Edet '" . $data['sfcode'] . "','" . $data['rptDt'] . "'";
            outputJSON( performQuery($query));
            break; 
        case "getvwvstdet":
         	$query="exec ios_spGetVstDetApp_Edet '".$data['ACd']."','" . $data['typ'] . "'";
		    outputJSON( performQuery($query));
            break; 
		case "getslidedet":
         	$query="exec ios_getsliderpt_Edet '".$data['ACd']."','" . $data['Mslcd'] . "'";
		    outputJSON( performQuery($query));
            break; 		
        case "getmissedrpt":
		
            $first ="SELECT cast(format(cast('".$data['rptDt']."' as datetime), 'yyyy-MM-01') as varchar) fdate"; 
	        $res = performQuery($first);
            $query = "exec ios_Missedreport_app_Edet '" . $data['sfcode'] . "','" . $data['rptDt'] . "','" . $res[0]["fdate"] . "'"; 
            outputJSON(performQuery($query));
            break; 	
        case "getmissedrptview":
           $query = "exec ios_Missedcall_report_app_Edet '" . $DivCode[0] . "','" .  $data['sfcode']  . "','" . date('n', strtotime($data['report_date'])) . "','" . date('Y', strtotime($data['report_date'])) . "','" . $data['report_date'] . "'"; 		   
            outputJSON(performQuery($query));
            break; 	
        case "getvisitmonitor":
         	$query="exec ios_Visit_Coverage_Analysis_App_Edet '" . $DivCode[0] . "','".$data['sfcode']."','".$data['month']."','".$data['year']."'";
            outputJSON(performQuery($query));
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

