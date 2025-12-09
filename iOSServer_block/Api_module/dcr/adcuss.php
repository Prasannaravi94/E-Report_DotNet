<?php
function adcuss($ARCd,$ARDCd,$Prods,$ProdsNm,$Inps,$InpsNm,$JWWrk,$JWWrkNm,$GC,$GN,$GQ,$StockNm,$data) {
	//global $data;
	 // $ProdsArr=$data['Products'];
	 $div_codes = (string) $data['division_code'];	
	 $DivCode = explode(",", $div_codes.",");
	 $DCSUPOB = (string) $data['DCSUPOB'];
	 $Drcallfeedbackcode = (string) $data['Drcallfeedbackcode'];
	 $loc = explode(":", str_replace("'", "", str_replace(" ", ":", $data['Entry_location'])) . ":");
	for($Adil=-1;$Adil<count($data["AdCuss"]);$Adil++)
    {
        if($Adil>-1){ 
            $Cus=$data["AdCuss"][$Adil];
            $CustCode=$Cus["Code"];
            $CustName=$Cus["Name"];
			$town_code=$Cus["town_code"];		
            $town_name=$Cus["town_name"];
			
            $query="exec ios_svDCRLstDet_App_Edet '".$ARCd."',0,'".$data['sfcode']."',1,'".$CustCode."','".$CustName."','".$data['vstTime']."','".$DCSUPOB."','".$JWWrk."','".$JWWrkNm."','".$Prods."','".$ProdsNm."','','','','','','','','','".$data['Remarks']."','".$DivCode[0]."','".$Drcallfeedbackcode."','".$data['ModTime']."','".$lat."','".$lng."','".$data['Rsf']."','NA','".$data['Mod']."','".$town_code."','".$town_name."','0','','','','','".$StockNm."','1','1'";
			//echo $query;
            $result["DQry"]=$query;
    	     $tr=performQuery($query);
			$ARDCd=$tr[0]['ARDCd'];
            if($ARDCd==""){
                $query="select Trans_Detail_Slno from vwActivity_MSL_Details where Trans_SlNo='".$ARCd."' and Trans_Detail_Info_Code='".$CustCode."'";
                $arr = performQuery($query);
                $ARDCd=$arr[0]["Trans_Detail_Slno"];
            }
            
        }
       /* for ($i = 0; $i < count($ProdsArr); $i++)
        {
	              $TmLn=$ProdsArr[$i]["Timesline"];
    	          $query="select isnull(Max(DDSl_No),0)+1 DDSl_No from tbDigitalDetailing_Head";
    	          $arr = performQuery($query);
    	          $DDSl = $arr[0]["DDSl_No"]; 
	              $query="insert into tbDigitalDetailing_Head(DDSl_No,Activity_Report_code,MSL_code,Product_Code,Product_Name,GroupID,Rating,StartTime,EndTime,Feedbk_Status) select ".$DDSl.",'".$ARCd."','".$CustCode."','".$ProdsArr[$i]["Code"]."','".$ProdsArr[$i]["Name"]."','".$ProdsArr[$i]["Group"]."','".$ProdsArr[$i]["Rating"]."','".$TmLn["sTm"]."','".$TmLn["eTm"]."',''";
	              performQuery($query);
	                $Prods="";
	                $ProdsNm="";
                if($ProdsArr[$i]["Group"]=="1"){
	                $Prods=$Prods.$ProdsArr[$i]["Code"]."~$#";
	                $ProdsNm=$ProdsNm.$ProdsArr[$i]["Name"]."~$#";
                }
    	        $PSlds=$ProdsArr[$i]["Slides"];
	       	for ($j = 0; $j < count($PSlds); $j++)
    	        { 
	        	$SlideNm=$PSlds[$j]["Slide"];
                    	$PSldsTM=$PSlds[$j]["Times"];
  	                for ($k = 0; $k < count($PSldsTM); $k++)
    	              	{
                        	$query="insert into tbDgDetailing_SlideDetails(DDSl_No,SlideName,StartTime,EndTime,Rating,Feedbk,usrLike) select ".$DDSl.",'".$SlideNm."','".$PSldsTM[$k]["sTm"]."','".$PSldsTM[$k]["eTm"]."','".$PSlds[$j]["SlideRating"]."','".$PSlds[$j]["SlideRem"]."','".$PSlds[$j]["usrLike"]."'";
	                	performQuery($query);
			}

                    	$Scribs=$PSlds[$j]["Scribbles"];
  			for ($k = 0; $k < count($Scribs); $k++)
    			{
             			$query="insert into tbDgDetScribFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,ScribImg,SlideNm,SlideSLNo) select '".$ARCd."','".$ARDCd."','".$data['sfcode']."','".$data['vstTime']."','".$CustCode."','Scribbles/" . $_FILES["ScribbleImg"]["name"]."','".$SlideNm."','".$DDSl."'";
	     			performQuery($query);
			}
		}
            
        }
		 if($ARDCd!="0" && count($_FILES["SignImg"])>0){
		        $query="insert into tbDgDetailingFilesDetail(AR_code,ARM_code,SF_Code,ADate,Cust_code,SignImg,FeedbkAudio) select '".$ARCd."','".$ARDCd."','".$data['sfcode']."','".$data['vstTime']."','".$CustCode."','signs/" . $_FILES["SignImg"]["name"]."',''";
		        $result["ImgQry"]=$query;
		        performQuery($query);
        }
		*/
	     
    }
      	
      	
}
?>


