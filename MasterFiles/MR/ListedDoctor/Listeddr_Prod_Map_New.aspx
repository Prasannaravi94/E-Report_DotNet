<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_Prod_Map_New.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_Listeddr_Prod_Map_New" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor - Product Tag</title>

    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <link href="../../../JScript/Bootstrap/dist/css/bootstrap.css" />

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>

    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />


    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[src*=plus]").live("click", function () {
                if ($(this).closest("tr").next().css('display') == "table-row") {
                    $(this).closest("tr").after("<tr class='camps'><td></td><td colspan = '1000'>" + $(this).next().html() + "</td></tr>")
                    $(this).next().remove();
                } else
                    $(this).closest("tr").next().css('display', 'table-row');
                $(this).attr("src", "../../../Images/minus.png");
            });
            $("[src*=minus]").live("click", function () {
                $(this).attr("src", "../../../Images/plus.png");
                $(this).closest("tr").next().css('display', 'none');
            });

        });
    </script>
    <script type="text/javascript">
        function ChkFn(x) {
            aid = x.id.split('_');
            y = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_chkCatName')
            z = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_ddlPriority');
            var pval = z.getAttribute('p');
            // alert('|'+pval+'|');
            z.setAttribute('p', ((z.value != '0') ? z.options[z.selectedIndex].text : ""));
            document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML = document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML.replace(y.parentNode.getElementsByTagName('label')[0].innerHTML + ((pval != '') ? ' ( ' + pval + ' )' : '') + ', ', '')
            if (y.checked == true)
                document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML += y.parentNode.getElementsByTagName('label')[0].innerHTML + ((z.value != '0') ? ' ( ' + z.options[z.selectedIndex].text + ' )' : '') + ', '


        }

    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=btnSubmit]").click(function () {
                var list2 = document.getElementById("<%=DataList2.ClientID%>");
                var hdnvalueee = $("#hndpri_yesno").val();

                if (hdnvalueee != "1") {
                    if (confirm('Do you want to Tag the Product?')) {
                        return true;
                    }
                    else {
                        return false;

                    }
                }

            });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id*=btnSubmit]").click(function () {
                var dropdowns = new Array();
                var samecheck = false;
                var test = new Array();
                var test2 = new Array();
                var list = document.getElementById("<%=DataList1.ClientID%>");
                var chklist = list.getElementsByTagName("input");
                var totCount = 0;
                var hdnvalue = $("#hndprio").val();
                var hdnvalueee = $("#hndpri_yesno").val();
                dropdowns = list.getElementsByTagName('select'); //Get all dropdown lists contained in GridView1.


                //              for (var j = 0; j < dropdowns.length; j++) {
                //                  alert(dropdowns.item(j).value);
                //              }

                if (hdnvalueee == "1") {
                    for (var i = 0, j = 0; i < chklist.length, j < dropdowns.length; i++, j++) {
                        if (chklist[i].checked) {
                            if (dropdowns.item(j).value == "0") {
                                alert("Select Priority");
                                return false;
                            }

                            if (dropdowns.item(j).value != "0") {

                                test += dropdowns.item(j).value;
                                test2 = test.substring(0, test.length - 1);

                                if (jQuery.inArray(dropdowns.item(j).value, test2) !== -1) {
                                    alert("Same Priority is Not Allowed for Different Product");
                                    samecheck = true;
                                    return false;

                                }


                            }
                            totCount++;
                        }
                    }
                    if (totCount > hdnvalue) {
                        alert("Not Select More than  " + hdnvalue + "  Product");
                        return false;
                    }
                    else {

                        if (confirm('Do you want to Tag the Product?')) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                }
                else {

                    if (confirm('Do you want to Tag the Product?')) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }


            });
        });
    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        .textalign {
            text-align: center;
            font-weight: bold;
        }

        .mycheckbox input[type="checkbox"] {
            margin-right: 7px;
        }

        .borderalignment {
            border-bottom: 1px solid #DCE2E8;
            border-right: 1px solid #DCE2E8;
            border-left: 1px solid #DCE2E8;
        }

        .alignment {
            text-align: center;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>


    <script type="text/javascript">

        function confirm_Save() {

            if (confirm('Do you want to Tag the Product?')) {
                if (confirm('Are you sure?')) {
                    ShowProgress();

                    return true;

                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    </script>
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
            $('#btnGo').click(function () {
                var type = $('#<%=ddldr.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Listed Doctor."); $('#ddldr').focus(); return false; }
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddldr]');
            var $items = $('select[id$=ddldr] option');

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

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">



                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" style="border-bottom: none">Listed Doctor - Product Tag</h2>
                        <asp:Panel ID="pnlDashbrd" runat="server">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">

                                    <div class="single-des-option">
                                        <asp:Label ID="lblDr" runat="server" Text="Listed Doctor Name" CssClass="label"></asp:Label>
                                        <%-- <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="130px" CssClass="TEXTAREA"
                                        ToolTip="Enter Text Here"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddldr" runat="server" CssClass="custom-select2 nice-select">
                                        </asp:DropDownList>

                                    </div>

                                </div>

                            </div>
                            <br />

                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" Text="GO" CssClass="savebutton" runat="server"
                                    OnClick="btnGo_Click" />
                                <%--<asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="GO" CssClass="savebutton"
                    OnClick="btnGo_Click" />--%>
                                <asp:Button ID="btnclr" OnClick="btnClear_Click" runat="server" Text="Clear"
                                    CssClass="resetbutton"></asp:Button>
                            </div>
                        </asp:Panel>
                        <asp:HiddenField ID="hndprio" runat="server" />
                        <asp:HiddenField ID="hndpri_yesno" runat="server" />
                        <br />
                    </div>

                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>
                <br />
                <div class="row justify-content-center">
                    <asp:Label ID="lblSelect" Text="Select the Product" runat="server" Visible="false"></asp:Label>
                </div>
                <br />

                </asp:Panel>

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; padding-bottom: 20px;">
                                <table align="center" width="100%" cellpadding="1" cellspacing="1" style="border-collapse: collapse">
                                    <tbody>
                                        <tr>
                                            <td>

                                                <asp:DataList ID="DataList1" HeaderStyle-BackColor="#f1f5f8" Width="100%" CellPadding="5"
                                                    runat="server" RepeatDirection="Vertical"
                                                    RepeatColumns="2">

                                                    <HeaderTemplate>
                                                        <div style="padding: 20px; min-width: 450px">
                                                            <asp:Label ID="lblsln" Text="#" Font-Bold="true" Width="90px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDocVisit" Text="Product Name" Font-Bold="true" Width="180px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblunit" Text="Pack" Font-Bold="true" Width="110px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblpri1" Text="Priority" Font-Bold="true" Width="157px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl1" Text="#" Font-Bold="true" Width="90px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl2" Text="Product Name" Font-Bold="true" Width="180px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl3" Text="Pack" Font-Bold="true" Width="110px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblpri2" Text="Priority" Font-Bold="true" runat="server"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="borderalignment" />
                                                    <AlternatingItemStyle />
                                                    <ItemStyle />
                                                    <ItemTemplate>
                                                        <b></b>
                                                        <div style="min-width: 420px; vertical-align: inherit; display: inline-block">
                                                            <asp:Label ID="lblSLNO" runat="server" Width="50px" CssClass="alignment" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="lblPrdCode" runat="server" Text='<%#Eval("Product_Code_SlNo")%>' Visible="false"></asp:Label>&nbsp&nbsp
                                    <asp:CheckBox ID="chkCatName" onclick="ChkFn(this)" Width="200px"
                                        CssClass="mycheckbox" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product_Detail_Name")%>' />&nbsp&nbsp&nbsp&nbsp&nbsp
                                    <asp:Label ID="lblprd_sale" runat="server" Width="80px" Text='<%#Eval("Product_Sale_Unit")%>'></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp
                                    <asp:DropDownList ID="ddlPriority" runat="server" onChange="ChkFn(this)">
                                        <%--  <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>

                                                <%--  </asp:DataList>--%>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:DataList ID="DataList2" HeaderStyle-BackColor="#f1f5f8" Width="85%" CellPadding="5"
                                                    runat="server" RepeatDirection="Vertical"
                                                    RepeatColumns="2">

                                                    <HeaderTemplate>
                                                        <div style="padding: 20px; min-width: 450px">
                                                            <asp:Label ID="lblsln" Text="#" Font-Bold="true" Width="90px" Height="20px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDocVisit" Text="Product Name" Font-Bold="true" Width="180px" Height="20px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblunit" Text="Pack" Font-Bold="true" Width="165px" Height="20px" runat="server"></asp:Label>

                                                            <asp:Label ID="lbl1" Text="#" Font-Bold="true" Width="90px" Height="20px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl2" Text="Product Name" Font-Bold="true" Width="180px" Height="20px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl3" Text="Pack" Font-Bold="true" Height="20px" runat="server"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="borderalignment" />
                                                    <AlternatingItemStyle />
                                                    <ItemStyle />

                                                    <ItemTemplate>
                                                        <b></b>
                                                        <div style="min-width: 420px; vertical-align: inherit; display: inline-block">
                                                            <asp:Label ID="lblSLNO" runat="server" CssClass="alignment" Width="70px" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="lblPrdCode" runat="server" Text='<%#Eval("Product_Code_SlNo")%>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkCatName" onclick="ChkFn(this)" Width="230px"
                                                                CssClass="mycheckbox" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product_Detail_Name")%>' />
                                                            <asp:Label ID="lblprd_sale" runat="server" Width="80px" Text='<%#Eval("Product_Sale_Unit")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <%--  </asp:DataList>--%>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--<table align ="center" >
                   <tr>
                     <td colspan="2" align="center">
                        <asp:CheckBoxList ID="chkprd" CssClass="chkboxLocation" CellPadding="10" Visible="false"  RepeatColumns="3" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"  RepeatDirection="vertical"  Width="300px" runat="server">
                        </asp:CheckBoxList>
                    </td>
                   </tr>
                </table>
                --%>
                <center>
                    <div class="row justify-content-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="savebutton" Visible="false"
                            OnClick="btnSubmit_Click" />
                    </div>
                </center>
            </div>
            <br />
            <br />

        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>
