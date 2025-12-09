<?php
function remaindersave(){ 
	global $data;
	$remdrArr=$data;
	$remdrArr1=$remdrArr[0]['tbRemdrCall'];
	//echo($remdrArr1['sf_emp_id']);
	 //echo $remdrArr1[0]['sf_emp_id'];
	$sql = "exec iOS_sv_RemainderCall_Edet '" . $remdrArr1['sf_emp_id'] . "','".$remdrArr1['sfcode']."','".$remdrArr1['vstTime']."','".$remdrArr1['Doctor_ID']."','".$remdrArr1['WWith']."','".$remdrArr1['WWithNm']."','".$remdrArr1['Prods']."','".$remdrArr1['ProdsNm']."','".$remdrArr1['Remarks']."','".$remdrArr1['location']."','".$remdrArr1['division_code']."'";
	performQuery( $sql ) ;
	
	//echo $sql;
	
	$result["success"]=true;
	$result[ "msg" ] = "Saved successfully";
    outputJSON($result);
	
}

?>