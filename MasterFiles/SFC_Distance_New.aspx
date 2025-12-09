<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SFC_Distance_New.aspx.cs" EnableEventValidation="false"
    Inherits="MasterFiles_SFC_Distance_New" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SFC/Fare Updation</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

        <script type="text/javascript" >

            $(window).on("load", function () {
             
        });

            $(document).ready(function () {                
             
            $("#testImg").hide();
            $(".custom-select2").select2();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });

   
            function Validate_Auth() {
                var Auth = "til@san";
                if ($("#password").val() == Auth) {
                    $("#block").hide();
                    $("#access").show();
                } else {
                    $("#block").show();
                    $("#access").hide();
                    if (Auth == "") { alert("Enter Password"); } else { alert("Enter Correct Password") }
                    
                }

            }
         

   
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
 
        function saveOtherExp() {
            var otherExp = document.getElementsByClassName("desig");
            var otherExp1 = document.getElementsByClassName("dropMode");
            var otherExp2 = document.getElementsByName("txtdistance");
            var desig = "";
            var dropMode = "";
            var txtdistance = "";
            var val = 0;
            for (var i = 0; i < otherExp.length; i++) {
                var value = otherExp[i].options[otherExp[i].selectedIndex].value;
                var text = otherExp[i].options[otherExp[i].selectedIndex].text;
                var value1 = otherExp1[i].options[otherExp1[i].selectedIndex].value;
                var text1 = otherExp1[i].options[otherExp1[i].selectedIndex].text;



                //alert(otherExpRmk[i].value + "::" + otherExpVal[i].value + "::" + otherExp[i].value);
                if (i == 0) {

                    desig = value + "=" + text;
                    dropMode = value1 + "=" + text1;
                    val = otherExp2[i].value;
                }
                else {

                    desig = desig + "," + value + "=" + text;
                    dropMode = dropMode + "," + value1 + "=" + text1;
                    val = val + "," + otherExp2[i].value;

                }

            }
            //alert(desig + "~" + dropMode);
            document.getElementById("otherExpValues").value = desig + "~" + dropMode + "~" + val;

        }
   

  
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

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
  
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
    <style type="text/css">
        td, th {
            padding: 10px;
            text-align: center;
        }

        .current {
            display: block;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            width: 250px;
        }
          #tableId select
        {
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            color: #90a1ac;
            font-size: 14px;
            width: 100%;
            padding-left: 20px;
            height: 43px;
        }
        /*table tr td {
        border:2px solid black;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>
        <div id="block" style="display:none">

                <h3 class="text-center">Authorized Personals Only</h3>
            <br />  <br /> 
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="designation-area clearfix">-
                            <div class="single-des clearfix">
                                
                                <div class="row clearfix">
                                    <div class="col-lg-5"></div>
                                    <div class="col-lg-2">
                                        <asp:textbox ID="password" TextMode="Password" placeholder="Enter Password" runat="server" Visible="True" Width="100%" ></asp:textbox>
                                           
                                    
                                    </div>

                                    <div class="col-lg-4" style="margin-left: 20px;">
                                        <asp:Button ID="Button1" runat="server" Width="65px" Text="Go" CssClass="savebutton" Visible="true" OnClientClick="Validate_Auth(); return false;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>


        </div>
        <div id="access" class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">SFC/Fare Updation</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-3"></div>
                                    <div class="col-lg-2" style="margin-right: -52px;">
                                        <asp:Label ID="lblFF" runat="server" CssClass="label" Text="FieldForce Name"></asp:Label>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-3"></div>
                                    <div class="col-lg-4">
                                        <asp:DropDownList ID="ddlFieldForce" runat="server" Visible="false" CssClass="custom-select2 nice-select" Width="100%">
                                            <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>
                                    </div>

                                    <div class="col-lg-4" style="margin-left: 20px;">
                                        <asp:Button ID="btnGo" runat="server" Width="65px" Text="Go" CssClass="savebutton"
                                            Visible="false" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnclear" runat="server" CssClass="resetbutton" Width="65px" Visible="false"
                                            Text="Clear" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <center>
                            <asp:Panel ID="pnl" runat="server" Visible="false">

                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <table>
                                        <tr>
                                            <td class="b" align="left" style="padding: 0px;" id="td1">
                                                <table id="tableId" runat="server" clientidmode="Static">
                                                    <tr style="font-size: 15px; color: #696D6E;">
                                                        <td class="tblHead" align="center">
                                                            <b>From Territory</b>
                                                        </td>
                                                        <td class="tblHead" align="center">
                                                            <b>To Territory</b>
                                                        </td>
                                                        <td class="tblHead" align="center">
                                                            <b>Distance</b>
                                                        </td>
                                                        <td class="tblHead" colspan="2" align="center">
                                                            <b>Add/Del</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList class="desig" ID="desig" runat="server" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList class="dropMode" ID="dropMode" runat="server" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="single-des">
                                                            <asp:TextBox ID="txtdistance" class="txtdistance" name="txtdistance" runat="server" CssClass="input" Width="60"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input type="button" id="btnadd" value=" + " class='btnSave' onclick="_AdRowByCurrElem(this)" />
                                                        </td>
                                                        <td>
                                                            <input type="button" value=" - " class='btnSave' onclick="DRForOthExp(this, this.parentNode.parentNode, 1)" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:HiddenField ID="otherExpValues" runat="server" Value="" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <br />

                                <asp:Button ID="btnSave" runat="server" CssClass="savebutton"
                                    Text="Save" OnClientClick="saveOtherExp()" OnClick="btnSave_Click" />
                            </asp:Panel>
                        </center>
                    </div>
                </div>
            </div>
        </div>

        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc"></td>
                    <td align="left" class="stylespc">
                        <%--<asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                            Visible="false" ToolTip="Enter Text Here"></asp:TextBox>
                        <asp:LinkButton ID="linkcheck" runat="server" OnClick="linkcheck_Click">
                          <img src="../Images/Selective_Mgr.png" />
                        </asp:LinkButton>--%>
                        
                        &nbsp
                        
                    </td>
                    <td>
                        <div id="testImg">
                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;"
                                runat="server" /><span style="font-family: Verdana; color: Red; font-weight: bold;">Loading
                                    Please Wait...</span>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <br />


        </center>
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
        <br />
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
