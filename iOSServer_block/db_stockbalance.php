<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
// ini_set( 'error_reporting', E_ALL );
// ini_set( 'display_errors', true );
//session_start();
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils_stk.php";
include "Mail.php";

$data = json_decode($_POST['data'], true);
//  echo($_POST['data']);

$NeedRollBack=false;
function actionLogin() {    
    global $data;
    
    $username = (string) $data['name'];
    $password = (string) $data['password'];
	$version = (string) $data['Appver'];
	$mode = (string) $data['Mod'];
	
    $query = "exec LoginAPP '$username','$password'";
    $arr = performQuery($query);
    if (count($arr) == 1) { 
		    $respon = $arr[0];   
    	  $respon['success'] = true;
    	  $respon['OurPRateEdit'] = false;
    	  $respon['CPRateEdit'] = false;
		  $dat=date('Y-m-d');
		  $ty = date('Y-m-d H:i:s');
		  $sql1 ="insert into version_ctrl select '".$arr[0]['SF_Code']."','".$dat."','".$ty."','".$version."','".$mode."'";
		performQuery($sql1);
        return outputJSON($respon);
    } else {
		    $respon['success'] = false;
        $respon['msg'] = "Check User and Password";
        return outputJSON($respon);
    }
}

function svDCRActivity(){
	global $data;
	
$val=$data['val'];

for ($i = 0; $i < count($val); $i++) 
{
	$det_no="0";
	$main_no="0";
	$type_val="0";
	$cust_code="0";
	$value=$val[$i];
	$sf=$value["SF"];
	$div=$value["div"];
	$act_date=$value["act_date"];
	$update_time=$value["update_time"];
	$slno=$value["slno"];
	$ctrl_id=$value["ctrl_id"];
	$create_id=$value["creat_id"];
	$va=$value["values"];
	$codes=$value["codes"];
	$type_val=$value["type"];
	$dt=$value["dcr_date"];
	if($type_val!="0"){
		if($type_val=="1"	||	$type_val=="2"	||	$type_val=="3"	||	$type_val=="4"){
		$query="exec svDCRMain_App '".$sf."','".$dt."','".$value['WT']."','".$value['Pl']."','".$div."','','','iOS'";
		$respon["MQry"]=$query;
		performQuery($query);
		
      
      $query="select Trans_SlNo from vwActivity_Report where Sf_Code='".$sf."' and Activity_Date='".$dt."'";
      $arr = performQuery($query);
      $respon["SlQry"]=$query;
	  $respon["valQry"]=$arr[0]["Trans_SlNo"];
      $det_no=$arr[0]["Trans_SlNo"];
	  $cust_code=$value["cus_code"];
	  }
	  
	  if($type_val=="1"){  
		$query="exec svDCRLstDet_App '".$det_no."',0,'".$sf."',1,'".$cust_code."','".$value['cusname']."','".$dt."',0,'','','','','','','','','','','','','".$div."',0,'".$dt."','".$value['lat']."','".$value['lng']."','".$value['DataSF']."','NA','iOS'";
		performQuery($query);
		$query="select Trans_Detail_Slno from vwActivity_MSL_Details where Trans_SlNo='".$det_no."' and Trans_Detail_Info_Code='".$cust_code."'";
        $arr = performQuery($query);
        $main_no=$arr[0]["Trans_Detail_Slno"];
		}

	if($type_val=="2" || $type_val=="3" ){
    	$query="exec svDCRCSHDet_App '".$det_no."',0,'".$sf."','".$type_val."','".$cust_code."','".$value['cusname']."','".$dt."',0,'','','','','','','','".$div."',0,'".$dt."','".$value['lat']."','".$value['lng']."','".$value['DataSF']."','NA','iOS'";
	    		//$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));

    	$result["CQry"]=$query;
      	performQuery($query);//performQueryWP($query, $params);
        $query="select Trans_Detail_Slno from vwActivity_CSH_Detail where Trans_SlNo='".$det_no."' and Trans_Detail_Info_Code='".$cust_code."'";
        $arr = performQuery($query);
        $main_no=$arr[0]["Trans_Detail_Slno"];
     }

	if($type_val=="4"){
    	$query="exec svDCRUnlstDet_App '".$det_no."',0,'".$sf."','".$type_val."','".$cust_code."','".$value['cusname']."','".$dt."',0,'','','','','','','','','','','','','".$div."',0,'".$dt."','".$value['lat']."','".$value['lng']."','".$value['DataSF']."','NA','iOS'";
		//$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
    	$result["NQry"]=$query;
      	performQuery($query);//performQueryWP($query, $params);
        $query="select Trans_Detail_Slno from vwActivity_Unlst_Detail where Trans_SlNo='".$det_no."' and Trans_Detail_Info_Code='".$cust_code."'";
        $arr = performQuery($query);
        $main_no=$arr[0]["Trans_Detail_Slno"];
     }
   
	}
	
	$query="exec svDcrActivity '$sf','$div','$act_date','$update_time','$slno','$ctrl_id','$create_id','$va','$codes','$det_no','$main_no','$type_val','$cust_code'";
	$arr = performQuery($query);
	$respon["finalQry"]=$arr;
	
}
$respon['success'] = true;
return	$respon;
}

function getSettings() {
    global $data;

    $sfCode=(string) $data['SF'];
    $query="exec iOS_getSetups '".$sfCode."'";
    return performQuery($query);
}
function getDynamicActivity(){	
	global $data;	
	$sfCode = (string) $data['div'];	
	$query = "select * from mas_activity where Division_Code='".$sfCode."'";	
	return performQuery($query);
}
/*function getDynamicViewTest(){	
	global $data;	
	$sfCode = (string) $data['slno'];	
	$query = "select * from mas_dynamic_screen_creation where Activity_SlNo='".$sfCode."'";	
	$res=performQuery($query);
	if (count($res)>0) 
	{
		for ($il=0;$il<count($res);$il++)
		{
			$id=$res[$il]["Control_Id"];
		$div=$res[$il]["Division_Code"];
		if($id=="8"	||	$id=="9"){
			$qu = "select * from ".$res[$il]["Control_Para"]." where Division_Code='".$div."'";	
	
			$res[$il]['inputss']=$qu;
			$res[$il]['input']=performQuery($qu);
		}
		else	if($id=="12"	||	$id=="13"){
			$qu = "select Sl_No from Mas_Customized_Table_Name where Name_Table='".$res[$il]["Control_Para"]."'";
			$cus=performQuery($qu);
			$qu = "select * from Mas_Customized_Table where Name_Table_Slno='".$cus[0]["Sl_No"]."'";
			$cus=performQuery($qu);
			$res[$il]['input']=$cus;
		}
		else{
			$res[$il]['input']=array();
		}
		}
	}
	
	return $res;
}*/
function getDynamicViewTest(){	
	global $data;	
	$sfCode = (string) $data['slno'];	
	$div = (string) $data['div'];
	$sf = (string) $data['SF'];
	$div = str_replace(",", "", $div);
	$query = "select * from mas_dynamic_screen_creation where Activity_SlNo='".$sfCode."' and Division_Code='".$div."' and Active_Flag='0'";	
	$res=performQuery($query);
	if (count($res)>0) 
	{
		for ($il=0;$il<count($res);$il++)
		{
			$id=$res[$il]["Control_Id"];
		
		if($id=="8"	||	$id=="9"){
			if($res[$il]["Control_Para"]=="Mas_ListedDr"){
			$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$div."' and Sf_Code='".$sf."'";	
			}
			else{
				$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$div."'";	
			}
			
			$res[$il]['inputss']=$qu;
			$res[$il]['input']=performQuery($qu);
		}
		else	if($id=="12"	||	$id=="13"){
			$qu = "select Sl_No from Mas_Customized_Table_Name where Name_Table='".$res[$il]["Control_Para"]."'";
				$res[$il]['inputss']=$qu;
			$cus=performQuery($qu);
			//$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from Mas_Customized_Table where Name_Table_Slno='".$cus[0]["Sl_No"]."'";
			$qu = "select Mas_Sl_No,Customized_Name from Mas_Customized_Table where Name_Table_Slno='".$cus[0]["Sl_No"]."'";
			$cus=performQuery($qu);
			$res[$il]['input']=$cus;
		}
		else{
			$res[$il]['input']=array();
		}
		}
	}
	
	return $res;
}
function getCustomSetup() {
    global $data;	
	$sfCode = (string) $data['div'];	
	$sfCode = str_replace(",", "", $sfCode);
	$query = "select * from Custom_Table where division_code='".$sfCode."'";	
	return performQuery($query);
}
function UpdateSign(){
    global $URL_BASE;

$target_dir = "Activity/";
$target_file_name = $target_dir .basename($_FILES["file"]["name"]);
$target_file = basename($_FILES["file"]["name"]);
$response = array();
//echo "file are here".$target_file_name;

/* $data = json_decode($_POST['data'], true);
$json = json_decode($data, true); */
 $upload_url;

if (isset($_FILES["file"])) 
{
	//echo "file are here";
 if (move_uploaded_file($_FILES["file"]["tmp_name"], $target_file_name)) 
 {
  $success = true;
  
  $upload_url = $target_file;
  
 // $UpdateMyAccount = "update Mas_Empolyee set File_Path='". $upload_url."',Emp_Name='".$Username."' ,Emp_Email='".$Email."',Emp_Mobile='".$Mobile."',Emp_DOB='".$DOB."',Emp_ContactAdd_One='".$Address."'    where Emp_ID='" . $Emp_ID . "'";
    
   // $Pass = performQuery($UpdateMyAccount);
  
  $message = "Profile Has Been Updated" ;
 }
 else 
 {
  $success = false;
  $message = "Error while uploading";
 }
}
else 
{
	  $message =  "Profile Has Been Updated";

	/* $UpdateMyAccount = "update Mas_Empolyee set Emp_Name='".$Username."' ,Emp_Email='".$Email."',Emp_Mobile='".$Mobile."',Emp_DOB='".$DOB."',Emp_ContactAdd_One='".$Address."'    where Emp_ID='" . $Emp_ID . "'";
    
    $Pass = performQuery($UpdateMyAccount);
	 */
 $success = true;
 
}
 
$response["success"] = $success;
$response["message"] = $message;
$response["url"] = $upload_url;
  return $response;
}
function getDynamicView(){	
	global $data;	
	$sfCode = (string) $data['slno'];	
	$query = "select * from mas_dynamic_screen_creation where Activity_SlNo='".$sfCode."'";	
	return performQuery($query);
}
function getSubordinate() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec getBaseLvlSFs_APP '".$sfCode."'";
    return performQuery($query);
}

