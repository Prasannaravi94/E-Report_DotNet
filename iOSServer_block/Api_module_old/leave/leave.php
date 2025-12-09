<?php
function leave() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");
		 
	switch ( strtolower( $data[ 'tableName' ] ) ) {
	 	
        case "getleavetype":
		    $query = "select Leave_code,Leave_SName,Leave_Name,Division_Code,Active_Flag,Created_Date from mas_Leave_Type where division_code='".$DivCode[0]."'";	
		    outputJSON(performQuery($query));
            break; 
        case "getleavestatus":
         	  $sql = "select Leave_Type_Code,cast(isnull(No_Of_Days,'0') as varchar) Elig,cast(isnull(Leave_Taken_Days,0) as varchar) Taken,cast(isnull(Leave_Balance_Days,0) as varchar) Avail,Leave_code from vwLeaveEntitle where Sf_Code='".$data['sfcode']."' and Leave_Type_Code in ('CL' ,'PL' ,'SL' ,'LOP' ) order by replace(Leave_Type_Code,'LOP','ZOP')";
        $leave = performQuery($sql);
	
		   
	        outputJSON($leave);
            break; 
        case "getlvlvalid":
         
            $query="exec iOS_getLvlValidate_Edet  '".$data['sfcode']."','".$data['Fdt']."','".$data['Tdt']."','".$data['LTy']."','".$data['state_code']."','".$DivCode[0]."'";
   
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