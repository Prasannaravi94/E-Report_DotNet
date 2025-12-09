<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginFieldforce.aspx.cs" EnableEventValidation="true"
    Inherits="MasterFiles_ActivityReports_LoginFieldforce" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Into FieldForce</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

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

            $('#btnSubmit').click(function () {

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "No Items Found") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }


            });
        });
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });
    </script>
    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            width: 190px;
            height: 21px;
            font: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow:;
        }

        #effect {
            width: 180px;
            height: 160px;
            padding: 0.4em;
            position: relative;
            overflow: auto;
        }

        .textbox {
            width: 185px;
            height: 14px;
        }

        body {
            font-size: 62.5%;
        }

        td.stylespc {
            padding-bottom: 20px;
            padding-right: 10px;
        }

        .style1 {
            width: 195px;
        }

        .style2 {
            width: 232px;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }
    </style>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <center>
                            <table>
                                <tr>
                                    <td align="center">
                                        <h2 class="text-center">Login Into FieldForce</h2>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnsubmit" runat="server" Text="Login" CssClass="savebutton" OnClick="btnsubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>            
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
</body>
</html>
