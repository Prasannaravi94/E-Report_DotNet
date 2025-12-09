<?php
function dcrdelete($amc,$typ) {
	    if (!is_null($amc)) {
		//changes made by bala to remove inner join of dcr view procedures
		if($typ=="1"){		
			$sql = "select cast(Trans_Detail_Info_Code as varchar) doctor_code,Trans_SlNo  from vwActivity_MSL_Details  where Trans_Detail_Slno ='" . $amc . "'";
			$sqldet=performQuery($sql);
			$ARCd=$sqldet[0]["Trans_SlNo"];
			$doc_code=$sqldet[0]["doctor_code"];
		}
		else if($typ=="2"){
			$sql = "select cast(Trans_Detail_Info_Code as varchar) doctor_code,Trans_SlNo  from vwActivity_CSH_Detail  where Trans_Detail_Slno ='" . $amc . "'";
			$sqldet=performQuery($sql);
			$ARCd=$sqldet[0]["Trans_SlNo"];
			$doc_code=$sqldet[0]["doctor_code"];
		}
		else if($typ=="3"){
			$sql = "select cast(Trans_Detail_Info_Code as varchar) doctor_code,Trans_SlNo  from vwActivity_CSH_Detail  where Trans_Detail_Slno ='" . $amc . "'";
			$sqldet=performQuery($sql);
			$ARCd=$sqldet[0]["Trans_SlNo"];
			$doc_code=$sqldet[0]["doctor_code"];
		}
		else if($typ=="4"){
			$sql = "select cast(Trans_Detail_Info_Code as varchar) doctor_code,Trans_SlNo  from vwActivity_Unlst_Detail  where Trans_Detail_Slno ='" . $amc . "'";
			$sqldet=performQuery($sql);
			$ARCd=$sqldet[0]["Trans_SlNo"];
			$doc_code=$sqldet[0]["doctor_code"];
		}else if($typ=="6"){
			$sql = "select cast(Trans_Detail_Info_Code as varchar) doctor_code,Trans_SlNo  from vwActivity_CIP_Details  where Trans_Detail_Slno ='" . $amc . "'";
			$sqldet=performQuery($sql);
			$ARCd=$sqldet[0]["Trans_SlNo"];
			$doc_code=$sqldet[0]["doctor_code"];
		}
		else{
			$ARCd="0";
			$doc_code="0";
		}
		//$sql = "DELETE s  from tbDgDetailing_SlideDetails s inner join tbDigitalDetailing_Head h on s.DDSl_No=h.DDSl_No inner join vwActivity_MSL_Details d on h.Activity_Report_code=d.Trans_SlNo AND cast(h.MSL_code as varchar)=cast(d.Trans_Detail_Info_Code as varchar) where Trans_Detail_Slno='" . $amc . "'";
		$sql = " DELETE s from tbDgDetailing_SlideDetails s inner join tbDigitalDetailing_Head h on s.DDSl_No=h.DDSl_No  where h.Activity_Report_code='".$ARCd."' and h.msl_code='".$doc_code."'";
		performQuery($sql);
		
       // $sql = "DELETE h from tbDigitalDetailing_Head h inner join vwActivity_MSL_Details d on h.Activity_Report_code=d.Trans_SlNo AND cast(h.MSL_code as varchar)=cast(d.Trans_Detail_Info_Code as varchar) where Trans_Detail_Slno='" . $amc . "'";
        $sql = "DELETE from tbDigitalDetailing_Head where Activity_Report_code='".$ARCd."' and msl_code='".$doc_code."'";		
		performQuery($sql);
		
		// $sql="delete DD from vwActivity_CSH_Detail  CSHD inn er join tbDigitalDetailing_Head DD on CSHD.Trans_SlNo=Activity_Report_code and Trans_Detail_Info_Code=MSL_code   where Trans_Detail_Slno='" . $amc . "'";
		// performQuery($sql);
		
		// $sql="delete DD from vwActivity_Unlst_Detail  CSHD inner join tbDigitalDetailing_Head DD on CSHD.Trans_SlNo=Activity_Report_code and Trans_Detail_Info_Code=MSL_code   where Trans_Detail_Slno='" . $amc . "'";
		// performQuery($sql);
		
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
		$sql = "DELETE FROM DCRDetail_Cip_Temp where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
        $sql = "DELETE FROM DCRDetail_Cip_Trans where Trans_Detail_Slno='" . $amc . "'";
        performQuery($sql);
		$sql="delete DD from Trans_RCPA_Detail  DD inner join Trans_RCPA_Head HD on HD.PK_ID=DD.FK_PK_ID where ARMSL_Code='" . $amc . "'";
		performQuery($sql);
		$sql="delete from Trans_RCPA_Head where ARMSL_Code='" . $amc . "'";
		performQuery($sql);
    }
	else{
		$result = array();
		$result['success'] = false;
		$result['msg'] = 'AMC Code Empty';
		outputJSON($result);
	}
}
?>