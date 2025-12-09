<?php
function misseddcrsave() {
	include 'Api_module/dcr/rcpasave.php';
	include 'Api_module/dcr/rcpasavechm.php';
	include 'Api_module/dcr/slidessave.php';
	include 'Api_module/dcr/adcuss.php';
	include 'Api_module/dcr/unlisteddcr.php';
	include 'Api_module/dcr/cshdcr.php';
	include 'Api_module/dcr/doctordcr.php';
	include 'Api_module/dcr/jointworkarraytostring.php';
	include 'Api_module/dcr/inputarraytostring.php';
	include 'Api_module/dcr/productarraytostring.php';
	global $data;
	$DivCodes = (string) $data['EData'][0]['division_code'];
    $DivCode = explode(",", $DivCodes.",");
		
    $VstTime = $data['EDt'];
    $DCRDt = date('Y-m-d 00:00:00.000', strtotime($VstTime));
    $CallsData=$data['EData'];
	$idata=$CallsData[0];
				
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
			    $result["Msg"]="Already Leave Posted for this Date";
			    $result["success"]=false;
			   outputJSON($result);
		    }
			$query="exec ios_svDCRMain_Edet '" .$idata['sfcode']. "','" . $DCRDt . "','" . $idata['WT_code'] . "','" . $idata['WTName'] . "','" . $idata['FWFlg'] . "','" . $idata['town_code'] . "','".$idata['town_name']."','" . $DivCode[0] . "','" . $idata['Remarks'] . "','" . $idata['sf_type'] . "','" . $idata['state_code'] . "','','".$mod."'";
			$result["HQry"]=$query;
			
			$tr=performQuery($query);
			
			$ARCd=$tr[0]['ARCode'];
		}
		else{
			$ARCd="jkl";
			$result["success"]=false;
			outputJSON($result);
		}
		$loaddoc=0;
		$loadjw=0;
		$loadproducts=0;
		$loadinputs=0;
		$loadunlisted=0;
		$loadcsh=0;
		$loadadcuss=0;
		$loadslide=0;
		for ($dloop = 0; $dloop < count($CallsData); $dloop++) 
		{ 
	
		    $data=$CallsData[$dloop];
			$sql="insert into tracking_dcr select '" .$data['sfcode']. "','" . $DivCode[0] . "','".$data."',getdate(),'$DCRDt','Edet'";
			performQuery($sql);
	
			if($work!=0)
			{
				$Prods="";
				$ProdsNm="";
				$Inps="";
				$InpsNm="";
				$JWWrk="";
				$JWWrkNm="";
				$GC="";
				$GN="";
				$GQ=0;
				if(isset($data["Products"])&&count($data['Products'])>0){
					//if($loadproducts==0)
					$productsStr=productarraytostring($data);
					$Prods=$productsStr['Prods'];
					$ProdsNm=$productsStr['ProdsNm'];
					$StockNm=$productsStr['StockNm'];
					$loadproducts=1;
				}

				if(isset($data["Inputs"])&&count($data['Inputs'])>0){
				//	if($loadinputs==0)					
					$inputsStr=inputarraytostring($data);
					$Inps=$inputsStr['Inps'];
					$InpsNm=$inputsStr['InpsNm'];
					$GC=$inputsStr['GC'];
					$GN=$inputsStr['GN'];
					$GQ=$inputsStr['GQ'];
					$loadinputs=1;
				}
				if(isset($data["JointWork"])&&count($data['JointWork'])>0){
					//if($loadjw==0)
					$jointworkStr=jointworkarraytostring($data);
					$JWWrk=$jointworkStr['JWWrk'];
					$JWWrkNm=$jointworkStr['JWWrkNm'];
					$loadjw=1;
				}
				
						
				if($data['CusType']=="1"){ 
				//if($loaddoc==0)
				  // include 'Api_module/dcr/doctordcr.php';				  
				   $details=doctordcr($ARCd,$Prods,$ProdsNm,$Inps,$InpsNm,$JWWrk,$JWWrkNm,$GC,$GN,$GQ,$StockNm,$data);
				   $loaddoc=1;
				} 
				if($data['CusType']=="2" || $data['CusType']=="3" ){
					//if($loadcsh==0)
					//include 'Api_module/dcr/cshdcr.php';
				
					$details=cshdcr($ARCd,$Prods,$ProdsNm,$Inps,$InpsNm,$JWWrk,$JWWrkNm,$GC,$GN,$GQ,$StockNm,$data);
					$loadcsh=1;
				}
				if($data['CusType']=="4"){
				//	if($loadunlisted==0)
					//include 'Api_module/dcr/unlisteddcr.php';
				
					$details=unlisteddcr($ARCd,$Prods,$ProdsNm,$Inps,$InpsNm,$JWWrk,$JWWrkNm,$GC,$GN,$GQ,$data);
					$loadunlisted=1;
				}
					$result['ACD']=$details['ACD'];
					$result['ADCD']=$details['ADCD'];
				if(isset($data["AdCuss"])&&count($data["AdCuss"])>0){
					//	 if($loadadcuss==0)
						 //include 'Api_module/dcr/adcuss.php';
					 
						 adcuss($result['ACD'],$result['ADCD'],$data);
						 $loadadcuss=1;
					}
				if(isset($data["Products"])&&count($data['Products'])>0){
					//	if($loadslide==0)
						//include 'Api_module/dcr/slidessave.php';

						slidessave($result['ACD'],$result['ADCD'],$data);
						$loadslide=1;
					}
					//echo count($data['RCPAEntry']);
					
				if(isset($data["RCPAEntry"])&&count($data['RCPAEntry'])>0){
						//include 'Api_module/dcr/rcpasave.php';
						//echo $DCRDt ;
						if($data['CusType']=="2"){
							SvchmRCPAEntry($ARCd,$result['ADCD'],$data,$DCRDt);
						}else{
							SvRCPAEntry($ARCd,$result['ADCD'],$data,$DCRDt);
						}
						
					}
			} 
			else{
				$ARCd="jkl";
				$result["success"]=false;
				outputJSON($result);
			}
		}

	$success = true;
	 if(isset($_FILES["SignImg"])&&count($_FILES["SignImg"])>0)
        if(move_uploaded_file($_FILES["SignImg"]["tmp_name"], "signs/" . $_FILES["SignImg"]["name"]))
	     {
          $success = true;
         }
        else 
        {
          $success = false;
		  $result["msg"]= "Error while uploading";
        }

	$result["success"]=$success;
	$result[ "msg" ] = "";
    outputJSON($result);  	



}
?>
