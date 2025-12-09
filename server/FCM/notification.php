<?php

function notification( $SFCode, $Msge, $SFType ) {
	$DeviceType = "Android";
	if($SFType==1){
		$query = "select A.Sf_Name name, B.DeviceRegId reg_id FROM Mas_Salesforce A LEFT JOIN Access_table B ON A.Sf_Code = B.sf_code where A.sf_code='$SFCode'";
		$response = performQuery( $query );
		$SFName = $response[ 0 ][ 'name' ];
		$FCMToken = $response[ 0 ][ 'reg_id' ];
		$Message = 'Hi ' . $SFName . ' ' . $Msge;
		FCM( $FCMToken, $Message, 'SANSFE', "-", $DeviceType );	
	}else{
		$query = "SELECT A.Sf_Name name, B.DeviceRegId reg_id FROM Mas_Salesforce A LEFT JOIN Access_table B ON A.Sf_Code = B.sf_code WHERE A.sf_code = (SELECT Reporting_To_SF FROM Mas_Salesforce WHERE Sf_Code = '$SFCode')";
		$response = performQuery( $query );
		$SFName = $response[ 0 ][ 'name' ];
		$FCMToken = $response[ 0 ][ 'reg_id' ];
		$Message = 'Hi ' . $SFName . ' ' . $Msge;
		FCM( $FCMToken, $Message, 'SANSFE', "-", $DeviceType );
	}
}

function notificationToMGR( $SFCode, $Msge) {
	$DeviceType = "Android";
	$query = "SELECT A.Sf_Name name, B.DeviceRegId reg_id FROM Mas_Salesforce A LEFT JOIN Access_table B ON A.Sf_Code = B.sf_code WHERE A.sf_code = (SELECT Reporting_To_SF FROM Mas_Salesforce WHERE Sf_Code = '$SFCode')";
	$response = performQuery( $query );
	$SFName = $response[ 0 ][ 'name' ];
	$FCMToken = $response[ 0 ][ 'reg_id' ];
	$Message = 'Hi ' . $SFName . ' ' . $Msge;
	FCM( $FCMToken, $Message, 'SANSFE', "-", $DeviceType );
}

function Android_Chat( $SFCodeTo, $Msge, $SFCodeFrom ) {
    $DeviceType = "Android";
    $query_1 = "select a.Sf_Name name,b.DeviceRegId reg_id from Mas_Salesforce a left JOIN Access_table b ON a.Sf_Code = b.sf_code where a.sf_code='" . $SFCodeTo . "'";
    $response_1 = performQuery( $query_1 );
    $FCMToken = $response_1[ 0 ][ 'reg_id' ];

    $query_2 = "select Sf_Code code,Sf_Name name from Mas_Salesforce where sf_code='" . $SFCodeFrom . "'";
    $response_2 = performQuery( $query_2 );

    $Message = 'Hi, ' . '~' . $response_2[ 0 ][ 'code' ] . '~' . $response_2[ 0 ][ 'name' ] . '~' . ' has send you a Message' . '~' . $Msge;
    FCM( $FCMToken, $Message, 'SANSFE', "-", $DeviceType );
}

function iOS_Chat( $SFCodeTo, $Msge, $SFCodeFrom ) {
    $DeviceType = "iOS";
    $query_1 = "select a.Sf_Name name,b.DeviceRegId reg_id from Mas_Salesforce a left JOIN Access_table b ON a.Sf_Code = b.sf_code where a.sf_code='" . $SFCodeTo . "'";
    $response_1 = performQuery( $query_1 );
    $FCMToken = $response_1[ 0 ][ 'reg_id' ];

    $query_2 = "select Sf_Code code,Sf_Name name from Mas_Salesforce where sf_code='" . $SFCodeFrom . "'";
    $response_2 = performQuery( $query_2 );

    $Message = 'Hi, ' . '~' . $response_2[ 0 ][ 'code' ] . '~' . $response_2[ 0 ][ 'name' ] . '~' . ' has send you a Message' . '~' . $msg;
    $iOS_Message = 'Hi, ' . $response_2[ 0 ][ 'name' ] . ' has send you a Message' . '~' . $msg;
    FCM( $FCMToken, $Message, 'SAN CRM', $iOS_Message, $DeviceType );

}

function FCM( $FCMToken, $Message, $Title, $iOS_Message, $DeviceType ) {
    if ( $DeviceType == "Android" ) {
        define( "GOOGLE_API_KEY", "AAAA72Fk1cA:APA91bFCX24_-3-x6qKu5bHHaL3THqXSPlxwd-847vBm1eFdF0lFpeNGF4OtEfbp3Rms6dtJ38VGniX4vM3RHi-E5NxpyO_MAgYRjTtoZ5swG-5x849BW8QKb5MzkbJU0w6Z6z6Lpite" );
        define( "GOOGLE_FCM_URL", "https://fcm.googleapis.com/fcm/send" );
        $postobject = array( 'registration_ids' => array( $FCMToken ), 'notification' => array( "body" => $Message, "title" => $Title ), 'priority' => 'high' );
        $headers = array( 'Authorization: key=' . GOOGLE_API_KEY, 'Content-Type: application/json' );
        $ch = curl_init();
        curl_setopt( $ch, CURLOPT_URL, GOOGLE_FCM_URL );
        curl_setopt( $ch, CURLOPT_POST, true );
        curl_setopt( $ch, CURLOPT_HTTPHEADER, $headers );
        curl_setopt( $ch, CURLOPT_RETURNTRANSFER, true );
        curl_setopt( $ch, CURLOPT_SSL_VERIFYPEER, false );
        curl_setopt( $ch, CURLOPT_POSTFIELDS, json_encode( $postobject ) );
        $result = curl_exec( $ch );
        if ( $result === FALSE ) {
            die( 'Problem occurred: ' . curl_error( $ch ) );
        }
        curl_close( $ch );
    } else {
        define( "GOOGLE_API_KEY", "AAAA72Fk1cA:APA91bFCX24_-3-x6qKu5bHHaL3THqXSPlxwd-847vBm1eFdF0lFpeNGF4OtEfbp3Rms6dtJ38VGniX4vM3RHi-E5NxpyO_MAgYRjTtoZ5swG-5x849BW8QKb5MzkbJU0w6Z6z6Lpite" );
        define( "GOOGLE_FCM_URL", "https://fcm.googleapis.com/fcm/send" );
        $fields = array(
            'registration_ids' => array( $reg_id ),
            'notification' => array( "body" => $iOS_Message, "title" => $Title, "msg" => $Message, 'apns-push-type' => 'background', 'content-available' => '1' ), 'priority' => 'high' );
        $headers = array( 'Authorization: key=' . GOOGLE_API_KEY, 'Content-Type: application/json' );
        $ch = curl_init();
        curl_setopt( $ch, CURLOPT_URL, GOOGLE_FCM_URL );
        curl_setopt( $ch, CURLOPT_POST, true );
        curl_setopt( $ch, CURLOPT_HTTPHEADER, $headers );
        curl_setopt( $ch, CURLOPT_RETURNTRANSFER, true );
        curl_setopt( $ch, CURLOPT_SSL_VERIFYPEER, false );
        curl_setopt( $ch, CURLOPT_POSTFIELDS, json_encode( $fields ) );
        $result = curl_exec( $ch );
        if ( $result === FALSE ) {
            die( 'Problem occurred: ' . curl_error( $ch ) );
        }
        curl_close( $ch );
    }
}
?>