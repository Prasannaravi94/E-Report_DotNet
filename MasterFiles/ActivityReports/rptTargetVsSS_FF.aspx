<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTargetVsSS_FF.aspx.cs" Inherits="MasterFiles_ActivityReports_rptTargetVsSS_FF" %>

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

    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>


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
    <script type="text/javascript">
        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#ffeb95';
            element.style.cursor = 'pointer';

        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';

        }
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .mGrid th {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .mGrid .alt {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }

            .mGrid .pgr {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        .display-table3rowspan .table td {
            border-color: #DCE2E8;
            border-right: none;
        }
    </style>


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
                $(this).css("color", "Fuchsia");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "15px");
            });
            $(".btnDrMt").mouseout(function () {
                $(this).css("color", "red");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            //
            $(".btnDrSn").mouseover(function () {
                $(this).css("color", "darkgreen");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "15px");
            });
            $(".btnDrSn").mouseout(function () {
                $(this).css("color", "blue");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            //
            $(".btnDrMsd").mouseover(function () {
                $(this).css("color", "Red");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "15px");
            });
            $(".btnDrMsd").mouseout(function () {
                $(this).css("color", "Red");
                $(this).css("font-weight", "bolder");
                $(this).css("font-size", "13px");
            });
            //
        });
    </script>


    <script type="text/javascript">

        var popUpObj;
        function showModalPopUp(div_code, sfcode, FMonth, FYear, Tmon, Tyr, Sf_Name, HQ_Code, HQ_Name, Mode) {
            //popUpObj = window.open("/MasterFiles/AnalysisReports/ViewDetails_DrWise_Report.aspx?sf_code=" + sfcode + "&FMnth=" + FMonth + "&FYear=" + FYear + "&TMonth=" + Tmon + "&TYear=" + Tyr + "&HQ_Code=" + HQ_Code + "&sf_name=" + Sf_Name + "&HQ_Name=" + HQ_Name + "&cMode=" + "7" + "&cTyp_cd=" + "" + "&typ=" + "",
            popUpObj = window.open("/MasterFiles/AnalysisReports/rpt_Primary_Stk_Bill_View.aspx?div_code=" + div_code + "&Sf_Code=" + sfcode + "&Frm_Month=" + FMonth + "&Frm_year=" + FYear + "&To_Month=" + Tmon + "&To_year=" + Tyr + "&HQ_Code=" + HQ_Code + "&sf_name=" + Sf_Name + "&HQ_Name=" + HQ_Name + "&Mode=" + Mode,
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
<body style="overflow-x: scroll">
    <form id="form1" runat="server">
        <br />
        <div class="row justify-content-center">
            <div class="col-lg-12">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                       
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

                                            <td style="padding-right: 40px">
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

            <div class="container clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                             <div align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Target View from " CssClass="reportheader"></asp:Label>
                            <br />
                            <asp:Label ID="LblForceName" runat="server" CssClass="reportheader" Font-Size="16px" ForeColor="#696d6e"></asp:Label>
                            <br />

                            <asp:Label ID="lblstock" runat="server" ForeColor="Pink" CssClass="reportheader" Font-Size="16px"></asp:Label>
                        </div>
                            <div align="center">

                                <asp:TextBox ID="Tomatocolor" runat="server" Width="30px" BackColor="Tomato" Enabled="false" Visible="false"></asp:TextBox>
                                <asp:Label ID="Tomato" runat="server" Visible="false"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                        
                        <asp:TextBox ID="Yellowcolor" runat="server" Width="30px" BackColor="Yellow" Visible="false" Enabled="false"></asp:TextBox>
                                <asp:Label ID="Yellow" runat="server" Visible="false"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                         
                        <asp:TextBox ID="LightGreencolor" runat="server" Width="30px" BackColor="LightGreen" Enabled="false" Visible="false"></asp:TextBox>
                                <asp:Label ID="LightGreen" runat="server" Visible="false"></asp:Label>

                                &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="LightPinkcolor" runat="server" Width="30px" BackColor="LightPink" Enabled="false" Visible="false"></asp:TextBox>
                                <asp:Label ID="LightPink" runat="server" Visible="false"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                        
                        <asp:TextBox ID="Aquacolor" runat="server" Width="30px" BackColor="Aqua" Enabled="false" Visible="false"></asp:TextBox>
                                <asp:Label ID="Aqua" runat="server" onclick="Aqua_Click"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="SkyBluecolor" runat="server" Width="30px" BackColor="SkyBlue" Enabled="false" Visible="false"></asp:TextBox>
                                <asp:Label ID="SkyBlue" runat="server" onclick="SkyBlue_Click"></asp:Label>

                            </div>

                            <div class="display-table3rowspan clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                    <asp:Panel ID="Panel1" runat="server">

                                        <asp:GridView ID="GrdFixation" runat="server" AlternatingRowStyle-CssClass="alt" Style="background-color: white"
                                            AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                            GridLines="Both" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated" BorderColor="WhiteSmoke" BorderWidth="1"
                                            ShowHeader="False" Width="100%" OnRowDataBound="GrdFixation_RowDataBound">

                                            <Columns>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                        <asp:GridView ID="grdAppdashboard" runat="server" Width="80%" Font-Size="11px" Font-Names="Calibri" OnRowDataBound="grdAppdashboard_Rowdatabound"
                                            HorizontalAlign="Center" OnRowCreated="grdAppdashboard_RowCreated" ShowHeader="false"
                                            AutoGenerateColumns="false" GridLines="None" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                            AllowSorting="True">
                                            <HeaderStyle Font-Bold="False" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsf_name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Desigantion" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblhq" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvalue" runat="server" Text='<%#Eval("1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" < 85" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl1" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="86 - 90" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl2" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="91 - 95" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl3" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="96 - 100" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl4" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="101 - 105" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl5" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--
                                         <asp:TemplateField HeaderText="51 - 60" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl6" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="61 - 70" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl7" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                           <asp:TemplateField HeaderText="71 - 80" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl8" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="81 - 90" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl9" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="91 - 100" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl10" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText=" > 105" ItemStyle-HorizontalAlign="center" HeaderStyle-Wrap="false"
                                                    ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl6" runat="server"></asp:Label>
                                                        <%-- <asp:Label ID="lbl11" runat="server"></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Color" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif"
                                                            ForeColor="#483d8b" Text='<%# Bind("des_Color") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>

                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
            <br />
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

