<?php
function dcrmasterdata() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");
	//echo $data[ 'tableName' ];
	//get dcr master data here

	switch ( strtolower( $data[ 'tableName' ] ) ) {
		case "getdoctors":
		    $sql = "exec ios_getDoctors_Edet '" . $data['Rsf'] . "'";
			outputJSON( performQuery( $sql ) );
            break; 
        case "getchemist":
            $sql = "exec ios_getChemists_Edet '" . $data['Rsf'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getstockist":
            $sql = "exec ios_getStockist_Edet '" . $data['Rsf'] . "'";
			outputJSON( performQuery( $sql ) );
            break;        
        case "getunlisteddr":
		    $sql = "exec ios_getUnListed_Edet '" . $data['Rsf'] . "'";			
			outputJSON( performQuery( $sql ) );			
            break;        
	    case "getcip":
            $sql = "exec ios_getCip_Edet '" . $data['Rsf'] . "'";
			outputJSON( performQuery( $sql ) );
            break;   
        case "gethospital":
            $sql = "exec iOS_getHospitals_Edet '" . $data['Rsf'] . "'";
			outputJSON( performQuery( $sql ) );
            break;     
		case "getspeciality":
		    $sql = "exec ios_getDocSpec_Edet '" . $data['Rsf'] . "','".$div_code[0]."'";
			outputJSON( performQuery( $sql ) );
            break;   
        case "getdeparts":
		    $sql = "exec ios_getDeparts_Edet '" . $data['Rsf'] . "','".$div_code[0]."'";
			outputJSON( performQuery( $sql ) );
            break;   		
        case "getcategorys":
		    $sql = "exec ios_getDocCats_Edet '" . $data['Rsf'] . "','".$div_code[0]."'";
			outputJSON( performQuery( $sql ) );
            break;   						
		case "getquali":
		    $sql = "exec ios_getDocQual_Edet '".$div_code[0]."'";
			outputJSON( performQuery( $sql ) );
            break;  
        case "getclass":
		    $sql = "exec ios_getDocClass_Edet '" . $data['Rsf'] . "','".$div_code[0]."'";
			outputJSON( performQuery( $sql ) );
            break;  
		case "gettypes":
		    $result=[];
	        array_push($result,array('Code' => "1",'Name' => "H"));
	        array_push($result,array('Code' => "2",'Name' => "P - T"));
	        array_push($result,array('Code' => "3",'Name' => "P - W"));
	        array_push($result,array('Code' => "4",'Name' => "P - I"));
			outputJSON($result);
            break;  
	    case "getdrfeedback":
		   $sql = "select FeedBack_Id id,Feedback_Content name from Mas_App_CallFeedback where Division_code='" . $div_code[0] . "' and Act_Flag='0' order by Feedback_Content";
			outputJSON( performQuery( $sql ) );
            break;  		 
		case "getworktype":
		    $sql = "exec iOS_getMas_WorkTypes_Edet '" . $data['sfcode'] . "'," . $data['sf_type'] . ",".$div_code[0]."";
			outputJSON( performQuery( $sql ) );
            break;  
		case "getterritory":
		    $sql = "exec iOS_getTerritorys_Edet '" . $data['Rsf'] . "'";
			outputJSON( performQuery( $sql ) );
            break;  
	    case "gettodaytpnew":
			$DCRDt = date('Y-m-d 00:00:00.000', strtotime($data['ReqDt']));
            $query="exec iOS_getTodayTPNew_Edet '".$data['sfcode']."','$DCRDt','" . $data['sf_type'] . "'";
			outputJSON( performQuery( $query ) );
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