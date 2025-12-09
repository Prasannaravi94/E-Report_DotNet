<?php
function misseddcrget() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");
    $query="exec ios_Get_MissedDates_App_detail_Edet '".$data['sfcode']."'";
   outputJSON(performQuery($query));
}
?>
