<?php
function tpget() {
	global $data;
	$DivCodes = (string) $data['DivCode'];
    $DivCode = explode(",", $DivCodes.",");

	//Save Tour Plan
	switch ( strtolower( $data[ 'tableName' ] ) ) {
	 	case "savetp":
		    include 'Api_module/tp/tpdatasave.php';
			
			 tpdatasave($data);
            break; 
        case "savetpapproval":
		  $query="exec iOS_svTPApprove_Edet '".$data['sfcode']."','".$data['Month']."','".$data['Year']."'";
		    performQuery($query);
            $result["success"]=true;
	        outputJSON($result);
            break; 
        case "savetpreject":
         	$query="exec iOS_svTPReject_Edet '".$data['sfcode']."','".$data['TPMonth']."','".$data['TPYear']."','".$data['reason']."'";
		    performQuery($query);
            $result["success"]=true;
	        outputJSON($result);
            break; 
		 case "savetpsubmit":
         	$query="exec iOS_svTPSubmit_Edet '".$data['sfcode']."','".$data['TPMonth']."','".$data['TPYear']."'";
		    performQuery($query);
            $result["success"]=true;
	        outputJSON($result);
            break;	
        case "gettpapproval":
         	$query="exec iOS_getTPApproval_Edet '".$data['sfcode']."'";
		    outputJSON( performQuery($query));
            break; 	
        case "gettpdetail":
         	$query="exec iOS_getTourPlanDetail_Edet '".$data['sfcode']."','".$data['Month']."','".$data['Year']."'";
            $res=performQuery($query);

	/*
	$result=array();
	$resTP=array();
	if (count($res)>0) 
	{
		for ($il=0;$il<count($res);$il++)
		{

			$sWTCd=explode("/",$res[$il]["WTCode"]."/".$res[$il]["WTCode2"]."/".$res[$il]["WTCode3"]);
			$sWTNm=explode("/",$res[$il]["WTName"]."/".$res[$il]["WTName2"]."/".$res[$il]["WTName3"]);
			$sPlCd=explode("/",$res[$il]["ClusterCode"]."/".$res[$il]["ClusterCode2"]."/".$res[$il]["ClusterCode3"]);
			$sPlNm=explode("/",$res[$il]["ClusterName"]."/".$res[$il]["ClusterName2"]."/".$res[$il]["ClusterName3"]);
			$sHSCd=explode("/",$res[$il]["HospCode"]."/".$res[$il]["HospCode2"]."/".$res[$il]["HospCode3"]);
			$sHSNm=explode("/",$res[$il]["HospName"]."/".$res[$il]["HospName2"]."/".$res[$il]["HospName3"]);
			$sHQCd=explode("/",$res[$il]["HQCodes"]."/".$res[$il]["HQCodes2"]."/".$res[$il]["HQCodes3"]);
			$sHQNm=explode("/",$res[$il]["HQNames"]."/".$res[$il]["HQNames2"]."/".$res[$il]["HQNames3"]);
			$sJWCd=explode("/",$res[$il]["JWCodes"]."/".$res[$il]["JWCodes2"]."/".$res[$il]["JWCodes3"]);
			$sJWNm=explode("/",$res[$il]["JWNames"]."/".$res[$il]["JWNames2"]."/".$res[$il]["JWNames3"]);
			$sDRCd=explode("/",$res[$il]["Dr_Code"]."/".$res[$il]["Dr_two_code"]."/".$res[$il]["Dr_three_code"]);
			$sDRNm=explode("/",$res[$il]["Dr_Name"]."/".$res[$il]["Dr_two_name"]."/".$res[$il]["Dr_three_name"]);
			$sCHCd=explode("/",$res[$il]["Chem_Code"]."/".$res[$il]["Chem_two_code"]."/".$res[$il]["Chem_three_code"]);
			$sCHNm=explode("/",$res[$il]["Chem_Name"]."/".$res[$il]["Chem_two_name"]."/".$res[$il]["Chem_three_name"]);
			$sRmks=explode("/",$res[$il]["DayRemarks"]."/".$res[$il]["DayRemarks2"]."/".$res[$il]["DayRemarks3"]);
			$FWFlg=explode("/",$res[$il]["FWFlg"]."/".$res[$il]["FWFlg2"]."/".$res[$il]["FWFlg3"]);
			$dypl=array();
			for($ij=0;$ij<count($sWTCd);$ij++){
				if($sWTCd[$ij]!="" && $sWTCd[$ij]!="0"){
				array_push($dypl,array(
                'ClusterCode' => $sPlCd[$ij],
                'ClusterName' =>$sPlNm[$ij],
                'HospCode' => $sHSCd[$ij],
                'HospName' =>$sHSNm[$ij],
                'ClusterSFNms' => $sJWNm[$ij],
                'ClusterSFs' => $sJWCd[$ij],
                'FWFlg' => $FWFlg[$ij],
                'DayRemarks' => $sRmks[$ij],
                'HQCodes' => $sHQCd[$ij],
                'HQNames' => $sHQNm[$ij],
				'JWCodes' => $sJWCd[$ij],
				'JWNames' => $sJWNm[$ij],
				'Dr_Code' => $sDRCd[$ij],
				'Dr_Name' => $sDRNm[$ij],
				'Chem_Code' => $sCHCd[$ij],
				'Chem_Name' => $sCHNm[$ij], 
                'WTCode' => $sWTCd[$ij],
                'WTName' => $sWTNm[$ij]));
				}
			}
			array_push($resTP,array('DayPlan' => $dypl ,'EFlag' =>$res[$il]["EFlag"],'TPDt' =>$res[$il]["TPDt"],'access' =>$res[$il]["access"],'dayno' =>$res[$il]["dayno"]));
      	}
      
		array_push($result,array('SFCode' => $sfCode,'SFName' =>$res[0]["SFName"],'DivCode' =>$res[0]["Div"],'status' =>$res[0]["Change_Status"],'TPDatas' =>$resTP,'TPFlag' => '0','TPMonth' => $res[0]["Mnth"],'TPYear' => $res[0]["Yr"],'reject'=>$res[0]["Rejection_Reason"]));
    }*/
            outputJSON($res);
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