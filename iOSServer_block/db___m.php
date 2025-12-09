<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
session_start();
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";
include "Mail.php";

$data = json_decode($_POST['data'], true);
//  echo($_POST['data']);

$NeedRollBack=false;

function writelog($msg){
    $myfile = fopen("Log/log_".date('Y_m_d').".txt", "a+");
    $sqlsp=$_POST['data']." message:".$msg." sf_code=".$_GET['sfCode']."\n";
    fwrite($myfile, $sqlsp);
    fclose($myfile);
}

function actionLogin() {    
    global $data;
    
    $username = (string) $data['name'];
    $password = (string) $data['password'];

    $Appver = (string) $data['Appver'];
    $Mod = (string) $data['Mod'];
    $query = "exec LoginAPP '$username','$password'";
    $arr = performQuery($query);
    if (count($arr) == 1) { 

    $query = "insert into version_ctrl select '".$arr[0]["SF_Code"]."',getDate(),getDate(),'$Appver','$Mod'";
    performQuery($query);
	$respon = $arr[0];   
    	$respon['success'] = true;
    	$respon['OurPRateEdit'] = false;
    	$respon['CPRateEdit'] = false;
        return outputJSON($respon);
    } else {
	$respon['success'] = false;
        $respon['msg'] = "Check User and Password";
        return outputJSON($respon);
    }
}

function getSettings() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getSetups '".$sfCode."'";
    return performQuery($query);
}

function getSubordinate() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec getBaseLvlSFs_APP '".$sfCode."'";
    return performQuery($query);
}

function svTrackDetail() {
    global $data;

    $sfCode = (string) $data['SF'];
    $device = (string) $data['device'];
    $gps = (string) $data['gps'];
    $time = (string) $data['time'];
    $query = "exec Map_SvTrackDetail '".$sfCode."','".$device."','".$gps."','".$time."'";
    performQuery($query);
    $respon['msg'] = "success";
    return $respon;
}

function getVstDetails() {
    global $data;

    $sfCode = (string) $data['SF'];
    $Typ = (string) $data['typ'];
    $CusCode = (string) $data['CusCode'];
    $query = "exec iOS_getVstDetails '".$sfCode."','".$Typ."','".$CusCode."'";
    return performQuery($query);
}

function getDrDetails() {
    global $data;

    $sfCode = (string) $data['SF'];
    $CusCode = (string) $data['CusCode'];
    $query = "exec iOS_getDrDetails '".$CusCode."'";
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

function getCompetitorDet() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getMas_CompetitorDets '".$sfCode."'";
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
function getBrands() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getBrands '".$sfCode."'";
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
function getTrackSetupp(){
    global $data;
	$SF=(string) $data['SF'];
	$div=(string) $data['div'];
	
	$query="exec getTrackSetup  '".$SF."','".$div."'";

        return performQuery($query);

}
function getInput() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getInputs '".$sfCode."'";
    return performQuery($query);
}
function getDeparts(){
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getDeparts '".$sfCode."'";
    return performQuery($query);
}
function getDocSpec() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getDocSpec '".$sfCode."'";
    return performQuery($query);
}

function getDocCats() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getDocCats '".$sfCode."'";
    return performQuery($query);
}

function getDocTypes() {
	$result=[];
	array_push($result,array('Code' => "1",'Name' => "H"));
	array_push($result,array('Code' => "2",'Name' => "P - T"));
	array_push($result,array('Code' => "3",'Name' => "P - W"));
	array_push($result,array('Code' => "4",'Name' => "P - I"));

/*	array_push($result,array('Code' => "1",'Name' => "Hospital"));
	array_push($result,array('Code' => "2",'Name' => "Clinic"));
	array_push($result,array('Code' => "3",'Name' => "Private clinic"));
	array_push($result,array('Code' => "4",'Name' => "Public Surgery"));
	array_push($result,array('Code' => "5",'Name' => "Intramoenia surgery"));*/
	return $result;
}
function getDocClass() {
    global $data;

    $sfCode=(string) $data['SF'];
	
    $query="exec iOS_getDocClass '".$sfCode."'";
    return performQuery($query);
}