function getSubordinateMgr() {
	 global $data;
    
	$sfCode = (string) $data['SF'];
    $query = "exec getHyrSF_APP '" . $sfCode . "'";
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

function getDrDetails() {
    global $data;

    $sfCode = (string) $data['SF'];
    $CusCode = (string) $data['CusCode'];
    $query = "exec iOS_getDrDetails '".$CusCode."'";
    return performQuery($query);
}

function svTrackDetail() {
    global $data;

    $sfCode = (string) $data['SF'];
    $device = (string) $data['device'];
    $gps = (string) $data['gps'];
    $time = (string) $data['time'];
    $net = (string) $data['net'];
    $battery = (string) $data['battery'];
    $query = "exec Map_SvTrackDetail '".$sfCode."','".$device."','".$gps."','".$time."','".$net."','".$battery."'";
    performQuery($query);
    $respon['msg'] = "success";
    return $respon;
}
function SvLocTrack(){
    global $data;
    $locData=$data["data"];

    $sXML="<ROOT>";
    for($il=0;$il<count($data);$il++){
        $Loc=$data[$il];

        $sXML=$sXML."<Loc SF=\"".$Loc["SF"]."\" Dtm=\"".$Loc["date"]."\" lat=\"".$Loc["latitude"]."\" lng=\"".$Loc["longitude"]."\" Auc=\"".$Loc["theAccuracy"]."\" Btry=\"".$Loc["Btry"]."\" mock=\"".$Loc["mock"]."\" />"; 
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

function getCompetitorDetMap() {
    global $data;

    $sfCode = (string) $data['SF'];
    $query = "exec iOS_getMas_CompetitorDetsByProd '".$sfCode."'";
    $res=performQuery($query);
	$result=[];
	for($il=0;$il<count($res);$il++){
		$rw=$res[$il];
		$arw=explode("/", $rw["Competitor_Prd_bulk"]);
		$iRes=[];
		for($ij=0;$ij<count($arw);$ij++){
			if($arw[$ij]!=""){
				$arw1=explode("~", $arw[$ij]);
				$Prod=explode("#", $arw1[0]);
				$Cmpt=explode("$", $arw1[1]);
				array_push($iRes,array('CCode' => $Cmpt[0],'CName' => $Cmpt[1],'PCode' => $Prod[0],'PName' => $Prod[1]));
			}
		}
		array_push($result,array('OProdCd' => $rw["Our_prd_code"],'OProdNm' => $rw["Our_prd_name"],'Cmpt' => $iRes));
	}
	return $result;
}

function getProductFeedback(){
	global $data;
	$div = (string) $data['div'];
	$query = "select FeedBack_Id id,FeedBack_Name name from Mas_Product_Feedback where Division_code='" . $div . "' ";
	$results=performQuery($query); 
	return $results;
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

function getEtDyCalls() {
    global $data;

    $sfCode=(string) $data['SF'];
	$dcrdt=date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']));
    $query="exec iOS_getEditdayCalls '".$sfCode."','".$dcrdt."'";
	$retdata =  performQuery($query);
	//echo ($retdata);
    return $retdata;
}

function getEtDates() {
    global $data;

    $sfCode=(string) $data['SF'];
	$dcrdt=date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']));
	$sql = "select Division_Code from Mas_Salesforce where Sf_Code='".$sfCode."'";
	$divi_code=performQuery($sql);
	//echo $divi_code[0]['Division_Code'];
	$divcode = str_replace(",", "",$divi_code[0]['Division_Code']);
	//echo $divcode;
    $query="exec iOS_getEditdayDates '".$sfCode."','".$dcrdt."','".$divcode."'";
	$retdata =  performQuery($query);
	//echo ($retdata);
    return $retdata;
}

function svEtDates(){
	 global $data;
	 $sfCode=(string) $data['SF'];
	 $dcrdt=date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']));
	 $query ="exec iOS_svEditDates '".$sfCode."','".$dcrdt."'";
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

function SvTPApprovalNew(){
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
			$TPWTCd=array();$TPWTNm=array();$TPSFCd=array();$TPSFNm=array();$TPPlCd=array();$TPPlNm=array();
			$TPDRCd=array();$TPDRNm=array();$TPCHCd=array();$TPCHNm=array();$TPJWCd=array();$TPJWNm=array();$TPRmks=array();
			for($il=0;$il<count($TPDet);$il++){
				array_push($TPWTCd,$TPDet[$il]["WTCd"]);
				array_push($TPWTNm,$TPDet[$il]["WTNm"]);
				
				array_push($TPSFCd,$TPDet[$il]["HQCd"]);
				array_push($TPSFNm,$TPDet[$il]["HQNm"]);

				array_push($TPPlCd,$TPDet[$il]["TerrCd"]);
				array_push($TPPlNm,$TPDet[$il]["TerrNm"]);

				array_push($TPJWCd,$TPDet[$il]["JWCd"]);
				array_push($TPJWNm,$TPDet[$il]["JWNm"]);
				array_push($TPDRCd,$TPDet[$il]["DRCd"]);
				array_push($TPDRNm,$TPDet[$il]["DRNm"]);
				array_push($TPCHCd,$TPDet[$il]["CHCd"]);
				array_push($TPCHNm,$TPDet[$il]["CHNm"]);
				array_push($TPRmks,$TPDet[$il]["DayRmk"]);

			}

			$query="exec iOS_svTourApprovalNew '".$sfCode."','".$sfName."','".$data['TPMonth']."','".$data['TPYear']."','".$TPData["TPDt"]."','".$Stat."','".$TPWTCd[0]."','".$TPWTCd[1]."','".$TPWTCd[2]."','".$TPWTNm[0]."','".$TPWTNm[1]."','".$TPWTNm[2]."','".$TPSFCd[0]."','".$TPSFCd[1]."','".$TPSFCd[2]."','".$TPSFNm[0]."','".$TPSFNm[1]."','".$TPSFNm[2]."','".$TPPlCd[0]."','".$TPPlCd[1]."','".$TPPlCd[2]."','".$TPPlNm[0]."','".$TPPlNm[1]."','".$TPPlNm[2]."','".$TPJWCd[0]."','".$TPJWCd[1]."','".$TPJWCd[2]."','".$TPJWNm[0]."','".$TPJWNm[1]."','".$TPJWNm[2]."','".$TPDRCd[0]."','".$TPDRCd[1]."','".$TPDRCd[2]."','".$TPDRNm[0]."','".$TPDRNm[1]."','".$TPDRNm[2]."','".$TPCHCd[0]."','".$TPCHCd[1]."','".$TPCHCd[2]."','".$TPCHNm[0]."','".$TPCHNm[1]."','".$TPCHNm[2]."','".$TPRmks[0]."','".$TPRmks[1]."','".$TPRmks[2]."','".$DivCode[0]."'";
			performQuery($query);
			$result["Qry"]=$query;
		} 
	}
    $result["success"]=true;
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

function getTodayTPNew() {
    global $data;

    $sfCode=(string) $data['SF'];
    $DCRDt = date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']));
    $query="exec iOS_getTodayTPNew '".$sfCode."','$DCRDt'";
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

function getLeaveType() {

    global $data;	
	$div_codes = (string) $data['div'];	
	$div_code = explode(",", $div_codes.",");
    $query = "select * from mas_Leave_Type where division_code='".$div_code[0]."'";	
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
/*function getTourPlan(){
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
                'JWCodes' => $res[$il]["JWCodes"],
                'JWNames' => $res[$il]["JWNames"],
                'WTCode' => $res[$il]["WTCode"],
                'WTName' => $res[$il]["WTName"]),'EFlag' =>$res[$il]["EFlag"],'TPDt' =>$res[$il]["TPDt"],'access' =>$res[$il]["access"],'dayno' =>$res[$il]["dayno"]));
      }
      
      array_push($result,array('SFCode' => $sfCode,'SFName' =>$res[0]["SFName"],'DivCode' =>$res[0]["Div"],'TPDatas' =>$resTP,'TPFlag' => '0','TPMonth' => $res[0]["Mnth"],'TPYear' => $res[0]["Yr"]));
    }
    return $result;
}*/

function getMissingDates(){
    global $data;
    $sfCode=(string) $data['SF'];
    $query="exec Get_MissedDates_App_detail '".$sfCode."'";
    return performQuery($query);

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
function getConversation(){
    global $data;
    $sfCode=(string) $data['SF'];
    $msgDt=(string) $data['MsgDt'];
    $query="exec iOS_GetMsgConversation '".$sfCode."','".$msgDt."'";
    $result=performQuery($query);
    $query="exec iOS_GetMsgConversationFiles '".$sfCode."','".$msgDt."'";
    $result1=performQuery($query);
	for($il=0;$il<count($result);$il++){
		$msgId=$result[$il]["Msg_Id"];
		$rArry=array_filter($result1, function($item) use ($msgId){
				return ($item["Msg_Id"]===$msgId);
			});
			$nAry=array();
			foreach($rArry as $key => $value){
				$nAry[]=$rArry[$key];
			}
			$result[$il]["Files"]=$nAry;
	}
    return $result;
}

function svConversation(){
    global $data;
    $sXML="<Root>";
    $sXML=$sXML."<Msg SF=\"".$data['SF']."\" Dt=\"".$data["MsgDt"]."\" To=\"".$data["MsgTo"]."\" ToName=\"".$data["MsgToName"]."\" mTxt=\"".$data["MsgText"]."\" mPID=\"".$data["MsgParent"]."\" />";
    $sXML=$sXML."</Root>";
 
    $query="exec iOS_SvMsgConversation '".$sXML."'";
    return performQuery($query);
}
function getTpSetup(){	
	global $data;	
	$sfCode = (string) $data['div'];	
	$query = "select * from tpSetup where div='".$sfCode."'";	
	return performQuery($query);
}

function getTrackSetUP(){
global $data;
$sfCode = (string) $data['SF'];
$div = (string) $data['div'];
$query = "exec getTrackSetup '".$sfCode."','".$div."'";
return performQuery($query);

}
function getCallDetails(){

	global $data;

	$ADetSLNo = (string) $data['ADetSLNo'];
    $sf=(string) $data['APPUserSF'];
	$CustCode = (string) $data['CustCode'];
    $CustName=data['CustName'];
	$CustType=data['CustType'];
    $vstTime=data['vstTime'];
	$query="exec iOS_GetCallDetails '".$ADetSLNo."','".$CustCode."','".$CustType."'";
	$Result=performQuery($query);
	return $Result;
    /*self.meetData.DataSF=[optLst objectForKey:@"DataSF"];
    self.meetData.CustCode=[optLst objectForKey:@"CustCode"];
    self.meetData.CustName=[optLst objectForKey:@"CustName"];
    self.meetData.CusType=[optLst objectForKey:@"CusType"];
    self.meetData.SpecCode=[optLst objectForKey:@"SpecCode"];
    self.meetData.CateCode=[optLst objectForKey:@"CateCode"];
    self.meetData.vstTime=[optLst objectForKey:@"vstTime"];
    self.meetData.ModTime=[optLst objectForKey:@"ModTime"];
    self.meetData.mappedProds=[optLst objectForKey:@"mappedProds"];
    self.meetData.Products=[[optLst objectForKey:@"Products"] mutableDeepCopy];
    self.meetData.Inputs=[[optLst objectForKey:@"Inputs"] mutableDeepCopy];
    self.meetData.AdCuss=[[optLst objectForKey:@"AdCuss"] mutableDeepCopy];
    self.meetData.RCPAEntry=[[optLst objectForKey:@"RCPAEntry"] mutableDeepCopy];
    self.meetData.JWWrk=[[optLst objectForKey:@"JWWrk"] mutableDeepCopy];
    self.meetData.Remks=[optLst objectForKey:@"Remks"];*/

}
function DeleteCalls(){

    global $data;

    $slNo=(string) $data['ADetSLNo'];
    $query = "exec iOS_DeleteCallEntry '".$slNo."'";
    return performQuery($query);
}
function SvMyTodayTP(){
    global $data;

    $DivCodes = (string) $data['Div'];
    $DivCode = explode(",", $DivCodes.",");
    $sfCode=(string) $data['SF'];
    $SFMem=(string) $data['SFMem'];
    $TPDt=(string) $data['TPDt'];
    $PlnCd=(string) $data['Pl'];
    $PlnNM=(string) $data['PlNm'];
    $WT=(string) $data['WT'];
    $WTNM=(string) $data['WTNMm'];
    $Rem=(string) $data['Rem'];
    $loc=(string) $data['location'];
    $query="select SF_type from Mas_Salesforce where Sf_Code='".$sfCode."'";
    $ExisArr = performQuery($query);
	$SFTy=$ExisArr[0]["SF_type"];
	if($WT=="0" || $WT=="" || $WT==" ")
    {
        $query="select type_code from vwMas_WorkType_all where Division_Code='".$DivCode[0]."' and SFTyp='".$SFTy."' and Wtype='Field Work' ";
        $resis= performQuery($query);
        $WT= $resis[0]["type_code"];
    }
    $InsMode=(string) $data['InsMode'];
	  $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,'".$TPDt."',101) as datetime) and FWFlg='L'";
    $ExisArr = performQuery($query);
	  if ($ExisArr[0]["Cnt"]>0){
			  $result["Msg"]="Today Already Leave Posted...";
			  $result["success"]=false;
			  return $result;
	  }else{
        $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,'".$TPDt."',101) as datetime) and Work_Type<>'".$WT."'";
		    $ExisArr = performQuery($query);
        $result["cqry"]=$query;
		if ($ExisArr[0]["Cnt"]>0 && $InsMode=="0"){
			$result["Msg"]="Already you are submitted your work. Now you are deviate. Do you want continue?";
			$result["update"]=true;
			$result["success"]=false;
		}
		else{
			$query="exec iOS_svTodayTP '".$sfCode."','".$SFMem."','".$PlnCd."','".$PlnNM."','".$WT."','".$WTNM."','".$Rem."','".$loc."'";
			performQuery($query);
			if ($InsMode=="2")
			{
				$query="select Work_Type,WorkType_Name,FWFlg,Half_Day_FW from vwActivity_Report where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,'".$TPDt."',101) as datetime) and Work_Type<>'".$WT."'";
				$ExisArr = performQuery($query);
				$PwTy=$ExisArr[0]["Work_Type"];
				$PwTyNm=$ExisArr[0]["WorkType_Name"];
				$PwFl=$ExisArr[0]["FWFlg"];
				$HwTy=$ExisArr[0]["Half_Day_FW"];
        $query="select FWFlg,Wtype from vwMas_WorkType_all where SFTyp='". $SFTy."' and type_code='".$WT."'";
				$ExisArr = performQuery($query);
				
				$query="update DCRMain_Trans set ";
				if($PwFl!="F"  ){
					$HwTy = $HwTy . $PwTy . ",";
					$query=$query." Work_type='" . $WT . "',FieldWork_Indicator='".$ExisArr[0]["FWFlg"]."',WorkType_Name='" . $ExisArr[0]["Wtype"] . "',";
				}else{
					$HwTy = $HwTy . $WT . ",";
				}
				$query=$query."Half_Day_FW='" . $HwTy . "' where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,'".$TPDt."',101) as datetime)";
				performQuery($query);
        performQuery(str_replace("DCRMain_Trans", "DCRMain_Temp", $query));
			}
			else
			{ 
				if ($InsMode=="1")
				{
					$query="exec DelDCRTempByDt '".$sfCode."','" . date('Y-m-d 00:00:00.000', strtotime($TPDt))."'";
					performQuery($query);
				}
        
				$query="exec svDCRMain_App '" . $sfCode . "','" . date('Y-m-d 00:00:00.000', strtotime($TPDt)) . "','" . $WT . "','" . $PlnCd . "','" . $DivCode[0] . "','" . $Rem . "','','iOS'";
				performQuery($query);
			}
			$result["Msg"]="Today Work Plan Submitted Successfully...";
			$result["success"]=true;
		}
			return $result;
	}
}
/*function SvMyTodayTP(){
    global $data;

    $sfCode=(string) $data['SF'];
    $SFMem=(string) $data['SFMem'];
    $PlnCd=(string) $data['Pl'];
    $PlnNM=(string) $data['PlNm'];
    $WT=(string) $data['WT'];
    $WTNM=(string) $data['WTNMm'];
    $Rem=(string) $data['Rem'];
    $loc=(string) $data['location'];
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
}*/

function SvMyTodayTPNew(){
    global $data;

    $DivCodes = (string) $data['Div'];
    $DivCode = explode(",", $DivCodes.",");
    $sfCode=(string) $data['SF'];
    $SFMem=(string) $data['SFMem'];
    $TPDt=(string) $data['TPDt'];
    $PlnCd=(string) $data['Pl'];
    $PlnNM=(string) $data['PlNm'];
    $WT=(string) $data['WT'];
    $WTNM=(string) $data['WTNMm'];
    $Rem=(string) $data['Rem'];
    $loc=(string) $data['location'];
	$TpVwFlg=(string) $data['TpVwFlg'];
	//$TP_Doctor=(string) $data['TP_cluster'];
	$TP_cluster=(string) $data['TP_cluster'];
	$TP_worktype=(string) $data['TP_worktype'];
    $query="select SF_type from Mas_Salesforce where Sf_Code='".$sfCode."'";
    $ExisArr = performQuery($query);
	$SFTy=$ExisArr[0]["SF_type"];
	if($WT=="0" || $WT=="" || $WT==" ")
    {
        $query="select type_code from vwMas_WorkType_all where Division_Code='".$DivCode[0]."' and SFTyp='".$SFTy."' and Wtype='Field Work' ";
        $resis= performQuery($query);
        $WT= $resis[0]["type_code"];
    }
    $InsMode=(string) $data['InsMode'];
	  $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,cast('".$TPDt."' as datetime),101) as datetime) and FWFlg='L'";
    $ExisArr = performQuery($query);
	  if ($ExisArr[0]["Cnt"]>0){
			  $result["Msg"]="Today Already Leave Posted...";
			  $result["success"]=false;
			  return $result;
	  }else{
        $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,cast('".$TPDt."' as datetime),101) as datetime) and Work_Type<>'".$WT."'";
		    $ExisArr = performQuery($query);
        $result["cqry"]=$query;
		if ($ExisArr[0]["Cnt"]>0 && $InsMode=="0"){
			$result["Msg"]="Already you are submitted your work. Now you are deviate. Do you want continue?";
			$result["update"]=true;
			$result["success"]=false;
		}
		else{
			$query="exec iOS_svTodayTPNew '".$sfCode."','".$SFMem."','".$PlnCd."','".$PlnNM."','".$WT."','".$WTNM."','".$Rem."','".$loc."','".$TPDt."','".$TpVwFlg."','','".$TP_cluster."','".$TP_worktype."'";
			performQuery($query);
			if ($InsMode=="2")
			{
				$query="select Work_Type,WorkType_Name,FWFlg,Half_Day_FW from vwActivity_Report where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,cast ('".$TPDt."' as datetime),101) as datetime) and Work_Type<>'".$WT."'";
				$ExisArr = performQuery($query);
				$PwTy=$ExisArr[0]["Work_Type"];
				$PwTyNm=$ExisArr[0]["WorkType_Name"];
				$PwFl=$ExisArr[0]["FWFlg"];
				$HwTy=$ExisArr[0]["Half_Day_FW"];
        $query="select FWFlg,Wtype from vwMas_WorkType_all where SFTyp='". $SFTy."' and type_code='".$WT."'";
				$ExisArr = performQuery($query);
				
				$query="update DCRMain_Trans set ";
				if($PwFl!="F"  ){
					$HwTy = $HwTy . $PwTyNm . ",";
					$query=$query." Work_type='" . $WT . "',FieldWork_Indicator='".$ExisArr[0]["FWFlg"]."',WorkType_Name='" . $ExisArr[0]["Wtype"] . "',";
				}else{
					$HwTy = $HwTy . $WTNM . ",";
				}
				$query=$query."Half_Day_FW='" . $HwTy . "',Remarks='".$Rem."' where Sf_Code='".$sfCode."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,cast('".$TPDt."' as datetime),101) as datetime)";
				$result["sqry"]=$query;
				performQuery($query);
        performQuery(str_replace("DCRMain_Trans", "DCRMain_Temp", $query));
			}
			else
			{ 
				if ($InsMode=="1")
				{
					$query="exec DelDCRTempByDt '".$sfCode."','" . date('Y-m-d 00:00:00.000', strtotime($TPDt))."'";
					performQuery($query);
				}
        
				$query="exec svDCRMain_App '" . $sfCode . "','" . date('Y-m-d 00:00:00.000', strtotime($TPDt)) . "','" . $WT . "','" . $PlnCd . "','" . $DivCode[0] . "','" . $Rem . "','','iOS'";
				performQuery($query);
			}
			$result["Msg"]="Today Work Plan Submitted Successfully...";
			$result["success"]=true;
		}
			return $result;
	}
}

