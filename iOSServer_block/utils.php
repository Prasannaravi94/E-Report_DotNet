<?php

function outputJSON($array) {
	 echo str_replace(".000000","",json_encode($array));
  sqlsrv_close($conn);
}

/**
 * Returns an  array of results. result signature : array( array( DB_COLOUMN1=>VALUE1,DB_COLOUMN2=>VALU2   ) )
 * @global {sqlsrv_conn} $conn db conection object
 * @param {String} $query sql query string
 * @return Boolean|Array
 */
function performQuery1($query) {
    global $conn,$NeedRollBack;
    $result = array();
    $res = sqlsrv_query($conn, $query);
    if (sqlsrv_errors() != null || !$res){
	$NeedRollBack=true;
	echo str_replace(".000000","",json_encode(sqlsrv_errors()));
			outputJSON("Err Qry:".$query);
			$myfile = fopen("../iosserver/log/errlog_".date('Y_m_d_H_i_s').".txt", "a+");
			$sqlsp="Error:".$query."\n|param:\n|error:".json_encode_unicode(sqlsrv_errors())."\n";
			fwrite($myfile, $sqlsp);
			fclose($myfile);
    }
    if ($res) {
        $qt = explode(" ", $query);
        if (strcmp(strtolower($qt[0]), "select") != 0 && strcmp($qt[0], "exec") != 0  ) {
            if (sqlsrv_errors() != null)
                return false;
            else
                return true;
        }
        else {

           $result = array();
            while ($row = sqlsrv_fetch_array($res, SQLSRV_FETCH_ASSOC)) {
                $arr = array();
                foreach ($row as $key => $value) {
		    if (is_null($value)) $value="";
		    if (is_string($value))
                        $arr[$key] = utf8_encode(trim(preg_replace("/[\r\n]+/", " ", $value)));
                    else {
                        $arr[$key] = $value;
                    }
                }

                $result[] = $arr;
            }

            return $result;
        }
    }
}
function performQuery($query) {
    global $conn,$NeedRollBack;
    $result = array();
    if ($res = sqlsrv_query($conn, $query)) {
		
		 if (sqlsrv_errors() != null || !$res){
		$NeedRollBack=true;
		//echo str_replace(".000000","",json_encode(sqlsrv_errors()));
			outputJSON("Err Qry:".$query);
			$myfile = fopen("../iosserver/log/errlog_".date('Y_m_d_H_i_s').".txt", "a+");
			$sqlsp="Error:".$query."\n|param:\n|error:".json_encode_unicode(sqlsrv_errors())."\n";
			fwrite($myfile, $sqlsp);
			fclose($myfile);
		    }
        $qt = explode(" ", $query);
        if (strcmp(strtolower($qt[0]), "select") != 0 && strcmp($qt[0], "exec") != 0  ) {
            if (sqlsrv_errors() != null)
                return false;
            else
                return true;
        }
        else {

           $result = array();
            while ($row = sqlsrv_fetch_array($res, SQLSRV_FETCH_ASSOC)) {
                $arr = array();
                foreach ($row as $key => $value) {
                    if (is_string($value))
                        $arr[$key] = utf8_encode(trim(preg_replace("/[\r\n]+/", " ", $value)));
                    else if(is_int($value)){
						$arr[$key] = (string)$value;
					}
					else {
                        $arr[$key] = $value;
                    }
                }

                $result[] = $arr;
            }

            return $result;
        }
    }
}
function isDate($value) 
{
    if (strtotime($value)) {
        return true;
	}
        return false;
}

function json_encode_unicode($data) {
	if (defined('JSON_UNESCAPED_UNICODE')) {
		return json_encode($data, JSON_UNESCAPED_UNICODE);
	}
	return preg_replace_callback('/(?<!\\\\)\\\\u([0-9a-f]{4})/i',
		function ($m) {
			$d = pack("H*", $m[1]);
			$r = mb_convert_encoding($d, "UTF8", "Windows-1256");
			return $r!=="?" && $r!=="" ? $r : $m[0];
		}, json_encode($data)
	);
}
function performQueryWP($query,$pram) {
    global $conn;
    $result = array();
    if ($res = sqlsrv_query($conn, $query,$pram)) {
        $qt = explode(" ", $query);
        if (sqlsrv_errors() != null)
		{
			outputJSON(sqlsrv_errors());
			outputJSON("Err Qry:".$query);
			$myfile = fopen("../iosserver/log/errlog_".date('Y_m_d_H_i_s').".txt", "a+");
			$sqlsp="Error:".$query."\n|param:".$pram."\n|error:".json_encode_unicode(sqlsrv_errors())."\n";
			fwrite($myfile, $sqlsp);
			fclose($myfile);
        	return false;
		}
        else
            return true;
        
    }
}
