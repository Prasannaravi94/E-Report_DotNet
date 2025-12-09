<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Blocking.aspx.cs" Inherits="MasterFiles_AnalysisReports_Analysis_Pob_count_Periodically" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Blocking</title>
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
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, sf_name, prdvalue, prdname, txtEffFrom, txtEffTo) {

            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptAnalysis_Pob_count_Periodically.aspx?sf_code=" + sfcode + " &sf_name=" + sf_name + "&prdvalue=" + prdvalue + "&prdname=" + prdname + "&txtEffFrom=" + txtEffFrom + "&txtEffTo=" + txtEffTo,
    "ModalPopUp"//,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=800," +
    //"height=600," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            // LoadModalDiv();
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript" language="javascript">
      
    </script>

    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.on('keyup', function () {
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
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });
    </script>


    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

</head>
<body>

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
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

            $('#btnSubmit').click(function () {

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }



                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;




                var txtEffFrom = document.getElementById('<%=txtEffFrom.ClientID%>').value;
                var txtEffTo = document.getElementById('<%=txtEffTo.ClientID%>').value;




                if ($('#chksample input:checked').length > 0) {
                    if ($('#chksample input:checked').length > 10) {
                        alert("Not Select more than 10 Product");
                        return false;
                    }
                }

                else {
                    alert("Select atleast one Product");
                    return false;
                }

                var prdname = "";
                var prdvalue = "";

                var CHK = document.getElementById("<%=chksample.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");

                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        prdname += label[i].innerHTML + ",";
                    }
                }

                var checked_checkboxes = $("[id*=chksample] input:checked");
                checked_checkboxes.each(function () {

                    prdvalue += $(this).parent().attr('cbValue') + ",";
                });
                showModalPopUp(sf_Code, Name, prdvalue, prdname, txtEffFrom, txtEffTo);

            });
        });
    </script>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">POB Wise - Periodically</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <%--         <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    Visible="false" ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:LinkButton ID="linkcheck" runat="server" OnClick="linkcheck_Click">
                          <img src="../../Images/Selective_Mgr.png"/>
                                </asp:LinkButton>--%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>

                                <%--                                <div id="testImg">
                                    <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;"
                                        runat="server" /><span style="font-family: Verdana; color: Red; font-weight: bold;">Loading
                                    Please Wait...</span>
                                </div>--%>
                            </div>

                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblfrom" runat="server" CssClass="label" Text="From Date"></asp:Label>
                                        <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" Width="100%" onkeypress="Calendar_enter(event);"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom" CssClass="cal_Theme1"
                                            runat="server" />
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblto" runat="server" CssClass="label" Text="To Date"></asp:Label>
                                        <asp:TextBox ID="txtEffTo" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo" CssClass="cal_Theme1"
                                            runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblmode" runat="server" Text="Product" CssClass="label"></asp:Label>
                                <asp:CheckBoxList ID="chksample" CssClass="chkboxLocation" CellPadding="10" RepeatColumns="2" Font-Bold="true" RepeatDirection="vertical" runat="server">
                                </asp:CheckBoxList>
                                <%-- <asp:CheckBoxList ID="CheckBoxList1" CssClass="chkboxLocation" CellPadding="10" RepeatColumns="2" Font-Bold="true" Font-Names="Verdana" Font-Size="11px" Visible="false"  RepeatDirection="vertical"   runat="server">
                        </asp:CheckBoxList>--%>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