function getTourPlaniPad(){
    global $data;
 	$data = json_decode($_POST['data'], true);
    $sfCode=(string) $data['SF'];
    $Mnth=(string) $data['Month'];
    $Yr=(string) $data['Year'];
	
	 $query="select SFCode,SFName,Div,Mnth,Yr,dayno,Change_Status,Rejection_Reason,convert(varchar,TPDt,20)TPDt,WTCode,WTCode2,WTCode3,WTName,WTName2,WTName3,ClusterCode,ClusterCode2,ClusterCode3,ClusterName,ClusterName2,ClusterName3,ClusterSFs,ClusterSFNms,JWCodes,JWNames,JWCodes2,JWNames2,JWCodes3,JWNames3,Dr_Code,Dr_Name,Dr_two_code,Dr_two_name,Dr_three_code,Dr_three_name,Chem_Code,Chem_Name,Chem_two_code,Chem_two_name,Chem_three_code,Chem_three_name,Stockist_Code,Stockist_Name,Stockist_two_code,Stockist_two_name,Stockist_three_code,Stockist_three_name,Day,Tour_Month,Tour_Year,tpmonth,tpday,DayRemarks,DayRemarks2,DayRemarks3,access,EFlag,FWFlg,FWFlg2,FWFlg3,HQCodes,HQNames,HQCodes2,HQNames2,HQCodes3,HQNames3,submitted_time,Entry_mode from Tourplan_detail where SFCode='".$sfCode."' and cast(Mnth as int)='".$Mnth."' and Yr='".$Yr."' order by cast(dayno as int) ASC";
  //  $query="exec iOS_getTourPlan '".$sfCode."','".$Mnth."','".$Yr."'";
    $res=performQuery($query);

	$result=array();
	$resTP=array();

	if (count($res)>0) 
	{
		for ($il=0;$il<count($res);$il++)
		{
			/*
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
			*/
			$dypl=array();
			
			if($res[$il]["WTCode"]!="" && $res[$il]["WTCode"]!="0"){
                $dypl["ClusterCode"] = $res[$il]["ClusterCode"];
                $dypl["ClusterName"] = $res[$il]["ClusterName"];
				if( $res[$il]["HospCode"]=="null") $res[$il]["HospCode"]="";
				if($res[$il]["HospCode"]!="" && $res[$il]["HospCode"]!="0" )
				{
                $dypl["HospCode"] = $res[$il]["HospCode"];
				}
				else
				{
					$dypl["HospCode"] = "";
				}
				if( $res[$il]["HospName"]=="null") $res[$il]["HospName"]="";
				if($res[$il]["HospName"]!="" && $res[$il]["HospName"]!="0")
				{
                $dypl["HospName"] = $res[$il]["HospName"];
				}
				else
				{
					 $dypl["HospName"] = "";
				}
                $dypl["ClusterSFNms"] = $res[$il]["ClusterSFNms"];
                $dypl["ClusterSFs"] = $res[$il]["ClusterSFs"];
                $dypl["FWFlg"] = $res[$il]["FWFlg"];
                $dypl["DayRemarks"] = $res[$il]["DayRemarks"];
                $dypl["HQCodes"] = $res[$il]["HQCodes"];
                $dypl["HQNames"] = $res[$il]["HQNames"];
				$dypl["JWCodes"] = $res[$il]["JWCodes"];
				$dypl["JWNames"] = $res[$il]["JWNames"];
				$dypl["DrsCodes"] = $res[$il]["Dr_Code"];
				$dypl["DrsNames"] = $res[$il]["Dr_Name"];
				$dypl["ChmCodes"] = $res[$il]["Chem_Code"];
				$dypl["ChmNames"] = $res[$il]["Chem_Name"]; 
                $dypl["WTCode"] = $res[$il]["WTCode"];
                $dypl["WTName"] = $res[$il]["WTName"];
			}
			
			array_push($resTP,array('DayPlan' => $dypl ,'EFlag' =>$res[$il]["EFlag"],'TPDt' =>$res[$il]["TPDt"],'access' =>$res[$il]["access"],'dayno' =>$res[$il]["dayno"]));
      	}
      
		array_push($result,array('SFCode' => $sfCode,'SFName' =>$res[0]["SFName"],'DivCode' =>$res[0]["Div"],'status' =>$res[0]["Change_Status"],'TPDatas' =>$resTP,'TPFlag' => '0','TPMonth' => $res[0]["Mnth"],'TPYear' => $res[0]["Yr"],'reject'=>$res[0]["Rejection_Reason"]));
    }
    return $result;
}

function getTourPlan(){
    global $data;
	//echo $_POST['data'];
 	$data = json_decode($_POST['data'], true);
    $sfCode=(string) $data['SF'];
    $Mnth=(string) $data['Month'];
    $Yr=(string) $data['Year'];
	
	// $sql="select * from vwTrans_TP_View where sf_code='".$sfCode."' and tour_month='".$Mnth."' and tour_year='".$Yr."'";
	// $rt=performQuery($sql);

	 // if(count($rt)==0  ){
	// $sql="select division_Code from mas_salesforce where sf_code='".$sfCode."'";
	// $rt1=performQuery($sql);
	// $divi = (string) str_replace(",", "", $rt1[0]['division_Code']);
	// holidays_weekly($sfCode,$divi,'next');
	// holidays_weekly($sfCode,$divi,'previous');
	// holidays_weekly($sfCode,$divi,'current');
	 // }
	
		 $query="select SFCode,SFName,Div,Mnth,Yr,dayno,Change_Status,Rejection_Reason,convert(varchar,TPDt,20)TPDt,WTCode,WTCode2,WTCode3,WTName,WTName2,WTName3,ClusterCode,ClusterCode2,ClusterCode3,ClusterName,ClusterName2,ClusterName3,ClusterSFs,ClusterSFNms,JWCodes,JWNames,JWCodes2,JWNames2,JWCodes3,JWNames3,Dr_Code,Dr_Name,Dr_two_code,Dr_two_name,Dr_three_code,Dr_three_name,Chem_Code,Chem_Name,Chem_two_code,Chem_two_name,Chem_three_code,Chem_three_name,Stockist_Code,Stockist_Name,Stockist_two_code,Stockist_two_name,Stockist_three_code,Stockist_three_name,Day,Tour_Month,Tour_Year,tpmonth,tpday,DayRemarks,DayRemarks2,DayRemarks3,access,EFlag,FWFlg,FWFlg2,FWFlg3,HQCodes,HQNames,HQCodes2,HQNames2,HQCodes3,HQNames3,submitted_time,Entry_mode from Tourplan_detail where SFCode='".$sfCode."' and cast(Mnth as int)='".$Mnth."' and Yr='".$Yr."' order by cast(dayno as int) ASC";
   
  // $query="exec iOS_getTourPlan '".$sfCode."','".$Mnth."','".$Yr."'";
    $res=performQuery($query);
//echo $query;
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
    }
    return $result;
}

function SvTourPlanNew($Stat){ 
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
			$TPWTCd=array();$TPWTNm=array();$TPSFCd=array();$TPSFNm=array();$TPPlCd=array();$TPPlNm=array();
			$TPDRCd=array();$TPDRNm=array();$TPCHCd=array();$TPCHNm=array();$TPJWCd=array();$TPJWNm=array();$TPRmks=array();
			for($il=0;$il<count($TPDet);$il++){
				array_push($TPWTCd,$TPDet[$il]["WTCd"]);
				array_push($TPWTNm,$TPDet[$il]["WTNm"]);
				
				array_push($TPSFCd,$TPDet[$il]["HQCd"]);
				array_push($TPSFNm,$TPDet[$il]["HQNm"]);

				array_push($TPPlCd,$TPDet[$il]["TerrCd"]);
				array_push($TPPlNm,$TPDet[$il]["TerrNm"]);

				array_push($TPJWCd,$TPDet[$il]["JWCd"]);
				array_push($TPJWNm,$TPDet[$il]["JWNm"]);
				array_push($TPDRCd,$TPDet[$il]["DRCd"]);
				array_push($TPDRNm,$TPDet[$il]["DRNm"]);
				array_push($TPCHCd,$TPDet[$il]["CHCd"]);
				array_push($TPCHNm,$TPDet[$il]["CHNm"]);
				array_push($TPRmks,$TPDet[$il]["DayRmk"]);

			}

			$query="exec iOS_svTourPlanNew '".$sfCode."','".$sfName."','".$data['TPMonth']."','".$data['TPYear']."','".$TPData["TPDt"]."','".$Stat."','".$TPWTCd[0]."','".$TPWTCd[1]."','".$TPWTCd[2]."','".$TPWTNm[0]."','".$TPWTNm[1]."','".$TPWTNm[2]."','".$TPSFCd[0]."','".$TPSFCd[1]."','".$TPSFCd[2]."','".$TPSFNm[0]."','".$TPSFNm[1]."','".$TPSFNm[2]."','".$TPPlCd[0]."','".$TPPlCd[1]."','".$TPPlCd[2]."','".$TPPlNm[0]."','".$TPPlNm[1]."','".$TPPlNm[2]."','".$TPJWCd[0]."','".$TPJWCd[1]."','".$TPJWCd[2]."','".$TPJWNm[0]."','".$TPJWNm[1]."','".$TPJWNm[2]."','".$TPDRCd[0]."','".$TPDRCd[1]."','".$TPDRCd[2]."','".$TPDRNm[0]."','".$TPDRNm[1]."','".$TPDRNm[2]."','".$TPCHCd[0]."','".$TPCHCd[1]."','".$TPCHCd[2]."','".$TPCHNm[0]."','".$TPCHNm[1]."','".$TPCHNm[2]."','".$TPRmks[0]."','".$TPRmks[1]."','".$TPRmks[2]."','".$DivCode[0]."'";
			performQuery($query);
			$query="exec svTourPlan_detail '".$sfCode."','".$sfName."','".$data['TPMonth']."',
			'".$data['TPYear']."','".$TPData["TPDt"]."','".$Stat."','".$TPWTCd."','".$TPWTCd2."','".$TPWTCd3."',
			'".$TPWTNm."','".$TPWTNm2."','".$TPWTNm3."','".$TPSFCd."','".$TPSFCd2."','".$TPSFCd3."',
			'".$TPSFNm."','".$TPSFNm2."','".$TPSFNm3."','".$TPPlCd."','".$TPPlCd2."','".$TPPlCd3."',
			'".$TPPlNm."','".$TPPlNm2."','".$TPPlNm3."','".$TPJWCd."','".$TPJWCd2."',
			'".$TPJWCd3."','".$TPJWNm."','".$TPJWNm2."','".$TPJWNm3."','".$TPDrsCd."','".$TPDrsCd2."',
			'".$TPDrsCd3."','".$TPDrsNm."','".$TPDrsNm2."','".$TPDrsNm3."','".$TPCHCd."','".$TPCHCd2."',
			'".$TPCHCd3."','".$TPCHNm."','".$TPCHNm2."','".$TPCHNm3."','".$TPDet["DayRemarks1"]."','".$TPDet["DayRemarks2"]."','".$TPDet["DayRemarks3"]."','".$DivCode[0]."','','','','','',''";
			//'".$TPSTCd[0]."','".$TPSTNm[0]."','".$TPSTCd[1]."','".$TPSTNm[1]."','".$TPSTCd[2]."','".$TPSTNm[2]."'";
			performQuery($query);
			$result["Qry"]=$query;
		} 
	}
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
			if($TPDet["WTCode"]=="") $TPDet=$TPData["DayPlan"][0];

			$TPWTCd=$TPDet["WTCode"];$TPWTNm=$TPDet["WTName"];
			$TPWTCd2=$TPDet["WTCode2"];$TPWTNm2=$TPDet["WTName2"];
			$TPWTCd3=$TPDet["WTCode3"];$TPWTNm3=$TPDet["WTName3"];
			
			$TPPlCd=$TPDet["ClusterCode"];$TPPlNm=$TPDet["ClusterName"];
			$TPPlCd2=$TPDet["ClusterCode2"];$TPPlNm2=$TPDet["ClusterName2"];
			$TPPlCd3=$TPDet["ClusterCode3"];$TPPlNm3=$TPDet["ClusterName3"];
			
			$TPHosCd=$TPDet["HospCode"];$TPHosNm=$TPDet["HospName"];
			$TPHosCd2=$TPDet["HospCode2"];$TPHosNm2=$TPDet["HospName2"];
			$TPHosCd3=$TPDet["HospCode3"];$TPHosNm3=$TPDet["HospName3"];
			
			$TPSFCd=$TPDet["HQCodes"];$TPSFNm=$TPDet["HQNames"];
			$TPSFCd2=$TPDet["HQCodes2"];$TPSFNm2=$TPDet["HQNames2"];
			$TPSFCd3=$TPDet["HQCodes3"];$TPSFNm3=$TPDet["HQNames3"];
			
			$TPJWCd=$TPDet["JWCodes"];$TPJWNm=$TPDet["JWNames"];
			$TPJWCd2=$TPDet["JWCodes2"];$TPJWNm2=$TPDet["JWNames2"];
			$TPJWCd3=$TPDet["JWCodes3"];$TPJWNm3=$TPDet["JWNames3"];
			
			$TPDrsCd=$TPDet["DrsCodes"];$TPDrsNm=$TPDet["DrsNames"];
			$TPDrsCd2=$TPDet["DrsCodes2"];$TPDrsNm2=$TPDet["DrsNames2"];
			$TPDrsCd3=$TPDet["DrsCodes3"];$TPDrsNm3=$TPDet["DrsNames3"];
			
			$TPCHCd=$TPDet["ChmCodes"];$TPCHNm=$TPDet["ChmNames"];
			$TPCHCd2=$TPDet["ChmCodes2"];$TPCHNm2=$TPDet["ChmNames2"];
			$TPCHCd3=$TPDet["ChmCodes3"];$TPCHNm3=$TPDet["ChmNames3"];
			
			$TPWFlg=$TPDet["FWFlg"];$TPWFlg2=$TPDet["FWFlg2"];$TPWFlg3=$TPDet["FWFlg3"];
			
			$query="exec iOS_svTourPlanNew '".$sfCode."','".$sfName."','".$data['TPMonth']."','".$data['TPYear']."','".$TPData["TPDt"]."',1,'".$TPWTCd."','".$TPWTCd2."','".$TPWTCd3."','".$TPWTNm."','".$TPWTNm2."','".$TPWTNm3."','".$TPSFCd."','".$TPSFCd2."','".$TPSFCd3."','".$TPSFNm."','".$TPSFNm2."','".$TPSFNm3."','".$TPPlCd."','".$TPPlCd2."','".$TPPlCd3."','".$TPPlNm."','".$TPPlNm2."','".$TPPlNm3."','".$TPJWCd."','".$TPJWCd2."','".$TPJWCd3."','".$TPJWNm."','".$TPJWNm2."','".$TPJWNm3."','".$TPDrsCd."','".$TPDrsCd2."','".$TPDrsCd3."','".$TPDrsNm."','".$TPDrsNm2."','".$TPDrsNm3."','".$TPCHCd."','".$TPCHCd2."','".$TPCHCd3."','".$TPCHNm."','".$TPCHNm2."','".$TPCHNm3."','".$TPDet["DayRemarks1"]."','".$TPDet["DayRemarks2"]."','".$TPDet["DayRemarks3"]."','".$DivCode[0]."','','','','','','',0,'IOS','".$TPHosCd."','".$TPHosNm."','".$TPHosCd2."','".$TPHosNm2."','".$TPHosCd3."','".$TPHosNm3."'";
			//$query="exec iOS_svTourPlan '".$sfCode."','".$sfName."','".$data['TPMonth']."','".$data['TPYear']."','".$TPData["TPDt"]."','".$TPWTCd."','".$TPWTNm."','".$TPPlCd."','".$TPPlNm."','".$TPJWCd."','".$TPJWNm."','".$DivCode[0]."','".$TPDet["DayRemarks"]."'";
			performQuery($query);
			
			
			$query="exec svTourPlan_detail '".$sfCode."','".$sfName."','".$data['TPMonth']."',
			'".$data['TPYear']."','".$TPData["TPDt"]."','".$Stat."','".$TPWTCd."','".$TPWTCd2."','".$TPWTCd3."',
			'".$TPWTNm."','".$TPWTNm2."','".$TPWTNm3."','".$TPSFCd."','".$TPSFCd2."','".$TPSFCd3."',
			'".$TPSFNm."','".$TPSFNm2."','".$TPSFNm3."','".$TPPlCd."','".$TPPlCd2."','".$TPPlCd3."',
			'".$TPPlNm."','".$TPPlNm2."','".$TPPlNm3."','".$TPJWCd."','".$TPJWCd2."',
			'".$TPJWCd3."','".$TPJWNm."','".$TPJWNm2."','".$TPJWNm3."','".$TPDrsCd."','".$TPDrsCd2."',
			'".$TPDrsCd3."','".$TPDrsNm."','".$TPDrsNm2."','".$TPDrsNm3."','".$TPCHCd."','".$TPCHCd2."',
			'".$TPCHCd3."','".$TPCHNm."','".$TPCHNm2."','".$TPCHNm3."','".$TPDet["DayRemarks1"]."','".$TPDet["DayRemarks2"]."','".$TPDet["DayRemarks3"]."','".$DivCode[0]."','','','','','',''";
			//'".$TPSTCd[0]."','".$TPSTNm[0]."','".$TPSTCd[1]."','".$TPSTNm[1]."','".$TPSTCd[2]."','".$TPSTNm[2]."'";
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
			$TPWTCd=array();$TPWTNm=array();$TPSFCd=array();$TPSFNm=array();$TPPlCd=array();$TPPlNm=array();
			$TPDRCd=array();$TPDRNm=array();$TPCHCd=array();$TPCHNm=array();$TPJWCd=array();$TPJWNm=array();$TPRmks=array();
			for($il=0;$il<count($TPDet);$il++){
				array_push($TPWTCd,$TPDet[$il]["WTCd"]);
				array_push($TPWTNm,$TPDet[$il]["WTNm"]);
				
				array_push($TPSFCd,$TPDet[$il]["HQCd"]);
				array_push($TPSFNm,$TPDet[$il]["HQNm"]);

				array_push($TPPlCd,$TPDet[$il]["TerrCd"]);
				array_push($TPPlNm,$TPDet[$il]["TerrNm"]);

				array_push($TPJWCd,$TPDet[$il]["JWCd"]);
				array_push($TPJWNm,$TPDet[$il]["JWNm"]);
				array_push($TPDRCd,$TPDet[$il]["DRCd"]);
				array_push($TPDRNm,$TPDet[$il]["DRNm"]);
				array_push($TPCHCd,$TPDet[$il]["CHCd"]);
				array_push($TPCHNm,$TPDet[$il]["CHNm"]);
				array_push($TPRmks,$TPDet[$il]["DayRmk"]);

			}

			$query="exec iOS_svTourApprovalNew '".$sfCode."','".$sfName."','".$data['TPMonth']."','".$data['TPYear']."','".$TPData["TPDt"]."','".$Stat."','".$TPWTCd[0]."','".$TPWTCd[1]."','".$TPWTCd[2]."','".$TPWTNm[0]."','".$TPWTNm[1]."','".$TPWTNm[2]."','".$TPSFCd[0]."','".$TPSFCd[1]."','".$TPSFCd[2]."','".$TPSFNm[0]."','".$TPSFNm[1]."','".$TPSFNm[2]."','".$TPPlCd[0]."','".$TPPlCd[1]."','".$TPPlCd[2]."','".$TPPlNm[0]."','".$TPPlNm[1]."','".$TPPlNm[2]."','".$TPJWCd[0]."','".$TPJWCd[1]."','".$TPJWCd[2]."','".$TPJWNm[0]."','".$TPJWNm[1]."','".$TPJWNm[2]."','".$TPDRCd[0]."','".$TPDRCd[1]."','".$TPDRCd[2]."','".$TPDRNm[0]."','".$TPDRNm[1]."','".$TPDRNm[2]."','".$TPCHCd[0]."','".$TPCHCd[1]."','".$TPCHCd[2]."','".$TPCHNm[0]."','".$TPCHNm[1]."','".$TPCHNm[2]."','".$TPRmks[0]."','".$TPRmks[1]."','".$TPRmks[2]."','".$DivCode[0]."'";
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
		$query="exec iOS_svTPReject '".$sfCode."','".$data['TPMonth']."','".$data['TPYear']."','".$data['reason']."'";
		performQuery($query);
		$result["Qry"]=$query;
