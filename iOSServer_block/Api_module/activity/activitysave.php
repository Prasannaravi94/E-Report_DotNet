<?php
function activitysave() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");

	//get approvals
	switch ( strtolower( $data[ 'tableName' ] ) ) {

        case "savetp_attendance":
		     global $data;
			
		    $dateTime = $data['DateTime'];
            $dateTime = date( 'Y-m-d H:i' );			
            $date = date('Y-m-d');
			$date = date('Y-m-d', strtotime($data['DateTime']));
			$mod=(string) $data['Mod'];
			if($mod==""){
				$mod="iOS";
			}            
            //$update =(string)  $data['update'];
			$Appver = (string) $data['Appver'];
			 $query="select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='".$data['sfcode']."' and cast(convert(varchar,Activity_Date,101) as datetime)=cast(convert(varchar,cast('".$dateTime."' as datetime),101) as datetime) and FWFlg='L'";
			$ExisArr = performQuery($query);
			//echo $query;
			
			if ($ExisArr[0]["Cnt"]>0){

			        $result1 = [];
					$result1[ "msg" ] = "Already Leave Posted For this date...";
					$result = [];
					$result[0]=$result1;
			  
			}else{
            
				if ( $data['update'] == 0 ) {
					$sql = "exec iOS_Attendance_entry_Edet '".$data['sfcode']."','".$data['division_code']."','".$dateTime."','".$data['lat']."','".$data['long']."','".$date."','".$data['address']."'";
					
					$result = performQuery( $sql );
					//echo $sql;
					if(($data['division_code']=="10")||($data['division_code']=="8")||($data['division_code']=="40")){
						if(($data['lat']==""||$data['long']=="")||($data['lat']=="0.0"||$data['long']=="0.0")||($data['lat']==null||$data['long']==null)){
						
						}else{				
							//$queryss="select sf_emp_id,Employee_Id,Sf_Name from Mas_Salesforce_two with (nolock) where  sf_code='".$$data['sfcode']."'";
							//$sf_data=performQuery($queryss);
							$queryqq="insert into sf_tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr,Auc,EMod,Battery,Mock,SF_Mobile,updatetime,IsOnline,Division_code,App_version,Track_type,DCR_ID,Dcr_Detail_id,DCRcall_id,Dcrcall_name,DCR_time,sf_name) select '".$data['sfcode']."','".$data['sf_emp_id']."','".$data['Employee_Id']."','".$dateTime."','".$data['lat']."','".$data['long']."','".$data['address']."','','".$data['Mod']."','','','','".$dateTime."','online','".$data['division_code']."','".$data['Appver']."','IN','','','','','".$dateTime."','".$data['sfname']."'";
							performQuery($queryqq);
						}
					}
					//$result[ "success" ] = "true";
				} else {
					$sql = "select id from TP_Attendance_App where Sf_Code='".$data['sfcode']."' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='".$date."' order by id desc";
					$tr = performQuery( $sql );
					$id = $tr[ 0 ][ 'id' ];

					$sql = "update TP_Attendance_App set End_Lat='".$data['lat']."',End_Long='".$data['long']."',End_Time='".$dateTime."',End_addres='".$data['address']."' where id='".$id."'";
					performQuery( $sql );

					$sql1 = "select ID from Attendance_history where Sf_Code='".$data['sfcode']."' and DATEADD(dd, 0, DATEDIFF(dd,0,Start_Time))='".$date."' order by id desc";
					$tr1 = performQuery( $sql1 );
					$id1 = $tr1[ 0 ][ 'ID' ];
					$sql1 = "update Attendance_history set End_Lat='".$data['lat']."',End_Long='".$data['long']."',End_Time='".$dateTime."',End_addres='".$data['address']."' where ID='".$id1."'";
					performQuery( $sql1 );
					if(($data['division_code']=="39")||($data['division_code']=="38")||($data['division_code']=="37")){
						if(($data['lat']==""||$data['long']=="")||($data['lat']=="0.0"||$data['long']=="0.0")||($data['lat']==null||$data['long']==null)){
						
						}else{				
							//$queryss="select sf_emp_id,Employee_Id,Sf_Name from Mas_Salesforce_two with (nolock) where  sf_code='".$data['sfcode']."'";
							//$sf_data=performQuery($queryss);
							$queryqq="insert into sf_tbTrackLoction(SF_code,Emp_Id,Employee_Id,DtTm,Lat,Lon,Addr,Auc,EMod,Battery,Mock,SF_Mobile,updatetime,IsOnline,Division_code,App_version,Track_type,DCR_ID,Dcr_Detail_id,DCRcall_id,Dcrcall_name,DCR_time,sf_name) select '".$data['sfcode']."','".$data['sf_emp_id']."','".$data['Employee_Id']."','".$dateTime."','".$data['lat']."','".$data['long']."','".$data['address']."','','".$data['Mod']."','','','','".$dateTime."','online','".$data['division_code']."','".$data['Appver']."','OUT','','','','','".$dateTime."','".$data['sfname']."'";
							performQuery($queryqq);
						}
					}
					$result1 = [];
					$result1[ "msg" ] = "1";
					$result = [];
					$result[0]=$result1;
					
				}
			}
          
            outputJSON($result);
            break; 
        case "savedcract":
         	//$val=$data['val'];
         	$val=$data['val'];
			$DivCode = explode(",", $value["division_code"].",");

				for ($i = 0; $i < count($val); $i++) 
				{
				$main_no="0";
				$value=$val[$i];
				if($value["type"]=="90"){
					if($value["type"]=="1"	||	$value["type"]=="2"	||	$value["type"]=="3"	||	$value["type"]=="4"){
					$query="exec ios_svDCRMain_Edet '" .$value['sfcode']. "','" . $value["dcr_date"] . "','" . $value['WT_code'] . "','" . $value['WTName'] . "','" . $value['FWFlg'] . "','" . $value['town_code'] . "','".$value['town_name']."','" .$DivCode[0]. "','" . $value['Remarks'] . "','" . $value['sf_type'] . "','" . $value['state_code'] . "','','iOS'";
					  $respon["MQry"]=$query;
					  $tr=performQuery($query);
					  $ARCd=$tr[0]['ARCode'];
				  $respon["valQry"]=$tr[0]['ARCode'];
				  }
				  
				  if($value["type"]=="1"){  
					$query="exec ios_svDCRLstDet_App_Edet '".$tr[0]['ARCode']."',0,'".$value["sfcode"]."',1,'".$value["cus_code"]."','".$value['cusname']."','".$value["dcr_date"]."',0,'','','','','','','','','','','','','".$DivCode[0]."',0,'".$value["dcr_date"]."','".$value['lat']."','".$value['lng']."','".$value['DataSF']."','NA','iOS','" . $data['town_code'] . "','".$data['town_name']."'";
					$result["DQry"]=$query;
					$tr1=performQuery($query);
					
					}

					if($value["type"]=="2" || $value["type"]=="3" ){
						$query="exec ios_svDCRCSHDet_App_Edet '".$tr[0]['ARCode']."',0,'".$value["sfcode"]."','".$value["type"]."','".$value["cus_code"]."','".$value['cusname']."','".$value["dcr_date"]."',0,'','','','','','','','".$DivCode[0]."',0,'".$value["dcr_date"]."','".$value['lat']."','".$value['lng']."','".$value['DataSF']."','NA','iOS','" . $data['town_code'] . "','".$data['town_name']."'";
						$result["CQry"]=$query;
						$tr1=performQuery($query);
					 }

					if($value["type"]=="4"){
						$query="exec ios_svDCRUnlstDet_App_Edet '".$tr[0]['ARCode']."',0,'".$value["sfcode"]."','".$value["type"]."','".$value["cus_code"]."','".$value['cusname']."','".$value["dcr_date"]."',0,'','','','','','','','','','','','','".$DivCode[0]."',0,'".$value["dcr_date"]."','".$value['lat']."','".$value['lng']."','".$value['DataSF']."','NA','iOS','" . $data['town_code'] . "','".$data['town_name']."'";
						$result["NQry"]=$query;
						$tr1=performQuery($query);
					 }
			   
				}
					// if ( $tr1[0]['ARDCd'] == "Exists" ) {
							// $respon[ "msg" ] = "Call Already Exists";
							// $respon[ "success" ] = false;
							// outputJSON( $respon );
							// die;
						// }
				if($value["type"]=="0"){		
				$query="exec iOS_svDcrActivity_Edet '".$value["sfcode"]."','".$DivCode[0]."','".$value["act_date"]."','".$value["update_time"]."','".$value["slno"]."','".$value["ctrl_id"]."','".$value["creat_id"]."','".$value["values"]."','".$value["codes"]."','".$tr[0]['ARCode']."','".$tr1[0]['ARDCd']."','".$value["type"]."','".$value["cus_code"]."'";
				$arr = performQuery($query);
				$respon["finalQry"]=$arr;
				}
			}
			$respon['success'] = true;
            outputJSON($respon);
            break; 
      
        default:
			$result = array();
			$result['success'] = false;
			$result['msg'] = 'Try Again';
			outputJSON($result);
            break;
    } 
}
?>
