<?php
function products() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");

	// need to get product slides
	switch ( strtolower( $data[ 'tableName' ] ) ) {
        case "getcompdet":
            $sql = "exec iOS_getMas_CompetitorDets_Edet '".$div_code[0]."'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getinputs":
            $sql = "exec iOS_getInputs_Edet '" . $data['subdivision_code'] . "','" . $data['state_code'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getproducts":
		    $sql = "exec iOS_getProducts_Edet '" . $data['subdivision_code'] . "','" . $data['state_code'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
		case "getbrands":
		    $sql = "exec iOS_getBrands_Edet '".$div_code[0]."'";
			outputJSON( performQuery( $sql ) );
            break;  
        case "getproductfb":
		    $sql = "select FeedBack_Id id,FeedBack_Name name from Mas_Product_Feedback where Division_code='" . $div_code[0] . "' ";
			outputJSON( performQuery( $sql ) );
            break;  			
        default:
            echo 'Sync Default';
            break;
    } 
}
?>

