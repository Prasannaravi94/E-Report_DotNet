<?php

header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
session_start();
$URL_BASE = "/";
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";

function send_gcm_notify($reg_id, $message) {
    define("GOOGLE_API_KEY", "AIzaSyC8LnqY6kl_sx27tGOZ_Q92L0id7I13Kyg");
    define("GOOGLE_GCM_URL", "https://android.googleapis.com/gcm/send");

    $fields = array(
        'registration_ids' => array($reg_id),
        'data' => array("message" => $message, "title" => "Leave Application"),
    );

    $headers = array(
        'Authorization: key=' . GOOGLE_API_KEY,
        'Content-Type: application/json'
    );

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, GOOGLE_GCM_URL);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
    curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($fields));

    $result = curl_exec($ch);
    if ($result === FALSE) {
        die('Problem occurred: ' . curl_error($ch));
    }

    curl_close($ch);
    //echo $result;
}

function actionLogin() {
    global $URL_BASE;
    $data = json_decode($_POST['data'], true);
    $username = (string) $data['name'];
    $password = (string) $data['password'];
$AppDeviceRegId=$data['AppDeviceRegId'];

if($AppDeviceRegId==null){
        $respon['success'] = false;
        $respon['msg'] = "Please Enable Perimission into your mobile setting like goto settings->apps->sansfa app->permission-> enable all the elements";
        return outputJSON($respon);
die;
}
    $query = "exec LoginAPP '$username','$password'";
    global $conn;
    $arr;
    $res = sqlsrv_query($conn, $query);
    if ($res) {
        $result = array();
        while ($row = sqlsrv_fetch_array($res, SQLSRV_FETCH_ASSOC)) {
            $result[] = $row;
        }

        $arr = $result;
    }
    sqlsrv_close();
    $respon = array();
    $count = count($arr);
    if ($count == 1) {
        $respon['success'] = true;
        $respon['sfCode'] = $arr[0]['SF_Code'];
        $respon['sfName'] = $arr[0]['SF_Name'];
        $respon['divisionCode'] = $arr[0]['Division_Code'];
        $respon['call_report'] = $arr[0]['call_report'];
        $respon['desigCode'] = $arr[0]['desig_Code'];
        $respon['HlfNeed'] = $arr[0]['MGRHlfDy'];
        if ($arr[0]['desig_Code'] == "MR") {
            $query = "Select count(SF_Code) Cnt from Salesforce_Master where sf_Tp_Reporting='" . $arr[0]['SF_Code'] . "'";
            $dsgc = performQuery($query);
            if ($dsgc[0]['Cnt'] > 0)
                $respon['desigCode'] = "AM";
            else
                $respon['HlfNeed'] = $arr[0]['MRHlfDy'];
        }
$dat=date('Y-m-d');
$sql="select * from TP_Attendance_App where Sf_Code='" . $arr[0]['SF_Code'] . "' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$dat'";
$attendance=performQuery($sql);
if(count($attendance)==0)
$respon['attendanceView'] = 0;
else
$respon['attendanceView'] = 1;
        $respon['AppTyp'] = 0;
        $respon['Attendance'] = $arr[0]['Attendance'];
        $respon['TBase'] = $arr[0]['TBase'];
        $respon['GeoChk'] = $arr[0]['GeoNeed'];
        $respon['ChmNeed'] = $arr[0]['ChmNeed'];
        $respon['StkNeed'] = $arr[0]['StkNeed'];
        $respon['UNLNeed'] = $arr[0]['UNLNeed'];
        $respon['DPNeed'] = $arr[0]['DPNeed'];
        $respon['DINeed'] = $arr[0]['DINeed'];
        $respon['CPNeed'] = $arr[0]['CPNeed'];
        $respon['CINeed'] = $arr[0]['CINeed'];
        $respon['SPNeed'] = $arr[0]['SPNeed'];
        $respon['SINeed'] = $arr[0]['SINeed'];
        $respon['NPNeed'] = $arr[0]['NPNeed'];
        $respon['NINeed'] = $arr[0]['NINeed'];
        $respon['DrCap'] = $arr[0]['DrCap'];
        $respon['ChmCap'] = $arr[0]['ChmCap'];
        $respon['StkCap'] = $arr[0]['StkCap'];
        $respon['NLCap'] = $arr[0]['NLCap'];
        $respon['MCLDet'] = $arr[0]['MCLDet'];
        $respon['username'] = $arr[0]['UsrDfd_UserName'];
        $respon['call_report_from_date'] = $arr[0]['call_report_from_date'];
        $respon['call_report_to_date'] = $arr[0]['call_report_to_date'];
      
        $respon['DRxCap'] = $arr[0]['DrRxQCap'];
        $respon['DSmpCap'] = $arr[0]['DrSmpQCap'];
        $respon['CQCap'] = $arr[0]['ChmQCap'];
        $respon['SQCap'] = $arr[0]['StkQCap'];
        $respon['NRxCap'] = $arr[0]['NLRxQCap'];
        $respon['NSmpCap'] = $arr[0]['NLSmpQCap'];
        $respon['SFStat'] = $arr[0]['SFStat'];
        $respon['days'] = $arr[0]['days'];
        $respon['No_of_TP_View'] = $arr[0]['No_of_TP_View'];
        $respon['SFTPDate'] = $arr[0]['SFTPDate'];
       
 $respon['circular'] = $arr[0]['circular'];
$respon['doctor_dobdow'] = $arr[0]['doctor_dobdow'];
$respon['Doc_Pob_Mandatory_Need'] = $arr[0]['Doc_Pob_Mandatory_Need'];
$respon['Chm_Pob_Mandatory_Need'] = $arr[0]['Chm_Pob_Mandatory_Need'];
 $respon['product_pob_need_msg'] = $arr[0]['product_pob_need_msg'];
 $respon['product_pob_need'] = $arr[0]['product_pob_need'];
$respon['multiple_doc_need'] = $arr[0]['multiple_doc_need'];
$respon['mailneed'] = $arr[0]['mailneed'];
     if($arr[0]['app_device_id']==""&&$arr[0]['SFStat']==0)
{
$sql="update access_table set app_device_id='$AppDeviceRegId' where Sf_Code='" . $arr[0]['SF_Code'] . "'";
performQuery($sql);
}
else if($arr[0]['app_device_id']!=$AppDeviceRegId&&$arr[0]['DeviceId_Need']==0){
$respon=array();
 $respon['success'] = false;
        $respon['msg'] = "Device Not Valid..";
        return outputJSON($respon); die;
}
        return outputJSON($respon);
    } else {
		$respon['success'] = false;
        $respon['msg'] = "Check User and Password";
        return outputJSON($respon);
    }
}
function getProducts() {
    $sfCode = $_GET['sfCode'];
    $DivisionCode = $_GET['divisionCode'];

    $query = "exec getAppProd '" . $sfCode . "'"; //,'".$DivisionCode."'";
    return performQuery($query);
}

function getAPPSetups() {
    $rqSF = $_GET['rSF'];
    $query = "exec getAPPSetups '" . $rqSF . "'";
    return performQuery($query);
}

function getSubordinateMgr() {
    $sfCode = $_GET['rSF'];
    $param = array($sfCode);
    $query = "exec getHyrSF_APP '" . $sfCode . "'";
    return performQuery($query);
}

function getSubordinate() {
    $sfCode = $_GET['rSF'];
    $param = array($sfCode);
    $query = "exec getBaseLvlSFs_APP '" . $sfCode . "'";
    return performQuery($query);
}

function getJointWork() {
    $sfCode = $_GET['sfCode'];
    $rqSF = $_GET['rSF'];
    $query = "exec getJointWork_App '" . $sfCode . "','" . $rqSF . "'";
    return performQuery($query);
}

function getDtTPview() {
    $data = json_decode($_POST['data'], true);
    $sfCode = (string) $data['sfCode'];
    $t = strtotime(str_replace("Z", "", str_replace("T", " ", $data['tpDate'])));
    $TpDt = date('Y-m-d 00:00:00', $t);
    $Qry = "exec spTPViewDtws '$sfCode','$TpDt'";
    $respon = performQuery($Qry);
    return outputJSON($respon);
}

function getTPview() {
    $data = json_decode($_POST['data'], true);
    $sfCode = (string) $data['sfCode'];
    $t = strtotime(str_replace("Z", "", str_replace("T", " ", $data['mnthYr'])));
    $TpDt = date('Y-m-d 00:00:00', $t);
    $Qry = "SELECT convert(varchar,Tour_Date,103) [date],Worktype_Name_B wtype,replace(isnull(Tour_Schedule1,''),'0','') towns,replace(isnull(Tour_Schedule1,''),'0','') PlnNo,Worktype_Name_B1 wtype2,replace(isnull(Tour_Schedule2,''),'0','') towns2,replace(isnull(Tour_Schedule2,''),'0','') PlnNo2
,Worktype_Name_B2 wtype3,replace(isnull(Tour_Schedule3,''),'0','') towns3,replace(isnull(Tour_Schedule2,''),'0','') PlnNo3,SF_Code sf_code from Trans_TP T where sf_code='$sfCode' and Tour_Month=month('$TpDt') and Tour_year=year('$TpDt') order by Tour_Date";
    $respon = performQuery($Qry);
    return outputJSON($respon);
}

