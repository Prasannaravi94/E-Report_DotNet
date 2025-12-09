<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptJointWk_Analysis.aspx.cs" Inherits="MasterFiles_AnalysisReports_rptJointWk_Analysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SAN eReport</title>

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

    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <style type="text/css">
        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
            border-style: solid;
        }

        .display-reportMaintable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 12px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        .display-reportMaintable .table tr:first-child td {
            padding: 5px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            /*font-size: 14px;*/
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }

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

        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .display-reportMaintable .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-reportMaintable .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 33px;
            z-index: 2;
            min-width: 158px;
        }

        .display-reportMaintable .table tr:nth-child(n+2) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background: inherit;
            left: 33px;
        }

        .display-reportMaintable .table td {
            border: 1px solid #DCE2E8;
            padding: 5px 10px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
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
                $(this).css("color", "black");
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
        function showModalPopUp(sfcode, FMonth, FYear, Tmon, Tyr, mode, sf_name, SfMGR) {
            popUpObj = window.open("rptMgr_Coverage_Detail_Zoom.aspx?sfcode=" + sfcode + "&FMnth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + Tmon + "&TYear=" + Tyr + "&mode=" + mode + "&sf_name=" + sf_name + "&SfMGR=" + SfMGR,
            "_blank",
        "ModalPopUp_Level1," +
         "0"// +
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

    </script>
    
</head>
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="row justify-content-center">
                        <div class="col-lg-2">
                            <br />
                            <div runat="server" id="divSettings" style="cursor: pointer; margin-left65px;">
                                <asp:ImageButton ID="settings" runat="server" ImageUrl="../../Images/cog.png" ToolTip="Show/Hide Grid Columns" Style="width: 30px; height: 30px; position: absolute;" />
                                <asp:Label ID="Label5" runat="server" Text="Show/Hide Columns" CssClass="label" Font-Size="14px" Style="margin-left: 32px; margin-top: 4px; height: 30px; display: inline-block; vertical-align: middle; font-weight: 401;"></asp:Label>
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
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnCancelnew" runat="server" Text="Cancel" CssClass="savebutton" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="col-lg-7">
                        </div>
                        <div class="col-lg-3">

                            <br />
                            <table width="100%">
                                <tr>
                                    <td></td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 30px">
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>

                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" >
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnPDF" ToolTip="Excel" runat="server" Visible="false" OnClick="btnPDF_Click">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../../assets/images/pdf.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label1" runat="server" Text="Pdf" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>

                                                </td>
                                                <td style="padding-right: 50px">
                                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </div>
            </div>

            <div class="container clearfix" style="max-width: 1350px;">
                <asp:Panel ID="pnlContents" runat="server">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">

                            <div align="center">
                                <br />
                                <asp:Label ID="lblHead" runat="server" Text="Manager - HQ - Coverage from " CssClass="reportheader"></asp:Label>
                                <br />
                                <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="18px"></asp:Label>
                            </div>


                            <div class="display-reportMaintable clearfix">



                                <div class="table-responsive" style="overflow: inherit !important">
                                    <asp:Table ID="tbl" runat="server" BorderStyle="None" BorderWidth="1" GridLines="Both" CssClass="table" BorderColor="WhiteSmoke"
                                        Width="100%" Style="background-color: white;">
                                    </asp:Table>
                                </div>
                            </div>


                        </div>
                    </div>
                </asp:Panel>
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
