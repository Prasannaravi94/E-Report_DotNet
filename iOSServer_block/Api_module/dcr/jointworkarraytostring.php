<?php
function jointworkarraytostring($data) {
	//global $data;
	$JWWrkArr=$data['JointWork'];
    $JWWrk="";
    $JWWrkNm="";
	
    for ($i = 0; $i < count($JWWrkArr); $i++) 
    {
	    $JWWrk=$JWWrk.$JWWrkArr[$i]["Code"]."$$";
	    $JWWrkNm=$JWWrkNm.$JWWrkArr[$i]["Name"]."$$";
    }
	$jointwork=array();
	$jointwork['JWWrk']=$JWWrk;
	$jointwork['JWWrkNm']=$JWWrkNm;
	return $jointwork;
}
?>


