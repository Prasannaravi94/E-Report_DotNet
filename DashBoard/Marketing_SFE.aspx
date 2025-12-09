<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Marketing_SFE.aspx.cs" Inherits="DashBoard_Marketing_SFE" %>

<%@ Register Src="~/UserControl/DashBoard.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .aclass
        {
            border: 1px solid lighgray;
        }
        .aclass
        {
            width: 50%;
        }
        .aclass tr td
        {
            background: White;
            font-weight: bold;
            color: Black;
            border: 1px solid black;
            border-collapse: collapse;
        }
        .aclass th
        {
            border: 1px solid black;
            border-collapse: collapse;
            background: LightBlue;
        }
        .lbl
        {
            color: Red;
        }
        
        
        .space
        {
            padding: 3px 3px;
        }
        .sp
        {
            padding-left: 11px;
        }
        
        .style6
        {
            padding: 3px 3px;
            height: 28px;
        }
        .marRight
        {
            margin-right: 35px;
        }
        
        .boxshadow
        {
            -moz-box-shadow: 3px 3px 5px #535353;
            -webkit-box-shadow: 3px 3px 5px #535353;
            box-shadow: 3px 3px 5px #535353;
        }
        .roundbox
        {
            -moz-border-radius: 6px 6px 6px 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px 6px 6px 6px;
        }
        .grd
        {
            border: 1;
            border-color: Black;
        }
        .roundbox-top
        {
            -moz-border-radius: 6px 6px 0 0;
            -webkit-border-radius: 6px 6px 0 0;
            border-radius: 6px 6px 0 0;
        }
        .roundbox-bottom
        {
            -moz-border-radius: 0 0 6px 6px;
            -webkit-border-radius: 0 0 6px 6px;
            border-radius: 0 0 6px 6px;
        }
        .gridheader, .gridheaderbig, .gridheaderleft, .gridheaderright
        {
            padding: 6px 6px 6px 6px;
            background: Pink url(images/vertgradient.png) repeat-x;
            text-align: center;
            font-weight: bold;
            text-decoration: none;
            color: Blue;
        }
        .gridheaderleft
        {
            text-align: left;
        }
        .gridheaderright
        {
            text-align: right;
        }
        .gridheaderbig
        {
            font-size: 135%;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: white;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript">
        function HideMenu_Det() {

            var HideMenu = document.getElementById("pnl");
            if (HideMenu.style.display == "none")
                HideMenu.style.display = "block";
            else
                HideMenu.style.display = "none";
            return;

        }

    </script>
    <script src="JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.js"></script>
<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/themes/fusioncharts.theme.fint.js?cacheBust=56"></script>--%>
    <script src="js1/fusioncharts.js" type="text/javascript"></script>
    <script src="js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {


            $("#btnGo").click(function () {

                var ddlBrand = $("#ddlBrand").val();
                var ddlmode = $("#ddlmode").val();
                var HideSin = document.getElementById("pnlSingle");
                var Hide = document.getElementById("pnlAll");
                if (ddlmode == "1") {
                    Br_Priority();
                }
                else if (ddlmode == "2") {
                    Potential();
                }
                else if (ddlmode == "3") {
                    Br_Campaign();
                }
                else if (ddlmode == "4") {
                    Br_Sample();
                }
                else if (ddlmode == "5") {
                    Br_Coverage();
                }

            });

        });
    </script>
    <script type="text/javascript">
        function Br_Priority() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
            var Month = $("#ddlFMonth").val();
            var Year = $("#ddlFYear").val();

            var Field = $("#ddlFieldForce").val();

            var mode = $("#ddlmode").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();

            var Brand = $("#ddlBrand").val();
            var Res = document.getElementById("chkR");
            var NewJ = document.getElementById("chkN");
            var Vac = document.getElementById("chkV");
            if (Res.checked && !NewJ.checked && !Vac.checked) {

                var Chk = 1;
            }
            else if (NewJ.checked && !Res.checked && !Vac.checked) {
                var Chk = 2;
            }
            else if (Res.checked && NewJ.checked && !Vac.checked) {
                var Chk = 3;
            }
            else if (!Res.checked && !NewJ.checked && !Vac.checked) {
                var Chk = 4;
            }
            else if (!Res.checked && !NewJ.checked && Vac.checked) {
                var Chk = 5;
            }
            else if (Res.checked && !NewJ.checked && Vac.checked) {
                var Chk = 6;
            }
            else if (!Res.checked && NewJ.checked && Vac.checked) {
                var Chk = 7;
            }
            else if (Res.checked && NewJ.checked && Vac.checked) {
                var Chk = 8;
            }
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + SsfName + "^" + Chk + "^" + Brand;
            $.ajax({

                type: 'POST',

                url: "Marketing_SFE.aspx/Priority",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({

                        "type": "ScrollCombi2D",
                        "renderAt": "chart-container",
                        "width": "600",
                        "height": "400",
                        "dataFormat": "json",
                        "dataSource": chartData
                    }

            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {d

                    $("#chart-Container").html(xhr.responseText);

                }

            });

        }

    </script>
    <script type="text/javascript">
        function Potential() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
            var Month = $("#ddlFMonth").val();
            var Year = $("#ddlFYear").val();

            var Field = $("#ddlFieldForce").val();

            var mode = $("#ddlmode").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();

            var Brand = $("#ddlBrand").val();
            var Res = document.getElementById("chkR");
            var NewJ = document.getElementById("chkN");
            var Vac = document.getElementById("chkV");
            if (Res.checked && !NewJ.checked && !Vac.checked) {

                var Chk = 1;
            }
            else if (NewJ.checked && !Res.checked && !Vac.checked) {
                var Chk = 2;
            }
            else if (Res.checked && NewJ.checked && !Vac.checked) {
                var Chk = 3;
            }
            else if (!Res.checked && !NewJ.checked && !Vac.checked) {
                var Chk = 4;
            }
            else if (!Res.checked && !NewJ.checked && Vac.checked) {
                var Chk = 5;
            }
            else if (Res.checked && !NewJ.checked && Vac.checked) {
                var Chk = 6;
            }
            else if (!Res.checked && NewJ.checked && Vac.checked) {
                var Chk = 7;
            }
            else if (Res.checked && NewJ.checked && Vac.checked) {
                var Chk = 8;
            }
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + SsfName + "^" + Chk + "^" + Brand;
            $.ajax({

                type: 'POST',

                url: "Marketing_SFE.aspx/Potential",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');
                    var fusioncharts = new FusionCharts({

                        "type": "doughnut3d",
                        "renderAt": "chart-container",
                        "dataFormat": "json",
                         "width": "600px",
                                "height": "400px",
                        "dataSource": {
                            "chart": {
                                "caption": "Potential & Yield (RCPA)",
                                "subcaption": "",
                                "xaxisname": "",
                                "yaxisname": "Amount (in Rs)",
                              
                                "showLabels": "1",
                                "showPercentValues": "0",
                                "showLegend": "1",
                                "palette": "5",
                                //Configure scrollbar
                                "formatNumber": "0",
                                "formatNumberScale": "0",
                                "useRoundEdges": "1",
                                //  "theme": "fint",
                                "paletteColors": "#FFC300,#FF5733,#fe33ff'",
                                "animation": "1",
                                "plotTooltext": "$label<br> $value"
                            
                               
                              
                            },


                            "categories": [{
                                "category": chartData
                            }],
                            "dataset": [{
                                "data": chartData
                            }]


                        }

                    }

            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");
                },

                error: function (xhr, ErrorText, thrownError) {

                    //     $("#chart-container3").html(xhr.responseText);
                 //   alert("Error: No Data Found!");

                }

            });

        }

    </script>
     <script type="text/javascript">
         function Br_Campaign() {
             var modal = $('<div />');
             modal.addClass("modal");
             $('body').append(modal);
             var loading = $(".loading");
             loading.show();
             var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
             var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
             loading.css({ top: top, left: left });
             var Month = $("#ddlFMonth").val();
             var Year = $("#ddlFYear").val();

             var Field = $("#ddlFieldForce").val();

             var mode = $("#ddlmode").val();


             var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
             var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
             var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();

             var Brand = $("#ddlBrand").val();
             var Res = document.getElementById("chkR");
             var NewJ = document.getElementById("chkN");
             var Vac = document.getElementById("chkV");
             if (Res.checked && !NewJ.checked && !Vac.checked) {

                 var Chk = 1;
             }
             else if (NewJ.checked && !Res.checked && !Vac.checked) {
                 var Chk = 2;
             }
             else if (Res.checked && NewJ.checked && !Vac.checked) {
                 var Chk = 3;
             }
             else if (!Res.checked && !NewJ.checked && !Vac.checked) {
                 var Chk = 4;
             }
             else if (!Res.checked && !NewJ.checked && Vac.checked) {
                 var Chk = 5;
             }
             else if (Res.checked && !NewJ.checked && Vac.checked) {
                 var Chk = 6;
             }
             else if (!Res.checked && NewJ.checked && Vac.checked) {
                 var Chk = 7;
             }
             else if (Res.checked && NewJ.checked && Vac.checked) {
                 var Chk = 8;
             }
             var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + SsfName + "^" + Chk + "^" + Brand;
             $.ajax({

                 type: 'POST',

                 url: "Marketing_SFE.aspx/Campaign",

                 contentType: "application/json; charset=utf-8",

                 dataType: "json",
                 data: '{objData:' + JSON.stringify(Data) + '}',

                 success: function (data) {

                     var chartData = eval("(" + data.d + ')');

                     var fusioncharts = new FusionCharts({

                         "type": "ScrollCombi2D",
                         "renderAt": "chart-container",
                         "width": "600",
                         "height": "400",
                         "dataFormat": "json",
                         "dataSource": chartData
                     }

            );

                     fusioncharts.render();
                     loading.hide();
                     modal.removeClass("modal");

                 },

                 error: function (xhr, ErrorText, thrownError) {                 
                     $("#chart-Container").html(xhr.responseText);

                 }

             });

         }

    </script>
    <script type="text/javascript">

        function Br_Sample() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
            var Month = $("#ddlFMonth").val();
            var Year = $("#ddlFYear").val();

            var Field = $("#ddlFieldForce").val();

            var mode = $("#ddlmode").val();


            var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
            var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();

            var Brand = $("#ddlBrand").val();
            var Res = document.getElementById("chkR");
            var NewJ = document.getElementById("chkN");
            var Vac = document.getElementById("chkV");
            if (Res.checked && !NewJ.checked && !Vac.checked) {

                var Chk = 1;
            }
            else if (NewJ.checked && !Res.checked && !Vac.checked) {
                var Chk = 2;
            }
            else if (Res.checked && NewJ.checked && !Vac.checked) {
                var Chk = 3;
            }
            else if (!Res.checked && !NewJ.checked && !Vac.checked) {
                var Chk = 4;
            }
            else if (!Res.checked && !NewJ.checked && Vac.checked) {
                var Chk = 5;
            }
            else if (Res.checked && !NewJ.checked && Vac.checked) {
                var Chk = 6;
            }
            else if (!Res.checked && NewJ.checked && Vac.checked) {
                var Chk = 7;
            }
            else if (Res.checked && NewJ.checked && Vac.checked) {
                var Chk = 8;
            }
            var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + SsfName + "^" + Chk + "^" + Brand;
            $.ajax({

                type: 'POST',

                url: "Marketing_SFE.aspx/Sample",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '{objData:' + JSON.stringify(Data) + '}',

                success: function (data) {

                    var chartData = eval("(" + data.d + ')');

                    var fusioncharts = new FusionCharts({

                        "type": "ScrollCombi2D",
                        "renderAt": "chart-container",
                        "width": "850",
                        "height": "600",
                        "dataFormat": "json",
                        "dataSource": chartData
                    }

            );

                    fusioncharts.render();
                    loading.hide();
                    modal.removeClass("modal");

                },

                error: function (xhr, ErrorText, thrownError) {

                    $("#chart-Container").html(xhr.responseText);

                }

            });
        }


