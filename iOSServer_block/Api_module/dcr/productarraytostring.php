<?php
function productarraytostring($data) {
	//global $data;
	
	$ProdsArr=$data['Products'];
	// 1)include product [sides]
    $Prods="";
    $ProdsNm="";
		
   for ($i = 0; $i < count($ProdsArr); $i++)
    {
        $stPC=$ProdsArr[$i]["Code"];
        $stPN=$ProdsArr[$i]["Name"];
		//Changes done by bala to save only products not slide in detail table
			

		if($ProdsArr[$i]["Group"]=="0"){
    
	      if($ProdsArr[$i]["SmpQty"]=="") $ProdsArr[$i]["SmpQty"]="0";
	      $Prods=$Prods.$stPC."~".$ProdsArr[$i]["SmpQty"];
	      $ProdsNm=$ProdsNm.$stPN."~".$ProdsArr[$i]["SmpQty"];
		  $pfb="";
			if($ProdsArr[$i]["prdfeed"]!=null) {
				$pfb=explode(",",$ProdsArr[$i]["prdfeed"]);
			}
          //if($data['CusType']=="1" || $data['CusType']=="2"){
			if($ProdsArr[$i]["RxQty"]=="") $ProdsArr[$i]["RxQty"]="0";
			if($pfb==""){
				$Prods=$Prods."$".$ProdsArr[$i]["RxQty"]."$0^0";
				$ProdsNm=$ProdsNm."$".$ProdsArr[$i]["RxQty"]."$0^0";
			}
			else{
            $Prods=$Prods."$".$ProdsArr[$i]["RxQty"]."$0^0$".$pfb[0];
            $ProdsNm=$ProdsNm."$".$ProdsArr[$i]["RxQty"]."$0^0$".$pfb[1];
			}
        //}
	      $Prods=$Prods."#";
	      $ProdsNm=$ProdsNm."#";
		  if($ProdsArr[$i]["StockistName"]=="") $ProdsArr[$i]["StockistName"]="0";
		if($ProdsArr[$i]["StockistCode"]!="0"){
			$StockNm=$StockNm.$stPC."$".$stPN."~".$ProdsArr[$i]["StockistName"]."^".$ProdsArr[$i]["StockistCode"];
			$StockNm=$StockNm."#";
		}else{
			$StockNm=$StockNm.$stPC."$".$stPN."~".$ProdsArr[$i]["StockistName"]."^"."0";
			$StockNm=$StockNm."#";
		}
		if($StockNm==null||$StockNm==""||$StockNm=="''") $StockNm="StockistName";
		}
		
	
		 		    
    }
	$products=array();
	$products['Prods']=$Prods;
	$products['ProdsNm']=$ProdsNm;
	$products['StockNm']=$StockNm;
	return $products;
}
?>