function getDocQual() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getDocQual '".$sfCode."'";
    return performQuery($query);
}

function getRatingInfos() {
	$result=[];
	/*array_push($result,array('Code' => "1",'Name' => "Hospital"));
	array_push($result,array('Code' => "2",'Name' => "Clinic"));
	array_push($result,array('Code' => "3",'Name' => "Private clinic"));
	array_push($result,array('Code' => "4",'Name' => "Public Surgery"));
	array_push($result,array('Code' => "5",'Name' => "Intramoenia surgery"));*/
	return $result;
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
function getLvValidate(){
    global $data;
    
    $sfCode=(string) $data['SF'];
    $Fdt=(string) $data['Fdt'];
    $Tdt=(string) $data['Tdt'];
    $LTy=(string) $data['LTy'];
    $query="exec iOS_getLvlValidate  '".$sfCode."','".$Fdt."','".$Tdt."','".$LTy."'";
    return performQuery($query);
  
}
function getLvApproval(){
    global $data;
    $sfCode=(string) $data['SF'];
    $query="exec iOS_getLvApproval '".$sfCode."'";
    return performQuery($query);
}
function getTPApproval(){
    global $data;
    $sfCode=(string) $data['SF'];
    $query="exec iOS_getTPApproval '".$sfCode."'";
    return performQuery($query);
}
function getTourPlan(){
    global $data;
    $sfCode=(string) $data['SF'];
    $Mnth=(string) $data['Month'];
    $Yr=(string) $data['Year'];
    $query="exec iOS_getTourPlan '".$sfCode."','".$Mnth."','".$Yr."'";
    $res=performQuery($query);
		$result=array();
		$resTP=array();
		if (count($res)>0) {
			for ($il=0;$il<count($res);$il++)
			{
				array_push($resTP,array('DayPlan' => array(
                'ClusterCode' => $res[$il]["ClusterCode"],
                'ClusterName' => $res[$il]["ClusterName"],
                'ClusterSFNms' => $res[$il]["ClusterSFNms"],
                'ClusterSFs' => $res[$il]["ClusterSFs"],
                'DayRemarks' => $res[$il]["DayRemarks"],
                'FWFlg' => $res[$il]["FWFlg"],
                'HQCodes' => $res[$il]["HQCodes"],
                'HQNames' => $res[$il]["HQNames"],
                'WTCode' => $res[$il]["WTCode"],
                'WTName' => $res[$il]["WTName"]),'EFlag' =>$res[$il]["EFlag"],'TPDt' =>$res[$il]["TPDt"],'access' =>$res[$il]["access"],'dayno' =>$res[$il]["dayno"]));
      }
      
      array_push($result,array('SFCode' => $sfCode,'SFName' =>$res[0]["SFName"],'DivCode' =>$res[0]["Div"],'TPDatas' =>$resTP,'TPFlag' => '0','TPMonth' => $res[0]["Mnth"],'TPYear' => $res[0]["Yr"]));
    }
    return $result;
}

function getDrQuery(){
    global $data;
    $sfCode=(string) $data['SF'];
	  $DrCode=(string) $data['CusCode'];
    $query="exec iOS_getDrQueries '".$sfCode."','".$DrCode."'";
    return performQuery($query);
}

function getNotification(){
    global $data;
    $sfCode=(string) $data['SF'];
    $query="exec iOS_getNotification '".$sfCode."'";
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
    $loc=(string) $data['location'];
 
    if($data['WT']=="0" || $data['WT']=="" || $data['WT']==" ")
    {
        $result['success'] = false;
        $result['Msg'] = 'Invalid worktype selection...';
	writelog($result['Msg']);
        outputJSON($result);
        die;
    }
    $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,getdate(),101) as datetime) and FWFlg='L'";
    $ExisArr = performQuery($query);
	if ($ExisArr[0]["Cnt"]>0){
	    $result["Msg"]="Today Already Leave Posted...";
	    $result["success"]=false;
	    return $result;
	}else{
	    $query="exec iOS_svTodayTP '".$sfCode."','".$SFMem."','".$PlnCd."','".$PlnNM."','".$WT."','".$WTNM."','".$Rem."','".$loc."'";
	    performQuery($query);
	    $result["Msg"]="Today Work Plan Submitted Successfully...";
	    $result["success"]=true;
	    return $result;
	}
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