function getMonthSummary() {
    $sfCode = $_GET['rptSF'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getMonthSummaryApp '" . $sfCode . "','" . $dyDt . "'";
    return performQuery($query);
}

function getDayReport() {
    $sfCode = $_GET['sfCode'];
    $dyDt = $_GET['rptDt'];
    $query = "exec getDayReportApp '" . $sfCode . "','" . $dyDt . "'";
    return performQuery($query);
}

function getVstDets() {
    $ACd = $_GET['ACd'];
    $typ = $_GET['typ'];
    $query = "exec spGetVstDetApp '" . $ACd . "','" . $typ . "'";
    return performQuery($query);
}
function getDoctorDet(){
    $SF = $_GET['sfCode'];
    $MSL = $_GET['Msl_No'];

    $query = "select Doc_Cat_Code,Doc_Cat_ShortName,Doc_QuaCode, visit_hours,visit_days,REPLACE(visit_days,'/',',') visit_days1,REPLACE(visit_hours,'/',',') visit_hours1,ListedDr_Address3,Doc_Qua_Name,Doc_Special_Code,Doc_Spec_ShortName,Hospital_Address,convert(nvarchar(MAX), ListedDr_DOB, 23) ListedDr_DOB,convert(nvarchar(MAX), ListedDr_DOW, 23) ListedDr_DOW,ListedDr_Hospital,ListedDr_Sex,ListedDr_RegNo,Visiting_Card,Dr_Potential,Dr_Contribution from mas_listeddr where ListedDrCode='" . $MSL . "'";
$result=performQuery($query);
 return outputJSON($result);
}
function getPreCallDet() {
    $SF = $_GET['sfCode'];
    $MSL = $_GET['Msl_No'];

    $result = array();
    $query = "select SLVNo SVL,Doc_Cat_ShortName DrCat,Doc_Spec_ShortName DrSpl,isnull(stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory S where CHARINDEX(cast(Doc_SubCatCode as varchar),D.Doc_SubCatCode)>0 for XML Path('')),1,2,''),'') DrCamp,isnull(stuff((select ', '+Product_Detail_Name from Map_LstDrs_Product M	inner join Mas_Product_Detail P on M.Product_Code=P.Product_Detail_Code and P.Division_Code=M.Division_Code where Listeddr_Code=D.ListedDrCode for XML Path('')),1,2,''),'') DrProd from mas_listeddr D where ListedDrCode='" . $MSL . "'";
    $as = performQuery($query);
    if (count($as) > 0) {
        $result['SVL'] = (string) $as[0]['SVL'];
        $result['DrCat'] = (string) $as[0]['DrCat'];
        $result['DrSpl'] = (string) $as[0]['DrSpl'];
        $result['DrCamp'] = (string) $as[0]['DrCamp'];
        $result['DrProd'] = (string) $as[0]['DrProd'];

        $result['success'] = true;

 $query = "select Trans_SlNo,Trans_Detail_Slno,convert(varchar,Activity_Date,0) Adate,Time DtTm1,convert(varchar,cast(convert(varchar,Activity_Date,101)+' '+Time  as datetime),20) as DtTm,(Select content from vwFeedTemplate where ID=Rx) CalFed,Activity_Remarks,products,gifts from vwLastVstDet where rw=1 and Trans_Detail_Info_Code='" . $MSL . "' and SF_Code='" . $SF . "'";
        $as = performQuery($query);
        if (count($as) > 0) {
$dat=$as[0]['DtTm1'];
   $result['LVDt'] =date_format($dat,'d / m / Y g:i a');
           // $result['LVDt'] = date('d / m / Y g:i a', strtotime((string) $as[0]['DtTm']));
            $Prods = (string) $as[0]['products'];
            $sProds = explode("#", $Prods . '#');
            $sSmp = '';
            $sProm = '';
            for ($il = 0; $il < count($sProds); $il++) {
                if ($sProds[$il] != '') {
                    $spr = explode("~", $sProds[$il]);
                    $Qty = 0;
                    if (count($spr) > 0) {
                        $QVls = explode("$", $spr[1]);
                        $Qty = $QVls[0];
                        $Vals = $QVls[1];
                    }
                    if ($Qty > 0)
                        $sSmp = $sSmp . $spr[0] . " ( " . $Qty . " )" . (($Vals > 0) ? " ( " . $Vals . " )" : "");
                    else
                        $sProm = $sProm . $spr[0] . ", ";
                }
            }

            $result['CallFd'] = (string) $as[0]['CalFed'];
            $result['Rmks'] = (string) $as[0]['Activity_Remarks'];
            $result['ProdSmp'] = $sSmp;
            $result['Prodgvn'] = $sProm;
            $result['DrGft'] = (string) $as[0]['gifts'];
        }else {
            $result['success'] = false;
        }
    } else {
        $result['success'] = false;
    }
    return outputJSON($result);
}

function getEntryCount() {
    $sfCode = $_GET['sfCode'];
    $today = date('Y-m-d 00:00:00');
    $results = array();
    $query = "select Count(Trans_Detail_Info_Code) doctor_count from vwActivity_MSL_Details D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) chemist_count from vwActivity_CSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=2";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) stockist_count from vwActivity_CSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=3";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) uldoctor_count from vwActivity_Unlst_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=4";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select isnull((SELECT top 1 isnull(remarks,'') from vwActivity_Report where sf_code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)),'') as remarks";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select isnull((SELECT top 1 Half_Day_FW from vwActivity_Report where sf_code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime)),'') as halfdaywrk";
    $temp = performQuery($query);
    $results[] = $temp[0];
    $query = "select Count(Trans_Detail_Info_Code) hospital_count from vwActivity_CSH_Detail D inner join vwActivity_Report H on H.Trans_SlNo=D.Trans_SlNo where H.SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$today' as datetime) and Trans_Detail_Info_Type=5";
    $temp = performQuery($query);
    $results[] = $temp[0];
    return $results;
}

function getaddress($lat, $lng) {
    $url = 'http://maps.googleapis.com/maps/api/geocode/json?latlng=' . trim($lat) . ',' . trim($lng) . '&sensor=false';
    $json = @file_get_contents($url);
    $data = json_decode($json);
    $status = $data->status;
    if ($status == "OK") {
        return $data->results[0]->formatted_address;
    } else {
        return false;
    }
}

function updEntry() {
    $today = date('Y-m-d 00:00:00');
    $data = json_decode($_POST['data'], true);
    $SFCode = (string) $data[0]['Activity_Report']['SF_code'];
    $sql = "select SF_Code from vwActivity_report where sf_Code=" . $SFCode . " and cast(activity_date as datetime)=cast('$today' as datetime)";
    $result = performQuery($sql);
    if (count($result) < 1) {
        $result = array();
        $result['success'] = false;
        $result['type'] = 2;
        $result['msg'] = 'No Call Report Submited...';
        outputJSON($result);
        die;
    }

    $Remarks = (string) $data[0]['Activity_Report']['remarks'];
    $HalfDy = (string) $data[0]['Activity_Report']['HalfDay_FW_Type'];


    $sql = "update DCRMain_Temp set Remarks=$Remarks,Half_Day_FW=$HalfDy where sf_Code=" . $SFCode . " and cast(activity_date as datetime)=cast('$today' as datetime)";
    $result = performQuery($sql);
    $sql = "update DCRMain_Trans set Remarks=$Remarks,Half_Day_FW=$HalfDy where sf_Code=" . $SFCode . " and cast(activity_date as datetime)=cast('$today' as datetime)";
    $result = performQuery($sql);
    $resp["success"] = true;
    echo json_encode($resp);
}

function getFromTableWR($tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null) {

    $query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
    $query.= " WHERE tab.Division_Code=" . $divisionCode;

    if (!is_null($where)) {
        $query.=" and " . join(" or ", $where);
    }

    if (!is_null($today)) {
        $today = date('Y-m-d 00:00:00');

        $query.="and cast(tab.activity_date as datetime)=cast('$today' as datetime)";
    }

    if (!is_null($orderBy)) {
        $query .=" ORDER BY " . join(", ", $orderBy);
    }
    //if($tableName=="vwActivity_Unlst_Detail") echo($query);
    return performQuery($query);
}

