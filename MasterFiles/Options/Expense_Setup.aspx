<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expense_Setup.aspx.cs" Inherits="MasterFiles_Options_Expense_Setup" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Setup</title>
    <%--  <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <link href="../../JScript/Bootstrap/dist/css/bootstrap.css" />
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link href="../../assets/css/Calender_CheckBox.css" rel="stylesheet" />

    <style type="text/css">
        .spc {
            padding-left: 5%;
        }

        .spc1 {
            padding-left: 10%;
        }

        .box {
            /*background: #FFFFFF;*/
            border: 2px solid #d1e2ea;
            border-radius: 8px;
        }

        .box1 {
            background: #FFFFFF;
            border: 2px solid #5f9ea0;
            border-radius: 8px;
        }

        .tableHead {
            color: black;
            border-style: solid;
            border-width: 1px;
            border-color: #a2cd5a;
        }

        .break {
            height: 7px;
        }

        #tableId select {
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            color: #90a1ac;
            font-size: 14px;
            /*width: 100%;*/
            padding-left: 20px;
            height: 33px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($('#rdoMgrRemarks :radio:checked').length > 0) {
                    return true;
                }
                else {
                    createCustomAlert('Select Manager Approval (Only Remarks Available)')
                    return false;
                }
            })
        })

    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($('#rdoMgrRow :radio:checked').length > 0) {
                    return true;
                }
                else {
                    createCustomAlert('Select Manager Approval (Row Wise Changes)')
                    return false;
                }
            })
        })

    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($('#rdoadmin :radio:checked').length > 0) {
                    return true;
                }
                else {
                    createCustomAlert('Select Manager Approval (Same as Admin)')
                    return false;
                }
            })
        })

    </script>


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($('#rdoEx_Subm :radio:checked').length > 0) {
                    return true;
                }
                else {
                    createCustomAlert('Select Expense Submission')
                    return false;
                }
            })
        })

    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($('#rdoLast_OS :radio:checked').length > 0) {
                    return true;
                }
                else {
                    createCustomAlert('Select Last Day OS Work')
                    return false;
                }
            })
        })

    </script>
    <%--<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#btnSubmit').click(function () {
            if ($("#ddlStart_date").val() > 0) {
                return true;
            }
            else {
                createCustomAlert('Select From Submission Range')
                return false;
            }
        })
    });

</script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#btnSubmit').click(function () {
            if ($("#ddlEnd_date").val() > 0) {
                return true;
            }
            else {
                createCustomAlert('Select To Submission Range')
                return false;
            }
        })
    });

