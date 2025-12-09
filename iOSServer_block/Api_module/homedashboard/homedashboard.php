<?php 
function homedashboard(){ 
	global $data;  

	switch ( strtolower( $data[ 'tableName' ] ) ) {
		case "getcallavgyrcht":
			//$query = "exec iOS_getDrCallAvgBySFYr_Edet '".$data[ 'sfcode' ]."',".date('Y'); 
			$query1 = []; 
			outputJSON($query1);
			break; 
		case "getcallvst":		
            $query = "exec iOS_getTVstDetails_Edet '".$data['sfcode'] ."',".date('m').",".date('Y');
			outputJSON( performQuery($query));
			break;
		case "getcatvst":		
            $query = "exec iOS_getCurrCateVst_Edet '".$data['sfcode'] ."',".date('m').",".date('Y');
			outputJSON( performQuery($query));
			break;	
        case "getcallavgyrcht_chm":
			$query = "exec iOS_getCSHCallAvgBySFYr_Edet '".$data[ 'sfcode' ]."',".date('Y');
			outputJSON( performQuery($query));
			break;
		case "getcallvst_chm":
			$query = "exec iOS_getCSH_TVstDetails_Edet '".$data['sfcode'] ."',".date('m').",".date('Y');
			outputJSON( performQuery($query));
			break;
		case "getcatvst_chm":
			$query = "exec iOS_getCSH_CurrCateVst_Edet '".$data['sfcode'] ."',".date('m').",".date('Y');
			outputJSON( performQuery($query));
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