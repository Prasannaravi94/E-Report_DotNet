<?php
function subordinates() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
	// need to check mr / mgr wise use
	switch ( strtolower( $data[ 'tableName' ] ) ) {
        case "getsubordinate":
            $sql = "exec ios_getHyrSF_Edet '" . $data['sfcode'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getsubordinatemgr":
            $sql = "exec ios_getHyrSF_MGR_Edet '" . $data['sfcode'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getjointwork":
		    $sql = "exec ios_getJointWork_Edet '" . $data['Rsf'] . "','" . $data['sfcode'] . "',".$div_code[0]."";
			outputJSON( performQuery( $sql ) );
            break; 
		case "getfullsubordinate":
		    $sql = "exec iOS_getFullHryList_Edet '" . $data['sfcode'] . "'";
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

