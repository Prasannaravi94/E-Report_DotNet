<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Content-Type: application/json; Charset="UTF-8"');
session_start();
date_default_timezone_set("Asia/Kolkata");

include "dbConn.php";
include "utils.php";
$data = json_decode($_POST['data'], true);

function getMonthSummary() {
    global $data;
    $sfCode = (string) $data['SF'];
    $Day = (string) $data['Day'];  
    $mnyr = explode("-", $Day);
    $query = "exec getMonthSummry '".$sfCode."','".$mnyr[1]."','".$mnyr[0]."'";
    return performQuery($query);
}
function getDayRpt() {
    global $data;
    $sfCode = (string) $data['SF'];
    $Day = (string) $data['Day'];
    $query = "exec getDayDCRHead '".$sfCode."','".$Day."'";
    return performQuery($query);
}
function getDayDrs() {
    global $data;
    $sfCode = (string) $data['SF'];
    $Day = (string) $data['Day'];
    $query = "exec getDayDrsDetails '".$sfCode."','".$Day."'";
    return performQuery($query);
}
function getDayChm() {
    global $data;
    $sfCode = (string) $data['SF'];
    $Day = (string) $data['Day'];
    $query = "exec getDayChmDetails '".$sfCode."','".$Day."'";
    return performQuery($query);
}
function getDayStk() {
    global $data;
    $sfCode = (string) $data['SF'];
    $Day = (string) $data['Day'];
    $query = "exec getDayStkDetails '".$sfCode."','".$Day."'";
    return performQuery($query);
}
function getDayNdr() {
    global $data;
    $sfCode = (string) $data['SF'];
    $Day = (string) $data['Day'];
    $query = "exec getDayNdrDetails '".$sfCode."','".$Day."'";
    return performQuery($query);
}

$axn = $_GET['axn'];
$value = explode(":", $axn);
switch (strtolower($value[0])) {
    case "get/dayrpt":
        outputJSON(getDayRpt());
        break;
    case "get/daydrs":
        outputJSON(getDayDrs());
        break;
    case "get/daychm":
        outputJSON(getDayChm());
        break;
    case "get/daystk":
        outputJSON(getDayStk());
        break;
    case "get/dayndr":
        outputJSON(getDayNdr());
        break;
    case "get/monthsumm":
        outputJSON(getMonthSummary());
        break;
}
?>