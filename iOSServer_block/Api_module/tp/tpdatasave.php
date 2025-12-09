<?php
function tpdatasave($data){ 
		
	$DivCode = (string) $data['DivCode'];
   $DivCode = explode(",", $DivCode.",");
  // echo  $DivCode[0];
	$TPData=$data['TPDatas'];

		//$TPData=$TPDatas[$i];
	/*{"tableName":"savetp","TPDatas":{"WTNm":"Meeting,","WTCd":"3255","WTFWFlg":"N","HQNm":"anil test,","HQCd":"MR2093,","DayRmk":"gud","WTNm1":"Camp Work,","WTCd1":"3264","WTFWFlg1":"N","HQNm1":"anil test,","HQCd1":"MR2093,","DayRmk1":"bad","WTNm2":"Field Work,","WTCd2":"3260","WTFWFlg2":"F","HQNm2":"anil test,","HQCd2":"MR2093,","TerrNm2":"dharmpuri,","TerrCd2":"21387,","DRNm":"mitali chouhan,","DRCd":"220359,","JWNm":"Test Manager,","JWCd":"MGR0252,","DayRmk2":"","HQCodes":"","HQNames":""},"dayno":"2","DivCode":"40","SFName":"anil test","SF":"MR2093","TPDt":"2023-08-2 00:00:00","TPMonth":"08","TPYear":"2023"}*/
			//$TPDet=$TPData["DayPlan"];
		    $queryStr="'".$data['SF']."','".$data['SFName']."','".$data['TPMonth']."',
			'".$data['TPYear']."','".$data["TPDt"]."','0','".$TPData["WTCd"]."','".$TPData["WTCd1"]."','".$TPData["WTCd2"]."','".$TPData["WTNm"]."','".$TPData["WTNm1"]."','".$TPData["WTNm2"]."','".$TPData["HQCd"]."','".$TPData["HQCd1"]."','".$TPData["HQCd2"]."','".$TPData["HQNm"]."','".$TPData["HQNm1"]."','".$TPData["HQNm2"]."','".$TPData["TerrCd"]."','".$TPData["TerrCd1"]."','".$TPData["TerrCd2"]."','".$TPData["TerrNm"]."','".$TPData["TerrNm1"]."','".$TPData["TerrNm2"]."','".$TPData["JWCd"]."','".$TPData["JWCd1"]."','".$TPData["JWCd2"]."','".$TPData["JWNm"]."','".$TPData["JWNm1"]."','".$TPData["JWNm2"]."','".$TPData["DRCd"]."','".$TPData["DRCd1"]."','".$TPData["DRCd2"]."','".$TPData["DRNm"]."','".$TPData["DRNm1"]."','".$TPData["DRNm2"]."','".$TPData["CHCd"]."','".$TPData["CHCd1"]."','".$TPData["CHCd2"]."','".$TPData["CHNm"]."','".$TPData["CHNm1"]."','".$TPData["CHNm2"]."','".$TPData["DayRmk"]."','".$TPData["DayRmk1"]."','".$TPData["DayRmk2"]."','".$DivCode[0]."'";
			$query="exec iOS_svTourPlanNew_Edet ".$queryStr.",'','','','','','','','','','','','',0,'ios','".$TPData["hosCd"]."','".$TPData["hosNm"]."','".$TPData["hosCd1"]."','".$TPData["hosNm1"]."','".$TPData["hosCd2"]."','".$TPData["hosNm2"]."','".$TPData["WTFWFlg"]."','".$TPData["WTFWFlg1"]."','".$TPData["WTFWFlg2"]."'";
			 			
			//echo $query;
			performQuery($query);
			
			$query="exec ios_svTourPlan_detail_Edet ".$queryStr.",'".$TPData["STCd"]."','".$TPData["STNm"]."','".$TPData["STCd1"]."','".$TPData["STNm1"]."','".$TPData["STCd2"]."','".$TPData["STNm2"]."',0,'ios','".$TPData["hosCd"]."','".$TPData["hosNm"]."','".$TPData["hosCd1"]."','".$TPData["hosNm1"]."','".$TPData["hosCd2"]."','".$TPData["hosNm2"]."','".$data['ClusterSFs']."','".$data['ClusterSFNms']."','".$TPData["WTFWFlg"]."','".$TPData["WTFWFlg1"]."','".$TPData["WTFWFlg2"]."','".$data['sf_type']."'
			,'".$TPData["HQCd"]."','".$TPData["HQNm"]."','".$TPData["HQCds1"]."','".$TPData["HQNm1"]."'
			,'".$TPData["HQCd2"]."','".$TPData["HQNm2"]."'";
			performQuery($query);
				//echo $query;
			$result["Qry"]=$query;
		
    $result["success"]=true;
   outputJSON($result);
}

?>