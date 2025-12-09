<?php
function leavemodulesave(){ 
		global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");
    
    $query="exec iOS_svLeaveApp_Edet '".$data['sfcode']."','".$data['FDate']."','".$data['TDate']."','".$data['NOD']."','".$data['LeaveType']."','".$data['LvRem']."','".$data['LvOnAdd']."','".$DivCode[0]."','".$data['sf_type']."','".$data['sf_emp_id']."','".$data['leave_typ_code']."'";
    performQuery($query);
	 $result["Qry"]=$query;
     $result["success"]=true;
     outputJSON($result);
}

?>