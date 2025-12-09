<?php
function cshdcr($ARCd,$Prods,$ProdsNm,$Inps,$InpsNm,$JWWrk,$JWWrkNm,$GC,$GN,$GQ,$StockNm,$data) {
	//global $data; changes made by bala for pob and call feedback
    $div_codes = (string) $data['division_code'];	
	$DivCode = explode(",", $div_codes.",");
	$DCSUPOB = (string) $data['DCSUPOB'];
	$Drcallfeedbackcode = (string) $data['Drcallfeedbackcode'];
	if($DCSUPOB=="")
	{
		$DCSUPOB=0;
	}
	if($Drcallfeedbackcode=="")
	{
		$Drcallfeedbackcode=0;
	}
    $loc = explode(":", str_replace("'", "", str_replace(" ", ":", $data['Entry_location'])) . ":");
   	if($loc[0] =="(null)"){$loc[0] ="";$loc[1] ="";}
	 $query="exec ios_svDCRCSHDet_App_Edet '".$ARCd."',0,'".$data['sfcode']."','".$data['CusType']."','".$data['CustCode']."','".$data['CustName']."','".$data['vstTime']."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','".$Inps."','".$InpsNm."','','".str_replace("'","''",$data['Remarks'])."','".$DivCode[0]."','".$Drcallfeedbackcode."','".$data['ModTime']."','".$loc[0]."','".$loc[1]."','".$data['Rsf']."','NA','".$data['Mod']."','" . $data['town_code'] . "','".$data['town_name']."','".$StockNm."'";
	 $tr=performQuery($query);
	            if ( $tr[0]['ARDCd'] == "Exists" ) {
                    $resp[ "msg" ] = "Call Already Exists";
                    $resp[ "success" ] = false;
                    outputJSON( $resp );
                    die;
                }
      	
	$cshdet=array();
	$cshdet["CQry"]=$query;
	$cshdet['ACD']=$ARCd;
	$cshdet['ADCD']=$ARDCd;
	return $cshdet;
}
?>


