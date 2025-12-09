<?php
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
session_start();

if($_GET['axn']=="ln"){
   echo(file_get_contents("language.json"));

}else{
   echo(file_get_contents("ConfigAPP.json"));

}
?>