</script>
    --%>



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                var rdoEx_Subm = $("input[name='<%=rdoEx_Subm.UniqueID%>']:radio:checked").val();
                if (rdoEx_Subm == 'P') {
                    if ($("#ddlperiodRange").val() > 0) {
                        return true;
                    }
                    else {
                        createCustomAlert('Select Start date for Periodically')
                        return false;
                    }
                }
            })
        });

        function _AdRowByCurrElem($x) {

            _tR = $x.parentNode.parentNode;
            _nTR = _tR.cloneNode(true);
            _tR.parentNode.appendChild(_nTR);
            //newRow.appendChild(_nTR);
            //_tR.parentNode.appendChild(newRow);
            clrNRw(_nTR)
        }


        function clrNRw($r) {
            for (var $rl = 0; $rl < $r.childNodes.length; $rl++) {
                $c = $r.childNodes[$rl];
                for (var $i = 0; $i < $c.childNodes.length; $i++) {
                    $o = $c.childNodes[$i];

                    if ($o.id != '' && $o.id != null) {
                        $s = $o.id.split('_');
                        $o.id = $s[0] + '_' + $r.rowIndex
                    }
                    if ($o.type == "checkbox") {
                        $o.checked = false;
                    }
                    else if ($o.tagName == 'SELECT') {
                        $o.selectedIndex = 0;
                    }
                    else if ($o.tagName == 'SPAN') {
                        $o.innerText = "";
                    }
                    else if ($o.value != null && $o.type != "button" && $o.type != "hidden") {
                        $o.value = "";

                    }
                    if ($o.pv != null) $o.pv = '';
                    if ($o.Pval != null) $o.Pval = '';
                }
            }
        }
        function DRForOthExp($x, $r, rCnt) {
            // var $temp = $r.cells[1].childNodes[0].value.replace(/,/g, '');
            //if (isNaN($temp) || $temp == '') $temp = 0;

            var tb = $r.parentNode;
            var Ttb = tb.parentNode

            if (Ttb.rows.length - 1 > rCnt) {
                tb.removeChild($r);
            }
            else
                clrNRw($r);



        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-bottom: none">Expense Setup</h2>

                        <div class="row box ">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-5" style="padding-top: 20px">
                                <div class="designation-area clearfix ">
                                    <div class="single-des clearfix">
                                        <div id="lblMgrRemarks" runat="server" align="left" class="label" style="font-weight: bold; color: #800080; font-size: 14px;">
                                            Manager Approval (Only Remarks Available)
                                               
                                        </div>
                                        <asp:RadioButtonList ID="rdoMgrRemarks" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <div id="lblMgrRow" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            Manager Approval (Row Wise Changes)
                                        </div>
                                        <asp:RadioButtonList ID="rdoMgrRow" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <div id="lblMgradmin" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            Manager Approval (Same as Admin)
                                        </div>
                                        <asp:RadioButtonList ID="rdoadmin" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <div id="lblsubm" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            Expense Submission Based on
                                        </div>
                                        <asp:RadioButtonList ID="rdoEx_Subm" runat="server" RepeatDirection="Vertical"
                                            AutoPostBack="true" OnSelectedIndexChanged="rdoEx_Subm_SelectedIndexChanged">
                                            <asp:ListItem Value="M">Monthly</asp:ListItem>
                                            <asp:ListItem Value="F">Fortnight </asp:ListItem>
                                            <asp:ListItem Value="P">Periodically</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:Label ID="lblstarfrom" runat="server" Text="Start From" Font-Bold="true" Visible="false"
                                            Font-Names="Arial" Font-Size="12px"> </asp:Label>
                                        <asp:DropDownList ID="ddlperiodRange" runat="server" Visible="false">
                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="21">21</asp:ListItem>
                                            <asp:ListItem Value="22">22</asp:ListItem>
                                            <asp:ListItem Value="23">23</asp:ListItem>
                                            <asp:ListItem Value="24">24</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="26">26</asp:ListItem>
                                            <asp:ListItem Value="27">27</asp:ListItem>
                                            <asp:ListItem Value="28">28</asp:ListItem>
                                            <asp:ListItem Value="29">29</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="31">31</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <div id="lblLastOS" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            Last Day 'OS' Work Consider as
                                        </div>
                                        <asp:RadioButtonList ID="rdoLast_OS" runat="server" RepeatDirection="Vertical">
                                            <%--  <asp:ListItem Value="HQ">HQ Allowance</asp:ListItem>--%>
                                            <asp:ListItem Value="OS">OS Allowance</asp:ListItem>
                                            <asp:ListItem Value="EX">EX Allowance</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="single-des clearfix">
                                        <div id="lblSingleOS" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            Single Day 'OS' Work Consider as
                                        </div>
                                        <asp:RadioButtonList ID="rdoSingle_OS" runat="server" RepeatDirection="Vertical">
                                            <asp:ListItem Value="OS">OS Allowance</asp:ListItem>
                                            <asp:ListItem Value="EX">EX Allowance</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="single-des clearfix">
                                        <div id="Div1" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            Mgr Expense Setup
                                        </div>

                                        <table border="0" id="tableId" runat="server" clientidmode="Static">
                                            <tr>
                                                <td class="tblHead" align="center" style="font-size: 12px"><b>Designation</b></td>
                                                <td class="tblHead" align="center" style="font-size: 12px"><b>Mode</b></td>
                                                <td class="tblHead" colspan="2" align="center" style="font-size: 12px"><b>Add/Del</b></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList class="desig" ID="desig" runat="server" AutoPostBack="false">
                                                        <asp:ListItem Selected="false" Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList class="dropMode" ID="dropMode" runat="server" AutoPostBack="false">
                                                        <asp:ListItem Selected="false" Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Automatic" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Manual" Value="M"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <input type="button" id="btnadd" value=" + " onclick="_AdRowByCurrElem(this)" style="width: 30px;" />
                                                </td>
                                                <td>
                                                    <input type="button" value=" - " onclick="DRForOthExp(this, this.parentNode.parentNode, 1)" style="width: 30px;" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="otherExpValues" runat="server" Value="" />
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-5" style="padding-top: 20px">
                                <div class="designation-area clearfix ">


                                    <div class="single-des clearfix">
                                        <div id="lblExpSubm" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            Expense Submission Range
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblfrom" runat="server" Text="From" CssClass="label"> </asp:Label>
                                                <asp:DropDownList ID="ddlStart_date" runat="server">
                                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblto" runat="server" Text="To" CssClass="label"> </asp:Label>
                                                <asp:DropDownList ID="ddlEnd_date" runat="server">
                                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>


                                    <div class="single-des clearfix">
                                        <div id="lblfieldforce" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            If Fieldforce Covers HQ & EX on the Same Day,Can We take the Allowance & Fare as Below:
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-1">
                                                <asp:Label ID="lblhq" runat="server" Text="I." Font-Bold="true" Font-Size="13px"></asp:Label>
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:RadioButton ID="rdofieldforceHQ" runat="server" Text="HQ (No Fare)"
                                                    GroupName="HQ" />
                                                <%-- <asp:RadioButtonList ID="rdofieldforce" runat="server" RepeatDirection="Vertical">
                                                     <asp:ListItem Value="HQ">HQ (No Fare)</asp:ListItem>
                                                      <asp:ListItem Value="EX">EX & Actual Fare</asp:ListItem>
                                                      </asp:RadioButtonList>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-1">
                                                <asp:Label ID="Label1" runat="server" Text="II." Font-Bold="true" Font-Size="13px"></asp:Label>
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:RadioButton ID="rdofieldforceEX" runat="server" Text="EX & Actual Fare"
                                                    GroupName="HQ" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div id="lblmax_calls" runat="server" align="left" style="font-weight: bold; font-size: 13px;">
                                                &nbsp;&nbsp;&nbsp;
                                                 III. Maximum Calls
                                            </div>
                                        </div>

                                        <div class="row" style="padding-left: 50px;">
                                            <asp:RadioButton ID="rdoMax_callsMHQ" runat="server" Text="HQ Allowance / No Fare"
                                                GroupName="HQ" />
                                            <%--<asp:RadioButtonList ID="rdoMax_calls" runat="server" RepeatDirection="Vertical">
                                             <asp:ListItem Value="MHQ">HQ Allowance / No Fare</asp:ListItem>
                                             <asp:ListItem Value="MHQF">HQ Allowance / With Fare</asp:ListItem>
                                             <asp:ListItem Value="MEXF">EX Allowance / With Fare</asp:ListItem>
                                             </asp:RadioButtonList>--%>
                                        </div>
                                        <div class="row" style="padding-left: 50px;">
                                            <asp:RadioButton ID="rdoMax_callsMHQF" runat="server" Text="HQ Allowance / With Fare"
                                                GroupName="HQ" />
                                        </div>
                                        <div class="row" style="padding-left: 50px;">

                                            <asp:RadioButton ID="rdoMax_callsMEXF" runat="server" Text="EX Allowance / With Fare"
                                                GroupName="HQ" />
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-1">
                                                <asp:Label ID="Label2" runat="server" Text="IV." Font-Bold="true" Font-Size="13px"></asp:Label>
                                            </div>
                                            <div class="col-lg-8">
                                                <%--   <div id="lblEx_minimum" runat="server" align="left" style="font-weight: bold; color: #800080;
                                                       font-size: 13px; text-decoration: underline;">
                                                       Ex Calls will have Minimum</div>--%>
                                                <asp:RadioButton ID="rdomaxcalls" runat="server" Text="Ex Calls will have Minimum" AutoPostBack="true"
                                                    GroupName="HQ" OnCheckedChanged="rdomaxcalls_CheckedChanged" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                 <asp:TextBox ID="txtminimum" runat="server" CssClass="input" Width="80px" Visible="false"></asp:TextBox>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="single-des clearfix">
                                        <div id="Divos" runat="server" align="left" style="font-weight: bold; color: #800080; font-size: 14px;" class="label">
                                            'OS' Work Consider as
                                        </div>
                                        <asp:RadioButtonList ID="RdoPackage" runat="server" RepeatDirection="Vertical">
                                            <asp:ListItem Value="P">Package Calculation(OS Only)</asp:ListItem>
                                            <asp:ListItem Value="R">Row Wise Calculation(OS Only)</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="single-des clearfix">
                                        <div id="rowwise" runat="server" style="font-weight: bold; font-size: 14px;">Row wise Additional Expense <span style="color: #800080;">'Text box'</span></div>
                                        <asp:RadioButtonList ID="rdoRow_wise" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Y">Needed</asp:ListItem>
                                            <asp:ListItem Value="N">Not Needed </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <br />
                        <br />
                        <center>
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton"
                                Text="Save" OnClientClick="saveOtherExp()" OnClick="btnSubmit_Click" />

                            <asp:Button ID="btnClear" runat="server" CssClass="resetbutton"
                                Text="Clear" OnClick="btnClear_Click" />
                        </center>

                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
<script>
    function saveOtherExp() {

        var otherExp = document.getElementsByClassName("desig");
        var otherExp1 = document.getElementsByClassName("dropMode");
        var desig = "";
        var dropMode = "";
        for (var i = 0; i < otherExp.length; i++) {
            var value = otherExp[i].options[otherExp[i].selectedIndex].value;
            var text = otherExp[i].options[otherExp[i].selectedIndex].text;
            var value1 = otherExp1[i].options[otherExp1[i].selectedIndex].value;
            var text1 = otherExp1[i].options[otherExp1[i].selectedIndex].text;

            //alert(otherExpRmk[i].value + "::" + otherExpVal[i].value + "::" + otherExp[i].value);
            if (i == 0) {

                desig = value + "=" + text;
                dropMode = value1 + "=" + text1;
            }
            else {

                desig = desig + "," + value + "=" + text;
                dropMode = dropMode + "," + value1 + "=" + text1;

            }

        }
        //alert(desig + "~" + dropMode);
        document.getElementById("otherExpValues").value = desig + "~" + dropMode;

    }
</script>
<script language="javascript" type="text/javascript">
    $(function () {

        $(".desig").show();
        $(".dropMode").show();

        $('#tableId > tbody  > tr').each(function () {
            $(this).find('td').each(function () {
                $(this).find(".nice-select").remove();
            })
        });
    });
</script>
</html>
