<?php
$serverName = "146.0.227.202,1433";
$connectionInfo = array(
    "Database" => "SanFDC_Eight",
    "LoginTimeout" => 30,
    "UID" => "sa",
    "PWD" => "fUvuD2veDReTh!"
);

/* Connect using Windows Authentication. */
$conn = sqlsrv_connect($serverName, $connectionInfo);
$NeedRollBack=false;
if ($conn === false) {
    echo "Unable to connect.</br>";
    die(print_r(sqlsrv_errors(), true));
}
echo("Test OK");
?>

