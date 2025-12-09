<?php
function tp() {
	global $data;
	$DivCodes = (string) $data['DivCode'];
    $DivCode = explode(",", $DivCodes.",");

	//Save Tour Plan
	switch ( strtolower( $data[ 'tableName' ] ) ) {
	 	case "savetp":
		    include 'Api_module/tp/tpdatasave.php';
			
			 tpdatasave($data);
            break; 
        case "savetpapproval":
		  $query="exec iOS_svTPApprove_Edet '".$data['Rsf']."','".$data['Month']."','".$data['Year']."'";
		    performQuery($query);
            $result["success"]=true;
	        outputJSON($result);
            break; 
        case "savetpreject":
         	$query="exec iOS_svTPReject_Edet '".$data['Rsf']."','".$data['Month']."','".$data['Year']."','".$data['reason']."'";
		    performQuery($query);
            $result["success"]=true;
	        outputJSON($result);
            break; 
		 case "savetpsubmit":
         	$query="exec iOS_svTPSubmit_Edet '".$data['sfcode']."','".$data['Month']."','".$data['Year']."'";
			echo $query;
		    performQuery($query);
            $result["success"]=true;
	        outputJSON($result);
            break;	
        case "gettpapproval":
         	$query="exec iOS_getTPApproval_Edet '".$data['sfcode']."'";
		    outputJSON( performQuery($query));
            break; 	
        case "gettpdetail":
         	$query="exec iOS_getTourPlanDetail_Edet '".$data['sfcode']."','".$data['Month']."','".$data['Year']."'";
            $res=performQuery($query);
            outputJSON($res);
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