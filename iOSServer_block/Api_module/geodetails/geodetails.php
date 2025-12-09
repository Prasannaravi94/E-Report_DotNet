<?php 
function geodetails(){ 
	global $data;  
	switch ( strtolower( $data[ 'tableName' ] ) ) {
		case "save_geo":
		$actualcount="";
		$maxcount="";
			if($data['cust']=='D'){
				$query = "select ListedDrCode as id,count(ListedDrCode) tagcnt from  Mas_ListedDr D INNER JOIN  map_GEO_Customers g ON Cust_Code = cast(ListedDrCode as varchar) where d.ListedDrCode='" . $data['cuscode'] . "' group by ListedDrCode";
				$tcount = performQuery( $query );
				$query = "select isnull(Geo_Tag_count,'1') Geo_Tag_count from Mas_ListedDr where ListedDrCode='" . $data['cuscode'] . "'";
				$tcount1 = performQuery( $query );
				$maxcount = $tcount1[ 0 ][ 'Geo_Tag_count' ];
				$actualcount = $tcount[ 0 ][ 'tagcnt' ];
				
				if ( $maxcount == "" ) {
					$maxcount = '0';
				}
				if ( $actualcount == "" ) {
					$actualcount = '0';
				}
			}
			else if($data['cust']=='C'){
				$query = "select Chemists_Code as id,count(Chemists_Code) tagcnt from  Mas_Chemists D INNER JOIN  map_GEO_ChemiCustomers g ON Cust_Code = Chemists_Code where d.Chemists_Code='" . $data['cuscode'] . "' group by Chemists_Code";
				$tcount = performQuery( $query );				
				$maxcount = '1';
				$actualcount = $tcount[ 0 ][ 'tagcnt' ];
				
				if ( $actualcount == "" ) {
					$actualcount = '0';
				}
			}
			else if($data['cust']=='S'){
				$query = "select Stockist_Code as id,count(Stockist_Code) tagcnt from  Mas_Stockist D INNER JOIN  map_GEO_StockCustomers g ON Cust_Code = Stockist_Code where d.Stockist_Code='" . $data['cuscode'] . "' group by Stockist_Code";
				$tcount = performQuery( $query );
				$maxcount = '1';
				$actualcount = $tcount[ 0 ][ 'tagcnt' ];
				
				if ( $actualcount == "" ) {
					$actualcount = '0';
				}
			}
			else if($data['cust']=='U'){
				$query = "select UnListedDrCode as id,count(UnListedDrCode) tagcnt from  Mas_UnListedDr D INNER JOIN  map_GEO_UnlistCustomers g ON Cust_Code = UnListedDrCode where d.UnListedDrCode='" . $data['cuscode'] . "' group by UnListedDrCode";
				$tcount = performQuery( $query );
				$maxcount = '1';
				$actualcount = $tcount[ 0 ][ 'tagcnt' ];
				
				if ( $actualcount == "" ) {
					$actualcount = '0';
				}
			}
			
			if ( $actualcount >= $maxcount ) {
				$result[ "Msg" ] = "You have reached the maximum tags...";
				$result[ "success" ] = false;
				outputJSON($result);
			} else {
				$query = "exec ios_saveGeo_Edet '" . $data['cuscode'] . "','" . $data['divcode'] . "','" . $data['lat'] . "','" . $data['long'] . "','" . $data['addr'] . "','" . $data['image_name'] . "','" . $data['tagged_time'] . "','" . $data['sfcode'] . "','" . $data['sfname'] . "','" . $data['tagged_cust_HQ'] . "','" . $data['mode'] . "','" . $data['cust'] . "','" . $data['version'] . "','" . $data['cust_name'] . "' ";
				performQuery($query);
				$result["success"]=true;
				$result[ "Msg" ] = "Tagged Successfully";
				outputJSON($result);
			}
			break; 
		case "get_geo":		
		// not needed
			$query="exec getViewTag '".$SF."','".$cust."'"; 
			//outputJSON( performQuery($query));
			break; 	
		default:
			$result = array();
			$result['success'] = false;
			$result['msg'] = 'Try Again';
			outputJSON($result);
            break;
	}
}

?>