<%@ Page Title="Location Finder" Language="C#" AutoEventWireup="true"
    CodeFile="Location_Finder.aspx.cs" Inherits="Default4" %>

<html>
<head>

    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/DCR_Entry.css" rel="stylesheet" type="text/css" /> 
    
<style type="text/css">
#map_canvas,#tbMain,#modal,body {
    width: 100%;
    height: 100%;
	padding:0px;
	margin:0px;
}
#modal{
	display:none;
	position: absolute;
	background-color:rgba(0,0,0,0.5);
	z-index: 10000;    
	line-height: 50;
    height: 100%;
    text-align: center;color: #fff;
}
#vstDet > ul{
    list-style-type: none;
    -webkit-margin-start: 0em;
    -webkit-margin-before: 0em;
    -webkit-padding-start: 0px;
}
#vstDet > ul > li{
margin-bottom: 5px;
}
table{    border-collapse: collapse;}
.k-icon{
margin:13px 5px;
}
#vstCnt
{
     position: absolute;
    display: block;
    width: 20px;
    height: 20px;
    margin: -27px 76px;
    line-height:20px;
    text-align:center;
    border-radius: 90%;
    font-size: 75%;
    background: rgb(82, 208, 139);
}
.ColHeader{
    display: block;
    background: #f1f1f1;
    margin: -1px -2px;
    padding: 5px;
}
form{margin:0px}
.pad{padding:0px}
.mnu-bt,.home-bt{margin:3px}
.my-custom-class-for-label1 {
}
.my-custom-class-for-label {
	width: 20px;
    height: 20px;
    padding: 2px;
    border: 1px solid #eb3a44;
    border-radius: 100%;
    background: #fee1d7;
    text-align: center;
    line-height: 20px;
    font-weight: bold;
    font-size: 14px;
    color: #eb3a44;
}
.my-custom-class-for-label1:after {
       position: fixed;
    border: solid transparent;
    content: " ";
    height: 0;
    width: 0;
    pointer-events: none;
    border-color: rgba(255, 255, 255, 0);
    border-top-color: #e8616b;
    border-width: 10px;
    margin: 3px;
    margin-top: -4px;
}
</style>

</head>
<body>
    <form id="form1" runat="server"><div id="modal" >Loading....</div>
	<table id="tbMain" >
		<tr>
			<td colspan=3 valign="top" style="height:1%;background-color: #f1f1f1;border-bottom:solid 1px #cacaca">
                <%--<div class="pad HDBg">
                    <a href="#" class="button mnu-bt" onclick="toggleMenu()"><i class="fa fa-bars"></i></a>
                    <a runat="server" href="BasicMaster.aspx" id="aHome" class="button home-bt">Home</a>
                    <div class='hCap' style="width:300px;overflow: hidden;">Location Finder</div>
                    <div class='hCap' id="spWT" style="width:230px;white-space:nowrap;overflow: hidden;text-overflow: ellipsis;">
                        </span></div>
                    <div class='hCap' id="spDis" style="width:300px;white-space:nowrap;overflow: hidden;text-overflow: ellipsis"></div>
                    <div class='hCap' id="spRT" style="width:300px;white-space:nowrap;overflow: hidden;text-overflow: ellipsis"></div>

                </div>--%>
    
				<table style="display:none" >
					<tr>
						<td>Employee Name : </td>
						<td valign="top">
							<asp:DropDownList ID="selSF" onchange="getMyTP();Callback()" runat="server" SkinID="ddlRequired" Width="210px" CssClass="ddl">
                   			</asp:DropDownList>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<%--<td style="min-width:250px;" valign="top"><div id="sideMnu">
						<div class="ColHeader">Employee Name</div>
                          <div id="smartTree" style="display:block;height:320px;overflow:auto;-webkit-overflow-scrolling: touch;"></div>
                        <div id="calendar"></div>
                        </div></td>--%>
			<td width="100%" valign="top">
	    		<div id="map_canvas" class="mapping"></div>
			</td>
			<td valign="top" style="width: 100% !important;">
				<div style="display:none"><div  class="panel-heading" style="display:block;white-space:nowrap;padding: 10px 15px;background: #ff865c;color: #fff;">Kilometers Traveled</div>
				<span id="total" style="display:block;white-space:nowrap;padding: 10px 15px;font-weight:bold;border:solid 1px #000;border-radius: 0px 0px 5px 5px;border-top-width: 0px;"></span>
				<small><small><br></small></small>
                </div>
                <div style="background:#f1f1f1;">
                <table style="width:100%">
                   <%-- <tr>
                        <td>Start </td><td>:</td><td id="sTM" style="width:100%;color: #00b0ff;background-color: #dbdbdb;padding:5px;"></td>
                    </tr>
                    <tr>
                        <td>End</td><td>:</td><td id="eTM" style="width:100%;color: #00b0ff;background-color: #dbdbdb;padding:5px;"></td>
                    </tr>--%>
                </table>
                </div>
				<div id="mvstDet" style="display:block;height:90.5%;white-space:nowrap;font-weight:bold;border:solid 1px #000;border-radius: 0px 0px 5px 5px;border-top-width: 0px;">
                <div class="panel-heading" style="display:block;white-space:nowrap;padding: 10px 15px;background: #ff865c;color: #fff;">Calls Detail<span id="vstCnt"></span><span id="dspDt" style="float:right;"></span></div>
				<div id="vstDet" style="display:block;padding: 5px;min-width:253px;height:87vh;overflow: auto;"></div>
                <div style="display:block;background:#dcdcdc;"><table style="width:100%;color:black;font:10px tahoma;font-weight:bold"><tr><td style="padding: 5px;">Tot.POB (value) : <span id="tpval" style="color:Green"></span></td><td style="padding: 5px;">Tot.POB (Ltrs) : <span id="tltrs" style="color:Green"></span></td></tr></table></div></div>

			</td>
		</tr>
	</table>

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>

