<?php
function approvalssave() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");
	
	//get approvals
	switch ( strtolower( $data[ 'tableName' ] ) ) {

		case "dcrapproval":
            $date = date('Y-m-d', strtotime( str_replace('/', '-', $data['date'])));
            $sql = "exec ios_ApproveDCRByDt_Edet '".$data['sfcode']."','$date'";
            performQuery($sql);
            $resp["success"] = true;
			outputJSON($resp);
            break;
	    case "dcrreject":
            $date = date('Y-m-d', strtotime( str_replace('/', '-', $data['date'])));
            $sql = "exec ios_App_DcrReject_Edet'" . $data['sfcode'] . "','$date','".$data['reason']."','".$DivCode[0]."'";			
            performQuery($sql);    			
            $resp["success"] = true;
			 outputJSON($resp);
            break;
	  case "leaveapproverej":
         	$query="exec iOS_svLeaveAppRej_Edet  '".$data['LvID']."','".$data['LvAPPFlag']."','".$data['RejRem']."','". $data['sfcode']."'";
             performQuery($query);
	        $result["Qry"]=$query;
            $result["success"]=true;
             outputJSON($result);
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

