<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mail_Zoom.aspx.cs" Inherits="MasterFiles_Mails_Mail_Zoom"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mail</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <%--<link type="text/css" rel="stylesheet" href="../../css/sfm_style.css" />--%>
    <script type="text/javascript">
        function PrintGridData() {
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>



    <style type="text/css">
        .img1 {
            margin: 0px 0px 0px 0px;
            background: url("../../images/sendicon.gif") left center no-repeat;
            padding: 0em 1.2em;
            font: 8pt "tahoma";
            color: #336699;
            text-decoration: none;
            font-weight: normal;
            letter-spacing: 0px;
        }

        .gvBorder {
            border: 1px;
        }

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

        .pgr1 {
            z-index: 1;
            left: 140px;
            top: 525px;
            position: absolute;
            width: 89%;
        }

        .CursorPointer {
            cursor: default;
        }

        .mGrid .pgr1 {
            background: #A6A6D2;
        }

            .mGrid .pgr1 table {
                margin: 5px 0;
            }

            .mGrid .pgr1 td {
                border-width: 0;
                padding: 0 6px;
                text-align: left;
                border-left: solid 0px #666;
                font-weight: bold;
                color: Red;
                line-height: 12px;
            }

            .mGrid .pgr1 th {
                background: #A6A6D2;
            }

            .mGrid .pgr1 a {
                color: Blue;
                text-decoration: none;
            }

                .mGrid .pgr1 a:hover {
                    color: White;
                    text-decoration: none;
                }
    </style>
    <script type="text/javascript" src="../../JsFiles/common.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
        $(function () {
            $('#chkDesgn_OnSelectedIndexChanged').click(function () {
                var status = $('#chkDesgn').is(':checked');

            })
        });
    </script>
    <style type="text/css">
        .HeaderPrint {
            color: White;
            background-color: #179BED;
            padding: 10px;
            border-radius: 10px;
            border-bottom-right-radius: 10px;
            border-bottom-left-radius: 10px;
            border: 0px solid black;
            text-align: center;
            border-bottom-left-radius: 0px;
            border-bottom-right-radius: 0px;
        }

        body {
            background-color: White;
        }

        .input {
            border: none;
            scrollbar-width: thin;
            font-family: 'Roboto', sans-serif;
            font-weight: 300;
        }

        .MainHeader {
            -webkit-box-shadow: 0 1px 8px rgba(30, 31, 32, 0.22);
        }
    </style>

</head>
<body>
    <form id="frmAddr" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdnstate" runat="server" />
        <asp:HiddenField ID="hdnFlg" Value="" runat="server" />
        <asp:HiddenField ID="hdnDesign" runat="server" />
        <asp:HiddenField ID="hdnVal" runat="server" />

        <div class="container1">
            <div class="row justify-content-center">
                <div class="col-lg-10">

                    <asp:Panel ID="pnlViewInbox" runat="server" BackColor="white" Height="610px"
                        Width="99%" Style="top: 55px; margin-left: 2px; font-size: 10pt; position: absolute;" CssClass="MainHeader">
                        <div style="border-collapse: collapse; width: 100%;" class="HeaderPrint">
                            <table border='0' style='border-collapse: collapse; width: 100%; height: 100%; font-size: 12pt'>
                                <tr>
                                    <td class='print Header' style='width: 100%'>&nbsp;<span class='itemImage1'><img alt='Address Book' src="../Images/Address_Book_Icon.gif" /></span>
                                        View Mail Details
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <asp:Panel ID="pnlViewMailInbox" runat="server" BackColor="#F1F5F8" Height="570px"
                            Width="100%" Style="left: 0px; top: 40px; position: absolute;">
                            <table>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize; font-size: 12pt; text-align: left; margin: 5px"
                                            Font-Bold="True" ForeColor="#0077FF">
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellpadding="0" cellspacing="0" style="height: 85px; margin: 5px;">
                                <tr style="width: 50px">
                                    <td style="width: 75px">
                                        <asp:Label ID="lblFrom" runat="server" Text="&nbspFrom"></asp:Label>
                                    </td>
                                    <td width="10px">:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblViewFrom" runat="server" ForeColor="#0077FF"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 50px">
                                    <td style="width: 25px">
                                        <asp:Label ID="lblTo" runat="server" Text="&nbsp;To"></asp:Label>
                                    </td>
                                    <td width="10px">:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblViewTo" runat="server" ForeColor="#0077FF"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 50px">
                                    <td style="width: 25px">
                                        <asp:Label ID="lblCc" runat="server" Text="&nbsp;Cc "></asp:Label>
                                    </td>
                                    <td width="10px">:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblViewCC" runat="server" ForeColor="#0077FF"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 50px">
                                    <td style="width: 25px">
                                        <asp:Label ID="lblSubject" runat="server" Text="&nbsp;Subject"></asp:Label>
                                    </td>
                                    <td width="10px">:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblViewSub" runat="server" ForeColor="#0077FF"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 50px">
                                    <td style="width: 25px">
                                        <asp:Label ID="lblSentDate" runat="server" Text="&nbsp;Sent Date"></asp:Label>
                                    </td>
                                    <td width="10px">:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblViewSent" runat="server" ForeColor="#0077FF"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 50px">
                                    <td style="width: 25px">
                                        <asp:Label ID="lblRead" runat="server" Text="&nbsp;Read Date"></asp:Label>
                                    </td>
                                    <td width="10px">:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReadDate" runat="server" ForeColor="#0077FF"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Image ID="imgViewAttach" runat="server" Visible="false" ImageUrl="~/Images/Attachment.gif" />
                                    </td>
                                    <td><a id="aTagAttach" runat="server">
                                        <asp:Label ID="lblViewAttach" runat="server"></asp:Label></a></td>
                                </tr>
                            </table>
                            <div style="margin: 7px;">
                                <span style="white-space: pre-line">
                                    <asp:TextBox ID="lblMailBody" onDrag="return false;" onDrop="return false;"
                                        name="txtMsg" TextMode="MultiLine" onpaste="return MaxLenOnPaste(5000)" MaxLength="5000"
                                        Height="400px" Width="100%" runat="server" onblur="javascript:generateTable()" CssClass="input"></asp:TextBox>
                                </span>
                            </div>
                        </asp:Panel>
                    </asp:Panel>

                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>

    </form>
</body>
</html>