<script src="https://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyC8mxU5lhFzKEAbRBrEAvZb1p4OZ0j1mmQ&libraries=geometry,places&ext=.js"></script>
<script type="text/javascript" src="https://cdn.sobekrepository.org/includes/gmaps-markerwithlabel/1.9.1/gmaps-markerwithlabel-1.9.1.min.js"></script>

<script type="text/javascript">
    var datas = [];
	function genTreeView(){
	    $.fn.smartTree.init($("#smartTree"), {
	        data: {
	            simpleData: {
	                enable: true
	            }
	        },
	        edit: {
	            enable: true,
	            drag: {}
	        },
	        callback: {
                onClick:function(e,itm,n){
	                $("#selSF").val(n.id); 
					$("#modal").css('display','block');
					getMyTP();
                    Callback();
                    return true;},
	            beforeDrag: function() {
	                console.log("=beforeDrag");
	                return true;
	            },
	            beforeDragOpen:null,
	            beforeDrop:null,
	            beforeEditName:null,
	            beforeRename:null,
	            onDrag: function() {
	                console.log("=onDrag")
	            },
	            onDragMove: function() {
	                console.log("=onDragMove")
	            },
	            onDrop: function() {
	                console.log("=onDrop")
	            },
	            onRename:null
	        }
	    }, datas);
	
	}

    toggleMenu=function(){
       if($("#sideMnu").css("margin-left")=="0px"){
       $("#sideMnu").css("margin-left","-250px");
       $("#sideMnu").parent().css("display","none");
       }else{
        $("#sideMnu").css("margin-left","0px");        
        $("#sideMnu").parent().css("display","table-cell");
        }
    }

    



    // Multiple Markers
    var infoWindow;
    var slatlng="";
    var CusCnt=0,tpval=0,tltrs=0;
    var markers = [];
	var prvMarkers=[];
	var NavMarkers=[];
    var path;
	var poly;
	var map;
    var geocoder ;
	var KMTot=0;
	var z=14 ;
	var Loaded=false;
	
	selDt="";
	cdt = new Date();
	var QStr = window.location.href.split('/');
	selDt = QStr[10] + '-' + QStr[9] + '-' + QStr[8] + ' 00:00:00.000';
	
	/*jQuery(function($) {
		// Asynchronously Load the map API 
		var script = document.createElement('script');
		script.src = "http://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyC8mxU5lhFzKEAbRBrEAvZb1p4OZ0j1mmQ&callback=initMap";
		document.body.appendChild(script);
		
	});*/

	google.maps.event.addDomListener(window, 'load', initMap);
	function initMap(){
		var mapOptions = {
			mapTypeId: 'roadmap',zoom:17,center: {lat: 13, lng: 80},
		};
		map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        
        geocoder = new google.maps.Geocoder;
		$("#modal").css('display','block');
		Callback(selDt);
		//getMyTP(selDt);
	} 
 	var timer;

	function Callback(DtTm,movMark){
		if(!movMark) movMark=false;
		if(!DtTm) DtTm=selDt;
		KMTot=0;
	    $.get("/server/old/db.php?axn=get/dcrtrack&SF_Code=" +  QStr[6] + "&Dt=" + QStr[10] + "-" + QStr[9] + "-" + QStr[8] + "&Mode="+QStr[12] +" ",
			function (response) {
				if(movMark==true){
					lastIndx=(markers.length>0)?markers.length:0;
                    var newMarkers=JSON.parse(response)||[];
					markers = $.merge(markers,newMarkers);
					if (newMarkers.length>0) moveMarker(lastIndx);
					
				}else{ 
					markers = JSON.parse(response);
					clearLocations();
					if (markers.length>0) initMarkers();   
				}
				cdt=new Date();
				$("#modal").css('display', 'none');
//				var QStr = window.location.href.split('/');
//				if (selDt == QStr[10] + '-' + QStr[9] + '-' + QStr[8] + ' 00:00:00.000')
//                { 
//                    
//    				window.clearTimeout(timer);
//					timer =setTimeout(function(){
//			            var aSync;
//                        if(markers.length>0)
//                        {
//                        lastDt=markers[markers.length-1].DtTm.date+'.000';
//                        aSync=true;
//                        }else{
//                        lastDt=selDt;
//                        aSync=false;
//                        }
//                        Callback(lastDt,aSync);
//                    },1000*5); 
//                }
			})
			.fail(function (response) {
				$("#modal").css('display','none');
				console.log("Connection Failed");
			});
	}

	function clearLocations() {
		for (var i = 0; i < prvMarkers.length; i++) {
			prvMarkers[i].setMap(null);
		}
		prvMarkers=[];
        NavMarkers=[];
		if(poly!=null){
			poly.setPath([]);
			poly.setMap(null);
		}
        
        $("#sTM").html("&nbsp;");
        $("#eTM").html("&nbsp;");
        $("#tpval").html(roundNumber(0,2));
        $("#tltrs").html(roundNumber(0,2));
        $("#vstCnt").html(0);
        $("#vstDet").html("");
	}
    var pPos=null;
	function moveMarker(lastIndx){
		var latLng = new google.maps.LatLng(49.47805, -123.84716);
		var homeLatLng = new google.maps.LatLng(49.47805, -123.84716);


	    var im = 'img/bluecircle.png';
	    var im1 = 'img/Trans.png';
		var position, marker, i;

        var src = new google.maps.LatLng(markers[0].lat, markers[0].lng);
		Vstlbl=0;
		for( i = lastIndx; i < markers.length; i++ ) {
			pos= new google.maps.LatLng(markers[i].lat, markers[i].lng);

            if(slatlng.indexOf(markers[i].lat+":"+ markers[i].lng+";")>-1){
                var rews = new RegExp(markers[i].lat+":"+ markers[i].lng+";","gi");
                var re = new RegExp(markers[i].lat+":"+ markers[i].lng,"gi");
                rptcnt=slatlng.replace(re,'').length-slatlng.replace(rews,'').length;
                lt=pos.lat()+ (rptcnt/(1000000-7));
                lg=pos.lng()+ (rptcnt/(100000-7));
			    position = new google.maps.LatLng(lt,lg);
            }
            else 
                position=pos;
                
			if (i==0 || NavMarkers.length<1 || markers[i].ordfld==0){
		

				if(markers[i].ordfld==0) 
			{ 
				Vstlbl++; 
                markers[i]["SLNo"]=Vstlbl.toString();
			 var marker = new MarkerWithLabel({
				   position: position,
							 map: map,
							 icon: '/img/sticker/empty.png',
							 shadow: '/img/sticker/bubble_shadow.png',
							 transparent: '/img/sticker/bubble_transparent.png',
							 draggable: false,
							 raiseOnDrag: false,
							 labelContent: "<div class='my-custom-class-for-label'>"+Vstlbl.toString()+"</div>",
							 labelAnchor: new google.maps.Point(13, 65),
							 labelClass: "my-custom-class-for-label1", // the CSS class for the label
							 labelInBackground: false
							});
			}
			else
			{
				marker = new google.maps.Marker({
					position: position,
					map: map,
					title: markers[i].DtTime					
				});
			}
                slatlng+=markers[i].lat+":"+ markers[i].lng+";"
				prvMarkers.push(marker);

                if(i>0 && NavMarkers.length<1 && markers[i].ordfld!=0) 
                {
                    marker.setIcon(im);
                    NavMarkers.push(marker);
                }else{
					if (i>0){
						marker.setIcon(im1);
						marker.setTitle( markers[i].DtTime);
					}
                    //NavMarkers.push(marker);
				}

				if(markers[i].ordfld==0) 
                {
                
			        dte=new Date(markers[i].DtTime);
                    dy=dte.getDate();
                    mn=dte.getMonth()+1;
                    hh=dte.getHours();
                    mm=dte.getMinutes();
                    ss=dte.getSeconds()
  			        ddmm= ((dy<10)?'0':'')+dy+ ' / ' + ((mn<10)?'0':'')+mn  + ' / ' +  dte.getFullYear() ;
                    sHH=((hh>12)?hh-12:hh)
                    sHH=((sHH<10)?'0':'')+sHH
                    $("#dspDt").html(ddmm);
                    CusCnt++;
                    tpval+=((markers[i].POB_Value!='0')?parseFloat(markers[i].POB_Value):0);
                    tltrs+=((markers[i].net_weight_value!='0')?parseFloat(markers[i].net_weight_value):0);
                    sStr+='<li><div style="display:block;"><table style="width:100%"><tr style="background:#dcdcdc;"><td style="padding:5px ;border-left:solid 2px #dcdcdc;"><font style="color:darkblue;font:15px tahoma;">'+ markers[i].Trans_Detail_Name + '</td>'
                            +'<td style="padding:5px;border-right:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + sHH +":"+((mm<10)?'0':'')+mm+":"+((ss<10)?'0':'')+ss+" "+((hh>11)?'PM':'AM') + '</a></td></tr>'  
                           // + '<tr><td colspan=2 style="padding:5px;font:12px verdana;border:solid 2px #dcdcdc;border-top-width:0px;border-radius: 0px 0px 5px 5px;">Visited Place : <br><font style="color:darkgreen;">' + markers[i].GeoAddrs + '</font></td></tr>' 
                            + '</table></div></li>';
                    marker.setIcon('img/shop.png');

				    google.maps.event.addListener(marker, 'click', (function(marker, i) {
					    return function() {
                            var latlng = {lat: parseFloat(markers[i].lat), lng: parseFloat(markers[i].lng)};
                            geocoder.geocode({'location': latlng}, function(results, status) {
                                if (status === 'OK') {
                                    if (results[0]) {
                                      infoWindow.setContent('<div style="display:block;width:500px;"><table style="width:100%"><tr><td style="padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;text-transform: capitalize;"><font style="color:darkblue;font:15px tahoma;">'+markers[i].Trans_Detail_Name +' - ( <b>'+ markers[i]["SLNo"]+ '</b> )</td>'
                                        +'<td style="padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + markers[i].DtTime + '</a></td></tr>'  
                                        + '<tr><td colspan=2 style="padding:5px;font:12px verdana;">Visited Place : <br><font style="color:darkgreen;"><span>' + results[0].formatted_address + '</font></td></tr>' 
                                        + '</table></div>'
                                        );
						                infoWindow.open(map, marker);
                                    } else {
						                infoWindow.setContent('<div style="display:block;width:500px;"><table style="width:100%"><tr><td style="padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;text-transform: capitalize;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name +' - ( <b>'+ markers[i]["SLNo"]+ '</b> )</td>'
                                        +'<td style="padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + markers[i].DtTime + '</a></td></tr>'  
                                        + '<tr><td colspan=2 style="padding:5px;font:12px verdana;">Visited Place : <br><font style="color:darkgreen;"><span>No results found</font></td></tr>' 
                                        + '</table></div>'
                                        );
						                infoWindow.open(map, marker);
                                     
                                    }
                                } else {                
                                    infoWindow.setContent('<div style="display:block;width:500px;"><table style="width:100%"><tr><td style="padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;text-transform: capitalize;"><font style="color:darkblue;font:15px tahoma;">' + markers[i].Trans_Detail_Name +' - ( <b>'+ markers[i]["SLNo"]+ '</b> )</td>'
                                        +'<td style="padding:0px 5px 10px 5px ;border-bottom:solid 2px #dcdcdc;"><a style="color:#00303f; font:bold 10px verdana;float: right;">' + markers[i].DtTime + '</a></td></tr>'  
                                        + '<tr><td colspan=2 style="padding:5px;font:12px verdana;">Visited Place : <br><font style="color:darkgreen;"><span>Address Loading issue</font></td></tr>' 
                                        + '</table></div>'
                                        );
						                infoWindow.open(map, marker);
                                }
                            });
					    }
				    })(marker, i))
                }
			}
			
			path.push(position);
			poly.setPath(path);
			if ((i + 1) < markers.length) {
				var des = new google.maps.LatLng(markers[i+1].lat, markers[i+1].lng);
                dis=getDistance(src, des,"K");
                if (dis>0.130) 
				var src =des; else dis=0;
				KMTot+=dis;

			}
            if(NavMarkers.length>0) 
            {
		        NavMarkers[0].setTitle( markers[i].DtTime);
		        NavMarkers[0].setPosition(position);
            }
			map.setCenter(position);
		}
		sStr+="</ul>";
        $("#sTM").html(markers[0].DtTime);
        $("#eTM").html(markers[markers.length-1].DtTime);
        $("#tpval").html(roundNumber(tpval,2));
        $("#tltrs").html(roundNumber(tltrs,2));
        $("#vstCnt").html(CusCnt);
        $("#vstDet").html(sStr);
		$("#total").html(roundNumber(KMTot,2)+ " Km");
		/*var lat = markers[markers.length-1].lat;
		var lng = markers[markers.length-1].lng;
        cpos=new google.maps.LatLng(lat, lng);
        if(pPos!=cpos){
		    NavMarkers[0].setPosition(cpos);
		    path.push(cpos);
	        poly.setPath(path);
            pPos!=cpos
        }*/
	}
	function initMarkers() {
		path = new google.maps.MVCArray();
		poly = new google.maps.Polyline({
			strokeColor	: '#FF8200',
		  strokeOpacity	: 0.5,
			strokeWeight: 15
		});
		poly.setMap(map);
        
        infoWindow = new google.maps.InfoWindow()
	    sStr='<ul>';
		// Display multiple markers on a map
        CusCnt=0;tpval=0;tltrs=0;
        moveMarker(0);
        
		

	}
	function getDistance(latlon1, latlon2, unit) {
		var radlat1 = Math.PI * latlon1.lat()/180;
		var radlat2 = Math.PI * latlon2.lat()/180;
		var theta = latlon1.lng()-latlon2.lng();
		var radtheta = Math.PI * theta/180
		var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
		dist = Math.acos(dist)
		dist = dist * 180/Math.PI
		dist = dist * 60 * 1.1515
		if (unit=="K") { dist = dist * 1.609344 }
		if (unit=="N") { dist = dist * 0.8684 }
		if(isNaN(dist)) dist=0;
		return dist
	}
	roundNumber = function(number, precision) {
		precision = Math.abs(parseInt(precision)) || 0;
		var multiplier = Math.pow(10, precision);
		return (Math.round(number * multiplier) / multiplier);
	}
 
	

	getMyTP = function (TPDt) {
	    if (!TPDt) TPDt = selDt;
	    $('#spWT').html("Loading...");
	    $('#spDis').html("");
	    $('#spRT').html("");
	    var QStr = window.location.href.split('/');
	    $.get("/server/old/db.php?axn=get/mytp&SF_Code=" + QStr[6] + "&Dt=" + QStr[10] + "-" + QStr[9] + "-" + QStr[8] + "",
			function (response) {
			    var myTP = JSON.parse(response) || [];
			    $('#spWT').html("");
			    if (myTP.length > 0) {
			        $('#spWT').html("Worktype : <b style='color:#0cad01'>" + myTP[0].Wtype + "</b>");
			        $('#spDis').html("Distributor : <b style='color:blue'>" + myTP[0].Stockist_Name + "</b>");
			        $('#spRT').html("Route : <b style='color:blue'>" + myTP[0].ClstrName + "</b>");
			    }
			})
			.fail(function (response) {
			    $('#spWT').html("");
			    console.log("Connection Failed");
			});
	}
</script>


	</form>
</body>
</html>