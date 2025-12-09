<?php

function dcrsave() {
    global $data, $conn;
    $div_codes = ( string )$data[ 'division_code' ];
    $DivCode = explode( ",", $div_codes . "," );
    $ARCd = "";
   // $sql = "insert into tracking_edet_dcr select '" . $data[ 'sfcode' ] . "','" . $DivCode[ 0 ] . "','" . json_encode( $_POST[ 'data' ], true ) . "',getdate(),'" . date( 'Y-m-d 00:00:00.000', strtotime( $data[ 'vstTime' ] ) ) . "','Edet'";
   $sql="insert into sanclm_calls select '".$data['sfcode']."','".$DivCode[0]."','".json_encode(str_replace("'", "", $_POST['data']))."',getdate(),'".date('Y-m-d 00:00:00.000', strtotime($data['vstTime']))."','Edet'";
   performQuery( $sql );
    if ( $data[ 'amc' ] != '' ) {
        if ( $data[ 'sample_validation' ] == '1' && $data[ 'headerno' ] != '' && $data[ 'detno' ] != '' ) {
            $query1 = "EXEC iOS_UpdateDCRWiseSampleStock_Edet '" . $data[ 'headerno' ] . "','" . $data[ 'detno' ] . "','" . $data[ 'sfcode' ] . "','" . $DivCode[ 0 ] . "'";
            performQuery( $query1 );
        }
        if ( $data[ 'input_validation' ] == '1' && $data[ 'headerno' ] != '' && $data[ 'detno' ] != '' ) {
            $query2 = "EXEC iOS_UpdateDCRWiseInputStock_Edet '" . $data[ 'headerno' ] . "','" . $data[ 'detno' ] . "','" . $data[ 'sfcode' ] . "','" . $DivCode[ 0 ] . "'";
            performQuery( $query2 );
        }
        include 'Api_module/dcr/dcrdelete.php';
        dcrdelete( $data[ 'amc' ], $data[ 'CusType' ] );
    }
    $query = "select Count(Trans_SlNo) Cnt from vwActivity_Report where Sf_Code='" . $data[ 'sfcode' ] . "' and Activity_Date='" . date( 'Y-m-d 00:00:00.000', strtotime( $data[ 'vstTime' ] ) ) . "' and FWFlg='L'";
    $ExisArr = performQuery( $query );
    if ( $ExisArr[ 0 ][ "Cnt" ] > 0 ) {
        $result[ "Msg" ] = "Today Already Leave Posted...";
        $result[ "success" ] = false;
        outputJSON( $result );
    }
    $query = "exec ios_svDCRMain_Edet '" . $data[ 'sfcode' ] . "','" . date( 'Y-m-d 00:00:00.000', strtotime( $data[ 'vstTime' ] ) ) . "','" . $data[ 'WT_code' ] . "','" . $data[ 'WTName' ] . "','" . $data[ 'FWFlg' ] . "','" . $data[ 'town_code' ] . "','" . $data[ 'town_name' ] . "','" . $DivCode[ 0 ] . "','" . str_replace("'", "",$data['Remarks']) . "','" . $data[ 'sf_type' ] . "','" . $data[ 'state_code' ] . "','','" . $data[ 'Mod' ] . "'";
    $result[ "HQry" ] = $query;
    $tr = performQuery( $query );
    $ARCd = $tr[ 0 ][ 'ARCode' ];
    include 'Api_module/dcr/dcrsavecommon.php';
    dcrsavecommon( $data, $ARCd );
}
?>