</script>
  <script type="text/javascript">

      function Br_Coverage() {
          var modal = $('<div />');
          modal.addClass("modal");
          $('body').append(modal);
          var loading = $(".loading");
          loading.show();
          var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
          var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
          loading.css({ top: top, left: left });
          var Month = $("#ddlFMonth").val();
          var Year = $("#ddlFYear").val();

          var Field = $("#ddlFieldForce").val();

          var mode = $("#ddlmode").val();


          var SsfName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
          var modeName = $('#<%=ddlmode.ClientID%> :selected').text();
          var FMName = $('#<%=ddlFMonth.ClientID%> :selected').text();

          var Brand = $("#ddlBrand").val();
          var Res = document.getElementById("chkR");
          var NewJ = document.getElementById("chkN");
          var Vac = document.getElementById("chkV");
          if (Res.checked && !NewJ.checked && !Vac.checked) {

              var Chk = 1;
          }
          else if (NewJ.checked && !Res.checked && !Vac.checked) {
              var Chk = 2;
          }
          else if (Res.checked && NewJ.checked && !Vac.checked) {
              var Chk = 3;
          }
          else if (!Res.checked && !NewJ.checked && !Vac.checked) {
              var Chk = 4;
          }
          else if (!Res.checked && !NewJ.checked && Vac.checked) {
              var Chk = 5;
          }
          else if (Res.checked && !NewJ.checked && Vac.checked) {
              var Chk = 6;
          }
          else if (!Res.checked && NewJ.checked && Vac.checked) {
              var Chk = 7;
          }
          else if (Res.checked && NewJ.checked && Vac.checked) {
              var Chk = 8;
          }
          var Data = Month + "^" + Year + "^" + Field + "^" + mode + "^" + SsfName + "^" + Chk + "^" + Brand;
          $.ajax({

              type: 'POST',

              url: "Marketing_SFE.aspx/Coverage",

              contentType: "application/json; charset=utf-8",

              dataType: "json",
              data: '{objData:' + JSON.stringify(Data) + '}',

              success: function (data) {

                  var chartData = eval("(" + data.d + ')');

                  var fusioncharts = new FusionCharts({

                      "type": "scrollcombidy2d",
                      "renderAt": "chart-container",
                      "width": "850",
                      "height": "600",
                      "dataFormat": "json",
                      "dataSource": chartData
                  }

            );

                  fusioncharts.render();
                  loading.hide();
                  modal.removeClass("modal");

              },

              error: function (xhr, ErrorText, thrownError) {

                  $("#chart-Container").html(xhr.responseText);

              }

          });
      }


