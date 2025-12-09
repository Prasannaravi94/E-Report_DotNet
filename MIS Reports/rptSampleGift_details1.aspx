<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSampleGift_details1.aspx.cs" Inherits="MIS_Reports_rptSampleGift_details1" %>

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

    <%--<link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, Fieldforce_Name, Year, Month, Gift_Code, sCurrentDate) {
            popUpObj = window.open("rptSampleGift_details2.aspx?sf_code=" + sfcode + "&Fieldforce_Name=" + Fieldforce_Name + "&Year=" + Year + "&Month=" + Month + "&Gift_Code=" + Gift_Code + "&sCurrentDate=" + sCurrentDate,
     "_blank",
    "ModalPopUp" //+
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=600," +
    //"height=400," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();


            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s10.postimg.org/4i4mt6p3t/loading_23_ook.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:400px; height: 300px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }



    </script>
    <script language="Javascript">
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

    <style type="text/css">
        .display-reportMaintable .table tr:first-child td:first-child {
            border-radius: 8px 0 0 8px;
            background-color: #414d55;
            color: #ffffff;
            font-size: 14px;
            font-weight: 400;
            border-left: 0px solid #F1F5F8;
        }

        .display-reportMaintable .table tr:first-child td {
            padding: 20px 10px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 14px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            background-color: #F1F5F8;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <br />
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                    <td></td>
                    <td></td>
                    <%-- <td width="80%" align="center">
                        <asp:Label ID="lblProd" runat="server" Text="Sample Details" SkinID="lblMand"></asp:Label>
                    </td>--%>
                    <td align="right">
                        <table>
                            <tr>
                                <td style="padding-right: 30px">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClick="btnPrint_Click">
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
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();" OnClick="btnClose_Click">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <div>
           
            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">

                            <div align="center">
                                <asp:Label ID="lblProd" runat="server" Text="Sample Details" CssClass="reportheader"></asp:Label>
                                <%--<asp:Label ID="lblProd" runat="server" Text="Product Exposure" SkinID="lblMand" ></asp:Label>--%>
                                <br />
                                <br />
                                <asp:Label ID="lblfieldname" runat="server" Font-Size="16px" CssClass="label" Text="Fieldforce Name:"></asp:Label>
                                <asp:Label ID="lblname" runat="server" Font-Size="16px" CssClass="label"></asp:Label>
                            </div>
                            <br />
                            <br />

                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                    <asp:Table ID="tbl" runat="server" CssClass="table"
                                        GridLines="None" Width="100%">
                                    </asp:Table>
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
