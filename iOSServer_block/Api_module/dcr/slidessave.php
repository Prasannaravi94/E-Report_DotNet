<?php
function slidessave( $ARCd, $ARDCd, $data ) {
    //global $data;
    $ProdsArr = $data[ 'Products' ];
    $div_codes = ( string )$data[ 'division_code' ];
    $DivCode = explode( ",", $div_codes . "," );
    for ( $i = 0; $i < count( $ProdsArr ); $i++ ) {
        $TmLn = $ProdsArr[ $i ][ "Timesline" ];
        //$query="select isnull(Max(DDSl_No),0)+1 DDSl_No from tbDigitalDetailing_Head";
        // $arr = performQuery($query);
        // $DDSl = $arr[0]["DDSl_No"]; 
        //changes done by bala to save slide in detailing number ddsl_no
        $query = "select DivSH,Max_Sl_No_Main,SFSlNo,DetailingSl from DCR_detailingSlNo where SF_Code='" . $data[ 'sfcode' ] . "'";
        $arr1 = performQuery( $query );
        //echo count($arr1);
        if ( count( $arr1 ) == 0 ) {
            $query = "select Division_SName from mas_division where division_code='" . $DivCode[ 0 ] . "'";
            $arrs = performQuery( $query );
            $query = "insert into DCR_detailingSlNo VALUES ('" . $data[ 'sfcode' ] . "','" . $arrs[ 0 ][ 'Division_SName' ] . "','1','" . $DivCode[ 0 ] . "','1','1','1')";
            performQuery( $query );
            $DDSl = $arrs[ 0 ][ 'Division_SName' ] . '1' . '-' . '1' . '-' . $data[ 'sfcode' ];
        } else {
            $detsl = $arr1[ 0 ][ "DetailingSl" ] + 1;
            $query = "update DCR_detailingSlNo set DetailingSl='" . $detsl . "',Max_Sl_No_Main='" . $ARCd . "',Max_Sl_No_Detail='" . $ARDCd . "' where SF_Code='" . $data[ 'sfcode' ] . "'";
            performQuery( $query );
            $DDSl = $arr1[ 0 ][ "DivSH" ] . $arr1[ 0 ][ "SFSlNo" ] . '-' . $detsl . '-' . $data[ 'sfcode' ];
        }

        $query = "insert into tbDigitalDetailing_Head(DDSl_No,Activity_Report_code,MSL_code,Product_Code,Product_Name,GroupID,Rating,StartTime,EndTime,Feedbk_Status) select '" . $DDSl . "','" . $ARCd . "','" . $data[ 'CustCode' ] . "','" . $ProdsArr[ $i ][ "Code" ] . "','" . $ProdsArr[ $i ][ "Name" ] . "','" . $ProdsArr[ $i ][ "Group" ] . "','" . $ProdsArr[ $i ][ "Rating" ] . "','" . $TmLn[ "sTm" ] . "','" . $TmLn[ "eTm" ] . "','" . $ProdsArr[ $i ][ "ProdFeedbk" ] . "'";
        //print_r($query);die;
        performQuery( $query );
        // echo $query;
        $Prods = "";
        $ProdsNm = "";
        if ( $ProdsArr[ $i ][ "Group" ] == "1" ) {
            $Prods = $Prods . $ProdsArr[ $i ][ "Code" ] . "~$#";
            $ProdsNm = $ProdsNm . $ProdsArr[ $i ][ "Name" ] . "~$#";
        }
        $PSlds = $ProdsArr[ $i ][ "Slides" ];
        for ( $j = 0; $j < count( $PSlds ); $j++ ) {
            $SlideNm = $PSlds[ $j ][ "Slide" ];
            $PSldsTM = $PSlds[ $j ][ "Times" ];
            for ( $k = 0; $k < count( $PSldsTM ); $k++ ) {
                $query = "insert into tbDgDetailing_SlideDetails(DDSl_No,SlideName,StartTime,EndTime,Rating,Feedbk,usrLike) select '" . $DDSl . "','" . $SlideNm . "','" . $PSldsTM[ $k ][ "sTm" ] . "','" . $PSldsTM[ $k ][ "eTm" ] . "','" . $PSlds[ $j ][ "SlideRating" ] . "','" . $PSlds[ $j ][ "SlideRem" ] . "','" . $PSlds[ $j ][ "usrLike" ] . "'";
                performQuery( $query );
            }

            $Scribs = $PSlds[ $j ][ "Scribbles" ];
            for ( $k = 0; $k < count( $Scribs ); $k++ ) {
                $query = "insert into tbDgDetScribFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,ScribImg,SlideNm,SlideSLNo) select '" . $ARCd . "','" . $ARDCd . "','" . $data[ 'sfcode' ] . "','" . $data[ 'vstTime' ] . "','" . $data[ 'CustCode' ] . "','Scribbles/" . $PSlds[ $j ][ "Scribbles" ] . "','" . $SlideNm . "','" . $DDSl . "'";
                performQuery( $query );
            }
        }
    }
    // if($ARDCd!="0" && count($_FILES["SignImg"])>0){
    // $query="insert into tbDgDetailingFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,SignImg,FeedbkAudio) select '".$ARCd."','".$ARDCd."','".$data['sfcode']."','".$data['vstTime']."','".$data['CustCode']."','signs/". $_FILES["SignImg"]["name"]."',''";
    // $result["ImgQry"]=$query;
    // performQuery($query);
    // }
}
?>