//echo $result;
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
function ViewGeoTag(){

global $data;
$SF=(string) $data['SF'];
$cust=(string)$data['cust'];

$query="exec getViewTag '".$SF."','".$cust."'";

return performQuery($query);;
}
function SaveGeoTag(){

global $data;

$drcode=(string) $data['cuscode'];
$div=(string) $data['divcode'];
$lat=(string) $data['lat'];
$long=(string) $data['long'];
$cust=(string) $data['cust'];

if($cust=='D'){

$query = "exec Map_geotag '".$drcode."','".$div."','".$lat."','".$long."'";

 performQuery($query);
$result["cat"]="D";
}
else if($cust=='C'){
$query = "exec Map_Chem_geotag '".$drcode."','".$div."','".$lat."','".$long."'";
performQuery($query);
$result["cat"]="C";
}
else if($cust=='S'){
$query = "exec Map_Stock_geotag '".$drcode."','".$div."','".$lat."','".$long."'";
performQuery($query);
$result["cat"]="S";
}
else{
$query = "exec Map_Unlist_geotag '".$drcode."','".$div."','".$lat."','".$long."'";
performQuery($query);
$result["cat"]="U";
}
$result["Msg"]="Tag Submitted Successfully...";
			$result["success"]=true;
			return $result;

}

