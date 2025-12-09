<?php
	for ($il=0;$il<count($res);$il++)
	{
		$id=$res[$il]["Control_Id"];				
		if($id=="8"	||	$id=="9"){
			if($res[$il]["Control_Para"]=="Mas_ListedDr"){
				$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$div."' and Sf_Code='".$sf."'";	
			}
			else{
				if($res[$il]["Control_Para"]=="Mas_salesforce"){
					$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$div.","."'";	
				}
				else{
					$qu = "select ".$res[$il]["Table_code"].",".$res[$il]["Table_name"]." from ".$res[$il]["Control_Para"]." where Division_Code='".$div."'";
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
?>