function SvTPApproval(){

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

			      $query="exec iOS_svTPApproval '".$sfCode."','".$sfName."','".$data['TPMonth']."','".$data['TPYear']."','".$TPData["TPDt"]."','".$TPWTCd."','".$TPWTNm."','".$TPPlCd."','".$TPPlNm."','".$TPSFCd."','".$TPSFNm."','".$DivCode[0]."','".$TPDet["DayRemarks"]."'";
			      performQuery($query);
			      $result["Qry"]=$query;
		    } 
	  }
    $result["success"]=true;
    return $result;
}
function SvTPReject(){
    global $data;

    $sfCode=(string) $data['SF'];
		$query="exec iOS_svTPReject '".$sfCode."','".$data['TPMonth']."','".$data['TPYear']."'";
		performQuery($query);
		$result["Qry"]=$query;
    $result["success"]=true;
    return $result;
}
function SvLocTrack(){
    global $data;
    $locData=$data["data"];
    $sXML="<ROOT>";
    for($il=0;$il<count($data);$il++){
        $Loc=$data[$il];
        $sXML=$sXML."<Loc SF=\"".$Loc["SF"]."\" Dtm=\"".$Loc["date"]."\" lat=\"".$Loc["latitude"]."\" lng=\"".$Loc["longitude"]."\" Auc=\"".$Loc["theAccuracy"]."\" />"; 
    }
    $sXML=$sXML."</ROOT>";
		$query="exec iOS_svTrackLocation '".$sXML."'";
		performQuery($query);
    /*
    $result["Qry"]=$query;
    $result["data"]=$data;*/
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
function SvLeaveAppRej(){
    global $data;

	$SF=(string) $data['SF'];
	$LvID=(string) $data['LvID'];
	$LvFlag=(string) $data['LvAPPFlag'];
	$RejRem=(string) $data['RejRem'];
	$query="exec iOS_svLeaveAppRej  '".$LvID."','".$LvFlag."','".$RejRem."','".$SF."'";
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
          
          if($ARMSLNo!="")
          {
              $query="select Trans_Detail_Slno,convert(varchar,time,20) tmv,Worked_with_Code,lati,long,DataSF,Division_code from vwActivity_MSL_Details where Trans_Detail_Slno='".$ARMSLNo."'";
              $arr = performQuery($query);
              if(count($arr[0])>0){
                  $VstTime=$arr[0]["tmv"];
                  $JWWrk=$arr[0]["Worked_with_Code"];
                  $DivCode=$arr[0]["Division_code"];
                  $lat=$arr[0]["lati"];
                  $lng=$arr[0]["long"];
                  $DataSF=$arr[0]["DataSF"];
                  $query="exec svDCRCSHDet_App '".$ARNo."',0,'".$sfCode."','2','".$Chms[$Rj]["Code"]."','".$Chms[$Rj]["Name"]."','".$VstTime."',0,'".$JWWrk."','','','','','','','".$DivCode."',0,'".$VstTime."','".$lat."','".$lng."','".$DataSF."','NA','iOS'";
	    		        //$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
    	            $result["CQry"]=$query;
      	          performQuery($query);
              }
          }
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
function SvDrPolicy(){
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

    $CustCode=$data['CustCode'];
    $CustName=$data['CustName'];

    $query="exec iOS_svDrPolicy '$CustCode','$CustName','".$sfCode."','".$DCRDt."','".$data['Email']."','".$data['PolicyAccept']."','".$data['PlcyCntMngt']."','".$data['PlcyProf']."','".$data['PlcySemInv']."','".$data['Entry_location']."','".$DivCode."'";
    $result["HQry"]=$query;
    performQuery($query);

    move_uploaded_file($_FILES["SignImg"]["tmp_name"], "signs/" . $_FILES["SignImg"]["name"]);
    if($NeedRollBack==true){
	sqlsrv_rollback( $conn );
     	$result["success"]=false;
    }else{
	if($data['PolicyAccept']==1 && $data['PlcyCntMngt'] ==1 && $data['PlcyProf']==1)
	{
	    SendMail("apps@saneforce.com",$data['Email'],"Policy Acceptance Notification","Dear Sir/Madam,<br> Your Accepted the Data Production Policy to Exeltis. ( $CustName )");
	}
	    sqlsrv_commit( $conn );
     	$result["success"]=true;
    }
    return $result;
}
function SvDrProfile(){
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

    $CustCode=$data['CustCode'];
    $DOB=$data['DrDOBY']."-".$data['DrDOBM']."-".$data['DrDOBD'];
    $DOW=$data['DrDOWY']."-".$data['DrDOWM']."-".$data['DrDOWD'];
    $sProdv="";
    $pItms=$data['Products'];
    for($il=0;$il< count($pItms);$il++){
      $sProdv=$sProdv.$pItms[$il]['Code']."/".$pItms[$il]['SetPoten']."/".$pItms[$il]['SetSegm']."#";
    }
    $sVstDet="";
    $sVSes="";
    $sVAvgP="";
    $sVClsP="";
    for($il=0;$il< count($data['VisitDays']);$il++){
      $sVstDet=$sVstDet.$data['VisitDays'][$il]."/";
      $sVSes=$sVSes.$data['VstSess'][$il]."/";
      $sVAvgP=$sVAvgP.$data['vstAvgPDy'][$il]."/";
      $sVClsP=$sVClsP.$data['vstEcoPats'][$il]."/";
    }
    
    $query="exec iOS_svDrProfile '$CustCode','".$data['DrQual']."','".$data['DrSpec']."','".$data['DrCat']."','".$data['DrGender']."','$DOB','$DOW','".$data['DrAdd1']."','".$data['DrAdd2']."','".$data['DrAdd3']."','".$data['DrAdd4']."','".$data['DrAdd5']."','".$data['DrPhone']."','".$data['DrMob']."','".$data['DrEmail']."','".$data['DrType']."','".$data['DrTar']."','".$sProdv."','".$sVstDet."','".$sVSes."','".$sVAvgP."','".$sVClsP."'";
    $result["HQry"]=$query;
    performQuery($query);

    if($NeedRollBack==true){
	    sqlsrv_rollback( $conn );
     	$result["success"]=false;
    }else{
	    sqlsrv_commit( $conn );
     	$result["success"]=true;
    }
    return $result;
}
function SvDrQuery(){
    global $data,$conn,$NeedRollBack;
    if ( sqlsrv_begin_transaction( $conn ) === false ) {
     	die( print_r( sqlsrv_errors(), true ));
    }
    $NeedRollBack=false;
    $sfCode = (string) $data['SF'];
    $QryDt = $data['QryDt'];
    
    $DivCode = (string) $data['DivCode'];
    $DivCode = str_replace(",", "", $DivCode);

    $CustCode=$data['DrCode'];
    $CustName=$data['DrName'];
    $DeptCode=$data['DeptCode'];
    $DeptName=$data['DeptName'];

    $QryMsg=$data['QryMsg'];
    $QryID=$data['QryID'];
    $query="exec iOS_svDrQueries '".$sfCode."','".$DeptCode."','".$DeptName."','".$CustCode."','".$CustName."','".$DivCode."','".$QryMsg."','F','".$QryDt."','".$QryID."'";
    $result["HQry"]=$query;
    performQuery($query);

    if($NeedRollBack==true){
	    sqlsrv_rollback( $conn );
     	$result["success"]=false;
    }else{
	    sqlsrv_commit( $conn );
     	$result["success"]=true;
    }
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
    $ARDCd = ""; /*(strlen($_GET['amc']) == 0) ? "0" : $_GET['amc'];*/

 
    if($data['WT']=="0" || $data['WT']=="" || $data['WT']==" ")
    {
        $result['success'] = false;
        $result['Msg'] = 'Invalid worktype selection...';
	writelog($result['Msg']);
        outputJSON($result);
        die;
    }
    /*if($data['CusType']=="1"){  
	$query="Select isnull(No_of_Visit,0) No_of_Visit  from Mas_ListedDr where ListedDrCode='".$data['CustCode']."'";
    	$ExisArr = performQuery($query);
	$MxCnt=$ExisArr[0]["No_of_Visit"];
	if($MxCnt>0){
  	    $query="select Count(Trans_Detail_Info_Code) Cnt from vwActivity_Msl_Details where Sf_Code='".$sfCode."' and Trans_Detail_Info_Code='".$data['CustCode']."' and  year(Activity_Date)=year('".$DCRDt."')";
    	    $ExisArr = performQuery($query);
	    if ($ExisArr[0]["Cnt"]>=$MxCnt){
		$result["Msg"]="Already visited ".$MxCnt." times...";
		$result["success"]=false;
		return $result;
	    }
	}
    }*/
    $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$sfCode."' and Activity_Date='".$DCRDt."' and FWFlg='L'";
    $ExisArr = performQuery($query);
	if ($ExisArr[0]["Cnt"]>0){
			$result["Msg"]="Today Already Leave Posted...";
			$result["success"]=false;
			return $result;
	}


    $query="exec svDCRMain_App '".$sfCode."','".$DCRDt."','".$data['WT']."','".$data['Pl']."','".$DivCode."','".$data['Rem']."','','iOS'";
    $params = array(array($ARCd, SQLSRV_PARAM_OUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
    $result["HQry"]=$query;
   // performQueryWP($query, $params);
    performQuery($query);
      $result["SQry"]=$ARCd;
   if($ARCd==""){
      $query="select Trans_SlNo from vwActivity_Report where Sf_Code='".$sfCode."' and Activity_Date='".$DCRDt."'";
      $arr = performQuery($query);
      $result["SlQry"]=$query;
      $ARCd=$arr[0]["Trans_SlNo"];
   }
    $query="exec iOS_UpdDCRFWType '".$sfCode."','".$DCRDt."','".$data['WT']."'";
    performQuery($query);

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
        $stPC=$ProdsArr[$i]["Code"];
        $stPN=$ProdsArr[$i]["Name"];
        
        if($ProdsArr[$i]["Group"]=="1"){
            $query="select top 1 Prod_Detail_Sl_No,Product_Detail_Name from  Mas_Product_Detail where Product_Brd_Code='".$stPC."' order by Prod_Detail_Sl_No";
            $arr = performQuery($query);
            if(count($arr)>0){
                $stPC=$arr[0]["Prod_Detail_Sl_No"];
                $stPN=$arr[0]["Product_Detail_Name"];
            }else{
                $stPC="B".$stPC;
            }
        }
	      $Prods=$Prods.$stPC."~".$ProdsArr[$i]["SmpQty"];
	      $ProdsNm=$ProdsNm.$stPN."~".$ProdsArr[$i]["SmpQty"];
        
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
	
    $sLoc=str_replace(" ", ":", $data['Entry_location']);
    $loc = explode(":", str_replace("'", "", $sLoc) . ":");
    $lat = $loc[0]; //latitude
    $lng = $loc[1]; //longitude
	if($lat =="(null)"){$lat ="";$lng ="";}
    if($data['CusType']=="1"){  
      $query="exec svDCRLstDet_App '".$ARCd."',0,'".$sfCode."',1,'".$CustCode."','".$CustName."','".$VstTime."',0,'".$JWWrk."','".$Prods."','".$ProdsNm."','','','".$GC."','".$GN."','".$GQ."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."',0,'".$VstTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS'";
      //$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
      
    	performQuery($query);//performQueryWP($query, $params);
      if($ARDCd==""){
        $query="select Trans_Detail_Slno from vwActivity_MSL_Details where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code='".$CustCode."'";
        $arr = performQuery($query);
        $result["SLaQry"]=$query;
        $ARDCd=$arr[0]["Trans_Detail_Slno"];
     }
      $result["ACD"]=$ARCd;
      $result["ADCD"]=$ARDCd;
      SvRCPAEntry($ARCd,$ARDCd);
    }
    if($data['CusType']=="2" || $data['CusType']=="3" ){
    	$query="exec svDCRCSHDet_App '".$ARCd."',0,'".$sfCode."','".$data['CusType']."','".$CustCode."','".$CustName."','".$VstTime."',0,'".$JWWrk."','".$Prods."','".$ProdsNm."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."',0,'".$VstTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS'";
	    		//$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
    	$result["CQry"]=$query;
      	performQuery($query);//performQueryWP($query, $params);
      if($ARDCd==""){
        $query="select Trans_Detail_Slno from vwActivity_CSH_Detail where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code='".$CustCode."'";
        $arr = performQuery($query);
        $ARDCd=$arr[0]["Trans_Detail_Slno"];
     }
      	$result["ACD"]=$ARCd;
      	$result["ADCD"]=$ARDCd;
    }
    if($data['CusType']=="4"){
    	$query="exec svDCRUnlstDet_App '".$ARCd."',0,'".$sfCode."','".$data['CusType']."','".$CustCode."','".$CustName."','".$VstTime."',0,'".$JWWrk."','".$Prods."','".$ProdsNm."','','','".$GC."','".$GN."','".$GQ."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."',0,'".$VstTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS'";
		//$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
    	$result["NQry"]=$query;
      	performQuery($query);//performQueryWP($query, $params);
      if($ARDCd==""){
        $query="select Trans_Detail_Slno from vwActivity_Unlst_Detail where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code='".$CustCode."'";
        $arr = performQuery($query);
        $ARDCd=$arr[0]["Trans_Detail_Slno"];
     }
      	$result["ACD"]=$ARCd;
      	$result["ADCD"]=$ARDCd;
    }
    for($Adil=-1;$Adil<count($data["AdCuss"]);$Adil++)
    {
        if($Adil>-1){ 
            $Cus=$data["AdCuss"][$Adil];
            $CustCode=$Cus["Code"];
            $CustName=$Cus["Name"];
        
            $query="exec svDCRLstDet_App '".$ARCd."',0,'".$sfCode."',1,'".$CustCode."','".$CustName."','".$VstTime."',0,'".$JWWrk."','".$Prods."','".$ProdsNm."','','','','','','','','','".$data['Remks']."','".$DivCode."',0,'".$VstTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS'";
            //$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
            $result["DQry"]=$query;
    	      performQuery($query);//performQueryWP($query, $params);
            if($ARDCd==""){
                $query="select Trans_Detail_Slno from vwActivity_MSL_Details where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code='".$CustCode."'";
                $arr = performQuery($query);
                $ARDCd=$arr[0]["Trans_Detail_Slno"];
            }
            
        }
        for ($i = 0; $i < count($ProdsArr); $i++)
        {
	              $TmLn=$ProdsArr[$i]["Timesline"];
    	          $query="select isnull(Max(DDSl_No),0)+1 DDSl_No from tbDigitalDetailing_Head";
    	          $arr = performQuery($query);
    	          $DDSl = $arr[0]["DDSl_No"]; 
	              $query="insert into tbDigitalDetailing_Head(DDSl_No,Activity_Report_code,MSL_code,Product_Code,Product_Name,GroupID,Rating,StartTime,EndTime,Feedbk_Status) select ".$DDSl.",'".$ARCd."','".$CustCode."','".$ProdsArr[$i]["Code"]."','".$ProdsArr[$i]["Name"]."','".$ProdsArr[$i]["Group"]."','".$ProdsArr[$i]["Rating"]."','".$TmLn["sTm"]."','".$TmLn["eTm"]."',''";
	              performQuery($query);
	                $Prods="";
	                $ProdsNm="";
                if($ProdsArr[$i]["Group"]=="1"){
	                $Prods=$Prods.$ProdsArr[$i]["Code"]."~$#";
	                $ProdsNm=$ProdsNm.$ProdsArr[$i]["Name"]."~$#";
                }
    	        $PSlds=$ProdsArr[$i]["Slides"];
	       	for ($j = 0; $j < count($PSlds); $j++)
    	        { 
	        	$SlideNm=$PSlds[$j]["Slide"];
                    	$PSldsTM=$PSlds[$j]["Times"];
  	                for ($k = 0; $k < count($PSldsTM); $k++)
    	              	{
                        	$query="insert into tbDgDetailing_SlideDetails(DDSl_No,SlideName,StartTime,EndTime,Rating,Feedbk,usrLike) select ".$DDSl.",'".$SlideNm."','".$PSldsTM[$k]["sTm"]."','".$PSldsTM[$k]["eTm"]."','".$PSlds[$j]["SlideRating"]."','".$PSlds[$j]["SlideRem"]."','".$PSlds[$j]["usrLike"]."'";
	                	performQuery($query);
			}

                    	$Scribs=$PSlds[$j]["Scribbles"];
  			for ($k = 0; $k < count($Scribs); $k++)
    			{
             			$query="insert into tbDgDetScribFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,ScribImg,SlideNm,SlideSLNo) select '".$ARCd."','".$ARDCd."','".$sfCode."','".$DCRDt."','".$CustCode."','Scribbles/" . $_FILES["ScribbleImg"]["name"]."','".$SlideNm."','".$DDSl."'";
	     			performQuery($query);
			}
		}
            
        }
	      if($ARDCd!="0" && count($_FILES["SignImg"])>0){
		        $query="insert into tbDgDetailingFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,SignImg,FeedbkAudio) select '".$ARCd."','".$ARDCd."','".$sfCode."','".$DCRDt."','".$CustCode."','signs/" . $_FILES["SignImg"]["name"]."',''";
		        $result["ImgQry"]=$query;
		        performQuery($query);
        }
    }
    move_uploaded_file($_FILES["SignImg"]["tmp_name"], "signs/" . $_FILES["SignImg"]["name"]);
    if($NeedRollBack==true || $ARCd==""){
	    sqlsrv_rollback( $conn );
     	$result["success"]=false;
    }else{
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
    case "get/setups":
        outputJSON(getSettings());
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
    case "get/compdet":
        outputJSON(getCompetitorDet());
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
    case "get/brands";
        outputJSON(getBrands());
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
    case "get/departs";
        outputJSON(getDeparts());
        break;
    case "get/categorys";
        outputJSON(getDocCats());
        break;
    case "get/speciality";
        outputJSON(getDocSpec());
        break;
    case "get/types";
        outputJSON(getDocTypes());
        break;
    case "get/class";
        outputJSON(getDocClass());
        break;
    case "get/quali";
        outputJSON(getDocQual());
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
    case "get/tpapproval";
	      outputJSON(getTPApproval());
	      break;
    case "get/lvlapproval";
	      outputJSON(getLvApproval());
	      break;
    case "get/lvlvalid";
	      outputJSON(getLvValidate());
	      break;
    case "get/tpdetail";
	      outputJSON(getTourPlan());
	      break;
    case "get/drquery";
        outputJSON(getDrQuery());
        break;
    case "get/notification";
	      outputJSON(getNotification());
	      break;
    case "get/ratinginf";
	      outputJSON(getRatingInfos());
        break;
    case "get/drdets";
	      outputJSON(getDrDetails());
	      break;
    case "get/tracksetup";
	      outputJSON(getTrackSetupp());
	      break;

        
    case "save/tpapproval";
        outputJSON(SvTPApproval());
        break;
    case "save/tpreject";
        outputJSON(SvTPReject());
        break;
    case "save/lvapproval";
	  case "save/lvreject";
        outputJSON(SvLeaveAppRej());
        break;
    case "save/leave";
        outputJSON(SvLeaveApp());
        break;
    case "save/tourplan";
        outputJSON(SvTourPlan());
        break;
    case "save/policy";
        outputJSON(SvDrPolicy());
        break;
    case "save/drprofile";
        outputJSON(SvDrProfile());
        break;
	case "save/trackdetail";
        outputJSON(svTrackDetail());
        break;
    case "save/mytp";
        outputJSON(SvMyTodayTP());
        break;
    case "save/call";
        outputJSON(SvDCREntry());
        break;
    case "save/track";
        outputJSON(SvLocTrack());
        break;
    case "save/drquery";
        outputJSON(SvDrQuery());
        break;
    case "delete/call";
        outputJSON(DeleteCalls());
        break;

    case "upload/scribble";
	      move_uploaded_file($_FILES["ScribbleImg"]["tmp_name"], "Scribbles/" . $_FILES["ScribbleImg"]["name"]);
        break;
}
?>