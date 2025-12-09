<?php
function dcrsave() {
	global $data,$conn;
	$div_codes = (string) $data['division_code'];	
	$DivCode = explode(",", $div_codes.",");
	   $ARCd="";
   //echo $DivCode[0];
    if($data['amc']!='') {
		include 'Api_module/dcr/dcrdelete.php';
		
		dcrdelete($data['amc'],$data['CusType']);
	}

    $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$data['sfcode']."' and Activity_Date='".date('Y-m-d 00:00:00.000', strtotime($data['vstTime']))."' and FWFlg='L'";
    $ExisArr = performQuery($query);
	if ($ExisArr[0]["Cnt"]>0){
			$result["Msg"]="Today Already Leave Posted...";
			$result["success"]=false;
			 outputJSON($result);
	}
    
     $sql="insert into tracking_edet_dcr select '".$data['sfcode']."','".$DivCode[0]."','".json_encode($_POST['data'],true)."',getdate(),'".date('Y-m-d 00:00:00.000', strtotime($data['vstTime']))."','Edet'";
     performQuery($sql);
	// echo $sql;
    $query="exec ios_svDCRMain_Edet '" .$data['sfcode']. "','" . date('Y-m-d 00:00:00.000', strtotime($data['vstTime'])) . "','" . $data['WT_code'] . "','" . $data['WTName'] . "','" . $data['FWFlg'] . "','" . $data['town_code'] . "','".$data['town_name']."','" . $DivCode[0] . "','" . $data['Remarks'] . "','" . $data['sf_type'] . "','" . $data['state_code'] . "','','".$data['Mod']."'";
			  
    $result["HQry"]=$query;
    $tr=performQuery($query);
	$ARCd=$tr[0]['ARCode'];
	
	include 'Api_module/dcr/dcrsavecommon.php';
	    dcrsavecommon($data,$ARCd);
	 
		  
}
?>


