<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Quiz_Graph.aspx.cs" Inherits="MIS_Reports_rpt_Quiz_Graph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../DashBoard/JS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/fusioncharts.js"></script>
<script type="text/javascript" src="http://static.fusioncharts.com/code/latest/themes/fusioncharts.theme.fint.js?cacheBust=56"></script>--%>
    <script src="../DashBoard/js1/fusioncharts.js" type="text/javascript"></script>
    <script src="../DashBoard/js1/themes/fusioncharts.theme.fint.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
      
            Quiz();
        });
    </script>
    <script type="text/javascript">
        function Quiz() {

            $.ajax({

                type: 'POST',

                url: "rpt_Quiz_Graph.aspx/Quiz",

                contentType: "application/json; charset=utf-8",

                dataType: "json",
                data: '',

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
                   

                },

                error: function (xhr, ErrorText, thrownError) {
                    $("#cchart-container").html(xhr.responseText);

                }

            });

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <br />
        <center>
            <div id="chart-container">
            </div>
        </center>
    </div>
    </form>
</body>
</html>
