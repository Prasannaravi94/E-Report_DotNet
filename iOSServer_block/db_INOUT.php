<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
session_start();
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

$data = json_decode($_POST['data'], true);
//  echo($_POST['data']);

$NeedRollBack=false;
function actionLogin() {    
    global $data;
    
    $username = (string) $data['name'];
    $password = (string) $data['password'];

    $query = "exec LoginAPP '$username','$password'";
    $arr = performQuery($query);
    if (count($arr) == 1) { 
		    $respon = $arr[0];   
    	  $respon['success'] = true;
        return outputJSON($respon);
    } else {
		    $respon['success'] = false;
        $respon['msg'] = "Check User and Password";
        return outputJSON($respon);
    }
}


function getSubordinate() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec getBaseLvlSFs_APP '".$sfCode."'";
    return performQuery($query);
}

function getVstDetails() {
    global $data;

    $sfCode = (string) $data['SF'];
    $Typ = (string) $data['typ'];
    $CusCode = (string) $data['CusCode'];
    $query = "exec iOS_getVstDetails '".$sfCode."','".$Typ."','".$CusCode."'";
    return performQuery($query);
}

function getCallAvg() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getDrCallAvgBySFYr '".$sfCode."',".date('Y');
    return performQuery($query);
}
function getCatVst() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getCurrCateVst '".$sfCode."'";
    return performQuery($query);
}
function getTVstDet(){
    global $data;
    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getTVstDetails '".$sfCode."',".date('m').",".date('Y');
   return performQuery($query);
}
function getMnVstDet(){
    global $data;

    $sfCode = (string) $data['SF'];
    $cusCode = (string) $data['CusCode'];
    $Typ = (string) $data['typ'];
    $query = "exec iOS_getMnVstDetails '".$sfCode."','".$cusCode."',".date('Y').",'".$Typ."'";
    /*    $respon['msg'] = $query;
        return outputJSON($respon);*/
   return performQuery($query);
}

function getWorkTypes() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getMas_WorkTypes '".$sfCode."'";
    return performQuery($query);
}

function getDoctors() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getDoctors '".$sfCode."'";
    return performQuery($query);
}

function getChemists() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getChemists '".$sfCode."'";
    return performQuery($query);
}

function getStockists() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getStockist '".$sfCode."'";
    return performQuery($query);
}

function getUnlistedDoctor() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getUnListed '".$sfCode."'";
    return performQuery($query);
}

function getTerritory() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getTerritorys '".$sfCode."'";
    return performQuery($query);
}


function getToDyCalls() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getTodayCalls '".$sfCode."'";
    return performQuery($query);
}
function getBrandProduct() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getBrandProducts '".$sfCode."'";
    return performQuery($query);
}
function getProduct() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getProducts '".$sfCode."'";
    return performQuery($query);
}

function getProductSlide() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getProductSlides '".$sfCode."'";
    return performQuery($query);
}

function getInput() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getInputs '".$sfCode."'";
    return performQuery($query);
}

function getTodayTP() {
    global $data;

    $sfCode=(string) $data['SF'];
    $DCRDt = date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']));
    $query="exec iOS_getTodayTP '".$sfCode."','$DCRDt'";
    return performQuery($query);
}
function getRptMenuList(){
    $query="exec iOS_getReportMenuList";
    return performQuery($query);
}

function getManagers() {
    global $data;

    $Datasf=(string) $data['SF'];
    $sfCode=(string) $data['APPUserSF'];
    $query = "exec iOS_getJointWork_App '".$Datasf."','".$sfCode."'";
    return performQuery($query);
}
function DeleteCalls(){

    global $data;

    $slNo=(string) $data['ADetSLNo'];
    $query = "exec iOS_DeleteCallEntry '".$slNo."'";
    return performQuery($query);
}
function SvMyTodayTP(){
    global $data;

    $sfCode=(string) $data['SF'];
    $SFMem=(string) $data['SFMem'];
    $PlnCd=(string) $data['Pl'];
    $PlnNM=(string) $data['PlNm'];
    $WT=(string) $data['WT'];
    $WTNM=(string) $data['WTNMm'];
    $Rem=(string) $data['Rem'];
	
    $query="exec iOS_svTodayTP '".$sfCode."','".$SFMem."','".$PlnCd."','".$PlnNM."','".$WT."','".$WTNM."','".$Rem."'";
    performQuery($query);
    $result["success"]=true;
    return $result;
}

