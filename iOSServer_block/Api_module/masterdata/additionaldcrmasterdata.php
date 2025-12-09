<?php
function additionaldcrmasterdata() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
	
	// get dcr master data here

	switch ( strtolower( $data[ 'tableName' ] ) ) {
		case "getmapcompdet":
		    $sql = "exec iOS_getMas_CompetitorDetsByProd_Edet '".$data['sfcode']."','".$data['sf_type']."','".$div_code[0]."'";
            outputJSON( performQuery( $sql ) );
	        break; 
        case "getvisit_contro":
           $sql = "select CustCode,CustType, convert(varchar, Vst_Date, 23)Dcr_dt,month(Vst_Date) Mnth,year(Vst_Date) Yr,CustName,isnull(SDP,'')town_code,isnull(SDP_Name,'')town_name,1 Dcr_flag from tbVisit_Details where SF_Code='" . $data['sfcode'] . "' and (CustType=1 OR CustType = 0) and  cast(CONVERT(varchar,Vst_Date,101)as datetime) >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) - 1, 0) order by Vst_Date";
			outputJSON( performQuery( $sql ) );
            break;     
        case "gettpstatus":
		      include 'Api_module/masterdata/gettpstatus.php';
			  gettpstatus();
            break;   
	    case "gettodycalls":
            $sql="exec iOS_getTodayCalls_Edet '".$data['sfcode']."'";
			outputJSON( performQuery( $sql ) );
            break; 
	    case "getstockbalance":
			include 'Api_module/masterdata/stockbalance.php';
			stockbalance();
            break; 
	    case "getproductfb":		
	  	   $sql = "select FeedBack_Id id,FeedBack_Name name from Mas_Product_Feedback where Division_code='".$div_code[0]."' ";
		   outputJSON( performQuery( $sql ) ); 
		   break; 	
        case "getcusmvst":	
            $sql = "exec iOS_getMnVstDetails_Edet '".$data['sfcode']."','".$data['CusCode']."',".date('Y').",'".$data['typ']."'";		 
		    outputJSON( performQuery( $sql ) ); 
		   break;		 
	    case "getcuslvst":	
            $sql = "exec iOS_getVstDetails_Edet '".$data['sfcode']."','".$data['typ']."','".$data['CusCode']."'";		 
		    outputJSON( performQuery( $sql ) ); 
		   break; 	
        case "getdrdets":	
            $sql = "exec iOS_getDrDetails_Edet '".$data['CusCode']."'";		 
		    outputJSON( performQuery( $sql ) ); 
		   break; 	
        case "getcustctrl":	
				
				$query="select Sl_no,Activity_SlNo,Cat_code,Field_Name,Control_Id,Control_Name,Control_Para,Order_by,Mandatory,Table_code,Table_name from Dynamic_Customer_Fixation where Type='CU' and Active_Flag=0 and Division_Code='".$div_code."' order by Cat_code,Order_by";
				//return performQuery($query)
           // $sql = "exec iOS_getDrDetails '".$data['CusCode']."'";		 
		    outputJSON( performQuery( $query ) ); 
		   break;
		case "getdetailreport":	
			$query = "exec ios_Rpt_DetailingCustomerwise_Edet '" .$data['sfcode'] . "','" . $data['fdt'] . "','" . $data['tdt'] . "'"; 
			outputJSON( performQuery($query));
		   break;  
        case "getvwchktpstatus":	
        	$sql="select TP_Entry_Count,TP_Flag, CASE WHEN TP_Flag = '1' THEN 'Tour Plan Not Approved. Contact Admin' WHEN TP_Flag = '2' THEN 'Tour Plan Rejected,Resubmit Tour Plan ' WHEN TP_Flag = '0' THEN 'Prepare Tour Plan' Else '3' END AS TP_Status From vwCheckTPStatus where SF_Code='".$data['sfcode']."' and Tour_Month ='".$data['month']."' and Tour_Year='".$data['year']."'"; 
		    outputJSON( performQuery( $sql ) ); 
		   break; 
        case "geteditdaycalls":	
            $sql="exec iOS_getEditdayCalls_Edet '".$data['sfcode']."','".date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']))."'";	 
			outputJSON( performQuery( $sql ) ); 
		   break; 		   
		case "geteditdates":	
            $sql="exec iOS_getEditdayDates_Edet '".$data['sfcode']."','".date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']))."','".$div_code[0]."'";	
		    outputJSON( performQuery( $sql ) ); 
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