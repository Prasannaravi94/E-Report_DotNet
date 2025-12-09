<?php
function gettpstatus() {
	global $data;
	$div_codes = (string) $data['division_code'];	
	$div_code = explode(",", $div_codes.",");

	 $sfCode=(string) $data['sfcode'];
	 $tpmonth = date('m', strtotime($data['ReqDt']));
	 $tpyear = date('Y', strtotime($data['ReqDt']));
	 $tpday = date('d', strtotime($data['ReqDt']));
	 
	
	 $query="select Change_Status status from trans_tp_one where sf_code='".$sfCode."' and tour_month='".$tpmonth."' and tour_year='".$tpyear."'";
	 $result = performQuery($query);	 
	  $resval=0;
	   $resvalt=0;
	   $resvaltt =0;
	   $resvalf = 0;
	   $resvalff = 0;
	   if(count($result)>0){
		  
		for ($i = 0; $i < count($result); $i++) {
			
		   if($result[$i]["status"]==0){
			    $resval = 1;
		    }
			else if($result[$i]["status"]==2){
			    $resvaltt = 1;
		    }
		    else{
		      $resvalt = 1;  
		   }
	     }
	   }
	  else{
		   $queryw="select Change_Status status from trans_tp where sf_code='".$sfCode."' and tour_month='".$tpmonth."' and tour_year='".$tpyear."'";
	       $resww= performQuery($queryw);
		  
		   if(count($resww)>0){
		         $resvalff = 1;
		   }else{
			    $resvalf = 1;
		   }
			
		    if($resvalff == 1){				
			
			    if(($tpday>=$data['Tp_StartDate'])&&($tpday<=$data['Tp_EndDate'])){
					
				   if($tpmonth==12){
					   $tpmonth=1;
					   $tpyear=$tpyear+1;
				   }
				   else{
					   $tpmonth=$tpmonth+1;
				   }
				  
				   $queryy="select Change_Status status from trans_tp_one where sf_code='".$sfCode."' and tour_month='".$tpmonth."' and tour_year='".$tpyear."'";
					$resultss = performQuery($queryy);
					
					
					$resval=0;
					$resvalt=0;
					$resvalf = 0;
					$resvalff = 0;
					$resvaltt =0;
					if(count($resultss)>0){
						  
						for ($i = 0; $i < count($resultss); $i++) {
							
						   if($resultss[$i]["status"]==0){
								$resval = 1;
							}
							else if($resultss[$i]["status"]==2){
								$resvaltt = 1;
							}
							else{
							  $resvalt = 1;  
						   }
						}
					}
					else{
						   $querywy="select Change_Status status from trans_tp where sf_code='".$sfCode."' and tour_month='".$tpmonth."' and tour_year='".$tpyear."'";
						   $reswwss= performQuery($querywy);
						  
						   if(count($reswwss)>0){
								 $resvalff = 1;
						   }else{
								$resvalf = 1;
						   }
					}
												
					
				  }
			   
		    }
		   
	   }
	  
	  if($resval == 1){
		  $response["Success"]="notcompleted";
	   }
	   else if($resvalt == 1){
		  $response["Success"]="pending";
	   } else if($resvalf == 1){
		  $response["Success"]="nodata";
	   }
	   else if($resvalff == 1){
		  $response["Success"]="approved";
	   }
	 else{
	     $response["Success"]="rejected";
	   }
			$response["month"]=$tpmonth;
	
			outputJSON( $response );
          
}
?>