function SvTourPlan(){
    global $data;

    $sfCode=(string) $data['SF'];
    $sfName=(string) $data['SFName'];
    $DivCodes = (string) $data['DivCode'];
    $DivCode = explode(",", $DivCodes.",");
	$TPDatas=$data['TPDatas'];
	for ($i = 0; $i < count($TPDatas); $i++) 
    {
		$TPData=$TPDatas[$i];
		if($TPData["dayno"]!="")
		{
			$TPDet=$TPData["DayPlan"];

			$TPWTCd=$TPDet["WTCode"];
			$TPWTNm=$TPDet["WTName"];

			$TPPlCd=$TPDet["ClusterCode"];
			$TPPlNm=$TPDet["ClusterName"];

			$TPSFCd=$TPDet["HQCodes"];
			$TPSFNm=$TPDet["HQNames"];

			$query="exec iOS_svTourPlan '".$sfCode."','".$sfName."','".$data['TPMonth']."','".$data['TPYear']."','".$TPData["TPDt"]."','".$TPWTCd."','".$TPWTNm."','".$TPPlCd."','".$TPPlNm."','".$TPSFCd."','".$TPSFNm."','".$DivCode[0]."','".$TPDet["DayRemarks"]."'";
			performQuery($query);
			$result["Qry"]=$query;
		} 
	}
    $result["success"]=true;
    return $result;
}

