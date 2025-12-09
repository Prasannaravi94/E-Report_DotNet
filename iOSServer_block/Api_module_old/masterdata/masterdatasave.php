<?php
function masterdatasave() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
	// get dcr master data here
	
	
	switch ( strtolower( $data[ 'tableName' ] ) ) {
		case "saveeditdatestatus":
	        $query ="exec iOS_svEditDates_Edet '".$data['sfcode']."','".date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']))."'";
			outputJSON( performQuery($query ) );
            break; 
        case "savenewdr":
	        $query="exec ios_svNewCustomer_App_Edet 0,'','".str_replace("'","",$data['DrName'])."','". $data["DrAddr"]."','".$data["DrTerCd"]."','".$data["DrTerNm"]."','".$data["DrCatCd"]."','".$data["DrCatNm"]."','". $data["DrSpcCd"]."','".$data["DrSpcNm"]."','".$data["DrClsCd"]."','".$data["DrClsNm"]."','".$data["DrQCd"]."','".$data["DrQNm"]."','U','".$data["sfcode"]."','','','".$data["DrPincd"]."','".$data["DrPhone"]."','".$data["DrMob"]."','".$data["Uid"]."'";
   	        $output=performQuery($query);
	        $result["Qry"]=$output[0]['Msg'];
            $result["success"]=true;
            outputJSON($result);
            break;        
        case "savenewchm":
			$query="exec ios_svNewCustomer_App_Edet 0,'','".str_replace("'","",$data['DrName'])."','". $data["DrAddr"]."','".$data["DrTerCd"]."','','','','','','','','','','C','".$data["sfcode"]."','','','','','','0'";
        	$output=performQuery($query);
			$result["EQry"]=$query;
			$result["Qry"]=$output;
			$result["success"]=true;
			outputJSON($result);
            break; 
        case "savedrprofile":
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
			$query="exec iOS_svDrProfile_Edet '".$data['CustCode']."','".$data['DrQual']."','".$data['DrSpec']."','".$data['DrCat']."','".$data['DrGender']."','$DOB','$DOW','".$data['DrAdd1']."','".$data['DrAdd2']."','".$data['DrAdd3']."','".$data['DrAdd4']."','".$data['DrAdd5']."','".$data['DrPhone']."','".$data['DrMob']."','".$data['DrEmail']."','".$data['DrType']."','".$data['DrTar']."','".$sProdv."','".$sVstDet."','".$sVSes."','".$sVAvgP."','".$sVClsP."'";
			$result["HQry"]=$query;
			//echo $query;
			performQuery($query);
			
			$result['success']=true;
			outputJSON($result);
		  break; 			
       case "savedrquery":
         $query="exec iOS_svDrQueries_Edet '". $data['sfcode']."','".$data['DeptCode']."','".$data['DeptName']."','".$data['DrCode']."','".$data['DrName']."','".$div_code[0] ."','".$data['QryMsg']."','F','".$data['QryDt']."','".$data['QryID']."'";
         $result["HQry"]=$query;
          performQuery($query);
		 $result['success']=true;
         outputJSON($result);
		 break; 
	  case "savechpwd":
         	$result=array();
	      $query = "select Sf_Code,Sf_Name,Sf_UserName,Sf_Password,Sf_Joining_Date,Reporting_To_SF,TP_Reporting_SF,State_Code,Sf_TP_DCR_Active_Dt,SF_ContactAdd_One,SF_ContactAdd_Two,SF_City_Pincode,SF_Email,SF_Mobile,SF_DOB,SF_Per_ContactAdd_One,SF_Per_ContactAdd_Two,SF_Per_City_Pincode,SF_Per_Contact_No,SF_Cat_Code,SF_Status,Sf_HQ,sf_TP_Active_Dt,sf_TP_Deactive_Dt,sf_TP_Active_Flag,Division_Code,Created_Date,sf_Sl_No,sf_type,sf_emp_id,sf_short_name,sf_desgn,LastUpdt_Date,sf_BlkReason,subdivision_code,Last_DCR_Date,Last_TP_Date,SF_DOW,Approved_By,Designation_Code,SF_VacantBlock,IsMultiDivision,Employee_Id,sf_Designation_Short_Name,UsrDfd_UserName,Sf_Date_Confirm,Fieldforce_Type,Sf_Deactivate_Permanent,Sf_DCRSample_Valid,Sf_DCRInput_Valid from Mas_Salesforce where Sf_Code='".$data['sfcode']."' and Sf_Password='".$data['txOPW']."'";	
	      $res=performQuery($query);
	      if (count($res)>0) 
	      {
		 $query = "update Mas_Salesforce set Sf_Password='". $data['txNPW']."' where Sf_Code='".$data['sfcode']."' and Sf_Password='".$data['txOPW']."'";
		 $res=performQuery($query);
		 $result['success']=true;
	     }
	     else
	     {
		  $result['success']=false;
		  $result['msg']="The given old password not correct";
	    }
           outputJSON($result);
		 break; 
      default:			
            //echo 'Not Match';
			$result = array();
			$result['success'] = false;
			$result['msg'] = 'Try Again';
			outputJSON($result);
            break;
}}
?>
