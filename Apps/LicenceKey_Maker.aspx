<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LicenceKey_Maker.aspx.cs" Inherits="MasterFiles_LicenceKey_Maker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <script src="../JsFiles/jquery-1.10.1.js"></script>
        <script src="../JsFiles/jquery-ui-1.10.3.js"></script>
        <style type="text/css">
            th,td {
                text-align:left;
                border-bottom:solid 1px #808080;
                padding:5px 10px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:block;width:80%;margin:auto;text-align:left;font-weight:bold;background:#F44336;padding: 10px;font-size: 25px;">Key Generator -  <b style="color:#fff"><%=HttpContext.Current.Request.ServerVariables["HTTP_HOST"]%></b></div>
    <div style="display:block;width:80%;margin:auto;padding: 10px;">
        <div class="row">
            <div class="col col-sm-12">
               <div class="row">
                   <div class="col-sm-3"><div>Web URL</div><input type="text" class="input" id="tLicWB" style="display:block;width:100%"  /></div>
                   <div class="col-sm-3"><div>Key For</div><select id="idKeyFor" onchange="LoadKeys()" style="display:block;width:100%"><option value="">----Select the Type----</option><option value="App">APP</option><option value="Det">Detailing</option></select></div>
                   <div class="col-sm-2"><div>Licence Key</div><input id="idLicKey"  type="text" class="input tLicKey"  style="display:block;width:100%"/></div>
                   <div class="col-sm-2"><div>Division Code</div><input type="text" id="idLicDiv"  class="input tLicKey"  style="display:block;width:100%"/></div><!--Company / Division Name <select id="idKeyTo" onfocus="LoadComp()"></select>-->
                   <div class="col-sm-2"><div>Short Name</div><input id="idLicDivSH"  type="text" class="input tLicKey"  style="display:block;width:100%"/></div>
               </div>
                </div>
            </div>
        <div class="row" style="padding:40px 0px">
            <div class="col col-sm-12" style="text-align:center;">
                <input type="button" style="background: #8BC34A;border: 1px solid #009688;padding: 5px 30px;margin:auto;" onclick="svLicKey()" value="Create License Key" />
            </div>
        </div>
        <div class="row">
            <div class="col col-sm-12">
               <div class="row">
                   
                   
               </div>
            </div>
            <div class="col col-sm-12">
                <div style="padding:10px 0px;background:#fff;">
                    <table id="KeyList" style="width:100%">
                        <thead><th>#</th><th>Base Url</th><th>Licence Key</th><th>Division</th><th>Short Name</th><th>Edit</th><th>Delete</th></thead>
                        <tbody></tbody>
                    </table>
                </div>
               <%-- "weburl" : "http://sanffr.info/",
		"baseurl":"Server/db_v5.php?axn=",
		"vCardUrl": "Visiting_Card/",
		"mailPath": "MasterFiles/Mails/Attachment/",
		"uploadPath" : "SFM/medUpDt/",
		"division":"4",
		"logoimg":"Apps/eDetCompImg/DEMO2903IC.png"--%>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $Comps = [];
        $Keys = [];
        $AllKeys = [];
		$("#tLicWB").val("<%=HttpContext.Current.Request.ServerVariables["HTTP_HOST"]%>");
        function LoadComp() {
            if ($("#tLicWB").val()==""){
                alert("Enter the Web Url");
                return false;
            }
            loadData("getCompany", "{'Url':'" + $("#tLicWB").val() + "'}", function ($data) {
                $Keys = $data
                console.log($Comps)
            }, function () { });
        }
        function LoadKeys() {
            if ($("#tLicWB").val() == "") {
                alert("Enter the Web Url");
                return false;
            }
            if ($("#idKeyFor > option:selected").val() == "") return;
            if ($("#tLicWB").val().indexOf('http') < 0) { $("#tLicWB").val('http://'+$("#tLicWB").val()) }

            url = $("#tLicWB").val()
            var lastChar = url[url.length - 1];
            if (lastChar != "/") url = url + "/"
			$("#tLicWB").val(url);
            loadData("getKeys", "{'Url':'" + $("#tLicWB").val() + "','Typ':'" + $("#idKeyFor > option:selected").val() + "'}", function ($data) {
                $Keys = $data
                $AllKeys = $data
                ReloadTable()
            }, function () { });
            
        }
        function loadData(key,param,success,fail) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //   async: false,
                url: "LicenceKey_Maker.aspx/" + key,
                data: param,
                dataType: "json",
                success: function (data) {
                    source = JSON.parse(data.d) || [];
                    success(source);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }

        $("#idLicDiv").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val();
                $Keys = $AllKeys.filter(function (a) {
                    if (a.config["division"] != null && (";" + a.config["division"]).toString().toLowerCase().indexOf(";" + shText.toLowerCase()) > -1) return true; // && (',' + searchKeys).indexOf(',' + key + ',') > -1
                    /*$.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })*/
                })
            }
            else
                $Keys = $AllKeys
            ReloadTable();
        })

        $("#idLicKey").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val();
                $Keys = $AllKeys.filter(function (a) {
                    if (a.key != null && (";"+a.key).toString().toLowerCase().indexOf(";"+shText.toLowerCase()) > -1) return true; // && (',' + searchKeys).indexOf(',' + key + ',') > -1
                    /*$.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })*/
                })
            }
            else
                $Keys = $AllKeys
            ReloadTable();
        })

        function ReloadTable() {
            $("#KeyList TBODY").html(""); TerrList = "";
            for ($i = 0; $i < $Keys.length; $i++) {
                tr = $("<tr></tr>");
                $(tr).html("<td>"+($i+1)+"</td><td>" + $Keys[$i].config["weburl"] + "</td><td>" + $Keys[$i].key + "</td><td>" + $Keys[$i].config["division"] + "</td><td>" + (($("#idKeyFor > option:selected").val()=='Det')?$Keys[$i].config["slideurl"].replace(/Edetailing_files\//gi, '').replace(/\/download\//gi, ''):'') + "</td><td>Edit</td><td>Delete</td>");
                
                $("#KeyList TBODY").append(tr);
            }
            //$("#orders_info").html("Showing " + Orders.length + " entries")
        }
        function svLicKey() {
            $Key={}
            $Key.key=$("#idLicKey").val();

            config = JSON.parse(JSON.stringify($AllKeys[0].config));
            url = $("#tLicWB").val()
            var lastChar = url[url.length - 1];
            if (lastChar != "/") url = url + "/";
            config.weburl=url;
            config.division = $("#idLicDiv").val();
            if($("#idKeyFor > option:selected").val()=="Det")
            config.slideurl = "Edetailing_files\/" + $("#idLicDivSH").val() + "\/download\/";
            $Key.config = config;
            $AllKeys.push($Key);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //   async: false,
                url: "LicenceKey_Maker.aspx/svData",
                data: "{'data':'" + JSON.stringify($AllKeys) + "','Typ':'" + $("#idKeyFor > option:selected").val() + "','path':'<%=SrvPath.Replace("\\","/")%>'}",
                dataType: "json",
                success: function (data) {
                    alert("Key Generated Successfully..");
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
    </script>
</body>
</html>
