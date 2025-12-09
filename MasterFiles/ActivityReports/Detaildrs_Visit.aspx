<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Detaildrs_Visit.aspx.cs"
    Inherits="MasterFiles_DDProductBrand_Detail" EnableEventValidation="false" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Drs</title>
        <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css">
<link rel="stylesheet" href="../../../assets/css/nice-select.css">
<link rel="stylesheet" href="../../../assets/css/bootstrap.min.css">
<link rel="stylesheet" href="../../../assets/css/style.css">
<link rel="stylesheet" href="../../../assets/css/responsive.css">
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />--%>
    <%--   <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/js/bootstrap-select.min.js"></script>--%>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(hdnSf_Code, hdnSf_Name, fmon, fyr, mode, selValues) {

            //alert(selValues);
            popUpObj = window.open("rptDetaildrs_Visit.aspx?Sf_Code=" + hdnSf_Code + "&Sf_Name=" + hdnSf_Name + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + "&mode=" + mode + "&selValues=" + selValues,
                       "ModalPopUp",
                       "toolbar=no," +
                       "scrollbars=yes," +
                       "location=no," +
                       "statusbar=no," +
                       "menubar=no," +
                       "addressbar=no," +
                       "resizable=yes," +
                       "width=800," +
                       "height=600," +
                       "left = 0," +
                       "top=0"
                       );
            popUpObj.focus();

            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://zippy.gfycat.com/GlitteringUnfitIndianhare.gif"
                $(popUpObj.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style="width:310px;height:300px;position:fixed;top:20%;left:30%;"  alt="" /></div>');
            });
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnGo').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

            $('#btnGo').click(function () {

                var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (sf_name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                <%--var Month1 = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (Month1 == "---Select---") { alert("Select Month."); $('#ddlFMonth').focus(); return false; }
                var Year1 = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (Year1 == "---Select---") { alert("Select Month."); $('#ddlFYear').focus(); return false; }
                var ddlMonth = document.getElementById('<%=ddlFMonth.ClientID%>').value;
                var ddlYear = document.getElementById('<%=ddlFYear.ClientID%>').value;--%>
                var sf_code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var mode = document.getElementById('<%=ddlmode.ClientID%>').value;
                if (mode == "---Select---") { alert("Select From mode."); $('#ddlmode').focus(); return false; }

                var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = ToMonYear[1];

                var group = $('#<%=cblProductList.ClientID%> input:checked');
                var hasChecked = false;
                for (var i = 0; i < group.length; i++) {
                    if (group[i].checked)
                        hasChecked = true;
                    break;
                }
                if (hasChecked == false) {
                    alert("Select any name"); $('cblProductList').focus(); return false;
                }

                selValues = "";
                var ChkboxList1Ctl = document.getElementById("cblProductList");
                var itemarr = document.getElementById("cblProductList").getElementsByTagName("span");
                var ChkboxList1Arr = document.getElementById("cblProductList").getElementsByTagName("input");

                if (ChkboxList1Arr != null) {
                    for (var i = 0; i < ChkboxList1Arr.length; i++) {
                        if (ChkboxList1Arr[i].checked)
                            selValues += "'" + itemarr[i].getAttribute("dvalue") + "'" + ",";
                    }
                    if (selValues.length > 0)
                        selValues = selValues.substr(0, selValues.length - 1);
                }
                showModalPopUp(sf_code, sf_name, Month1, Year1, mode, selValues);
                //if (sf_Code != '' && sf_Code != 0 && sf_Code_Name != '' && selValues != '') {
                //    debugger;
                //    showModalPopUp(sf_Code, sf_Name, Month1, Year1, mode, selValues);
                //}

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#chkprod').click(function () {
                debugger
                var CHK = document.getElementById("<%=cblProductList.ClientID%>");
            var checkboxes = CHK.getElementsByTagName("input");
                //var eles = $(":input[name^='q1_']");
            var label = CHK.getElementsByTagName("label");
            if (chkprod.checked) {
                for (var i = 0; i < checkboxes.length; i++) {
                    checkboxes[i].checked = true;
                }
            } else {
                for (var i = 0; i < checkboxes.length; i++) {
                    checkboxes[i].checked = false;
                }
            }

            var count = 0;
            var CH = document.getElementById("<%=cblProductList.ClientID%>");
            var chkboxes = CH.getElementsByTagName("input");
            var labl = CH.getElementsByTagName("label");
            for (var i = 0; i < chkboxes.length; i++) {
                if (chkboxes[i].checked == true) {
                    count += 1;
                }
            }
            document.getElementById('length').innerHTML = 'Selected Products - ' + count;
            document.getElementById('length').style = 'color:Red';
            })
            //$("#chk_Prodbind ").css("display", "none");
            //$('#chkprod').change(function () {
            //    if ($(this).is(":checked")) {
            //        $("#chk_Prodbind ").css("display", "block");
            //    }
            //    else {
            //        $("#chk_Prodbind ").css("display", "none");
            //    }
            //});
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
            <%--    <asp:UpdatePanel ID="upSaleBifurcation" runat="server">
        <ContentTemplate>--%>

            <%-- <center>
                <table>
                    <tr>
                        <td align="center" style="color: #8A2EE6; font-family: Verdana; font-weight: bold; text-transform: capitalize; font-size: 14px; text-align: center;">
                            <asp:Label ID="lblHead" runat="server" Text="" Font-Underline="True" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
            </center>
            <center>
                <div id="Div1" runat="server">
                </div>--%>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Visit wise</h2>
                        <asp:Label ID="Lblmain" SkinID="lblMand" runat="server" Text="Detailing Drs-Visit wise"></asp:Label>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">


                                <asp:Label ID="lblDivision" runat="server" Visible="false" Text="Division Name " SkinID="lblMand"></asp:Label>

                                <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">

                                <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name " SkinID="lblMand"></asp:Label>

                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select" data-live-search="true">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                <%--                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblFMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>

                                    <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="ddl">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                                </div>
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lblFYear" runat="server" SkinID="lblMand" Text="Year" Width="60"></asp:Label>
                                    <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="ddl"
                                        Width="60">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                            <div class="single-des clearfix">

                                <asp:Label ID="lblmode" runat="server" Text="Mode" SkinID="lblMand"></asp:Label>

                                <asp:DropDownList ID="ddlmode" runat="server" SkinID="ddlRequired" AutoPostBack="true"  CssClass="custom-select2 nice-select" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Brand"></asp:ListItem>
                                   <%-- <asp:ListItem Value="2" Text="Speciality"></asp:ListItem>--%>
                                    <%--<asp:ListItem Value="3" Text="Therapetics"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">

                                <asp:Label ID="selectedmode" runat="server" SkinID="lblMand" Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkprod" Text="Select All Brands" Font-Names="Calibri" Font-Size="14px"
                                            ForeColor="Black" runat="server"  Visible="false"/>
                            </div>
                            <div class="single-des clearfix">
                                <%-- <td align="left" class="stylespc">
                          <asp:CheckBoxList ID="CheckBoxList1" Width="85%" Font-Size="8pt" runat="server"
                                RepeatColumns="4" DataTextField="Ch_Name" DataValueField="Ch_Code" CssClass="chkChoice">
                            </asp:CheckBoxList>
                        </td>--%>

                                <asp:CheckBoxList ID="cblProductList" ClientIDMode="Static" Width="100%" Font-Size="8pt" runat="server"
                                    RepeatColumns="3" DataTextField="Ch_Name" DataValueField="Ch_Code" CssClass="chkChoice">
                                </asp:CheckBoxList>
                            </div>

                            <%--     <div id="Product" runat="server" visible="false">
                            <table width="85%">
                                <tr height="30" bgcolor="lightblue">
                                    <td align="center" colspan="4">
                                        <b class="subheading"><font color="blue">Select Product Name</font></b>
                                    </td>
                                </tr>
                            </table>
                            <table width="85%">
                                <tr height="30">
                                    <asp:CheckBoxList ID="cblProductList" Width="85%" Font-Size="8pt" runat="server"
                                        RepeatColumns="4" DataTextField="Ch_Name" DataValueField="Ch_Code" CssClass="chkChoice">
                                    </asp:CheckBoxList>
                                </tr>
                            </table>
                            <table width="85%">
                                <tr height="30" bgcolor="lightblue">
                                    <td align="center" colspan="4"></td>
                                </tr>
                            </table>
                            <br />
                            <table width="85%">
                                <tr height="30">
                                    <td>
                                        <br />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="BUTTON"
                                            Width="70px" Height="25px" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="BUTTON" OnClick="btnClear_Click"
                                            Width="70px" Height="25px" />
                                    </td>
                                </tr>
                            </table>
                        </div>--%>

                            <div class="w-100 designation-submit-button text-center clearfix">
                                <%-- <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" />--%>
                                <button type="button" style="width: 30px; height: 25px" id="btnGo" class="savebutton">Go</button>

                                <%--<asp:Button ID="btnGo" runat="server" Text="Go" Width="30px"  Height="25px" CssClass="BUTTON" />--%>
                                <%--                            <button type="button" style="width: 30px; height: 25px" id="btnGo" class="BUTTON">Go</button>--%>
                            </div>

                            <%--</ContentTemplate>
        
    </asp:UpdatePanel>--%>
                        </div>
                        
                        <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
                        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
                        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
                        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
                        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
                        <script type="text/javascript">
                            $(function () {
                                $('[id*=txtMonthYear]').datepicker({
                                    changeMonth: true,
                                    changeYear: true,
                                    format: "M-yyyy",
                                    viewMode: "months",
                                    minViewMode: "months",
                                    language: "tr"
                                });
                            });
                        </script>
                        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
