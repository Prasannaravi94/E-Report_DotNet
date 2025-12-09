<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Carservice.aspx.cs" Inherits="Carservice" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />
    <%--  <link type="text/css" rel="stylesheet" href="css/style.css" />--%>
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

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .auto-style1 {
            width: 289px;
        }

        .auto-style2 {
            width: 391px;
        }


        #tbl {
            width: 820px;
        }

        .auto-style3 {
            width: 143px;
        }

        #tbl11 tr td {
            border-color: #DCE2E8;
        }

            #tbl11 tr td:first-child {
                background-color: #F1F5F8;
            }
            #tbl11 .single-des {
    margin-bottom: 2px;
}
    </style>

    <script type="text/javascript" src="JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="JsFiles/jquery-1.10.1.js"></script>
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

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', '');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>


    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <%--    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

    <%--<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />--%>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.12.4.js"></script>
    <script src="//code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script type="text/javascript" language="javascript">
        var today = new Date();
        var lastDate = new Date(today.getFullYear(), today.getMonth(0) - 1, 31);
        var year = today.getFullYear() - 1;


        var dd = today.getDate();
        var mm = today.getMonth() + 01; //January is 0!
        var yyyy = today.getFullYear();

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
            ({
                minDate: dd + '/' + mm + '/' + yyyy,

                dateFormat: 'dd/mm/yy'

            });

            j('.DOBfROMDate').datepicker
            ({


                dateFormat: 'dd/mm/yy'

            });

        });
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <div id="Divid" runat="server"></div>

        <table width="100%" id="tbl5" runat="server" visible="false">
            <tr>
                <td></td>
                <td style="float: right;">
                    <table id="tblfm" runat="server" visible="false">
                        <tr>
                            <td style="padding-right: 30px">
                                <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                </asp:LinkButton>
                                <asp:Label ID="Label23" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                <%--<asp:ImageButton ID="btnPrint" ImageUrl="~/Images/printer.png" runat="server" Width="35px" 
                                    Height="30px" OnClientClick="return PrintPanel();" />--%>
                            </td>
                            <td style="padding-right: 30px">
                                <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                </asp:LinkButton>
                                <asp:Label ID="Label22" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                <%-- <asp:ImageButton ID="btnExcel" ImageUrl="~/Images/Excels.png" runat="server" Height="30px"
                                    Width="35px" />--%>
                            </td>
                            <td style="padding-right: 50px">
                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                </asp:LinkButton>
                                <asp:Label ID="Label18" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                <%-- <asp:ImageButton ID="btnClose" ImageUrl="~/Images/closebtn.png" runat="server" Height="30px"
                                    Width="35px" OnClientClick="RefreshParent()" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-6">
                    <asp:Panel ID="pnlContents" runat="server">
                        <center>
                            <div align="center" class="reportheader">
                                CAR SERVICE
                            </div>
                        </center>

                        <br />

                        <div class="row " id="tbl" visible="false" runat="server">

                            <div class="col-lg-6">
                                <asp:Label ID="Label14" CssClass="label" Font-Bold="True" runat="server">FieldForce Name </asp:Label>
                                &nbsp;&nbsp;<asp:Label ID="LabelSf_name" CssClass="label" runat="server" ForeColor="#0077FF"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label ID="Label17" CssClass="label" Font-Bold="true" runat="server">HQ </asp:Label>
                                &nbsp;&nbsp;<asp:Label ID="LabelHQ" CssClass="label" runat="server" ForeColor="#0077FF"></asp:Label>
                            </div>

                        </div>

                        <br />
                        <center>
                            <table border="1" style="height: 100%; width: 100%; border-color: #DCE2E8" id="tbl11">
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="lblName" CssClass="label" runat="server">Name of Doctor  </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtName" runat="server" Width="220px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="200">
                                            </asp:TextBox>
                                        </div>

                                        <asp:Label ID="LabelName" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label1" CssClass="label" runat="server">Category  </asp:Label>

                                    </td>
                                    <td align="left">
                       <asp:DropDownList ID="drpCategory" runat="server"></asp:DropDownList>
                                        <asp:Label ID="LabelCategory" runat="server" CssClass="label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label2" CssClass="label" runat="server">Place  </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtPlace" runat="server" Width="220px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="200">
                                            </asp:TextBox>
                                        </div>

                                        <asp:Label ID="LabelPlace" CssClass="label" runat="server"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label3" CssClass="label" runat="server">MobileNo  </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtMobileNo" runat="server" Width="87px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="200"></asp:TextBox>
                                        </div>

                                        <asp:Label ID="LabelMobileNo" runat="server" CssClass="label"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label4" runat="server" CssClass="label">No.of days Car service required  </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtdays" runat="server" Width="59px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="200"></asp:TextBox>
                                        </div>

                                        <asp:Label ID="Labeldays" CssClass="label" runat="server"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label5" CssClass="label" runat="server">Contact point for pickup  </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtPic" runat="server" MaxLength="200" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='#f4f8fa'" onkeypress="AlphaNumeric_NoSpecialChars(event)" Width="300px"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="LabelPic" CssClass="label" runat="server"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label6" CssClass="label" runat="server">No.of persons </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtpersons" runat="server" Width="62px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='#f4f8fa'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="200"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="Labelpersons" CssClass="label" runat="server"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label7" CssClass="label" runat="server">Arrival Date </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtADate" runat="server" Width="220px" CssClass="DOBfROMDate input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="Calendar_enter(event)" onblur="this.style.backgroundColor='#f4f8fa'">
                                            </asp:TextBox>
                                            <%-- <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtADate"
                                                CssClass="cal_Theme1" runat="server" />--%>
                                        </div>


                                        <asp:Label ID="LabelADate" CssClass="label" runat="server"> </asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label8" CssClass="label" runat="server">Time </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtATime" runat="server" CssClass="input" TextMode="Time" format="HH:mm" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                onblur="this.style.backgroundColor='#f4f8fa'" Width="105px"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="LabelATime" CssClass="label" runat="server"> </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label15" CssClass="label" runat="server">Departure Date </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtDDate" runat="server" CssClass="DOBDate input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="Calendar_enter(event)" MaxLength="200" onblur="this.style.backgroundColor='#f4f8fa'">
                                            </asp:TextBox>
                                        </div>

                                        <asp:Label ID="LabelDDate" CssClass="label" runat="server"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label16" CssClass="label" runat="server">Time </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtDtime" runat="server" CssClass="input" TextMode="Time" format="HH:mm" onfocus="this.style.backgroundColor='LavenderBlush'"
                                                onblur="this.style.backgroundColor='#f4f8fa'" Width="105px"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="LabelDtime" CssClass="label" runat="server"> </asp:Label>
                                    </td>
                                </tr>



                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label9" CssClass="label" runat="server">Flt.No/Train Name </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txttrainname" runat="server" Width="298px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="200" onblur="this.style.backgroundColor='#f4f8fa'"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="Labeltrainname" CssClass="label" runat="server"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label10" CssClass="label" runat="server">Business in last three months </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtlastmonth1" runat="server" Width="80px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="50" onblur="this.style.backgroundColor='#f4f8fa'"></asp:TextBox>
                                            <asp:TextBox ID="txtlastmonth2" runat="server" Width="80px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="50" onblur="this.style.backgroundColor='#f4f8fa'"></asp:TextBox>
                                            <asp:TextBox ID="txtlastmonth3" runat="server" Width="80px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="50" onblur="this.style.backgroundColor='#f4f8fa'"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="Labellastmonth1" CssClass="label" runat="server"> </asp:Label>

                                        <asp:Label ID="Labellastmonth2" CssClass="label" runat="server"> </asp:Label>
                                        <asp:Label ID="Labellastmonth3" CssClass="label" runat="server"> </asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label11" CssClass="label" runat="server">Business Assured by Dr.in next 3 months </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtnextmonth1" runat="server" Width="80px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="50" onblur="this.style.backgroundColor='#f4f8fa'">
                                            </asp:TextBox>
                                            <asp:TextBox ID="txtnextmonth2" runat="server" Width="80px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="50" onblur="this.style.backgroundColor='#f4f8fa'">
                                            </asp:TextBox>
                                            <asp:TextBox ID="txtnextmonth3" runat="server" Width="80px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="50" onblur="this.style.backgroundColor='#f4f8fa'"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="Labelnextmonth1" CssClass="label" runat="server"> </asp:Label>
                                        <asp:Label ID="Labelnextmonth2" CssClass="label" runat="server"> </asp:Label>
                                        <asp:Label ID="Labelnextmonth3" CssClass="label" runat="server"> </asp:Label>


                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label12" CssClass="label" runat="server">Approx No.of times car service used earlier </asp:Label>

                                    </td>
                                    <td align="left">
                                        <div class="single-des">
                                            <asp:TextBox ID="txtearlier" runat="server" Width="49px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="200" onblur="this.style.backgroundColor='#f4f8fa'"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="Labelearlier" CssClass="label" runat="server"> </asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style1">
                                        <asp:Label ID="Label13" CssClass="label" runat="server">Last Car Service Month </asp:Label>

                                    </td>
                                    <td align="left">
 <div class="single-des">
     <asp:TextBox ID="txtlastService" runat="server" Width="65px" CssClass="input" onfocus="this.style.backgroundColor='#E0EE9D'"
         onkeypress="AlphaNumeric_NoSpecialChars(event)" MaxLength="200" onblur="this.style.backgroundColor='#f4f8fa'"></asp:TextBox>
 </div>
                                        <asp:Label ID="LabellastService" CssClass="label" runat="server"> </asp:Label>

                                    </td>
                                </tr>


                            </table>

                            <br />
                            <table id="tbl1" style="height: 100%; width: 100%" visible="false" runat="server">

                                <tr>
                                    <td align="left" class="auto-style2"></td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label21" runat="server" Font-Bold="True" ForeColor="#292a34" CssClass="label">kindly arrange for the above service</asp:Label>
                                    </td>
                                    <td align="left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" class="auto-style2">
                                        <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="#292a34" CssClass="label">Form Submitted  Date:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label20" runat="server" Font-Bold="True" ForeColor="#292a34" CssClass="label">ABM/RBM:</asp:Label>
                                        &nbsp;</td>
                                </tr>


                            </table>


                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSubmit" CssClass="savebutton" runat="server" Text="Save"  OnClick="btnSubmit_Click" Width="57px"></asp:Button>
                                    </td>
                                </tr>
                            </table>

                        </center>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="Images/loader.gif" alt="" />

        </div>
    </form>
</body>
</html>
