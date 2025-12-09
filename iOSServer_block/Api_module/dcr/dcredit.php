<?php
function dcredit() {
	  	
global $data;
$sq="exec ios_Activity_Details_Edet '".$data['detno']."','".$data['custype']."'";
$a1=performQuery($sq);

$sq="exec ios_Activity_Report_Edet '".$data['headerno']."'";
$a=performQuery($sq);

if (count($a)>0) {
$response=array();
$response["DCRMain"]=$a;
$response["DCRDetail"]=$a1;
$img="select AR_code,ARM_code,SF_Code,ADate,Cust_code,SignImg,FeedbkAudio from tbDgDetailingFilesDetail where ARM_code='".$data['detno']."'";
$per=performQuery($img);
$response["sign_path"]=$per[0]["SignImg"];

$head="select DDSl_No,Activity_Report_code,MSL_code,Product_Code,Product_Name,GroupID,Rating,StartTime,EndTime,Feedbk_Status,SignImg,convert(varchar, StartTime, 120) stm,convert(varchar, EndTime, 120) etm from tbDigitalDetailing_Head where Activity_Report_code='".$a1[0]["Trans_SlNo"]."' and MSL_code='".$a1[0]["Trans_Detail_Info_Code"]."' order by len(DDSl_No),DDSl_No";
$a3=performQuery($head);
$response["DigitalHead"]=[];
if (count($a3)>0) {
for($i=0;$i<count($a3);$i++){
$slid_data="select case  when CHARINDEX('.jpg',SlideName)>0 or CHARINDEX('.png',SlideName)>0 then 'I' when CHARINDEX('.zip',SlideName)>0  then 'H' when CHARINDEX('.pdf',SlideName)>0 then 'P' else 'V' end SlideType,DDSl_No,SlideName,StartTime,EndTime,Rating,Feedbk,usrLike,DetSlNo,convert(varchar, StartTime, 120) stm,convert(varchar, EndTime, 120) etm  from tbDgDetailing_SlideDetails where DDSl_No='".$a3[$i]["DDSl_No"]."'";

$slides=performQuery($slid_data);

$a3[$i]['DigitalDet']=$slides;

}
$response["DigitalHead"]=$a3;
}

if($data['custype']=='1'||$data['custype']=='2'){
	
$rcphead="select PK_ID,SF_Code,SF_Name,RCPA_Date,DrCode,DrName,ChmCode,ChmName,OPCode,OPName,OPQty,OPRate,OPValue,AR_Code,ARMSL_Code,UpdatedOn,EID from Trans_RCPA_Head where Sf_Code='".$data['sfcode']."' and DrCode='".$a1[0]["Trans_Detail_Info_Code"]."' and cast(convert(varchar,RCPA_Date,101) as datetime) =cast(convert(varchar,GetDate(),101) as datetime)";
$b1=performQuery($rcphead);
//echo $rcphead;

for($k=0;$k<count($b1);$k++) {
	
$rcpdet="select b.FK_PK_ID,b.CompCode,b.CompName,b.CompPCode,b.CompPName,b.CPQty,b.CPRate,b.CPValue,b.CPRemarks,a.ChmName as Chemname,a.ChmCode as Chemcode from trans_rcpa_head a inner join trans_rcpa_detail b on a.PK_ID =b.fk_pk_id where sf_code='".$data['sfcode']."' and pk_id='".$b1[$k]["PK_ID"]."'";
$b2=performQuery($rcpdet);


$b1[$k]['RCPADet']=$b2;
}
$response["RCPAHead"]=$b1;
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


