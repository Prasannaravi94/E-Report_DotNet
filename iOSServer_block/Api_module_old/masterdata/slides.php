<?php
function slides() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
	// need to get product slides
	switch ( strtolower( $data[ 'tableName' ] ) ) {
        case "getprodslides":
            $sql = "exec ios_getProductSlides_Edet '".$div_code[0]."','" . $data['subdivision_code'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getslidespeciality":
            $sql = "exec ios_getSlideSpecialityPriority_Edet '".$div_code[0]."','" . $data['subdivision_code'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getslidebrand":
		    $sql = "exec ios_getSlideBrandPriority_Edet '".$div_code[0]."','" . $data['subdivision_code'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
		case "gettheraptic":
		    $sql = "exec iOS_getTheraptic_Edet '" . $div_code[0] . "'";
			 
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