</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnl" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
    </asp:Panel>
    <div style="display: block; float: right;">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/menu2.png" OnClientClick="HideMenu_Det(); return false;" />
    </div>
    <div>
        <asp:Panel ID="pnlchart" runat="server" Style="vertical-align: top">
            <div class="roundbox boxshadow" style="height: 800px; border: solid 2px steelblue;">
                <div class="gridheaderleft">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblSF" runat="server" Text="SFName" SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="180px" SkinID="ddlRequired">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblFMonth" runat="server" Text="From Mnth/Yr " Width="60px" SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" Width="60px">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" Width="70px">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblBrand" runat="server" Text="Brand" SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBrand" runat="server" EnableViewState="true" SkinID="ddlRequired">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblmode" runat="server" Text="Mode" SkinID="lblMand"></asp:Label>
                            </td>
                            <td>
                            <asp:DropDownList ID="ddlmode" runat="server" SkinID="ddlRequired">
                                     <asp:ListItem  Value="1">Brand - Priority Visit</asp:ListItem>
                                     <asp:ListItem  Value="2">Potential & Yield</asp:ListItem>
                                     <asp:ListItem  Value="3">Brandwise - Campaign</asp:ListItem>
                                     <asp:ListItem  Value="4">Sample/Input Issued</asp:ListItem>
                                     <asp:ListItem  Value="5">Coverage - Brandwise</asp:ListItem>
                           </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblR" Width="80px" Style="font-size: 10px">
                                <input type="checkbox" id="chkR" />W/O - R</asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblN" Width="80px" Style="font-size: 10px"> <input type="checkbox" id="chkN" />W/O - NJ</asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblV" Width="80px" Style="font-size: 10px"> <input type="checkbox" id="chkV" />W/O - Vac</asp:Label>
                            </td>
                            <td>
                                <input type="button" id="btnGo" class="button5" value="Go" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="boxcontenttext" style="background: White;">
                    <div id="pnlPreviewSurveyData">
                        <center>
                            <table border="0" cellpadding="3" class="" cellspacing="3" style="border-style: none">
                                <tr>
                                    <td id="HideChart" runat="server" style="border: none;">
                                        <table>
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Panel ID="pnlSingle" runat="server">
                                                        <center>
                                                            <div id="chart-container">
                                                            </div>
                                                        </center>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </div>
                    </div>
                    </div>
        </asp:Panel>
           <div class="loading" align="center">
                <br />
                <%--<img src="../Images/loading/loadingScreen.gif" width="350px"  height="250px" alt="" />--%>
                <%--<img src="../Images/loading/tenor.gif" width="350px" height="250px" alt="" />--%>
                  <img src="../Images/loading/Graph_Loading.gif" width="350px" height="250px" alt="" />
            </div>
    </div>
    </form>
</body>
</html>
