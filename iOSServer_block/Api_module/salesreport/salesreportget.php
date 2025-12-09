<?php 
function salesreportget(){ 
	global $data;
	$div = $_GET['divisionCode'];
		$sf = $_GET['sfCode'];
		$fm = $_GET['fmonth'];
		$tm = $_GET['tomonth'];
		$fy = $_GET['fyear'];
		$ty = $_GET['toyear'];
	if(!isset($_GET['tableName'])){
		$query = "exec Primary_SS_Dashboard_APP '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
		outputJSON( performQuery($query));
	}else{
		switch ( strtolower( $_GET['tableName'] ) ) {
		case "primary_ss_dashboard":
			$query = "exec Primary_SS_Dashboard_APP '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
			outputJSON( performQuery($query));
			break; 
		case "primary_hq":
			$query = "exec Primary_HQwise_Dashboard_APP '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//exec Primary_HQwise_Dashboard_APP '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;  
		case "primary_hq_detail":
			$hq_code = $_GET['hqcode'];
			$hq_name = $_GET['hqname'];
			$query = "exec Primary_Brandwise_Dashboard_APP '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."','".$hq_code."','".$hq_name."'";
			//exec Primary_Brandwise_Dashboard_APP '8,','MR0026','07','07','2023','2023','CDH','SELVA FM';
			outputJSON( performQuery($query));
			break;   
		case "primary_hq_brand_product":
			$hq_cat_id = $_GET['hq_cat_id'];
			$hq_name = $_GET['hqname'];
			$query = "exec Primary_Sale_HQ_Brandwise_Product_App '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."','".$hq_cat_id."','".$hq_name."'";
			// exec Primary_Sale_HQ_Brandwise_Product_App '8,','MR0026','804','07','07','2023','2023','CDH','SELVA FM';
			outputJSON( performQuery($query));
			break;
		case "primary_brand":
			$query = "exec Primary_SS_Dashboard_APP '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//exec Primary_Sale_Brandwise_App '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break; 
		case "primary_brand_product":
			$query = "exec Primary_Sale_Brandwise_Product_App '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//exec Primary_Sale_Brandwise_Product_App '8,','MR0026','804','07','07','2023','2023';
			outputJSON( performQuery($query));
			break; 
		case "primary_fieldforce":
			$query = "exec Target_Sale_Fieldforcewise_APP '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//exec Target_Sale_Fieldforcewise_APP '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;  
		case "primary_groupwise_dashboard_hq":
			$divis=$_GET['division_code'];
			$sfcodee=$_GET['sfcode'];
			$hq_cat_id = $_GET['hq_cat_id'];
			$hq_name = $_GET['hqname'];
			$query = "exec HQwise_Primary_Groupwise_Dashboard_APP '".$divis."','".$sfcodee."','".$fm."','".$tm."','".$fy."','".$ty."','".$hq_cat_id."','".$hq_name."'";
			// exec HQwise_Primary_Groupwise_Dashboard_APP '8,','MR0026','07','07','2023','2023','CDH','SELVA FM';
			//echo $query;
			outputJSON( performQuery($query));
			break;  
		case "primary_groupwise_product_hq":
			$hq_cat_id = $_GET['hq_cat_id'];
			$hq_name = $_GET['hqname'];
			$divis=$_GET['division_code'];
			$sfcodee=$_GET['sfcode'];
			$query = "exec HQwise_Primary_Sale_HQ_Groupwise_Product_App '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."','".$hq_cat_id."','".$hq_name."'";
			//exec HQwise_Primary_Sale_HQ_Groupwise_Product_App '8,','MR0026','2','07','07','2023','2023','CDH','SELVA FM';
			outputJSON( performQuery($query));
			break;   
		case "primary_fieldforce_brand":
			$query = "exec Primary_Sale_FF_Brandwise_App '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//exec Primary_Sale_FF_Brandwise_App '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;    
		case "primary_sale_groupwise":
			$query = "exec Primary_Sale_Groupwise_App '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//exec Primary_Sale_Groupwise_App '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;    
		case "primary_sale_groupwise_product":
			$grpCode = $_GET['grpCode'];
			$divis=$_GET['division_code'];
			$sfcodee=$_GET['sfcode'];
			$query = "exec Primary_Sale_Groupwise_Product_App '".$divis."','".$sfcodee."','".$grpCode."','".$fm."','".$tm."','".$fy."','".$ty."'";			
			//exec Primary_Sale_Groupwise_Product_App '8,','MR0026','2''07','07','2023','2023';
			outputJSON( performQuery($query));
			break;    
		case "primary_sale_ff_groupwise":
			$query = "exec Primary_Sale_FF_Groupwise_App '".$div."','".$sf."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//exec Primary_Sale_FF_Groupwise_App '8,','MR0026','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;    
		case "primary_sale_ff_groupwise_product":
			$grpCode = $_GET['grpCode'];
			$query = "exec Primary_Sale_FF_Groupwise_Product_App '".$div."','".$sf."','".$grpCode."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//echo $query;die;
			//exec Primary_Sale_FF_Groupwise_Product_App '8,','MR0026','2','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;     
		case "primary_fieldforce_brand_product":
			$Brand_product = $_GET['Brand_product'];
			$query = "exec Primary_Sale_FF_Brandwise_Product_App '".$div."','".$sf."','".$Brand_product."','".$fm."','".$tm."','".$fy."','".$ty."'";
			//exec Primary_Sale_FF_Brandwise_Product_App '8,','MR0026','2','07','07','2023','2023';
			outputJSON( performQuery($query));
			break;     
		case "mvd_coverage":
			$query = "exec MVD_Dashboard_App '".$div."','".$sf."','".$fm."','".$tm."','".$fy."',''";
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
}

?>