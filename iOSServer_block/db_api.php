<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
//ini_set( 'error_reporting', E_ALL );
//ini_set( 'display_errors', true );
date_default_timezone_set("Asia/Kolkata");
include "dbConn.php";
include "utils.php";
$data = json_decode($_POST['data'], true);
$axn = $_GET['axn'];
$value = explode(":", $axn);
$folders = explode("/", $value[0]);
if($folders[0]=="table"||$folders[0]=="action"){
	$foldername="masterdata";
	$filename=$folders[1];
}
else if($folders[1]!=""){
	$foldername=$folders[1];
	$filename=$folders[1].$folders[0];
}
else{
	$foldername=$folders[0];
	$filename=$folders[0];

}
$filenamee=str_replace(' ','',$filename);

if (is_dir( 'Api_module/'.$foldername )) {
	// $var='Api_module/'.$foldername.'/'.$filename.'.php';
	// echo $var;
     include 'Api_module/'.$foldername.'/'.$filenamee.'.php';
  if($filenamee=="dcrdelete")
     $filenamee($data['amc'],$data['CusType']);
  else
	$filenamee();
}
else{
	    $result = array();
		$result['success'] = false;
		$result['msg'] = 'No Folder..';
		outputJSON($result);
}

?>