function SvNewDr(){
    global $data;

	$SF=(string) $data['SF'];
    $DivCodes = (string) $data['DivCode'];
    $DivCode = explode(",", $DivCodes.",");
	$DrName=(string) $data['DrName'];
	$DrQCd=(string) $data["DrQCd"];
	$DrQNm=(string) $data["DrQNm"];
	$DrClsCd=(string) $data["DrClsCd"];
	$DrClsNm=(string) $data["DrClsNm"];
	$DrCatCd=(string) $data["DrCatCd"];
    $CatNm=(string)  $data["DrCatNm"];
	$DrSpcCd=(string) $data["DrSpcCd"];
	$DrSpcNm=(string) $data["DrSpcNm"];
	$DrAddr=(string) $data["DrAddr"];
	$DrTerCd=(string) $data["DrTerCd"];
	$DrTerNm=(string) $data["DrTerNm"];
	$DrPincd=(string) $data["DrPincd"];
	$DrPhone=(string) $data["DrPhone"];
	$DrMob=(string) $data["DrMob"];
	$Uid=(string) $data["Uid"];
	$query="exec svNewCustomer_App 0,'','".$DrName."','".$DrAddr."','".$DrTerCd."','".$DrTerNm."','".$DrCatCd."','".$CatNm."','".$DrSpcCd."','".$DrSpcNm."','".$DrClsCd."','".$DrClsNm."','".$DrQCd."','".$DrQNm."','U','".$SF."','','','".$DrPincd."','".$DrPhone."','".$DrMob."','".$Uid."'";
   	
	$output=performQuery($query);
	$result["Qry"]=$output[0]['Msg'];
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
function getDayReport() { 
	global $data;
    $sfCode = (string) $data['rSF'];
    $dyDt = (string) $data['rptDt'];
    $query = "exec getDayReportApp '" . $sfCode . "','" . $dyDt . "'"; 
    return performQuery($query);
}
function getVstDets() {
	global $data;
    $ACd = (string) $data['ACd'];
    $typ = (string) $data['typ'];
    $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";
    return performQuery($query);
}
function getMonthSummary() {
	 global $data;
    $sfCode = (string) $data['rptSF'];
    $dyDt = (string) $data['rptDt'];
    $query = "exec getMonthSummaryApp '" . $sfCode . "','" . $dyDt . "'";
    return performQuery($query);
}
function getMissedReport() {
	global $data;
    $sfCode = (string) $data['sfCode'];
    $dyDt = (string) $data['rptDt']; 
	$first ="SELECT cast(format(cast('$dyDt' as datetime), 'yyyy-MM-01') as varchar) fdate"; 
	$res = performQuery($first);
	$fst_date =$res[0]["fdate"];
    $query = "exec Missedreport_app '" . $sfCode . "','" . $dyDt . "','" . $fst_date . "'"; 
    return performQuery($query);
}
function getMissedReportDetail() {
	global $data;
    $sfCode = (string) $data['sfCode'];
	$div = (string) $data['divisionCode'];
	$Rptdt = (string) $data['report_date'];
	$year = date('Y', strtotime($Rptdt));
	$month = date('n', strtotime($Rptdt));
    $query = "exec Missedcall_report_app '" . $div . "','" . $sfCode . "','" . $month . "','" . $year . "','" . $Rptdt . "'"; 
     return performQuery($query);
}
function getVisitCover() {
	global $data;
    $sfCode = (string) $data['SF'];
    $cMnth = (string) $data['month'];
	$cYr = (string) $data['year'];
	$div_code = (string) $data['div'];
	$div_code = str_replace(",", "", $div_code);
    $query = "exec Visit_Coverage_Analysis_App '" . $div_code . "','" . $sfCode . "','" . $cMnth . "','" . $cYr . "'"; 
    return performQuery($query);
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

function SvNewCHM(){
 global $data;

	$SF=(string) $data['SF'];
    $DivCodes = (string) $data['DivCode'];
    $DivCode = explode(",", $DivCodes.",");
$DrName=(string) $data['DrName'];
$DrAddr=(string) $data["DrAddr"];
$DrTerCd=(string) $data["DrTerCd"];
$DrName=str_replace("'","",$DrName);
$query="exec svNewCustomer_App 0,'','".$DrName."','".$DrAddr."','".$DrTerCd."','','','','','','','','','','C','".$SF."','','','','','','0'";
	$output=performQuery($query);
$result["EQry"]=$query;
	$result["Qry"]=$output;
    $result["success"]=true;
    return $result;
}

function deleteCallEntry() {

global $data;
    
   $amc = (string) $data['amc'];
   $CusType = (string) $data['CusType'];
   
	if($data['sample_validation']!=""){
		$sample_validation = $data['sample_validation'];
	}
	else{
		$sample_validation='0';
	}
	if($data['input_validation']!=""){
		$input_validation = $data['input_validation'];
	}
	else{
		$input_validation = '0';
	}
		
	
    if (!is_null($amc)) {
		if($data['CusType']=="1"){
			
			$sql = "SELECT Trans_SlNo,sf_code,Division_Code FROM DCRDetail_Lst_Temp where Trans_Detail_Slno='" . $amc . "'";
			$slno=performQuery($sql);
			//echo $slno[0]["Trans_SlNo"];
			if($slno[0]["Trans_SlNo"]==""){
				
				$sql = "SELECT Trans_SlNo,sf_code,Division_Code FROM DCRDetail_Lst_Trans where Trans_Detail_Slno='" . $amc . "'";
				$slno=performQuery($sql);
			}
		
		}
		else if($data['CusType']=="2"||$data['CusType']=="3"){
			
			$sql = "SELECT Trans_SlNo,sf_code,Division_Code FROM DCRDetail_CSH_Temp where Trans_Detail_Slno='" . $amc . "'";
			$slno=performQuery($sql);
			if($slno[0]["Trans_SlNo"]==""){
				$sql = "SELECT Trans_SlNo,sf_code,Division_Code FROM DCRDetail_CSH_Trans where Trans_Detail_Slno='" . $amc . "'";
				$slno=performQuery($sql);
			}
		
		}
		else if($data['CusType']=="4"){
			
			$sql = "SELECT Trans_SlNo,sf_code,Division_Code FROM DCRDetail_unLst_Temp where Trans_Detail_Slno='" . $amc . "'";
			$slno=performQuery($sql);
			if($slno[0]["Trans_SlNo"]==""){
				$sql = "SELECT Trans_SlNo,sf_code,Division_Code FROM DCRDetail_unLst_Trans where Trans_Detail_Slno='" . $amc . "'";
				$slno=performQuery($sql);
			}
		
		}
		$ARCd=$slno[0]["Trans_SlNo"];
		$sfCode=$slno[0]["sf_code"];
		$DivCode=$slno[0]["Division_Code"];
		
		
		
		if($sample_validation == '1' && $ARCd != '' && $amc != ''){
				$query1 = "EXEC UpdateDCRWiseSampleStock '".$ARCd."','".$amc."','".$sfCode."','".$DivCode."'";
				performQuery($query1);	
		}
		
		if($input_validation == '1' && $ARCd != '' && $amc != ''){
				$query2 = "EXEC UpdateDCRWiseInputStock '".$ARCd."','".$amc."','".$sfCode."','".$DivCode."'";
				performQuery($query2);
		}

        $sql = "DELETE s  from tbDgDetailing_SlideDetails s inner join tbDigitalDetailing_Head h on s.DDSl_No=h.DDSl_No inner join vwActivity_MSL_Details d on h.Activity_Report_code=d.Trans_SlNo AND cast(h.MSL_code as varchar)=cast(d.Trans_Detail_Info_Code as varchar) where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE h from tbDigitalDetailing_Head h inner join vwActivity_MSL_Details d on h.Activity_Report_code=d.Trans_SlNo AND cast(h.MSL_code as varchar)=cast(d.Trans_Detail_Info_Code as varchar) where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

        $sql = "DELETE FROM tbDgDetScribFilesDetail where ARM_code='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM tbDgDetailingFilesDetail where ARM_code='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_Lst_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_Lst_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

        $sql = "DELETE FROM DCRDetail_CSH_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_CSH_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

        $sql = "DELETE FROM DCRDetail_Unlst_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_Unlst_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
		
		$sql="delete DD from Trans_RCPA_Detail  DD inner join Trans_RCPA_Head HD on HD.PK_ID=DD.FK_PK_ID where ARMSL_Code='" . $amc . "'";
		performQuery($sql);
		
		$sql="delete from Trans_RCPA_Head where ARMSL_Code='" . $amc . "'";
		performQuery($sql);
		
		$result["success"]=true;
		return $result;
    }
}


function deleteEntry($amc) {
    if (!is_null($amc)) {


        $sql = "DELETE s  from tbDgDetailing_SlideDetails s inner join tbDigitalDetailing_Head h on s.DDSl_No=h.DDSl_No inner join vwActivity_MSL_Details d on h.Activity_Report_code=d.Trans_SlNo AND cast(h.MSL_code as varchar)=cast(d.Trans_Detail_Info_Code as varchar) where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE h from tbDigitalDetailing_Head h inner join vwActivity_MSL_Details d on h.Activity_Report_code=d.Trans_SlNo AND cast(h.MSL_code as varchar)=cast(d.Trans_Detail_Info_Code as varchar) where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

		$sql="delete DD from vwActivity_CSH_Detail  CSHD inner join tbDigitalDetailing_Head DD on CSHD.Trans_SlNo=Activity_Report_code and Trans_Detail_Info_Code=MSL_code   where Trans_Detail_Slno='" . $amc . "'";
		performQuery($sql);
		
		$sql="delete DD from vwActivity_Unlst_Detail  CSHD inner join tbDigitalDetailing_Head DD on CSHD.Trans_SlNo=Activity_Report_code and Trans_Detail_Info_Code=MSL_code   where Trans_Detail_Slno='" . $amc . "'";
		performQuery($sql);

        $sql = "DELETE FROM tbDgDetScribFilesDetail where ARM_code='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM tbDgDetailingFilesDetail where ARM_code='" . $amc . "'";
        performQuery($sql);


        $sql = "DELETE FROM DCRDetail_Lst_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_Lst_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

        $sql = "DELETE FROM DCRDetail_CSH_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_CSH_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);

        $sql = "DELETE FROM DCRDetail_Unlst_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_Unlst_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
		
		$sql="delete DD from Trans_RCPA_Detail  DD inner join Trans_RCPA_Head HD on HD.PK_ID=DD.FK_PK_ID where ARMSL_Code='" . $amc . "'";
		performQuery($sql);
		
		$sql="delete from Trans_RCPA_Head where ARMSL_Code='" . $amc . "'";
		performQuery($sql);

    }
}

function SvDCREntry(){
    global $data,$conn,$NeedRollBack;
    if ( sqlsrv_begin_transaction( $conn ) === false ) {
     	die( print_r( sqlsrv_errors(), true ));
    }
    $NeedRollBack=false;
    $sfCode = (string) $data['SF'];
    $VstTime = $data['vstTime'];
	$ModTime = $data['ModTime'];
    $DCRDt = date('Y-m-d 00:00:00.000', strtotime($VstTime));
    $DivCode = (string) $data['DivCode'];
    $DivCode = str_replace(",", "", $DivCode);
	
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
    $ARCd = "";
	if($data['sample_validation']!=""){
		$sample_validation = $data['sample_validation'];
	}
	else{
		$sample_validation='0';
	}
	if($data['input_validation']!=""){
		$input_validation = $data['input_validation'];
	}
	else{
		$input_validation = '0';
	}
	

    $ARDCd= $data['amc'];
    if($ARDCd!=''){ 
			
		$querys="select Trans_SlNo from vwActivity_Report where Sf_Code='".$sfCode."' and Activity_Date='".$DCRDt."' and Division_Code='".$DivCode."'";
		$arr = performQuery($querys);		
		$ARCd=$arr[0]["Trans_SlNo"];
		if($sample_validation == '1' && $ARCd != '' && $ARDCd != ''){
				$query1 = "EXEC UpdateDCRWiseSampleStock '".$ARCd."','".$ARDCd."','".$sfCode."','".$DivCode."'";
				performQuery($query1);	
		}
		
		if($input_validation == '1' && $ARCd != '' && $ARDCd != ''){
				$query2 = "EXEC UpdateDCRWiseInputStock '".$ARCd."','".$ARDCd."','".$sfCode."','".$DivCode."'";
				performQuery($query2);
		}
		deleteEntry($ARDCd);
	}
	

    $ARDCd = ""; /*(strlen($_GET['amc']) == 0) ? "0" : $_GET['amc'];*/

	if($data['WT']=="0" || $data['WT']=="" || $data['WT']==" " || is_null($data['WT']))
    {
		$query="select SF_type from Mas_Salesforce where Sf_Code='".$sfCode."'";
		$ExisArrq = performQuery($query);
		$SFTy=$ExisArrq[0]["SF_type"];
		$query="select type_code from vwMas_WorkType_all where Division_Code='".$DivCode."' and SFTyp='".$SFTy."' and Wtype='Field Work' ";
		$resis= performQuery($query);
		$data['WT']= $resis[0]["type_code"];
			// $result['success'] = false;
			// $result['Msg'] = 'Invalid worktype selection...';
			// writelog($result['Msg']);
			// outputJSON($result);
			// die;
    }

    /*if($data['CusType']=="1"){  
	$query="Select isnull(No_of_Visit,0) No_of_Visit  from Mas_ListedDr where ListedDrCode='".$data['CustCode']."'";
    	$ExisArr = performQuery($query);
	$MxCnt=$ExisArr[0]["No_of_Visit"];
	if($MxCnt>0){
  	    $query="select Count(Trans_Detail_Info_Code) Cnt from vwActivity_Msl_Details where Sf_Code='".$sfCode."' and Trans_Detail_Info_Code='".$data['CustCode']."' and  year(DCRDt)=year('".$DCRDt."')";
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
	
	$data_dcr = json_encode($_POST['data'],true);
    $data_dcr1 = str_replace("'", "",$data_dcr);
    $sql="insert into tracking_dcr select '$sfCode','$DivCode','$data_dcr',getdate(),'$DCRDt','Edet'";
    performQuery($sql);
	
    $query="exec svDCRMain_App '".$sfCode."','".$DCRDt."','".$data['WT']."','".$data['Pl']."','".$DivCode."','".$data['Rem']."','','iOS'";
	//echo $query;die;
    $result["HQry"]=$query;
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
            // $query="select top 1 Product_Code_SlNo,Product_Detail_Name from  Mas_Product_Detail where Product_Brd_Code='".$stPC."' order by Product_Code_SlNo";
            // $arr = performQuery($query);
            // if(count($arr)>0){
                // $stPC=$arr[0]["Product_Code_SlNo"];
                // $stPN=$arr[0]["Product_Detail_Name"];
            // }else{
                // $stPC="B".$stPC;
            // }
        }else if($ProdsArr[$i]["Group"]=="0"){
			  if($ProdsArr[$i]["SmpQty"]=="") $ProdsArr[$i]["SmpQty"]="0";
			  $Prods=$Prods.$stPC."~".$ProdsArr[$i]["SmpQty"];
			  $ProdsNm=$ProdsNm.$stPN."~".$ProdsArr[$i]["SmpQty"];

			//if($data['CusType']=="1" || $data['CusType']=="2"){
				if($ProdsArr[$i]["RxQty"]=="") $ProdsArr[$i]["RxQty"]="0";
				$Prods=$Prods."$".$ProdsArr[$i]["RxQty"]."$0^0";
				$ProdsNm=$ProdsNm."$".$ProdsArr[$i]["RxQty"]."$0^0";
			//}
			  $Prods=$Prods."#";
			  $ProdsNm=$ProdsNm."#";
		}else {
			 if($ProdsArr[$i]["SmpQty"]=="") $ProdsArr[$i]["SmpQty"]="0";
			  $Prods=$Prods.$stPC."~".$ProdsArr[$i]["SmpQty"];
			  $ProdsNm=$ProdsNm.$stPN."~".$ProdsArr[$i]["SmpQty"];

		//	if($data['CusType']=="1" || $data['CusType']=="2"){
				if($ProdsArr[$i]["RxQty"]=="") $ProdsArr[$i]["RxQty"]="0";
				$Prods=$Prods."$".$ProdsArr[$i]["RxQty"]."$0^0";
				$ProdsNm=$ProdsNm."$".$ProdsArr[$i]["RxQty"]."$0^0";
			//}
			  $Prods=$Prods."#";
			  $ProdsNm=$ProdsNm."#";
		}
    }

    $InpArr=$data['Inputs'];
    $Inps="";
    $InpsNm="";
	  $GC="";
	  $GN="";
	  $GQ=0;
    for ($i = 0; $i < count($InpArr); $i++) 
    {
        if($data['CusType']=="1"||$data['CusType']=="4"){
			if ($i==0){
				  $GC=$InpArr[$i]["Code"];
				  $GN=$InpArr[$i]["Name"];
				  $GQ=$InpArr[$i]["IQty"];
			
			}else{
				  $Inps=$Inps.$InpArr[$i]["Code"]."~".$InpArr[$i]["IQty"]."#";
				  $InpsNm=$InpsNm.$InpArr[$i]["Name"]."~".$InpArr[$i]["IQty"]."#";
			}
		}else{
	        $Inps=$Inps.$InpArr[$i]["Code"]."~".$InpArr[$i]["IQty"]."#";
			$InpsNm=$InpsNm.$InpArr[$i]["Name"]."~".$InpArr[$i]["IQty"]."#";
		}
    }
	$data['Remks']=str_replace("'","",$data['Remks']);
    $sLoc=str_replace(" ", ":", $data['Entry_location']);
    $loc = explode(":", str_replace("'", "", $sLoc) . ":");
    $lat = $loc[0]; //latitude
    $lng = $loc[1]; //longitude
	
	
	if($lat =="(null)"){$lat ="";$lng ="";}
    if($data['CusType']=="1"){  
      $query="exec svDCRLstDet_App '".$ARCd."',0,'".$sfCode."',1,'".$CustCode."','".$CustName."','".$VstTime."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','','','".$GC."','".$GN."','".$GQ."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."','".$Drcallfeedbackcode."','".$ModTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS','0','','','".$sample_validation."','".$input_validation."'";
      //$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
      $result["checkQry"]=$query;
	 // echo $result["checkQry"];
	// die;
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
    	$query="exec svDCRCSHDet_App '".$ARCd."',0,'".$sfCode."','".$data['CusType']."','".$CustCode."','".$CustName."','".$VstTime."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."','".$Drcallfeedbackcode."','".$ModTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS','".$sample_validation."','".$input_validation."'";
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
    	$query="exec svDCRUnlstDet_App '".$ARCd."',0,'".$sfCode."','".$data['CusType']."','".$CustCode."','".$CustName."','".$VstTime."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','','','".$GC."','".$GN."','".$GQ."','".$Inps."','".$InpsNm."','','".$data['Remks']."','".$DivCode."','".$Drcallfeedbackcode."','".$ModTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS','".$sample_validation."','".$input_validation."'";
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
        
            $query="exec svDCRLstDet_App '".$ARCd."',0,'".$sfCode."',1,'".$CustCode."','".$CustName."','".$VstTime."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','','','','','','','','','".$data['Remks']."','".$DivCode."','".$Drcallfeedbackcode."','".$ModTime."','".$lat."','".$lng."','".$data['DataSF']."','NA','iOS','0','0'";
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
			// $query="select isnull(Max(DDSl_No),0)+1 DDSl_No from tbDigitalDetailing_Head";
			// $arr = performQuery($query);
			// $DDSl = $arr[0]["DDSl_No"]; 
			
			 $query="select DivSH,DetailingSl,SFSlNo from DCR_detailingSlNo where sf_code='".$sfCode."'";
					$arr1 = performQuery($query);
					
					$detsl=$arr1[0]["DetailingSl"]+1;
						$query="update DCR_detailingSlNo set DetailingSl='".$detsl."',Max_Sl_No_Main='".$ARCd."',Max_Sl_No_Detail='".$ARDCd."' where SF_Code='".$sfCode."'";
						performQuery($query);
						$DDSl=$arr1[0]["DivSH"].$arr1[0]["SFSlNo"].'-'.$detsl;
			$query="insert into tbDigitalDetailing_Head(DDSl_No,Activity_Report_code,MSL_code,Product_Code,Product_Name,GroupID,Rating,StartTime,EndTime,Feedbk_Status) select '".$DDSl."','".$ARCd."','".$CustCode."','".$ProdsArr[$i]["Code"]."','".$ProdsArr[$i]["Name"]."','".$ProdsArr[$i]["Group"]."','".$ProdsArr[$i]["Rating"]."','".$TmLn["sTm"]."','".$TmLn["eTm"]."','".$ProdsArr[$i]["ProdFeedbk"]."'";
			performQuery($query);
			$Prods="";
			$ProdsNm="";
			if($ProdsArr[$i]["Group"]=="1"){
				$Prods=$Prods.$ProdsArr[$i]["Code"]."~$#";
				$ProdsNm=$ProdsNm.$ProdsArr[$i]["Name"]."~$#";
			}
				$edtmm="";
			    $srtmm="";
				$PSlds=$ProdsArr[$i]["Slides"];
			//	if(count($PSlds)>0){
					for ($j = 0; $j < count($PSlds); $j++)
					{ 
						$SlideNm=$PSlds[$j]["Slide"];
						$PSldsTM=$PSlds[$j]["Times"];
						
						for ($k = 0; $k < count($PSldsTM); $k++)
						{
							if($srtmm==''||$srtmm==""){
								$srtmm=$PSldsTM[$k]["sTm"];
							}
							
							$st_time  =   strtotime($PSldsTM[$k]["eTm"]);
                            
						    if($edtmm==''||$edtmm==""){
							 $edtmm=$PSldsTM[$k]["eTm"];
							}
						    else{
								if($st_time>strtotime($edtmm)){
								$edtmm=$PSldsTM[$k]["eTm"];
								}
						     }
							$query="insert into tbDgDetailing_SlideDetails(DDSl_No,SlideName,StartTime,EndTime,Rating,Feedbk,usrLike) select '".$DDSl."','".$SlideNm."','".$PSldsTM[$k]["sTm"]."','".$PSldsTM[$k]["eTm"]."','".$PSlds[$j]["SlideRating"]."','".$PSlds[$j]["SlideRem"]."','".$PSlds[$j]["usrLike"]."'";
							performQuery($query);
						}

						$Scribs=$PSlds[$j]["Scribbles"];
						for ($k = 0; $k < count($Scribs); $k++)
						{
								$query="insert into tbDgDetScribFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,ScribImg,SlideNm,SlideSLNo) select '".$ARCd."','".$ARDCd."','".$sfCode."','".$DCRDt."','".$CustCode."','Scribbles/" . $_FILES["ScribbleImg"]["name"]."','".$SlideNm."','".$DDSl."'";
							performQuery($query);
						}
					}
				//}
				if(($srtmm!="")&&($edtmm!="")){
					$query="update tbDigitalDetailing_Head set StartTime='".$srtmm."',EndTime='".$edtmm."' where DDSl_No='".$DDSl."'";
					performQuery($query);
				}
            
        }
		
		// $query="update tbDigitalDetailing_Head set StartTime=St,EndTime=Et from (select D.DDSl_No DLNo,min(D.StartTime) St,Max(d.EndTime) Et from tbDgDetailing_SlideDetails d inner join tbDigitalDetailing_Head h on d.DDSl_No=h.DDSl_No where Activity_Report_code='".$ARCd."' group by D.DDSl_No) t where DDSl_No=DLNo";
		// performQuery($query);
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

function getSlideBrand(){
	global $data;
	$sfcode=(string) $data['SF'];
	$query = "exec ios_getSlideBrandPriority '".$sfcode."'";
	return performQuery($query);
}
function getSlideProduct(){
	global $data;
	$sfcode=(string) $data['SF'];
	$query = "exec ios_getSlideProductPriority '".$sfcode."'";
	return performQuery($query);
}
function getSlideSpeciality(){
	global $data;
	$sfcode=(string) $data['SF'];
	$query = "exec ios_getSlideSpecialityPriority '".$sfcode."'";
	return performQuery($query);
}

function SvMissDCREntry(){
    global $data,$conn,$NeedRollBack;
    if ( sqlsrv_begin_transaction( $conn ) === false ) {
     	die( print_r( sqlsrv_errors(), true ));
    }
    $NeedRollBack=false;
    $sfCode = (string) $data['SF'];
    $VstTime = $data['EDt'];
    $DCRDt = date('Y-m-d 00:00:00.000', strtotime($VstTime));
    $DivCode = (string) $data['Div'];
    $DivCode = str_replace(",", "", $DivCode);
	  $CallsData=$data['EData'];
	  
	  $data_dcr = json_encode($_POST['data'],true);
     $data_dcr1 = str_replace("'", "",$data_dcr);
     $sql="insert into tracking_dcr select '$sfCode','$DivCode','$data_dcr1',getdate(),'$DCRDt','MEdet'";
     performQuery($sql);
	
	
    for ($dloop = 0; $dloop < count($CallsData); $dloop++) 
    {
		    $idata=$CallsData[$dloop];
		    $ARCd = "";
		    $ARDCd = ""; /*(strlen($_GET['amc']) == 0) ? "0" : $_GET['amc'];*/
			$DCSUPOB = (string) $idata['DCSUPOB'];
			$Drcallfeedbackcode = (string) $idata['Drcallfeedbackcode'];
			
			if($DCSUPOB=="")
			{
				$DCSUPOB=0;
			}
			if($Drcallfeedbackcode=="")
			{
				$Drcallfeedbackcode=0;
			}
			if($idata['sample_validation']!=""){
				$sample_validation = $idata['sample_validation'];
			}
			else{
				$sample_validation='0';
			}
			if($idata['input_validation']!=""){
				$input_validation = $idata['input_validation'];
			}
			else{
				$input_validation = '0';
			}
			$work=$idata['WT'];
			if($work!=0)
			{
		    if($idata['CusType']=="1"){  
			    $query="Select isnull(No_of_Visit,0) No_of_Visit  from Mas_ListedDr where ListedDrCode='".$idata['CustCode']."'";
    		  $ExisArr = performQuery($query);
			    $MxCnt=$ExisArr[0]["No_of_Visit"];
          
			    if($MxCnt>0){
            
  				    $query="select Count(CustCode) Cnt from tbVisit_Details where SF_Code='".$sfCode."' and CustCode='".$idata['CustCode']."' and  year(Vst_Date)=year('".$DCRDt."')";
    			    $ExisArr = performQuery($query);
            
				    if ($ExisArr[0]["Cnt"]>=$MxCnt){
					    $result["Msg"]=$idata['CustName'] . "Already visited ".$MxCnt." times...";
					    $result["success"]=false;
					    return $result;
				    }
			    }
		    }
        
		    $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$sfCode."' and Activity_Date='".$DCRDt."' and FWFlg='L'";
		    $ExisArr = performQuery($query);
		    if ($ExisArr[0]["Cnt"]>0){
			    $result["Msg"]="Today Already Leave Posted...";
			    $result["success"]=false;
			    return $result;
		    }
			
				


		    $query="exec svDCRMain_App '".$sfCode."','".$DCRDt."','".$idata['WT']."','".$idata['Pl']."','".$DivCode."','".$idata['Rem']."','','iOS'";
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
		    //$query="exec iOS_UpdDCRFWType '".$sfCode."','".$DCRDt."','".$idata['WT']."'";
		    //performQuery($query);

		    $CustCode=$idata['CustCode'];
		    $CustName=$idata['CustName'];
		    $JWWrkArr=$idata['JWWrk'];
		    $JWWrk="";
		    $JWWrkNm="";
		    for ($i = 0; $i < count($JWWrkArr); $i++) 
		    {
			    $JWWrk=$JWWrk.$JWWrkArr[$i]["Code"]."$$";
			    $JWWrkNm=$JWWrkNm.$JWWrkArr[$i]["Name"]."$$";
		    }


		    $ProdsArr=$idata['Products'];
		    $Prods="";
		    $ProdsNm="";
		    for ($i = 0; $i < count($ProdsArr); $i++)
		    {
			     $stPC=$ProdsArr[$i]["Code"];
			    $stPN=$ProdsArr[$i]["Name"];
				if($ProdsArr[$i]["SmpQty"]=="") $ProdsArr[$i]["SmpQty"]="0";
				
			    $Prods=$Prods.$stPC."~".$ProdsArr[$i]["SmpQty"];
			    $ProdsNm=$ProdsNm.$stPN."~".$ProdsArr[$i]["SmpQty"];
				
				if($ProdsArr[$i]["RxQty"]=="") $ProdsArr[$i]["RxQty"]="0";
			   
				$Prods=$Prods."$".$ProdsArr[$i]["RxQty"]."$0^0$";
				$ProdsNm=$ProdsNm."$".$ProdsArr[$i]["RxQty"]."$0^0$";
			   
			    $Prods=$Prods."#";
			    $ProdsNm=$ProdsNm."#";
		    }

		    $InpArr=$idata['Inputs'];
		    $Inps="";
		    $InpsNm="";
		    $GC="";
		    $GN="";
		    $GQ=0;
		    for ($i = 0; $i < count($InpArr); $i++) 
		    {
			   if($idata['CusType']=="1"|| $idata['CusType']=="4"){
				   if ($i==0){
						  $GC=$InpArr[$i]["Code"];
						  $GN=$InpArr[$i]["Name"];
						  $GQ=$InpArr[$i]["IQty"];
			
					}else{
						  $Inps=$Inps.$InpArr[$i]["Code"]."~".$InpArr[$i]["IQty"]."#";
						  $InpsNm=$InpsNm.$InpArr[$i]["Name"]."~".$InpArr[$i]["IQty"]."#";
				   }
			   }
				else{
				      $Inps=$Inps.$InpArr[$i]["Code"]."~".$InpArr[$i]["IQty"]."#";
				      $InpsNm=$InpsNm.$InpArr[$i]["Name"]."~".$InpArr[$i]["IQty"]."#";
			    }
		    }
	
		    $sLoc=$idata['Entry_location'];
		    $loc = explode(":", str_replace("'", "", $sLoc) . ":");
		    $lat = $loc[0]; //latitude
		    $lng = $loc[1]; //longitude
		    if($lat =="(null)"){$lat ="";$lng ="";}
		    if($idata['CusType']=="1"){  
			    $query="exec svDCRLstDet_App '".$ARCd."',0,'".$sfCode."',1,'".$CustCode."','".$CustName."','".$VstTime."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','','','".$GC."','".$GN."','".$GQ."','".$Inps."','".$InpsNm."','','".$idata['Remks']."','".$DivCode."','".$Drcallfeedbackcode."','".$VstTime."','".$lat."','".$lng."','".$idata['DataSF']."','NA','iOS','".$idata['CallType']."','','','".$sample_validation."','".$input_validation."'";
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
		    if($idata['CusType']=="2" || $idata['CusType']=="3" ){
    		    $query="exec svDCRCSHDet_App '".$ARCd."',0,'".$sfCode."','".$idata['CusType']."','".$CustCode."','".$CustName."','".$VstTime."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','".$Inps."','".$InpsNm."','','".$idata['Remks']."','".$DivCode."','".$Drcallfeedbackcode."','".$VstTime."','".$lat."','".$lng."','".$idata['DataSF']."','NA','iOS','".$sample_validation."','".$input_validation."'";
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
		    if($idata['CusType']=="4"){
    		    $query="exec svDCRUnlstDet_App '".$ARCd."',0,'".$sfCode."','".$idata['CusType']."','".$CustCode."','".$CustName."','".$VstTime."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','','','".$GC."','".$GN."','".$GQ."','".$Inps."','".$InpsNm."','','".$idata['Remks']."','".$DivCode."','".$Drcallfeedbackcode."','".$VstTime."','".$lat."','".$lng."','".$idata['DataSF']."','NA','iOS','".$sample_validation."','".$input_validation."'";
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
			// for($Adil=-1;$Adil<count($idata["AdCuss"]);$Adil++)
    // {
        // if($Adil>-1){ 
            // $Cus=$idata["AdCuss"][$Adil];
            // $CustCode=$Cus["Code"];
            // $CustName=$Cus["Name"];
        
            // $query="exec svDCRLstDet_App '".$ARCd."',0,'".$sfCode."',1,'".$CustCode."','".$CustName."','".$VstTime."','".$DCSUPOB."','".$JWWrk."','".$Prods."','".$ProdsNm."','','','','','','','','','".$idata['Remks']."','".$DivCode."','".$Drcallfeedbackcode."','".$ModTime."','".$lat."','".$lng."','".$idata['DataSF']."','NA','iOS',''";
            // //$params = array(array($ARDCd, SQLSRV_PARAM_INOUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
            // $result["DQry"]=$query;
    	      // performQuery($query);//performQueryWP($query, $params);
            // if($ARDCd==""){
                // $query="select Trans_Detail_Slno from vwActivity_MSL_Details where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code='".$CustCode."'";
                // $arr = performQuery($query);
                // $ARDCd=$arr[0]["Trans_Detail_Slno"];
            // }
            
        // }
        // for ($i = 0; $i < count($ProdsArr); $i++)
        // {
			// $TmLn=$ProdsArr[$i]["Timesline"];
			// $query="select isnull(Max(DDSl_No),0)+1 DDSl_No from tbDigitalDetailing_Head";
			// $arr = performQuery($query);
			// $DDSl = $arr[0]["DDSl_No"]; 
			// $query="insert into tbDigitalDetailing_Head(DDSl_No,Activity_Report_code,MSL_code,Product_Code,Product_Name,GroupID,Rating,StartTime,EndTime,Feedbk_Status) select ".$DDSl.",'".$ARCd."','".$CustCode."','".$ProdsArr[$i]["Code"]."','".$ProdsArr[$i]["Name"]."','".$ProdsArr[$i]["Group"]."','".$ProdsArr[$i]["Rating"]."','".$TmLn["sTm"]."','".$TmLn["eTm"]."','".$ProdsArr[$i]["ProdFeedbk"]."'";
			// performQuery($query);
			// $Prods="";
			// $ProdsNm="";
			// if($ProdsArr[$i]["Group"]=="1"){
				// $Prods=$Prods.$ProdsArr[$i]["Code"]."~$#";
				// $ProdsNm=$ProdsNm.$ProdsArr[$i]["Name"]."~$#";
			// }
			// $PSlds=$ProdsArr[$i]["Slides"];
	       	// for ($j = 0; $j < count($PSlds); $j++)
			// { 
				// $SlideNm=$PSlds[$j]["Slide"];
				// $PSldsTM=$PSlds[$j]["Times"];
				// for ($k = 0; $k < count($PSldsTM); $k++)
				// {
                   	// $query="insert into tbDgDetailing_SlideDetails(DDSl_No,SlideName,StartTime,EndTime,Rating,Feedbk,usrLike) select ".$DDSl.",'".$SlideNm."','".$PSldsTM[$k]["sTm"]."','".$PSldsTM[$k]["eTm"]."','".$PSlds[$j]["SlideRating"]."','".$PSlds[$j]["SlideRem"]."','".$PSlds[$j]["usrLike"]."'";
	                // performQuery($query);
				// }

               	// $Scribs=$PSlds[$j]["Scribbles"];
				// for ($k = 0; $k < count($Scribs); $k++)
    			// {
             			// $query="insert into tbDgDetScribFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,ScribImg,SlideNm,SlideSLNo) select '".$ARCd."','".$ARDCd."','".$sfCode."','".$DCRDt."','".$CustCode."','Scribbles/" . $_FILES["ScribbleImg"]["name"]."','".$SlideNm."','".$DDSl."'";
	     			// performQuery($query);
				// }
			// }
            
        // }
		
		// $query="update tbDigitalDetailing_Head set StartTime=St,EndTime=Et from (select D.DDSl_No DLNo,min(D.StartTime) St,Max(d.EndTime) Et from tbDgDetailing_SlideDetails d inner join tbDigitalDetailing_Head h on d.DDSl_No=h.DDSl_No where Activity_Report_code='".$ARCd."' group by D.DDSl_No) t where DDSl_No=DLNo";
		// performQuery($query);
		// if($ARDCd!="0" && count($_FILES["SignImg"])>0){
			// $query="insert into tbDgDetailingFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,SignImg,FeedbkAudio) select '".$ARCd."','".$ARDCd."','".$sfCode."','".$DCRDt."','".$CustCode."','signs/" . $_FILES["SignImg"]["name"]."',''";
			// $result["ImgQry"]=$query;
			// performQuery($query);
        // }
    // }
	  }
	  else{
		$ARCd="jkl";
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

function saveQuiz(){
	global $data;
	$sfCode = (string) $data['SF'];
	$div = (string) $data['div'];
	$qid = (string) $data['qid'];
	$sid = (string) $data['sid'];
	$quizresults =  $data['result'];
	$StartTime=(string) $data['start'];
	$EndTime=(string) $data['end'];	
	$attempt=(string) $data['attempt'];	
 	
				$result['second22']=count($quizresults);
				
			for($i=0;$i<count($quizresults);$i++){
				$quesid=$quizresults[$i]['Question_Id'];
				$inputid=$quizresults[$i]['input_id'];
				$sql="exec svQuizResultNew '$sfCode','$div','$quesid','$inputid','$sid','$StartTime','$EndTime','$attempt'"; 
				$result['squry'] = $sql;
				performQuery($sql);
			}
			
				$result['success'] = true;
	             return $result;
			
}

function getQuiz(){
	global $data;

   $div = (string) $data['div'];
   $sf = (string) $data['SF'];
   $sfcode = (string) $data['SF'];
   $query = "select survey_id,quiz_title,substring(filepath,charindex(')',filepath)+1,len(filepath)) FileName from QuizTitleCreation where division_code='" . $div . "' and active=0  and month(effective_date)=Month(GETDATE()) and year(effective_date)=Year(GETDATE()) and cast(effective_date as date)>=cast(GETDATE() as date)  order by survey_id desc";
   $results['fquery']=$query;
   
   $quiztitle = performQuery($query);
				$surveyid=$quiztitle[0]['survey_id'];
				
if($quiztitle[0]['FileName']!=""){
$extn=end(explode('.', $quiztitle[0]['FileName']));

if ($extn == "png" || $extn == "jpg")
             $quiztitle[0]['mimetype']= "image/png";
 else if($extn=="doc"||$extn=="dot")
			  $quiztitle[0]['mimetype']= "application/msword";
 else if($extn=="docx"|| $extn == "DOCX")
			  $quiztitle[0]['mimetype']="application/msword";
 else if ($extn == "xls"||$extn == "xlt"||$extn == "xla")
             $quiztitle[0]['mimetype']= "application/vnd.ms-excel";
		  else if ($extn == "xlsx")
             $quiztitle[0]['mimetype']= "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
         else if ($extn == "mp4")
             $quiztitle[0]['mimetype']= "video/mp4";
else if($extn == "pptx")
  $quiztitle[0]['mimetype']= "application/vnd.openxmlformats-officedocument.presentationml.presentation";
         else
             $quiztitle[0]['mimetype']= "application/" + $extn;

}

		 $query = "select NoOfAttempts type,Type NoOfAttempts,timelimit from Processing_UserList where surveyid='" . $surveyid . "' and sf_code='" . $sfcode . "' and Process_Status ='P' and cast(from_date as date)<=cast(GETDATE() as date) and cast(to_date as date)>=cast(GETDATE() as date)";
 $processUser = performQuery($query);
 $results['squery']=$query;
if(count($processUser)==0){
	  $query = "select Process_Status from Processing_UserList where surveyid='" . $surveyid . "' and sf_code='" . $sfcode . "'";
	 $quizstaus=performQuery($query);
     if($quizstaus[0]['Process_Status']=="F")
	 {
	 $results['success']=false;
     $results['msg']="Quiz Attempt exceed !!";
	 }else{
		$results['success']=false;
    $results['msg']="Quiz Not Processed!!";
	}
}
else{
                $query = "select Question_Type_id,Question_Id,Question_Text,surveyid from AddQuestions where surveyid='" . $surveyid . "' order by question_id asc";
                $questions = performQuery($query);
                $query = "select input_id,Question_Id,Input_Text,Correct_Ans from AddInputOptions where question_id in (select question_id from AddQuestions where surveyid='" . $surveyid . "') order by question_id asc";
                $answers = performQuery($query);
				$results=array();
				$results['quiztitle'][0]=$quiztitle[0];
				$results['processUser']=$processUser;
				$results['questions']=$questions;
				$results['answers']=$answers;
				$results['success']=true;
}
/*if(count($processUser)==0){
	$results="";
	$results['success']=false;
//$results=array();
}*/


return $results;
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
	case "get/hqmgr":
        outputJSON(getSubordinateMgr());
        break;			
    case "get/worktype":
        outputJSON(getWorkTypes());
        break;
    case "get/compdet":
        outputJSON(getCompetitorDet());
        break;
	case "get/mapcompdet":
        outputJSON(getCompetitorDetMap());
        break;	
	case "get/productfb";
		outputJSON(getProductFeedback());
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
	
	case "get/slidebrand";
        outputJSON(getSlideBrand());
        break;
	case "get/slideproduct";
        outputJSON(getSlideProduct());
        break;
	case "get/slidespeciality";
        outputJSON(getSlideSpeciality());
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
	
	case "get/todaytpnew";
        outputJSON(getTodayTPNew());
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
	case "get/editdaycalls";
        outputJSON(getEtDyCalls());
        break;	
	case "get/editdates";
        outputJSON(getEtDates());
        break;	
    case "save/editdatestatus";
        outputJSON(svEtDates());
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
	case "get/leavetype":       
		outputJSON(getLeaveType());       
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
	case "get/tpdetails";
	      outputJSON(getTourPlaniPad());
	      break;	 	  
    case "get/drquery";
        outputJSON(getDrQuery());
        break;
	case "get/tracksetup";
	      outputJSON(getTrackSetUP());
	      break;
    case "get/notification";
	      outputJSON(getNotification());
	      break;
    case "get/conversation";
	      outputJSON(getConversation());
	      break;
		  case "get/dynactivity";
	      outputJSON(getDynamicActivity());
	      break;
		   case "get/dynview";
	      outputJSON(getDynamicView());
	      break;
		  case "get/dynviewtest";
	      outputJSON(getDynamicViewTest());
	      break;
		  
    case "get/ratinginf";
	      outputJSON(getRatingInfos());
        break;
    case "get/drdets";
	      outputJSON(getDrDetails());
	      break;
    case "get/missdates";
	      outputJSON(getMissingDates());
	      break;
	case "get/tpsetup":       
		outputJSON(getTpSetup());       
		break;
		
	case "get/quiz";
        outputJSON(getQuiz());
        break;
	
    case "save/quiz";
        outputJSON(saveQuiz());
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
   case "save/tpdaynew";
        outputJSON(SvTourPlanNew(0));
        break;
    case "save/tourplannew";
        outputJSON(SvTourPlanNew(1));
        break;
    case "save/policy";
        outputJSON(SvDrPolicy());
        break;
    case "save/converstion";
        outputJSON(svConversation());
        break;
    case "save/newdr";
        outputJSON(SvNewDr());
        break;
    case "save/drprofile";
        outputJSON(SvDrProfile());
        break;
    case "save/mytp";
        outputJSON(SvMyTodayTP());
        break;
	
	case "save/mytpnew";
        outputJSON(SvMyTodayTPNew());
        break;		
    case "save/call";
        outputJSON(SvDCREntry());
        break;
		case "del/todaycall";
        outputJSON(deleteCallEntry());
        break;
	case "save/newchm";
        outputJSON(SvNewCHM());
        break;


    case "save/track_1";
        outputJSON(SvLocTrack());
        break;
	case "save/trackdetail_1";
        outputJSON(svTrackDetail());
        break;
    case "save/drquery";
        outputJSON(SvDrQuery());
        break;
    case "delete/call";
        outputJSON(DeleteCalls());
        break;
    case "save/missentry";
        outputJSON(SvMissDCREntry());
        break;

    case "upload/scribble";
	      move_uploaded_file($_FILES["ScribbleImg"]["tmp_name"], "Scribbles/" . $_FILES["ScribbleImg"]["name"]);
        break;
case "get/calldets";
	outputJSON(getCallDetails());
break;
case "save/tpapprovalnew"; 
        outputJSON(SvTPApprovalNew());
        break;
case "get/dayrpt":
        outputJSON(getDayReport());
        break;
		case "get/vwvstdet":
        outputJSON(getVstDets());
        break;
		case "get/mnthsumm":
        outputJSON(getMonthSummary());
        break;
		case "get/missedrpt":
        outputJSON(getMissedReport());
        break;
		case "get/missedrptview":
        outputJSON(getMissedReportDetail());
        break;
		case "get/visitmonitor":
        outputJSON(getVisitCover());
        break;
		case "upload/sign":
		outputJSON(UpdateSign());
		break;
case "editdata/call";
 global $data;
    
    $detno = (string) $data['detno'];
    $sf=(string) $data['sf_code'];
	if($sf=="" || $sf==null)  $sf=(string) $data['APPUserSF'];
	$cusname=(string) $data['cusname'];
	$custype=(string) $data['custype'];
	$pob=(string) $data['pob'];


$giftCode;
$giftname;
$work_code;
$work_name;
$cusCodes;
$sqll;
if($custype=='1'){
$sqll="select * from vwActivity_MSL_Details where Trans_Detail_SlNo='".$detno."'";
}
else if($custype=='2'){
$sqll="select  * from vwActivity_CSH_Detail where Trans_Detail_Slno='".$detno."'";
}
else if($custype=='4'){
$sqll="select Time vstTime,* from vwActivity_Unlst_Detail where Trans_Detail_Slno='".$detno."'";
}
else{
$sqll="select  * from vwActivity_CSH_Detail where Trans_Detail_Slno='".$detno."'";
}

$a=performQuery($sqll);
$arcode=$a[0]["Trans_SlNo"];

$sql="select * from vwActivity_Report where Sf_Code='".$sf."' and Trans_SlNo='".$arcode."'";

$a=performQuery($sql);

if (count($a)>0) {
$response;
$response["Products"]=array();
$response["Inputs"]=array();
$response["JWWrk"]=array();
$response["AdCuss"]=array();
$response["RCPAEntry"]=array();

$response["SFName"]=$a[0]["Sf_Name"];

$response["SF"]=$a[0]["Sf_Code"];
$response["ADetSLNo"]=$detno;
$response["DataSF"]=$a[0]["Sf_Code"];
$response["ModTime"]="";
$response["WT"]=$a[0]["Work_Type"];
$response["WTNm"]=$a[0]["WorkType_Name"];
$response["mode"]=$a[0]["Entry_Mode"];
$response["CustName"]=$cusname;
$response["CusType"]=$custype;
$response["SFName"]=$a[0]["Sf_Name"];
$response["Entry_location"]="";
$response["CateCode"]="";
$response["Sf_Code"]=$a[0]["Sf_Code"];
$response["mappedProds"]="";
$response["AppUserSF"]="";


$response["CustCode"]="";
$response["SpecCode"]="";

$sq;
if($custype=='1'){
$sq="select Time vstTime,cast(Gift_Code as varchar)  +'~'+cast(Gift_Qty as varchar) +'#'+Additional_Gift_Code as addGiftcode,Gift_Name+'~'+cast(Gift_Qty as varchar)+'#'+Additional_Gift_Dtl as addGiftname, * from vwActivity_MSL_Details where Trans_Slno='".$a[0]["Trans_SlNo"]."' and Trans_Detail_Slno='".$detno."'";
}
else if($custype=='2'){
$sq="select Additional_Prod_Code as Product_Code,Additional_Prod_Dtls as Product_Detail,  * from vwActivity_CSH_Detail where Trans_Detail_Slno='".$detno."'";
}
else if($custype=='4'){
$sq="select  Time vstTime,cast(Gift_Code as varchar)  +'~'+cast(Gift_Qty as varchar) +'#'+Additional_Gift_Code as addGiftcode,Gift_Name+'~'+cast(Gift_Qty as varchar)+'#'+Additional_Gift_Dtl as addGiftname,* from vwActivity_Unlst_Detail where Trans_Detail_Slno='".$detno."'";
}
else{
$sq="select Additional_Prod_Code as Product_Code,Additional_Prod_Dtls as Product_Detail,  * from vwActivity_CSH_Detail where Trans_Detail_Slno='".$detno."'";
}
$a1=performQuery($sq);

$img="select * from tbDgDetailingFilesDetail where ARM_code='".$detno."'";
$per=performQuery($img);

$response["sign_path"]=$per[0]["SignImg"];

if($custype=='4')
	$response["vstTime"]=$a1[0]["vstTime"];
else
$response["vstTime"]=$a1[0]["vstTime"]->format('Y-m-d H:i:s');
$response["DivCode"]=$a1[0]["Division_Code"];
$response["Div"]=$a1[0]["Division_Code"];
$response["Entry_location"]=$a1[0]["lati"].":".$a1[0]["long"];
$response["CustCode"]=$a1[0]["Trans_Detail_Info_Code"];
$response["Remks"]=$a1[0]["Activity_Remarks"];
$response["DCSUPOB"]=$a1[0]["POB"];
$response["Drcallfeedbackcode"]=$a1[0]["Rx"];
$arr=array();
$arrpob=array();
$val=$a1[0]["Product_Code"];
$def=explode("#",$val);
/*for($k=0;$k<sizeof($def)-1;$k++){
 $substring='~';
 $substri='$';
    $pos=stripos($def[$k], $substring);
    $poss=stripos($def[$k], $substri);
if($custype=='3' || $custype=='4')
		$arr[]=array(substr($def[$k],$pos+1));
	else
    $arr[]=array(substr($def[$k],$pos+1,$poss-($pos+1)));

if($pob=='1'){
if($custype=='1'|| $custype=='2'){
$var=(substr($def[$k],$poss+1));
    $ssub='$';
    $posss=stripos($var, $ssub);
$arrpob[]=array(substr($def[$k],$poss+1,$posss));
}
}

}*/

for($k=0;$k<sizeof($def)-1;$k++){
 $substring='~';
 $substri='$';
    $pos=stripos($def[$k], $substring);
    $poss=stripos($def[$k], $substri);
	//$response["Remks678"]=$def[$k];
//if($custype=='3' || $custype=='4')
		//$arr[]=array(substr($def[$k],$pos+1));
	//else
    $arr[]=array(substr($def[$k],$pos+1,$poss-($pos+1)));
$arrdd[]=array(substr($def[$k],$pos-$pos,$pos));

if($pob=='1'){
//if($custype=='1'|| $custype=='2'){
	$var=(substr($def[$k],$poss+1));
    $ssub='$';
    $posss=stripos($var, $ssub);
$arrpob[]=array(substr($def[$k],$poss+1,$posss));


//}
}
if (strpos($def[$k], '/') !== false) {
	$lastIndex = strripos($def[$k], $substri);
	$arrFb[]=array(substr($def[$k],$lastIndex+1)); 
}
else{
	$arrFb[]=array("");
}
}


/*if($pob=='1'){
if($custype=='1'|| $custype=='2'){
$defp=explode("#",$val);
for($k=1;$k<sizeof($defp);$k++){
if($custype=='1')
$substring='$';
else
$substring='~';

$pos=stripos($defp[$k], $substring);
$arrpob[]=array(substr($defp[$k],$pos+1,1));
}

}
}
*/

$vvv=$a1[0]["Product_Detail"];
$detail=explode('#',$vvv);
for($k=0;$k<sizeof($detail);$k++){
$substring='~';
$pos=stripos($detail[$k], $substring);
$detail[$k]=substr($detail[$k],0,$pos);
}


if($custype=='1' || $custype=='4'){
	
$giftCode=$a1[0]["addGiftcode"];
$giftname=$a1[0]["addGiftname"];



}
else{
$giftCode=$a1[0]["Additional_Gift_Code"];
$giftname=$a1[0]["Additional_Gift_Dtl"];
}

$work_code=$a1[0]["Worked_with_Code"];
$work_name=$a1[0]["Worked_with_Name"];

$def=explode("#",$giftCode);
$defnam=explode("#",$giftname);

for($k=0;$k<sizeof($def)-1;$k++) {

$substring='~';
$pos=stripos($def[$k], $substring);
$posnam=stripos($defnam[$k], $substring);
$ip=array();
$ip["Name"]=substr($defnam[$k],0,$posnam);
$ip["Code"]=substr($def[$k],0,$pos);
$ip["IQty"]=substr($def[$k],$pos+1);

if(substr($defnam[$k],0,1)!='~')
array_push($response["Inputs"],$ip);
   
}

$head="select * from tbDigitalDetailing_Head where Activity_Report_code='".$a1[0]["Trans_SlNo"]."' and MSL_code='".$a1[0]["Trans_Detail_Info_Code"]."'";
$a3=performQuery($head);

if (count($a3)>0) {
for($i=0;$i<count($a3);$i++){
$product=array();
$product["Name"]=$a3[$i]["Product_Name"];
$position=array_search($a3[$i]["Product_Name"], $detail);
$product["Rating"]=$a3[$i]["Rating"];
$product["Type"]="";
$product["ProdFeedbk"]=$a3[$i]["Feedbk_Status"];
$cond_flag=false;
$sqvalue=0;
for($q=0;$q<count($arrdd);$q++){
	if($a3[$i]["Product_Code"]==$arrdd[$q][0]){
		$cond_flag=true;
		$sqvalue=$q;
	}
}

	if($cond_flag==true){
		$product["SmpQty"]=(string)$arr[$sqvalue][0];
		$product["rx_pob"]=(string)$arrpob[$sqvalue][0];
	}
	else{
		$product["SmpQty"]="0";
		$product["rx_pob"]="0";
	}

// $product["SmpQty"]=(string)$arr[$i][0];

// if($pob=='1')
	// //if($custype == '1' || $custype == '2')

// $product["rx_pob"]=(string)$arrpob[$i][0];
$product["prdfeed"]=(string)$arrFb[$i][0];

$product["Code"]=$a3[$i]["Product_Code"];
$product["Group"]=$a3[$i]["GroupID"];
if($product["Group"]=="1"){$product["Type"]="D";}
$product["Slides"]=array();
$prd1=$a3[$i]['StartTime']->format('Y-m-d H:i:s');
$prd2=$a3[$i]['EndTime']->format('Y-m-d H:i:s');

$product["Timesline"]=(object) array('sTm' => $prd1,'eTm' =>$prd2);


//if($custype=='1'){

$slid_data="select case  when CHARINDEX('.jpg',SlideName)>0 or CHARINDEX('.png',SlideName)>0 then 'I' when CHARINDEX('.zip',SlideName)>0  then 'H' when CHARINDEX('.pdf',SlideName)>0 then 'P' else 'V' end SlideType,* from tbDgDetailing_SlideDetails where DDSl_No='".$a3[$i]["DDSl_No"]."'";

$produc["Slides"]=performQuery($slid_data);

for($l=0;$l<count($produc["Slides"]);$l++){
$pfdd=$produc["Slides"][$l]["SlideName"];
$slid=array();
$slid["Slide"]=$pfdd;
$slid["SlideType"]=$produc["Slides"][$l]["SlideType"];
$slid["SlideRem"]=$produc["Slides"][$l]["Feedbk"];
$slid["SlidePath"]="";
$slid["SlideRating"]=$produc["Slides"][$l]["Rating"];
$prd11=$produc["Slides"][$l]['StartTime']->format('Y-m-d H:i:s');
$prd22=$produc["Slides"][$l]['EndTime']->format('Y-m-d H:i:s');
$slid["Times"]=array();
$slidTim=array();
$slidTim["sTm"]=$prd11;
$slidTim["eTm"]=$prd22;
array_push($slid["Times"],$slidTim);
array_push($product["Slides"],$slid);
}
//}

array_push($response["Products"],$product);


}

}

$deff=explode("$$",$work_code);
$deffname=explode(",",$work_name);

for($k=0;$k<sizeof($deff)-1;$k++) {
$jw=array();
$jw["Name"]=$deffname[$k];
$jw["Code"]=$deff[$k];
$jw["IQty"]="";
array_push($response["JWWrk"],$jw);
}

if($custype=='1'){
$rcphead="select * from Trans_RCPA_Head where Sf_Code='".$sf."' and DrCode='".$a1[0]["Trans_Detail_Info_Code"]."' and cast(convert(varchar,RCPA_Date,101) as datetime) =cast(convert(varchar,GetDate(),101) as datetime)";
$b1=performQuery($rcphead);


for($k=0;$k<count($b1);$k++) {
$jw=array();
$jw["OPName"]=$b1[$k]["OPName"];
$jw["OPQty"]=$b1[$k]["OPQty"];
$jw["OPRate"]=$b1[$k]["OPRate"];
$jw["OPValue"]=$b1[$k]["OPValue"];
$jw["OPCode"]=$b1[$k]["OPCode"];
$jw["Chemists"]=array();

$chemycode=$b1[$k]["ChmCode"];
$chemyname=$b1[$k]["ChmName"];
$defchem=explode(",",$chemycode);
$defchemnm=explode(",",$chemyname);

for($m=0;$m<sizeof($defchem)-1;$m++) {
$jw1=array();
$jw1["Name"]=$defchemnm[$m];
$jw1["Code"]=$defchem[$m];

array_push($jw["Chemists"],$jw1);
}

$rcpdet="select b.FK_PK_ID,b.CompCode,b.CompName,b.CompPCode,b.CompPName,b.CPQty,b.CPRate,b.CPValue,b.CPUnit,b.DCR_id,b.Dcrdetail_id,a.ChmName as Chemname,a.ChmCode as Chemcode from trans_rcpa_head a inner join trans_rcpa_detail b on a.PK_ID =b.fk_pk_id where sf_code='".$sf."' and pk_id='".$b1[$k]["PK_ID"]."'";
//$rcpdet="select * from Trans_RCPA_detail where FK_PK_ID=cast('" . $b1["PK_ID"] ."' as int)";
$b2=performQuery($rcpdet);
$jw["Competitors"]=array();
$jw["Competitors"]=$b2;

array_push($response["RCPAEntry"],$jw);
}
}

outputJSON($response);

}

break;
case "get/vwdcr";
	
	  global $data;
	  
	   $sfCode = (string) $data['sfCode'];
	   
		$sql = "select d.Plan_Name,d.Trans_SlNo,d.Sf_Code,d.FieldWork_Indicator,d.WorkType_Name,d.Sf_Name,convert(varchar,Activity_Date,103) Activity_Date,s.Reporting_To_SF from DCRMain_Temp d
        inner join Mas_Salesforce_One s on d.Sf_Code=s.Sf_Code where d.Confirmed=1 and s.Reporting_To_SF='$sfCode' and cast(Activity_Date as date)<cast(GETDATE() as date)";
        $dcr = performQuery($sql);
		
        outputJSON($dcr);
        break;	  
		
	 case "get/vwdcrone":
		global $data;
        $TransSlNo =(string) $data['Trans_SlNo'];
        $sql = "exec getDCRApprovalApp '" . $TransSlNo . "'";
        $dcr = performQuery($sql);
        outputJSON($dcr);

        break;	
	case "dcrapproval":
		global $data;
            $date = (string) $data['date'];
            $code = (string) $data['sfCode'];
            $date = str_replace('/', '-', $date);
            $date = date('Y-m-d', strtotime($date));
            $sql = "exec ApproveDCRByDt '" . $code . "','$date'";

            performQuery($sql);
            $resp["success"] = true;
			outputJSON($resp);
            // echo json_encode($resp);
            // die;
            break;
    case "dcrreject":
		global $data;
    
            $date = (string) $data['date'];
            $code = (string) $data['sfCode'];
			$div = (string) $data['div'];
			$reject_reason = (string) $data['reason'];
            $date = str_replace('/', '-', $date);
            $date = date('Y-m-d', strtotime($date));
            $sql = "exec App_DcrReject_edetail'" . $code . "','$date','$reject_reason','$div'";
            performQuery($sql);
            // $sql = "insert into DCR_MissedDates ()" ;
			// performQuery($sql);
          
            $resp["success"] = true;
			 outputJSON($resp);
            // echo json_encode($resp);
            // die;
            break;	
case "save/geotag";
outputJSON(SaveGeoTag());
break;
case "get/customsetup":       
		outputJSON(getCustomSetup());       
		break;
case "save/dcract";
outputJSON(svDCRActivity());
break;

	case "upload/files";
	      move_uploaded_file($_FILES["AFile"]["tmp_name"], "Activity/" . $_FILES["AFile"]["name"]);
        break;	

case "get/geotag";
outputJSON(ViewGeoTag());
break;

case "get/target_sales_primary":
        	$divC = $_GET['divisionCode'];
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
			$fromdate =$_GET['from_date'];
			
        	$Fmonth = date("n", strtotime($fromdate));
			$Fyear = date('Y', strtotime($fromdate));
			$todate =$_GET['to_date'];			
        	$Tmonth = date("n", strtotime($todate));      	
        	$Tyear = date('Y', strtotime($todate));
			
		$query = "exec Target_Sale_Primary_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyear."','".$Tyear."'";
		//echo $query;
		 

		outputJSON(performQuery($query));
		break;
		case "get/target_sales_secondary":
        	$divC = $_GET['divisionCode'];
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
			$fromdate =$_GET['from_date'];
			
        	$Fmonth = date("n", strtotime($fromdate));
			$Fyear = date('Y', strtotime($fromdate));
			$todate =$_GET['to_date'];			
        	$Tmonth = date("n", strtotime($todate));      	
        	$Tyear = date('Y', strtotime($todate));
			
		$query = "exec Target_Sale_Secondary_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyear."','".$Tyear."'";

		outputJSON(performQuery($query));
		break;
	case "get/product_sales_primary":
        	$divC = $_GET['divisionCode'];
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$fromdate =$_GET['from_date'];			
        	$Fmonth = date("n", strtotime($fromdate));
			$Fyear = date('Y', strtotime($fromdate));
			$todate =$_GET['to_date'];			
        	$Tmonth = date("n", strtotime($todate));      	
        	$Tyear = date('Y', strtotime($todate));
		$query = "exec Product_Sales_Primary_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyear."','".$Tyear."'";

//echo 	$query;
		outputJSON(performQuery($query));
		break;
		
		case "get/product_sales_secondary":
        	$divC = $_GET['divisionCode'];
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$fromdate =$_GET['from_date'];			
        	$Fmonth = date("n", strtotime($fromdate));
			$Fyear = date('Y', strtotime($fromdate));
			$todate =$_GET['to_date'];			
        	$Tmonth = date("n", strtotime($todate));      	
        	$Tyear = date('Y', strtotime($todate));
			
		$query = "exec Product_Sales_Secondary_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyear."','".$Tyear."'";
//echo $query;
		outputJSON(performQuery($query));
		break; 
		case "get/Primary_YTD_All":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$fromdate =$_GET['from_date'];			
        	$Fmonth = date("n", strtotime($fromdate));
			$Fyear = date('Y', strtotime($fromdate));
			$todate =$_GET['to_date'];			
        	$Tmonth = date("n", strtotime($todate));      	
        	$Tyear = date('Y', strtotime($todate));			
			$query = "exec Primary_YTD_All_Div_Dash_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyear."','".$Tyear."'";
			 
			outputJSON(performQuery($query));
		break; 
		case "get/Secondary_YTD_All":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$fromdate =$_GET['from_date'];			
        	$Fmonth = date("n", strtotime($fromdate));
			$Fyear = date('Y', strtotime($fromdate));
			$todate =$_GET['to_date'];			
        	$Tmonth = date("n", strtotime($todate));      	
        	$Tyear = date('Y', strtotime($todate));			
			$query = "exec Secondary_YTD_All_Div_Dash_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyear."','".$Tyear."'";
			outputJSON(performQuery($query));
		break;
		
		
			case "get/category_sfe":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];					
			$query = "exec Effort_Analysis_Dashboard_CatSpecl '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."'";
			//echo $query;
			outputJSON(performQuery($query));
		break;
		case "get/speciality_sfe":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];					
        	$mode =$_GET['mode'];					
        	$code =$_GET['spec_code'];					
			$query = "exec Effort_Analysis_Dashboard_SpeclWise '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."','1','".$code."'";
			//echo $query;
			outputJSON(performQuery($query));
			break;
			
		case "get/primary_ss_dashboard":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 					
			//$query = "exec Effort_Analysis_Dashboard_SpeclWise '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."','1','".$code."'";
			$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//$query = "exec Primary_SS_Dashboard_APP '2,','mgr0088','8','8','2021','2021'";
			// echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;
			case "get/mvd_coverage":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear']; 
//MVD_Dashboard_App '2,','mgr0088','1','2022','02-01-2021'			
			$query = "exec MVD_Dashboard_App '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."',''";
			//echo $query;
			outputJSON(performQuery($query));
			break;
		case "get/primary_hq":
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 	
			//$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			$query = "exec Primary_HQwise_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			//echo $query;
			break;
		case "get/primary_hq_detail":
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 	
        	$HQC =$_GET['hqcode']; 	
        	$HQN =$_GET['hqname']; 	 
			//$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'"; 
			$query = "exec Primary_Brandwise_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."','".$HQC."','".$HQN."'";
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;
		case "get/primary_brand":
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear'];  
			//$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'"; 
			$query = "exec Primary_Sale_Brandwise_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;
		case "get/primary_fieldforce":
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear'];   
			//Target_Sale_Fieldforcewise_APP '2','admin','7','7','2021','2021'
			//$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'"; 
			$query = "exec Target_Sale_Fieldforcewise_APP  '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;
		case "get/primary_brand_product":
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear'];  
        	$Bprod =$_GET['Brand_product'];  
			//Primary_Sale_Brandwise_Product_App '2,','MR0238','40','11','11','2021','2021'
			//$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'"; 
			$query = "exec Primary_Sale_Brandwise_Product_App  '".$divC."','".$sfCode."','".$Bprod."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//echo$query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;
		case "get/primary_hq_brand_product":
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear'];  
        	$Bprod =$_GET['Brand_product'];  
        	$hqid =$_GET['hq_cat_id'];  
        	$hqname =$_GET['hq_name'];  
			//Primary_Sale_HQ_Brandwise_Product_App '2,','mgr0003','40','7','7','2021','2021','TP01' ,'CHENNAI'
			//$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'"; 
			$query = "exec Primary_Sale_HQ_Brandwise_Product_App  '".$divC."','".$sfCode."','".$Bprod."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."','".$hqid."','".$hqname."'";
			//echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;
		case "get/primary_fieldforce_brand":
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear'];   
			//Primary_Sale_FF_Brandwise_App '2,','MR0238','11','11','2021','2021'
			//$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'"; 
			$query = "exec Primary_Sale_FF_Brandwise_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			} 
			break;
		case "get/primary_fieldforce_brand_product":
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 
			$Bprod =$_GET['Brand_product'];  			
			//Primary_Sale_FF_Brandwise_Product_App '2,','mgr0003','40','7','7','2021','2021'
			//$query = "exec Primary_SS_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'"; 
			$query = "exec Primary_Sale_FF_Brandwise_Product_App '".$divC."','".$sfCode."','".$Bprod."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;	
		
		case "get/hierarchy":
        	//$divC = $_GET['divisionCode'];
			$divC = $_GET['divisionCode'];
        	$sfCode = $_GET['sfCode'];			
			$query = "select  division_code,Sf_Code,Sf_Name,sf_type from mas_salesforce where Reporting_To_SF='".$sfCode."' and division_code='".$divC."'";
			$res =  performQuery($query);
			if(count($res) ==0)
				$query = "select  division_code,Sf_Code,Sf_Name,sf_type from mas_salesforce where sf_code='".$sfCode."' and division_code='".$divC."' ";
			$res =  performQuery($query);
			outputJSON($res);
		break;
		
		
		case "getdivision_ho_sf":
		$sfCode = $_GET['sfCode'];
		$HOID = $_GET['Ho_Id'];
		$query = "exec getDivision '" . $HOID . "','" . $sfCode . "'";
        $results = performQuery($query);	
		outputJSON($results);
		break;
	case "getdivision_speciality":
		$sfCode = $_GET['sfCode'];
		$HOID = $_GET['Ho_Id'];
		// $query = "SELECT Doc_Special_Code id, (d.Doc_Special_Name+' ( '+d.Doc_Special_SName +' ) ' ) name,Division_Code FROM dbo.SplitString((select division_code from mas_ho_id_creation where ho_id='" . $HOID . "'), ',') a inner join Mas_Doctor_Speciality d on a.item=d.division_code where  Doc_Special_Active_Flag=0"; 
		$query = "exec getDivision_speciality '" . $HOID . "','" . $sfCode . "'";
		$results = performQuery($query);	
		outputJSON($results);
		break;
		
	case "get/stockbalance":
		global $data;
    
            $SF_Code = (string) $data['sfCode'];
			$DivisionCode = (string) $data['div'];
			$Response = array();
			$sql1 = "exec GetSampleStockDetail '".$SF_Code."', '".$DivisionCode."'";
			$result1 = performQuery( $sql1 );
			$sql2 = "exec GetInputStockDetail '".$SF_Code."', '".$DivisionCode."'";
			$result2 = performQuery( $sql2 );
			$Response[ 'Sample_Stock' ] = $result1;
			$Response[ 'Input_Stock' ] = $result2;
			outputJSON($Response);
	
		break;
		
	case "get/primary_sale_groupwise":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];					
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 					
			//$query = "exec Effort_Analysis_Dashboard_SpeclWise '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."','1','".$code."'";
			$query = "exec Primary_Sale_Groupwise_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//$query = "exec Primary_Sale_Groupwise_App '2,','mgr0088','8','8','2021','2021'";
			// echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;	
			
	    case "get/primary_sale_groupwise_product":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
			$grpCode = $_GET['grpCode'];
        	$Fmonth =$_GET['fmonth'];				
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 					
			//$query = "exec Effort_Analysis_Dashboard_SpeclWise '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."','1','".$code."'";
			$query = "exec Primary_Sale_Groupwise_Product_App '".$divC."','".$sfCode."','".$grpCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//$query = "exec Primary_Sale_Groupwise_App '2,','mgr0088','8','8','2021','2021'";
			// echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;	

		case "get/primary_sale_ff_groupwise":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
        	$Fmonth =$_GET['fmonth'];				
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 					
			//$query = "exec Effort_Analysis_Dashboard_SpeclWise '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."','1','".$code."'";
			$query = "exec Primary_Sale_FF_Groupwise_App '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//$query = "exec Primary_Sale_Groupwise_App '2,','mgr0088','8','8','2021','2021'";
			// echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;
			
		case "get/primary_sale_ff_groupwise_product":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
			$grpCode = $_GET['grpCode'];
        	$Fmonth =$_GET['fmonth'];				
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 					
			//$query = "exec Effort_Analysis_Dashboard_SpeclWise '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."','1','".$code."'";
			$query = "exec Primary_Sale_FF_Groupwise_Product_App '".$divC."','".$sfCode."','".$grpCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."'";
			//$query = "exec Primary_Sale_Groupwise_App '2,','mgr0088','8','8','2021','2021'";
			// echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;

		case "get/primary_groupwise_dashboard_hq":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
			$Fmonth =$_GET['fmonth'];				
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 	
			$hqid =$_GET['hq_cat_id'];  
        	$hqname =$_GET['hq_name'];			
			//$query = "exec Effort_Analysis_Dashboard_SpeclWise '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."','1','".$code."'";
			$query = "exec HQwise_Primary_Groupwise_Dashboard_APP '".$divC."','".$sfCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."','".$hqid."','".$hqname."'";
			//$query = "exec Primary_Sale_Groupwise_App '2,','mgr0088','8','8','2021','2021'";
			// echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;
			
		case "get/primary_groupwise_product_hq":
        	//$divC = $_GET['divisionCode'];
			$divC = str_replace(",,", ",", $_GET['divisionCode']);
        	$sfCode = $_GET['sfCode'];
        	$rptSF = $_GET['rSF'];
			$grpCode = $_GET['grpCode'];
			$Fmonth =$_GET['fmonth'];				
        	$Fyr =$_GET['fyear'];	
			$Tmonth =$_GET['tomonth'];					
        	$Tyr =$_GET['toyear']; 	
			$hqid =$_GET['hq_cat_id'];  
        	$hqname =$_GET['hq_name'];			
			//$query = "exec Effort_Analysis_Dashboard_SpeclWise '".$divC."','".$sfCode."','".$Fmonth."','".$Fyr."','1','".$code."'";
			$query = "exec HQwise_Primary_Sale_HQ_Groupwise_Product_App '".$divC."','".$sfCode."','".$grpCode."','".$Fmonth."','".$Tmonth."','".$Fyr."','".$Tyr."','".$hqid."','".$hqname."'";
			//$query = "exec Primary_Sale_Groupwise_App '2,','mgr0088','8','8','2021','2021'";
			// echo $query;
			$data = performQuery($query);
			$result = array();
			if (count($data) == 0) {
				$results[] = $data[0];
				outputJSON($result);
			}else{
				outputJSON($data);
			}
			break;		
		
}
?>
