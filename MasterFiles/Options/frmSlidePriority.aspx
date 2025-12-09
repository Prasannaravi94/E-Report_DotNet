<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSlidePriority.aspx.cs" Inherits="MasterFiles_Options_frmSlidePriority" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
    
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="SlideFile/SlideUpd.js"></script>
    <script type="text/javascript">
        function BindPriorty() {
            var Mode = $('#ddlMode :selected').text();
            var ddlFiler = $('#ddlFiler :selected').val();

            var obj = { Mode: Mode, ddlFiler: ddlFiler };

            $.ajax({
                type: "POST",
                url: "frmSlidePriority.aspx/GetProductSlidePriorty",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                dataType: "json",
                success: function (data) {
                    //alert(data.d.length);
                    $("#DCVisit").html("");
                    if (data.d.length > 0) {
                        var arrary = [];
                        arrary = data.d;

                        var FilterValue = data.d[data.d.length - 1].FilterArray;
                        //alert(FilterValue);
                        FilterValue = FilterValue.slice(0, -1);
                        var data1 = [];
                        data1 = FilterValue.split(',');
                        data1 = unique(data1);
                        var BrandCunt = 0;
                        for (var i = 0; i < data1.length; i++) {
                            $("#btnUpdate").css("display", "block");
                            BrandCunt += 1;
                            var totValue = data1.length;
                            var BrandName = data1[i];

                            var tbRow1 = '';

                            tbRow1 += "<table id='tblvisit_" + i + "' cellpadding='5' cellspacing='5' class='tblCall' style='min-width:45%;max-width:35%;border: solid 1px Black;'>";
                            tbRow1 += "<tr>";
                            tbRow1 += "<td style='background-color:#00cc44'><span style='font-family:Verdana;font-weight:bold;width:100%;margin-left:100px'>" + BrandName + "</span> </td>";
                            tbRow1 += "<td style='background-color:#00cc44'></td>";
                            tbRow1 += "<td style='background-color:#00cc44'></td>";
                            tbRow1 += "<td style='background-color:#00cc44'><select id='ddlBrand_" + i + "'>";
                            for (var k = 1; k <= totValue; k++) {
                                if (BrandCunt == k) {
                                    tbRow1 += "<option selected='selected'>" + k + "</option>";
                                }
                                else {
                                    tbRow1 += "<option>" + k + "</option>";
                                }
                            }

                            tbRow1 += '</tr>';
                            var Count = 0;
                            for (var j = 0; j < data.d.length; j++) {

                                var Product_Brand = data.d[j].Product_Brand;


                                if (BrandName == Product_Brand) {
                                    var Img_Count = data.d[j].Img_Count;
                                    Count += 1;
                                    tbRow1 += "<tr class='tdSlideTable'>";
                                    tbRow1 += "<td></td>";
                                    tbRow1 += "<td><img width='100px' height='100px' src='" + data.d[j].Img_Name + "' id='imgremove" + j + "'/> </td>";
                                    tbRow1 += "<td><span style='font-family:Verdana;font-weight:bold'>" + data.d[j].Image + "</span> </td>";
                                    tbRow1 += "<td class='myClass' align='center'><select id='ddl_" + j + "'>";
                                    for (var k = 1; k <= Img_Count; k++) {
                                        if (Count == k) {
                                            tbRow1 += "<option selected='selected'>" + k + "</option>";
                                        }
                                        else {
                                            tbRow1 += "<option>" + k + "</option>";
                                        }
                                    }

                                    tbRow1 += " </select></td>";
                                    tbRow1 += "</tr>";

                                }
                            }

                            tbRow1 += "</table>";
                            $("#DCVisit").append(tbRow1);
                        }
                    }
                }
            });

        }

        function unique(list) {

            var result = [];
            for (i = 0; i < list.length; i++) {
                if (result.indexOf(list[i]) == -1) {
                    result.push(list[i])
                }
            }

            return result;
        }
    </script>
   
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="script" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div>
       <div id="Divid" runat="server">
        </div>
        <center>
        <table cellpadding="0" cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="lblMode" runat="server" Text="Mode" SkinID="lblMand"></asp:Label>
                </td>
                <td>
                   
                    <asp:DropDownList ID="ddlMode" SkinID="ddldcr" onchange="GetspecHera()" runat="server">
                         <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Brand Wise"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Speciality Wise"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Herapathite"></asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:Label ID="lblFiler" Text="Filter By" Style="display: none;" runat="server" SkinID="lblMand"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFiler" runat="server" Style="display: none" SkinID="ddldcr">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
            <input type="button" id="btnSubmit" onclick="BindPriorty()" value="View" runat="server" />
            </center>
            <br />
            <center>
              <div id="DCVisit">
              </div>
                <br />
                <br />
                <input type="button" id="btnUpdate" value=" Update " style="display:none" onclick="SetPriority()" />
        </center>
    </div>
    </form>
</body>
</html>
