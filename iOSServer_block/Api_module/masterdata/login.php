<?php
function login() {    
    global $data;
    $query = "exec LoginAPP '".$data['name']."','".$data['password']."'";
    $arr = performQuery($query);
    if (count($arr) == 1) {
		  if (!empty($data['AppDeviceRegId'])) {
            $sql = "update Access_Table set  DeviceRegId='".$data['AppDeviceRegId']."',app_device_id='".$data['device_id']."' where sf_code='" . $arr[0]['SF_Code'] . "'";
            performQuery($sql);
        }
		$query = "insert into version_ctrl select '".$arr[0]["SF_Code"]."',getDate(),getDate(),'".$data['versionNo']."','".$data['mode']."','".$data['Device_version']."','".$data['Device_name']."'";
		performQuery($query);
		//echo $query;
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