function getFromTable($tableName, $coloumns, $divisionCode, $sfCode = null, $orderBy = null, $where = null, $join = null, $today = null, $wt = null) {

    $query = "SELECT " . join(",", $coloumns) . " FROM $tableName as tab";
    if (!is_null($join)) {
        $query.=" join " . join(" join ", $join);
    }
    if (!is_null($sfCode)) {
        $query .=" WHERE tab.SF_Code='$sfCode'";
    } else {
        $query.= " WHERE tab.Division_Code=" . $divisionCode;
    }
    if (!is_null($where)) {
        $query.=" and " . join(" and ", $where);
    }
    if (!is_null($today)) {
        $today = date('Y-m-d 00:00:00');
        $query.=" and cast(tab.activity_date as datetime)=cast('$today' as datetime)";
    }
    if (!is_null($orderBy)) {
        $query .=" ORDER BY " . join(",", $orderBy);
    }
//	if(strtolower($tableName)=="vwActivity_Unlst_Detail") echo($query);
    return performQuery($query);
}

function deleteEntry($arc, $amc) {
    if (!is_null($amc)) {
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

         $sql = "DELETE FROM DCREvent_Captures where Trans_Detail_Slno='".$amc."'";performQuery($sql); 
    }
}

function delAREntry($SF, $WT, $Dt) {

    $sqlH = "SELECT Trans_SlNo FROM vwActivity_Report where SF_Code='" . $SF . "' and lower(Work_Type) <> lower(" . $WT . ") and cast(activity_date as datetime)=cast('$Dt' as datetime)";
    $sql = "DELETE FROM DCRDetail_Lst_Temp where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
    $sql = "DELETE FROM DCRDetail_Lst_Trans where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCRDetail_CSH_Temp where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
    $sql = "DELETE FROM DCRDetail_CSH_Trans where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCRDetail_Unlst_Temp where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);
    $sql = "DELETE FROM DCRDetail_Unlst_Trans where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCREvent_Captures where Trans_SlNo in (" . $sqlH . ")";
    performQuery($sql);

    $sql = "DELETE FROM DCRMain_Temp where SF_Code='" . $SF . "' and lower(Work_Type) <> lower(" . $WT . ") and cast(activity_date as datetime)=cast('$Dt' as datetime)";
    performQuery($sql);
    $sql = "DELETE FROM DCRMain_Trans where SF_Code='" . $SF . "' and lower(Work_Type) <> lower(" . $WT . ") and cast(activity_date as datetime)=cast('$Dt' as datetime)";
    performQuery($sql);
}

function addEntry() {
    $sfCode = $_GET['sfCode'];
    $div = $_GET['divisionCode'];
    $divs = explode(",", $div . ",");
    $Owndiv = (string) $divs[0];
    $data = json_decode($_POST['data'], true);
    $today = date('Y-m-d 00:00:00');
    $temp = array_keys($data[0]);
    $vals = $data[0][$temp[0]];

    $sql = "SELECT Employee_Id,case sf_type when 1 then 'MR' else 'MGR' End SF_Type FROM Mas_Salesforce_One where SF_code='" . $sfCode . "'";
    $as = performQuery($sql);
    $IdNo = (string) $as[0]['Employee_Id'];
    $SFTyp = (string) $as[0]['SF_Type'];
    switch ($temp[0]) {
        case "tbMyDayPlan":
			if($vals["location"]==null)
					$location="";
			else
					$location=$vals["location"];

			if($vals["dcr_activity_date"]==$today){
					$mydayDate=date('Y-m-d H:i:s');
			}
			else
					$mydayDate=$vals["dcr_activity_date"];

			if($vals["dcr_activity_date"]!=null&&$vals["dcr_activity_date"]!='')
			{
					$today=$vals["dcr_activity_date"];
			}

            $sql = "insert into tbMyDayPlan select '" . $sfCode . "'," . $vals["sf_member_code"] . ",'$mydayDate'," . $vals["cluster"] . "," . $vals["remarks"] . ",'" . $Owndiv . "'," . $vals["wtype"] . "," . $vals["FWFlg"] . "," . $vals["ClstrName"] . ",'','$location'";
            performQuery($sql);
            if (str_replace("'", "", $vals["FWFlg"]) != "F") {
				$sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "'  and cast(activity_date as datetime)=cast('$today' as datetime)";
                $result1 = performQuery($sql);
				if (count($result1) > 0) {
						if ($result1[0]['FWFlg'] == 'L' && $result1[0]['Confirmed'] != 2 && $result1[0]['Confirmed'] != 3) {
							$result = array();
							$result['success'] = false;
							$result['msg'] = 'Leave Post Already Updated';
							outputJSON($result);
							die;
						} else {
							delAREntry($sfCode, $vals["wtype"], $today);
							$ARCd = 0;
							$sql = "{call  svDCRMain_App(?,?," . $vals["wtype"] . ",'" . str_replace("'", "", $vals["cluster"]) . "',?,'" . str_replace("'", "", $vals["remarks"]) . "',?)}";
							$params = array(array($sfCode, SQLSRV_PARAM_IN),
							array($today, SQLSRV_PARAM_IN),
							array($Owndiv, SQLSRV_PARAM_IN),
							array($ARCd, SQLSRV_PARAM_IN)); ////, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
							performQueryWP($sql, $params);
				
					if($ARCd==""){
						$query="select Trans_SlNo from vwActivity_Report where Sf_Code='".$sfCode."' and Activity_Date='".$today."'";
						$arr = performQuery($query);
						$ARCd=$arr[0]["Trans_SlNo"];
					}
			}
}
else
 {
                delAREntry($sfCode, $vals["wtype"], $today);
                $ARCd = 0;
                $sql = "{call  svDCRMain_App(?,?," . $vals["wtype"] . ",'" . str_replace("'", "", $vals["cluster"]) . "',?,'" . str_replace("'", "", $vals["remarks"]) . "',?)}";
                $params = array(array($sfCode, SQLSRV_PARAM_IN),
                    array($today, SQLSRV_PARAM_IN),
                    array($Owndiv, SQLSRV_PARAM_IN),
                    array($ARCd, SQLSRV_PARAM_IN)); ////, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)));
                performQueryWP($sql, $params);
				
				if($ARCd==""){
					$query="select Trans_SlNo from vwActivity_Report where Sf_Code='".$sfCode."' and Activity_Date='".$today."'";
					$arr = performQuery($query);
					$ARCd=$arr[0]["Trans_SlNo"];
				}
}
            }

            break;
        case "chemists_master":
            $sql = "SELECT isNull(max(Chemists_Code),0)+1 as RwID FROM Mas_Chemists";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];

            $sql = "insert into Mas_Chemists(Chemists_Code,Chemists_Name,Chemists_Address1,Territory_Code,Chemists_Phone,Chemists_Contact,Division_Code,Cat_Code,Chemists_Active_Flag,Sf_Code,Created_Date,Created_By) select '" . $pk . "'," . $vals["chemists_name"] . "," . $vals["Chemists_Address1"] . "," . $vals["town_code"] . "," . $vals["Chemists_Phone"] . ",'','" . $Owndiv . "','',0,'" . $sfCode . "','" . date('Y-m-d H:i:s') . "','Apps'";
            performQuery($sql);
            break;
        case "unlisted_doctor_master":
            $sql = "SELECT isNull(max(UnListedDrCode),0)+1 as RwID FROM Mas_UnListedDr";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];

            $sql = "insert into Mas_UnListedDr(UnListedDrCode,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code,Territory_Code,UnListedDr_Active_Flag,UnListedDr_Sl_No,Division_Code,SLVNo,Doc_QuaCode,Doc_ClsCode,Sf_Code,UnListedDr_Created_Date,Created_By) select '" . $pk . "'," . $vals["unlisted_doctor_name"] . ",''," . $vals["unlisted_specialty_code"] . "," . $vals["unlisted_cat_code"] . "," . $vals["town_code"] . ",0,'" . $pk . "','" . $Owndiv . "','" . $pk . "'," . $vals["unlisted_qulifi"] . "," . $vals["unlisted_class"] . ",'" . $sfCode . "','" . date('Y-m-d H:i:s') . "','Apps'";
            performQuery($sql);
            break;
case "TP_Attendance":
$dateTime=date('Y-m-d H:i');
$date=date('Y-m-d');
$lat=$vals['lat'];
$long=$vals['long'];
$update=$_GET['update'];
if($update==0){
$sql="insert into TP_Attendance_App select '$sfCode',$Owndiv,'$dateTime','',$lat,$long,'',''";
performQuery($sql);
}
else{
$sql="select id from TP_Attendance_App where Sf_Code='$sfCode' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='$date' order by id desc";
$tr=performQuery($sql);
$id=$tr[0]['id'];

$sql="update TP_Attendance_App set End_Lat=$lat,End_Long=$long,End_Time='$dateTime' where id=$id";
performQuery($sql);
}
break;
case "MCL_Details":

$primary_key="ListedDrCode";
$row_id=$data[0]['MCL_Details']['doctorCode'];
$data[0]['MCL_Details']['Update_Mode']="'Apps'";
//$data[0]['MCL_Details']['Visiting_Card']="~/Visiting_Card/".$data[0]['MCL_Details']['Visiting_Card'];
unset($data[0]['MCL_Details']['doctorCode']);
 foreach ($data[0]['MCL_Details'] as $col => $val) {
//$val=str_replace("''","",$val);
            $cols[] = $col . " = " . $val;
//            $values[] = $val;
        }

        $sql = "UPDATE Mas_ListedDr set "
                . join(", ", $cols)
                . " where $primary_key = $row_id";

    performQuery($sql);
