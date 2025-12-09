<?php
function login() {    
    global $data;
    $query = "exec LoginAPP '".$data['name']."','".$data['password']."'";
    $arr = performQuery($query);
    if (count($arr) == 1) {
		$respon = $arr[0];   
    	$respon['success'] = true;		
        return outputJSON($respon);
    } else {
		$respon['success'] = false;
        $respon['msg'] = "Check User and Password";
        return outputJSON($respon);
    }
}
?>

