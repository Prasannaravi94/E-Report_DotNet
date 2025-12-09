<?php
function surveyget(){ 
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
	
	// get dcr master data here

	switch ( strtolower( $data[ 'tableName' ] ) ) {
		case "getsurvey":

			$survey_details=[];
			$query = "select Survey_ID id,Survey_Title name,CONVERT(varchar,Effective_From_Date,23) as from_date,CONVERT(varchar,Effective_To_Date,23) as to_date from Mas_Question_Survey_Creation_Head where division_code='".$div_code[0]."' and Close_flag='0' and Active_Flag='0' and cast(effective_from_date as date)<=cast(GETDATE() as date) and cast(effective_to_date as date)>=cast(GETDATE() as date) order by Survey_ID desc"; 
			$surveytitle = performQuery($query);
			//echo $query;
			for($i=0;$i<count($surveytitle);$i++){
				$survey_id = $surveytitle[$i]['id']; 
				$query = "select Question_Id id,Survey_ID Survey,Doctor_Category DrCat,Doctor_Speclty DrSpl,Doctor_Cls DrCls,Hospital_Class HosCls,Chemist_Category ChmCat,Stockist_State Stkstate,Stockist_HQ StkHQ,Processing_Type Stype from Mas_Question_Survey_Creation_Detail where division_code='".$div_code[0]."' and Survey_id='$survey_id' and  charindex(','+'".$data['sfcode']."'+',',','+SF_Code+',')>0"; 
								
				$surveyfor = performQuery($query);
				$survey_details[$i]= $surveytitle[$i];
				$survey_details[$i]['survey_for'] =[];
				for($j=0;$j<count($surveyfor);$j++){
					$Survey=$surveyfor[$j]['Survey'];
					//$survey_details[$i]['survey_for'] ='';
					if($survey_id==$Survey){
						$query = "select sc.Question_Id id,Survey_ID Survey,Doctor_Category DrCat,Doctor_Speclty DrSpl,Doctor_Cls DrCls,Hospital_Class HosCls,Chemist_Category ChmCat,Stockist_State Stkstate,Stockist_HQ StkHQ,Processing_Type Stype,Control_Id Qc_id,Control_Name Qtype,Control_Para Qlength,'0' Mandatory,Question_Name Qname,Question_Add_Names Qanswer,Active_Flag from Mas_Question_Survey_Creation_Detail sc
						 inner join Mas_Question_Creation qc on qc.Question_Id=sc.Question_Id
						 where sc.division_code='".$div_code[0]."' and Survey_id='$survey_id' and  charindex(','+'".$data['sfcode']."'+',',','+SF_Code+',')>0";
						$ssurveydetail  = performQuery($query);

						$survey_details[$i]['survey_for']=$ssurveydetail;
					}
				}
			}
			//return $survey_details;
			outputJSON($survey_details);
		break; 
		case "savesurvey":
			$result=[];
			$SurveyDatas=$data['val'];
			for ($Ri = 0; $Ri < count($SurveyDatas); $Ri++) 
			{
				$sVal=$SurveyDatas[$Ri];
				$sDRCd="0";$sChmCd="0";
				if($sVal['CustType']=="D") $sDRCd=$sVal['CustCode'];
				if($sVal['CustType']=="C") $sChmCd=$sVal['CustCode'];
				$mn=date('m',strtotime($sVal['SurveyDate']));
				$yr=date('Y',strtotime($sVal['SurveyDate']));
				$sql="exec iOS_sv_Survey_Edet '" . date('Y-m-d 00:00:00')."','".$div_code[0]."','" . $sVal['Survey_Id'] . "','" . $sVal['Question_Id'] . "','".$data['sfcode']."','" . $sDRCd . "','" . $sChmCd . "','" . $mn . "','" . $yr . "','" .date('Y-m-d H:i:s')."','" . $sVal['Answer'] . "'"; 
				performQuery($sql);	
			}
			
			$result['success']=true;
		    outputJSON($result);		
			break;
		default:
				//echo 'Not Match';
				$result = array();
				$result['success'] = false;
				$result['msg'] = 'Try Again';
				outputJSON($result);
			break;
			} 
}

?>