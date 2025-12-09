<?php
function dcredit() {
	  	
 global $data;
    $detno = (string) $data['detno'];
    $sf=(string) $data['sfcode'];
	$cusname=(string) $data['cusname'];
	$custype=(string) $data['custype'];


$giftCode;
$giftname;
$work_code;
$work_name;
$cusCodes;
$sqll;
if($custype=='1'){
$sqll="select Trans_SlNo from vwActivity_MSL_Details where Trans_Detail_SlNo='".$detno."'";
}
else if($custype=='2'){
$sqll="select  * from vwActivity_CSH_Detail where Trans_Detail_Slno='".$detno."'";
}
else if($custype=='4'){
$sqll="select  * from vwActivity_Unlst_Detail where Trans_Detail_Slno='".$detno."'";
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
$response["vstTime"]="";


$response["CustCode"]="";
$response["SpecCode"]="";

$sq;
if($custype=='1'){
$sq="select cast(Gift_Code as varchar)  +'~'+cast(Gift_Qty as varchar) +'#'+Additional_Gift_Code as addGiftcode,Gift_Name+'~'+cast(Gift_Qty as varchar)+'#'+Additional_Gift_Dtl as addGiftname, * from vwActivity_MSL_Details where Trans_Slno='".$a[0]["Trans_SlNo"]."' and Trans_Detail_Slno='".$detno."'";
}
else if($custype=='2'){
$sq="select Additional_Prod_Code as Product_Code,Additional_Prod_Dtls as Product_Detail,  * from vwActivity_CSH_Detail where Trans_Detail_Slno='".$detno."'";
}
else if($custype=='4'){
$sq="select  * from vwActivity_Unlst_Detail where Trans_Detail_Slno='".$detno."'";
}
else{
$sq="select Additional_Prod_Code as Product_Code,Additional_Prod_Dtls as Product_Detail,  * from vwActivity_CSH_Detail where Trans_Detail_Slno='".$detno."'";
}
$a1=performQuery($sq);

$img="select * from tbDgDetailingFilesDetail where ARM_code='".$detno."'";
$per=performQuery($img);

$response["sign_path"]=$per[0]["SignImg"];


$response["DivCode"]=$a1[0]["Division_Code"];
$response["Div"]=$a1[0]["Division_Code"];
$response["Entry_location"]=$a1[0]["lati"]+":"+$a1[0]["long"];
$cusCodes=$a1[0]["Trans_Detail_Info_Code"];
$response["Remks"]=$a1[0]["Activity_Remarks"];
$response["DCSUPOB"]=$a1[0]["POB"];
$response["Drcallfeedbackcode"]=$a1[0]["Rx"];
$arr=array();
$val=$a1[0]["Product_Code"];
$def=explode("~",$val);
for($k=0;$k<sizeof($def)-1;$k++){
 $substring='~';
 $substri='$';
    $pos=stripos($def[$k], $substring);
    $poss=stripos($def[$k], $substri);
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

}


$vvv=$a1[0]["Product_Detail"];
$detail=explode('#',$vvv);
for($k=0;$k<sizeof($detail);$k++){
$substring='~';
$pos=stripos($detail[$k], $substring);
$detail[$k]=substr($detail[$k],0,$pos);
}


if($custype=='1'){
$giftCode=$a1[0]["addGiftcode"];
$giftname=$a1[0]["addGiftname"];

}
else{
$giftCode=$a1[0]["Additional_Gift_Code"];
$giftname=$a1[0]["Additional_Gift_Dtl"];
}

$work_code=$a1[0]["Worked_with_Code"];
$work_name=$a1[0]["Worked_with_Name"];

$head="select * from tbDigitalDetailing_Head where Activity_Report_code='".$a1[0]["Trans_SlNo"]."' and MSL_code='".$a1[0]["Trans_Detail_Info_Code"]."' order by len(DDSl_No),DDSl_No";
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
//$product["SmpQty"]=(string)$arr[$position][0];
$product["Code"]=$a3[$i]["Product_Code"];
$product["Group"]=$a3[$i]["GroupID"];
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


$def=explode("#",$giftCode);
$defnam=explode("#",$giftname);

for($k=0;$k<sizeof($def)-1;$k++) {

$substring='~';
$pos=stripos($def[$k], $substring);
$posnam=stripos($defnam[$k], $substring);
$ip=array();
$ip["Name"]=substr($defnam[$k], 0,$posnam);
$ip["Code"]=substr($def[$k], 0,$pos);
$ip["IQty"]=substr($def[$k],$pos+1);
if(substr($defnam[$k],0,1)!='~')
array_push($response["Inputs"],$ip);
   
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

if($custype=='1'||$custype=='2'){
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

$rcpdet="select b.FK_PK_ID,b.CompCode,b.CompName,b.CompPCode,b.CompPName,b.CPQty,b.CPRate,b.CPValue,b.CPRemarks,a.ChmName as Chemname,a.ChmCode as Chemcode from trans_rcpa_head a inner join trans_rcpa_detail b on a.PK_ID =b.fk_pk_id where sf_code='".$sf."' and pk_id='".$b1[$k]["PK_ID"]."'";
//$rcpdet="select * from Trans_RCPA_detail where FK_PK_ID=cast('" . $b1["PK_ID"] ."' as int)";
$b2=performQuery($rcpdet);
$jw["Competitors"]=array();
$jw["Competitors"]=$b2;

array_push($response["RCPAEntry"],$jw);
}
}

outputJSON($response);

}
else{
	$result = array();
	$result['success'] = false;
	$result['msg'] = 'No Data Found..';
	outputJSON($result);
}

}
?>


