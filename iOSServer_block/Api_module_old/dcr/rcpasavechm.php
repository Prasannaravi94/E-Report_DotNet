<?php
function SvchmRCPAEntry($ARNo,$ARMSLNo,$data,$DCRDt){
    //global $data;

    $DivCodes = (string) $data['DivCode'];
    $DivCode = explode(",", $DivCodes.",");
	  $RCPADatas=$data['RCPAEntry'];
	
	for ($Ri = 0; $Ri < count($RCPADatas); $Ri++) 
    {
		$RCPAData=$RCPADatas[$Ri];
		$Chms=$RCPAData["Chemists"];
        $ChmIds="";
        $ChmNms="";
		
		
        for ($Rj = 0; $Rj < count($Chms); $Rj++) 
        {   
          $ChmIds=$ChmIds.$Chms[$Rj]["Code"].",";
          $ChmNms=$ChmNms.$Chms[$Rj]["Name"].",";
         
          // if($ARMSLNo!="")
          // {
              // $query="select Trans_Detail_Slno,convert(varchar,time,20) tmv,Worked_with_Code,lati,long,DataSF,Division_code from vwActivity_MSL_Details where Trans_Detail_Slno='".$ARMSLNo."'";
              // $arr = performQuery($query);
              // if(count($arr)>0){
                 // $query="exec ios_svDCRCSHDet_App_Edet '".$ARNo."',0,'".$data['sfcode']."','2','".$Chms[$Rj]["Code"]."','".$Chms[$Rj]["Name"]."','".$arr[0]["tmv"]."',0,'".$arr[0]["Worked_with_Code"]."','','','','','','','".$arr[0]["Division_code"]."',0,'".$arr[0]["tmv"]."','".$arr[0]["lati"]."','".$arr[0]["long"]."','".$arr[0]["DataSF"]."','NA','iOS','".$data['town_code']."','".$data['town_name']."'";
    	          // $result["CQry"]=$query;
      	          // performQuery($query);
              // }
          // }
        }
        $sXML="<ROOT>";
        $Comps=$RCPAData["Competitors"];
        for ($Rj = 0; $Rj < count($Comps); $Rj++) 
        {   
          $Comp=$Comps[$Rj];
          $sXML=$sXML."<Comp CCode=\"".$Comp["CompCode"]."\" CName=\"".$Comp["CompName"]."\" CPCode=\"".$Comp["CompPCode"]."\" CPName=\"".$Comp["CompPName"]."\" CPQty=\"".$Comp["CPQty"]."\" CPRate=\"".$Comp["CPRate"]."\" CPValue=\"".$Comp["CPValue"]."\" CPRemarks=\"".$Comp["CPRemarks"]."\" />";
        }
        $sXML=$sXML."</ROOT>";
			  $query="exec iOS_svRCPAEntry_Edet '".$data['sfcode']."','".$data['SFName']."','".$DCRDt."','".$data['CustCode']."','".$data['CustName']."','".$ChmIds."','".$ChmNms."','".$RCPAData["OPCode"]."','".$RCPAData["OPName"]."','".$RCPAData["OPQty"]."','".$RCPAData["OPRate"]."','".$RCPAData["OPValue"]."','0.0','".$ARNo."','".$ARMSLNo."','0','".$sXML."'";
			  performQuery($query);
			 
	}
	    $result["msg"]=$query;
		$result["success"]=true;
		return $result;
}

?>