//$result = array();
                   // $result['success'] = false;
//$result['msg'] = "dsjdjhd";
 //outputJSON($result);
// die;
break;
case "Map_GEO_Customers":
					$addr="'".getaddress(str_replace("'","",$vals["lat"]),str_replace("'","",$vals["long"]))."'";
					$sql = "SELECT isNull(max(MapId),0)+1 as MapId FROM Map_GEO_Customers";
                    $topr = performQuery($sql);
                    $pk = (int) $topr[0]['MapId'];

             $sql = "insert into Map_GEO_Customers(MapId, Cust_Code, lat, long, addrs, StatFlag, Division_code) select $pk,".$vals["Cust_Code"].",".$vals["lat"].",".$vals["long"].",".$addr.",".$vals["StatFlag"].",$Owndiv";
            performQuery($sql);      
					break;

        case "tbRCPADetails":
            $sql = "insert into tbRCPADetails select '" . $sfCode . "','" . date('Y-m-d H:i:s') . "'," . $vals["RCPADt"] . "," .
                    $vals["ChmId"] . "," . $vals["DrId"] . "," . $vals["CmptrName"] . "," . $vals["CmptrBrnd"] . "," . $vals["CmptrPriz"] . "," .
                    $vals["ourBrnd"] . "," . $vals["ourBrndNm"] . "," . $vals["Remark"] . ",'" . $div . "'," . $vals["CmptrQty"] . "," . $vals["CmptrPOB"] . "," . $vals["ChmName"] . "," . $vals["DrName"];
            performQuery($sql);
            break;
        case "tbRemdrCall":
            $sql = "SELECT isNull(max(cast(replace(RwID,'RC/" . $IdNo . "/','') as numeric)),0)+1 as RwID FROM tbRemdrCall where RwID like 'RC/" . $IdNo . "/%'";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];

            $sql = "insert into tbRemdrCall select 'RC/" . $IdNo . "/" . $pk . "','" . $sfCode . "','" . date('Y-m-d H:i:s') . "'," . $vals["Doctor_ID"] . "," .
                    $vals["WWith"] . "," . $vals["WWithNm"] . "," . $vals["Prods"] . "," . $vals["ProdsNm"] . "," . $vals["Remarks"] . "," .
                    $vals["location"] . ",'" . $div . "'";
            performQuery($sql);
            break;
        case "Tour_Plan":
            $divCode = $_GET['divisionCode'];
            $divisionCode = explode(",", $divCode);
            $desig = $_GET['desig'];
            $objective = $data[0]['Tour_Plan']['objective'];
            $tourDate = $data[0]['Tour_Plan']['Tour_Date'];
            $worktype_code = $data[0]['Tour_Plan']['worktype_code'];
            $worktype_name = $data[0]['Tour_Plan']['worktype_name'];
            $worktype_code2 = $data[0]['Tour_Plan']['worktype_code2'];
            $worktype_name2 = $data[0]['Tour_Plan']['worktype_name2'];
            $worktype_code3 = $data[0]['Tour_Plan']['worktype_code3'];
            $worktype_name3 = $data[0]['Tour_Plan']['worktype_name3'];
            $worked_with_code = $data[0]['Tour_Plan']['Worked_with_Code'];
            $worked_with_name = $data[0]['Tour_Plan']['Worked_with_Name'];
 if($data[0]['Tour_Plan']['worktype_name']!="'Field Work'"){
 $RouteCode ="''";
            $RouteName ="''";
}
else{
            $RouteCode = $data[0]['Tour_Plan']['RouteCode'];
            $RouteName = $data[0]['Tour_Plan']['RouteName'];
}
           // $RouteCode = $data[0]['Tour_Plan']['RouteCode'];
           // $RouteName = $data[0]['Tour_Plan']['RouteName'];
            $RouteCode2 = $data[0]['Tour_Plan']['RouteCode2'];
            $RouteName2 = $data[0]['Tour_Plan']['RouteName2'];
            $RouteCode3 = $data[0]['Tour_Plan']['RouteCode3'];
            $RouteName3 = $data[0]['Tour_Plan']['RouteName3'];
            $sfName = $data[0]['Tour_Plan']['sfName'];
            $sql = "delete from Trans_TP_One WHERE SF_Code ='" . $sfCode . "' and Tour_Date=cast($tourDate as datetime)";
            //  print_r($sql);die;
            performQuery($sql);
            $sql = "insert into Trans_TP_One(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B,Territory_Code1,Objective,Worked_With_SF_Code,Division_Code,Tour_Schedule1,Worked_With_SF_Name,TP_Sf_Name,Confirmed,Territory_Code2,Tour_Schedule2,Territory_Code3,Tour_Schedule3,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Change_Status) select '" . $sfCode . "',MONTH($tourDate),YEAR($tourDate),'" . date('Y-m-d') . "',$tourDate,$worktype_code,$worktype_name,$RouteCode,$objective,$worked_with_code," . $divisionCode[0] . ",$RouteName,$worked_with_name,$sfName,0,$RouteCode2,$RouteName2,$RouteCode3,$RouteName3,$worktype_code2,$worktype_name2,$worktype_code3,$worktype_name3,0";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "TourPlanSubmit":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $sql = "update Trans_TP_One set Change_Status=1 where Tour_Month=$month and Tour_Year=$year and Sf_Code='$sfCode'";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "TPApproval":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $code = $_GET['code'];
            $sql = "delete from Trans_TP where sf_Code='$code' and Tour_Month=$month and Tour_Year=$year";
            performQuery($sql);

            $sql = "insert into Trans_TP(Division_Code,SF_Code,Worked_With_SF_Code,Worked_With_SF_Name,Tour_Date,Tour_Month,Tour_Year,WorkType_Code_B,Worktype_Name_B,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Objective,Confirmed,Confirmed_Date,Rejection_Reason,Territory_Code1,Territory_Code2,Territory_Code3,TP_Sf_Name,TP_Approval_MGR,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Submission_date,Change_Status)
		          select Division_Code,SF_Code,Worked_With_SF_Code,Worked_With_SF_Name,Tour_Date,Tour_Month,Tour_Year,WorkType_Code_B,Worktype_Name_B,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Objective,1,GETDATE(),Rejection_Reason,Territory_Code1,Territory_Code2,Territory_Code3,TP_Sf_Name,TP_Approval_MGR,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Submission_date,Change_Status
		from Trans_TP_One where sf_Code='$code' and Tour_Month=$month and Tour_Year=$year";
            $trs = performQuery($sql);
          if (count($trs) > 0) {
                $sql = "delete from Trans_TP_One where sf_Code='$code' and Tour_Month=$month and Tour_Year=$year";
                performQuery($sql);

if($month=="12"){
$year=$year+1;
$month=1;
}
else
$month=$month+1;
$date=$year.'-'.$month.'-01';
$sql = "update mas_salesforce_dcrtpdate set Last_TP_Date='$date' where sf_Code='$code' and '$date'>Last_TP_Date";
            performQuery($sql);

            }

            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "TPReject":
            $month = $_GET['month'];
            $year = $_GET['year'];
            $code = $_GET['code'];
            $sql = "insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Reject_date,Division_Code,Rejection_Reason) select '" . $code . "',$month,$year,'" . date('Y-m-d H:i') . "',$Owndiv," . $vals['reason'] . "";
            performQuery($sql);

            $sql = "update Trans_TP_One set Change_Status=2,Confirmed=0,Rejection_Reason=" . $vals['reason'] . " where Tour_Month=$month and Tour_Year=$year and Sf_Code='$code'";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
      
       case "LeaveApproval":
            $leaveid = $_GET['leaveid'];
            $RSF = $_GET['rSF'];
			$sql = "exec iOS_svLeaveAppRej  " . $leaveid  . ",0,'','".$RSF."','Apps'";
			performQuery($sql);
            break;

        case "LeaveReject":
            $leaveid = $_GET['leaveid'];
            $sql = "update Mas_Leave_Form set Leave_Active_Flag=1,Rejected_Reason=" . $vals['reason'] . ",LastUpdt_Date='" . date('Y-m-d') . "' where Leave_Id=$leaveid";
            performQuery($sql);
            break;
        case "LeaveForm":
            $name = $_GET['sf_name'];

            $sql = "SELECT sf_emp_id  FROM Mas_Salesforce where Sf_Code='$sfCode'";
            $tRw = performQuery($sql);
            $empid =$tRw[0]['sf_emp_id'];
            $sql = "SELECT isNull(max(Leave_Id),0)+1 as RwID FROM Mas_Leave_Form";
            $tRw = performQuery($sql);
            $pk = (int) $tRw[0]['RwID'];
            $sql = "insert into Mas_Leave_Form(Leave_Id,Leave_Type,From_Date,To_Date,Reason,sf_code,Division_Code,Leave_Active_Flag,Created_Date,No_of_Days,Address,Sf_Emp_Id) select '$pk'," . $vals['Leave_Type'] . ",'" . $vals['From_Date'] . "'," . $vals['To_Date'] . "," . $vals['Reason'] . ",'$sfCode','$Owndiv',2,'" . date('Y-m-d') . "'," . $vals['No_of_Days'] . "," . $vals['address'] . ",'".   $empid ."'";
            performQuery($sql);
            $sql = "SELECT DeviceRegId FROM Access_Table where sf_code=(select Reporting_To_SF from mas_salesforce_one where Sf_Code='$sfCode')";
            $device = performQuery($sql);
            $reg_id = $device[0]['DeviceRegId'];
            if (!empty($reg_id)) {
                //   $msg = $name . " Applied Leave for " . $vals['No_of_Days'] . " days";
                $msg = "Leave Application Received";
                send_gcm_notify($reg_id, $msg);
            }
            break;
             case "DCRApproval":
				$date = $_GET['date'];
				$code = $_GET['code'];
				$date = str_replace('/', '-', $date);
				$date = date('Y-m-d', strtotime($date));
				$sql = "exec ApproveDCRByDt '" . $code . "','$date'";
				performQuery($sql);
				$resp["success"] = true;
				echo json_encode($resp);
            die;
            break;
        case "DCRReject":
            $date = $_GET['date'];
            $code = $_GET['code'];
            $date = str_replace('/', '-', $date);
            $date = date('Y-m-d', strtotime($date));
            $sql = "update DCRMain_Temp set Confirmed=2,ReasonforRejection=" . $vals['reason'] . " where Sf_Code='$code' and Activity_Date='$date'";
            performQuery($sql);
            $resp["success"] = true;
            echo json_encode($resp);
            die;
            break;
        case "Activity_Report_APP":
			$username=$vals['username'];
			$AppDeviceRegId=$vals['app_device_id'];

			$sql="select * from mas_salesforce where UsrDfd_UserName='$username' and SF_Status=0";
			$tr=performQuery($sql);
			if(count($tr)==0&&$AppDeviceRegId!=null){
					$respon = array();
					$respon['success'] = false;
					$respon['type'] = 3;
					$respon['msg'] = "User Status Changed,. Kindly Login Again....";
					return outputJSON($respon);
					die;
			}
