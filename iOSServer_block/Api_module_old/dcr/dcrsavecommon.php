<?php
function dcrsavecommon($data,$ARCd) {
//	global $data,$conn;
	$div_codes = (string) $data['division_code'];	
	$DivCode = explode(",", $div_codes.",");
	 //echo $DivCode[0];
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
	    include 'Api_module/dcr/productarraytostring.php';
        $productsStr=productarraytostring($data);
		$Prods=$productsStr['Prods'];
		$ProdsNm=$productsStr['ProdsNm'];
		$StockNm=$productsStr['StockNm'];
	}

	if(isset($data["Inputs"])&&count($data['Inputs'])>0){
	  include 'Api_module/dcr/inputarraytostring.php';
      $inputsStr=inputarraytostring($data);
	  $Inps=$inputsStr['Inps'];
	  $InpsNm=$inputsStr['InpsNm'];
	  $GC=$inputsStr['GC'];
	  $GN=$inputsStr['GN'];
	  $GQ=$inputsStr['GQ'];
	}
	if(isset($data["JointWork"])&&count($data['JointWork'])>0){
		include 'Api_module/dcr/jointworkarraytostring.php';
		$jointworkStr=jointworkarraytostring($data);
		$JWWrk=$jointworkStr['JWWrk'];
	    $JWWrkNm=$jointworkStr['JWWrkNm'];
	}
	
			
    if($data['CusType']=="1"){ 
       include 'Api_module/dcr/doctordcr.php';	
       $details=doctordcr($ARCd,$Prods,$ProdsNm,$Inps,$InpsNm,$JWWrk,$JWWrkNm,$GC,$GN,$GQ,$StockNm,$data);
    } 
    if($data['CusType']=="2" || $data['CusType']=="3" ){
		include 'Api_module/dcr/cshdcr.php';
    	$details=cshdcr($ARCd,$Prods,$ProdsNm,$Inps,$InpsNm,$JWWrk,$JWWrkNm,$GC,$GN,$GQ,$StockNm,$data);
    }
    if($data['CusType']=="4"){
		include 'Api_module/dcr/unlisteddcr.php';
			
    	$details=unlisteddcr($ARCd,$Prods,$ProdsNm,$Inps,$InpsNm,$JWWrk,$JWWrkNm,$GC,$GN,$GQ,$data);
    }
	$result['ACD']=$details['ACD'];
	$result['ADCD']=$details['ADCD'];
     if(isset($data["AdCuss"])&&count($data["AdCuss"])>0){
		 include 'Api_module/dcr/adcuss.php';
	     adcuss($result['ACD'],$result['ADCD'],$data);
	 }
	if(isset($data["Products"])&&count($data['Products'])>0){
	  include 'Api_module/dcr/slidessave.php';

	     slidessave($result['ACD'],$result['ADCD'],$data);
	  }
	if(isset($data["RCPAEntry"])&&count($data['RCPAEntry'])>0){
		if($data['CusType']=="2"){
			include 'Api_module/dcr/rcpasavechm.php';
			SvchmRCPAEntry($ARCd,$result['ADCD'],$data,$data['vstTime']);
		}else{
			include 'Api_module/dcr/rcpasave.php';
			SvRCPAEntry($ARCd,$result['ADCD'],$data,$data['vstTime']);
		} 
	  
	}
	$success = true;
	 if(isset($_FILES["SignImg"])&&count($_FILES["SignImg"])>0)
        if(move_uploaded_file($_FILES["SignImg"]["tmp_name"], "../signs/" . $_FILES["SignImg"]["name"]))
	     {
          $success = true;	

		  $query="insert into tbDgDetailingFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,SignImg,FeedbkAudio) select '".$ARCd."','".$result['ADCD']."','".$data['sfcode']."','".$data['vstTime']."','".$data['CustCode']."','signs/".$data['SignImageName']."',''";
		  //$result["ImgQry"]=$query;
		  performQuery($query);
					  
         }
        else 
        {
          $success = false;
		  $result["msg"]= "Error while uploading";
        }
		if($data['filepath']!=""){
				
				$query="insert into dcrevent_captures(Trans_SlNo,Trans_Detail_Slno,imgurl,Division_Code,sf_code) select '".$ARCd."','".$result['ADCD']."','".$data['EventImageName']."','".$DivCode[0]."','".$data['sfcode']."'";
				//$result["EventQry"]=$query;
				performQuery($query);
			}

    /*$result["fcnt"]=count($_FILES["SignImg"]);
    $result["fnm"]=$_FILES["SignImg"]["tmp_name"];
    $result["fn"]=$_FILES["SignImg"]["name"];*/
	$result["success"]=$success;
	 $result[ "msg" ] = "";
    outputJSON($result);
}
?>


