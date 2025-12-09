<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Quiz_Status_dtwise.aspx.cs" Inherits="MIS_Reports_Quiz_Status_dtwise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quiz Test Result</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />

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

        .selected_row {
            background-color: lightblue;
            color: Black;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=GrdFixation] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=GrdFixation] tr").each(function () {
                    if ($(this)[0] != row[0]) {
                        $("td", this).removeClass("selected_row");
                    }
                });
                $("td", row).each(function () {
                    if (!$(this).hasClass("selected_row")) {
                        $(this).addClass("selected_row");
                    } else {
                        $(this).removeClass("selected_row");
                    }
                });
            });
        });
    </script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

    <script type="text/javascript">
        var popUpObj;
        debugger
        function showQuizDetails(sfcode, div_code, sf_name, imonth, iyear, ans, day) {
            popUpObj = window.open("rptQuiz_Status.aspx?sfcode=" + sfcode + "&div_code=" + div_code + "&sf_name=" + sf_name + "&imonth=" + imonth + "&iyear=" + iyear + "&ans=" + ans + "&day=" + day,
         "_blank",
 "ModalPopUp," +
 "0" //+
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
 //"top = 0"
 );
            popUpObj.focus();
            //  LoadModalDiv();
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();" Visible="false">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px" Visible="false"></asp:Label>

                                </td>
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label2" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>

                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />

            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                          
                                <div align="center">
                                    <asp:Label ID="lblHead" runat="server" Text="" CssClass="reportheader"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" ForeColor="#696d6e" Font-Size="18px"></asp:Label>
                                </div>

                                <br />
                                <br />

                                <div class="display-Approvaltable clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                        <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                            GridLines="None" HorizontalAlign="Center" 
                                            OnRowCreated="GrdFixation_RowCreated" OnRowDataBound="GrdFixation_RowDataBound"
                                            ShowHeader="False" Width="100%" >
                                           
                                            <Columns>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>

    </form>
</body>
</html>