$sql="select app_device_id from access_table where Sf_Code='$sfCode'";
$arr=performQuery($sql);
if($arr[0]['app_device_id']==""&&$AppDeviceRegId!=null)
{
$sql="update access_table set app_device_id='$AppDeviceRegId' where Sf_Code='$sfCode'";
performQuery($sql);
}
else if($arr[0]['app_device_id']!=$AppDeviceRegId&&$AppDeviceRegId!=null){
$sql="select DeviceId_Need from Access_Master where division_code='$Owndiv'";
$tr=performQuery($sql);
if($tr[0]['DeviceId_Need']=="0"){
$respon=array();
 $respon['success'] = false;
 $respon['type'] = 3;
        $respon['msg'] = "Device Not Valid..";
        return outputJSON($respon); die;
}
}
if($vals["dcr_activity_date"]!=null&&$vals["dcr_activity_date"]!=''){
$today=$vals["dcr_activity_date"];
}

            $sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "' and lower(Work_Type) <>lower(" . $vals["Worktype_code"] . ")  and cast(activity_date as datetime)=cast('$today' as datetime)";
            $result1 = performQuery($sql);
 $sql = "SELECT * FROM dcrmain_temp where SF_Code='" . $sfCode . "' and  cast(activity_date as datetime)=cast('$today' as datetime) and confirmed=2 and fieldwork_indicator='L'";
 $leavereg = performQuery($sql);
 if (count($leavereg) > 0) {
 $sql = "delete FROM dcrmain_temp where SF_Code='" . $sfCode . "' and  cast(activity_date as datetime)=cast('$today' as datetime) and confirmed=2 and fieldwork_indicator='L'";
performQuery($sql);
}
            if (count($result1) > 0) {
                if (!isset($_GET['replace'])) {
$result = array();
                    $result['success'] = false;
                    if ($result1[0]['FWFlg'] == 'L' && $result1[0]['Confirmed'] != 2 && $result1[0]['Confirmed'] != 3) {
                        $result['type'] = 2;
                        $result['msg'] = 'Leave Post Already Updated';
                    } else {
                        $result['type'] = 1;
                        $result['msg'] = 'Already There is a Data For other Work do you want to replace....?';
                    }
                    $result['data'] = $data;
                    outputJSON($result);
                    die;
                   
                } else {

                    delAREntry($sfCode, $vals["Worktype_code"], $today);
                }
            }

            $pProd = '';
            $npProd = '';
            $pGCd = '';
            $pGNm = '';
            $pGQty = '';
            $SPProds = '';
            $nSPProds = '';
            $Inps = '';
            $nInps = '';
            $vTyp = 0;
            for ($i = 1; $i < count($data); $i++) {
                $tableData = $data[$i];
                if (isset($tableData['Activity_Doctor_Report'])) {
                    $vTyp = 1;
                    $DetTB = $tableData['Activity_Doctor_Report'];
                    $cCode = $DetTB["doctor_code"];
                    $vTm = $DetTB["Doc_Meet_Time"];
                    $pob = $DetTB["Doctor_POB"];
                    $proc = "svDCRLstDet_App";
                    $sql = "SELECT Doctor_Name name from vwDoctor_Master_APP where Doctor_Code=" . $cCode;
                }
                if (isset($tableData['Activity_Chemist_Report'])) {
                    $vTyp = 2;
                    $DetTB = $tableData['Activity_Chemist_Report'];
                    $cCode = $DetTB["chemist_code"];
                    $vTm = $DetTB["Chm_Meet_Time"];
                    $pob = $DetTB["Chemist_POB"];
                    $sql = "SELECT Chemists_Name name from vwChemists_Master_APP where Chemists_Code=" . $cCode;
                }
                if (isset($tableData['Activity_Stockist_Report'])) {
                    $vTyp = 3;
                    $DetTB = $tableData['Activity_Stockist_Report'];
                    $cCode = $DetTB["stockist_code"];
                    $vTm = $DetTB["Stk_Meet_Time"];
                    $pob = $DetTB["Stockist_POB"];
                    $sql = "SELECT stockiest_name name from vwstockiest_Master_APP where stockiest_code=" . $cCode;
                }
                if (isset($tableData['Activity_UnListedDoctor_Report'])) {
                    $vTyp = 4;
                    $DetTB = $tableData['Activity_UnListedDoctor_Report'];
                    $cCode = $DetTB["uldoctor_code"];
                    $vTm = $DetTB["UnListed_Doc_Meet_Time"];
                    $pob = $DetTB["UnListed_Doctor_POB"];
                    $proc = "svDCRUnlstDet_App";
                    $sql = "SELECT unlisted_doctor_name name from vwunlisted_doctor_master_APP where unlisted_doctor_code=" . $cCode;
                }
	if(isset($tableData["Activity_Event_Captures"]))
				{
					$Event_Captures = $tableData["Activity_Event_Captures"];
					
				}

                $tRw = performQuery($sql);
                $cName = $tRw[0]["name"];

                if (isset($tableData['Activity_Sample_Report']) || isset($tableData['Activity_Unlistedsample_Report'])) {

                    if (isset($tableData['Activity_Sample_Report']))
                        $samp = $tableData['Activity_Sample_Report'];
                    if (isset($tableData['Activity_Unlistedsample_Report']))
                        $samp = $tableData['Activity_Unlistedsample_Report'];

                    for ($j = 0; $j < count($samp); $j++) {
                        if ($j < 3) {
                            $pProd = $pProd . (($pProd != "") ? "#" : '') . $samp[$j]["product_code"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"];
                            $npProd = $npProd . (($npProd != "") ? "#" : '') . $samp[$j]["product_Name"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"];
                        } else {
                            $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"] . "#";
                            $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Product_Sample_Qty"] . "$" . $samp[$j]["Product_Rx_Qty"] . "#";
                        }
                    }
                }

                if (isset($tableData['Activity_POB_Report'])) {

                    if (isset($tableData['Activity_POB_Report']))
                        $samp = $tableData['Activity_POB_Report'];
                    if (isset($tableData['Activity_Stk_POB_Report']))
                        $samp = $tableData['Activity_Stk_POB_Report'];

                    for ($j = 0; $j < count($samp); $j++) {
                        $SPProds = $SPProds . $samp[$j]["product_code"] . "~" . $samp[$j]["Qty"] . "#";
                        $nSPProds = $nSPProds . $samp[$j]["product_Name"] . "~" . $samp[$j]["Qty"] . "#";
                    }
                }
                if (isset($tableData['Activity_Input_Report']) || isset($tableData['Activity_Chm_Sample_Report']) || isset($tableData['Activity_Chm_Sample_Report']) || isset($tableData['activity_unlistedGift_Report'])) {
                    if (isset($tableData['Activity_Input_Report']))
                        $inp = $tableData['Activity_Input_Report'];
                    if (isset($tableData['Activity_Chm_Sample_Report']))
                        $inp = $tableData['Activity_Chm_Sample_Report'];
                    if (isset($tableData['Activity_Stk_Sample_Report']))
                        $inp = $tableData['Activity_Stk_Sample_Report'];
                    if (isset($tableData['activity_unlistedGift_Report']))
                        $inp = $tableData['activity_unlistedGift_Report'];

                    for ($j = 0; $j < count($inp); $j++) {
                        if ($j == 0 && ($vTyp == 1 || $vTyp == 4 )) {
                            $pGCd = $inp[$j]["Gift_Code"];
                            $pGNm = $inp[$j]["Gift_Name"];
                            $pGQty = $inp[$j]["Gift_Qty"];
                        } else {
                            $Inps = $Inps . $inp[$j]["Gift_Code"] . "~" . $inp[$j]["Gift_Qty"] . "#";
                            $nInps = $nInps . $inp[$j]["Gift_Name"] . "~" . $inp[$j]["Gift_Qty"] . "#";
                        }
                    }
                }
            }


            $ARCd = 0;
            $ARDCd = (strlen($_GET['amc']) == 0) ? "0" : $_GET['amc'];
            $sql = "{call  svDCRMain_App(?,?," . $vals["Worktype_code"] . ",'" . str_replace("'", "", $vals["Town_code"]) . "',?,'" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?)}";
            $params = array(array($sfCode, SQLSRV_PARAM_IN),
                array($today, SQLSRV_PARAM_IN),
                array($Owndiv, SQLSRV_PARAM_IN)
				,array($ARCd, SQLSRV_PARAM_IN)  //, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_VARCHAR(50)
				);
                
            performQueryWP($sql, $params);
			
				if($ARCd==""){
					$query="select Trans_SlNo from vwActivity_Report where Sf_Code='".$sfCode."' and Activity_Date='".$today."'";
					$arr = performQuery($query);
					$ARCd=$arr[0]["Trans_SlNo"];
				}
            $loc = explode(":", str_replace("'", "", $DetTB["location"]) . ":");
            $lat = $loc[0]; //latitude
            $lng = $loc[1]; //longitude
            $address = getaddress($lat, $lng);
            if ($address) {
                $DetTB["geoaddress"] = $address;
            } else {
                $DetTB["geoaddress"] = "NA";
            }

            $sqlsp = "{call  ";
            if ($vTyp != 0) {
                if ($vTyp == 2 || $vTyp == 3)
                    $proc = "svDCRCSHDet_App";

                if ($pob == '')
                    $pob = '0';
                $sqlsp = $sqlsp . $proc . " (?,?,?," . $vTyp . "," . $cCode . ",'" . $cName . "'," . $vTm . "," . $pob . ",'" . str_replace("'", "", $DetTB["Worked_With"]) . "',?,?,?,?,";
                if ($vTyp == 1 || $vTyp == 4)
                    $sqlsp = $sqlsp . "?,?,?,?,?,";
                $sqlsp = $sqlsp . "'" . str_replace("'", "", $vals["Town_code"]) . "','" . str_replace("'", "", $vals["Daywise_Remarks"]) . "',?,'" . str_replace("'", "", $vals["rx_t"]) . "'," . $DetTB["modified_time"] . ",?,?," . $vals["DataSF"] . ",'" . $DetTB["geoaddress"] . "')}";

                $params = array(array($ARCd, SQLSRV_PARAM_IN),
                    array($ARDCd, SQLSRV_PARAM_IN),   //, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR), SQLSRV_SQLTYPE_NVARCHAR(50)
                	array($sfCode, SQLSRV_PARAM_IN));
                if ($vTyp == 1 || $vTyp == 4) {
                    array_push($params, array($pProd, SQLSRV_PARAM_IN));
                    array_push($params, array($npProd, SQLSRV_PARAM_IN));
                }

                array_push($params, array($SPProds, SQLSRV_PARAM_IN));
                array_push($params, array($nSPProds, SQLSRV_PARAM_IN));

                if ($vTyp == 1 || $vTyp == 4) {
                    array_push($params, array($pGCd, SQLSRV_PARAM_IN));
                    array_push($params, array($pGNm, SQLSRV_PARAM_IN));
                    array_push($params, array($pGQty, SQLSRV_PARAM_IN));
                }
                array_push($params, array($Inps, SQLSRV_PARAM_IN));
                array_push($params, array($nInps, SQLSRV_PARAM_IN));
                array_push($params, array($Owndiv, SQLSRV_PARAM_IN));
                array_push($params, array($loc[0], SQLSRV_PARAM_IN));
                array_push($params, array($loc[1], SQLSRV_PARAM_IN));
                performQueryWP($sqlsp, $params);

for ($j = 0; $j < count($Event_Captures); $j++) {
	                	
						$ev_imgurl=$sfCode."_".$Event_Captures[$j]["imgurl"];
				 		$ev_title=$Event_Captures[$j]["title"];
				 		$ev_remarks=$Event_Captures[$j]["remarks"];
				 		if ($vTyp ==1){
							$query="select Trans_Detail_Slno from vwActivity_MSL_Details where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code=".$cCode;

							$arr = performQuery($query);
							$ARDCd=$arr[0]["Trans_Detail_Slno"];
						 }
						if ($vTyp == 2 || $vTyp == 3){
							$query="select Trans_Detail_Slno from vwActivity_CSH_Detail where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code=".$cCode;
							$arr = performQuery($query);
							$ARDCd=$arr[0]["Trans_Detail_Slno"];
						 }
						if ($vTyp == 4 ){
							$query="select Trans_Detail_Slno from vwActivity_Unlst_Detail where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code=".$cCode;
							$arr = performQuery($query);
							$ARDCd=$arr[0]["Trans_Detail_Slno"];
						 }
						$sql = "insert into DCREvent_Captures(Trans_slno,Trans_detail_slno,imgurl,title,remarks,Division_Code,sf_code) select '" . $ARCd . "','".$ARDCd."','" . $ev_imgurl . "'," . $ev_title . "," . $ev_remarks . ",'" . $Owndiv . "','$sfCode'";
		            	performQuery($sql);
	
					}

                if (sqlsrv_errors() != null) {
                    echo($sqlsp . "<br>");
                    outputJSON($params . "<br>");
                    outputJSON(sqlsrv_errors());
                    die;
                }
                if ($ARDCd == "Exists") {
                    $resp["msg"] = "Call Already Exists";
                    $resp["success"] = false;
                    echo json_encode($resp);
                    die;
                }
            }
            break;
    }
    $resp["success"] = true;
    echo json_encode($resp);
}

