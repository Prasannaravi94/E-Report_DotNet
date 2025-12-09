<?php
function coachingsave() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");

	//get approvals
	switch ( strtolower( $data[ 'tableName' ] ) ) {

        case "savecoachingform":
			$val=$data['val'];
			$DivCode = explode(",", $value["division_code"].",");
		    for ($i = 0; $i < count($val); $i++) 
			{
				$value=$val[$i];

				$query="exec iOS_svCoaching_form_Edet '".$value["SF"]."','".$value["div"]."','".$value["act_date"]."','".$value["update_time"]."','".$value["slno"]."','".$value["ctrl_id"]."','".$value["creat_id"]."','".$value["values"]."','".$value["codes"]."','','0','".$value["toSFCode"]."','".$value["toSFName"]."'";
				$arr = performQuery($query);
				$respon["finalQry"]=$arr;
				if($value["ctrl_id"] =="3"){
					$querys="select Caption_Name from Coaching_Dynamic_Screen_Creation where Creation_ID='".$value["creat_id"]."' and Activity_Sl_No='".$value["slno"]."'";
					$arr_mean = performQuery($querys);
					
					if($arr_mean[0]["Caption_Name"]=="Convert to task"){
						$query_no="select isnull(Max(Task_ID),0)+1 Task_ID from Trans_Task_Details";
						$arr_no = performQuery($query_no);
						$TID = $arr_no[0]["Task_ID"]; 
						
						//$query_name="select Sf_Name  from mas_salesforce_two where sf_code = '".$sf."'" ;
						//$arr_sf = performQuery($query_name);
						
						$acttDt = date('Y-m-d 00:00:00.000', strtotime($value["act_date"]));
						$deadln=date('Y-m-d', strtotime($acttDt. ' + 6 days'));
						$deadlndt=date('Y-m-d 00:00:00.000', strtotime($deadln));
						
						$queryss="insert into Trans_Task_Details values ('".$TID."','3331','Coaching Based','".$value["SF"]."','".$value["SFName"]."','".$value["values"]."','H','".$acttDt."','".$deadlndt."','".$value["toSFCode"]."','".$value["toSFName"]."',getdate(),NULL,'1','New',NULL,'".$value["div"]."','','','','','','','','','')";
						//echo $queryss;
						$arrss = performQuery($queryss);
					}
						
				}
				
			}
			$respon['success'] = true;
            outputJSON($respon);
            break; 
        case "uploadsign":
         	  $target_dir = "Activity/";
              $target_file_name = $target_dir .basename($_FILES["file"]["name"]);
              $target_file = basename($_FILES["file"]["name"]);
			  $upload_url="";
              $response = array();
              if (isset($_FILES["file"])) 
              { 
                 if (move_uploaded_file($_FILES["file"]["tmp_name"], $target_file_name)) 
                 {
                    $success = true;
                    $upload_url = $target_file;
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
             	$message =  "No File...";
                $success = true;
             }
 
             $response["success"] = $success;
             $response["message"] = $message;
             $response["url"] = $upload_url;
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
