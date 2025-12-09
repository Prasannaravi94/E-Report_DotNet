<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Month_Vertical_Wise_Report.aspx.cs"
    Inherits="MIS_Reports_Visit_Details_Basedonfield_Level1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visit Details Field Report</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/repstyle.css" />--%>


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

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, cmode, Type) {
            popUpObj = window.open("Visit_Details_Basedonfield_Level2.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&cMode=" + cmode + "&Type=" + Type,
            "_blank",
        "ModalPopUp_Level1," +
         "0," +
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
            //LoadModalDiv();
        }

    </script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
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
        $("form1").live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(document).live('load', function () {
            $('#GrdDoctor tr').each(function () {
                var vara = $(this).find('td').html();
                if (vara != null)
                    alert($(this).find('td').html());
            });
        });
    </script>
    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <style type="text/css">
        .gvHeader th {
            padding: 3px;
            background-color: #DDEECC;
            color: maroon;
            border: 1px solid #bbb;
        }

        .gvRow td {
            padding: 3px;
            background-color: #ffffff;
            border: 1px solid #bbb;
        }

        .gvAltRow td {
            padding: 3px;
            background-color: #f1f1f1;
            border: 1px solid #bbb;
        }

        .gvHeader th:first-child {
            display: none;
        }

        .gvRow td:first-child {
            display: none;
        }

        .gvAltRow td:first-child {
            display: none;
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
        /*Fixed Heading & Fixed Column-Begin*/
        /*Fixed Heading & Fixed Column-Begin*/
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

        .stickySecondRow {
            position: sticky;
            position: -webkit-sticky;
            top: 31px;
            z-index: 1;
            background: inherit;
        }

        .stickyThirdRow {
            position: sticky;
            position: -webkit-sticky;
            top: 63px;
            z-index: 1;
            background: inherit;
        }

        .display-table3rowspan .table tr:first-child td:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-table3rowspan .table tr:nth-child(n+4) td:first-child, .display-table3rowspan .table tr:nth-child(n+4) td:nth-child(2),
        display-table3rowspan .table tr:nth-child(n+4) td:nth-child(3) {
            z-index: 0;
        }

        .stickyfirstdata {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 1;
        }

        .display-table3rowspan .table tr:first-child td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 33px;
            background: inherit;
            z-index: 2;
            min-width: 158px;
        }

        .stickySeconddata {
            position: sticky;
            position: -webkit-sticky;
            z-index: 1;
            /*background-color: white;*/
            background: inherit;
            left: 33px;
        }

        .display-table3rowspan .table tr:first-child td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 190px;
            background: inherit;
            z-index: 2;
        }

        .stickyThirddata {
            position: sticky;
            position: -webkit-sticky;
            z-index: 1;
            /*background-color: white;*/
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
        /*tr:first-child, .display-table3rowspan .table tr:nth-child(2), .display-table3rowspan .table tr:nth-child(3) {

            border-bottom: 2px solid #dce2e8;
        }*/

        /*.display-table3rowspan .table td {
            padding: 5px 5px;
        }*/

        .display-table3rowspan .table tr:first-child, .display-table3rowspan .table tr:nth-child(2), .display-table3rowspan .table tr:nth-child(3) {
            border-bottom: 2px solid #dce2e8;
        }

        .display-table3rowspan .table td {
            padding: 5px 5px;
        }

        .display-table3rowspan .table tr:nth-child(2) td:first-child, .display-table3rowspan .table tr:nth-child(3) td:first-child {
            border-left: 1px solid #DCE2E8;
            padding: 5px 18px;
            border-bottom: 2px solid #DCE2E8;
        }
        .display-table3rowspan .table tr td:first-child {
            padding: 5px 10px;
        }
     
    </style>
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
            popUpObj = window.open("Visit_Month_Vertical_Wise_Report_Cat_Zoom.aspx?sfcode=" + sfcode + "&FMnth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + Tmon + "&TYear=" + Tyr + "&mode=" + mode + "&sf_name=" + sf_name + "&SfMGR=" + SfMGR,
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
    </script>

</head>
<body style="overflow-x: scroll;">
    <form id="form1" runat="server">
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-2">
                        <br />
                        <div runat="server" id="divSettings" style="margin-left: 35px; cursor: pointer;">
                            <asp:ImageButton ID="settings" runat="server" ImageUrl="../../Images/cog.png" ToolTip="Show/Hide Grid Columns" Style="width: 30px; height: 30px; position: absolute;" />
                            <asp:Label ID="Label5" runat="server" Text="Show/Hide Columns" CssClass="label" Font-Size="14px" Style="margin-left: 32px; margin-top: 4px; height: 30px; display: inline-block; vertical-align: middle; font-weight: 401;"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-7">
                        <div align="center">
                            <br />
                            <asp:Label ID="lblHead" runat="server" Text="Visit Details for the month of " CssClass="reportheader"></asp:Label>

                            <br />
                            <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" Font-Size="18px" ForeColor="#696d6e"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>
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
                                                <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                                </asp:LinkButton>
                                                <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                            </td>

                                            <td style="padding-right: 50px">
                                                <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
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
            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">

                            <left>
                         <asp:Panel ID="Panel1" runat="server">
                                 <div class="display-table3rowspan clearfix">
                                       <br />
                             
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
                                <div class="table-responsive" style="scrollbar-width: thin; overflow-x:inherit;">
                                  <table width="100%" > 
                                   <caption>
                        
                                      <asp:GridView ID="GrdFixationMode" runat="server" AlternatingRowStyle-CssClass="alt" 
                                        AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found" 
                                       GridLines="None" HorizontalAlign="Center"  BorderWidth="0"
                                       OnRowCreated="GrdFixationMode_RowCreated" onrowdatabound="GrdFixationMode_RowDataBound" 
                                        ShowHeader="False" Width="100%">
                         
                                      <Columns>
                                     </Columns>
                                       <EmptyDataRowStyle CssClass="no-result-area" />
                                     </asp:GridView>
                                   
                                  </caption>
                                  </table>
                                     </div>
                               </div>
                      </asp:Panel>
                     </left>
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
