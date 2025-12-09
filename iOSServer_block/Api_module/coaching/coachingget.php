<?php
function coachingget() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");
	//echo $data[['tableName'];
	//get approvals
	switch ( strtolower( $data[ 'tableName' ] ) ) {

        case "getcoachingform":
			$query = "select Activity_sl_no,Activity_SName,Activity_Name,Activity_OrderBy,division_code,Created_Date,Activity_flag,Activity_For,Activity_Type,Updated_date,Based_on,Mandatory,Activity_For_Name from Coaching_Activity_Master where Division_Code='".$DivCode[0]."' and Activity_Flag='0'";				
		    outputJSON( performQuery($query));
            break; 
		case "getcoachingview_detail":
			$query = "select Creation_ID,Activity_Sl_No,Activity_Name,Caption_Name,Type,Control_ID,Control_Name,Control_Para,Division_Code,Created_Date,OrderBy,Updated_Date,Active_flag,Table_Code,Table_Name,Mandatory from Coaching_Dynamic_Screen_Creation where Activity_Sl_No='".$data['slno']."' and Division_Code='".$DivCode[0]."' and Active_Flag='0' order by OrderBy";	
			$res=performQuery($query);
			
			if (count($res)>0) 
			{
				for ($il=0;$il<count($res);$il++)
				{
					$id=$res[$il]["Control_ID"];
					
					if($id=="8"	||	$id=="9"){
						$qu = "select Sl_No from Coaching_Mas_Customized_Table_Name where Name_Table='".$res[$il]["Control_Para"]."'";
							$res[$il]['inputss']=$qu;
						$cus=performQuery($qu);
						//$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from Mas_Customized_Table where Name_Table_Slno='".$cus[0]["Sl_No"]."'";
						$qu = "select Mas_Sl_No,Customized_Name from Coaching_Mas_Customized_Table_Custom_Screen where Name_Table_Slno='".$cus[0]["Sl_No"]."'";
						$cus=performQuery($qu);
						$res[$il]['input']=$cus;
					}
					else{
						$res[$il]['input']=array();
					}
				}
			}
			 outputJSON($res);
			 break;
        case "getjointworkdone":
         	$query = "select replace(JointWork_Code,'$$',',') JointWork_Code,isnull(JointWork_Name,'') JointWork_Name from tbvisit_details where cast(Vst_Date as date ) ='".$data['date']."' and SF_Code='".$data['sfcode']."' and JointWork_Code<>'' group by JointWork_Code,JointWork_Name";
			$response = performQuery( $query );
			$jws= array();
			for($k=0;$k<count($response);$k++) {
				$jw=array();
				$jw["JointWork_Code"]=$response[$k]["JointWork_Code"];
				$jw["JointWork_Name"]=$response[$k]["JointWork_Name"];
				array_push($jws,$jw);
			}
            outputJSON($jws);
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
