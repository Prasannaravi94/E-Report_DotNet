<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Coverage_Pivot.aspx.cs" Inherits="MasterFiles_AnalysisReports_rpt_Coverage_Pivot" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/Calender_CheckBox.css" />


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var pageIndex = 1;
        var pageCount;
        $(function () {
            //Remove the original GridView header
            $("[id$=gvCustomers] tr").eq(0).remove();
        });

        //Load GridView Rows when DIV is scrolled
        $("#dvGrid").on("scroll", function (e) {
            var $o = $(e.currentTarget);
            if ($o[0].scrollHeight - $o.scrollTop() <= $o.outerHeight()) {
                GetRecords();
            }
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
    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />
    <script type="text/javascript" src="../../JsFiles/jquery.effects.core.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery.effects.blind.js"></script>
    <script type="text/javascript" src="../../JsFiles/multiple-select.js"></script>

    <link href="../../css/multiple-select.css" rel="stylesheet" type="text/css" />
    <script src="../../JsFiles/multiple-select.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstFruits]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#pnlNew").hide();

        });

        $(document).keypress(function (e) {

            var key = e.which;
            // alert(key);
            if (key == 116) {
                // if the user pressed 't':
                $("#pnlNew").show();
            }
        });

    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>



    <script type="text/javascript">
        $(document).ready(function () {
            //
            $(".btnLstDr").mouseover(function () {
                $(this).css("color", "Fuchsia");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            $(".btnLstDr").mouseout(function () {
                $(this).css("color", "black");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "11px");
            });
            //
            $(".btnDrMt").mouseover(function () {
                $(this).css("color", "darkgreen");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            $(".btnDrMt").mouseout(function () {
                $(this).css("color", "Fuchsia");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "11px");
            });
            //
            $(".btnDrSn").mouseover(function () {
                $(this).css("color", "red");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            $(".btnDrSn").mouseout(function () {
                $(this).css("color", "blue");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "11px");
            });
            //
            $(".btnDrMsd").mouseover(function () {
                $(this).css("color", "red");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            $(".btnDrMsd").mouseout(function () {
                $(this).css("color", "black");
                $(this).css("font-weight", "normal");
                $(this).css("font-size", "11px");
            });
            //
        });
    </script>


    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, FMonth, FYear, Tmon, Tyr, mode, Sf_Name, SfMGR) {
            popUpObj = window.open("rptMgr_Coverage_Detail_Zoom.aspx?sfcode=" + sfcode + "&FMnth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + Tmon + "&TYear=" + Tyr + "&mode=" + mode + "&Sf_Name=" + Sf_Name + "&SfMGR=" + SfMGR,
            "_blank",
        "ModalPopUp_Level1," +
         "0" //+
        //"toolbar=no," +
        //"scrollbars=1," +
        //"location=no," +
        //"statusbar=no," +
        //"menubar=no," +
        //"status=no," +
        //"addressbar=no," +
        //"resizable=yes," +
        //"width=650," +
        //"height=450," +
        //"left = 0," +
        //"top=0"
        );
            popUpObj.focus();
            //LoadModalDiv();
        }

        var popUpObj1;
        function showModalPopUpChmst(sfcode, FMonth, FYear, Tmon, Tyr, mode, Sf_Name, SfMGR) {
            popUpObj1 = window.open("rpt_Coverage_Pivot_Chmst_Zoom.aspx?sfcode=" + sfcode + "&FMnth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + Tmon + "&TYear=" + Tyr + "&mode=" + mode + "&Sf_Name=" + Sf_Name + "&SfMGR=" + SfMGR,
            "_blank",
        "ModalPopUp_Level1," +
         "0" //+
        //"toolbar=no," +
        //"scrollbars=1," +
        //"location=no," +
        //"statusbar=no," +
        //"menubar=no," +
        //"status=no," +
        //"addressbar=no," +
        //"resizable=yes," +
        //"width=650," +
        //"height=450," +
        //"left = 0," +
        //"top=0"
        );
            popUpObj1.focus();
            //LoadModalDiv();
        }
    </script>


    <style type="text/css">
        #pnlNew {
            position: fixed;
            top: 40%;
            left: 30%;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
        }

        .modalBackgroundNew {
            /*background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;*/
        }

        .modalPopupNew {
            background-color: #FFFFFF;
            width: 350px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }

            .modalPopupNew .header {
                background-color: #2FBDF1;
                height: 20px;
                color: White;
                line-height: 20px;
                text-align: center;
                font-weight: bold;
                font-size: 14px;
                font-family: Verdana;
            }

            .modalPopupNew .body {
                min-height: 120px;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                padding: 5px;
            }

        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-size: 11px;
            font-family: Calibri;
            -webkit-appearance: none;
            width: 300px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }
        /*Fixed Heading & Fixed Column-Begin*/
        /*.display-Approvaltable .table tr:first-child*/ .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        /*.display-Approvaltable .table tr:nth-child(2)*/ .stickySecondRow {
            position: sticky;
            position: -webkit-sticky;
            top: 20px;
            /*top: 35px;*/
            left: 250px;
            z-index: 0;
            background: inherit;
        }

        .display-Approvaltable .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-Approvaltable .table tr:nth-child(n+3) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            /*top: 150px;*/
            left: 0;
            z-index: 0;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 33px;
            background: inherit;
            z-index: 2;
            min-width: 158px;
        }

        .display-Approvaltable .table tr:nth-child(n+3) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            /*top: 150px;*/
            z-index: 0;
            background: inherit;
            left: 33px;
        }

        .display-Approvaltable .table tr:first-child td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 190px;
            background: inherit;
            z-index: 2;
        }

        .display-Approvaltable .table tr:nth-child(n+3) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            /*top: 150px;*/
            z-index: 0;
            background: inherit;
            left: 190px;
        }


        /*Fixed Heading & Fixed Column-End*/

        .modalPopup {
            background-color: #FFFFFF;
            width: 650px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

            .modalPopup .header {
                background-color: #2FBDF1;
                height: 40px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

            .modalPopup .body {
            }

            .modalPopup .footer {
                padding: 6px;
            }

            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

        [type="checkbox"]:disabled + label {
            color: #636d73;
        }

        [type="checkbox"]:disabled:checked + label::after {
            background: #636d73 !important;
        }
    </style>
    <%-- Newly Added  for grid size minimization--%>
    <style type="text/css">
        .display-Approvaltable .table tr:first-child, .display-Approvaltable .table tr:nth-child(2) {
            font-size: 12px;
        }

        .table td, .table th {
            padding: .4rem;
        }

        .display-Approvaltable {
            font-size: 11px;
        }

            .display-Approvaltable .table tr td:first-child {
                padding: 1px 1px;
            }

            .display-Approvaltable .table tr:first-child td:first-child {
                padding: 5px 10px;
            }

        body {
            overflow-x: scroll;
        }

        .table-responsive {
            overflow-x: inherit;
        }

        .display-Approvaltable .table tr:first-child, .display-Approvaltable .table tr:nth-child(2) {
            border-bottom: 2px solid #dce2e8;
        }

        .display-Approvaltable .table td {
            padding: 1px 16px;
        }

        #GrdFixation > tbody > tr:nth-child(1) > td:nth-child(3) {
            width: 6%;
        }

        .display-Approvaltable .table td {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>


            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />

                        <div class="row justify-content-center">
                            <div class="col-lg-2">


                                <div runat="server" id="divSettings" style="cursor: pointer;">
                                    <asp:ImageButton ID="settings" runat="server" ImageUrl="../../Images/cog.png" ToolTip="Edit Columns" Style="width: 30px; height: 30px; position: absolute;" />
                                    <asp:Label ID="Label5" runat="server" Text="Edit Columns" CssClass="label" Font-Size="14px" Style="margin-left: 32px; margin-top: 4px; height: 30px; display: inline-block; vertical-align: middle; font-weight: 401;"></asp:Label>
                                </div>
                                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancelnew"
                                    PopupControlID="Panel2" TargetControlID="divSettings" BackgroundCssClass="modalBackgroundNew">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Width="250px" align="center" Style="display: none; height: auto;">
                                    <div class="header">
                                        Show/Hide Column
                                    </div>
                                    <div class="body">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div style="height: auto; background-color: #f4f8fa; border: 1px solid Silver; overflow: auto; color: #90a1ac; font-size: 14px; border-radius: 10px; border: 1px solid #d1e2ea; background-color: #f4f8fa; margin-top: 0px; text-align: left;">
                                                    <asp:CheckBoxList ID="cblGridColumnList" Font-Size="8pt" runat="server"
                                                        CssClass="chkChoice">
                                                    </asp:CheckBoxList>
                                                    <br />
                                                    <br />
                                                    <div class="w-100 designation-submit-button text-center clearfix">
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click1" />
                                                        <asp:Button ID="btnCancelnew" runat="server" Text="Cancel" CssClass="savebutton" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>

                            </div>
                            <div class="col-lg-7">
                                <div align="center">
                                </div>

                            </div>
                            <div class="col-lg-3">

                                <table width="100%">
                                    <tr>
                                        <td></td>
                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 30px">
                                                        <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>

                                                    </td>
                                                    <td style="padding-right: 15px">
                                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                                    </td>

                                                    <td style="padding-right: 50px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="25px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <%-- <br />--%>
                            </div>
                        </div>
                        <asp:Panel ID="pnlContents" runat="server">
                            <div class="row justify-content-center">
                                <div class="col-lg-12">
                                    <div align="center">
                                        <asp:Label ID="lblHead" runat="server" Text="Coverage Analysis for the month of " CssClass="reportheader"></asp:Label>
                                        <br />
                                        <%--  <br />--%>
                                        <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="18px"></asp:Label>
                                    </div>

                                </div>
                            </div>
                            <br />

                            <div class="display-Approvaltable clearfix">


                                <div class="table-responsive">
                                    <asp:Panel runat="server">
                                        <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                            GridLines="Both" HorizontalAlign="Center" BorderWidth="1" BorderColor="WhiteSmoke"
                                            OnRowCreated="GrdFixation_RowCreated" OnRowDataBound="GrdFixation_RowDataBound"
                                            ShowHeader="False" Width="100%">

                                            <Columns>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlNew" runat="server" Width="700px" Height="500px" CssClass="modalPopupNew"
                            Style="display: none">
                            <div class="header">
                                Send to Mail
                <asp:ImageButton ID="imgbtnclosegift" runat="server" ImageUrl="~/Images/Close.gif"
                    ImageAlign="Right" />
                            </div>
                            <div class="body">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="From "></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblFrom" runat="server" SkinID="lblMand"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblto" runat="server" Width="100px" SkinID="lblMand" Text="To "></asp:Label>
                                        </td>
                                        <td align="left">


                                            <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple" CssClass="ddl">
                                                <%--     <asp:ListItem Text="Mango" Value="1" />
        <asp:ListItem Text="Apple" Value="2" />
        <asp:ListItem Text="Banana" Value="3" />
        <asp:ListItem Text="Guava" Value="4" />
        <asp:ListItem Text="Orange" Value="5" />--%>
                                            </asp:ListBox>

                                            <asp:Button ID="Button1" Text="Submit" runat="server" Visible="false" OnClick="Submit" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblSub" runat="server" SkinID="lblMand">Subject</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtsub" runat="server" Width="250px" Font-Bold="true" Font-Size="Medium"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblmessage" runat="server" SkinID="lblMand">Message</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtMessage" runat="server" Width="500px" Height="150px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="btnSend" runat="server" Font-Size="12px" Font-Bold="true" OnClick="btnSend_Onclick" BackColor="LightBlue" Width="80px" Text="Send" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Font-Size="12px" Font-Bold="true" BackColor="LightBlue" Width="100px" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>

        <script type="text/javascript">

            if ('<%= Session["Div_color"]!= null%>' == 'False') {
                document.body.style.backgroundColor = '#e8ebec';
            } else {
                document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
              }

        </script>
    </form>
</body>
</html>
