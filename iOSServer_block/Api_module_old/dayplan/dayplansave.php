<?php
function dayplansave() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$DivCode = explode(",", $div_codes.",");
	$InsMode=(string) $data['InsMode'];
	
		   $query="select Trans_SlNo,Work_Type,WorkType_Name,isnull(FWFlg,'F') FWFlg,Half_Day_FW,Typ from vwActivity_Report where Sf_Code='".$data['sfcode']."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,cast ('".$data['TPDt']."' as datetime),101) as datetime) and Work_Type<>'".$data['WT_code']."'";
		   //echo $query;
			$ExisArr1 = performQuery($query);
			// echo $ExisArr1[0]['FWFlg'];
			// echo count($ExisArr1)>0;
			// echo $InsMode;
	        if ($ExisArr1[0]['FWFlg']=='L'){
			  $result["Msg"]="Today Already Leave Posted...";
			  $result["success"]=false;
			  outputJSON($result);
	        }else{        
			
           // $result["cqry"]=$query;
		    if (count($ExisArr1)>"0" && $InsMode=="0"){
		    	$result["Msg"]="Already you are submitted your work. Now you are deviate. Do you want continue?";
		    	$result["update"]=true;
			    $result["success"]=false;
		    }
		    else{
		    	$query="exec iOS_svTodayTPNew_Edet '".$data['sfcode']."','".$data['Rsf']."','".$data['town_code']."','".$data['Town_name']."','".$data['WT_code']."','".$data['WTName']."','".$data['Remarks']."','".$data['location']."','".$data['TPDt']."','".$data['TpVwFlg']."','','".$data['TP_cluster']."','".$data['TP_worktype']."','" . $data['sf_type'] . "','" . $DivCode[0] . "','" . $data['state_code'] . "','".$data['Mod']."'";
		    	performQuery($query);
				//echo $query;
			if ($InsMode=="2")
			{
                if($ExisArr1[0]["Typ"]=="0")	
                    $query="update DCRMain_Temp set ";
                else					
			    	$query="update DCRMain_Trans set ";
				if($ExisArr1[0]["FWFlg"]!="F"  ){
					$ExisArr1[0]["Half_Day_FW"] = $ExisArr1[0]["Half_Day_FW"] . $ExisArr1[0]["WorkType_Name"] . ",";
					$query=$query." Work_type='" . $data['WT_code'] . "',FieldWork_Indicator='".$data["FwFlg"]."',WorkType_Name='" . $data['WTName'] . "',";
				}else{
					$ExisArr1[0]["Half_Day_FW"] = $ExisArr1[0]["Half_Day_FW"] . $data['WTName'] . ",";
				}
				$query=$query."Half_Day_FW='" . $ExisArr1[0]["Half_Day_FW"] . "',Remarks='".$data['Remarks']."' where Sf_Code='".$data['sfcode']."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,cast('".$data['TPDt']."' as datetime),101) as datetime)";
				$result["sqry"]=$query;
				performQuery($query);
                //performQuery(str_replace("DCRMain_Trans", "DCRMain_Temp", $query));
			}
			else
			{ 
				if ($InsMode=="1")
				{
					$query="exec ios_DelDCRTempByDt_Edet '".$data['sfcode']."','" . date('Y-m-d 00:00:00.000', strtotime($data['TPDt']))."'";
					performQuery($query);
					
					
				}
        
			$query="exec ios_svDCRMain_Edet '" .$data['sfcode']. "','" . date('Y-m-d 00:00:00.000', strtotime($data['TPDt'])) . "','" . $data['WT_code'] . "','" . $data['WTName'] . "','" . $data['FwFlg'] . "','" . $data['town_code'] . "','".$data['Town_name']."','" . $DivCode[0] . "','" . $data['Remarks'] . "','" . $data['sf_type'] . "','" . $data['state_code'] . "','','".$data['Mod']."'";
				performQuery($query);
				//echo $query;
				
			}
			$result["Msg"]="Today Work Plan Submitted Successfully...";
			$result["success"]=true;
		}
			 outputJSON($result);
	    
        
    } 
}
?>


