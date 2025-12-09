<?php 
function salesreportget(){ 
	global $data;  
	switch ( strtolower( $data[ 'tableName' ] ) ) {
		case "primary_ss_dashboard":
			$query = "exec Primary_SS_Dashboard_APP '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			outputJSON( performQuery($query));
			break; 
		case "primary_hq":
			$query = "exec Primary_HQwise_Dashboard_APP '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_HQwise_Dashboard_APP '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;  
		case "primary_hq_detail":
			$query = "exec Primary_Brandwise_Dashboard_APP '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."','".$data['hqcode']."','".$data['hqname']."'";
			//exec Primary_Brandwise_Dashboard_APP '8,','MR0026','07','07','2023','2023','CDH','SELVA FM';
			outputJSON( performQuery($query));
			break;   
		case "primary_hq_brand_product":
			$query = "exec Primary_Sale_HQ_Brandwise_Product_App '".$data['division_code']."','".$data['sfcode']."','".$data['Brand_product']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."','".$data['hq_cat_id']."','".$data['hqname']."'";
			// exec Primary_Sale_HQ_Brandwise_Product_App '8,','MR0026','804','07','07','2023','2023','CDH','SELVA FM';
			outputJSON( performQuery($query));
			break;
		case "primary_brand":
			$query = "exec Primary_SS_Dashboard_APP '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_Sale_Brandwise_App '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break; 
		case "primary_brand_product":
			$query = "exec Primary_Sale_Brandwise_Product_App '".$data['division_code']."','".$data['sfcode']."','".$data['Brand_product']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_Sale_Brandwise_Product_App '8,','MR0026','804','07','07','2023','2023';
			outputJSON( performQuery($query));
			break; 
		case "primary_fieldforce":
			$query = "exec Target_Sale_Fieldforcewise_APP '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Target_Sale_Fieldforcewise_APP '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;  
		case "primary_groupwise_dashboard_hq":
			$query = "exec HQwise_Primary_Groupwise_Dashboard_APP '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."','".$data['hq_cat_id']."','".$data['hqname']."'";
			// exec HQwise_Primary_Groupwise_Dashboard_APP '8,','MR0026','07','07','2023','2023','CDH','SELVA FM';
			outputJSON( performQuery($query));
			break;  
		case "primary_groupwise_product_hq":
			$query = "exec HQwise_Primary_Sale_HQ_Groupwise_Product_App '".$data['division_code']."','".$data['sfcode']."','".$data['grpCode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."','".$data['hq_cat_id']."','".$data['hqname']."'";
			//exec HQwise_Primary_Sale_HQ_Groupwise_Product_App '8,','MR0026','2','07','07','2023','2023','CDH','SELVA FM';
			outputJSON( performQuery($query));
			break;   
		case "primary_fieldforce_brand":
			$query = "exec Primary_Sale_FF_Brandwise_App '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_Sale_FF_Brandwise_App '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;    
		case "primary_sale_groupwise":
			$query = "exec Primary_Sale_Groupwise_App '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_Sale_Groupwise_App '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;    
		case "primary_sale_groupwise_product":
			$query = "exec Primary_Sale_Groupwise_Product_App '".$data['division_code']."','".$data['sfcode']."','".$data['grpCode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_Sale_Groupwise_Product_App '8,','MR0026','2''07','07','2023','2023';
			outputJSON( performQuery($query));
			break;    
		case "primary_sale_ff_groupwise":
			$query = "exec Primary_Sale_FF_Groupwise_App '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_Sale_FF_Groupwise_App '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;    
		case "primary_sale_ff_groupwise_product":
			$query = "exec Primary_Sale_FF_Groupwise_Product_App '".$data['division_code']."','".$data['sfcode']."','".$data['grpCode']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_Sale_FF_Groupwise_Product_App '8,','MR0026','2','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;     
		case "primary_fieldforce_brand_product":
			$query = "exec Primary_Sale_FF_Brandwise_Product_App '".$data['division_code']."','".$data['sfcode']."','".$data['Brand_product']."','".$data['fmonth']."','".$data['tomonth']."','".$data['fyear']."','".$data['toyear']."'";
			//exec Primary_Sale_FF_Brandwise_Product_App '8,','MR0026','2','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;     
		case "mvd_coverage":
			$query = "exec MVD_Dashboard_App '".$data['division_code']."','".$data['sfcode']."','".$data['fmonth']."','".$data['fyear']."',''";
			//exec MVD_Dashboard_App '8,','MR0026','07','2023','';
			outputJSON( performQuery($query));
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