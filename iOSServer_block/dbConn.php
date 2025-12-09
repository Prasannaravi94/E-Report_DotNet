<?php

$serverName = "10.10.37.74,24897";
$connectionInfo = array(
    "Database" => "SANSFA_TwentyThree",
    "LoginTimeout" => 30,
    "UID" => "PSAPLN69A",
    "PWD" => "wZ9+Cy9!VH*Ran@tHYJ*7E3kLdXSMr89"
);

/* Connect using Windows Authentication. */
$conn = sqlsrv_connect($serverName, $connectionInfo);
$NeedRollBack=false;
if ($conn === false) {
    echo "Unable to connect.</br>";
    die(print_r(sqlsrv_errors(), true));
}
?>

