<?php
function inputarraytostring($data) {
	//global $data;
	
	$InpArr=$data['Inputs'];
    $Inps="";
    $InpsNm="";
	  $GC="";
	  $GN="";
	  $GQ=0;
    for ($i = 0; $i < count($InpArr); $i++) 
    {
		
	if($data['CusType']=="1"||$data['CusType']=="4"){
        if ($i==0){
	          $GC=$InpArr[$i]["Code"];
	          $GN=$InpArr[$i]["Name"];
	          $GQ=$InpArr[$i]["IQty"];
			 // $Inps=$Inps.$InpArr[$i]["Code"]."~".$InpArr[$i]["IQty"]."#";
	         // $InpsNm=$InpsNm.$InpArr[$i]["Name"]."~".$InpArr[$i]["IQty"]."#";
        
        }else{
	          $Inps=$Inps.$InpArr[$i]["Code"]."~".$InpArr[$i]["IQty"]."#";
	          $InpsNm=$InpsNm.$InpArr[$i]["Name"]."~".$InpArr[$i]["IQty"]."#";
        }
		
	}else{
	        $Inps=$Inps.$InpArr[$i]["Code"]."~".$InpArr[$i]["IQty"]."#";
			$InpsNm=$InpsNm.$InpArr[$i]["Name"]."~".$InpArr[$i]["IQty"]."#";
		}
    }
	
	$inputs=array();
	$inputs['Inps']=$Inps;
	$inputs['InpsNm']=$InpsNm;
	$inputs['GC']=$GC;
	$inputs['GN']=$GN;
	$inputs['GQ']=$GQ;
	return $inputs;
}
?>