function SvLeaveApp(){
    global $data;

    $sfCode=(string) $data['SF'];
    $FDate=(string) $data['FDate'];
    $TDate=(string) $data['TDate'];
    $NFD=(string) $data['NOD'];
	
    $LvType=(string) $data['LeaveType'];
    $LvRem=(string) $data['LvRem'];
    $LvOnAdd=(string) $data['LvOnAdd'];
    
    $query="exec iOS_svLeaveApp '".$sfCode."','".$FDate."','".$TDate."','".$NFD."','".$LvType."','".$LvRem."','".$LvOnAdd."'";
    performQuery($query);
	$result["Qry"]=$query;
    $result["success"]=true;
    return $result;
}
function SvRCPAEntry($ARNo,$ARMSLNo){
    global $data;

    $sfCode=(string) $data['SF'];
    $sfName=(string) $data['SFName'];
    $RCPADt=(string) $data['vstTime'];
    $CustCode=(string) $data['CustCode'];
    $CustName=(string) $data['CustName'];
    
    $DivCodes = (string) $data['DivCode'];
    $DivCode = explode(",", $DivCodes.",");
	  $RCPADatas=$data['RCPAEntry'];
    $query="select isnull(Max(EID),0)+1 EID from Trans_RCPA_Head";
    $arr = performQuery($query);
    $EID = $arr[0]["EID"]; 
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
        }
        $sXML="<ROOT>";
        $Comps=$RCPAData["Competitors"];
        for ($Rj = 0; $Rj < count($Comps); $Rj++) 
        {   
          $Comp=$Comps[$Rj];
          $sXML=$sXML."<Comp CCode=\"".$Comp["CompCode"]."\" CName=\"".$Comp["CompName"]."\" CPCode=\"".$Comp["CompPCode"]."\" CPName=\"".$Comp["CompPName"]."\" CPQty=\"".$Comp["CPQty"]."\" CPRate=\"".$Comp["CPRate"]."\" CPValue=\"".$Comp["CPValue"]."\" />";
        }
        $sXML=$sXML."</ROOT>";
			  $query="exec iOS_svRCPAEntry '".$sfCode."','".$sfName."','".$RCPADt."','".$CustCode."','".$CustName."','".$ChmIds."','".$ChmNms."','".$RCPAData["OPCode"]."','".$RCPAData["OPName"]."','".$RCPAData["OPQty"]."','".$RCPAData["OPRate"]."','".$RCPAData["OPValue"]."','".$ARNo."','".$ARMSLNo."','".$EID."','".$sXML."'";
			  performQuery($query);
	  }
    $result["success"]=true;
    return $result;
}
function SvDCREntry(){
    global $data,$conn,$NeedRollBack;
    if ( sqlsrv_begin_transaction( $conn ) === false ) {
     	die( print_r( sqlsrv_errors(), true ));
    }
    $NeedRollBack=false;
    $sfCode = (string) $data['SF'];
    $VstTime = $data['vstTime'];
    $DCRDt = date('Y-m-d 00:00:00.000', strtotime($VstTime));
    $DivCode = (string) $data['DivCode'];
    $DivCode = str_replace(",", "", $DivCode);
    $ARCd = "";
    $ARDCd = "0"; /*(strlen($_GET['amc']) == 0) ? "0" : $_GET['amc'];*/
    
    $query="exec svDCRMain_App '".$sfCode."','".$DCRDt."','".$data['WT']."','".$data['Pl']."','".$DivCode."','".$data['Rem']."',?,'iOS'";
    $params = array(array($ARCd, SQLSRV_PARAM_OUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
    $result["HQry"]=$query;
    performQueryWP($query, $params);
   
    $CustCode=$data['CustCode'];
    $CustName=$data['CustName'];
    $JWWrkArr=$data['JWWrk'];
    $JWWrk="";
    $JWWrkNm="";
	
    for ($i = 0; $i < count($JWWrkArr); $i++) 
    {
	    $JWWrk=$JWWrk.$JWWrkArr[$i]["Code"]."$$";
	    $JWWrkNm=$JWWrkNm.$JWWrkArr[$i]["Name"]."$$";
    }


    $ProdsArr=$data['Products'];
    $Prods="";
    $ProdsNm="";
    for ($i = 0; $i < count($ProdsArr); $i++)
    {
	      $Prods=$Prods.$ProdsArr[$i]["Code"]."~".$ProdsArr[$i]["SmpQty"];
	      $ProdsNm=$ProdsNm.$ProdsArr[$i]["Name"]."~".$ProdsArr[$i]["SmpQty"];
	      if($data['CusType']=="1"){
	          $Prods=$Prods."$".$ProdsArr[$i]["RxQty"];
	          $ProdsNm=$ProdsNm."$".$ProdsArr[$i]["RxQty"];
	      }
	      $Prods=$Prods."#";
	      $ProdsNm=$ProdsNm."#";
    }

    $InpArr=$data['Inputs'];
    $Inps="";
    $InpsNm="";
	  $GC="";
	  $GN="";
	  $GQ=0;
    for ($i = 0; $i < count($InpArr); $i++) 
    {
        if ($i==0){
	          $GC=$InpArr[$i]["Code"];
	          $GN=$InpArr[$i]["Name"];
	          $GQ=$InpArr[$i]["IQty"];
        
        }else{
	          $Inps=$Inps.$InpArr[$i]["Code"]."~".$InpArr[$i]["IQty"]."#";
	          $InpsNm=$InpsNm.$InpArr[$i]["Name"]."~".$InpArr[$i]["IQty"]."#";
        }
    }
	
    $sLoc=$data['Entry_location'];
    $loc = explode(":", str_replace("'", "", $sLoc) . ":");
    $lat = $loc[0]; //latitude
    $lng = $loc[1]; //longitude
	if($lat =="(null)"){$lat ="";$lng ="";}
    if($data['CusType']=="1"){  
      $query="exec svDCRLstDet_App '".$ARCd."',?,'".$sfCode."',1,'".$CustCode."','".$CustName."','".$VstTime."',0,'".$JWWrk."','".$Prods."','".$ProdsNm."','','','".$GC."','".$GN."','".$GQ."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."',0,'".$VstTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS'";
      $params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
      $result["DQry"]=$query;
    	performQueryWP($query, $params);
      $result["ACD"]=$ARCd;
      $result["ADCD"]=$ARDCd;
      SvRCPAEntry($ARCd,$ARDCd);
    }
    if($data['CusType']=="2" || $data['CusType']=="3" ){
    	$query="exec svDCRCSHDet_App '".$ARCd."',?,'".$sfCode."','".$data['CusType']."','".$CustCode."','".$CustName."','".$VstTime."',0,'".$JWWrk."','".$Prods."','".$ProdsNm."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."',0,'".$VstTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS'";
	    		$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
    	$result["CQry"]=$query;
      	performQueryWP($query, $params);
      	$result["ACD"]=$ARCd;
      	$result["ADCD"]=$ARDCd;
    }
    if($data['CusType']=="4"){
    	$query="exec svDCRUnlstDet_App '".$ARCd."',?,'".$sfCode."','".$data['CusType']."','".$CustCode."','".$CustName."','".$VstTime."',0,'".$JWWrk."','".$Prods."','".$ProdsNm."','','','".$GC."','".$GN."','".$GQ."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."',0,'".$VstTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS'";
		$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
    	$result["NQry"]=$query;
      	performQueryWP($query, $params);
      	$result["ACD"]=$ARCd;
      	$result["ADCD"]=$ARDCd;
    }

    for ($i = 0; $i < count($ProdsArr); $i++)
    {
	      $TmLn=$ProdsArr[$i]["Timesline"];
    	  $query="select isnull(Max(DDSl_No),0)+1 DDSl_No from tbDigitalDetailing_Head";
    	  $arr = performQuery($query);
    	  $DDSl = $arr[0]["DDSl_No"]; 
	      $query="insert into tbDigitalDetailing_Head(DDSl_No,Activity_Report_code,MSL_code,Product_Code,Product_Name,GroupID,Rating,StartTime,EndTime,Feedbk_Status) select ".$DDSl.",'".$ARCd."','".$CustCode."','".$ProdsArr[$i]["Code"]."','".$ProdsArr[$i]["Name"]."','".$ProdsArr[$i]["Group"]."','".$ProdsArr[$i]["Rating"]."','".$TmLn["sTm"]."','".$TmLn["eTm"]."',''";
	      performQuery($query);
        
    	  $PSlds=$ProdsArr[$i]["Slides"];
	      for ($j = 0; $j < count($PSlds); $j++)
    	  { 
	          $SlideNm=$PSlds[$j]["Slide"];
            $PSldsTM=$PSlds[$j]["Times"];
  	        for ($k = 0; $k < count($PSldsTM); $k++)
    	      {
                $query="insert into tbDgDetailing_SlideDetails(DDSl_No,SlideName,StartTime,EndTime,Rating,Feedbk) select ".$DDSl.",'".$SlideNm."','".$PSldsTM[$k]["sTm"]."','".$PSldsTM[$k]["eTm"]."','".$PSlds[$j]["SlideRating"]."','".$PSlds[$j]["SlideRem"]."'";
	              performQuery($query);
	          }
	      }

    }
	if($ARDCd!="0" && count($_FILES["SignImg"])>0){
		$query="insert into tbDgDetailingFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,SignImg,FeedbkAudio) select '".$ARCd."','".$ARDCd."','".$sfCode."','".$DCRDt."','".$CustCode."','signs/" . $_FILES["SignImg"]["name"]."',''";
		$result["ImgQry"]=$query;
		performQuery($query);
    }
    move_uploaded_file($_FILES["SignImg"]["tmp_name"], "signs/" . $_FILES["SignImg"]["name"]);
    if($NeedRollBack==true || $ARCd==""){
	    sqlsrv_rollback( $conn );
     	$result["success"]=false;
}
    else{
	    sqlsrv_commit( $conn );
     	$result["success"]=true;
    }
    $result["fcnt"]=count($_FILES["SignImg"]);
    $result["fnm"]=$_FILES["SignImg"]["tmp_name"];
    $result["fn"]=$_FILES["SignImg"]["name"];
    return $result;
}


$axn = $_GET['axn'];
$value = explode(":", $axn);
switch (strtolower($value[0])) {
    case "login":
        actionLogin();
        break;
    case "get/jntwrk":
        outputJSON(getManagers());
        break;
    case "get/hq":
        outputJSON(getSubordinate());
        break;
    case "get/worktype":
        outputJSON(getWorkTypes());
        break;
    case "get/doctors":
        outputJSON(getDoctors());
	      break;
    case "get/chemist":
        outputJSON(getChemists());
	      break;
    case "get/stockist";
        outputJSON(getstockists());
        break;
    case "get/unlisteddr";
        outputJSON(getUnlistedDoctor());
        break;
    case "get/territory";
        outputJSON(getTerritory());
        break;
    case "get/products";
        outputJSON(getProduct());
        break;
    case "get/prodslides";
        outputJSON(getProductSlide());
        break;
    case "get/inputs";
        outputJSON(getInput());
        break;

    case "get/todaytp";
        outputJSON(getTodayTP());
        break;
    case "get/leavestatus":
        $sfCode = $data['SF'];
        $sql = "select Leave_Type_Code,cast(isnull(No_Of_Days,'0') as varchar) Elig,cast(isnull(Leave_Taken_Days,0) as varchar) Taken,cast(isnull(Leave_Balance_Days,0) as varchar) Avail from vwLeaveEntitle where Sf_Code='$sfCode' and Leave_Type_Code in ('CL' ,'PL' ,'SL' ,'LOP' ) order by replace(Leave_Type_Code,'LOP','ZOP')";
        $leave = performQuery($sql);
		$resE=array();$resT=array();$resA=array();
		if (count($leave)>0) {
			for ($il=0;$il<count($leave);$il++)
			{
				array_push($resE,array('id' => $il,'Name' =>$leave[$il]["Leave_Type_Code"],'Value' =>$leave[$il]["Elig"]));
				array_push($resT,array('id' => $il,'Name' =>$leave[$il]["Leave_Type_Code"],'Value' =>$leave[$il]["Taken"]));
				array_push($resA,array('id' => $il,'Name' =>$leave[$il]["Leave_Type_Code"],'Value' =>$leave[$il]["Avail"]));
			}
		}else{
				array_push($resE,array('id' => "1",'Name' =>"CL",'Value' =>"0"));
				array_push($resE,array('id' => "2",'Name' =>"PL",'Value' =>"0"));
				array_push($resE,array('id' => "3",'Name' =>"SL",'Value' =>"0"));
				array_push($resE,array('id' => "4",'Name' =>"LOP",'Value' =>"0"));
				$resT=$resE;
				$resA=$resE;
		}
        $respon = array(array('id' => "1",'Name' => "Eligibility",'values' => $resE),
								array('id' => "2",'Name' => "Taken",'values' => $resT),
								array('id' => "3",'Name' => "Available",'values' => $resA));
        outputJSON($respon);
        break;

    case "get/rptmenulist";
        outputJSON(getRptMenuList());
        break;
    case "get/todycalls";
        outputJSON(getToDyCalls());
        break;
    case "get/vstdr";
	      outputJSON(getVstDetails());
	      break;
    case "get/cuslvst";
	      outputJSON(getVstDetails());
	      break;
    case "get/callavgyrcht";
	      outputJSON(getCallAvg());
	      break;
    case "get/catvst";
	      outputJSON(getCatVst());
	      break;
    case "get/cusmvst";
	      outputJSON(getMnVstDet());
	      break;
    case "get/callvst";
	      outputJSON(getTVstDet());
	      break;
        

    case "save/leave";
        outputJSON(SvLeaveApp());
        break;
    case "save/tourplan";
        outputJSON(SvTourPlan());
        break;
    case "save/mytp";
        outputJSON(SvMyTodayTP());
        break;
    case "save/call";
        outputJSON(SvDCREntry());
        break;
    case "delete/call";
        outputJSON(DeleteCalls());
        break;
}
?>