$axn = $_GET['axn'];
$value = explode(":", $axn);
switch ($value[0]) {
    case "login":
        actionLogin();
        break;
    case "deleteEntry":
        $data = json_decode($_POST['data'], true);
        $arc = (isset($data['arc']) && strlen($data['arc']) == 0) ? null : $data['arc'];
        $amc = (isset($data['amc']) && strlen($data['amc']) == 0) ? null : $data['amc'];
        $result = deleteEntry($arc, $amc);
        break;
    case "get/doctorCount":
        outputJSON(getDoctorPCount());
        break;
    case "get/setup":
        outputJSON(getAPPSetups());
        break;
 
    case "imgupload":
        $sf = $_GET['sf_code'];
        print_r(sf);
        move_uploaded_file($_FILES["imgfile"]["tmp_name"], "../photos/" . $sf . "_" . $_FILES["imgfile"]["name"]);
        break;
    case "get/jointwork":
        outputJSON(getJointWork());
        break;
    case "get/subordinate":
        outputJSON(getSubordinate());
        break;
    case "get/submgr":
        outputJSON(getSubordinateMgr());
        break;
    case "get/uldoctorCount":
        outputJSON(getUlDoctorPCount());
        break;
    case "get/chemistCount":
        outputJSON(getChemistPCount());
        break;
    case "get/stockistCount":
        outputJSON(getStockistPCount());
        break;
    case "table/list":
        $results;
        $data = json_decode($_POST['data'], true);
        $sfCode = $_GET['sfCode'];
        $RSF = $_GET['rSF'];
        $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        switch (strtolower($data['tableName'])) {
            case "mas_worktype":
                $query = "exec GetWorkTypes_App '" . $RSF . "'";
                $results = performQuery($query);
for($i=0;$i<count($results);$i++) {
if($results[$i]['name']=="Leave"){
unset( $results[$i]);
   
}
}
$results = array_values($results);
                break;
            case "product_master":
                $results = getProducts();
                break;
            case "category_master":
                $query = "exec GetProdBrand_App '" . $div . "'";
                $results = performQuery($query);
                break;
            case "gift_master":
                $query = "exec getAppGift '" . $sfCode . "'";
                $results = performQuery($query);
                break;
            case "doctor_category":
                $query = "select Doc_Cat_Code id,Doc_Cat_Name name from Mas_Doctor_Category where Division_code='" . $Owndiv . "' and Doc_Cat_Active_Flag=0";
                $results = performQuery($query);
                break;
            case "doctor_specialty":
                $query = "select Doc_Special_Code id,Doc_Special_Name name from Mas_Doctor_Speciality where Division_code='" . $Owndiv . "' and Doc_Special_Active_Flag=0";
                $results = performQuery($query);
                break;
            case "mas_doc_class":
                $query = "select Doc_ClsCode id,Doc_ClsSName name from Mas_Doc_Class where Division_code='" . $Owndiv . "' and Doc_Cls_ActiveFlag=0";
                $results = performQuery($query);
                break;
            case "mas_doc_qualification":
                $query = "select Doc_QuaCode id,Doc_QuaName name from Mas_Doc_Qualification where Division_code='" . $Owndiv . "' and Doc_Qua_ActiveFlag=0";
                $results = performQuery($query);
                break;
case "vwfolders":
            $result = array();
            $sql = "select Move_MailFolder_Id id,Move_MailFolder_Name name from Mas_Mail_Folder_Name where division_code='$Owndiv'";
          
$result = performQuery($sql);

            array_unshift($result, array("id" => "inbox", "name" => "Inbox"), array("id" => "sent", "name" => "Sent Item"), array("id" => "view", "name" => "Viewed"));
            outputJSON($result);
 
            die;
break;
        case "getmailsf":
            $sfCode = $_GET['sfCode'];
            $divCode = $_GET['divisionCode'];
            $sql = "exec getFullHryList '$sfCode'";
            $mailsSF = performQuery($sql);
           /* foreach ($mailsSF as $k => $v) {
                $mailsSF[$k] ['id'] = $mailsSF[$k] ['sf_code'];
                unset($mailsSF[$k]['sf_code']);
                $mailsSF[$k] ['name'] = $mailsSF[$k] ['Sf_Name'];
                unset($mailsSF[$k]['Sf_Name']);
            }*/
            outputJSON($mailsSF);
            die;
        break;
                case "vwtourplan":
$sfCode=$_GET['rSF'];
$current=array();
$next=array();
$previous=array();
                $query = "select  * from vwTourPlan where SF_Code='$sfCode' and worktype_code!='0'";
                $next = performQuery($query);
$query = "select  * from vwTourPlan_current where SF_Code='$sfCode'  and worktype_code!='0'";
                $current = performQuery($query);
$query = "select  * from vwTourPlan_previous where SF_Code='$sfCode'  and worktype_code!='0'";
                $previous = performQuery($query);
$result=array();
$result['current']=$current;
$result['next']=$next;
$result['previous']=$previous;
                outputJSON($result);
                die;
                break;
            case "vwleavetype":

                $query = "select Leave_Code id,Leave_SName name,Leave_Name from vwLeaveType where Division_code='" . $Owndiv . "'";
                $results = performQuery($query);
                break;
            case "vwactivity_csh_detail":
                $or = (isset($data['or']) && $data['or'] == 0) ? null : $data['or'];
                $where = isset($data['where']) ? json_decode($data['where']) : null;
                $query = "select * from vwActivity_CSH_Detail where Trans_Detail_Info_Type=" . $or . " and " . join(" or ", $where) . " order by vstTime";
                $results = performQuery($query);
                break;
            default:
                $sfCode = (isset($data['sfCode']) && $data['sfCode'] == 0) ? null : $_GET['sfCode'];
                $divisionCode = (int) $Owndiv;
                //$divisionCode = 1;

                $today = (isset($data['today']) && $data['today'] == 0) ? null : $data['today'];
                $or = (isset($data['or']) && $data['or'] == 0) ? null : $data['or'];
                $wt = (isset($data['wt']) && $data['wt'] == 0) ? null : $data['wt'];
                $tableName = $data['tableName'];
                $coloumns = json_decode($data['coloumns']);

                $where = isset($data['where']) ? json_decode($data['where']) : null;

                $join = isset($data['join']) ? $data['join'] : null;
                $orderBy = isset($data['orderBy']) ? json_decode($data['orderBy']) : null;

                if (!is_null($or)) {
                    $results = getFromTableWR($tableName, $coloumns, $divisionCode, $sfCode, $orderBy, $where, $join, $today, $wt);
                } else {
                    $results = getFromTable($tableName, $coloumns, $divisionCode, $sfCode, $orderBy, $where, $join, $today, $wt);
                }
                break;
        }
        outputJson($results);
        break;
    case "dcr/updateEntry":
        $data = json_decode($_POST['data'], true);
        $arc = (isset($_GET['arc']) && strlen($_GET['arc']) == 0) ? null : $_GET['arc'];
        $amc = (isset($_GET['amc']) && strlen($_GET['amc']) == 0) ? null : $_GET['amc'];
        deleteEntry($arc, $amc);
        addEntry();
        break;
    case "dcr/callReport":
        outputJSON(getCallReport());
        break;
    case "tpview":
        getTPview();
        break;
    case "tpviewdt":
        getDtTPview();
        break;
    case "dcr/updRem":
        updEntry();
        break;
case "error/data":
		$myfile = fopen("../server/errorfile.txt", "a+");
		$txt = "John Doe\n";
		$sqlsp=$_POST['data']." message:".$_GET['msg']."sf_code=".$_GET['sfCode']."\n";
		fwrite($myfile, $sqlsp);
		fclose($myfile);
		die;
		break;
    
    case "dcr/save":
        addEntry();
        break;
case "get/doctor":
        getDoctorDet();
        break;

 case "mailView":
$date=date('Y-m-d H:i:s');
        $id = $_GET['id'];
        $sql = "update trans_mail_detail set Mail_Active_Flag='10',Mail_Read_Date='$date' where Trans_Sl_No=$id";
        performQuery($sql);
        $result['success'] = true;
        outputJSON($result);
        break;
    case "mailMove":
        $folder = $_GET['folder'];
        $id = $_GET['id'];
$date=date('Y-m-d H:i:s');
        $sql = "update trans_mail_detail set Mail_moved_to='$folder',Mail_Active_Flag='12',mail_moved_date='$date' where Trans_Sl_No=$id";
        performQuery($sql);
        $result['success'] = true;
        outputJSON($result);
        break;
    case "mailDel":
        $folder = $_GET['folder'];
$date=date('Y-m-d H:i:s');
        $id = $_GET['id'];
        if ($folder == "Sent")
            $sql = "update MailBox_Details set Mail_SentItem_DelFlag=1 where Trans_Sl_No=$id";
        else
            $sql = "update trans_mail_detail set Mail_Active_Flag='-1',mail_delete_date='$date' where Trans_Sl_No=$id";

        performQuery($sql);
        $result['success'] = true;
        outputJSON($result);
        break;

case "createMail":
        $sf = $_GET['sfCode'];
        $date = date('Y-m-d H:i');
        $divCode = $_GET['divisionCode'];
        $file = $_POST['fileName'];
        if (!empty($file)) {
            $info = pathinfo($file);
            $file_name = basename($file, '.' . $info['extension']);
            $ext = $info['extension'];
            $fileName = $file_name . "_" . $sf . "_" . date('d_m_Y') . "." . $ext;
        } else
            $fileName = "";
        $msg1 = urldecode($_POST['message']);
        $msg = trim($msg1, '"');
        $sub1 = urldecode($_POST['subject']);
        $sub = trim($sub1, '"');
$sql="select max(isnull(Trans_sl_no,0))+1 transslno from trans_mail_head";
  $tr=performQuery($sql);
$transslno=$tr[0]['transslno'];
        $sql = "insert into trans_mail_head(Trans_sl_no,System_ip,Mail_SF_From,Mail_SF_To,Mail_Subject,Mail_Content,Mail_Attachement,Mail_CC,Mail_BCC,Division_Code,Mail_Sent_Time,To_SFName,CC_Sfname,Bcc_SfName,Mail_SF_Name,sent_flag)
        select '$transslno','','$sf'," . $_POST['to_id'] . ",'$sub','$msg','$fileName'," . $_POST['cc_id'] . "," . $_POST['bcc_id'] . ",'$Owndiv','$date','" . $_POST['to'] . "'," . $_POST['cc'] . "," . $_POST['bcc'] . ",'" . $_POST['from'] . "',0";
   
   performQuery($sql);
      
      
        $ToCcBcc = explode(', ', $_POST['ToCcBcc']);
        for ($i = 0; $i < count($ToCcBcc); $i++) {
            if ($ToCcBcc[$i]) {
                //  Mail_int_Det_No,Mail_View_Color
                $sql = "insert into trans_mail_detail(Trans_Sl_no,open_mail_id,mail_active_flag,Division_code)
                                                   select '$transslno','" . $ToCcBcc[$i] . "',0,'$Owndiv'";

                performQuery($sql);
            }
        }
        $result["success"] = true;
        outputJSON($result);
        break;
    case "fileAttachment_mail":
        $sf = $_GET['sf_code'];
        $file = $_FILES['imgfile']['name'];
        $info = pathinfo($file);
        $file_name = basename($file, '.' . $info['extension']);
        $ext = $info['extension'];
        $fileName = $file_name . "_" . $sf . "_" . date('d_m_Y') . "." . $ext;
        $file_src = '../MasterFiles/Mails/Attachment/' . $fileName;
        move_uploaded_file($_FILES['imgfile']['tmp_name'], $file_src);
        break;
case "fileAttachment":
 $doctorCode = $_GET['doctor_code'];
        $file = $_FILES['imgfile']['name'];
        $info = pathinfo($file);
        $file_name = basename($file, '.' . $info['extension']);
        $ext = $info['extension'];
        $fileName =$doctorCode . "_" . date('d-m-Y') . "_". $file_name . "." . $ext;
        $file_src = '../Visiting_Card/' . $fileName;
        move_uploaded_file($_FILES['imgfile']['tmp_name'], $file_src);
break;
    case "get/precall":
        getPreCallDet();
        break;
    case "get/MnthSumm":
        outputJSON(getMonthSummary());
        break;
    case "get/DayRpt":
        outputJSON(getDayReport());
        break;
    case "get/vwVstDet":
        outputJSON(getVstDets());
        break;
 case "getDoctor_Dob_Dow":
        $sfCode = $_GET['sfCode'];
 //$sfCode = $_GET['sf'];
        $sql = "exec getDoctor_Dob_Dow '$sfCode'";
        $result = performQuery($sql);
        outputJSON($result);
        break;
    case "vwLeaveStatus":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwLeaveEntitle where Sf_Code='$sfCode'";
        $leave = performQuery($sql);
        outputJSON($leave);
        break;
  case "getMailsApp":
        $sfCode = $_GET['sfCode'];
          $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $folder = $_GET['folder'];
        $month = $_GET['month'];
        $year = $_GET['year'];
       // $sql = "exec GetMails '$sfCode','$Owndiv','','$folder',$month,$year";
$fldr=$folder;
if($folder!='Inbox'&&$folder!='Sent Item'&&$folder!='Viewed'){
$folder='Flder';
}

$sql = "exec MailInbox_App '$sfCode','$Owndiv','$folder','$fldr','$year','$month', 'Mail_Sent_Time', 'Desc',''";
//print_r($sql);die;
        $mails = performQuery($sql);
        outputJSON($mails);
        die;
        break;
 case "vwMedUpdateUpload":
        $sfCode = $_GET['sfCode'];
   $div = $_GET['divisionCode'];
        $divs = explode(",", $div . ",");
        $Owndiv = (string) $divs[0];
        $sql = "select * from vwMedUpdateUpload where Division_Code='$Owndiv'";
//print_r($sql);die;
        $medUpload = performQuery($sql);
        outputJSON($medUpload);
        die;
        break;
case "vaccancyList":
            
$divCode = $_GET['divisionCode'];
            $sql = "select Sf_HQ HQName,sf_name from  Mas_Salesforce where sf_tp_active_flag=1 and sf_type=1 and division_code='$divCode'";
            $vaccancyList = performQuery($sql);

              outputJSON($vaccancyList);
            die;
        break;
      case "vwLeave":
            $sfCode = $_GET['sfCode'];
            $sql = "select * from vwLeave where Reporting_To_SF='$sfCode'";
        
            $leave = performQuery($sql);
            outputJSON($leave);
            break;
    case "vwCheckLeave":
        $sfCode = $_GET['sfCode'];
        $date = date('Y-m-d');
        $sql = "select From_Date,To_Date,No_of_Days from mas_Leave_Form where To_Date>='$date' and sf_code='$sfCode' order by From_Date";
        $leaveDays = performQuery($sql);
        $currentDate = date_create($date);
        $disableDates = array();
        $sql = "SELECT * FROM vwActivity_Report where SF_Code='" . $sfCode . "' and cast(activity_date as datetime)=cast('$date' as datetime)";
        $dcrEntry = performQuery($sql);
        if (count($dcrEntry) > 0)
            array_push($disableDates, $currentDate->format('d/m/Y'));
        for ($i = 0; $i < count($leaveDays); $i++) {
            $fromDate = $leaveDays[$i]['From_Date'];
            $toDate = $leaveDays[$i]['To_Date'];
            $noOfDays = $leaveDays[$i]['No_of_Days'];
            if ($currentDate > $fromDate)
                $fromDate = $currentDate;
            $diff = date_diff($fromDate, $toDate, TRUE);
            $days = $diff->format("%a") + 1;
            for ($j = 0; $j < $days; $j++) {
                array_push($disableDates, $fromDate->format('d/m/Y'));
                $fromDate->modify('+1 day');
            }
        }
        outputJSON($disableDates);
        break;
         case "vwChkTransApproval":
        $sfCode = $_GET['sfCode'];
        $sql = "select * from vwChkTransApproval where Reporting_To_SF='$sfCode'";
        $tp = performQuery($sql);
        outputJSON($tp);
        break;
    case "vwChkTransApprovalOne":
        $sfCode = $_GET['code'];
        $month = $_GET['month'];
        $year = $_GET['year'];
        $sql = "select Objective,Worked_With_SF_Name,Worktype_Name_B,Worktype_Name_B1,Worktype_Name_B2,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,convert(varchar,Tour_Date,103) Tour_Date from Trans_TP_One where Sf_Code='$sfCode' order by Tour_Date";
        $tp = performQuery($sql);
        outputJSON($tp);
        break;

         case "vwDcr":
        $sfCode = $_GET['sfCode'];
        $sql = "select d.Plan_Name,d.Trans_SlNo,d.Sf_Code,d.FieldWork_Indicator,d.WorkType_Name,d.Sf_Name,convert(varchar,Activity_Date,103) Activity_Date,s.Reporting_To_SF from DCRMain_Temp d
            inner join Mas_Salesforce_One s on d.Sf_Code=s.Sf_Code
             where d.Confirmed=1 and s.Reporting_To_SF='$sfCode' and Activity_Date<cast(GETDATE() as date)";
        $dcr = performQuery($sql);
        outputJSON($dcr);
        break;
    case "vwDcrOne":
        $TransSlNo = $_GET['Trans_SlNo'];
        $sql = "exec getDCRApprovalApp '" . $TransSlNo . "'";
        $dcr = performQuery($sql);
        outputJSON($dcr);

        break;
    case "entry/count":
        $today = date('Y-m-d 00:00:00');
        $sfCode = $_GET['sfCode'];

        $sql = "SELECT Employee_Id,case sf_type when 1 then 'MR' else 'MGR' End SF_Type FROM Mas_Salesforce_One where SF_code='" . $sfCode . "'";
        $as = performQuery($sql);
        $SFTyp = (string) $as[0]['SF_Type'];

        $query = "SELECT work_Type worktype_code,Remarks daywise_remarks,Half_Day_FW halfdaywrk from vwActivity_Report H where SF_Code='" . $sfCode . "' and FWFlg <> 'F' and cast(activity_date as datetime)=cast('$today' as datetime)";
        $data = performQuery($query);
        $result = array();
        if (count($data) > 0) {
            $result["success"] = false;
            $result['data'] = $data;
            outputJSON($result);
            die;
        }
        $result["success"] = true;
        $result['data'] = getEntryCount();
        outputJSON($result);
        break;
    case "save/trackloc":
		$data1 = json_decode($_POST['data'], true);
		$TrcLocs=$data1[0]['TrackLoction'];
		$sfCode=$TrcLocs['SF_code'];
		$TLocs=$TrcLocs['TLocations'];
		$sql = "select sf_emp_id,Employee_Id from Mas_Salesforce where Sf_Code='$sfCode'";
        $sf = performQuery($sql);
        $empid = $sf[0]['sf_emp_id'];
        $employeeid = $sf[0]['Employee_Id'];
		for($ik=0;$ik<count($TLocs);$ik++){
        	$lng = $TLocs[$ik]['Longitude'];
        	$lat = $TLocs[$ik]['Latitude'];
			
        	$address = getaddress($lat, $lng);
        	$sql = "insert into tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr,Auc,deg,DvcID) select '$sfCode','$empid','$employeeid','" . $TLocs[$ik]['Time'] . "','$lat','$lng','','" . str_replace("'", "", $TLocs[$ik]['Accuracy']) . "','" . str_replace("'", "", $TLocs[$ik]['Bearing']) . "','" . str_replace("'", "", $TrcLocs['DvcID']) . "'";
			
        	performQuery($sql);
		}
        break;
}
?>