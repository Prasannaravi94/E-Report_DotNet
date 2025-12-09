<?php
function activityget() {
	global $data;
	$DivCodes = (string) $data['division_code'];
    $DivCode = explode(",", $DivCodes.",");
	
	switch ( strtolower( $data[ 'tableName' ] ) ) {

	case "getdynactivity":
		$query = "select Activity_SlNo,Activity_Mode,Activity_Desig,Activity_SName,Activity_Name,Activity_OrderBy,Division_Code,Creation_date,Active_Flag,Activity_For,Activity_Available,Other_Multi_Activity_Name,Related_Activity_SlNo,Approval_Needed,Approved_By,Transaction_Involved,Editable from mas_activity where Division_Code='".str_replace(",","", $DivCode[0])."' and Active_Flag='0'";
        outputJSON(performQuery($query));
		break;
	case "getdynactivity_details":
		$query = "select Creation_Id,Activity_SlNo,Field_Name,Control_Id,Control_Name,Control_Para,Division_Code,Activity_Name,Created_date,Order_by,Updated_Date,Active_Flag,Table_code,Table_name,Mandatory,For_act,Group_Creation_ID,Activity_For from mas_dynamic_screen_creation where Activity_SlNo='" . $data['slno'] . "' and Division_Code='" .$DivCode[0]. "' and Active_Flag='0' order by Order_by";	
		$res=performQuery($query);
			if (count($res)>0) 
			{
				for ($il=0;$il<count($res);$il++)
					{
						$id=$res[$il]["Control_Id"];				
					if($id=="8"	||	$id=="9"){
						if($res[$il]["Control_Para"]=="Mas_ListedDr"){
							$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$DivCode[0]."' and Sf_Code='".$sf."' and ListedDr_Active_Flag='0'";	
						}
						else if($res[$il]["Control_Para"]=="Mas_chemists"){
							$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$DivCode[0]."' and Sf_Code='".$sf."' and Chemists_Active_Flag='0'";	
						}
						else{
							if($res[$il]["Control_Para"]=="Mas_salesforce"){
								$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$DivCode[0].","."'";	
							}
							else{
								$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$DivCode[0]."'";
							}
						}
						
						$res[$il]['inputss']=$qu;
						$res[$il]['input']=performQuery($qu);
					}
					else	if($id=="12"	||	$id=="13"){
						$qu = "select Sl_No from Mas_Customized_Table_Name where Name_Table='".$res[$il]["Control_Para"]."'";
							$res[$il]['inputss']=$qu;
						$cus=performQuery($qu);
						//$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from Mas_Customized_Table where Name_Table_Slno='".$cus[0]["Sl_No"]."'";
						$qu = "select Mas_Sl_No,Customized_Name from Mas_Customized_Table where Name_Table_Slno='".$cus[0]["Sl_No"]."'";
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
		default:
		$result = array();
			$result['success'] = false;
			$result['msg'] = 'Try Again';
			outputJSON($result);
		break;
	}
}
?>

