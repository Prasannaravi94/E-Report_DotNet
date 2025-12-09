<?php
function stockbalance() {
	global $data; 
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
			$sql1 = "exec iOS_GetSampleStockDetail_Edet '".$data['sfcode']."', '".$div_code[0]."'";
			$result1 = performQuery( $sql1 );
			$sql2 = "exec iOS_GetInputStockDetail_Edet '".$data['sfcode']."', '".$div_code[0]."'";
			//echo $sql2;
			$result2 = performQuery( $sql2 );
			$Response[ 'Sample_Stock' ] = $result1;
			$Response[ 'Input_Stock' ] = $result2;
			outputJSON($Response);
}
?>