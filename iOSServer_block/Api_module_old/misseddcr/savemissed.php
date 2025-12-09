<?php
function savemissed() {
	global $data;
	$DivCodes = (string) $data['EData'][0]['division_code'];
    $DivCode = explode(",", $DivCodes.",");
		
    $VstTime = $data['EDt'];
    $DCRDt = date('Y-m-d 00:00:00.000', strtotime($VstTime));
    $CallsData=$data['EData'];
    for ($dloop = 0; $dloop < count($CallsData); $dloop++) 
    {
		    $idata=$CallsData[$dloop];
			
	
			$sql="insert into tracking_edetdcr select '" .$idata['sfcode']. "','" . $DivCode[0] . "','$idata',getdate(),'$DCRDt','Edet'";
			performQuery($sql);
		    $ARCd = "";
		    $ARDCd = ""; 
			$mod=(string) $idata['Mod'];			
			if($mod==""){
				$mod="iOS";
			}
			
			$work=$idata['WT_code'];
			if($work!=0)
        {
		       
		    $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='" .$idata['sfcode']. "' and Activity_Date='".$DCRDt."' and FWFlg='L'";
		    $ExisArr = performQuery($query);
		    if ($ExisArr[0]["Cnt"]>0){
			    $result["Msg"]="Today Already Leave Posted...";
			    $result["success"]=false;
			    return $result;
		    }
			$query="exec ios_svDCRMain_Edet '" .$idata['sfcode']. "','" . $DCRDt . "','" . $idata['WT_code'] . "','" . $idata['WTName'] . "','" . $idata['FWFlg'] . "','" . $idata['town_code'] . "','".$idata['town_name']."','" . $DivCode[0] . "','" . $idata['Remarks'] . "','" . $idata['sf_type'] . "','" . $idata['state_code'] . "','','iOS'";
			 // print_r($query);die;
    $result["HQry"]=$query;
    $tr=performQuery($query);
	$ARCd=$tr[0]['ARCode'];
		include 'Api_module/dcr/dcrsavecommon.php';
		
	     dcrsavecommon($idata,$ARCd);
		
		
	  }
	  else{
		$ARCd="jkl";
		$result["success"]=false;
    outputJSON($result);
		}
	}

     	



}
?>
